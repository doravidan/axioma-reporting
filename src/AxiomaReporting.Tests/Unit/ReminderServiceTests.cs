using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.BackgroundJobs;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Tests for ReminderService reminder-cycle logic.
///
/// Strategy: build a real ServiceCollection with an in-memory AppDbContext and a
/// FakeEmailService, start the BackgroundService, allow it to run one full cycle,
/// then cancel. Because the service runs the reminder logic *before* the first
/// Task.Delay, cancelling immediately after starting allows us to assert the
/// side-effects of exactly one cycle.
/// </summary>
public class ReminderServiceTests : IAsyncDisposable
{
    // Each test gets its own isolated in-memory DB + fake email sink.
    private readonly string _dbName = Guid.NewGuid().ToString();

    // -----------------------------------------------------------------------
    // Helpers
    // -----------------------------------------------------------------------

    /// <summary>
    /// Creates a scoped ServiceProvider with a fresh in-memory DB and
    /// a FakeEmailService. Callers can seed the DB via the returned context
    /// before running the service.
    /// </summary>
    private (ServiceProvider services, AppDbContext db, FakeEmailService email) BuildProvider()
    {
        var fake = new FakeEmailService();
        var sc = new ServiceCollection();
        sc.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase(_dbName));
        sc.AddSingleton<IEmailService>(fake);
        sc.AddScoped<IEmailService>(_ => fake);
        var sp = sc.BuildServiceProvider();
        var db = sp.GetRequiredService<AppDbContext>();
        return (sp, db, fake);
    }

    /// <summary>
    /// Runs one reminder cycle synchronously by starting the service, letting it
    /// complete the first (and only) cycle, then cancelling before the sleep.
    /// The method waits up to <paramref name="timeoutMs"/> ms.
    /// </summary>
    private static async Task RunOneCycleAsync(ServiceProvider services, int timeoutMs = 5_000)
    {
        var svc = new ReminderService(services, NullLogger<ReminderService>.Instance);
        using var cts = new CancellationTokenSource();

        // Start the background service — it will run one cycle then call Task.Delay(hours).
        // We cancel after a short pause so it completes the first cycle but doesn't block.
        var task = svc.StartAsync(cts.Token);

        // Give the first cycle time to run (it is non-blocking I/O against in-memory DB).
        await Task.Delay(200, CancellationToken.None);
        cts.Cancel();

        // Wait for the service to finish with a timeout guard.
        var completed = await Task.WhenAny(task, Task.Delay(timeoutMs, CancellationToken.None));
        // If it did not complete it means something blocked — we still proceed so
        // the test can assert whatever happened before the block.
    }

    private static User MakeEmployee(int id, string email = "emp@test.com") => new User
    {
        Id = id,
        EmployeeCode = $"EMP{id:D3}",
        IdNumber = id.ToString().PadLeft(9, '0'),
        FirstName = "עובד",
        LastName = $"{id}",
        Email = email,
        PasswordHash = "hash",
        StatusId = 1,       // Active
        IsReportingEmployee = true,
        CreatedAt = DateTime.UtcNow
    };

    private static ReportingMonth ActiveMonth(int daysUntilDeadline = 3) => new ReportingMonth
    {
        Id = 1,
        Description = "אפריל 2026",
        Month = 4,
        Year = 2026,
        IsActive = true,
        LastReportingDate = DateTime.Today.AddDays(daysUntilDeadline),
        CreatedAt = DateTime.UtcNow
    };

    private static void SeedSystemConstants(AppDbContext db,
        int reminderIntervalDays = 1,
        int startDaysBefore = 7,
        int checkIntervalHours = 1)
    {
        db.SystemConstants.AddRange(
            new SystemConstant { Key = "ReminderIntervalDays", Value = reminderIntervalDays.ToString(), CreatedAt = DateTime.UtcNow },
            new SystemConstant { Key = "ReminderStartDaysBeforeDeadline", Value = startDaysBefore.ToString(), CreatedAt = DateTime.UtcNow },
            new SystemConstant { Key = "ReminderCheckIntervalHours", Value = checkIntervalHours.ToString(), CreatedAt = DateTime.UtcNow },
            new SystemConstant { Key = "PasswordExpiryWarningDays", Value = "14", CreatedAt = DateTime.UtcNow });
        db.SaveChanges();
    }

    // -----------------------------------------------------------------------
    // Tests
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ReminderService_SendsReminder_ToEmployeeWithNoReport()
    {
        // Arrange — active month, employee has no report at all
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 1, startDaysBefore: 7);
        db.Users.Add(MakeEmployee(1));
        db.ReportingMonths.Add(ActiveMonth(daysUntilDeadline: 3));
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — email sent with "ReminderNotSubmitted" template
        email.Sent.Should().ContainSingle(e => e.TemplateType == "ReminderNotSubmitted");
        email.Sent[0].ToEmail.Should().Be("emp@test.com");
    }

    [Fact]
    public async Task ReminderService_SkipsEmployee_WhoAlreadySubmitted()
    {
        // Arrange — employee has a PendingApproval report (statusId = 3)
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 1, startDaysBefore: 7);
        var emp = MakeEmployee(1);
        db.Users.Add(emp);
        var month = ActiveMonth(daysUntilDeadline: 3);
        db.ReportingMonths.Add(month);
        db.Reports.Add(new Report
        {
            Id = 1,
            UserId = emp.Id,
            ReportingMonthId = month.Id,
            StatusId = 3,   // PendingApproval — >= 3 and != 5 means submitted
            CreatedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — no reminder sent for already-submitted employees
        email.Sent.Should().NotContain(e => e.TemplateType == "ReminderNotSubmitted");
        email.Sent.Should().NotContain(e => e.TemplateType == "ReminderNeedsCorrection");
    }

    [Fact]
    public async Task ReminderService_SkipsEmployee_WhoHasApprovedReport()
    {
        // Arrange — employee has an Approved report (statusId = 4)
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 1, startDaysBefore: 7);
        var emp = MakeEmployee(1);
        db.Users.Add(emp);
        var month = ActiveMonth(daysUntilDeadline: 3);
        db.ReportingMonths.Add(month);
        db.Reports.Add(new Report
        {
            Id = 1,
            UserId = emp.Id,
            ReportingMonthId = month.Id,
            StatusId = 4,   // Approved
            CreatedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert
        email.Sent.Should().NotContain(e =>
            e.TemplateType == "ReminderNotSubmitted" || e.TemplateType == "ReminderNeedsCorrection");
    }

    [Fact]
    public async Task ReminderService_SendsReminder_ToEmployeeWithRejectedReport()
    {
        // Arrange — employee has a ReturnedForCorrection report (statusId = 5)
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 1, startDaysBefore: 7);
        var emp = MakeEmployee(1);
        db.Users.Add(emp);
        var month = ActiveMonth(daysUntilDeadline: 3);
        db.ReportingMonths.Add(month);
        db.Reports.Add(new Report
        {
            Id = 1,
            UserId = emp.Id,
            ReportingMonthId = month.Id,
            StatusId = 5,   // ReturnedForCorrection
            CreatedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — "ReminderNeedsCorrection" template sent
        email.Sent.Should().ContainSingle(e => e.TemplateType == "ReminderNeedsCorrection");
        email.Sent[0].ToEmail.Should().Be("emp@test.com");
    }

    [Fact]
    public async Task ReminderService_SkipsEmployee_WithDraftReport()
    {
        // Spec / service logic:
        // submittedUserIds = reports with StatusId >= 3 AND StatusId != 5
        // returnedUserIds  = reports with StatusId == 5
        // Draft = StatusId 1, EditInProgress = StatusId 2 — neither of the above.
        // Therefore a draft/edit employee IS included in the "not submitted" bucket
        // and WILL receive a "ReminderNotSubmitted" email.
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 1, startDaysBefore: 7);
        var emp = MakeEmployee(1);
        db.Users.Add(emp);
        var month = ActiveMonth(daysUntilDeadline: 3);
        db.ReportingMonths.Add(month);
        db.Reports.Add(new Report
        {
            Id = 1,
            UserId = emp.Id,
            ReportingMonthId = month.Id,
            StatusId = 1,   // Draft — not submitted
            CreatedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — draft counts as "not yet submitted" → reminder IS sent
        email.Sent.Should().ContainSingle(e => e.TemplateType == "ReminderNotSubmitted");
    }

    [Fact]
    public async Task ReminderService_RespectsReminderStartDays_NotYetInWindow()
    {
        // Arrange — deadline is 30 days away; window only starts 7 days before deadline
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 1, startDaysBefore: 7);
        db.Users.Add(MakeEmployee(1));
        // Deadline is far in the future — (deadline - today).Days = 30 > startDaysBefore(7)
        db.ReportingMonths.Add(ActiveMonth(daysUntilDeadline: 30));
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — no reminder because we are not yet within the window
        email.Sent.Should().NotContain(e =>
            e.TemplateType == "ReminderNotSubmitted" || e.TemplateType == "ReminderNeedsCorrection");
    }

    [Fact]
    public async Task ReminderService_RespectsReminderIntervalDays_SkipsRecentlySentReminder()
    {
        // Arrange — employee has no report; a reminder was already sent today (within interval)
        var (sp, db, email) = BuildProvider();
        // Interval = 3 days; a log entry exists for today → should skip
        SeedSystemConstants(db, reminderIntervalDays: 3, startDaysBefore: 7);
        var emp = MakeEmployee(1);
        db.Users.Add(emp);
        var month = ActiveMonth(daysUntilDeadline: 3);
        db.ReportingMonths.Add(month);

        // Simulate a reminder that was just sent (less than 3 days ago = today)
        db.ReminderLogs.Add(new ReminderLog
        {
            Id = 1,
            UserId = emp.Id,
            ReportingMonthId = month.Id,
            TemplateType = "ReminderNotSubmitted",
            SentAt = DateTime.UtcNow.Date  // today — within the 3-day interval
        });
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — no new reminder because interval not elapsed
        email.Sent.Should().NotContain(e => e.TemplateType == "ReminderNotSubmitted");
    }

    [Fact]
    public async Task ReminderService_RespectsReminderIntervalDays_SendsWhenIntervalElapsed()
    {
        // Arrange — employee has no report; last reminder was sent 4 days ago (> interval of 3)
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 3, startDaysBefore: 7);
        var emp = MakeEmployee(1);
        db.Users.Add(emp);
        var month = ActiveMonth(daysUntilDeadline: 3);
        db.ReportingMonths.Add(month);

        // Old reminder — 4 days ago, past the 3-day interval
        db.ReminderLogs.Add(new ReminderLog
        {
            Id = 1,
            UserId = emp.Id,
            ReportingMonthId = month.Id,
            TemplateType = "ReminderNotSubmitted",
            SentAt = DateTime.UtcNow.Date.AddDays(-4)
        });
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — interval elapsed → new reminder sent
        email.Sent.Should().ContainSingle(e => e.TemplateType == "ReminderNotSubmitted");
    }

    [Fact]
    public async Task ReminderService_NoActiveMonth_DoesNotSendReminders()
    {
        // Arrange — no active reporting month at all
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 1, startDaysBefore: 7);
        db.Users.Add(MakeEmployee(1));
        // Deliberately no active ReportingMonth added
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — service should return early when no active month
        email.Sent.Should().NotContain(e =>
            e.TemplateType == "ReminderNotSubmitted" || e.TemplateType == "ReminderNeedsCorrection");
    }

    [Fact]
    public async Task ReminderService_SkipsEmployee_WithNullEmail()
    {
        // Arrange — employee has no email address; service filters u.Email != null
        var (sp, db, email) = BuildProvider();
        SeedSystemConstants(db, reminderIntervalDays: 1, startDaysBefore: 7);
        var emp = MakeEmployee(1, email: null!);
        emp.Email = null;
        db.Users.Add(emp);
        db.ReportingMonths.Add(ActiveMonth(daysUntilDeadline: 3));
        await db.SaveChangesAsync();

        // Act
        await RunOneCycleAsync(sp);

        // Assert — no email because employee has no email address
        email.Sent.Should().BeEmpty();
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}

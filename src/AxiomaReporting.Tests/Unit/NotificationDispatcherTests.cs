using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.BackgroundJobs;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;

namespace AxiomaReporting.Tests.Unit;

public class NotificationDispatcherTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly EmailTemplateRenderer _renderer;

  public NotificationDispatcherTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);

    _db.EmailServerSettings.Add(new EmailServerSetting
    {
      SmtpServer = "smtp.example.test",
      Port = 587,
      Username = "user",
      Password = "pass",
      FromAddress = "noreply@example.test",
      FromName = "Test",
      UseSsl = false,
      CreatedAt = DateTime.UtcNow
    });
    _db.EmailTemplates.Add(new EmailTemplate
    {
      TypeDescription = "ReportApproved",
      Subject = "דיווח אושר {EmployeeName}",
      Body = "שלום {EmployeeName}, דיווחך לחודש {MonthName} אושר.",
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    });
    _db.EmailTemplates.Add(new EmailTemplate
    {
      TypeDescription = "PasswordReset",
      Subject = "איפוס סיסמה",
      Body = "שלום {EmployeeName}, לאיפוס הסיסמה לחץ כאן: {ResetLink}",
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    });
    _db.SaveChanges();

    _renderer = new EmailTemplateRenderer(_db);
  }

  public void Dispose() => _db.Dispose();

  private NotificationDispatcher CreateDispatcher(ISmtpSender smtp) =>
    new(_db, smtp, _renderer, NullLogger<NotificationDispatcher>.Instance);

  [Fact]
  public async Task NotificationDispatcher_SuccessfulSend_WritesSentLog()
  {
    var smtp = new FakeSmtpSender();
    var sut = CreateDispatcher(smtp);

    await sut.SendAsync("user@example.test", "Test User", "ReportApproved",
      new Dictionary<string, string>
      {
        ["EmployeeName"] = "דני",
        ["MonthName"] = "ינואר 2026"
      });

    var log = await _db.NotificationLogs.SingleAsync();
    log.Status.Should().Be("Sent");
    log.AttemptCount.Should().Be(1);
    log.LastAttemptAt.Should().NotBeNull();
    log.NextRetryAt.Should().BeNull();
    log.FailureReason.Should().BeNull();
    log.NotificationType.Should().Be("Report");
    log.TemplateType.Should().Be("ReportApproved");
    log.RecipientEmail.Should().Be("user@example.test");
    log.Subject.Should().Contain("דני");
    log.Body.Should().Contain("ינואר 2026");

    smtp.Sent.Should().HaveCount(1);
  }

  [Fact]
  public async Task NotificationDispatcher_PasswordResetSuccessfulSend_WritesSentLog()
  {
    var smtp = new FakeSmtpSender();
    var sut = CreateDispatcher(smtp);

    await sut.SendAsync("user@example.test", "Test User", "PasswordReset",
      new Dictionary<string, string>
      {
        ["EmployeeName"] = "דני",
        ["ResetLink"] = "https://example.test/reset"
      });

    var log = await _db.NotificationLogs.SingleAsync();
    log.Status.Should().Be("Sent");
    log.TemplateType.Should().Be("PasswordReset");
    log.NotificationType.Should().Be("Account");
    log.AttemptCount.Should().Be(1);
    log.FailureReason.Should().BeNull();
    smtp.Sent.Should().ContainSingle();
  }

  [Fact]
  public async Task EmailTemplateRenderer_PasswordResetLink_RendersClickableAnchorWithoutTrailingPunctuation()
  {
    var template = await _db.EmailTemplates.SingleAsync(t => t.TypeDescription == "PasswordReset");
    template.Body = "לאיפוס הסיסמה לחץ כאן: ({ResetLink})";
    await _db.SaveChangesAsync();

    var resetLink = "https://example.test/Account/ResetPassword?token=abc-123";

    var rendered = await _renderer.RenderAsync("PasswordReset", new Dictionary<string, string>
    {
      ["ResetLink"] = resetLink
    });

    var encoded = WebUtility.HtmlEncode(resetLink);
    rendered.Should().NotBeNull();
    rendered!.BodyHtml.Should().Contain($"href=\"{encoded}\"");
    rendered.BodyHtml.Should().NotContain($"href=\"{encoded})\"");
    rendered.BodyHtml.Should().Contain($"(<a href=\"{encoded}\"");
  }

  [Fact]
  public async Task NotificationDispatcher_FailedSend_WritesFailedLogWithNextRetry_AndDoesNotThrow()
  {
    var smtp = new FakeSmtpSender { ThrowOnSend = true };
    var sut = CreateDispatcher(smtp);

    Func<Task> act = () => sut.SendAsync("user@example.test", "Test User", "ReportApproved",
      new Dictionary<string, string> { ["EmployeeName"] = "דני", ["MonthName"] = "ינואר" });

    await act.Should().NotThrowAsync();

    var log = await _db.NotificationLogs.SingleAsync();
    log.Status.Should().Be("Failed");
    log.AttemptCount.Should().Be(1);
    log.LastAttemptAt.Should().NotBeNull();
    log.NextRetryAt.Should().NotBeNull();
    log.NextRetryAt!.Value.Should().BeAfter(DateTime.UtcNow);
    log.FailureReason.Should().NotBeNullOrEmpty();
  }

  [Fact]
  public void MapNotificationType_ClassifiesByTemplatePrefix()
  {
    NotificationDispatcher.MapNotificationType("ReportReceived").Should().Be("Report");
    NotificationDispatcher.MapNotificationType("ReminderNotSubmitted").Should().Be("Reminder");
    NotificationDispatcher.MapNotificationType("PasswordReset").Should().Be("Account");
    NotificationDispatcher.MapNotificationType("TwoFactorCode").Should().Be("Account");
    NotificationDispatcher.MapNotificationType("PasswordExpiryWarning").Should().Be("Account");
    NotificationDispatcher.MapNotificationType("BatchImportErrors").Should().Be("Excel");
    NotificationDispatcher.MapNotificationType("SomethingElse").Should().Be("Other");
  }

  [Fact]
  public void BackoffPolicy_FollowsExponentialSchedule()
  {
    NotificationRetryPolicy.Backoff(1).Should().Be(TimeSpan.FromMinutes(5));
    NotificationRetryPolicy.Backoff(2).Should().Be(TimeSpan.FromMinutes(10));
    NotificationRetryPolicy.Backoff(3).Should().Be(TimeSpan.FromMinutes(20));
    NotificationRetryPolicy.Backoff(4).Should().Be(TimeSpan.FromMinutes(40));
    NotificationRetryPolicy.Backoff(5).Should().Be(TimeSpan.FromMinutes(60));
    NotificationRetryPolicy.Backoff(6).Should().Be(TimeSpan.FromMinutes(60));
  }

  [Fact]
  public async Task NotificationRetryService_PicksFailedRow_Retries_OnSuccess_MarksSent()
  {
    _db.NotificationLogs.Add(new NotificationLog
    {
      NotificationType = "Report",
      TemplateType = "ReportApproved",
      RecipientEmail = "user@example.test",
      Subject = "subj",
      Body = "<p>body</p>",
      Status = "Failed",
      AttemptCount = 1,
      LastAttemptAt = DateTime.UtcNow.AddMinutes(-10),
      NextRetryAt = DateTime.UtcNow.AddMinutes(-1),
      FailureReason = "boom",
      CreatedAt = DateTime.UtcNow.AddMinutes(-15)
    });
    await _db.SaveChangesAsync();

    var smtp = new FakeSmtpSender();
    using var provider = BuildRetryProvider(_db, smtp);
    var svc = new NotificationRetryService(provider, NullLogger<NotificationRetryService>.Instance);

    await svc.RunCycleAsync(CancellationToken.None);

    var log = await _db.NotificationLogs.SingleAsync();
    log.Status.Should().Be("Sent");
    log.AttemptCount.Should().Be(2);
    log.FailureReason.Should().BeNull();
    log.NextRetryAt.Should().BeNull();
    smtp.Sent.Should().HaveCount(1);
  }

  [Fact]
  public async Task NotificationRetryService_AfterMaxAttempts_MarksAbandoned()
  {
    _db.NotificationLogs.Add(new NotificationLog
    {
      NotificationType = "Report",
      TemplateType = "ReportApproved",
      RecipientEmail = "user@example.test",
      Subject = "subj",
      Body = "<p>body</p>",
      Status = "Failed",
      AttemptCount = 4, // one more attempt = 5 = MaxAttempts
      LastAttemptAt = DateTime.UtcNow.AddMinutes(-10),
      NextRetryAt = DateTime.UtcNow.AddMinutes(-1),
      FailureReason = "boom",
      CreatedAt = DateTime.UtcNow.AddHours(-1)
    });
    await _db.SaveChangesAsync();

    var smtp = new FakeSmtpSender { ThrowOnSend = true };
    using var provider = BuildRetryProvider(_db, smtp);
    var svc = new NotificationRetryService(provider, NullLogger<NotificationRetryService>.Instance);

    await svc.RunCycleAsync(CancellationToken.None);

    var log = await _db.NotificationLogs.SingleAsync();
    log.Status.Should().Be("Abandoned");
    log.AttemptCount.Should().Be(5);
    log.NextRetryAt.Should().BeNull();
    log.FailureReason.Should().NotBeNullOrEmpty();
  }

  [Fact]
  public async Task NotificationRetryService_SkipsRowsWithFutureNextRetryAt()
  {
    _db.NotificationLogs.Add(new NotificationLog
    {
      NotificationType = "Report",
      TemplateType = "ReportApproved",
      RecipientEmail = "user@example.test",
      Subject = "subj",
      Body = "<p>body</p>",
      Status = "Failed",
      AttemptCount = 1,
      NextRetryAt = DateTime.UtcNow.AddHours(1),
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    var smtp = new FakeSmtpSender();
    using var provider = BuildRetryProvider(_db, smtp);
    var svc = new NotificationRetryService(provider, NullLogger<NotificationRetryService>.Instance);

    await svc.RunCycleAsync(CancellationToken.None);

    smtp.Sent.Should().BeEmpty();
    var log = await _db.NotificationLogs.SingleAsync();
    log.Status.Should().Be("Failed");
    log.AttemptCount.Should().Be(1);
  }

  private static ServiceProvider BuildRetryProvider(AppDbContext db, ISmtpSender smtp)
  {
    var services = new ServiceCollection();
    services.AddSingleton(db);
    services.AddSingleton(smtp);
    return services.BuildServiceProvider();
  }

  [Fact]
  public async Task AdminNotificationLogs_Resend_RequeuesRow()
  {
    _db.NotificationLogs.Add(new NotificationLog
    {
      Id = 42,
      NotificationType = "Report",
      TemplateType = "ReportApproved",
      RecipientEmail = "user@example.test",
      Subject = "subj",
      Body = "<p>body</p>",
      Status = "Abandoned",
      AttemptCount = 5,
      LastAttemptAt = DateTime.UtcNow.AddHours(-1),
      NextRetryAt = null,
      FailureReason = "previous boom",
      CreatedAt = DateTime.UtcNow.AddHours(-2)
    });
    await _db.SaveChangesAsync();

    var sut = AdminControllerFactory.Create(_db);

    var result = await sut.ResendNotification(42);

    result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
    var log = await _db.NotificationLogs.SingleAsync();
    log.Status.Should().Be("Pending");
    log.AttemptCount.Should().Be(0);
    log.FailureReason.Should().BeNull();
    log.NextRetryAt.Should().NotBeNull();
    log.NextRetryAt!.Value.Should().BeOnOrBefore(DateTime.UtcNow.AddSeconds(1));

    // Retry service should now find and send the row
    var smtp = new FakeSmtpSender();
    using var provider = BuildRetryProvider(_db, smtp);
    var retry = new NotificationRetryService(provider, NullLogger<NotificationRetryService>.Instance);
    await retry.RunCycleAsync(CancellationToken.None);

    smtp.Sent.Should().HaveCount(1);
    (await _db.NotificationLogs.SingleAsync()).Status.Should().Be("Sent");
  }
}

internal sealed class FakeSmtpSender : ISmtpSender
{
  private readonly List<(string Email, string Subject)> _sent = new();
  public IReadOnlyList<(string Email, string Subject)> Sent => _sent;
  public bool ThrowOnSend { get; set; }

  public Task SendRenderedAsync(
    string toEmail,
    string toName,
    string subject,
    string bodyHtml,
    IReadOnlyList<EmailAttachment>? attachments = null,
    CancellationToken cancellationToken = default)
  {
    if (ThrowOnSend)
      throw new InvalidOperationException("simulated smtp failure");
    _sent.Add((toEmail, subject));
    return Task.CompletedTask;
  }
}

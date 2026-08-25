using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Infrastructure.BackgroundJobs;

public class ReminderService : BackgroundService
{
  private readonly IServiceProvider _services;
  private readonly ILogger<ReminderService> _logger;
  public ReminderService(IServiceProvider services, ILogger<ReminderService> logger)
  {
    _services = services;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _logger.LogInformation("ReminderService started");

    while (!stoppingToken.IsCancellationRequested)
    {
      int checkIntervalHours;
      try
      {
        (checkIntervalHours, _) = await RunReminderCycleAsync(stoppingToken);
      }
      catch (Exception ex) when (ex is not OperationCanceledException)
      {
        _logger.LogError(ex, "ReminderService: Unhandled error in reminder cycle");
        checkIntervalHours = 1;
      }

      await Task.Delay(TimeSpan.FromHours(checkIntervalHours), stoppingToken);
    }
  }

  private async Task<(int checkIntervalHours, int processed)> RunReminderCycleAsync(CancellationToken stoppingToken)
  {
    using var scope = _services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

    var constants = await db.SystemConstants.ToListAsync(stoppingToken);
    var reminderInterval = GetConstant(constants, "ReminderIntervalDays", 3);
    var startDaysBefore = GetConstant(constants, "ReminderStartDaysBeforeDeadline", 7);
    var checkIntervalHours = GetConstant(constants, "ReminderCheckIntervalHours", 1);
    var passwordExpiryWarningDays = GetConstant(constants, "PasswordExpiryWarningDays", 14);

    // Password expiry warnings run regardless of active reporting month
    await SendPasswordExpiryWarningsAsync(db, emailService, passwordExpiryWarningDays, stoppingToken);

    var activeMonth = await db.ReportingMonths.FirstOrDefaultAsync(m => m.IsActive, stoppingToken);
    if (activeMonth == null)
    {
      _logger.LogDebug("ReminderService: No active reporting month");
      return (checkIntervalHours, 0);
    }

    var today = DateTime.Today;
    var deadline = activeMonth.LastReportingDate.Date;
    if ((deadline - today).Days > startDaysBefore)
    {
      _logger.LogDebug("ReminderService: Not yet within reminder window");
      return (checkIntervalHours, 0);
    }

    var reportingEmployees = await db.Users
      .Where(u => u.IsReportingEmployee && u.StatusId == 1 && u.Email != null)
      .ToListAsync(stoppingToken);

    var submittedUserIds = await db.Reports
      .Where(r => !r.IsArchived && r.ReportingMonthId == activeMonth.Id && r.StatusId >= 3 && r.StatusId != 5)
      .Select(r => r.UserId)
      .ToListAsync(stoppingToken);

    var returnedUserIds = await db.Reports
      .Where(r => !r.IsArchived && r.ReportingMonthId == activeMonth.Id && r.StatusId == 5)
      .Select(r => r.UserId)
      .ToListAsync(stoppingToken);

    foreach (var employee in reportingEmployees)
    {
      if (stoppingToken.IsCancellationRequested) break;

      string templateType;
      if (returnedUserIds.Contains(employee.Id))
        templateType = "ReminderNeedsCorrection";
      else if (!submittedUserIds.Contains(employee.Id))
        templateType = "ReminderNotSubmitted";
      else
        continue;

      if (!await ShouldSendReminderAsync(db, employee.Id, activeMonth.Id, templateType, reminderInterval, stoppingToken))
        continue;

      await emailService.SendAsync(
        employee.Email!,
        $"{employee.FirstName} {employee.LastName}",
        templateType,
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{employee.FirstName} {employee.LastName}",
          ["MonthName"] = activeMonth.Description,
          ["Month"] = activeMonth.Month.ToString(),
          ["Year"] = activeMonth.Year.ToString(),
          ["DeadlineDate"] = deadline.ToString("dd/MM/yyyy"),
          ["Deadline"] = deadline.ToString("dd/MM/yyyy")
        },
        cancellationToken: stoppingToken);

      db.ReminderLogs.Add(new ReminderLog
      {
        UserId = employee.Id,
        ReportingMonthId = activeMonth.Id,
        TemplateType = templateType,
        SentAt = DateTime.UtcNow
      });
      await db.SaveChangesAsync(stoppingToken);

      _logger.LogInformation("ReminderService: Sent {Template} to {Email}", templateType, employee.Email);
    }

    return (checkIntervalHours, reportingEmployees.Count);
  }

  private async Task SendPasswordExpiryWarningsAsync(
    AppDbContext db,
    IEmailService emailService,
    int warningDays,
    CancellationToken stoppingToken)
  {
    var today = DateTime.UtcNow.Date;
    var expiryThreshold = today.AddDays(warningDays);

    // Find active users whose password was last changed more than (90 - warningDays) days ago
    // Password rotation is 90 days; PasswordChangedAt tracks last change
    var usersNearExpiry = await db.Users
      .Where(u => u.StatusId == 1 &&
                  u.Email != null &&
                  u.LastPasswordChange != null &&
                  u.LastPasswordChange.Value.AddDays(90) <= expiryThreshold &&
                  u.LastPasswordChange.Value.AddDays(90) > today)
      .ToListAsync(stoppingToken);

    foreach (var user in usersNearExpiry)
    {
      if (stoppingToken.IsCancellationRequested) break;

      var expiryDate = user.LastPasswordChange!.Value.AddDays(90);
      var daysLeft = (expiryDate.Date - today).Days;

      // Only send once per expiry period (check if already warned this cycle)
      var alreadyWarned = await db.ReminderLogs
        .AnyAsync(l => l.UserId == user.Id &&
                       l.TemplateType == "PasswordExpiryWarning" &&
                       l.SentAt >= today.AddDays(-1),
                  stoppingToken);
      if (alreadyWarned) continue;

      await emailService.SendAsync(
        user.Email!,
        $"{user.FirstName} {user.LastName}",
        "PasswordExpiryWarning",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{user.FirstName} {user.LastName}",
          ["DaysLeft"] = daysLeft.ToString(),
          ["ExpiryDate"] = expiryDate.ToString("dd/MM/yyyy")
        },
        cancellationToken: stoppingToken);

      db.ReminderLogs.Add(new ReminderLog
      {
        UserId = user.Id,
        ReportingMonthId = null,
        TemplateType = "PasswordExpiryWarning",
        SentAt = DateTime.UtcNow
      });
      await db.SaveChangesAsync(stoppingToken);

      _logger.LogInformation(
        "ReminderService: Sent PasswordExpiryWarning to {Email} ({DaysLeft} days left)",
        user.Email, daysLeft);
    }
  }

  private static async Task<bool> ShouldSendReminderAsync(
    AppDbContext db,
    int userId,
    int reportingMonthId,
    string templateType,
    int reminderIntervalDays,
    CancellationToken cancellationToken)
  {
    var lastSent = await db.ReminderLogs
      .Where(l => l.UserId == userId &&
                  l.ReportingMonthId == reportingMonthId &&
                  l.TemplateType == templateType)
      .OrderByDescending(l => l.SentAt)
      .Select(l => (DateTime?)l.SentAt)
      .FirstOrDefaultAsync(cancellationToken);

    return !lastSent.HasValue || lastSent.Value.Date <= DateTime.UtcNow.Date.AddDays(-reminderIntervalDays);
  }

  private static int GetConstant(IEnumerable<SystemConstant> constants, string key, int fallback)
  {
    var value = constants.FirstOrDefault(c => c.Key == key)?.Value;
    return int.TryParse(value, out var parsed) ? parsed : fallback;
  }
}

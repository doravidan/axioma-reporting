using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Infrastructure.BackgroundJobs;

/// <summary>
/// Scans NotificationLogs every 5 minutes for Failed rows whose NextRetryAt has elapsed
/// and re-attempts delivery with exponential backoff. Abandons after MaxAttempts.
/// </summary>
public class NotificationRetryService : BackgroundService
{
  private static readonly TimeSpan TickInterval = TimeSpan.FromMinutes(5);
  private const int BatchSize = 50;

  private readonly IServiceProvider _services;
  private readonly ILogger<NotificationRetryService> _logger;

  public NotificationRetryService(IServiceProvider services, ILogger<NotificationRetryService> logger)
  {
    _services = services;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _logger.LogInformation("NotificationRetryService started");

    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        await RunCycleAsync(stoppingToken);
      }
      catch (Exception ex) when (ex is not OperationCanceledException)
      {
        _logger.LogError(ex, "NotificationRetryService: Unhandled error in retry cycle");
      }

      try
      {
        await Task.Delay(TickInterval, stoppingToken);
      }
      catch (OperationCanceledException) { }
    }
  }

  public async Task<int> RunCycleAsync(CancellationToken stoppingToken)
  {
    using var scope = _services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var smtp = scope.ServiceProvider.GetRequiredService<ISmtpSender>();

    var now = DateTime.UtcNow;
    var candidates = await db.NotificationLogs
      .Where(n =>
        (n.Status == "Failed" || n.Status == "Pending") &&
        n.AttemptCount < NotificationRetryPolicy.MaxAttempts &&
        (n.NextRetryAt == null || n.NextRetryAt <= now))
      .OrderBy(n => n.NextRetryAt ?? n.CreatedAt)
      .Take(BatchSize)
      .ToListAsync(stoppingToken);

    foreach (var log in candidates)
    {
      if (stoppingToken.IsCancellationRequested) break;
      await RetryOneAsync(db, smtp, log, stoppingToken);
    }

    return candidates.Count;
  }

  private async Task RetryOneAsync(AppDbContext db, ISmtpSender smtp, NotificationLog log, CancellationToken stoppingToken)
  {
    var attempt = log.AttemptCount + 1;
    log.LastAttemptAt = DateTime.UtcNow;
    log.AttemptCount = attempt;

    try
    {
      await smtp.SendRenderedAsync(
        log.RecipientEmail,
        log.RecipientUser != null
          ? $"{log.RecipientUser.FirstName} {log.RecipientUser.LastName}"
          : string.Empty,
        log.Subject,
        log.Body,
        attachments: null,
        stoppingToken);

      log.Status = "Sent";
      log.FailureReason = null;
      log.NextRetryAt = null;
      _logger.LogInformation(
        "NotificationRetryService: Resent notification {Id} to {Email} on attempt {Attempt}",
        log.Id, log.RecipientEmail, attempt);
    }
    catch (Exception ex) when (ex is not OperationCanceledException)
    {
      log.FailureReason = Truncate(ex.Message, 2000);
      if (attempt >= NotificationRetryPolicy.MaxAttempts)
      {
        log.Status = "Abandoned";
        log.NextRetryAt = null;
        _logger.LogWarning(ex,
          "NotificationRetryService: Notification {Id} abandoned after {Attempt} attempts",
          log.Id, attempt);
      }
      else
      {
        log.Status = "Failed";
        log.NextRetryAt = DateTime.UtcNow.Add(NotificationRetryPolicy.Backoff(attempt));
        _logger.LogWarning(ex,
          "NotificationRetryService: Retry {Attempt} failed for notification {Id}; next attempt at {NextRetryAt:o}",
          attempt, log.Id, log.NextRetryAt);
      }
    }

    await db.SaveChangesAsync(stoppingToken);
  }

  private static string Truncate(string value, int max) =>
    value.Length <= max ? value : value.Substring(0, max);
}

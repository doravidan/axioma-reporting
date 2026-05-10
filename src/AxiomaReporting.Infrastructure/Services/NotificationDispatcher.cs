using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// IEmailService implementation that persists every send attempt to NotificationLogs
/// and cooperates with NotificationRetryService for failed messages.
/// Never throws — callers treat email failure as soft.
/// </summary>
public class NotificationDispatcher : IEmailService
{
  private readonly AppDbContext _db;
  private readonly ISmtpSender _smtp;
  private readonly EmailTemplateRenderer _renderer;
  private readonly ILogger<NotificationDispatcher> _logger;

  public NotificationDispatcher(
    AppDbContext db,
    ISmtpSender smtp,
    EmailTemplateRenderer renderer,
    ILogger<NotificationDispatcher> logger)
  {
    _db = db;
    _smtp = smtp;
    _renderer = renderer;
    _logger = logger;
  }

  public async Task SendAsync(
    string toEmail,
    string toName,
    string templateType,
    Dictionary<string, string> tokens,
    IReadOnlyList<EmailAttachment>? attachments = null,
    CancellationToken cancellationToken = default)
  {
    if (string.IsNullOrWhiteSpace(toEmail))
    {
      _logger.LogWarning("EMAIL: No email address for recipient {Name}, skipping", toName);
      return;
    }

    var rendered = await _renderer.RenderAsync(templateType, tokens, cancellationToken);
    if (rendered == null)
    {
      _logger.LogWarning("EMAIL: Template {Type} not found", templateType);
      return;
    }

    var recipientUserId = TryResolveUserId(tokens);
    var log = new NotificationLog
    {
      NotificationType = MapNotificationType(templateType),
      TemplateType = templateType,
      RecipientUserId = recipientUserId,
      RecipientEmail = toEmail,
      RelatedReportId = TryParseInt(tokens, "ReportId"),
      RelatedReportingMonthId = TryParseInt(tokens, "ReportingMonthId"),
      Subject = rendered.Subject,
      Body = rendered.BodyHtml,
      Status = "Pending",
      AttemptCount = 0,
      CreatedAt = DateTime.UtcNow
    };
    _db.NotificationLogs.Add(log);
    await _db.SaveChangesAsync(cancellationToken);

    try
    {
      await _smtp.SendRenderedAsync(toEmail, toName, rendered.Subject, rendered.BodyHtml, attachments, cancellationToken);
      log.Status = "Sent";
      log.AttemptCount = 1;
      log.LastAttemptAt = DateTime.UtcNow;
      log.FailureReason = null;
      log.NextRetryAt = null;
      await _db.SaveChangesAsync(cancellationToken);
    }
    catch (Exception ex) when (ex is not OperationCanceledException)
    {
      log.Status = "Failed";
      log.AttemptCount = 1;
      log.LastAttemptAt = DateTime.UtcNow;
      log.FailureReason = Truncate(ex.Message, 2000);
      log.NextRetryAt = DateTime.UtcNow.Add(NotificationRetryPolicy.Backoff(1));
      await _db.SaveChangesAsync(cancellationToken);
      _logger.LogError(ex, "EMAIL: Failed to send {Type} to {Email}; queued for retry", templateType, toEmail);
      // Do NOT rethrow — email failure is soft.
    }
  }

  public static string MapNotificationType(string templateType)
  {
    if (string.IsNullOrEmpty(templateType)) return "Other";
    if (templateType.StartsWith("Reminder", StringComparison.OrdinalIgnoreCase)) return "Reminder";
    if (templateType.StartsWith("Report", StringComparison.OrdinalIgnoreCase)) return "Report";
    if (templateType.Equals("PasswordReset", StringComparison.OrdinalIgnoreCase) ||
        templateType.Equals("TwoFactorCode", StringComparison.OrdinalIgnoreCase) ||
        templateType.Equals("PasswordExpiryWarning", StringComparison.OrdinalIgnoreCase))
      return "Account";
    if (templateType.StartsWith("BatchImport", StringComparison.OrdinalIgnoreCase)) return "Excel";
    return "Other";
  }

  private static int? TryParseInt(Dictionary<string, string> tokens, string key)
  {
    if (tokens.TryGetValue(key, out var raw) && int.TryParse(raw, out var value)) return value;
    return null;
  }

  private static int? TryResolveUserId(Dictionary<string, string> tokens) =>
    TryParseInt(tokens, "UserId") ?? TryParseInt(tokens, "RecipientUserId");

  private static string Truncate(string value, int max) =>
    value.Length <= max ? value : value.Substring(0, max);
}

public static class NotificationRetryPolicy
{
  public const int MaxAttempts = 5;

  // backoff(n) = min(60, 5 * 2^(n-1)) minutes — 5, 10, 20, 40, 60
  public static TimeSpan Backoff(int attempt)
  {
    var n = Math.Max(attempt, 1);
    var minutes = Math.Min(60, 5 * Math.Pow(2, n - 1));
    return TimeSpan.FromMinutes(minutes);
  }
}

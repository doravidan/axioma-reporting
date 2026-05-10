namespace AxiomaReporting.Core.Entities;

public class NotificationLog
{
  public int Id { get; set; }
  public string NotificationType { get; set; } = string.Empty;
  public string TemplateType { get; set; } = string.Empty;
  public int? RecipientUserId { get; set; }
  public User? RecipientUser { get; set; }
  public string RecipientEmail { get; set; } = string.Empty;
  public int? RelatedReportId { get; set; }
  public Report? RelatedReport { get; set; }
  public int? RelatedReportingMonthId { get; set; }
  public ReportingMonth? RelatedReportingMonth { get; set; }
  public string Subject { get; set; } = string.Empty;
  public string Body { get; set; } = string.Empty;
  public string Status { get; set; } = "Pending";
  public int AttemptCount { get; set; }
  public DateTime? LastAttemptAt { get; set; }
  public DateTime? NextRetryAt { get; set; }
  public string? FailureReason { get; set; }
  public DateTime CreatedAt { get; set; }
}

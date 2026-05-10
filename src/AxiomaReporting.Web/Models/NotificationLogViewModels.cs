namespace AxiomaReporting.Web.Models;

public class NotificationLogListItem
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public string NotificationType { get; set; } = string.Empty;
  public string TemplateType { get; set; } = string.Empty;
  public string RecipientEmail { get; set; } = string.Empty;
  public string? RecipientName { get; set; }
  public string Subject { get; set; } = string.Empty;
  public string Status { get; set; } = string.Empty;
  public int AttemptCount { get; set; }
  public DateTime? LastAttemptAt { get; set; }
  public DateTime? NextRetryAt { get; set; }
  public string? FailureReason { get; set; }
}

public class NotificationLogListViewModel
{
  public List<NotificationLogListItem> Items { get; set; } = new();
  public int Page { get; set; }
  public int PageSize { get; set; }
  public int TotalCount { get; set; }
  public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);

  public string? Type { get; set; }
  public string? Status { get; set; }
  public string? TemplateType { get; set; }
  public DateTime? FromDate { get; set; }
  public DateTime? ToDate { get; set; }
  public string? RecipientEmail { get; set; }
  public int? UserId { get; set; }
}

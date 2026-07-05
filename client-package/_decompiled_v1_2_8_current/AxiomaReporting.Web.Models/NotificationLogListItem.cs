using System;

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

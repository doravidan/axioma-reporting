using System;

namespace AxiomaReporting.Core.Entities;

public class ReminderLog
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public User? User { get; set; }

	public int? ReportingMonthId { get; set; }

	public ReportingMonth? ReportingMonth { get; set; }

	public string TemplateType { get; set; } = string.Empty;


	public DateTime SentAt { get; set; }
}

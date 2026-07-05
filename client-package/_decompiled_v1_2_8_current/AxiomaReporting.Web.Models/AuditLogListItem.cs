using System;

namespace AxiomaReporting.Web.Models;

public class AuditLogListItem
{
	public long Id { get; set; }

	public DateTime Timestamp { get; set; }

	public int? ActorUserId { get; set; }

	public string? ActorName { get; set; }

	public string Action { get; set; } = string.Empty;


	public string EntityType { get; set; } = string.Empty;


	public string? EntityId { get; set; }

	public string? Notes { get; set; }

	public string? IpAddress { get; set; }

	public string? UserAgent { get; set; }

	public string? Before { get; set; }

	public string? After { get; set; }
}

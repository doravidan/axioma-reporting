using System;

namespace AxiomaReporting.Core.Entities;

public class AuditLog
{
	public long Id { get; set; }

	public DateTime Timestamp { get; set; }

	public int? ActorUserId { get; set; }

	public User? ActorUser { get; set; }

	public string Action { get; set; } = string.Empty;


	public string EntityType { get; set; } = string.Empty;


	public string? EntityId { get; set; }

	public string? Before { get; set; }

	public string? After { get; set; }

	public string? IpAddress { get; set; }

	public string? UserAgent { get; set; }

	public string? Notes { get; set; }
}

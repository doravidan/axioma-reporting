using System;

namespace AxiomaReporting.Core.Entities;

public class TwoFactorCode
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public User? User { get; set; }

	public string CodeHash { get; set; } = string.Empty;


	public DateTime ExpiresAt { get; set; }

	public DateTime? UsedAt { get; set; }

	public DateTime CreatedAt { get; set; }
}

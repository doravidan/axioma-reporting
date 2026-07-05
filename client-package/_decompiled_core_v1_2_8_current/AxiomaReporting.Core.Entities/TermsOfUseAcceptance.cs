using System;

namespace AxiomaReporting.Core.Entities;

public class TermsOfUseAcceptance
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public User? User { get; set; }

	public int VersionId { get; set; }

	public TermsOfUseVersion? Version { get; set; }

	public DateTime AcceptedAt { get; set; }

	public string? IpAddress { get; set; }
}

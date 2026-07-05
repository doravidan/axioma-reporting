using System;
using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class PrivacyPolicyVersion : BaseEntity
{
	public int VersionNumber { get; set; }

	public string BodyHtml { get; set; } = string.Empty;

	public DateTime EffectiveFrom { get; set; }

	public int PublishedByUserId { get; set; }

	public User? PublishedByUser { get; set; }
}

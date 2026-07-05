using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class TermsOfUseAcceptanceConfiguration : IEntityTypeConfiguration<TermsOfUseAcceptance>
{
	public void Configure(EntityTypeBuilder<TermsOfUseAcceptance> builder)
	{
		builder.ToTable("TermsOfUseAcceptances");
		builder.HasKey((TermsOfUseAcceptance e) => e.Id);
		builder.Property((TermsOfUseAcceptance e) => e.AcceptedAt).IsRequired();
		builder.Property((TermsOfUseAcceptance e) => e.IpAddress).HasMaxLength(64);
		builder.HasIndex((TermsOfUseAcceptance e) => new { e.UserId, e.VersionId }).IsUnique();
		builder.HasOne((TermsOfUseAcceptance e) => e.User).WithMany().HasForeignKey((TermsOfUseAcceptance e) => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((TermsOfUseAcceptance e) => e.Version).WithMany((TermsOfUseVersion v) => v.Acceptances).HasForeignKey((TermsOfUseAcceptance e) => e.VersionId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

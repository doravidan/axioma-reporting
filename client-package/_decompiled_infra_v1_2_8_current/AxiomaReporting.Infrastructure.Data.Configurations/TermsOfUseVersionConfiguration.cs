using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class TermsOfUseVersionConfiguration : IEntityTypeConfiguration<TermsOfUseVersion>
{
	public void Configure(EntityTypeBuilder<TermsOfUseVersion> builder)
	{
		builder.ToTable("TermsOfUseVersions");
		builder.HasKey((TermsOfUseVersion e) => e.Id);
		builder.Property((TermsOfUseVersion e) => e.BodyHtml).IsRequired();
		builder.Property((TermsOfUseVersion e) => e.EffectiveFrom).IsRequired();
		builder.Property((TermsOfUseVersion e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.HasIndex((TermsOfUseVersion e) => e.VersionNumber).IsUnique().HasDatabaseName("IX_TermsOfUseVersion_VersionNumber");
		builder.HasOne((TermsOfUseVersion e) => e.PublishedByUser).WithMany().HasForeignKey((TermsOfUseVersion e) => e.PublishedByUserId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

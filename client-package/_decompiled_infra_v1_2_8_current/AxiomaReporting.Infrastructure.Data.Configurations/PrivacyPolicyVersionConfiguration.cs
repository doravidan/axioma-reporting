using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class PrivacyPolicyVersionConfiguration : IEntityTypeConfiguration<PrivacyPolicyVersion>
{
	public void Configure(EntityTypeBuilder<PrivacyPolicyVersion> builder)
	{
		builder.ToTable("PrivacyPolicyVersions");
		builder.HasKey((PrivacyPolicyVersion e) => e.Id);
		builder.Property((PrivacyPolicyVersion e) => e.BodyHtml).IsRequired();
		builder.Property((PrivacyPolicyVersion e) => e.EffectiveFrom).IsRequired();
		builder.Property((PrivacyPolicyVersion e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.HasIndex((PrivacyPolicyVersion e) => e.VersionNumber).IsUnique().HasDatabaseName("IX_PrivacyPolicyVersion_VersionNumber");
		builder.HasOne((PrivacyPolicyVersion e) => e.PublishedByUser).WithMany().HasForeignKey((PrivacyPolicyVersion e) => e.PublishedByUserId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

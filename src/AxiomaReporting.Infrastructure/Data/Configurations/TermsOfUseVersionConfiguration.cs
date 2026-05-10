using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class TermsOfUseVersionConfiguration : IEntityTypeConfiguration<TermsOfUseVersion>
{
  public void Configure(EntityTypeBuilder<TermsOfUseVersion> builder)
  {
    builder.ToTable("TermsOfUseVersions");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.BodyHtml).IsRequired();
    builder.Property(e => e.EffectiveFrom).IsRequired();
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

    builder.HasIndex(e => e.VersionNumber)
      .IsUnique()
      .HasDatabaseName("IX_TermsOfUseVersion_VersionNumber");

    builder.HasOne(e => e.PublishedByUser)
      .WithMany()
      .HasForeignKey(e => e.PublishedByUserId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

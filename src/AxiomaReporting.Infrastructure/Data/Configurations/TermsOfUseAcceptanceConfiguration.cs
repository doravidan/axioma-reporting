using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class TermsOfUseAcceptanceConfiguration : IEntityTypeConfiguration<TermsOfUseAcceptance>
{
  public void Configure(EntityTypeBuilder<TermsOfUseAcceptance> builder)
  {
    builder.ToTable("TermsOfUseAcceptances");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.AcceptedAt).IsRequired();
    builder.Property(e => e.IpAddress).HasMaxLength(64);

    builder.HasIndex(e => new { e.UserId, e.VersionId }).IsUnique();

    builder.HasOne(e => e.User)
      .WithMany()
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.Version)
      .WithMany(v => v.Acceptances)
      .HasForeignKey(e => e.VersionId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
{
  public void Configure(EntityTypeBuilder<Institution> builder)
  {
    builder.ToTable("Institutions");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Name).HasMaxLength(500).IsRequired();
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

    builder.HasIndex(e => new { e.InstitutionSymbol, e.EducationalStageId }).IsUnique();

    builder.HasOne(e => e.Locality)
      .WithMany()
      .HasForeignKey(e => e.LocalityId)
      .OnDelete(DeleteBehavior.SetNull);

    builder.HasOne(e => e.District)
      .WithMany()
      .HasForeignKey(e => e.DistrictId)
      .OnDelete(DeleteBehavior.SetNull);

    builder.HasOne(e => e.Sector)
      .WithMany()
      .HasForeignKey(e => e.SectorId)
      .OnDelete(DeleteBehavior.SetNull);

    builder.HasOne(e => e.Type)
      .WithMany()
      .HasForeignKey(e => e.TypeId)
      .OnDelete(DeleteBehavior.SetNull);

    builder.HasOne(e => e.EducationalStage)
      .WithMany()
      .HasForeignKey(e => e.EducationalStageId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}

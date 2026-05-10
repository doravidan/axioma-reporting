using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class FrameworkConfiguration : IEntityTypeConfiguration<Framework>
{
  public void Configure(EntityTypeBuilder<Framework> builder)
  {
    builder.ToTable("Frameworks");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Description).HasMaxLength(500).IsRequired();
    builder.Property(e => e.InstitutionSymbol).HasMaxLength(100).IsRequired();
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

    builder.HasIndex(e => new { e.InstitutionSymbol, e.EducationalStageId }).IsUnique();

    builder.HasOne(e => e.EducationalStage)
      .WithMany()
      .HasForeignKey(e => e.EducationalStageId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}

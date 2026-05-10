using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationGradeLevelConfiguration : IEntityTypeConfiguration<AllocationGradeLevel>
{
  public void Configure(EntityTypeBuilder<AllocationGradeLevel> builder)
  {
    builder.ToTable("AllocationGradeLevels");
    builder.HasKey(e => new { e.AllocationId, e.GradeLevelId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationGradeLevels)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.GradeLevel)
      .WithMany()
      .HasForeignKey(e => e.GradeLevelId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationEducationalProgramConfiguration : IEntityTypeConfiguration<AllocationEducationalProgram>
{
  public void Configure(EntityTypeBuilder<AllocationEducationalProgram> builder)
  {
    builder.ToTable("AllocationEducationalPrograms");
    builder.HasKey(e => new { e.AllocationId, e.EducationalProgramId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationEducationalPrograms)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.EducationalProgram)
      .WithMany()
      .HasForeignKey(e => e.EducationalProgramId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

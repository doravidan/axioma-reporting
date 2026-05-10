using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationProgramConfiguration : IEntityTypeConfiguration<AllocationProgram>
{
  public void Configure(EntityTypeBuilder<AllocationProgram> builder)
  {
    builder.ToTable("AllocationPrograms");
    builder.HasKey(e => new { e.AllocationId, e.ProgramId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationPrograms)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Program)
      .WithMany()
      .HasForeignKey(e => e.ProgramId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

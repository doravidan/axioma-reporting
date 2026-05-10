using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationFrameworkConfiguration : IEntityTypeConfiguration<AllocationFramework>
{
  public void Configure(EntityTypeBuilder<AllocationFramework> builder)
  {
    builder.ToTable("AllocationFrameworks");
    builder.HasKey(e => new { e.AllocationId, e.FrameworkId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationFrameworks)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Framework)
      .WithMany()
      .HasForeignKey(e => e.FrameworkId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

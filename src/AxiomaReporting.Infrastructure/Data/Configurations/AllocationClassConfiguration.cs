using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationClassConfiguration : IEntityTypeConfiguration<AllocationClass>
{
  public void Configure(EntityTypeBuilder<AllocationClass> builder)
  {
    builder.ToTable("AllocationClasses");
    builder.HasKey(e => new { e.AllocationId, e.ClassId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationClasses)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.SchoolClass)
      .WithMany()
      .HasForeignKey(e => e.ClassId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

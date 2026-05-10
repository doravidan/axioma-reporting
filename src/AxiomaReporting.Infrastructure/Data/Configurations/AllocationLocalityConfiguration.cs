using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationLocalityConfiguration : IEntityTypeConfiguration<AllocationLocality>
{
  public void Configure(EntityTypeBuilder<AllocationLocality> builder)
  {
    builder.ToTable("AllocationLocalities");
    builder.HasKey(e => new { e.AllocationId, e.LocalityId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationLocalities)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Locality)
      .WithMany()
      .HasForeignKey(e => e.LocalityId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

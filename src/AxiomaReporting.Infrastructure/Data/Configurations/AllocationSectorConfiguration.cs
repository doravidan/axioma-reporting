using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationSectorConfiguration : IEntityTypeConfiguration<AllocationSector>
{
  public void Configure(EntityTypeBuilder<AllocationSector> builder)
  {
    builder.ToTable("AllocationSectors");
    builder.HasKey(e => new { e.AllocationId, e.SectorId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationSectors)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Sector)
      .WithMany()
      .HasForeignKey(e => e.SectorId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

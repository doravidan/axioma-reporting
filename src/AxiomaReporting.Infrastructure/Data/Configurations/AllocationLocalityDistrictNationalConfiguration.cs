using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationLocalityDistrictNationalConfiguration
  : IEntityTypeConfiguration<AllocationLocalityDistrictNational>
{
  public void Configure(EntityTypeBuilder<AllocationLocalityDistrictNational> builder)
  {
    builder.ToTable("AllocationLocalityDistrictNationals");
    builder.HasKey(e => new { e.AllocationId, e.LocalityDistrictNationalId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationLocalityDistrictNationals)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.LocalityDistrictNational)
      .WithMany()
      .HasForeignKey(e => e.LocalityDistrictNationalId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

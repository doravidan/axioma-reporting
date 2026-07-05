using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationLocalityDistrictNationalConfiguration : IEntityTypeConfiguration<AllocationLocalityDistrictNational>
{
	public void Configure(EntityTypeBuilder<AllocationLocalityDistrictNational> builder)
	{
		builder.ToTable("AllocationLocalityDistrictNationals");
		builder.HasKey((AllocationLocalityDistrictNational e) => new { e.AllocationId, e.LocalityDistrictNationalId });
		builder.HasOne((AllocationLocalityDistrictNational e) => e.Allocation).WithMany((Allocation a) => a.AllocationLocalityDistrictNationals).HasForeignKey((AllocationLocalityDistrictNational e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationLocalityDistrictNational e) => e.LocalityDistrictNational).WithMany().HasForeignKey((AllocationLocalityDistrictNational e) => e.LocalityDistrictNationalId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

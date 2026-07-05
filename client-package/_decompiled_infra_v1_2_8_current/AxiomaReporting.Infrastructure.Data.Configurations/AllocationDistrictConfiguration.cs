using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationDistrictConfiguration : IEntityTypeConfiguration<AllocationDistrict>
{
	public void Configure(EntityTypeBuilder<AllocationDistrict> builder)
	{
		builder.ToTable("AllocationDistricts");
		builder.HasKey((AllocationDistrict e) => new { e.AllocationId, e.DistrictId });
		builder.HasOne((AllocationDistrict e) => e.Allocation).WithMany((Allocation a) => a.AllocationDistricts).HasForeignKey((AllocationDistrict e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationDistrict e) => e.District).WithMany().HasForeignKey((AllocationDistrict e) => e.DistrictId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

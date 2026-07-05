using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationLocalityConfiguration : IEntityTypeConfiguration<AllocationLocality>
{
	public void Configure(EntityTypeBuilder<AllocationLocality> builder)
	{
		builder.ToTable("AllocationLocalities");
		builder.HasKey((AllocationLocality e) => new { e.AllocationId, e.LocalityId });
		builder.HasOne((AllocationLocality e) => e.Allocation).WithMany((Allocation a) => a.AllocationLocalities).HasForeignKey((AllocationLocality e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationLocality e) => e.Locality).WithMany().HasForeignKey((AllocationLocality e) => e.LocalityId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

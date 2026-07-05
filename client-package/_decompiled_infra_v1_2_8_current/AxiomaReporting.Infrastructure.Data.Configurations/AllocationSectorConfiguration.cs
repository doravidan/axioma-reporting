using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationSectorConfiguration : IEntityTypeConfiguration<AllocationSector>
{
	public void Configure(EntityTypeBuilder<AllocationSector> builder)
	{
		builder.ToTable("AllocationSectors");
		builder.HasKey((AllocationSector e) => new { e.AllocationId, e.SectorId });
		builder.HasOne((AllocationSector e) => e.Allocation).WithMany((Allocation a) => a.AllocationSectors).HasForeignKey((AllocationSector e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationSector e) => e.Sector).WithMany().HasForeignKey((AllocationSector e) => e.SectorId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

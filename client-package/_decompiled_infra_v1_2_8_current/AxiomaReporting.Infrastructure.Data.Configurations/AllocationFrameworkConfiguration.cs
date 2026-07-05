using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationFrameworkConfiguration : IEntityTypeConfiguration<AllocationFramework>
{
	public void Configure(EntityTypeBuilder<AllocationFramework> builder)
	{
		builder.ToTable("AllocationFrameworks");
		builder.HasKey((AllocationFramework e) => new { e.AllocationId, e.FrameworkId });
		builder.HasOne((AllocationFramework e) => e.Allocation).WithMany((Allocation a) => a.AllocationFrameworks).HasForeignKey((AllocationFramework e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationFramework e) => e.Framework).WithMany().HasForeignKey((AllocationFramework e) => e.FrameworkId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

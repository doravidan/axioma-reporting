using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationClassConfiguration : IEntityTypeConfiguration<AllocationClass>
{
	public void Configure(EntityTypeBuilder<AllocationClass> builder)
	{
		builder.ToTable("AllocationClasses");
		builder.HasKey((AllocationClass e) => new { e.AllocationId, e.ClassId });
		builder.HasOne((AllocationClass e) => e.Allocation).WithMany((Allocation a) => a.AllocationClasses).HasForeignKey((AllocationClass e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationClass e) => e.SchoolClass).WithMany().HasForeignKey((AllocationClass e) => e.ClassId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

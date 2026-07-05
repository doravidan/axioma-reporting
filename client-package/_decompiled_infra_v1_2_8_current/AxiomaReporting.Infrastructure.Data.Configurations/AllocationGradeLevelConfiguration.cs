using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationGradeLevelConfiguration : IEntityTypeConfiguration<AllocationGradeLevel>
{
	public void Configure(EntityTypeBuilder<AllocationGradeLevel> builder)
	{
		builder.ToTable("AllocationGradeLevels");
		builder.HasKey((AllocationGradeLevel e) => new { e.AllocationId, e.GradeLevelId });
		builder.HasOne((AllocationGradeLevel e) => e.Allocation).WithMany((Allocation a) => a.AllocationGradeLevels).HasForeignKey((AllocationGradeLevel e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationGradeLevel e) => e.GradeLevel).WithMany().HasForeignKey((AllocationGradeLevel e) => e.GradeLevelId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationEducationalProgramConfiguration : IEntityTypeConfiguration<AllocationEducationalProgram>
{
	public void Configure(EntityTypeBuilder<AllocationEducationalProgram> builder)
	{
		builder.ToTable("AllocationEducationalPrograms");
		builder.HasKey((AllocationEducationalProgram e) => new { e.AllocationId, e.EducationalProgramId });
		builder.HasOne((AllocationEducationalProgram e) => e.Allocation).WithMany((Allocation a) => a.AllocationEducationalPrograms).HasForeignKey((AllocationEducationalProgram e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationEducationalProgram e) => e.EducationalProgram).WithMany().HasForeignKey((AllocationEducationalProgram e) => e.EducationalProgramId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

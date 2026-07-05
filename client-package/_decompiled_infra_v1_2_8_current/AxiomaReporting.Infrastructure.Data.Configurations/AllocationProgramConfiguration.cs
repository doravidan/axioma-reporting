using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationProgramConfiguration : IEntityTypeConfiguration<AllocationProgram>
{
	public void Configure(EntityTypeBuilder<AllocationProgram> builder)
	{
		builder.ToTable("AllocationPrograms");
		builder.HasKey((AllocationProgram e) => new { e.AllocationId, e.ProgramId });
		builder.HasOne((AllocationProgram e) => e.Allocation).WithMany((Allocation a) => a.AllocationPrograms).HasForeignKey((AllocationProgram e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationProgram e) => e.Program).WithMany().HasForeignKey((AllocationProgram e) => e.ProgramId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ProjectProgramConfiguration : IEntityTypeConfiguration<ProjectProgram>
{
	public void Configure(EntityTypeBuilder<ProjectProgram> builder)
	{
		builder.ToTable("ProjectPrograms");
		builder.HasKey((ProjectProgram e) => new { e.ProjectId, e.ProgramId });
		builder.HasOne((ProjectProgram e) => e.Project).WithMany((Project p) => p.ProjectPrograms).HasForeignKey((ProjectProgram e) => e.ProjectId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((ProjectProgram e) => e.Program).WithMany().HasForeignKey((ProjectProgram e) => e.ProgramId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

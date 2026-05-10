using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ProjectProgramConfiguration : IEntityTypeConfiguration<ProjectProgram>
{
  public void Configure(EntityTypeBuilder<ProjectProgram> builder)
  {
    builder.ToTable("ProjectPrograms");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId });

    builder.HasOne(e => e.Project)
      .WithMany(p => p.ProjectPrograms)
      .HasForeignKey(e => e.ProjectId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Program)
      .WithMany()
      .HasForeignKey(e => e.ProgramId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}

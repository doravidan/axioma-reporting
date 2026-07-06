using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

// Composite-key link tables binding code-table values to a (Project, Program) pair.
// The physical tables were introduced by the AddProjectProgramScopeTables migration
// and are populated by the FW onboarding import.

public class ProjectProgramSubjectConfiguration : IEntityTypeConfiguration<ProjectProgramSubject>
{
  public void Configure(EntityTypeBuilder<ProjectProgramSubject> builder)
  {
    builder.ToTable("ProjectProgramSubjects");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.SubjectId });
    builder.HasOne(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class ProjectProgramDomainConfiguration : IEntityTypeConfiguration<ProjectProgramDomain>
{
  public void Configure(EntityTypeBuilder<ProjectProgramDomain> builder)
  {
    builder.ToTable("ProjectProgramDomains");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.DomainId });
    builder.HasOne(e => e.Domain).WithMany().HasForeignKey(e => e.DomainId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class ProjectProgramEducationalProgramConfiguration : IEntityTypeConfiguration<ProjectProgramEducationalProgram>
{
  public void Configure(EntityTypeBuilder<ProjectProgramEducationalProgram> builder)
  {
    builder.ToTable("ProjectProgramEducationalPrograms");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.EducationalProgramId });
    builder.HasOne(e => e.EducationalProgram).WithMany().HasForeignKey(e => e.EducationalProgramId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class ProjectProgramDiscussionCodeConfiguration : IEntityTypeConfiguration<ProjectProgramDiscussionCode>
{
  public void Configure(EntityTypeBuilder<ProjectProgramDiscussionCode> builder)
  {
    builder.ToTable("ProjectProgramDiscussionCodes");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.DiscussionCodeId });
    builder.HasOne(e => e.DiscussionCode).WithMany().HasForeignKey(e => e.DiscussionCodeId).OnDelete(DeleteBehavior.Cascade);
  }
}

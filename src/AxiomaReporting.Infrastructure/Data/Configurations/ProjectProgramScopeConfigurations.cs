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

public class ProjectProgramFrameworkConfiguration : IEntityTypeConfiguration<ProjectProgramFramework>
{
  public void Configure(EntityTypeBuilder<ProjectProgramFramework> builder)
  {
    builder.ToTable("ProjectProgramFrameworks");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.FrameworkId });
    builder.HasOne(e => e.Framework).WithMany().HasForeignKey(e => e.FrameworkId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class ProjectProgramGradeLevelConfiguration : IEntityTypeConfiguration<ProjectProgramGradeLevel>
{
  public void Configure(EntityTypeBuilder<ProjectProgramGradeLevel> builder)
  {
    builder.ToTable("ProjectProgramGradeLevels");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.GradeLevelId });
    builder.HasOne(e => e.GradeLevel).WithMany().HasForeignKey(e => e.GradeLevelId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class ProjectProgramClassConfiguration : IEntityTypeConfiguration<ProjectProgramClass>
{
  public void Configure(EntityTypeBuilder<ProjectProgramClass> builder)
  {
    builder.ToTable("ProjectProgramClasses");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.ClassId });
    builder.HasOne(e => e.SchoolClass).WithMany().HasForeignKey(e => e.ClassId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class ProjectProgramLocalityConfiguration : IEntityTypeConfiguration<ProjectProgramLocality>
{
  public void Configure(EntityTypeBuilder<ProjectProgramLocality> builder)
  {
    builder.ToTable("ProjectProgramLocalities");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.LocalityId });
    builder.HasOne(e => e.Locality).WithMany().HasForeignKey(e => e.LocalityId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class ProjectProgramLocalityDistrictNationalConfiguration : IEntityTypeConfiguration<ProjectProgramLocalityDistrictNational>
{
  public void Configure(EntityTypeBuilder<ProjectProgramLocalityDistrictNational> builder)
  {
    builder.ToTable("ProjectProgramLocalityDistrictNationals");
    builder.HasKey(e => new { e.ProjectId, e.ProgramId, e.LocalityDistrictNationalId });
    builder.HasOne(e => e.LocalityDistrictNational).WithMany().HasForeignKey(e => e.LocalityDistrictNationalId).OnDelete(DeleteBehavior.Cascade);
  }
}

public class PrivacyPolicyVersionConfiguration : IEntityTypeConfiguration<PrivacyPolicyVersion>
{
  public void Configure(EntityTypeBuilder<PrivacyPolicyVersion> builder)
  {
    builder.ToTable("PrivacyPolicyVersions");
    builder.Property(e => e.BodyHtml).IsRequired();
    builder.HasIndex(e => e.VersionNumber).IsUnique();
    builder.HasOne(e => e.PublishedByUser).WithMany().HasForeignKey(e => e.PublishedByUserId).OnDelete(DeleteBehavior.Restrict);
  }
}

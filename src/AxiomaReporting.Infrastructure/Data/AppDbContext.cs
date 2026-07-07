using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  // Lookup tables
  public DbSet<District> Districts => Set<District>();
  public DbSet<Sector> Sectors => Set<Sector>();
  public DbSet<Locality> Localities => Set<Locality>();
  public DbSet<Authority> Authorities => Set<Authority>();
  public DbSet<Project> Projects => Set<Project>();
  public DbSet<Program> Programs => Set<Program>();
  public DbSet<EducationalProgram> EducationalPrograms => Set<EducationalProgram>();
  public DbSet<Subject> Subjects => Set<Subject>();
  public DbSet<Domain> Domains => Set<Domain>();
  public DbSet<Framework> Frameworks => Set<Framework>();
  public DbSet<SchoolClass> Classes => Set<SchoolClass>();
  public DbSet<GradeLevel> GradeLevels => Set<GradeLevel>();
  public DbSet<EmployeeRole> Roles => Set<EmployeeRole>();
  public DbSet<EducationalStage> EducationalStages => Set<EducationalStage>();
  public DbSet<EducationType> EducationTypes => Set<EducationType>();
  public DbSet<LocalityDistrictNational> LocalityDistrictNationals => Set<LocalityDistrictNational>();
  public DbSet<DiscussionCode> DiscussionCodes => Set<DiscussionCode>();
  public DbSet<ClassConclusion> ClassConclusions => Set<ClassConclusion>();
  public DbSet<FrameworkConclusion> FrameworkConclusions => Set<FrameworkConclusion>();
  public DbSet<Institution> Institutions => Set<Institution>();
  public DbSet<ReportType> ReportTypes => Set<ReportType>();
  public DbSet<ProjectProgram> ProjectPrograms => Set<ProjectProgram>();
  public DbSet<ProjectProgramSubject> ProjectProgramSubjects => Set<ProjectProgramSubject>();
  public DbSet<ProjectProgramDomain> ProjectProgramDomains => Set<ProjectProgramDomain>();
  public DbSet<ProjectProgramEducationalProgram> ProjectProgramEducationalPrograms => Set<ProjectProgramEducationalProgram>();
  public DbSet<ProjectProgramDiscussionCode> ProjectProgramDiscussionCodes => Set<ProjectProgramDiscussionCode>();
  public DbSet<ProjectProgramFramework> ProjectProgramFrameworks => Set<ProjectProgramFramework>();
  public DbSet<ProjectProgramGradeLevel> ProjectProgramGradeLevels => Set<ProjectProgramGradeLevel>();
  public DbSet<ProjectProgramClass> ProjectProgramClasses => Set<ProjectProgramClass>();

  // System tables
  public DbSet<ReportStatus> ReportStatuses => Set<ReportStatus>();
  public DbSet<UserStatus> UserStatuses => Set<UserStatus>();
  public DbSet<UserRole> UserRoles => Set<UserRole>();
  public DbSet<SystemConstant> SystemConstants => Set<SystemConstant>();
  public DbSet<EmailServerSetting> EmailServerSettings => Set<EmailServerSetting>();
  public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

  // Core business tables
  public DbSet<User> Users => Set<User>();
  public DbSet<PasswordHistory> PasswordHistories => Set<PasswordHistory>();
  public DbSet<Allocation> Allocations => Set<Allocation>();
  public DbSet<ReportingMonth> ReportingMonths => Set<ReportingMonth>();
  public DbSet<Report> Reports => Set<Report>();
  public DbSet<ReportRow> ReportRows => Set<ReportRow>();
  public DbSet<DocumentAttachment> DocumentAttachments => Set<DocumentAttachment>();
  public DbSet<InspectorAssignment> InspectorAssignments => Set<InspectorAssignment>();
  public DbSet<ReminderLog> ReminderLogs => Set<ReminderLog>();
  public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
  public DbSet<TwoFactorCode> TwoFactorCodes => Set<TwoFactorCode>();
  public DbSet<TermsOfUseVersion> TermsOfUseVersions => Set<TermsOfUseVersion>();
  public DbSet<TermsOfUseAcceptance> TermsOfUseAcceptances => Set<TermsOfUseAcceptance>();
  public DbSet<PrivacyPolicyVersion> PrivacyPolicyVersions => Set<PrivacyPolicyVersion>();
  public DbSet<NotificationLog> NotificationLogs => Set<NotificationLog>();
  public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    SeedData.Seed(modelBuilder);
  }
}

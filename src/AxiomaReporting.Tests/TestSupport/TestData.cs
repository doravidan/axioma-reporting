using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;

namespace AxiomaReporting.Tests.TestSupport;

public static class TestData
{
  public const string EmployeeIdNumber = "111111111";
  public const string EmployeePassword = "Password123";
  public const string AdminIdNumber = "999999999";
  public const string AdminPassword = "Admin1234";

  public static void SeedIdentity(AppDbContext db, bool tfaEnabled = false, bool seedTermsVersion = true, bool acceptLatestTerms = true)
  {
    db.UserRoles.AddRange(
      new UserRole { Id = 1, Name = "SystemAdmin", Description = "System admin" },
      new UserRole { Id = 2, Name = "ProjectManager", Description = "Project manager" },
      new UserRole { Id = 3, Name = "ProjectCoordinator", Description = "Coordinator" },
      new UserRole { Id = 4, Name = "InspectorView", Description = "Inspector view" },
      new UserRole { Id = 5, Name = "InspectorApproval", Description = "Inspector approval" },
      new UserRole { Id = 6, Name = "Employee", Description = "Employee" });

    db.UserStatuses.AddRange(
      new UserStatus { Id = (int)UserStatusEnum.Active, Name = "Active" },
      new UserStatus { Id = (int)UserStatusEnum.Inactive, Name = "Inactive" },
      new UserStatus { Id = (int)UserStatusEnum.Locked, Name = "Locked" });

    // Seed a base EmployeeRole so Users have a valid RoleId reference.
    // Controllers include User.Role; a missing FK target causes EF in-memory join
    // to produce unexpected results, leading to 404s.
    // Use Id=999 to avoid conflicting with EmployeeRoles seeded by individual tests (which often use Id=1).
    db.Roles.Add(new EmployeeRole { Id = 999, Description = "Test Role", IsActive = true, CreatedAt = DateTime.UtcNow });

    var password = new PasswordService();
    db.Users.AddRange(
      new User
      {
        Id = 1,
        EmployeeCode = "EMP001",
        IdNumber = EmployeeIdNumber,
        FirstName = "Test",
        LastName = "Employee",
        Email = "employee@example.test",
        PasswordHash = password.HashPassword(EmployeePassword),
        RoleId = 999,
        UserRoleId = (int)UserRoleEnum.Employee,
        StatusId = (int)UserStatusEnum.Active,
        AcceptedTermsOfUse = true,
        MustChangePassword = false,
        LastPasswordChange = DateTime.UtcNow,
        IsReportingEmployee = true,
        CreatedAt = DateTime.UtcNow
      },
      new User
      {
        Id = 2,
        EmployeeCode = "ADMIN001",
        IdNumber = AdminIdNumber,
        FirstName = "Test",
        LastName = "Admin",
        Email = "admin@example.test",
        PasswordHash = password.HashPassword(AdminPassword),
        RoleId = 999,
        UserRoleId = (int)UserRoleEnum.SystemAdmin,
        StatusId = (int)UserStatusEnum.Active,
        AcceptedTermsOfUse = true,
        MustChangePassword = false,
        LastPasswordChange = DateTime.UtcNow,
        CreatedAt = DateTime.UtcNow
      });

    db.SystemConstants.AddRange(
      new SystemConstant
      {
        Id = 1,
        Key = "TfaEmailEnabled",
        Value = tfaEnabled ? "true" : "false",
        Description = "Email TFA",
        CreatedAt = DateTime.UtcNow
      },
      new SystemConstant
      {
        Id = 2,
        Key = "NotesSimilarityThresholdPercent",
        Value = "90",
        Description = "Notes similarity threshold",
        CreatedAt = DateTime.UtcNow
      },
      new SystemConstant
      {
        Id = 3,
        Key = "MaxDailyHoursDefault",
        Value = "9",
        Description = "Default max daily hours",
        CreatedAt = DateTime.UtcNow
      });

    db.SaveChanges();

    if (seedTermsVersion)
    {
      var versionId = 1;
      db.TermsOfUseVersions.Add(new TermsOfUseVersion
      {
        Id = versionId,
        VersionNumber = 1,
        BodyHtml = "<p>תנאי שימוש לבדיקה</p>",
        EffectiveFrom = DateTime.UtcNow.AddDays(-1),
        PublishedByUserId = 2,
        CreatedAt = DateTime.UtcNow.AddDays(-1)
      });

      if (acceptLatestTerms)
      {
        db.TermsOfUseAcceptances.AddRange(
          new TermsOfUseAcceptance { UserId = 1, VersionId = versionId, AcceptedAt = DateTime.UtcNow.AddDays(-1) },
          new TermsOfUseAcceptance { UserId = 2, VersionId = versionId, AcceptedAt = DateTime.UtcNow.AddDays(-1) });
      }

      db.SaveChanges();
    }
  }
}

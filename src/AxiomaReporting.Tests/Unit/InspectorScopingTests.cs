using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Unit tests for inspector scoping rules (spec section on Inspector-View /
/// Inspector-Approval).
///
/// Key rules under test:
///   - AND logic within one assignment row: all non-null fields must match.
///   - OR logic across rows: an employee visible in ANY assignment row is in scope.
///   - A null field in an assignment row acts as a wildcard for that dimension.
///   - Inspector-View (role 4) may access but NOT approve a report.
///   - Inspector-Approval (role 5) may both access AND approve a report.
///   - Employees may only access their own reports.
/// </summary>
public class InspectorScopingTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly DashboardFilterService _sut;

  // Fixed IDs used throughout
  private const int InspectorViewUserId      = 10;
  private const int InspectorApprovalUserId  = 11;
  private const int EmployeeAUserId          = 20;  // District A, Program B, Sector X
  private const int EmployeeBUserId          = 21;  // District B, Program C, Sector X
  private const int EmployeeCUserId          = 22;  // District C, Program D, Sector Y (not in any scope)
  private const int DistrictAId             = 1;
  private const int DistrictBId             = 2;
  private const int DistrictCId             = 3;
  private const int ProgramBId              = 1;
  private const int ProgramCId              = 2;
  private const int ProgramDId              = 3;
  private const int SectorXId               = 1;
  private const int SectorYId               = 2;
  private const int ProjectId               = 1;
  private const int ReportStatusApprovedId  = 3;
  private const int ReportStatusDraftId     = 1;
  private const int ReportingMonthId        = 1;

  public InspectorScopingTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new DashboardFilterService(_db);
  }

  public void Dispose() => _db.Dispose();

  // =========================================================================
  // Scoping: single assignment row with AND logic
  // =========================================================================

  [Fact]
  public async Task GetScopedUsers_SingleAssignment_DistrictAndProgram_SectorNull_MatchesAnd()
  {
    // Inspector assignment: District=A, Program=B, Sector=null (wildcard)
    // Employee A has Allocation in District A AND Program B  -> IN scope
    // Employee B has Allocation in District B AND Program C  -> OUT (District B != A)
    // Employee C has Allocation in District C               -> OUT
    await SeedBaseDataAsync();

    // Assignment for inspector-view: District A + Program B, no sector constraint
    _db.InspectorAssignments.Add(new InspectorAssignment
    {
      Id = 1,
      InspectorUserId = InspectorViewUserId,
      DistrictId = DistrictAId,
      ProgramId = ProgramBId,
      SectorId = null
    });
    await _db.SaveChangesAsync();

    var scopedIds = await GetScopedUserIdsViaReportsAsync(InspectorViewUserId, UserRoleEnum.InspectorView);

    scopedIds.Should().Contain(EmployeeAUserId,
      "Employee A has an allocation tagged with District A and Program B, matching the inspector's assignment");
    scopedIds.Should().NotContain(EmployeeBUserId,
      "Employee B is in District B, which does not match District A in the inspector's assignment");
    scopedIds.Should().NotContain(EmployeeCUserId,
      "Employee C is in District C and not associated with Program B");
  }

  // =========================================================================
  // Scoping: two assignment rows with OR logic
  // =========================================================================

  [Fact]
  public async Task GetScopedUsers_TwoAssignmentRows_OrLogicAcrossRows()
  {
    // Assignment row 1: District=A (only)
    // Assignment row 2: District=B (only)
    // Expected: employees in District A OR District B are in scope.
    await SeedBaseDataAsync();

    _db.InspectorAssignments.AddRange(
      new InspectorAssignment
      {
        Id = 1,
        InspectorUserId = InspectorViewUserId,
        DistrictId = DistrictAId,
        ProgramId = null,
        SectorId = null
      },
      new InspectorAssignment
      {
        Id = 2,
        InspectorUserId = InspectorViewUserId,
        DistrictId = DistrictBId,
        ProgramId = null,
        SectorId = null
      });
    await _db.SaveChangesAsync();

    var scopedIds = await GetScopedUserIdsViaReportsAsync(InspectorViewUserId, UserRoleEnum.InspectorView);

    scopedIds.Should().Contain(EmployeeAUserId,
      "Employee A is in District A, covered by assignment row 1");
    scopedIds.Should().Contain(EmployeeBUserId,
      "Employee B is in District B, covered by assignment row 2");
    scopedIds.Should().NotContain(EmployeeCUserId,
      "Employee C is in District C, which appears in neither assignment row");
  }

  // =========================================================================
  // Scoping: all-null assignment row is a wildcard (sees everything)
  // =========================================================================

  [Fact]
  public async Task GetScopedUsers_AllNullAssignment_SeesAllEmployees()
  {
    await SeedBaseDataAsync();

    // An assignment with all dimensions null acts as a global wildcard
    _db.InspectorAssignments.Add(new InspectorAssignment
    {
      Id = 1,
      InspectorUserId = InspectorViewUserId,
      DistrictId = null,
      ProgramId = null,
      SectorId = null
    });
    await _db.SaveChangesAsync();

    var scopedIds = await GetScopedUserIdsViaReportsAsync(InspectorViewUserId, UserRoleEnum.InspectorView);

    scopedIds.Should().Contain(EmployeeAUserId);
    scopedIds.Should().Contain(EmployeeBUserId);
    scopedIds.Should().Contain(EmployeeCUserId,
      "a fully-null assignment row is a wildcard that matches every employee with an active allocation");
  }

  // =========================================================================
  // CanAccessReportAsync – permission boundaries
  // =========================================================================

  [Fact]
  public async Task CanAccessReportAsync_InspectorView_CanAccessReport_WhenEmployeeIsInScope()
  {
    await SeedBaseDataAsync();

    // Inspector sees District A, Program B
    _db.InspectorAssignments.Add(new InspectorAssignment
    {
      Id = 1,
      InspectorUserId = InspectorViewUserId,
      DistrictId = DistrictAId,
      ProgramId = ProgramBId,
      SectorId = null
    });

    // Report belonging to Employee A (who is in scope)
    var report = CreateReport(100, EmployeeAUserId);
    _db.Reports.Add(report);
    await _db.SaveChangesAsync();

    var canAccess = await _sut.CanAccessReportAsync(report.Id, InspectorViewUserId, UserRoleEnum.InspectorView);

    canAccess.Should().BeTrue(
      "Inspector-View must be able to read a report that belongs to an employee within their scope");
  }

  [Fact]
  public async Task CanAccessReportAsync_InspectorView_CannotAccessReport_WhenEmployeeIsOutOfScope()
  {
    await SeedBaseDataAsync();

    // Inspector sees only District A
    _db.InspectorAssignments.Add(new InspectorAssignment
    {
      Id = 1,
      InspectorUserId = InspectorViewUserId,
      DistrictId = DistrictAId,
      ProgramId = null,
      SectorId = null
    });

    // Report belonging to Employee C (District C – out of scope)
    var report = CreateReport(101, EmployeeCUserId);
    _db.Reports.Add(report);
    await _db.SaveChangesAsync();

    var canAccess = await _sut.CanAccessReportAsync(report.Id, InspectorViewUserId, UserRoleEnum.InspectorView);

    canAccess.Should().BeFalse(
      "Inspector-View must not access reports of employees outside their assigned scope");
  }

  [Fact]
  public async Task CanAccessReportAsync_InspectorApproval_CanAccessReport_WhenEmployeeIsInScope()
  {
    // Inspector-Approval has the same access check logic as Inspector-View
    // (both use GetScopedUserIdsAsync); this confirms the role enum branching.
    await SeedBaseDataAsync();

    _db.InspectorAssignments.Add(new InspectorAssignment
    {
      Id = 1,
      InspectorUserId = InspectorApprovalUserId,
      DistrictId = DistrictAId,
      ProgramId = null,
      SectorId = null
    });

    var report = CreateReport(102, EmployeeAUserId);
    _db.Reports.Add(report);
    await _db.SaveChangesAsync();

    var canAccess = await _sut.CanAccessReportAsync(report.Id, InspectorApprovalUserId, UserRoleEnum.InspectorApproval);

    canAccess.Should().BeTrue(
      "Inspector-Approval must be able to access (and subsequently approve) a report that is in scope");
  }

  [Fact]
  public async Task CanAccessReportAsync_EmployeeCanOnlySeeOwnReports()
  {
    await SeedBaseDataAsync();

    var ownReport   = CreateReport(200, EmployeeAUserId);
    var otherReport = CreateReport(201, EmployeeBUserId);
    _db.Reports.AddRange(ownReport, otherReport);
    await _db.SaveChangesAsync();

    var canSeeOwn   = await _sut.CanAccessReportAsync(ownReport.Id,   EmployeeAUserId, UserRoleEnum.Employee);
    var canSeeOther = await _sut.CanAccessReportAsync(otherReport.Id, EmployeeAUserId, UserRoleEnum.Employee);

    canSeeOwn.Should().BeTrue("an employee must always be able to access their own report");
    canSeeOther.Should().BeFalse("an employee must not be able to access another employee's report");
  }

  [Fact]
  public async Task CanAccessReportAsync_AdminBypassesScope_ButManagersRequireAssignment()
  {
    await SeedBaseDataAsync();

    // Admin user (id 99) has no inspector assignments – admins bypass scope checks
    const int adminUserId = 99;
    var report = CreateReport(300, EmployeeCUserId);
    _db.Reports.Add(report);
    await _db.SaveChangesAsync();

    var adminAccess = await _sut.CanAccessReportAsync(report.Id, adminUserId, UserRoleEnum.SystemAdmin);
    var pmWithoutAssignment = await _sut.CanAccessReportAsync(report.Id, adminUserId, UserRoleEnum.ProjectManager);
    var coordWithoutAssignment = await _sut.CanAccessReportAsync(report.Id, adminUserId, UserRoleEnum.ProjectCoordinator);

    adminAccess.Should().BeTrue("SystemAdmin must be able to access any report");
    pmWithoutAssignment.Should().BeFalse("ProjectManager access must be constrained by explicit assignments");
    coordWithoutAssignment.Should().BeFalse("ProjectCoordinator access must be constrained by explicit assignments");

    _db.InspectorAssignments.Add(new InspectorAssignment
    {
      Id = 1,
      InspectorUserId = adminUserId,
      DistrictId = DistrictCId
    });
    await _db.SaveChangesAsync();

    (await _sut.CanAccessReportAsync(report.Id, adminUserId, UserRoleEnum.ProjectManager))
      .Should().BeTrue("the explicit District C assignment covers the report owner");
    (await _sut.CanAccessReportAsync(report.Id, adminUserId, UserRoleEnum.ProjectCoordinator))
      .Should().BeTrue("the same assignment model applies to coordinators");
  }

  [Fact]
  public async Task CanAccessReportAsync_NonExistentReport_ReturnsFalse()
  {
    await SeedBaseDataAsync();

    var result = await _sut.CanAccessReportAsync(99999, InspectorViewUserId, UserRoleEnum.InspectorView);

    result.Should().BeFalse("a non-existent report id must return false for any role");
  }

  // =========================================================================
  // Inspector with no assignments has empty scope
  // =========================================================================

  [Fact]
  public async Task GetScopedUsers_InspectorWithNoAssignments_HasEmptyScope()
  {
    await SeedBaseDataAsync();
    // No assignments seeded for this inspector
    var report = CreateReport(400, EmployeeAUserId);
    _db.Reports.Add(report);
    await _db.SaveChangesAsync();

    var canAccess = await _sut.CanAccessReportAsync(report.Id, InspectorViewUserId, UserRoleEnum.InspectorView);

    canAccess.Should().BeFalse(
      "an inspector with no assignments has an empty scope and cannot access any report");
  }

  // =========================================================================
  // Seed helpers
  // =========================================================================

  private async Task SeedBaseDataAsync()
  {
    // User roles and statuses
    _db.UserRoles.AddRange(
      new UserRole { Id = (int)UserRoleEnum.SystemAdmin,          Name = "SystemAdmin" },
      new UserRole { Id = (int)UserRoleEnum.ProjectManager,       Name = "ProjectManager" },
      new UserRole { Id = (int)UserRoleEnum.ProjectCoordinator,   Name = "ProjectCoordinator" },
      new UserRole { Id = (int)UserRoleEnum.InspectorView,        Name = "InspectorView" },
      new UserRole { Id = (int)UserRoleEnum.InspectorApproval,    Name = "InspectorApproval" },
      new UserRole { Id = (int)UserRoleEnum.Employee,             Name = "Employee" });

    _db.UserStatuses.AddRange(
      new UserStatus { Id = (int)UserStatusEnum.Active,   Name = "Active" },
      new UserStatus { Id = (int)UserStatusEnum.Inactive, Name = "Inactive" },
      new UserStatus { Id = (int)UserStatusEnum.Locked,   Name = "Locked" });

    // Inspector users
    _db.Users.AddRange(
      CreateUser(InspectorViewUserId,     "INSP001", UserRoleEnum.InspectorView),
      CreateUser(InspectorApprovalUserId, "INSP002", UserRoleEnum.InspectorApproval));

    // Employee users
    _db.Users.AddRange(
      CreateEmployee(EmployeeAUserId, "EMP_A"),
      CreateEmployee(EmployeeBUserId, "EMP_B"),
      CreateEmployee(EmployeeCUserId, "EMP_C"));

    // Lookup entities: districts, programs, sectors, project
    _db.Districts.AddRange(
      new District { Id = DistrictAId, Description = "District A", IsActive = true, CreatedAt = DateTime.UtcNow },
      new District { Id = DistrictBId, Description = "District B", IsActive = true, CreatedAt = DateTime.UtcNow },
      new District { Id = DistrictCId, Description = "District C", IsActive = true, CreatedAt = DateTime.UtcNow });

    _db.Programs.AddRange(
      new AxiomaReporting.Core.Entities.Program { Id = ProgramBId, Description = "Program B", IsActive = true, CreatedAt = DateTime.UtcNow },
      new AxiomaReporting.Core.Entities.Program { Id = ProgramCId, Description = "Program C", IsActive = true, CreatedAt = DateTime.UtcNow },
      new AxiomaReporting.Core.Entities.Program { Id = ProgramDId, Description = "Program D", IsActive = true, CreatedAt = DateTime.UtcNow });

    _db.Sectors.AddRange(
      new Sector { Id = SectorXId, Description = "Sector X", IsActive = true, CreatedAt = DateTime.UtcNow },
      new Sector { Id = SectorYId, Description = "Sector Y", IsActive = true, CreatedAt = DateTime.UtcNow });

    _db.Projects.Add(
      new Project { Id = ProjectId, Description = "Project 1", IsActive = true, CreatedAt = DateTime.UtcNow });

    // Reporting month
    _db.ReportingMonths.Add(new ReportingMonth
    {
      Id = ReportingMonthId,
      Month = 1,
      Year = 2025,
      Description = "2025-01",
      LastReportingDate = new DateTime(2025, 1, 31),
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    });

    // Report statuses
    _db.ReportStatuses.AddRange(
      new ReportStatus { Id = ReportStatusDraftId,    Name = "Draft" },
      new ReportStatus { Id = 2,                      Name = "Submitted" },
      new ReportStatus { Id = ReportStatusApprovedId, Name = "Approved" });

    await _db.SaveChangesAsync();

    // Allocations for each employee linked to the project
    var allocationA = new Allocation { Id = 1, UserId = EmployeeAUserId, ProjectId = ProjectId, IsActive = true, CreatedAt = DateTime.UtcNow };
    var allocationB = new Allocation { Id = 2, UserId = EmployeeBUserId, ProjectId = ProjectId, IsActive = true, CreatedAt = DateTime.UtcNow };
    var allocationC = new Allocation { Id = 3, UserId = EmployeeCUserId, ProjectId = ProjectId, IsActive = true, CreatedAt = DateTime.UtcNow };
    _db.Allocations.AddRange(allocationA, allocationB, allocationC);
    await _db.SaveChangesAsync();

    // Tag allocations with districts and programs
    // Employee A -> District A, Program B
    _db.Set<AllocationDistrict>().Add(new AllocationDistrict { AllocationId = allocationA.Id, DistrictId = DistrictAId });
    _db.Set<AllocationProgram>().Add(new AllocationProgram   { AllocationId = allocationA.Id, ProgramId  = ProgramBId  });
    _db.Set<AllocationSector>().Add(new AllocationSector     { AllocationId = allocationA.Id, SectorId   = SectorXId   });

    // Employee B -> District B, Program C
    _db.Set<AllocationDistrict>().Add(new AllocationDistrict { AllocationId = allocationB.Id, DistrictId = DistrictBId });
    _db.Set<AllocationProgram>().Add(new AllocationProgram   { AllocationId = allocationB.Id, ProgramId  = ProgramCId  });
    _db.Set<AllocationSector>().Add(new AllocationSector     { AllocationId = allocationB.Id, SectorId   = SectorXId   });

    // Employee C -> District C, Program D
    _db.Set<AllocationDistrict>().Add(new AllocationDistrict { AllocationId = allocationC.Id, DistrictId = DistrictCId });
    _db.Set<AllocationProgram>().Add(new AllocationProgram   { AllocationId = allocationC.Id, ProgramId  = ProgramDId  });
    _db.Set<AllocationSector>().Add(new AllocationSector     { AllocationId = allocationC.Id, SectorId   = SectorYId   });

    await _db.SaveChangesAsync();
  }

  /// <summary>
  /// Retrieves the user IDs that are in scope for the given inspector by querying
  /// reports that belong to those users.  Because <see cref="DashboardFilterService"/>
  /// exposes scope only indirectly (through <see cref="IDashboardFilterService.CanAccessReportAsync"/>
  /// or <see cref="IDashboardFilterService.GetReportsAsync"/>), we seed a report for each
  /// employee and test which ones the inspector can access.
  /// </summary>
  private async Task<List<int>> GetScopedUserIdsViaReportsAsync(int inspectorId, UserRoleEnum role)
  {
    // Ensure reports exist for all three employees
    var existingReportUserIds = await _db.Reports.Select(r => r.UserId).ToListAsync();
    var reportId = 50;
    foreach (var empId in new[] { EmployeeAUserId, EmployeeBUserId, EmployeeCUserId })
    {
      if (!existingReportUserIds.Contains(empId))
      {
        _db.Reports.Add(CreateReport(reportId++, empId));
      }
    }
    await _db.SaveChangesAsync();

    var reports = await _db.Reports.ToListAsync();
    var visible = new List<int>();
    foreach (var report in reports)
    {
      if (await _sut.CanAccessReportAsync(report.Id, inspectorId, role))
        visible.Add(report.UserId);
    }
    return visible.Distinct().ToList();
  }

  private static User CreateUser(int id, string code, UserRoleEnum role) => new()
  {
    Id = id,
    EmployeeCode = code,
    IdNumber = $"ID{id:D9}",
    FirstName = "Inspector",
    LastName = code,
    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123"),
    UserRoleId = (int)role,
    StatusId = (int)UserStatusEnum.Active,
    IsReportingEmployee = false,
    CreatedAt = DateTime.UtcNow
  };

  private static User CreateEmployee(int id, string code) => new()
  {
    Id = id,
    EmployeeCode = code,
    IdNumber = $"ID{id:D9}",
    FirstName = "Employee",
    LastName = code,
    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123"),
    UserRoleId = (int)UserRoleEnum.Employee,
    StatusId = (int)UserStatusEnum.Active,
    IsReportingEmployee = true,
    CreatedAt = DateTime.UtcNow
  };

  private static Report CreateReport(int id, int userId) => new()
  {
    Id = id,
    UserId = userId,
    ReportingMonthId = ReportingMonthId,
    StatusId = ReportStatusDraftId,
    CreatedAt = DateTime.UtcNow
  };
}

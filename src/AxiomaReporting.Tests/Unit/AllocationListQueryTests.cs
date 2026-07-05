using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Web.Controllers;
using AxiomaReporting.Web.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProgramEntity = AxiomaReporting.Core.Entities.Program;

namespace AxiomaReporting.Tests.Unit;

public class AllocationListQueryTests : IDisposable
{
  private readonly AppDbContext _db;

  public AllocationListQueryTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    Seed();
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task BuildAllocationListQuery_Applies_DistrictAndProgramFilters()
  {
    var filter = new AllocationListFilterModel
    {
      DistrictIds = new List<int> { 1 },
      ProgramIds = new List<int> { 10 }
    };
    filter.Normalize();

    var results = await EmployeeController.BuildAllocationListQuery(_db, filter).ToListAsync();

    results.Should().ContainSingle();
    results[0].Id.Should().Be(100); // allocation that carries District 1 AND Program 10
  }

  [Fact]
  public async Task BuildAllocationListQuery_SectorMultiselect_MatchesAny()
  {
    var filter = new AllocationListFilterModel
    {
      SectorIds = new List<int> { 1, 2 }
    };
    filter.Normalize();

    var results = await EmployeeController.BuildAllocationListQuery(_db, filter).ToListAsync();

    results.Select(a => a.Id).Should().BeEquivalentTo(new[] { 100, 101 });
  }

  [Fact]
  public async Task BuildAllocationListQuery_OutputDurationFilter_SubstringMatchRequiresAll()
  {
    var filter = new AllocationListFilterModel
    {
      OutputDurations = new List<string> { "1", "Unlimited" }
    };
    filter.Normalize();

    var results = await EmployeeController.BuildAllocationListQuery(_db, filter).ToListAsync();

    results.Should().ContainSingle(a => a.Id == 101);
  }

  [Fact]
  public async Task BuildAllocationListQuery_NameAndCodeFilters_ApplyIndependently()
  {
    var filter = new AllocationListFilterModel
    {
      FirstName = "Alice",
      EmployeeCode = "E1"
    };
    filter.Normalize();

    var results = await EmployeeController.BuildAllocationListQuery(_db, filter).ToListAsync();

    results.Should().ContainSingle(a => a.Id == 100);
  }

  [Fact]
  public async Task ScopedAllocationDashboard_SystemAdminSeesAllAllocations()
  {
    var results = await (await AllocationsController.BuildScopedAllocationQueryAsync(
        _db,
        currentUserId: 999,
        UserRoleEnum.SystemAdmin))
      .ToListAsync();

    results.Select(a => a.Id).Should().BeEquivalentTo(new[] { 100, 101 });
  }

  [Fact]
  public async Task ScopedAllocationDashboard_EmployeeSeesOnlyOwnAllocations()
  {
    var results = await (await AllocationsController.BuildScopedAllocationQueryAsync(
        _db,
        currentUserId: 1,
        UserRoleEnum.Employee))
      .ToListAsync();

    results.Should().ContainSingle(a => a.Id == 100);
  }

  [Fact]
  public async Task ScopedAllocationDashboard_ManagerSeesOnlyAssignedAllocations()
  {
    _db.Users.Add(new User
    {
      Id = 50, EmployeeCode = "PM50", IdNumber = "500", FirstName = "Manager", LastName = "Scoped",
      PasswordHash = "h", RoleId = 1, UserRoleId = (int)UserRoleEnum.ProjectManager, StatusId = 1, CreatedAt = DateTime.UtcNow
    });
    _db.InspectorAssignments.Add(new InspectorAssignment { InspectorUserId = 50, DistrictId = 1 });
    await _db.SaveChangesAsync();

    var results = await (await AllocationsController.BuildScopedAllocationQueryAsync(
        _db,
        currentUserId: 50,
        UserRoleEnum.ProjectManager))
      .ToListAsync();

    results.Should().ContainSingle(a => a.Id == 100);
  }

  // ProjectManager/ProjectCoordinator manage ALL allocations regardless of InspectorAssignments
  // (H6 fix) - they must NOT be scoped like Inspector-View/Inspector-Approval.
  [Fact]
  public async Task ScopedAllocationDashboard_CoordinatorWithoutAssignmentsSeesAllAllocations()
  {
    var results = await (await AllocationsController.BuildScopedAllocationQueryAsync(
        _db,
        currentUserId: 51,
        UserRoleEnum.ProjectCoordinator))
      .ToListAsync();

    results.Select(a => a.Id).Should().BeEquivalentTo(new[] { 100, 101 });
  }

  [Fact]
  public async Task ScopedAllocationDashboard_ManagerWithoutAssignmentsSeesAllAllocations()
  {
    var results = await (await AllocationsController.BuildScopedAllocationQueryAsync(
        _db,
        currentUserId: 52,
        UserRoleEnum.ProjectManager))
      .ToListAsync();

    results.Select(a => a.Id).Should().BeEquivalentTo(new[] { 100, 101 });
  }

  // Only InspectorView/InspectorApproval are scoped by InspectorAssignments - an inspector
  // with no assignment rows should see nothing.
  [Fact]
  public async Task ScopedAllocationDashboard_InspectorViewWithoutAssignmentsSeesNoAllocations()
  {
    var results = await (await AllocationsController.BuildScopedAllocationQueryAsync(
        _db,
        currentUserId: 53,
        UserRoleEnum.InspectorView))
      .ToListAsync();

    results.Should().BeEmpty();
  }

  [Fact]
  public async Task GetProgramsForProjectAsync_ReturnsOnlyMapped_WhenMappingExists()
  {
    _db.ProjectPrograms.AddRange(
      new ProjectProgram { ProjectId = 1, ProgramId = 10 },
      new ProjectProgram { ProjectId = 1, ProgramId = 11 });
    await _db.SaveChangesAsync();

    var result = await EmployeeController.GetProgramsForProjectAsync(_db, projectId: 1);

    result.Select(x => x.Id).Should().BeEquivalentTo(new[] { 10, 11 });
  }

  [Fact]
  public async Task GetProgramsForProjectAsync_ReturnsEmpty_WhenNoMapping()
  {
    // Since v1.2.11 the all-active-programs fallback was removed: an unmapped
    // project yields an empty list.
    var result = await EmployeeController.GetProgramsForProjectAsync(_db, projectId: 1);

    result.Should().BeEmpty();
  }

  [Fact]
  public async Task GetProgramsForProjectAsync_NeverReturnsInactivePrograms()
  {
    var result = await EmployeeController.GetProgramsForProjectAsync(_db, projectId: 1);

    result.Should().NotContain(x => x.Id == 13); // 13 is inactive
  }

  private void Seed()
  {
    _db.Projects.AddRange(
      new Project { Id = 1, Description = "Proj A", IsActive = true },
      new Project { Id = 2, Description = "Proj B", IsActive = true });

    _db.Programs.AddRange(
      new ProgramEntity { Id = 10, Description = "Prog 10", IsActive = true },
      new ProgramEntity { Id = 11, Description = "Prog 11", IsActive = true },
      new ProgramEntity { Id = 12, Description = "Prog 12", IsActive = true },
      new ProgramEntity { Id = 13, Description = "Prog 13 (inactive)", IsActive = false });

    _db.Districts.AddRange(
      new District { Id = 1, Description = "D1", IsActive = true },
      new District { Id = 2, Description = "D2", IsActive = true });

    _db.Sectors.AddRange(
      new Sector { Id = 1, Description = "S1", IsActive = true },
      new Sector { Id = 2, Description = "S2", IsActive = true });

    _db.Users.AddRange(
      new User
      {
        Id = 1, EmployeeCode = "E1", IdNumber = "111", FirstName = "Alice", LastName = "A",
        PasswordHash = "h", RoleId = 1, UserRoleId = 6, StatusId = 1, CreatedAt = DateTime.UtcNow
      },
      new User
      {
        Id = 2, EmployeeCode = "E2", IdNumber = "222", FirstName = "Bob", LastName = "B",
        PasswordHash = "h", RoleId = 1, UserRoleId = 6, StatusId = 1, CreatedAt = DateTime.UtcNow
      });

    _db.Allocations.AddRange(
      new Allocation
      {
        Id = 100, UserId = 1, ProjectId = 1, IsActive = true,
        OutputDuration = "0.5,1",
        MonthlyEmploymentScope = 10m, AnnualEmploymentScope = 100m,
        CreatedAt = DateTime.UtcNow
      },
      new Allocation
      {
        Id = 101, UserId = 2, ProjectId = 2, IsActive = true,
        OutputDuration = "1,Unlimited",
        MonthlyEmploymentScope = 20m, AnnualEmploymentScope = 200m,
        CreatedAt = DateTime.UtcNow
      });
    _db.SaveChanges();

    _db.Set<AllocationDistrict>().AddRange(
      new AllocationDistrict { AllocationId = 100, DistrictId = 1 },
      new AllocationDistrict { AllocationId = 101, DistrictId = 2 });
    _db.Set<AllocationProgram>().AddRange(
      new AllocationProgram { AllocationId = 100, ProgramId = 10 },
      new AllocationProgram { AllocationId = 101, ProgramId = 11 });
    _db.Set<AllocationSector>().AddRange(
      new AllocationSector { AllocationId = 100, SectorId = 1 },
      new AllocationSector { AllocationId = 101, SectorId = 2 });
    _db.SaveChanges();
  }
}

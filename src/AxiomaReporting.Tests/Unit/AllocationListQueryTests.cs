using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Migrations;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Controllers;
using AxiomaReporting.Web.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Moq;
using System.Reflection;
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

  [Fact]
  public async Task ScopedAllocationDashboard_ManagerWithoutAssignmentsSeesNoAllocations()
  {
    var results = await (await AllocationsController.BuildScopedAllocationQueryAsync(
        _db,
        currentUserId: 51,
        UserRoleEnum.ProjectCoordinator))
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
  public async Task GetProgramsForProjectAsync_ReturnsAllActive_WhenNoMapping()
  {
    var result = await EmployeeController.GetProgramsForProjectAsync(_db, projectId: 1);

    // all three active programs come back (the seeded inactive one is excluded)
    result.Select(x => x.Id).Should().BeEquivalentTo(new[] { 10, 11, 12 });
  }

  [Fact]
  public async Task GetProgramsForProjectAsync_SkipsInactivePrograms_InFallback()
  {
    var result = await EmployeeController.GetProgramsForProjectAsync(_db, projectId: 1);

    result.Should().NotContain(x => x.Id == 13); // 13 is inactive
  }

  [Fact]
  public async Task ValuesForProgram_ReturnsEveryScopedDefaultList()
  {
    _db.ProjectProgramSubjects.Add(new ProjectProgramSubject { ProjectId = 1, ProgramId = 10, SubjectId = 101 });
    _db.ProjectProgramDomains.Add(new ProjectProgramDomain { ProjectId = 1, ProgramId = 10, DomainId = 102 });
    _db.ProjectProgramEducationalPrograms.Add(new ProjectProgramEducationalProgram { ProjectId = 1, ProgramId = 10, EducationalProgramId = 103 });
    _db.ProjectProgramDiscussionCodes.Add(new ProjectProgramDiscussionCode { ProjectId = 1, ProgramId = 10, DiscussionCodeId = 104 });
    _db.ProjectProgramFrameworks.Add(new ProjectProgramFramework { ProjectId = 1, ProgramId = 10, FrameworkId = 105 });
    _db.ProjectProgramGradeLevels.Add(new ProjectProgramGradeLevel { ProjectId = 1, ProgramId = 10, GradeLevelId = 106 });
    _db.ProjectProgramClasses.Add(new ProjectProgramClass { ProjectId = 1, ProgramId = 10, ClassId = 107 });
    _db.ProjectProgramLocalities.Add(new ProjectProgramLocality { ProjectId = 1, ProgramId = 10, LocalityId = 109 });
    _db.ProjectProgramLocalityDistrictNationals.Add(new ProjectProgramLocalityDistrictNational { ProjectId = 1, ProgramId = 10, LocalityDistrictNationalId = 108 });
    await _db.SaveChangesAsync();

    var controller = BuildEmployeeController();

    var result = (JsonResult)await controller.ValuesForProgram(projectId: 1, programId: 10);

    JsonIds(result, "subjectIds").Should().Equal(101);
    JsonIds(result, "domainIds").Should().Equal(102);
    JsonIds(result, "educationalProgramIds").Should().Equal(103);
    JsonIds(result, "discussionCodeIds").Should().Equal(104);
    JsonIds(result, "frameworkIds").Should().Equal(105);
    JsonIds(result, "gradeLevelIds").Should().Equal(106);
    JsonIds(result, "classIds").Should().Equal(107);
    JsonIds(result, "localityIds").Should().Equal(109);
    JsonIds(result, "localityDistrictNationalIds").Should().Equal(108);
  }

  [Fact]
  public void MergeDuplicateProgramsMigration_CoversExpectedPairsAndReferencingTables()
  {
    var migration = new MergeDuplicateProgramsAndSeedProjectProgramScopes();
    var builder = new MigrationBuilder("Microsoft.EntityFrameworkCore.SqlServer");
    typeof(MergeDuplicateProgramsAndSeedProjectProgramScopes)
      .GetMethod("Up", BindingFlags.Instance | BindingFlags.NonPublic)!
      .Invoke(migration, new object[] { builder });

    var sql = builder.Operations.OfType<SqlOperation>().Should().ContainSingle().Subject.Sql;

    foreach (var pair in new[]
    {
      "(1, 89)", "(2, 95)", "(3, 93)", "(4, 94)", "(5, 87)",
      "(6, 96)", "(7, 100)", "(8, 92)", "(9, 97)", "(10, 90)",
      "(11, 91)", "(85, 104)", "(101, 89)", "(102, 88)", "(103, 87)"
    })
      sql.Should().Contain(pair);

    foreach (var table in new[]
    {
      "AllocationPrograms",
      "ProjectPrograms",
      "ProjectProgramFrameworks",
      "ProjectProgramGradeLevels",
      "ProjectProgramClasses",
      "ProjectProgramSubjects",
      "ProjectProgramDomains",
      "ProjectProgramEducationalPrograms",
      "ProjectProgramDiscussionCodes",
      "ProjectProgramLocalityDistrictNationals",
      "InspectorAssignments"
    })
      sql.Should().Contain($"dbo.{table}");

    var parentInsert = sql.IndexOf("INSERT INTO dbo.ProjectPrograms", StringComparison.Ordinal);
    var firstScopeRemap = sql.IndexOf("FROM dbo.ProjectProgramFrameworks oldRow", StringComparison.Ordinal);
    var parentDelete = System.Text.RegularExpressions.Regex
      .Match(sql, @"DELETE oldRow\s+FROM dbo\.ProjectPrograms oldRow")
      .Index;
    var localityScopeRemap = sql.IndexOf("FROM dbo.ProjectProgramLocalityDistrictNationals oldRow", StringComparison.Ordinal);
    parentInsert.Should().BeGreaterThanOrEqualTo(0);
    parentInsert.Should().BeLessThan(firstScopeRemap,
      because: "canonical ProjectPrograms parent rows must exist before remapping child scope rows");
    parentDelete.Should().BeGreaterThan(localityScopeRemap,
      because: "old ProjectPrograms rows must be deleted only after all child scope rows are remapped");

    sql.Should().Contain("DELETE oldProgram");
    sql.Should().Contain("SET IsActive = 0");
  }

  [Fact]
  public void SeedProjectSixProgramScopeDefaultsMigration_UsesUtf8HebrewSeedAndAllScopeTables()
  {
    var migration = new SeedProjectSixProgramScopeDefaults();
    var builder = new MigrationBuilder("Microsoft.EntityFrameworkCore.SqlServer");
    typeof(SeedProjectSixProgramScopeDefaults)
      .GetMethod("Up", BindingFlags.Instance | BindingFlags.NonPublic)!
      .Invoke(migration, new object[] { builder });

    var sql = builder.Operations.OfType<SqlOperation>().Should().ContainSingle().Subject.Sql;

    sql.Should().Contain("\u05d0\u05d5\u05e8 \u05d1\u05d2\u05e0\u05d9\u05dd");
    sql.Should().Contain("\u05e8\u05d5\u05d5\u05d7\u05d4 \u05d5\u05e7\u05d4\u05d9\u05dc\u05d4");
    sql.Should().NotContain("\u05f3\u00b3", because: "Hebrew seed values must not be double-encoded mojibake");

    foreach (var table in new[]
    {
      "ProjectProgramFrameworks",
      "ProjectProgramGradeLevels",
      "ProjectProgramClasses",
      "ProjectProgramSubjects",
      "ProjectProgramDomains",
      "ProjectProgramEducationalPrograms",
      "ProjectProgramDiscussionCodes",
      "ProjectProgramLocalityDistrictNationals"
    })
      sql.Should().Contain($"dbo.{table}");
  }

  [Fact]
  public void SeedProjectSixFrameworkScopeBySymbolMigration_MapsFrameworksByInstitutionSymbol()
  {
    var migration = new SeedProjectSixFrameworkScopeBySymbol();
    var builder = new MigrationBuilder("Microsoft.EntityFrameworkCore.SqlServer");
    typeof(SeedProjectSixFrameworkScopeBySymbol)
      .GetMethod("Up", BindingFlags.Instance | BindingFlags.NonPublic)!
      .Invoke(migration, new object[] { builder });

    var sql = builder.Operations.OfType<SqlOperation>().Should().ContainSingle().Subject.Sql;

    sql.Should().Contain("DECLARE @FrameworkSeed");
    sql.Should().Contain("(100, N'442087')");
    sql.Should().Contain("framework.InstitutionSymbol = seed.InstitutionSymbol");
    sql.Should().Contain("TRY_CONVERT(int, framework.InstitutionSymbol)");
    sql.Should().Contain("dbo.ProjectProgramFrameworks");
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

  private EmployeeController BuildEmployeeController() => new(
    Mock.Of<IEmployeeService>(),
    Mock.Of<ICurrentUserService>(),
    Mock.Of<IPasswordService>(),
    Mock.Of<IReportStatusService>(),
    Mock.Of<IReportExcelImportService>(),
    Mock.Of<IAuditLogService>(),
    _db);

  private static int[] JsonIds(JsonResult result, string propertyName)
  {
    var value = result.Value!.GetType().GetProperty(propertyName)!.GetValue(result.Value);
    return ((IEnumerable<int>)value!).ToArray();
  }
}

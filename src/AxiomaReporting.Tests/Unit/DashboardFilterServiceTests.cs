using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProgramEntity = AxiomaReporting.Core.Entities.Program;

namespace AxiomaReporting.Tests.Unit;

public class DashboardFilterServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly DashboardFilterService _sut;

  public DashboardFilterServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new DashboardFilterService(_db);
    Seed();
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task GetReportsAsync_ReturnsScopedRowsAndAppliesAllocationFilters()
  {
    var result = await _sut.GetReportsAsync(
      new DashboardFilter { DistrictId = 1, Page = 0, PageSize = 50000 },
      currentUserId: 99,
      currentUserRole: UserRoleEnum.SystemAdmin);

    result.TotalCount.Should().Be(1);
    result.Rows.Should().ContainSingle();
    result.Rows[0].FullName.Should().Be("Alpha Employee");
    result.Rows[0].RowCount.Should().Be(1);
    result.Rows[0].TotalDuration.Should().Be(2);
    result.Rows[0].MonthlyRowAllocation.Should().Be(10);
  }

  [Fact]
  public async Task GetReportsAsync_StatusZeroReturnsMissingReportsForResolvedMonth()
  {
    _db.Reports.Add(new Report
    {
      Id = 3,
      UserId = 2,
      ReportingMonthId = 1,
      StatusId = 1,
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    var result = await _sut.GetReportsAsync(
      new DashboardFilter { StatusId = 0, FromMonthId = 1, ToMonthId = 1 },
      currentUserId: 99,
      currentUserRole: UserRoleEnum.SystemAdmin);

    result.TotalCount.Should().Be(1);
    result.Rows.Should().ContainSingle(r => r.UserId == 2 && r.ReportId == 0 && r.StatusId == 0);
  }

  [Fact]
  public async Task GetReportsAsync_AppliesRequestedSort()
  {
    _db.Reports.Add(new Report
    {
      Id = 2,
      UserId = 2,
      ReportingMonthId = 1,
      StatusId = 3,
      SubmittedAt = DateTime.UtcNow.AddDays(-1),
      CreatedAt = DateTime.UtcNow
    });
    _db.ReportRows.Add(new ReportRow
    {
      Id = 2,
      ReportId = 2,
      AllocationId = 2,
      SequenceNumber = 1,
      MeetingDate = DateTime.Today,
      MeetingDuration = 1,
      DistrictId = 1,
      LocalityId = 1,
      FrameworkId = 1,
      EducationalProgramId = 1,
      DomainId = 1,
      Subject1Id = 1,
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    var result = await _sut.GetReportsAsync(
      new DashboardFilter { SortBy = "code", SortDesc = true },
      currentUserId: 99,
      currentUserRole: UserRoleEnum.SystemAdmin);

    result.Rows.Select(r => r.EmployeeCode).Should().Equal("EMP2", "EMP1");
  }

  [Fact]
  public async Task GetReportsAsync_SplitsOneEmployeeReportByAllocation()
  {
    _db.Allocations.Add(new Allocation
    {
      Id = 3,
      UserId = 1,
      ProjectId = 2,
      IsActive = true,
      MonthlyRowAllocation = 12,
      CreatedAt = DateTime.UtcNow
    });
    _db.Set<AllocationDistrict>().Add(new AllocationDistrict { AllocationId = 3, DistrictId = 1 });
    _db.ReportRows.Add(new ReportRow
    {
      Id = 3,
      ReportId = 1,
      AllocationId = 3,
      SequenceNumber = 2,
      MeetingDate = DateTime.Today,
      MeetingDuration = 3,
      DistrictId = 1,
      LocalityId = 1,
      FrameworkId = 1,
      EducationalProgramId = 1,
      DomainId = 1,
      Subject1Id = 1,
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    var result = await _sut.GetReportsAsync(
      new DashboardFilter { DistrictId = 1, PageSize = 25 },
      currentUserId: 99,
      currentUserRole: UserRoleEnum.SystemAdmin);

    result.TotalCount.Should().Be(2);
    result.Rows.Should().HaveCount(2);
    result.Rows.Should().OnlyContain(r => r.UserId == 1 && r.ReportId == 1);
    result.Rows.Select(r => r.AllocationId).Should().BeEquivalentTo(new[] { 1, 3 });
    result.Rows.Sum(r => r.RowCount).Should().Be(2);
  }

  [Fact]
  public async Task CanAccessReportAsync_UsesRoleAndInspectorScope()
  {
    (await _sut.CanAccessReportAsync(1, 1, UserRoleEnum.Employee)).Should().BeTrue();
    (await _sut.CanAccessReportAsync(1, 2, UserRoleEnum.Employee)).Should().BeFalse();
    (await _sut.CanAccessReportAsync(1, 99, UserRoleEnum.ProjectManager)).Should().BeTrue();
    (await _sut.CanAccessReportAsync(1, 3, UserRoleEnum.InspectorView)).Should().BeTrue();
    (await _sut.CanAccessReportAsync(1, 4, UserRoleEnum.InspectorView)).Should().BeFalse();
    (await _sut.CanAccessReportAsync(999, 99, UserRoleEnum.SystemAdmin)).Should().BeFalse();
  }

  [Fact]
  public async Task FilterLookups_ReturnOnlyValuesInsideCurrentScope()
  {
    var districts = await _sut.GetFilteredDistrictsAsync(3, UserRoleEnum.InspectorView);
    var sectors = await _sut.GetFilteredSectorsAsync(3, UserRoleEnum.InspectorView, districtId: 1);
    var programs = await _sut.GetFilteredProgramsAsync(3, UserRoleEnum.InspectorView, districtId: 1);

    districts.Should().ContainSingle(d => d.Id == 1);
    sectors.Should().ContainSingle(s => s.Id == 1);
    programs.Should().ContainSingle(p => p.Id == 1);
  }

  [Fact]
  public async Task ProgramFilter_IsCombinedWithOtherFiltersUsingAnd()
  {
    var allowedPrograms = await _sut.GetFilteredProgramsAsync(99, UserRoleEnum.ProjectManager);
    allowedPrograms.Should().ContainSingle(p => p.Id == 1);
    var scopedWithoutFilters = await _sut.GetReportRowsAsync(
      new DashboardFilter(), 99, UserRoleEnum.ProjectManager);
    scopedWithoutFilters.TotalCount.Should().Be(1);
    scopedWithoutFilters.Rows.Should().ContainSingle(r => r.ReportId == 1);
    var matching = await _sut.GetReportRowsAsync(
      new DashboardFilter { ProgramId = 1, DistrictId = 1 }, 99, UserRoleEnum.ProjectManager);
    var unauthorized = await _sut.GetReportRowsAsync(
      new DashboardFilter { ProgramId = 2, DistrictId = 1 }, 99, UserRoleEnum.ProjectManager);

    matching.Rows.Should().ContainSingle(r => r.ReportId == 1);
    unauthorized.Rows.Should().BeEmpty("a manually supplied program id outside the manager assignment must not expose data");
  }

  [Fact]
  public async Task ExistingReports_RemainVisibleWhenEmployeeAndAllocationAreInactive()
  {
    _db.Users.Find(1)!.StatusId = 2;
    _db.Allocations.Find(1)!.IsActive = false;
    await _db.SaveChangesAsync();

    var result = await _sut.GetReportRowsAsync(
      new DashboardFilter(), 99, UserRoleEnum.SystemAdmin);

    result.Rows.Should().ContainSingle(r => r.ReportId == 1);
  }

  [Fact]
  public async Task GetAllReportRowsAsync_IgnoresUiPaginationAndReturnsEveryFilteredRow()
  {
    for (var id = 10; id < 40; id++)
    {
      _db.ReportRows.Add(new ReportRow
      {
        Id = id, ReportId = 1, AllocationId = 1, SequenceNumber = id,
        MeetingDate = DateTime.Today, MeetingDuration = 1, DistrictId = 1,
        LocalityId = 1, FrameworkId = 1, EducationalProgramId = 1,
        DomainId = 1, Subject1Id = 1, CreatedAt = DateTime.UtcNow
      });
    }
    await _db.SaveChangesAsync();

    var rows = await _sut.GetAllReportRowsAsync(
      new DashboardFilter { Page = 2, PageSize = 1, ProgramId = 1 },
      99, UserRoleEnum.SystemAdmin);

    rows.Should().HaveCount(31);
  }

  private void Seed()
  {
    _db.Users.AddRange(
      User(1, "Alpha", "Employee", isReporting: true),
      User(2, "Missing", "Employee", isReporting: true),
      User(3, "Scoped", "Inspector", isReporting: false),
      User(4, "Empty", "Inspector", isReporting: false),
      User(99, "Scoped", "Manager", isReporting: false));

    _db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 1,
      Description = "April 2026",
      Month = 4,
      Year = 2026,
      IsActive = true,
      LastReportingDate = DateTime.Today.AddDays(5),
      CreatedAt = DateTime.UtcNow
    });
    _db.ReportStatuses.AddRange(
      new ReportStatus { Id = 3, Name = "Pending" },
      new ReportStatus { Id = 4, Name = "Approved" });
    _db.Districts.Add(new District { Id = 1, Description = "District 1", IsActive = true });
    _db.Sectors.Add(new Sector { Id = 1, Description = "Sector 1", IsActive = true });
    _db.Localities.Add(new Locality { Id = 1, Description = "Locality 1", IsActive = true });
    _db.Frameworks.Add(new Framework { Id = 1, Description = "Framework 1", InstitutionSymbol = "00100", IsActive = true });
    _db.EducationalPrograms.Add(new EducationalProgram { Id = 1, Description = "Educational Program 1", IsActive = true });
    _db.Domains.Add(new Domain { Id = 1, Description = "Domain 1", IsActive = true });
    _db.Subjects.Add(new Subject { Id = 1, Description = "Subject 1", IsActive = true });
    _db.Programs.AddRange(
      new ProgramEntity { Id = 1, Description = "Program 1", IsActive = true },
      new ProgramEntity { Id = 2, Description = "Program 2", IsActive = true });
    _db.Projects.AddRange(
      new Project { Id = 1, Description = "Project 1", IsActive = true },
      new Project { Id = 2, Description = "Project 2", IsActive = true });

    _db.Allocations.AddRange(
      new Allocation { Id = 1, UserId = 1, ProjectId = 1, IsActive = true, MonthlyRowAllocation = 10, CreatedAt = DateTime.UtcNow },
      new Allocation { Id = 2, UserId = 2, ProjectId = 1, IsActive = true, MonthlyRowAllocation = 5, CreatedAt = DateTime.UtcNow });
    _db.Set<AllocationDistrict>().AddRange(
      new AllocationDistrict { AllocationId = 1, DistrictId = 1 },
      new AllocationDistrict { AllocationId = 2, DistrictId = 1 });
    _db.Set<AllocationSector>().Add(new AllocationSector { AllocationId = 1, SectorId = 1 });
    _db.Set<AllocationProgram>().Add(new AllocationProgram { AllocationId = 1, ProgramId = 1 });
    _db.Set<AllocationProgram>().Add(new AllocationProgram { AllocationId = 2, ProgramId = 2 });

    _db.Reports.Add(new Report
    {
      Id = 1,
      UserId = 1,
      ReportingMonthId = 1,
      StatusId = 3,
      SubmittedAt = DateTime.UtcNow,
      CreatedAt = DateTime.UtcNow
    });
    _db.ReportRows.Add(new ReportRow
    {
      Id = 1,
      ReportId = 1,
      AllocationId = 1,
      SequenceNumber = 1,
      MeetingDate = DateTime.Today,
      MeetingDuration = 2,
      DistrictId = 1,
      LocalityId = 1,
      FrameworkId = 1,
      EducationalProgramId = 1,
      DomainId = 1,
      Subject1Id = 1,
      CreatedAt = DateTime.UtcNow
    });
    _db.InspectorAssignments.Add(new InspectorAssignment
    {
      Id = 1,
      InspectorUserId = 3,
      ProgramId = 1
    });
    _db.InspectorAssignments.Add(new InspectorAssignment
    {
      Id = 2,
      InspectorUserId = 99,
      ProgramId = 1
    });
    _db.SaveChanges();
  }

  private static User User(int id, string first, string last, bool isReporting) => new()
  {
    Id = id,
    EmployeeCode = $"EMP{id}",
    IdNumber = id.ToString(),
    FirstName = first,
    LastName = last,
    PasswordHash = "hash",
    StatusId = 1,
    UserRoleId = isReporting ? (int)UserRoleEnum.Employee : (int)UserRoleEnum.InspectorView,
    IsReportingEmployee = isReporting,
    CreatedAt = DateTime.UtcNow
  };
}

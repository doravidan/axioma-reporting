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

  private void Seed()
  {
    _db.Users.AddRange(
      User(1, "Alpha", "Employee", isReporting: true),
      User(2, "Missing", "Employee", isReporting: true),
      User(3, "Scoped", "Inspector", isReporting: false),
      User(4, "Empty", "Inspector", isReporting: false));

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
    _db.Programs.Add(new ProgramEntity { Id = 1, Description = "Program 1", IsActive = true });

    _db.Allocations.AddRange(
      new Allocation { Id = 1, UserId = 1, ProjectId = 1, IsActive = true, MonthlyRowAllocation = 10, CreatedAt = DateTime.UtcNow },
      new Allocation { Id = 2, UserId = 2, ProjectId = 1, IsActive = true, MonthlyRowAllocation = 5, CreatedAt = DateTime.UtcNow });
    _db.Set<AllocationDistrict>().AddRange(
      new AllocationDistrict { AllocationId = 1, DistrictId = 1 },
      new AllocationDistrict { AllocationId = 2, DistrictId = 1 });
    _db.Set<AllocationSector>().Add(new AllocationSector { AllocationId = 1, SectorId = 1 });
    _db.Set<AllocationProgram>().Add(new AllocationProgram { AllocationId = 1, ProgramId = 1 });

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
      DistrictId = 1
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

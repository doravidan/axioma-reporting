using System.Net;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

/// <summary>
/// Integration tests for the /MyAllocations Employee submenu landing page.
///
/// Covers:
///  - Authenticated GET returns 200 and displays the active reporting month name.
///  - When the user has NO allocation with AllowExcelUpload=true, the Excel upload
///    tile is not rendered (link to /Report/UploadExcel absent).
///  - When at least one active allocation grants AllowExcelUpload=true, the tile
///    IS rendered.
/// </summary>
public class MyAllocationsFlowTests : IDisposable
{
  private readonly CustomWebApplicationFactory _factory;
  private int _projectId;
  private int _allocationId;
  private int _otherAllocationId;
  private int _reportingMonthId;

  public MyAllocationsFlowTests()
  {
    _factory = new CustomWebApplicationFactory();
    SeedFixture(allowExcelUpload: false);
  }

  public void Dispose() => _factory.DisposeAsync().AsTask().GetAwaiter().GetResult();

  [Fact]
  public async Task Employee_GetMyAllocations_Returns200_ContainsActiveMonthName()
  {
    var client = await AccessControlTests.SignInAsAsync(_factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var response = await client.GetAsync("/MyAllocations");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var body = System.Net.WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync());
    body.Should().Contain("מאי 2026", "the active reporting month label must appear in the banner");
    body.Should().Contain("עדכון פעילות חודשית", "the monthly-update tile must always render");
    body.Should().Contain("MyAlloc Project", "the employee's own allocations should render in the allocations table");
    body.Should().NotContain("Other User Project", "employees must not see allocations owned by another user");
  }

  [Fact]
  public async Task Employee_WhenNoAllocationAllowsExcel_UploadTileIsHidden()
  {
    var client = await AccessControlTests.SignInAsAsync(_factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var response = await client.GetAsync("/MyAllocations");
    var body = System.Net.WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync());

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    body.Should().NotContain("העלאת אקסל חודשי",
      "no allocation grants AllowExcelUpload, so the upload tile must be hidden");
  }

  [Fact]
  public async Task Employee_WhenAtLeastOneAllocationAllowsExcel_UploadTileIsRendered()
  {
    // Recreate factory with an allocation that grants Excel upload.
    Dispose(); // dispose initial factory
    var factory = new CustomWebApplicationFactory();
    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      foreach (var existingMonth in db.ReportingMonths.ToList())
      {
        existingMonth.IsActive = false;
      }
      db.SaveChanges();
      db.ReportingMonths.Add(new ReportingMonth
      {
        Month = 5,
        Year = 2026,
        Description = "מאי 2026",
        LastReportingDate = DateTime.UtcNow.AddDays(20),
        IsActive = true,
        CreatedAt = DateTime.UtcNow
      });
      var project = new Project { Description = "MyAlloc Project", IsActive = true, CreatedAt = DateTime.UtcNow };
      db.Projects.Add(project);
      db.SaveChanges();

      db.Allocations.Add(new Allocation
      {
        UserId = 1,
        ProjectId = project.Id,
        IsActive = true,
        AllowExcelUpload = true,
        CreatedAt = DateTime.UtcNow
      });
      db.SaveChanges();
    }

    var client = await AccessControlTests.SignInAsAsync(factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);
    var response = await client.GetAsync("/MyAllocations");
    var body = System.Net.WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync());

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    body.Should().Contain("העלאת אקסל חודשי",
      "an allocation grants AllowExcelUpload, so the tile must render");

    await factory.DisposeAsync();
  }

  [Fact]
  public async Task Employee_CanOpenOwnAllocationDetails_ReadOnly()
  {
    var client = await AccessControlTests.SignInAsAsync(_factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var response = await client.GetAsync($"/MyAllocations/Details/{_allocationId}");
    var body = System.Net.WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync());

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    body.Should().Contain("פרטי הקצאה");
    body.Should().Contain("MyAlloc Project");
    body.Should().Contain("חזרה להקצאות");
    body.Should().NotContain("שמור הקצאה", "regular employees only get a read-only allocation details screen");
  }

  [Fact]
  public async Task Employee_AllocationDashboardShowsOnlyOwnRows()
  {
    var client = await AccessControlTests.SignInAsAsync(_factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var response = await client.GetAsync("/allocations");
    var body = System.Net.WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync());

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    body.Should().Contain("הקצאות עובדים");
    body.Should().Contain("MyAlloc Project");
    body.Should().NotContain("Other User Project");
    body.Should().Contain($"/allocations/{_allocationId}");
  }

  [Fact]
  public async Task Employee_AllocationDashboardDetailsAllowsOnlyOwnRows()
  {
    var client = await AccessControlTests.SignInAsAsync(_factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var own = await client.GetAsync($"/allocations/{_allocationId}");
    var ownBody = System.Net.WebUtility.HtmlDecode(await own.Content.ReadAsStringAsync());
    var other = await client.GetAsync($"/allocations/{_otherAllocationId}");

    own.StatusCode.Should().Be(HttpStatusCode.OK);
    ownBody.Should().Contain("פרטי הקצאה");
    ownBody.Should().Contain("MyAlloc Project");
    other.StatusCode.Should().Be(HttpStatusCode.NotFound);
  }

  [Fact]
  public async Task Employee_CannotOpenAnotherUsersAllocationDetails()
  {
    var client = await AccessControlTests.SignInAsAsync(_factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var response = await client.GetAsync($"/MyAllocations/Details/{_otherAllocationId}");

    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
  }

  [Fact]
  public async Task Employee_CanExportOnlyOwnAllocations()
  {
    var client = await AccessControlTests.SignInAsAsync(_factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var response = await client.GetAsync("/MyAllocations/ExportExcel");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Content.Headers.ContentType?.MediaType.Should()
      .Be("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
  }

  // ─── Fixture seeding ────────────────────────────────────────────────────────

  private void SeedFixture(bool allowExcelUpload)
  {
    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    foreach (var existingMonth in db.ReportingMonths.ToList())
    {
      existingMonth.IsActive = false;
    }
    db.SaveChanges();

    var month = new ReportingMonth
    {
      Month = 5,
      Year = 2026,
      Description = "מאי 2026",
      LastReportingDate = DateTime.UtcNow.AddDays(20),
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    };
    db.ReportingMonths.Add(month);

    var project = new Project { Description = "MyAlloc Project", IsActive = true, CreatedAt = DateTime.UtcNow };
    db.Projects.Add(project);
    var otherProject = new Project { Description = "Other User Project", IsActive = true, CreatedAt = DateTime.UtcNow };
    db.Projects.Add(otherProject);
    db.SaveChanges();
    _projectId = project.Id;
    _reportingMonthId = month.Id;

    var allocation = new Allocation
    {
      UserId = 1, // TestData seeded employee
      ProjectId = _projectId,
      IsActive = true,
      AllowExcelUpload = allowExcelUpload,
      MonthlyEmploymentScope = 180,
      DailyEmploymentScope = 9,
      AnnualEmploymentScope = 1800,
      CreatedAt = DateTime.UtcNow
    };
    var otherAllocation = new Allocation
    {
      UserId = 2, // TestData seeded admin, used here to prove scoping by current user
      ProjectId = otherProject.Id,
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    };
    db.Allocations.AddRange(allocation, otherAllocation);
    db.SaveChanges();
    _allocationId = allocation.Id;
    _otherAllocationId = otherAllocation.Id;
  }
}

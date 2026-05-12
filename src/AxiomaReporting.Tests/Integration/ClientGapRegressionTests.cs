using System.Net;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

public class ClientGapRegressionTests
{
  [Fact]
  public async Task EfSeededAdmin_IsNotPreAcceptedForTerms()
  {
    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase("seed-admin-terms-" + Guid.NewGuid())
      .Options;

    await using var db = new AppDbContext(options);
    await db.Database.EnsureCreatedAsync();

    var admin = await db.Users.SingleAsync(u => u.IdNumber == "admin");

    admin.AcceptedTermsOfUse.Should().BeFalse();
    admin.MustChangePassword.Should().BeTrue();
    new PasswordService().VerifyPassword("admin1234", admin.PasswordHash).Should().BeTrue();
    (await db.TermsOfUseAcceptances.AnyAsync(a => a.UserId == admin.Id)).Should().BeFalse();
  }

  [Fact]
  public async Task AllocationListPages_DoNotUseHoursOrPeriodTerminology()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedAllocationExportFixture(factory);
    var adminClient = await AccessControlTests.SignInAsAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);
    var employeeClient = await AccessControlTests.SignInAsAsync(factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var adminAllocationList = await adminClient.GetStringAsync("/Employee/AllocationList");
    var scopedAllocationList = await adminClient.GetStringAsync("/allocations");
    var employeeAllocations = await employeeClient.GetStringAsync("/MyAllocations");

    foreach (var body in new[] { adminAllocationList, scopedAllocationList, employeeAllocations })
    {
      body.Should().NotContain("היקף שעות");
      body.Should().NotContain("משך תקופה");
      body.Should().Contain("משך תפוקה");
    }
  }

  [Fact]
  public async Task AllocationExports_UseActivityAndOutputDurationHeaders()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedAllocationExportFixture(factory);

    var adminClient = await AccessControlTests.SignInAsAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);
    var employeeClient = await AccessControlTests.SignInAsAsync(factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    await AssertWorkbookTerminologyAsync(await adminClient.GetAsync("/Employee/ExportAllocationsExcel"));
    await AssertWorkbookTerminologyAsync(await adminClient.GetAsync("/allocations/export"));
    await AssertWorkbookTerminologyAsync(await employeeClient.GetAsync("/MyAllocations/ExportExcel"));
  }

  [Fact]
  public async Task AllocationListPages_ShowRealAllocationFields()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedAllocationExportFixture(factory);
    var adminClient = await AccessControlTests.SignInAsAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);

    foreach (var path in new[] { "/Employee/AllocationList", "/allocations" })
    {
      var body = await adminClient.GetStringAsync(path);

      body.Should().Contain("היקף פעילות יומי");
      body.Should().Contain("הקצאת שורות חודשית");
      body.Should().Contain("הקצאת שורות שנתית");
      body.Should().Contain("העלאת אקסל");
      body.Should().Contain("9");
      body.Should().Contain("180");
      body.Should().Contain("1800");
    }
  }

  private static async Task AssertWorkbookTerminologyAsync(HttpResponseMessage response)
  {
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var bytes = await response.Content.ReadAsByteArrayAsync();
    using var stream = new MemoryStream(bytes);
    using var workbook = new XLWorkbook(stream);
    var ws = workbook.Worksheets.First();
    var headers = ws.Row(1).CellsUsed().Select(c => c.GetString()).ToList();

    headers.Should().Contain(h => h.Contains("היקף פעילות"));
    headers.Should().Contain("משך תפוקה");
    headers.Should().NotContain(h => h.Contains("היקף שעות"));
    headers.Should().NotContain("משך תקופה");
  }

  private static void SeedAllocationExportFixture(CustomWebApplicationFactory factory)
  {
    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var project = new AxiomaReporting.Core.Entities.Project
    {
      Description = "Export Project",
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    };
    db.Projects.Add(project);
    db.SaveChanges();

    db.Allocations.Add(new AxiomaReporting.Core.Entities.Allocation
    {
      UserId = 1,
      ProjectId = project.Id,
      MonthlyEmploymentScope = 10,
      DailyEmploymentScope = 9,
      AnnualEmploymentScope = 100,
      MonthlyRowAllocation = 180,
      AnnualRowAllocation = 1800,
      OutputDuration = "1,2",
      AllowExcelUpload = true,
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    });
    db.SaveChanges();
  }
}

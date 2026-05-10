using System.Net;
using System.Text.Json;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProgramEntity = AxiomaReporting.Core.Entities.Program;

namespace AxiomaReporting.Tests.Integration;

public class DashboardFilterOptionsTests
{
  [Fact]
  public async Task FilterOptions_NoSelection_ReturnsBothDistricts()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedDashboardFixture(factory);
    var client = await SignInAdminAsync(factory);

    var response = await client.GetAsync("/Dashboard/FilterOptions");
    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var dto = await DeserializeAsync(response);
    dto.Districts.Select(d => d.Id).Should().BeEquivalentTo(new[] { 1, 2 });
  }

  [Fact]
  public async Task FilterOptions_WithUnrelatedProgram_ReturnsEmptyDistricts()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedDashboardFixture(factory);
    var client = await SignInAdminAsync(factory);

    // Program 99 has NO reports -> should yield empty districts
    var selected = JsonSerializer.Serialize(new { ProgramId = 99 });
    var response = await client.GetAsync($"/Dashboard/FilterOptions?selected={Uri.EscapeDataString(selected)}");
    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var dto = await DeserializeAsync(response);
    dto.Districts.Should().BeEmpty();
  }

  [Fact]
  public async Task FilterOptions_WithUsedProgram_ReturnsOnlyDistrictA()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedDashboardFixture(factory);
    var client = await SignInAdminAsync(factory);

    // Program 1 is tied to the single report row in District A (1)
    var selected = JsonSerializer.Serialize(new { ProgramId = 1 });
    var response = await client.GetAsync($"/Dashboard/FilterOptions?selected={Uri.EscapeDataString(selected)}");
    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var dto = await DeserializeAsync(response);
    dto.Districts.Should().ContainSingle(d => d.Id == 1);
  }

  [Fact]
  public async Task Index_ShowWithoutFilters_RendersResultsWithoutModelNullCrash()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedDashboardFixture(factory);
    var client = await SignInAdminAsync(factory);

    var response = await client.GetAsync("/Dashboard/Index?show=1");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var body = await response.Content.ReadAsStringAsync();
    body.Should().Contain("דשבורד דיווחים");
    body.Should().Contain("נמצאו");
  }

  private static async Task<FilterOptionsDto> DeserializeAsync(HttpResponseMessage response)
  {
    var body = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<FilterOptionsDto>(body, new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    })!;
  }

  private static async Task<HttpClient> SignInAdminAsync(CustomWebApplicationFactory factory)
  {
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });
    var loginHtml = await client.GetStringAsync("/Account/Login");
    var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.AdminIdNumber,
      ["Password"] = TestData.AdminPassword
    }));
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    return client;
  }

  private static void SeedDashboardFixture(CustomWebApplicationFactory factory)
  {
    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Districts.AddRange(
      new District { Id = 1, Description = "District A", IsActive = true },
      new District { Id = 2, Description = "District B", IsActive = true });
    db.Sectors.Add(new Sector { Id = 1, Description = "Sector", IsActive = true });
    db.Programs.Add(new ProgramEntity { Id = 1, Description = "Program 1", IsActive = true });
    db.Programs.Add(new ProgramEntity { Id = 99, Description = "Program 99", IsActive = true });
    db.Projects.Add(new Project { Id = 1, Description = "Project 1", IsActive = true });

    db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 10,
      Description = "Jan 2026",
      Year = 2026,
      Month = 1,
      IsActive = true,
      LastReportingDate = DateTime.Today.AddDays(7),
      CreatedAt = DateTime.UtcNow
    });
    db.ReportStatuses.AddRange(
      new ReportStatus { Id = 3, Name = "Pending" },
      new ReportStatus { Id = 4, Name = "Approved" });

    // Allocations: employee 1 belongs to both districts with program 1 on District A
    db.Allocations.AddRange(
      new Allocation { Id = 10, UserId = 1, ProjectId = 1, IsActive = true, MonthlyRowAllocation = 5, CreatedAt = DateTime.UtcNow },
      new Allocation { Id = 20, UserId = 1, ProjectId = 1, IsActive = true, MonthlyRowAllocation = 5, CreatedAt = DateTime.UtcNow });
    db.Set<AllocationDistrict>().AddRange(
      new AllocationDistrict { AllocationId = 10, DistrictId = 1 },
      new AllocationDistrict { AllocationId = 20, DistrictId = 2 });
    db.Set<AllocationSector>().AddRange(
      new AllocationSector { AllocationId = 10, SectorId = 1 },
      new AllocationSector { AllocationId = 20, SectorId = 1 });
    db.Set<AllocationProgram>().AddRange(
      new AllocationProgram { AllocationId = 10, ProgramId = 1 });

    // One report in District A using allocation 10
    db.Reports.Add(new Report
    {
      Id = 100,
      UserId = 1,
      ReportingMonthId = 10,
      StatusId = 3,
      SubmittedAt = DateTime.UtcNow,
      CreatedAt = DateTime.UtcNow
    });
    db.ReportRows.Add(new ReportRow
    {
      Id = 1000,
      ReportId = 100,
      AllocationId = 10,
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

    db.SaveChanges();
  }
}

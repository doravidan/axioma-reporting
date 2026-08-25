using System.Net;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Tests.TestSupport;
using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProgramEntity = AxiomaReporting.Core.Entities.Program;

namespace AxiomaReporting.Tests.Integration;

public class DashboardAndInstitutionExportRegressionTests
{
  [Fact]
  public async Task DashboardExport_UsesAllFilteredRows_AllStatusesAndFullFrameworkLabel()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedExportFixture(factory);
    var client = await SignInAdminAsync(factory);

    var response = await client.GetAsync("/Dashboard/ExportExcel?ProgramId=1&Page=2&PageSize=1");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    await using var stream = await response.Content.ReadAsStreamAsync();
    using var workbook = new XLWorkbook(stream);
    var sheet = workbook.Worksheet("דיווחים");
    var dataRows = sheet.RowsUsed().Skip(1).ToList();
    dataRows.Should().HaveCount(31, "export must not inherit the one-row UI page size");
    dataRows.Select(row => row.Cell(4).GetString()).Should().Contain(new[] { "6474", "8580", "9144", "9083", "9101" });
    dataRows.Select(row => row.Cell(10).GetString()).Should().Contain("רחלים — 0872903 — הילה ישיבה פרי הארץ");
    dataRows.Select(row => row.Cell(10).GetString()).Should().Contain("רחלים — 000777 — מסגרת נוספת");
    sheet.Cell(1, 10).GetString().Should().Be("מסגרת חינוכית");
    sheet.LastColumnUsed()!.ColumnNumber().Should().Be(24, "the framework fix must not remove other export columns");
  }

  [Fact]
  public async Task InstitutionExport_RespectsFiltersAndUsesHebrewHeadersAndExpectedFilename()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedExportFixture(factory);
    var client = await SignInAdminAsync(factory);

    var response = await client.GetAsync("/Admin/ExportInstitutionsExcel?localityId=1&isActive=true");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Content.Headers.ContentDisposition?.FileName?.Trim('"')
      .Should().Be($"institutions_{DateTime.Now:yyyy-MM-dd}.xlsx");
    await using var stream = await response.Content.ReadAsStreamAsync();
    using var workbook = new XLWorkbook(stream);
    var sheet = workbook.Worksheet("מוסדות");
    sheet.Row(1).Cells(1, 8).Select(cell => cell.GetString()).Should().Equal(
      "שם המוסד", "סמל מוסד", "יישוב", "מחוז", "מגזר", "סוג חינוך", "שלב חינוך", "פעיל");
    sheet.RowsUsed().Skip(1).Should().HaveCount(2);
  }

  private static void SeedExportFixture(CustomWebApplicationFactory factory)
  {
    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Districts.Add(new District { Id = 1, Description = "מרכז", IsActive = true });
    db.Sectors.Add(new Sector { Id = 1, Description = "כללי", IsActive = true });
    db.Localities.Add(new Locality { Id = 1, Description = "רחלים", IsActive = true });
    db.EducationTypes.Add(new EducationType { Id = 1, Description = "ממלכתי", IsActive = true });
    db.EducationalStages.Add(new EducationalStage { Id = 1, Description = "יסודי", IsActive = true });
    db.Institutions.AddRange(
      new Institution { Id = 1, InstitutionSymbol = "872903", Name = "הילה ישיבה פרי הארץ", LocalityId = 1, DistrictId = 1, SectorId = 1, TypeId = 1, EducationalStageId = 1, IsActive = true },
      new Institution { Id = 2, InstitutionSymbol = "777", Name = "מסגרת נוספת", LocalityId = 1, DistrictId = 1, SectorId = 1, TypeId = 1, EducationalStageId = 1, IsActive = true });
    db.Frameworks.AddRange(
      new Framework { Id = 1, InstitutionSymbol = "0872903", Description = "הילה ישיבה פרי הארץ", EducationalStageId = 1, IsActive = true },
      new Framework { Id = 2, InstitutionSymbol = "000777", Description = "מסגרת נוספת", EducationalStageId = 1, IsActive = true });
    db.Programs.Add(new ProgramEntity { Id = 1, Description = "מועדוניות משפחתיות", IsActive = true });
    db.Projects.Add(new Project { Id = 1, Description = "פרויקט בדיקה", IsActive = true });
    db.EducationalPrograms.Add(new EducationalProgram { Id = 1, Description = "חינוך", IsActive = true });
    db.Domains.Add(new Domain { Id = 1, Description = "תחום", IsActive = true });
    db.Subjects.Add(new Subject { Id = 1, Description = "נושא", IsActive = true });
    db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 1, Description = "אוגוסט 2026", Month = 8, Year = 2026,
      IsActive = true, LastReportingDate = DateTime.Today.AddDays(10)
    });
    db.ReportStatuses.AddRange(
      new ReportStatus { Id = 2, Name = "InEntry", Description = "בהזנה" },
      new ReportStatus { Id = 3, Name = "Submitted", Description = "הוגש" },
      new ReportStatus { Id = 4, Name = "Approved", Description = "אושר" });

    var codes = new[] { "6474", "8580", "9144", "9083", "9101" };
    for (var index = 0; index < codes.Length; index++)
    {
      var userId = 10 + index;
      var allocationId = 10 + index;
      var reportId = 10 + index;
      db.Users.Add(new User
      {
        Id = userId, EmployeeCode = codes[index], IdNumber = $"0000000{index}",
        FirstName = "עובד", LastName = codes[index], PasswordHash = "hash", RoleId = 999,
        UserRoleId = (int)UserRoleEnum.Employee, StatusId = index == 4 ? 2 : 1,
        IsReportingEmployee = true, CreatedAt = DateTime.UtcNow
      });
      db.Allocations.Add(new Allocation
      {
        Id = allocationId, UserId = userId, ProjectId = 1,
        IsActive = index != 4, MonthlyRowAllocation = 100, CreatedAt = DateTime.UtcNow
      });
      db.Set<AllocationProgram>().Add(new AllocationProgram { AllocationId = allocationId, ProgramId = 1 });
      db.Set<AllocationDistrict>().Add(new AllocationDistrict { AllocationId = allocationId, DistrictId = 1 });
      db.Reports.Add(new Report
      {
        Id = reportId, UserId = userId, ReportingMonthId = 1,
        StatusId = new[] { 2, 3, 4, 2, 3 }[index], CreatedAt = DateTime.UtcNow
      });
      db.ReportRows.Add(Row(100 + index, reportId, allocationId, 1, 1));
    }

    // More rows than the requested UI PageSize plus a second framework in one report.
    for (var i = 0; i < 25; i++)
      db.ReportRows.Add(Row(200 + i, 10, 10, i + 2, 1));
    db.ReportRows.Add(Row(300, 10, 10, 27, 2));
    db.SaveChanges();
  }

  private static ReportRow Row(int id, int reportId, int allocationId, int sequence, int frameworkId) => new()
  {
    Id = id, ReportId = reportId, AllocationId = allocationId, SequenceNumber = sequence,
    MeetingDate = DateTime.Today, MeetingDuration = 1, DistrictId = 1, LocalityId = 1,
    FrameworkId = frameworkId, EducationalProgramId = 1, DomainId = 1, Subject1Id = 1,
    Notes = "בדיקת ייצוא בעברית", CreatedAt = DateTime.UtcNow
  };

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
}

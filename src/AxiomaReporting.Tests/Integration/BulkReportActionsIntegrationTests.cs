using System.Net;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProgramEntity = AxiomaReporting.Core.Entities.Program;

namespace AxiomaReporting.Tests.Integration;

public class BulkReportActionsIntegrationTests
{
  [Fact]
  public async Task ProjectManager_MixedAuthorizedAndUnauthorizedIds_ArchivesNothing()
  {
    await using var factory = new CustomWebApplicationFactory(settings: new Dictionary<string, string?>
    {
      ["BulkReportActions:AllowProjectManagersToDelete"] = "true"
    });
    SeedBulkFixture(factory);
    var client = await SignInAsync(factory, "888888888", "Manager1234");
    var page = await client.GetStringAsync("/Dashboard/Index");
    page.Should().Contain("id=\"bulkDeleteReportsBtn\"");
    page.Should().Contain("dashboard-single-delete");

    var response = await client.PostAsync("/Dashboard/BulkDelete", new FormUrlEncodedContent(new[]
    {
      Pair("__RequestVerificationToken", HtmlForm.AntiForgeryToken(page)),
      Pair("reportIds", "10"),
      Pair("reportIds", "11"),
      Pair("reason", "integration authorization test")
    }));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.RequestMessage!.RequestUri!.AbsolutePath.Should().Be("/Dashboard");
    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Reports.Where(r => r.Id == 10 || r.Id == 11).Should().OnlyContain(r => !r.IsArchived);
    db.AuditLogs.Should().NotContain(a => a.EntityId == "10" || a.EntityId == "11");
  }

  [Fact]
  public async Task Admin_BulkStatusEndpoints_ApplyLegalSequenceAndWriteAudit()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedBulkFixture(factory);
    var client = await SignInAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);

    var summary = await client.GetStringAsync("/Dashboard/Summary");
    var submit = await client.PostAsync("/Dashboard/BulkSubmit", new FormUrlEncodedContent(new[]
    {
      Pair("__RequestVerificationToken", HtmlForm.AntiForgeryToken(summary)), Pair("reportIds", "10")
    }));
    submit.StatusCode.Should().Be(HttpStatusCode.OK);

    summary = await client.GetStringAsync("/Dashboard/Summary");
    var approve = await client.PostAsync("/Dashboard/BulkApprove", new FormUrlEncodedContent(new[]
    {
      Pair("__RequestVerificationToken", HtmlForm.AntiForgeryToken(summary)), Pair("reportIds", "10")
    }));
    approve.StatusCode.Should().Be(HttpStatusCode.OK);

    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Reports.Find(10)!.StatusId.Should().Be(4);
    db.AuditLogs.Count(a => a.EntityId == "10" && a.Action == "Report.BulkStatusChange").Should().Be(2);
  }

  [Fact]
  public async Task Admin_Delete_HidesReportEverywhereAndAllowsFreshReportForSameMonth()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedBulkFixture(factory);
    var client = await SignInAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);
    var page = await client.GetStringAsync("/Dashboard/Index");

    var response = await client.PostAsync("/Dashboard/BulkDelete", new FormUrlEncodedContent(new[]
    {
      Pair("__RequestVerificationToken", HtmlForm.AntiForgeryToken(page)),
      Pair("reportIds", "10"),
      Pair("reason", "integration consistency test")
    }));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Reports.Find(10)!.IsArchived.Should().BeTrue();
    db.ReportRows.Should().ContainSingle(r => r.ReportId == 10,
      because: "soft-deleted dependants remain linked and are not orphaned");

    var filters = scope.ServiceProvider.GetRequiredService<IDashboardFilterService>();
    (await filters.GetAllReportsAsync(new DashboardFilter { FromMonthId = 1, ToMonthId = 1 },
      2, UserRoleEnum.SystemAdmin)).Should().NotContain(r => r.ReportId == 10);
    (await filters.GetAllReportRowsAsync(new DashboardFilter { FromMonthId = 1, ToMonthId = 1 },
      2, UserRoleEnum.SystemAdmin)).Should().NotContain(r => r.ReportId == 10);

    var statusService = scope.ServiceProvider.GetRequiredService<IReportStatusService>();
    var fresh = await statusService.GetOrCreateDraftAsync(10, 1);
    fresh.Should().NotBeNull();
    fresh!.Id.Should().NotBe(10);
    fresh.IsArchived.Should().BeFalse();
  }

  [Fact]
  public async Task Admin_ReturnAllFilteredApprovedReports_RequeriesFilterAndAuditsEveryReport()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedBulkFixture(factory);
    using (var seedScope = factory.Services.CreateScope())
    {
      var seedDb = seedScope.ServiceProvider.GetRequiredService<AppDbContext>();
      seedDb.Reports.Find(10)!.StatusId = 4;
      seedDb.Reports.Find(11)!.StatusId = 4;
      seedDb.SaveChanges();
    }
    var client = await SignInAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);
    var summary = await client.GetStringAsync("/Dashboard/Summary?StatusId=4");

    var response = await client.PostAsync("/Dashboard/BulkReturnApproved", new FormUrlEncodedContent(new[]
    {
      Pair("__RequestVerificationToken", HtmlForm.AntiForgeryToken(summary)),
      Pair("selectAllFiltered", "true"),
      Pair("filter.StatusId", "4"),
      Pair("reason", "integration approved return")
    }));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Reports.Where(r => r.Id == 10 || r.Id == 11).Should().OnlyContain(r => r.StatusId == 3);
    db.AuditLogs.Count(a => a.Action == "Report.BulkApprovedReturn").Should().Be(2);
  }

  [Fact]
  public async Task ProjectManager_CannotReopenReportOutsideAssignmentScope()
  {
    await using var factory = new CustomWebApplicationFactory();
    SeedBulkFixture(factory);
    using (var seedScope = factory.Services.CreateScope())
    {
      var seedDb = seedScope.ServiceProvider.GetRequiredService<AppDbContext>();
      seedDb.Reports.Find(11)!.StatusId = 4;
      seedDb.SaveChanges();
    }

    var client = await SignInAsync(factory, "888888888", "Manager1234");
    var page = await client.GetStringAsync("/Dashboard/Index");
    var response = await client.PostAsync("/Report/Reopen?reportId=11", new FormUrlEncodedContent(new[]
    {
      Pair("__RequestVerificationToken", HtmlForm.AntiForgeryToken(page))
    }));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.RequestMessage!.RequestUri!.AbsolutePath.Should().Contain("AccessDenied");
    using var scope = factory.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Reports.Find(11)!.StatusId.Should().Be(4);
  }

  private static KeyValuePair<string, string> Pair(string key, string value) => new(key, value);

  private static async Task<HttpClient> SignInAsync(CustomWebApplicationFactory factory, string idNumber, string password)
  {
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });
    var login = await client.GetStringAsync("/Account/Login");
    var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(login), ["IdNumber"] = idNumber, ["Password"] = password
    }));
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    return client;
  }

  private static void SeedBulkFixture(CustomWebApplicationFactory factory)
  {
    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var passwords = new PasswordService();
    db.Users.Add(new User
    {
      Id = 20, EmployeeCode = "PM20", IdNumber = "888888888", FirstName = "מנהלת", LastName = "פרויקט",
      PasswordHash = passwords.HashPassword("Manager1234"), RoleId = 999, UserRoleId = (int)UserRoleEnum.ProjectManager,
      StatusId = 1, AcceptedTermsOfUse = true, MustChangePassword = false,
      LastPasswordChange = DateTime.UtcNow, CreatedAt = DateTime.UtcNow
    });
    db.Users.AddRange(Employee(10, "A10"), Employee(11, "A11"));
    db.TermsOfUseAcceptances.Add(new TermsOfUseAcceptance { UserId = 20, VersionId = 1, AcceptedAt = DateTime.UtcNow });
    db.Districts.Add(new District { Id = 1, Description = "מחוז", IsActive = true });
    db.Localities.Add(new Locality { Id = 1, Description = "יישוב", IsActive = true });
    db.Frameworks.Add(new Framework { Id = 1, Description = "מסגרת", InstitutionSymbol = "100", IsActive = true });
    db.EducationalPrograms.Add(new EducationalProgram { Id = 1, Description = "חינוכית", IsActive = true });
    db.Domains.Add(new Domain { Id = 1, Description = "תחום", IsActive = true });
    db.Subjects.Add(new Subject { Id = 1, Description = "נושא", IsActive = true });
    db.Programs.AddRange(
      new ProgramEntity { Id = 1, Description = "מורשית", IsActive = true },
      new ProgramEntity { Id = 2, Description = "אסורה", IsActive = true });
    db.Projects.Add(new Project { Id = 1, Description = "פרויקט", IsActive = true });
    db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 1, Description = "אוגוסט 2026", Month = 8, Year = 2026,
      IsActive = true, LastReportingDate = DateTime.Today.AddDays(5)
    });
    db.ReportStatuses.AddRange(
      new ReportStatus { Id = 2, Name = "InEntry", Description = "בהזנה" },
      new ReportStatus { Id = 3, Name = "Submitted", Description = "הוגש" },
      new ReportStatus { Id = 4, Name = "Approved", Description = "אושר" });
    db.Allocations.AddRange(
      new Allocation { Id = 10, UserId = 10, ProjectId = 1, IsActive = true, CreatedAt = DateTime.UtcNow },
      new Allocation { Id = 11, UserId = 11, ProjectId = 1, IsActive = true, CreatedAt = DateTime.UtcNow });
    db.Set<AllocationProgram>().AddRange(
      new AllocationProgram { AllocationId = 10, ProgramId = 1 },
      new AllocationProgram { AllocationId = 11, ProgramId = 2 });
    db.Set<AllocationDistrict>().AddRange(
      new AllocationDistrict { AllocationId = 10, DistrictId = 1 },
      new AllocationDistrict { AllocationId = 11, DistrictId = 1 });
    db.InspectorAssignments.Add(new InspectorAssignment { Id = 1, InspectorUserId = 20, ProgramId = 1 });
    db.Reports.AddRange(
      new Report { Id = 10, UserId = 10, ReportingMonthId = 1, StatusId = 2, CreatedAt = DateTime.UtcNow },
      new Report { Id = 11, UserId = 11, ReportingMonthId = 1, StatusId = 2, CreatedAt = DateTime.UtcNow });
    db.ReportRows.AddRange(Row(10, 10, 10), Row(11, 11, 11));
    db.SaveChanges();
  }

  private static User Employee(int id, string code) => new()
  {
    Id = id, EmployeeCode = code, IdNumber = $"1000000{id}", FirstName = "עובד", LastName = code,
    PasswordHash = "hash", RoleId = 999, UserRoleId = 6, StatusId = 1, IsReportingEmployee = true, CreatedAt = DateTime.UtcNow
  };

  private static ReportRow Row(int id, int reportId, int allocationId) => new()
  {
    Id = id, ReportId = reportId, AllocationId = allocationId, SequenceNumber = 1,
    MeetingDate = DateTime.Today, MeetingDuration = 1, DistrictId = 1, LocalityId = 1,
    FrameworkId = 1, EducationalProgramId = 1, DomainId = 1, Subject1Id = 1, CreatedAt = DateTime.UtcNow
  };
}

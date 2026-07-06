using System.Net;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

public class AuditLogFlowTests
{
  [Fact]
  public async Task EmployeeController_Create_WritesAuditLog_WithActionEmployeeCreate()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = await SignInAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);

    // Seed lookups required by the Employee form
    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.Roles.Add(new EmployeeRole { Id = 1, Description = "Trainer" });
      await db.SaveChangesAsync();
    }

    var formHtml = await client.GetStringAsync("/Employee/Create");
    var form = new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(formHtml),
      ["EmployeeCode"] = "777",
      ["IdNumber"] = "123456782",
      ["FirstName"] = "Audited",
      ["LastName"] = "Employee",
      ["Email"] = "audited@example.test",
      ["RoleId"] = "1",
      ["UserRoleId"] = ((int)UserRoleEnum.Employee).ToString(),
      ["StatusId"] = ((int)UserStatusEnum.Active).ToString(),
      ["IsReportingEmployee"] = "true",
      ["AllowFutureReporting"] = "false"
    };
    var response = await client.PostAsync("/Employee/Create", new FormUrlEncodedContent(form));
    response.IsSuccessStatusCode.Should().BeTrue();

    using var verifyScope = factory.Services.CreateScope();
    var verifyDb = verifyScope.ServiceProvider.GetRequiredService<AppDbContext>();
    var audit = await verifyDb.AuditLogs
      .Where(a => a.Action == "Employee.Create")
      .OrderByDescending(a => a.Timestamp)
      .FirstOrDefaultAsync();
    audit.Should().NotBeNull();
    audit!.EntityType.Should().Be(nameof(User));
    audit.ActorUserId.Should().Be(2); // the admin user from TestData
    audit.After.Should().NotBeNull();
    audit.After!.Should().Contain("777");
  }

  [Fact]
  public async Task ReportStatusService_Approve_WritesAuditLog_WithStatusChange()
  {
    await using var factory = new CustomWebApplicationFactory();

    int reportId;
    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.ReportingMonths.Add(new ReportingMonth
      {
        Id = 10,
        Month = 3,
        Year = 2026,
        Description = "March 2026",
        LastReportingDate = DateTime.UtcNow.AddDays(5),
        IsActive = true,
        CreatedAt = DateTime.UtcNow
      });
      var report = new Report
      {
        UserId = 1,
        ReportingMonthId = 10,
        StatusId = 3, // Pending Approval
        CreatedAt = DateTime.UtcNow
      };
      db.Reports.Add(report);
      await db.SaveChangesAsync();
      reportId = report.Id;

      var status = scope.ServiceProvider.GetRequiredService<IReportStatusService>();
      await status.ApproveReportAsync(reportId, approvedByUserId: 2);
    }

    using var verifyScope = factory.Services.CreateScope();
    var verifyDb = verifyScope.ServiceProvider.GetRequiredService<AppDbContext>();
    var audit = await verifyDb.AuditLogs
      .Where(a => a.Action == "Report.StatusChange" && a.EntityId == reportId.ToString())
      .FirstOrDefaultAsync();
    audit.Should().NotBeNull();
    audit!.Before.Should().Contain("\"statusId\":3");
    audit.After.Should().Contain("\"statusId\":4");
  }

  [Fact]
  public async Task LookupController_Delete_WritesAuditLog_WithEntityTypeAndId()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = await SignInAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);

    int districtId;
    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      var d = new District { Description = "Audit District", IsActive = true, CreatedAt = DateTime.UtcNow };
      db.Districts.Add(d);
      await db.SaveChangesAsync();
      districtId = d.Id;
    }

    // Need antiforgery token from a GET on a lookup page
    var listHtml = await client.GetStringAsync("/Lookup/districts");
    var token = HtmlForm.AntiForgeryToken(listHtml);

    var response = await client.PostAsync($"/Lookup/districts/Delete/{districtId}",
      new FormUrlEncodedContent(new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = token
      }));
    response.IsSuccessStatusCode.Should().BeTrue();

    using var verifyScope = factory.Services.CreateScope();
    var verifyDb = verifyScope.ServiceProvider.GetRequiredService<AppDbContext>();
    var audit = await verifyDb.AuditLogs
      .Where(a => a.Action == "Lookup.Delete" && a.EntityType == "districts" && a.EntityId == districtId.ToString())
      .FirstOrDefaultAsync();
    audit.Should().NotBeNull();
    audit!.ActorUserId.Should().Be(2);
  }

  [Fact]
  public async Task AdminAuditLogExport_ReturnsCsvWithFilteredRows()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = await SignInAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);

    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.AuditLogs.AddRange(
        new AuditLog { Action = "Employee.Create", EntityType = "User", EntityId = "10", Timestamp = DateTime.UtcNow, ActorUserId = 2, Notes = "created" },
        new AuditLog { Action = "Employee.Update", EntityType = "User", EntityId = "11", Timestamp = DateTime.UtcNow, ActorUserId = 2 },
        new AuditLog { Action = "Lookup.Create", EntityType = "districts", EntityId = "5", Timestamp = DateTime.UtcNow, ActorUserId = 2 });
      await db.SaveChangesAsync();
    }

    var response = await client.GetAsync("/Admin/AuditLog/Export?action=Employee");
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Content.Headers.ContentType!.MediaType.Should().Be("text/csv");
    var body = await response.Content.ReadAsStringAsync();
    body.Should().Contain("Employee.Create");
    body.Should().Contain("Employee.Update");
    body.Should().NotContain("Lookup.Create");
  }

  [Fact]
  public async Task AdminAuditLog_Index_RequiresAdmin_AndRenders()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = await SignInAsync(factory, TestData.AdminIdNumber, TestData.AdminPassword);

    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.AuditLogs.Add(new AuditLog
      {
        Action = "Auth.LoginSucceeded",
        EntityType = "User",
        EntityId = "42",
        Timestamp = DateTime.UtcNow,
        ActorUserId = 2
      });
      await db.SaveChangesAsync();
    }

    var page = await client.GetAsync("/Admin/AuditLog");
    page.StatusCode.Should().Be(HttpStatusCode.OK);
    // Razor HTML-encodes Hebrew emitted from C# expressions into numeric entities,
    // so decode before asserting. The view shows Hebrew labels for actions
    // (client requirement: all-Hebrew UI); Auth.LoginSucceeded => "כניסה הצליחה".
    var body = System.Net.WebUtility.HtmlDecode(await page.Content.ReadAsStringAsync());
    body.Should().Contain("יומן ביקורת");
    body.Should().Contain("כניסה הצליחה");
  }

  private static async Task<HttpClient> SignInAsync(CustomWebApplicationFactory factory, string idNumber, string password)
  {
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });
    var loginHtml = await client.GetStringAsync("/Account/Login");
    var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = idNumber,
      ["Password"] = password
    }));
    response.IsSuccessStatusCode.Should().BeTrue();
    return client;
  }
}

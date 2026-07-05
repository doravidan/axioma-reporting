using System.Net;
using System.Text.Json;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

/// <summary>
/// End-to-end HTTP integration tests for the full report submission workflow:
/// draft creation, row save, submit, coordinator approve/reject, and edit-lock after approval.
/// </summary>
public class ReportFlowTests : IDisposable
{
  private const string CoordinatorIdNumber = "700000001";
  private const string CoordinatorPassword = "Password123";

  private readonly CustomWebApplicationFactory _factory;

  // IDs assigned during SeedReportingFixture — read by tests.
  private int _reportingMonthId;
  private int _projectId;
  private int _allocationId;
  private int _districtId;
  private int _localityId;
  private int _frameworkId;
  private int _educationalProgramId;
  private int _domainId;
  private int _subject1Id;
  private int _coordinatorUserId;

  public ReportFlowTests()
  {
    _factory = new CustomWebApplicationFactory();
    SeedReportingFixture();
  }

  public void Dispose() => _factory.DisposeAsync().AsTask().GetAwaiter().GetResult();

  private static DateTime ValidReportDate() =>
    DateTime.Today.Day > 1 ? DateTime.Today.AddDays(-1) : DateTime.Today;

  // ─── Draft / Row save ───────────────────────────────────────────────────────

  [Fact]
  public async Task Employee_CanCreateDraftReport_AndSaveDraftRow()
  {
    var client = await SignInAs(TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var indexResponse = await client.GetAsync($"/Report/Index?userId=1&allocationId={_allocationId}");
    indexResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    // Verify the draft was created in the DB despite the 500 response.
    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var report = await db.Reports.FirstOrDefaultAsync(r => r.UserId == 1 && r.ReportingMonthId == _reportingMonthId);
    report.Should().NotBeNull("opening the monthly activity page creates the draft");
    report!.StatusId.Should().BeOneOf(new[] { 1, 2 }, "new report should be Draft or InEntry");

    // Get a valid antiforgery token from a page the employee CAN render (ChangePassword is
    // accessible to all authenticated users and has a standard form with a CSRF token).
    // ASP.NET Core antiforgery tokens are session-scoped, so a token from any page works.
    var changePwdHtml = await client.GetStringAsync("/Account/ChangePassword");
    var token = HtmlForm.AntiForgeryToken(changePwdHtml);

    // POST /Report/SaveRow — add a row to the draft.
    var saveResponse = await client.PostAsync("/Report/SaveRow", new FormUrlEncodedContent(
      BuildRowFormFields(report.Id, _allocationId, token, ValidReportDate())));
    saveResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var json = await ParseJsonAsync(saveResponse);
    json.GetProperty("success").GetBoolean().Should().BeTrue("SaveRow should succeed for a valid row");

    using var verify = _factory.Services.CreateScope();
    var verifyDb = verify.ServiceProvider.GetRequiredService<AppDbContext>();
    verifyDb.ReportRows.Any(r => r.ReportId == report.Id).Should().BeTrue();
  }

  [Fact]
  public async Task Employee_ReportIndex_WithRowAttachments_ReturnsOk()
  {
    int reportId;
    int rowId;
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var report = new Report
      {
        UserId = 1,
        ReportingMonthId = _reportingMonthId,
        StatusId = 2,
        CreatedAt = DateTime.UtcNow
      };
      db.Reports.Add(report);
      await db.SaveChangesAsync();
      reportId = report.Id;

      var row = BuildReportRow(reportId, _allocationId);
      db.ReportRows.Add(row);
      await db.SaveChangesAsync();
      rowId = row.Id;

      db.DocumentAttachments.Add(new DocumentAttachment
      {
        ReportRowId = rowId,
        FileName = "activity.pdf",
        FilePath = "uploads/activity.pdf",
        FileSize = 128,
        MimeType = "application/pdf",
        UploadedAt = DateTime.UtcNow,
        UploadedBy = 1
      });
      await db.SaveChangesAsync();
    }

    var client = await SignInAs(TestData.EmployeeIdNumber, TestData.EmployeePassword);

    var response = await client.GetAsync($"/Report/Index?userId=1&allocationId={_allocationId}");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var body = await response.Content.ReadAsStringAsync();
    body.Should().Contain("activity.pdf");
  }

  // ─── Submit ─────────────────────────────────────────────────────────────────

  [Fact]
  public async Task Employee_CanSubmitReport_ChangesStatusToPendingAndSendsEmail()
  {
    var client = await SignInAs(TestData.EmployeeIdNumber, TestData.EmployeePassword);

    // Create the draft via GET first.
    await client.GetAsync($"/Report/Index?userId=1&allocationId={_allocationId}");

    int reportId;
    using (var scope = _factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      var report = await db.Reports.FirstOrDefaultAsync(r => r.UserId == 1 && r.ReportingMonthId == _reportingMonthId);
      report.Should().NotBeNull();
      reportId = report!.Id;

      // Insert one row directly so the submit validation passes.
      db.ReportRows.Add(BuildReportRow(reportId, _allocationId));
      report.StatusId = 2; // InEntry
      await db.SaveChangesAsync();
    }

    // Use ChangePassword page to get a valid CSRF token — Report/Index returns 500 in-memory
    // due to an EF GroupBy translation limitation, but any session-scoped page works.
    var changePwdHtml = await client.GetStringAsync("/Account/ChangePassword");
    var token = HtmlForm.AntiForgeryToken(changePwdHtml);

    var submitResponse = await client.PostAsync("/Report/Submit", new FormUrlEncodedContent(
      new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = token,
        ["reportId"] = reportId.ToString(),
        ["allocationId"] = _allocationId.ToString()
      }));

    // Submit redirects back to Index on success; that redirect also returns 500 in-memory,
    // so accept both OK (direct) and any 5xx as it proves the Submit action ran.
    ((int)submitResponse.StatusCode).Should().BeOneOf(
      new[] { 200, 302, 303 },
      because: "Submit redirects back to a renderable monthly activity page");

    using var verify = _factory.Services.CreateScope();
    var verifyDb = verify.ServiceProvider.GetRequiredService<AppDbContext>();
    var updated = await verifyDb.Reports.FindAsync(reportId);
    updated!.StatusId.Should().Be(3, "submitted report must be PendingApproval (status 3)");
    updated.SubmittedAt.Should().NotBeNull();

    // Confirm the ReportReceived notification email was dispatched.
    _factory.EmailService.Sent
      .Should().Contain(e => e.TemplateType == "ReportReceived",
        because: "submitting a report must enqueue a ReportReceived email");
  }

  // ─── Coordinator approve ────────────────────────────────────────────────────

  [Fact]
  public async Task Coordinator_CanApproveReport_ChangesStatusToApprovedAndSendsEmail()
  {
    int reportId = await SeedPendingReportAsync(userId: 1);

    var client = await SignInAs(CoordinatorIdNumber, CoordinatorPassword);

    var dashHtml = await client.GetStringAsync("/Dashboard/Summary");
    string token;
    try { token = HtmlForm.AntiForgeryToken(dashHtml); }
    catch (InvalidOperationException) { token = "no_form"; }

    var response = await client.PostAsync("/Report/Approve", new FormUrlEncodedContent(
      new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = token,
        ["reportId"] = reportId.ToString()
      }));

    response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect);

    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var report = await db.Reports.FindAsync(reportId);
    report!.StatusId.Should().Be(4, "approved report must have status 4");
    report.ApprovedBy.Should().Be(_coordinatorUserId);

    _factory.EmailService.Sent
      .Should().Contain(e => e.TemplateType == "ReportApproved",
        because: "approving must enqueue a ReportApproved email");
  }

  // ─── Coordinator reject ─────────────────────────────────────────────────────

  [Fact]
  public async Task Coordinator_CanRejectReport_SendsRejectionEmailWithReason()
  {
    int reportId = await SeedPendingReportAsync(userId: 1);

    var client = await SignInAs(CoordinatorIdNumber, CoordinatorPassword);

    var dashHtml = await client.GetStringAsync("/Dashboard/Summary");
    string token;
    try { token = HtmlForm.AntiForgeryToken(dashHtml); }
    catch (InvalidOperationException) { token = "no_form"; }

    const string reason = "נדרש תיקון בתאריכים";
    var response = await client.PostAsync("/Report/Reject", new FormUrlEncodedContent(
      new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = token,
        ["reportId"] = reportId.ToString(),
        ["rejectionReason"] = reason
      }));

    response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect);

    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var report = await db.Reports.FindAsync(reportId);
    report!.StatusId.Should().Be(5, "rejected report must have status 5 (ReturnedForCorrection)");
    report.RejectionReason.Should().Be(reason);

    _factory.EmailService.Sent
      .Should().Contain(e => e.TemplateType == "ReportRejected",
        because: "rejection must enqueue a ReportRejected email");
  }

  // ─── Resubmit after rejection ───────────────────────────────────────────────

  [Fact]
  public async Task Employee_AfterRejection_CanCorrectAndResubmit()
  {
    // Seed report already in ReturnedForCorrection state.
    int reportId;
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var report = new Report
      {
        UserId = 1,
        ReportingMonthId = _reportingMonthId,
        StatusId = 5, // ReturnedForCorrection
        RejectionReason = "Please fix",
        CreatedAt = DateTime.UtcNow
      };
      db.Reports.Add(report);
      await db.SaveChangesAsync();
      reportId = report.Id;
    }

    var client = await SignInAs(TestData.EmployeeIdNumber, TestData.EmployeePassword);
    // Report/Index returns 500 in the in-memory test environment due to an EF GroupBy
    // translation limitation. Use ChangePassword (accessible to all authenticated users)
    // to obtain a session-scoped antiforgery token that works for any form submission.
    var changePwdHtml = await client.GetStringAsync("/Account/ChangePassword");
    var token = HtmlForm.AntiForgeryToken(changePwdHtml);

    // Save a corrected row.
    var saveResponse = await client.PostAsync("/Report/SaveRow", new FormUrlEncodedContent(
      BuildRowFormFields(reportId, _allocationId, token, ValidReportDate())));
    saveResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    var saveJson = await ParseJsonAsync(saveResponse);
    saveJson.GetProperty("success").GetBoolean().Should().BeTrue();

    // Re-submit using the same token (tokens are single-use in prod but reusable in test).
    var submitResponse = await client.PostAsync("/Report/Submit", new FormUrlEncodedContent(
      new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = token,
        ["reportId"] = reportId.ToString(),
        ["allocationId"] = _allocationId.ToString()
      }));

    // The redirect after Submit goes to Report/Index which also returns 500 in-memory.
    ((int)submitResponse.StatusCode).Should().BeOneOf(
      new[] { 200, 302, 303 },
      because: "Submit redirects back to a renderable monthly activity page");

    using var verify = _factory.Services.CreateScope();
    var verifyDb = verify.ServiceProvider.GetRequiredService<AppDbContext>();
    (await verifyDb.Reports.FindAsync(reportId))!.StatusId
      .Should().Be(3, "resubmitted report must return to PendingApproval");
  }

  // ─── Edit-lock after approval ───────────────────────────────────────────────

  [Fact]
  public async Task Employee_CannotEditApprovedReport_SaveRowReturnsError()
  {
    int reportId;
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var report = new Report
      {
        UserId = 1,
        ReportingMonthId = _reportingMonthId,
        StatusId = 4, // Approved
        CreatedAt = DateTime.UtcNow
      };
      db.Reports.Add(report);
      await db.SaveChangesAsync();
      reportId = report.Id;
    }

    var client = await SignInAs(TestData.EmployeeIdNumber, TestData.EmployeePassword);

    // Report/Index returns 500 in the in-memory test environment due to an EF GroupBy
    // translation limitation. Use ChangePassword (always accessible to authenticated users)
    // to get a valid session-scoped antiforgery token that works for any form submission.
    var changePwdHtml = await client.GetStringAsync("/Account/ChangePassword");
    var token = HtmlForm.AntiForgeryToken(changePwdHtml);

    var saveResponse = await client.PostAsync("/Report/SaveRow", new FormUrlEncodedContent(
      BuildRowFormFields(reportId, _allocationId, token, ValidReportDate())));
    saveResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var json = await ParseJsonAsync(saveResponse);
    json.GetProperty("success").GetBoolean().Should().BeFalse(
      because: "employees cannot add rows to an already-approved report");
  }

  [Fact]
  public async Task Coordinator_CanEditApprovedReport_SaveRowSucceeds()
  {
    int reportId;
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var report = new Report
      {
        UserId = 1,
        ReportingMonthId = _reportingMonthId,
        StatusId = 4, // Approved
        CreatedAt = DateTime.UtcNow
      };
      db.Reports.Add(report);
      await db.SaveChangesAsync();
      reportId = report.Id;
    }

    var client = await SignInAs(CoordinatorIdNumber, CoordinatorPassword);

    // Report/Index returns 500 in the in-memory test environment due to an EF GroupBy
    // translation limitation. Use ChangePassword (always accessible to authenticated users)
    // to get a valid session-scoped antiforgery token that works for any form submission.
    var changePwdHtml = await client.GetStringAsync("/Account/ChangePassword");
    var token = HtmlForm.AntiForgeryToken(changePwdHtml);

    var saveResponse = await client.PostAsync("/Report/SaveRow", new FormUrlEncodedContent(
      BuildRowFormFields(reportId, _allocationId, token, ValidReportDate())));
    saveResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var json = await ParseJsonAsync(saveResponse);
    // Since v1.2.11 coordinators (like admins and PMs) may correct already-approved
    // reports — the deadline-override roles bypass the approved-status lock.
    json.GetProperty("success").GetBoolean().Should().BeTrue(
      because: "coordinators may correct rows on an already-approved report since v1.2.11");
  }

  // ─── Private helpers ────────────────────────────────────────────────────────

  private async Task<HttpClient> SignInAs(string idNumber, string password)
    => await AccessControlTests.SignInAsAsync(_factory, idNumber, password);

  private async Task<int> SeedPendingReportAsync(int userId)
  {
    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Remove any draft/in-entry report first to avoid duplicate-unique constraint issues.
    var existing = db.Reports.Where(r => r.UserId == userId && r.ReportingMonthId == _reportingMonthId);
    db.Reports.RemoveRange(existing);
    await db.SaveChangesAsync();

    var report = new Report
    {
      UserId = userId,
      ReportingMonthId = _reportingMonthId,
      StatusId = 3, // PendingApproval
      SubmittedAt = DateTime.UtcNow,
      CreatedAt = DateTime.UtcNow
    };
    db.Reports.Add(report);
    await db.SaveChangesAsync();
    return report.Id;
  }

  private void SeedReportingFixture()
  {
    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var pw = new PasswordService();

    // Lookup tables.
    var district = new District { Description = "Report District", IsActive = true, CreatedAt = DateTime.UtcNow };
    var locality = new Locality { Description = "Report Locality", IsActive = true, CreatedAt = DateTime.UtcNow };
    var framework = new Framework { Description = "Report Framework", InstitutionSymbol = "RF1", IsActive = true, CreatedAt = DateTime.UtcNow };
    var eduProg = new EducationalProgram { Description = "Report EduProg", IsActive = true, CreatedAt = DateTime.UtcNow };
    var domain = new Domain { Description = "Report Domain", IsActive = true, CreatedAt = DateTime.UtcNow };
    var subject = new Subject { Description = "Report Subject", IsActive = true, CreatedAt = DateTime.UtcNow };
    db.Districts.Add(district);
    db.Localities.Add(locality);
    db.Frameworks.Add(framework);
    db.EducationalPrograms.Add(eduProg);
    db.Domains.Add(domain);
    db.Subjects.Add(subject);
    db.ReportStatuses.AddRange(
      new ReportStatus { Id = 1, Name = "Draft" },
      new ReportStatus { Id = 2, Name = "InEntry" },
      new ReportStatus { Id = 3, Name = "PendingApproval" },
      new ReportStatus { Id = 4, Name = "Approved" },
      new ReportStatus { Id = 5, Name = "ReturnedForCorrection" });
    db.SaveChanges();

    _districtId = district.Id;
    _localityId = locality.Id;
    _frameworkId = framework.Id;
    _educationalProgramId = eduProg.Id;
    _domainId = domain.Id;
    _subject1Id = subject.Id;

    // Reporting month (active, deadline in the future so edit is allowed).
    var reportDate = ValidReportDate();
    var month = new ReportingMonth
    {
      Month = reportDate.Month,
      Year = reportDate.Year,
      Description = $"{reportDate:MM/yyyy}",
      LastReportingDate = DateTime.UtcNow.AddDays(30),
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    };
    db.ReportingMonths.Add(month);
    db.SaveChanges();
    _reportingMonthId = month.Id;

    // Project and allocation for the default employee (UserId=1).
    var project = new Project { Description = "Flow Test Project", IsActive = true, CreatedAt = DateTime.UtcNow };
    db.Projects.Add(project);
    db.SaveChanges();
    _projectId = project.Id;

    var allocation = new Allocation
    {
      UserId = 1, // TestData.Employee
      ProjectId = _projectId,
      IsActive = true,
      MonthlyRowAllocation = 10,
      CreatedAt = DateTime.UtcNow
    };
    db.Allocations.Add(allocation);
    db.SaveChanges();
    _allocationId = allocation.Id;

    // Coordinator user.
    db.Roles.Add(new EmployeeRole { Id = 20, Description = "Role Flow", IsActive = true, CreatedAt = DateTime.UtcNow });
    var coordinator = new User
    {
      EmployeeCode = "COORD_FLOW",
      IdNumber = CoordinatorIdNumber,
      FirstName = "Flow",
      LastName = "Coordinator",
      Email = "coord_flow@example.test",
      PasswordHash = pw.HashPassword(CoordinatorPassword),
      RoleId = 20,
      UserRoleId = (int)UserRoleEnum.ProjectCoordinator,
      StatusId = (int)UserStatusEnum.Active,
      AcceptedTermsOfUse = true,
      MustChangePassword = false,
      LastPasswordChange = DateTime.UtcNow,
      CreatedAt = DateTime.UtcNow
    };
    db.Users.Add(coordinator);
    db.SaveChanges();
    _coordinatorUserId = coordinator.Id;

    db.TermsOfUseAcceptances.Add(new TermsOfUseAcceptance
    {
      UserId = _coordinatorUserId,
      VersionId = 1,
      AcceptedAt = DateTime.UtcNow
    });
    db.SaveChanges();
  }

  private Dictionary<string, string> BuildRowFormFields(int reportId, int allocationId, string token, DateTime date)
    => new()
    {
      ["__RequestVerificationToken"] = token,
      ["reportId"] = reportId.ToString(),
      ["allocationId"] = allocationId.ToString(),
      ["Id"] = "0",
      ["MeetingDate"] = date.ToString("yyyy-MM-dd"),
      ["MeetingDuration"] = "1",
      ["DistrictId"] = _districtId.ToString(),
      ["LocalityId"] = _localityId.ToString(),
      ["FrameworkId"] = _frameworkId.ToString(),
      ["EducationalProgramId"] = _educationalProgramId.ToString(),
      ["DomainId"] = _domainId.ToString(),
      ["Subject1Id"] = _subject1Id.ToString(),
      ["Notes"] = ""
    };

  private ReportRow BuildReportRow(int reportId, int allocationId) => new()
  {
    ReportId = reportId,
    AllocationId = allocationId,
    SequenceNumber = 1,
    MeetingDate = DateTime.Today,
    MeetingDuration = 1m,
    DistrictId = _districtId,
    LocalityId = _localityId,
    FrameworkId = _frameworkId,
    EducationalProgramId = _educationalProgramId,
    DomainId = _domainId,
    Subject1Id = _subject1Id,
    CreatedAt = DateTime.UtcNow
  };

  private static async Task<JsonElement> ParseJsonAsync(HttpResponseMessage response)
  {
    var body = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<JsonElement>(body,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
  }
}

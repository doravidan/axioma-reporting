using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using AxiomaReporting.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Covers Gap 2 (deadline-passed edit block), Gap 3 (optimistic concurrency),
/// and Gap 4 (Excel-failure email) behaviors on <see cref="ReportController"/>.
/// </summary>
public class ReportControllerGapsTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly FakeEmailService _email = new();

  public ReportControllerGapsTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    Seed();
  }

  public void Dispose() => _db.Dispose();

  // ---------- Gap 2: deadline block ----------

  [Fact]
  public async Task SaveRow_WhenDeadlinePassedAndUserIsEmployee_ReturnsDeadlineMessage()
  {
    var month = await _db.ReportingMonths.FindAsync(1);
    month!.LastReportingDate = DateTime.Today.AddDays(-1);
    await _db.SaveChangesAsync();

    var controller = BuildController(UserRoleEnum.Employee, userId: 1);
    var row = NewRow();

    var result = await controller.SaveRow(row, reportId: 1, allocationId: 1);

    var json = (JsonResult)result;
    json.Value.Should().BeEquivalentTo(new { success = false, error = ReportController.DeadlinePassedMessage });
  }

  [Fact]
  public async Task SaveRow_WhenDeadlinePassedAndUserIsPM_AllowsSave()
  {
    var month = await _db.ReportingMonths.FindAsync(1);
    month!.LastReportingDate = DateTime.Today.AddDays(-1);
    await _db.SaveChangesAsync();

    var controller = BuildController(UserRoleEnum.ProjectManager, userId: 99);
    var row = NewRow();

    var result = await controller.SaveRow(row, reportId: 1, allocationId: 1);

    var json = (JsonResult)result;
    // Reflection-free shape check
    var props = json.Value!.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(json.Value));
    props["success"].Should().Be(true);
  }

  [Fact]
  public async Task DeleteRow_WhenDeadlinePassedAndUserIsEmployee_ReturnsDeadlineMessage()
  {
    var reportRow = new ReportRow
    {
      ReportId = 1,
      AllocationId = 1,
      SequenceNumber = 1,
      MeetingDate = DateTime.Today.AddDays(-5),
      MeetingDuration = 1,
      DistrictId = 1, LocalityId = 1, FrameworkId = 1,
      EducationalProgramId = 1, DomainId = 1, Subject1Id = 1,
      CreatedAt = DateTime.UtcNow
    };
    _db.ReportRows.Add(reportRow);
    var month = await _db.ReportingMonths.FindAsync(1);
    month!.LastReportingDate = DateTime.Today.AddDays(-1);
    await _db.SaveChangesAsync();

    var controller = BuildController(UserRoleEnum.Employee, userId: 1);

    var result = await controller.DeleteRow(reportRow.Id);

    var json = (JsonResult)result;
    var props = json.Value!.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(json.Value));
    props["success"].Should().Be(false);
    props["error"].Should().Be(ReportController.DeadlinePassedMessage);
  }

  // ---------- Gap 3: optimistic concurrency ----------

  [Fact]
  public async Task SaveRow_ConcurrentEdits_SecondSaveReturnsConflictMessage()
  {
    // Seed a row that the "first tab" is about to save. The stale RowVersion
    // we send in the POST simulates a second-writer race — EF Core will reject
    // the update because the OriginalValues token does not match the stored value.
    var existing = new ReportRow
    {
      ReportId = 1,
      AllocationId = 1,
      SequenceNumber = 1,
      MeetingDate = DateTime.Today.AddDays(-5),
      MeetingDuration = 1,
      DistrictId = 1, LocalityId = 1, FrameworkId = 1,
      EducationalProgramId = 1, DomainId = 1, Subject1Id = 1,
      RowVersion = new byte[] { 1, 0, 0, 0, 0, 0, 0, 1 },
      CreatedAt = DateTime.UtcNow
    };
    _db.ReportRows.Add(existing);
    await _db.SaveChangesAsync();

    // A stale RowVersion the client thinks it has.
    var staleRowVersion = Convert.ToBase64String(new byte[] { 9, 9, 9, 9, 9, 9, 9, 9 });

    var controller = BuildController(UserRoleEnum.Employee, userId: 1);
    var edited = new ReportRow
    {
      Id = existing.Id,
      ReportId = 1,
      AllocationId = 1,
      MeetingDate = DateTime.Today.AddDays(-5),
      MeetingDuration = 1,
      DistrictId = 1, LocalityId = 1, FrameworkId = 1,
      EducationalProgramId = 1, DomainId = 1, Subject1Id = 1,
      Notes = "first tab edit"
    };

    var result = await controller.SaveRow(edited, reportId: 1, allocationId: 1, rowVersion: staleRowVersion);

    var json = (JsonResult)result;
    var props = json.Value!.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(json.Value));
    props["success"].Should().Be(false);
    props["error"].Should().Be(ReportController.ConcurrencyConflictMessage);
  }

  // ---------- Gap 4: Excel upload failure email ----------

  [Fact]
  public async Task UploadExcel_WhenImportFails_EnqueuesBatchImportErrorsEmailWithErrorList()
  {
    // Use an import service stub that returns failure with two errors.
    var import = new StubFailingImportService(new[]
    {
      "שורה 2: MeetingDuration אינו מספר תקין",
      "שורה 3: DistrictId חסר או לא תקין"
    });

    var controller = BuildController(UserRoleEnum.Employee, userId: 1, importOverride: import);

    // Provide a small fake xlsx stream — not actually parsed since we override the service.
    var file = new Mock<IFormFile>();
    using var ms = new MemoryStream(new byte[] { 0x50, 0x4B, 0x03, 0x04 });
    file.Setup(f => f.OpenReadStream()).Returns(ms);
    file.Setup(f => f.FileName).Returns("rows.xlsx");
    file.Setup(f => f.Length).Returns(ms.Length);
    file.Setup(f => f.ContentType).Returns("application/xlsx");

    await controller.UploadExcel(reportId: 1, allocationId: 1, file: file.Object);

    _email.Sent.Should().ContainSingle(e => e.TemplateType == "BatchImportErrors");
    var sent = _email.Sent.Single();
    sent.ToEmail.Should().Be("employee@example.test");
    sent.Tokens["ErrorsCount"].Should().Be("2");
    sent.Tokens["ErrorList"].Should().Contain("שורה 2:").And.Contain("שורה 3:");
  }

  [Fact]
  public async Task UploadExcel_WhenImportFailsAndUserHasNoEmail_DoesNotThrowOrSend()
  {
    // Clear the user's email
    var user = await _db.Users.FindAsync(1);
    user!.Email = null;
    await _db.SaveChangesAsync();

    var import = new StubFailingImportService(new[] { "שורה 2: שגיאה כלשהי" });
    var controller = BuildController(UserRoleEnum.Employee, userId: 1, importOverride: import);

    var file = new Mock<IFormFile>();
    using var ms = new MemoryStream(new byte[] { 0x50, 0x4B, 0x03, 0x04 });
    file.Setup(f => f.OpenReadStream()).Returns(ms);
    file.Setup(f => f.FileName).Returns("rows.xlsx");
    file.Setup(f => f.Length).Returns(ms.Length);
    file.Setup(f => f.ContentType).Returns("application/xlsx");

    var act = async () => await controller.UploadExcel(reportId: 1, allocationId: 1, file: file.Object);

    await act.Should().NotThrowAsync();
    _email.Sent.Should().BeEmpty();
  }

  // ---------- helpers ----------

  private ReportController BuildController(
    UserRoleEnum role,
    int userId,
    IReportExcelImportService? importOverride = null)
  {
    var currentUser = new Mock<ICurrentUserService>();
    currentUser.SetupGet(c => c.UserId).Returns(userId);
    currentUser.SetupGet(c => c.UserRole).Returns(role);
    currentUser.SetupGet(c => c.IsAuthenticated).Returns(true);

    var validator = new ReportValidationService(_db);
    var status = new ReportStatusService(_db, _email);
    var import = importOverride ?? new ReportExcelImportService(_db, validator, status, _email);
    var pdf = new Mock<IPdfReportService>();
    pdf.Setup(p => p.CreateErrorReport(It.IsAny<IEnumerable<string>>())).Returns(new byte[] { 1, 2, 3 });

    var ctrl = new ReportController(
      _db, validator, status, import, pdf.Object, currentUser.Object, _email, NullLogger<ReportController>.Instance);

    ctrl.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
    ctrl.TempData = new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(
      ctrl.HttpContext,
      Mock.Of<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>());
    return ctrl;
  }

  private static ReportRow NewRow() => new()
  {
    MeetingDate = DateTime.Today.AddDays(-3),
    MeetingDuration = 1,
    DistrictId = 1,
    LocalityId = 1,
    FrameworkId = 1,
    EducationalProgramId = 1,
    DomainId = 1,
    Subject1Id = 1
  };

  private void Seed()
  {
    _db.Users.Add(new User
    {
      Id = 1,
      EmployeeCode = "EMP001",
      IdNumber = "111",
      FirstName = "Test",
      LastName = "Employee",
      Email = "employee@example.test",
      PasswordHash = "hash",
      CreatedAt = DateTime.UtcNow
    });
    _db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 1,
      Description = "April",
      Month = DateTime.Today.Month,
      Year = DateTime.Today.Year,
      LastReportingDate = DateTime.Today.AddDays(10),
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    });
    _db.Reports.Add(new Report
    {
      Id = 1,
      UserId = 1,
      ReportingMonthId = 1,
      StatusId = 2,
      CreatedAt = DateTime.UtcNow
    });
    _db.Allocations.Add(new Allocation
    {
      Id = 1,
      UserId = 1,
      ProjectId = 1,
      IsActive = true,
      AllowExcelUpload = true,
      CreatedAt = DateTime.UtcNow
    });
    _db.SaveChanges();
  }

  private sealed class StubFailingImportService : IReportExcelImportService
  {
    private readonly string[] _errors;
    public StubFailingImportService(IEnumerable<string> errors) => _errors = errors.ToArray();

    public Task<ExcelImportResult> ImportAsync(int reportId, int allocationId, Stream stream, int currentUserId)
    {
      var result = new ExcelImportResult();
      foreach (var error in _errors) result.Errors.Add(error);
      return Task.FromResult(result);
    }
  }
}

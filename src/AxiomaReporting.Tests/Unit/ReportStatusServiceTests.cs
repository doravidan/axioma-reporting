using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class ReportStatusServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly FakeEmailService _email = new();
  private readonly ReportStatusService _sut;

  public ReportStatusServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new ReportStatusService(_db, _email);
    Seed();
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task GetOrCreateDraftAsync_CreatesOnceThenReturnsExisting()
  {
    var created = await _sut.GetOrCreateDraftAsync(1, 1);
    var existing = await _sut.GetOrCreateDraftAsync(1, 1);

    created.Should().NotBeNull();
    existing!.Id.Should().Be(created!.Id);
    _db.Reports.Should().ContainSingle(r => r.UserId == 1 && r.ReportingMonthId == 1);
  }

  [Fact]
  public async Task SaveDraftAsync_MovesDraftToInEntryOnly()
  {
    var draft = AddReport(statusId: 1);
    var pending = AddReport(statusId: 3, id: 2);
    await _db.SaveChangesAsync();

    (await _sut.SaveDraftAsync(draft.Id)).Should().BeTrue();
    (await _sut.SaveDraftAsync(pending.Id)).Should().BeTrue();
    (await _sut.SaveDraftAsync(999)).Should().BeFalse();

    (await _db.Reports.FindAsync(draft.Id))!.StatusId.Should().Be(2);
    (await _db.Reports.FindAsync(pending.Id))!.StatusId.Should().Be(3);
  }

  [Fact]
  public async Task SubmitApproveAndReject_UpdateStatusAndSendExpectedEmails()
  {
    var submitReport = AddReport(statusId: 2);
    var approveReport = AddReport(statusId: 3, id: 2);
    var rejectReport = AddReport(statusId: 3, id: 3);
    await _db.SaveChangesAsync();

    (await _sut.SubmitReportAsync(submitReport.Id, 1)).Should().BeTrue();
    (await _sut.ApproveReportAsync(approveReport.Id, 2)).Should().BeTrue();
    (await _sut.RejectReportAsync(rejectReport.Id, 2, "Fix rows")).Should().BeTrue();

    submitReport.StatusId.Should().Be(3);
    approveReport.StatusId.Should().Be(4);
    approveReport.ApprovedBy.Should().Be(2);
    rejectReport.StatusId.Should().Be(5);
    rejectReport.RejectionReason.Should().Be("Fix rows");
    _email.Sent.Select(e => e.TemplateType).Should()
      .Equal("ReportReceived", "ReportApproved", "ReportRejected");
  }

  [Fact]
  public async Task StatusActions_ReturnFalseForMissingReport()
  {
    (await _sut.SubmitReportAsync(999, 1)).Should().BeFalse();
    (await _sut.ApproveReportAsync(999, 1)).Should().BeFalse();
    (await _sut.RejectReportAsync(999, 1, "x")).Should().BeFalse();
  }

  private Report AddReport(int statusId, int id = 1)
  {
    var report = new Report
    {
      Id = id,
      UserId = 1,
      ReportingMonthId = 1,
      StatusId = statusId,
      CreatedAt = DateTime.UtcNow
    };
    _db.Reports.Add(report);
    return report;
  }

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
      Description = "April 2026",
      Month = 4,
      Year = 2026,
      LastReportingDate = DateTime.Today.AddDays(5),
      CreatedAt = DateTime.UtcNow
    });
    _db.SaveChanges();
  }
}

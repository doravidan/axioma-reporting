using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class ReportExcelImportServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly FakeEmailService _email = new();
  private readonly ReportExcelImportService _sut;

  public ReportExcelImportServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    var status = new ReportStatusService(_db, _email);
    _sut = new ReportExcelImportService(_db, new ReportValidationService(_db), status, _email);
    Seed();
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task ImportAsync_ReturnsErrorWhenReportOrAllocationIsInvalid()
  {
    var missingReport = await _sut.ImportAsync(999, 1, ValidWorkbook(), 1);
    var badAllocation = await _sut.ImportAsync(1, 999, ValidWorkbook(), 1);

    missingReport.Success.Should().BeFalse();
    missingReport.Errors.Should().ContainSingle(e => e.Contains("לא נמצא"));
    badAllocation.Success.Should().BeFalse();
    badAllocation.Errors.Should().ContainSingle(e => e.Contains("אינה מאפשרת"));
  }

  [Fact]
  public async Task ImportAsync_BlocksPendingAndApprovedReports()
  {
    _db.Reports.Add(new Report { Id = 2, UserId = 1, ReportingMonthId = 1, StatusId = 3, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var result = await _sut.ImportAsync(2, 1, ValidWorkbook(), 1);

    result.Success.Should().BeFalse();
    result.Errors.Should().ContainSingle(e => e.Contains("ממתין לאישור"));
  }

  [Fact]
  public async Task ImportAsync_WithInvalidCell_ReturnsRowErrorWithoutSavingRows()
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.AddWorksheet("Rows");
    AddHeader(ws);
    ws.Cell(2, 1).Value = "not a date";
    using var stream = ToStream(workbook);

    var result = await _sut.ImportAsync(1, 1, stream, 1);

    result.Success.Should().BeFalse();
    result.Errors.Should().ContainSingle(e => e.StartsWith("שורה 2"));
    _db.ReportRows.Should().BeEmpty();
  }

  [Fact]
  public async Task ImportAsync_WithValidRows_ReplacesAllocationRowsAndMarksReportImported()
  {
    _db.ReportRows.Add(new ReportRow
    {
      ReportId = 1,
      AllocationId = 1,
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

    var result = await _sut.ImportAsync(1, 1, ValidWorkbook(), 1);

    result.Success.Should().BeTrue();
    result.ImportedRows.Should().Be(1);
    _db.ReportRows.Should().ContainSingle(r => r.ReportId == 1 && r.AllocationId == 1 && r.MeetingDuration == 2);
    var report = await _db.Reports.FindAsync(1);
    report!.ImportedFromExcel.Should().BeTrue();
    report.StatusId.Should().Be(2);
    _email.Sent.Should().ContainSingle(e => e.TemplateType == "ReportReceived");
  }

  [Fact]
  public async Task ImportAsync_ClientHebrewWorkbook_StartsAtRow3AndResolvesDescriptions()
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.AddWorksheet("Rows");
    ws.Cell(1, 11).Value = "נושא המפגש";
    ws.Cell(1, 14).Value = "מסקנות";
    ws.Cell(2, 1).Value = "מס\"ד";
    ws.Cell(2, 2).Value = "קוד עובד";
    ws.Cell(2, 4).Value = "מחוז";
    ws.Cell(2, 5).Value = "יישוב";
    ws.Cell(2, 6).Value = "שם המסגרת חינוכית";
    ws.Cell(2, 7).Value = "תאריך המפגש";
    ws.Cell(2, 8).Value = "משך המפגש (בשעות)";
    ws.Cell(2, 9).Value = "תוכנית חינוכית";
    ws.Cell(2, 10).Value = "תחום";
    ws.Cell(2, 11).Value = "נושא 1";
    ws.Cell(2, 19).Value = "הערות";
    ws.Cell(3, 1).Value = 1;
    ws.Cell(3, 2).Value = "EMP001";
    ws.Cell(3, 4).Value = "District A";
    ws.Cell(3, 5).Value = "Locality A";
    ws.Cell(3, 6).Value = "640086 Framework A";
    ws.Cell(3, 7).Value = DateTime.Today.AddDays(-1);
    ws.Cell(3, 8).Value = 2;
    ws.Cell(3, 9).Value = "Education Program A";
    ws.Cell(3, 10).Value = "Domain A";
    ws.Cell(3, 11).Value = "Subject A";
    ws.Cell(3, 19).Value = "client format";

    using var stream = ToStream(workbook);

    var result = await _sut.ImportAsync(1, 1, stream, 1);

    result.Success.Should().BeTrue();
    result.ImportedRows.Should().Be(1);
    _db.ReportRows.Should().ContainSingle(r =>
      r.DistrictId == 1 &&
      r.LocalityId == 1 &&
      r.FrameworkId == 1 &&
      r.EducationalProgramId == 1 &&
      r.DomainId == 1 &&
      r.Subject1Id == 1 &&
      r.Notes == "client format");
  }

  [Fact]
  public async Task ImportAsync_TemplateWithHebrewValues_ResolvesDescriptionsAndCompositeFrameworkLabel()
  {
    // Regression (client fix 07/2026 #4): the personal template filled with Hebrew
    // descriptions — including the composite framework label from the UI — must import.
    using var workbook = new XLWorkbook();
    var ws = workbook.AddWorksheet("דיווח");
    ws.Cell(1, 1).Value = "תאריך מפגש";
    ws.Cell(1, 2).Value = "משך תפוקה";
    ws.Cell(1, 3).Value = "מחוז";
    ws.Cell(1, 4).Value = "יישוב";
    ws.Cell(1, 5).Value = "מסגרת";
    ws.Cell(2, 1).Value = DateTime.Today.AddDays(-1);
    ws.Cell(2, 2).Value = 1.5;
    ws.Cell(2, 3).Value = "District A";
    ws.Cell(2, 4).Value = "Locality A";
    ws.Cell(2, 5).Value = "Locality A — 640086 — Framework A";
    ws.Cell(2, 6).Value = "Education Program A";
    ws.Cell(2, 7).Value = "Domain A";
    ws.Cell(2, 8).Value = "Subject A";
    ws.Cell(2, 16).Value = "hebrew template";

    using var stream = ToStream(workbook);

    var result = await _sut.ImportAsync(1, 1, stream, 1);

    result.Success.Should().BeTrue(string.Join("; ", result.Errors));
    _db.ReportRows.Should().ContainSingle(r =>
      r.DistrictId == 1 && r.LocalityId == 1 && r.FrameworkId == 1 &&
      r.MeetingDuration == 1.5m && r.Notes == "hebrew template");
  }

  [Fact]
  public async Task ImportAsync_ClientHebrewWorkbook_FindsHeaderBelowRow2AndDoesNotImportHeaderRows()
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.AddWorksheet("Rows");
    ws.Cell(1, 11).Value = "נושא המפגש";
    ws.Cell(1, 14).Value = "מסקנות";
    ws.Cell(2, 4).Value = "מחוז וכותרות כלליות";
    ws.Cell(2, 7).Value = "תאריך ופרטי מפגש";
    AddClientHebrewHeader(ws, 3);
    AddClientHebrewDataRow(ws, 4, "client row after header");

    using var stream = ToStream(workbook);

    var result = await _sut.ImportAsync(1, 1, stream, 1);

    result.Success.Should().BeTrue(string.Join("; ", result.Errors));
    result.ImportedRows.Should().Be(1);
    result.Errors.Should().NotContain(e => e.Contains("שורה 2"));
    _db.ReportRows.Should().ContainSingle(r => r.Notes == "client row after header" && r.DistrictId == 1);
  }

  [Fact]
  public async Task ImportAsync_ClientHebrewWorkbook_SkipsRepeatedHeaderRowsInsideData()
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.AddWorksheet("Rows");
    AddClientHebrewHeader(ws, 2);
    AddClientHebrewDataRow(ws, 3, "first row");
    AddClientHebrewHeader(ws, 4);
    AddClientHebrewDataRow(ws, 5, "second row");

    using var stream = ToStream(workbook);

    var result = await _sut.ImportAsync(1, 1, stream, 1);

    result.Success.Should().BeTrue(string.Join("; ", result.Errors));
    result.ImportedRows.Should().Be(2);
    _db.ReportRows.Select(r => r.Notes).Should().BeEquivalentTo("first row", "second row");
  }

  [Fact]
  public async Task ImportAsync_ClientHebrewWorkbook_WithDifferentEmployeeCode_ReturnsErrorWithoutSavingRows()
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.AddWorksheet("Rows");
    AddClientHebrewHeader(ws, 2);
    AddClientHebrewDataRow(ws, 3, "wrong employee");
    ws.Cell(3, 2).Value = "OTHER_EMPLOYEE";

    using var stream = ToStream(workbook);

    var result = await _sut.ImportAsync(1, 1, stream, 1);

    result.Success.Should().BeFalse();
    result.Errors.Should().ContainSingle(e => e.Contains("קוד העובד בקובץ"));
    _db.ReportRows.Should().BeEmpty();
  }

  private static MemoryStream ValidWorkbook()
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.AddWorksheet("Rows");
    AddHeader(ws);
    ws.Cell(2, 1).Value = DateTime.Today.AddDays(-1);
    ws.Cell(2, 2).Value = 2;
    for (var col = 3; col <= 8; col++)
      ws.Cell(2, col).Value = 1;
    ws.Cell(2, 16).Value = "notes";
    return ToStream(workbook);
  }

  private static void AddHeader(IXLWorksheet ws)
  {
    for (var i = 1; i <= 16; i++)
      ws.Cell(1, i).Value = $"Column{i}";
  }

  private static void AddClientHebrewHeader(IXLWorksheet ws, int row)
  {
    ws.Cell(row, 1).Value = "מס\"ד";
    ws.Cell(row, 2).Value = "קוד עובד";
    ws.Cell(row, 4).Value = "מחוז";
    ws.Cell(row, 5).Value = "יישוב";
    ws.Cell(row, 6).Value = "שם המסגרת חינוכית";
    ws.Cell(row, 7).Value = "תאריך המפגש";
    ws.Cell(row, 8).Value = "משך המפגש (בשעות)";
    ws.Cell(row, 9).Value = "תוכנית חינוכית";
    ws.Cell(row, 10).Value = "תחום";
    ws.Cell(row, 11).Value = "נושא 1";
    ws.Cell(row, 19).Value = "הערות";
  }

  private static void AddClientHebrewDataRow(IXLWorksheet ws, int row, string notes)
  {
    ws.Cell(row, 1).Value = row;
    ws.Cell(row, 2).Value = "EMP001";
    ws.Cell(row, 4).Value = "District A";
    ws.Cell(row, 5).Value = "Locality A";
    ws.Cell(row, 6).Value = "640086 Framework A";
    ws.Cell(row, 7).Value = DateTime.Today.AddDays(-1);
    ws.Cell(row, 8).Value = 2;
    ws.Cell(row, 9).Value = "Education Program A";
    ws.Cell(row, 10).Value = "Domain A";
    ws.Cell(row, 11).Value = "Subject A";
    ws.Cell(row, 19).Value = notes;
  }

  private static MemoryStream ToStream(XLWorkbook workbook)
  {
    var stream = new MemoryStream();
    workbook.SaveAs(stream);
    stream.Position = 0;
    return stream;
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
      Description = "April",
      Month = DateTime.Today.Month,
      Year = DateTime.Today.Year,
      LastReportingDate = DateTime.Today.AddDays(5),
      CreatedAt = DateTime.UtcNow
    });
    _db.Reports.Add(new Report { Id = 1, UserId = 1, ReportingMonthId = 1, StatusId = 1, CreatedAt = DateTime.UtcNow });
    _db.Allocations.Add(new Allocation
    {
      Id = 1,
      UserId = 1,
      ProjectId = 1,
      IsActive = true,
      AllowExcelUpload = true,
      CreatedAt = DateTime.UtcNow
    });
    _db.Districts.Add(new District { Id = 1, Description = "District A", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Localities.Add(new Locality { Id = 1, Description = "Locality A", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Frameworks.Add(new Framework { Id = 1, Description = "Framework A", InstitutionSymbol = "640086", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.EducationalPrograms.Add(new EducationalProgram { Id = 1, Description = "Education Program A", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Domains.Add(new Domain { Id = 1, Description = "Domain A", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Subjects.Add(new Subject { Id = 1, Description = "Subject A", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.SaveChanges();
  }
}

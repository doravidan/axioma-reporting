using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Additional Excel import validation tests NOT covered by ReportExcelImportServiceTests.cs.
///
/// Covered here:
/// - Approved report (StatusId = 4) blocks import
/// - Draft report (StatusId = 1/2) allows overwrite
/// - ReturnedForCorrection (StatusId = 5) allows overwrite
///
/// Excluded (already in ReportExcelImportServiceTests):
/// - PendingApproval (StatusId = 3) is already tested there
/// - Missing report / bad allocation
/// - Invalid cell values
/// - Valid import replaces rows and marks ImportedFromExcel = true
/// </summary>
public class ExcelImportValidationTests : IDisposable
{
    private readonly AppDbContext _db;
    private readonly FakeEmailService _email = new();
    private readonly ReportExcelImportService _sut;

    public ExcelImportValidationTests()
    {
        _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);

        var status = new ReportStatusService(_db, _email);
        _sut = new ReportExcelImportService(_db, new ReportValidationService(_db), status, _email);

        Seed();
    }

    public void Dispose() => _db.Dispose();

    // -----------------------------------------------------------------------
    // Tests: Approved report must block import
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ImportAsync_BlocksApprovedReport_ReturnsSpecificError()
    {
        // Arrange — report has StatusId = 4 (Approved); spec says import ONLY allowed for unapproved reports
        _db.Reports.Add(new Report
        {
            Id = 2,
            UserId = 1,
            ReportingMonthId = 1,
            StatusId = 4,   // Approved
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        // Act
        var result = await _sut.ImportAsync(2, 1, ValidWorkbook(), 1);

        // Assert
        result.Success.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.Contains("אושר") || e.Contains("ממתין לאישור"),
            "importing into an approved report must produce an explanatory error");
    }

    // -----------------------------------------------------------------------
    // Tests: Draft report allows overwrite
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ImportAsync_AllowsOverwriteOfDraftReport()
    {
        // Arrange — report has StatusId = 1 (Draft); an existing row is already present
        _db.Reports.Add(new Report
        {
            Id = 3,
            UserId = 1,
            ReportingMonthId = 1,
            StatusId = 1,   // Draft
            CreatedAt = DateTime.UtcNow
        });
        _db.ReportRows.Add(new ReportRow
        {
            Id = 10,
            ReportId = 3,
            AllocationId = 1,
            SequenceNumber = 1,
            MeetingDate = DateTime.Today.AddDays(-5),
            MeetingDuration = 3,
            DistrictId = 1,
            LocalityId = 1,
            FrameworkId = 1,
            EducationalProgramId = 1,
            DomainId = 1,
            Subject1Id = 1,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        // Act
        var result = await _sut.ImportAsync(3, 1, ValidWorkbook(), 1);

        // Assert — import succeeded and replaced the existing row
        result.Success.Should().BeTrue(string.Join("; ", result.Errors));
        result.ImportedRows.Should().Be(1);

        // Old row (MeetingDuration = 3) is gone; new row (MeetingDuration = 2) is present
        var rows = await _db.ReportRows.Where(r => r.ReportId == 3 && r.AllocationId == 1).ToListAsync();
        rows.Should().ContainSingle(r => r.MeetingDuration == 2);
        rows.Should().NotContain(r => r.MeetingDuration == 3, "previous draft rows must have been replaced");

        var report = await _db.Reports.FindAsync(3);
        report!.ImportedFromExcel.Should().BeTrue();
    }

    [Fact]
    public async Task ImportAsync_AllowsOverwriteOfEditInProgressReport()
    {
        // Arrange — report has StatusId = 2 (EditInProgress); also not yet submitted
        _db.Reports.Add(new Report
        {
            Id = 4,
            UserId = 1,
            ReportingMonthId = 1,
            StatusId = 2,   // EditInProgress
            CreatedAt = DateTime.UtcNow
        });
        _db.ReportRows.Add(new ReportRow
        {
            Id = 20,
            ReportId = 4,
            AllocationId = 1,
            SequenceNumber = 1,
            MeetingDate = DateTime.Today.AddDays(-5),
            MeetingDuration = 5,
            DistrictId = 1,
            LocalityId = 1,
            FrameworkId = 1,
            EducationalProgramId = 1,
            DomainId = 1,
            Subject1Id = 1,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        // Act
        var result = await _sut.ImportAsync(4, 1, ValidWorkbook(), 1);

        // Assert
        result.Success.Should().BeTrue(string.Join("; ", result.Errors));
        var rows = await _db.ReportRows.Where(r => r.ReportId == 4 && r.AllocationId == 1).ToListAsync();
        rows.Should().ContainSingle(r => r.MeetingDuration == 2);
    }

    // -----------------------------------------------------------------------
    // Tests: ReturnedForCorrection report allows overwrite
    // -----------------------------------------------------------------------

    [Fact]
    public async Task ImportAsync_AllowsOverwriteOfRejectedReport()
    {
        // Arrange — report has StatusId = 5 (ReturnedForCorrection)
        _db.Reports.Add(new Report
        {
            Id = 5,
            UserId = 1,
            ReportingMonthId = 1,
            StatusId = 5,   // ReturnedForCorrection
            RejectionReason = "צריך תיקון",
            CreatedAt = DateTime.UtcNow
        });
        _db.ReportRows.Add(new ReportRow
        {
            Id = 30,
            ReportId = 5,
            AllocationId = 1,
            SequenceNumber = 1,
            MeetingDate = DateTime.Today.AddDays(-5),
            MeetingDuration = 4,
            DistrictId = 1,
            LocalityId = 1,
            FrameworkId = 1,
            EducationalProgramId = 1,
            DomainId = 1,
            Subject1Id = 1,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        // Act
        var result = await _sut.ImportAsync(5, 1, ValidWorkbook(), 1);

        // Assert — import replaces the rejected report's rows
        result.Success.Should().BeTrue(string.Join("; ", result.Errors));
        result.ImportedRows.Should().Be(1);

        var rows = await _db.ReportRows.Where(r => r.ReportId == 5 && r.AllocationId == 1).ToListAsync();
        rows.Should().ContainSingle(r => r.MeetingDuration == 2,
            "the rejected report's rows must be replaced with the imported rows");

        // After import the report is marked as ImportedFromExcel; SaveDraftAsync only
        // transitions Draft(1) → InEntry(2); a ReturnedForCorrection(5) report keeps
        // its status unchanged until the employee explicitly submits.
        var report = await _db.Reports.FindAsync(5);
        report!.ImportedFromExcel.Should().BeTrue();
        report.StatusId.Should().Be(5,
            "SaveDraftAsync only transitions status 1→2; ReturnedForCorrection stays at 5 " +
            "until the employee resubmits");
    }

    [Fact]
    public async Task ImportAsync_AfterSuccessfulOverwrite_SendsReportReceivedEmail()
    {
        // Arrange
        _db.Reports.Add(new Report
        {
            Id = 6,
            UserId = 1,
            ReportingMonthId = 1,
            StatusId = 1,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        // Act
        var result = await _sut.ImportAsync(6, 1, ValidWorkbook(), 1);

        // Assert
        result.Success.Should().BeTrue(string.Join("; ", result.Errors));
        _email.Sent.Should().ContainSingle(e =>
            e.TemplateType == "ReportReceived" && e.ToEmail == "employee@example.test");
    }

    // -----------------------------------------------------------------------
    // Helpers
    // -----------------------------------------------------------------------

    /// <summary>
    /// Produces an in-memory XLWorkbook stream with one valid data row.
    /// Column layout matches ReportExcelImportService.ParseRow:
    ///   1=MeetingDate  2=MeetingDuration  3-8=required IDs  9-15=optional  16=notes
    /// </summary>
    private static MemoryStream ValidWorkbook()
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.AddWorksheet("Rows");
        AddHeader(ws);
        ws.Cell(2, 1).Value = DateTime.Today.AddDays(-1);   // MeetingDate in current month
        ws.Cell(2, 2).Value = 2;                             // MeetingDuration
        for (var col = 3; col <= 8; col++)
            ws.Cell(2, col).Value = 1;                       // required IDs
        ws.Cell(2, 16).Value = "test notes";
        return ToStream(workbook);
    }

    private static void AddHeader(IXLWorksheet ws)
    {
        for (var i = 1; i <= 16; i++)
            ws.Cell(1, i).Value = $"Column{i}";
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
            StatusId = 1,
            CreatedAt = DateTime.UtcNow
        });
        _db.ReportingMonths.Add(new ReportingMonth
        {
            Id = 1,
            Description = "Current Month",
            Month = DateTime.Today.Month,
            Year = DateTime.Today.Year,
            LastReportingDate = DateTime.Today.AddDays(10),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });
        // Report Id = 1 is used as the base valid seeded report by helper methods.
        // Individual tests add their own reports with Id >= 2.
        _db.Allocations.Add(new Allocation
        {
            Id = 1,
            UserId = 1,
            ProjectId = 1,
            IsActive = true,
            AllowExcelUpload = true,
            CreatedAt = DateTime.UtcNow
        });
        // The importer resolves every lookup column (id or Hebrew description) and
        // rejects ids that do not exist — the workbook's id=1 values need real rows.
        _db.Districts.Add(new District { Id = 1, Description = "District A", IsActive = true, CreatedAt = DateTime.UtcNow });
        _db.Localities.Add(new Locality { Id = 1, Description = "Locality A", IsActive = true, CreatedAt = DateTime.UtcNow });
        _db.Frameworks.Add(new Framework { Id = 1, Description = "Framework A", InstitutionSymbol = "640086", IsActive = true, CreatedAt = DateTime.UtcNow });
        _db.EducationalPrograms.Add(new EducationalProgram { Id = 1, Description = "Education Program A", IsActive = true, CreatedAt = DateTime.UtcNow });
        _db.Domains.Add(new Domain { Id = 1, Description = "Domain A", IsActive = true, CreatedAt = DateTime.UtcNow });
        _db.Subjects.Add(new Subject { Id = 1, Description = "Subject A", IsActive = true, CreatedAt = DateTime.UtcNow });
        _db.SaveChanges();
    }
}

using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class BatchReportImportServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly FakeEmailService _email = new();
  private readonly BatchReportImportService _sut;

  public BatchReportImportServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    var resolver = new LookupResolver(_db);
    var validator = new ReportValidationService(_db);
    _sut = new BatchReportImportService(_db, resolver, validator, _email);
    Seed();
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public void FindHeaderRow_WithHeaderAtRow7_Returns7()
  {
    using var wb = new XLWorkbook();
    var ws = wb.AddWorksheet("Data");
    // Put some empty / free-text rows
    ws.Cell(1, 1).Value = "דו\"ח חודשי";
    ws.Cell(7, 1).Value = "מס\"ד";
    ws.Cell(7, 2).Value = "קוד עובד";
    ws.Cell(7, 3).Value = "שם המדווח";

    BatchReportImportService.FindHeaderRow(ws).Should().Be(7);
  }

  [Fact]
  public void FindHeaderRow_WithHeaderAtRow1_Returns1()
  {
    using var wb = new XLWorkbook();
    var ws = wb.AddWorksheet("Data");
    ws.Cell(1, 1).Value = "קוד עובד";
    ws.Cell(1, 2).Value = "שם המדווח";

    BatchReportImportService.FindHeaderRow(ws).Should().Be(1);
  }

  [Fact]
  public async Task ResolveDistrictAsync_ExactMatch_ReturnsId()
  {
    var resolver = new LookupResolver(_db);
    var id = await resolver.ResolveDistrictAsync("צפון");
    id.Should().Be(5);
  }

  [Fact]
  public async Task ResolveDistrictAsync_NumericIdWhenExists_ReturnsId()
  {
    var resolver = new LookupResolver(_db);
    var id = await resolver.ResolveDistrictAsync("5");
    id.Should().Be(5);
  }

  [Fact]
  public async Task ResolveDistrictAsync_Unknown_ReturnsNull()
  {
    var resolver = new LookupResolver(_db);
    var id = await resolver.ResolveDistrictAsync("לא קיים");
    id.Should().BeNull();
  }

  [Fact]
  public async Task ImportAsync_HappyPath_ImportsAllRowsAndSendsEmails()
  {
    using var stream = BuildWorkbook(new[]
    {
      Row(1, "EMP001", "ישראל ישראלי", DateTime.Today.AddDays(-1), 2),
      Row(2, "EMP001", "ישראל ישראלי", DateTime.Today.AddDays(-2), 3),
      Row(3, "EMP001", "ישראל ישראלי", DateTime.Today.AddDays(-3), 1),
      Row(4, "EMP002", "דנה כהן", DateTime.Today.AddDays(-1), 2),
      Row(5, "EMP002", "דנה כהן", DateTime.Today.AddDays(-2), 1),
      Row(6, "EMP002", "דנה כהן", DateTime.Today.AddDays(-3), 1),
    });

    var result = await _sut.ImportAsync(stream, reportingMonthId: 1, uploaderUserId: 99);

    result.TotalRowsRead.Should().Be(6);
    result.RowsImported.Should().Be(6);
    result.ErrorRowsCount.Should().Be(0);
    result.EmployeesAffected.Should().Be(2);
    _db.ReportRows.Count().Should().Be(6);
    _db.Reports.Count().Should().Be(2);
    // One success email per reporting employee.
    _email.Sent.Count(e => e.TemplateType == "ReportReceived").Should().Be(2);
  }

  [Fact]
  public async Task ImportAsync_WithOneInvalidRow_ImportsValidRowsAndReportsError()
  {
    // Third row uses an unknown district.
    using var stream = BuildWorkbook(new[]
    {
      Row(1, "EMP001", "ישראל ישראלי", DateTime.Today.AddDays(-1), 2),
      Row(2, "EMP001", "ישראל ישראלי", DateTime.Today.AddDays(-2), 1),
      Row(3, "EMP001", "ישראל ישראלי", DateTime.Today.AddDays(-3), 2, district: "מחוז שלא קיים"),
    });

    var result = await _sut.ImportAsync(stream, reportingMonthId: 1, uploaderUserId: 99);

    result.TotalRowsRead.Should().Be(3);
    result.RowsImported.Should().Be(2);
    result.ErrorRowsCount.Should().Be(1);
    result.Errors.Single().ErrorMessage.Should().Contain("מחוז");
    _db.ReportRows.Count().Should().Be(2);
  }

  [Fact]
  public async Task ImportAsync_EmployeeNotFound_DoesNotImportRow()
  {
    using var stream = BuildWorkbook(new[]
    {
      Row(1, "GHOST", "רוח רפאים", DateTime.Today.AddDays(-1), 2),
    });

    var result = await _sut.ImportAsync(stream, reportingMonthId: 1, uploaderUserId: 99);

    result.TotalRowsRead.Should().Be(1);
    result.RowsImported.Should().Be(0);
    result.ErrorRowsCount.Should().Be(1);
    result.Errors.Single().ErrorMessage.Should().Contain("GHOST");
    _db.ReportRows.Should().BeEmpty();
  }

  // ---------- helpers ----------

  private static MemoryStream BuildWorkbook(IEnumerable<TestRow> rows)
  {
    using var wb = new XLWorkbook();
    var ws = wb.AddWorksheet("Data");

    // Introduce some blank rows above the header to mimic the real file where the header sits at row 7.
    ws.Cell(3, 1).Value = "דו\"ח מרוכז";

    // Header at row 7
    ws.Cell(7, 1).Value = "מס\"ד";
    ws.Cell(7, 2).Value = "קוד עובד";
    ws.Cell(7, 3).Value = "שם המדווח \n (פרטי + משפחה)";
    ws.Cell(7, 4).Value = "מחוז";
    ws.Cell(7, 5).Value = "יישוב";
    ws.Cell(7, 6).Value = "שם המסגרת חינוכית";
    ws.Cell(7, 7).Value = "תאריך המפגש";
    ws.Cell(7, 8).Value = "משך המפגש (בשעות)";
    ws.Cell(7, 9).Value = "תוכנית חינוכית";
    ws.Cell(7, 10).Value = "תחום";
    ws.Cell(7, 11).Value = "נושא 1";
    ws.Cell(7, 12).Value = "הערות";

    var r = 8;
    foreach (var row in rows)
    {
      ws.Cell(r, 1).Value = row.Seq;
      ws.Cell(r, 2).Value = row.EmployeeCode;
      ws.Cell(r, 3).Value = row.ReporterName;
      ws.Cell(r, 4).Value = row.District;
      ws.Cell(r, 5).Value = row.Locality;
      ws.Cell(r, 6).Value = row.Framework;
      ws.Cell(r, 7).Value = row.Date;
      ws.Cell(r, 8).Value = row.Duration;
      ws.Cell(r, 9).Value = row.EducationalProgram;
      ws.Cell(r, 10).Value = row.Domain;
      ws.Cell(r, 11).Value = row.Subject1;
      ws.Cell(r, 12).Value = row.Notes;
      r++;
    }

    var stream = new MemoryStream();
    wb.SaveAs(stream);
    stream.Position = 0;
    return stream;
  }

  private static TestRow Row(int seq, string emp, string name, DateTime date, decimal duration,
    string district = "צפון", string locality = "חיפה", string framework = "בית ספר יסודי",
    string eduProgram = "תוכנית א", string domain = "תחום א", string subject1 = "נושא ראשון",
    string notes = "")
    => new(seq, emp, name, district, locality, framework, date, duration, eduProgram, domain, subject1, notes);

  private sealed record TestRow(
    int Seq, string EmployeeCode, string ReporterName,
    string District, string Locality, string Framework,
    DateTime Date, decimal Duration,
    string EducationalProgram, string Domain, string Subject1, string Notes);

  private void Seed()
  {
    // Lookups
    _db.Districts.Add(new District { Id = 5, Description = "צפון", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Localities.Add(new Locality { Id = 1, Description = "חיפה", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Frameworks.Add(new Framework { Id = 1, Description = "בית ספר יסודי", InstitutionSymbol = "1234", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.EducationalPrograms.Add(new EducationalProgram { Id = 1, Description = "תוכנית א", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Domains.Add(new Domain { Id = 1, Description = "תחום א", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Subjects.Add(new Subject { Id = 1, Description = "נושא ראשון", IsActive = true, CreatedAt = DateTime.UtcNow });

    // Users (employees)
    _db.Users.Add(new User
    {
      Id = 1, EmployeeCode = "EMP001", IdNumber = "111",
      FirstName = "ישראל", LastName = "ישראלי",
      Email = "emp1@example.test", PasswordHash = "x", CreatedAt = DateTime.UtcNow
    });
    _db.Users.Add(new User
    {
      Id = 2, EmployeeCode = "EMP002", IdNumber = "222",
      FirstName = "דנה", LastName = "כהן",
      Email = "emp2@example.test", PasswordHash = "x", CreatedAt = DateTime.UtcNow
    });
    _db.Users.Add(new User
    {
      Id = 99, EmployeeCode = "ADMIN", IdNumber = "999",
      FirstName = "מנהל", LastName = "מערכת",
      Email = "admin@example.test", PasswordHash = "x", CreatedAt = DateTime.UtcNow
    });

    // Allocations (single per user -> unambiguous resolution)
    _db.Allocations.Add(new Allocation
    {
      Id = 10, UserId = 1, ProjectId = 1, IsActive = true,
      AllowExcelUpload = true, CreatedAt = DateTime.UtcNow,
      MonthlyEmploymentScope = 100m, DailyEmploymentScope = 9m
    });
    _db.Allocations.Add(new Allocation
    {
      Id = 20, UserId = 2, ProjectId = 1, IsActive = true,
      AllowExcelUpload = true, CreatedAt = DateTime.UtcNow,
      MonthlyEmploymentScope = 100m, DailyEmploymentScope = 9m
    });

    // Reporting month - include current date
    _db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 1,
      Description = "חודש דיווח",
      Month = DateTime.Today.Month,
      Year = DateTime.Today.Year,
      LastReportingDate = DateTime.Today.AddDays(10),
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    });

    _db.SaveChanges();
  }
}

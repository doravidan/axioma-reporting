using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Verifies that <see cref="BatchReportImportService"/> populates a per-row
/// Hebrew <c>ResultDescription</c> column for every processed row in the file
/// (added, duplicate skipped, unresolved/rejected) — covering client feedback item #10.
/// </summary>
public class BatchReportImportRowDescriptionTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly FakeEmailService _email = new();
  private readonly BatchReportImportService _sut;

  public BatchReportImportRowDescriptionTests()
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
  public async Task ImportAsync_ProducesAddedDuplicateAndRejectedRowDescriptions()
  {
    // One good new row, one duplicate of it, one row with an unknown employee code.
    var date = DateTime.Today.AddDays(-1);
    using var stream = BuildWorkbook(new[]
    {
      Row("EMP001", "ישראל ישראלי", date, 2),
      Row("EMP001", "ישראל ישראלי", date, 2), // exact duplicate (no notes)
      Row("GHOST",  "רוח רפאים",   date, 2),  // unresolved employee
    });

    var result = await _sut.ImportAsync(stream, reportingMonthId: 1, uploaderUserId: 99);

    result.RowResults.Should().HaveCount(3);

    var byOutcome = result.RowResults
      .GroupBy(r => r.Outcome)
      .ToDictionary(g => g.Key, g => g.ToList());

    byOutcome.Should().ContainKey(BatchImportRowOutcome.Added);
    byOutcome.Should().ContainKey(BatchImportRowOutcome.Skipped);
    byOutcome.Should().ContainKey(BatchImportRowOutcome.Rejected);

    byOutcome[BatchImportRowOutcome.Added].Single().ResultDescription
      .Should().Contain("התאמה להקצאה")
      .And.Contain("שורה נוספה");

    byOutcome[BatchImportRowOutcome.Skipped].Single().ResultDescription
      .Should().Contain("דולגה")
      .And.Contain("דוח כפול");

    byOutcome[BatchImportRowOutcome.Rejected].Single().ResultDescription
      .Should().Contain("נדחה")
      .And.Contain("אין הקצאה תואמת");
  }

  [Fact]
  public async Task ImportAsync_NewRowAddedToExistingReport_ReportsUpdatedDescription()
  {
    // Seed an existing report for EMP001 in the target reporting month.
    _db.Reports.Add(new Report
    {
      Id = 50,
      UserId = 1,
      ReportingMonthId = 1,
      StatusId = 2,
      CreatedAt = DateTime.UtcNow.AddDays(-2)
    });
    _db.SaveChanges();

    using var stream = BuildWorkbook(new[]
    {
      Row("EMP001", "ישראל ישראלי", DateTime.Today.AddDays(-3), 1),
    });

    var result = await _sut.ImportAsync(stream, reportingMonthId: 1, uploaderUserId: 99);

    result.RowResults.Should().HaveCount(1);
    var only = result.RowResults.Single();
    only.Outcome.Should().Be(BatchImportRowOutcome.Updated);
    only.ResultDescription.Should().Contain("עודכן דוח קיים");
  }

  // ---------- helpers ----------

  private static MemoryStream BuildWorkbook(IEnumerable<TestRow> rows)
  {
    using var wb = new XLWorkbook();
    var ws = wb.AddWorksheet("Data");

    ws.Cell(7, 1).Value = "מס\"ד";
    ws.Cell(7, 2).Value = "קוד עובד";
    ws.Cell(7, 3).Value = "שם המדווח";
    ws.Cell(7, 4).Value = "מחוז";
    ws.Cell(7, 5).Value = "יישוב";
    ws.Cell(7, 6).Value = "שם המסגרת חינוכית";
    ws.Cell(7, 7).Value = "תאריך המפגש";
    ws.Cell(7, 8).Value = "משך המפגש";
    ws.Cell(7, 9).Value = "תוכנית חינוכית";
    ws.Cell(7, 10).Value = "תחום";
    ws.Cell(7, 11).Value = "נושא 1";
    ws.Cell(7, 12).Value = "הערות";

    var r = 8;
    foreach (var row in rows)
    {
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

  private static TestRow Row(string emp, string name, DateTime date, decimal duration,
    string district = "צפון", string locality = "חיפה", string framework = "בית ספר יסודי",
    string eduProgram = "תוכנית א", string domain = "תחום א", string subject1 = "נושא ראשון",
    string notes = "")
    => new(emp, name, district, locality, framework, date, duration, eduProgram, domain, subject1, notes);

  private sealed record TestRow(
    string EmployeeCode, string ReporterName,
    string District, string Locality, string Framework,
    DateTime Date, decimal Duration,
    string EducationalProgram, string Domain, string Subject1, string Notes);

  private void Seed()
  {
    _db.Districts.Add(new District { Id = 5, Description = "צפון", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Localities.Add(new Locality { Id = 1, Description = "חיפה", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Frameworks.Add(new Framework { Id = 1, Description = "בית ספר יסודי", InstitutionSymbol = "1234", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.EducationalPrograms.Add(new EducationalProgram { Id = 1, Description = "תוכנית א", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Domains.Add(new Domain { Id = 1, Description = "תחום א", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.Subjects.Add(new Subject { Id = 1, Description = "נושא ראשון", IsActive = true, CreatedAt = DateTime.UtcNow });

    _db.Users.Add(new User
    {
      Id = 1, EmployeeCode = "EMP001", IdNumber = "111",
      FirstName = "ישראל", LastName = "ישראלי",
      Email = "emp1@example.test", PasswordHash = "x", CreatedAt = DateTime.UtcNow
    });
    _db.Users.Add(new User
    {
      Id = 99, EmployeeCode = "ADMIN", IdNumber = "999",
      FirstName = "מנהל", LastName = "מערכת",
      Email = "admin@example.test", PasswordHash = "x", CreatedAt = DateTime.UtcNow
    });

    _db.Allocations.Add(new Allocation
    {
      Id = 10, UserId = 1, ProjectId = 1, IsActive = true,
      AllowExcelUpload = true, CreatedAt = DateTime.UtcNow,
      MonthlyEmploymentScope = 100m, DailyEmploymentScope = 9m
    });

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

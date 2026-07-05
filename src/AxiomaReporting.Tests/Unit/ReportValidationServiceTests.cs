using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class ReportValidationServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly IReportValidationService _sut;

  public ReportValidationServiceTests()
  {
    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options;
    _db = new AppDbContext(options);
    _db.Allocations.Add(new Allocation
    {
      Id = 1,
      UserId = 1,
      ProjectId = 1,
      CreatedAt = DateTime.UtcNow,
      IsActive = true
    });
    _db.SaveChanges();
    _sut = new ReportValidationService(_db);
  }

  public void Dispose() => _db.Dispose();

  // ---- helpers ----

  private static User DefaultEmployee() => new User
  {
    Id = 1,
    EmployeeCode = "EMP001",
    IdNumber = "123456789",
    FirstName = "ישראל",
    LastName = "ישראלי",
    PasswordHash = "hash",
    RestDay = null,
    AllowFutureReporting = false
  };

  private static DateTime ValidMeetingDate()
  {
    return DateTime.Today.Day > 1 ? DateTime.Today.AddDays(-1) : DateTime.Today;
  }

  private static ReportingMonth DefaultMonth()
  {
    var meetingDate = ValidMeetingDate();
    return new ReportingMonth
    {
      Id = 1,
      Description = $"{meetingDate:MM/yyyy}",
      Month = meetingDate.Month,
      Year = meetingDate.Year,
      LastReportingDate = DateTime.Today.AddDays(10),
      IsActive = true,
      AllowFutureReporting = false
    };
  }

  private static ReportRow ValidRow() => new ReportRow
  {
    Id = 0,
    ReportId = 1,
    AllocationId = 1,
    SequenceNumber = 1,
    MeetingDate = ValidMeetingDate(),
    MeetingDuration = 2,
    DistrictId = 1,
    LocalityId = 1,
    FrameworkId = 1,
    EducationalProgramId = 1,
    DomainId = 1,
    Subject1Id = 1
  };

  // ---- tests ----

  [Fact]
  public async Task ValidateRowAsync_MissingRequiredFields_ReturnsErrors()
  {
    // Arrange
    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var row = new ReportRow
    {
      MeetingDate = DateTime.Today,
      MeetingDuration = 1
      // All required IDs are 0 (default)
    };

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("מחוז"));
    result.Errors.Should().Contain(e => e.Contains("ישוב"));
    result.Errors.Should().Contain(e => e.Contains("מסגרת"));
    result.Errors.Should().Contain(e => e.Contains("תוכנית חינוכית"));
    result.Errors.Should().Contain(e => e.Contains("תחום"));
    result.Errors.Should().Contain(e => e.Contains("נושא 1"));
  }

  [Fact]
  public async Task ValidateRowAsync_ZeroDuration_ReturnsError()
  {
    // Arrange
    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var row = ValidRow();
    row.MeetingDuration = 0;

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("משך המפגש"));
  }

  [Fact]
  public async Task ValidateRowAsync_EmployeeRestDay_ReturnsError()
  {
    // Arrange — employee rests on Sunday (0)
    var employee = DefaultEmployee();
    employee.RestDay = 0;

    var month = DefaultMonth();
    // Find the coming Sunday
    var today = DateTime.Today;
    var daysUntilSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7) % 7;
    // Use past Sunday
    var sunday = today.AddDays(-(7 - daysUntilSunday) % 7 == 0 ? 7 : (7 - daysUntilSunday) % 7);
    // Simpler: just pick any past date that is a Sunday
    var pastSunday = today;
    while (pastSunday.DayOfWeek != DayOfWeek.Sunday)
      pastSunday = pastSunday.AddDays(-1);

    var row = ValidRow();
    row.MeetingDate = pastSunday;

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("יום המנוחה"));
  }

  [Fact]
  public async Task ValidateRowAsync_DuplicateRowBothEmptyNotes_ReturnsError()
  {
    // Arrange
    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var row = ValidRow();
    row.Notes = null;

    var existingDuplicate = ValidRow();
    existingDuplicate.Id = 99; // different object
    existingDuplicate.Notes = null;

    var allRows = new List<ReportRow> { existingDuplicate };

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, allRows);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("שורה כפולה") && e.Contains("הערות"));
  }

  [Fact]
  public async Task ValidateRowAsync_DuplicateRowIdenticalNotes_ReturnsError()
  {
    // Arrange
    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var row = ValidRow();
    row.Notes = "הערה זהה";

    var existingDuplicate = ValidRow();
    existingDuplicate.Id = 99;
    existingDuplicate.Notes = "הערה זהה";

    var allRows = new List<ReportRow> { existingDuplicate };

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, allRows);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("שורה כפולה") && e.Contains("זהות"));
  }

  [Fact]
  public async Task ValidateRowAsync_SimilarNotes_NoLongerBlocked()
  {
    // Arrange — add threshold constant to DB
    _db.SystemConstants.Add(new SystemConstant
    {
      Key = "NotesSimilarityThresholdPercent",
      Value = "90",
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    var employee = DefaultEmployee();
    var month = DefaultMonth();

    // Create a row with very similar notes (>90% similarity)
    var baseNote = "פגישה עם מורים בנושא תכנית הלימודים החדשה";
    var similarNote = "פגישה עם מורים בנושא תכנית הלימודים החדשה!"; // 1 char difference

    var row = ValidRow();
    row.Notes = similarNote;

    // Other row has different subject so it's NOT a pure duplicate, but notes are similar
    var otherRow = ValidRow();
    otherRow.Id = 55;
    otherRow.Subject1Id = 2; // different subject — no duplicate detection
    otherRow.Notes = baseNote;

    var allRows = new List<ReportRow> { otherRow };

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, allRows);

    // Assert — the notes-similarity check was disabled in v1.2.11 (client request),
    // so similar notes no longer block the row.
    result.IsValid.Should().BeTrue();
    result.Errors.Should().NotContain(e => e.Contains("דמיון"));
  }

  [Fact]
  public async Task ValidateRowAsync_ValidRow_PassesValidation()
  {
    // Arrange
    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var row = ValidRow();

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

    // Assert
    result.IsValid.Should().BeTrue();
    result.Errors.Should().BeEmpty();
  }

  [Fact]
  public async Task ValidateRowAsync_FutureDateWithoutPermission_ReturnsError()
  {
    // Arrange
    var employee = DefaultEmployee();
    employee.AllowFutureReporting = false;
    var month = DefaultMonth();
    month.AllowFutureReporting = false;

    var row = ValidRow();
    row.MeetingDate = DateTime.Today.AddDays(5); // future date

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("עתידי"));
  }

  [Fact]
  public async Task ValidateRowAsync_DateAfterReportingMonth_ReturnsError()
  {
    // Arrange
    var employee = DefaultEmployee();
    employee.AllowFutureReporting = true;
    var month = DefaultMonth();
    month.Month = 3;
    month.Year = 2026;
    month.AllowFutureReporting = true;

    var row = ValidRow();
    row.MeetingDate = new DateTime(2026, 4, 1);

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("חודש הדיווח"));
  }

  [Fact]
  public async Task ValidateRowAsync_MonthlyDurationAboveAllocationScope_ReturnsError()
  {
    // Arrange
    var allocation = await _db.Allocations.FindAsync(1);
    allocation!.MonthlyEmploymentScope = 3;
    await _db.SaveChangesAsync();

    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var existing = ValidRow();
    existing.Id = 55;
    existing.MeetingDuration = 2;
    existing.Subject1Id = 2;

    var row = ValidRow();
    row.MeetingDuration = 2;

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow> { existing, row });

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("היקף פעילות חודשי"));
  }

  [Fact]
  public async Task ValidateRowAsync_NullDailyScopeTreatsAllocationAsUnlimited()
  {
    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var existing = ValidRow();
    existing.Id = 66;
    existing.MeetingDuration = 12;
    existing.AllocationId = 1;
    existing.Subject1Id = 2;
    var row = ValidRow();
    row.MeetingDuration = 12;
    row.AllocationId = 1;

    var allocation = await _db.Allocations.FindAsync(1);
    allocation!.DailyEmploymentScope = null;
    await _db.SaveChangesAsync();

    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow> { existing, row });

    result.Errors.Should().NotContain(e => e.Contains("יומי"));
  }

  [Fact]
  public async Task ValidateRowAsync_OutputDurationOutsideAllocationOptions_ReturnsError()
  {
    var allocation = await _db.Allocations.FindAsync(1);
    allocation!.OutputDuration = "0.5,1,1.5";
    await _db.SaveChangesAsync();

    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var row = ValidRow();
    row.MeetingDuration = 2;

    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow> { row });

    result.Errors.Should().Contain(e => e.Contains("משך התפוקה"));
  }

  [Fact]
  public async Task ValidateRowAsync_RequiredFieldsConstant_AllowsDeveloperLevelOptionalField()
  {
    // Arrange
    _db.SystemConstants.Add(new SystemConstant
    {
      Key = "RequiredReportFields",
      Value = "AllocationId,MeetingDate,MeetingDuration",
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var row = ValidRow();
    row.DistrictId = 0;
    row.LocalityId = 0;
    row.FrameworkId = 0;
    row.EducationalProgramId = 0;
    row.DomainId = 0;
    row.Subject1Id = 0;

    // Act
    var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

    // Assert
    result.Errors.Should().NotContain(e => e.Contains("מחוז"));
    result.Errors.Should().NotContain(e => e.Contains("ישוב"));
  }

  [Fact]
  public async Task ValidateSubmitAsync_PastDeadline_ReturnsError()
  {
    // Arrange
    var employee = DefaultEmployee();
    var month = DefaultMonth();
    month.LastReportingDate = DateTime.Today.AddDays(-1); // deadline passed

    var report = new Report
    {
      Id = 1,
      UserId = employee.Id,
      ReportingMonthId = month.Id,
      StatusId = 2,
      CreatedAt = DateTime.UtcNow
    };

    // Act
    var result = await _sut.ValidateSubmitAsync(report, employee, month);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("תאריך הגשת הדיווח עבר"));
  }

  [Fact]
  public async Task ValidateSubmitAsync_MonthlyDurationAboveAllocationScope_ReturnsError()
  {
    // Arrange
    var employee = DefaultEmployee();
    var month = DefaultMonth();
    var allocation = await _db.Allocations.FindAsync(1);
    allocation!.MonthlyEmploymentScope = 3;

    var report = new Report
    {
      Id = 77,
      UserId = employee.Id,
      ReportingMonthId = month.Id,
      StatusId = 2,
      CreatedAt = DateTime.UtcNow
    };
    _db.Reports.Add(report);
    _db.ReportRows.AddRange(
      WithReport(ValidRow(), report.Id, 1, 2),
      WithReport(ValidRow(), report.Id, 2, 2));
    await _db.SaveChangesAsync();

    // Act
    var result = await _sut.ValidateSubmitAsync(report, employee, month);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.Contains("היקף פעילות חודשי"));

    static ReportRow WithReport(ReportRow row, int reportId, int sequence, decimal duration)
    {
      row.ReportId = reportId;
      row.SequenceNumber = sequence;
      row.MeetingDuration = duration;
      row.Subject1Id = sequence;
      return row;
    }
  }

  [Fact]
  public async Task ValidateSubmitAsync_WithinDeadline_IsValid()
  {
    // Arrange
    var employee = DefaultEmployee();
    var month = DefaultMonth(); // deadline is today+10

    var report = new Report
    {
      Id = 42,
      UserId = employee.Id,
      ReportingMonthId = month.Id,
      StatusId = 2,
      CreatedAt = DateTime.UtcNow
    };

    // Act
    var result = await _sut.ValidateSubmitAsync(report, employee, month);

    // Assert — no deadline error (may have warnings but should be valid)
    result.Errors.Should().NotContain(e => e.Contains("תאריך הגשת הדיווח עבר"));
  }
}

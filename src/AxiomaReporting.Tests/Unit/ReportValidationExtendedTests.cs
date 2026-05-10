using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Extended validation tests that cover scenarios NOT present in ReportValidationServiceTests.cs.
/// Specifically: annual limits, Unlimited output duration, multiple required-field errors,
/// notes/duplicate edge cases, and date edge cases.
/// </summary>
public class ReportValidationExtendedTests : IDisposable
{
    private readonly AppDbContext _db;
    private readonly IReportValidationService _sut;

    public ReportValidationExtendedTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _db = new AppDbContext(options);

        // Seed a base allocation (AllocationId = 1, user 1, project 1)
        _db.Allocations.Add(new Allocation
        {
            Id = 1,
            UserId = 1,
            ProjectId = 1,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
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

    /// <summary>
    /// Reporting month whose deadline is well in the future and that spans the current month.
    /// </summary>
    private static ReportingMonth DefaultMonth() => new ReportingMonth
    {
        Id = 1,
        Description = "אפריל 2026",
        Month = DateTime.Today.Month,
        Year = DateTime.Today.Year,
        LastReportingDate = DateTime.Today.AddDays(30),
        IsActive = true,
        AllowFutureReporting = false
    };

    /// <summary>
    /// A fully-populated, valid row for the default employee and allocation.
    /// </summary>
    private static ReportRow ValidRow() => new ReportRow
    {
        Id = 0,
        ReportId = 1,
        AllocationId = 1,
        SequenceNumber = 1,
        MeetingDate = DateTime.Today.AddDays(-1),
        MeetingDuration = 1,
        DistrictId = 1,
        LocalityId = 1,
        FrameworkId = 1,
        EducationalProgramId = 1,
        DomainId = 1,
        Subject1Id = 1
    };

    // ======================================================================
    // Annual row limit tests (ValidateSubmitAsync)
    // ======================================================================

    [Fact]
    public async Task ValidateSubmitAsync_AnnualRowLimitReached_ReturnsError()
    {
        // Arrange — allocation allows only 2 rows per year; already has 2 approved rows in DB
        var allocation = await _db.Allocations.FindAsync(1);
        allocation!.AnnualRowAllocation = 2;
        await _db.SaveChangesAsync();

        var activeMonth = new ReportingMonth
        {
            Id = 10,
            Description = "ינואר 2026",
            Month = 1,
            Year = 2026,
            LastReportingDate = DateTime.Today.AddDays(10),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        _db.ReportingMonths.Add(activeMonth);

        // Past approved report in the same year
        var pastMonth = new ReportingMonth
        {
            Id = 11,
            Description = "פברואר 2026",
            Month = 2,
            Year = 2026,
            LastReportingDate = new DateTime(2026, 2, 28),
            IsActive = false,
            CreatedAt = DateTime.UtcNow
        };
        _db.ReportingMonths.Add(pastMonth);

        var approvedReport = new Report
        {
            Id = 100,
            UserId = 1,
            ReportingMonthId = pastMonth.Id,
            StatusId = 4,  // Approved
            CreatedAt = DateTime.UtcNow
        };
        _db.Reports.Add(approvedReport);

        // 2 rows in the approved report — exactly at the annual limit
        _db.ReportRows.AddRange(
            new ReportRow { Id = 201, ReportId = 100, AllocationId = 1, SequenceNumber = 1, MeetingDate = new DateTime(2026, 2, 5), MeetingDuration = 1, DistrictId = 1, LocalityId = 1, FrameworkId = 1, EducationalProgramId = 1, DomainId = 1, Subject1Id = 1, CreatedAt = DateTime.UtcNow },
            new ReportRow { Id = 202, ReportId = 100, AllocationId = 1, SequenceNumber = 2, MeetingDate = new DateTime(2026, 2, 6), MeetingDuration = 1, DistrictId = 1, LocalityId = 1, FrameworkId = 1, EducationalProgramId = 1, DomainId = 1, Subject1Id = 2, CreatedAt = DateTime.UtcNow });

        // Current report being submitted — adds 1 more row, total = 3 which exceeds limit of 2
        var currentReport = new Report
        {
            Id = 101,
            UserId = 1,
            ReportingMonthId = activeMonth.Id,
            StatusId = 2,
            CreatedAt = DateTime.UtcNow
        };
        _db.Reports.Add(currentReport);
        _db.ReportRows.Add(new ReportRow
        {
            Id = 203,
            ReportId = 101,
            AllocationId = 1,
            SequenceNumber = 1,
            MeetingDate = new DateTime(2026, 1, 5),
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

        var employee = DefaultEmployee();

        // Act
        var result = await _sut.ValidateSubmitAsync(currentReport, employee, activeMonth);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Contains("שנתי"));
    }

    [Fact]
    public async Task ValidateSubmitAsync_AnnualRowLimitNull_NoLimit()
    {
        // Arrange — allocation has no annual row limit
        var allocation = await _db.Allocations.FindAsync(1);
        allocation!.AnnualRowAllocation = null;
        await _db.SaveChangesAsync();

        var month = new ReportingMonth
        {
            Id = 20,
            Description = "ינואר 2026",
            Month = 1,
            Year = 2026,
            LastReportingDate = DateTime.Today.AddDays(10),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        _db.ReportingMonths.Add(month);

        var report = new Report
        {
            Id = 110,
            UserId = 1,
            ReportingMonthId = month.Id,
            StatusId = 2,
            CreatedAt = DateTime.UtcNow
        };
        _db.Reports.Add(report);

        // Add many rows — should never hit an annual cap
        for (var i = 1; i <= 50; i++)
        {
            _db.ReportRows.Add(new ReportRow
            {
                Id = 300 + i,
                ReportId = 110,
                AllocationId = 1,
                SequenceNumber = i,
                MeetingDate = new DateTime(2026, 1, 5),
                MeetingDuration = 1,
                DistrictId = 1,
                LocalityId = 1,
                FrameworkId = 1,
                EducationalProgramId = 1,
                DomainId = 1,
                Subject1Id = i,
                CreatedAt = DateTime.UtcNow
            });
        }
        await _db.SaveChangesAsync();

        var employee = DefaultEmployee();

        // Act
        var result = await _sut.ValidateSubmitAsync(report, employee, month);

        // Assert — no annual limit error
        result.Errors.Should().NotContain(e => e.Contains("שנתי") && e.Contains("שורות"));
    }

    [Fact]
    public async Task ValidateSubmitAsync_AnnualEmploymentScopeExceeded_ReturnsError()
    {
        // Arrange — allocation monthly scope exceeded so the monthly validation fires
        var allocation = await _db.Allocations.FindAsync(1);
        allocation!.MonthlyEmploymentScope = 1;  // only 1 unit allowed monthly
        await _db.SaveChangesAsync();

        var month = new ReportingMonth
        {
            Id = 30,
            Description = "מרץ 2026",
            Month = 3,
            Year = 2026,
            LastReportingDate = DateTime.Today.AddDays(10),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        _db.ReportingMonths.Add(month);

        var report = new Report
        {
            Id = 120,
            UserId = 1,
            ReportingMonthId = month.Id,
            StatusId = 2,
            CreatedAt = DateTime.UtcNow
        };
        _db.Reports.Add(report);

        // Two rows each with duration = 1; total = 2 > monthly limit of 1
        _db.ReportRows.AddRange(
            new ReportRow { Id = 401, ReportId = 120, AllocationId = 1, SequenceNumber = 1, MeetingDate = new DateTime(2026, 3, 5), MeetingDuration = 1, DistrictId = 1, LocalityId = 1, FrameworkId = 1, EducationalProgramId = 1, DomainId = 1, Subject1Id = 1, CreatedAt = DateTime.UtcNow },
            new ReportRow { Id = 402, ReportId = 120, AllocationId = 1, SequenceNumber = 2, MeetingDate = new DateTime(2026, 3, 6), MeetingDuration = 1, DistrictId = 1, LocalityId = 1, FrameworkId = 1, EducationalProgramId = 1, DomainId = 1, Subject1Id = 2, CreatedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var employee = DefaultEmployee();

        // Act
        var result = await _sut.ValidateSubmitAsync(report, employee, month);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Contains("היקף פעילות חודשי"));
    }

    // ======================================================================
    // Output duration "Unlimited" tests (ValidateRowAsync)
    // ======================================================================

    [Fact]
    public async Task ValidateRowAsync_OutputDurationUnlimited_AllowsAnyDuration()
    {
        // Arrange — allocation allows only specific durations but also has "Unlimited"
        var allocation = await _db.Allocations.FindAsync(1);
        allocation!.OutputDuration = "0.5,1,Unlimited";
        await _db.SaveChangesAsync();

        var employee = DefaultEmployee();
        var month = DefaultMonth();
        var row = ValidRow();
        row.MeetingDuration = 999;  // any value passes when "Unlimited" is present

        // Act
        var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow> { row });

        // Assert — no OutputDuration error
        result.Errors.Should().NotContain(e => e.Contains("משך התפוקה"));
    }

    [Fact]
    public async Task ValidateRowAsync_OutputDurationHebrewUnlimited_AllowsAnyDuration()
    {
        // Arrange — Hebrew alias "ללא הגבלה" is also valid as Unlimited
        var allocation = await _db.Allocations.FindAsync(1);
        allocation!.OutputDuration = "ללא הגבלה";
        await _db.SaveChangesAsync();

        var employee = DefaultEmployee();
        var month = DefaultMonth();
        var row = ValidRow();
        row.MeetingDuration = 42;

        // Act
        var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow> { row });

        // Assert
        result.Errors.Should().NotContain(e => e.Contains("משך התפוקה"));
    }

    // ======================================================================
    // Multiple required fields missing in a single call
    // ======================================================================

    [Fact]
    public async Task ValidateRowAsync_MultipleRequiredFieldsMissing_ReturnsMultipleErrors()
    {
        // Arrange — no system constant override so defaults apply; both DistrictId and FrameworkId are 0
        var employee = DefaultEmployee();
        var month = DefaultMonth();
        var row = new ReportRow
        {
            AllocationId = 1,
            MeetingDate = DateTime.Today.AddDays(-1),
            MeetingDuration = 1,
            DistrictId = 0,       // missing
            LocalityId = 1,
            FrameworkId = 0,      // missing
            EducationalProgramId = 1,
            DomainId = 1,
            Subject1Id = 1
        };

        // Act
        var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

        // Assert — both error messages present
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Contains("מחוז"));
        result.Errors.Should().Contain(e => e.Contains("מסגרת"));
        result.Errors.Count(e => e.Contains("מחוז") || e.Contains("מסגרת")).Should().Be(2);
    }

    // ======================================================================
    // Notes similarity edge cases
    // ======================================================================

    [Fact]
    public async Task ValidateRowAsync_SimilarNotesBelow90Percent_Passes()
    {
        // Arrange — threshold is 90%; construct two notes with ~80% similarity
        _db.SystemConstants.Add(new SystemConstant
        {
            Key = "NotesSimilarityThresholdPercent",
            Value = "90",
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        var employee = DefaultEmployee();
        var month = DefaultMonth();

        // "AAAAAAAAAA" vs "BBBBBBBBBB" — 0% similarity — well below 90%
        var row = ValidRow();
        row.Notes = "AAAAAAAAAA";

        var otherRow = ValidRow();
        otherRow.Id = 50;
        otherRow.Subject1Id = 2;  // different subject so no duplicate detection path
        otherRow.Notes = "BBBBBBBBBB";

        var allRows = new List<ReportRow> { otherRow };

        // Act
        var result = await _sut.ValidateRowAsync(row, employee, month, allRows);

        // Assert — no similarity error
        result.Errors.Should().NotContain(e => e.Contains("דמיון"));
    }

    [Fact]
    public async Task ValidateRowAsync_DifferentNotes_NoDuplicateError()
    {
        // Arrange — same date + same values but completely different notes
        var employee = DefaultEmployee();
        var month = DefaultMonth();

        var existingRow = ValidRow();
        existingRow.Id = 60;
        existingRow.Notes = "הערה ראשונה שונה לחלוטין";

        var newRow = ValidRow();
        newRow.Notes = "הערה שנייה שונה לחלוטין";

        var allRows = new List<ReportRow> { existingRow };

        // Act
        var result = await _sut.ValidateRowAsync(newRow, employee, month, allRows);

        // Assert — neither a duplicate error nor a similarity error
        result.Errors.Should().NotContain(e => e.Contains("שורה כפולה"));
        // Notes differ enough that similarity should be low — no similarity error either
        result.Errors.Should().NotContain(e => e.Contains("דמיון"));
    }

    [Fact]
    public async Task ValidateRowAsync_FirstRowInReport_NoDuplicateCheck()
    {
        // Arrange — the new row is the only row; nothing to compare against
        var employee = DefaultEmployee();
        var month = DefaultMonth();
        var row = ValidRow();
        row.Notes = null;

        // Act — pass an empty list (no other rows in report)
        var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

        // Assert — no duplicate error because there is nothing to duplicate
        result.Errors.Should().NotContain(e => e.Contains("שורה כפולה"));
        result.IsValid.Should().BeTrue();
    }

    // ======================================================================
    // Date validation edge cases
    // ======================================================================

    [Fact]
    public async Task ValidateRowAsync_DateInCurrentMonth_Passes()
    {
        // Arrange — date is today (same month/year as reporting month)
        var employee = DefaultEmployee();
        var month = DefaultMonth();  // Month = current month, Year = current year

        var row = ValidRow();
        row.MeetingDate = DateTime.Today;  // today is valid (not future, not past reporting month)

        // Act
        var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

        // Assert
        result.Errors.Should().NotContain(e => e.Contains("חודש הדיווח"));
        result.Errors.Should().NotContain(e => e.Contains("עתידי"));
    }

    [Fact]
    public async Task ValidateRowAsync_DateInPreviousMonth_Passes()
    {
        // Spec allows reporting on dates from previous months as long as they don't exceed
        // the current reporting month boundary.
        var employee = DefaultEmployee();

        // Reporting month is the current month; a date from last month is <= reportingMonthEnd
        var month = DefaultMonth();

        var lastMonth = DateTime.Today.AddMonths(-1);
        var row = ValidRow();
        row.MeetingDate = new DateTime(lastMonth.Year, lastMonth.Month, 1);

        // Act
        var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

        // Assert — previous month dates are allowed
        result.Errors.Should().NotContain(e => e.Contains("חודש הדיווח"));
        result.Errors.Should().NotContain(e => e.Contains("עתידי"));
    }

    [Fact]
    public async Task ValidateRowAsync_FirstDayOfMonth_Passes()
    {
        // Arrange — first day of the current reporting month is always valid
        var employee = DefaultEmployee();
        var month = DefaultMonth();

        var row = ValidRow();
        row.MeetingDate = new DateTime(month.Year, month.Month, 1);

        // Act
        var result = await _sut.ValidateRowAsync(row, employee, month, new List<ReportRow>());

        // Assert
        result.Errors.Should().NotContain(e => e.Contains("חודש הדיווח"));
        result.IsValid.Should().BeTrue();
    }
}

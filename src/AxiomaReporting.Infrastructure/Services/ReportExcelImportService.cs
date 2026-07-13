using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class ExcelImportResult
{
  public bool Success => Errors.Count == 0;
  public int ImportedRows { get; set; }
  public List<string> Errors { get; } = new();
}

public interface IReportExcelImportService
{
  Task<ExcelImportResult> ImportAsync(int reportId, int allocationId, Stream stream, int currentUserId);
}

public class ReportExcelImportService : IReportExcelImportService
{
  private readonly AppDbContext _db;
  private readonly IReportValidationService _validator;
  private readonly IReportStatusService _statusService;
  private readonly AxiomaReporting.Core.Interfaces.IEmailService _emailService;
  private readonly ILookupResolver _lookupResolver;

  public ReportExcelImportService(
    AppDbContext db,
    IReportValidationService validator,
    IReportStatusService statusService,
    AxiomaReporting.Core.Interfaces.IEmailService emailService,
    ILookupResolver? lookupResolver = null)
  {
    _db = db;
    _validator = validator;
    _statusService = statusService;
    _emailService = emailService;
    _lookupResolver = lookupResolver ?? new LookupResolver(db);
  }

  public async Task<ExcelImportResult> ImportAsync(int reportId, int allocationId, Stream stream, int currentUserId)
  {
    var result = new ExcelImportResult();
    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report?.User == null || report.ReportingMonth == null)
    {
      result.Errors.Add("הדיווח לא נמצא");
      return result;
    }

    if (report.StatusId is 3 or 4)
    {
      result.Errors.Add("לא ניתן לייבא אקסל לדיווח שממתין לאישור או אושר");
      return result;
    }

    var allocation = await _db.Allocations.FirstOrDefaultAsync(a =>
      a.Id == allocationId && a.UserId == report.UserId && a.IsActive);
    if (allocation == null || !allocation.AllowExcelUpload)
    {
      result.Errors.Add("הקצאה זו אינה מאפשרת העלאת אקסל");
      return result;
    }

    using var workbook = new XLWorkbook(stream);
    var ws = workbook.Worksheets.FirstOrDefault();
    if (ws == null)
    {
      result.Errors.Add("קובץ האקסל אינו מכיל גיליון");
      return result;
    }

    var importedRows = new List<ReportRow>();
    var lastRow = ws.LastRowUsed()?.RowNumber() ?? 0;
    var clientHebrewHeaderRow = FindClientHebrewHeaderRow(ws);
    var clientHebrewFormat = clientHebrewHeaderRow > 0;
    var firstDataRow = clientHebrewFormat ? clientHebrewHeaderRow + 1 : 2;
    for (var rowNumber = firstDataRow; rowNumber <= lastRow; rowNumber++)
    {
      try
      {
        var excelRow = ws.Row(rowNumber);
        if (IsEmptyDataRow(excelRow, clientHebrewFormat)) continue;
        if (clientHebrewFormat && IsClientHebrewHeaderRow(excelRow)) continue;

        if (clientHebrewFormat && !IsRowForReportEmployee(excelRow, report.User))
        {
          var uploadedEmployeeCode = excelRow.Cell(2).GetString();
          result.Errors.Add($"שורה {rowNumber}: קוד העובד בקובץ ({uploadedEmployeeCode}) אינו תואם לעובד שעבורו הועלה הדיווח ({report.User.EmployeeCode})");
          continue;
        }

        var row = clientHebrewFormat
          ? await ParseClientHebrewRowAsync(excelRow, reportId, allocationId)
          : await ParseTemplateRowAsync(excelRow, reportId, allocationId);
        var allRows = importedRows.Concat(new[] { row }).ToList();
        var validation = await _validator.ValidateRowAsync(row, report.User, report.ReportingMonth, allRows);
        if (!validation.IsValid)
        {
          result.Errors.Add($"שורה {rowNumber}: {string.Join("; ", validation.Errors)}");
          continue;
        }
        importedRows.Add(row);
      }
      catch (Exception ex)
      {
        result.Errors.Add($"שורה {rowNumber}: {ex.Message}");
      }
    }

    if (result.Errors.Any()) return result;

    var existingRows = await _db.ReportRows
      .Where(r => r.ReportId == reportId && r.AllocationId == allocationId)
      .ToListAsync();
    _db.ReportRows.RemoveRange(existingRows);

    var nextSeq = (await _db.ReportRows
      .Where(r => r.ReportId == reportId && r.AllocationId != allocationId)
      .MaxAsync(r => (int?)r.SequenceNumber) ?? 0) + 1;

    foreach (var row in importedRows)
    {
      row.SequenceNumber = nextSeq++;
      row.CreatedAt = DateTime.UtcNow;
      _db.ReportRows.Add(row);
    }

    report.ImportedFromExcel = true;
    report.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();
    await _statusService.SaveDraftAsync(reportId);
    await SendImportSuccessEmailAsync(report);

    result.ImportedRows = importedRows.Count;
    return result;
  }

  private async Task SendImportSuccessEmailAsync(Report report)
  {
    if (report.User == null || report.ReportingMonth == null || string.IsNullOrWhiteSpace(report.User.Email))
      return;

    await _emailService.SendAsync(
      report.User.Email,
      $"{report.User.FirstName} {report.User.LastName}",
      "ReportReceived",
      new Dictionary<string, string>
      {
        ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}",
        ["MonthName"] = report.ReportingMonth.Description,
        ["Month"] = report.ReportingMonth.Month.ToString(),
        ["Year"] = report.ReportingMonth.Year.ToString(),
        ["DeadlineDate"] = report.ReportingMonth.LastReportingDate.ToString("dd/MM/yyyy"),
        ["Deadline"] = report.ReportingMonth.LastReportingDate.ToString("dd/MM/yyyy")
      });
  }

  private static int FindClientHebrewHeaderRow(IXLWorksheet ws)
  {
    var lastRow = Math.Min(15, ws.LastRowUsed()?.RowNumber() ?? 0);
    for (var rowNumber = 1; rowNumber <= lastRow; rowNumber++)
    {
      if (IsClientHebrewHeaderRow(ws.Row(rowNumber)))
        return rowNumber;
    }

    return 0;
  }

  private static bool IsClientHebrewHeaderRow(IXLRow row)
  {
    var lastColumn = Math.Min(25, row.LastCellUsed()?.Address.ColumnNumber ?? 25);
    var rowText = NormalizeHebrewHeader(string.Join("|", Enumerable.Range(1, lastColumn)
      .Select(c => row.Cell(c).GetString())));

    if (string.IsNullOrWhiteSpace(rowText)) return false;

    var score = 0;
    if (rowText.Contains("קוד עובד")) score++;
    if (rowText.Contains("מחוז")) score++;
    if (rowText.Contains("תאריך")) score++;
    if (rowText.Contains("משך")) score++;
    if (rowText.Contains("יישוב") || rowText.Contains("ישוב")) score++;
    if (rowText.Contains("מסגרת")) score++;
    if (rowText.Contains("תוכנית חינוכית") || rowText.Contains("תכנית חינוכית")) score++;
    if (rowText.Contains("תחום")) score++;
    if (rowText.Contains("נושא")) score++;

    // "קוד עובד" is what distinguishes the multi-employee batch format from the
    // personal upload template (whose headers are Hebrew too since client fix
    // 07/2026 #3, but per-row without an employee-code column).
    return score >= 4 && rowText.Contains("קוד עובד") && rowText.Contains("מחוז") && rowText.Contains("תאריך");
  }

  private static string NormalizeHebrewHeader(string? value)
  {
    if (string.IsNullOrWhiteSpace(value)) return string.Empty;
    return value
      .Replace('\u00a0', ' ')
      .Replace("\"", string.Empty)
      .Trim()
      .ToLowerInvariant();
  }

  private static bool IsEmptyDataRow(IXLRow row, bool clientHebrewFormat)
  {
    var columns = clientHebrewFormat ? Enumerable.Range(2, 18) : Enumerable.Range(1, 16);
    return columns.All(c => row.Cell(c).IsEmpty() || string.IsNullOrWhiteSpace(row.Cell(c).GetString()));
  }

  private static bool IsRowForReportEmployee(IXLRow row, User employee)
  {
    var employeeCode = row.Cell(2).GetString().Trim();
    if (string.IsNullOrWhiteSpace(employeeCode)) return true;
    return string.Equals(employeeCode, employee.EmployeeCode, StringComparison.OrdinalIgnoreCase) ||
           string.Equals(employeeCode, employee.IdNumber, StringComparison.OrdinalIgnoreCase);
  }

  // הפורמט האישי (התבנית שהעובד מוריד): 16 עמודות לפי מיקום. מקבל גם ערכים
  // בעברית (תיאור מהטבלאות) וגם מזהים מספריים — תיקון לקוח 07/2026 #4
  // ("לא קלט את עמודת המחוז").
  private async Task<ReportRow> ParseTemplateRowAsync(IXLRow row, int reportId, int allocationId)
  {
    return new ReportRow
    {
      ReportId = reportId,
      AllocationId = allocationId,
      MeetingDate = ReadDate(row, 1),
      MeetingDuration = ReadDecimal(row, 2),
      DistrictId = await ResolveRequiredAsync(row, 3, "מחוז", _lookupResolver.ResolveDistrictAsync),
      LocalityId = await ResolveRequiredAsync(row, 4, "יישוב", _lookupResolver.ResolveLocalityAsync),
      FrameworkId = await ResolveRequiredFrameworkAsync(row, 5),
      EducationalProgramId = await ResolveRequiredAsync(row, 6, "תוכנית חינוכית", _lookupResolver.ResolveEducationalProgramAsync),
      DomainId = await ResolveRequiredAsync(row, 7, "תחום", _lookupResolver.ResolveDomainAsync),
      Subject1Id = await ResolveRequiredAsync(row, 8, "נושא 1", _lookupResolver.ResolveSubjectAsync),
      Subject2Id = await ResolveOptionalAsync(row, 9, _lookupResolver.ResolveSubjectAsync),
      DiscussionCodeId = await ResolveOptionalAsync(row, 10, _lookupResolver.ResolveDiscussionCodeAsync),
      ConclusionClassId = await ResolveOptionalAsync(row, 11, _lookupResolver.ResolveClassConclusionAsync),
      ConclusionFrameworkId = await ResolveOptionalAsync(row, 12, _lookupResolver.ResolveFrameworkConclusionAsync),
      ConclusionLocationId = await ResolveOptionalAsync(row, 13, _lookupResolver.ResolveLocalityDistrictNationalAsync),
      GradeLevelId = await ResolveOptionalAsync(row, 14, _lookupResolver.ResolveGradeLevelAsync),
      ClassId = await ResolveOptionalAsync(row, 15, _lookupResolver.ResolveClassAsync),
      Notes = row.Cell(16).GetString()
    };
  }

  private async Task<ReportRow> ParseClientHebrewRowAsync(IXLRow row, int reportId, int allocationId)
  {
    return new ReportRow
    {
      ReportId = reportId,
      AllocationId = allocationId,
      MeetingDate = ReadDate(row, 7),
      MeetingDuration = ReadDecimal(row, 8),
      DistrictId = await ResolveRequiredAsync(row, 4, "מחוז", _lookupResolver.ResolveDistrictAsync),
      LocalityId = await ResolveRequiredAsync(row, 5, "יישוב", _lookupResolver.ResolveLocalityAsync),
      FrameworkId = await ResolveRequiredFrameworkAsync(row, 6),
      EducationalProgramId = await ResolveRequiredAsync(row, 9, "תוכנית חינוכית", _lookupResolver.ResolveEducationalProgramAsync),
      DomainId = await ResolveRequiredAsync(row, 10, "תחום", _lookupResolver.ResolveDomainAsync),
      Subject1Id = await ResolveRequiredAsync(row, 11, "נושא 1", _lookupResolver.ResolveSubjectAsync),
      Subject2Id = await ResolveOptionalAsync(row, 12, _lookupResolver.ResolveSubjectAsync),
      DiscussionCodeId = await ResolveOptionalAsync(row, 13, _lookupResolver.ResolveDiscussionCodeAsync),
      // מסקנות כיתה/מסגרת נפתרות מטבלאות המסקנות הנפרדות — לא מכיתות/מסגרות בפועל.
      ConclusionClassId = await ResolveOptionalAsync(row, 14, _lookupResolver.ResolveClassConclusionAsync),
      ConclusionFrameworkId = await ResolveOptionalAsync(row, 15, _lookupResolver.ResolveFrameworkConclusionAsync),
      ConclusionLocationId = await ResolveOptionalAsync(row, 16, _lookupResolver.ResolveLocalityDistrictNationalAsync),
      GradeLevelId = await ResolveOptionalAsync(row, 17, _lookupResolver.ResolveGradeLevelAsync),
      ClassId = await ResolveOptionalAsync(row, 18, _lookupResolver.ResolveClassAsync),
      Notes = row.Cell(19).GetString()
    };
  }

  private static DateTime ReadDate(IXLRow row, int column)
  {
    var cell = row.Cell(column);
    if (cell.TryGetValue<DateTime>(out var date)) return date.Date;
    throw new InvalidOperationException("MeetingDate אינו תאריך תקין");
  }

  private static decimal ReadDecimal(IXLRow row, int column)
  {
    if (row.Cell(column).TryGetValue<decimal>(out var value)) return value;
    throw new InvalidOperationException("MeetingDuration אינו מספר תקין");
  }

  private static async Task<int> ResolveRequiredAsync(
    IXLRow row,
    int column,
    string name,
    Func<string?, CancellationToken, Task<int?>> resolver)
  {
    var value = row.Cell(column).GetString();
    var id = await resolver(value, default);
    if (id.HasValue && id.Value > 0) return id.Value;
    throw new InvalidOperationException($"{name} לא נמצא בטבלאות המערכת: '{value}'");
  }

  private static async Task<int?> ResolveOptionalAsync(
    IXLRow row,
    int column,
    Func<string?, CancellationToken, Task<int?>> resolver)
  {
    var value = row.Cell(column).GetString();
    if (string.IsNullOrWhiteSpace(value)) return null;
    return await resolver(value, default);
  }

  private async Task<int> ResolveRequiredFrameworkAsync(IXLRow row, int column)
  {
    var value = row.Cell(column).GetString();
    var id = await ResolveFrameworkValueAsync(value);
    if (id.HasValue && id.Value > 0) return id.Value;
    throw new InvalidOperationException($"מסגרת לא נמצאה בטבלאות המערכת: '{value}'");
  }

  private async Task<int?> ResolveFrameworkValueAsync(string? value)
  {
    var id = await _lookupResolver.ResolveFrameworkAsync(value);
    if (id.HasValue || string.IsNullOrWhiteSpace(value)) return id;

    var trimmed = value.Trim();

    // Legacy: a symbol pasted with trailing text ("442889 מקיף ד").
    var leadingSymbol = new string(trimmed.TakeWhile(char.IsDigit).ToArray());
    if (!string.IsNullOrWhiteSpace(leadingSymbol))
    {
      id = await _lookupResolver.ResolveFrameworkAsync(leadingSymbol);
      if (id.HasValue) return id;
    }

    // Composite UI label "יישוב — סמל — שם מסגרת" (the export/dropdown format the
    // client copies from): resolve by the embedded institution symbol.
    var embeddedSymbol = System.Text.RegularExpressions.Regex.Match(trimmed, @"\d{3,}");
    if (embeddedSymbol.Success)
    {
      id = await _lookupResolver.ResolveFrameworkAsync(embeddedSymbol.Value);
      if (id.HasValue) return id;
    }

    // The name segment after the final dash of the composite label.
    var lastSegment = trimmed.Split('—', '-').LastOrDefault()?.Trim();
    if (!string.IsNullOrWhiteSpace(lastSegment) && lastSegment != trimmed)
    {
      id = await _lookupResolver.ResolveFrameworkAsync(lastSegment);
      if (id.HasValue) return id;
    }

    // Backward compatibility with the old numeric template: a plain framework row id.
    return int.TryParse(trimmed, out var frameworkId) &&
           await _db.Frameworks.AnyAsync(f => f.Id == frameworkId)
      ? frameworkId
      : null;
  }
}

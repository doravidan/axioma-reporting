using System.Globalization;
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
    var clientHebrewHeaderMap = clientHebrewFormat
      ? BuildClientHebrewHeaderMap(ws.Row(clientHebrewHeaderRow))
      : new Dictionary<string, int>();
    var firstDataRow = clientHebrewFormat ? clientHebrewHeaderRow + 1 : 2;
    for (var rowNumber = firstDataRow; rowNumber <= lastRow; rowNumber++)
    {
      try
      {
        var excelRow = ws.Row(rowNumber);
        if (IsEmptyDataRow(excelRow, clientHebrewFormat)) continue;
        if (clientHebrewFormat && IsClientHebrewHeaderRow(excelRow)) continue;

        if (clientHebrewFormat && !IsRowForReportEmployee(excelRow, report.User, clientHebrewHeaderMap))
        {
          var uploadedEmployeeCode = excelRow.Cell(GetColumn(clientHebrewHeaderMap, "EmployeeCode", 2)).GetString();
          result.Errors.Add($"שורה {rowNumber}: קוד העובד בקובץ ({uploadedEmployeeCode}) אינו תואם לעובד שעבורו הועלה הדיווח ({report.User.EmployeeCode})");
          continue;
        }

        var row = clientHebrewFormat
          ? await ParseClientHebrewRowAsync(excelRow, reportId, allocationId, clientHebrewHeaderMap)
          : ParseRow(excelRow, reportId, allocationId);
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

    return score >= 4 && rowText.Contains("מחוז") && rowText.Contains("תאריך");
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

  private static Dictionary<string, int> BuildClientHebrewHeaderMap(IXLRow row)
  {
    var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    var lastColumn = Math.Min(40, row.LastCellUsed()?.Address.ColumnNumber ?? 40);
    for (var column = 1; column <= lastColumn; column++)
    {
      var header = NormalizeHebrewHeader(row.Cell(column).GetString());
      if (string.IsNullOrWhiteSpace(header)) continue;

      if (HeaderContains(header, "קוד עובד"))
        SetHeader(map, "EmployeeCode", column);
      else if (HeaderContains(header, "שם המדווח"))
        SetHeader(map, "ReporterName", column);
      else if (HeaderContains(header, "סוג דיווח"))
        SetHeader(map, "ReportType", column);
      else if (HeaderContains(header, "יישוב/מחוז/ארצי") || HeaderContains(header, "ישוב/מחוז/ארצי"))
        SetHeader(map, "ConclusionLocation", column);
      else if (HeaderContains(header, "יישוב") || HeaderContains(header, "ישוב"))
        SetHeader(map, "Locality", column);
      else if (HeaderContains(header, "מחוז"))
        SetHeader(map, "District", column);
      else if (HeaderContains(header, "תאריך"))
        SetHeader(map, "MeetingDate", column);
      else if (HeaderContains(header, "משך"))
        SetHeader(map, "MeetingDuration", column);
      else if (HeaderContains(header, "תוכנית חינוכית") || HeaderContains(header, "תכנית חינוכית"))
        SetHeader(map, "EducationalProgram", column);
      else if (HeaderContains(header, "תחום"))
        SetHeader(map, "Domain", column);
      else if (HeaderContains(header, "נושא 2") || HeaderContains(header, "נושא2"))
        SetHeader(map, "Subject2", column);
      else if (HeaderContains(header, "נושא 1") || HeaderContains(header, "נושא1") || HeaderContains(header, "נושא"))
        SetHeader(map, "Subject1", column);
      else if (HeaderContains(header, "קיום דיון") || HeaderContains(header, "קוד דיון"))
        SetHeader(map, "DiscussionCode", column);
      else if (HeaderContains(header, "מסגרת"))
      {
        // First "מסגרת" column is the framework; a second one is the conclusion framework
        if (!map.ContainsKey("Framework"))
          map["Framework"] = column;
        else
          SetHeader(map, "ConclusionFramework", column);
      }
      else if (HeaderContains(header, "שכבה"))
        SetHeader(map, "GradeLevel", column);
      else if (HeaderContains(header, "כיתה"))
      {
        // First "כיתה" column is the conclusion class; a second one is the class
        if (!map.ContainsKey("ConclusionClass"))
          map["ConclusionClass"] = column;
        else
          SetHeader(map, "Class", column);
      }
      else if (HeaderContains(header, "הערות"))
        SetHeader(map, "Notes", column);
    }

    return map;
  }

  private static void SetHeader(Dictionary<string, int> map, string key, int column)
  {
    if (!map.ContainsKey(key))
      map[key] = column;
  }

  private static bool HeaderContains(string header, string value) =>
    header.Contains(NormalizeHebrewHeader(value), StringComparison.Ordinal);

  private static int GetColumn(Dictionary<string, int> map, string key, int fallbackColumn) =>
    map.TryGetValue(key, out var column) ? column : fallbackColumn;

  private static int? GetOptionalColumn(Dictionary<string, int> map, string key, int fallbackColumn)
  {
    var column = GetColumn(map, key, fallbackColumn);
    return column <= 0 ? null : column;
  }

  private static bool IsEmptyDataRow(IXLRow row, bool clientHebrewFormat)
  {
    var columns = clientHebrewFormat ? Enumerable.Range(2, 18) : Enumerable.Range(1, 16);
    return columns.All(c => row.Cell(c).IsEmpty() || string.IsNullOrWhiteSpace(row.Cell(c).GetString()));
  }

  private static bool IsRowForReportEmployee(IXLRow row, User employee, Dictionary<string, int>? headerMap = null)
  {
    var employeeCodeColumn = headerMap == null ? 2 : GetColumn(headerMap, "EmployeeCode", 2);
    var employeeCode = row.Cell(employeeCodeColumn).GetString().Trim();
    if (string.IsNullOrWhiteSpace(employeeCode)) return true;
    return string.Equals(employeeCode, employee.EmployeeCode, StringComparison.OrdinalIgnoreCase) ||
           string.Equals(employeeCode, employee.IdNumber, StringComparison.OrdinalIgnoreCase);
  }

  private static ReportRow ParseRow(IXLRow row, int reportId, int allocationId)
  {
    return new ReportRow
    {
      ReportId = reportId,
      AllocationId = allocationId,
      MeetingDate = ReadDate(row, 1),
      MeetingDuration = ReadDecimal(row, 2),
      DistrictId = ReadRequiredInt(row, 3, "DistrictId"),
      LocalityId = ReadRequiredInt(row, 4, "LocalityId"),
      FrameworkId = ReadRequiredInt(row, 5, "FrameworkId"),
      EducationalProgramId = ReadRequiredInt(row, 6, "EducationalProgramId"),
      DomainId = ReadRequiredInt(row, 7, "DomainId"),
      Subject1Id = ReadRequiredInt(row, 8, "Subject1Id"),
      Subject2Id = ReadOptionalInt(row, 9),
      DiscussionCodeId = ReadOptionalInt(row, 10),
      ConclusionClassId = ReadOptionalInt(row, 11),
      ConclusionFrameworkId = ReadOptionalInt(row, 12),
      ConclusionLocationId = ReadOptionalInt(row, 13),
      GradeLevelId = ReadOptionalInt(row, 14),
      ClassId = ReadOptionalInt(row, 15),
      Notes = row.Cell(16).GetString()
    };
  }

  private async Task<ReportRow> ParseClientHebrewRowAsync(
    IXLRow row, int reportId, int allocationId, Dictionary<string, int> headerMap)
  {
    return await ParseClientHebrewRowByHeaderAsync(row, reportId, allocationId, headerMap);
  }

  private async Task<ReportRow> ParseClientHebrewRowByHeaderAsync(
    IXLRow row, int reportId, int allocationId, Dictionary<string, int> headerMap)
  {
    var frameworkColumn = GetColumn(headerMap, "Framework", 6);
    var frameworkText = row.Cell(frameworkColumn).GetString();
    var frameworkId = await ResolveRequiredFrameworkAsync(row, frameworkColumn);
    var location = await ResolveLocationFromFrameworkAsync(frameworkId, frameworkText, allocationId);

    var districtColumn = GetColumn(headerMap, "District", 4);
    var localityColumn = GetColumn(headerMap, "Locality", 5);
    var districtId = await ResolveLookupAsync(row, districtColumn, _lookupResolver.ResolveDistrictAsync);
    var localityId = await ResolveLookupAsync(row, localityColumn, _lookupResolver.ResolveLocalityAsync);

    if (!districtId.HasValue || districtId.Value <= 0) districtId = location.DistrictId;
    if (!localityId.HasValue || localityId.Value <= 0) localityId = location.LocalityId;

    if (!districtId.HasValue || districtId.Value <= 0)
      throw new InvalidOperationException($"מחוז לא נמצא בטבלאות המערכת: '{row.Cell(districtColumn).GetString()}'");
    if (!localityId.HasValue || localityId.Value <= 0)
      throw new InvalidOperationException($"יישוב לא נמצא בטבלאות המערכת: '{row.Cell(localityColumn).GetString()}'");

    return new ReportRow
    {
      ReportId = reportId,
      AllocationId = allocationId,
      MeetingDate = ReadDate(row, GetColumn(headerMap, "MeetingDate", 8)),
      MeetingDuration = ReadDecimal(row, GetColumn(headerMap, "MeetingDuration", 9)),
      DistrictId = districtId.Value,
      LocalityId = localityId.Value,
      FrameworkId = frameworkId,
      EducationalProgramId = await ResolveRequiredAsync(row, GetColumn(headerMap, "EducationalProgram", 9), "תוכנית חינוכית", _lookupResolver.ResolveEducationalProgramAsync),
      DomainId = await ResolveRequiredDomainAsync(row, allocationId, GetColumn(headerMap, "Domain", 10)),
      Subject1Id = await ResolveRequiredAsync(row, GetColumn(headerMap, "Subject1", 11), "נושא 1", _lookupResolver.ResolveSubjectAsync),
      Subject2Id = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "Subject2", 13), _lookupResolver.ResolveSubjectAsync),
      DiscussionCodeId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "DiscussionCode", 14), _lookupResolver.ResolveDiscussionCodeAsync),
      ConclusionClassId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "ConclusionClass", 15), _lookupResolver.ResolveClassAsync),
      ConclusionFrameworkId = await ResolveOptionalFrameworkByColumnAsync(row, GetOptionalColumn(headerMap, "ConclusionFramework", 16)),
      ConclusionLocationId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "ConclusionLocation", 17), _lookupResolver.ResolveLocalityDistrictNationalAsync),
      GradeLevelId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "GradeLevel", 18), _lookupResolver.ResolveGradeLevelAsync),
      ClassId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "Class", 19), _lookupResolver.ResolveClassAsync),
      ReportTypeId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "ReportType", 0), _lookupResolver.ResolveReportTypeAsync),
      Notes = row.Cell(GetColumn(headerMap, "Notes", 20)).GetString()
    };
  }

  private async Task<ReportRow> ParseShiftedClientHebrewRowAsync(IXLRow row, int reportId, int allocationId)
  {
    var frameworkText = row.Cell(3).GetString();
    var frameworkId = await ResolveRequiredFrameworkAsync(row, 3);
    var (districtId, localityId) = await ResolveLocationFromFrameworkAsync(frameworkId, frameworkText, allocationId);

    if (!districtId.HasValue || districtId.Value <= 0)
      throw new InvalidOperationException($"מחוז לא נמצא בטבלאות המערכת עבור המסגרת: '{frameworkText}'");
    if (!localityId.HasValue || localityId.Value <= 0)
      throw new InvalidOperationException($"יישוב לא נמצא בטבלאות המערכת עבור המסגרת: '{frameworkText}'");

    var notes1 = row.Cell(10).GetString().Trim();
    var notes2 = row.Cell(20).GetString().Trim();

    return new ReportRow
    {
      ReportId = reportId,
      AllocationId = allocationId,
      MeetingDate = ReadDate(row, 8),
      MeetingDuration = ReadDecimal(row, 9),
      DistrictId = districtId.Value,
      LocalityId = localityId.Value,
      FrameworkId = frameworkId,
      EducationalProgramId = await ResolveRequiredAsync(row, 7, "תוכנית חינוכית", _lookupResolver.ResolveEducationalProgramAsync),
      DomainId = await ResolveRequiredDomainAsync(row, allocationId, 11),
      Subject1Id = await ResolveRequiredAsync(row, 12, "נושא 1", _lookupResolver.ResolveSubjectAsync),
      Subject2Id = await ResolveOptionalAsync(row, 13, _lookupResolver.ResolveSubjectAsync),
      DiscussionCodeId = await ResolveOptionalAsync(row, 14, _lookupResolver.ResolveDiscussionCodeAsync),
      ConclusionClassId = await ResolveOptionalAsync(row, 15, _lookupResolver.ResolveClassAsync),
      ConclusionFrameworkId = await ResolveOptionalFrameworkAsync(row, 16),
      ConclusionLocationId = await ResolveOptionalAsync(row, 17, _lookupResolver.ResolveLocalityDistrictNationalAsync),
      GradeLevelId = await ResolveOptionalAsync(row, 18, _lookupResolver.ResolveGradeLevelAsync),
      ClassId = await ResolveOptionalAsync(row, 19, _lookupResolver.ResolveClassAsync),
      ReportTypeId = await ResolveOptionalAsync(row, 4, _lookupResolver.ResolveReportTypeAsync),
      Notes = CombineNotes(notes1, notes2)
    };
  }

  private static bool LooksLikeShiftedClientHebrewRow(IXLRow row)
  {
    var dateHeaderText = row.Cell(6).GetString().Trim();
    return !string.IsNullOrWhiteSpace(row.Cell(3).GetString()) &&
           !string.IsNullOrWhiteSpace(row.Cell(7).GetString()) &&
           dateHeaderText.Contains("תאריך", StringComparison.Ordinal) &&
           row.Cell(8).TryGetValue<double>(out _) &&
           row.Cell(9).TryGetValue<decimal>(out _);
  }

  private async Task<(int? DistrictId, int? LocalityId)> ResolveLocationFromFrameworkAsync(
    int frameworkId, string frameworkText, int allocationId)
  {
    var institutionSymbolText = await _db.Frameworks.AsNoTracking()
      .Where(f => f.Id == frameworkId)
      .Select(f => f.InstitutionSymbol)
      .FirstOrDefaultAsync();
    if (string.IsNullOrWhiteSpace(institutionSymbolText))
      institutionSymbolText = FindFirstNumberToken(frameworkText);

    int? districtId = null;
    int? localityId = null;
    if (int.TryParse(institutionSymbolText, out var institutionSymbol))
    {
      var institution = await _db.Institutions.AsNoTracking()
        .Where(i => i.InstitutionSymbol == institutionSymbol)
        .OrderByDescending(i => i.LocalityId.HasValue && i.DistrictId.HasValue)
        .Select(i => new { i.LocalityId, i.DistrictId })
        .FirstOrDefaultAsync();
      localityId = institution?.LocalityId;
      districtId = institution?.DistrictId;
    }

    if (!localityId.HasValue || localityId.Value <= 0)
    {
      var localityText = FindFirstTextToken(frameworkText);
      localityId = await _lookupResolver.ResolveLocalityAsync(localityText);
    }

    if (!districtId.HasValue || districtId.Value <= 0)
    {
      var allocationDistrictIds = await _db.Set<AllocationDistrict>().AsNoTracking()
        .Where(x => x.AllocationId == allocationId)
        .Select(x => x.DistrictId)
        .Distinct()
        .ToListAsync();
      if (allocationDistrictIds.Count == 1)
        districtId = allocationDistrictIds[0];
    }

    if (!localityId.HasValue || localityId.Value <= 0)
    {
      var allocationLocalityIds = await _db.Set<AllocationLocality>().AsNoTracking()
        .Where(x => x.AllocationId == allocationId)
        .Select(x => x.LocalityId)
        .Distinct()
        .ToListAsync();
      if (allocationLocalityIds.Count == 1)
        localityId = allocationLocalityIds[0];
    }

    return (districtId, localityId);
  }

  private static DateTime ReadDate(IXLRow row, int column)
  {
    var cell = row.Cell(column);
    if (cell.TryGetValue<DateTime>(out var date)) return date.Date;

    // Excel serial date (OADate); upper bound is 31/12/9999
    if (cell.TryGetValue<double>(out var oaDate) && oaDate > 0 && oaDate < 2958466)
      return DateTime.FromOADate(oaDate).Date;

    var text = cell.GetString().Trim();
    var formats = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd/MM/yy", "d/M/yy", "yyyy-MM-dd", "dd.MM.yyyy" };
    if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
      return date.Date;
    if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
      return date.Date;

    throw new InvalidOperationException("MeetingDate אינו תאריך תקין");
  }

  private static decimal ReadDecimal(IXLRow row, int column)
  {
    if (row.Cell(column).TryGetValue<decimal>(out var value)) return value;
    throw new InvalidOperationException("MeetingDuration אינו מספר תקין");
  }

  private static int ReadRequiredInt(IXLRow row, int column, string name)
  {
    if (row.Cell(column).TryGetValue<int>(out var value) && value > 0) return value;
    throw new InvalidOperationException($"{name} חסר או לא תקין");
  }

  private static int? ReadOptionalInt(IXLRow row, int column)
  {
    var cell = row.Cell(column);
    if (cell.IsEmpty()) return null;
    return cell.TryGetValue<int>(out var value) && value > 0 ? value : null;
  }

  private static async Task<int> ResolveRequiredAsync(
    IXLRow row,
    int column,
    string name,
    Func<string?, CancellationToken, Task<int?>> resolver)
  {
    var value = row.Cell(column).GetString();
    var id = await resolver(value, default);
    if ((!id.HasValue || id.Value <= 0) && TryGetClientHebrewFallbackColumn(column, out var fallbackColumn))
    {
      value = row.Cell(fallbackColumn).GetString();
      id = await resolver(value, default);
    }
    if (id.HasValue && id.Value > 0) return id.Value;
    throw new InvalidOperationException($"{name} לא נמצא בטבלאות המערכת: '{value}'");
  }

  private static bool TryGetClientHebrewFallbackColumn(int column, out int fallbackColumn)
  {
    fallbackColumn = column switch
    {
      4 => 5,
      5 => 4,
      9 => 10,
      10 => 11,
      11 => 12,
      _ => 0
    };
    return fallbackColumn != 0;
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

  private static async Task<int?> ResolveLookupAsync(
    IXLRow row,
    int column,
    Func<string?, CancellationToken, Task<int?>> resolver)
  {
    if (column <= 0) return null;
    var value = row.Cell(column).GetString();
    if (string.IsNullOrWhiteSpace(value)) return null;
    return await resolver(value, default);
  }

  private static async Task<int?> ResolveOptionalByColumnAsync(
    IXLRow row,
    int? column,
    Func<string?, CancellationToken, Task<int?>> resolver)
  {
    if (!column.HasValue || column.Value <= 0) return null;
    return await ResolveOptionalAsync(row, column.Value, resolver);
  }

  private async Task<int> ResolveRequiredFrameworkAsync(IXLRow row, int column)
  {
    var value = row.Cell(column).GetString();
    var id = await ResolveFrameworkValueAsync(value);
    if (id.HasValue && id.Value > 0) return id.Value;
    throw new InvalidOperationException($"מסגרת לא נמצאה בטבלאות המערכת: '{value}'");
  }

  private async Task<int?> ResolveOptionalFrameworkAsync(IXLRow row, int column)
  {
    var value = row.Cell(column).GetString();
    if (string.IsNullOrWhiteSpace(value)) return null;
    return await ResolveFrameworkValueAsync(value);
  }

  private async Task<int?> ResolveOptionalFrameworkByColumnAsync(IXLRow row, int? column)
  {
    if (!column.HasValue || column.Value <= 0) return null;
    return await ResolveOptionalFrameworkAsync(row, column.Value);
  }

  private async Task<int?> ResolveFrameworkValueAsync(string? value)
  {
    var id = await _lookupResolver.ResolveFrameworkAsync(value);
    if (id.HasValue || string.IsNullOrWhiteSpace(value)) return id;

    var numberToken = FindFirstNumberToken(value) ?? string.Empty;
    return string.IsNullOrWhiteSpace(numberToken)
      ? null
      : await _lookupResolver.ResolveFrameworkAsync(numberToken);
  }

  private async Task<int> ResolveRequiredDomainAsync(IXLRow row, int allocationId, params int[] columns)
  {
    foreach (var column in columns)
    {
      var value = row.Cell(column).GetString();
      var id = await _lookupResolver.ResolveDomainAsync(value);
      if (id.HasValue && id.Value > 0) return id.Value;
    }

    // Fall back to the allocation's single domain when the cell value doesn't resolve
    var allocationDomainIds = await _db.Set<AllocationDomain>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId)
      .Select(x => x.DomainId)
      .Distinct()
      .ToListAsync();
    if (allocationDomainIds.Count == 1)
      return allocationDomainIds[0];

    var attemptedValues = string.Join("' / '", columns
      .Select(c => row.Cell(c).GetString())
      .Where(v => !string.IsNullOrWhiteSpace(v)));
    throw new InvalidOperationException($"תחום לא נמצא בטבלאות המערכת: '{attemptedValues}'");
  }

  private static string? FindFirstNumberToken(string? value)
  {
    if (string.IsNullOrWhiteSpace(value)) return null;
    var token = value
      .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
      .FirstOrDefault(part => part.All(char.IsDigit));
    return string.IsNullOrWhiteSpace(token) ? null : token;
  }

  private static string? FindFirstTextToken(string? value)
  {
    if (string.IsNullOrWhiteSpace(value)) return null;
    var token = value
      .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
      .FirstOrDefault(part => part.Any(char.IsLetter));
    return string.IsNullOrWhiteSpace(token) ? null : token;
  }

  private static string? CombineNotes(string? first, string? second)
  {
    if (string.IsNullOrWhiteSpace(first))
      return string.IsNullOrWhiteSpace(second) ? null : second;
    if (string.IsNullOrWhiteSpace(second)) return first;
    return first + Environment.NewLine + second;
  }
}

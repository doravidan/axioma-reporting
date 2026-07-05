using System.Globalization;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public interface IBatchReportImportService
{
  Task<BatchImportResult> ImportAsync(Stream xlsxStream, int reportingMonthId, int uploaderUserId, CancellationToken ct = default, string? progressId = null);
}

public class BatchImportResult
{
  public int TotalRowsRead { get; set; }
  public int RowsImported { get; set; }
  public int ErrorRowsCount { get; set; }
  public int EmployeesAffected { get; set; }
  public List<BatchImportError> Errors { get; set; } = new();
  public List<BatchImportEmployeeSummary> EmployeeSummaries { get; set; } = new();
  /// <summary>
  /// One entry per processed row in the source file (added, updated, skipped, rejected).
  /// Each entry carries a Hebrew <see cref="BatchImportRowResult.ResultDescription"/>
  /// suitable for direct display in the import-result view.
  /// </summary>
  public List<BatchImportRowResult> RowResults { get; set; } = new();
}

public class BatchImportError
{
  public int FileRowNumber { get; set; }
  public string? EmployeeCode { get; set; }
  public string? ReporterName { get; set; }
  public string ErrorMessage { get; set; } = string.Empty;
  public string? RawValues { get; set; }
}

public enum BatchImportRowOutcome
{
  Added,
  Updated,
  Skipped,
  Rejected
}

public class BatchImportRowResult
{
  public int FileRowNumber { get; set; }
  public string? EmployeeCode { get; set; }
  public string? ReporterName { get; set; }
  public BatchImportRowOutcome Outcome { get; set; }
  /// <summary>
  /// Hebrew, ready-to-display description (e.g. "שורה 8: התאמה להקצאה ... — שורה נוספה").
  /// </summary>
  public string ResultDescription { get; set; } = string.Empty;
}

public class BatchImportEmployeeSummary
{
  public int? UserId { get; set; }
  public string EmployeeCode { get; set; } = "";
  public string ReporterName { get; set; } = "";
  public int RowsImported { get; set; }
  public int RowsRejected { get; set; }
}

public class BatchReportImportService : IBatchReportImportService
{
  private readonly AppDbContext _db;
  private readonly ILookupResolver _resolver;
  private readonly IReportValidationService _validator;
  private readonly IEmailService _emailService;

  // Header aliases: canonical key => list of accepted header substrings (case-insensitive, after whitespace collapse).
  private static readonly Dictionary<string, string[]> HeaderAliases = new(StringComparer.OrdinalIgnoreCase)
  {
    ["EmployeeCode"] = new[] { "קוד עובד" },
    ["ReporterName"] = new[] { "שם המדווח" },
    ["ReportType"] = new[] { "סוג דיווח" },
    ["District"] = new[] { "מחוז מאשר", "מחוז" },
    ["Locality"] = new[] { "יישוב", "ישוב" },
    ["Framework"] = new[] { "שם המסגרת חינוכית", "מסגרת חינוכית" },
    ["MeetingDate"] = new[] { "תאריך המפגש", "תאריך מפגש", "תאריך" },
    ["MeetingDuration"] = new[] { "משך המפגש", "משך המפגש (בשעות)" },
    ["EducationalProgram"] = new[] { "תוכנית חינוכית" },
    ["Domain"] = new[] { "תחום" },
    ["Subject1"] = new[] { "נושא 1", "נושא1" },
    ["Subject2"] = new[] { "נושא 2", "נושא2" },
    ["DiscussionCode"] = new[] { "קיום דיון", "קוד דיון" },
    ["ConclusionFramework"] = new[] { "מסגרת חינוכית" },
    ["ConclusionLocation"] = new[] { "יישוב/מחוז/ארצי", "ישוב/מחוז/ארצי" },
    ["GradeLevel"] = new[] { "שכבה" },
    ["Class"] = new[] { "כיתה" },
    ["Notes"] = new[] { "הערות" }
  };

  public BatchReportImportService(
    AppDbContext db,
    ILookupResolver resolver,
    IReportValidationService validator,
    IEmailService emailService)
  {
    _db = db;
    _resolver = resolver;
    _validator = validator;
    _emailService = emailService;
  }

  public async Task<BatchImportResult> ImportAsync(Stream xlsxStream, int reportingMonthId, int uploaderUserId, CancellationToken ct = default, string? progressId = null)
  {
    var result = new BatchImportResult();

    var month = await _db.ReportingMonths.FindAsync(new object[] { reportingMonthId }, ct);
    if (month == null)
    {
      result.Errors.Add(new BatchImportError { FileRowNumber = 0, ErrorMessage = "חודש הדיווח לא נמצא" });
      result.ErrorRowsCount = 1;
      return result;
    }

    using var workbook = new XLWorkbook(xlsxStream);

    // Snapshot which (UserId) already had a report for this month BEFORE the import,
    // so we can emit "עודכן דוח קיים" vs "שורה נוספה" descriptions per row.
    var preExistingReportUserIds = await _db.Reports
      .Where(rep => rep.ReportingMonthId == reportingMonthId)
      .Select(rep => rep.UserId)
      .ToListAsync(ct);
    var preExistingReportUsers = new HashSet<int>(preExistingReportUserIds);

    // Collect pending inserts per (UserId -> list of (row, fileRowNumber))
    var pendingByUser = new Dictionary<int, List<(ReportRow Row, int FileRow, string EmployeeCode, string ReporterName, string AllocationLabel)>>();
    var rejectionsByUser = new Dictionary<string, (int UserId, string ReporterName, int Count)>();

    // Per-import caches so repeated employee codes don't re-query the DB per row
    var usersByCode = new Dictionary<string, User?>(StringComparer.OrdinalIgnoreCase);
    var existingRowsByUser = new Dictionary<int, List<ReportRow>>();

    var rowsByHeaderRow = new Dictionary<int, int>();
    var progressTotalRows = CountImportableRows(workbook, rowsByHeaderRow);
    var progressProcessedRows = 0;
    BatchImportProgressStore.Start(progressId, progressTotalRows);

    foreach (var sheet in workbook.Worksheets)
    {
      var headerRow = FindHeaderRow(sheet);
      if (headerRow < 0) continue;

      var headerMap = BuildHeaderMap(sheet, headerRow);
      if (!headerMap.ContainsKey("EmployeeCode")) continue;

      var lastRow = sheet.LastRowUsed()?.RowNumber() ?? headerRow;
      for (var r = headerRow + 1; r <= lastRow; r++)
      {
        ct.ThrowIfCancellationRequested();
        if (IsRowEmpty(sheet, r, headerMap)) continue;
        progressProcessedRows++;
        result.TotalRowsRead++;
        BatchImportProgressStore.Update(progressId, progressProcessedRows, progressTotalRows);

        var rawEmpCode = GetCellString(sheet, r, headerMap, "EmployeeCode");
        var reporterName = GetCellString(sheet, r, headerMap, "ReporterName");
        var raw = BuildRawPreview(sheet, r, headerMap);

        if (string.IsNullOrWhiteSpace(rawEmpCode))
        {
          result.Errors.Add(new BatchImportError
          {
            FileRowNumber = r,
            ReporterName = reporterName,
            ErrorMessage = "חסר קוד עובד בשורה",
            RawValues = raw
          });
          AddRowResult(result, r, null, reporterName, BatchImportRowOutcome.Rejected,
            $"שורה {r}: נדחה — חסר קוד עובד");
          continue;
        }

        if (!usersByCode.TryGetValue(rawEmpCode, out var user))
        {
          user = await _db.Users
            .Include(u => u.Allocations)
              .ThenInclude(a => a.AllocationDistricts)
            .Include(u => u.Allocations).ThenInclude(a => a.AllocationLocalities)
            .Include(u => u.Allocations).ThenInclude(a => a.AllocationFrameworks)
              .ThenInclude(af => af.Framework)
            .Include(u => u.Allocations).ThenInclude(a => a.AllocationEducationalPrograms)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.EmployeeCode == rawEmpCode, ct);
          usersByCode[rawEmpCode] = user;
        }

        if (user == null)
        {
          result.Errors.Add(new BatchImportError
          {
            FileRowNumber = r,
            EmployeeCode = rawEmpCode,
            ReporterName = reporterName,
            ErrorMessage = $"עובד עם קוד {rawEmpCode} לא נמצא במערכת",
            RawValues = raw
          });
          AddRowResult(result, r, rawEmpCode, reporterName, BatchImportRowOutcome.Rejected,
            $"שורה {r}: נדחה — אין הקצאה תואמת");
          continue;
        }

        var displayName = string.IsNullOrWhiteSpace(reporterName)
          ? $"{user.FirstName} {user.LastName}".Trim()
          : reporterName;

        // Parse meeting date
        if (!TryReadMeetingDate(sheet, r, headerMap, out var meetingDate, out var dateError))
        {
          AddError(result, r, rawEmpCode, displayName, dateError, raw);
          Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
          AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected,
            $"שורה {r}: נדחה — {dateError}");
          continue;
        }

        if (!TryReadDecimal(sheet, r, headerMap, "MeetingDuration", out var duration))
        {
          AddError(result, r, rawEmpCode, displayName, "משך המפגש אינו מספר תקין", raw);
          Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
          AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected,
            $"שורה {r}: נדחה — משך המפגש אינו מספר תקין");
          continue;
        }

        // Resolve required lookups
        var districtText = GetCellString(sheet, r, headerMap, "District");
        var localityText = GetCellString(sheet, r, headerMap, "Locality");
        var frameworkText = GetCellString(sheet, r, headerMap, "Framework");
        var eduProgramText = GetCellString(sheet, r, headerMap, "EducationalProgram");
        var domainText = GetCellString(sheet, r, headerMap, "Domain");
        var subject1Text = GetCellString(sheet, r, headerMap, "Subject1");
        var subject2Text = GetCellString(sheet, r, headerMap, "Subject2");
        var discussionCodeText = GetCellString(sheet, r, headerMap, "DiscussionCode");
        var conclusionFrameworkText = GetCellString(sheet, r, headerMap, "ConclusionFramework");
        var conclusionLocationText = GetCellString(sheet, r, headerMap, "ConclusionLocation");
        var gradeLevelText = GetCellString(sheet, r, headerMap, "GradeLevel");
        var classText = GetCellString(sheet, r, headerMap, "Class");
        var reportTypeText = GetCellString(sheet, r, headerMap, "ReportType");
        var notes = GetCellString(sheet, r, headerMap, "Notes");

        var districtId = await _resolver.ResolveDistrictAsync(districtText, ct);
        var localityId = await _resolver.ResolveLocalityAsync(localityText, ct);
        var frameworkId = await _resolver.ResolveFrameworkAsync(frameworkText, ct);
        var eduProgramId = await _resolver.ResolveEducationalProgramAsync(eduProgramText, ct);
        var domainId = await _resolver.ResolveDomainAsync(domainText, ct);
        var subject1Id = await _resolver.ResolveSubjectAsync(subject1Text, ct);
        var subject2Id = await _resolver.ResolveSubjectAsync(subject2Text, ct);
        var discussionCodeId = await _resolver.ResolveDiscussionCodeAsync(discussionCodeText, ct);
        var conclusionFrameworkId = await _resolver.ResolveFrameworkAsync(conclusionFrameworkText, ct);
        var conclusionLocationId = await _resolver.ResolveLocalityDistrictNationalAsync(conclusionLocationText, ct);
        var gradeLevelId = await _resolver.ResolveGradeLevelAsync(gradeLevelText, ct);
        var classId = await _resolver.ResolveClassAsync(classText, ct);
        var reportTypeId = await _resolver.ResolveReportTypeAsync(reportTypeText, ct);

        var lookupErrors = new List<string>();
        if (!string.IsNullOrWhiteSpace(districtText) && districtId == null) lookupErrors.Add($"מחוז '{districtText}' לא קיים במערכת");
        if (!string.IsNullOrWhiteSpace(localityText) && localityId == null) lookupErrors.Add($"יישוב '{localityText}' לא קיים במערכת");
        if (!string.IsNullOrWhiteSpace(frameworkText) && frameworkId == null) lookupErrors.Add($"מסגרת חינוכית '{frameworkText}' לא קיימת במערכת");
        if (!string.IsNullOrWhiteSpace(eduProgramText) && eduProgramId == null) lookupErrors.Add($"תוכנית חינוכית '{eduProgramText}' לא קיימת במערכת");
        if (!string.IsNullOrWhiteSpace(domainText) && domainId == null) lookupErrors.Add($"תחום '{domainText}' לא קיים במערכת");
        if (!string.IsNullOrWhiteSpace(subject1Text) && subject1Id == null) lookupErrors.Add($"נושא 1 '{subject1Text}' לא קיים במערכת");
        if (!string.IsNullOrWhiteSpace(subject2Text) && subject2Id == null) lookupErrors.Add($"נושא 2 '{subject2Text}' לא קיים במערכת");
        if (!string.IsNullOrWhiteSpace(reportTypeText) && reportTypeId == null) lookupErrors.Add($"סוג דיווח '{reportTypeText}' לא קיים במערכת");

        if (lookupErrors.Any())
        {
          AddError(result, r, rawEmpCode, displayName, string.Join("; ", lookupErrors), raw);
          Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
          AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected,
            $"שורה {r}: נדחה — אין הקצאה תואמת");
          continue;
        }

        // Determine allocation for this row.
        // Rule: if the employee has one active allocation -> use it.
        // Otherwise, find active allocations where every non-null resolved lookup
        // (District, Locality, Framework, EducationalProgram) is in the allocation's set,
        // and (if present) matches ReportType. Pick if exactly one.
        var allocation = ResolveAllocation(
          user,
          districtId, localityId, frameworkId, eduProgramId);

        if (allocation == null)
        {
          AddError(result, r, rawEmpCode, displayName,
            $"לא ניתן לקבוע הקצאה ייחודית לעובד {rawEmpCode} בשורה {r}", raw);
          Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
          AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected,
            $"שורה {r}: נדחה — אין הקצאה תואמת");
          continue;
        }

        // Build candidate row
        var candidate = new ReportRow
        {
          AllocationId = allocation.Id,
          MeetingDate = meetingDate,
          MeetingDuration = duration,
          DistrictId = districtId ?? 0,
          LocalityId = localityId ?? 0,
          FrameworkId = frameworkId ?? 0,
          EducationalProgramId = eduProgramId ?? 0,
          DomainId = domainId ?? 0,
          Subject1Id = subject1Id ?? 0,
          Subject2Id = subject2Id,
          DiscussionCodeId = discussionCodeId,
          ConclusionFrameworkId = conclusionFrameworkId,
          ConclusionLocationId = conclusionLocationId,
          GradeLevelId = gradeLevelId,
          ClassId = classId,
          ReportTypeId = reportTypeId,
          Notes = string.IsNullOrWhiteSpace(notes) ? null : notes
        };

        // Find existing report rows for this user+month (cached per user)
        if (!existingRowsByUser.TryGetValue(user.Id, out var existingRows))
        {
          existingRows = (await _db.Reports
              .Include(rep => rep.ReportRows)
              .FirstOrDefaultAsync(rep => rep.UserId == user.Id && rep.ReportingMonthId == reportingMonthId, ct))
            ?.ReportRows.ToList() ?? new List<ReportRow>();
          existingRowsByUser[user.Id] = existingRows;
        }
        else
        {
          // Copy so pending rows appended below don't pollute the cached list
          existingRows = existingRows.ToList();
        }

        // Add the already-pending rows for this user to the validation context
        if (pendingByUser.TryGetValue(user.Id, out var queued))
          existingRows.AddRange(queued.Select(q => q.Row));

        var validationRows = existingRows.Concat(new[] { candidate }).ToList();
        var validation = await _validator.ValidateRowAsync(candidate, user, month, validationRows);

        if (!validation.IsValid)
        {
          AddError(result, r, rawEmpCode, displayName, string.Join("; ", validation.Errors), raw);
          Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
          var joined = string.Join("; ", validation.Errors);
          var isDuplicate = validation.Errors.Any(e => e.Contains("שורה כפולה", StringComparison.Ordinal));
          if (isDuplicate)
          {
            AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Skipped,
              $"שורה {r}: דולגה — דוח כפול");
          }
          else
          {
            AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected,
              $"שורה {r}: נדחה — {joined}");
          }
          continue;
        }

        if (!pendingByUser.TryGetValue(user.Id, out var list))
        {
          list = new List<(ReportRow, int, string, string, string)>();
          pendingByUser[user.Id] = list;
        }
        var allocLabel = BuildAllocationLabel(allocation);
        list.Add((candidate, r, rawEmpCode, displayName, allocLabel));
      }
    }

    // Persist: per user, find or create Report, insert rows
    foreach (var kvp in pendingByUser)
    {
      var userId = kvp.Key;
      var rows = kvp.Value;
      if (!rows.Any()) continue;

      var report = await _db.Reports
        .FirstOrDefaultAsync(rep => rep.UserId == userId && rep.ReportingMonthId == reportingMonthId, ct);

      if (report == null)
      {
        report = new Report
        {
          UserId = userId,
          ReportingMonthId = reportingMonthId,
          StatusId = 2, // InEntry
          CreatedAt = DateTime.UtcNow,
          ImportedFromExcel = true
        };
        _db.Reports.Add(report);
        await _db.SaveChangesAsync(ct);
      }
      else
      {
        report.ImportedFromExcel = true;
        report.UpdatedAt = DateTime.UtcNow;
        if (report.StatusId == 1) report.StatusId = 2;
      }

      var nextSeq = (await _db.ReportRows
        .Where(rr => rr.ReportId == report.Id)
        .MaxAsync(rr => (int?)rr.SequenceNumber, ct) ?? 0) + 1;

      var reportExistedBefore = preExistingReportUsers.Contains(userId);

      foreach (var pending in rows)
      {
        pending.Row.ReportId = report.Id;
        pending.Row.SequenceNumber = nextSeq++;
        pending.Row.CreatedAt = DateTime.UtcNow;
        _db.ReportRows.Add(pending.Row);

        if (reportExistedBefore)
        {
          AddRowResult(result, pending.FileRow, pending.EmployeeCode, pending.ReporterName,
            BatchImportRowOutcome.Updated,
            $"שורה {pending.FileRow}: עודכן דוח קיים");
        }
        else
        {
          AddRowResult(result, pending.FileRow, pending.EmployeeCode, pending.ReporterName,
            BatchImportRowOutcome.Added,
            $"שורה {pending.FileRow}: התאמה להקצאה {pending.AllocationLabel} — שורה נוספה");
        }
      }

      await _db.SaveChangesAsync(ct);

      var sample = rows.First();
      result.EmployeeSummaries.Add(new BatchImportEmployeeSummary
      {
        UserId = userId,
        EmployeeCode = sample.EmployeeCode,
        ReporterName = sample.ReporterName,
        RowsImported = rows.Count,
        RowsRejected = rejectionsByUser.TryGetValue(sample.EmployeeCode, out var rej) ? rej.Count : 0
      });
      result.RowsImported += rows.Count;
    }

    // Include employees that had only rejections (no imported rows)
    foreach (var rej in rejectionsByUser)
    {
      if (result.EmployeeSummaries.Any(s => s.EmployeeCode == rej.Key)) continue;
      result.EmployeeSummaries.Add(new BatchImportEmployeeSummary
      {
        UserId = rej.Value.UserId,
        EmployeeCode = rej.Key,
        ReporterName = rej.Value.ReporterName,
        RowsImported = 0,
        RowsRejected = rej.Value.Count
      });
    }

    result.ErrorRowsCount = result.Errors.Count;
    result.EmployeesAffected = result.EmployeeSummaries.Count(s => s.RowsImported > 0);

    // Send per-employee success emails (one per employee, not per row)
    foreach (var summary in result.EmployeeSummaries.Where(s => s.UserId.HasValue && s.RowsImported > 0))
    {
      var user = await _db.Users.FindAsync(new object[] { summary.UserId!.Value }, ct);
      if (user == null || string.IsNullOrWhiteSpace(user.Email)) continue;

      await _emailService.SendAsync(
        user.Email,
        $"{user.FirstName} {user.LastName}",
        "ReportReceived",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{user.FirstName} {user.LastName}",
          ["MonthName"] = month.Description,
          ["Month"] = month.Month.ToString(CultureInfo.InvariantCulture),
          ["Year"] = month.Year.ToString(CultureInfo.InvariantCulture),
          ["DeadlineDate"] = month.LastReportingDate.ToString("dd/MM/yyyy"),
          ["Deadline"] = month.LastReportingDate.ToString("dd/MM/yyyy")
        },
        cancellationToken: ct);
    }

    // Uploader summary emails (sent by controller because it may need to attach PDF).
    BatchImportProgressStore.Complete(progressId, progressProcessedRows, progressTotalRows);
    return result;
  }

  private static int CountImportableRows(XLWorkbook workbook, Dictionary<int, int> rowsByHeaderRow)
  {
    var count = 0;
    foreach (var sheet in workbook.Worksheets)
    {
      var headerRow = FindHeaderRow(sheet);
      if (headerRow < 0) continue;

      var headerMap = BuildHeaderMap(sheet, headerRow);
      if (!headerMap.ContainsKey("EmployeeCode")) continue;

      var lastRow = sheet.LastRowUsed()?.RowNumber() ?? headerRow;
      for (var r = headerRow + 1; r <= lastRow; r++)
      {
        if (!IsRowEmpty(sheet, r, headerMap))
          count++;
      }
    }
    return count;
  }

  private static Allocation? ResolveAllocation(
    User user,
    int? districtId,
    int? localityId,
    int? frameworkId,
    int? eduProgramId)
  {
    var actives = user.Allocations.Where(a => a.IsActive).ToList();
    if (actives.Count == 0) return null;
    if (actives.Count == 1) return actives[0];

    var matches = actives.Where(a =>
      (districtId == null || a.AllocationDistricts.Count == 0 || a.AllocationDistricts.Any(ad => ad.DistrictId == districtId)) &&
      (localityId == null || a.AllocationLocalities.Count == 0 || a.AllocationLocalities.Any(al => al.LocalityId == localityId)) &&
      MatchesFrameworkScope(a, frameworkId) &&
      (eduProgramId == null || a.AllocationEducationalPrograms.Count == 0 || a.AllocationEducationalPrograms.Any(ae => ae.EducationalProgramId == eduProgramId)))
      .ToList();

    return matches.Count == 1 ? matches[0] : null;
  }

  private static bool MatchesFrameworkScope(Allocation allocation, int? frameworkId)
  {
    if (!frameworkId.HasValue) return true;

    // Only frameworks with a numeric institution symbol constrain the allocation match;
    // non-numeric ones are conclusion frameworks and don't scope the row.
    var numericFrameworks = allocation.AllocationFrameworks
      .Where(af => af.Framework != null && int.TryParse(af.Framework.InstitutionSymbol, out _))
      .ToList();
    if (numericFrameworks.Count == 0) return true;
    return numericFrameworks.Any(af => af.FrameworkId == frameworkId);
  }

  private static void AddError(BatchImportResult result, int row, string? empCode, string? name, string msg, string? raw)
  {
    result.Errors.Add(new BatchImportError
    {
      FileRowNumber = row,
      EmployeeCode = empCode,
      ReporterName = name,
      ErrorMessage = msg,
      RawValues = raw
    });
  }

  private static void AddRowResult(BatchImportResult result, int row, string? empCode, string? name,
    BatchImportRowOutcome outcome, string description)
  {
    result.RowResults.Add(new BatchImportRowResult
    {
      FileRowNumber = row,
      EmployeeCode = empCode,
      ReporterName = name,
      Outcome = outcome,
      ResultDescription = description
    });
  }

  private static string BuildAllocationLabel(Allocation allocation)
  {
    var projectDescription = allocation.Project?.Description;
    return string.IsNullOrWhiteSpace(projectDescription)
      ? $"#{allocation.Id}"
      : $"{projectDescription} (#{allocation.Id})";
  }

  private static void Increment(Dictionary<string, (int UserId, string ReporterName, int Count)> dict, string code, int userId, string name)
  {
    if (dict.TryGetValue(code, out var existing))
      dict[code] = (existing.UserId, existing.ReporterName, existing.Count + 1);
    else
      dict[code] = (userId, name, 1);
  }

  // --- Excel parsing helpers ---

  /// <summary>
  /// Scans rows 1..15 of the sheet for a cell whose normalized text contains "קוד עובד".
  /// Returns the 1-based row number, or -1 if not found.
  /// </summary>
  public static int FindHeaderRow(IXLWorksheet sheet)
  {
    var lastRow = sheet.LastRowUsed()?.RowNumber() ?? 0;
    var scanTo = Math.Min(15, lastRow);
    for (var r = 1; r <= scanTo; r++)
    {
      var row = sheet.Row(r);
      var lastCol = row.LastCellUsed()?.Address.ColumnNumber ?? 0;
      for (var c = 1; c <= lastCol; c++)
      {
        var txt = NormalizeHeader(row.Cell(c).GetString());
        if (!string.IsNullOrEmpty(txt) && txt.Contains("קוד עובד", StringComparison.Ordinal))
          return r;
      }
    }
    return -1;
  }

  private static Dictionary<string, int> BuildHeaderMap(IXLWorksheet sheet, int headerRow)
  {
    var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    var row = sheet.Row(headerRow);
    var lastCol = row.LastCellUsed()?.Address.ColumnNumber ?? 0;
    for (var c = 1; c <= lastCol; c++)
    {
      var header = NormalizeHeader(row.Cell(c).GetString());
      if (string.IsNullOrEmpty(header)) continue;

      foreach (var (canonical, aliases) in HeaderAliases)
      {
        if (map.ContainsKey(canonical)) continue;
        foreach (var alias in aliases)
        {
          if (header.Contains(alias, StringComparison.OrdinalIgnoreCase))
          {
            map[canonical] = c;
            break;
          }
        }
      }
    }
    return map;
  }

  private static string NormalizeHeader(string? text)
  {
    if (string.IsNullOrWhiteSpace(text)) return string.Empty;
    // Collapse all whitespace (including line breaks) to a single space.
    var chars = text.Trim().ToCharArray();
    var sb = new System.Text.StringBuilder(chars.Length);
    var lastWasSpace = false;
    foreach (var ch in chars)
    {
      if (char.IsWhiteSpace(ch))
      {
        if (!lastWasSpace) { sb.Append(' '); lastWasSpace = true; }
      }
      else
      {
        sb.Append(ch);
        lastWasSpace = false;
      }
    }
    return sb.ToString().Trim();
  }

  private static string GetCellString(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap, string key)
  {
    if (!headerMap.TryGetValue(key, out var col)) return string.Empty;
    return sheet.Cell(row, col).GetString().Trim();
  }

  private static bool IsRowEmpty(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap)
  {
    foreach (var col in headerMap.Values)
    {
      if (!sheet.Cell(row, col).IsEmpty()) return false;
    }
    return true;
  }

  private static bool TryReadDecimal(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap, string key, out decimal value)
  {
    value = 0m;
    if (!headerMap.TryGetValue(key, out var col)) return false;
    var cell = sheet.Cell(row, col);
    if (cell.TryGetValue<decimal>(out var parsed)) { value = parsed; return true; }
    var text = cell.GetString().Trim();
    return decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value)
        || decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out value);
  }

  private static bool TryReadMeetingDate(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap,
    out DateTime date, out string error)
  {
    date = default;
    error = "שדה תאריך המפגש הינו חובה";
    if (!headerMap.TryGetValue("MeetingDate", out var col)) return false;

    var cell = sheet.Cell(row, col);
    if (cell.IsEmpty()) return false;

    if (cell.TryGetValue<DateTime>(out var dt)) { date = dt.Date; return true; }

    if (cell.TryGetValue<double>(out var oaDate) && oaDate > 0)
    {
      try
      {
        date = DateTime.FromOADate(oaDate).Date;
        return true;
      }
      catch (ArgumentException)
      {
        // Not a valid OADate; fall through to text parsing
      }
    }

    var text = cell.GetString().Trim();
    if (string.IsNullOrEmpty(text)) return false;

    string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/MM/yy", "d/M/yy", "yyyy-MM-dd", "dd.MM.yyyy" };
    if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
    { date = dt.Date; return true; }

    if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
    { date = dt.Date; return true; }

    error = $"תאריך '{text}' אינו תקין";
    return false;
  }

  private static string BuildRawPreview(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap)
  {
    var parts = new List<string>();
    string[] keys = { "EmployeeCode", "ReporterName", "MeetingDate", "MeetingDuration" };
    foreach (var k in keys)
    {
      var v = GetCellString(sheet, row, headerMap, k);
      if (!string.IsNullOrWhiteSpace(v)) parts.Add(v);
    }
    var s = string.Join(" | ", parts);
    return s.Length > 200 ? s[..200] : s;
  }
}

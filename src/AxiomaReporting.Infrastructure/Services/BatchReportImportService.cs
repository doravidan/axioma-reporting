using System.Globalization;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public interface IBatchReportImportService
{
  Task<BatchImportResult> ImportAsync(
    Stream xlsxStream,
    int reportingMonthId,
    int uploaderUserId,
    CancellationToken ct = default,
    string? progressId = null,
    bool previewOnly = false);
}

public class BatchImportResult
{
  public bool IsPreview { get; set; }
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
  public int? AllocationId { get; set; }
  public string? ProjectName { get; set; }
  public string? ProgramNames { get; set; }
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

  // Header aliases: canonical key => list of accepted header substrings (case-insensitive,
  // after whitespace collapse). ORDERED array — matching priority matters: overlapping
  // aliases (for example "כיתה" vs "מסקנות כיתה") must be tested in this order. Each
  // spreadsheet column is claimed by at most one canonical key so overlapping labels cannot
  // route a lookup value to the wrong foreign key.
  private static readonly (string Key, string[] Aliases)[] HeaderAliases =
  {
    ("EmployeeCode", new[] { "קוד עובד" }),
    ("ReporterName", new[] { "שם המדווח" }),
    ("AllocationId", new[] { "מזהה הקצאה", "allocationid" }),
    ("AllocationProject", new[] { "פרויקט הקצאה", "פרויקט" }),
    ("AllocationProgram", new[] { "תוכנית הקצאה", "תכנית הקצאה" }),
    ("ReportType", new[] { "סוג דיווח" }),
    ("District", new[] { "מחוז מאשר", "מחוז" }),
    ("Locality", new[] { "יישוב", "ישוב" }),
    ("ConclusionFramework", new[]
    {
      "מסגרת חינוכית (מסקנה)", "מסקנות מסגרת חינוכית", "מסקנת מסגרת חינוכית", "מסקנה-מסגרת"
    }),
    ("Framework", new[] { "שם המסגרת חינוכית", "מסגרת חינוכית" }),
    ("MeetingDate", new[] { "תאריך המפגש", "תאריך מפגש" }),
    ("MeetingDuration", new[] { "משך המפגש", "משך מפגש", "משך תפוקה" }),
    ("EducationalProgram", new[] { "תוכנית חינוכית" }),
    ("Domain", new[] { "תחום" }),
    ("Subject1", new[] { "נושא 1", "נושא1" }),
    ("Subject2", new[] { "נושא 2", "נושא2" }),
    ("DiscussionCode", new[] { "קיום דיון", "קוד דיון" }),
    ("ConclusionClass", new[] { "מסקנות כיתה", "מסקנת כיתה", "מסקנה-כיתה" }),
    ("ConclusionLocation", new[] { "יישוב/מחוז/ארצי", "ישוב/מחוז/ארצי" }),
    ("GradeLevel", new[] { "שכבה" }),
    ("Class", new[] { "כיתה" }),
    ("Notes", new[] { "הערות" })
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

  public async Task<BatchImportResult> ImportAsync(
    Stream xlsxStream,
    int reportingMonthId,
    int uploaderUserId,
    CancellationToken ct = default,
    string? progressId = null,
    bool previewOnly = false)
  {
    var result = new BatchImportResult { IsPreview = previewOnly };

    var month = await _db.ReportingMonths.FindAsync(new object[] { reportingMonthId }, ct);
    if (month == null)
    {
      result.Errors.Add(new BatchImportError { FileRowNumber = 0, ErrorMessage = "חודש הדיווח לא נמצא" });
      result.ErrorRowsCount = 1;
      BatchImportProgressStore.Update(progressId, 0, 0, "error");
      return result;
    }

    using var workbook = new XLWorkbook(xlsxStream);

    // אומדן סך השורות לצורך אחוז ההתקדמות (נדגם מהדפדפן דרך BatchReportImportProgress).
    // Scan the workbook once up front. The employee/report graphs are then loaded in
    // batches instead of issuing the same queries for every spreadsheet row.
    var importSheets = new List<(IXLWorksheet Sheet, int HeaderRow, Dictionary<string, int> HeaderMap, int LastRow)>();
    var employeeCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    var estimatedTotalRows = 0;
    foreach (var sheet in workbook.Worksheets)
    {
      var headerRow = FindHeaderRow(sheet);
      if (headerRow < 0) continue;

      var headerMap = BuildHeaderMap(sheet, headerRow);
      if (!headerMap.ContainsKey("EmployeeCode")) continue;

      var lastRow = sheet.LastRowUsed()?.RowNumber() ?? headerRow;
      importSheets.Add((sheet, headerRow, headerMap, lastRow));

      for (var rowNumber = headerRow + 1; rowNumber <= lastRow; rowNumber++)
      {
        if (IsRowEmpty(sheet, rowNumber, headerMap)) continue;
        estimatedTotalRows++;

        var employeeCode = GetCellString(sheet, rowNumber, headerMap, "EmployeeCode");
        if (!string.IsNullOrWhiteSpace(employeeCode))
          employeeCodes.Add(employeeCode);
      }
    }
    BatchImportProgressStore.Start(progressId, estimatedTotalRows);

    var codeList = employeeCodes.ToList();
    var users = new List<User>();
    foreach (var codeBatch in codeList.Chunk(1000))
    {
      var batch = codeBatch.ToArray();
      users.AddRange(await _db.Users
        .Where(user => batch.Contains(user.EmployeeCode))
        .AsSplitQuery()
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.Project)
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.AllocationPrograms).ThenInclude(item => item.Program)
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.AllocationDistricts)
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.AllocationLocalities)
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.AllocationFrameworks)
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.AllocationEducationalPrograms)
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.AllocationDomains)
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.AllocationSubjects)
        .Include(user => user.Allocations).ThenInclude(allocation => allocation.AllocationDiscussionCodes)
        .ToListAsync(ct));
    }

    var usersByEmployeeCode = users
      .GroupBy(user => user.EmployeeCode, StringComparer.OrdinalIgnoreCase)
      .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);
    var importedUserIds = users.Select(user => user.Id).Distinct().ToList();
    var reports = new List<Report>();
    foreach (var userIdBatch in importedUserIds.Chunk(1000))
    {
      var batch = userIdBatch.ToArray();
      reports.AddRange(await _db.Reports
        .Where(report => report.ReportingMonthId == reportingMonthId &&
                         batch.Contains(report.UserId) &&
                         !report.IsArchived)
        .Include(report => report.ReportRows)
        .ToListAsync(ct));
    }
    var reportsByUser = reports
      .GroupBy(report => report.UserId)
      .ToDictionary(group => group.Key, group => group.First());
    var preExistingReportUsers = reportsByUser.Keys.ToHashSet();
    var validationRowsByUser = users.ToDictionary(
      user => user.Id,
      user => reportsByUser.TryGetValue(user.Id, out var report)
        ? report.ReportRows.ToList()
        : new List<ReportRow>());

    // Collect pending inserts per (UserId -> list of (row, fileRowNumber))
    var pendingByUser = new Dictionary<int, List<(ReportRow Row, int FileRow, string EmployeeCode, string ReporterName, string AllocationLabel)>>();
    var rejectionsByUser = new Dictionary<string, (int UserId, string ReporterName, int Count)>();

    foreach (var importSheet in importSheets)
    {
      var sheet = importSheet.Sheet;
      var headerRow = importSheet.HeaderRow;
      var headerMap = importSheet.HeaderMap;
      var lastRow = importSheet.LastRow;
      for (var r = headerRow + 1; r <= lastRow; r++)
      {
        ct.ThrowIfCancellationRequested();
        if (IsRowEmpty(sheet, r, headerMap)) continue;
        result.TotalRowsRead++;
        BatchImportProgressStore.Update(progressId, result.TotalRowsRead, estimatedTotalRows);

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

        if (!usersByEmployeeCode.TryGetValue(rawEmpCode, out var user))
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

        if (!TryReadDuration(sheet, r, headerMap, out var duration, out var durationError))
        {
          AddError(result, r, rawEmpCode, displayName,
            $"שורה {r}, עמודה 'משך תפוקה': {durationError}", raw);
          Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
          AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected,
            $"שורה {r}: נדחה — {durationError}");
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
        var conclusionClassText = GetCellString(sheet, r, headerMap, "ConclusionClass");
        var conclusionFrameworkText = GetCellString(sheet, r, headerMap, "ConclusionFramework");
        var conclusionLocationText = GetCellString(sheet, r, headerMap, "ConclusionLocation");
        var gradeLevelText = GetCellString(sheet, r, headerMap, "GradeLevel");
        var classText = GetCellString(sheet, r, headerMap, "Class");
        var reportTypeText = GetCellString(sheet, r, headerMap, "ReportType");
        var notes = GetCellString(sheet, r, headerMap, "Notes");
        var explicitAllocationText = GetCellString(sheet, r, headerMap, "AllocationId");
        var allocationProjectText = GetCellString(sheet, r, headerMap, "AllocationProject");
        var allocationProgramText = GetCellString(sheet, r, headerMap, "AllocationProgram");

        var districtId = await _resolver.ResolveDistrictAsync(districtText, ct);
        var localityId = await _resolver.ResolveLocalityAsync(localityText, ct);
        var frameworkId = await _resolver.ResolveFrameworkAsync(frameworkText, ct);
        var eduProgramId = await _resolver.ResolveEducationalProgramAsync(eduProgramText, ct);
        var domainId = await _resolver.ResolveDomainAsync(domainText, ct);
        var subject1Id = await _resolver.ResolveSubjectAsync(subject1Text, ct);
        var subject2Id = await _resolver.ResolveSubjectAsync(subject2Text, ct);
        var discussionCodeId = await _resolver.ResolveDiscussionCodeAsync(discussionCodeText, ct);
        var conclusionClassId = await _resolver.ResolveClassConclusionAsync(conclusionClassText, ct);
        // מסקנת מסגרת נפתרת מטבלת המסקנות — לא ממסגרות בפועל (ה-FK מפנה
        // ל-FrameworkConclusions; פתרון מול Frameworks הפיל את הייבוא כולו).
        var conclusionFrameworkId = await _resolver.ResolveFrameworkConclusionAsync(conclusionFrameworkText, ct);
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
        if (!string.IsNullOrWhiteSpace(conclusionClassText) && conclusionClassId == null) lookupErrors.Add($"מסקנות כיתה '{conclusionClassText}' לא קיימות במערכת");
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
        var allocationResolution = ResolveAllocation(
          user,
          explicitAllocationText,
          allocationProjectText,
          allocationProgramText,
          districtId, localityId, frameworkId, eduProgramId);
        var allocation = allocationResolution.Allocation;

        if (allocation == null)
        {
          AddError(result, r, rawEmpCode, displayName,
            allocationResolution.ErrorMessage, raw);
          Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
          AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected,
            $"שורה {r}: נדחה — {allocationResolution.ErrorMessage}");
          continue;
        }

        // Build candidate row
        var candidate = new ReportRow
        {
          AllocationId = allocation.Id,
          Allocation = allocation,
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
          ConclusionClassId = conclusionClassId,
          ConclusionFrameworkId = conclusionFrameworkId,
          ConclusionLocationId = conclusionLocationId,
          GradeLevelId = gradeLevelId,
          ClassId = classId,
          ReportTypeId = reportTypeId,
          Notes = string.IsNullOrWhiteSpace(notes) ? null : notes
        };

        var validationRows = validationRowsByUser[user.Id];
        validationRows.Add(candidate);
        var validation = await _validator.ValidateRowAsync(candidate, user, month, validationRows);

        if (!validation.IsValid)
        {
          validationRows.RemoveAt(validationRows.Count - 1);
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

    if (previewOnly)
    {
      foreach (var item in pendingByUser)
      {
        var rows = item.Value;
        if (rows.Count == 0) continue;
        var reportExists = preExistingReportUsers.Contains(item.Key);
        foreach (var pending in rows)
        {
          AddRowResult(
            result,
            pending.FileRow,
            pending.EmployeeCode,
            pending.ReporterName,
            reportExists ? BatchImportRowOutcome.Updated : BatchImportRowOutcome.Added,
            $"שורה {pending.FileRow}: תצוגה מקדימה — תואמה להקצאה {pending.AllocationLabel}",
            pending.Row.AllocationId,
            pending.Row.Allocation?.Project?.Description,
            pending.Row.Allocation == null
              ? null
              : string.Join(", ", pending.Row.Allocation.AllocationPrograms
                .Select(program => program.Program?.Description)
                .Where(description => !string.IsNullOrWhiteSpace(description))));
        }

        var sample = rows[0];
        result.EmployeeSummaries.Add(new BatchImportEmployeeSummary
        {
          UserId = item.Key,
          EmployeeCode = sample.EmployeeCode,
          ReporterName = sample.ReporterName,
          RowsImported = rows.Count,
          RowsRejected = rejectionsByUser.TryGetValue(sample.EmployeeCode, out var rejected) ? rejected.Count : 0
        });
        result.RowsImported += rows.Count;
      }

      foreach (var rejected in rejectionsByUser)
      {
        if (result.EmployeeSummaries.Any(summary => summary.EmployeeCode == rejected.Key)) continue;
        result.EmployeeSummaries.Add(new BatchImportEmployeeSummary
        {
          UserId = rejected.Value.UserId,
          EmployeeCode = rejected.Key,
          ReporterName = rejected.Value.ReporterName,
          RowsRejected = rejected.Value.Count
        });
      }

      result.ErrorRowsCount = result.Errors.Count;
      result.EmployeesAffected = pendingByUser.Count(item => item.Value.Count > 0);
      BatchImportProgressStore.Complete(progressId, result.TotalRowsRead);
      return result;
    }

    // Persist: per user, find or create Report, insert rows
    foreach (var kvp in pendingByUser)
    {
      var userId = kvp.Key;
      var rows = kvp.Value;
      if (!rows.Any()) continue;

      reportsByUser.TryGetValue(userId, out var report);

      // Business rule #11: Excel upload may overwrite ONLY unapproved reports.
      // A report pending approval (3) or approved (4) must not be touched —
      // mirror the single-report import guard (ReportExcelImportService).
      if (report != null && report.StatusId is 3 or 4)
      {
        foreach (var pending in rows)
        {
          AddRowResult(result, pending.FileRow, pending.EmployeeCode, pending.ReporterName,
            BatchImportRowOutcome.Rejected,
            $"שורה {pending.FileRow}: לא ניתן לייבא לדיווח שממתין לאישור או אושר");
        }
        continue;
      }

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
        reportsByUser[userId] = report;
      }
      else
      {
        report.ImportedFromExcel = true;
        report.UpdatedAt = DateTime.UtcNow;
        if (report.StatusId == 1) report.StatusId = 2;
      }

      var nextSeq = (report.ReportRows.Select(row => (int?)row.SequenceNumber).Max() ?? 0) + 1;

      var reportExistedBefore = preExistingReportUsers.Contains(userId);

      foreach (var pending in rows)
      {
        pending.Row.Report = report;
        pending.Row.SequenceNumber = nextSeq++;
        pending.Row.CreatedAt = DateTime.UtcNow;
        report.ReportRows.Add(pending.Row);

        if (reportExistedBefore)
        {
          AddRowResult(result, pending.FileRow, pending.EmployeeCode, pending.ReporterName,
            BatchImportRowOutcome.Updated,
            $"שורה {pending.FileRow}: עודכן דוח קיים — הקצאה {pending.AllocationLabel}",
            pending.Row.AllocationId,
            pending.Row.Allocation?.Project?.Description,
            AllocationProgramNames(pending.Row.Allocation));
        }
        else
        {
          AddRowResult(result, pending.FileRow, pending.EmployeeCode, pending.ReporterName,
            BatchImportRowOutcome.Added,
            $"שורה {pending.FileRow}: התאמה להקצאה {pending.AllocationLabel} — שורה נוספה",
            pending.Row.AllocationId,
            pending.Row.Allocation?.Project?.Description,
            AllocationProgramNames(pending.Row.Allocation));
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
    BatchImportProgressStore.Complete(progressId, result.TotalRowsRead);
    return result;
  }

  private sealed record AllocationResolution(Allocation? Allocation, string ErrorMessage);

  private static AllocationResolution ResolveAllocation(
    User user,
    string? explicitAllocationText,
    string? projectText,
    string? programText,
    int? districtId,
    int? localityId,
    int? frameworkId,
    int? eduProgramId)
  {
    var actives = user.Allocations.Where(a => a.IsActive).ToList();
    if (actives.Count == 0)
      return new AllocationResolution(null, "אין לעובד הקצאה פעילה");

    if (!string.IsNullOrWhiteSpace(explicitAllocationText))
    {
      if (!int.TryParse(explicitAllocationText.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture,
            out var explicitAllocationId) || explicitAllocationId <= 0)
        return new AllocationResolution(null,
          $"מזהה ההקצאה '{explicitAllocationText}' אינו מספר תקין");
      actives = actives.Where(allocation => allocation.Id == explicitAllocationId).ToList();
      if (actives.Count == 0)
        return new AllocationResolution(null,
          $"הקצאה #{explicitAllocationId} אינה פעילה או אינה שייכת לעובד {user.EmployeeCode}");
    }

    if (!string.IsNullOrWhiteSpace(projectText))
      actives = actives.Where(allocation =>
        MatchesAllocationValue(projectText, allocation.ProjectId, allocation.Project?.Description)).ToList();

    if (!string.IsNullOrWhiteSpace(programText))
      actives = actives.Where(allocation => allocation.AllocationPrograms.Any(program =>
        MatchesAllocationValue(programText, program.ProgramId, program.Program?.Description))).ToList();

    var matches = actives.Where(a =>
      (districtId == null || a.AllocationDistricts.Count == 0 || a.AllocationDistricts.Any(ad => ad.DistrictId == districtId)) &&
      (localityId == null || a.AllocationLocalities.Count == 0 || a.AllocationLocalities.Any(al => al.LocalityId == localityId)) &&
      (frameworkId == null || a.AllocationFrameworks.Count == 0 || a.AllocationFrameworks.Any(af => af.FrameworkId == frameworkId)) &&
      (eduProgramId == null || a.AllocationEducationalPrograms.Count == 0 || a.AllocationEducationalPrograms.Any(ae => ae.EducationalProgramId == eduProgramId)))
      .ToList();

    return matches.Count switch
    {
      1 => new AllocationResolution(matches[0], string.Empty),
      0 => new AllocationResolution(null, "לא נמצאה הקצאה תואמת לעובד ולנתוני השורה"),
      _ => new AllocationResolution(null,
        "נמצאו מספר הקצאות מתאימות. יש לבחור תוכנית או הקצאה.")
    };
  }

  private static bool MatchesAllocationValue(string rawValue, int id, string? description)
  {
    var normalized = ExcelReportParsing.NormalizeHeader(rawValue);
    if (int.TryParse(normalized, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericId))
      return numericId == id;
    return !string.IsNullOrWhiteSpace(description) &&
           string.Equals(normalized, ExcelReportParsing.NormalizeHeader(description), StringComparison.Ordinal);
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
    BatchImportRowOutcome outcome, string description, int? allocationId = null,
    string? projectName = null, string? programNames = null)
  {
    result.RowResults.Add(new BatchImportRowResult
    {
      FileRowNumber = row,
      EmployeeCode = empCode,
      ReporterName = name,
      Outcome = outcome,
      ResultDescription = description,
      AllocationId = allocationId,
      ProjectName = projectName,
      ProgramNames = programNames
    });
  }

  private static string BuildAllocationLabel(Allocation allocation)
  {
    var projectDescription = allocation.Project?.Description;
    var programs = string.Join(", ", allocation.AllocationPrograms
      .Select(item => item.Program?.Description)
      .Where(description => !string.IsNullOrWhiteSpace(description)));
    var parts = new[] { programs, projectDescription }
      .Where(value => !string.IsNullOrWhiteSpace(value));
    var description = string.Join(" — ", parts);
    return string.IsNullOrWhiteSpace(description) ? $"#{allocation.Id}" : $"{description} (#{allocation.Id})";
  }

  private static string? AllocationProgramNames(Allocation? allocation)
  {
    if (allocation == null) return null;
    var names = allocation.AllocationPrograms
      .Select(item => item.Program?.Description)
      .Where(description => !string.IsNullOrWhiteSpace(description));
    var value = string.Join(", ", names);
    return string.IsNullOrWhiteSpace(value) ? null : value;
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
      var txt = ExcelReportParsing.NormalizeHeader(row.Cell(c).GetString());
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
      var header = ExcelReportParsing.NormalizeHeader(row.Cell(c).GetString());
      if (string.IsNullOrEmpty(header)) continue;

      foreach (var (canonical, aliases) in HeaderAliases)
      {
        if (map.ContainsKey(canonical)) continue;
        if (aliases.Any(alias => header.Contains(alias, StringComparison.OrdinalIgnoreCase)))
        {
          map[canonical] = c;
          break;
        }
      }
    }
    return map;
  }

  private static string NormalizeHeader(string? text)
  {
    return ExcelReportParsing.NormalizeHeader(text);
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

  private static bool TryReadDuration(
    IXLWorksheet sheet,
    int row,
    Dictionary<string, int> headerMap,
    out decimal value,
    out string error)
  {
    value = 0m;
    error = $"העמודה חסרה; הפורמט הנדרש: {ExcelReportParsing.DurationFormatDescription}";
    if (!headerMap.TryGetValue("MeetingDuration", out var col)) return false;
    return ExcelReportParsing.TryParseDuration(
      sheet.Cell(row, col), out value, out _, out error);
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

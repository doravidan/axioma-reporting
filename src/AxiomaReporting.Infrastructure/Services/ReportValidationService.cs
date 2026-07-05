using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class ValidationResult
{
  public bool IsValid => !Errors.Any();
  public List<string> Errors { get; } = new();
  public List<string> Warnings { get; } = new();
  public void AddError(string msg) => Errors.Add(msg);
  public void AddWarning(string msg) => Warnings.Add(msg);
}

public interface IReportValidationService
{
  Task<ValidationResult> ValidateRowAsync(
    ReportRow row, User employee, ReportingMonth month, List<ReportRow> allRowsInReport);
  Task<ValidationResult> ValidateSubmitAsync(
    Report report, User employee, ReportingMonth month);
}

public class ReportValidationService : IReportValidationService
{
  private const string RequiredReportFieldsKey = "RequiredReportFields";

  private static readonly HashSet<string> DefaultRequiredFields = new(StringComparer.OrdinalIgnoreCase)
  {
    "AllocationId",
    "DistrictId",
    "LocalityId",
    "FrameworkId",
    "EducationalProgramId",
    "DomainId",
    "Subject1Id",
    "MeetingDate",
    "MeetingDuration"
  };

  private readonly AppDbContext _db;

  // Per-request caches: validation runs row-by-row over the same few allocations
  private readonly Dictionary<int, Allocation> _allocationScopeCache = new();
  private readonly Dictionary<int, Allocation> _allocationSimpleCache = new();
  private HashSet<string>? _requiredFieldsCache;

  public ReportValidationService(AppDbContext db) { _db = db; }

  public async Task<ValidationResult> ValidateRowAsync(
    ReportRow row, User employee, ReportingMonth month, List<ReportRow> allRowsInReport)
  {
    var result = new ValidationResult();
    var requiredFields = await GetRequiredFieldsAsync();

    if (requiredFields.Contains("AllocationId") && !row.AllocationId.HasValue) result.AddError("חובה לבחור הקצאה לשורת הדיווח");
    if (requiredFields.Contains("DistrictId") && row.DistrictId == 0) result.AddError("שדה מחוז הינו חובה");
    if (requiredFields.Contains("LocalityId") && row.LocalityId == 0) result.AddError("שדה ישוב הינו חובה");
    if (requiredFields.Contains("FrameworkId") && row.FrameworkId == 0) result.AddError("שדה מסגרת הינו חובה");
    if (requiredFields.Contains("EducationalProgramId") && row.EducationalProgramId == 0) result.AddError("שדה תוכנית חינוכית הינו חובה");
    if (requiredFields.Contains("DomainId") && row.DomainId == 0) result.AddError("שדה תחום הינו חובה");
    if (requiredFields.Contains("Subject1Id") && row.Subject1Id == 0) result.AddError("שדה נושא 1 הינו חובה");
    if (requiredFields.Contains("MeetingDate") && row.MeetingDate == default) result.AddError("שדה תאריך המפגש הינו חובה");
    if (requiredFields.Contains("MeetingDuration") && row.MeetingDuration <= 0) result.AddError("משך המפגש חייב להיות גדול מ-0");
    else if (row.MeetingDuration < 0) result.AddError("משך המפגש אינו יכול להיות שלילי");

    if (!result.IsValid) return result;

    var today = DateTime.Today;
    var meetingDate = row.MeetingDate.Date;
    var allowFuture = employee.AllowFutureReporting && month.AllowFutureReporting;
    var reportingMonthEnd = new DateTime(month.Year, month.Month, DateTime.DaysInMonth(month.Year, month.Month));

    if (!allowFuture && meetingDate > today)
      result.AddError("לא ניתן לדווח על תאריך עתידי");

    if (meetingDate > reportingMonthEnd)
      result.AddError("תאריך המפגש חייב להיות בחודש הדיווח או בחודש קודם");

    if (employee.RestDay.HasValue && (int)meetingDate.DayOfWeek == employee.RestDay.Value)
      result.AddError("לא ניתן לדווח ביום המנוחה של העובד");

    await ValidateAllocationScopeAsync(result, row);
    await ValidateDailyLimitAsync(result, row, meetingDate, allRowsInReport);
    await ValidateOutputDurationAsync(result, row);
    await ValidateMonthlyDurationLimitAsync(result, row, allRowsInReport);
    ValidateDuplicateRows(result, row, meetingDate, allRowsInReport);
    await ValidateNotesSimilarityAsync(result, row, allRowsInReport);

    return result;
  }

  public async Task<ValidationResult> ValidateSubmitAsync(
    Report report, User employee, ReportingMonth month)
  {
    var result = new ValidationResult();

    if (DateTime.Today > month.LastReportingDate.Date)
      result.AddError($"תאריך הגשת הדיווח עבר ב-{month.LastReportingDate:dd/MM/yyyy}");

    if (!result.IsValid) return result;

    var rows = await _db.ReportRows.Where(r => r.ReportId == report.Id).ToListAsync();
    if (!rows.Any())
    {
      result.AddError("לא ניתן להגיש דיווח ללא שורות");
      return result;
    }

    if (rows.Any(r => !r.AllocationId.HasValue))
      result.AddError("כל שורות הדיווח חייבות להיות משויכות להקצאה");

    foreach (var allocationGroup in rows.Where(r => r.AllocationId.HasValue).GroupBy(r => r.AllocationId!.Value))
    {
      var allocation = await _db.Allocations.FindAsync(allocationGroup.Key);
      if (allocation == null)
      {
        result.AddError("נמצאה שורת דיווח עם הקצאה שלא קיימת");
        continue;
      }

      var monthlyCount = allocationGroup.Count();
      if (allocation.MonthlyRowAllocation.HasValue && monthlyCount > allocation.MonthlyRowAllocation.Value)
        result.AddError(
          $"חריגה ממספר שורות חודשי להקצאה {allocation.ProjectId} ({allocation.MonthlyRowAllocation.Value} שורות מותרות, {monthlyCount} שורות בדיווח)");

      var monthlyDuration = allocationGroup.Sum(r => r.MeetingDuration);
      if (allocation.MonthlyEmploymentScope.HasValue && monthlyDuration > allocation.MonthlyEmploymentScope.Value)
        result.AddError(
          $"חריגה מהיקף פעילות חודשי להקצאה {allocation.ProjectId} (מותר: {allocation.MonthlyEmploymentScope.Value}, בדיווח: {monthlyDuration})");

      if (allocation.AnnualRowAllocation.HasValue)
      {
        var annualRows = await _db.ReportRows
          .Include(r => r.Report).ThenInclude(rep => rep!.ReportingMonth)
          .Where(r => r.AllocationId == allocation.Id &&
                      r.Report!.ReportingMonth!.Year == month.Year)
          .CountAsync();
        if (annualRows > allocation.AnnualRowAllocation.Value)
          result.AddError($"חריגה ממספר שורות שנתי להקצאה {allocation.ProjectId} ({allocation.AnnualRowAllocation.Value} שורות מותרות)");
      }
    }

    var totalDuration = rows.Sum(r => r.MeetingDuration);
    result.AddWarning($"סה\"כ משך תפוקה בדיווח: {totalDuration}");

    return result;
  }

  private async Task ValidateAllocationScopeAsync(ValidationResult result, ReportRow row)
  {
    if (!row.AllocationId.HasValue) return;

    if (!_allocationScopeCache.TryGetValue(row.AllocationId.Value, out var allocation))
    {
      allocation = await _db.Allocations
        .Include(a => a.AllocationDistricts)
        .Include(a => a.AllocationLocalities)
        .Include(a => a.AllocationFrameworks).ThenInclude(x => x.Framework)
        .Include(a => a.AllocationEducationalPrograms)
        .Include(a => a.AllocationDomains)
        .Include(a => a.AllocationSubjects)
        .Include(a => a.AllocationDiscussionCodes)
        .Include(a => a.AllocationClasses).ThenInclude(x => x.SchoolClass)
        .Include(a => a.AllocationGradeLevels)
        .Include(a => a.AllocationLocalityDistrictNationals)
        .AsSplitQuery()
        .FirstOrDefaultAsync(a => a.Id == row.AllocationId.Value);
      if (allocation != null)
        _allocationScopeCache[row.AllocationId.Value] = allocation;
    }

    if (allocation == null)
    {
      result.AddError("ההקצאה שנבחרה לא קיימת");
      return;
    }

    // Frameworks with a numeric institution symbol scope the framework field; the rest
    // scope the conclusion framework. Classes split the same way by description.
    var numericFrameworks = allocation.AllocationFrameworks
      .Where(x => x.Framework != null && IsNumberOnly(x.Framework.InstitutionSymbol)).ToList();
    var conclusionFrameworks = allocation.AllocationFrameworks
      .Where(x => x.Framework != null && !IsNumberOnly(x.Framework.InstitutionSymbol)).ToList();
    var numericClasses = allocation.AllocationClasses
      .Where(x => x.SchoolClass != null && IsNumberOnly(x.SchoolClass.Description)).ToList();
    var conclusionClasses = allocation.AllocationClasses
      .Where(x => x.SchoolClass != null && !IsNumberOnly(x.SchoolClass.Description)).ToList();

    if (allocation.AllocationDistricts.Any() && !allocation.AllocationDistricts.Any(x => x.DistrictId == row.DistrictId))
      result.AddError("המחוז אינו תואם להקצאת העובד");
    if (allocation.AllocationLocalities.Any() && !allocation.AllocationLocalities.Any(x => x.LocalityId == row.LocalityId))
      result.AddError("היישוב אינו תואם להקצאת העובד");
    if (numericFrameworks.Any() && !numericFrameworks.Any(x => x.FrameworkId == row.FrameworkId))
      result.AddError("המסגרת אינה תואמת להקצאת העובד");
    if (allocation.AllocationEducationalPrograms.Any() && !allocation.AllocationEducationalPrograms.Any(x => x.EducationalProgramId == row.EducationalProgramId))
      result.AddError("התוכנית החינוכית אינה תואמת להקצאת העובד");
    if (allocation.AllocationDomains.Any() && !allocation.AllocationDomains.Any(x => x.DomainId == row.DomainId))
      result.AddError("התחום אינו תואם להקצאת העובד");
    if (allocation.AllocationSubjects.Any() && !allocation.AllocationSubjects.Any(x => x.SubjectId == row.Subject1Id))
      result.AddError("נושא 1 אינו תואם להקצאת העובד");
    if (row.Subject2Id.HasValue && allocation.AllocationSubjects.Any() && !allocation.AllocationSubjects.Any(x => x.SubjectId == row.Subject2Id.Value))
      result.AddError("נושא 2 אינו תואם להקצאת העובד");
    if (row.DiscussionCodeId.HasValue && allocation.AllocationDiscussionCodes.Any() && !allocation.AllocationDiscussionCodes.Any(x => x.DiscussionCodeId == row.DiscussionCodeId.Value))
      result.AddError("קוד הדיון אינו תואם להקצאת העובד");
    if (row.ClassId.HasValue && numericClasses.Any() && !numericClasses.Any(x => x.ClassId == row.ClassId.Value))
      result.AddError("הכיתה אינה תואמת להקצאת העובד");
    if (row.GradeLevelId.HasValue && allocation.AllocationGradeLevels.Any() && !allocation.AllocationGradeLevels.Any(x => x.GradeLevelId == row.GradeLevelId.Value))
      result.AddError("השכבה אינה תואמת להקצאת העובד");
    if (row.ConclusionClassId.HasValue && conclusionClasses.Any() && !conclusionClasses.Any(x => x.ClassId == row.ConclusionClassId.Value))
      result.AddError("מסקנה - כיתה אינה תואמת להקצאת העובד");
    if (row.ConclusionFrameworkId.HasValue && conclusionFrameworks.Any() && !conclusionFrameworks.Any(x => x.FrameworkId == row.ConclusionFrameworkId.Value))
      result.AddError("מסקנה - מסגרת אינה תואמת להקצאת העובד");
    if (row.ConclusionLocationId.HasValue && allocation.AllocationLocalityDistrictNationals.Any() && !allocation.AllocationLocalityDistrictNationals.Any(x => x.LocalityDistrictNationalId == row.ConclusionLocationId.Value))
      result.AddError("מסקנה - מיקום אינה תואמת להקצאת העובד");
  }

  private static bool IsNumberOnly(string? value) => int.TryParse(value?.Trim(), out _);

  private async Task ValidateDailyLimitAsync(
    ValidationResult result, ReportRow row, DateTime meetingDate, List<ReportRow> allRowsInReport)
  {
    var rowsInDailyScope = allRowsInReport
      .Where(r => r != row && r.MeetingDate.Date == meetingDate && r.AllocationId == row.AllocationId);

    decimal? limit = null;
    if (row.AllocationId.HasValue)
    {
      var allocation = await GetSimpleAllocationAsync(row.AllocationId.Value);
      if (allocation != null && !allocation.DailyEmploymentScope.HasValue)
        return;
      limit = allocation?.DailyEmploymentScope;
    }

    if (!limit.HasValue)
    {
      var maxHoursConstant = await _db.SystemConstants
        .FirstOrDefaultAsync(c => c.Key == "MaxDailyHoursDefault");
      limit = decimal.TryParse(maxHoursConstant?.Value, out var maxHours) ? maxHours : 9m;
    }

    var dailyTotal = rowsInDailyScope.Sum(r => r.MeetingDuration) + row.MeetingDuration;
    if (dailyTotal > limit.Value)
      result.AddError($"חריגה ממשך תפוקה יומי (מותר: {limit.Value})");
  }

  private async Task ValidateMonthlyDurationLimitAsync(
    ValidationResult result, ReportRow row, List<ReportRow> allRowsInReport)
  {
    if (!row.AllocationId.HasValue) return;

    var allocation = await GetSimpleAllocationAsync(row.AllocationId.Value);
    if (allocation?.MonthlyEmploymentScope == null) return;

    var monthlyTotal = allRowsInReport
      .Where(r => r.AllocationId == row.AllocationId)
      .Sum(r => r.MeetingDuration);

    if (monthlyTotal > allocation.MonthlyEmploymentScope.Value)
      result.AddError($"חריגה מהיקף פעילות חודשי (מותר: {allocation.MonthlyEmploymentScope.Value})");
  }

  private async Task ValidateOutputDurationAsync(ValidationResult result, ReportRow row)
  {
    if (!row.AllocationId.HasValue) return;

    var allocation = await GetSimpleAllocationAsync(row.AllocationId.Value);
    if (string.IsNullOrWhiteSpace(allocation?.OutputDuration)) return;

    var tokens = allocation.OutputDuration
      .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    if (tokens.Any(t => t.Equals("Unlimited", StringComparison.OrdinalIgnoreCase) ||
                        t.Equals("ללא הגבלה", StringComparison.OrdinalIgnoreCase)))
      return;

    var allowed = tokens
      .Select(t => decimal.TryParse(t, out var value) ? value : (decimal?)null)
      .Where(v => v.HasValue)
      .Select(v => v!.Value)
      .ToHashSet();

    if (allowed.Any() && !allowed.Contains(row.MeetingDuration))
      result.AddError($"משך התפוקה חייב להיות אחד מהערכים שהוגדרו בהקצאה: {string.Join(", ", allowed.OrderBy(v => v))}");
  }

  private static void ValidateDuplicateRows(
    ValidationResult result, ReportRow row, DateTime meetingDate, List<ReportRow> allRowsInReport)
  {
    var duplicates = allRowsInReport.Where(r =>
      r != row &&
      r.AllocationId == row.AllocationId &&
      r.MeetingDate.Date == meetingDate &&
      r.DistrictId == row.DistrictId &&
      r.LocalityId == row.LocalityId &&
      r.FrameworkId == row.FrameworkId &&
      r.EducationalProgramId == row.EducationalProgramId &&
      r.DomainId == row.DomainId &&
      r.Subject1Id == row.Subject1Id &&
      r.Subject2Id == row.Subject2Id &&
      r.DiscussionCodeId == row.DiscussionCodeId &&
      r.ConclusionClassId == row.ConclusionClassId &&
      r.ConclusionFrameworkId == row.ConclusionFrameworkId &&
      r.ConclusionLocationId == row.ConclusionLocationId &&
      r.GradeLevelId == row.GradeLevelId &&
      r.ClassId == row.ClassId);

    foreach (var dup in duplicates)
    {
      if (string.IsNullOrEmpty(row.Notes) && string.IsNullOrEmpty(dup.Notes))
      {
        result.AddError("שורה כפולה: אותם ערכים ואין הערות");
        break;
      }

      if (!string.IsNullOrEmpty(row.Notes) && row.Notes == dup.Notes)
      {
        result.AddError("שורה כפולה: אותם ערכים והערות זהות");
        break;
      }
    }
  }

  private async Task ValidateNotesSimilarityAsync(
    ValidationResult result, ReportRow row, List<ReportRow> allRowsInReport)
  {
    // Notes similarity check disabled in v1.2.x per client feedback.
    if (string.IsNullOrEmpty(row.Notes)) return;
    return;

#pragma warning disable CS0162 // Unreachable code kept intentionally for potential re-enable
    var thresholdConstant = await _db.SystemConstants
      .FirstOrDefaultAsync(c => c.Key == "NotesSimilarityThresholdPercent");
    var threshold = double.TryParse(thresholdConstant?.Value, out var t) ? t : 90.0;

    foreach (var other in allRowsInReport.Where(r => r != row && !string.IsNullOrEmpty(r.Notes)))
    {
      var similarity = NotesSimilarity(row.Notes!, other.Notes!);
      if (similarity >= threshold)
      {
        result.AddError($"הערות דומות מדי לשורה אחרת בדיווח ({similarity:F0}% דמיון)");
        break;
      }
    }
#pragma warning restore CS0162
  }

  private async Task<HashSet<string>> GetRequiredFieldsAsync()
  {
    if (_requiredFieldsCache != null) return _requiredFieldsCache;

    var configured = await _db.SystemConstants
      .Where(c => c.Key == RequiredReportFieldsKey)
      .Select(c => c.Value)
      .FirstOrDefaultAsync();

    _requiredFieldsCache = string.IsNullOrWhiteSpace(configured)
      ? new HashSet<string>(DefaultRequiredFields, StringComparer.OrdinalIgnoreCase)
      : configured
          .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
          .ToHashSet(StringComparer.OrdinalIgnoreCase);

    return _requiredFieldsCache;
  }

  private async Task<Allocation?> GetSimpleAllocationAsync(int allocationId)
  {
    if (!_allocationSimpleCache.TryGetValue(allocationId, out var allocation))
    {
      allocation = await _db.Allocations.FindAsync(allocationId);
      if (allocation != null)
        _allocationSimpleCache[allocationId] = allocation;
    }
    return allocation;
  }

  private static double NotesSimilarity(string a, string b)
  {
    var distance = LevenshteinDistance(a, b);
    var maxLen = Math.Max(a.Length, b.Length);
    if (maxLen == 0) return 100.0;
    return (1.0 - (double)distance / maxLen) * 100.0;
  }

  private static int LevenshteinDistance(string s, string t)
  {
    int n = s.Length, m = t.Length;
    var d = new int[n + 1, m + 1];
    for (int i = 0; i <= n; i++) d[i, 0] = i;
    for (int j = 0; j <= m; j++) d[0, j] = j;
    for (int i = 1; i <= n; i++)
      for (int j = 1; j <= m; j++)
        d[i, j] = Math.Min(
          Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
          d[i - 1, j - 1] + (s[i - 1] == t[j - 1] ? 0 : 1));
    return d[n, m];
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class ReportValidationService : IReportValidationService
{
	private const string RequiredReportFieldsKey = "RequiredReportFields";

	private static readonly HashSet<string> DefaultRequiredFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "AllocationId", "DistrictId", "LocalityId", "FrameworkId", "EducationalProgramId", "DomainId", "Subject1Id", "MeetingDate", "MeetingDuration" };

	private readonly AppDbContext _db;

	private readonly Dictionary<int, Allocation> _allocationScopeCache = new Dictionary<int, Allocation>();

	private readonly Dictionary<int, Allocation> _allocationSimpleCache = new Dictionary<int, Allocation>();

	private HashSet<string> _requiredFieldsCache;

	public ReportValidationService(AppDbContext db)
	{
		_db = db;
	}

	public async Task<ValidationResult> ValidateRowAsync(ReportRow row, User employee, ReportingMonth month, List<ReportRow> allRowsInReport)
	{
		ValidationResult result = new ValidationResult();
		HashSet<string> obj = await GetRequiredFieldsAsync();
		if (obj.Contains("AllocationId") && !row.AllocationId.HasValue)
		{
			result.AddError("חובה לבחור הקצאה לשורת הדיווח");
		}
		if (obj.Contains("DistrictId") && row.DistrictId == 0)
		{
			result.AddError("שדה מחוז הינו חובה");
		}
		if (obj.Contains("LocalityId") && row.LocalityId == 0)
		{
			result.AddError("שדה ישוב הינו חובה");
		}
		if (obj.Contains("FrameworkId") && row.FrameworkId == 0)
		{
			result.AddError("שדה מסגרת הינו חובה");
		}
		if (obj.Contains("EducationalProgramId") && row.EducationalProgramId == 0)
		{
			result.AddError("שדה תוכנית חינוכית הינו חובה");
		}
		if (obj.Contains("DomainId") && row.DomainId == 0)
		{
			result.AddError("שדה תחום הינו חובה");
		}
		if (obj.Contains("Subject1Id") && row.Subject1Id == 0)
		{
			result.AddError("שדה נושא 1 הינו חובה");
		}
		if (obj.Contains("MeetingDate") && row.MeetingDate == default(DateTime))
		{
			result.AddError("שדה תאריך המפגש הינו חובה");
		}
		if (obj.Contains("MeetingDuration") && row.MeetingDuration <= 0m)
		{
			result.AddError("משך המפגש חייב להיות גדול מ-0");
		}
		else if (row.MeetingDuration < 0m)
		{
			result.AddError("משך המפגש אינו יכול להיות שלילי");
		}
		if (!result.IsValid)
		{
			return result;
		}
		DateTime today = DateTime.Today;
		DateTime meetingDate = row.MeetingDate.Date;
		bool num = employee.AllowFutureReporting && month.AllowFutureReporting;
		DateTime dateTime = new DateTime(month.Year, month.Month, DateTime.DaysInMonth(month.Year, month.Month));
		if (!num && meetingDate > today)
		{
			result.AddError("לא ניתן לדווח על תאריך עתידי");
		}
		if (meetingDate > dateTime)
		{
			result.AddError("תאריך המפגש חייב להיות בחודש הדיווח או בחודש קודם");
		}
		if (employee.RestDay.HasValue && meetingDate.DayOfWeek == (DayOfWeek)employee.RestDay.Value)
		{
			result.AddError("לא ניתן לדווח ביום המנוחה של העובד");
		}
		await ValidateAllocationScopeAsync(result, row);
		await ValidateDailyLimitAsync(result, row, meetingDate, allRowsInReport);
		await ValidateOutputDurationAsync(result, row);
		await ValidateMonthlyDurationLimitAsync(result, row, allRowsInReport);
		ValidateDuplicateRows(result, row, meetingDate, allRowsInReport);
		await ValidateNotesSimilarityAsync(result, row, allRowsInReport);
		return result;
	}

	public async Task<ValidationResult> ValidateSubmitAsync(Report report, User employee, ReportingMonth month)
	{
		Report report2 = report;
		ReportingMonth month2 = month;
		ValidationResult result = new ValidationResult();
		if (DateTime.Today > month2.LastReportingDate.Date)
		{
			result.AddError($"תאריך הגשת הדיווח עבר ב-{month2.LastReportingDate:dd/MM/yyyy}");
		}
		if (!result.IsValid)
		{
			return result;
		}
		List<ReportRow> rows = await _db.ReportRows.Where((ReportRow r) => r.ReportId == report2.Id).ToListAsync();
		if (!rows.Any())
		{
			result.AddError("לא ניתן להגיש דיווח ללא שורות");
			return result;
		}
		if (rows.Any((ReportRow r) => !r.AllocationId.HasValue))
		{
			result.AddError("כל שורות הדיווח חייבות להיות משויכות להקצאה");
		}
		foreach (IGrouping<int, ReportRow> allocationGroup in from r in rows
			where r.AllocationId.HasValue
			group r by r.AllocationId.Value)
		{
			Allocation allocation = await _db.Allocations.FindAsync(allocationGroup.Key);
			if (allocation == null)
			{
				result.AddError("נמצאה שורת דיווח עם הקצאה שלא קיימת");
				continue;
			}
			int num = allocationGroup.Count();
			if (allocation.MonthlyRowAllocation.HasValue && num > allocation.MonthlyRowAllocation.Value)
			{
				result.AddError($"חריגה ממספר שורות חודשי להקצאה {allocation.ProjectId} ({allocation.MonthlyRowAllocation.Value} שורות מותרות, {num} שורות בדיווח)");
			}
			decimal num2 = allocationGroup.Sum((ReportRow r) => r.MeetingDuration);
			if (allocation.MonthlyEmploymentScope.HasValue && num2 > allocation.MonthlyEmploymentScope.Value)
			{
				result.AddError($"חריגה מהיקף פעילות חודשי להקצאה {allocation.ProjectId} (מותר: {allocation.MonthlyEmploymentScope.Value}, בדיווח: {num2})");
			}
			if (allocation.AnnualRowAllocation.HasValue && await (from r in _db.ReportRows.Include((ReportRow r) => r.Report).ThenInclude((Report rep) => rep.ReportingMonth)
				where r.AllocationId == (int?)allocation.Id && r.Report.ReportingMonth.Year == month2.Year
				select r).CountAsync() > allocation.AnnualRowAllocation.Value)
			{
				result.AddError($"חריגה ממספר שורות שנתי להקצאה {allocation.ProjectId} ({allocation.AnnualRowAllocation.Value} שורות מותרות)");
			}
		}
		decimal value = rows.Sum((ReportRow r) => r.MeetingDuration);
		result.AddWarning($"סה\"כ משך תפוקה בדיווח: {value}");
		return result;
	}

	private async Task ValidateAllocationScopeAsync(ValidationResult result, ReportRow row)
	{
		ReportRow row2 = row;
		if (!row2.AllocationId.HasValue)
		{
			return;
		}
		if (!_allocationScopeCache.TryGetValue(row2.AllocationId.Value, out Allocation allocation))
		{
			allocation = await _db.Allocations.Include((Allocation a) => a.AllocationDistricts).Include((Allocation a) => a.AllocationLocalities).Include((Allocation a) => a.AllocationFrameworks)
				.ThenInclude((AllocationFramework x) => x.Framework)
				.Include((Allocation a) => a.AllocationEducationalPrograms)
				.Include((Allocation a) => a.AllocationDomains)
				.Include((Allocation a) => a.AllocationSubjects)
				.Include((Allocation a) => a.AllocationDiscussionCodes)
				.Include((Allocation a) => a.AllocationClasses)
				.ThenInclude((AllocationClass x) => x.SchoolClass)
				.Include((Allocation a) => a.AllocationGradeLevels)
				.Include((Allocation a) => a.AllocationLocalityDistrictNationals)
				.AsSplitQuery()
				.FirstOrDefaultAsync((Allocation a) => a.Id == row2.AllocationId.Value);
			if (allocation != null)
			{
				_allocationScopeCache[row2.AllocationId.Value] = allocation;
			}
		}
		if (allocation == null)
		{
			result.AddError("ההקצאה שנבחרה לא קיימת");
			return;
		}
		if (allocation.AllocationDistricts.Any() && !allocation.AllocationDistricts.Any((AllocationDistrict x) => x.DistrictId == row2.DistrictId))
		{
			result.AddError("המחוז אינו תואם להקצאת העובד");
		}
		if (allocation.AllocationLocalities.Any() && !allocation.AllocationLocalities.Any((AllocationLocality x) => x.LocalityId == row2.LocalityId))
		{
			result.AddError("היישוב אינו תואם להקצאת העובד");
		}
		List<AllocationFramework> institutionFrameworks = allocation.AllocationFrameworks.Where((AllocationFramework x) => x.Framework != null && IsNumberOnly(x.Framework.InstitutionSymbol)).ToList();
		List<AllocationFramework> conclusionFrameworks = allocation.AllocationFrameworks.Where((AllocationFramework x) => x.Framework != null && !IsNumberOnly(x.Framework.InstitutionSymbol)).ToList();
		List<AllocationClass> classes = allocation.AllocationClasses.Where((AllocationClass x) => x.SchoolClass != null && IsNumberOnly(x.SchoolClass.Description)).ToList();
		List<AllocationClass> conclusionClasses = allocation.AllocationClasses.Where((AllocationClass x) => x.SchoolClass != null && !IsNumberOnly(x.SchoolClass.Description)).ToList();
		if (institutionFrameworks.Any() && !institutionFrameworks.Any((AllocationFramework x) => x.FrameworkId == row2.FrameworkId))
		{
			result.AddError("המסגרת אינה תואמת להקצאת העובד");
		}
		if (allocation.AllocationEducationalPrograms.Any() && !allocation.AllocationEducationalPrograms.Any((AllocationEducationalProgram x) => x.EducationalProgramId == row2.EducationalProgramId))
		{
			result.AddError("התוכנית החינוכית אינה תואמת להקצאת העובד");
		}
		if (allocation.AllocationDomains.Any() && !allocation.AllocationDomains.Any((AllocationDomain x) => x.DomainId == row2.DomainId))
		{
			result.AddError("התחום אינו תואם להקצאת העובד");
		}
		if (allocation.AllocationSubjects.Any() && !allocation.AllocationSubjects.Any((AllocationSubject x) => x.SubjectId == row2.Subject1Id))
		{
			result.AddError("נושא 1 אינו תואם להקצאת העובד");
		}
		if (row2.Subject2Id.HasValue && allocation.AllocationSubjects.Any() && !allocation.AllocationSubjects.Any((AllocationSubject x) => x.SubjectId == row2.Subject2Id.Value))
		{
			result.AddError("נושא 2 אינו תואם להקצאת העובד");
		}
		if (row2.DiscussionCodeId.HasValue && allocation.AllocationDiscussionCodes.Any() && !allocation.AllocationDiscussionCodes.Any((AllocationDiscussionCode x) => x.DiscussionCodeId == row2.DiscussionCodeId.Value))
		{
			result.AddError("קוד הדיון אינו תואם להקצאת העובד");
		}
		if (row2.ClassId.HasValue && classes.Any() && !classes.Any((AllocationClass x) => x.ClassId == row2.ClassId.Value))
		{
			result.AddError("הכיתה אינה תואמת להקצאת העובד");
		}
		if (row2.GradeLevelId.HasValue && allocation.AllocationGradeLevels.Any() && !allocation.AllocationGradeLevels.Any((AllocationGradeLevel x) => x.GradeLevelId == row2.GradeLevelId.Value))
		{
			result.AddError("השכבה אינה תואמת להקצאת העובד");
		}
		if (row2.ConclusionClassId.HasValue && conclusionClasses.Any() && !conclusionClasses.Any((AllocationClass x) => x.ClassId == row2.ConclusionClassId.Value))
		{
			result.AddError("מסקנה - כיתה אינה תואמת להקצאת העובד");
		}
		if (row2.ConclusionFrameworkId.HasValue && conclusionFrameworks.Any() && !conclusionFrameworks.Any((AllocationFramework x) => x.FrameworkId == row2.ConclusionFrameworkId.Value))
		{
			result.AddError("מסקנה - מסגרת אינה תואמת להקצאת העובד");
		}
		if (row2.ConclusionLocationId.HasValue && allocation.AllocationLocalityDistrictNationals.Any() && !allocation.AllocationLocalityDistrictNationals.Any((AllocationLocalityDistrictNational x) => x.LocalityDistrictNationalId == row2.ConclusionLocationId.Value))
		{
			result.AddError("מסקנה - מיקום אינה תואמת להקצאת העובד");
		}
	}

	private static bool IsNumberOnly(string? value)
	{
		return int.TryParse(value?.Trim(), out var _);
	}

	private async Task ValidateDailyLimitAsync(ValidationResult result, ReportRow row, DateTime meetingDate, List<ReportRow> allRowsInReport)
	{
		ReportRow row2 = row;
		IEnumerable<ReportRow> rowsInDailyScope = allRowsInReport.Where((ReportRow r) => r != row2 && r.MeetingDate.Date == meetingDate && r.AllocationId == row2.AllocationId);
		decimal? num = null;
		if (row2.AllocationId.HasValue)
		{
			Allocation allocation = await GetSimpleAllocationAsync(row2.AllocationId.Value);
			if (allocation != null && !allocation.DailyEmploymentScope.HasValue)
			{
				return;
			}
			num = allocation?.DailyEmploymentScope;
		}
		if (!num.HasValue)
		{
			num = (decimal.TryParse((await _db.SystemConstants.FirstOrDefaultAsync((SystemConstant c) => c.Key == "MaxDailyHoursDefault"))?.Value, out var result2) ? result2 : 9m);
		}
		if (rowsInDailyScope.Sum((ReportRow r) => r.MeetingDuration) + row2.MeetingDuration > num.Value)
		{
			result.AddError($"חריגה ממשך תפוקה יומי (מותר: {num.Value})");
		}
	}

	private async Task ValidateMonthlyDurationLimitAsync(ValidationResult result, ReportRow row, List<ReportRow> allRowsInReport)
	{
		ReportRow row2 = row;
		if (row2.AllocationId.HasValue)
		{
			Allocation allocation = await GetSimpleAllocationAsync(row2.AllocationId.Value);
			if (allocation != null && allocation.MonthlyEmploymentScope.HasValue && allRowsInReport.Where((ReportRow r) => r.AllocationId == row2.AllocationId).Sum((ReportRow r) => r.MeetingDuration) > allocation.MonthlyEmploymentScope.Value)
			{
				result.AddError($"חריגה מהיקף פעילות חודשי (מותר: {allocation.MonthlyEmploymentScope.Value})");
			}
		}
	}

	private async Task ValidateOutputDurationAsync(ValidationResult result, ReportRow row)
	{
		if (!row.AllocationId.HasValue)
		{
			return;
		}
		Allocation allocation = await GetSimpleAllocationAsync(row.AllocationId.Value);
		if (string.IsNullOrWhiteSpace(allocation?.OutputDuration))
		{
			return;
		}
		string[] source = allocation.OutputDuration.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		if (source.Any((string t) => t.Equals("Unlimited", StringComparison.OrdinalIgnoreCase) || t.Equals("ללא הגבלה", StringComparison.OrdinalIgnoreCase)))
		{
			return;
		}
		decimal result2;
		HashSet<decimal> hashSet = (from t in source
			select (!decimal.TryParse(t, out result2)) ? null : new decimal?(result2) into v
			where v.HasValue
			select v.Value).ToHashSet();
		if (hashSet.Any() && !hashSet.Contains(row.MeetingDuration))
		{
			result.AddError("משך התפוקה חייב להיות אחד מהערכים שהוגדרו בהקצאה: " + string.Join(", ", hashSet.OrderBy((decimal v) => v)));
		}
	}

	private static void ValidateDuplicateRows(ValidationResult result, ReportRow row, DateTime meetingDate, List<ReportRow> allRowsInReport)
	{
		ReportRow row2 = row;
		foreach (ReportRow item in allRowsInReport.Where((ReportRow r) => r != row2 && r.AllocationId == row2.AllocationId && r.MeetingDate.Date == meetingDate && r.DistrictId == row2.DistrictId && r.LocalityId == row2.LocalityId && r.FrameworkId == row2.FrameworkId && r.EducationalProgramId == row2.EducationalProgramId && r.DomainId == row2.DomainId && r.Subject1Id == row2.Subject1Id && r.Subject2Id == row2.Subject2Id && r.DiscussionCodeId == row2.DiscussionCodeId && r.ConclusionClassId == row2.ConclusionClassId && r.ConclusionFrameworkId == row2.ConclusionFrameworkId && r.ConclusionLocationId == row2.ConclusionLocationId && r.GradeLevelId == row2.GradeLevelId && r.ClassId == row2.ClassId))
		{
			if (string.IsNullOrEmpty(row2.Notes) && string.IsNullOrEmpty(item.Notes))
			{
				result.AddError("שורה כפולה: אותם ערכים ואין הערות");
				break;
			}
			if (!string.IsNullOrEmpty(row2.Notes) && row2.Notes == item.Notes)
			{
				result.AddError("שורה כפולה: אותם ערכים והערות זהות");
				break;
			}
		}
	}

	private async Task ValidateNotesSimilarityAsync(ValidationResult result, ReportRow row, List<ReportRow> allRowsInReport)
	{
		ReportRow row2 = row;
		if (string.IsNullOrEmpty(row2.Notes))
		{
			return;
		}
		return;
		double result2;
		double num = (double.TryParse((await _db.SystemConstants.FirstOrDefaultAsync((SystemConstant c) => c.Key == "NotesSimilarityThresholdPercent"))?.Value, out result2) ? result2 : 90.0);
		foreach (ReportRow item in allRowsInReport.Where((ReportRow r) => r != row2 && !string.IsNullOrEmpty(r.Notes)))
		{
			double num2 = NotesSimilarity(row2.Notes, item.Notes);
			if (num2 >= num)
			{
				result.AddError($"הערות דומות מדי לשורה אחרת בדיווח ({num2:F0}% דמיון)");
				break;
			}
		}
	}

	private async Task<HashSet<string>> GetRequiredFieldsAsync()
	{
		if (_requiredFieldsCache != null)
		{
			return _requiredFieldsCache;
		}
		string text = await (from c in _db.SystemConstants
			where c.Key == "RequiredReportFields"
			select c.Value).FirstOrDefaultAsync();
		if (string.IsNullOrWhiteSpace(text))
		{
			_requiredFieldsCache = new HashSet<string>(DefaultRequiredFields, StringComparer.OrdinalIgnoreCase);
			return _requiredFieldsCache;
		}
		_requiredFieldsCache = text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet<string>(StringComparer.OrdinalIgnoreCase);
		return _requiredFieldsCache;
	}

	private async Task<Allocation> GetSimpleAllocationAsync(int allocationId)
	{
		if (!_allocationSimpleCache.TryGetValue(allocationId, out Allocation allocation))
		{
			allocation = await _db.Allocations.FindAsync(allocationId);
			if (allocation != null)
			{
				_allocationSimpleCache[allocationId] = allocation;
			}
		}
		return allocation;
	}

	private static double NotesSimilarity(string a, string b)
	{
		int num = LevenshteinDistance(a, b);
		int num2 = Math.Max(a.Length, b.Length);
		if (num2 == 0)
		{
			return 100.0;
		}
		return (1.0 - (double)num / (double)num2) * 100.0;
	}

	private static int LevenshteinDistance(string s, string t)
	{
		int length = s.Length;
		int length2 = t.Length;
		int[,] array = new int[length + 1, length2 + 1];
		for (int i = 0; i <= length; i++)
		{
			array[i, 0] = i;
		}
		for (int j = 0; j <= length2; j++)
		{
			array[0, j] = j;
		}
		for (int k = 1; k <= length; k++)
		{
			for (int l = 1; l <= length2; l++)
			{
				array[k, l] = Math.Min(Math.Min(array[k - 1, l] + 1, array[k, l - 1] + 1), array[k - 1, l - 1] + ((s[k - 1] != t[l - 1]) ? 1 : 0));
			}
		}
		return array[length, length2];
	}
}

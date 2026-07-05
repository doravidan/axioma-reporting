using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class ReportExcelImportService : IReportExcelImportService
{
	private readonly AppDbContext _db;

	private readonly IReportValidationService _validator;

	private readonly IReportStatusService _statusService;

	private readonly IEmailService _emailService;

	private readonly ILookupResolver _lookupResolver;

	public ReportExcelImportService(AppDbContext db, IReportValidationService validator, IReportStatusService statusService, IEmailService emailService, ILookupResolver? lookupResolver = null)
	{
		_db = db;
		_validator = validator;
		_statusService = statusService;
		_emailService = emailService;
		_lookupResolver = lookupResolver ?? new LookupResolver(db);
	}

	public async Task<ExcelImportResult> ImportAsync(int reportId, int allocationId, Stream stream, int currentUserId)
	{
		ExcelImportResult result = new ExcelImportResult();
		Report report = await _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report?.User == null || report.ReportingMonth == null)
		{
			result.Errors.Add("הדיווח לא נמצא");
			return result;
		}
		int statusId = report.StatusId;
		if ((uint)(statusId - 3) <= 1u)
		{
			result.Errors.Add("לא ניתן לייבא אקסל לדיווח שממתין לאישור או אושר");
			return result;
		}
		Allocation allocation = await _db.Allocations.FirstOrDefaultAsync((Allocation a) => a.Id == allocationId && a.UserId == report.UserId && a.IsActive);
		if (allocation == null || !allocation.AllowExcelUpload)
		{
			result.Errors.Add("הקצאה זו אינה מאפשרת העלאת אקסל");
			return result;
		}
		using XLWorkbook workbook = new XLWorkbook(stream);
		IXLWorksheet ws = workbook.Worksheets.FirstOrDefault();
		if (ws == null)
		{
			result.Errors.Add("קובץ האקסל אינו מכיל גיליון");
			return result;
		}
		List<ReportRow> importedRows = new List<ReportRow>();
		int lastRow = ws.LastRowUsed()?.RowNumber() ?? 0;
		int num = FindClientHebrewHeaderRow(ws);
		bool clientHebrewFormat = num > 0;
		Dictionary<string, int> clientHebrewHeaderMap = clientHebrewFormat ? BuildClientHebrewHeaderMap(ws.Row(num)) : new Dictionary<string, int>();
		int num2 = (clientHebrewFormat ? (num + 1) : 2);
		for (int rowNumber = num2; rowNumber <= lastRow; rowNumber++)
		{
			try
			{
				IXLRow iXLRow = ws.Row(rowNumber);
				if (IsEmptyDataRow(iXLRow, clientHebrewFormat) || (clientHebrewFormat && IsClientHebrewHeaderRow(iXLRow)))
				{
					continue;
				}
				if (clientHebrewFormat && !IsRowForReportEmployee(iXLRow, report.User, clientHebrewHeaderMap))
				{
					string @string = iXLRow.Cell(GetColumn(clientHebrewHeaderMap, "EmployeeCode", 2)).GetString();
					result.Errors.Add($"שורה {rowNumber}: קוד העובד בקובץ ({@string}) אינו תואם לעובד שעבורו הועלה הדיווח ({report.User.EmployeeCode})");
					continue;
				}
				ReportRow reportRow = ((!clientHebrewFormat) ? ParseRow(iXLRow, reportId, allocationId) : (await ParseClientHebrewRowAsync(iXLRow, reportId, allocationId, clientHebrewHeaderMap)));
				ReportRow row = reportRow;
				List<ReportRow> allRowsInReport = importedRows.Concat(new ReportRow[1] { row }).ToList();
				ValidationResult validationResult = await _validator.ValidateRowAsync(row, report.User, report.ReportingMonth, allRowsInReport);
				if (!validationResult.IsValid)
				{
					result.Errors.Add($"שורה {rowNumber}: {string.Join("; ", validationResult.Errors)}");
				}
				else
				{
					importedRows.Add(row);
				}
			}
			catch (Exception ex)
			{
				result.Errors.Add($"שורה {rowNumber}: {ex.Message}");
			}
		}
		if (result.Errors.Any())
		{
			return result;
		}
		List<ReportRow> entities = await _db.ReportRows.Where((ReportRow r) => r.ReportId == reportId && r.AllocationId == (int?)allocationId).ToListAsync();
		_db.ReportRows.RemoveRange(entities);
		int num3 = (await _db.ReportRows.Where((ReportRow r) => r.ReportId == reportId && r.AllocationId != (int?)allocationId).MaxAsync((Expression<Func<ReportRow, int?>>)((ReportRow r) => r.SequenceNumber), default(CancellationToken))).GetValueOrDefault() + 1;
		foreach (ReportRow item in importedRows)
		{
			item.SequenceNumber = num3++;
			item.CreatedAt = DateTime.UtcNow;
			_db.ReportRows.Add(item);
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
		if (report.User != null && report.ReportingMonth != null && !string.IsNullOrWhiteSpace(report.User.Email))
		{
			await _emailService.SendAsync(report.User.Email, report.User.FirstName + " " + report.User.LastName, "ReportReceived", new Dictionary<string, string>
			{
				["EmployeeName"] = report.User.FirstName + " " + report.User.LastName,
				["MonthName"] = report.ReportingMonth.Description,
				["Month"] = report.ReportingMonth.Month.ToString(),
				["Year"] = report.ReportingMonth.Year.ToString(),
				["DeadlineDate"] = report.ReportingMonth.LastReportingDate.ToString("dd/MM/yyyy"),
				["Deadline"] = report.ReportingMonth.LastReportingDate.ToString("dd/MM/yyyy")
			});
		}
	}

	private static int FindClientHebrewHeaderRow(IXLWorksheet ws)
	{
		int num = Math.Min(15, ws.LastRowUsed()?.RowNumber() ?? 0);
		for (int i = 1; i <= num; i++)
		{
			if (IsClientHebrewHeaderRow(ws.Row(i)))
			{
				return i;
			}
		}
		return 0;
	}

	private static bool IsClientHebrewHeaderRow(IXLRow row)
	{
		IXLRow row2 = row;
		int count = Math.Min(25, row2.LastCellUsed()?.Address.ColumnNumber ?? 25);
		string text = NormalizeHebrewHeader(string.Join("|", from c in Enumerable.Range(1, count)
			select row2.Cell(c).GetString()));
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}
		int num = 0;
		if (text.Contains("קוד עובד"))
		{
			num++;
		}
		if (text.Contains("מחוז"))
		{
			num++;
		}
		if (text.Contains("תאריך"))
		{
			num++;
		}
		if (text.Contains("משך"))
		{
			num++;
		}
		if (text.Contains("יישוב") || text.Contains("ישוב"))
		{
			num++;
		}
		if (text.Contains("מסגרת"))
		{
			num++;
		}
		if (text.Contains("תוכנית חינוכית") || text.Contains("תכנית חינוכית"))
		{
			num++;
		}
		if (text.Contains("תחום"))
		{
			num++;
		}
		if (text.Contains("נושא"))
		{
			num++;
		}
		if (num >= 4 && text.Contains("מחוז"))
		{
			return text.Contains("תאריך");
		}
		return false;
	}

	private static string NormalizeHebrewHeader(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return string.Empty;
		}
		return value.Replace('\u00a0', ' ').Replace("\"", string.Empty).Trim()
			.ToLowerInvariant();
	}

	private static Dictionary<string, int> BuildClientHebrewHeaderMap(IXLRow row)
	{
		Dictionary<string, int> map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
		int count = Math.Min(40, row.LastCellUsed()?.Address.ColumnNumber ?? 40);
		for (int column = 1; column <= count; column++)
		{
			string header = NormalizeHebrewHeader(row.Cell(column).GetString());
			if (string.IsNullOrWhiteSpace(header))
			{
				continue;
			}
			if (HeaderContains(header, "קוד עובד"))
			{
				SetHeader(map, "EmployeeCode", column);
			}
			else if (HeaderContains(header, "שם המדווח"))
			{
				SetHeader(map, "ReporterName", column);
			}
			else if (HeaderContains(header, "סוג דיווח"))
			{
				SetHeader(map, "ReportType", column);
			}
			else if (HeaderContains(header, "יישוב/מחוז/ארצי") || HeaderContains(header, "ישוב/מחוז/ארצי"))
			{
				SetHeader(map, "ConclusionLocation", column);
			}
			else if (HeaderContains(header, "יישוב") || HeaderContains(header, "ישוב"))
			{
				SetHeader(map, "Locality", column);
			}
			else if (HeaderContains(header, "מחוז"))
			{
				SetHeader(map, "District", column);
			}
			else if (HeaderContains(header, "תאריך"))
			{
				SetHeader(map, "MeetingDate", column);
			}
			else if (HeaderContains(header, "משך"))
			{
				SetHeader(map, "MeetingDuration", column);
			}
			else if (HeaderContains(header, "תוכנית חינוכית") || HeaderContains(header, "תכנית חינוכית"))
			{
				SetHeader(map, "EducationalProgram", column);
			}
			else if (HeaderContains(header, "תחום"))
			{
				SetHeader(map, "Domain", column);
			}
			else if (HeaderContains(header, "נושא 2") || HeaderContains(header, "נושא2"))
			{
				SetHeader(map, "Subject2", column);
			}
			else if (HeaderContains(header, "נושא 1") || HeaderContains(header, "נושא1") || HeaderContains(header, "נושא"))
			{
				SetHeader(map, "Subject1", column);
			}
			else if (HeaderContains(header, "קיום דיון") || HeaderContains(header, "קוד דיון"))
			{
				SetHeader(map, "DiscussionCode", column);
			}
			else if (HeaderContains(header, "מסגרת"))
			{
				if (!map.ContainsKey("Framework"))
				{
					map["Framework"] = column;
				}
				else
				{
					SetHeader(map, "ConclusionFramework", column);
				}
			}
			else if (HeaderContains(header, "שכבה"))
			{
				SetHeader(map, "GradeLevel", column);
			}
			else if (HeaderContains(header, "כיתה"))
			{
				if (!map.ContainsKey("ConclusionClass"))
				{
					map["ConclusionClass"] = column;
				}
				else
				{
					SetHeader(map, "Class", column);
				}
			}
			else if (HeaderContains(header, "הערות"))
			{
				SetHeader(map, "Notes", column);
			}
		}
		return map;
	}

	private static void SetHeader(Dictionary<string, int> map, string key, int column)
	{
		if (!map.ContainsKey(key))
		{
			map[key] = column;
		}
	}

	private static bool HeaderContains(string header, string value)
	{
		return header.Contains(NormalizeHebrewHeader(value), StringComparison.Ordinal);
	}

	private static int GetColumn(Dictionary<string, int> map, string key, int fallbackColumn)
	{
		return map.TryGetValue(key, out var column) ? column : fallbackColumn;
	}

	private static int? GetOptionalColumn(Dictionary<string, int> map, string key, int fallbackColumn)
	{
		int column = GetColumn(map, key, fallbackColumn);
		return column > 0 ? column : null;
	}

	private static bool IsEmptyDataRow(IXLRow row, bool clientHebrewFormat)
	{
		IXLRow row2 = row;
		return (clientHebrewFormat ? Enumerable.Range(2, 18) : Enumerable.Range(1, 16)).All((int c) => row2.Cell(c).IsEmpty() || string.IsNullOrWhiteSpace(row2.Cell(c).GetString()));
	}

	private static bool IsRowForReportEmployee(IXLRow row, User employee, Dictionary<string, int>? headerMap = null)
	{
		string text = row.Cell(headerMap == null ? 2 : GetColumn(headerMap, "EmployeeCode", 2)).GetString().Trim();
		if (string.IsNullOrWhiteSpace(text))
		{
			return true;
		}
		if (!string.Equals(text, employee.EmployeeCode, StringComparison.OrdinalIgnoreCase))
		{
			return string.Equals(text, employee.IdNumber, StringComparison.OrdinalIgnoreCase);
		}
		return true;
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

	private async Task<ReportRow> ParseClientHebrewRowAsync(IXLRow row, int reportId, int allocationId, Dictionary<string, int> headerMap)
	{
		return await ParseClientHebrewRowByHeaderAsync(row, reportId, allocationId, headerMap);
		if (LooksLikeShiftedClientHebrewRow(row))
		{
			return await ParseShiftedClientHebrewRowAsync(row, reportId, allocationId);
		}
		ReportRow reportRow = new ReportRow
		{
			ReportId = reportId,
			AllocationId = allocationId,
			MeetingDate = ReadDate(row, 8),
			MeetingDuration = ReadDecimal(row, 9)
		};
		ReportRow reportRow2 = reportRow;
		reportRow2.DistrictId = await ResolveRequiredAsync(row, 4, "מחוז", _lookupResolver.ResolveDistrictAsync);
		ReportRow reportRow3 = reportRow;
		reportRow3.LocalityId = await ResolveRequiredAsync(row, 5, "יישוב", _lookupResolver.ResolveLocalityAsync);
		ReportRow reportRow4 = reportRow;
		reportRow4.FrameworkId = await ResolveRequiredFrameworkAsync(row, 6);
		ReportRow reportRow5 = reportRow;
		reportRow5.EducationalProgramId = await ResolveRequiredAsync(row, 9, "תוכנית חינוכית", _lookupResolver.ResolveEducationalProgramAsync);
		ReportRow reportRow6 = reportRow;
		reportRow6.DomainId = await ResolveRequiredAsync(row, 10, "תחום", _lookupResolver.ResolveDomainAsync);
		ReportRow reportRow7 = reportRow;
		reportRow7.Subject1Id = await ResolveRequiredAsync(row, 11, "נושא 1", _lookupResolver.ResolveSubjectAsync);
		ReportRow reportRow8 = reportRow;
		reportRow8.Subject2Id = await ResolveOptionalAsync(row, 13, _lookupResolver.ResolveSubjectAsync);
		ReportRow reportRow9 = reportRow;
		reportRow9.DiscussionCodeId = await ResolveOptionalAsync(row, 14, _lookupResolver.ResolveDiscussionCodeAsync);
		ReportRow reportRow10 = reportRow;
		reportRow10.ConclusionClassId = await ResolveOptionalAsync(row, 15, _lookupResolver.ResolveClassAsync);
		ReportRow reportRow11 = reportRow;
		reportRow11.ConclusionFrameworkId = await ResolveOptionalFrameworkAsync(row, 16);
		ReportRow reportRow12 = reportRow;
		reportRow12.ConclusionLocationId = await ResolveOptionalAsync(row, 17, _lookupResolver.ResolveLocalityDistrictNationalAsync);
		ReportRow reportRow13 = reportRow;
		reportRow13.GradeLevelId = await ResolveOptionalAsync(row, 18, _lookupResolver.ResolveGradeLevelAsync);
		ReportRow reportRow14 = reportRow;
		reportRow14.ClassId = await ResolveOptionalAsync(row, 19, _lookupResolver.ResolveClassAsync);
		reportRow.Notes = row.Cell(20).GetString();
		return reportRow;
	}

	private async Task<ReportRow> ParseClientHebrewRowByHeaderAsync(IXLRow row, int reportId, int allocationId, Dictionary<string, int> headerMap)
	{
		int frameworkColumn = GetColumn(headerMap, "Framework", 6);
		string frameworkText = row.Cell(frameworkColumn).GetString();
		int frameworkId = await ResolveRequiredFrameworkAsync(row, frameworkColumn);
		var location = await ResolveLocationFromFrameworkAsync(frameworkId, frameworkText, allocationId);
		int districtColumn = GetColumn(headerMap, "District", 4);
		int localityColumn = GetColumn(headerMap, "Locality", 5);
		int? districtId = await ResolveLookupAsync(row, districtColumn, _lookupResolver.ResolveDistrictAsync);
		int? localityId = await ResolveLookupAsync(row, localityColumn, _lookupResolver.ResolveLocalityAsync);
		if (!districtId.HasValue || districtId.Value <= 0)
		{
			districtId = location.DistrictId;
		}
		if (!localityId.HasValue || localityId.Value <= 0)
		{
			localityId = location.LocalityId;
		}
		if (!districtId.HasValue || districtId.Value <= 0)
		{
			throw new InvalidOperationException("מחוז לא נמצא בטבלאות המערכת: '" + row.Cell(districtColumn).GetString() + "'");
		}
		if (!localityId.HasValue || localityId.Value <= 0)
		{
			throw new InvalidOperationException("יישוב לא נמצא בטבלאות המערכת: '" + row.Cell(localityColumn).GetString() + "'");
		}
		ReportRow reportRow = new ReportRow
		{
			ReportId = reportId,
			AllocationId = allocationId,
			MeetingDate = ReadDate(row, GetColumn(headerMap, "MeetingDate", 8)),
			MeetingDuration = ReadDecimal(row, GetColumn(headerMap, "MeetingDuration", 9)),
			DistrictId = districtId.Value,
			LocalityId = localityId.Value,
			FrameworkId = frameworkId
		};
		reportRow.EducationalProgramId = await ResolveRequiredAsync(row, GetColumn(headerMap, "EducationalProgram", 9), "תוכנית חינוכית", _lookupResolver.ResolveEducationalProgramAsync);
		reportRow.DomainId = await ResolveRequiredDomainAsync(row, allocationId, GetColumn(headerMap, "Domain", 10));
		reportRow.Subject1Id = await ResolveRequiredAsync(row, GetColumn(headerMap, "Subject1", 11), "נושא 1", _lookupResolver.ResolveSubjectAsync);
		reportRow.Subject2Id = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "Subject2", 13), _lookupResolver.ResolveSubjectAsync);
		reportRow.DiscussionCodeId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "DiscussionCode", 14), _lookupResolver.ResolveDiscussionCodeAsync);
		reportRow.ConclusionClassId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "ConclusionClass", 15), _lookupResolver.ResolveClassAsync);
		reportRow.ConclusionFrameworkId = await ResolveOptionalFrameworkByColumnAsync(row, GetOptionalColumn(headerMap, "ConclusionFramework", 16));
		reportRow.ConclusionLocationId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "ConclusionLocation", 17), _lookupResolver.ResolveLocalityDistrictNationalAsync);
		reportRow.GradeLevelId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "GradeLevel", 18), _lookupResolver.ResolveGradeLevelAsync);
		reportRow.ClassId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "Class", 19), _lookupResolver.ResolveClassAsync);
		reportRow.ReportTypeId = await ResolveOptionalByColumnAsync(row, GetOptionalColumn(headerMap, "ReportType", 0), _lookupResolver.ResolveReportTypeAsync);
		reportRow.Notes = row.Cell(GetColumn(headerMap, "Notes", 20)).GetString();
		return reportRow;
	}

	private async Task<ReportRow> ParseShiftedClientHebrewRowAsync(IXLRow row, int reportId, int allocationId)
	{
		string frameworkText = row.Cell(3).GetString();
		int frameworkId = await ResolveRequiredFrameworkAsync(row, 3);
		var location = await ResolveLocationFromFrameworkAsync(frameworkId, frameworkText, allocationId);
		if (!location.DistrictId.HasValue || location.DistrictId.Value <= 0)
		{
			throw new InvalidOperationException("מחוז לא נמצא בטבלאות המערכת עבור המסגרת: '" + frameworkText + "'");
		}
		if (!location.LocalityId.HasValue || location.LocalityId.Value <= 0)
		{
			throw new InvalidOperationException("יישוב לא נמצא בטבלאות המערכת עבור המסגרת: '" + frameworkText + "'");
		}
		string notes1 = row.Cell(10).GetString().Trim();
		string notes2 = row.Cell(20).GetString().Trim();
		ReportRow reportRow = new ReportRow
		{
			ReportId = reportId,
			AllocationId = allocationId,
			MeetingDate = ReadDate(row, 8),
			MeetingDuration = ReadDecimal(row, 9),
			DistrictId = location.DistrictId.Value,
			LocalityId = location.LocalityId.Value,
			FrameworkId = frameworkId
		};
		reportRow.EducationalProgramId = await ResolveRequiredAsync(row, 7, "תוכנית חינוכית", _lookupResolver.ResolveEducationalProgramAsync);
		reportRow.DomainId = await ResolveRequiredDomainAsync(row, allocationId, 11);
		reportRow.Subject1Id = await ResolveRequiredAsync(row, 12, "נושא 1", _lookupResolver.ResolveSubjectAsync);
		reportRow.Subject2Id = await ResolveOptionalAsync(row, 13, _lookupResolver.ResolveSubjectAsync);
		reportRow.DiscussionCodeId = await ResolveOptionalAsync(row, 14, _lookupResolver.ResolveDiscussionCodeAsync);
		reportRow.ConclusionClassId = await ResolveOptionalAsync(row, 15, _lookupResolver.ResolveClassAsync);
		reportRow.ConclusionFrameworkId = await ResolveOptionalFrameworkAsync(row, 16);
		reportRow.ConclusionLocationId = await ResolveOptionalAsync(row, 17, _lookupResolver.ResolveLocalityDistrictNationalAsync);
		reportRow.GradeLevelId = await ResolveOptionalAsync(row, 18, _lookupResolver.ResolveGradeLevelAsync);
		reportRow.ClassId = await ResolveOptionalAsync(row, 19, _lookupResolver.ResolveClassAsync);
		reportRow.ReportTypeId = await ResolveOptionalAsync(row, 4, _lookupResolver.ResolveReportTypeAsync);
		reportRow.Notes = CombineNotes(notes1, notes2);
		return reportRow;
	}

	private static bool LooksLikeShiftedClientHebrewRow(IXLRow row)
	{
		string text = row.Cell(6).GetString().Trim();
		return !string.IsNullOrWhiteSpace(row.Cell(3).GetString())
			&& !string.IsNullOrWhiteSpace(row.Cell(7).GetString())
			&& text.Contains("תאריך", StringComparison.Ordinal)
			&& row.Cell(8).TryGetValue<double>(out _)
			&& row.Cell(9).TryGetValue<decimal>(out _);
	}

	private async Task<(int? DistrictId, int? LocalityId)> ResolveLocationFromFrameworkAsync(int frameworkId, string frameworkText, int allocationId)
	{
		string? symbol = await _db.Frameworks.AsNoTracking().Where((Framework f) => f.Id == frameworkId).Select((Framework f) => f.InstitutionSymbol).FirstOrDefaultAsync();
		if (string.IsNullOrWhiteSpace(symbol))
		{
			symbol = FindFirstNumberToken(frameworkText);
		}
		int? districtId = null;
		int? localityId = null;
		if (int.TryParse(symbol, out var institutionSymbol))
		{
			var institution = await (from i in _db.Institutions.AsNoTracking()
				where i.InstitutionSymbol == institutionSymbol
				orderby i.LocalityId.HasValue && i.DistrictId.HasValue descending
				select new { i.LocalityId, i.DistrictId }).FirstOrDefaultAsync();
			localityId = institution?.LocalityId;
			districtId = institution?.DistrictId;
		}
		if (!localityId.HasValue || localityId.Value <= 0)
		{
			string? localityText = FindFirstTextToken(frameworkText);
			localityId = await _lookupResolver.ResolveLocalityAsync(localityText);
		}
		if (!districtId.HasValue || districtId.Value <= 0)
		{
			List<int> districtIds = await _db.Set<AllocationDistrict>().AsNoTracking().Where((AllocationDistrict x) => x.AllocationId == allocationId).Select((AllocationDistrict x) => x.DistrictId).Distinct().ToListAsync();
			if (districtIds.Count == 1)
			{
				districtId = districtIds[0];
			}
		}
		if (!localityId.HasValue || localityId.Value <= 0)
		{
			List<int> localityIds = await _db.Set<AllocationLocality>().AsNoTracking().Where((AllocationLocality x) => x.AllocationId == allocationId).Select((AllocationLocality x) => x.LocalityId).Distinct().ToListAsync();
			if (localityIds.Count == 1)
			{
				localityId = localityIds[0];
			}
		}
		return (districtId, localityId);
	}

	private static DateTime ReadDate(IXLRow row, int column)
	{
		IXLCell iXLCell = row.Cell(column);
		if (iXLCell.TryGetValue<DateTime>(out var value))
		{
			return value.Date;
		}
		if (iXLCell.TryGetValue<double>(out var value2) && value2 > 0.0 && value2 < 2958466.0)
		{
			return DateTime.FromOADate(value2).Date;
		}
		string text = iXLCell.GetString().Trim();
		string[] formats = new string[6] { "dd/MM/yyyy", "d/M/yyyy", "dd/MM/yy", "d/M/yy", "yyyy-MM-dd", "dd.MM.yyyy" };
		if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out value))
		{
			return value.Date;
		}
		if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out value))
		{
			return value.Date;
		}
		throw new InvalidOperationException("MeetingDate אינו תאריך תקין");
	}

	private static decimal ReadDecimal(IXLRow row, int column)
	{
		if (row.Cell(column).TryGetValue<decimal>(out var value))
		{
			return value;
		}
		throw new InvalidOperationException("MeetingDuration אינו מספר תקין");
	}

	private static int ReadRequiredInt(IXLRow row, int column, string name)
	{
		if (row.Cell(column).TryGetValue<int>(out var value) && value > 0)
		{
			return value;
		}
		throw new InvalidOperationException(name + " חסר או לא תקין");
	}

	private static int? ReadOptionalInt(IXLRow row, int column)
	{
		IXLCell iXLCell = row.Cell(column);
		if (iXLCell.IsEmpty())
		{
			return null;
		}
		if (!iXLCell.TryGetValue<int>(out var value) || value <= 0)
		{
			return null;
		}
		return value;
	}

	private static async Task<int> ResolveRequiredAsync(IXLRow row, int column, string name, Func<string?, CancellationToken, Task<int?>> resolver)
	{
		string value = row.Cell(column).GetString();
		int? num = await resolver(value, default(CancellationToken));
		if ((!num.HasValue || num.Value <= 0) && TryGetClientHebrewFallbackColumn(column, out var fallbackColumn))
		{
			value = row.Cell(fallbackColumn).GetString();
			num = await resolver(value, default(CancellationToken));
		}
		if (num.HasValue && num.Value > 0)
		{
			return num.Value;
		}
		throw new InvalidOperationException(name + " לא נמצא בטבלאות המערכת: '" + value + "'");
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

	private static async Task<int?> ResolveOptionalAsync(IXLRow row, int column, Func<string?, CancellationToken, Task<int?>> resolver)
	{
		string @string = row.Cell(column).GetString();
		if (string.IsNullOrWhiteSpace(@string))
		{
			return null;
		}
		return await resolver(@string, default(CancellationToken));
	}

	private static async Task<int?> ResolveLookupAsync(IXLRow row, int column, Func<string?, CancellationToken, Task<int?>> resolver)
	{
		if (column <= 0)
		{
			return null;
		}
		string value = row.Cell(column).GetString();
		if (string.IsNullOrWhiteSpace(value))
		{
			return null;
		}
		return await resolver(value, default(CancellationToken));
	}

	private static async Task<int?> ResolveOptionalByColumnAsync(IXLRow row, int? column, Func<string?, CancellationToken, Task<int?>> resolver)
	{
		if (!column.HasValue || column.Value <= 0)
		{
			return null;
		}
		return await ResolveOptionalAsync(row, column.Value, resolver);
	}

	private async Task<int> ResolveRequiredFrameworkAsync(IXLRow row, int column)
	{
		string value = row.Cell(column).GetString();
		int? num = await ResolveFrameworkValueAsync(value);
		if (num.HasValue && num.Value > 0)
		{
			return num.Value;
		}
		throw new InvalidOperationException("מסגרת לא נמצאה בטבלאות המערכת: '" + value + "'");
	}

	private async Task<int?> ResolveOptionalFrameworkAsync(IXLRow row, int column)
	{
		string @string = row.Cell(column).GetString();
		if (string.IsNullOrWhiteSpace(@string))
		{
			return null;
		}
		return await ResolveFrameworkValueAsync(@string);
	}

	private async Task<int?> ResolveOptionalFrameworkByColumnAsync(IXLRow row, int? column)
	{
		if (!column.HasValue || column.Value <= 0)
		{
			return null;
		}
		return await ResolveOptionalFrameworkAsync(row, column.Value);
	}

	private async Task<int?> ResolveFrameworkValueAsync(string? value)
	{
		int? result = await _lookupResolver.ResolveFrameworkAsync(value);
		if (result.HasValue || string.IsNullOrWhiteSpace(value))
		{
			return result;
		}
		string value2 = FindFirstNumberToken(value) ?? string.Empty;
		return (!string.IsNullOrWhiteSpace(value2)) ? (await _lookupResolver.ResolveFrameworkAsync(value2)) : null;
	}

	private async Task<int> ResolveRequiredDomainAsync(IXLRow row, int allocationId, params int[] columns)
	{
		foreach (int column in columns)
		{
			string value = row.Cell(column).GetString();
			int? num = await _lookupResolver.ResolveDomainAsync(value);
			if (num.HasValue && num.Value > 0)
			{
				return num.Value;
			}
		}
		List<int> domainIds = await _db.Set<AllocationDomain>().AsNoTracking().Where((AllocationDomain x) => x.AllocationId == allocationId).Select((AllocationDomain x) => x.DomainId).Distinct().ToListAsync();
		if (domainIds.Count == 1)
		{
			return domainIds[0];
		}
		string value2 = string.Join("' / '", columns.Select((int c) => row.Cell(c).GetString()).Where((string v) => !string.IsNullOrWhiteSpace(v)));
		throw new InvalidOperationException("תחום לא נמצא בטבלאות המערכת: '" + value2 + "'");
	}

	private static string? FindFirstNumberToken(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return null;
		}
		string? token = value.Split(new char[2] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault((string part) => part.All(char.IsDigit));
		return string.IsNullOrWhiteSpace(token) ? null : token;
	}

	private static string? FindFirstTextToken(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return null;
		}
		string? token = value.Split(new char[2] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault((string part) => part.Any(char.IsLetter));
		return string.IsNullOrWhiteSpace(token) ? null : token;
	}

	private static string? CombineNotes(string? first, string? second)
	{
		if (string.IsNullOrWhiteSpace(first))
		{
			return string.IsNullOrWhiteSpace(second) ? null : second;
		}
		if (string.IsNullOrWhiteSpace(second))
		{
			return first;
		}
		return first + Environment.NewLine + second;
	}
}

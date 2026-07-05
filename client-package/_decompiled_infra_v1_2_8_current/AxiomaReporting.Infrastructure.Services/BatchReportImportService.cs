using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class BatchReportImportService : IBatchReportImportService
{
	private readonly AppDbContext _db;

	private readonly ILookupResolver _resolver;

	private readonly IReportValidationService _validator;

	private readonly IEmailService _emailService;

	private static readonly Dictionary<string, string[]> HeaderAliases = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
	{
		["EmployeeCode"] = new string[1] { "קוד עובד" },
		["ReporterName"] = new string[1] { "שם המדווח" },
		["ReportType"] = new string[1] { "סוג דיווח" },
		["District"] = new string[2] { "מחוז מאשר", "מחוז" },
		["Locality"] = new string[2] { "יישוב", "ישוב" },
		["Framework"] = new string[2] { "שם המסגרת חינוכית", "מסגרת חינוכית" },
		["MeetingDate"] = new string[3] { "תאריך המפגש", "תאריך מפגש", "תאריך" },
		["MeetingDuration"] = new string[2] { "משך המפגש", "משך המפגש (בשעות)" },
		["EducationalProgram"] = new string[1] { "תוכנית חינוכית" },
		["Domain"] = new string[1] { "תחום" },
		["Subject1"] = new string[2] { "נושא 1", "נושא1" },
		["Subject2"] = new string[2] { "נושא 2", "נושא2" },
		["DiscussionCode"] = new string[2] { "קיום דיון", "קוד דיון" },
		["ConclusionFramework"] = new string[1] { "מסגרת חינוכית" },
		["ConclusionLocation"] = new string[2] { "יישוב/מחוז/ארצי", "ישוב/מחוז/ארצי" },
		["GradeLevel"] = new string[1] { "שכבה" },
		["Class"] = new string[1] { "כיתה" },
		["Notes"] = new string[1] { "הערות" }
	};

	public BatchReportImportService(AppDbContext db, ILookupResolver resolver, IReportValidationService validator, IEmailService emailService)
	{
		_db = db;
		_resolver = resolver;
		_validator = validator;
		_emailService = emailService;
	}

	public async Task<BatchImportResult> ImportAsync(Stream xlsxStream, int reportingMonthId, int uploaderUserId, CancellationToken ct = default(CancellationToken), string? progressId = null)
	{
		BatchImportResult result = new BatchImportResult();
		ReportingMonth month = await _db.ReportingMonths.FindAsync(new object[1] { reportingMonthId }, ct);
		if (month == null)
		{
			result.Errors.Add(new BatchImportError
			{
				FileRowNumber = 0,
				ErrorMessage = "חודש הדיווח לא נמצא"
			});
			result.ErrorRowsCount = 1;
			return result;
		}
		using XLWorkbook workbook = new XLWorkbook(xlsxStream);
		HashSet<int> preExistingReportUsers = new HashSet<int>(await (from rep in _db.Reports
			where rep.ReportingMonthId == reportingMonthId
			select rep.UserId).ToListAsync(ct));
		Dictionary<int, List<(ReportRow Row, int FileRow, string EmployeeCode, string ReporterName, string AllocationLabel)>> pendingByUser = new Dictionary<int, List<(ReportRow, int, string, string, string)>>();
		Dictionary<string, (int UserId, string ReporterName, int Count)> rejectionsByUser = new Dictionary<string, (int, string, int)>();
		Dictionary<string, User?> usersByCode = new Dictionary<string, User?>(StringComparer.OrdinalIgnoreCase);
		Dictionary<int, List<ReportRow>> existingRowsByUser = new Dictionary<int, List<ReportRow>>();
		Dictionary<int, int> importableRowsByHeaderRow = new Dictionary<int, int>();
		int progressTotalRows = CountImportableRows(workbook, importableRowsByHeaderRow);
		int progressProcessedRows = 0;
		BatchImportProgressStore.Start(progressId, progressTotalRows);
		foreach (IXLWorksheet sheet in workbook.Worksheets)
		{
			int num = FindHeaderRow(sheet);
			if (num < 0)
			{
				continue;
			}
			Dictionary<string, int> headerMap = BuildHeaderMap(sheet, num);
			if (!headerMap.ContainsKey("EmployeeCode"))
			{
				continue;
			}
			int lastRow = sheet.LastRowUsed()?.RowNumber() ?? num;
			for (int r = num + 1; r <= lastRow; r++)
			{
				ct.ThrowIfCancellationRequested();
				if (IsRowEmpty(sheet, r, headerMap))
				{
					continue;
				}
				progressProcessedRows++;
				result.TotalRowsRead++;
				BatchImportProgressStore.Update(progressId, progressProcessedRows, progressTotalRows);
				string rawEmpCode = GetCellString(sheet, r, headerMap, "EmployeeCode");
				string reporterName = GetCellString(sheet, r, headerMap, "ReporterName");
				string raw = BuildRawPreview(sheet, r, headerMap);
				if (string.IsNullOrWhiteSpace(rawEmpCode))
				{
					result.Errors.Add(new BatchImportError
					{
						FileRowNumber = r,
						ReporterName = reporterName,
						ErrorMessage = "חסר קוד עובד בשורה",
						RawValues = raw
					});
					AddRowResult(result, r, null, reporterName, BatchImportRowOutcome.Rejected, $"שורה {r}: נדחה — חסר קוד עובד");
					continue;
				}
				if (!usersByCode.TryGetValue(rawEmpCode, out User user))
				{
					user = await _db.Users.Include((User u) => u.Allocations).ThenInclude((Allocation a) => a.AllocationDistricts).Include((User u) => u.Allocations)
						.ThenInclude((Allocation a) => a.AllocationLocalities)
						.Include((User u) => u.Allocations)
						.ThenInclude((Allocation a) => a.AllocationFrameworks)
						.ThenInclude((AllocationFramework af) => af.Framework)
						.Include((User u) => u.Allocations)
						.ThenInclude((Allocation a) => a.AllocationEducationalPrograms)
						.AsSplitQuery()
						.FirstOrDefaultAsync((User u) => u.EmployeeCode == rawEmpCode, ct);
					usersByCode[rawEmpCode] = user;
				}
				if (user == null)
				{
					result.Errors.Add(new BatchImportError
					{
						FileRowNumber = r,
						EmployeeCode = rawEmpCode,
						ReporterName = reporterName,
						ErrorMessage = "עובד עם קוד " + rawEmpCode + " לא נמצא במערכת",
						RawValues = raw
					});
					AddRowResult(result, r, rawEmpCode, reporterName, BatchImportRowOutcome.Rejected, $"שורה {r}: נדחה — אין הקצאה תואמת");
					continue;
				}
				string displayName = (string.IsNullOrWhiteSpace(reporterName) ? (user.FirstName + " " + user.LastName).Trim() : reporterName);
				if (!TryReadMeetingDate(sheet, r, headerMap, out DateTime meetingDate, out string error))
				{
					AddError(result, r, rawEmpCode, displayName, error, raw);
					Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
					AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected, $"שורה {r}: נדחה — {error}");
					continue;
				}
				if (!TryReadDecimal(sheet, r, headerMap, "MeetingDuration", out var duration))
				{
					AddError(result, r, rawEmpCode, displayName, "משך המפגש אינו מספר תקין", raw);
					Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
					AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected, $"שורה {r}: נדחה — משך המפגש אינו מספר תקין");
					continue;
				}
				string districtText = GetCellString(sheet, r, headerMap, "District");
				string localityText = GetCellString(sheet, r, headerMap, "Locality");
				string frameworkText = GetCellString(sheet, r, headerMap, "Framework");
				string eduProgramText = GetCellString(sheet, r, headerMap, "EducationalProgram");
				string domainText = GetCellString(sheet, r, headerMap, "Domain");
				string subject1Text = GetCellString(sheet, r, headerMap, "Subject1");
				string subject2Text = GetCellString(sheet, r, headerMap, "Subject2");
				string discussionCodeText = GetCellString(sheet, r, headerMap, "DiscussionCode");
				string conclusionFrameworkText = GetCellString(sheet, r, headerMap, "ConclusionFramework");
				string conclusionLocationText = GetCellString(sheet, r, headerMap, "ConclusionLocation");
				string gradeLevelText = GetCellString(sheet, r, headerMap, "GradeLevel");
				string classText = GetCellString(sheet, r, headerMap, "Class");
				string reportTypeText = GetCellString(sheet, r, headerMap, "ReportType");
				string notes = GetCellString(sheet, r, headerMap, "Notes");
				int? districtId = await _resolver.ResolveDistrictAsync(districtText, ct);
				int? localityId = await _resolver.ResolveLocalityAsync(localityText, ct);
				int? frameworkId = await _resolver.ResolveFrameworkAsync(frameworkText, ct);
				int? eduProgramId = await _resolver.ResolveEducationalProgramAsync(eduProgramText, ct);
				int? domainId = await _resolver.ResolveDomainAsync(domainText, ct);
				int? subject1Id = await _resolver.ResolveSubjectAsync(subject1Text, ct);
				int? subject2Id = await _resolver.ResolveSubjectAsync(subject2Text, ct);
				int? discussionCodeId = await _resolver.ResolveDiscussionCodeAsync(discussionCodeText, ct);
				int? conclusionFrameworkId = await _resolver.ResolveFrameworkAsync(conclusionFrameworkText, ct);
				int? conclusionLocationId = await _resolver.ResolveLocalityDistrictNationalAsync(conclusionLocationText, ct);
				int? gradeLevelId = await _resolver.ResolveGradeLevelAsync(gradeLevelText, ct);
				int? classId = await _resolver.ResolveClassAsync(classText, ct);
				int? reportTypeId = await _resolver.ResolveReportTypeAsync(reportTypeText, ct);
				List<string> list = new List<string>();
				if (!string.IsNullOrWhiteSpace(districtText) && !districtId.HasValue)
				{
					list.Add("מחוז '" + districtText + "' לא קיים במערכת");
				}
				if (!string.IsNullOrWhiteSpace(localityText) && !localityId.HasValue)
				{
					list.Add("יישוב '" + localityText + "' לא קיים במערכת");
				}
				if (!string.IsNullOrWhiteSpace(frameworkText) && !frameworkId.HasValue)
				{
					list.Add("מסגרת חינוכית '" + frameworkText + "' לא קיימת במערכת");
				}
				if (!string.IsNullOrWhiteSpace(eduProgramText) && !eduProgramId.HasValue)
				{
					list.Add("תוכנית חינוכית '" + eduProgramText + "' לא קיימת במערכת");
				}
				if (!string.IsNullOrWhiteSpace(domainText) && !domainId.HasValue)
				{
					list.Add("תחום '" + domainText + "' לא קיים במערכת");
				}
				if (!string.IsNullOrWhiteSpace(subject1Text) && !subject1Id.HasValue)
				{
					list.Add("נושא 1 '" + subject1Text + "' לא קיים במערכת");
				}
				if (!string.IsNullOrWhiteSpace(subject2Text) && !subject2Id.HasValue)
				{
					list.Add("נושא 2 '" + subject2Text + "' לא קיים במערכת");
				}
				if (!string.IsNullOrWhiteSpace(reportTypeText) && !reportTypeId.HasValue)
				{
					list.Add("סוג דיווח '" + reportTypeText + "' לא קיים במערכת");
				}
				if (list.Any())
				{
					AddError(result, r, rawEmpCode, displayName, string.Join("; ", list), raw);
					Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
					AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected, $"שורה {r}: נדחה — אין הקצאה תואמת");
					continue;
				}
				Allocation allocation = ResolveAllocation(user, districtId, localityId, frameworkId, eduProgramId);
				if (allocation == null)
				{
					AddError(result, r, rawEmpCode, displayName, $"לא ניתן לקבוע הקצאה ייחודית לעובד {rawEmpCode} בשורה {r}", raw);
					Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
					AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected, $"שורה {r}: נדחה — אין הקצאה תואמת");
					continue;
				}
				ReportRow candidate = new ReportRow
				{
					AllocationId = allocation.Id,
					MeetingDate = meetingDate,
					MeetingDuration = duration,
					DistrictId = districtId.GetValueOrDefault(),
					LocalityId = localityId.GetValueOrDefault(),
					FrameworkId = frameworkId.GetValueOrDefault(),
					EducationalProgramId = eduProgramId.GetValueOrDefault(),
					DomainId = domainId.GetValueOrDefault(),
					Subject1Id = subject1Id.GetValueOrDefault(),
					Subject2Id = subject2Id,
					DiscussionCodeId = discussionCodeId,
					ConclusionFrameworkId = conclusionFrameworkId,
					ConclusionLocationId = conclusionLocationId,
					GradeLevelId = gradeLevelId,
					ClassId = classId,
					ReportTypeId = reportTypeId,
					Notes = (string.IsNullOrWhiteSpace(notes) ? null : notes)
				};
				if (!existingRowsByUser.TryGetValue(user.Id, out List<ReportRow> list2))
				{
					list2 = (await _db.Reports.Include((Report rep) => rep.ReportRows).FirstOrDefaultAsync((Report rep) => rep.UserId == user.Id && rep.ReportingMonthId == reportingMonthId, ct))?.ReportRows.ToList() ?? new List<ReportRow>();
					existingRowsByUser[user.Id] = list2;
				}
				else
				{
					list2 = list2.ToList();
				}
				if (pendingByUser.TryGetValue(user.Id, out List<(ReportRow, int, string, string, string)> value))
				{
					list2.AddRange(value.Select<(ReportRow, int, string, string, string), ReportRow>(((ReportRow Row, int FileRow, string EmployeeCode, string ReporterName, string AllocationLabel) q) => q.Row));
				}
				List<ReportRow> allRowsInReport = list2.Concat(new ReportRow[1] { candidate }).ToList();
				ValidationResult validationResult = await _validator.ValidateRowAsync(candidate, user, month, allRowsInReport);
				if (!validationResult.IsValid)
				{
					AddError(result, r, rawEmpCode, displayName, string.Join("; ", validationResult.Errors), raw);
					Increment(rejectionsByUser, rawEmpCode, user.Id, displayName);
					string value2 = string.Join("; ", validationResult.Errors);
					if (validationResult.Errors.Any((string e) => e.Contains("שורה כפולה", StringComparison.Ordinal)))
					{
						AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Skipped, $"שורה {r}: דולגה — דוח כפול");
					}
					else
					{
						AddRowResult(result, r, rawEmpCode, displayName, BatchImportRowOutcome.Rejected, $"שורה {r}: נדחה — {value2}");
					}
				}
				else
				{
					if (!pendingByUser.TryGetValue(user.Id, out List<(ReportRow, int, string, string, string)> value3))
					{
						value3 = new List<(ReportRow, int, string, string, string)>();
						pendingByUser[user.Id] = value3;
					}
					string item = BuildAllocationLabel(allocation);
					value3.Add((candidate, r, rawEmpCode, displayName, item));
				}
			}
		}
		foreach (KeyValuePair<int, List<(ReportRow, int, string, string, string)>> item2 in pendingByUser)
		{
			int userId = item2.Key;
			List<(ReportRow Row, int FileRow, string EmployeeCode, string ReporterName, string AllocationLabel)> rows = item2.Value;
			if (!rows.Any())
			{
				continue;
			}
			Report report = await _db.Reports.FirstOrDefaultAsync((Report rep) => rep.UserId == userId && rep.ReportingMonthId == reportingMonthId, ct);
			if (report == null)
			{
				report = new Report
				{
					UserId = userId,
					ReportingMonthId = reportingMonthId,
					StatusId = 2,
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
				if (report.StatusId == 1)
				{
					report.StatusId = 2;
				}
			}
			int num2 = (await _db.ReportRows.Where((ReportRow rr) => rr.ReportId == report.Id).MaxAsync((Expression<Func<ReportRow, int?>>)((ReportRow rr) => rr.SequenceNumber), ct)).GetValueOrDefault() + 1;
			bool flag = preExistingReportUsers.Contains(userId);
			foreach (var item3 in rows)
			{
				item3.Row.ReportId = report.Id;
				item3.Row.SequenceNumber = num2++;
				item3.Row.CreatedAt = DateTime.UtcNow;
				_db.ReportRows.Add(item3.Row);
				if (flag)
				{
					AddRowResult(result, item3.FileRow, item3.EmployeeCode, item3.ReporterName, BatchImportRowOutcome.Updated, $"שורה {item3.FileRow}: עודכן דוח קיים");
				}
				else
				{
					AddRowResult(result, item3.FileRow, item3.EmployeeCode, item3.ReporterName, BatchImportRowOutcome.Added, $"שורה {item3.FileRow}: התאמה להקצאה {item3.AllocationLabel} — שורה נוספה");
				}
			}
			await _db.SaveChangesAsync(ct);
			(ReportRow, int, string, string, string) tuple = rows.First();
			result.EmployeeSummaries.Add(new BatchImportEmployeeSummary
			{
				UserId = userId,
				EmployeeCode = tuple.Item3,
				ReporterName = tuple.Item4,
				RowsImported = rows.Count,
				RowsRejected = (rejectionsByUser.TryGetValue(tuple.Item3, out (int, string, int) value4) ? value4.Item3 : 0)
			});
			result.RowsImported += rows.Count;
		}
		foreach (KeyValuePair<string, (int, string, int)> rej in rejectionsByUser)
		{
			if (!result.EmployeeSummaries.Any((BatchImportEmployeeSummary s) => s.EmployeeCode == rej.Key))
			{
				result.EmployeeSummaries.Add(new BatchImportEmployeeSummary
				{
					UserId = rej.Value.Item1,
					EmployeeCode = rej.Key,
					ReporterName = rej.Value.Item2,
					RowsImported = 0,
					RowsRejected = rej.Value.Item3
				});
			}
		}
		result.ErrorRowsCount = result.Errors.Count;
		result.EmployeesAffected = result.EmployeeSummaries.Count((BatchImportEmployeeSummary s) => s.RowsImported > 0);
		foreach (BatchImportEmployeeSummary item4 in result.EmployeeSummaries.Where((BatchImportEmployeeSummary s) => s.UserId.HasValue && s.RowsImported > 0))
		{
			User user2 = await _db.Users.FindAsync(new object[1] { item4.UserId.Value }, ct);
			if (user2 != null && !string.IsNullOrWhiteSpace(user2.Email))
			{
				await _emailService.SendAsync(user2.Email, user2.FirstName + " " + user2.LastName, "ReportReceived", new Dictionary<string, string>
				{
					["EmployeeName"] = user2.FirstName + " " + user2.LastName,
					["MonthName"] = month.Description,
					["Month"] = month.Month.ToString(CultureInfo.InvariantCulture),
					["Year"] = month.Year.ToString(CultureInfo.InvariantCulture),
					["DeadlineDate"] = month.LastReportingDate.ToString("dd/MM/yyyy"),
					["Deadline"] = month.LastReportingDate.ToString("dd/MM/yyyy")
				}, null, ct);
			}
		}
		BatchImportProgressStore.Complete(progressId, progressProcessedRows, progressTotalRows);
		return result;
	}

	private static int CountImportableRows(XLWorkbook workbook, Dictionary<int, int> rowsByHeaderRow)
	{
		int total = 0;
		foreach (IXLWorksheet sheet in workbook.Worksheets)
		{
			int headerRow = FindHeaderRow(sheet);
			if (headerRow < 0)
			{
				continue;
			}
			Dictionary<string, int> headerMap = BuildHeaderMap(sheet, headerRow);
			if (!headerMap.ContainsKey("EmployeeCode"))
			{
				continue;
			}
			int lastRow = sheet.LastRowUsed()?.RowNumber() ?? headerRow;
			for (int r = headerRow + 1; r <= lastRow; r++)
			{
				if (!IsRowEmpty(sheet, r, headerMap))
				{
					total++;
				}
			}
		}
		return total;
	}

	private static Allocation? ResolveAllocation(User user, int? districtId, int? localityId, int? frameworkId, int? eduProgramId)
	{
		List<Allocation> list = user.Allocations.Where((Allocation a) => a.IsActive).ToList();
		if (list.Count == 0)
		{
			return null;
		}
		if (list.Count == 1)
		{
			return list[0];
		}
		List<Allocation> list2 = list.Where((Allocation a) => (!districtId.HasValue || a.AllocationDistricts.Count == 0 || a.AllocationDistricts.Any((AllocationDistrict ad) => ad.DistrictId == districtId)) && (!localityId.HasValue || a.AllocationLocalities.Count == 0 || a.AllocationLocalities.Any((AllocationLocality al) => al.LocalityId == localityId)) && MatchesFrameworkScope(a, frameworkId) && (!eduProgramId.HasValue || a.AllocationEducationalPrograms.Count == 0 || a.AllocationEducationalPrograms.Any((AllocationEducationalProgram ae) => ae.EducationalProgramId == eduProgramId))).ToList();
		if (list2.Count != 1)
		{
			return null;
		}
		return list2[0];
	}

	private static bool MatchesFrameworkScope(Allocation allocation, int? frameworkId)
	{
		if (!frameworkId.HasValue)
		{
			return true;
		}
		List<AllocationFramework> institutionFrameworks = allocation.AllocationFrameworks.Where((AllocationFramework af) => af.Framework != null && int.TryParse(af.Framework.InstitutionSymbol, out var _)).ToList();
		return institutionFrameworks.Count == 0 || institutionFrameworks.Any((AllocationFramework af) => af.FrameworkId == frameworkId);
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

	private static void AddRowResult(BatchImportResult result, int row, string? empCode, string? name, BatchImportRowOutcome outcome, string description)
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
		string value = allocation.Project?.Description;
		if (string.IsNullOrWhiteSpace(value))
		{
			return $"#{allocation.Id}";
		}
		return $"{value} (#{allocation.Id})";
	}

	private static void Increment(Dictionary<string, (int UserId, string ReporterName, int Count)> dict, string code, int userId, string name)
	{
		if (dict.TryGetValue(code, out (int, string, int) value))
		{
			dict[code] = (value.Item1, value.Item2, value.Item3 + 1);
		}
		else
		{
			dict[code] = (userId, name, 1);
		}
	}

	public static int FindHeaderRow(IXLWorksheet sheet)
	{
		int val = sheet.LastRowUsed()?.RowNumber() ?? 0;
		int num = Math.Min(15, val);
		for (int i = 1; i <= num; i++)
		{
			IXLRow iXLRow = sheet.Row(i);
			int num2 = iXLRow.LastCellUsed()?.Address.ColumnNumber ?? 0;
			for (int j = 1; j <= num2; j++)
			{
				string text = NormalizeHeader(iXLRow.Cell(j).GetString());
				if (!string.IsNullOrEmpty(text) && text.Contains("קוד עובד", StringComparison.Ordinal))
				{
					return i;
				}
			}
		}
		return -1;
	}

	private static Dictionary<string, int> BuildHeaderMap(IXLWorksheet sheet, int headerRow)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
		IXLRow iXLRow = sheet.Row(headerRow);
		int num = iXLRow.LastCellUsed()?.Address.ColumnNumber ?? 0;
		for (int i = 1; i <= num; i++)
		{
			string text = NormalizeHeader(iXLRow.Cell(i).GetString());
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			foreach (KeyValuePair<string, string[]> headerAlias in HeaderAliases)
			{
				headerAlias.Deconstruct(out var key, out var value);
				string key2 = key;
				string[] array = value;
				if (dictionary.ContainsKey(key2))
				{
					continue;
				}
				value = array;
				foreach (string value2 in value)
				{
					if (text.Contains(value2, StringComparison.OrdinalIgnoreCase))
					{
						dictionary[key2] = i;
						break;
					}
				}
			}
		}
		return dictionary;
	}

	private static string NormalizeHeader(string? text)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			return string.Empty;
		}
		char[] array = text.Trim().ToCharArray();
		StringBuilder stringBuilder = new StringBuilder(array.Length);
		bool flag = false;
		char[] array2 = array;
		foreach (char c in array2)
		{
			if (char.IsWhiteSpace(c))
			{
				if (!flag)
				{
					stringBuilder.Append(' ');
					flag = true;
				}
			}
			else
			{
				stringBuilder.Append(c);
				flag = false;
			}
		}
		return stringBuilder.ToString().Trim();
	}

	private static string GetCellString(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap, string key)
	{
		if (!headerMap.TryGetValue(key, out var value))
		{
			return string.Empty;
		}
		return sheet.Cell(row, value).GetString().Trim();
	}

	private static bool IsRowEmpty(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap)
	{
		foreach (int value in headerMap.Values)
		{
			if (!sheet.Cell(row, value).IsEmpty())
			{
				return false;
			}
		}
		return true;
	}

	private static bool TryReadDecimal(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap, string key, out decimal value)
	{
		value = default(decimal);
		if (!headerMap.TryGetValue(key, out var value2))
		{
			return false;
		}
		IXLCell iXLCell = sheet.Cell(row, value2);
		if (iXLCell.TryGetValue<decimal>(out var value3))
		{
			value = value3;
			return true;
		}
		string s = iXLCell.GetString().Trim();
		if (!decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
		{
			return decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out value);
		}
		return true;
	}

	private static bool TryReadMeetingDate(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap, out DateTime date, out string error)
	{
		date = default(DateTime);
		error = "שדה תאריך המפגש הינו חובה";
		if (!headerMap.TryGetValue("MeetingDate", out var value))
		{
			return false;
		}
		IXLCell iXLCell = sheet.Cell(row, value);
		if (iXLCell.IsEmpty())
		{
			return false;
		}
		if (iXLCell.TryGetValue<DateTime>(out var value2))
		{
			date = value2.Date;
			return true;
		}
		if (iXLCell.TryGetValue<double>(out var value3) && value3 > 0.0)
		{
			try
			{
				date = DateTime.FromOADate(value3).Date;
				return true;
			}
			catch (ArgumentException)
			{
			}
		}
		string text = iXLCell.GetString().Trim();
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		string[] formats = new string[6] { "dd/MM/yyyy", "d/M/yyyy", "dd/MM/yy", "d/M/yy", "yyyy-MM-dd", "dd.MM.yyyy" };
		if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out value2))
		{
			date = value2.Date;
			return true;
		}
		if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out value2))
		{
			date = value2.Date;
			return true;
		}
		error = "תאריך '" + text + "' אינו תקין";
		return false;
	}

	private static string BuildRawPreview(IXLWorksheet sheet, int row, Dictionary<string, int> headerMap)
	{
		List<string> list = new List<string>();
		string[] array = new string[4] { "EmployeeCode", "ReporterName", "MeetingDate", "MeetingDuration" };
		foreach (string key in array)
		{
			string cellString = GetCellString(sheet, row, headerMap, key);
			if (!string.IsNullOrWhiteSpace(cellString))
			{
				list.Add(cellString);
			}
		}
		string text = string.Join(" | ", list);
		if (text.Length <= 200)
		{
			return text;
		}
		return text.Substring(0, 200);
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Policy = "CanViewDashboard")]
public class DashboardController : Controller
{
	[CompilerGenerated]
	private static class _003C_003Eo__10
	{
		public static CallSite<Func<CallSite, object, DashboardFilter, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__1;

		public static CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__2;

		public static CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__3;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__4;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__5;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__6;

		public static CallSite<Func<CallSite, object, List<ReportingMonth>, object>> _003C_003Ep__7;

		public static CallSite<Func<CallSite, object, List<Locality>, object>> _003C_003Ep__8;

		public static CallSite<Func<CallSite, object, List<Framework>, object>> _003C_003Ep__9;

		public static CallSite<Func<CallSite, object, List<EducationalProgram>, object>> _003C_003Ep__10;

		public static CallSite<Func<CallSite, object, List<Domain>, object>> _003C_003Ep__11;

		public static CallSite<Func<CallSite, object, List<Subject>, object>> _003C_003Ep__12;

		public static CallSite<Func<CallSite, object, List<DiscussionCode>, object>> _003C_003Ep__13;

		public static CallSite<Func<CallSite, object, List<SchoolClass>, object>> _003C_003Ep__14;

		public static CallSite<Func<CallSite, object, List<GradeLevel>, object>> _003C_003Ep__15;

		public static CallSite<Func<CallSite, object, List<LocalityDistrictNational>, object>> _003C_003Ep__16;

		public static CallSite<Func<CallSite, object, List<ReportType>, object>> _003C_003Ep__17;
	}

	private readonly IDashboardFilterService _filterService;

	private readonly ICurrentUserService _currentUser;

	private readonly IReportStatusService _reportStatusService;

	private readonly AppDbContext _db;

	public DashboardController(IDashboardFilterService filterService, ICurrentUserService currentUser, IReportStatusService reportStatusService, AppDbContext db)
	{
		_filterService = filterService;
		_currentUser = currentUser;
		_reportStatusService = reportStatusService;
		_db = db;
	}

	[HttpGet]
	public async Task<IActionResult> Index(DashboardFilter? filter = null)
	{
		if (filter == null)
		{
			filter = new DashboardFilter();
		}
		await PopulateFilterDataAsync(filter);
		bool flag = true;
		base.ViewBag.ShowData = flag;
		List<DashboardReportDetailRow> list = new List<DashboardReportDetailRow>();
		int num = 0;
		if (flag)
		{
			(list, num) = await _filterService.GetReportRowsAsync(filter, _currentUser.UserId, _currentUser.UserRole);
		}
		base.ViewBag.Rows = list;
		base.ViewBag.TotalCount = num;
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> FilterOptions(string? selected = null)
	{
		DashboardFilter currentSelection;
		try
		{
			currentSelection = (string.IsNullOrWhiteSpace(selected) ? new DashboardFilter() : (JsonSerializer.Deserialize<DashboardFilter>(selected, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			}) ?? new DashboardFilter()));
		}
		catch (JsonException)
		{
			currentSelection = new DashboardFilter();
		}
		return Json(await _filterService.GetCompatibleOptionsAsync(currentSelection, _currentUser.UserId, _currentUser.UserRole));
	}

	[HttpGet]
	public async Task<IActionResult> ReportDocuments(int reportId, int allocationId)
	{
		Report report = await _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		Allocation allocation = await _db.Allocations.Include((Allocation a) => a.Project).FirstOrDefaultAsync((Allocation a) => a.Id == allocationId);
		if (report == null || allocation == null || allocation.UserId != report.UserId)
		{
			return NotFound();
		}
		List<int> reportRowIds = await (from rr in _db.ReportRows
			where rr.ReportId == reportId && rr.AllocationId == (int?)allocationId
			select rr.Id).ToListAsync();
		List<DocumentAttachment> attachments = await (from a in _db.DocumentAttachments
			where a.ReportId == (int?)reportId || (a.ReportRowId.HasValue && reportRowIds.Contains(a.ReportRowId.Value)) || (a.UserId == (int?)report.UserId && a.ReportId == null && a.ReportRowId == null)
			orderby a.UploadedAt descending
			select a).ToListAsync();
		return Json(new
		{
			employeeName = ((report.User?.FirstName + " " + report.User?.LastName) ?? "").Trim(),
			employeeId = report.User?.IdNumber ?? report.User?.EmployeeCode ?? "",
			projectName = allocation.Project?.Description ?? "",
			reportMonth = report.ReportingMonth?.Description ?? ((report.ReportingMonth != null) ? $"{report.ReportingMonth.Month}/{report.ReportingMonth.Year}" : ""),
			documents = attachments.Select((DocumentAttachment a) => new
			{
				id = a.Id,
				fileName = a.FileName,
				description = a.Description,
				uploadedAt = a.UploadedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
				fileSize = FormatFileSize(a.FileSize),
				viewUrl = Url.Action("DocumentAttachment", "Dashboard", new { attachmentId = a.Id }),
				downloadUrl = Url.Action("DocumentAttachment", "Dashboard", new { attachmentId = a.Id, download = true })
			})
		});
	}

	[HttpGet]
	public async Task<IActionResult> DocumentAttachment(int attachmentId, bool download = false)
	{
		DocumentAttachment attachment = await _db.DocumentAttachments.FirstOrDefaultAsync((DocumentAttachment a) => a.Id == attachmentId);
		if (attachment == null)
		{
			return NotFound();
		}
		string uploadsRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
		string relativePath = attachment.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
		string filePath = Path.GetFullPath(Path.Combine(uploadsRoot, relativePath));
		if (!filePath.StartsWith(uploadsRoot, StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(filePath))
		{
			return NotFound();
		}
		string contentType = string.IsNullOrWhiteSpace(attachment.MimeType) ? "application/octet-stream" : attachment.MimeType;
		return PhysicalFile(filePath, contentType, download ? attachment.FileName : null);
	}

	private static string FormatFileSize(long bytes)
	{
		if (bytes >= 1048576L)
		{
			return (bytes / 1048576m).ToString("0.#") + " MB";
		}
		if (bytes >= 1024L)
		{
			return (bytes / 1024m).ToString("0.#") + " KB";
		}
		return bytes.ToString() + " B";
	}

	[HttpGet]
	public async Task<IActionResult> ExportExcel(DashboardFilter filter)
	{
		UserRoleEnum userRole = _currentUser.UserRole;
		if ((uint)(userRole - 4) <= 1u)
		{
			filter.StatusId = 4;
		}
		filter.Page = 1;
		filter.PageSize = 10000;
		var (list, _) = await _filterService.GetReportRowsAsync(filter, _currentUser.UserId, _currentUser.UserRole);
		using XLWorkbook xLWorkbook = new XLWorkbook();
		IXLWorksheet iXLWorksheet = xLWorkbook.Worksheets.Add("דיווחים");
		iXLWorksheet.RightToLeft = true;
		string[] array = new string[24]
		{
			"מס\"ד", "ת.ז", "קוד עובד", "שם מדווח", "חודש דיווח", "פרויקט", "מחוז", "ישוב", "מסגרת חינוכית", "תאריך מפגש",
			"משך מפגש", "תוכנית חינוכית", "תחום", "נושא 1", "נושא 2", "קיום דיון", "כיתה", "שכבה", "סוג דיווח", "מסקנות כיתה",
			"מסקנות מסגרת", "מסקנות ישוב/מחוז/ארצי", "מסמכים", "הערות"
		};
		for (int i = 0; i < array.Length; i++)
		{
			iXLWorksheet.Cell(1, i + 1).Value = array[i];
		}
		string[] orderedHeaders = new string[24]
		{
			"מס\"ד", "ת.ז", "סוג דיווח", "קוד עובד", "שם מדווח", "חודש דיווח", "פרויקט", "מחוז", "ישוב", "מסגרת חינוכית",
			"תאריך מפגש", "משך מפגש", "תוכנית חינוכית", "תחום", "נושא 1", "נושא 2", "קיום דיון", "כיתה", "שכבה", "מסקנות כיתה",
			"מסקנות מסגרת", "מסקנות ישוב/מחוז/ארצי", "מסמכים", "הערות"
		};
		for (int i = 0; i < orderedHeaders.Length; i++)
		{
			iXLWorksheet.Cell(1, i + 1).Value = orderedHeaders[i];
		}
		int num = 2;
		foreach (DashboardReportDetailRow item in list)
		{
			iXLWorksheet.Cell(num, 1).Value = item.SequenceNumber;
			iXLWorksheet.Cell(num, 2).Value = item.IdNumber;
			iXLWorksheet.Cell(num, 3).Value = item.ReportTypeName;
			iXLWorksheet.Cell(num, 4).Value = item.EmployeeCode;
			iXLWorksheet.Cell(num, 5).Value = item.FullName;
			iXLWorksheet.Cell(num, 6).Value = item.MonthDescription;
			iXLWorksheet.Cell(num, 7).Value = item.ProjectName;
			iXLWorksheet.Cell(num, 8).Value = item.DistrictName;
			iXLWorksheet.Cell(num, 9).Value = item.LocalityName;
			iXLWorksheet.Cell(num, 10).Value = item.FrameworkName;
			iXLWorksheet.Cell(num, 11).Value = item.MeetingDate.ToString("dd/MM/yyyy");
			iXLWorksheet.Cell(num, 12).Value = (double)item.MeetingDuration;
			iXLWorksheet.Cell(num, 13).Value = item.EducationalProgramName;
			iXLWorksheet.Cell(num, 14).Value = item.DomainName;
			iXLWorksheet.Cell(num, 15).Value = item.Subject1Name;
			iXLWorksheet.Cell(num, 16).Value = item.Subject2Name;
			iXLWorksheet.Cell(num, 17).Value = item.DiscussionCodeName;
			iXLWorksheet.Cell(num, 18).Value = item.ClassName;
			iXLWorksheet.Cell(num, 19).Value = item.GradeLevelName;
			iXLWorksheet.Cell(num, 20).Value = item.ConclusionClassName;
			iXLWorksheet.Cell(num, 21).Value = item.ConclusionFrameworkName;
			iXLWorksheet.Cell(num, 22).Value = item.ConclusionLocationName;
			iXLWorksheet.Cell(num, 23).Value = (item.HasAttachments ? "כן" : "לא");
			iXLWorksheet.Cell(num, 24).Value = item.Notes;
			num++;
		}
		iXLWorksheet.Columns().AdjustToContents();
		using MemoryStream memoryStream = new MemoryStream();
		xLWorkbook.SaveAs(memoryStream);
		return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"reports_{DateTime.Now:yyyyMMdd}.xlsx");
	}

	[HttpGet]
	public async Task<IActionResult> SummaryExportExcel(DashboardFilter filter)
	{
		filter.Page = 1;
		filter.PageSize = 10000;
		var (list, _) = await _filterService.GetReportsAsync(filter, _currentUser.UserId, _currentUser.UserRole);
		using XLWorkbook xLWorkbook = new XLWorkbook();
		IXLWorksheet iXLWorksheet = xLWorkbook.Worksheets.Add("סיכום");
		iXLWorksheet.RightToLeft = true;
		string[] array = new string[11]
		{
			"קוד עובד", "ת.ז", "שם עובד", "פרויקט", "חודש", "סטטוס", "מס' שורות", "סך משך תפוקה", "יתרת שורות", "מסמכים",
			"תאריך הגשה"
		};
		for (int i = 0; i < array.Length; i++)
		{
			iXLWorksheet.Cell(1, i + 1).Value = array[i];
		}
		int num = 2;
		foreach (DashboardReportRow item in list)
		{
			iXLWorksheet.Cell(num, 1).Value = item.EmployeeCode;
			iXLWorksheet.Cell(num, 2).Value = item.IdNumber;
			iXLWorksheet.Cell(num, 3).Value = item.FullName;
			iXLWorksheet.Cell(num, 4).Value = item.ProjectName;
			iXLWorksheet.Cell(num, 5).Value = item.MonthDescription;
			iXLWorksheet.Cell(num, 6).Value = item.StatusName;
			iXLWorksheet.Cell(num, 7).Value = item.RowCount;
			iXLWorksheet.Cell(num, 8).Value = (double)item.TotalDuration;
			iXLWorksheet.Cell(num, 9).Value = (item.MonthlyRowAllocation.HasValue ? ((XLCellValue)item.RemainingRows) : ((XLCellValue)string.Empty));
			iXLWorksheet.Cell(num, 10).Value = item.DocumentCount;
			iXLWorksheet.Cell(num, 11).Value = item.SubmittedAt?.ToString("dd/MM/yyyy") ?? string.Empty;
			num++;
		}
		iXLWorksheet.Columns().AdjustToContents();
		using MemoryStream memoryStream = new MemoryStream();
		xLWorkbook.SaveAs(memoryStream);
		return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"summary_{DateTime.Now:yyyyMMdd}.xlsx");
	}

	[HttpGet]
	public async Task<IActionResult> Summary(DashboardFilter? filter = null)
	{
		if (filter == null)
		{
			filter = new DashboardFilter();
		}
		await PopulateFilterDataAsync(filter);
		var (list, num) = await _filterService.GetReportsAsync(filter, _currentUser.UserId, _currentUser.UserRole);
		base.ViewBag.Rows = list;
		base.ViewBag.TotalCount = num;
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "CanApproveReports")]
	public async Task<IActionResult> BulkApprove(List<int> reportIds)
	{
		int approved = 0;
		foreach (int id in reportIds.Distinct())
		{
			if (await _filterService.CanAccessReportAsync(id, _currentUser.UserId, _currentUser.UserRole) && await _reportStatusService.ApproveReportAsync(id, _currentUser.UserId))
			{
				approved++;
			}
		}
		base.TempData["Success"] = $"{approved} דיווחים אושרו בהצלחה";
		return RedirectToAction("Summary");
	}

	private async Task PopulateFilterDataAsync(DashboardFilter filter)
	{
		base.ViewBag.Filter = filter;
		if (_003C_003Eo__10._003C_003Ep__1 == null)
		{
			_003C_003Eo__10._003C_003Ep__1 = CallSite<Func<CallSite, object, List<District>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Districts", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<District>, object> target = _003C_003Eo__10._003C_003Ep__1.Target;
		CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__ = _003C_003Eo__10._003C_003Ep__1;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, await _filterService.GetFilteredDistrictsAsync(_currentUser.UserId, _currentUser.UserRole));
		if (_003C_003Eo__10._003C_003Ep__2 == null)
		{
			_003C_003Eo__10._003C_003Ep__2 = CallSite<Func<CallSite, object, List<Sector>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sectors", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Sector>, object> target2 = _003C_003Eo__10._003C_003Ep__2.Target;
		CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__2 = _003C_003Eo__10._003C_003Ep__2;
		viewBag = base.ViewBag;
		target2(_003C_003Ep__2, viewBag, await _filterService.GetFilteredSectorsAsync(_currentUser.UserId, _currentUser.UserRole, filter.DistrictId));
		if (_003C_003Eo__10._003C_003Ep__3 == null)
		{
			_003C_003Eo__10._003C_003Ep__3 = CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Programs", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object> target3 = _003C_003Eo__10._003C_003Ep__3.Target;
		CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__3 = _003C_003Eo__10._003C_003Ep__3;
		viewBag = base.ViewBag;
		target3(_003C_003Ep__3, viewBag, await _filterService.GetFilteredProgramsAsync(_currentUser.UserId, _currentUser.UserRole, filter.DistrictId));
		if (_003C_003Eo__10._003C_003Ep__4 == null)
		{
			_003C_003Eo__10._003C_003Ep__4 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "IsInspector", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, bool, object> target4 = _003C_003Eo__10._003C_003Ep__4.Target;
		CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__4 = _003C_003Eo__10._003C_003Ep__4;
		object viewBag2 = base.ViewBag;
		UserRoleEnum userRole = _currentUser.UserRole;
		bool arg = (uint)(userRole - 4) <= 1u;
		target4(_003C_003Ep__4, viewBag2, arg);
		base.ViewBag.CanEditDashboardRows = _currentUser.UserRole == UserRoleEnum.SystemAdmin;
		if (_003C_003Eo__10._003C_003Ep__6 == null)
		{
			_003C_003Eo__10._003C_003Ep__6 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CanApprove", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, bool, object> target5 = _003C_003Eo__10._003C_003Ep__6.Target;
		CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__5 = _003C_003Eo__10._003C_003Ep__6;
		object viewBag3 = base.ViewBag;
		userRole = _currentUser.UserRole;
		arg = (((uint)(userRole - 1) <= 2u || userRole == UserRoleEnum.InspectorApproval) ? true : false);
		target5(_003C_003Ep__5, viewBag3, arg);
		using IServiceScope scope = base.HttpContext.RequestServices.CreateScope();
		AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		if (_003C_003Eo__10._003C_003Ep__7 == null)
		{
			_003C_003Eo__10._003C_003Ep__7 = CallSite<Func<CallSite, object, List<ReportingMonth>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportingMonths", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<ReportingMonth>, object> target6 = _003C_003Eo__10._003C_003Ep__7.Target;
		CallSite<Func<CallSite, object, List<ReportingMonth>, object>> _003C_003Ep__6 = _003C_003Eo__10._003C_003Ep__7;
		viewBag = base.ViewBag;
		target6(_003C_003Ep__6, viewBag, await (from m in db.ReportingMonths
			orderby m.Year descending, m.Month descending
			select m).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__8 == null)
		{
			_003C_003Eo__10._003C_003Ep__8 = CallSite<Func<CallSite, object, List<Locality>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Localities", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Locality>, object> target7 = _003C_003Eo__10._003C_003Ep__8.Target;
		CallSite<Func<CallSite, object, List<Locality>, object>> _003C_003Ep__7 = _003C_003Eo__10._003C_003Ep__8;
		viewBag = base.ViewBag;
		target7(_003C_003Ep__7, viewBag, await (from x in db.Localities
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__9 == null)
		{
			_003C_003Eo__10._003C_003Ep__9 = CallSite<Func<CallSite, object, List<Framework>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Frameworks", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Framework>, object> target8 = _003C_003Eo__10._003C_003Ep__9.Target;
		CallSite<Func<CallSite, object, List<Framework>, object>> _003C_003Ep__8 = _003C_003Eo__10._003C_003Ep__9;
		viewBag = base.ViewBag;
		target8(_003C_003Ep__8, viewBag, await (from x in db.Frameworks
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__10 == null)
		{
			_003C_003Eo__10._003C_003Ep__10 = CallSite<Func<CallSite, object, List<EducationalProgram>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EducationalPrograms", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<EducationalProgram>, object> target9 = _003C_003Eo__10._003C_003Ep__10.Target;
		CallSite<Func<CallSite, object, List<EducationalProgram>, object>> _003C_003Ep__9 = _003C_003Eo__10._003C_003Ep__10;
		viewBag = base.ViewBag;
		target9(_003C_003Ep__9, viewBag, await (from x in db.EducationalPrograms
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__11 == null)
		{
			_003C_003Eo__10._003C_003Ep__11 = CallSite<Func<CallSite, object, List<Domain>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Domains", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Domain>, object> target10 = _003C_003Eo__10._003C_003Ep__11.Target;
		CallSite<Func<CallSite, object, List<Domain>, object>> _003C_003Ep__10 = _003C_003Eo__10._003C_003Ep__11;
		viewBag = base.ViewBag;
		target10(_003C_003Ep__10, viewBag, await (from x in db.Domains
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__12 == null)
		{
			_003C_003Eo__10._003C_003Ep__12 = CallSite<Func<CallSite, object, List<Subject>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Subjects", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Subject>, object> target11 = _003C_003Eo__10._003C_003Ep__12.Target;
		CallSite<Func<CallSite, object, List<Subject>, object>> _003C_003Ep__11 = _003C_003Eo__10._003C_003Ep__12;
		viewBag = base.ViewBag;
		target11(_003C_003Ep__11, viewBag, await (from x in db.Subjects
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__13 == null)
		{
			_003C_003Eo__10._003C_003Ep__13 = CallSite<Func<CallSite, object, List<DiscussionCode>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DiscussionCodes", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<DiscussionCode>, object> target12 = _003C_003Eo__10._003C_003Ep__13.Target;
		CallSite<Func<CallSite, object, List<DiscussionCode>, object>> _003C_003Ep__12 = _003C_003Eo__10._003C_003Ep__13;
		viewBag = base.ViewBag;
		target12(_003C_003Ep__12, viewBag, await (from x in db.DiscussionCodes
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__14 == null)
		{
			_003C_003Eo__10._003C_003Ep__14 = CallSite<Func<CallSite, object, List<SchoolClass>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Classes", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<SchoolClass>, object> target13 = _003C_003Eo__10._003C_003Ep__14.Target;
		CallSite<Func<CallSite, object, List<SchoolClass>, object>> _003C_003Ep__13 = _003C_003Eo__10._003C_003Ep__14;
		viewBag = base.ViewBag;
		target13(_003C_003Ep__13, viewBag, await (from x in db.Classes
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__15 == null)
		{
			_003C_003Eo__10._003C_003Ep__15 = CallSite<Func<CallSite, object, List<GradeLevel>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GradeLevels", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<GradeLevel>, object> target14 = _003C_003Eo__10._003C_003Ep__15.Target;
		CallSite<Func<CallSite, object, List<GradeLevel>, object>> _003C_003Ep__14 = _003C_003Eo__10._003C_003Ep__15;
		viewBag = base.ViewBag;
		target14(_003C_003Ep__14, viewBag, await (from x in db.GradeLevels
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__16 == null)
		{
			_003C_003Eo__10._003C_003Ep__16 = CallSite<Func<CallSite, object, List<LocalityDistrictNational>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ConclusionLocations", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<LocalityDistrictNational>, object> target15 = _003C_003Eo__10._003C_003Ep__16.Target;
		CallSite<Func<CallSite, object, List<LocalityDistrictNational>, object>> _003C_003Ep__15 = _003C_003Eo__10._003C_003Ep__16;
		viewBag = base.ViewBag;
		target15(_003C_003Ep__15, viewBag, await (from x in db.LocalityDistrictNationals
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
		if (_003C_003Eo__10._003C_003Ep__17 == null)
		{
			_003C_003Eo__10._003C_003Ep__17 = CallSite<Func<CallSite, object, List<ReportType>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportTypes", typeof(DashboardController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<ReportType>, object> target16 = _003C_003Eo__10._003C_003Ep__17.Target;
		CallSite<Func<CallSite, object, List<ReportType>, object>> _003C_003Ep__16 = _003C_003Eo__10._003C_003Ep__17;
		viewBag = base.ViewBag;
		target16(_003C_003Ep__16, viewBag, await (from x in db.ReportTypes
			where x.IsActive
			orderby x.Description
			select x).ToListAsync());
	}
}

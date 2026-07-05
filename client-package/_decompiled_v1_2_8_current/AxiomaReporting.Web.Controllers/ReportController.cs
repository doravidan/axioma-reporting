using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Web.Controllers;

[Authorize]
public class ReportController : Controller
{
	[CompilerGenerated]
	private static class _003C_003Eo__14
	{
		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__1;

		public static CallSite<Func<CallSite, object, User, object>> _003C_003Ep__2;

		public static CallSite<Func<CallSite, object, List<Allocation>, object>> _003C_003Ep__3;

		public static CallSite<Func<CallSite, object, ReportingMonth, object>> _003C_003Ep__4;

		public static CallSite<Func<CallSite, object, List<DocumentAttachment>, object>> _003C_003Ep__5;

		public static CallSite<Func<CallSite, object, User, object>> _003C_003Ep__6;

		public static CallSite<Func<CallSite, object, ReportingMonth, object>> _003C_003Ep__7;

		public static CallSite<Func<CallSite, object, Report, object>> _003C_003Ep__8;

		public static CallSite<Func<CallSite, object, Allocation, object>> _003C_003Ep__9;

		public static CallSite<Func<CallSite, object, List<Allocation>, object>> _003C_003Ep__10;

		public static CallSite<Func<CallSite, object, int, object>> _003C_003Ep__11;

		public static CallSite<Func<CallSite, object, int?, object>> _003C_003Ep__12;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__13;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__14;

		public static CallSite<Func<CallSite, object, HashSet<string>, object>> _003C_003Ep__15;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__16;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__17;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__18;
	}

	private static readonly HashSet<string> AllowedAttachmentExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };

	private const long MaxAttachmentBytes = 10485760L;

	private const int MaxAttachmentDescriptionLength = 1000;

	internal const string DeadlinePassedMessage = "המועד האחרון לדיווח עבר. ניתן לערוך רק באמצעות מנהל מערכת, מנהל פרויקט או רכז פרויקט.";

	internal const string ConcurrencyConflictMessage = "\u05d4\u05e9\u05d5\u05e8\u05d4 \u05e2\u05d5\u05d3\u05db\u05e0\u05d4 \u05d1\u05de\u05e7\u05d1\u05d9\u05dc \u05e2\u05dc \u05d9\u05d3\u05d9 \u05de\u05e9\u05ea\u05de\u05e9 \u05d0\u05d7\u05e8. \u05d9\u05e9 \u05dc\u05e8\u05e2\u05e0\u05df \u05d5\u05dc\u05e0\u05e1\u05d5\u05ea \u05e9\u05d5\u05d1.";

	private readonly AppDbContext _db;

	private readonly IReportValidationService _validator;

	private readonly IReportStatusService _statusService;

	private readonly IReportExcelImportService _excelImportService;

	private readonly IPdfReportService _pdfReportService;

	private readonly ICurrentUserService _currentUser;

	private readonly IEmailService _emailService;

	private readonly IAuditLogService _auditLog;

	private readonly ILogger<ReportController> _logger;

	public ReportController(AppDbContext db, IReportValidationService validator, IReportStatusService statusService, IReportExcelImportService excelImportService, IPdfReportService pdfReportService, ICurrentUserService currentUser, IEmailService emailService, IAuditLogService auditLog, ILogger<ReportController> logger)
	{
		_db = db;
		_validator = validator;
		_statusService = statusService;
		_excelImportService = excelImportService;
		_pdfReportService = pdfReportService;
		_currentUser = currentUser;
		_emailService = emailService;
		_auditLog = auditLog;
		_logger = logger;
	}

	[HttpGet]
	public async Task<IActionResult> Index(int? userId = null, int? allocationId = null, int? reportId = null, int? reportingMonthId = null, int? editRowId = null, string? returnUrl = null, bool manual = false)
	{
		Report report2 = ((!reportId.HasValue) ? null : (await _db.Reports.Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == ((int?)reportId).Value)));
		Report requestedReport = report2;
		if (reportId.HasValue && requestedReport == null)
		{
			return NotFound();
		}
		if (requestedReport?.IsArchived == true && (uint)(_currentUser.UserRole - 1) > 2u)
		{
			return NotFound();
		}
		int targetUserId = requestedReport?.UserId ?? userId ?? _currentUser.UserId;
		if (!(await CanViewEmployeeReportAsync(targetUserId)))
		{
			return Forbid();
		}
		ReportingMonth reportingMonth = requestedReport?.ReportingMonth;
		ReportingMonth reportingMonth2 = reportingMonth;
		if (reportingMonth2 == null && reportingMonthId.HasValue)
		{
			reportingMonth2 = await _db.ReportingMonths.FirstOrDefaultAsync((ReportingMonth m) => m.Id == reportingMonthId.Value);
		}
		if (reportingMonth2 == null)
		{
			reportingMonth2 = await _db.ReportingMonths.FirstOrDefaultAsync((ReportingMonth m) => m.IsActive);
		}
		ReportingMonth activeMonth = reportingMonth2;
		if (activeMonth == null)
		{
			base.ViewBag.Error = "\u05d0\u05d9\u05df \u05d7\u05d5\u05d3\u05e9 \u05d3\u05d9\u05d5\u05d5\u05d7 \u05e4\u05e2\u05d9\u05dc \u05db\u05e8\u05d2\u05e2";
			return View("NoActiveMonth");
		}
		User employee = await _db.Users.Include((User u) => u.Role).FirstOrDefaultAsync((User u) => u.Id == targetUserId);
		if (employee == null)
		{
			return NotFound();
		}
		List<Allocation> allocations = await (from a in _db.Allocations.Include((Allocation a) => a.Project)
			where a.UserId == targetUserId && a.IsActive
			select a).ToListAsync();
		if (allocations.Count == 0)
		{
			base.ViewBag.Error = "\u05d0\u05d9\u05df \u05d4\u05e7\u05e6\u05d0\u05d4 \u05e4\u05e2\u05d9\u05dc\u05d4 \u05dc\u05e2\u05d5\u05d1\u05d3 \u05d6\u05d4";
			return View("NoAllocation");
		}
		Allocation selectedAllocation;
		if (allocationId.HasValue)
		{
			selectedAllocation = allocations.FirstOrDefault((Allocation a) => a.Id == allocationId.Value) ?? allocations[0];
		}
		else
		{
			if (allocations.Count != 1)
			{
				base.ViewBag.Employee = employee;
				base.ViewBag.Allocations = allocations;
				base.ViewBag.ActiveMonth = activeMonth;
				return View("SelectAllocation");
			}
			selectedAllocation = allocations[0];
		}
		Report report3 = requestedReport;
		if (report3 == null)
		{
			report3 = await _db.Reports.Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.UserId == targetUserId && r.ReportingMonthId == activeMonth.Id);
		}
		if (report3 == null && activeMonth.IsActive)
		{
			report3 = await _statusService.GetOrCreateDraftAsync(targetUserId, activeMonth.Id);
		}
		if (report3 == null)
		{
			report3 = new Report
			{
				UserId = targetUserId,
				ReportingMonthId = activeMonth.Id,
				ReportingMonth = activeMonth,
				StatusId = 1
			};
		}
		Report report = report3;
		if (report == null)
		{
			return StatusCode(500);
		}
		List<ReportRow> rows = await (from r in _db.ReportRows.Include((ReportRow r) => r.ReportType).Include((ReportRow r) => r.District).Include((ReportRow r) => r.Locality).Include((ReportRow r) => r.Framework)
				.Include((ReportRow r) => r.EducationalProgram)
				.Include((ReportRow r) => r.Domain)
				.Include((ReportRow r) => r.Subject1)
				.Include((ReportRow r) => r.Subject2)
				.Include((ReportRow r) => r.DiscussionCode)
				.Include((ReportRow r) => r.ConclusionClass)
				.Include((ReportRow r) => r.ConclusionFramework)
				.Include((ReportRow r) => r.ConclusionLocation)
				.Include((ReportRow r) => r.GradeLevel)
				.Include((ReportRow r) => r.Class)
			where r.ReportId == report.Id && r.AllocationId == (int?)selectedAllocation.Id
			orderby r.MeetingDate, r.SequenceNumber
			select r).ToListAsync();
		Dictionary<int, string> frameworkLabels = await BuildFrameworkLabelsAsync(rows.Select((ReportRow r) => (int?)r.FrameworkId).Concat(rows.Select((ReportRow r) => r.ConclusionFrameworkId)));
		ApplyFrameworkLabels(rows, frameworkLabels);
		List<int> rowIds = rows.Select((ReportRow r) => r.Id).ToList();
		if (_003C_003Eo__14._003C_003Ep__5 == null)
		{
			_003C_003Eo__14._003C_003Ep__5 = CallSite<Func<CallSite, object, List<DocumentAttachment>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportAttachments", typeof(ReportController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<DocumentAttachment>, object> target = _003C_003Eo__14._003C_003Ep__5.Target;
		CallSite<Func<CallSite, object, List<DocumentAttachment>, object>> _003C_003Ep__ = _003C_003Eo__14._003C_003Ep__5;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, await (from a in _db.DocumentAttachments
			where a.ReportId == (int?)report.Id || (a.ReportRowId.HasValue && rowIds.Contains(a.ReportRowId.Value))
			orderby a.UploadedAt descending
			select a).ToListAsync());
		Allocation allocation = await _db.Allocations.Include((Allocation a) => a.ReportType).Include((Allocation a) => a.AllocationDistricts).ThenInclude((AllocationDistrict x) => x.District).Include((Allocation a) => a.AllocationLocalities)
			.ThenInclude((AllocationLocality x) => x.Locality)
			.Include((Allocation a) => a.AllocationFrameworks)
			.ThenInclude((AllocationFramework x) => x.Framework)
			.Include((Allocation a) => a.AllocationEducationalPrograms)
			.ThenInclude((AllocationEducationalProgram x) => x.EducationalProgram)
			.Include((Allocation a) => a.AllocationDomains)
			.ThenInclude((AllocationDomain x) => x.Domain)
			.Include((Allocation a) => a.AllocationSubjects)
			.ThenInclude((AllocationSubject x) => x.Subject)
			.Include((Allocation a) => a.AllocationDiscussionCodes)
			.ThenInclude((AllocationDiscussionCode x) => x.DiscussionCode)
			.Include((Allocation a) => a.AllocationClasses)
			.ThenInclude((AllocationClass x) => x.SchoolClass)
			.Include((Allocation a) => a.AllocationGradeLevels)
			.ThenInclude((AllocationGradeLevel x) => x.GradeLevel)
			.Include((Allocation a) => a.AllocationLocalityDistrictNationals)
			.ThenInclude((AllocationLocalityDistrictNational x) => x.LocalityDistrictNational)
			.AsSplitQuery()
			.FirstOrDefaultAsync((Allocation a) => a.Id == selectedAllocation.Id);
		if (allocation != null)
		{
			Dictionary<int, string> allocationFrameworkLabels = await BuildFrameworkLabelsAsync(allocation.AllocationFrameworks.Select((AllocationFramework x) => (int?)x.FrameworkId).Concat(allocation.AllocationFrameworks.Select((AllocationFramework x) => (int?)x.FrameworkId)));
			foreach (AllocationFramework item in allocation.AllocationFrameworks)
			{
				if (item.Framework != null && allocationFrameworkLabels.TryGetValue(item.Framework.Id, out string label))
				{
					item.Framework.Description = label;
				}
			}
		}
		Report report4 = report;
		if (report4.ReportingMonth == null)
		{
			report4.ReportingMonth = activeMonth;
		}
		bool deadlinePassed = IsDeadlinePassed(activeMonth);
		bool isOverrideRole = IsDeadlineOverrideRole();
		base.ViewBag.Employee = employee;
		base.ViewBag.ActiveMonth = activeMonth;
		base.ViewBag.Report = report;
		base.ViewBag.Allocation = allocation;
		base.ViewBag.Allocations = allocations;
		base.ViewBag.AllocationId = selectedAllocation.Id;
		base.ViewBag.EditRowId = editRowId;
		base.ViewBag.ReturnUrl = NormalizeLocalReturnUrl(returnUrl);
		if (manual)
		{
			base.ViewData["ManualLocalities"] = (await _db.Localities.Where((Locality l) => l.IsActive).OrderBy((Locality l) => l.Description).ToListAsync()).Where(IsCityLocality).ToList();
		}
		base.ViewBag.CanEdit = CanEditReport(report);
		if (_003C_003Eo__14._003C_003Ep__15 == null)
		{
			_003C_003Eo__14._003C_003Ep__15 = CallSite<Func<CallSite, object, HashSet<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "RequiredReportFields", typeof(ReportController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, HashSet<string>, object> target2 = _003C_003Eo__14._003C_003Ep__15.Target;
		CallSite<Func<CallSite, object, HashSet<string>, object>> _003C_003Ep__2 = _003C_003Eo__14._003C_003Ep__15;
		viewBag = base.ViewBag;
		target2(_003C_003Ep__2, viewBag, await GetRequiredReportFieldsAsync());
		base.ViewBag.DeadlinePassed = deadlinePassed;
		base.ViewBag.DeadlineOverrideActive = deadlinePassed && isOverrideRole;
		base.ViewBag.DeadlineBlockMessage = ((deadlinePassed && !isOverrideRole) ? DeadlinePassedMessage : null);
		return View("Index", rows);
	}

	[HttpGet]
	[Route("Report/History")]
	public async Task<IActionResult> History(int? userId = null)
	{
		int targetUserId = (_currentUser.UserRole == UserRoleEnum.Employee) ? _currentUser.UserId : (userId ?? _currentUser.UserId);
		if (!(await CanViewEmployeeReportAsync(targetUserId)))
		{
			return Forbid();
		}
		User employee = await _db.Users.AsNoTracking().FirstOrDefaultAsync((User u) => u.Id == targetUserId);
		if (employee == null)
		{
			return NotFound();
		}
		List<Report> reports = await _db.Reports.AsNoTracking().Include((Report r) => r.ReportingMonth).Include((Report r) => r.Status).Where((Report r) => r.UserId == targetUserId && !r.IsArchived).OrderByDescending((Report r) => r.ReportingMonth.Year).ThenByDescending((Report r) => r.ReportingMonth.Month).ThenByDescending((Report r) => r.Id).ToListAsync();
		List<int> reportIds = reports.Select((Report r) => r.Id).ToList();
		Dictionary<int, (int RowCount, int? AllocationId)> rowSummaries = new Dictionary<int, (int, int?)>();
		if (reportIds.Count > 0)
		{
			var rowData = await _db.ReportRows.AsNoTracking().Where((ReportRow r) => reportIds.Contains(r.ReportId)).Select((ReportRow r) => new
			{
				r.ReportId,
				r.AllocationId
			}).ToListAsync();
			rowSummaries = rowData.GroupBy(r => r.ReportId).ToDictionary(g => g.Key, g => (g.Count(), g.Where(r => r.AllocationId.HasValue).Select(r => r.AllocationId).FirstOrDefault()));
		}
		StringBuilder html = new StringBuilder();
		string employeeName = (employee.FirstName + " " + employee.LastName).Trim();
		html.Append("<!doctype html><html lang=\"he\" dir=\"rtl\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link rel=\"stylesheet\" href=\"/lib/bootstrap/dist/css/bootstrap.min.css\"><title>היסטוריית דיווחים</title></head><body><main class=\"container py-4\">");
		html.Append("<div class=\"d-flex justify-content-between align-items-start gap-3 flex-wrap mb-3\"><div><h1 class=\"h3 mb-2\">היסטוריית דיווחים</h1><div class=\"text-muted\">");
		html.Append(WebUtility.HtmlEncode(employeeName));
		if (!string.IsNullOrWhiteSpace(employee.IdNumber))
		{
			html.Append(" | ת.ז ");
			html.Append(WebUtility.HtmlEncode(employee.IdNumber));
		}
		html.Append("</div></div><div class=\"d-flex gap-2\"><a class=\"btn btn-outline-secondary btn-sm\" href=\"/MyAllocations\">פעילות חודשית</a><a class=\"btn btn-primary btn-sm\" href=\"/Report\">דיווח נוכחי</a></div></div>");
		html.Append("<div class=\"alert alert-info\">ניתן לצפות כאן בדיווחים מחודשים קודמים. חודשים שאינם פעילים נפתחים לקריאה בלבד.</div>");
		if (reports.Count == 0)
		{
			html.Append("<div class=\"card\"><div class=\"card-body text-muted\">אין דיווחים קודמים להצגה.</div></div>");
		}
		else
		{
			html.Append("<div class=\"table-responsive\"><table class=\"table table-striped table-hover align-middle\"><thead class=\"table-light\"><tr><th>חודש דיווח</th><th>מצב חודש</th><th>סטטוס דיווח</th><th>שורות</th><th>עודכן</th><th class=\"text-nowrap\">פעולה</th></tr></thead><tbody>");
			foreach (Report report in reports)
			{
				ReportingMonth month = report.ReportingMonth;
				rowSummaries.TryGetValue(report.Id, out var summary);
				int? allocationId = summary.AllocationId;
				string reportUrl = "/Report?reportingMonthId=" + report.ReportingMonthId.ToString(CultureInfo.InvariantCulture);
				if (allocationId.HasValue)
				{
					reportUrl += "&allocationId=" + allocationId.Value.ToString(CultureInfo.InvariantCulture);
				}
				if (_currentUser.UserRole != UserRoleEnum.Employee)
				{
					reportUrl += "&userId=" + targetUserId.ToString(CultureInfo.InvariantCulture);
				}
				DateTime updatedAt = report.UpdatedAt ?? report.CreatedAt;
				string updatedText = updatedAt == default(DateTime) ? "" : updatedAt.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
				string monthText = month?.Description ?? ("חודש " + report.ReportingMonthId.ToString(CultureInfo.InvariantCulture));
				string monthState = month?.IsActive == true ? "פעיל" : "סגור";
				string statusText = report.Status?.Description ?? report.Status?.Name ?? report.StatusId.ToString(CultureInfo.InvariantCulture);
				string actionText = month?.IsActive == true ? "פתיחה" : "צפייה";
				html.Append("<tr><td>").Append(WebUtility.HtmlEncode(monthText)).Append("</td><td>").Append(WebUtility.HtmlEncode(monthState)).Append("</td><td>").Append(WebUtility.HtmlEncode(statusText)).Append("</td><td>").Append(summary.RowCount.ToString(CultureInfo.InvariantCulture)).Append("</td><td>").Append(WebUtility.HtmlEncode(updatedText)).Append("</td><td><a class=\"btn btn-sm btn-outline-primary\" href=\"").Append(WebUtility.HtmlEncode(reportUrl)).Append("\">").Append(WebUtility.HtmlEncode(actionText)).Append("</a></td></tr>");
			}
			html.Append("</tbody></table></div>");
		}
		html.Append("</main><script src=\"/lib/bootstrap/dist/js/bootstrap.bundle.min.js\"></script></body></html>");
		return Content(html.ToString(), "text/html; charset=utf-8");
	}

	private static bool IsDeadlinePassed(ReportingMonth? month)
	{
		if (month == null)
		{
			return false;
		}
		return DateTime.Today > month.LastReportingDate.Date;
	}

	[HttpGet]
	[Route("Report/Manual")]
	[Route("Report/Manuel")]
	public async Task<IActionResult> Manual()
	{
		List<ReportingMonth> months = await (from m in _db.ReportingMonths
			orderby m.Year descending, m.Month descending
			select m).ToListAsync();
		return View("~/Views/Report/Manual.cshtml", new ManualReportViewModel
		{
			ReportingMonths = months
		});
		StringBuilder html = new StringBuilder();
		html.Append("<!doctype html><html lang=\"he\" dir=\"rtl\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link rel=\"stylesheet\" href=\"/lib/bootstrap/dist/css/bootstrap.min.css\"><title>הוספת דיווח ידני</title></head><body><main class=\"container mt-4\"><h3>הוספת דיווח ידני</h3>");
		if (TempData["ManualError"] != null)
		{
			html.Append("<div class=\"alert alert-danger\" role=\"alert\">").Append(WebUtility.HtmlEncode(TempData["ManualError"]?.ToString())).Append("</div>");
		}
		html.Append("<form method=\"get\" action=\"/Report/ManualOpen\" class=\"card card-body\"><input type=\"hidden\" id=\"manualUserId\" name=\"userId\" required><div class=\"row g-3 align-items-end\"><div class=\"col-md-3\"><label class=\"form-label\">ת.ז</label><input id=\"manualIdNumber\" class=\"form-control manual-employee-filter\" autocomplete=\"off\"></div><div class=\"col-md-3\"><label class=\"form-label\">קוד</label><input id=\"manualEmployeeCode\" class=\"form-control manual-employee-filter\" autocomplete=\"off\"></div><div class=\"col-md-3\"><label class=\"form-label\">שם פרטי</label><input id=\"manualFirstName\" class=\"form-control manual-employee-filter\" autocomplete=\"off\"></div><div class=\"col-md-3\"><label class=\"form-label\">שם משפחה</label><input id=\"manualLastName\" class=\"form-control manual-employee-filter\" autocomplete=\"off\"></div><div class=\"col-12\"><div id=\"manualEmployeeResults\" class=\"list-group small\"></div></div><div class=\"col-md-5\"><label class=\"form-label\">הקצאה</label><select id=\"manualAllocationSelect\" name=\"allocationId\" class=\"form-select\" required disabled></select></div><div class=\"col-md-5\"><label class=\"form-label\">חודש דיווח</label><select name=\"reportingMonthId\" class=\"form-select\" required>");
		foreach (ReportingMonth month in months)
		{
			html.Append("<option value=\"").Append(month.Id).Append("\">").Append(WebUtility.HtmlEncode(month.Description)).Append("</option>");
		}
		html.Append("</select></div><div class=\"col-md-2\"><button id=\"manualOpenButton\" type=\"submit\" class=\"btn btn-primary w-100\" disabled>פתח</button></div></div><div id=\"manualNoAllocations\" class=\"text-danger small mt-2\" style=\"display:none\">אין הקצאה פעילה לעובד שנבחר</div></form></main><script>(function(){const id=document.getElementById('manualIdNumber'),code=document.getElementById('manualEmployeeCode'),first=document.getElementById('manualFirstName'),last=document.getElementById('manualLastName'),results=document.getElementById('manualEmployeeResults'),userId=document.getElementById('manualUserId'),alloc=document.getElementById('manualAllocationSelect'),button=document.getElementById('manualOpenButton'),empty=document.getElementById('manualNoAllocations');let timer=null;function clearSelection(){userId.value='';alloc.innerHTML='';alloc.disabled=true;button.disabled=true;empty.style.display='none';}function render(data){results.innerHTML='';clearSelection();(data.employees||[]).forEach(function(emp){const item=document.createElement('button');item.type='button';item.className='list-group-item list-group-item-action';item.textContent=[emp.idNumber,emp.employeeCode,emp.firstName,emp.lastName].filter(Boolean).join(' | ');item.addEventListener('click',function(){userId.value=emp.id;results.innerHTML='<div class=\"list-group-item active\">'+item.textContent+'</div>';alloc.innerHTML='';(data.allocations||[]).filter(a=>a.userId===emp.id).forEach(function(a){const opt=document.createElement('option');opt.value=a.id;opt.textContent=a.projectName;alloc.appendChild(opt);});alloc.disabled=alloc.options.length===0;button.disabled=alloc.options.length===0;empty.style.display=alloc.options.length===0?'block':'none';});results.appendChild(item);});if((data.employees||[]).length===0){results.innerHTML='<div class=\"list-group-item text-muted\">לא נמצאו עובדים</div>';}}async function search(){const params=new URLSearchParams({idNumber:id.value,employeeCode:code.value,firstName:first.value,lastName:last.value});const resp=await fetch('/Report/ManualEmployeeSearch?'+params.toString(),{headers:{Accept:'application/json'}});if(resp.ok)render(await resp.json());}function schedule(){clearTimeout(timer);timer=setTimeout(search,250);}document.querySelectorAll('.manual-employee-filter').forEach(el=>el.addEventListener('input',schedule));search();})();</script></body></html>");
		return Content(html.ToString(), "text/html; charset=utf-8");
	}

	[HttpGet]
	[Route("Report/ManualEmployeeSearch")]
	public async Task<IActionResult> ManualEmployeeSearch(string? idNumber, string? employeeCode, string? firstName, string? lastName)
	{
		IQueryable<User> query = _db.Users.Where((User u) => u.StatusId == 1 && u.IsReportingEmployee);
		if (_currentUser.UserRole == UserRoleEnum.Employee)
		{
			query = query.Where((User u) => u.Id == _currentUser.UserId);
		}
		if (!string.IsNullOrWhiteSpace(idNumber))
		{
			string raw = idNumber.Trim();
			string digits = NormalizeDigits(raw);
			query = string.IsNullOrWhiteSpace(digits) ? query.Where((User u) => u.IdNumber.Contains(raw)) : query.Where((User u) => u.IdNumber.Contains(raw) || u.IdNumber.Replace("-", "").Replace(" ", "").Contains(digits));
		}
		if (!string.IsNullOrWhiteSpace(employeeCode))
		{
			string value = employeeCode.Trim();
			query = query.Where((User u) => u.EmployeeCode.Contains(value));
		}
		if (!string.IsNullOrWhiteSpace(firstName))
		{
			string value2 = firstName.Trim();
			query = query.Where((User u) => u.FirstName.Contains(value2));
		}
		if (!string.IsNullOrWhiteSpace(lastName))
		{
			string value3 = lastName.Trim();
			query = query.Where((User u) => u.LastName.Contains(value3));
		}
		var employees = await query.OrderBy((User u) => u.LastName).ThenBy((User u) => u.FirstName).Take(30).Select((User u) => new
		{
			id = u.Id,
			idNumber = u.IdNumber,
			employeeCode = u.EmployeeCode,
			firstName = u.FirstName,
			lastName = u.LastName
		}).ToListAsync();
		List<int> employeeIds = employees.Select(e => e.id).ToList();
		var allocations = await _db.Allocations.Include((Allocation a) => a.Project).Where((Allocation a) => a.IsActive && employeeIds.Contains(a.UserId)).OrderBy((Allocation a) => a.Project.Description).Select((Allocation a) => new
		{
			id = a.Id,
			userId = a.UserId,
			projectName = a.Project != null ? a.Project.Description : ""
		}).ToListAsync();
		return Json(new { employees, allocations });
	}

	[HttpGet]
	public async Task<IActionResult> ManualOpen(int userId, int allocationId, int reportingMonthId)
	{
		if (!(await CanViewEmployeeReportAsync(userId)))
		{
			return Forbid();
		}
		Allocation allocation = await _db.Allocations.FirstOrDefaultAsync((Allocation a) => a.Id == allocationId && a.IsActive);
		if (allocation == null)
		{
			TempData["ManualError"] = "ההקצאה שנבחרה אינה פעילה או לא קיימת.";
			return RedirectToAction("Manual");
		}
		if (allocation.UserId != userId)
		{
			TempData["ManualError"] = "יש לבחור הקצאה ששייכת לעובד שנבחר.";
			return RedirectToAction("Manual");
		}
		Report report = await _statusService.GetOrCreateDraftAsync(userId, reportingMonthId);
		return RedirectToAction("Index", new
		{
			userId,
			allocationId,
			reportId = report.Id,
			manual = true
		});
	}

	[HttpGet]
	[Route("Report/FrameworkLabels")]
	public async Task<IActionResult> FrameworkLabels(string? ids)
	{
		List<int?> frameworkIds = (ids ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(delegate(string value)
		{
			int id;
			return int.TryParse(value, out id) ? (int?)id : null;
		}).Where((int? id) => id.HasValue).Distinct().ToList();
		Dictionary<int, string> labels = await BuildFrameworkLabelsAsync(frameworkIds);
		return Json(labels.Select(kvp => new
		{
			id = kvp.Key,
			text = kvp.Value
		}));
	}

	[HttpGet]
	public IActionResult DownloadExcelTemplate()
	{
		using XLWorkbook xLWorkbook = new XLWorkbook();
		IXLWorksheet iXLWorksheet = xLWorkbook.Worksheets.Add("דיווחים");
		iXLWorksheet.RightToLeft = true;
		string[] array = new string[16]
		{
			"MeetingDate", "MeetingDuration", "DistrictId", "LocalityId", "FrameworkId", "EducationalProgramId", "DomainId", "Subject1Id", "Subject2Id", "DiscussionCodeId",
			"ConclusionClassId", "ConclusionFrameworkId", "ConclusionLocationId", "GradeLevelId", "ClassId", "Notes"
		};
		for (int i = 0; i < array.Length; i++)
		{
			iXLWorksheet.Cell(1, i + 1).Value = array[i];
			iXLWorksheet.Cell(1, i + 1).Style.Font.Bold = true;
		}
		iXLWorksheet.Cell(2, 1).Value = DateTime.Today;
		iXLWorksheet.Cell(2, 1).Style.DateFormat.Format = "dd/MM/yyyy";
		iXLWorksheet.Cell(2, 2).Value = 1.0;
		iXLWorksheet.Cell(2, 16).Value = "לדוגמה - יש למחוק לפני העלאת הקובץ";
		iXLWorksheet.Columns().AdjustToContents();
		using MemoryStream memoryStream = new MemoryStream();
		xLWorkbook.SaveAs(memoryStream);
		return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report_upload_template.xlsx");
	}

	[HttpGet]
	public async Task<IActionResult> ExportMine(int allocationId, int? reportingMonthId = null)
	{
		Allocation allocation = await _db.Allocations.Include((Allocation a) => a.User).Include((Allocation a) => a.Project).Include((Allocation a) => a.ReportType).Include((Allocation a) => a.AllocationPrograms).ThenInclude((AllocationProgram ap) => ap.Program).FirstOrDefaultAsync((Allocation a) => a.Id == allocationId && a.UserId == _currentUser.UserId && a.IsActive);
		if (allocation == null)
		{
			return Forbid();
		}
		ReportingMonth month = (reportingMonthId.HasValue ? await _db.ReportingMonths.FirstOrDefaultAsync((ReportingMonth m) => m.Id == reportingMonthId.Value) : await _db.ReportingMonths.Where((ReportingMonth m) => m.IsActive).OrderByDescending((ReportingMonth m) => m.Year).ThenByDescending((ReportingMonth m) => m.Month).FirstOrDefaultAsync());
		if (month == null)
		{
			return NotFound();
		}
		Report report = await _db.Reports.Include((Report r) => r.Status).FirstOrDefaultAsync((Report r) => r.UserId == _currentUser.UserId && r.ReportingMonthId == month.Id);
		if (report == null)
		{
			return NotFound();
		}
		List<ReportRow> rows = await _db.ReportRows.Include((ReportRow r) => r.ReportType).Include((ReportRow r) => r.District).Include((ReportRow r) => r.Locality).Include((ReportRow r) => r.Framework).Include((ReportRow r) => r.EducationalProgram).Include((ReportRow r) => r.Domain).Include((ReportRow r) => r.Subject1).Include((ReportRow r) => r.Subject2).Include((ReportRow r) => r.DiscussionCode).Include((ReportRow r) => r.ConclusionClass).Include((ReportRow r) => r.ConclusionFramework).Include((ReportRow r) => r.ConclusionLocation).Include((ReportRow r) => r.GradeLevel).Include((ReportRow r) => r.Class).Where((ReportRow r) => r.ReportId == report.Id && r.AllocationId == allocationId).OrderBy((ReportRow r) => r.SequenceNumber).ThenBy((ReportRow r) => r.MeetingDate).ToListAsync();
		using XLWorkbook workbook = new XLWorkbook();
		IXLWorksheet ws = workbook.Worksheets.Add("דיווחים");
		ws.RightToLeft = true;
		ws.Style.Font.FontName = "Arial";
		ws.Style.Font.FontSize = 11.0;
		User? employee = allocation.User;
		string employeeName = ((employee?.FirstName ?? string.Empty) + " " + (employee?.LastName ?? string.Empty)).Trim();
		ws.Cell(1, 1).Value = "עובד";
		ws.Cell(1, 2).Value = employeeName;
		ws.Cell(1, 4).Value = "ת.ז.";
		ws.Cell(1, 5).Value = employee?.IdNumber ?? string.Empty;
		ws.Cell(1, 7).Value = "קוד עובד";
		ws.Cell(1, 8).Value = employee?.EmployeeCode ?? string.Empty;
		ws.Cell(2, 1).Value = "חודש דיווח";
		ws.Cell(2, 2).Value = month.Description;
		ws.Cell(2, 4).Value = "סטטוס דיווח";
		ws.Cell(2, 5).Value = report.Status?.Description ?? report.Status?.Name ?? report.StatusId.ToString(CultureInfo.InvariantCulture);
		ws.Cell(2, 7).Value = "פרויקט";
		ws.Cell(2, 8).Value = allocation.Project?.Description ?? string.Empty;
		ws.Cell(3, 1).Value = "תוכניות";
		ws.Cell(3, 2).Value = string.Join(", ", allocation.AllocationPrograms.Select((AllocationProgram ap) => ap.Program?.Description).Where((string? p) => !string.IsNullOrWhiteSpace(p)));
		string[] headers = new string[18]
		{
			"מספר שורה", "סוג דיווח", "תאריך מפגש", "משך תפוקה", "מחוז", "יישוב", "מסגרת", "תוכנית חינוכית", "תחום", "נושא 1",
			"נושא 2", "קיום דיון", "מסקנה - כיתה", "מסקנה - מסגרת", "מסקנה - מיקום", "שכבה", "כיתה", "הערות"
		};
		int headerRow = 5;
		for (int i = 0; i < headers.Length; i++)
		{
			ws.Cell(headerRow, i + 1).Value = headers[i];
			ws.Cell(headerRow, i + 1).Style.Font.Bold = true;
			ws.Cell(headerRow, i + 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#D9EAF7");
		}
		int rowIndex = headerRow + 1;
		foreach (ReportRow row in rows)
		{
			ws.Cell(rowIndex, 1).Value = row.SequenceNumber;
			ws.Cell(rowIndex, 2).Value = row.ReportType?.Description ?? allocation.ReportType?.Description ?? string.Empty;
			ws.Cell(rowIndex, 3).Value = row.MeetingDate;
			ws.Cell(rowIndex, 3).Style.DateFormat.Format = "dd/MM/yyyy";
			ws.Cell(rowIndex, 4).Value = row.MeetingDuration;
			ws.Cell(rowIndex, 5).Value = row.District?.Description ?? string.Empty;
			ws.Cell(rowIndex, 6).Value = row.Locality?.Description ?? string.Empty;
			ws.Cell(rowIndex, 7).Value = row.Framework?.Description ?? string.Empty;
			ws.Cell(rowIndex, 8).Value = row.EducationalProgram?.Description ?? string.Empty;
			ws.Cell(rowIndex, 9).Value = row.Domain?.Description ?? string.Empty;
			ws.Cell(rowIndex, 10).Value = row.Subject1?.Description ?? string.Empty;
			ws.Cell(rowIndex, 11).Value = row.Subject2?.Description ?? string.Empty;
			ws.Cell(rowIndex, 12).Value = row.DiscussionCode?.Description ?? string.Empty;
			ws.Cell(rowIndex, 13).Value = row.ConclusionClass?.Description ?? string.Empty;
			ws.Cell(rowIndex, 14).Value = row.ConclusionFramework?.Description ?? string.Empty;
			ws.Cell(rowIndex, 15).Value = row.ConclusionLocation?.Description ?? string.Empty;
			ws.Cell(rowIndex, 16).Value = row.GradeLevel?.Description ?? string.Empty;
			ws.Cell(rowIndex, 17).Value = row.Class?.Description ?? string.Empty;
			ws.Cell(rowIndex, 18).Value = row.Notes ?? string.Empty;
			rowIndex++;
		}
		ws.Range(headerRow, 1, Math.Max(headerRow, rowIndex - 1), headers.Length).SetAutoFilter();
		ws.SheetView.FreezeRows(headerRow);
		ws.Columns().AdjustToContents();
		await _auditLog.LogAsync("Report.ExportMine", "Report", report.Id.ToString(CultureInfo.InvariantCulture), null, null, $"allocationId={allocationId}; reportingMonthId={month.Id}");
		using MemoryStream memoryStream = new MemoryStream();
		workbook.SaveAs(memoryStream);
		string filename = $"employee-report-{SafeFilePart(employee?.EmployeeCode ?? _currentUser.UserId.ToString(CultureInfo.InvariantCulture))}-{month.Year}-{month.Month:00}.xlsx";
		return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
	}

	[HttpGet]
	public async Task<IActionResult> ExportReportMonth(int reportId)
	{
		Report report = await _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth).Include((Report r) => r.Status).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return NotFound();
		}
		if (!(await CanViewEmployeeReportAsync(report.UserId)))
		{
			return Forbid();
		}
		List<ReportRow> rows = await _db.ReportRows.Include((ReportRow r) => r.ReportType).Include((ReportRow r) => r.Allocation).ThenInclude((Allocation a) => a.Project).Include((ReportRow r) => r.Allocation).ThenInclude((Allocation a) => a.ReportType).Include((ReportRow r) => r.District).Include((ReportRow r) => r.Locality).Include((ReportRow r) => r.Framework)
			.Include((ReportRow r) => r.EducationalProgram)
			.Include((ReportRow r) => r.Domain)
			.Include((ReportRow r) => r.Subject1)
			.Include((ReportRow r) => r.Subject2)
			.Include((ReportRow r) => r.DiscussionCode)
			.Include((ReportRow r) => r.ConclusionClass)
			.Include((ReportRow r) => r.ConclusionFramework)
			.Include((ReportRow r) => r.ConclusionLocation)
			.Include((ReportRow r) => r.GradeLevel)
			.Include((ReportRow r) => r.Class)
			.AsSplitQuery()
			.Where((ReportRow r) => r.ReportId == report.Id)
			.OrderBy((ReportRow r) => r.AllocationId)
			.ThenBy((ReportRow r) => r.SequenceNumber)
			.ThenBy((ReportRow r) => r.MeetingDate)
			.ToListAsync();
		using XLWorkbook workbook = new XLWorkbook();
		IXLWorksheet ws = workbook.Worksheets.Add("דיווחים");
		ws.RightToLeft = true;
		ws.Style.Font.FontName = "Arial";
		ws.Style.Font.FontSize = 11.0;
		User? employee = report.User;
		ReportingMonth? month = report.ReportingMonth;
		string employeeName = ((employee?.FirstName ?? string.Empty) + " " + (employee?.LastName ?? string.Empty)).Trim();
		ws.Cell(1, 1).Value = "עובד";
		ws.Cell(1, 2).Value = employeeName;
		ws.Cell(1, 4).Value = "ת.ז.";
		ws.Cell(1, 5).Value = employee?.IdNumber ?? string.Empty;
		ws.Cell(2, 1).Value = "חודש דיווח";
		ws.Cell(2, 2).Value = month?.Description ?? string.Empty;
		ws.Cell(2, 4).Value = "סטטוס דיווח";
		ws.Cell(2, 5).Value = report.Status?.Description ?? report.Status?.Name ?? report.StatusId.ToString(CultureInfo.InvariantCulture);
		ws.Cell(3, 1).Value = "תוכניות";
		ws.Cell(3, 2).Value = rows.Count;
		ws.Cell(3, 4).Value = "סה\"כ משך תפוקה";
		ws.Cell(3, 5).Value = rows.Sum((ReportRow r) => r.MeetingDuration);
		string[] headers = new string[19]
		{
			"#", "Project", "Report type", "Allocation", "Meeting date", "Duration", "District", "Locality", "Framework", "Program",
			"Domain", "Subject 1", "Subject 2", "Discussion code", "Conclusion class", "Conclusion framework", "Conclusion location", "Grade/Class", "Notes"
		};
		int headerRow = 5;
		headers = new string[19]
		{
			"מספר שורה", "פרויקט", "סוג דיווח", "הקצאה", "תאריך מפגש", "משך תפוקה", "מחוז", "יישוב", "מסגרת", "תוכנית חינוכית",
			"תחום", "נושא 1", "נושא 2", "קיום דיון", "מסקנה - כיתה", "מסקנה - מסגרת", "מסקנה - מיקום", "שכבה/כיתה", "הערות"
		};
		for (int i = 0; i < headers.Length; i++)
		{
			ws.Cell(headerRow, i + 1).Value = headers[i];
			ws.Cell(headerRow, i + 1).Style.Font.Bold = true;
			ws.Cell(headerRow, i + 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#D9EAF7");
		}
		int rowIndex = headerRow + 1;
		foreach (ReportRow row in rows)
		{
			ws.Cell(rowIndex, 1).Value = row.SequenceNumber;
			ws.Cell(rowIndex, 2).Value = row.Allocation?.Project?.Description ?? string.Empty;
			ws.Cell(rowIndex, 3).Value = row.ReportType?.Description ?? row.Allocation?.ReportType?.Description ?? string.Empty;
			ws.Cell(rowIndex, 4).Value = row.AllocationId?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
			ws.Cell(rowIndex, 5).Value = row.MeetingDate;
			ws.Cell(rowIndex, 5).Style.DateFormat.Format = "dd/MM/yyyy";
			ws.Cell(rowIndex, 6).Value = row.MeetingDuration;
			ws.Cell(rowIndex, 7).Value = row.District?.Description ?? string.Empty;
			ws.Cell(rowIndex, 8).Value = row.Locality?.Description ?? string.Empty;
			ws.Cell(rowIndex, 9).Value = row.Framework?.Description ?? string.Empty;
			ws.Cell(rowIndex, 10).Value = row.EducationalProgram?.Description ?? string.Empty;
			ws.Cell(rowIndex, 11).Value = row.Domain?.Description ?? string.Empty;
			ws.Cell(rowIndex, 12).Value = row.Subject1?.Description ?? string.Empty;
			ws.Cell(rowIndex, 13).Value = row.Subject2?.Description ?? string.Empty;
			ws.Cell(rowIndex, 14).Value = row.DiscussionCode?.Description ?? string.Empty;
			ws.Cell(rowIndex, 15).Value = row.ConclusionClass?.Description ?? string.Empty;
			ws.Cell(rowIndex, 16).Value = row.ConclusionFramework?.Description ?? string.Empty;
			ws.Cell(rowIndex, 17).Value = row.ConclusionLocation?.Description ?? string.Empty;
			ws.Cell(rowIndex, 18).Value = row.GradeLevel?.Description ?? row.Class?.Description ?? string.Empty;
			ws.Cell(rowIndex, 19).Value = row.Notes ?? string.Empty;
			rowIndex++;
		}
		ws.Range(headerRow, 1, Math.Max(headerRow, rowIndex - 1), headers.Length).SetAutoFilter();
		ws.SheetView.FreezeRows(headerRow);
		ws.Columns().AdjustToContents();
		await _auditLog.LogAsync("Report.ExportReportMonth", "Report", report.Id.ToString(CultureInfo.InvariantCulture), null, null, $"reportingMonthId={report.ReportingMonthId}; userId={report.UserId}");
		using MemoryStream memoryStream = new MemoryStream();
		workbook.SaveAs(memoryStream);
		string filename = $"monthly-report-{SafeFilePart(employee?.EmployeeCode ?? report.UserId.ToString(CultureInfo.InvariantCulture))}-{month?.Year ?? 0}-{month?.Month ?? 0:00}.xlsx";
		return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
	}

	[HttpGet]
	public async Task<IActionResult> GetRow(int rowId)
	{
		ReportRow row = await _db.ReportRows.Include((ReportRow r) => r.Report).FirstOrDefaultAsync((ReportRow r) => r.Id == rowId);
		if (row?.Report == null)
		{
			return NotFound();
		}
		if (!(await CanViewEmployeeReportAsync(row.Report.UserId)))
		{
			return Forbid();
		}
		return Json(new
		{
			id = row.Id,
			meetingDate = row.MeetingDate.ToString("yyyy-MM-dd"),
			meetingDuration = row.MeetingDuration,
			districtId = row.DistrictId,
			localityId = row.LocalityId,
			frameworkId = row.FrameworkId,
			educationalProgramId = row.EducationalProgramId,
			domainId = row.DomainId,
			subject1Id = row.Subject1Id,
			subject2Id = row.Subject2Id,
			discussionCodeId = row.DiscussionCodeId,
			conclusionClassId = row.ConclusionClassId,
			conclusionFrameworkId = row.ConclusionFrameworkId,
			conclusionLocationId = row.ConclusionLocationId,
			gradeLevelId = row.GradeLevelId,
			classId = row.ClassId,
			notes = row.Notes,
			rowVersion = Convert.ToBase64String(row.RowVersion ?? Array.Empty<byte>())
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> SaveRow(ReportRow row, int reportId, int allocationId, string? rowVersion = null)
	{
		ReportRow row2 = row;
		Report report = await _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return Json(new
			{
				success = false,
				error = "\u05d3\u05d9\u05d5\u05d5\u05d7 \u05dc\u05d0 \u05e0\u05de\u05e6\u05d0"
			});
		}
		if (!(await CanViewEmployeeReportAsync(report.UserId)) || !CanEditReport(report))
		{
			return Json(new
			{
				success = false,
				error = EditBlockMessage(report)
			});
		}
		Allocation allocation = await _db.Allocations.FirstOrDefaultAsync((Allocation a) => a.Id == allocationId && a.UserId == report.UserId && a.IsActive);
		if (allocation == null)
		{
			return Json(new
			{
				success = false,
				error = "\u05d4\u05e7\u05e6\u05d0\u05d4 \u05dc\u05d0 \u05ea\u05e7\u05d9\u05e0\u05d4"
			});
		}
		bool flag = row2.Id != 0;
		bool flag2 = flag;
		if (flag2)
		{
			flag2 = !(await _db.ReportRows.AnyAsync((ReportRow r) => r.Id == row2.Id && r.ReportId == reportId));
		}
		if (flag2)
		{
			return Json(new
			{
				success = false,
				error = "\u05e9\u05d5\u05e8\u05d4 \u05dc\u05d0 \u05e0\u05de\u05e6\u05d0\u05d4"
			});
		}
		List<ReportRow> first = await _db.ReportRows.Where((ReportRow r) => r.ReportId == reportId && r.Id != row2.Id).ToListAsync();
		row2.AllocationId = allocationId;
		row2.ReportId = reportId;
		if (!row2.ReportTypeId.HasValue && allocation.ReportTypeId.HasValue)
		{
			row2.ReportTypeId = allocation.ReportTypeId;
		}
		List<ReportRow> allRowsInReport = first.Concat(new ReportRow[1] { row2 }).ToList();
		ValidationResult validation = await _validator.ValidateRowAsync(row2, report.User, report.ReportingMonth, allRowsInReport);
		if (!validation.IsValid)
		{
			return Json(new
			{
				success = false,
				errors = validation.Errors
			});
		}
		if (row2.Id == 0)
		{
			int sequenceNumber = (await _db.ReportRows.Where((ReportRow r) => r.ReportId == reportId).MaxAsync((Expression<Func<ReportRow, int?>>)((ReportRow r) => r.SequenceNumber), default(CancellationToken))).GetValueOrDefault() + 1;
			row2.SequenceNumber = sequenceNumber;
			row2.CreatedAt = DateTime.UtcNow;
			_db.ReportRows.Add(row2);
		}
		else
		{
			ReportRow reportRow = await _db.ReportRows.FirstAsync((ReportRow r) => r.Id == row2.Id && r.ReportId == reportId);
			CopyEditableFields(row2, reportRow);
			reportRow.AllocationId = allocationId;
			reportRow.UpdatedAt = DateTime.UtcNow;
			if (TryParseRowVersion(rowVersion, out byte[] bytes))
			{
				_db.Entry(reportRow).OriginalValues["RowVersion"] = bytes;
			}
		}
		try
		{
			await _db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			return Json(new
			{
				success = false,
				error = ConcurrencyConflictMessage
			});
		}
		await _statusService.SaveDraftAsync(reportId);
		ReportRow reportRow2 = ((row2.Id != 0) ? (await _db.ReportRows.AsNoTracking().FirstAsync((ReportRow r) => r.Id == row2.Id && r.ReportId == reportId)) : row2);
		ReportRow reportRow3 = reportRow2;
		return Json(new
		{
			success = true,
			warnings = validation.Warnings,
			rowId = reportRow3.Id,
			rowVersion = Convert.ToBase64String(reportRow3.RowVersion ?? Array.Empty<byte>())
		});
	}

	private static bool TryParseRowVersion(string? value, out byte[] bytes)
	{
		bytes = Array.Empty<byte>();
		if (string.IsNullOrWhiteSpace(value))
		{
			return false;
		}
		try
		{
			bytes = Convert.FromBase64String(value);
			return bytes.Length != 0;
		}
		catch (FormatException)
		{
			return false;
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteRow(int rowId, string? rowVersion = null)
	{
		ReportRow row = await _db.ReportRows.Include((ReportRow r) => r.Report).ThenInclude((Report rep) => rep.ReportingMonth).FirstOrDefaultAsync((ReportRow r) => r.Id == rowId);
		if (row?.Report == null)
		{
			return Json(new
			{
				success = false
			});
		}
		if (!(await CanViewEmployeeReportAsync(row.Report.UserId)) || !CanEditReport(row.Report))
		{
			return Json(new
			{
				success = false,
				error = EditBlockMessage(row.Report)
			});
		}
		if (TryParseRowVersion(rowVersion, out byte[] bytes))
		{
			_db.Entry(row).OriginalValues["RowVersion"] = bytes;
		}
		_db.ReportRows.Remove(row);
		try
		{
			await _db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			return Json(new
			{
				success = false,
				error = ConcurrencyConflictMessage
			});
		}
		return Json(new
		{
			success = true
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Submit(int reportId, int? allocationId = null, string? rowVersion = null, string? returnUrl = null)
	{
		Report report = await _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return NotFound();
		}
		if (!(await CanViewEmployeeReportAsync(report.UserId)) || !CanEditReport(report))
		{
			base.TempData["Errors"] = EditBlockMessage(report);
			return RedirectToReport(report.UserId, allocationId.GetValueOrDefault(), reportId, returnUrl);
		}
		ValidationResult validationResult = await _validator.ValidateSubmitAsync(report, report.User, report.ReportingMonth);
		if (!validationResult.IsValid)
		{
			base.TempData["Errors"] = string.Join("|", validationResult.Errors);
			return RedirectToReport(report.UserId, allocationId.GetValueOrDefault(), reportId, returnUrl);
		}
		if (TryParseRowVersion(rowVersion, out byte[] bytes))
		{
			_db.Entry(report).OriginalValues["RowVersion"] = bytes;
		}
		try
		{
			await _statusService.SubmitReportAsync(reportId, _currentUser.UserId);
		}
		catch (DbUpdateConcurrencyException)
		{
			base.TempData["Errors"] = ConcurrencyConflictMessage;
			return RedirectToAction("Index", new
			{
				userId = report.UserId,
				allocationId = allocationId
			});
		}
		base.TempData["Success"] = "\u05d4\u05d3\u05d9\u05d5\u05d5\u05d7 \u05d4\u05d5\u05d2\u05e9 \u05d1\u05d4\u05e6\u05dc\u05d7\u05d4";
		return RedirectToReport(report.UserId, allocationId.GetValueOrDefault(), reportId, returnUrl);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "CanApproveReports")]
	public async Task<IActionResult> Approve(int reportId, string? returnUrl = null, string? rowVersion = null)
	{
		if (!(await CanApproveReportAsync(reportId)))
		{
			return Forbid();
		}
		Report report = await _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return NotFound();
		}
		if (report.StatusId == 4)
		{
			base.TempData["Success"] = "\u05d4\u05d3\u05d9\u05d5\u05d5\u05d7 \u05d0\u05d5\u05e9\u05e8";
			if (WantsJson())
			{
				return Json(new { success = true, status = "Approved", reportId });
			}
			return RedirectBackToDashboard(returnUrl);
		}
		if (TryParseRowVersion(rowVersion, out byte[] bytes))
		{
			_db.Entry(report).OriginalValues["RowVersion"] = bytes;
		}
		int previousStatus = report.StatusId;
		report.StatusId = 4;
		report.ApprovedAt = DateTime.UtcNow;
		report.ApprovedBy = _currentUser.UserId;
		report.UpdatedAt = DateTime.UtcNow;
		try
		{
			await _db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			base.TempData["Error"] = ConcurrencyConflictMessage;
			if (WantsJson())
			{
				return Json(new { success = false, error = ConcurrencyConflictMessage });
			}
			return RedirectBackToDashboard(returnUrl);
		}
		await _auditLog.LogAsync("Report.StatusChange", "Report", report.Id.ToString(CultureInfo.InvariantCulture), new
		{
			StatusId = previousStatus
		}, new { report.StatusId }, $"approved by user {_currentUser.UserId}");
		await TrySendApprovalEmailAsync(report);
		base.TempData["Success"] = "\u05d4\u05d3\u05d9\u05d5\u05d5\u05d7 \u05d0\u05d5\u05e9\u05e8";
		if (WantsJson())
		{
			return Json(new { success = true, status = "Approved", reportId });
		}
		return RedirectBackToDashboard(returnUrl);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "CanApproveReports")]
	public async Task<IActionResult> Reject(int reportId, string rejectionReason, string? returnUrl = null, string? rowVersion = null)
	{
		if (!(await CanApproveReportAsync(reportId)))
		{
			return Forbid();
		}
		if (string.IsNullOrWhiteSpace(rejectionReason))
		{
			base.TempData["Error"] = "\u05d9\u05e9 \u05dc\u05e6\u05d9\u05d9\u05df \u05e1\u05d9\u05d1\u05ea \u05d3\u05d7\u05d9\u05d9\u05d4";
			if (WantsJson())
			{
				return Json(new { success = false, error = "\u05d9\u05e9 \u05dc\u05e6\u05d9\u05d9\u05df \u05e1\u05d9\u05d1\u05ea \u05d3\u05d7\u05d9\u05d9\u05d4" });
			}
			return RedirectBackToDashboard(returnUrl);
		}
		Report report = await _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return NotFound();
		}
		if (report.StatusId == 5 && string.Equals(report.RejectionReason ?? string.Empty, rejectionReason, StringComparison.Ordinal))
		{
			base.TempData["Success"] = "\u05d4\u05d3\u05d9\u05d5\u05d5\u05d7 \u05d4\u05d5\u05d7\u05d6\u05e8 \u05dc\u05ea\u05d9\u05e7\u05d5\u05df";
			if (WantsJson())
			{
				return Json(new { success = true, status = "Rejected", reportId });
			}
			return RedirectBackToDashboard(returnUrl);
		}
		if (TryParseRowVersion(rowVersion, out byte[] bytes))
		{
			_db.Entry(report).OriginalValues["RowVersion"] = bytes;
		}
		int previousStatus = report.StatusId;
		report.StatusId = 5;
		report.RejectionReason = rejectionReason;
		report.RejectedAt = DateTime.UtcNow;
		report.RejectedBy = _currentUser.UserId;
		report.UpdatedAt = DateTime.UtcNow;
		try
		{
			await _db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			base.TempData["Error"] = ConcurrencyConflictMessage;
			if (WantsJson())
			{
				return Json(new { success = false, error = ConcurrencyConflictMessage });
			}
			return RedirectBackToDashboard(returnUrl);
		}
		await _auditLog.LogAsync("Report.StatusChange", "Report", report.Id.ToString(CultureInfo.InvariantCulture), new
		{
			StatusId = previousStatus
		}, new { report.StatusId, rejectionReason }, $"rejected by user {_currentUser.UserId}");
		await TrySendRejectionEmailAsync(report, rejectionReason);
		base.TempData["Success"] = "\u05d4\u05d3\u05d9\u05d5\u05d5\u05d7 \u05d4\u05d5\u05d7\u05d6\u05e8 \u05dc\u05ea\u05d9\u05e7\u05d5\u05df";
		if (WantsJson())
		{
			return Json(new { success = true, status = "Rejected", reportId });
		}
		return RedirectBackToDashboard(returnUrl);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> UploadExcel(int reportId, int allocationId, IFormFile file, string? returnUrl = null)
	{
		Report report = await _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return NotFound();
		}
		if (!(await CanViewEmployeeReportAsync(report.UserId)) || !CanEditReport(report))
		{
			base.TempData["Errors"] = EditBlockMessage(report);
			return RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
		}
		if (file == null || file.Length == 0L)
		{
			base.TempData["Errors"] = "\u05dc\u05d0 \u05e0\u05d1\u05d7\u05e8 \u05e7\u05d5\u05d1\u05e5 \u05d0\u05e7\u05e1\u05dc";
			return RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
		}
		string extension = Path.GetExtension(file.FileName);
		if (!extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
		{
			base.TempData["Errors"] = "\u05e0\u05d9\u05ea\u05df \u05dc\u05d4\u05e2\u05dc\u05d5\u05ea \u05e7\u05d5\u05d1\u05e5 xlsx \u05d1\u05dc\u05d1\u05d3";
			return RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
		}
		Stream stream = file.OpenReadStream();
		IActionResult result2;
		try
		{
			ExcelImportResult result = await _excelImportService.ImportAsync(reportId, allocationId, stream, _currentUser.UserId);
			if (!result.Success)
			{
				string errorId = Guid.NewGuid().ToString("N");
				string text = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "excel-errors");
				Directory.CreateDirectory(text);
				await System.IO.File.WriteAllBytesAsync(Path.Combine(text, errorId + ".xlsx"), CreateImportErrorsExcel(result.Errors));
				base.TempData["Errors"] = BuildImportErrorSummary(result.Errors);
				base.TempData["ExcelErrorFile"] = base.Url?.Content("~/uploads/excel-errors/" + errorId + ".xlsx") ?? ("/uploads/excel-errors/" + errorId + ".xlsx");
				await SendImportFailureEmailAsync(report, result.Errors);
				result2 = RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
			}
			else
			{
				base.TempData["Success"] = $"\u05d9\u05d5\u05d1\u05d0\u05d5 {result.ImportedRows} \u05e9\u05d5\u05e8\u05d5\u05ea \u05de\u05d0\u05e7\u05e1\u05dc";
				result2 = RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
			}
		}
		finally
		{
			if (stream != null)
			{
				await stream.DisposeAsync();
			}
		}
		return result2;
	}

	private async Task SendImportFailureEmailAsync(Report report, IReadOnlyCollection<string> errors)
	{
		if (report.User == null)
		{
			return;
		}
		if (string.IsNullOrWhiteSpace(report.User.Email))
		{
			_logger.LogInformation("Skipping BatchImportErrors email for user {UserId} - no email address on file", report.UserId);
			return;
		}
		string value = string.Join("\n", errors);
		ReportingMonth reportingMonth = report.ReportingMonth;
		try
		{
			await _emailService.SendAsync(report.User.Email, report.User.FirstName + " " + report.User.LastName, "BatchImportErrors", new Dictionary<string, string>
			{
				["UploaderName"] = report.User.FirstName + " " + report.User.LastName,
				["EmployeeName"] = report.User.FirstName + " " + report.User.LastName,
				["ErrorsCount"] = errors.Count.ToString(CultureInfo.InvariantCulture),
				["ErrorList"] = value,
				["Month"] = reportingMonth?.Month.ToString(CultureInfo.InvariantCulture) ?? "",
				["Year"] = reportingMonth?.Year.ToString(CultureInfo.InvariantCulture) ?? ""
			});
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "Failed to enqueue BatchImportErrors email for user {UserId}", report.UserId);
		}
	}

	private static string BuildImportErrorSummary(IReadOnlyCollection<string> errors)
	{
		string[] source = errors.Take(3).Select(TrimImportError).ToArray();
		string text = string.Join("|", source);
		if (errors.Count > source.Length)
		{
			text = text + $"|Excel import found {errors.Count} errors. Open the Excel error file for the full list.";
		}
		return text;
	}

	private static byte[] CreateImportErrorsExcel(IEnumerable<string> errors)
	{
		using XLWorkbook workbook = new XLWorkbook();
		IXLWorksheet worksheet = workbook.Worksheets.Add("שגיאות יבוא");
		worksheet.RightToLeft = true;
		worksheet.Cell(1, 1).Value = "מספר";
		worksheet.Cell(1, 2).Value = "שגיאה";
		int row = 2;
		foreach (string error in errors)
		{
			worksheet.Cell(row, 1).Value = row - 1;
			worksheet.Cell(row, 2).Value = error ?? string.Empty;
			row++;
		}
		worksheet.Row(1).Style.Font.Bold = true;
		worksheet.Columns().AdjustToContents();
		using MemoryStream stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}

	private static string TrimImportError(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return string.Empty;
		}
		value = value.Trim();
		return (value.Length <= 220) ? value : (value.Substring(0, 220) + "...");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> UploadAttachment(int reportId, IFormFile file, string? description)
	{
		Report report = await _db.Reports.Include((Report r) => r.ReportingMonth).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return Json(new
			{
				success = false,
				error = "\u05d3\u05d9\u05d5\u05d5\u05d7 \u05dc\u05d0 \u05e0\u05de\u05e6\u05d0"
			});
		}
		if (!(await CanViewEmployeeReportAsync(report.UserId)) || !CanEditReport(report))
		{
			return Json(new
			{
				success = false,
				error = EditBlockMessage(report)
			});
		}
		if (file == null || file.Length == 0L)
		{
			return Json(new
			{
				success = false,
				error = "\u05dc\u05d0 \u05e0\u05d1\u05d7\u05e8 \u05e7\u05d5\u05d1\u05e5"
			});
		}
		if (file.Length > 10485760)
		{
			return Json(new
			{
				success = false,
				error = "\u05d2\u05d5\u05d3\u05dc \u05d4\u05e7\u05d5\u05d1\u05e5 \u05d7\u05d5\u05e8\u05d2 \u05de\u05d4\u05de\u05d5\u05ea\u05e8"
			});
		}
		string extension = Path.GetExtension(file.FileName);
		if (!AllowedAttachmentExtensions.Contains(extension))
		{
			return Json(new
			{
				success = false,
				error = "\u05e1\u05d5\u05d2 \u05d4\u05e7\u05d5\u05d1\u05e5 \u05d0\u05d9\u05e0\u05d5 \u05e0\u05ea\u05de\u05da. \u05e0\u05d9\u05ea\u05df \u05dc\u05d4\u05e2\u05dc\u05d5\u05ea PDF, Word \u05d0\u05d5 Excel \u05d1\u05dc\u05d1\u05d3"
			});
		}
		string text = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "attachments");
		Directory.CreateDirectory(text);
		string fileName = $"{Guid.NewGuid()}{extension}";
		string path = Path.Combine(text, fileName);
		FileStream stream = new FileStream(path, FileMode.CreateNew);
		try
		{
			await file.CopyToAsync(stream);
		}
		finally
		{
			if (stream != null)
			{
				await stream.DisposeAsync();
			}
		}
		DocumentAttachment attachment = new DocumentAttachment
		{
			ReportId = reportId,
			FileName = Path.GetFileName(file.FileName),
			Description = NormalizeAttachmentDescription(description),
			FilePath = "/uploads/attachments/" + fileName,
			FileSize = file.Length,
			MimeType = file.ContentType,
			UploadedAt = DateTime.UtcNow,
			UploadedBy = _currentUser.UserId
		};
		_db.DocumentAttachments.Add(attachment);
		await _db.SaveChangesAsync();
		return Json(new
		{
			success = true,
			id = attachment.Id,
			fileName = attachment.FileName,
			description = attachment.Description
		});
	}

	private static string? NormalizeAttachmentDescription(string? description)
	{
		if (string.IsNullOrWhiteSpace(description))
		{
			return null;
		}
		description = description.Trim();
		if (description.Length > 1000)
		{
			return description.Substring(0, 1000);
		}
		return description;
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteAttachment(int attachmentId)
	{
		DocumentAttachment attachment = await _db.DocumentAttachments.Include((DocumentAttachment a) => a.Report).ThenInclude((Report rep) => rep.ReportingMonth).Include((DocumentAttachment a) => a.ReportRow)
			.ThenInclude((ReportRow r) => r.Report)
			.ThenInclude((Report rep) => rep.ReportingMonth)
			.FirstOrDefaultAsync((DocumentAttachment a) => a.Id == attachmentId);
		Report report = attachment?.Report ?? attachment?.ReportRow?.Report;
		if (attachment == null || report == null)
		{
			return Json(new
			{
				success = false
			});
		}
		if (!(await CanViewEmployeeReportAsync(report.UserId)) || !CanEditReport(report))
		{
			return Json(new
			{
				success = false,
				error = EditBlockMessage(report)
			});
		}
		string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", attachment.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
		if (System.IO.File.Exists(path))
		{
			System.IO.File.Delete(path);
		}
		_db.DocumentAttachments.Remove(attachment);
		await _db.SaveChangesAsync();
		return Json(new
		{
			success = true
		});
	}

	private async Task<Dictionary<int, string>> BuildFrameworkLabelsAsync(IEnumerable<int?> frameworkIds)
	{
		List<int> ids = frameworkIds.Where((int? id) => id.HasValue).Select((int? id) => id.Value).Distinct().ToList();
		if (ids.Count == 0)
		{
			return new Dictionary<int, string>();
		}
		var frameworks = await _db.Frameworks.Where((Framework f) => ids.Contains(f.Id)).Select((Framework f) => new
		{
			f.Id,
			f.Description,
			f.InstitutionSymbol
		}).ToListAsync();
		List<int> symbols = frameworks.Select(f =>
		{
			int value;
			return int.TryParse(f.InstitutionSymbol, out value) ? (int?)value : null;
		}).Where((int? value) => value.HasValue).Select((int? value) => value.Value).Distinct().ToList();
		var institutions = await _db.Institutions.Include((Institution i) => i.Locality).Where((Institution i) => symbols.Contains(i.InstitutionSymbol)).Select((Institution i) => new
		{
			i.InstitutionSymbol,
			i.Name,
			LocalityName = i.Locality != null ? i.Locality.Description : string.Empty
		}).ToListAsync();
		Dictionary<int, string> result = new Dictionary<int, string>();
		foreach (var framework in frameworks)
		{
			int symbol;
			var institution = int.TryParse(framework.InstitutionSymbol, out symbol) ? institutions.FirstOrDefault(i => i.InstitutionSymbol == symbol) : null;
			string name = !string.IsNullOrWhiteSpace(institution?.Name) ? institution.Name : framework.Description;
			List<string> labelParts = new List<string>();
			foreach (string part in new[] { institution?.LocalityName, framework.InstitutionSymbol, name }.Where((string part) => !string.IsNullOrWhiteSpace(part)))
			{
				string trimmed = part.Trim();
				if (!labelParts.Any((string existing) => string.Equals(existing, trimmed, StringComparison.OrdinalIgnoreCase)))
				{
					labelParts.Add(trimmed);
				}
			}
			string label = string.Join(", ", labelParts);
			result[framework.Id] = string.IsNullOrWhiteSpace(label) ? framework.Description : label;
		}
		return result;
	}

	private static void ApplyFrameworkLabels(IEnumerable<ReportRow> rows, IReadOnlyDictionary<int, string> labels)
	{
		foreach (ReportRow row in rows)
		{
			if (row.Framework != null && labels.TryGetValue(row.Framework.Id, out string frameworkLabel))
			{
				row.Framework.Description = frameworkLabel;
			}
			if (row.ConclusionFramework != null && labels.TryGetValue(row.ConclusionFramework.Id, out string conclusionLabel))
			{
				row.ConclusionFramework.Description = conclusionLabel;
			}
		}
	}

	private static bool IsNumberOnly(string? value)
	{
		return int.TryParse(value?.Trim(), out var _);
	}

	private static readonly string[] NonCityLocalityTokens = new[]
	{
		"בית ספר", "בתי ספר", "בי\"ס", "בי'ס", "אולפנ", "אורט", "מח\"ט", "מועדונית", "מרכז נוער", "מרכזי חינוך", "מרכזים לגיל הרך", "מרכז לגיל הרך", "גיל הרך",
		"עוגנים", "מסגרות", "כיתות", "על יסודי", "תיכון", "ישיבה", "ישיבת", "תורה", "תלמוד", "חינוך", "אמי\"ת", "אמי״ת", "עמל", "הילה ", "בית חם",
		"תעשית", "חברה וטבע", "ברסלב", "לצעירים", "מדרשה", "מכנובקא", "ק.הרצוג", "ברנקו", "משכן", "אהבת", "באר אברהם", "בית דוד", "בית אליהו",
		"בית צבי", "בית רבן", "בני אהרון", "אמרי", "אקרא", "היכל"
	};

	private static bool IsCityLocalityText(string? value)
	{
		string text = value?.Trim() ?? string.Empty;
		return !string.IsNullOrWhiteSpace(text) && !IsNumberOnly(text) && !NonCityLocalityTokens.Any((string token) => text.Contains(token, StringComparison.OrdinalIgnoreCase));
	}

	private static bool IsCityLocality(Locality locality)
	{
		return locality != null && IsCityLocalityText(locality.Description);
	}

	private static int NumberSortKey(string? value)
	{
		return int.TryParse(value?.Trim(), out int parsed) ? parsed : int.MaxValue;
	}

	private bool CanEditReport(Report report)
	{
		if (report.Id == 0)
		{
			return false;
		}
		if (report.IsArchived)
		{
			return false;
		}
		if (IsDeadlineOverrideRole())
		{
			return true;
		}
		if (report.ReportingMonth != null && !report.ReportingMonth.IsActive)
		{
			return false;
		}
		if (IsDeadlinePassed(report.ReportingMonth))
		{
			return false;
		}
		int statusId = report.StatusId;
		if ((uint)(statusId - 3) <= 1u)
		{
			return false;
		}
		if (_currentUser.UserRole != UserRoleEnum.ProjectCoordinator)
		{
			return report.UserId == _currentUser.UserId;
		}
		return true;
	}

	private string EditBlockMessage(Report report)
	{
		if (!IsDeadlinePassed(report.ReportingMonth) || IsDeadlineOverrideRole())
		{
			return "\u05d0\u05d9\u05df \u05d4\u05e8\u05e9\u05d0\u05d4 \u05dc\u05e2\u05e8\u05d5\u05da \u05d3\u05d9\u05d5\u05d5\u05d7 \u05d6\u05d4";
		}
		return DeadlinePassedMessage;
	}

	private bool IsDeadlineOverrideRole()
	{
		UserRoleEnum userRole = _currentUser.UserRole;
		return (uint)(userRole - 1) <= 2u;
	}


	private bool WantsJson()
	{
		string accept = base.Request.Headers["Accept"].ToString();
		string requestedWith = base.Request.Headers["X-Requested-With"].ToString();
		return accept.IndexOf("application/json", StringComparison.OrdinalIgnoreCase) >= 0 || string.Equals(requestedWith, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
	}

	private IActionResult RedirectBackToDashboard(string? returnUrl)
	{
		string text = NormalizeLocalReturnUrl(returnUrl);
		if (!string.IsNullOrWhiteSpace(text))
		{
			return LocalRedirect(text);
		}
		return RedirectToAction("Index", "Dashboard");
	}

	private IActionResult RedirectToReport(int userId, int allocationId, int reportId, string? returnUrl)
	{
		return RedirectToAction("Index", new
		{
			userId = userId,
			allocationId = allocationId,
			reportId = reportId,
			returnUrl = NormalizeLocalReturnUrl(returnUrl)
		});
	}

	private async Task TrySendApprovalEmailAsync(Report report)
	{
		if (report.User?.Email == null)
		{
			return;
		}
		try
		{
			await _emailService.SendAsync(report.User.Email, report.User.FirstName + " " + report.User.LastName, "ReportApproved", new Dictionary<string, string>
			{
				["EmployeeName"] = report.User.FirstName + " " + report.User.LastName,
				["MonthName"] = report.ReportingMonth?.Description ?? string.Empty,
				["Month"] = report.ReportingMonth?.Month.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
				["Year"] = report.ReportingMonth?.Year.ToString(CultureInfo.InvariantCulture) ?? string.Empty
			});
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "EMAIL: Failed to send report approval email for report {ReportId}", report.Id);
		}
	}

	[HttpGet]
	public async Task<IActionResult> AllocationLookups(int allocationId, bool manual = false)
	{
		Allocation allocation = await _db.Allocations.AsNoTracking().FirstOrDefaultAsync((Allocation a) => a.Id == allocationId && a.IsActive);
		if (allocation == null)
		{
			return NotFound();
		}
		if (!(await CanViewEmployeeReportAsync(allocation.UserId)))
		{
			return Forbid();
		}
		var allFrameworkRows = await _db.Set<AllocationFramework>().AsNoTracking().Where((AllocationFramework x) => x.AllocationId == allocationId && x.Framework != null && x.Framework.IsActive).OrderBy((AllocationFramework x) => x.Framework.Description).Select((AllocationFramework x) => new
		{
			id = x.FrameworkId,
			text = x.Framework.Description,
			institutionSymbol = x.Framework.InstitutionSymbol
		}).ToListAsync();
		var institutionFrameworkRows = allFrameworkRows.Where(x => IsNumberOnly(x.institutionSymbol)).ToList();
		var conclusionFrameworkRows = allFrameworkRows.Where(x => !IsNumberOnly(x.institutionSymbol)).ToList();
		var frameworkRows = institutionFrameworkRows.Count > 0 ? institutionFrameworkRows : allFrameworkRows;
		Dictionary<int, string> frameworkLabels = await BuildFrameworkLabelsAsync(frameworkRows.Select(x => (int?)x.id));
		Dictionary<int, string> conclusionFrameworkLabels = await BuildFrameworkLabelsAsync(conclusionFrameworkRows.Select(x => (int?)x.id));
		var localities = (manual ? (await _db.Localities.AsNoTracking().Where((Locality x) => x.IsActive).OrderBy((Locality x) => x.Description).Select((Locality x) => new
		{
			id = x.Id,
			text = x.Description
		}).ToListAsync()) : (await _db.Set<AllocationLocality>().AsNoTracking().Where((AllocationLocality x) => x.AllocationId == allocationId && x.Locality != null && x.Locality.IsActive).OrderBy((AllocationLocality x) => x.Locality.Description).Select((AllocationLocality x) => new
		{
			id = x.LocalityId,
			text = x.Locality.Description
		}).ToListAsync())).Where(x => IsCityLocalityText(x.text)).ToList();
		var allClassRows = await _db.Set<AllocationClass>().AsNoTracking().Where((AllocationClass x) => x.AllocationId == allocationId && x.SchoolClass != null && x.SchoolClass.IsActive).Select((AllocationClass x) => new
		{
			id = x.ClassId,
			text = x.SchoolClass.Description
		}).ToListAsync();
		var classRows = allClassRows.Where(x => IsNumberOnly(x.text)).OrderBy(x => NumberSortKey(x.text)).ThenBy(x => x.text).ToList();
		var conclusionClassRows = allClassRows.Where(x => !IsNumberOnly(x.text)).OrderBy(x => x.text).ToList();
		return Json(new
		{
			districts = await _db.Set<AllocationDistrict>().AsNoTracking().Where((AllocationDistrict x) => x.AllocationId == allocationId && x.District != null && x.District.IsActive).OrderBy((AllocationDistrict x) => x.District.Description).Select((AllocationDistrict x) => new { id = x.DistrictId, text = x.District.Description }).ToListAsync(),
			localities,
			frameworks = frameworkRows.Select(x => new
			{
				x.id,
				text = frameworkLabels.TryGetValue(x.id, out string label) ? label : x.text
			}).ToList(),
			conclusionFrameworks = conclusionFrameworkRows.Select(x => new
			{
				x.id,
				text = conclusionFrameworkLabels.TryGetValue(x.id, out string label) ? label : x.text
			}).ToList(),
			educationalPrograms = await _db.Set<AllocationEducationalProgram>().AsNoTracking().Where((AllocationEducationalProgram x) => x.AllocationId == allocationId && x.EducationalProgram != null && x.EducationalProgram.IsActive).OrderBy((AllocationEducationalProgram x) => x.EducationalProgram.Description).Select((AllocationEducationalProgram x) => new { id = x.EducationalProgramId, text = x.EducationalProgram.Description }).ToListAsync(),
			domains = await _db.Set<AllocationDomain>().AsNoTracking().Where((AllocationDomain x) => x.AllocationId == allocationId && x.Domain != null && x.Domain.IsActive).OrderBy((AllocationDomain x) => x.Domain.Description).Select((AllocationDomain x) => new { id = x.DomainId, text = x.Domain.Description }).ToListAsync(),
			subjects = await _db.Set<AllocationSubject>().AsNoTracking().Where((AllocationSubject x) => x.AllocationId == allocationId && x.Subject != null && x.Subject.IsActive).OrderBy((AllocationSubject x) => x.Subject.Description).Select((AllocationSubject x) => new { id = x.SubjectId, text = x.Subject.Description }).ToListAsync(),
			discussionCodes = await _db.Set<AllocationDiscussionCode>().AsNoTracking().Where((AllocationDiscussionCode x) => x.AllocationId == allocationId && x.DiscussionCode != null && x.DiscussionCode.IsActive).OrderBy((AllocationDiscussionCode x) => x.DiscussionCode.Description).Select((AllocationDiscussionCode x) => new { id = x.DiscussionCodeId, text = x.DiscussionCode.Description }).ToListAsync(),
			classes = classRows,
			conclusionClasses = conclusionClassRows,
			gradeLevels = await _db.Set<AllocationGradeLevel>().AsNoTracking().Where((AllocationGradeLevel x) => x.AllocationId == allocationId && x.GradeLevel != null && x.GradeLevel.IsActive).OrderBy((AllocationGradeLevel x) => x.GradeLevel.Description).Select((AllocationGradeLevel x) => new { id = x.GradeLevelId, text = x.GradeLevel.Description }).ToListAsync(),
			locations = await _db.Set<AllocationLocalityDistrictNational>().AsNoTracking().Where((AllocationLocalityDistrictNational x) => x.AllocationId == allocationId && x.LocalityDistrictNational != null && x.LocalityDistrictNational.IsActive).OrderBy((AllocationLocalityDistrictNational x) => x.LocalityDistrictNational.Description).Select((AllocationLocalityDistrictNational x) => new { id = x.LocalityDistrictNationalId, text = x.LocalityDistrictNational.Description }).ToListAsync()
		});
	}

	[HttpGet]
	public async Task<IActionResult> ScopedForProgram(int allocationId, int programId)
	{
		Allocation allocation = await _db.Allocations.AsNoTracking().FirstOrDefaultAsync((Allocation a) => a.Id == allocationId && a.IsActive);
		if (allocation == null)
		{
			return NotFound();
		}
		if (!(await CanViewEmployeeReportAsync(allocation.UserId)))
		{
			return Forbid();
		}
		List<int> programIds = new List<int>();
		bool programAllowed = await _db.Set<AllocationProgram>().AnyAsync((AllocationProgram ap) => ap.AllocationId == allocationId && ap.ProgramId == programId);
		if (programAllowed)
		{
			programIds.Add(programId);
		}
		else
		{
			programIds = await ResolveProgramIdsForEducationalProgramAsync(allocationId, allocation.ProjectId, programId);
		}
		if (programIds.Count == 0)
		{
			return BadRequest();
		}
		int projectId = allocation.ProjectId;
		List<int> subjectIds = await ScopedAllocationIdsAsync("AllocationSubjects", "SubjectId", "ProjectProgramSubjects", allocationId, projectId, programIds);
		List<int> domainIds = await ScopedAllocationIdsAsync("AllocationDomains", "DomainId", "ProjectProgramDomains", allocationId, projectId, programIds);
		List<int> frameworkIds = await ScopedAllocationIdsAsync("AllocationFrameworks", "FrameworkId", "ProjectProgramFrameworks", allocationId, projectId, programIds);
		List<int> discussionCodeIds = await ScopedAllocationIdsAsync("AllocationDiscussionCodes", "DiscussionCodeId", "ProjectProgramDiscussionCodes", allocationId, projectId, programIds);
		var scopedFrameworkRows = await _db.Frameworks.Where((Framework x) => x.IsActive && frameworkIds.Contains(x.Id)).OrderBy((Framework x) => x.Description).Select((Framework x) => new { x.Id, x.Description, x.InstitutionSymbol }).ToListAsync();
		var scopedInstitutionFrameworkRows = scopedFrameworkRows.Where(x => IsNumberOnly(x.InstitutionSymbol)).ToList();
		var scopedRegularFrameworkRows = scopedInstitutionFrameworkRows.Count > 0 ? scopedInstitutionFrameworkRows : scopedFrameworkRows;
		Dictionary<int, string> frameworkLabels = await BuildFrameworkLabelsAsync(scopedRegularFrameworkRows.Select(x => (int?)x.Id));
		return Json(new
		{
			subjects = await _db.Subjects.Where((Subject x) => x.IsActive && subjectIds.Contains(x.Id)).OrderBy((Subject x) => x.Description).Select((Subject x) => new { id = x.Id, description = x.Description }).ToListAsync(),
			domains = await _db.Domains.Where((Domain x) => x.IsActive && domainIds.Contains(x.Id)).OrderBy((Domain x) => x.Description).Select((Domain x) => new { id = x.Id, description = x.Description }).ToListAsync(),
			frameworks = scopedRegularFrameworkRows.Select(x => new { id = x.Id, description = frameworkLabels.TryGetValue(x.Id, out string label) ? label : x.Description }),
			discussionCodes = await _db.DiscussionCodes.Where((DiscussionCode x) => x.IsActive && discussionCodeIds.Contains(x.Id)).OrderBy((DiscussionCode x) => x.Description).Select((DiscussionCode x) => new { id = x.Id, description = x.Description }).ToListAsync()
		});
	}

	private async Task TrySendRejectionEmailAsync(Report report, string rejectionReason)
	{
		if (report.User?.Email == null)
		{
			return;
		}
		try
		{
			await _emailService.SendAsync(report.User.Email, report.User.FirstName + " " + report.User.LastName, "ReportRejected", new Dictionary<string, string>
			{
				["EmployeeName"] = report.User.FirstName + " " + report.User.LastName,
				["MonthName"] = report.ReportingMonth?.Description ?? string.Empty,
				["Month"] = report.ReportingMonth?.Month.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
				["Year"] = report.ReportingMonth?.Year.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
				["RejectionReason"] = rejectionReason
			});
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "EMAIL: Failed to send report rejection email for report {ReportId}", report.Id);
		}
	}

	private static string SafeFilePart(string value)
	{
		foreach (char invalidChar in Path.GetInvalidFileNameChars())
		{
			value = value.Replace(invalidChar, '_');
		}
		return string.IsNullOrWhiteSpace(value) ? "employee" : value;
	}

	private static string NormalizeDigits(string value)
	{
		return new string((value ?? string.Empty).Where(char.IsDigit).ToArray());
	}

	private string? NormalizeLocalReturnUrl(string? returnUrl)
	{
		if (string.IsNullOrWhiteSpace(returnUrl) || !base.Url.IsLocalUrl(returnUrl))
		{
			return null;
		}
		string value = base.HttpContext.Request.PathBase.Value;
		if (string.IsNullOrEmpty(value) || returnUrl.StartsWith(value, StringComparison.OrdinalIgnoreCase))
		{
			return returnUrl;
		}
		if (!returnUrl.StartsWith("/", StringComparison.Ordinal))
		{
			return returnUrl;
		}
		return value + returnUrl;
	}

	private async Task<bool> CanViewEmployeeReportAsync(int employeeUserId)
	{
		if (_currentUser.UserRole == UserRoleEnum.Employee)
		{
			return employeeUserId == _currentUser.UserId;
		}
		UserRoleEnum userRole = _currentUser.UserRole;
		if ((uint)(userRole - 1) <= 2u)
		{
			return true;
		}
		return await IsEmployeeInInspectorScopeAsync(employeeUserId);
	}

	private async Task<bool> CanApproveReportAsync(int reportId)
	{
		Report report = await _db.Reports.FindAsync(reportId);
		if (report == null || report.StatusId != 3)
		{
			return false;
		}
		UserRoleEnum userRole = _currentUser.UserRole;
		if ((uint)(userRole - 1) <= 2u)
		{
			return true;
		}
		bool flag = _currentUser.UserRole == UserRoleEnum.InspectorApproval;
		bool flag2 = flag;
		if (flag2)
		{
			flag2 = await IsEmployeeInInspectorScopeAsync(report.UserId);
		}
		return flag2;
	}

	private async Task<bool> IsEmployeeInInspectorScopeAsync(int employeeUserId)
	{
		UserRoleEnum userRole = _currentUser.UserRole;
		if ((uint)(userRole - 4) > 1u)
		{
			return false;
		}
		List<InspectorAssignment> list = await _db.InspectorAssignments.Where((InspectorAssignment a) => a.InspectorUserId == _currentUser.UserId).ToListAsync();
		if (!list.Any())
		{
			return false;
		}
		foreach (InspectorAssignment item in list)
		{
			IQueryable<Allocation> source = _db.Allocations.Where((Allocation a) => a.UserId == employeeUserId && a.IsActive);
			if (item.DistrictId.HasValue)
			{
				int districtId = item.DistrictId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationDistrict>().Any((AllocationDistrict ad) => ad.AllocationId == a.Id && ad.DistrictId == districtId));
			}
			if (item.SectorId.HasValue)
			{
				int sectorId = item.SectorId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationSector>().Any((AllocationSector s) => s.AllocationId == a.Id && s.SectorId == sectorId));
			}
			if (item.ProgramId.HasValue)
			{
				int programId = item.ProgramId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationProgram>().Any((AllocationProgram p) => p.AllocationId == a.Id && p.ProgramId == programId));
			}
			if (await source.AnyAsync())
			{
				return true;
			}
		}
		return false;
	}

	private static void CopyEditableFields(ReportRow source, ReportRow target)
	{
		target.MeetingDate = source.MeetingDate;
		target.MeetingDuration = source.MeetingDuration;
		target.DistrictId = source.DistrictId;
		target.LocalityId = source.LocalityId;
		target.FrameworkId = source.FrameworkId;
		target.EducationalProgramId = source.EducationalProgramId;
		target.DomainId = source.DomainId;
		target.Subject1Id = source.Subject1Id;
		target.Subject2Id = source.Subject2Id;
		target.DiscussionCodeId = source.DiscussionCodeId;
		target.ReportTypeId = source.ReportTypeId;
		target.ConclusionClassId = source.ConclusionClassId;
		target.ConclusionFrameworkId = source.ConclusionFrameworkId;
		target.ConclusionLocationId = source.ConclusionLocationId;
		target.GradeLevelId = source.GradeLevelId;
		target.ClassId = source.ClassId;
		target.Notes = source.Notes;
	}

	private async Task<HashSet<string>> GetRequiredReportFieldsAsync()
	{
		string text = await (from c in _db.SystemConstants
			where c.Key == "RequiredReportFields"
			select c.Value).FirstOrDefaultAsync();
		return (string.IsNullOrWhiteSpace(text) ? "AllocationId,DistrictId,LocalityId,FrameworkId,EducationalProgramId,DomainId,Subject1Id,MeetingDate,MeetingDuration" : text).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet<string>(StringComparer.OrdinalIgnoreCase);
	}

	private async Task<List<int>> ScopedAllocationIdsAsync(string allocationTable, string idColumn, string scopeTable, int allocationId, int projectId, int programId)
	{
		List<int> values = new List<int>();
		DbConnection connection = _db.Database.GetDbConnection();
		bool shouldClose = connection.State == System.Data.ConnectionState.Closed;
		if (shouldClose)
		{
			await connection.OpenAsync();
		}
		try
		{
			using DbCommand command = connection.CreateCommand();
			command.CommandText = $"SELECT DISTINCT a.{idColumn} FROM dbo.{allocationTable} a INNER JOIN dbo.{scopeTable} s ON s.{idColumn} = a.{idColumn} WHERE a.AllocationId = @allocationId AND s.ProjectId = @projectId AND s.ProgramId = @programId";
			AddDbParameter(command, "@allocationId", allocationId);
			AddDbParameter(command, "@projectId", projectId);
			AddDbParameter(command, "@programId", programId);
			using DbDataReader reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				values.Add(reader.GetInt32(0));
			}
		}
		finally
		{
			if (shouldClose)
			{
				await connection.CloseAsync();
			}
		}
		return values;
	}

	private async Task<List<int>> ScopedAllocationIdsAsync(string allocationTable, string idColumn, string scopeTable, int allocationId, int projectId, IReadOnlyCollection<int> programIds)
	{
		HashSet<int> values = new HashSet<int>();
		foreach (int programId in programIds)
		{
			foreach (int value in await ScopedAllocationIdsAsync(allocationTable, idColumn, scopeTable, allocationId, projectId, programId))
			{
				values.Add(value);
			}
		}
		return values.ToList();
	}

	private async Task<List<int>> ResolveProgramIdsForEducationalProgramAsync(int allocationId, int projectId, int educationalProgramId)
	{
		List<int> values = new List<int>();
		DbConnection connection = _db.Database.GetDbConnection();
		bool shouldClose = connection.State == System.Data.ConnectionState.Closed;
		if (shouldClose)
		{
			await connection.OpenAsync();
		}
		try
		{
			using DbCommand command = connection.CreateCommand();
			command.CommandText = "SELECT DISTINCT ap.ProgramId FROM dbo.AllocationPrograms ap INNER JOIN dbo.ProjectProgramEducationalPrograms ppe ON ppe.ProjectId = @projectId AND ppe.ProgramId = ap.ProgramId AND ppe.EducationalProgramId = @educationalProgramId WHERE ap.AllocationId = @allocationId";
			AddDbParameter(command, "@allocationId", allocationId);
			AddDbParameter(command, "@projectId", projectId);
			AddDbParameter(command, "@educationalProgramId", educationalProgramId);
			using DbDataReader reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				values.Add(reader.GetInt32(0));
			}
		}
		finally
		{
			if (shouldClose)
			{
				await connection.CloseAsync();
			}
		}
		return values;
	}

	private static void AddDbParameter(DbCommand command, string name, object value)
	{
		DbParameter parameter = command.CreateParameter();
		parameter.ParameterName = name;
		parameter.Value = value;
		command.Parameters.Add(parameter);
	}
}


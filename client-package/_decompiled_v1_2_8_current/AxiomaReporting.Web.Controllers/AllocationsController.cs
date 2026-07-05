using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

[Authorize]
[Route("allocations")]
public class AllocationsController : Controller
{
	[CompilerGenerated]
	private static class _003C_003Eo__9
	{
		public static CallSite<Func<CallSite, object, AllocationListFilterModel, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__1;

		public static CallSite<Func<CallSite, object, int?, object>> _003C_003Ep__2;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__3;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__4;

		public static CallSite<Func<CallSite, object, int, object>> _003C_003Ep__5;

		public static CallSite<Func<CallSite, object, int, object>> _003C_003Ep__6;

		public static CallSite<Func<CallSite, object, int, object>> _003C_003Ep__7;

		public static CallSite<Func<CallSite, object, User, object>> _003C_003Ep__8;

		public static CallSite<Func<CallSite, object, List<Project>, object>> _003C_003Ep__9;

		public static CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__10;

		public static CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__11;

		public static CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__12;

		public static CallSite<Func<CallSite, object, decimal[], object>> _003C_003Ep__13;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__14;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__15;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__16;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__17;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__18;
	}

	private readonly AppDbContext _db;

	private readonly ICurrentUserService _currentUser;

	private static readonly decimal[] OUTPUT_DURATION_OPTIONS = new decimal[6] { 0.5m, 1m, 1.5m, 2m, 2.5m, 3m };

	public AllocationsController(AppDbContext db, ICurrentUserService currentUser)
	{
		_db = db;
		_currentUser = currentUser;
	}

	[HttpGet("")]
	public async Task<IActionResult> Index(AllocationListFilterModel filter)
	{
		filter.Normalize();
		IQueryable<Allocation> scopedQuery = await BuildScopedAllocationQueryAsync(_db, _currentUser.UserId, _currentUser.UserRole);
		int total = await ApplyAllocationFilters(scopedQuery, filter).CountAsync();
		int page = Math.Max(filter.Page, 1);
		int pageSize2 = filter.PageSize;
		bool flag = ((pageSize2 < 1 || pageSize2 > 500) ? true : false);
		int pageSize = (flag ? 25 : filter.PageSize);
		List<Allocation> allocations = await ApplyAllocationSort(ApplyAllocationFilters(scopedQuery, filter), filter.SortBy, filter.SortDesc).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
		await PopulateViewBagsAsync(scopedQuery, filter, page, pageSize, total);
		return View("~/Views/Employee/AllocationList.cshtml", allocations);
	}

	[HttpGet("create")]
	[Authorize(Policy = "AdminPMOrCoordinator")]
	public async Task<IActionResult> Create(string? idNumber = null, string? employeeCode = null, string? firstName = null, string? lastName = null)
	{
		IQueryable<User> query = _db.Users.Where((User u) => u.StatusId == 1 && u.IsReportingEmployee);
		if (!string.IsNullOrWhiteSpace(idNumber))
		{
			string value = NormalizeDigits(idNumber);
			query = query.Where((User u) => u.IdNumber.Replace("-", "").Replace(" ", "").Contains(value));
		}
		if (!string.IsNullOrWhiteSpace(employeeCode))
		{
			query = query.Where((User u) => u.EmployeeCode.Contains(employeeCode.Trim()));
		}
		if (!string.IsNullOrWhiteSpace(firstName))
		{
			query = query.Where((User u) => u.FirstName.Contains(firstName.Trim()));
		}
		if (!string.IsNullOrWhiteSpace(lastName))
		{
			query = query.Where((User u) => u.LastName.Contains(lastName.Trim()));
		}
		List<User> employees = await query.OrderBy((User u) => u.LastName).ThenBy((User u) => u.FirstName).Take(100).ToListAsync();
		return View("~/Views/Allocations/Create.cshtml", new AllocationEmployeePickerViewModel
		{
			IdNumber = idNumber,
			EmployeeCode = employeeCode,
			FirstName = firstName,
			LastName = lastName,
			Employees = employees
		});
		StringBuilder html = new StringBuilder();
		html.Append("<!doctype html><html lang=\"he\" dir=\"rtl\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link rel=\"stylesheet\" href=\"/lib/bootstrap/dist/css/bootstrap.min.css\"><title>הוספת הקצאה</title></head><body><main class=\"container mt-4\"><div class=\"d-flex justify-content-between align-items-center mb-3\"><h3>הוספת הקצאה לעובד</h3><a class=\"btn btn-outline-secondary\" href=\"/allocations\">חזרה להקצאות</a></div>");
		html.Append("<form method=\"get\" action=\"/allocations/create\" class=\"card card-body mb-3\"><div class=\"row g-2 align-items-end\"><div class=\"col-md-3\"><label class=\"form-label\">ת.ז</label><input class=\"form-control\" name=\"idNumber\" value=\"").Append(WebUtility.HtmlEncode(idNumber ?? "")).Append("\"></div><div class=\"col-md-3\"><label class=\"form-label\">קוד עובד</label><input class=\"form-control\" name=\"employeeCode\" value=\"").Append(WebUtility.HtmlEncode(employeeCode ?? "")).Append("\"></div><div class=\"col-md-3\"><label class=\"form-label\">שם פרטי</label><input class=\"form-control\" name=\"firstName\" value=\"").Append(WebUtility.HtmlEncode(firstName ?? "")).Append("\"></div><div class=\"col-md-3\"><label class=\"form-label\">שם משפחה</label><input class=\"form-control\" name=\"lastName\" value=\"").Append(WebUtility.HtmlEncode(lastName ?? "")).Append("\"></div><div class=\"col-12\"><button class=\"btn btn-primary\">חפש</button></div></div></form>");
		html.Append("<div class=\"card\"><div class=\"card-header\">בחר עובד</div><div class=\"table-responsive\"><table class=\"table table-sm table-hover mb-0\"><thead><tr><th>ת.ז</th><th>קוד עובד</th><th>שם פרטי</th><th>שם משפחה</th><th></th></tr></thead><tbody>");
		if (employees.Count == 0)
		{
			html.Append("<tr><td colspan=\"5\" class=\"text-muted text-center py-4\">לא נמצאו עובדים תואמים</td></tr>");
		}
		foreach (User employee in employees)
		{
			html.Append("<tr><td>").Append(WebUtility.HtmlEncode(employee.IdNumber)).Append("</td><td>").Append(WebUtility.HtmlEncode(employee.EmployeeCode)).Append("</td><td>").Append(WebUtility.HtmlEncode(employee.FirstName)).Append("</td><td>").Append(WebUtility.HtmlEncode(employee.LastName)).Append("</td><td><a class=\"btn btn-sm btn-success\" href=\"/Employee/").Append(employee.Id).Append("/Allocations/Create\">הוסף הקצאה</a></td></tr>");
		}
		html.Append("</tbody></table></div></div></main></body></html>");
		return Content(html.ToString(), "text/html; charset=utf-8");
	}

	[HttpGet("{allocationId:int}")]
	public async Task<IActionResult> Details(int allocationId)
	{
		Allocation allocation = await (await BuildScopedAllocationQueryAsync(_db, _currentUser.UserId, _currentUser.UserRole)).FirstOrDefaultAsync((Allocation a) => a.Id == allocationId);
		if (allocation == null)
		{
			return NotFound();
		}
		UserRoleEnum userRole = _currentUser.UserRole;
		if ((uint)(userRole - 1) <= 2u)
		{
			return RedirectToAction("EditAllocation", "Employee", new
			{
				id = allocation.UserId,
				allocationId = allocation.Id
			});
		}
		return View("~/Views/MyAllocations/Details.cshtml", allocation);
	}

	[HttpGet("export")]
	public async Task<IActionResult> ExportExcel(AllocationListFilterModel filter)
	{
		filter.Normalize();
		List<Allocation> list = await ApplyAllocationSort(ApplyAllocationFilters(await BuildScopedAllocationQueryAsync(_db, _currentUser.UserId, _currentUser.UserRole), filter), filter.SortBy, filter.SortDesc).ToListAsync();
		using XLWorkbook xLWorkbook = new XLWorkbook();
		IXLWorksheet iXLWorksheet = xLWorkbook.Worksheets.Add("הקצאות עובדים");
		iXLWorksheet.RightToLeft = true;
		string[] array = new string[16]
		{
			"פרויקט", "תוכנית", "מחוז", "מגזר", "ת.ז", "קוד עובד", "שם פרטי", "שם משפחה", "היקף פעילות חודשי", "היקף פעילות יומי",
			"היקף פעילות שנתי", "הקצאת שורות חודשית", "הקצאת שורות שנתית", "משך תפוקה", "הערות", "אפשר העלאת קובץ דיווח"
		};
		for (int i = 0; i < array.Length; i++)
		{
			iXLWorksheet.Cell(1, i + 1).Value = array[i];
		}
		int num = 2;
		foreach (Allocation item in list)
		{
			iXLWorksheet.Cell(num, 1).Value = item.Project?.Description;
			iXLWorksheet.Cell(num, 2).Value = JoinValues(item.AllocationPrograms.Select((AllocationProgram x) => x.Program?.Description));
			iXLWorksheet.Cell(num, 3).Value = JoinValues(item.AllocationDistricts.Select((AllocationDistrict x) => x.District?.Description));
			iXLWorksheet.Cell(num, 4).Value = JoinValues(item.AllocationSectors.Select((AllocationSector x) => x.Sector?.Description));
			iXLWorksheet.Cell(num, 5).Value = item.User?.IdNumber;
			iXLWorksheet.Cell(num, 6).Value = item.User?.EmployeeCode;
			iXLWorksheet.Cell(num, 7).Value = item.User?.FirstName;
			iXLWorksheet.Cell(num, 8).Value = item.User?.LastName;
			if (item.MonthlyEmploymentScope.HasValue)
			{
				iXLWorksheet.Cell(num, 9).Value = (double)item.MonthlyEmploymentScope.Value;
			}
			iXLWorksheet.Cell(num, 10).Value = item.DailyEmploymentScope?.ToString("0.##") ?? "ללא הגבלה";
			if (item.AnnualEmploymentScope.HasValue)
			{
				iXLWorksheet.Cell(num, 11).Value = (double)item.AnnualEmploymentScope.Value;
			}
			if (item.MonthlyRowAllocation.HasValue)
			{
				iXLWorksheet.Cell(num, 12).Value = item.MonthlyRowAllocation.Value;
			}
			if (item.AnnualRowAllocation.HasValue)
			{
				iXLWorksheet.Cell(num, 13).Value = item.AnnualRowAllocation.Value;
			}
			iXLWorksheet.Cell(num, 14).Value = item.OutputDuration;
			iXLWorksheet.Cell(num, 15).Value = item.Notes;
			iXLWorksheet.Cell(num, 16).Value = (item.AllowExcelUpload ? "כן" : "לא");
			num++;
		}
		iXLWorksheet.Row(1).Style.Font.Bold = true;
		iXLWorksheet.Columns().AdjustToContents();
		using MemoryStream memoryStream = new MemoryStream();
		xLWorkbook.SaveAs(memoryStream);
		return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"employee_allocations_{DateTime.Now:yyyy-MM-dd}.xlsx");
	}

	[HttpGet("ScopedLookups")]
	public async Task<IActionResult> ScopedLookups(int projectId, string? programIds = null)
	{
		if (projectId <= 0)
		{
			return BadRequest();
		}
		UserRoleEnum userRole = _currentUser.UserRole;
		if ((uint)(userRole - 1) > 2u)
		{
			return Forbid();
		}
		List<int> programs = ParseIds(programIds);
		if (programs.Count == 0)
		{
			programs = await (from pp in _db.ProjectPrograms
				where pp.ProjectId == projectId
				select pp.ProgramId).Distinct().ToListAsync();
		}
		return Json(new
		{
			subjects = await ScopedLookupAsync("ProjectProgramSubjects", "SubjectId", "Subjects", projectId, programs),
			domains = await ScopedLookupAsync("ProjectProgramDomains", "DomainId", "Domains", projectId, programs),
			frameworks = await ScopedLookupAsync("ProjectProgramFrameworks", "FrameworkId", "Frameworks", projectId, programs),
			educationalPrograms = await ScopedLookupAsync("ProjectProgramEducationalPrograms", "EducationalProgramId", "EducationalPrograms", projectId, programs),
			discussionCodes = await ScopedLookupAsync("ProjectProgramDiscussionCodes", "DiscussionCodeId", "DiscussionCodes", projectId, programs),
			gradeLevels = await ScopedLookupAsync("ProjectProgramGradeLevels", "GradeLevelId", "GradeLevels", projectId, programs),
			classes = await ScopedLookupAsync("ProjectProgramClasses", "ClassId", "SchoolClasses", projectId, programs),
			scopedKeys = new string[7] { "subjects", "domains", "frameworks", "educationalPrograms", "discussionCodes", "gradeLevels", "classes" }
		});
	}

	internal static async Task<IQueryable<Allocation>> BuildScopedAllocationQueryAsync(AppDbContext db, int currentUserId, UserRoleEnum role)
	{
		AppDbContext db2 = db;
		IQueryable<Allocation> query = BaseAllocationQuery(db2);
		switch (role)
		{
		case UserRoleEnum.SystemAdmin:
			return query;
		case UserRoleEnum.Employee:
			return query.Where((Allocation a) => a.UserId == currentUserId);
		default:
		{
			List<InspectorAssignment> list = await (from a in db2.InspectorAssignments.AsNoTracking()
				where a.InspectorUserId == currentUserId
				select a).ToListAsync();
			if (list.Count == 0)
			{
				return query.Where((Allocation a) => false);
			}
			HashSet<int> scopedIds = new HashSet<int>();
			foreach (InspectorAssignment item in list)
			{
				IQueryable<Allocation> source = db2.Allocations.Where((Allocation a) => a.IsActive);
				if (item.DistrictId.HasValue)
				{
					int districtId = item.DistrictId.Value;
					source = source.Where((Allocation a) => db2.Set<AllocationDistrict>().Any((AllocationDistrict x) => x.AllocationId == a.Id && x.DistrictId == districtId));
				}
				if (item.SectorId.HasValue)
				{
					int sectorId = item.SectorId.Value;
					source = source.Where((Allocation a) => db2.Set<AllocationSector>().Any((AllocationSector x) => x.AllocationId == a.Id && x.SectorId == sectorId));
				}
				if (item.ProgramId.HasValue)
				{
					int programId = item.ProgramId.Value;
					source = source.Where((Allocation a) => db2.Set<AllocationProgram>().Any((AllocationProgram x) => x.AllocationId == a.Id && x.ProgramId == programId));
				}
				foreach (int item2 in await source.Select((Allocation a) => a.Id).ToListAsync())
				{
					scopedIds.Add(item2);
				}
			}
			return (scopedIds.Count == 0) ? query.Where((Allocation a) => false) : query.Where((Allocation a) => scopedIds.Contains(a.Id));
		}
		}
	}

	internal static IQueryable<Allocation> ApplyAllocationFilters(IQueryable<Allocation> query, AllocationListFilterModel filter)
	{
		AllocationListFilterModel filter2 = filter;
		if (filter2.ProjectId.HasValue)
		{
			query = query.Where((Allocation a) => a.ProjectId == filter2.ProjectId.Value);
		}
		if (filter2.EmployeeId.HasValue)
		{
			query = query.Where((Allocation a) => a.UserId == filter2.EmployeeId.Value);
		}
		if (!string.IsNullOrWhiteSpace(filter2.Search))
		{
			string search = filter2.Search;
			query = query.Where((Allocation a) => a.User.EmployeeCode.Contains(search) || a.User.IdNumber.Contains(search) || string.Concat(a.User.FirstName + " ", a.User.LastName).Contains(search) || a.Project.Description.Contains(search));
		}
		List<int> programIds = filter2.ProgramIds;
		if (programIds != null && programIds.Count > 0)
		{
			query = query.Where((Allocation a) => a.AllocationPrograms.Any((AllocationProgram ap) => filter2.ProgramIds.Contains(ap.ProgramId)));
		}
		programIds = filter2.DistrictIds;
		if (programIds != null && programIds.Count > 0)
		{
			query = query.Where((Allocation a) => a.AllocationDistricts.Any((AllocationDistrict ad) => filter2.DistrictIds.Contains(ad.DistrictId)));
		}
		programIds = filter2.SectorIds;
		if (programIds != null && programIds.Count > 0)
		{
			query = query.Where((Allocation a) => a.AllocationSectors.Any((AllocationSector asc) => filter2.SectorIds.Contains(asc.SectorId)));
		}
		if (!string.IsNullOrWhiteSpace(filter2.IdNumber))
		{
			query = query.Where((Allocation a) => a.User.IdNumber.Contains(filter2.IdNumber));
		}
		if (!string.IsNullOrWhiteSpace(filter2.EmployeeCode))
		{
			query = query.Where((Allocation a) => a.User.EmployeeCode.Contains(filter2.EmployeeCode));
		}
		if (!string.IsNullOrWhiteSpace(filter2.FirstName))
		{
			query = query.Where((Allocation a) => a.User.FirstName.Contains(filter2.FirstName));
		}
		if (!string.IsNullOrWhiteSpace(filter2.LastName))
		{
			query = query.Where((Allocation a) => a.User.LastName.Contains(filter2.LastName));
		}
		if (filter2.MonthlyEmploymentScope.HasValue)
		{
			query = query.Where((Allocation a) => a.MonthlyEmploymentScope == (decimal?)filter2.MonthlyEmploymentScope.Value);
		}
		if (filter2.AnnualEmploymentScope.HasValue)
		{
			query = query.Where((Allocation a) => a.AnnualEmploymentScope == (decimal?)filter2.AnnualEmploymentScope.Value);
		}
		List<string> outputDurations = filter2.OutputDurations;
		if (outputDurations != null && outputDurations.Count > 0)
		{
			foreach (string outputDuration in filter2.OutputDurations)
			{
				string token = "," + outputDuration + ",";
				query = query.Where((Allocation a) => a.OutputDuration != null && string.Concat("," + a.OutputDuration, ",").Contains(token));
			}
		}
		if (!string.IsNullOrWhiteSpace(filter2.Notes))
		{
			query = query.Where((Allocation a) => a.Notes != null && a.Notes.Contains(filter2.Notes));
		}
		return query;
	}

	private async Task PopulateViewBagsAsync(IQueryable<Allocation> scopedQuery, AllocationListFilterModel filter, int page, int pageSize, int total)
	{
		AllocationListFilterModel filter2 = filter;
		IQueryable<int> scopedAllocationIds = scopedQuery.Select((Allocation a) => a.Id);
		IQueryable<int> projectIds = scopedQuery.Select((Allocation a) => a.ProjectId).Distinct();
		IQueryable<int> programIds = (from x in _db.Set<AllocationProgram>()
			where scopedAllocationIds.Contains(x.AllocationId)
			select x.ProgramId).Distinct();
		IQueryable<int> districtIds = (from x in _db.Set<AllocationDistrict>()
			where scopedAllocationIds.Contains(x.AllocationId)
			select x.DistrictId).Distinct();
		IQueryable<int> sectorIds = (from x in _db.Set<AllocationSector>()
			where scopedAllocationIds.Contains(x.AllocationId)
			select x.SectorId).Distinct();
		base.ViewBag.Filter = filter2;
		base.ViewBag.Search = filter2.Search;
		base.ViewBag.ProjectId = filter2.ProjectId;
		base.ViewBag.SortBy = filter2.SortBy;
		base.ViewBag.SortDesc = filter2.SortDesc;
		base.ViewBag.Page = page;
		base.ViewBag.PageSize = pageSize;
		base.ViewBag.TotalPages = (int)Math.Ceiling((double)total / (double)pageSize);
		if (_003C_003Eo__9._003C_003Ep__8 == null)
		{
			_003C_003Eo__9._003C_003Ep__8 = CallSite<Func<CallSite, object, AxiomaReporting.Core.Entities.User, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EmployeeContext", typeof(AllocationsController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, User, object> target = _003C_003Eo__9._003C_003Ep__8.Target;
		CallSite<Func<CallSite, object, User, object>> _003C_003Ep__ = _003C_003Eo__9._003C_003Ep__8;
		object viewBag = base.ViewBag;
		User arg = ((!filter2.EmployeeId.HasValue) ? null : (await _db.Users.AsNoTracking().FirstOrDefaultAsync((User u) => u.Id == filter2.EmployeeId.Value)));
		target(_003C_003Ep__, viewBag, arg);
		if (_003C_003Eo__9._003C_003Ep__9 == null)
		{
			_003C_003Eo__9._003C_003Ep__9 = CallSite<Func<CallSite, object, List<Project>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Projects", typeof(AllocationsController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Project>, object> target2 = _003C_003Eo__9._003C_003Ep__9.Target;
		CallSite<Func<CallSite, object, List<Project>, object>> _003C_003Ep__2 = _003C_003Eo__9._003C_003Ep__9;
		viewBag = base.ViewBag;
		target2(_003C_003Ep__2, viewBag, await (from p in _db.Projects
			where p.IsActive && projectIds.Contains(p.Id)
			orderby p.Description
			select p).ToListAsync());
		if (_003C_003Eo__9._003C_003Ep__10 == null)
		{
			_003C_003Eo__9._003C_003Ep__10 = CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AllPrograms", typeof(AllocationsController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object> target3 = _003C_003Eo__9._003C_003Ep__10.Target;
		CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__3 = _003C_003Eo__9._003C_003Ep__10;
		viewBag = base.ViewBag;
		target3(_003C_003Ep__3, viewBag, await (from p in _db.Programs
			where p.IsActive && programIds.Contains(p.Id)
			orderby p.Description
			select p).ToListAsync());
		if (_003C_003Eo__9._003C_003Ep__11 == null)
		{
			_003C_003Eo__9._003C_003Ep__11 = CallSite<Func<CallSite, object, List<District>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AllDistricts", typeof(AllocationsController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<District>, object> target4 = _003C_003Eo__9._003C_003Ep__11.Target;
		CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__4 = _003C_003Eo__9._003C_003Ep__11;
		viewBag = base.ViewBag;
		target4(_003C_003Ep__4, viewBag, await (from d in _db.Districts
			where d.IsActive && districtIds.Contains(d.Id)
			orderby d.Description
			select d).ToListAsync());
		if (_003C_003Eo__9._003C_003Ep__12 == null)
		{
			_003C_003Eo__9._003C_003Ep__12 = CallSite<Func<CallSite, object, List<Sector>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AllSectors", typeof(AllocationsController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Sector>, object> target5 = _003C_003Eo__9._003C_003Ep__12.Target;
		CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__5 = _003C_003Eo__9._003C_003Ep__12;
		viewBag = base.ViewBag;
		target5(_003C_003Ep__5, viewBag, await (from s in _db.Sectors
			where s.IsActive && sectorIds.Contains(s.Id)
			orderby s.Description
			select s).ToListAsync());
		base.ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;
		base.ViewBag.AllocationListController = "Allocations";
		base.ViewBag.AllocationListAction = "Index";
		base.ViewBag.AllocationExportAction = "ExportExcel";
		base.ViewBag.AllocationDetailController = "Allocations";
		base.ViewBag.AllocationDetailAction = "Details";
	}

	private static IQueryable<Allocation> BaseAllocationQuery(AppDbContext db)
	{
		return from a in db.Allocations.Include((Allocation a) => a.User).Include((Allocation a) => a.Project).Include((Allocation a) => a.AllocationDistricts)
				.ThenInclude((AllocationDistrict x) => x.District)
				.Include((Allocation a) => a.AllocationPrograms)
				.ThenInclude((AllocationProgram x) => x.Program)
				.Include((Allocation a) => a.AllocationSectors)
				.ThenInclude((AllocationSector x) => x.Sector)
				.Include((Allocation a) => a.AllocationLocalities)
				.ThenInclude((AllocationLocality x) => x.Locality)
				.Include((Allocation a) => a.AllocationFrameworks)
				.ThenInclude((AllocationFramework x) => x.Framework)
				.Include((Allocation a) => a.AllocationSubjects)
				.ThenInclude((AllocationSubject x) => x.Subject)
				.Include((Allocation a) => a.AllocationDomains)
				.ThenInclude((AllocationDomain x) => x.Domain)
				.Include((Allocation a) => a.AllocationEducationalPrograms)
				.ThenInclude((AllocationEducationalProgram x) => x.EducationalProgram)
				.Include((Allocation a) => a.AllocationClasses)
				.ThenInclude((AllocationClass x) => x.SchoolClass)
				.Include((Allocation a) => a.AllocationGradeLevels)
				.ThenInclude((AllocationGradeLevel x) => x.GradeLevel)
				.Include((Allocation a) => a.AllocationDiscussionCodes)
				.ThenInclude((AllocationDiscussionCode x) => x.DiscussionCode)
				.Include((Allocation a) => a.AllocationLocalityDistrictNationals)
				.ThenInclude((AllocationLocalityDistrictNational x) => x.LocalityDistrictNational)
				.AsSplitQuery()
			where a.IsActive
			select a;
	}

	private static IOrderedQueryable<Allocation> ApplyAllocationSort(IQueryable<Allocation> query, string? sortBy, bool sortDesc)
	{
		return sortBy?.ToLowerInvariant() switch
		{
			"project" => sortDesc ? query.OrderByDescending((Allocation a) => a.Project.Description) : query.OrderBy((Allocation a) => a.Project.Description), 
			"idnumber" => sortDesc ? query.OrderByDescending((Allocation a) => a.User.IdNumber) : query.OrderBy((Allocation a) => a.User.IdNumber), 
			"code" => sortDesc ? query.OrderByDescending((Allocation a) => a.User.EmployeeCode) : query.OrderBy((Allocation a) => a.User.EmployeeCode), 
			"firstname" => sortDesc ? query.OrderByDescending((Allocation a) => a.User.FirstName) : query.OrderBy((Allocation a) => a.User.FirstName), 
			"lastname" => sortDesc ? query.OrderByDescending((Allocation a) => a.User.LastName) : query.OrderBy((Allocation a) => a.User.LastName), 
			"monthlyscope" => sortDesc ? query.OrderByDescending((Allocation a) => a.MonthlyEmploymentScope) : query.OrderBy((Allocation a) => a.MonthlyEmploymentScope), 
			"dailyscope" => sortDesc ? query.OrderByDescending((Allocation a) => a.DailyEmploymentScope) : query.OrderBy((Allocation a) => a.DailyEmploymentScope), 
			"annualscope" => sortDesc ? query.OrderByDescending((Allocation a) => a.AnnualEmploymentScope) : query.OrderBy((Allocation a) => a.AnnualEmploymentScope), 
			"monthlyrows" => sortDesc ? query.OrderByDescending((Allocation a) => a.MonthlyRowAllocation) : query.OrderBy((Allocation a) => a.MonthlyRowAllocation), 
			"annualrows" => sortDesc ? query.OrderByDescending((Allocation a) => a.AnnualRowAllocation) : query.OrderBy((Allocation a) => a.AnnualRowAllocation), 
			"outputduration" => sortDesc ? query.OrderByDescending((Allocation a) => a.OutputDuration) : query.OrderBy((Allocation a) => a.OutputDuration), 
			"allowexcelupload" => sortDesc ? query.OrderByDescending((Allocation a) => a.AllowExcelUpload) : query.OrderBy((Allocation a) => a.AllowExcelUpload), 
			"notes" => sortDesc ? query.OrderByDescending((Allocation a) => a.Notes) : query.OrderBy((Allocation a) => a.Notes), 
			_ => from a in query
				orderby a.User.LastName, a.User.FirstName, a.Project.Description
				select a, 
		};
	}

	private static string JoinValues(IEnumerable<string?> values)
	{
		List<string> list = values.Where((string v) => !string.IsNullOrWhiteSpace(v)).Distinct().ToList();
		if (list.Count != 0)
		{
			return string.Join(", ", list);
		}
		return "";
	}

	private static List<int> ParseIds(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new List<int>();
		}
		return value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(delegate(string part)
		{
			int result;
			return int.TryParse(part, out result) ? result : 0;
		}).Where((int id) => id > 0).Distinct().ToList();
	}

	private static string NormalizeDigits(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return string.Empty;
		}
		return new string(value.Where(char.IsDigit).ToArray());
	}

	private async Task<List<object>> ScopedLookupAsync(string scopeTable, string scopeColumn, string lookupTable, int projectId, IReadOnlyCollection<int> programIds)
	{
		if (programIds.Count == 0)
		{
			return new List<object>();
		}
		List<object> values = new List<object>();
		DbConnection connection = _db.Database.GetDbConnection();
		bool shouldClose = connection.State == System.Data.ConnectionState.Closed;
		if (shouldClose)
		{
			await connection.OpenAsync();
		}
		try
		{
			using DbCommand command = connection.CreateCommand();
			if (lookupTable == "Frameworks")
			{
				command.CommandText = $"SELECT DISTINCT l.Id, COALESCE(NULLIF(loc.Description, '') + N', ', N'') + l.InstitutionSymbol + N', ' + COALESCE(NULLIF(i.Name, ''), l.Description) AS Description FROM dbo.{scopeTable} s JOIN dbo.Frameworks l ON l.Id = s.{scopeColumn} LEFT JOIN dbo.Institutions i ON TRY_CONVERT(int, l.InstitutionSymbol) = i.InstitutionSymbol LEFT JOIN dbo.Localities loc ON loc.Id = i.LocalityId WHERE s.ProjectId = @projectId AND s.ProgramId IN ({string.Join(",", programIds)}) AND l.IsActive = 1 AND TRY_CONVERT(int, l.InstitutionSymbol) IS NOT NULL ORDER BY Description";
			}
			else
			{
				command.CommandText = $"SELECT DISTINCT l.Id, l.Description FROM dbo.{scopeTable} s JOIN dbo.{lookupTable} l ON l.Id = s.{scopeColumn} WHERE s.ProjectId = @projectId AND s.ProgramId IN ({string.Join(",", programIds)}) AND l.IsActive = 1 ORDER BY l.Description";
			}
			DbParameter parameter = command.CreateParameter();
			parameter.ParameterName = "@projectId";
			parameter.Value = projectId;
			command.Parameters.Add(parameter);
			using DbDataReader reader = await command.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				values.Add(new
				{
					id = reader.GetInt32(0),
					description = reader.GetString(1)
				});
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
}

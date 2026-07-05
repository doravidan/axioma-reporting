using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Entities.Base;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Policy = "AdminOrPM")]
public class AdminController : Controller
{
	private sealed record ScopeDefinition(string Key, string TableName, string ColumnName, string LookupTable);

	private static readonly ScopeDefinition[] ProjectProgramScopeDefinitions = new ScopeDefinition[7]
	{
		new ScopeDefinition("subjects", "ProjectProgramSubjects", "SubjectId", "Subjects"),
		new ScopeDefinition("domains", "ProjectProgramDomains", "DomainId", "Domains"),
		new ScopeDefinition("frameworks", "ProjectProgramFrameworks", "FrameworkId", "Frameworks"),
		new ScopeDefinition("educationalPrograms", "ProjectProgramEducationalPrograms", "EducationalProgramId", "EducationalPrograms"),
		new ScopeDefinition("discussionCodes", "ProjectProgramDiscussionCodes", "DiscussionCodeId", "DiscussionCodes"),
		new ScopeDefinition("gradeLevels", "ProjectProgramGradeLevels", "GradeLevelId", "GradeLevels"),
		new ScopeDefinition("classes", "ProjectProgramClasses", "ClassId", "SchoolClasses")
	};

	[CompilerGenerated]
	private static class _003C_003Eo__16
	{
		public static CallSite<Func<CallSite, object, List<EducationalStage>, object>> _003C_003Ep__0;
	}

	[CompilerGenerated]
	private static class _003C_003Eo__24
	{
		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__0;
	}

	[CompilerGenerated]
	private static class _003C_003Eo__31
	{
		public static CallSite<Func<CallSite, object, List<InspectorAssignment>, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, List<User>, object>> _003C_003Ep__1;

		public static CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__2;

		public static CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__3;

		public static CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__4;
	}

	[CompilerGenerated]
	private static class _003C_003Eo__92
	{
		public static CallSite<Func<CallSite, object, List<Locality>, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__1;

		public static CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__2;

		public static CallSite<Func<CallSite, object, List<EducationType>, object>> _003C_003Ep__3;

		public static CallSite<Func<CallSite, object, List<EducationalStage>, object>> _003C_003Ep__4;
	}

	private readonly AppDbContext _db;

	private readonly IPasswordService _passwordService;

	private readonly IBatchReportImportService _batchImportService;

	private readonly IPdfReportService _pdfReportService;

	private readonly IEmailService _emailService;

	private readonly IBrandingService _brandingService;

	private readonly IAuditLogService _auditLog;

	private readonly IWebHostEnvironment _hostEnvironment;

	private readonly IAntiforgery _antiforgery;

	public AdminController(AppDbContext db, IPasswordService passwordService, IBatchReportImportService batchImportService, IPdfReportService pdfReportService, IEmailService emailService, IBrandingService brandingService, IAuditLogService auditLog, IWebHostEnvironment hostEnvironment, IAntiforgery antiforgery)
	{
		_db = db;
		_passwordService = passwordService;
		_batchImportService = batchImportService;
		_pdfReportService = pdfReportService;
		_emailService = emailService;
		_brandingService = brandingService;
		_auditLog = auditLog;
		_hostEnvironment = hostEnvironment;
		_antiforgery = antiforgery;
	}

	public async Task<IActionResult> ReportingMonths()
	{
		return View(await (from m in _db.ReportingMonths
			orderby m.Year descending, m.Month descending
			select m).ToListAsync());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CreateReportingMonth(int month, int year, string description, DateTime lastReportingDate, bool allowFutureReporting = false)
	{
		ReportingMonth entity = new ReportingMonth
		{
			Month = month,
			Year = year,
			Description = description,
			LastReportingDate = lastReportingDate,
			AllowFutureReporting = allowFutureReporting,
			IsActive = false,
			CreatedAt = DateTime.UtcNow
		};
		_db.ReportingMonths.Add(entity);
		await _db.SaveChangesAsync();
		base.TempData["Success"] = "חודש דיווח נוצר";
		return RedirectToAction("ReportingMonths");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ActivateReportingMonth(int id)
	{
		foreach (ReportingMonth item in await _db.ReportingMonths.ToListAsync())
		{
			item.IsActive = false;
		}
		ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(id);
		if (reportingMonth != null)
		{
			reportingMonth.IsActive = true;
			reportingMonth.UpdatedAt = DateTime.UtcNow;
			await _db.SaveChangesAsync();
		}
		base.TempData["Success"] = "חודש הדיווח הופעל";
		return RedirectToAction("ReportingMonths");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeactivateReportingMonth(int id)
	{
		ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(id);
		if (reportingMonth != null)
		{
			reportingMonth.IsActive = false;
			reportingMonth.UpdatedAt = DateTime.UtcNow;
			await _db.SaveChangesAsync();
		}
		base.TempData["Success"] = "חודש הדיווח הושבת";
		return RedirectToAction("ReportingMonths");
	}

	[HttpGet]
	[Authorize(Policy = "AdminPMOrCoordinator")]
	public async Task<IActionResult> EditReportingMonth(int id)
	{
		ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(id);
		if (reportingMonth == null)
		{
			base.TempData["Error"] = "חודש הדיווח לא נמצא";
			return RedirectToAction("ReportingMonths");
		}
		ReportingMonthEditViewModel model = new ReportingMonthEditViewModel
		{
			Id = reportingMonth.Id,
			Description = reportingMonth.Description,
			Month = reportingMonth.Month,
			Year = reportingMonth.Year,
			LastReportingDate = reportingMonth.LastReportingDate,
			AllowFutureReporting = reportingMonth.AllowFutureReporting,
			IsActive = reportingMonth.IsActive,
			LockNonAdminFields = (reportingMonth.IsActive && !IsAdminOrProjectManager())
		};
		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminPMOrCoordinator")]
	public async Task<IActionResult> EditReportingMonth(ReportingMonthEditViewModel input)
	{
		ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(input.Id);
		if (reportingMonth == null)
		{
			base.TempData["Error"] = "חודש הדיווח לא נמצא";
			return RedirectToAction("ReportingMonths");
		}
		bool flag = reportingMonth.IsActive && !IsAdminOrProjectManager();
		reportingMonth.Description = (string.IsNullOrWhiteSpace(input.Description) ? reportingMonth.Description : input.Description.Trim());
		reportingMonth.Month = input.Month;
		reportingMonth.Year = input.Year;
		if (!flag)
		{
			reportingMonth.LastReportingDate = input.LastReportingDate;
			reportingMonth.AllowFutureReporting = input.AllowFutureReporting;
		}
		reportingMonth.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		base.TempData["Success"] = "חודש הדיווח עודכן";
		return RedirectToAction("ReportingMonths");
	}

	private bool IsAdminOrProjectManager()
	{
		string text = base.User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
		if (!(text == "1"))
		{
			return text == "2";
		}
		return true;
	}

	private async Task<Dictionary<int, string>> LoadFrameworkLocalityMapAsync(IEnumerable<Framework> frameworks)
	{
		var frameworkSymbols = frameworks.Select(delegate(Framework f)
		{
			int symbol;
			return int.TryParse(f.InstitutionSymbol, out symbol) ? new { f.Id, Symbol = (int?)symbol } : new { f.Id, Symbol = (int?)null };
		}).Where(x => x.Symbol.HasValue).ToList();
		List<int> symbols = frameworkSymbols.Select(x => x.Symbol!.Value).Distinct().ToList();
		if (symbols.Count == 0)
		{
			return new Dictionary<int, string>();
		}
		var institutionLocalities = await _db.Institutions.Include((Institution i) => i.Locality).Where((Institution i) => symbols.Contains(i.InstitutionSymbol)).Select((Institution i) => new
		{
			i.InstitutionSymbol,
			LocalityName = i.Locality != null ? i.Locality.Description : string.Empty
		}).ToListAsync();
		return frameworkSymbols.ToDictionary(x => x.Id, x => institutionLocalities.FirstOrDefault(i => i.InstitutionSymbol == x.Symbol!.Value)?.LocalityName ?? string.Empty);
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> Frameworks(string? frameworkName, string? institutionSymbol, int? educationalStageId, string? localityName, bool? isActive)
	{
		string frameworkName2 = frameworkName;
		string institutionSymbol2 = institutionSymbol;
		IQueryable<Framework> query = _db.Frameworks.Include((Framework f) => f.EducationalStage).AsQueryable();
		if (!string.IsNullOrWhiteSpace(frameworkName2))
		{
			query = query.Where((Framework f) => f.Description.Contains(frameworkName2));
		}
		if (!string.IsNullOrWhiteSpace(institutionSymbol2))
		{
			query = query.Where((Framework f) => f.InstitutionSymbol.Contains(institutionSymbol2));
		}
		if (educationalStageId.HasValue)
		{
			query = query.Where((Framework f) => f.EducationalStageId == educationalStageId);
		}
		if (!string.IsNullOrWhiteSpace(localityName))
		{
			IQueryable<string> localitySymbols = from i in _db.Institutions
				where i.Locality != null && i.Locality.Description.Contains(localityName)
				select i.InstitutionSymbol.ToString();
			query = query.Where((Framework f) => localitySymbols.Contains(f.InstitutionSymbol));
		}
		if (isActive.HasValue)
		{
			query = query.Where((Framework f) => f.IsActive == isActive.Value);
		}
		List<Framework> items = await (from f in query
			orderby f.Description
			select f).ToListAsync();
		base.ViewBag.FrameworkLocalities = await LoadFrameworkLocalityMapAsync(items);
		base.ViewBag.FrameworkName = frameworkName2;
		base.ViewBag.InstitutionSymbol = institutionSymbol2;
		base.ViewBag.EducationalStageId = educationalStageId;
		base.ViewBag.LocalityName = localityName;
		base.ViewBag.IsActive = isActive;
		if (_003C_003Eo__16._003C_003Ep__0 == null)
		{
			_003C_003Eo__16._003C_003Ep__0 = CallSite<Func<CallSite, object, List<EducationalStage>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EducationalStages", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<EducationalStage>, object> target = _003C_003Eo__16._003C_003Ep__0.Target;
		CallSite<Func<CallSite, object, List<EducationalStage>, object>> _003C_003Ep__ = _003C_003Eo__16._003C_003Ep__0;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, await _db.EducationalStages.Where((EducationalStage s) => s.IsActive).ToListAsync());
		return View(items);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> BulkSetFrameworksActive(bool isActive, string? frameworkName, string? institutionSymbol, int? educationalStageId, string? localityName)
	{
		string frameworkName2 = frameworkName;
		string institutionSymbol2 = institutionSymbol;
		IQueryable<Framework> query = _db.Frameworks.AsQueryable();
		if (!string.IsNullOrWhiteSpace(frameworkName2))
		{
			query = query.Where((Framework f) => f.Description.Contains(frameworkName2));
		}
		if (!string.IsNullOrWhiteSpace(institutionSymbol2))
		{
			query = query.Where((Framework f) => f.InstitutionSymbol.Contains(institutionSymbol2));
		}
		if (educationalStageId.HasValue)
		{
			query = query.Where((Framework f) => f.EducationalStageId == educationalStageId);
		}
		if (!string.IsNullOrWhiteSpace(localityName))
		{
			IQueryable<string> localitySymbols = from i in _db.Institutions
				where i.Locality != null && i.Locality.Description.Contains(localityName)
				select i.InstitutionSymbol.ToString();
			query = query.Where((Framework f) => localitySymbols.Contains(f.InstitutionSymbol));
		}
		List<Framework> frameworks = await query.ToListAsync();
		foreach (Framework framework in frameworks)
		{
			framework.IsActive = isActive;
			framework.UpdatedAt = DateTime.UtcNow;
		}
		await _db.SaveChangesAsync();
		base.TempData["Success"] = $"{frameworks.Count} מסגרות עודכנו ל-{(isActive ? "פעיל" : "לא פעיל")}";
		return RedirectToAction("Frameworks", new
		{
			frameworkName = frameworkName2,
			institutionSymbol = institutionSymbol2,
			educationalStageId,
			localityName,
			isActive = (bool?)null
		});
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ExportFrameworks()
	{
		List<Framework> frameworks = await _db.Frameworks.Include((Framework f) => f.EducationalStage).OrderBy((Framework f) => f.Description).ToListAsync();
		Dictionary<int, string> localityMap = await LoadFrameworkLocalityMapAsync(frameworks);
		using XLWorkbook workbook = new XLWorkbook();
		IXLWorksheet ws = workbook.Worksheets.Add("מסגרות");
		ws.RightToLeft = true;
		string[] headers = new string[5] { "יישוב", "שם מסגרת", "סמל מוסד", "שלב חינוך", "פעיל" };
		for (int i = 0; i < headers.Length; i++)
		{
			ws.Cell(1, i + 1).Value = headers[i];
		}
		int row = 2;
		foreach (Framework framework in frameworks)
		{
			ws.Cell(row, 1).Value = localityMap.TryGetValue(framework.Id, out string localityNameValue) ? localityNameValue : string.Empty;
			ws.Cell(row, 2).Value = framework.Description;
			ws.Cell(row, 3).Value = framework.InstitutionSymbol;
			ws.Cell(row, 4).Value = framework.EducationalStage?.Description;
			ws.Cell(row, 5).Value = (framework.IsActive ? "כן" : "לא");
			row++;
		}
		ws.Row(1).Style.Font.Bold = true;
		ws.Columns().AdjustToContents();
		using MemoryStream stream = new MemoryStream();
		workbook.SaveAs(stream);
		return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"frameworks_{DateTime.Now:yyyy-MM-dd}.xlsx");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> CreateFramework(string description, string institutionSymbol, int? educationalStageId)
	{
		string institutionSymbol2 = institutionSymbol;
		if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(institutionSymbol2))
		{
			base.TempData["Error"] = "יש להזין תיאור וסמל מוסד";
			return RedirectToAction("Frameworks");
		}
		if (await _db.Frameworks.AnyAsync((Framework f) => f.InstitutionSymbol == institutionSymbol2 && f.EducationalStageId == educationalStageId))
		{
			base.TempData["Error"] = "סמל מוסד זה כבר קיים עבור שלב חינוך זה";
			return RedirectToAction("Frameworks");
		}
		_db.Frameworks.Add(new Framework
		{
			Description = description.Trim(),
			InstitutionSymbol = institutionSymbol2.Trim(),
			EducationalStageId = educationalStageId,
			IsActive = true,
			CreatedAt = DateTime.UtcNow
		});
		try
		{
			await _db.SaveChangesAsync();
		}
		catch (DbUpdateException)
		{
			base.TempData["Error"] = "סמל מוסד זה כבר קיים עבור שלב חינוך זה";
			return RedirectToAction("Frameworks");
		}
		base.TempData["Success"] = "מסגרת נוצרה";
		return RedirectToAction("Frameworks");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> EditFramework(int id, string description, string institutionSymbol, int? educationalStageId, bool isActive)
	{
		string institutionSymbol2 = institutionSymbol;
		Framework framework = await _db.Frameworks.FindAsync(id);
		if (framework == null)
		{
			base.TempData["Error"] = "מסגרת לא נמצאה";
			return RedirectToAction("Frameworks");
		}
		if (await _db.Frameworks.AnyAsync((Framework f) => f.Id != id && f.InstitutionSymbol == institutionSymbol2 && f.EducationalStageId == educationalStageId))
		{
			base.TempData["Error"] = "סמל מוסד זה כבר קיים עבור שלב חינוך זה";
			return RedirectToAction("Frameworks");
		}
		framework.Description = description;
		framework.InstitutionSymbol = institutionSymbol2;
		framework.EducationalStageId = educationalStageId;
		framework.IsActive = isActive;
		framework.UpdatedAt = DateTime.UtcNow;
		try
		{
			await _db.SaveChangesAsync();
		}
		catch (DbUpdateException)
		{
			base.TempData["Error"] = "סמל מוסד זה כבר קיים עבור שלב חינוך זה";
			return RedirectToAction("Frameworks");
		}
		base.TempData["Success"] = "מסגרת עודכנה";
		return RedirectToAction("Frameworks");
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> Institutions()
	{
		List<Institution> items = await (from i in _db.Institutions.Include((Institution i) => i.Locality).Include((Institution i) => i.District).Include((Institution i) => i.Sector)
				.Include((Institution i) => i.Type)
				.Include((Institution i) => i.EducationalStage)
			orderby i.Name
			select i).ToListAsync();
		await LoadInstitutionDropdowns();
		return View(items);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> CreateInstitution(int institutionSymbol, string name, int? localityId, int? districtId, int? sectorId, int? typeId, int? educationalStageId)
	{
		if (await _db.Institutions.AnyAsync((Institution i) => i.InstitutionSymbol == institutionSymbol && i.EducationalStageId == educationalStageId))
		{
			base.TempData["Error"] = "סמל מוסד כבר קיים עבור שלב חינוך זה";
			return RedirectToAction("Institutions");
		}
		_db.Institutions.Add(new Institution
		{
			InstitutionSymbol = institutionSymbol,
			Name = name,
			LocalityId = localityId,
			DistrictId = districtId,
			SectorId = sectorId,
			TypeId = typeId,
			EducationalStageId = educationalStageId,
			IsActive = true,
			CreatedAt = DateTime.UtcNow
		});
		await _db.SaveChangesAsync();
		base.TempData["Success"] = "מוסד נוצר";
		return RedirectToAction("Institutions");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> EditInstitution(int id, int institutionSymbol, string name, int? localityId, int? districtId, int? sectorId, int? typeId, int? educationalStageId, bool isActive)
	{
		Institution institution = await _db.Institutions.FindAsync(id);
		if (institution == null)
		{
			base.TempData["Error"] = "מוסד לא נמצא";
			return RedirectToAction("Institutions");
		}
		if (await _db.Institutions.AnyAsync((Institution i) => i.Id != id && i.InstitutionSymbol == institutionSymbol && i.EducationalStageId == educationalStageId))
		{
			base.TempData["Error"] = "סמל מוסד כבר קיים עבור שלב חינוך זה";
			return RedirectToAction("Institutions");
		}
		institution.InstitutionSymbol = institutionSymbol;
		institution.Name = name;
		institution.LocalityId = localityId;
		institution.DistrictId = districtId;
		institution.SectorId = sectorId;
		institution.TypeId = typeId;
		institution.EducationalStageId = educationalStageId;
		institution.IsActive = isActive;
		institution.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		base.TempData["Success"] = "מוסד עודכן";
		return RedirectToAction("Institutions");
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> SystemConstants()
	{
		string[] obsoleteKeys = new string[2] { "NotesSimilarityThreshold", "MaxDailyHours" };
		return View(await _db.SystemConstants.Where((SystemConstant c) => !obsoleteKeys.Contains(c.Key)).OrderBy((SystemConstant c) => c.Key).ToListAsync());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> UpdateSystemConstant(int id, string value)
	{
		SystemConstant constant = await _db.SystemConstants.FindAsync(id);
		if (constant != null)
		{
			var before = new { constant.Key, constant.Value };
			constant.Value = value;
			constant.UpdatedAt = DateTime.UtcNow;
			await _db.SaveChangesAsync();
			await _auditLog.LogAsync("SystemConstant.Update", "SystemConstant", id.ToString(), before, new { constant.Key, constant.Value });
		}
		base.TempData["Success"] = "הערך עודכן";
		return RedirectToAction("SystemConstants");
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> Branding()
	{
		if (_003C_003Eo__24._003C_003Ep__0 == null)
		{
			_003C_003Eo__24._003C_003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentLogoPath", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, string, object> target = _003C_003Eo__24._003C_003Ep__0.Target;
		CallSite<Func<CallSite, object, string, object>> _003C_003Ep__ = _003C_003Eo__24._003C_003Ep__0;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, await _brandingService.GetLogoPathAsync());
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	[RequestSizeLimit(2101248L)]
	public async Task<IActionResult> UploadLogo(IFormFile logoFile)
	{
		string[] source = new string[4] { ".png", ".svg", ".jpg", ".jpeg" };
		if (logoFile == null || logoFile.Length == 0L)
		{
			base.TempData["Error"] = "לא נבחר קובץ לוגו";
			return RedirectToAction("Branding");
		}
		if (logoFile.Length > 2097152)
		{
			base.TempData["Error"] = "גודל הקובץ חורג מהמותר (מקסימום 2 מ\"ב)";
			return RedirectToAction("Branding");
		}
		string text = Path.GetExtension(logoFile.FileName).ToLowerInvariant();
		if (!source.Contains(text))
		{
			base.TempData["Error"] = "סוג קובץ לא נתמך. מותר: PNG, SVG, JPG";
			return RedirectToAction("Branding");
		}
		string text2 = _hostEnvironment.WebRootPath;
		if (string.IsNullOrEmpty(text2))
		{
			text2 = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot");
		}
		string text3 = Path.Combine(text2, "uploads", "branding");
		Directory.CreateDirectory(text3);
		string text4 = ((text == ".jpeg") ? ".jpg" : text);
		string fileName = "site-logo" + text4;
		string text5 = Path.Combine(text3, fileName);
		foreach (string item in source.Select((string a) => (!(a == ".jpeg")) ? a : ".jpg").Distinct())
		{
			string text6 = Path.Combine(text3, "site-logo" + item);
			if (!string.Equals(text6, text5, StringComparison.OrdinalIgnoreCase) && System.IO.File.Exists(text6))
			{
				try
				{
					System.IO.File.Delete(text6);
				}
				catch
				{
				}
			}
		}
		FileStream fs = System.IO.File.Create(text5);
		try
		{
			await logoFile.CopyToAsync(fs);
		}
		finally
		{
			if (fs != null)
			{
				await fs.DisposeAsync();
			}
		}
		string publicPath = "/uploads/branding/" + fileName;
		int result;
		int? updatedByUserId = (int.TryParse(base.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value, out result) ? new int?(result) : null);
		await _brandingService.SetLogoPathAsync(publicPath, updatedByUserId);
		base.TempData["Success"] = "הלוגו עודכן בהצלחה";
		return RedirectToAction("Branding");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ResetLogo()
	{
		int result;
		int? updatedByUserId = (int.TryParse(base.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value, out result) ? new int?(result) : null);
		await _brandingService.SetLogoPathAsync("/images/logo.png", updatedByUserId);
		base.TempData["Success"] = "הלוגו הוחזר לברירת המחדל";
		return RedirectToAction("Branding");
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> EmailTemplates()
	{
		return View(await (from t in _db.EmailTemplates
			where t.IsActive
			orderby t.TypeDescription
			select t).ToListAsync());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> UpdateEmailTemplate(int id, string subject, string body)
	{
		EmailTemplate template = await _db.EmailTemplates.FindAsync(id);
		if (template != null)
		{
			var before = new { template.Subject, template.Body };
			template.Subject = subject;
			template.Body = body;
			template.UpdatedAt = DateTime.UtcNow;
			await _db.SaveChangesAsync();
			await _auditLog.LogAsync("EmailTemplate.Update", "EmailTemplate", id.ToString(), before, new { template.Subject, template.Body });
		}
		base.TempData["Success"] = "תבנית עודכנה";
		return RedirectToAction("EmailTemplates");
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> EmailServerSettings()
	{
		return View(await _db.EmailServerSettings.FirstOrDefaultAsync());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> SaveEmailServerSettings(string smtpServer, int port, string username, string? password, string fromAddress, string? fromName, bool useSsl)
	{
		EmailServerSetting existing = await _db.EmailServerSettings.FirstOrDefaultAsync();
		object before = null;
		if (existing == null)
		{
			_db.EmailServerSettings.Add(new EmailServerSetting
			{
				SmtpServer = smtpServer,
				Port = port,
				Username = username,
				Password = (password ?? string.Empty),
				FromAddress = fromAddress,
				FromName = fromName,
				UseSsl = useSsl,
				CreatedAt = DateTime.UtcNow
			});
		}
		else
		{
			before = new { existing.SmtpServer, existing.Port, existing.Username, existing.FromAddress, existing.FromName, existing.UseSsl };
			existing.SmtpServer = smtpServer;
			existing.Port = port;
			existing.Username = username;
			if (!string.IsNullOrEmpty(password))
			{
				existing.Password = password;
			}
			existing.FromAddress = fromAddress;
			existing.FromName = fromName;
			existing.UseSsl = useSsl;
			existing.UpdatedAt = DateTime.UtcNow;
		}
		await _db.SaveChangesAsync();
		await _auditLog.LogAsync("EmailServerSetting.Update", "EmailServerSetting", existing?.Id.ToString(), before, new { smtpServer, port, username, fromAddress, fromName, useSsl });
		base.TempData["Success"] = "הגדרות SMTP נשמרו";
		return RedirectToAction("EmailServerSettings");
	}

	[Authorize(Policy = "AdminOrPM")]
	public async Task<IActionResult> InspectorAssignments()
	{
		if (_003C_003Eo__31._003C_003Ep__0 == null)
		{
			_003C_003Eo__31._003C_003Ep__0 = CallSite<Func<CallSite, object, List<InspectorAssignment>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Assignments", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<InspectorAssignment>, object> target = _003C_003Eo__31._003C_003Ep__0.Target;
		CallSite<Func<CallSite, object, List<InspectorAssignment>, object>> _003C_003Ep__ = _003C_003Eo__31._003C_003Ep__0;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, await (from a in _db.InspectorAssignments.Include((InspectorAssignment a) => a.Inspector).Include((InspectorAssignment a) => a.Program).Include((InspectorAssignment a) => a.District)
				.Include((InspectorAssignment a) => a.Sector)
			orderby a.Inspector.LastName
			select a).ToListAsync());
		if (_003C_003Eo__31._003C_003Ep__1 == null)
		{
			_003C_003Eo__31._003C_003Ep__1 = CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.User>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Inspectors", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<User>, object> target2 = _003C_003Eo__31._003C_003Ep__1.Target;
		CallSite<Func<CallSite, object, List<User>, object>> _003C_003Ep__2 = _003C_003Eo__31._003C_003Ep__1;
		viewBag = base.ViewBag;
		target2(_003C_003Ep__2, viewBag, await (from u in _db.Users
			where u.UserRoleId == 4 || u.UserRoleId == 5
			orderby u.LastName, u.FirstName
			select u).ToListAsync());
		if (_003C_003Eo__31._003C_003Ep__2 == null)
		{
			_003C_003Eo__31._003C_003Ep__2 = CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Programs", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object> target3 = _003C_003Eo__31._003C_003Ep__2.Target;
		CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__3 = _003C_003Eo__31._003C_003Ep__2;
		viewBag = base.ViewBag;
		target3(_003C_003Ep__3, viewBag, await (from p in _db.Programs
			where p.IsActive
			orderby p.Description
			select p).ToListAsync());
		if (_003C_003Eo__31._003C_003Ep__3 == null)
		{
			_003C_003Eo__31._003C_003Ep__3 = CallSite<Func<CallSite, object, List<District>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Districts", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<District>, object> target4 = _003C_003Eo__31._003C_003Ep__3.Target;
		CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__4 = _003C_003Eo__31._003C_003Ep__3;
		viewBag = base.ViewBag;
		target4(_003C_003Ep__4, viewBag, await (from d in _db.Districts
			where d.IsActive
			orderby d.Description
			select d).ToListAsync());
		if (_003C_003Eo__31._003C_003Ep__4 == null)
		{
			_003C_003Eo__31._003C_003Ep__4 = CallSite<Func<CallSite, object, List<Sector>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sectors", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Sector>, object> target5 = _003C_003Eo__31._003C_003Ep__4.Target;
		CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__5 = _003C_003Eo__31._003C_003Ep__4;
		viewBag = base.ViewBag;
		target5(_003C_003Ep__5, viewBag, await (from s in _db.Sectors
			where s.IsActive
			orderby s.Description
			select s).ToListAsync());
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOrPM")]
	public async Task<IActionResult> CreateInspectorAssignment(int inspectorUserId, int? programId, int? districtId, int? sectorId)
	{
		InspectorAssignment assignment = new InspectorAssignment
		{
			InspectorUserId = inspectorUserId,
			ProgramId = programId,
			DistrictId = districtId,
			SectorId = sectorId
		};
		_db.InspectorAssignments.Add(assignment);
		await _db.SaveChangesAsync();
		await _auditLog.LogAsync("InspectorAssignment.Create", "InspectorAssignment", assignment.Id.ToString(), null, new { assignment.InspectorUserId, assignment.ProgramId, assignment.DistrictId, assignment.SectorId });
		base.TempData["Success"] = "שיוך המפקח נוסף";
		return RedirectToAction("InspectorAssignments");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOrPM")]
	public async Task<IActionResult> DeleteInspectorAssignment(int id)
	{
		InspectorAssignment inspectorAssignment = await _db.InspectorAssignments.FindAsync(id);
		if (inspectorAssignment != null)
		{
			var before = new { inspectorAssignment.InspectorUserId, inspectorAssignment.ProgramId, inspectorAssignment.DistrictId, inspectorAssignment.SectorId };
			_db.InspectorAssignments.Remove(inspectorAssignment);
			await _db.SaveChangesAsync();
			await _auditLog.LogAsync("InspectorAssignment.Delete", "InspectorAssignment", id.ToString(), before);
		}
		base.TempData["Success"] = "שיוך המפקח נמחק";
		return RedirectToAction("InspectorAssignments");
	}

	[HttpGet]
	[Route("Admin/ProjectPrograms")]
	public async Task<IActionResult> ProjectPrograms()
	{
		List<Project> projects = await (from p in _db.Projects
			where p.IsActive
			orderby p.Description
			select p).ToListAsync();
		List<AxiomaReporting.Core.Entities.Program> programs = await (from p in _db.Programs
			where p.IsActive
			orderby p.Description
			select p).ToListAsync();
		Dictionary<int, HashSet<int>> dictionary = (from pp in await _db.ProjectPrograms.AsNoTracking().ToListAsync()
			group pp by pp.ProjectId).ToDictionary((IGrouping<int, ProjectProgram> g) => g.Key, (IGrouping<int, ProjectProgram> g) => g.Select((ProjectProgram x) => x.ProgramId).ToHashSet());
		base.ViewBag.Projects = projects;
		base.ViewBag.Programs = programs;
		base.ViewBag.Mapping = dictionary;
		base.ViewBag.Subjects = await _db.Subjects.Where((Subject x) => x.IsActive).OrderBy((Subject x) => x.Description).ToListAsync();
		base.ViewBag.Domains = await _db.Domains.Where((Domain x) => x.IsActive).OrderBy((Domain x) => x.Description).ToListAsync();
		base.ViewBag.Frameworks = await _db.Frameworks.Where((Framework x) => x.IsActive).OrderBy((Framework x) => x.Description).ToListAsync();
		base.ViewBag.EducationalPrograms = await _db.EducationalPrograms.Where((EducationalProgram x) => x.IsActive).OrderBy((EducationalProgram x) => x.Description).ToListAsync();
		base.ViewBag.DiscussionCodes = await _db.DiscussionCodes.Where((DiscussionCode x) => x.IsActive).OrderBy((DiscussionCode x) => x.Description).ToListAsync();
		base.ViewBag.GradeLevels = await _db.GradeLevels.Where((GradeLevel x) => x.IsActive).OrderBy((GradeLevel x) => x.Description).ToListAsync();
		base.ViewBag.Classes = await _db.Classes.Where((SchoolClass x) => x.IsActive).OrderBy((SchoolClass x) => x.Description).ToListAsync();
		base.ViewBag.ScopeMapping = await LoadProjectProgramScopeMappingAsync();
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Route("Admin/ProjectPrograms/Save")]
	public async Task<IActionResult> SaveProjectPrograms(int projectId, int[]? programIds)
	{
		Project project = await _db.Projects.FirstOrDefaultAsync((Project p) => p.Id == projectId && p.IsActive);
		if (project == null)
		{
			base.TempData["Error"] = "פרויקט לא נמצא";
			return RedirectToAction("ProjectPrograms");
		}
		HashSet<int> desired = (programIds ?? Array.Empty<int>()).Where((int x) => x > 0).Distinct().ToHashSet();
		if (desired.Count > 0)
		{
			desired = (await (from p in _db.Programs
				where p.IsActive && desired.Contains(p.Id)
				select p.Id).ToListAsync()).ToHashSet();
		}
		List<ProjectProgram> source = await _db.ProjectPrograms.Where((ProjectProgram pp) => pp.ProjectId == projectId).ToListAsync();
		List<ProjectProgram> list = source.Where((ProjectProgram e) => !desired.Contains(e.ProgramId)).ToList();
		if (list.Count > 0)
		{
			_db.ProjectPrograms.RemoveRange(list);
		}
		HashSet<int> existingIds = source.Select((ProjectProgram e) => e.ProgramId).ToHashSet();
		List<int> addedProgramIds = desired.Where((int id) => !existingIds.Contains(id)).ToList();
		foreach (int item in desired.Where((int id) => !existingIds.Contains(id)))
		{
			_db.ProjectPrograms.Add(new ProjectProgram
			{
				ProjectId = projectId,
				ProgramId = item
			});
		}
		await _db.SaveChangesAsync();
		foreach (ProjectProgram removed in list)
		{
			await DeleteAllProjectProgramScopeRowsAsync(removed.ProjectId, removed.ProgramId);
		}
		foreach (int addedProgramId in addedProgramIds)
		{
			await BackfillProjectProgramScopeRowsAsync(projectId, addedProgramId);
		}
		await _db.SaveChangesAsync();
		base.TempData["Success"] = $"נשמרו {desired.Count} תוכניות עבור פרויקט '{project.Description}'";
		return RedirectToAction("ProjectPrograms");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Route("Admin/ProjectPrograms/SaveScope")]
	public async Task<IActionResult> SaveProjectProgramScope(int projectId, int programId, int[]? subjectIds, int[]? domainIds, int[]? frameworkIds, int[]? educationalProgramIds, int[]? discussionCodeIds, int[]? gradeLevelIds, int[]? classIds)
	{
		bool exists = await _db.ProjectPrograms.AnyAsync((ProjectProgram pp) => pp.ProjectId == projectId && pp.ProgramId == programId);
		if (!exists)
		{
			base.TempData["Error"] = "Project-program mapping was not found.";
			return RedirectToAction("ProjectPrograms");
		}
		await ReplaceProjectProgramScopeAsync(projectId, programId, ProjectProgramScopeDefinitions[0], subjectIds);
		await ReplaceProjectProgramScopeAsync(projectId, programId, ProjectProgramScopeDefinitions[1], domainIds);
		await ReplaceProjectProgramScopeAsync(projectId, programId, ProjectProgramScopeDefinitions[2], frameworkIds);
		await ReplaceProjectProgramScopeAsync(projectId, programId, ProjectProgramScopeDefinitions[3], educationalProgramIds);
		await ReplaceProjectProgramScopeAsync(projectId, programId, ProjectProgramScopeDefinitions[4], discussionCodeIds);
		await ReplaceProjectProgramScopeAsync(projectId, programId, ProjectProgramScopeDefinitions[5], gradeLevelIds);
		await ReplaceProjectProgramScopeAsync(projectId, programId, ProjectProgramScopeDefinitions[6], classIds);
		await _auditLog.LogAsync("ProjectProgramScope.Update", "ProjectProgram", $"{projectId}:{programId}", null, new
		{
			projectId,
			programId,
			subjectIds,
			domainIds,
			frameworkIds,
			educationalProgramIds,
			discussionCodeIds,
			gradeLevelIds,
			classIds
		});
		base.TempData["Success"] = "Scope settings were saved.";
		return RedirectToAction("ProjectPrograms");
	}

	[Authorize(Policy = "AdminOnly")]
	public IActionResult DataMigration()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ImportLookups(IFormFile? file)
	{
		if (!ValidateXlsxUpload(file))
		{
			return RedirectToAction("DataMigration");
		}
		List<string> results = new List<string>();
		DateTime now = DateTime.UtcNow;
		using Stream stream = file.OpenReadStream();
		using XLWorkbook wb = new XLWorkbook(stream);
		Dictionary<string, Func<IXLWorksheet, Task<int>>> sheetHandlers = new Dictionary<string, Func<IXLWorksheet, Task<int>>>(StringComparer.OrdinalIgnoreCase)
		{
			["מחוזות"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc16 = desc;
				if (!(await _db.Districts.AnyAsync((District x) => x.Description == desc16)))
				{
					_db.Districts.Add(new District
					{
						Description = desc16,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["מגזרים"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc15 = desc;
				if (!(await _db.Sectors.AnyAsync((Sector x) => x.Description == desc15)))
				{
					_db.Sectors.Add(new Sector
					{
						Description = desc15,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["ישובים"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc14 = desc;
				if (!(await _db.Localities.AnyAsync((Locality x) => x.Description == desc14)))
				{
					_db.Localities.Add(new Locality
					{
						Description = desc14,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["רשויות"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc13 = desc;
				if (!(await _db.Authorities.AnyAsync((Authority x) => x.Description == desc13)))
				{
					_db.Authorities.Add(new Authority
					{
						Description = desc13,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["פרויקטים"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc12 = desc;
				if (!(await _db.Projects.AnyAsync((Project x) => x.Description == desc12)))
				{
					_db.Projects.Add(new Project
					{
						Description = desc12,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["תוכניות"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc11 = desc;
				if (!(await _db.Programs.AnyAsync((AxiomaReporting.Core.Entities.Program x) => x.Description == desc11)))
				{
					_db.Programs.Add(new AxiomaReporting.Core.Entities.Program
					{
						Description = desc11,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["נושאים"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc10 = desc;
				if (!(await _db.Subjects.AnyAsync((Subject x) => x.Description == desc10)))
				{
					_db.Subjects.Add(new Subject
					{
						Description = desc10,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["תחומים"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc9 = desc;
				if (!(await _db.Domains.AnyAsync((Domain x) => x.Description == desc9)))
				{
					_db.Domains.Add(new Domain
					{
						Description = desc9,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["תוכניות חינוכיות"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc8 = desc;
				if (!(await _db.EducationalPrograms.AnyAsync((EducationalProgram x) => x.Description == desc8)))
				{
					_db.EducationalPrograms.Add(new EducationalProgram
					{
						Description = desc8,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["שכבות"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc7 = desc;
				if (!(await _db.GradeLevels.AnyAsync((GradeLevel x) => x.Description == desc7)))
				{
					_db.GradeLevels.Add(new GradeLevel
					{
						Description = desc7,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["כיתות"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc6 = desc;
				if (!(await _db.Classes.AnyAsync((SchoolClass x) => x.Description == desc6)))
				{
					_db.Classes.Add(new SchoolClass
					{
						Description = desc6,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["קיום דיון"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc5 = desc;
				if (!(await _db.DiscussionCodes.AnyAsync((DiscussionCode x) => x.Description == desc5)))
				{
					_db.DiscussionCodes.Add(new DiscussionCode
					{
						Description = desc5,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["ישוב מחוז ארצי"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc4 = desc;
				if (!(await _db.LocalityDistrictNationals.AnyAsync((LocalityDistrictNational x) => x.Description == desc4)))
				{
					_db.LocalityDistrictNationals.Add(new LocalityDistrictNational
					{
						Description = desc4,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["שלבי חינוך"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc3 = desc;
				if (!(await _db.EducationalStages.AnyAsync((EducationalStage x) => x.Description == desc3)))
				{
					_db.EducationalStages.Add(new EducationalStage
					{
						Description = desc3,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			}),
			["רמות השכלה"] = (IXLWorksheet ws) => ImportSimpleAsync(ws, async delegate(string desc)
			{
				string desc2 = desc;
				if (!(await _db.EducationTypes.AnyAsync((EducationType x) => x.Description == desc2)))
				{
					_db.EducationTypes.Add(new EducationType
					{
						Description = desc2,
						IsActive = true,
						CreatedAt = now
					});
					return true;
				}
				return false;
			})
		};
		foreach (IXLWorksheet worksheet in wb.Worksheets)
		{
			string sheetName = worksheet.Name.Trim();
			Func<IXLWorksheet, Task<int>> value;
			if (sheetName.Equals("מסגרות", StringComparison.OrdinalIgnoreCase))
			{
				int count = await ImportFrameworksAsync(worksheet);
				await _db.SaveChangesAsync();
				results.Add($"גיליון '{sheetName}': {count} רשומות נוספו");
			}
			else if (sheetHandlers.TryGetValue(sheetName, out value))
			{
				int count = await value(worksheet);
				await _db.SaveChangesAsync();
				results.Add($"גיליון '{sheetName}': {count} רשומות נוספו");
			}
			else
			{
				results.Add("גיליון '" + sheetName + "': לא זוהה — דולג");
			}
		}
		base.TempData["ImportResults"] = string.Join("|", results);
		base.TempData["Success"] = "ייבוא טבלאות עזר הושלם";
		return RedirectToAction("DataMigration");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ImportQuestionnaireCatalog(IFormFile? file)
	{
		if (!ValidateXlsxUpload(file))
		{
			return RedirectToAction("DataMigration");
		}
		DateTime now = DateTime.UtcNow;
		Dictionary<string, int> results = new Dictionary<string, int>
		{
			["פרויקטים"] = 0,
			["תוכניות חינוכיות"] = 0,
			["תחומים"] = 0,
			["נושאים"] = 0,
			["קיום דיון"] = 0,
			["כיתות"] = 0,
			["מסגרת חינוכית"] = 0,
			["ישוב/מחוז/ארצי"] = 0,
			["שכבות"] = 0
		};
		using Stream stream = file.OpenReadStream();
		using XLWorkbook wb = new XLWorkbook(stream);
		IXLWorksheet ws = wb.Worksheets.FirstOrDefault((IXLWorksheet x) => x.Name.Trim().Equals("כללי - מאוחד", StringComparison.OrdinalIgnoreCase));
		if (ws == null)
		{
			base.TempData["Error"] = "לא נמצא גיליון 'כללי - מאוחד' בקובץ השאלונים";
			return RedirectToAction("DataMigration");
		}
		HashSet<string> existingProjects = await LoadLookupSetAsync(_db.Projects);
		HashSet<string> existingEducationalPrograms = await LoadLookupSetAsync(_db.EducationalPrograms);
		HashSet<string> existingDomains = await LoadLookupSetAsync(_db.Domains);
		HashSet<string> existingSubjects = await LoadLookupSetAsync(_db.Subjects);
		HashSet<string> existingDiscussionCodes = await LoadLookupSetAsync(_db.DiscussionCodes);
		HashSet<string> existingClasses = await LoadLookupSetAsync(_db.Classes);
		HashSet<string> existingFrameworks = await LoadLookupSetAsync(_db.Frameworks);
		HashSet<string> existingLocalityDistrictNationals = await LoadLookupSetAsync(_db.LocalityDistrictNationals);
		HashSet<string> existingValues = await LoadLookupSetAsync(_db.GradeLevels);
		int num = ws.LastRowUsed()?.RowNumber() ?? 1;
		for (int i = 2; i <= num; i++)
		{
			results["פרויקטים"] += EnsureLookup(_db.Projects, existingProjects, ws.Cell(i, 1).GetString(), now);
			results["תוכניות חינוכיות"] += EnsureLookup(_db.EducationalPrograms, existingEducationalPrograms, ws.Cell(i, 2).GetString(), now);
			results["תחומים"] += EnsureLookup(_db.Domains, existingDomains, ws.Cell(i, 3).GetString(), now);
			results["נושאים"] += EnsureLookup(_db.Subjects, existingSubjects, ws.Cell(i, 4).GetString(), now);
			results["נושאים"] += EnsureLookup(_db.Subjects, existingSubjects, ws.Cell(i, 5).GetString(), now);
			results["קיום דיון"] += EnsureLookup(_db.DiscussionCodes, existingDiscussionCodes, ws.Cell(i, 6).GetString(), now);
			results["כיתות"] += EnsureLookup(_db.Classes, existingClasses, ws.Cell(i, 7).GetString(), now);
			results["מסגרת חינוכית"] += EnsureQuestionnaireFramework(_db.Frameworks, existingFrameworks, ws.Cell(i, 8).GetString(), now);
			results["ישוב/מחוז/ארצי"] += EnsureLookup(_db.LocalityDistrictNationals, existingLocalityDistrictNationals, ws.Cell(i, 9).GetString(), now);
			results["שכבות"] += EnsureLookup(_db.GradeLevels, existingValues, ws.Cell(i, 10).GetString(), now);
			results["כיתות"] += EnsureLookup(_db.Classes, existingClasses, ws.Cell(i, 11).GetString(), now);
		}
		await _db.SaveChangesAsync();
		base.TempData["ImportResults"] = string.Join("|", results.Select<KeyValuePair<string, int>, string>((KeyValuePair<string, int> x) => $"{x.Key}: {x.Value} רשומות נוספו"));
		base.TempData["Success"] = "ייבוא קטלוג השאלונים הושלם";
		return RedirectToAction("DataMigration");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ImportClientLookupXlsb(IFormFile? file)
	{
		if (!ValidateExcelUpload(file, ".xlsb"))
		{
			return RedirectToAction("DataMigration");
		}
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		using Stream stream = file.OpenReadStream();
		using IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
		DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
		{
			ConfigureDataTable = (IExcelDataReader _) => new ExcelDataTableConfiguration
			{
				UseHeaderRow = false
			}
		});
		Dictionary<string, int> results = new Dictionary<string, int>();
		DateTime now = DateTime.UtcNow;
		DataTable dataTable = dataSet.Tables["גיליון מרכז רשימות לפי שדות"];
		if (dataTable == null)
		{
			base.TempData["Error"] = "לא נמצא גיליון 'גיליון מרכז רשימות לפי שדות'";
			return RedirectToAction("DataMigration");
		}
		HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
		HashSet<string> sectors = new HashSet<string>(StringComparer.Ordinal);
		HashSet<string> stages = new HashSet<string>(StringComparer.Ordinal);
		HashSet<string> projects = new HashSet<string>(StringComparer.Ordinal);
		HashSet<string> educationalPrograms = new HashSet<string>(StringComparer.Ordinal);
		HashSet<string> domains = new HashSet<string>(StringComparer.Ordinal);
		HashSet<string> subjects = new HashSet<string>(StringComparer.Ordinal);
		HashSet<string> discussionCodes = new HashSet<string>(StringComparer.Ordinal);
		HashSet<string> classes = new HashSet<string>(StringComparer.Ordinal);
		for (int i = 22; i <= 56 && i < dataTable.Rows.Count; i++)
		{
			AddIfValue(sectors, GetCell(dataTable, i, 2), "מגזר עובד", "None");
			AddIfValue(hashSet, GetCell(dataTable, i, 4), "מחוז", "None");
			AddIfValue(stages, GetCell(dataTable, i, 12), "שלב חינוך", "None");
		}
		for (int j = 61; j < dataTable.Rows.Count; j++)
		{
			AddIfValue(projects, GetCell(dataTable, j, 0));
			AddIfValue(educationalPrograms, GetCell(dataTable, j, 2));
			AddIfValue(domains, GetCell(dataTable, j, 4));
			AddIfValue(subjects, GetCell(dataTable, j, 6));
			AddIfValue(subjects, GetCell(dataTable, j, 8));
			AddIfValue(discussionCodes, GetCell(dataTable, j, 10));
			AddIfValue(classes, GetCell(dataTable, j, 12));
		}
		string[] array = new string[13]
		{
			"ארצי", "מטה", "כללי", "דרום", "תל אביב", "חיפה", "צפון", "מרכז", "התישבותי", "מנח\"י",
			"ירושלים", "חרדי", "אחר"
		};
		foreach (string item in array)
		{
			hashSet.Add(item);
		}
		Dictionary<string, int> dictionary = results;
		dictionary["מחוזות"] = await ImportValuesAsync(_db.Districts, hashSet, now);
		dictionary = results;
		dictionary["מגזרים"] = await ImportValuesAsync(_db.Sectors, sectors, now);
		dictionary = results;
		dictionary["שלבי חינוך"] = await ImportValuesAsync(_db.EducationalStages, stages, now);
		dictionary = results;
		dictionary["פרויקטים"] = await ImportValuesAsync(_db.Projects, projects, now);
		dictionary = results;
		dictionary["תוכניות חינוכיות"] = await ImportValuesAsync(_db.EducationalPrograms, educationalPrograms, now);
		dictionary = results;
		dictionary["תחומים"] = await ImportValuesAsync(_db.Domains, domains, now);
		dictionary = results;
		dictionary["נושאים"] = await ImportValuesAsync(_db.Subjects, subjects, now);
		dictionary = results;
		dictionary["קיום דיון"] = await ImportValuesAsync(_db.DiscussionCodes, discussionCodes, now);
		dictionary = results;
		dictionary["כיתות"] = await ImportValuesAsync(_db.Classes, classes, now);
		DataTable dataTable2 = dataSet.Tables["יישוב"];
		if (dataTable2 != null)
		{
			HashSet<string> values = new HashSet<string>(StringComparer.Ordinal);
			for (int l = 1; l < dataTable2.Rows.Count; l++)
			{
				AddIfValue(values, GetCell(dataTable2, l, 0), "יישוב");
			}
			dictionary = results;
			dictionary["ישובים"] = await ImportValuesAsync(_db.Localities, values, now);
		}
		DataTable institutions = dataSet.Tables["מוסדות"];
		if (institutions != null)
		{
			dictionary = results;
			dictionary["רמות השכלה"] = await ImportEducationTypesFromInstitutionsAsync(institutions, now);
			await _db.SaveChangesAsync();
			dictionary = results;
			dictionary["מוסדות"] = await ImportInstitutionsFromDataTableAsync(institutions, now);
		}
		await _db.SaveChangesAsync();
		base.TempData["ImportResults"] = string.Join("|", results.Select<KeyValuePair<string, int>, string>((KeyValuePair<string, int> x) => $"{x.Key}: {x.Value} רשומות נוספו"));
		base.TempData["Success"] = "ייבוא קובץ טבלאות xlsb הושלם";
		return RedirectToAction("DataMigration");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ImportEmployees(IFormFile? file)
	{
		if (!ValidateXlsxUpload(file))
		{
			return RedirectToAction("DataMigration");
		}
		DateTime now = DateTime.UtcNow;
		List<EmployeeRole> roles = await _db.Roles.ToListAsync();
		int added = 0;
		int skipped = 0;
		List<string> errors = new List<string>();
		using Stream stream = file.OpenReadStream();
		using XLWorkbook wb = new XLWorkbook(stream);
		IXLWorksheet ws = wb.Worksheet(1);
		int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
		for (int row = 2; row <= lastRow; row++)
		{
			string employeeCode = ws.Cell(row, 1).GetString().Trim();
			string idNumber = ws.Cell(row, 2).GetString().Trim();
			string firstName = ws.Cell(row, 3).GetString().Trim();
			string lastName = ws.Cell(row, 4).GetString().Trim();
			string email = ws.Cell(row, 5).GetString().Trim();
			string phone = ws.Cell(row, 6).GetString().Trim();
			string roleName = ws.Cell(row, 7).GetString().Trim();
			string notes = ws.Cell(row, 8).GetString().Trim();
			if (string.IsNullOrEmpty(idNumber) || string.IsNullOrEmpty(firstName))
			{
				errors.Add($"שורה {row}: חסר ת.ז או שם פרטי — דולגת");
				continue;
			}
			if (await _db.Users.AnyAsync((User u) => u.IdNumber == idNumber))
			{
				skipped++;
				continue;
			}
			EmployeeRole employeeRole = roles.FirstOrDefault((EmployeeRole r) => r.Description.Contains(roleName, StringComparison.OrdinalIgnoreCase));
			string passwordHash = _passwordService.HashPassword(idNumber);
			_db.Users.Add(new User
			{
				EmployeeCode = (string.IsNullOrEmpty(employeeCode) ? idNumber : employeeCode),
				IdNumber = idNumber,
				FirstName = firstName,
				LastName = lastName,
				Email = (string.IsNullOrEmpty(email) ? null : email),
				Phone = (string.IsNullOrEmpty(phone) ? null : phone),
				Notes = (string.IsNullOrEmpty(notes) ? null : notes),
				RoleId = (employeeRole?.Id ?? 1),
				UserRoleId = 6,
				StatusId = 1,
				IsReportingEmployee = true,
				MustChangePassword = true,
				PasswordHash = passwordHash,
				CreatedAt = now
			});
			added++;
		}
		await _db.SaveChangesAsync();
		string text = $"עובדים: {added} נוספו, {skipped} קיימים (דולגו)";
		if (errors.Any())
		{
			text += $" | שגיאות: {errors.Count}";
		}
		base.TempData["ImportResults"] = string.Join("|", new string[1] { text }.Concat(errors));
		base.TempData["Success"] = "ייבוא עובדים הושלם";
		return RedirectToAction("DataMigration");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ImportInstitutions(IFormFile? file)
	{
		if (!ValidateXlsxUpload(file))
		{
			return RedirectToAction("DataMigration");
		}
		DateTime now = DateTime.UtcNow;
		int added = 0;
		int skipped = 0;
		List<string> errors = new List<string>();
		using Stream stream = file.OpenReadStream();
		using XLWorkbook wb = new XLWorkbook(stream);
		IXLWorksheet ws = wb.Worksheet(1);
		int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
		for (int row = 2; row <= lastRow; row++)
		{
			if (!ws.Cell(row, 1).TryGetValue<int>(out var symbol))
			{
				errors.Add($"שורה {row}: סמל מוסד חסר או לא תקין");
				continue;
			}
			string name = ws.Cell(row, 2).GetString().Trim();
			if (string.IsNullOrWhiteSpace(name))
			{
				errors.Add($"שורה {row}: שם מוסד חסר");
				continue;
			}
			int? localityId = await FindLocalityIdAsync(ws.Cell(row, 3).GetString());
			int? districtId = await FindDistrictIdAsync(ws.Cell(row, 4).GetString());
			int? sectorId = await FindSectorIdAsync(ws.Cell(row, 5).GetString());
			int? typeId = await FindEducationTypeIdAsync(ws.Cell(row, 6).GetString());
			int? stageId = await FindEducationalStageIdAsync(ws.Cell(row, 7).GetString());
			if (await _db.Institutions.AnyAsync((Institution i) => i.InstitutionSymbol == symbol && i.EducationalStageId == stageId))
			{
				skipped++;
				continue;
			}
			_db.Institutions.Add(new Institution
			{
				InstitutionSymbol = symbol,
				Name = name,
				LocalityId = localityId,
				DistrictId = districtId,
				SectorId = sectorId,
				TypeId = typeId,
				EducationalStageId = stageId,
				IsActive = true,
				CreatedAt = now
			});
			added++;
		}
		await _db.SaveChangesAsync();
		base.TempData["ImportResults"] = string.Join("|", new string[1] { $"מוסדות: {added} נוספו, {skipped} קיימים (דולגו)" }.Concat(errors));
		base.TempData["Success"] = "ייבוא מוסדות הושלם";
		return RedirectToAction("DataMigration");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ImportAllocations(IFormFile? file)
	{
		if (!ValidateXlsxUpload(file))
		{
			return RedirectToAction("DataMigration");
		}
		int added = 0;
		int updated = 0;
		int errorsCount = 0;
		List<string> errors = new List<string>();
		using Stream stream = file.OpenReadStream();
		using XLWorkbook wb = new XLWorkbook(stream);
		IXLWorksheet ws = wb.Worksheet(1);
		int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
		for (int row = 2; row <= lastRow; row++)
		{
			string idNumber = ws.Cell(row, 1).GetString().Trim();
			string projectName = ws.Cell(row, 2).GetString().Trim();
			User user = await _db.Users.FirstOrDefaultAsync((User u) => u.IdNumber == idNumber);
			int? projectId = await FindProjectIdAsync(projectName);
			if (user == null || !projectId.HasValue)
			{
				errors.Add($"שורה {row}: עובד או פרויקט לא נמצאו");
				errorsCount++;
				continue;
			}
			Allocation allocation = await _db.Allocations.FirstOrDefaultAsync((Allocation a) => a.UserId == user.Id && a.ProjectId == ((int?)projectId).Value);
			bool isNew = allocation == null;
			if (allocation == null)
			{
				allocation = new Allocation
				{
					UserId = user.Id,
					ProjectId = projectId.Value,
					CreatedAt = DateTime.UtcNow
				};
			}
			allocation.AnnualEmploymentScope = ReadDecimal(ws, row, 3);
			allocation.MonthlyEmploymentScope = ReadDecimal(ws, row, 4);
			allocation.DailyEmploymentScope = ReadDailyScope(ws.Cell(row, 5).GetString());
			allocation.MonthlyRowAllocation = ReadInt(ws, row, 6);
			allocation.AnnualRowAllocation = ReadInt(ws, row, 7);
			allocation.OutputDuration = ws.Cell(row, 8).GetString().Trim();
			allocation.AllowExcelUpload = ReadBool(ws.Cell(row, 9).GetString());
			allocation.Notes = EmptyToNull(ws.Cell(row, 10).GetString());
			allocation.IsActive = true;
			allocation.UpdatedAt = DateTime.UtcNow;
			if (isNew)
			{
				_db.Allocations.Add(allocation);
			}
			await _db.SaveChangesAsync();
			await ReplaceAllocationLinksAsync(allocation.Id, ws, row);
			if (isNew)
			{
				added++;
			}
			else
			{
				updated++;
			}
		}
		base.TempData["ImportResults"] = string.Join("|", new string[1] { $"הקצאות: {added} נוספו, {updated} עודכנו, {errorsCount} שגיאות" }.Concat(errors));
		base.TempData["Success"] = "ייבוא הקצאות הושלם";
		return RedirectToAction("DataMigration");
	}

	private async Task<int> ImportSimpleAsync(IXLWorksheet ws, Func<string, Task<bool>> insertFn)
	{
		int count = 0;
		int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
		for (int row = 2; row <= lastRow; row++)
		{
			string text = ws.Cell(row, 1).GetString().Trim();
			if (!string.IsNullOrEmpty(text) && await insertFn(text))
			{
				count++;
			}
		}
		return count;
	}

	private bool ValidateXlsxUpload(IFormFile? file)
	{
		return ValidateExcelUpload(file, ".xlsx");
	}

	private bool ValidateExcelUpload(IFormFile? file, string extension)
	{
		if (file == null || file.Length == 0L)
		{
			base.TempData["Error"] = "לא נבחר קובץ";
			return false;
		}
		if (!Path.GetExtension(file.FileName).Equals(extension, StringComparison.OrdinalIgnoreCase))
		{
			base.TempData["Error"] = "קובץ לא תקין. פעולה זו תומכת בקובץ " + extension + " בלבד.";
			return false;
		}
		return true;
	}

	private static async Task<HashSet<string>> LoadLookupSetAsync<T>(DbSet<T> set) where T : LookupEntity, new()
	{
		return (await set.Select((T x) => x.Description).ToListAsync()).ToHashSet<string>(StringComparer.Ordinal);
	}

	private static int EnsureLookup<T>(DbSet<T> set, HashSet<string> existingValues, string value, DateTime now) where T : LookupEntity, new()
	{
		string text = value.Trim();
		if (string.IsNullOrWhiteSpace(text))
		{
			return 0;
		}
		if (!existingValues.Add(text))
		{
			return 0;
		}
		set.Add(new T
		{
			Description = text,
			IsActive = true,
			CreatedAt = now
		});
		return 1;
	}

	private async Task<int> ImportValuesAsync<T>(DbSet<T> set, IEnumerable<string> values, DateTime now) where T : LookupEntity, new()
	{
		HashSet<string> existingValues = await LoadLookupSetAsync(set);
		int num = 0;
		foreach (string value in values)
		{
			num += EnsureLookup(set, existingValues, value, now);
		}
		return num;
	}

	private static int EnsureQuestionnaireFramework(DbSet<Framework> set, HashSet<string> existingValues, string value, DateTime now)
	{
		string text = value.Trim();
		if (string.IsNullOrWhiteSpace(text))
		{
			return 0;
		}
		if (!existingValues.Add(text))
		{
			return 0;
		}
		set.Add(new Framework
		{
			Description = text,
			InstitutionSymbol = "QCAT-" + StableShortHash(text),
			IsActive = true,
			CreatedAt = now
		});
		return 1;
	}

	private static string StableShortHash(string value)
	{
		byte[] inArray = SHA256.HashData(Encoding.UTF8.GetBytes(value));
		return Convert.ToHexString(inArray, 0, 6);
	}

	private static string GetCell(DataTable table, int row, int col)
	{
		if (row < 0 || row >= table.Rows.Count || col < 0 || col >= table.Columns.Count)
		{
			return string.Empty;
		}
		return table.Rows[row][col]?.ToString()?.Trim() ?? string.Empty;
	}

	private static void AddIfValue(HashSet<string> values, string value, params string[] excluded)
	{
		value = value.Trim();
		if (!string.IsNullOrWhiteSpace(value) && !excluded.Contains<string>(value, StringComparer.Ordinal))
		{
			values.Add(value);
		}
	}

	private async Task<int> ImportEducationTypesFromInstitutionsAsync(DataTable institutions, DateTime now)
	{
		HashSet<string> values = new HashSet<string>(StringComparer.Ordinal);
		for (int i = 1; i < institutions.Rows.Count; i++)
		{
			AddIfValue(values, GetCell(institutions, i, 5));
		}
		return await ImportValuesAsync(_db.EducationTypes, values, now);
	}

	private async Task<int> ImportInstitutionsFromDataTableAsync(DataTable institutions, DateTime now)
	{
		Dictionary<string, int> localityMap = await _db.Localities.ToDictionaryAsync((Locality x) => x.Description, (Locality x) => x.Id);
		Dictionary<string, int> districtMap = await _db.Districts.ToDictionaryAsync((District x) => x.Description, (District x) => x.Id);
		Dictionary<string, int> sectorMap = await _db.Sectors.ToDictionaryAsync((Sector x) => x.Description, (Sector x) => x.Id);
		Dictionary<string, int> educationTypeMap = await _db.EducationTypes.ToDictionaryAsync((EducationType x) => x.Description, (EducationType x) => x.Id);
		Dictionary<string, int> stageMap = await _db.EducationalStages.ToDictionaryAsync((EducationalStage x) => x.Description, (EducationalStage x) => x.Id);
		HashSet<string> hashSet = (await _db.Institutions.Select((Institution x) => new { x.InstitutionSymbol, x.EducationalStageId }).ToListAsync()).Select(x => $"{x.InstitutionSymbol}|{x.EducationalStageId}").ToHashSet<string>(StringComparer.Ordinal);
		int num = 0;
		for (int i = 1; i < institutions.Rows.Count; i++)
		{
			string cell = GetCell(institutions, i, 0);
			if (!decimal.TryParse(cell, out var result))
			{
				continue;
			}
			int num2 = (int)result;
			string cell2 = GetCell(institutions, i, 1);
			if (!string.IsNullOrWhiteSpace(cell2))
			{
				int? mappedId = GetMappedId(localityMap, GetCell(institutions, i, 2));
				int? mappedId2 = GetMappedId(districtMap, GetCell(institutions, i, 3));
				int? mappedId3 = GetMappedId(sectorMap, GetCell(institutions, i, 4));
				int? mappedId4 = GetMappedId(educationTypeMap, GetCell(institutions, i, 5));
				int? mappedId5 = GetMappedId(stageMap, GetCell(institutions, i, 6));
				if (hashSet.Add($"{num2}|{mappedId5}"))
				{
					_db.Institutions.Add(new Institution
					{
						InstitutionSymbol = num2,
						Name = cell2,
						LocalityId = mappedId,
						DistrictId = mappedId2,
						SectorId = mappedId3,
						TypeId = mappedId4,
						EducationalStageId = mappedId5,
						IsActive = true,
						CreatedAt = now
					});
					num++;
				}
			}
		}
		return num;
	}

	private static int? GetMappedId(Dictionary<string, int> map, string value)
	{
		if (!map.TryGetValue(value, out var value2))
		{
			return null;
		}
		return value2;
	}

	private async Task<int> ImportFrameworksAsync(IXLWorksheet ws)
	{
		int count = 0;
		DateTime now = DateTime.UtcNow;
		int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
		for (int row = 2; row <= lastRow; row++)
		{
			string description = ws.Cell(row, 1).GetString().Trim();
			string institutionSymbol = ws.Cell(row, 2).GetString().Trim();
			int? stageId = await FindEducationalStageIdAsync(ws.Cell(row, 3).GetString());
			if (!string.IsNullOrWhiteSpace(description) && !(string.IsNullOrWhiteSpace(institutionSymbol) ? (await _db.Frameworks.AnyAsync((Framework f) => f.Description == description)) : (await _db.Frameworks.AnyAsync((Framework f) => f.InstitutionSymbol == institutionSymbol && f.EducationalStageId == stageId))))
			{
				_db.Frameworks.Add(new Framework
				{
					Description = description,
					InstitutionSymbol = institutionSymbol,
					EducationalStageId = stageId,
					IsActive = true,
					CreatedAt = now
				});
				count++;
			}
		}
		return count;
	}

	private async Task ReplaceAllocationLinksAsync(int allocationId, IXLWorksheet ws, int row)
	{
		_db.Set<AllocationDistrict>().RemoveRange(from x in _db.Set<AllocationDistrict>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationProgram>().RemoveRange(from x in _db.Set<AllocationProgram>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationSector>().RemoveRange(from x in _db.Set<AllocationSector>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationLocality>().RemoveRange(from x in _db.Set<AllocationLocality>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationFramework>().RemoveRange(from x in _db.Set<AllocationFramework>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationSubject>().RemoveRange(from x in _db.Set<AllocationSubject>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationDomain>().RemoveRange(from x in _db.Set<AllocationDomain>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationEducationalProgram>().RemoveRange(from x in _db.Set<AllocationEducationalProgram>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationClass>().RemoveRange(from x in _db.Set<AllocationClass>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationGradeLevel>().RemoveRange(from x in _db.Set<AllocationGradeLevel>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationDiscussionCode>().RemoveRange(from x in _db.Set<AllocationDiscussionCode>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationLocalityDistrictNational>().RemoveRange(from x in _db.Set<AllocationLocalityDistrictNational>()
			where x.AllocationId == allocationId
			select x);
		await _db.SaveChangesAsync();
		foreach (int item in await FindIdsAsync(ws.Cell(row, 11).GetString(), FindDistrictIdAsync))
		{
			_db.Set<AllocationDistrict>().Add(new AllocationDistrict
			{
				AllocationId = allocationId,
				DistrictId = item
			});
		}
		foreach (int item2 in await FindIdsAsync(ws.Cell(row, 12).GetString(), FindProgramIdAsync))
		{
			_db.Set<AllocationProgram>().Add(new AllocationProgram
			{
				AllocationId = allocationId,
				ProgramId = item2
			});
		}
		foreach (int item3 in await FindIdsAsync(ws.Cell(row, 13).GetString(), FindSectorIdAsync))
		{
			_db.Set<AllocationSector>().Add(new AllocationSector
			{
				AllocationId = allocationId,
				SectorId = item3
			});
		}
		foreach (int item4 in await FindIdsAsync(ws.Cell(row, 14).GetString(), FindLocalityIdAsync))
		{
			_db.Set<AllocationLocality>().Add(new AllocationLocality
			{
				AllocationId = allocationId,
				LocalityId = item4
			});
		}
		foreach (int item5 in await FindIdsAsync(ws.Cell(row, 15).GetString(), FindFrameworkIdAsync))
		{
			_db.Set<AllocationFramework>().Add(new AllocationFramework
			{
				AllocationId = allocationId,
				FrameworkId = item5
			});
		}
		foreach (int item6 in await FindIdsAsync(ws.Cell(row, 16).GetString(), FindSubjectIdAsync))
		{
			_db.Set<AllocationSubject>().Add(new AllocationSubject
			{
				AllocationId = allocationId,
				SubjectId = item6
			});
		}
		foreach (int item7 in await FindIdsAsync(ws.Cell(row, 17).GetString(), FindDomainIdAsync))
		{
			_db.Set<AllocationDomain>().Add(new AllocationDomain
			{
				AllocationId = allocationId,
				DomainId = item7
			});
		}
		foreach (int item8 in await FindIdsAsync(ws.Cell(row, 18).GetString(), FindEducationalProgramIdAsync))
		{
			_db.Set<AllocationEducationalProgram>().Add(new AllocationEducationalProgram
			{
				AllocationId = allocationId,
				EducationalProgramId = item8
			});
		}
		foreach (int item9 in await FindIdsAsync(ws.Cell(row, 19).GetString(), FindClassIdAsync))
		{
			_db.Set<AllocationClass>().Add(new AllocationClass
			{
				AllocationId = allocationId,
				ClassId = item9
			});
		}
		foreach (int item10 in await FindIdsAsync(ws.Cell(row, 20).GetString(), FindGradeLevelIdAsync))
		{
			_db.Set<AllocationGradeLevel>().Add(new AllocationGradeLevel
			{
				AllocationId = allocationId,
				GradeLevelId = item10
			});
		}
		foreach (int item11 in await FindIdsAsync(ws.Cell(row, 21).GetString(), FindDiscussionCodeIdAsync))
		{
			_db.Set<AllocationDiscussionCode>().Add(new AllocationDiscussionCode
			{
				AllocationId = allocationId,
				DiscussionCodeId = item11
			});
		}
		foreach (int item12 in await FindIdsAsync(ws.Cell(row, 22).GetString(), FindLocalityDistrictNationalIdAsync))
		{
			_db.Set<AllocationLocalityDistrictNational>().Add(new AllocationLocalityDistrictNational
			{
				AllocationId = allocationId,
				LocalityDistrictNationalId = item12
			});
		}
		await _db.SaveChangesAsync();
	}

	private static decimal? ReadDecimal(IXLWorksheet ws, int row, int col)
	{
		if (!ws.Cell(row, col).TryGetValue<decimal>(out var value))
		{
			return null;
		}
		return value;
	}

	private static int? ReadInt(IXLWorksheet ws, int row, int col)
	{
		if (!ws.Cell(row, col).TryGetValue<int>(out var value))
		{
			return null;
		}
		return value;
	}

	private static bool ReadBool(string value)
	{
		if (!value.Trim().Equals("true", StringComparison.OrdinalIgnoreCase) && !value.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase) && !value.Trim().Equals("כן", StringComparison.OrdinalIgnoreCase))
		{
			return value.Trim() == "1";
		}
		return true;
	}

	private static decimal? ReadDailyScope(string value)
	{
		value = value.Trim();
		if (string.IsNullOrEmpty(value) || value.Equals("Unlimited", StringComparison.OrdinalIgnoreCase) || value.Equals("ללא הגבלה", StringComparison.OrdinalIgnoreCase))
		{
			return null;
		}
		if (!decimal.TryParse(value, out var result))
		{
			return null;
		}
		return result;
	}

	private static string? EmptyToNull(string value)
	{
		if (!string.IsNullOrWhiteSpace(value))
		{
			return value.Trim();
		}
		return null;
	}

	private static async Task<List<int>> FindIdsAsync(string values, Func<string, Task<int?>> resolver)
	{
		List<int> ids = new List<int>();
		string[] array = values.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		foreach (string arg in array)
		{
			int? num = await resolver(arg);
			if (num.HasValue)
			{
				ids.Add(num.Value);
			}
		}
		return ids.Distinct().ToList();
	}

	private async Task<int?> FindDistrictIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.Districts, value);
	}

	private async Task<int?> FindSectorIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.Sectors, value);
	}

	private async Task<int?> FindLocalityIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.Localities, value);
	}

	private async Task<int?> FindProjectIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.Projects, value);
	}

	private async Task<int?> FindProgramIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.Programs, value);
	}

	private async Task<int?> FindSubjectIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.Subjects, value);
	}

	private async Task<int?> FindDomainIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.Domains, value);
	}

	private async Task<int?> FindEducationalProgramIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.EducationalPrograms, value);
	}

	private async Task<int?> FindClassIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.Classes, value);
	}

	private async Task<int?> FindGradeLevelIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.GradeLevels, value);
	}

	private async Task<int?> FindDiscussionCodeIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.DiscussionCodes, value);
	}

	private async Task<int?> FindLocalityDistrictNationalIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.LocalityDistrictNationals, value);
	}

	private async Task<int?> FindEducationTypeIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.EducationTypes, value);
	}

	private async Task<int?> FindEducationalStageIdAsync(string value)
	{
		return await FindLookupIdAsync(_db.EducationalStages, value);
	}

	private async Task<int?> FindFrameworkIdAsync(string value)
	{
		string value2 = value;
		value2 = value2.Trim();
		if (string.IsNullOrEmpty(value2))
		{
			return null;
		}
		if (int.TryParse(value2, out var symbol))
		{
			return await _db.Frameworks.Where((Framework f) => f.InstitutionSymbol == ((int)symbol).ToString()).Select((Expression<Func<Framework, int?>>)((Framework f) => f.Id)).FirstOrDefaultAsync();
		}
		return await _db.Frameworks.Where((Framework f) => f.Description == value2).Select((Expression<Func<Framework, int?>>)((Framework f) => f.Id)).FirstOrDefaultAsync();
	}

	private static async Task<int?> FindLookupIdAsync<T>(IQueryable<T> query, string value) where T : LookupEntity
	{
		string value2 = value;
		value2 = value2.Trim();
		if (string.IsNullOrEmpty(value2))
		{
			return null;
		}
		int id;
		bool flag = int.TryParse(value2, out id);
		bool flag2 = flag;
		if (flag2)
		{
			flag2 = await query.AnyAsync((T x) => x.Id == id);
		}
		if (flag2)
		{
			return id;
		}
		return await query.Where((T x) => x.Description == value2).Select((Expression<Func<T, int?>>)((T x) => x.Id)).FirstOrDefaultAsync();
	}

	[HttpGet]
	public async Task<IActionResult> BatchReportImport()
	{
		List<ReportingMonth> list = await (from m in _db.ReportingMonths
			orderby m.Year descending, m.Month descending
			select m).ToListAsync();
		ReportingMonth reportingMonth = list.FirstOrDefault((ReportingMonth m) => m.IsActive);
		return View(new BatchReportImportFormViewModel
		{
			ReportingMonths = list,
			SelectedReportingMonthId = ((reportingMonth != null) ? new int?(reportingMonth.Id) : list.FirstOrDefault()?.Id)
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> BatchReportImport(IFormFile file, int reportingMonthId, string progressId, CancellationToken ct)
	{
		if (file == null || file.Length == 0L)
		{
			base.TempData["Error"] = "יש לבחור קובץ Excel לייבוא";
			return RedirectToAction("BatchReportImport");
		}
		int uploaderId = int.Parse(base.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));
		User uploader = await _db.Users.FindAsync(new object[1] { uploaderId }, ct);
		string uploaderName = ((uploader == null) ? "" : (uploader.FirstName + " " + uploader.LastName).Trim());
		ReportingMonth month = await _db.ReportingMonths.FindAsync(new object[1] { reportingMonthId }, ct);
		Stream stream = file.OpenReadStream();
		IActionResult result2;
		try
		{
			BatchImportResult result = await _batchImportService.ImportAsync(stream, reportingMonthId, uploaderId, ct, progressId);
			byte[] errorsExcel = null;
			if (result.Errors.Any())
			{
				errorsExcel = CreateBatchImportErrorsExcel(result.Errors);
			}
			if (!string.IsNullOrWhiteSpace(uploader?.Email) && month != null)
			{
				await _emailService.SendAsync(uploader.Email, uploaderName, "BatchImportSuccessUploader", new Dictionary<string, string>
				{
					["UploaderName"] = uploaderName,
					["RowsImported"] = result.RowsImported.ToString(CultureInfo.InvariantCulture),
					["EmployeesCount"] = result.EmployeesAffected.ToString(CultureInfo.InvariantCulture),
					["Month"] = month.Month.ToString(CultureInfo.InvariantCulture),
					["Year"] = month.Year.ToString(CultureInfo.InvariantCulture)
				}, null, ct);
				if (errorsExcel != null)
				{
					string fileName = $"batch-import-errors-{month.Year}-{month.Month:D2}.xlsx";
					List<EmailAttachment> attachments = new List<EmailAttachment>
					{
						new EmailAttachment(fileName, errorsExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
					};
					await _emailService.SendAsync(uploader.Email, uploaderName, "BatchImportErrors", new Dictionary<string, string>
					{
						["UploaderName"] = uploaderName,
						["ErrorsCount"] = result.ErrorRowsCount.ToString(CultureInfo.InvariantCulture),
						["Month"] = month.Month.ToString(CultureInfo.InvariantCulture),
						["Year"] = month.Year.ToString(CultureInfo.InvariantCulture)
					}, attachments, ct);
				}
			}
			if (result.Errors.Any())
			{
				string value = string.Join("\n", result.Errors.Select((BatchImportError e) => string.Join("\t", new string[5]
				{
					e.FileRowNumber.ToString(CultureInfo.InvariantCulture),
					e.EmployeeCode ?? string.Empty,
					e.ReporterName ?? string.Empty,
					e.ErrorMessage,
					e.RawValues ?? string.Empty
				})));
				base.TempData["BatchImportErrors"] = value;
			}
			result2 = View("BatchReportImportResult", new BatchReportImportResultViewModel
			{
				MonthDescription = month?.Description,
				MonthNumber = month?.Month,
				Year = month?.Year,
				Result = result
			});
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

	[HttpGet]
	public IActionResult BatchReportImportProgress(string id)
	{
		return Json(BatchImportProgressStore.Get(id));
	}

	[HttpGet]
	public IActionResult BatchReportImportErrorsExcel()
	{
		string text = (base.TempData["BatchImportErrors"] as string) ?? string.Empty;
		base.TempData.Keep("BatchImportErrors");
		byte[] fileContents = CreateBatchImportErrorsExcel(text);
		return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"batch-import-errors-{DateTime.Now:yyyyMMddHHmm}.xlsx");
	}

	[HttpGet]
	public IActionResult BatchReportImportErrorsPdf()
	{
		return BatchReportImportErrorsExcel();
	}

	private static byte[] CreateBatchImportErrorsExcel(IEnumerable<BatchImportError> errors)
	{
		using XLWorkbook workbook = new XLWorkbook();
		IXLWorksheet ws = workbook.Worksheets.Add("שגיאות יבוא");
		ws.RightToLeft = true;
		string[] headers = new string[5] { "שורה בקובץ", "קוד עובד", "שם מדווח", "שגיאה", "ערכי מקור" };
		for (int i = 0; i < headers.Length; i++)
		{
			ws.Cell(1, i + 1).Value = headers[i];
		}
		int row = 2;
		foreach (BatchImportError error in errors.OrderBy((BatchImportError e) => e.FileRowNumber))
		{
			ws.Cell(row, 1).Value = error.FileRowNumber;
			ws.Cell(row, 2).Value = error.EmployeeCode ?? string.Empty;
			ws.Cell(row, 3).Value = error.ReporterName ?? string.Empty;
			ws.Cell(row, 4).Value = error.ErrorMessage;
			ws.Cell(row, 5).Value = error.RawValues ?? string.Empty;
			row++;
		}
		ws.Row(1).Style.Font.Bold = true;
		ws.Columns().AdjustToContents();
		using MemoryStream stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}

	private static byte[] CreateBatchImportErrorsExcel(string tempDataText)
	{
		using XLWorkbook workbook = new XLWorkbook();
		IXLWorksheet ws = workbook.Worksheets.Add("שגיאות יבוא");
		ws.RightToLeft = true;
		string[] headers = new string[5] { "שורה בקובץ", "קוד עובד", "שם מדווח", "שגיאה", "ערכי מקור" };
		for (int i = 0; i < headers.Length; i++)
		{
			ws.Cell(1, i + 1).Value = headers[i];
		}
		string[] lines = string.IsNullOrWhiteSpace(tempDataText) ? Array.Empty<string>() : tempDataText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
		int row = 2;
		foreach (string line in lines)
		{
			string[] parts = line.Split('\t');
			for (int col = 0; col < headers.Length; col++)
			{
				ws.Cell(row, col + 1).Value = col < parts.Length ? parts[col] : string.Empty;
			}
			row++;
		}
		if (lines.Length == 0)
		{
			ws.Cell(2, 1).Value = "לא נרשמו שגיאות";
		}
		ws.Row(1).Style.Font.Bold = true;
		ws.Columns().AdjustToContents();
		using MemoryStream stream = new MemoryStream();
		workbook.SaveAs(stream);
		return stream.ToArray();
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> TermsOfUse()
	{
		return View(await (from v in _db.TermsOfUseVersions
			orderby v.VersionNumber descending
			select new TermsOfUseVersionListItem
			{
				Id = v.Id,
				VersionNumber = v.VersionNumber,
				EffectiveFrom = v.EffectiveFrom,
				CreatedAt = v.CreatedAt,
				PublishedByName = ((v.PublishedByUser == null) ? "" : string.Concat(v.PublishedByUser.FirstName + " ", v.PublishedByUser.LastName).Trim()),
				AcceptanceCount = v.Acceptances.Count,
				BodyHtml = v.BodyHtml
			}).ToListAsync());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> PublishTermsOfUse(string bodyHtml, DateTime? effectiveFrom)
	{
		if (string.IsNullOrWhiteSpace(bodyHtml))
		{
			base.TempData["Error"] = "יש להזין תוכן לתנאי השימוש";
			return RedirectToAction("TermsOfUse");
		}
		string s = base.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
		if (!int.TryParse(s, out var actorId))
		{
			base.TempData["Error"] = "לא ניתן לזהות את המשתמש המבצע";
			return RedirectToAction("TermsOfUse");
		}
		int nextVersion = (await ((IQueryable<TermsOfUseVersion>)_db.TermsOfUseVersions).MaxAsync((Expression<Func<TermsOfUseVersion, int?>>)((TermsOfUseVersion v) => v.VersionNumber), default(CancellationToken))).GetValueOrDefault() + 1;
		DateTime utcNow = DateTime.UtcNow;
		TermsOfUseVersion termsVersion = new TermsOfUseVersion
		{
			VersionNumber = nextVersion,
			BodyHtml = bodyHtml,
			EffectiveFrom = effectiveFrom.GetValueOrDefault(utcNow),
			PublishedByUserId = actorId,
			CreatedAt = utcNow
		};
		_db.TermsOfUseVersions.Add(termsVersion);
		await _db.SaveChangesAsync();
		await _auditLog.LogAsync("Terms.Publish", "TermsOfUseVersion", termsVersion.Id.ToString(), null, new { termsVersion.Id, termsVersion.VersionNumber, termsVersion.EffectiveFrom, termsVersion.PublishedByUserId });
		foreach (User item in await _db.Users.ToListAsync())
		{
			item.AcceptedTermsOfUse = false;
		}
		await _db.SaveChangesAsync();
		base.TempData["Success"] = $"גרסה {nextVersion} של תנאי השימוש פורסמה. כל המשתמשים יתבקשו לאשר מחדש.";
		return RedirectToAction("TermsOfUse");
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> PrivacyPolicy()
	{
		List<TermsOfUseVersionListItem> versions = await (from v in _db.PrivacyPolicyVersions
			orderby v.VersionNumber descending
			select new TermsOfUseVersionListItem
			{
				Id = v.Id,
				VersionNumber = v.VersionNumber,
				EffectiveFrom = v.EffectiveFrom,
				CreatedAt = v.CreatedAt,
				PublishedByName = ((v.PublishedByUser == null) ? "" : string.Concat(v.PublishedByUser.FirstName + " ", v.PublishedByUser.LastName).Trim()),
				AcceptanceCount = 0,
				BodyHtml = v.BodyHtml
			}).ToListAsync();
		string latestBody = versions.FirstOrDefault()?.BodyHtml ?? string.Empty;
		string token = _antiforgery.GetAndStoreTokens(base.HttpContext).RequestToken ?? string.Empty;
		StringBuilder html = new StringBuilder();
		html.Append("<!doctype html><html lang=\"he\" dir=\"rtl\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link rel=\"stylesheet\" href=\"/lib/bootstrap/dist/css/bootstrap.min.css\"><title>ניהול גרסאות מדיניות פרטיות</title></head><body><main class=\"container-fluid mt-3\">");
		html.Append("<div class=\"d-flex justify-content-between align-items-center mb-3\"><h3>ניהול גרסאות מדיניות פרטיות</h3><a class=\"btn btn-outline-secondary btn-sm\" href=\"/Home/Privacy\" target=\"_blank\">צפייה במדיניות</a></div>");
		if (base.TempData["Success"] != null)
		{
			html.Append("<div class=\"alert alert-success\">").Append(WebUtility.HtmlEncode(base.TempData["Success"]?.ToString())).Append("</div>");
		}
		if (base.TempData["Error"] != null)
		{
			html.Append("<div class=\"alert alert-danger\">").Append(WebUtility.HtmlEncode(base.TempData["Error"]?.ToString())).Append("</div>");
		}
		html.Append("<div class=\"card mb-4\"><div class=\"card-header\"><h5 class=\"mb-0\">פרסום גרסה חדשה</h5></div><div class=\"card-body\">");
		html.Append("<form method=\"post\" action=\"/Admin/PublishPrivacyPolicy\"><input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"").Append(WebUtility.HtmlEncode(token)).Append("\" />");
		html.Append("<div class=\"mb-3\"><label for=\"effectiveFrom\" class=\"form-label\">בתוקף מתאריך</label><input id=\"effectiveFrom\" name=\"effectiveFrom\" type=\"datetime-local\" class=\"form-control\" /></div>");
		html.Append("<div class=\"mb-3\"><label for=\"bodyHtml\" class=\"form-label\">תוכן מדיניות הפרטיות <span class=\"text-danger\">*</span></label><textarea id=\"bodyHtml\" name=\"bodyHtml\" class=\"form-control\" rows=\"14\" required>");
		html.Append(WebUtility.HtmlEncode(latestBody));
		html.Append("</textarea><div class=\"form-text\">כל שמירה מפרסמת גרסה חדשה. הגרסה האחרונה היא זו שמוצגת למשתמשים במסך מדיניות פרטיות.</div></div>");
		html.Append("<button type=\"submit\" class=\"btn btn-primary\">פרסם גרסה חדשה</button></form></div></div>");
		html.Append("<div class=\"card\"><div class=\"card-header\"><h5 class=\"mb-0\">היסטוריית גרסאות</h5></div><div class=\"table-responsive\"><table class=\"table table-striped align-middle mb-0\"><thead><tr><th>גרסה</th><th>בתוקף מתאריך</th><th>נוצרה בתאריך</th><th>פורסם על ידי</th><th>תוכן</th><th></th></tr></thead><tbody>");
		foreach (TermsOfUseVersionListItem version in versions)
		{
			string previewId = "privacy-preview-" + version.Id.ToString(CultureInfo.InvariantCulture);
			html.Append("<tr><td>").Append(version.VersionNumber).Append("</td><td>").Append(WebUtility.HtmlEncode(version.EffectiveFrom.ToLocalTime().ToString("dd/MM/yyyy HH:mm"))).Append("</td><td>").Append(WebUtility.HtmlEncode(version.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"))).Append("</td><td>").Append(WebUtility.HtmlEncode(version.PublishedByName)).Append("</td><td><div id=\"").Append(previewId).Append("\" class=\"border rounded p-2 bg-light\" style=\"max-height:160px;overflow:auto\">").Append(version.BodyHtml).Append("</div></td><td><button type=\"button\" class=\"btn btn-outline-secondary btn-sm\" data-source=\"").Append(previewId).Append("\" onclick=\"document.getElementById('bodyHtml').value=document.getElementById(this.dataset.source).innerHTML;document.getElementById('bodyHtml').focus();\">ערוך לפי גרסה זו</button></td></tr>");
		}
		if (versions.Count == 0)
		{
			html.Append("<tr><td colspan=\"6\" class=\"text-muted text-center\">לא פורסמו גרסאות מדיניות פרטיות</td></tr>");
		}
		html.Append("</tbody></table></div></div></main></body></html>");
		return Content(html.ToString(), "text/html; charset=utf-8");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> PublishPrivacyPolicy(string bodyHtml, DateTime? effectiveFrom)
	{
		if (string.IsNullOrWhiteSpace(bodyHtml))
		{
			base.TempData["Error"] = "יש להזין תוכן למדיניות הפרטיות";
			return RedirectToAction("PrivacyPolicy");
		}
		string s = base.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
		if (!int.TryParse(s, out var actorId))
		{
			base.TempData["Error"] = "לא ניתן לזהות את המשתמש המבצע";
			return RedirectToAction("PrivacyPolicy");
		}
		int nextVersion = (await ((IQueryable<PrivacyPolicyVersion>)_db.PrivacyPolicyVersions).MaxAsync((Expression<Func<PrivacyPolicyVersion, int?>>)((PrivacyPolicyVersion v) => v.VersionNumber), default(CancellationToken))).GetValueOrDefault() + 1;
		DateTime utcNow = DateTime.UtcNow;
		PrivacyPolicyVersion privacyVersion = new PrivacyPolicyVersion
		{
			VersionNumber = nextVersion,
			BodyHtml = bodyHtml,
			EffectiveFrom = effectiveFrom.GetValueOrDefault(utcNow),
			PublishedByUserId = actorId,
			CreatedAt = utcNow
		};
		_db.PrivacyPolicyVersions.Add(privacyVersion);
		await _db.SaveChangesAsync();
		await _auditLog.LogAsync("Privacy.Publish", "PrivacyPolicyVersion", privacyVersion.Id.ToString(), null, new { privacyVersion.Id, privacyVersion.VersionNumber, privacyVersion.EffectiveFrom, privacyVersion.PublishedByUserId });
		base.TempData["Success"] = $"גרסה {nextVersion} של מדיניות הפרטיות פורסמה.";
		return RedirectToAction("PrivacyPolicy");
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> NotificationLogs(string? type, string? status, string? templateType, DateTime? fromDate, DateTime? toDate, string? recipientEmail, int? userId, int page = 1, int pageSize = 50)
	{
		string type2 = type;
		string status2 = status;
		string templateType2 = templateType;
		string recipientEmail2 = recipientEmail;
		if (pageSize <= 0 || pageSize > 500)
		{
			pageSize = 50;
		}
		if (page <= 0)
		{
			page = 1;
		}
		IQueryable<NotificationLog> query = _db.NotificationLogs.AsNoTracking().AsQueryable();
		if (!string.IsNullOrWhiteSpace(type2))
		{
			query = query.Where((NotificationLog n) => n.NotificationType == type2);
		}
		if (!string.IsNullOrWhiteSpace(status2))
		{
			query = query.Where((NotificationLog n) => n.Status == status2);
		}
		if (!string.IsNullOrWhiteSpace(templateType2))
		{
			query = query.Where((NotificationLog n) => n.TemplateType == templateType2);
		}
		if (fromDate.HasValue)
		{
			query = query.Where((NotificationLog n) => n.CreatedAt >= ((DateTime?)fromDate).Value.Date);
		}
		if (toDate.HasValue)
		{
			DateTime end = toDate.Value.Date.AddDays(1.0);
			query = query.Where((NotificationLog n) => n.CreatedAt < end);
		}
		if (!string.IsNullOrWhiteSpace(recipientEmail2))
		{
			query = query.Where((NotificationLog n) => n.RecipientEmail.Contains(recipientEmail2));
		}
		if (userId.HasValue)
		{
			query = query.Where((NotificationLog n) => n.RecipientUserId == (int?)((int?)userId).Value);
		}
		int total = await query.CountAsync();
		List<NotificationLogListItem> items = await (from n in query.OrderByDescending((NotificationLog n) => n.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize)
			select new NotificationLogListItem
			{
				Id = n.Id,
				CreatedAt = n.CreatedAt,
				NotificationType = n.NotificationType,
				TemplateType = n.TemplateType,
				RecipientEmail = n.RecipientEmail,
				RecipientName = ((n.RecipientUser != null) ? string.Concat(n.RecipientUser.FirstName + " ", n.RecipientUser.LastName) : null),
				Subject = n.Subject,
				Status = n.Status,
				AttemptCount = n.AttemptCount,
				LastAttemptAt = n.LastAttemptAt,
				NextRetryAt = n.NextRetryAt,
				FailureReason = n.FailureReason
			}).ToListAsync();
		NotificationLogListViewModel model = new NotificationLogListViewModel
		{
			Items = items,
			Page = page,
			PageSize = pageSize,
			TotalCount = total,
			Type = type2,
			Status = status2,
			TemplateType = templateType2,
			FromDate = fromDate,
			ToDate = toDate,
			RecipientEmail = recipientEmail2,
			UserId = userId
		};
		return View(model);
	}

	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> NotificationLogDetails(int id)
	{
		NotificationLog notificationLog = await _db.NotificationLogs.AsNoTracking().Include((NotificationLog n) => n.RecipientUser).FirstOrDefaultAsync((NotificationLog n) => n.Id == id);
		if (notificationLog == null)
		{
			return NotFound();
		}
		return PartialView("_NotificationLogDetails", notificationLog);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ResendNotification(int id)
	{
		NotificationLog log = await _db.NotificationLogs.FirstOrDefaultAsync((NotificationLog n) => n.Id == id);
		if (log == null)
		{
			base.TempData["Error"] = "התראה לא נמצאה";
			return RedirectToAction("NotificationLogs");
		}
		log.Status = "Pending";
		log.AttemptCount = 0;
		log.FailureReason = null;
		log.NextRetryAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		base.TempData["Success"] = $"התראה מס' {log.Id} הוחזרה לתור לשליחה";
		return RedirectToAction("NotificationLogs");
	}

	[HttpGet]
	[Route("Admin/AuditLog")]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> AuditLog([FromQuery(Name = "action")] string? actionFilter, string? entityType, string? entityId, int? actorUserId, DateTime? fromDate, DateTime? toDate, int page = 1, int pageSize = 50)
	{
		bool flag = ((pageSize < 1 || pageSize > 500) ? true : false);
		pageSize = (flag ? 50 : pageSize);
		page = Math.Max(page, 1);
		IQueryable<AuditLog> query = BuildAuditLogQuery(actionFilter, entityType, entityId, actorUserId, fromDate, toDate);
		int totalCount = await query.CountAsync();
		List<AuditLogListItem> items = await (from a in query.OrderByDescending((AuditLog a) => a.Timestamp).Skip((page - 1) * pageSize).Take(pageSize)
			select new AuditLogListItem
			{
				Id = a.Id,
				Timestamp = a.Timestamp,
				ActorUserId = a.ActorUserId,
				ActorName = ((a.ActorUser == null) ? null : string.Concat(a.ActorUser.FirstName + " ", a.ActorUser.LastName).Trim()),
				Action = a.Action,
				EntityType = a.EntityType,
				EntityId = a.EntityId,
				Notes = a.Notes,
				IpAddress = a.IpAddress,
				UserAgent = a.UserAgent,
				Before = a.Before,
				After = a.After
			}).ToListAsync();
		AuditLogListViewModel model = new AuditLogListViewModel
		{
			Items = items,
			Page = page,
			PageSize = pageSize,
			TotalCount = totalCount,
			Action = actionFilter,
			EntityType = entityType,
			EntityId = entityId,
			ActorUserId = actorUserId,
			FromDate = fromDate,
			ToDate = toDate
		};
		return View(model);
	}

	[HttpGet]
	[Route("Admin/AuditLog/Export")]
	[Authorize(Policy = "AdminOnly")]
	public async Task<IActionResult> ExportAuditLog([FromQuery(Name = "action")] string? actionFilter, string? entityType, string? entityId, int? actorUserId, DateTime? fromDate, DateTime? toDate)
	{
		var list = await (from a in (from a in BuildAuditLogQuery(actionFilter, entityType, entityId, actorUserId, fromDate, toDate)
				orderby a.Timestamp descending
				select a).Take(50000)
			select new
			{
				Id = a.Id,
				Timestamp = a.Timestamp,
				ActorUserId = a.ActorUserId,
				ActorName = ((a.ActorUser == null) ? null : string.Concat(a.ActorUser.FirstName + " ", a.ActorUser.LastName).Trim()),
				Action = a.Action,
				EntityType = a.EntityType,
				EntityId = a.EntityId,
				Notes = a.Notes,
				IpAddress = a.IpAddress,
				UserAgent = a.UserAgent,
				Before = a.Before,
				After = a.After
			}).ToListAsync();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append('\ufeff');
		stringBuilder.AppendLine("Id,Timestamp,ActorUserId,ActorName,Action,EntityType,EntityId,Notes,IpAddress,UserAgent,Before,After");
		foreach (var item in list)
		{
			stringBuilder.Append(item.Id).Append(',').Append(item.Timestamp.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
				.Append(',')
				.Append(item.ActorUserId?.ToString() ?? string.Empty)
				.Append(',')
				.Append(CsvEscape(item.ActorName))
				.Append(',')
				.Append(CsvEscape(item.Action))
				.Append(',')
				.Append(CsvEscape(item.EntityType))
				.Append(',')
				.Append(CsvEscape(item.EntityId))
				.Append(',')
				.Append(CsvEscape(item.Notes))
				.Append(',')
				.Append(CsvEscape(item.IpAddress))
				.Append(',')
				.Append(CsvEscape(item.UserAgent))
				.Append(',')
				.Append(CsvEscape(item.Before))
				.Append(',')
				.Append(CsvEscape(item.After))
				.Append('\n');
		}
		byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
		string fileDownloadName = $"audit-log-{DateTime.UtcNow:yyyyMMdd}.csv";
		return File(bytes, "text/csv; charset=utf-8", fileDownloadName);
	}

	private IQueryable<AuditLog> BuildAuditLogQuery(string? action, string? entityType, string? entityId, int? actorUserId, DateTime? fromDate, DateTime? toDate)
	{
		string action2 = action;
		string entityType2 = entityType;
		string entityId2 = entityId;
		IQueryable<AuditLog> queryable = _db.AuditLogs.Include((AuditLog a) => a.ActorUser).AsNoTracking().AsQueryable();
		if (!string.IsNullOrWhiteSpace(action2))
		{
			queryable = queryable.Where((AuditLog a) => a.Action.Contains(action2));
		}
		if (!string.IsNullOrWhiteSpace(entityType2))
		{
			queryable = queryable.Where((AuditLog a) => a.EntityType == entityType2);
		}
		if (!string.IsNullOrWhiteSpace(entityId2))
		{
			queryable = queryable.Where((AuditLog a) => a.EntityId == entityId2);
		}
		if (actorUserId.HasValue)
		{
			queryable = queryable.Where((AuditLog a) => a.ActorUserId == actorUserId);
		}
		if (fromDate.HasValue)
		{
			queryable = queryable.Where((AuditLog a) => a.Timestamp >= fromDate);
		}
		if (toDate.HasValue)
		{
			queryable = queryable.Where((AuditLog a) => a.Timestamp <= toDate);
		}
		return queryable;
	}

	private static string CsvEscape(string? value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return string.Empty;
		}
		bool flag = value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r');
		string text = value.Replace("\"", "\"\"");
		if (!flag)
		{
			return text;
		}
		return "\"" + text + "\"";
	}

	private async Task LoadInstitutionDropdowns()
	{
		if (_003C_003Eo__92._003C_003Ep__0 == null)
		{
			_003C_003Eo__92._003C_003Ep__0 = CallSite<Func<CallSite, object, List<Locality>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Localities", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Locality>, object> target = _003C_003Eo__92._003C_003Ep__0.Target;
		CallSite<Func<CallSite, object, List<Locality>, object>> _003C_003Ep__ = _003C_003Eo__92._003C_003Ep__0;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, await (from l in _db.Localities
			where l.IsActive
			orderby l.Description
			select l).ToListAsync());
		if (_003C_003Eo__92._003C_003Ep__1 == null)
		{
			_003C_003Eo__92._003C_003Ep__1 = CallSite<Func<CallSite, object, List<District>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Districts", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<District>, object> target2 = _003C_003Eo__92._003C_003Ep__1.Target;
		CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__2 = _003C_003Eo__92._003C_003Ep__1;
		viewBag = base.ViewBag;
		target2(_003C_003Ep__2, viewBag, await (from d in _db.Districts
			where d.IsActive
			orderby d.Description
			select d).ToListAsync());
		if (_003C_003Eo__92._003C_003Ep__2 == null)
		{
			_003C_003Eo__92._003C_003Ep__2 = CallSite<Func<CallSite, object, List<Sector>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sectors", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Sector>, object> target3 = _003C_003Eo__92._003C_003Ep__2.Target;
		CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__3 = _003C_003Eo__92._003C_003Ep__2;
		viewBag = base.ViewBag;
		target3(_003C_003Ep__3, viewBag, await (from s in _db.Sectors
			where s.IsActive
			orderby s.Description
			select s).ToListAsync());
		if (_003C_003Eo__92._003C_003Ep__3 == null)
		{
			_003C_003Eo__92._003C_003Ep__3 = CallSite<Func<CallSite, object, List<EducationType>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EducationTypes", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<EducationType>, object> target4 = _003C_003Eo__92._003C_003Ep__3.Target;
		CallSite<Func<CallSite, object, List<EducationType>, object>> _003C_003Ep__4 = _003C_003Eo__92._003C_003Ep__3;
		viewBag = base.ViewBag;
		target4(_003C_003Ep__4, viewBag, await (from t in _db.EducationTypes
			where t.IsActive
			orderby t.Description
			select t).ToListAsync());
		if (_003C_003Eo__92._003C_003Ep__4 == null)
		{
			_003C_003Eo__92._003C_003Ep__4 = CallSite<Func<CallSite, object, List<EducationalStage>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EducationalStages", typeof(AdminController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<EducationalStage>, object> target5 = _003C_003Eo__92._003C_003Ep__4.Target;
		CallSite<Func<CallSite, object, List<EducationalStage>, object>> _003C_003Ep__5 = _003C_003Eo__92._003C_003Ep__4;
		viewBag = base.ViewBag;
		target5(_003C_003Ep__5, viewBag, await (from s in _db.EducationalStages
			where s.IsActive
			orderby s.Description
			select s).ToListAsync());
	}

	private async Task<Dictionary<string, HashSet<int>>> LoadProjectProgramScopeMappingAsync()
	{
		Dictionary<string, HashSet<int>> result = new Dictionary<string, HashSet<int>>(StringComparer.OrdinalIgnoreCase);
		DbConnection connection = _db.Database.GetDbConnection();
		bool shouldClose = connection.State == ConnectionState.Closed;
		if (shouldClose)
		{
			await connection.OpenAsync();
		}
		try
		{
			foreach (ScopeDefinition scope in ProjectProgramScopeDefinitions)
			{
				using DbCommand command = connection.CreateCommand();
				command.CommandText = $"SELECT ProjectId, ProgramId, {scope.ColumnName} FROM dbo.{scope.TableName}";
				using DbDataReader reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					string key = ScopeKey(scope.Key, reader.GetInt32(0), reader.GetInt32(1));
					if (!result.TryGetValue(key, out HashSet<int>? ids))
					{
						ids = new HashSet<int>();
						result[key] = ids;
					}
					ids.Add(reader.GetInt32(2));
				}
			}
		}
		finally
		{
			if (shouldClose)
			{
				await connection.CloseAsync();
			}
		}
		return result;
	}

	private async Task ReplaceProjectProgramScopeAsync(int projectId, int programId, ScopeDefinition scope, int[]? selectedIds)
	{
		List<int> ids = (selectedIds ?? Array.Empty<int>()).Where((int id) => id > 0).Distinct().ToList();
		DbConnection connection = _db.Database.GetDbConnection();
		bool shouldClose = connection.State == ConnectionState.Closed;
		if (shouldClose)
		{
			await connection.OpenAsync();
		}
		try
		{
			using (DbCommand delete = connection.CreateCommand())
			{
				delete.CommandText = $"DELETE FROM dbo.{scope.TableName} WHERE ProjectId = @projectId AND ProgramId = @programId";
				AddParameter(delete, "@projectId", projectId);
				AddParameter(delete, "@programId", programId);
				await delete.ExecuteNonQueryAsync();
			}
			if (ids.Count == 0)
			{
				return;
			}
			string values = string.Join(",", ids);
			using DbCommand insert = connection.CreateCommand();
			insert.CommandText = $"INSERT INTO dbo.{scope.TableName} (ProjectId, ProgramId, {scope.ColumnName}) SELECT @projectId, @programId, l.Id FROM dbo.{scope.LookupTable} l WHERE l.IsActive = 1 AND l.Id IN ({values})";
			AddParameter(insert, "@projectId", projectId);
			AddParameter(insert, "@programId", programId);
			await insert.ExecuteNonQueryAsync();
		}
		finally
		{
			if (shouldClose)
			{
				await connection.CloseAsync();
			}
		}
	}

	private async Task DeleteAllProjectProgramScopeRowsAsync(int projectId, int programId)
	{
		foreach (ScopeDefinition scope in ProjectProgramScopeDefinitions)
		{
			await ReplaceProjectProgramScopeAsync(projectId, programId, scope, Array.Empty<int>());
		}
	}

	private async Task BackfillProjectProgramScopeRowsAsync(int projectId, int programId)
	{
		DbConnection connection = _db.Database.GetDbConnection();
		bool shouldClose = connection.State == ConnectionState.Closed;
		if (shouldClose)
		{
			await connection.OpenAsync();
		}
		try
		{
			foreach (ScopeDefinition scope in ProjectProgramScopeDefinitions)
			{
				using DbCommand command = connection.CreateCommand();
				command.CommandText = $"INSERT INTO dbo.{scope.TableName} (ProjectId, ProgramId, {scope.ColumnName}) SELECT @projectId, @programId, l.Id FROM dbo.{scope.LookupTable} l WHERE l.IsActive = 1 AND NOT EXISTS (SELECT 1 FROM dbo.{scope.TableName} s WHERE s.ProjectId = @projectId AND s.ProgramId = @programId AND s.{scope.ColumnName} = l.Id)";
				AddParameter(command, "@projectId", projectId);
				AddParameter(command, "@programId", programId);
				await command.ExecuteNonQueryAsync();
			}
		}
		finally
		{
			if (shouldClose)
			{
				await connection.CloseAsync();
			}
		}
	}

	private static void AddParameter(DbCommand command, string name, object value)
	{
		DbParameter parameter = command.CreateParameter();
		parameter.ParameterName = name;
		parameter.Value = value;
		command.Parameters.Add(parameter);
	}

	private static string ScopeKey(string scope, int projectId, int programId)
	{
		return $"{scope}:{projectId}:{programId}";
	}
}

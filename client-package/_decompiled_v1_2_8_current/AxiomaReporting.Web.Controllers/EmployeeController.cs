using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Infrastructure.Validators;
using AxiomaReporting.Web.Helpers;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Policy = "AdminPMOrCoordinator")]
public class EmployeeController : Controller
{
	[CompilerGenerated]
	private static class _003C_003Eo__12
	{
		public static CallSite<Func<CallSite, object, EmployeeListFilterModel, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__1;

		public static CallSite<Func<CallSite, object, int?, object>> _003C_003Ep__2;

		public static CallSite<Func<CallSite, object, int?, object>> _003C_003Ep__3;

		public static CallSite<Func<CallSite, object, int?, object>> _003C_003Ep__4;

		public static CallSite<Func<CallSite, object, string, object>> _003C_003Ep__5;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__6;

		public static CallSite<Func<CallSite, object, int, object>> _003C_003Ep__7;

		public static CallSite<Func<CallSite, object, int, object>> _003C_003Ep__8;

		public static CallSite<Func<CallSite, object, int, object>> _003C_003Ep__9;

		public static CallSite<Func<CallSite, object, List<UserStatus>, object>> _003C_003Ep__10;

		public static CallSite<Func<CallSite, object, List<UserRole>, object>> _003C_003Ep__11;

		public static CallSite<Func<CallSite, object, List<Project>, object>> _003C_003Ep__12;

		public static CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__13;

		public static CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__14;

		public static CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__15;
	}

	[CompilerGenerated]
	private static class _003C_003Eo__16
	{
		public static CallSite<Func<CallSite, object, List<DocumentAttachment>, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__1;
	}

	[CompilerGenerated]
	private static class _003C_003Eo__35
	{
		public static CallSite<Func<CallSite, object, SelectList, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, SelectList, object>> _003C_003Ep__1;

		public static CallSite<Func<CallSite, object, SelectList, object>> _003C_003Ep__2;

		public static CallSite<Func<CallSite, object, List<SelectListItem>, object>> _003C_003Ep__3;

		public static CallSite<Func<CallSite, object, bool, object>> _003C_003Ep__4;
	}

	[CompilerGenerated]
	private static class _003C_003Eo__36
	{
		public static CallSite<Func<CallSite, object, SelectList, object>> _003C_003Ep__0;

		public static CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__1;

		public static CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__2;

		public static CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__3;

		public static CallSite<Func<CallSite, object, List<Locality>, object>> _003C_003Ep__4;

		public static CallSite<Func<CallSite, object, List<Framework>, object>> _003C_003Ep__5;

		public static CallSite<Func<CallSite, object, List<Subject>, object>> _003C_003Ep__6;

		public static CallSite<Func<CallSite, object, List<Domain>, object>> _003C_003Ep__7;

		public static CallSite<Func<CallSite, object, List<EducationalProgram>, object>> _003C_003Ep__8;

		public static CallSite<Func<CallSite, object, List<SchoolClass>, object>> _003C_003Ep__9;

		public static CallSite<Func<CallSite, object, List<GradeLevel>, object>> _003C_003Ep__10;

		public static CallSite<Func<CallSite, object, List<DiscussionCode>, object>> _003C_003Ep__11;

		public static CallSite<Func<CallSite, object, List<LocalityDistrictNational>, object>> _003C_003Ep__12;
	}

	private static readonly HashSet<string> AllowedAttachmentExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx", ".xls", ".xlsx" };

	private const long MaxAttachmentBytes = 10485760L;

	private const int MaxAttachmentDescriptionLength = 1000;

	private readonly IEmployeeService _employeeService;

	private readonly ICurrentUserService _currentUser;

	private readonly IPasswordService _passwordService;

	private readonly IReportStatusService _reportStatusService;

	private readonly IReportExcelImportService _excelImportService;

	private readonly IAuditLogService _auditLog;

	private readonly AppDbContext _db;

	private static readonly decimal[] OUTPUT_DURATION_OPTIONS = new decimal[6] { 0.5m, 1m, 1.5m, 2m, 2.5m, 3m };

	public EmployeeController(IEmployeeService employeeService, ICurrentUserService currentUser, IPasswordService passwordService, IReportStatusService reportStatusService, IReportExcelImportService excelImportService, IAuditLogService auditLog, AppDbContext db)
	{
		_employeeService = employeeService;
		_currentUser = currentUser;
		_passwordService = passwordService;
		_reportStatusService = reportStatusService;
		_excelImportService = excelImportService;
		_auditLog = auditLog;
		_db = db;
	}

	[HttpGet]
	public async Task<IActionResult> Index(EmployeeListFilterModel filter)
	{
		filter.Normalize();
		List<User> employees = ApplyEmployeeFilters(await _employeeService.GetAllAsync(filter.Search, filter.StatusId, filter.RoleId), filter);
		employees = ApplyEmployeeSort(employees, filter.SortBy, filter.SortDesc);
		int count = employees.Count;
		int num = Math.Max(filter.Page, 1);
		int pageSize = filter.PageSize;
		bool flag = ((pageSize < 1 || pageSize > 500) ? true : false);
		int num2 = (flag ? 25 : filter.PageSize);
		List<User> pagedEmployees = employees.Skip((num - 1) * num2).Take(num2).ToList();
		base.ViewBag.Filter = filter;
		base.ViewBag.Search = filter.Search;
		base.ViewBag.StatusId = filter.StatusId;
		base.ViewBag.RoleId = filter.RoleId;
		base.ViewBag.ProjectId = filter.ProjectId;
		base.ViewBag.SortBy = filter.SortBy;
		base.ViewBag.SortDesc = filter.SortDesc;
		base.ViewBag.Page = num;
		base.ViewBag.PageSize = num2;
		base.ViewBag.TotalPages = (int)Math.Ceiling((double)count / (double)num2);
		if (_003C_003Eo__12._003C_003Ep__10 == null)
		{
			_003C_003Eo__12._003C_003Ep__10 = CallSite<Func<CallSite, object, List<UserStatus>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Statuses", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<UserStatus>, object> target = _003C_003Eo__12._003C_003Ep__10.Target;
		CallSite<Func<CallSite, object, List<UserStatus>, object>> _003C_003Ep__ = _003C_003Eo__12._003C_003Ep__10;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, await _db.UserStatuses.ToListAsync());
		if (_003C_003Eo__12._003C_003Ep__11 == null)
		{
			_003C_003Eo__12._003C_003Ep__11 = CallSite<Func<CallSite, object, List<UserRole>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserRoles", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<UserRole>, object> target2 = _003C_003Eo__12._003C_003Ep__11.Target;
		CallSite<Func<CallSite, object, List<UserRole>, object>> _003C_003Ep__2 = _003C_003Eo__12._003C_003Ep__11;
		viewBag = base.ViewBag;
		target2(_003C_003Ep__2, viewBag, await _db.UserRoles.ToListAsync());
		if (_003C_003Eo__12._003C_003Ep__12 == null)
		{
			_003C_003Eo__12._003C_003Ep__12 = CallSite<Func<CallSite, object, List<Project>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Projects", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Project>, object> target3 = _003C_003Eo__12._003C_003Ep__12.Target;
		CallSite<Func<CallSite, object, List<Project>, object>> _003C_003Ep__3 = _003C_003Eo__12._003C_003Ep__12;
		viewBag = base.ViewBag;
		target3(_003C_003Ep__3, viewBag, await (from p in _db.Projects
			where p.IsActive
			orderby p.Description
			select p).ToListAsync());
		if (_003C_003Eo__12._003C_003Ep__13 == null)
		{
			_003C_003Eo__12._003C_003Ep__13 = CallSite<Func<CallSite, object, List<District>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AllDistricts", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<District>, object> target4 = _003C_003Eo__12._003C_003Ep__13.Target;
		CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__4 = _003C_003Eo__12._003C_003Ep__13;
		viewBag = base.ViewBag;
		target4(_003C_003Ep__4, viewBag, await (from d in _db.Districts
			where d.IsActive
			orderby d.Description
			select d).ToListAsync());
		if (_003C_003Eo__12._003C_003Ep__14 == null)
		{
			_003C_003Eo__12._003C_003Ep__14 = CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AllPrograms", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object> target5 = _003C_003Eo__12._003C_003Ep__14.Target;
		CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__5 = _003C_003Eo__12._003C_003Ep__14;
		viewBag = base.ViewBag;
		target5(_003C_003Ep__5, viewBag, await (from p in _db.Programs
			where p.IsActive
			orderby p.Description
			select p).ToListAsync());
		if (_003C_003Eo__12._003C_003Ep__15 == null)
		{
			_003C_003Eo__12._003C_003Ep__15 = CallSite<Func<CallSite, object, List<Sector>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AllSectors", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Sector>, object> target6 = _003C_003Eo__12._003C_003Ep__15.Target;
		CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__6 = _003C_003Eo__12._003C_003Ep__15;
		viewBag = base.ViewBag;
		target6(_003C_003Ep__6, viewBag, await (from s in _db.Sectors
			where s.IsActive
			orderby s.Description
			select s).ToListAsync());
		return View(pagedEmployees);
	}

	internal static List<User> ApplyEmployeeFilters(List<User> source, EmployeeListFilterModel filter)
	{
		EmployeeListFilterModel filter2 = filter;
		IEnumerable<User> source2 = source;
		if (!string.IsNullOrWhiteSpace(filter2.IdNumber))
		{
			source2 = source2.Where((User u) => u.IdNumber.Contains(filter2.IdNumber, StringComparison.OrdinalIgnoreCase));
		}
		if (!string.IsNullOrWhiteSpace(filter2.EmployeeCode))
		{
			source2 = source2.Where((User u) => u.EmployeeCode.Contains(filter2.EmployeeCode, StringComparison.OrdinalIgnoreCase));
		}
		if (!string.IsNullOrWhiteSpace(filter2.FirstName))
		{
			source2 = source2.Where((User u) => u.FirstName.Contains(filter2.FirstName, StringComparison.OrdinalIgnoreCase));
		}
		if (!string.IsNullOrWhiteSpace(filter2.LastName))
		{
			source2 = source2.Where((User u) => u.LastName.Contains(filter2.LastName, StringComparison.OrdinalIgnoreCase));
		}
		if (!string.IsNullOrWhiteSpace(filter2.Notes))
		{
			source2 = source2.Where((User u) => !string.IsNullOrWhiteSpace(u.Notes) && u.Notes.Contains(filter2.Notes, StringComparison.OrdinalIgnoreCase));
		}
		if (filter2.RestDay.HasValue)
		{
			source2 = source2.Where((User u) => u.RestDay == filter2.RestDay.Value);
		}
		if (filter2.AllowFutureReporting.HasValue)
		{
			source2 = source2.Where((User u) => u.AllowFutureReporting == filter2.AllowFutureReporting.Value);
		}
		if (filter2.LockedOnly)
		{
			source2 = source2.Where((User u) => u.StatusId == 3 || u.FailedLoginAttempts >= 3);
		}
		if (filter2.HasAllocations.HasValue)
		{
			bool has = filter2.HasAllocations.Value;
			source2 = source2.Where((User u) => u.Allocations.Any((Allocation a) => a.IsActive) == has);
		}
		if (filter2.ProjectId.HasValue)
		{
			source2 = source2.Where((User u) => u.Allocations.Any((Allocation a) => a.IsActive && a.ProjectId == filter2.ProjectId.Value));
		}
		List<int> districtIds = filter2.DistrictIds;
		if (districtIds != null && districtIds.Count > 0)
		{
			source2 = source2.Where((User u) => u.Allocations.Any((Allocation a) => a.IsActive && a.AllocationDistricts.Any((AllocationDistrict ad) => filter2.DistrictIds.Contains(ad.DistrictId))));
		}
		districtIds = filter2.ProgramIds;
		if (districtIds != null && districtIds.Count > 0)
		{
			source2 = source2.Where((User u) => u.Allocations.Any((Allocation a) => a.IsActive && a.AllocationPrograms.Any((AllocationProgram ap) => filter2.ProgramIds.Contains(ap.ProgramId))));
		}
		districtIds = filter2.SectorIds;
		if (districtIds != null && districtIds.Count > 0)
		{
			source2 = source2.Where((User u) => u.Allocations.Any((Allocation a) => a.IsActive && a.AllocationSectors.Any((AllocationSector asec) => filter2.SectorIds.Contains(asec.SectorId))));
		}
		return source2.ToList();
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		await PopulateFormDropdownsAsync();
		base.ViewBag.IsEdit = false;
		return View("Form", new EmployeeDto());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(EmployeeDto dto)
	{
		NormalizeRequestedUserRole(dto);
		AddValidationErrors(new UserValidator().Validate(dto));
		if (!base.ModelState.IsValid)
		{
			await PopulateFormDropdownsAsync();
			base.ViewBag.IsEdit = false;
			return View("Form", dto);
		}
		await _employeeService.CreateAsync(dto, _currentUser.IdNumber);
		base.TempData["Success"] = "העובד נוצר בהצלחה";
		return RedirectToAction("Index");
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		User user = await _employeeService.GetByIdAsync(id);
		if (user == null)
		{
			return NotFound();
		}
		EmployeeDto dto = new EmployeeDto
		{
			Id = user.Id,
			EmployeeCode = user.EmployeeCode,
			FirstName = user.FirstName,
			LastName = user.LastName,
			IdNumber = user.IdNumber,
			RoleId = user.RoleId,
			UserRoleId = user.UserRoleId,
			StatusId = user.StatusId,
			IsReportingEmployee = user.IsReportingEmployee,
			RestDay = user.RestDay,
			AllowFutureReporting = user.AllowFutureReporting,
			Notes = user.Notes,
			Email = user.Email,
			Phone = user.Phone
		};
		await PopulateFormDropdownsAsync();
		if (_003C_003Eo__16._003C_003Ep__0 == null)
		{
			_003C_003Eo__16._003C_003Ep__0 = CallSite<Func<CallSite, object, List<DocumentAttachment>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Attachments", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<DocumentAttachment>, object> target = _003C_003Eo__16._003C_003Ep__0.Target;
		CallSite<Func<CallSite, object, List<DocumentAttachment>, object>> _003C_003Ep__ = _003C_003Eo__16._003C_003Ep__0;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, await (from a in _db.DocumentAttachments
			where a.UserId == (int?)id
			orderby a.UploadedAt descending
			select a).ToListAsync());
		base.ViewBag.IsEdit = true;
		return View("Form", dto);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, EmployeeDto dto)
	{
		NormalizeRequestedUserRole(dto);
		var anon = await (from u in _db.Users.AsNoTracking()
			where u.Id == id
			select new { u.EmployeeCode, u.IdNumber, u.Phone }).FirstOrDefaultAsync();
		if (anon == null)
		{
			return NotFound();
		}
		FluentValidation.Results.ValidationResult validationResult = new UserValidator().Validate(dto);
		if (string.Equals(dto.EmployeeCode, anon.EmployeeCode, StringComparison.Ordinal))
		{
			base.ModelState.Remove("EmployeeCode");
			validationResult.Errors.RemoveAll((ValidationFailure e) => e.PropertyName == "EmployeeCode");
		}
		if (string.Equals(dto.IdNumber, anon.IdNumber, StringComparison.Ordinal))
		{
			validationResult.Errors.RemoveAll((ValidationFailure e) => e.PropertyName == "IdNumber");
		}
		if (string.Equals(dto.Phone, anon.Phone, StringComparison.Ordinal))
		{
			validationResult.Errors.RemoveAll((ValidationFailure e) => e.PropertyName == "Phone");
		}
		AddValidationErrors(validationResult);
		if (!base.ModelState.IsValid)
		{
			await PopulateFormDropdownsAsync();
			base.ViewBag.IsEdit = true;
			return View("Form", dto);
		}
		if (!(await _employeeService.UpdateAsync(id, dto)))
		{
			return NotFound();
		}
		base.TempData["Success"] = "פרטי העובד עודכנו בהצלחה";
		return RedirectToAction("Index");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminPMOrCoordinator")]
	public async Task<IActionResult> ResetPassword(int id)
	{
		User user = await _db.Users.FindAsync(id);
		if (user == null)
		{
			return NotFound();
		}
		if (_currentUser.UserRole == UserRoleEnum.ProjectCoordinator && user.UserRoleId <= 2)
		{
			return Forbid();
		}
		string passwordHash = user.PasswordHash;
		DateTime utcNow = DateTime.UtcNow;
		if (!string.IsNullOrEmpty(passwordHash))
		{
			_db.PasswordHistories.Add(new PasswordHistory
			{
				UserId = user.Id,
				PasswordHash = passwordHash,
				CreatedAt = utcNow
			});
		}
		user.PasswordHash = _passwordService.HashPassword(user.IdNumber);
		user.MustChangePassword = true;
		user.FailedLoginAttempts = 0;
		user.LastPasswordChange = utcNow;
		if (user.StatusId == 3)
		{
			user.StatusId = 1;
		}
		user.UpdatedAt = utcNow;
		await _db.SaveChangesAsync();
		string text = base.User.Identity?.Name ?? _currentUser.UserId.ToString();
		await _auditLog.LogAsync("User.PasswordReset", "User", id.ToString(), null, null, "reset-by=" + text);
		base.TempData["Success"] = $"הסיסמה של {user.FirstName} {user.LastName} אופסה למספר הזהות";
		return RedirectToFilteredIndex();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminPMOrCoordinator")]
	public async Task<IActionResult> UnlockAccount(int id)
	{
		User user = await _db.Users.FindAsync(id);
		if (user == null)
		{
			return NotFound();
		}
		if (_currentUser.UserRole == UserRoleEnum.ProjectCoordinator && user.UserRoleId <= 2)
		{
			return Forbid();
		}
		bool wasLocked = user.StatusId == 3;
		user.FailedLoginAttempts = 0;
		if (user.StatusId == 3)
		{
			user.StatusId = 1;
		}
		user.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		if (wasLocked)
		{
			await _auditLog.LogAsync("Auth.Unlock", "User", id.ToString(), null, null, $"unlocked by user {_currentUser.UserId}");
		}
		base.TempData["Success"] = $"החשבון של {user.FirstName} {user.LastName} שוחרר מנעילה";
		return RedirectToFilteredIndex();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminPMOrCoordinator")]
	public async Task<IActionResult> UploadAttachment(int id, IFormFile file, string? description)
	{
		if (await _employeeService.GetByIdAsync(id) == null)
		{
			return NotFound();
		}
		if (file == null || file.Length == 0L)
		{
			base.TempData["Error"] = "לא נבחר קובץ";
			return RedirectToAction("Edit", new { id });
		}
		if (file.Length > 10485760)
		{
			base.TempData["Error"] = "גודל הקובץ חורג מהמותר";
			return RedirectToAction("Edit", new { id });
		}
		string extension = Path.GetExtension(file.FileName);
		if (!AllowedAttachmentExtensions.Contains(extension))
		{
			base.TempData["Error"] = "סוג הקובץ אינו נתמך";
			return RedirectToAction("Edit", new { id });
		}
		string text = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "employees");
		Directory.CreateDirectory(text);
		string storedFileName = $"{Guid.NewGuid()}{extension}";
		string path = Path.Combine(text, storedFileName);
		string attachmentDescription = (string.IsNullOrWhiteSpace(description) ? null : description.Trim());
		string text2 = attachmentDescription;
		if (text2 != null && text2.Length > 1000)
		{
			attachmentDescription = attachmentDescription.Substring(0, 1000);
		}
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
		_db.DocumentAttachments.Add(new DocumentAttachment
		{
			UserId = id,
			FileName = Path.GetFileName(file.FileName),
			Description = attachmentDescription,
			FilePath = "/uploads/employees/" + storedFileName,
			FileSize = file.Length,
			MimeType = file.ContentType,
			UploadedAt = DateTime.UtcNow,
			UploadedBy = _currentUser.UserId
		});
		await _db.SaveChangesAsync();
		base.TempData["Success"] = "המסמך נוסף בהצלחה";
		return RedirectToAction("Edit", new { id });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminPMOrCoordinator")]
	public async Task<IActionResult> DeleteAttachment(int id, int attachmentId)
	{
		DocumentAttachment documentAttachment = await _db.DocumentAttachments.FirstOrDefaultAsync((DocumentAttachment a) => a.Id == attachmentId && a.UserId == (int?)id);
		if (documentAttachment == null)
		{
			return NotFound();
		}
		string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", documentAttachment.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
		if (System.IO.File.Exists(path))
		{
			System.IO.File.Delete(path);
		}
		_db.DocumentAttachments.Remove(documentAttachment);
		await _db.SaveChangesAsync();
		base.TempData["Success"] = "המסמך נמחק";
		return RedirectToAction("Edit", new { id });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOrPM")]
	public async Task<IActionResult> DeleteEmployee(int id)
	{
		User user = await _db.Users.FindAsync(id);
		if (user == null)
		{
			return NotFound();
		}
		int previousStatusId = user.StatusId;
		user.StatusId = 2;
		user.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		await _auditLog.LogAsync("Employee.Deactivate", "User", id.ToString(), new
		{
			StatusId = previousStatusId
		}, new
		{
			StatusId = 2
		});
		base.TempData["Success"] = $"העובד {user.FirstName} {user.LastName} הושבת";
		return RedirectToFilteredIndex();
	}

	[HttpGet]
	[Route("Employee/{id}/Allocations")]
	public async Task<IActionResult> Allocations(int id)
	{
		User user = await _employeeService.GetByIdAsync(id);
		if (user == null)
		{
			return NotFound();
		}
		return RedirectToAction("Index", "Allocations", new
		{
			employeeId = user.Id,
			employeeCode = user.EmployeeCode,
			idNumber = user.IdNumber,
			showAll = true,
			pageSize = 500
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOrPM")]
	public async Task<IActionResult> UploadAllocationExcel(int id, int allocationId, int reportingMonthId, IFormFile file)
	{
		if (await _db.Allocations.FirstOrDefaultAsync((Allocation a) => a.Id == allocationId && a.UserId == id) == null)
		{
			return NotFound();
		}
		if (file == null || file.Length == 0L)
		{
			base.TempData["Error"] = "לא נבחר קובץ אקסל";
			return RedirectToAction("Allocations", new { id });
		}
		Report report = await _reportStatusService.GetOrCreateDraftAsync(id, reportingMonthId);
		if (report == null)
		{
			return StatusCode(500);
		}
		Stream stream = file.OpenReadStream();
		IActionResult result;
		try
		{
			ExcelImportResult excelImportResult = await _excelImportService.ImportAsync(report.Id, allocationId, stream, _currentUser.UserId);
			if (!excelImportResult.Success)
			{
				base.TempData["Error"] = string.Join(" | ", excelImportResult.Errors.Take(5));
				result = RedirectToAction("Allocations", new { id });
			}
			else
			{
				base.TempData["Success"] = $"יובאו {excelImportResult.ImportedRows} שורות לדיווח העובד";
				result = RedirectToAction("Allocations", new { id });
			}
		}
		finally
		{
			if (stream != null)
			{
				await stream.DisposeAsync();
			}
		}
		return result;
	}

	[HttpGet]
	[Route("Employee/AllocationReportTypes")]
	public async Task<IActionResult> AllocationReportTypes()
	{
		return Json(await _db.ReportTypes.Where((ReportType r) => r.IsActive).OrderBy((ReportType r) => r.Description).Select((ReportType r) => new
		{
			id = r.Id,
			text = r.Description
		}).ToListAsync());
	}

	[HttpGet]
	[Route("Employee/{id}/Allocations/Create")]
	public async Task<IActionResult> CreateAllocation(int id)
	{
		User user = await _employeeService.GetByIdAsync(id);
		if (user == null)
		{
			return NotFound();
		}
		await PopulateAllocationDropdownsAsync();
		base.ViewBag.Employee = user;
		base.ViewBag.IsEdit = false;
		base.ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;
		return View("AllocationForm", new AllocationDto
		{
			UserId = id
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Route("Employee/{id}/Allocations/Create")]
	public async Task<IActionResult> CreateAllocation(int id, AllocationDto dto, bool continueAdding = false)
	{
		dto.UserId = id;
		AddValidationErrors(new AllocationValidator().Validate(dto));
		await AddAllocationScopeValidationErrorsAsync(dto);
		if (!base.ModelState.IsValid)
		{
			User user = await _employeeService.GetByIdAsync(id);
			base.ViewBag.Employee = user;
			await PopulateAllocationDropdownsAsync(dto.ProjectId, dto.ProgramIds);
			base.ViewBag.IsEdit = false;
			base.ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;
			return View("AllocationForm", dto);
		}
		await _employeeService.CreateAllocationAsync(dto);
		base.TempData["Success"] = "ההקצאה נוצרה בהצלחה";
		if (continueAdding)
		{
			return RedirectToAction("CreateAllocation", new { id });
		}
		return RedirectToAction("Allocations", new { id });
	}

	[HttpGet]
	[Route("Employee/{id}/Allocations/{allocationId}/Edit")]
	public async Task<IActionResult> EditAllocation(int id, int allocationId)
	{
		User user = await _employeeService.GetByIdAsync(id);
		if (user == null)
		{
			return NotFound();
		}
		Allocation allocation = await _employeeService.GetAllocationByIdAsync(allocationId);
		if (allocation == null || allocation.UserId != id)
		{
			return NotFound();
		}
		decimal result;
		List<decimal> outputDurationValues = (from s in (allocation.OutputDuration ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries)
			select (!decimal.TryParse(s, out result)) ? null : new decimal?(result) into v
			where v.HasValue
			select v.Value).ToList();
		AllocationDto dto = new AllocationDto
		{
			Id = allocation.Id,
			UserId = id,
			ProjectId = allocation.ProjectId,
			ReportTypeId = allocation.ReportTypeId,
			AnnualEmploymentScope = allocation.AnnualEmploymentScope,
			MonthlyEmploymentScope = allocation.MonthlyEmploymentScope,
			DailyEmploymentScope = allocation.DailyEmploymentScope,
			MonthlyRowAllocation = allocation.MonthlyRowAllocation,
			AnnualRowAllocation = allocation.AnnualRowAllocation,
			OutputDuration = allocation.OutputDuration,
			OutputDurationValues = outputDurationValues,
			AllowExcelUpload = allocation.AllowExcelUpload,
			Notes = allocation.Notes,
			IsActive = allocation.IsActive,
			DistrictIds = allocation.AllocationDistricts.Select((AllocationDistrict x) => x.DistrictId).ToList(),
			ProgramIds = allocation.AllocationPrograms.Select((AllocationProgram x) => x.ProgramId).ToList(),
			SectorIds = allocation.AllocationSectors.Select((AllocationSector x) => x.SectorId).ToList(),
			LocalityIds = allocation.AllocationLocalities.Select((AllocationLocality x) => x.LocalityId).ToList(),
			FrameworkIds = allocation.AllocationFrameworks.Select((AllocationFramework x) => x.FrameworkId).ToList(),
			SubjectIds = allocation.AllocationSubjects.Select((AllocationSubject x) => x.SubjectId).ToList(),
			DomainIds = allocation.AllocationDomains.Select((AllocationDomain x) => x.DomainId).ToList(),
			EducationalProgramIds = allocation.AllocationEducationalPrograms.Select((AllocationEducationalProgram x) => x.EducationalProgramId).ToList(),
			ClassIds = allocation.AllocationClasses.Select((AllocationClass x) => x.ClassId).ToList(),
			GradeLevelIds = allocation.AllocationGradeLevels.Select((AllocationGradeLevel x) => x.GradeLevelId).ToList(),
			DiscussionCodeIds = allocation.AllocationDiscussionCodes.Select((AllocationDiscussionCode x) => x.DiscussionCodeId).ToList(),
			LocalityDistrictNationalIds = allocation.AllocationLocalityDistrictNationals.Select((AllocationLocalityDistrictNational x) => x.LocalityDistrictNationalId).ToList()
		};
		await PopulateAllocationDropdownsAsync(dto.ProjectId, dto.ProgramIds);
		base.ViewBag.Employee = user;
		base.ViewBag.IsEdit = true;
		base.ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;
		return View("AllocationForm", dto);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Route("Employee/{id}/Allocations/{allocationId}/Edit")]
	public async Task<IActionResult> EditAllocation(int id, int allocationId, AllocationDto dto)
	{
		dto.UserId = id;
		AddValidationErrors(new AllocationValidator().Validate(dto));
		await AddAllocationScopeValidationErrorsAsync(dto);
		if (!base.ModelState.IsValid)
		{
			User user = await _employeeService.GetByIdAsync(id);
			base.ViewBag.Employee = user;
			await PopulateAllocationDropdownsAsync(dto.ProjectId, dto.ProgramIds);
			base.ViewBag.IsEdit = true;
			base.ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;
			return View("AllocationForm", dto);
		}
		if (!(await _employeeService.UpdateAllocationAsync(allocationId, dto)))
		{
			return NotFound();
		}
		base.TempData["Success"] = "ההקצאה עודכנה בהצלחה";
		return RedirectToAction("EditAllocation", new { id, allocationId });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Route("Employee/{id}/Allocations/{allocationId}/Delete")]
	public async Task<IActionResult> DeleteAllocation(int id, int allocationId)
	{
		await _employeeService.DeleteAllocationAsync(allocationId);
		base.TempData["Success"] = "ההקצאה הושבתה בהצלחה";
		return RedirectToAction("Allocations", new { id });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOrPM")]
	public async Task<IActionResult> BulkAction(List<int> selectedIds, int newStatusId)
	{
		List<int> selectedIds2 = selectedIds;
		if (selectedIds2 == null || selectedIds2.Count == 0)
		{
			base.TempData["Error"] = "לא נבחרו עובדים";
			return RedirectToFilteredIndex();
		}
		List<User> users = await _db.Users.Where((User u) => selectedIds2.Contains(u.Id)).ToListAsync();
		foreach (User item in users)
		{
			item.StatusId = newStatusId;
			item.UpdatedAt = DateTime.UtcNow;
		}
		await _db.SaveChangesAsync();
		base.TempData["Success"] = $"עודכנו {users.Count} עובדים";
		return RedirectToFilteredIndex();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Policy = "AdminOrPM")]
	public async Task<IActionResult> BulkAddAllocation(List<int> selectedIds, int bulkProjectId)
	{
		if (selectedIds == null || selectedIds.Count == 0)
		{
			base.TempData["Error"] = "לא נבחרו עובדים";
			return RedirectToFilteredIndex();
		}
		bool flag = bulkProjectId <= 0;
		bool flag2 = flag;
		if (!flag2)
		{
			flag2 = !(await _db.Projects.AnyAsync((Project p) => p.Id == bulkProjectId && p.IsActive));
		}
		if (flag2)
		{
			base.TempData["Error"] = "פרויקט לא תקין";
			return RedirectToFilteredIndex();
		}
		int created = 0;
		foreach (int item in selectedIds.Distinct())
		{
			_db.Allocations.Add(new Allocation
			{
				UserId = item,
				ProjectId = bulkProjectId,
				IsActive = true,
				CreatedAt = DateTime.UtcNow
			});
			created++;
		}
		await _db.SaveChangesAsync();
		base.TempData["Success"] = $"נוספה הקצאה ל-{created} עובדים";
		return RedirectToFilteredIndex();
	}

	[HttpGet]
	[Route("Employee/AllocationList")]
	public IActionResult AllocationList(AllocationListFilterModel filter)
	{
		return RedirectToAction("Index", "Allocations", AllocationRouteValues(filter));
	}

	[HttpGet]
	public async Task<IActionResult> ExportAllocationsExcel(AllocationListFilterModel filter)
	{
		return RedirectToAction("ExportExcel", "Allocations", AllocationRouteValues(filter));
	}

	[HttpGet]
	public async Task<IActionResult> ExportExcel(string? search, int? statusId, int? roleId, int? projectId)
	{
		List<User> list = await _employeeService.GetAllAsync(search, statusId, roleId);
		if (projectId.HasValue)
		{
			list = list.Where((User e) => e.Allocations.Any((Allocation a) => a.ProjectId == projectId.Value)).ToList();
		}
		using XLWorkbook xLWorkbook = new XLWorkbook();
		IXLWorksheet iXLWorksheet = xLWorkbook.Worksheets.Add("עובדים");
		iXLWorksheet.Cell(1, 1).Value = "קוד עובד";
		iXLWorksheet.Cell(1, 2).Value = "מספר זהות";
		iXLWorksheet.Cell(1, 3).Value = "שם פרטי";
		iXLWorksheet.Cell(1, 4).Value = "שם משפחה";
		iXLWorksheet.Cell(1, 5).Value = "תפקיד";
		iXLWorksheet.Cell(1, 6).Value = "סטטוס";
		iXLWorksheet.Cell(1, 7).Value = "עובד מדווח";
		iXLWorksheet.Cell(1, 8).Value = "מייל";
		iXLWorksheet.Cell(1, 9).Value = "טלפון";
		iXLWorksheet.Cell(1, 10).Value = "דיווח עתידי";
		iXLWorksheet.Cell(1, 11).Value = "פרויקטים";
		iXLWorksheet.Cell(1, 12).Value = "מחוזות";
		iXLWorksheet.Cell(1, 13).Value = "תוכניות";
		iXLWorksheet.Cell(1, 14).Value = "מגזרים";
		iXLWorksheet.Cell(1, 15).Value = "הערות";
		IXLRow iXLRow = iXLWorksheet.Row(1);
		iXLRow.Style.Font.Bold = true;
		iXLRow.Style.Fill.BackgroundColor = XLColor.LightBlue;
		int num = 2;
		foreach (User item in list)
		{
			iXLWorksheet.Cell(num, 1).Value = item.EmployeeCode;
			iXLWorksheet.Cell(num, 2).Value = item.IdNumber;
			iXLWorksheet.Cell(num, 3).Value = item.FirstName;
			iXLWorksheet.Cell(num, 4).Value = item.LastName;
			iXLWorksheet.Cell(num, 5).Value = item.UserRole?.DescriptionHebrew ?? item.UserRole?.Name ?? string.Empty;
			iXLWorksheet.Cell(num, 6).Value = item.Status?.DescriptionHebrew ?? item.Status?.Name ?? string.Empty;
			iXLWorksheet.Cell(num, 7).Value = (item.IsReportingEmployee ? "כן" : "לא");
			iXLWorksheet.Cell(num, 8).Value = item.Email;
			iXLWorksheet.Cell(num, 9).Value = item.Phone;
			iXLWorksheet.Cell(num, 10).Value = (item.AllowFutureReporting ? "כן" : "לא");
			iXLWorksheet.Cell(num, 11).Value = string.Join(", ", (from a in item.Allocations
				select a.Project?.Description into x
				where x != null
				select x).Distinct());
			iXLWorksheet.Cell(num, 12).Value = string.Join(", ", (from x in item.Allocations.SelectMany((Allocation a) => a.AllocationDistricts)
				select x.District?.Description into x
				where x != null
				select x).Distinct());
			iXLWorksheet.Cell(num, 13).Value = string.Join(", ", (from x in item.Allocations.SelectMany((Allocation a) => a.AllocationPrograms)
				select x.Program?.Description into x
				where x != null
				select x).Distinct());
			iXLWorksheet.Cell(num, 14).Value = string.Join(", ", (from x in item.Allocations.SelectMany((Allocation a) => a.AllocationSectors)
				select x.Sector?.Description into x
				where x != null
				select x).Distinct());
			iXLWorksheet.Cell(num, 15).Value = item.Notes;
			num++;
		}
		iXLWorksheet.Columns().AdjustToContents();
		using MemoryStream memoryStream = new MemoryStream();
		xLWorkbook.SaveAs(memoryStream);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		string fileDownloadName = $"employees_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
		return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
	}

	private async Task PopulateFormDropdownsAsync()
	{
		bool isAdmin = _currentUser.UserRole == UserRoleEnum.SystemAdmin;
		bool isPM = _currentUser.UserRole == UserRoleEnum.ProjectManager;
		if (_003C_003Eo__35._003C_003Ep__0 == null)
		{
			_003C_003Eo__35._003C_003Ep__0 = CallSite<Func<CallSite, object, SelectList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EmployeeRoles", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, SelectList, object> target = _003C_003Eo__35._003C_003Ep__0.Target;
		CallSite<Func<CallSite, object, SelectList, object>> _003C_003Ep__ = _003C_003Eo__35._003C_003Ep__0;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, new SelectList(await _db.Roles.Where((EmployeeRole r) => r.IsActive).ToListAsync(), "Id", "Description"));
		IQueryable<UserRole> source = _db.UserRoles.AsQueryable();
		if (!isAdmin)
		{
			source = source.Where((UserRole r) => r.Id != 1);
		}
		if (_003C_003Eo__35._003C_003Ep__1 == null)
		{
			_003C_003Eo__35._003C_003Ep__1 = CallSite<Func<CallSite, object, SelectList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserRoles", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		target = _003C_003Eo__35._003C_003Ep__1.Target;
		_003C_003Ep__ = _003C_003Eo__35._003C_003Ep__1;
		viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, new SelectList((await source.ToListAsync()).Select((UserRole r) => new
		{
			Id = r.Id,
			Label = (r.DescriptionHebrew ?? r.Name)
		}), "Id", "Label"));
		if (_003C_003Eo__35._003C_003Ep__2 == null)
		{
			_003C_003Eo__35._003C_003Ep__2 = CallSite<Func<CallSite, object, SelectList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Statuses", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		target = _003C_003Eo__35._003C_003Ep__2.Target;
		_003C_003Ep__ = _003C_003Eo__35._003C_003Ep__2;
		viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, new SelectList((await _db.UserStatuses.ToListAsync()).Select((UserStatus s) => new
		{
			Id = s.Id,
			Label = (s.DescriptionHebrew ?? s.Name)
		}), "Id", "Label"));
		base.ViewBag.RestDays = SelectListProviders.RestDayOptions.ToList();
		base.ViewBag.IsAdminOrPM = isAdmin || isPM;
	}

	private async Task PopulateAllocationDropdownsAsync(int? projectId = null, IReadOnlyCollection<int>? programIds = null)
	{
		if (_003C_003Eo__36._003C_003Ep__0 == null)
		{
			_003C_003Eo__36._003C_003Ep__0 = CallSite<Func<CallSite, object, SelectList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Projects", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, SelectList, object> target = _003C_003Eo__36._003C_003Ep__0.Target;
		CallSite<Func<CallSite, object, SelectList, object>> _003C_003Ep__ = _003C_003Eo__36._003C_003Ep__0;
		object viewBag = base.ViewBag;
		target(_003C_003Ep__, viewBag, new SelectList(await _db.Projects.Where((Project p) => p.IsActive).ToListAsync(), "Id", "Description"));
		base.ViewBag.ReportTypes = await _db.ReportTypes.Where((ReportType r) => r.IsActive).OrderBy((ReportType r) => r.Description).ToListAsync();
		if (_003C_003Eo__36._003C_003Ep__1 == null)
		{
			_003C_003Eo__36._003C_003Ep__1 = CallSite<Func<CallSite, object, List<District>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Districts", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<District>, object> target2 = _003C_003Eo__36._003C_003Ep__1.Target;
		CallSite<Func<CallSite, object, List<District>, object>> _003C_003Ep__2 = _003C_003Eo__36._003C_003Ep__1;
		viewBag = base.ViewBag;
		target2(_003C_003Ep__2, viewBag, await _db.Districts.Where((District d) => d.IsActive).ToListAsync());
		if (_003C_003Eo__36._003C_003Ep__2 == null)
		{
			_003C_003Eo__36._003C_003Ep__2 = CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Programs", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object> target3 = _003C_003Eo__36._003C_003Ep__2.Target;
		CallSite<Func<CallSite, object, List<AxiomaReporting.Core.Entities.Program>, object>> _003C_003Ep__3 = _003C_003Eo__36._003C_003Ep__2;
		viewBag = base.ViewBag;
		target3(_003C_003Ep__3, viewBag, await LoadProgramsForProjectAsync(projectId));
		if (_003C_003Eo__36._003C_003Ep__3 == null)
		{
			_003C_003Eo__36._003C_003Ep__3 = CallSite<Func<CallSite, object, List<Sector>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sectors", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Sector>, object> target4 = _003C_003Eo__36._003C_003Ep__3.Target;
		CallSite<Func<CallSite, object, List<Sector>, object>> _003C_003Ep__4 = _003C_003Eo__36._003C_003Ep__3;
		viewBag = base.ViewBag;
		target4(_003C_003Ep__4, viewBag, await _db.Sectors.Where((Sector s) => s.IsActive).ToListAsync());
		if (_003C_003Eo__36._003C_003Ep__4 == null)
		{
			_003C_003Eo__36._003C_003Ep__4 = CallSite<Func<CallSite, object, List<Locality>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Localities", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Locality>, object> target5 = _003C_003Eo__36._003C_003Ep__4.Target;
		CallSite<Func<CallSite, object, List<Locality>, object>> _003C_003Ep__5 = _003C_003Eo__36._003C_003Ep__4;
		viewBag = base.ViewBag;
		target5(_003C_003Ep__5, viewBag, await _db.Localities.Where((Locality l) => l.IsActive).ToListAsync());
		if (_003C_003Eo__36._003C_003Ep__5 == null)
		{
			_003C_003Eo__36._003C_003Ep__5 = CallSite<Func<CallSite, object, List<Framework>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Frameworks", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Framework>, object> target6 = _003C_003Eo__36._003C_003Ep__5.Target;
		CallSite<Func<CallSite, object, List<Framework>, object>> _003C_003Ep__6 = _003C_003Eo__36._003C_003Ep__5;
		viewBag = base.ViewBag;
		target6(_003C_003Ep__6, viewBag, await LoadScopedFrameworksAsync(projectId, programIds));
		if (_003C_003Eo__36._003C_003Ep__6 == null)
		{
			_003C_003Eo__36._003C_003Ep__6 = CallSite<Func<CallSite, object, List<Subject>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Subjects", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Subject>, object> target7 = _003C_003Eo__36._003C_003Ep__6.Target;
		CallSite<Func<CallSite, object, List<Subject>, object>> _003C_003Ep__7 = _003C_003Eo__36._003C_003Ep__6;
		viewBag = base.ViewBag;
		target7(_003C_003Ep__7, viewBag, await LoadScopedSubjectsAsync(projectId, programIds));
		if (_003C_003Eo__36._003C_003Ep__7 == null)
		{
			_003C_003Eo__36._003C_003Ep__7 = CallSite<Func<CallSite, object, List<Domain>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Domains", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<Domain>, object> target8 = _003C_003Eo__36._003C_003Ep__7.Target;
		CallSite<Func<CallSite, object, List<Domain>, object>> _003C_003Ep__8 = _003C_003Eo__36._003C_003Ep__7;
		viewBag = base.ViewBag;
		target8(_003C_003Ep__8, viewBag, await LoadScopedDomainsAsync(projectId, programIds));
		if (_003C_003Eo__36._003C_003Ep__8 == null)
		{
			_003C_003Eo__36._003C_003Ep__8 = CallSite<Func<CallSite, object, List<EducationalProgram>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EducationalPrograms", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<EducationalProgram>, object> target9 = _003C_003Eo__36._003C_003Ep__8.Target;
		CallSite<Func<CallSite, object, List<EducationalProgram>, object>> _003C_003Ep__9 = _003C_003Eo__36._003C_003Ep__8;
		viewBag = base.ViewBag;
		target9(_003C_003Ep__9, viewBag, await LoadScopedEducationalProgramsAsync(projectId, programIds));
		if (_003C_003Eo__36._003C_003Ep__9 == null)
		{
			_003C_003Eo__36._003C_003Ep__9 = CallSite<Func<CallSite, object, List<SchoolClass>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Classes", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<SchoolClass>, object> target10 = _003C_003Eo__36._003C_003Ep__9.Target;
		CallSite<Func<CallSite, object, List<SchoolClass>, object>> _003C_003Ep__10 = _003C_003Eo__36._003C_003Ep__9;
		viewBag = base.ViewBag;
		target10(_003C_003Ep__10, viewBag, await LoadScopedClassesAsync(projectId, programIds));
		if (_003C_003Eo__36._003C_003Ep__10 == null)
		{
			_003C_003Eo__36._003C_003Ep__10 = CallSite<Func<CallSite, object, List<GradeLevel>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GradeLevels", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<GradeLevel>, object> target11 = _003C_003Eo__36._003C_003Ep__10.Target;
		CallSite<Func<CallSite, object, List<GradeLevel>, object>> _003C_003Ep__11 = _003C_003Eo__36._003C_003Ep__10;
		viewBag = base.ViewBag;
		target11(_003C_003Ep__11, viewBag, await LoadScopedGradeLevelsAsync(projectId, programIds));
		if (_003C_003Eo__36._003C_003Ep__11 == null)
		{
			_003C_003Eo__36._003C_003Ep__11 = CallSite<Func<CallSite, object, List<DiscussionCode>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DiscussionCodes", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<DiscussionCode>, object> target12 = _003C_003Eo__36._003C_003Ep__11.Target;
		CallSite<Func<CallSite, object, List<DiscussionCode>, object>> _003C_003Ep__12 = _003C_003Eo__36._003C_003Ep__11;
		viewBag = base.ViewBag;
		target12(_003C_003Ep__12, viewBag, await LoadScopedDiscussionCodesAsync(projectId, programIds));
		if (_003C_003Eo__36._003C_003Ep__12 == null)
		{
			_003C_003Eo__36._003C_003Ep__12 = CallSite<Func<CallSite, object, List<LocalityDistrictNational>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LocalityDistrictNationals", typeof(EmployeeController), new CSharpArgumentInfo[2]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
			}));
		}
		Func<CallSite, object, List<LocalityDistrictNational>, object> target13 = _003C_003Eo__36._003C_003Ep__12.Target;
		CallSite<Func<CallSite, object, List<LocalityDistrictNational>, object>> _003C_003Ep__13 = _003C_003Eo__36._003C_003Ep__12;
		viewBag = base.ViewBag;
		target13(_003C_003Ep__13, viewBag, await _db.LocalityDistrictNationals.Where((LocalityDistrictNational l) => l.IsActive).ToListAsync());
	}

	private async Task<List<AxiomaReporting.Core.Entities.Program>> LoadProgramsForProjectAsync(int? projectId)
	{
		if (!projectId.HasValue || projectId.Value <= 0)
		{
			return new List<AxiomaReporting.Core.Entities.Program>();
		}
		List<AxiomaReporting.Core.Entities.Program> list = await (from pp in _db.ProjectPrograms
			where pp.ProjectId == ((int?)projectId).Value && pp.Program.IsActive
			orderby pp.Program.Description
			select pp.Program).ToListAsync();
		return list;
	}

	private async Task AddAllocationScopeValidationErrorsAsync(AllocationDto dto)
	{
		List<int> programIds = NormalizeIds(dto.ProgramIds);
		if (dto.ProjectId > 0 && programIds.Count > 0)
		{
			int validPrograms = await (from pp in _db.ProjectPrograms
				where pp.ProjectId == dto.ProjectId && programIds.Contains(pp.ProgramId)
				select pp.ProgramId).Distinct().CountAsync();
			if (validPrograms != programIds.Count)
			{
				base.ModelState.AddModelError("ProgramIds", "נבחרה תוכנית שאינה שייכת לפרויקט");
			}
		}
		await ValidateScopedIdsAsync("SubjectIds", dto.ProjectId, programIds, dto.SubjectIds, "ProjectProgramSubjects", "SubjectId");
		await ValidateScopedIdsAsync("DomainIds", dto.ProjectId, programIds, dto.DomainIds, "ProjectProgramDomains", "DomainId");
		await ValidateScopedIdsAsync("FrameworkIds", dto.ProjectId, programIds, dto.FrameworkIds, "ProjectProgramFrameworks", "FrameworkId");
		await ValidateScopedIdsAsync("EducationalProgramIds", dto.ProjectId, programIds, dto.EducationalProgramIds, "ProjectProgramEducationalPrograms", "EducationalProgramId");
		await ValidateScopedIdsAsync("DiscussionCodeIds", dto.ProjectId, programIds, dto.DiscussionCodeIds, "ProjectProgramDiscussionCodes", "DiscussionCodeId");
		await ValidateScopedIdsAsync("GradeLevelIds", dto.ProjectId, programIds, dto.GradeLevelIds, "ProjectProgramGradeLevels", "GradeLevelId");
		await ValidateScopedIdsAsync("ClassIds", dto.ProjectId, programIds, dto.ClassIds, "ProjectProgramClasses", "ClassId");
	}

	private async Task ValidateScopedIdsAsync(string field, int projectId, IReadOnlyCollection<int> programIds, IReadOnlyCollection<int>? selectedIds, string tableName, string idColumn)
	{
		List<int> ids = NormalizeIds(selectedIds);
		if (projectId <= 0 || programIds.Count == 0 || ids.Count == 0)
		{
			return;
		}
		HashSet<int> allowed = await ScopedIdSetAsync(tableName, idColumn, projectId, programIds);
		if (ids.Any((int id) => !allowed.Contains(id)))
		{
			base.ModelState.AddModelError(field, "נבחר ערך שאינו שייך לפרויקט/תוכנית הנבחרים");
		}
	}

	private async Task<List<Framework>> LoadScopedFrameworksAsync(int? projectId, IReadOnlyCollection<int>? programIds)
	{
		HashSet<int>? ids = await TryScopedIdSetAsync("ProjectProgramFrameworks", "FrameworkId", projectId, programIds);
		List<Framework> frameworks = await _db.Frameworks.Where((Framework f) => f.IsActive && (ids == null || ids.Contains(f.Id))).OrderBy((Framework f) => f.Description).ToListAsync();
		frameworks = frameworks.Where((Framework f) => int.TryParse(f.InstitutionSymbol, out _)).ToList();
		await ApplyFrameworkDisplayLabelsAsync(frameworks);
		return frameworks;
	}

	private async Task ApplyFrameworkDisplayLabelsAsync(List<Framework> frameworks)
	{
		if (frameworks.Count == 0)
		{
			return;
		}
		List<int> symbols = frameworks.Select(delegate(Framework f)
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
		foreach (Framework framework in frameworks)
		{
			int symbol;
			var institution = int.TryParse(framework.InstitutionSymbol, out symbol) ? institutions.FirstOrDefault(i => i.InstitutionSymbol == symbol) : null;
			string name = !string.IsNullOrWhiteSpace(institution?.Name) ? institution.Name : framework.Description;
			string label = string.Join(", ", new[] { institution?.LocalityName, framework.InstitutionSymbol, name }.Where((string part) => !string.IsNullOrWhiteSpace(part)));
			if (!string.IsNullOrWhiteSpace(label))
			{
				framework.Description = label;
			}
		}
	}

	private async Task<List<Subject>> LoadScopedSubjectsAsync(int? projectId, IReadOnlyCollection<int>? programIds)
	{
		HashSet<int>? ids = await TryScopedIdSetAsync("ProjectProgramSubjects", "SubjectId", projectId, programIds);
		return await _db.Subjects.Where((Subject s) => s.IsActive && (ids == null || ids.Contains(s.Id))).OrderBy((Subject s) => s.Description).ToListAsync();
	}

	private async Task<List<Domain>> LoadScopedDomainsAsync(int? projectId, IReadOnlyCollection<int>? programIds)
	{
		HashSet<int>? ids = await TryScopedIdSetAsync("ProjectProgramDomains", "DomainId", projectId, programIds);
		return await _db.Domains.Where((Domain d) => d.IsActive && (ids == null || ids.Contains(d.Id))).OrderBy((Domain d) => d.Description).ToListAsync();
	}

	private async Task<List<EducationalProgram>> LoadScopedEducationalProgramsAsync(int? projectId, IReadOnlyCollection<int>? programIds)
	{
		HashSet<int>? ids = await TryScopedIdSetAsync("ProjectProgramEducationalPrograms", "EducationalProgramId", projectId, programIds);
		return await _db.EducationalPrograms.Where((EducationalProgram e) => e.IsActive && (ids == null || ids.Contains(e.Id))).OrderBy((EducationalProgram e) => e.Description).ToListAsync();
	}

	private async Task<List<SchoolClass>> LoadScopedClassesAsync(int? projectId, IReadOnlyCollection<int>? programIds)
	{
		HashSet<int>? ids = await TryScopedIdSetAsync("ProjectProgramClasses", "ClassId", projectId, programIds);
		return await _db.Classes.Where((SchoolClass c) => c.IsActive && (ids == null || ids.Contains(c.Id))).OrderBy((SchoolClass c) => c.Description).ToListAsync();
	}

	private async Task<List<GradeLevel>> LoadScopedGradeLevelsAsync(int? projectId, IReadOnlyCollection<int>? programIds)
	{
		HashSet<int>? ids = await TryScopedIdSetAsync("ProjectProgramGradeLevels", "GradeLevelId", projectId, programIds);
		return await _db.GradeLevels.Where((GradeLevel g) => g.IsActive && (ids == null || ids.Contains(g.Id))).OrderBy((GradeLevel g) => g.Description).ToListAsync();
	}

	private async Task<List<DiscussionCode>> LoadScopedDiscussionCodesAsync(int? projectId, IReadOnlyCollection<int>? programIds)
	{
		HashSet<int>? ids = await TryScopedIdSetAsync("ProjectProgramDiscussionCodes", "DiscussionCodeId", projectId, programIds);
		return await _db.DiscussionCodes.Where((DiscussionCode d) => d.IsActive && (ids == null || ids.Contains(d.Id))).OrderBy((DiscussionCode d) => d.Description).ToListAsync();
	}

	private async Task<HashSet<int>?> TryScopedIdSetAsync(string tableName, string idColumn, int? projectId, IReadOnlyCollection<int>? programIds)
	{
		if (!projectId.HasValue || projectId.Value <= 0)
		{
			return null;
		}
		List<int> programs = NormalizeIds(programIds);
		if (programs.Count == 0)
		{
			programs = await (from pp in _db.ProjectPrograms
				where pp.ProjectId == projectId.Value
				select pp.ProgramId).Distinct().ToListAsync();
		}
		return await ScopedIdSetAsync(tableName, idColumn, projectId.Value, programs);
	}

	private async Task<HashSet<int>> ScopedIdSetAsync(string tableName, string idColumn, int projectId, IReadOnlyCollection<int> programIds)
	{
		HashSet<int> values = new HashSet<int>();
		if (programIds.Count == 0)
		{
			return values;
		}
		DbConnection connection = _db.Database.GetDbConnection();
		bool shouldClose = connection.State == System.Data.ConnectionState.Closed;
		if (shouldClose)
		{
			await connection.OpenAsync();
		}
		try
		{
			using DbCommand command = connection.CreateCommand();
			command.CommandText = $"SELECT DISTINCT {idColumn} FROM dbo.{tableName} WHERE ProjectId = @projectId AND ProgramId IN ({string.Join(",", programIds)})";
			DbParameter parameter = command.CreateParameter();
			parameter.ParameterName = "@projectId";
			parameter.Value = projectId;
			command.Parameters.Add(parameter);
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

	private static List<int> NormalizeIds(IReadOnlyCollection<int>? ids)
	{
		return ids?.Where((int id) => id > 0).Distinct().ToList() ?? new List<int>();
	}

	private Dictionary<string, object?> CurrentFilterRouteValues()
	{
		Dictionary<string, object?> rv = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		string[] array = new string[17]
		{
			"search", "idNumber", "employeeCode", "firstName", "lastName", "notes", "statusId", "roleId", "restDay", "allowFutureReporting",
			"hasAllocations", "lockedOnly", "projectId", "sortBy", "sortDesc", "page", "pageSize"
		};
		string[] array2 = new string[3] { "districtIds", "programIds", "sectorIds" };
		HttpRequest request = base.Request;
		if (request != null && request.HasFormContentType)
		{
			string[] array3 = array;
			foreach (string key2 in array3)
			{
				if (base.Request.Form.TryGetValue(key2, out var value2) && !string.IsNullOrEmpty(value2.ToString()))
				{
					TryAdd(key2, value2.ToString());
				}
			}
			string[] array4 = array2;
			foreach (string key3 in array4)
			{
				if (base.Request.Form.TryGetValue(key3, out var value3))
				{
					TryAddMulti(key3, value3);
				}
			}
		}
		if (base.Request != null)
		{
			string[] array5 = array;
			foreach (string key4 in array5)
			{
				if (base.Request.Query.TryGetValue(key4, out var value4) && !string.IsNullOrEmpty(value4.ToString()))
				{
					TryAdd(key4, value4.ToString());
				}
			}
			string[] array6 = array2;
			foreach (string key5 in array6)
			{
				if (base.Request.Query.TryGetValue(key5, out var value5))
				{
					TryAddMulti(key5, value5);
				}
			}
		}
		return rv;
		void TryAdd(string key, string? value)
		{
			if (!string.IsNullOrEmpty(value) && !rv.ContainsKey(key))
			{
				rv[key] = value;
			}
		}
		void TryAddMulti(string key, IEnumerable<string?> values)
		{
			List<string> list = values.Where((string v) => !string.IsNullOrEmpty(v)).ToList();
			if (list.Count == 0)
			{
				return;
			}
			int num = 0;
			foreach (string item in list)
			{
				rv[$"{key}[{num++}]"] = item;
			}
		}
	}

	private IActionResult RedirectToFilteredIndex()
	{
		return RedirectToAction("Index", CurrentFilterRouteValues());
	}

	private static Dictionary<string, object?> AllocationRouteValues(AllocationListFilterModel filter)
	{
		filter.Normalize();
		Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
		{
			["search"] = filter.Search,
			["projectId"] = filter.ProjectId,
			["idNumber"] = filter.IdNumber,
			["employeeCode"] = filter.EmployeeCode,
			["firstName"] = filter.FirstName,
			["lastName"] = filter.LastName,
			["monthlyEmploymentScope"] = filter.MonthlyEmploymentScope,
			["annualEmploymentScope"] = filter.AnnualEmploymentScope,
			["notes"] = filter.Notes,
			["showAll"] = (filter.ShowAll ? ((object)true) : null),
			["sortBy"] = filter.SortBy,
			["sortDesc"] = (filter.SortDesc ? ((object)true) : null),
			["page"] = ((filter.Page > 1) ? ((object)filter.Page) : null),
			["pageSize"] = ((filter.PageSize != 25) ? ((object)filter.PageSize) : null)
		};
		AddIndexed(dictionary, "programIds", filter.ProgramIds);
		AddIndexed(dictionary, "districtIds", filter.DistrictIds);
		AddIndexed(dictionary, "sectorIds", filter.SectorIds);
		AddIndexed(dictionary, "outputDurations", filter.OutputDurations);
		return dictionary;
	}

	private static void AddIndexed<T>(Dictionary<string, object?> values, string key, IReadOnlyList<T> items)
	{
		for (int i = 0; i < items.Count; i++)
		{
			values[$"{key}[{i}]"] = items[i];
		}
	}

	private void AddValidationErrors(FluentValidation.Results.ValidationResult result)
	{
		foreach (ValidationFailure error in result.Errors)
		{
			if (!base.ModelState.TryGetValue(error.PropertyName, out ModelStateEntry value) || !value.Errors.Any((ModelError e) => e.ErrorMessage == error.ErrorMessage))
			{
				base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
		}
	}

	private void NormalizeRequestedUserRole(EmployeeDto dto)
	{
		if (_currentUser.UserRole != UserRoleEnum.SystemAdmin && dto.UserRoleId == 1)
		{
			dto.UserRoleId = 6;
			base.ModelState.AddModelError("UserRoleId", "רק מנהל מערכת יכול להגדיר מנהל מערכת");
		}
	}

	internal static List<User> ApplyEmployeeSort(List<User> employees, string? sortBy, bool sortDesc)
	{
		Func<User, object> func;
		switch (sortBy?.ToLowerInvariant())
		{
		case "code":
		case "employeecode":
			func = (User u) => u.EmployeeCode;
			break;
		case "idnumber":
			func = (User u) => u.IdNumber;
			break;
		case "firstname":
			func = (User u) => u.FirstName;
			break;
		case "lastname":
			func = (User u) => u.LastName;
			break;
		case "name":
			func = (User u) => u.LastName + " " + u.FirstName;
			break;
		case "role":
		case "userrole":
		case "userroleid":
			func = (User u) => u.UserRole?.Name;
			break;
		case "employeerole":
		case "roleid":
		case "employeeroleid":
			func = (User u) => u.Role?.Description;
			break;
		case "statusid":
		case "status":
			func = (User u) => u.Status?.Name;
			break;
		case "isreportingemployee":
			func = (User u) => u.IsReportingEmployee;
			break;
		case "locked":
			func = (User u) => u.StatusId == 3 || u.FailedLoginAttempts >= 3;
			break;
		case "email":
			func = (User u) => u.Email;
			break;
		case "phone":
			func = (User u) => u.Phone;
			break;
		case "allowfuturereporting":
			func = (User u) => u.AllowFutureReporting;
			break;
		case "restday":
			func = (User u) => u.RestDay;
			break;
		case "projects":
		case "project":
			func = (User u) => (from a in u.Allocations
				where a.IsActive
				select a.Project?.Description into v
				where !string.IsNullOrWhiteSpace(v)
				orderby v
				select v).FirstOrDefault();
			break;
		case "district":
		case "districts":
			func = (User u) => (from x in u.Allocations.Where((Allocation a) => a.IsActive).SelectMany((Allocation a) => a.AllocationDistricts)
				select x.District?.Description into v
				where !string.IsNullOrWhiteSpace(v)
				orderby v
				select v).FirstOrDefault();
			break;
		case "programs":
		case "program":
			func = (User u) => (from x in u.Allocations.Where((Allocation a) => a.IsActive).SelectMany((Allocation a) => a.AllocationPrograms)
				select x.Program?.Description into v
				where !string.IsNullOrWhiteSpace(v)
				orderby v
				select v).FirstOrDefault();
			break;
		case "sector":
		case "sectors":
			func = (User u) => (from x in u.Allocations.Where((Allocation a) => a.IsActive).SelectMany((Allocation a) => a.AllocationSectors)
				select x.Sector?.Description into v
				where !string.IsNullOrWhiteSpace(v)
				orderby v
				select v).FirstOrDefault();
			break;
		case "notes":
			func = (User u) => u.Notes;
			break;
		default:
			func = (User u) => u.LastName + " " + u.FirstName;
			break;
		}
		Func<User, object> keySelector = func;
		return (sortDesc ? employees.OrderByDescending(keySelector) : employees.OrderBy(keySelector)).ToList();
	}

	private IQueryable<Allocation> AllocationListQuery(AllocationListFilterModel filter)
	{
		return BuildAllocationListQuery(_db, filter);
	}

	internal static IQueryable<Allocation> BuildAllocationListQuery(AppDbContext db, AllocationListFilterModel filter)
	{
		AllocationListFilterModel filter2 = filter;
		IQueryable<Allocation> queryable = (from a in db.Allocations.Include((Allocation a) => a.User).Include((Allocation a) => a.Project).Include((Allocation a) => a.AllocationDistricts)
				.ThenInclude((AllocationDistrict x) => x.District)
				.Include((Allocation a) => a.AllocationPrograms)
				.ThenInclude((AllocationProgram x) => x.Program)
				.Include((Allocation a) => a.AllocationSectors)
				.ThenInclude((AllocationSector x) => x.Sector)
			where a.IsActive
			select a).AsQueryable();
		if (filter2.ProjectId.HasValue)
		{
			queryable = queryable.Where((Allocation a) => a.ProjectId == filter2.ProjectId.Value);
		}
		if (!string.IsNullOrWhiteSpace(filter2.Search))
		{
			string search = filter2.Search;
			queryable = queryable.Where((Allocation a) => a.User.EmployeeCode.Contains(search) || a.User.IdNumber.Contains(search) || string.Concat(a.User.FirstName + " ", a.User.LastName).Contains(search) || a.Project.Description.Contains(search));
		}
		List<int> programIds = filter2.ProgramIds;
		if (programIds != null && programIds.Count > 0)
		{
			queryable = queryable.Where((Allocation a) => a.AllocationPrograms.Any((AllocationProgram ap) => filter2.ProgramIds.Contains(ap.ProgramId)));
		}
		programIds = filter2.DistrictIds;
		if (programIds != null && programIds.Count > 0)
		{
			queryable = queryable.Where((Allocation a) => a.AllocationDistricts.Any((AllocationDistrict ad) => filter2.DistrictIds.Contains(ad.DistrictId)));
		}
		programIds = filter2.SectorIds;
		if (programIds != null && programIds.Count > 0)
		{
			queryable = queryable.Where((Allocation a) => a.AllocationSectors.Any((AllocationSector asc) => filter2.SectorIds.Contains(asc.SectorId)));
		}
		if (!string.IsNullOrWhiteSpace(filter2.IdNumber))
		{
			queryable = queryable.Where((Allocation a) => a.User.IdNumber.Contains(filter2.IdNumber));
		}
		if (!string.IsNullOrWhiteSpace(filter2.EmployeeCode))
		{
			queryable = queryable.Where((Allocation a) => a.User.EmployeeCode.Contains(filter2.EmployeeCode));
		}
		if (!string.IsNullOrWhiteSpace(filter2.FirstName))
		{
			queryable = queryable.Where((Allocation a) => a.User.FirstName.Contains(filter2.FirstName));
		}
		if (!string.IsNullOrWhiteSpace(filter2.LastName))
		{
			queryable = queryable.Where((Allocation a) => a.User.LastName.Contains(filter2.LastName));
		}
		if (filter2.MonthlyEmploymentScope.HasValue)
		{
			queryable = queryable.Where((Allocation a) => a.MonthlyEmploymentScope == (decimal?)filter2.MonthlyEmploymentScope.Value);
		}
		if (filter2.AnnualEmploymentScope.HasValue)
		{
			queryable = queryable.Where((Allocation a) => a.AnnualEmploymentScope == (decimal?)filter2.AnnualEmploymentScope.Value);
		}
		List<string> outputDurations = filter2.OutputDurations;
		if (outputDurations != null && outputDurations.Count > 0)
		{
			foreach (string outputDuration in filter2.OutputDurations)
			{
				string token = "," + outputDuration + ",";
				queryable = queryable.Where((Allocation a) => a.OutputDuration != null && string.Concat("," + a.OutputDuration, ",").Contains(token));
			}
		}
		if (!string.IsNullOrWhiteSpace(filter2.Notes))
		{
			queryable = queryable.Where((Allocation a) => a.Notes != null && a.Notes.Contains(filter2.Notes));
		}
		return queryable;
	}

	internal static async Task<List<(int Id, string Description)>> GetProgramsForProjectAsync(AppDbContext db, int projectId)
	{
		if (projectId <= 0)
		{
			return new List<(int Id, string Description)>();
		}
		var projectPrograms = await (from pp in db.ProjectPrograms
			where pp.ProjectId == projectId && pp.Program.IsActive
			orderby pp.Program.Description
			select new
			{
				Id = pp.ProgramId,
				pp.Program.Description
			}).ToListAsync();
		return projectPrograms.Select(p => (p.Id, p.Description)).ToList();
	}

	[HttpGet]
	[Authorize(Policy = "AdminPMOrCoordinator")]
	public async Task<IActionResult> ProgramsForProject(int projectId)
	{
		if (projectId <= 0)
		{
			return Json(Array.Empty<object>());
		}
		return Json(Enumerable.Select(await GetProgramsForProjectAsync(_db, projectId), ((int Id, string Description) p) => new
		{
			id = p.Id,
			description = p.Description
		}));
	}

	internal static IOrderedQueryable<Allocation> ApplyAllocationSort(IQueryable<Allocation> query, string? sortBy, bool sortDesc)
	{
		switch (sortBy?.ToLowerInvariant())
		{
		case "code":
		case "employeecode":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.User.EmployeeCode) : query.OrderBy((Allocation a) => a.User.EmployeeCode);
		case "idnumber":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.User.IdNumber) : query.OrderBy((Allocation a) => a.User.IdNumber);
		case "firstname":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.User.FirstName) : query.OrderBy((Allocation a) => a.User.FirstName);
		case "lastname":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.User.LastName) : query.OrderBy((Allocation a) => a.User.LastName);
		case "name":
			return sortDesc ? (from a in query
				orderby a.User.LastName descending, a.User.FirstName descending
				select a) : (from a in query
				orderby a.User.LastName, a.User.FirstName
				select a);
		case "projectid":
		case "project":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.Project.Description) : query.OrderBy((Allocation a) => a.Project.Description);
		case "annualscope":
		case "annualemploymentscope":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.AnnualEmploymentScope) : query.OrderBy((Allocation a) => a.AnnualEmploymentScope);
		case "monthlyscope":
		case "monthlyemploymentscope":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.MonthlyEmploymentScope) : query.OrderBy((Allocation a) => a.MonthlyEmploymentScope);
		case "dailyscope":
		case "dailyemploymentscope":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.DailyEmploymentScope) : query.OrderBy((Allocation a) => a.DailyEmploymentScope);
		case "monthlyrows":
		case "monthlyrowallocation":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.MonthlyRowAllocation) : query.OrderBy((Allocation a) => a.MonthlyRowAllocation);
		case "annualrows":
		case "annualrowallocation":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.AnnualRowAllocation) : query.OrderBy((Allocation a) => a.AnnualRowAllocation);
		case "districts":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.AllocationDistricts.Select((AllocationDistrict x) => x.District.Description).FirstOrDefault()) : query.OrderBy((Allocation a) => a.AllocationDistricts.Select((AllocationDistrict x) => x.District.Description).FirstOrDefault());
		case "programs":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.AllocationPrograms.Select((AllocationProgram x) => x.Program.Description).FirstOrDefault()) : query.OrderBy((Allocation a) => a.AllocationPrograms.Select((AllocationProgram x) => x.Program.Description).FirstOrDefault());
		case "sectors":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.AllocationSectors.Select((AllocationSector x) => x.Sector.Description).FirstOrDefault()) : query.OrderBy((Allocation a) => a.AllocationSectors.Select((AllocationSector x) => x.Sector.Description).FirstOrDefault());
		case "outputduration":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.OutputDuration) : query.OrderBy((Allocation a) => a.OutputDuration);
		case "allowexcelupload":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.AllowExcelUpload) : query.OrderBy((Allocation a) => a.AllowExcelUpload);
		case "notes":
			return sortDesc ? query.OrderByDescending((Allocation a) => a.Notes) : query.OrderBy((Allocation a) => a.Notes);
		default:
			return sortDesc ? (from a in query
				orderby a.User.LastName descending, a.User.FirstName descending
				select a) : (from a in query
				orderby a.User.LastName, a.User.FirstName
				select a);
		}
	}
}

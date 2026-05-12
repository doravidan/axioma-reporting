using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Infrastructure.Validators;
using AxiomaReporting.Web.Authorization;
using AxiomaReporting.Web.Helpers;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Policy = PolicyNames.AdminPMOrCoordinator)]
public class EmployeeController : Controller
{
  private static readonly HashSet<string> AllowedAttachmentExtensions =
    new(StringComparer.OrdinalIgnoreCase) { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx", ".xls", ".xlsx" };
  private const long MaxAttachmentBytes = 10 * 1024 * 1024;
  private const int MaxAttachmentDescriptionLength = 1000;

  private readonly IEmployeeService _employeeService;
  private readonly ICurrentUserService _currentUser;
  private readonly IPasswordService _passwordService;
  private readonly IReportStatusService _reportStatusService;
  private readonly IReportExcelImportService _excelImportService;
  private readonly IAuditLogService _auditLog;
  private readonly AppDbContext _db;

  private static readonly decimal[] OUTPUT_DURATION_OPTIONS = { 0.5m, 1m, 1.5m, 2m, 2.5m, 3m };

  public EmployeeController(
    IEmployeeService employeeService,
    ICurrentUserService currentUser,
    IPasswordService passwordService,
    IReportStatusService reportStatusService,
    IReportExcelImportService excelImportService,
    IAuditLogService auditLog,
    AppDbContext db)
  {
    _employeeService = employeeService;
    _currentUser = currentUser;
    _passwordService = passwordService;
    _reportStatusService = reportStatusService;
    _excelImportService = excelImportService;
    _auditLog = auditLog;
    _db = db;
  }

  // GET /Employee
  [HttpGet]
  public async Task<IActionResult> Index(EmployeeListFilterModel filter)
  {
    filter.Normalize();

    var employees = await _employeeService.GetAllAsync(filter.Search, filter.StatusId, filter.RoleId);
    employees = ApplyEmployeeFilters(employees, filter);
    employees = ApplyEmployeeSort(employees, filter.SortBy, filter.SortDesc);

    var totalCount = employees.Count;
    var page = Math.Max(filter.Page, 1);
    var pageSize = filter.PageSize is < 1 or > 500 ? 25 : filter.PageSize;
    var pagedEmployees = employees
      .Skip((page - 1) * pageSize)
      .Take(pageSize)
      .ToList();

    ViewBag.Filter = filter;
    // Backwards-compat ViewBag entries (older partials may still reference them).
    ViewBag.Search = filter.Search;
    ViewBag.StatusId = filter.StatusId;
    ViewBag.RoleId = filter.RoleId;
    ViewBag.ProjectId = filter.ProjectId;
    ViewBag.SortBy = filter.SortBy;
    ViewBag.SortDesc = filter.SortDesc;
    ViewBag.Page = page;
    ViewBag.PageSize = pageSize;
    ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
    ViewBag.Statuses = await _db.UserStatuses.ToListAsync();
    ViewBag.UserRoles = await _db.UserRoles.ToListAsync();
    ViewBag.Projects = await _db.Projects.Where(p => p.IsActive).OrderBy(p => p.Description).ToListAsync();
    ViewBag.AllDistricts = await _db.Districts.Where(d => d.IsActive).OrderBy(d => d.Description).ToListAsync();
    ViewBag.AllPrograms = await _db.Programs.Where(p => p.IsActive).OrderBy(p => p.Description).ToListAsync();
    ViewBag.AllSectors = await _db.Sectors.Where(s => s.IsActive).OrderBy(s => s.Description).ToListAsync();

    return View(pagedEmployees);
  }

  /// <summary>
  /// Apply the additional EmployeeListFilterModel filters that aren't already handled by
  /// <see cref="IEmployeeService.GetAllAsync"/>. Exposed internally for unit tests.
  /// </summary>
  internal static List<User> ApplyEmployeeFilters(List<User> source, EmployeeListFilterModel filter)
  {
    IEnumerable<User> q = source;

    if (!string.IsNullOrWhiteSpace(filter.IdNumber))
      q = q.Where(u => u.IdNumber.Contains(filter.IdNumber, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(filter.EmployeeCode))
      q = q.Where(u => u.EmployeeCode.Contains(filter.EmployeeCode, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(filter.FirstName))
      q = q.Where(u => u.FirstName.Contains(filter.FirstName, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(filter.LastName))
      q = q.Where(u => u.LastName.Contains(filter.LastName, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(filter.Notes))
      q = q.Where(u => !string.IsNullOrWhiteSpace(u.Notes) &&
                        u.Notes!.Contains(filter.Notes, StringComparison.OrdinalIgnoreCase));

    if (filter.RestDay.HasValue)
      q = q.Where(u => u.RestDay == filter.RestDay.Value);

    if (filter.AllowFutureReporting.HasValue)
      q = q.Where(u => u.AllowFutureReporting == filter.AllowFutureReporting.Value);

    if (filter.LockedOnly)
      q = q.Where(u => u.StatusId == (int)UserStatusEnum.Locked || u.FailedLoginAttempts >= 3);

    if (filter.HasAllocations.HasValue)
    {
      var has = filter.HasAllocations.Value;
      q = q.Where(u => u.Allocations.Any(a => a.IsActive) == has);
    }

    if (filter.ProjectId.HasValue)
      q = q.Where(u => u.Allocations.Any(a => a.IsActive && a.ProjectId == filter.ProjectId.Value));

    if (filter.DistrictIds is { Count: > 0 })
      q = q.Where(u => u.Allocations.Any(a => a.IsActive &&
        a.AllocationDistricts.Any(ad => filter.DistrictIds.Contains(ad.DistrictId))));

    if (filter.ProgramIds is { Count: > 0 })
      q = q.Where(u => u.Allocations.Any(a => a.IsActive &&
        a.AllocationPrograms.Any(ap => filter.ProgramIds.Contains(ap.ProgramId))));

    if (filter.SectorIds is { Count: > 0 })
      q = q.Where(u => u.Allocations.Any(a => a.IsActive &&
        a.AllocationSectors.Any(asec => filter.SectorIds.Contains(asec.SectorId))));

    return q.ToList();
  }

  // GET /Employee/Create
  [HttpGet]
  public async Task<IActionResult> Create()
  {
    await PopulateFormDropdownsAsync();
    ViewBag.IsEdit = false;
    return View("Form", new EmployeeDto());
  }

  // POST /Employee/Create
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(EmployeeDto dto)
  {
    NormalizeRequestedUserRole(dto);
    AddValidationErrors(new UserValidator().Validate(dto));
    if (!ModelState.IsValid)
    {
      await PopulateFormDropdownsAsync();
      ViewBag.IsEdit = false;
      return View("Form", dto);
    }

    await _employeeService.CreateAsync(dto, _currentUser.IdNumber);
    TempData["Success"] = "העובד נוצר בהצלחה";
    return RedirectToAction(nameof(Index));
  }

  // GET /Employee/Edit/{id}
  [HttpGet]
  public async Task<IActionResult> Edit(int id)
  {
    var user = await _employeeService.GetByIdAsync(id);
    if (user == null) return NotFound();

    var dto = new EmployeeDto
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
    ViewBag.Attachments = await _db.DocumentAttachments
      .Where(a => a.UserId == id)
      .OrderByDescending(a => a.UploadedAt)
      .ToListAsync();
    ViewBag.IsEdit = true;
    return View("Form", dto);
  }

  // POST /Employee/Edit/{id}
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, EmployeeDto dto)
  {
    NormalizeRequestedUserRole(dto);
    var existing = await _db.Users.AsNoTracking()
      .Where(u => u.Id == id)
      .Select(u => new { u.EmployeeCode, u.IdNumber, u.Phone })
      .FirstOrDefaultAsync();
    if (existing == null) return NotFound();

    var validation = new UserValidator().Validate(dto);
    // Let admins edit other fields on legacy users whose existing values were
    // created before the stricter validation rules. Changed values still validate strictly.
    if (string.Equals(dto.EmployeeCode, existing.EmployeeCode, StringComparison.Ordinal))
    {
      ModelState.Remove(nameof(EmployeeDto.EmployeeCode));
      validation.Errors.RemoveAll(e => e.PropertyName == nameof(EmployeeDto.EmployeeCode));
    }
    if (string.Equals(dto.IdNumber, existing.IdNumber, StringComparison.Ordinal))
      validation.Errors.RemoveAll(e => e.PropertyName == nameof(EmployeeDto.IdNumber));
    if (string.Equals(dto.Phone, existing.Phone, StringComparison.Ordinal))
      validation.Errors.RemoveAll(e => e.PropertyName == nameof(EmployeeDto.Phone));
    AddValidationErrors(validation);
    if (!ModelState.IsValid)
    {
      await PopulateFormDropdownsAsync();
      ViewBag.IsEdit = true;
      return View("Form", dto);
    }

    var updated = await _employeeService.UpdateAsync(id, dto);
    if (!updated) return NotFound();

    TempData["Success"] = "פרטי העובד עודכנו בהצלחה";
    return RedirectToAction(nameof(Index));
  }

  // POST /Employee/ResetPassword/{id}
  // Admin-initiated reset: sets PasswordHash = BCrypt(IdNumber), clears lockout,
  // pushes the previous hash into PasswordHistory, sets MustChangePassword=true,
  // and records an AuditLog row. The user is forced to ChangePassword on next login.
  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminPMOrCoordinator)]
  public async Task<IActionResult> ResetPassword(int id)
  {
    var user = await _db.Users.FindAsync(id);
    if (user == null) return NotFound();

    // Coordinator cannot reset Admin or PM passwords.
    if (_currentUser.UserRole == UserRoleEnum.ProjectCoordinator &&
        user.UserRoleId <= (int)UserRoleEnum.ProjectManager)
      return Forbid();

    var previousHash = user.PasswordHash;
    var now = DateTime.UtcNow;

    // Push the current hash into history (so the user can't reuse it on next ChangePassword).
    if (!string.IsNullOrEmpty(previousHash))
    {
      _db.PasswordHistories.Add(new PasswordHistory
      {
        UserId = user.Id,
        PasswordHash = previousHash,
        CreatedAt = now
      });
    }

    user.PasswordHash = _passwordService.HashPassword(user.IdNumber);
    user.MustChangePassword = true;
    user.FailedLoginAttempts = 0;
    user.LastPasswordChange = now;
    if (user.StatusId == (int)UserStatusEnum.Locked)
      user.StatusId = (int)UserStatusEnum.Active;
    user.UpdatedAt = now;

    await _db.SaveChangesAsync();

    var resetByIdNumber = User.Identity?.Name ?? _currentUser.UserId.ToString();
    await _auditLog.LogAsync("User.PasswordReset", nameof(User), id.ToString(),
      notes: $"reset-by={resetByIdNumber}");

    TempData["Success"] = $"הסיסמה של {user.FirstName} {user.LastName} אופסה למספר הזהות";
    return RedirectToFilteredIndex();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminPMOrCoordinator)]
  public async Task<IActionResult> UnlockAccount(int id)
  {
    var user = await _db.Users.FindAsync(id);
    if (user == null) return NotFound();

    // Coordinator cannot unlock Admin or PM accounts
    if (_currentUser.UserRole == UserRoleEnum.ProjectCoordinator &&
        user.UserRoleId <= (int)UserRoleEnum.ProjectManager)
      return Forbid();

    var wasLocked = user.StatusId == (int)UserStatusEnum.Locked;
    user.FailedLoginAttempts = 0;
    if (user.StatusId == (int)UserStatusEnum.Locked)
      user.StatusId = (int)UserStatusEnum.Active;
    user.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    if (wasLocked)
      await _auditLog.LogAsync("Auth.Unlock", nameof(User), id.ToString(),
        notes: $"unlocked by user {_currentUser.UserId}");

    TempData["Success"] = $"החשבון של {user.FirstName} {user.LastName} שוחרר מנעילה";
    return RedirectToFilteredIndex();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminPMOrCoordinator)]
  public async Task<IActionResult> UploadAttachment(int id, IFormFile file, string? description)
  {
    var user = await _employeeService.GetByIdAsync(id);
    if (user == null) return NotFound();

    if (file == null || file.Length == 0)
    {
      TempData["Error"] = "לא נבחר קובץ";
      return RedirectToAction(nameof(Edit), new { id });
    }

    if (file.Length > MaxAttachmentBytes)
    {
      TempData["Error"] = "גודל הקובץ חורג מהמותר";
      return RedirectToAction(nameof(Edit), new { id });
    }

    var extension = Path.GetExtension(file.FileName);
    if (!AllowedAttachmentExtensions.Contains(extension))
    {
      TempData["Error"] = "סוג הקובץ אינו נתמך";
      return RedirectToAction(nameof(Edit), new { id });
    }

    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "employees");
    Directory.CreateDirectory(uploadsDir);
    var storedFileName = $"{Guid.NewGuid()}{extension}";
    var filePath = Path.Combine(uploadsDir, storedFileName);
    var attachmentDescription = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
    if (attachmentDescription?.Length > MaxAttachmentDescriptionLength)
    {
      attachmentDescription = attachmentDescription.Substring(0, MaxAttachmentDescriptionLength);
    }

    await using (var stream = new FileStream(filePath, FileMode.CreateNew))
      await file.CopyToAsync(stream);

    _db.DocumentAttachments.Add(new DocumentAttachment
    {
      UserId = id,
      FileName = Path.GetFileName(file.FileName),
      Description = attachmentDescription,
      FilePath = $"/uploads/employees/{storedFileName}",
      FileSize = file.Length,
      MimeType = file.ContentType,
      UploadedAt = DateTime.UtcNow,
      UploadedBy = _currentUser.UserId
    });
    await _db.SaveChangesAsync();

    TempData["Success"] = "המסמך נוסף בהצלחה";
    return RedirectToAction(nameof(Edit), new { id });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminPMOrCoordinator)]
  public async Task<IActionResult> DeleteAttachment(int id, int attachmentId)
  {
    var attachment = await _db.DocumentAttachments
      .FirstOrDefaultAsync(a => a.Id == attachmentId && a.UserId == id);
    if (attachment == null) return NotFound();

    var fullPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      attachment.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
    if (System.IO.File.Exists(fullPath))
      System.IO.File.Delete(fullPath);

    _db.DocumentAttachments.Remove(attachment);
    await _db.SaveChangesAsync();
    TempData["Success"] = "המסמך נמחק";
    return RedirectToAction(nameof(Edit), new { id });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOrPM)]
  public async Task<IActionResult> DeleteEmployee(int id)
  {
    var user = await _db.Users.FindAsync(id);
    if (user == null) return NotFound();

    var previousStatusId = user.StatusId;
    user.StatusId = (int)UserStatusEnum.Inactive;
    user.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    await _auditLog.LogAsync("Employee.Deactivate", nameof(User), id.ToString(),
      before: new { StatusId = previousStatusId },
      after: new { StatusId = (int)UserStatusEnum.Inactive });

    TempData["Success"] = $"העובד {user.FirstName} {user.LastName} הושבת";
    return RedirectToFilteredIndex();
  }

  // GET /Employee/{id}/Allocations
  [HttpGet]
  [Route("Employee/{id}/Allocations")]
  public async Task<IActionResult> Allocations(int id)
  {
    var user = await _employeeService.GetByIdAsync(id);
    if (user == null) return NotFound();

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
  [Authorize(Policy = PolicyNames.AdminOrPM)]
  public async Task<IActionResult> UploadAllocationExcel(int id, int allocationId, int reportingMonthId, IFormFile file)
  {
    var allocation = await _db.Allocations.FirstOrDefaultAsync(a => a.Id == allocationId && a.UserId == id);
    if (allocation == null) return NotFound();

    if (file == null || file.Length == 0)
    {
      TempData["Error"] = "לא נבחר קובץ אקסל";
      return RedirectToAction(nameof(Allocations), new { id });
    }

    var report = await _reportStatusService.GetOrCreateDraftAsync(id, reportingMonthId);
    if (report == null) return StatusCode(500);

    await using var stream = file.OpenReadStream();
    var result = await _excelImportService.ImportAsync(report.Id, allocationId, stream, _currentUser.UserId);
    if (!result.Success)
    {
      TempData["Error"] = string.Join(" | ", result.Errors.Take(5));
      return RedirectToAction(nameof(Allocations), new { id });
    }

    TempData["Success"] = $"יובאו {result.ImportedRows} שורות לדיווח העובד";
    return RedirectToAction(nameof(Allocations), new { id });
  }

  // GET /Employee/{id}/Allocations/Create
  [HttpGet]
  [Route("Employee/{id}/Allocations/Create")]
  public async Task<IActionResult> CreateAllocation(int id)
  {
    var user = await _employeeService.GetByIdAsync(id);
    if (user == null) return NotFound();

    await PopulateAllocationDropdownsAsync(projectId: null);
    ViewBag.Employee = user;
    ViewBag.IsEdit = false;
    ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;

    return View("AllocationForm", new AllocationDto { UserId = id });
  }

  // POST /Employee/{id}/Allocations/Create
  [HttpPost]
  [ValidateAntiForgeryToken]
  [Route("Employee/{id}/Allocations/Create")]
  public async Task<IActionResult> CreateAllocation(int id, AllocationDto dto, bool continueAdding = false)
  {
    dto.UserId = id;
    AddValidationErrors(new AllocationValidator().Validate(dto));

    if (!ModelState.IsValid)
    {
      var user = await _employeeService.GetByIdAsync(id);
      ViewBag.Employee = user;
      await PopulateAllocationDropdownsAsync(dto.ProjectId);
      ViewBag.IsEdit = false;
      ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;
      return View("AllocationForm", dto);
    }

    await _employeeService.CreateAllocationAsync(dto);
    TempData["Success"] = "ההקצאה נוצרה בהצלחה";
    if (continueAdding)
      return RedirectToAction(nameof(CreateAllocation), new { id });

    return RedirectToAction(nameof(Allocations), new { id });
  }

  // GET /Employee/{id}/Allocations/{allocationId}/Edit
  [HttpGet]
  [Route("Employee/{id}/Allocations/{allocationId}/Edit")]
  public async Task<IActionResult> EditAllocation(int id, int allocationId)
  {
    var user = await _employeeService.GetByIdAsync(id);
    if (user == null) return NotFound();

    var allocation = await _employeeService.GetAllocationByIdAsync(allocationId);
    if (allocation == null || allocation.UserId != id) return NotFound();

    var existingDurations = (allocation.OutputDuration ?? string.Empty)
      .Split(',', StringSplitOptions.RemoveEmptyEntries)
      .Select(s => decimal.TryParse(s, out var v) ? v : (decimal?)null)
      .Where(v => v.HasValue)
      .Select(v => v!.Value)
      .ToList();

    var dto = new AllocationDto
    {
      Id = allocation.Id,
      UserId = id,
      ProjectId = allocation.ProjectId,
      AnnualEmploymentScope = allocation.AnnualEmploymentScope,
      MonthlyEmploymentScope = allocation.MonthlyEmploymentScope,
      DailyEmploymentScope = allocation.DailyEmploymentScope,
      MonthlyRowAllocation = allocation.MonthlyRowAllocation,
      AnnualRowAllocation = allocation.AnnualRowAllocation,
      OutputDuration = allocation.OutputDuration,
      OutputDurationValues = existingDurations,
      AllowExcelUpload = allocation.AllowExcelUpload,
      Notes = allocation.Notes,
      IsActive = allocation.IsActive,
      DistrictIds = allocation.AllocationDistricts.Select(x => x.DistrictId).ToList(),
      ProgramIds = allocation.AllocationPrograms.Select(x => x.ProgramId).ToList(),
      SectorIds = allocation.AllocationSectors.Select(x => x.SectorId).ToList(),
      LocalityIds = allocation.AllocationLocalities.Select(x => x.LocalityId).ToList(),
      FrameworkIds = allocation.AllocationFrameworks.Select(x => x.FrameworkId).ToList(),
      SubjectIds = allocation.AllocationSubjects.Select(x => x.SubjectId).ToList(),
      DomainIds = allocation.AllocationDomains.Select(x => x.DomainId).ToList(),
      EducationalProgramIds = allocation.AllocationEducationalPrograms.Select(x => x.EducationalProgramId).ToList(),
      ClassIds = allocation.AllocationClasses.Select(x => x.ClassId).ToList(),
      GradeLevelIds = allocation.AllocationGradeLevels.Select(x => x.GradeLevelId).ToList(),
      DiscussionCodeIds = allocation.AllocationDiscussionCodes.Select(x => x.DiscussionCodeId).ToList(),
      LocalityDistrictNationalIds = allocation.AllocationLocalityDistrictNationals
        .Select(x => x.LocalityDistrictNationalId).ToList()
    };

    await PopulateAllocationDropdownsAsync(dto.ProjectId);
    ViewBag.Employee = user;
    ViewBag.IsEdit = true;
    ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;

    return View("AllocationForm", dto);
  }

  // POST /Employee/{id}/Allocations/{allocationId}/Edit
  [HttpPost]
  [ValidateAntiForgeryToken]
  [Route("Employee/{id}/Allocations/{allocationId}/Edit")]
  public async Task<IActionResult> EditAllocation(int id, int allocationId, AllocationDto dto)
  {
    dto.UserId = id;
    AddValidationErrors(new AllocationValidator().Validate(dto));

    if (!ModelState.IsValid)
    {
      var user = await _employeeService.GetByIdAsync(id);
      ViewBag.Employee = user;
      await PopulateAllocationDropdownsAsync(dto.ProjectId);
      ViewBag.IsEdit = true;
      ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;
      return View("AllocationForm", dto);
    }

    var updated = await _employeeService.UpdateAllocationAsync(allocationId, dto);
    if (!updated) return NotFound();

    TempData["Success"] = "ההקצאה עודכנה בהצלחה";
    return RedirectToAction(nameof(EditAllocation), new { id, allocationId });
  }

  // POST /Employee/{id}/Allocations/{allocationId}/Delete
  [HttpPost]
  [ValidateAntiForgeryToken]
  [Route("Employee/{id}/Allocations/{allocationId}/Delete")]
  public async Task<IActionResult> DeleteAllocation(int id, int allocationId)
  {
    await _employeeService.DeleteAllocationAsync(allocationId);
    TempData["Success"] = "ההקצאה הושבתה בהצלחה";
    return RedirectToAction(nameof(Allocations), new { id });
  }

  // POST /Employee/BulkAction
  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOrPM)]
  public async Task<IActionResult> BulkAction(List<int> selectedIds, int newStatusId)
  {
    if (selectedIds == null || selectedIds.Count == 0)
    {
      TempData["Error"] = "לא נבחרו עובדים";
      return RedirectToFilteredIndex();
    }

    var users = await _db.Users.Where(u => selectedIds.Contains(u.Id)).ToListAsync();
    foreach (var user in users)
    {
      user.StatusId = newStatusId;
      user.UpdatedAt = DateTime.UtcNow;
    }

    await _db.SaveChangesAsync();
    TempData["Success"] = $"עודכנו {users.Count} עובדים";
    return RedirectToFilteredIndex();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOrPM)]
  // bulkProjectId comes from the bulk-action bar; the filter's `projectId` is reserved for the
  // Index filter so we use a distinct form field name to avoid model-binding collision.
  public async Task<IActionResult> BulkAddAllocation(List<int> selectedIds, int bulkProjectId)
  {
    if (selectedIds == null || selectedIds.Count == 0)
    {
      TempData["Error"] = "לא נבחרו עובדים";
      return RedirectToFilteredIndex();
    }

    if (bulkProjectId <= 0 || !await _db.Projects.AnyAsync(p => p.Id == bulkProjectId && p.IsActive))
    {
      TempData["Error"] = "פרויקט לא תקין";
      return RedirectToFilteredIndex();
    }

    var created = 0;
    foreach (var userId in selectedIds.Distinct())
    {
      _db.Allocations.Add(new Allocation
      {
        UserId = userId,
        ProjectId = bulkProjectId,
        IsActive = true,
        CreatedAt = DateTime.UtcNow
      });
      created++;
    }

    await _db.SaveChangesAsync();
    TempData["Success"] = $"נוספה הקצאה ל-{created} עובדים";
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

#pragma warning disable CS0162
    filter.Normalize();
    var allocations = await AllocationListQuery(filter)
      .OrderBy(a => a.User!.LastName)
      .ThenBy(a => a.User!.FirstName)
      .ToListAsync();

    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("הקצאות");
    ws.RightToLeft = true;
    var headers = new[]
    {
      "פרויקט", "תוכנית", "מחוז", "מגזר", "ת.ז", "קוד עובד", "שם פרטי", "שם משפחה",
      "היקף פעילות חודשי", "היקף פעילות יומי", "היקף פעילות שנתי", "הקצאת שורות חודשית",
      "הקצאת שורות שנתית", "משך תפוקה", "הערות", "אפשר העלאת קובץ דיווח"
    };
    for (var i = 0; i < headers.Length; i++) ws.Cell(1, i + 1).Value = headers[i];

    var row = 2;
    foreach (var a in allocations)
    {
      ws.Cell(row, 1).Value = a.Project?.Description;
      ws.Cell(row, 2).Value = string.Join(", ", a.AllocationPrograms.Select(x => x.Program?.Description).Where(x => x != null));
      ws.Cell(row, 3).Value = string.Join(", ", a.AllocationDistricts.Select(x => x.District?.Description).Where(x => x != null));
      ws.Cell(row, 4).Value = string.Join(", ", a.AllocationSectors.Select(x => x.Sector?.Description).Where(x => x != null));
      ws.Cell(row, 5).Value = a.User?.IdNumber;
      ws.Cell(row, 6).Value = a.User?.EmployeeCode;
      ws.Cell(row, 7).Value = a.User?.FirstName;
      ws.Cell(row, 8).Value = a.User?.LastName;
      if (a.MonthlyEmploymentScope.HasValue) ws.Cell(row, 9).Value = (double)a.MonthlyEmploymentScope.Value;
      ws.Cell(row, 10).Value = a.DailyEmploymentScope?.ToString("0.##") ?? "ללא הגבלה";
      if (a.AnnualEmploymentScope.HasValue) ws.Cell(row, 11).Value = (double)a.AnnualEmploymentScope.Value;
      if (a.MonthlyRowAllocation.HasValue) ws.Cell(row, 12).Value = a.MonthlyRowAllocation.Value;
      if (a.AnnualRowAllocation.HasValue) ws.Cell(row, 13).Value = a.AnnualRowAllocation.Value;
      ws.Cell(row, 14).Value = a.OutputDuration;
      ws.Cell(row, 15).Value = a.Notes;
      ws.Cell(row, 16).Value = a.AllowExcelUpload ? "כן" : "לא";
      row++;
    }

    ws.Columns().AdjustToContents();
    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    return File(stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"employee_allocations_{DateTime.Now:yyyy-MM-dd}.xlsx");
#pragma warning restore CS0162
  }

  // GET /Employee/ExportExcel
  [HttpGet]
  public async Task<IActionResult> ExportExcel(string? search, int? statusId, int? roleId, int? projectId)
  {
    var employees = await _employeeService.GetAllAsync(search, statusId, roleId);
    if (projectId.HasValue)
      employees = employees.Where(e => e.Allocations.Any(a => a.ProjectId == projectId.Value)).ToList();

    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("עובדים");

    // Header row
    ws.Cell(1, 1).Value = "קוד עובד";
    ws.Cell(1, 2).Value = "מספר זהות";
    ws.Cell(1, 3).Value = "שם פרטי";
    ws.Cell(1, 4).Value = "שם משפחה";
    ws.Cell(1, 5).Value = "תפקיד";
    ws.Cell(1, 6).Value = "סטטוס";
    ws.Cell(1, 7).Value = "עובד מדווח";
    ws.Cell(1, 8).Value = "מייל";
    ws.Cell(1, 9).Value = "טלפון";
    ws.Cell(1, 10).Value = "דיווח עתידי";
    ws.Cell(1, 11).Value = "פרויקטים";
    ws.Cell(1, 12).Value = "מחוזות";
    ws.Cell(1, 13).Value = "תוכניות";
    ws.Cell(1, 14).Value = "מגזרים";
    ws.Cell(1, 15).Value = "הערות";

    var headerRow = ws.Row(1);
    headerRow.Style.Font.Bold = true;
    headerRow.Style.Fill.BackgroundColor = XLColor.LightBlue;

    // Data rows
    int row = 2;
    foreach (var emp in employees)
    {
      ws.Cell(row, 1).Value = emp.EmployeeCode;
      ws.Cell(row, 2).Value = emp.IdNumber;
      ws.Cell(row, 3).Value = emp.FirstName;
      ws.Cell(row, 4).Value = emp.LastName;
      ws.Cell(row, 5).Value = emp.UserRole?.DescriptionHebrew ?? emp.UserRole?.Name ?? string.Empty;
      ws.Cell(row, 6).Value = emp.Status?.DescriptionHebrew ?? emp.Status?.Name ?? string.Empty;
      ws.Cell(row, 7).Value = emp.IsReportingEmployee ? "כן" : "לא";
      ws.Cell(row, 8).Value = emp.Email;
      ws.Cell(row, 9).Value = emp.Phone;
      ws.Cell(row, 10).Value = emp.AllowFutureReporting ? "כן" : "לא";
      ws.Cell(row, 11).Value = string.Join(", ", emp.Allocations.Select(a => a.Project?.Description).Where(x => x != null).Distinct());
      ws.Cell(row, 12).Value = string.Join(", ", emp.Allocations.SelectMany(a => a.AllocationDistricts).Select(x => x.District?.Description).Where(x => x != null).Distinct());
      ws.Cell(row, 13).Value = string.Join(", ", emp.Allocations.SelectMany(a => a.AllocationPrograms).Select(x => x.Program?.Description).Where(x => x != null).Distinct());
      ws.Cell(row, 14).Value = string.Join(", ", emp.Allocations.SelectMany(a => a.AllocationSectors).Select(x => x.Sector?.Description).Where(x => x != null).Distinct());
      ws.Cell(row, 15).Value = emp.Notes;
      row++;
    }

    ws.Columns().AdjustToContents();

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    stream.Seek(0, SeekOrigin.Begin);

    var fileName = $"employees_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
    return File(stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      fileName);
  }

  private async Task PopulateFormDropdownsAsync()
  {
    var isAdmin = _currentUser.UserRole == UserRoleEnum.SystemAdmin;
    var isPM = _currentUser.UserRole == UserRoleEnum.ProjectManager;

    ViewBag.EmployeeRoles = new SelectList(await _db.Roles.Where(r => r.IsActive).ToListAsync(),
      "Id", "Description");
    var userRoles = _db.UserRoles.AsQueryable();
    if (!isAdmin)
      userRoles = userRoles.Where(r => r.Id != (int)UserRoleEnum.SystemAdmin);
    ViewBag.UserRoles = new SelectList(
      (await userRoles.ToListAsync()).Select(r => new { r.Id, Label = r.DescriptionHebrew ?? r.Name }),
      "Id", "Label");
    ViewBag.Statuses = new SelectList(
      (await _db.UserStatuses.ToListAsync()).Select(s => new { s.Id, Label = s.DescriptionHebrew ?? s.Name }),
      "Id", "Label");
    ViewBag.RestDays = SelectListProviders.RestDayOptions.ToList();
    ViewBag.IsAdminOrPM = isAdmin || isPM;
  }

  private async Task PopulateAllocationDropdownsAsync(int? projectId = null)
  {
    ViewBag.Projects = new SelectList(
      await _db.Projects.Where(p => p.IsActive).ToListAsync(), "Id", "Description");
    ViewBag.Districts = await _db.Districts.Where(d => d.IsActive).ToListAsync();
    ViewBag.Programs = await LoadProgramsForProjectAsync(projectId);
    ViewBag.Sectors = await _db.Sectors.Where(s => s.IsActive).ToListAsync();
    ViewBag.Localities = await _db.Localities.Where(l => l.IsActive).ToListAsync();
    ViewBag.Frameworks = await _db.Frameworks.Where(f => f.IsActive).ToListAsync();
    ViewBag.Subjects = await _db.Subjects.Where(s => s.IsActive).ToListAsync();
    ViewBag.Domains = await _db.Domains.Where(d => d.IsActive).ToListAsync();
    ViewBag.EducationalPrograms = await _db.EducationalPrograms.Where(e => e.IsActive).ToListAsync();
    ViewBag.Classes = await _db.Classes.Where(c => c.IsActive).ToListAsync();
    ViewBag.GradeLevels = await _db.GradeLevels.Where(g => g.IsActive).ToListAsync();
    ViewBag.DiscussionCodes = await _db.DiscussionCodes.Where(d => d.IsActive).ToListAsync();
    ViewBag.LocalityDistrictNationals = await _db.LocalityDistrictNationals.Where(l => l.IsActive).ToListAsync();
  }

  private async Task<List<Core.Entities.Program>> LoadProgramsForProjectAsync(int? projectId)
  {
    if (!projectId.HasValue || projectId.Value <= 0)
      return await _db.Programs.Where(p => p.IsActive).OrderBy(p => p.Description).ToListAsync();

    var mapped = await _db.ProjectPrograms
      .Where(pp => pp.ProjectId == projectId.Value && pp.Program!.IsActive)
      .OrderBy(pp => pp.Program!.Description)
      .Select(pp => pp.Program!)
      .ToListAsync();

    return mapped.Count > 0
      ? mapped
      : await _db.Programs.Where(p => p.IsActive).OrderBy(p => p.Description).ToListAsync();
  }

  /// <summary>
  /// Build route values that preserve the active filter / sort / paging state across a
  /// POST -> RedirectToAction(Index) cycle (client Fix #14).
  /// Reads from <c>Request.Form</c> first (the row-action form may carry hidden filter inputs),
  /// then <c>Request.Query</c> (when the form has no filter inputs but the user came from a
  /// filtered list URL).
  /// </summary>
  private Dictionary<string, object?> CurrentFilterRouteValues()
  {
    var rv = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

    void TryAdd(string key, string? value)
    {
      if (string.IsNullOrEmpty(value)) return;
      if (rv.ContainsKey(key)) return;
      rv[key] = value;
    }

    void TryAddMulti(string key, IEnumerable<string?> values)
    {
      var list = values.Where(v => !string.IsNullOrEmpty(v)).ToList();
      if (list.Count == 0) return;
      var i = 0;
      foreach (var v in list)
      {
        rv[$"{key}[{i++}]"] = v;
      }
    }

    string[] textKeys = {
      "search", "idNumber", "employeeCode", "firstName", "lastName", "notes",
      "statusId", "roleId", "restDay", "allowFutureReporting", "hasAllocations",
      "lockedOnly", "projectId", "sortBy", "sortDesc", "page", "pageSize"
    };
    string[] multiKeys = { "districtIds", "programIds", "sectorIds" };

    if (Request?.HasFormContentType == true)
    {
      foreach (var k in textKeys)
        if (Request.Form.TryGetValue(k, out var v) && !string.IsNullOrEmpty(v.ToString()))
          TryAdd(k, v.ToString());
      foreach (var k in multiKeys)
        if (Request.Form.TryGetValue(k, out var v))
          TryAddMulti(k, v);
    }

    if (Request != null)
    {
      foreach (var k in textKeys)
        if (Request.Query.TryGetValue(k, out var v) && !string.IsNullOrEmpty(v.ToString()))
          TryAdd(k, v.ToString());
      foreach (var k in multiKeys)
        if (Request.Query.TryGetValue(k, out var v))
          TryAddMulti(k, v);
    }

    return rv;
  }

  private IActionResult RedirectToFilteredIndex() =>
    RedirectToAction(nameof(Index), CurrentFilterRouteValues());

  private static Dictionary<string, object?> AllocationRouteValues(AllocationListFilterModel filter)
  {
    filter.Normalize();
    var values = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase)
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
      ["showAll"] = filter.ShowAll ? true : null,
      ["sortBy"] = filter.SortBy,
      ["sortDesc"] = filter.SortDesc ? true : null,
      ["page"] = filter.Page > 1 ? filter.Page : null,
      ["pageSize"] = filter.PageSize != 25 ? filter.PageSize : null
    };

    AddIndexed(values, "programIds", filter.ProgramIds);
    AddIndexed(values, "districtIds", filter.DistrictIds);
    AddIndexed(values, "sectorIds", filter.SectorIds);
    AddIndexed(values, "outputDurations", filter.OutputDurations);
    return values;
  }

  private static void AddIndexed<T>(Dictionary<string, object?> values, string key, IReadOnlyList<T> items)
  {
    for (var i = 0; i < items.Count; i++)
      values[$"{key}[{i}]"] = items[i];
  }

  private void AddValidationErrors(FluentValidation.Results.ValidationResult result)
  {
    foreach (var error in result.Errors)
    {
      if (ModelState.TryGetValue(error.PropertyName, out var entry) &&
          entry.Errors.Any(e => e.ErrorMessage == error.ErrorMessage))
        continue;

      ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
    }
  }

  private void NormalizeRequestedUserRole(EmployeeDto dto)
  {
    if (_currentUser.UserRole != UserRoleEnum.SystemAdmin &&
        dto.UserRoleId == (int)UserRoleEnum.SystemAdmin)
    {
      dto.UserRoleId = (int)UserRoleEnum.Employee;
      ModelState.AddModelError(nameof(dto.UserRoleId), "רק מנהל מערכת יכול להגדיר מנהל מערכת");
    }
  }

  internal static List<User> ApplyEmployeeSort(List<User> employees, string? sortBy, bool sortDesc)
  {
    // Each visible column on the employee list must be sortable (client Fix #16).
    Func<User, object?> key = sortBy?.ToLowerInvariant() switch
    {
      "code" or "employeecode" => u => u.EmployeeCode,
      "idnumber" => u => u.IdNumber,
      "firstname" => u => u.FirstName,
      "lastname" => u => u.LastName,
      "name" => u => u.LastName + " " + u.FirstName,
      "role" or "userrole" or "userroleid" => u => u.UserRole?.Name,
      "employeerole" or "employeeroleid" or "roleid" => u => u.Role?.Description,
      "status" or "statusid" => u => u.Status?.Name,
      "isreportingemployee" => u => u.IsReportingEmployee,
      "locked" => u => u.StatusId == (int)UserStatusEnum.Locked || u.FailedLoginAttempts >= 3,
      "email" => u => u.Email,
      "phone" => u => u.Phone,
      "allowfuturereporting" => u => u.AllowFutureReporting,
      "restday" => u => u.RestDay,
      "projects" or "project" => u => u.Allocations
        .Where(a => a.IsActive)
        .Select(a => a.Project?.Description)
        .Where(v => !string.IsNullOrWhiteSpace(v))
        .OrderBy(v => v)
        .FirstOrDefault(),
      "districts" or "district" => u => u.Allocations
        .Where(a => a.IsActive)
        .SelectMany(a => a.AllocationDistricts)
        .Select(x => x.District?.Description)
        .Where(v => !string.IsNullOrWhiteSpace(v))
        .OrderBy(v => v)
        .FirstOrDefault(),
      "programs" or "program" => u => u.Allocations
        .Where(a => a.IsActive)
        .SelectMany(a => a.AllocationPrograms)
        .Select(x => x.Program?.Description)
        .Where(v => !string.IsNullOrWhiteSpace(v))
        .OrderBy(v => v)
        .FirstOrDefault(),
      "sectors" or "sector" => u => u.Allocations
        .Where(a => a.IsActive)
        .SelectMany(a => a.AllocationSectors)
        .Select(x => x.Sector?.Description)
        .Where(v => !string.IsNullOrWhiteSpace(v))
        .OrderBy(v => v)
        .FirstOrDefault(),
      "notes" => u => u.Notes,
      _ => u => u.LastName + " " + u.FirstName
    };

    return (sortDesc ? employees.OrderByDescending(key) : employees.OrderBy(key)).ToList();
  }

  private IQueryable<Allocation> AllocationListQuery(AllocationListFilterModel filter) =>
    BuildAllocationListQuery(_db, filter);

  /// <summary>
  /// Composes the filtered allocation list query. Exposed internally so that it can be covered
  /// by unit tests against an in-memory AppDbContext.
  /// </summary>
  internal static IQueryable<Allocation> BuildAllocationListQuery(AppDbContext db, AllocationListFilterModel filter)
  {
    var query = db.Allocations
      .Include(a => a.User)
      .Include(a => a.Project)
      .Include(a => a.AllocationDistricts).ThenInclude(x => x.District)
      .Include(a => a.AllocationPrograms).ThenInclude(x => x.Program)
      .Include(a => a.AllocationSectors).ThenInclude(x => x.Sector)
      .Where(a => a.IsActive)
      .AsQueryable();

    if (filter.ProjectId.HasValue)
      query = query.Where(a => a.ProjectId == filter.ProjectId.Value);

    if (!string.IsNullOrWhiteSpace(filter.Search))
    {
      var search = filter.Search;
      query = query.Where(a =>
        a.User!.EmployeeCode.Contains(search) ||
        a.User.IdNumber.Contains(search) ||
        (a.User.FirstName + " " + a.User.LastName).Contains(search) ||
        a.Project!.Description.Contains(search));
    }

    if (filter.ProgramIds is { Count: > 0 })
      query = query.Where(a => a.AllocationPrograms.Any(ap => filter.ProgramIds.Contains(ap.ProgramId)));
    if (filter.DistrictIds is { Count: > 0 })
      query = query.Where(a => a.AllocationDistricts.Any(ad => filter.DistrictIds.Contains(ad.DistrictId)));
    if (filter.SectorIds is { Count: > 0 })
      query = query.Where(a => a.AllocationSectors.Any(asc => filter.SectorIds.Contains(asc.SectorId)));

    if (!string.IsNullOrWhiteSpace(filter.IdNumber))
      query = query.Where(a => a.User!.IdNumber.Contains(filter.IdNumber));
    if (!string.IsNullOrWhiteSpace(filter.EmployeeCode))
      query = query.Where(a => a.User!.EmployeeCode.Contains(filter.EmployeeCode));
    if (!string.IsNullOrWhiteSpace(filter.FirstName))
      query = query.Where(a => a.User!.FirstName.Contains(filter.FirstName));
    if (!string.IsNullOrWhiteSpace(filter.LastName))
      query = query.Where(a => a.User!.LastName.Contains(filter.LastName));

    if (filter.MonthlyEmploymentScope.HasValue)
      query = query.Where(a => a.MonthlyEmploymentScope == filter.MonthlyEmploymentScope.Value);
    if (filter.AnnualEmploymentScope.HasValue)
      query = query.Where(a => a.AnnualEmploymentScope == filter.AnnualEmploymentScope.Value);

    if (filter.OutputDurations is { Count: > 0 })
    {
      foreach (var duration in filter.OutputDurations)
      {
        var token = "," + duration + ",";
        query = query.Where(a => a.OutputDuration != null && ("," + a.OutputDuration + ",").Contains(token));
      }
    }

    if (!string.IsNullOrWhiteSpace(filter.Notes))
      query = query.Where(a => a.Notes != null && a.Notes.Contains(filter.Notes));

    return query;
  }

  /// <summary>
  /// Returns the set of Programs (id + description) that should populate the Program dropdown for a given project.
  /// Falls back to ALL active programs when no ProjectPrograms mapping exists (backwards-compatible default).
  /// </summary>
  internal static async Task<List<(int Id, string Description)>> GetProgramsForProjectAsync(AppDbContext db, int projectId)
  {
    if (projectId <= 0)
      return await db.Programs
        .Where(p => p.IsActive)
        .OrderBy(p => p.Description)
        .Select(p => new ValueTuple<int, string>(p.Id, p.Description))
        .ToListAsync();

    var mapped = await db.ProjectPrograms
      .Where(pp => pp.ProjectId == projectId && pp.Program!.IsActive)
      .OrderBy(pp => pp.Program!.Description)
      .Select(pp => new ValueTuple<int, string>(pp.ProgramId, pp.Program!.Description))
      .ToListAsync();

    if (mapped.Count > 0) return mapped;

    return await db.Programs
      .Where(p => p.IsActive)
      .OrderBy(p => p.Description)
      .Select(p => new ValueTuple<int, string>(p.Id, p.Description))
      .ToListAsync();
  }

  // GET /Employee/ProgramsForProject?projectId=N - JSON endpoint for cascading Program dropdown.
  [HttpGet]
  [Authorize(Policy = PolicyNames.AdminPMOrCoordinator)]
  public async Task<IActionResult> ProgramsForProject(int projectId)
  {
    if (projectId <= 0) return Json(Array.Empty<object>());
    var result = await GetProgramsForProjectAsync(_db, projectId);
    return Json(result.Select(p => new { id = p.Id, description = p.Description }));
  }

  internal static IOrderedQueryable<Allocation> ApplyAllocationSort(
    IQueryable<Allocation> query, string? sortBy, bool sortDesc)
  {
    // Every visible column on the allocation list must be sortable (client Fix #16).
    return sortBy?.ToLowerInvariant() switch
    {
      "code" or "employeecode" => sortDesc ? query.OrderByDescending(a => a.User!.EmployeeCode) : query.OrderBy(a => a.User!.EmployeeCode),
      "idnumber" => sortDesc ? query.OrderByDescending(a => a.User!.IdNumber) : query.OrderBy(a => a.User!.IdNumber),
      "firstname" => sortDesc ? query.OrderByDescending(a => a.User!.FirstName) : query.OrderBy(a => a.User!.FirstName),
      "lastname" => sortDesc ? query.OrderByDescending(a => a.User!.LastName) : query.OrderBy(a => a.User!.LastName),
      "name" => sortDesc
        ? query.OrderByDescending(a => a.User!.LastName).ThenByDescending(a => a.User!.FirstName)
        : query.OrderBy(a => a.User!.LastName).ThenBy(a => a.User!.FirstName),
      "project" or "projectid" => sortDesc ? query.OrderByDescending(a => a.Project!.Description) : query.OrderBy(a => a.Project!.Description),
      "annualscope" or "annualemploymentscope" => sortDesc ? query.OrderByDescending(a => a.AnnualEmploymentScope) : query.OrderBy(a => a.AnnualEmploymentScope),
      "monthlyscope" or "monthlyemploymentscope" => sortDesc ? query.OrderByDescending(a => a.MonthlyEmploymentScope) : query.OrderBy(a => a.MonthlyEmploymentScope),
      "dailyscope" or "dailyemploymentscope" => sortDesc ? query.OrderByDescending(a => a.DailyEmploymentScope) : query.OrderBy(a => a.DailyEmploymentScope),
      "monthlyrows" or "monthlyrowallocation" => sortDesc ? query.OrderByDescending(a => a.MonthlyRowAllocation) : query.OrderBy(a => a.MonthlyRowAllocation),
      "annualrows" or "annualrowallocation" => sortDesc ? query.OrderByDescending(a => a.AnnualRowAllocation) : query.OrderBy(a => a.AnnualRowAllocation),
      "districts" => sortDesc
        ? query.OrderByDescending(a => a.AllocationDistricts.Select(x => x.District!.Description).FirstOrDefault())
        : query.OrderBy(a => a.AllocationDistricts.Select(x => x.District!.Description).FirstOrDefault()),
      "programs" => sortDesc
        ? query.OrderByDescending(a => a.AllocationPrograms.Select(x => x.Program!.Description).FirstOrDefault())
        : query.OrderBy(a => a.AllocationPrograms.Select(x => x.Program!.Description).FirstOrDefault()),
      "sectors" => sortDesc
        ? query.OrderByDescending(a => a.AllocationSectors.Select(x => x.Sector!.Description).FirstOrDefault())
        : query.OrderBy(a => a.AllocationSectors.Select(x => x.Sector!.Description).FirstOrDefault()),
      "outputduration" => sortDesc ? query.OrderByDescending(a => a.OutputDuration) : query.OrderBy(a => a.OutputDuration),
      "allowexcelupload" => sortDesc ? query.OrderByDescending(a => a.AllowExcelUpload) : query.OrderBy(a => a.AllowExcelUpload),
      "notes" => sortDesc ? query.OrderByDescending(a => a.Notes) : query.OrderBy(a => a.Notes),
      _ => sortDesc
        ? query.OrderByDescending(a => a.User!.LastName).ThenByDescending(a => a.User!.FirstName)
        : query.OrderBy(a => a.User!.LastName).ThenBy(a => a.User!.FirstName)
    };
  }
}

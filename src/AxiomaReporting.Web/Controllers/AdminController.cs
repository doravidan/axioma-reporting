using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Policy = PolicyNames.AdminOrPM)]
public class AdminController : Controller
{
  private readonly AppDbContext _db;
  private readonly IPasswordService _passwordService;
  private readonly IBatchReportImportService _batchImportService;
  private readonly IPdfReportService _pdfReportService;
  private readonly IEmailService _emailService;
  private readonly IBrandingService _brandingService;
  private readonly IAuditLogService _auditLog;
  private readonly IWebHostEnvironment _hostEnvironment;

  public AdminController(
    AppDbContext db,
    IPasswordService passwordService,
    IBatchReportImportService batchImportService,
    IPdfReportService pdfReportService,
    IEmailService emailService,
    IBrandingService brandingService,
    IAuditLogService auditLog,
    IWebHostEnvironment hostEnvironment)
  {
    _db = db;
    _passwordService = passwordService;
    _batchImportService = batchImportService;
    _pdfReportService = pdfReportService;
    _emailService = emailService;
    _brandingService = brandingService;
    _auditLog = auditLog;
    _hostEnvironment = hostEnvironment;
  }

  // --- Reporting Months ---

  public async Task<IActionResult> ReportingMonths()
  {
    var months = await _db.ReportingMonths
      .OrderByDescending(m => m.Year)
      .ThenByDescending(m => m.Month)
      .ToListAsync();
    return View(months);
  }

  [HttpPost, ValidateAntiForgeryToken]
  public async Task<IActionResult> CreateReportingMonth(int month, int year, string description,
    DateTime lastReportingDate, bool allowFutureReporting = false)
  {
    var model = new ReportingMonth
    {
      Month = month,
      Year = year,
      Description = description,
      LastReportingDate = lastReportingDate,
      AllowFutureReporting = allowFutureReporting,
      IsActive = false,
      CreatedAt = DateTime.UtcNow
    };
    _db.ReportingMonths.Add(model);
    await _db.SaveChangesAsync();
    TempData["Success"] = "חודש דיווח נוצר";
    return RedirectToAction(nameof(ReportingMonths));
  }

  [HttpPost, ValidateAntiForgeryToken]
  public async Task<IActionResult> ActivateReportingMonth(int id)
  {
    // Only one reporting month can be active at a time (business rule #4)
    await _db.ReportingMonths.ExecuteUpdateAsync(s => s.SetProperty(m => m.IsActive, false));
    var month = await _db.ReportingMonths.FindAsync(id);
    if (month != null)
    {
      month.IsActive = true;
      month.UpdatedAt = DateTime.UtcNow;
      await _db.SaveChangesAsync();
    }
    TempData["Success"] = "חודש הדיווח הופעל";
    return RedirectToAction(nameof(ReportingMonths));
  }

  [HttpPost, ValidateAntiForgeryToken]
  public async Task<IActionResult> DeactivateReportingMonth(int id)
  {
    var month = await _db.ReportingMonths.FindAsync(id);
    if (month != null)
    {
      month.IsActive = false;
      month.UpdatedAt = DateTime.UtcNow;
      await _db.SaveChangesAsync();
    }
    TempData["Success"] = "חודש הדיווח הושבת";
    return RedirectToAction(nameof(ReportingMonths));
  }

  // --- Reporting Month Edit ---
  // Coordinators (role 3) may reach this screen but only Admin (1) / PM (2)
  // can change LastReportingDate / AllowFutureReporting on an active month.

  [HttpGet]
  [Authorize(Policy = PolicyNames.AdminPMOrCoordinator)]
  public async Task<IActionResult> EditReportingMonth(int id)
  {
    var month = await _db.ReportingMonths.FindAsync(id);
    if (month == null)
    {
      TempData["Error"] = "חודש הדיווח לא נמצא";
      return RedirectToAction(nameof(ReportingMonths));
    }

    var vm = new ReportingMonthEditViewModel
    {
      Id = month.Id,
      Description = month.Description,
      Month = month.Month,
      Year = month.Year,
      LastReportingDate = month.LastReportingDate,
      AllowFutureReporting = month.AllowFutureReporting,
      IsActive = month.IsActive,
      LockNonAdminFields = month.IsActive && !IsAdminOrProjectManager()
    };

    return View(vm);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminPMOrCoordinator)]
  public async Task<IActionResult> EditReportingMonth(ReportingMonthEditViewModel input)
  {
    var month = await _db.ReportingMonths.FindAsync(input.Id);
    if (month == null)
    {
      TempData["Error"] = "חודש הדיווח לא נמצא";
      return RedirectToAction(nameof(ReportingMonths));
    }

    var lockNonAdmin = month.IsActive && !IsAdminOrProjectManager();

    // Always-editable fields
    month.Description = string.IsNullOrWhiteSpace(input.Description) ? month.Description : input.Description.Trim();
    month.Month = input.Month;
    month.Year = input.Year;

    if (lockNonAdmin)
    {
      // Ignore inbound LastReportingDate / AllowFutureReporting — keep the DB values.
      // (Already loaded into `month`, so nothing to do.)
    }
    else
    {
      month.LastReportingDate = input.LastReportingDate;
      month.AllowFutureReporting = input.AllowFutureReporting;
    }

    month.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();
    TempData["Success"] = "חודש הדיווח עודכן";
    return RedirectToAction(nameof(ReportingMonths));
  }

  private bool IsAdminOrProjectManager()
  {
    var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
    return role == "1" || role == "2";
  }

  // --- Frameworks ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> Frameworks()
  {
    var items = await _db.Frameworks
      .Include(f => f.EducationalStage)
      .OrderBy(f => f.Description)
      .ToListAsync();
    ViewBag.EducationalStages = await _db.EducationalStages.Where(s => s.IsActive).ToListAsync();
    return View(items);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> CreateFramework(string description, string institutionSymbol,
    int? educationalStageId)
  {
    if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(institutionSymbol))
    {
      TempData["Error"] = "יש להזין תיאור וסמל מוסד";
      return RedirectToAction(nameof(Frameworks));
    }
    // Institution symbol must be unique per educational stage (business rule #5)
    var exists = await _db.Frameworks.AnyAsync(f =>
      f.InstitutionSymbol == institutionSymbol && f.EducationalStageId == educationalStageId);
    if (exists)
    {
      TempData["Error"] = "סמל מוסד זה כבר קיים עבור שלב חינוך זה";
      return RedirectToAction(nameof(Frameworks));
    }
    _db.Frameworks.Add(new Framework
    {
      Description = description.Trim(),
      InstitutionSymbol = institutionSymbol.Trim(),
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
      TempData["Error"] = "סמל מוסד זה כבר קיים עבור שלב חינוך זה";
      return RedirectToAction(nameof(Frameworks));
    }
    TempData["Success"] = "מסגרת נוצרה";
    return RedirectToAction(nameof(Frameworks));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> EditFramework(int id, string description, string institutionSymbol,
    int? educationalStageId, bool isActive)
  {
    var framework = await _db.Frameworks.FindAsync(id);
    if (framework == null)
    {
      TempData["Error"] = "מסגרת לא נמצאה";
      return RedirectToAction(nameof(Frameworks));
    }
    var symbolTaken = await _db.Frameworks.AnyAsync(f =>
      f.Id != id && f.InstitutionSymbol == institutionSymbol && f.EducationalStageId == educationalStageId);
    if (symbolTaken)
    {
      TempData["Error"] = "סמל מוסד זה כבר קיים עבור שלב חינוך זה";
      return RedirectToAction(nameof(Frameworks));
    }
    framework.Description = description;
    framework.InstitutionSymbol = institutionSymbol;
    framework.EducationalStageId = educationalStageId;
    framework.IsActive = isActive;
    framework.UpdatedAt = DateTime.UtcNow;
    try
    {
      await _db.SaveChangesAsync();
    }
    catch (DbUpdateException)
    {
      TempData["Error"] = "סמל מוסד זה כבר קיים עבור שלב חינוך זה";
      return RedirectToAction(nameof(Frameworks));
    }
    TempData["Success"] = "מסגרת עודכנה";
    return RedirectToAction(nameof(Frameworks));
  }

  // --- Institutions ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> Institutions()
  {
    var items = await _db.Institutions
      .Include(i => i.Locality)
      .Include(i => i.District)
      .Include(i => i.Sector)
      .Include(i => i.Type)
      .Include(i => i.EducationalStage)
      .OrderBy(i => i.Name)
      .ToListAsync();
    await LoadInstitutionDropdowns();
    return View(items);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> CreateInstitution(int institutionSymbol, string name,
    int? localityId, int? districtId, int? sectorId, int? typeId, int? educationalStageId)
  {
    // Institution symbol must be unique per educational stage (business rule #5)
    var exists = await _db.Institutions.AnyAsync(i =>
      i.InstitutionSymbol == institutionSymbol && i.EducationalStageId == educationalStageId);
    if (exists)
    {
      TempData["Error"] = "סמל מוסד כבר קיים עבור שלב חינוך זה";
      return RedirectToAction(nameof(Institutions));
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
    TempData["Success"] = "מוסד נוצר";
    return RedirectToAction(nameof(Institutions));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> EditInstitution(int id, int institutionSymbol, string name,
    int? localityId, int? districtId, int? sectorId, int? typeId, int? educationalStageId, bool isActive)
  {
    var institution = await _db.Institutions.FindAsync(id);
    if (institution == null)
    {
      TempData["Error"] = "מוסד לא נמצא";
      return RedirectToAction(nameof(Institutions));
    }
    var symbolTaken = await _db.Institutions.AnyAsync(i =>
      i.Id != id && i.InstitutionSymbol == institutionSymbol && i.EducationalStageId == educationalStageId);
    if (symbolTaken)
    {
      TempData["Error"] = "סמל מוסד כבר קיים עבור שלב חינוך זה";
      return RedirectToAction(nameof(Institutions));
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
    TempData["Success"] = "מוסד עודכן";
    return RedirectToAction(nameof(Institutions));
  }

  // --- System Constants ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> SystemConstants()
  {
    var constants = await _db.SystemConstants.OrderBy(c => c.Key).ToListAsync();
    return View(constants);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> UpdateSystemConstant(int id, string value)
  {
    var constant = await _db.SystemConstants.FindAsync(id);
    if (constant != null)
    {
      var before = new { constant.Key, constant.Value };
      constant.Value = value;
      constant.UpdatedAt = DateTime.UtcNow;
      await _db.SaveChangesAsync();
      await _auditLog.LogAsync("SystemConstant.Update", nameof(SystemConstant), id.ToString(),
        before: before,
        after: new { constant.Key, constant.Value });
    }
    TempData["Success"] = "הערך עודכן";
    return RedirectToAction(nameof(SystemConstants));
  }

  // --- Branding (logo upload) ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> Branding()
  {
    ViewBag.CurrentLogoPath = await _brandingService.GetLogoPathAsync();
    return View();
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  [RequestSizeLimit(2L * 1024 * 1024 + 4096)] // 2 MB + a small header allowance
  public async Task<IActionResult> UploadLogo(IFormFile logoFile)
  {
    const long maxBytes = 2L * 1024 * 1024;
    var allowed = new[] { ".png", ".svg", ".jpg", ".jpeg" };

    if (logoFile == null || logoFile.Length == 0)
    {
      TempData["Error"] = "לא נבחר קובץ לוגו";
      return RedirectToAction(nameof(Branding));
    }
    if (logoFile.Length > maxBytes)
    {
      TempData["Error"] = "גודל הקובץ חורג מהמותר (מקסימום 2 מ\"ב)";
      return RedirectToAction(nameof(Branding));
    }

    var ext = Path.GetExtension(logoFile.FileName).ToLowerInvariant();
    if (!allowed.Contains(ext))
    {
      TempData["Error"] = "סוג קובץ לא נתמך. מותר: PNG, SVG, JPG";
      return RedirectToAction(nameof(Branding));
    }

    var webRoot = _hostEnvironment.WebRootPath;
    if (string.IsNullOrEmpty(webRoot))
    {
      webRoot = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot");
    }

    var brandingDir = Path.Combine(webRoot, "uploads", "branding");
    Directory.CreateDirectory(brandingDir);

    // Normalize ".jpeg" filenames to ".jpg" for the stored asset.
    var storedExt = ext == ".jpeg" ? ".jpg" : ext;
    var fileName = "site-logo" + storedExt;
    var physicalPath = Path.Combine(brandingDir, fileName);

    // Remove any older logo in a different extension so only the active file remains.
    foreach (var e in allowed.Select(a => a == ".jpeg" ? ".jpg" : a).Distinct())
    {
      var stale = Path.Combine(brandingDir, "site-logo" + e);
      if (!string.Equals(stale, physicalPath, StringComparison.OrdinalIgnoreCase) && System.IO.File.Exists(stale))
      {
        try { System.IO.File.Delete(stale); } catch { /* best-effort */ }
      }
    }

    await using (var fs = System.IO.File.Create(physicalPath))
    {
      await logoFile.CopyToAsync(fs);
    }

    var publicPath = $"/uploads/branding/{fileName}";
    var actorId = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var uid) ? uid : (int?)null;
    await _brandingService.SetLogoPathAsync(publicPath, actorId);

    TempData["Success"] = "הלוגו עודכן בהצלחה";
    return RedirectToAction(nameof(Branding));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ResetLogo()
  {
    var actorId = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var uid) ? uid : (int?)null;
    await _brandingService.SetLogoPathAsync(IBrandingService.DefaultLogoPath, actorId);
    TempData["Success"] = "הלוגו הוחזר לברירת המחדל";
    return RedirectToAction(nameof(Branding));
  }

  // --- Email Templates ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> EmailTemplates()
  {
    var templates = await _db.EmailTemplates
      .Where(t => t.IsActive)
      .OrderBy(t => t.TypeDescription)
      .ToListAsync();
    return View(templates);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> UpdateEmailTemplate(int id, string subject, string body)
  {
    var template = await _db.EmailTemplates.FindAsync(id);
    if (template != null)
    {
      var before = new { template.Subject, template.Body };
      template.Subject = subject;
      template.Body = body;
      template.UpdatedAt = DateTime.UtcNow;
      await _db.SaveChangesAsync();
      await _auditLog.LogAsync("EmailTemplate.Update", nameof(EmailTemplate), id.ToString(),
        before: before,
        after: new { template.Subject, template.Body });
    }
    TempData["Success"] = "תבנית עודכנה";
    return RedirectToAction(nameof(EmailTemplates));
  }

  // --- Email Server Settings ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> EmailServerSettings()
  {
    var settings = await _db.EmailServerSettings.FirstOrDefaultAsync();
    return View(settings);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> SaveEmailServerSettings(string smtpServer, int port, string username,
    string? password, string fromAddress, string? fromName, bool useSsl)
  {
    var existing = await _db.EmailServerSettings.FirstOrDefaultAsync();
    object? before = null;
    if (existing == null)
    {
      _db.EmailServerSettings.Add(new EmailServerSetting
      {
        SmtpServer = smtpServer,
        Port = port,
        Username = username,
        Password = password ?? string.Empty,
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
      if (!string.IsNullOrEmpty(password)) existing.Password = password;
      existing.FromAddress = fromAddress;
      existing.FromName = fromName;
      existing.UseSsl = useSsl;
      existing.UpdatedAt = DateTime.UtcNow;
    }
    await _db.SaveChangesAsync();
    // Never include the SMTP password in audit payloads.
    await _auditLog.LogAsync("EmailServerSetting.Update", nameof(EmailServerSetting), existing?.Id.ToString(),
      before: before,
      after: new { smtpServer, port, username, fromAddress, fromName, useSsl });
    TempData["Success"] = "הגדרות SMTP נשמרו";
    return RedirectToAction(nameof(EmailServerSettings));
  }

  // --- Inspector Assignments ---

  [Authorize(Policy = PolicyNames.AdminOrPM)]
  public async Task<IActionResult> InspectorAssignments()
  {
    ViewBag.Assignments = await _db.InspectorAssignments
      .Include(a => a.Inspector)
      .Include(a => a.Program)
      .Include(a => a.District)
      .Include(a => a.Sector)
      .OrderBy(a => a.Inspector!.LastName)
      .ToListAsync();
    ViewBag.Inspectors = await _db.Users
      .Where(u => u.UserRoleId == 4 || u.UserRoleId == 5)
      .OrderBy(u => u.LastName)
      .ThenBy(u => u.FirstName)
      .ToListAsync();
    ViewBag.Programs = await _db.Programs.Where(p => p.IsActive).OrderBy(p => p.Description).ToListAsync();
    ViewBag.Districts = await _db.Districts.Where(d => d.IsActive).OrderBy(d => d.Description).ToListAsync();
    ViewBag.Sectors = await _db.Sectors.Where(s => s.IsActive).OrderBy(s => s.Description).ToListAsync();
    return View();
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOrPM)]
  public async Task<IActionResult> CreateInspectorAssignment(
    int inspectorUserId, int? programId, int? districtId, int? sectorId)
  {
    var assignment = new InspectorAssignment
    {
      InspectorUserId = inspectorUserId,
      ProgramId = programId,
      DistrictId = districtId,
      SectorId = sectorId
    };
    _db.InspectorAssignments.Add(assignment);
    await _db.SaveChangesAsync();
    await _auditLog.LogAsync("InspectorAssignment.Create", nameof(InspectorAssignment), assignment.Id.ToString(),
      after: new { assignment.InspectorUserId, assignment.ProgramId, assignment.DistrictId, assignment.SectorId });
    TempData["Success"] = "שיוך המפקח נוסף";
    return RedirectToAction(nameof(InspectorAssignments));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOrPM)]
  public async Task<IActionResult> DeleteInspectorAssignment(int id)
  {
    var assignment = await _db.InspectorAssignments.FindAsync(id);
    if (assignment != null)
    {
      var before = new { assignment.InspectorUserId, assignment.ProgramId, assignment.DistrictId, assignment.SectorId };
      _db.InspectorAssignments.Remove(assignment);
      await _db.SaveChangesAsync();
      await _auditLog.LogAsync("InspectorAssignment.Delete", nameof(InspectorAssignment), id.ToString(),
        before: before);
    }
    TempData["Success"] = "שיוך המפקח נמחק";
    return RedirectToAction(nameof(InspectorAssignments));
  }

  // --- Project ↔ Programs mapping ---

  [HttpGet]
  [Route("Admin/ProjectPrograms")]
  public async Task<IActionResult> ProjectPrograms()
  {
    var projects = await _db.Projects
      .Where(p => p.IsActive)
      .OrderBy(p => p.Description)
      .ToListAsync();

    var programs = await _db.Programs
      .Where(p => p.IsActive)
      .OrderBy(p => p.Description)
      .ToListAsync();

    var projectPrograms = await _db.ProjectPrograms
      .AsNoTracking()
      .ToListAsync();

    var mapping = projectPrograms
      .GroupBy(pp => pp.ProjectId)
      .ToDictionary(g => g.Key, g => g.Select(x => x.ProgramId).ToHashSet());

    ViewBag.Projects = projects;
    ViewBag.Programs = programs;
    ViewBag.Mapping = mapping;
    return View();
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Route("Admin/ProjectPrograms/Save")]
  public async Task<IActionResult> SaveProjectPrograms(int projectId, int[]? programIds)
  {
    var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == projectId && p.IsActive);
    if (project == null)
    {
      TempData["Error"] = "פרויקט לא נמצא";
      return RedirectToAction(nameof(ProjectPrograms));
    }

    var desired = (programIds ?? Array.Empty<int>()).Where(x => x > 0).Distinct().ToHashSet();

    // Intersect with active programs to ignore stale ids from form tampering.
    if (desired.Count > 0)
    {
      var validIds = await _db.Programs
        .Where(p => p.IsActive && desired.Contains(p.Id))
        .Select(p => p.Id)
        .ToListAsync();
      desired = validIds.ToHashSet();
    }

    var existing = await _db.ProjectPrograms
      .Where(pp => pp.ProjectId == projectId)
      .ToListAsync();

    // Remove rows that are no longer desired.
    var toRemove = existing.Where(e => !desired.Contains(e.ProgramId)).ToList();
    if (toRemove.Count > 0) _db.ProjectPrograms.RemoveRange(toRemove);

    // Add rows that don't yet exist.
    var existingIds = existing.Select(e => e.ProgramId).ToHashSet();
    foreach (var programId in desired.Where(id => !existingIds.Contains(id)))
      _db.ProjectPrograms.Add(new ProjectProgram { ProjectId = projectId, ProgramId = programId });

    await _db.SaveChangesAsync();
    TempData["Success"] = $"נשמרו {desired.Count} תוכניות עבור פרויקט '{project.Description}'";
    return RedirectToAction(nameof(ProjectPrograms));
  }

  // --- AX-024: Data Migration Tool ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public IActionResult DataMigration()
  {
    return View();
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ImportLookups(IFormFile? file)
  {
    if (!ValidateXlsxUpload(file))
    {
      return RedirectToAction(nameof(DataMigration));
    }

    var results = new List<string>();
    var now = DateTime.UtcNow;

    using var stream = file!.OpenReadStream();
    using var wb = new XLWorkbook(stream);

    var sheetHandlers = new Dictionary<string, Func<IXLWorksheet, Task<int>>>(StringComparer.OrdinalIgnoreCase)
    {
      ["מחוזות"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Districts.AnyAsync(x => x.Description == desc))
        { _db.Districts.Add(new District { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["מגזרים"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Sectors.AnyAsync(x => x.Description == desc))
        { _db.Sectors.Add(new Sector { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["ישובים"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Localities.AnyAsync(x => x.Description == desc))
        { _db.Localities.Add(new Locality { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["רשויות"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Authorities.AnyAsync(x => x.Description == desc))
        { _db.Authorities.Add(new Authority { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["פרויקטים"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Projects.AnyAsync(x => x.Description == desc))
        { _db.Projects.Add(new Project { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["תוכניות"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Programs.AnyAsync(x => x.Description == desc))
        { _db.Programs.Add(new AxiomaReporting.Core.Entities.Program { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["נושאים"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Subjects.AnyAsync(x => x.Description == desc))
        { _db.Subjects.Add(new Subject { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["תחומים"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Domains.AnyAsync(x => x.Description == desc))
        { _db.Domains.Add(new Domain { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["תוכניות חינוכיות"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.EducationalPrograms.AnyAsync(x => x.Description == desc))
        { _db.EducationalPrograms.Add(new EducationalProgram { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["שכבות"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.GradeLevels.AnyAsync(x => x.Description == desc))
        { _db.GradeLevels.Add(new GradeLevel { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["כיתות"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.Classes.AnyAsync(x => x.Description == desc))
        { _db.Classes.Add(new SchoolClass { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["קודי דיון"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.DiscussionCodes.AnyAsync(x => x.Description == desc))
        { _db.DiscussionCodes.Add(new DiscussionCode { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["ישוב מחוז ארצי"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.LocalityDistrictNationals.AnyAsync(x => x.Description == desc))
        { _db.LocalityDistrictNationals.Add(new LocalityDistrictNational { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["שלבי חינוך"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.EducationalStages.AnyAsync(x => x.Description == desc))
        { _db.EducationalStages.Add(new EducationalStage { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
      ["רמות השכלה"] = ws => ImportSimpleAsync(ws, async desc =>
      {
        if (!await _db.EducationTypes.AnyAsync(x => x.Description == desc))
        { _db.EducationTypes.Add(new EducationType { Description = desc, IsActive = true, CreatedAt = now }); return true; }
        return false;
      }),
    };

    foreach (var ws in wb.Worksheets)
    {
      var sheetName = ws.Name.Trim();
      if (sheetName.Equals("מסגרות", StringComparison.OrdinalIgnoreCase))
      {
        var count = await ImportFrameworksAsync(ws);
        await _db.SaveChangesAsync();
        results.Add($"גיליון '{sheetName}': {count} רשומות נוספו");
      }
      else if (sheetHandlers.TryGetValue(sheetName, out var handler))
      {
        var count = await handler(ws);
        await _db.SaveChangesAsync();
        results.Add($"גיליון '{sheetName}': {count} רשומות נוספו");
      }
      else
      {
        results.Add($"גיליון '{sheetName}': לא זוהה — דולג");
      }
    }

    TempData["ImportResults"] = string.Join("|", results);
    TempData["Success"] = "ייבוא טבלאות עזר הושלם";
    return RedirectToAction(nameof(DataMigration));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ImportQuestionnaireCatalog(IFormFile? file)
  {
    if (!ValidateXlsxUpload(file))
    {
      return RedirectToAction(nameof(DataMigration));
    }

    var now = DateTime.UtcNow;
    var results = new Dictionary<string, int>
    {
      ["פרויקטים"] = 0,
      ["תוכניות חינוכיות"] = 0,
      ["תחומים"] = 0,
      ["נושאים"] = 0,
      ["קודי דיון"] = 0,
      ["כיתות"] = 0,
      ["מסגרת חינוכית"] = 0,
      ["ישוב/מחוז/ארצי"] = 0,
      ["שכבות"] = 0
    };

    using var stream = file!.OpenReadStream();
    using var wb = new XLWorkbook(stream);
    var ws = wb.Worksheets.FirstOrDefault(x => x.Name.Trim().Equals("כללי - מאוחד", StringComparison.OrdinalIgnoreCase));
    if (ws == null)
    {
      TempData["Error"] = "לא נמצא גיליון 'כללי - מאוחד' בקובץ השאלונים";
      return RedirectToAction(nameof(DataMigration));
    }

    var existingProjects = await LoadLookupSetAsync(_db.Projects);
    var existingEducationalPrograms = await LoadLookupSetAsync(_db.EducationalPrograms);
    var existingDomains = await LoadLookupSetAsync(_db.Domains);
    var existingSubjects = await LoadLookupSetAsync(_db.Subjects);
    var existingDiscussionCodes = await LoadLookupSetAsync(_db.DiscussionCodes);
    var existingClasses = await LoadLookupSetAsync(_db.Classes);
    var existingFrameworks = await LoadLookupSetAsync(_db.Frameworks);
    var existingLocalityDistrictNationals = await LoadLookupSetAsync(_db.LocalityDistrictNationals);
    var existingGradeLevels = await LoadLookupSetAsync(_db.GradeLevels);

    var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
    for (var row = 2; row <= lastRow; row++)
    {
      results["פרויקטים"] += EnsureLookup(_db.Projects, existingProjects, ws.Cell(row, 1).GetString(), now);
      results["תוכניות חינוכיות"] += EnsureLookup(_db.EducationalPrograms, existingEducationalPrograms, ws.Cell(row, 2).GetString(), now);
      results["תחומים"] += EnsureLookup(_db.Domains, existingDomains, ws.Cell(row, 3).GetString(), now);
      results["נושאים"] += EnsureLookup(_db.Subjects, existingSubjects, ws.Cell(row, 4).GetString(), now);
      results["נושאים"] += EnsureLookup(_db.Subjects, existingSubjects, ws.Cell(row, 5).GetString(), now);
      results["קודי דיון"] += EnsureLookup(_db.DiscussionCodes, existingDiscussionCodes, ws.Cell(row, 6).GetString(), now);
      results["כיתות"] += EnsureLookup(_db.Classes, existingClasses, ws.Cell(row, 7).GetString(), now);
      results["מסגרת חינוכית"] += EnsureQuestionnaireFramework(_db.Frameworks, existingFrameworks, ws.Cell(row, 8).GetString(), now);
      results["ישוב/מחוז/ארצי"] += EnsureLookup(_db.LocalityDistrictNationals, existingLocalityDistrictNationals, ws.Cell(row, 9).GetString(), now);
      results["שכבות"] += EnsureLookup(_db.GradeLevels, existingGradeLevels, ws.Cell(row, 10).GetString(), now);
      results["כיתות"] += EnsureLookup(_db.Classes, existingClasses, ws.Cell(row, 11).GetString(), now);
    }

    await _db.SaveChangesAsync();
    TempData["ImportResults"] = string.Join("|", results.Select(x => $"{x.Key}: {x.Value} רשומות נוספו"));
    TempData["Success"] = "ייבוא קטלוג השאלונים הושלם";
    return RedirectToAction(nameof(DataMigration));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ImportClientLookupXlsb(IFormFile? file)
  {
    if (!ValidateExcelUpload(file, ".xlsb"))
    {
      return RedirectToAction(nameof(DataMigration));
    }

    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    using var stream = file!.OpenReadStream();
    using var reader = ExcelReaderFactory.CreateReader(stream);
    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
    {
      ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = false }
    });

    var results = new Dictionary<string, int>();
    var now = DateTime.UtcNow;

    var lists = dataSet.Tables["גיליון מרכז רשימות לפי שדות"];
    if (lists == null)
    {
      TempData["Error"] = "לא נמצא גיליון 'גיליון מרכז רשימות לפי שדות'";
      return RedirectToAction(nameof(DataMigration));
    }

    var districts = new HashSet<string>(StringComparer.Ordinal);
    var sectors = new HashSet<string>(StringComparer.Ordinal);
    var stages = new HashSet<string>(StringComparer.Ordinal);
    var projects = new HashSet<string>(StringComparer.Ordinal);
    var educationalPrograms = new HashSet<string>(StringComparer.Ordinal);
    var domains = new HashSet<string>(StringComparer.Ordinal);
    var subjects = new HashSet<string>(StringComparer.Ordinal);
    var discussionCodes = new HashSet<string>(StringComparer.Ordinal);
    var classes = new HashSet<string>(StringComparer.Ordinal);

    for (var i = 22; i <= 56 && i < lists.Rows.Count; i++)
    {
      AddIfValue(sectors, GetCell(lists, i, 2), "מגזר עובד", "None");
      AddIfValue(districts, GetCell(lists, i, 4), "מחוז", "None");
      AddIfValue(stages, GetCell(lists, i, 12), "שלב חינוך", "None");
    }

    for (var i = 61; i < lists.Rows.Count; i++)
    {
      AddIfValue(projects, GetCell(lists, i, 0));
      AddIfValue(educationalPrograms, GetCell(lists, i, 2));
      AddIfValue(domains, GetCell(lists, i, 4));
      AddIfValue(subjects, GetCell(lists, i, 6));
      AddIfValue(subjects, GetCell(lists, i, 8));
      AddIfValue(discussionCodes, GetCell(lists, i, 10));
      AddIfValue(classes, GetCell(lists, i, 12));
    }

    foreach (var district in new[] { "ארצי", "מטה", "כללי", "דרום", "תל אביב", "חיפה", "צפון", "מרכז", "התישבותי", "מנח\"י", "ירושלים", "חרדי", "אחר" })
      districts.Add(district);

    results["מחוזות"] = await ImportValuesAsync(_db.Districts, districts, now);
    results["מגזרים"] = await ImportValuesAsync(_db.Sectors, sectors, now);
    results["שלבי חינוך"] = await ImportValuesAsync(_db.EducationalStages, stages, now);
    results["פרויקטים"] = await ImportValuesAsync(_db.Projects, projects, now);
    results["תוכניות חינוכיות"] = await ImportValuesAsync(_db.EducationalPrograms, educationalPrograms, now);
    results["תחומים"] = await ImportValuesAsync(_db.Domains, domains, now);
    results["נושאים"] = await ImportValuesAsync(_db.Subjects, subjects, now);
    results["קודי דיון"] = await ImportValuesAsync(_db.DiscussionCodes, discussionCodes, now);
    results["כיתות"] = await ImportValuesAsync(_db.Classes, classes, now);

    var localities = dataSet.Tables["יישוב"];
    if (localities != null)
    {
      var values = new HashSet<string>(StringComparer.Ordinal);
      for (var i = 1; i < localities.Rows.Count; i++) AddIfValue(values, GetCell(localities, i, 0), "יישוב");
      results["ישובים"] = await ImportValuesAsync(_db.Localities, values, now);
    }

    var institutions = dataSet.Tables["מוסדות"];
    if (institutions != null)
    {
      results["רמות השכלה"] = await ImportEducationTypesFromInstitutionsAsync(institutions, now);
      await _db.SaveChangesAsync();
      results["מוסדות"] = await ImportInstitutionsFromDataTableAsync(institutions, now);
    }

    await _db.SaveChangesAsync();
    TempData["ImportResults"] = string.Join("|", results.Select(x => $"{x.Key}: {x.Value} רשומות נוספו"));
    TempData["Success"] = "ייבוא קובץ טבלאות xlsb הושלם";
    return RedirectToAction(nameof(DataMigration));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ImportEmployees(IFormFile? file)
  {
    if (!ValidateXlsxUpload(file))
    {
      return RedirectToAction(nameof(DataMigration));
    }

    var now = DateTime.UtcNow;
    var roles = await _db.Roles.ToListAsync();
    int added = 0, skipped = 0;
    var errors = new List<string>();

    using var stream = file!.OpenReadStream();
    using var wb = new XLWorkbook(stream);
    var ws = wb.Worksheet(1);
    var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;

    // Expected columns (row 1 = header): A=EmployeeCode, B=IdNumber, C=FirstName, D=LastName,
    // E=Email, F=Phone, G=RoleName, H=Notes
    for (var row = 2; row <= lastRow; row++)
    {
      var employeeCode = ws.Cell(row, 1).GetString().Trim();
      var idNumber = ws.Cell(row, 2).GetString().Trim();
      var firstName = ws.Cell(row, 3).GetString().Trim();
      var lastName = ws.Cell(row, 4).GetString().Trim();
      var email = ws.Cell(row, 5).GetString().Trim();
      var phone = ws.Cell(row, 6).GetString().Trim();
      var roleName = ws.Cell(row, 7).GetString().Trim();
      var notes = ws.Cell(row, 8).GetString().Trim();

      if (string.IsNullOrEmpty(idNumber) || string.IsNullOrEmpty(firstName))
      {
        errors.Add($"שורה {row}: חסר ת.ז או שם פרטי — דולגת");
        continue;
      }

      if (await _db.Users.AnyAsync(u => u.IdNumber == idNumber))
      {
        skipped++;
        continue;
      }

      var role = roles.FirstOrDefault(r =>
        r.Description.Contains(roleName, StringComparison.OrdinalIgnoreCase));

      var passwordHash = _passwordService.HashPassword(idNumber);

      _db.Users.Add(new User
      {
        EmployeeCode = string.IsNullOrEmpty(employeeCode) ? idNumber : employeeCode,
        IdNumber = idNumber,
        FirstName = firstName,
        LastName = lastName,
        Email = string.IsNullOrEmpty(email) ? null : email,
        Phone = string.IsNullOrEmpty(phone) ? null : phone,
        Notes = string.IsNullOrEmpty(notes) ? null : notes,
        RoleId = role?.Id ?? 1,
        UserRoleId = 6, // Employee
        StatusId = 1,   // Active
        IsReportingEmployee = true,
        MustChangePassword = true,
        PasswordHash = passwordHash,
        CreatedAt = now
      });
      added++;
    }

    await _db.SaveChangesAsync();

    var summary = $"עובדים: {added} נוספו, {skipped} קיימים (דולגו)";
    if (errors.Any()) summary += $" | שגיאות: {errors.Count}";
    TempData["ImportResults"] = string.Join("|", new[] { summary }.Concat(errors));
    TempData["Success"] = "ייבוא עובדים הושלם";
    return RedirectToAction(nameof(DataMigration));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ImportInstitutions(IFormFile? file)
  {
    if (!ValidateXlsxUpload(file))
    {
      return RedirectToAction(nameof(DataMigration));
    }

    var now = DateTime.UtcNow;
    int added = 0, skipped = 0;
    var errors = new List<string>();

    using var stream = file!.OpenReadStream();
    using var wb = new XLWorkbook(stream);
    var ws = wb.Worksheet(1);
    var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;

    for (var row = 2; row <= lastRow; row++)
    {
      if (!ws.Cell(row, 1).TryGetValue<int>(out var symbol))
      {
        errors.Add($"שורה {row}: סמל מוסד חסר או לא תקין");
        continue;
      }

      var name = ws.Cell(row, 2).GetString().Trim();
      if (string.IsNullOrWhiteSpace(name))
      {
        errors.Add($"שורה {row}: שם מוסד חסר");
        continue;
      }

      var localityId = await FindLocalityIdAsync(ws.Cell(row, 3).GetString());
      var districtId = await FindDistrictIdAsync(ws.Cell(row, 4).GetString());
      var sectorId = await FindSectorIdAsync(ws.Cell(row, 5).GetString());
      var typeId = await FindEducationTypeIdAsync(ws.Cell(row, 6).GetString());
      var stageId = await FindEducationalStageIdAsync(ws.Cell(row, 7).GetString());

      if (await _db.Institutions.AnyAsync(i => i.InstitutionSymbol == symbol && i.EducationalStageId == stageId))
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
    TempData["ImportResults"] = string.Join("|",
      new[] { $"מוסדות: {added} נוספו, {skipped} קיימים (דולגו)" }.Concat(errors));
    TempData["Success"] = "ייבוא מוסדות הושלם";
    return RedirectToAction(nameof(DataMigration));
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ImportAllocations(IFormFile? file)
  {
    if (!ValidateXlsxUpload(file))
    {
      return RedirectToAction(nameof(DataMigration));
    }

    int added = 0, updated = 0, errorsCount = 0;
    var errors = new List<string>();

    using var stream = file!.OpenReadStream();
    using var wb = new XLWorkbook(stream);
    var ws = wb.Worksheet(1);
    var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;

    for (var row = 2; row <= lastRow; row++)
    {
      var idNumber = ws.Cell(row, 1).GetString().Trim();
      var projectName = ws.Cell(row, 2).GetString().Trim();
      var user = await _db.Users.FirstOrDefaultAsync(u => u.IdNumber == idNumber);
      var projectId = await FindProjectIdAsync(projectName);
      if (user == null || !projectId.HasValue)
      {
        errors.Add($"שורה {row}: עובד או פרויקט לא נמצאו");
        errorsCount++;
        continue;
      }

      var allocation = await _db.Allocations
        .FirstOrDefaultAsync(a => a.UserId == user.Id && a.ProjectId == projectId.Value);
      var isNew = allocation == null;
      allocation ??= new Allocation
      {
        UserId = user.Id,
        ProjectId = projectId.Value,
        CreatedAt = DateTime.UtcNow
      };

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

      if (isNew) _db.Allocations.Add(allocation);
      await _db.SaveChangesAsync();

      await ReplaceAllocationLinksAsync(allocation.Id, ws, row);
      if (isNew) added++; else updated++;
    }

    TempData["ImportResults"] = string.Join("|",
      new[] { $"הקצאות: {added} נוספו, {updated} עודכנו, {errorsCount} שגיאות" }.Concat(errors));
    TempData["Success"] = "ייבוא הקצאות הושלם";
    return RedirectToAction(nameof(DataMigration));
  }

  private async Task<int> ImportSimpleAsync(IXLWorksheet ws, Func<string, Task<bool>> insertFn)
  {
    var count = 0;
    var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
    for (var row = 2; row <= lastRow; row++)
    {
      var desc = ws.Cell(row, 1).GetString().Trim();
      if (string.IsNullOrEmpty(desc)) continue;
      if (await insertFn(desc)) count++;
    }
    return count;
  }

  private bool ValidateXlsxUpload(IFormFile? file)
  {
    return ValidateExcelUpload(file, ".xlsx");
  }

  private bool ValidateExcelUpload(IFormFile? file, string extension)
  {
    if (file == null || file.Length == 0)
    {
      TempData["Error"] = "לא נבחר קובץ";
      return false;
    }

    if (!Path.GetExtension(file.FileName).Equals(extension, StringComparison.OrdinalIgnoreCase))
    {
      TempData["Error"] = $"קובץ לא תקין. פעולה זו תומכת בקובץ {extension} בלבד.";
      return false;
    }

    return true;
  }

  private static async Task<HashSet<string>> LoadLookupSetAsync<T>(DbSet<T> set)
    where T : AxiomaReporting.Core.Entities.Base.LookupEntity, new()
  {
    var values = await set.Select(x => x.Description).ToListAsync();
    return values.ToHashSet(StringComparer.Ordinal);
  }

  private static int EnsureLookup<T>(DbSet<T> set, HashSet<string> existingValues, string value, DateTime now)
    where T : AxiomaReporting.Core.Entities.Base.LookupEntity, new()
  {
    var description = value.Trim();
    if (string.IsNullOrWhiteSpace(description)) return 0;
    if (!existingValues.Add(description)) return 0;
    set.Add(new T { Description = description, IsActive = true, CreatedAt = now });
    return 1;
  }

  private async Task<int> ImportValuesAsync<T>(DbSet<T> set, IEnumerable<string> values, DateTime now)
    where T : AxiomaReporting.Core.Entities.Base.LookupEntity, new()
  {
    var existing = await LoadLookupSetAsync(set);
    var count = 0;
    foreach (var value in values)
      count += EnsureLookup(set, existing, value, now);
    return count;
  }

  private static int EnsureQuestionnaireFramework(DbSet<Framework> set, HashSet<string> existingValues, string value, DateTime now)
  {
    var description = value.Trim();
    if (string.IsNullOrWhiteSpace(description)) return 0;
    if (!existingValues.Add(description)) return 0;

    set.Add(new Framework
    {
      Description = description,
      InstitutionSymbol = $"QCAT-{StableShortHash(description)}",
      IsActive = true,
      CreatedAt = now
    });
    return 1;
  }

  private static string StableShortHash(string value)
  {
    var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
    return Convert.ToHexString(bytes, 0, 6);
  }

  private static string GetCell(DataTable table, int row, int col)
  {
    if (row < 0 || row >= table.Rows.Count || col < 0 || col >= table.Columns.Count)
      return string.Empty;
    return table.Rows[row][col]?.ToString()?.Trim() ?? string.Empty;
  }

  private static void AddIfValue(HashSet<string> values, string value, params string[] excluded)
  {
    value = value.Trim();
    if (string.IsNullOrWhiteSpace(value)) return;
    if (excluded.Contains(value, StringComparer.Ordinal)) return;
    values.Add(value);
  }

  private async Task<int> ImportEducationTypesFromInstitutionsAsync(DataTable institutions, DateTime now)
  {
    var values = new HashSet<string>(StringComparer.Ordinal);
    for (var i = 1; i < institutions.Rows.Count; i++)
      AddIfValue(values, GetCell(institutions, i, 5));
    return await ImportValuesAsync(_db.EducationTypes, values, now);
  }

  private async Task<int> ImportInstitutionsFromDataTableAsync(DataTable institutions, DateTime now)
  {
    var localityMap = await _db.Localities.ToDictionaryAsync(x => x.Description, x => x.Id);
    var districtMap = await _db.Districts.ToDictionaryAsync(x => x.Description, x => x.Id);
    var sectorMap = await _db.Sectors.ToDictionaryAsync(x => x.Description, x => x.Id);
    var educationTypeMap = await _db.EducationTypes.ToDictionaryAsync(x => x.Description, x => x.Id);
    var stageMap = await _db.EducationalStages.ToDictionaryAsync(x => x.Description, x => x.Id);
    var existing = await _db.Institutions
      .Select(x => new { x.InstitutionSymbol, x.EducationalStageId })
      .ToListAsync();
    var existingKeys = existing.Select(x => $"{x.InstitutionSymbol}|{x.EducationalStageId}").ToHashSet(StringComparer.Ordinal);

    var count = 0;
    for (var i = 1; i < institutions.Rows.Count; i++)
    {
      var symbolValue = GetCell(institutions, i, 0);
      if (!decimal.TryParse(symbolValue, out var symbolDecimal)) continue;
      var symbol = (int)symbolDecimal;
      var name = GetCell(institutions, i, 1);
      if (string.IsNullOrWhiteSpace(name)) continue;

      var localityId = GetMappedId(localityMap, GetCell(institutions, i, 2));
      var districtId = GetMappedId(districtMap, GetCell(institutions, i, 3));
      var sectorId = GetMappedId(sectorMap, GetCell(institutions, i, 4));
      var typeId = GetMappedId(educationTypeMap, GetCell(institutions, i, 5));
      var stageId = GetMappedId(stageMap, GetCell(institutions, i, 6));
      if (!existingKeys.Add($"{symbol}|{stageId}")) continue;

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
      count++;
    }

    return count;
  }

  private static int? GetMappedId(Dictionary<string, int> map, string value)
  {
    return map.TryGetValue(value, out var id) ? id : null;
  }

  private async Task<int> ImportFrameworksAsync(IXLWorksheet ws)
  {
    var count = 0;
    var now = DateTime.UtcNow;
    var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
    for (var row = 2; row <= lastRow; row++)
    {
      var description = ws.Cell(row, 1).GetString().Trim();
      var institutionSymbol = ws.Cell(row, 2).GetString().Trim();
      var stageId = await FindEducationalStageIdAsync(ws.Cell(row, 3).GetString());
      if (string.IsNullOrWhiteSpace(description)) continue;

      var exists = !string.IsNullOrWhiteSpace(institutionSymbol)
        ? await _db.Frameworks.AnyAsync(f => f.InstitutionSymbol == institutionSymbol && f.EducationalStageId == stageId)
        : await _db.Frameworks.AnyAsync(f => f.Description == description);
      if (exists) continue;

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
    return count;
  }

  private async Task ReplaceAllocationLinksAsync(int allocationId, IXLWorksheet ws, int row)
  {
    _db.Set<AllocationDistrict>().RemoveRange(_db.Set<AllocationDistrict>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationProgram>().RemoveRange(_db.Set<AllocationProgram>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationSector>().RemoveRange(_db.Set<AllocationSector>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationLocality>().RemoveRange(_db.Set<AllocationLocality>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationFramework>().RemoveRange(_db.Set<AllocationFramework>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationSubject>().RemoveRange(_db.Set<AllocationSubject>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationDomain>().RemoveRange(_db.Set<AllocationDomain>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationEducationalProgram>().RemoveRange(_db.Set<AllocationEducationalProgram>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationClass>().RemoveRange(_db.Set<AllocationClass>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationGradeLevel>().RemoveRange(_db.Set<AllocationGradeLevel>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationDiscussionCode>().RemoveRange(_db.Set<AllocationDiscussionCode>().Where(x => x.AllocationId == allocationId));
    _db.Set<AllocationLocalityDistrictNational>().RemoveRange(_db.Set<AllocationLocalityDistrictNational>().Where(x => x.AllocationId == allocationId));
    await _db.SaveChangesAsync();

    foreach (var id in await FindIdsAsync(ws.Cell(row, 11).GetString(), FindDistrictIdAsync))
      _db.Set<AllocationDistrict>().Add(new AllocationDistrict { AllocationId = allocationId, DistrictId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 12).GetString(), FindProgramIdAsync))
      _db.Set<AllocationProgram>().Add(new AllocationProgram { AllocationId = allocationId, ProgramId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 13).GetString(), FindSectorIdAsync))
      _db.Set<AllocationSector>().Add(new AllocationSector { AllocationId = allocationId, SectorId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 14).GetString(), FindLocalityIdAsync))
      _db.Set<AllocationLocality>().Add(new AllocationLocality { AllocationId = allocationId, LocalityId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 15).GetString(), FindFrameworkIdAsync))
      _db.Set<AllocationFramework>().Add(new AllocationFramework { AllocationId = allocationId, FrameworkId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 16).GetString(), FindSubjectIdAsync))
      _db.Set<AllocationSubject>().Add(new AllocationSubject { AllocationId = allocationId, SubjectId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 17).GetString(), FindDomainIdAsync))
      _db.Set<AllocationDomain>().Add(new AllocationDomain { AllocationId = allocationId, DomainId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 18).GetString(), FindEducationalProgramIdAsync))
      _db.Set<AllocationEducationalProgram>().Add(new AllocationEducationalProgram { AllocationId = allocationId, EducationalProgramId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 19).GetString(), FindClassIdAsync))
      _db.Set<AllocationClass>().Add(new AllocationClass { AllocationId = allocationId, ClassId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 20).GetString(), FindGradeLevelIdAsync))
      _db.Set<AllocationGradeLevel>().Add(new AllocationGradeLevel { AllocationId = allocationId, GradeLevelId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 21).GetString(), FindDiscussionCodeIdAsync))
      _db.Set<AllocationDiscussionCode>().Add(new AllocationDiscussionCode { AllocationId = allocationId, DiscussionCodeId = id });
    foreach (var id in await FindIdsAsync(ws.Cell(row, 22).GetString(), FindLocalityDistrictNationalIdAsync))
      _db.Set<AllocationLocalityDistrictNational>().Add(new AllocationLocalityDistrictNational { AllocationId = allocationId, LocalityDistrictNationalId = id });
    await _db.SaveChangesAsync();
  }

  private static decimal? ReadDecimal(IXLWorksheet ws, int row, int col) =>
    ws.Cell(row, col).TryGetValue<decimal>(out var value) ? value : null;

  private static int? ReadInt(IXLWorksheet ws, int row, int col) =>
    ws.Cell(row, col).TryGetValue<int>(out var value) ? value : null;

  private static bool ReadBool(string value) =>
    value.Trim().Equals("true", StringComparison.OrdinalIgnoreCase) ||
    value.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase) ||
    value.Trim().Equals("כן", StringComparison.OrdinalIgnoreCase) ||
    value.Trim() == "1";

  private static decimal? ReadDailyScope(string value)
  {
    value = value.Trim();
    if (string.IsNullOrEmpty(value) ||
        value.Equals("Unlimited", StringComparison.OrdinalIgnoreCase) ||
        value.Equals("ללא הגבלה", StringComparison.OrdinalIgnoreCase))
      return null;
    return decimal.TryParse(value, out var parsed) ? parsed : null;
  }

  private static string? EmptyToNull(string value) =>
    string.IsNullOrWhiteSpace(value) ? null : value.Trim();

  private static async Task<List<int>> FindIdsAsync(string values, Func<string, Task<int?>> resolver)
  {
    var ids = new List<int>();
    foreach (var value in values.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
    {
      var id = await resolver(value);
      if (id.HasValue) ids.Add(id.Value);
    }
    return ids.Distinct().ToList();
  }

  private async Task<int?> FindDistrictIdAsync(string value) => await FindLookupIdAsync(_db.Districts, value);
  private async Task<int?> FindSectorIdAsync(string value) => await FindLookupIdAsync(_db.Sectors, value);
  private async Task<int?> FindLocalityIdAsync(string value) => await FindLookupIdAsync(_db.Localities, value);
  private async Task<int?> FindProjectIdAsync(string value) => await FindLookupIdAsync(_db.Projects, value);
  private async Task<int?> FindProgramIdAsync(string value) => await FindLookupIdAsync(_db.Programs, value);
  private async Task<int?> FindSubjectIdAsync(string value) => await FindLookupIdAsync(_db.Subjects, value);
  private async Task<int?> FindDomainIdAsync(string value) => await FindLookupIdAsync(_db.Domains, value);
  private async Task<int?> FindEducationalProgramIdAsync(string value) => await FindLookupIdAsync(_db.EducationalPrograms, value);
  private async Task<int?> FindClassIdAsync(string value) => await FindLookupIdAsync(_db.Classes, value);
  private async Task<int?> FindGradeLevelIdAsync(string value) => await FindLookupIdAsync(_db.GradeLevels, value);
  private async Task<int?> FindDiscussionCodeIdAsync(string value) => await FindLookupIdAsync(_db.DiscussionCodes, value);
  private async Task<int?> FindLocalityDistrictNationalIdAsync(string value) => await FindLookupIdAsync(_db.LocalityDistrictNationals, value);
  private async Task<int?> FindEducationTypeIdAsync(string value) => await FindLookupIdAsync(_db.EducationTypes, value);
  private async Task<int?> FindEducationalStageIdAsync(string value) => await FindLookupIdAsync(_db.EducationalStages, value);
  private async Task<int?> FindFrameworkIdAsync(string value)
  {
    value = value.Trim();
    if (string.IsNullOrEmpty(value)) return null;
    if (int.TryParse(value, out var symbol))
      return await _db.Frameworks.Where(f => f.InstitutionSymbol == symbol.ToString()).Select(f => (int?)f.Id).FirstOrDefaultAsync();
    return await _db.Frameworks.Where(f => f.Description == value).Select(f => (int?)f.Id).FirstOrDefaultAsync();
  }

  private static async Task<int?> FindLookupIdAsync<T>(IQueryable<T> query, string value)
    where T : AxiomaReporting.Core.Entities.Base.LookupEntity
  {
    value = value.Trim();
    if (string.IsNullOrEmpty(value)) return null;
    if (int.TryParse(value, out var id) && await query.AnyAsync(x => x.Id == id)) return id;
    return await query.Where(x => x.Description == value).Select(x => (int?)x.Id).FirstOrDefaultAsync();
  }

  // --- Batch Report Import (multi-employee) — AX-018 extension ---

  [HttpGet]
  public async Task<IActionResult> BatchReportImport()
  {
    var months = await _db.ReportingMonths
      .OrderByDescending(m => m.Year).ThenByDescending(m => m.Month)
      .ToListAsync();
    var activeMonth = months.FirstOrDefault(m => m.IsActive);
    return View(new AxiomaReporting.Web.Models.BatchReportImportFormViewModel
    {
      ReportingMonths = months,
      SelectedReportingMonthId = activeMonth?.Id ?? months.FirstOrDefault()?.Id
    });
  }

  [HttpPost, ValidateAntiForgeryToken]
  public async Task<IActionResult> BatchReportImport(IFormFile file, int reportingMonthId, CancellationToken ct)
  {
    if (file == null || file.Length == 0)
    {
      TempData["Error"] = "יש לבחור קובץ Excel לייבוא";
      return RedirectToAction(nameof(BatchReportImport));
    }

    var uploaderId = int.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)!);
    var uploader = await _db.Users.FindAsync(new object[] { uploaderId }, ct);
    var uploaderName = uploader == null ? "" : $"{uploader.FirstName} {uploader.LastName}".Trim();
    var month = await _db.ReportingMonths.FindAsync(new object[] { reportingMonthId }, ct);

    await using var stream = file.OpenReadStream();
    var result = await _batchImportService.ImportAsync(stream, reportingMonthId, uploaderId, ct);

    // Pre-generate the errors PDF once so we can both email it as an attachment
    // and offer it as a download from the results screen.
    byte[]? errorsPdf = null;
    if (result.Errors.Any())
    {
      var errorLines = result.Errors.Select(e =>
        $"שורה {e.FileRowNumber} | קוד {e.EmployeeCode} | {e.ReporterName} | {e.ErrorMessage}").ToArray();
      errorsPdf = _pdfReportService.CreateErrorReport(errorLines);
    }

    // Send uploader emails
    if (!string.IsNullOrWhiteSpace(uploader?.Email) && month != null)
    {
      await _emailService.SendAsync(
        uploader.Email, uploaderName, "BatchImportSuccessUploader",
        new Dictionary<string, string>
        {
          ["UploaderName"] = uploaderName,
          ["RowsImported"] = result.RowsImported.ToString(CultureInfo.InvariantCulture),
          ["EmployeesCount"] = result.EmployeesAffected.ToString(CultureInfo.InvariantCulture),
          ["Month"] = month.Month.ToString(CultureInfo.InvariantCulture),
          ["Year"] = month.Year.ToString(CultureInfo.InvariantCulture)
        }, cancellationToken: ct);

      if (errorsPdf != null)
      {
        var pdfName = $"batch-import-errors-{month.Year}-{month.Month:D2}.pdf";
        var attachments = new List<AxiomaReporting.Core.Interfaces.EmailAttachment>
        {
          new(pdfName, errorsPdf, "application/pdf")
        };
        await _emailService.SendAsync(
          uploader.Email, uploaderName, "BatchImportErrors",
          new Dictionary<string, string>
          {
            ["UploaderName"] = uploaderName,
            ["ErrorsCount"] = result.ErrorRowsCount.ToString(CultureInfo.InvariantCulture),
            ["Month"] = month.Month.ToString(CultureInfo.InvariantCulture),
            ["Year"] = month.Year.ToString(CultureInfo.InvariantCulture)
          }, attachments, ct);
      }
    }

    // Stash errors in TempData so a subsequent GET can download the PDF.
    if (result.Errors.Any())
    {
      var serialized = string.Join("\n", result.Errors.Select(e =>
        $"שורה {e.FileRowNumber} | קוד {e.EmployeeCode} | {e.ReporterName} | {e.ErrorMessage}"));
      TempData["BatchImportErrors"] = serialized;
    }

    return View("BatchReportImportResult", new AxiomaReporting.Web.Models.BatchReportImportResultViewModel
    {
      MonthDescription = month?.Description,
      MonthNumber = month?.Month,
      Year = month?.Year,
      Result = result
    });
  }

  [HttpGet]
  public IActionResult BatchReportImportErrorsPdf()
  {
    var serialized = TempData["BatchImportErrors"] as string ?? string.Empty;
    TempData.Keep("BatchImportErrors");

    var lines = string.IsNullOrWhiteSpace(serialized)
      ? new[] { "No errors recorded." }
      : serialized.Split('\n', StringSplitOptions.RemoveEmptyEntries);

    var bytes = _pdfReportService.CreateErrorReport(lines);
    return File(bytes, "application/pdf", $"batch-import-errors-{DateTime.Now:yyyyMMddHHmm}.pdf");
  }

  // --- Terms of Use Versions ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> TermsOfUse()
  {
    var versions = await _db.TermsOfUseVersions
      .OrderByDescending(v => v.VersionNumber)
      .Select(v => new AxiomaReporting.Web.Models.TermsOfUseVersionListItem
      {
        Id = v.Id,
        VersionNumber = v.VersionNumber,
        EffectiveFrom = v.EffectiveFrom,
        CreatedAt = v.CreatedAt,
        PublishedByName = v.PublishedByUser == null
          ? ""
          : (v.PublishedByUser.FirstName + " " + v.PublishedByUser.LastName).Trim(),
        AcceptanceCount = v.Acceptances.Count,
        BodyHtml = v.BodyHtml
      })
      .ToListAsync();
    return View(versions);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> PublishTermsOfUse(string bodyHtml, DateTime? effectiveFrom)
  {
    if (string.IsNullOrWhiteSpace(bodyHtml))
    {
      TempData["Error"] = "יש להזין תוכן לתנאי השימוש";
      return RedirectToAction(nameof(TermsOfUse));
    }

    var actorIdRaw = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (!int.TryParse(actorIdRaw, out var actorId))
    {
      TempData["Error"] = "לא ניתן לזהות את המשתמש המבצע";
      return RedirectToAction(nameof(TermsOfUse));
    }

    var nextVersion = (await _db.TermsOfUseVersions.MaxAsync(v => (int?)v.VersionNumber) ?? 0) + 1;
    var now = DateTime.UtcNow;

    var termsVersion = new TermsOfUseVersion
    {
      VersionNumber = nextVersion,
      BodyHtml = bodyHtml,
      EffectiveFrom = effectiveFrom ?? now,
      PublishedByUserId = actorId,
      CreatedAt = now
    };
    _db.TermsOfUseVersions.Add(termsVersion);
    await _db.SaveChangesAsync();

    await _auditLog.LogAsync("Terms.Publish", nameof(TermsOfUseVersion), termsVersion.Id.ToString(),
      after: new { termsVersion.Id, termsVersion.VersionNumber, termsVersion.EffectiveFrom, termsVersion.PublishedByUserId });

    // Force every user to re-accept the new version on their next request.
    // Using ExecuteUpdateAsync avoids loading all users into memory. Providers that
    // do not support it (test in-memory) will fall back through EF's shim where possible.
    if (_db.Database.IsRelational())
    {
      await _db.Users.ExecuteUpdateAsync(s => s.SetProperty(u => u.AcceptedTermsOfUse, false));
    }
    else
    {
      var users = await _db.Users.ToListAsync();
      foreach (var u in users) u.AcceptedTermsOfUse = false;
      await _db.SaveChangesAsync();
    }

    TempData["Success"] = $"גרסה {nextVersion} של תנאי השימוש פורסמה. כל המשתמשים יתבקשו לאשר מחדש.";
    return RedirectToAction(nameof(TermsOfUse));
  }

  // --- Notification Logs (Gap 6) ---

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> NotificationLogs(
    string? type,
    string? status,
    string? templateType,
    DateTime? fromDate,
    DateTime? toDate,
    string? recipientEmail,
    int? userId,
    int page = 1,
    int pageSize = 50)
  {
    if (pageSize <= 0 || pageSize > 500) pageSize = 50;
    if (page <= 0) page = 1;

    var query = _db.NotificationLogs.AsNoTracking().AsQueryable();

    if (!string.IsNullOrWhiteSpace(type))
      query = query.Where(n => n.NotificationType == type);
    if (!string.IsNullOrWhiteSpace(status))
      query = query.Where(n => n.Status == status);
    if (!string.IsNullOrWhiteSpace(templateType))
      query = query.Where(n => n.TemplateType == templateType);
    if (fromDate.HasValue)
      query = query.Where(n => n.CreatedAt >= fromDate.Value.Date);
    if (toDate.HasValue)
    {
      var end = toDate.Value.Date.AddDays(1);
      query = query.Where(n => n.CreatedAt < end);
    }
    if (!string.IsNullOrWhiteSpace(recipientEmail))
      query = query.Where(n => n.RecipientEmail.Contains(recipientEmail));
    if (userId.HasValue)
      query = query.Where(n => n.RecipientUserId == userId.Value);

    var total = await query.CountAsync();
    var items = await query
      .OrderByDescending(n => n.CreatedAt)
      .Skip((page - 1) * pageSize)
      .Take(pageSize)
      .Select(n => new AxiomaReporting.Web.Models.NotificationLogListItem
      {
        Id = n.Id,
        CreatedAt = n.CreatedAt,
        NotificationType = n.NotificationType,
        TemplateType = n.TemplateType,
        RecipientEmail = n.RecipientEmail,
        RecipientName = n.RecipientUser != null
          ? (n.RecipientUser.FirstName + " " + n.RecipientUser.LastName)
          : null,
        Subject = n.Subject,
        Status = n.Status,
        AttemptCount = n.AttemptCount,
        LastAttemptAt = n.LastAttemptAt,
        NextRetryAt = n.NextRetryAt,
        FailureReason = n.FailureReason
      })
      .ToListAsync();

    var model = new AxiomaReporting.Web.Models.NotificationLogListViewModel
    {
      Items = items,
      Page = page,
      PageSize = pageSize,
      TotalCount = total,
      Type = type,
      Status = status,
      TemplateType = templateType,
      FromDate = fromDate,
      ToDate = toDate,
      RecipientEmail = recipientEmail,
      UserId = userId
    };
    return View(model);
  }

  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> NotificationLogDetails(int id)
  {
    var log = await _db.NotificationLogs
      .AsNoTracking()
      .Include(n => n.RecipientUser)
      .FirstOrDefaultAsync(n => n.Id == id);
    if (log == null) return NotFound();
    return PartialView("_NotificationLogDetails", log);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ResendNotification(int id)
  {
    var log = await _db.NotificationLogs.FirstOrDefaultAsync(n => n.Id == id);
    if (log == null)
    {
      TempData["Error"] = "התראה לא נמצאה";
      return RedirectToAction(nameof(NotificationLogs));
    }

    log.Status = "Pending";
    log.AttemptCount = 0;
    log.FailureReason = null;
    log.NextRetryAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    TempData["Success"] = $"התראה מס' {log.Id} הוחזרה לתור לשליחה";
    return RedirectToAction(nameof(NotificationLogs));
  }

  // --- Audit Log (Gap 7) ---

  [HttpGet]
  [Route("Admin/AuditLog")]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> AuditLog(
    [FromQuery(Name = "action")] string? actionFilter,
    string? entityType,
    string? entityId,
    int? actorUserId,
    DateTime? fromDate,
    DateTime? toDate,
    int page = 1,
    int pageSize = 50)
  {
    pageSize = pageSize is < 1 or > 500 ? 50 : pageSize;
    page = Math.Max(page, 1);

    var query = BuildAuditLogQuery(actionFilter, entityType, entityId, actorUserId, fromDate, toDate);

    var totalCount = await query.CountAsync();
    var rows = await query
      .OrderByDescending(a => a.Timestamp)
      .Skip((page - 1) * pageSize)
      .Take(pageSize)
      .Select(a => new AuditLogListItem
      {
        Id = a.Id,
        Timestamp = a.Timestamp,
        ActorUserId = a.ActorUserId,
        ActorName = a.ActorUser == null ? null : (a.ActorUser.FirstName + " " + a.ActorUser.LastName).Trim(),
        Action = a.Action,
        EntityType = a.EntityType,
        EntityId = a.EntityId,
        Notes = a.Notes,
        IpAddress = a.IpAddress,
        UserAgent = a.UserAgent,
        Before = a.Before,
        After = a.After
      })
      .ToListAsync();

    var vm = new AuditLogListViewModel
    {
      Items = rows,
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
    return View(vm);
  }

  [HttpGet]
  [Route("Admin/AuditLog/Export")]
  [Authorize(Policy = PolicyNames.AdminOnly)]
  public async Task<IActionResult> ExportAuditLog(
    [FromQuery(Name = "action")] string? actionFilter,
    string? entityType,
    string? entityId,
    int? actorUserId,
    DateTime? fromDate,
    DateTime? toDate)
  {
    var rows = await BuildAuditLogQuery(actionFilter, entityType, entityId, actorUserId, fromDate, toDate)
      .OrderByDescending(a => a.Timestamp)
      .Take(50000) // sanity cap
      .Select(a => new
      {
        a.Id,
        a.Timestamp,
        a.ActorUserId,
        ActorName = a.ActorUser == null ? null : (a.ActorUser.FirstName + " " + a.ActorUser.LastName).Trim(),
        a.Action,
        a.EntityType,
        a.EntityId,
        a.Notes,
        a.IpAddress,
        a.UserAgent,
        a.Before,
        a.After
      })
      .ToListAsync();

    var sb = new StringBuilder();
    sb.Append('\uFEFF'); // UTF-8 BOM for Excel Hebrew compatibility
    sb.AppendLine("Id,Timestamp,ActorUserId,ActorName,Action,EntityType,EntityId,Notes,IpAddress,UserAgent,Before,After");
    foreach (var r in rows)
    {
      sb.Append(r.Id).Append(',')
        .Append(r.Timestamp.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)).Append(',')
        .Append(r.ActorUserId?.ToString() ?? string.Empty).Append(',')
        .Append(CsvEscape(r.ActorName)).Append(',')
        .Append(CsvEscape(r.Action)).Append(',')
        .Append(CsvEscape(r.EntityType)).Append(',')
        .Append(CsvEscape(r.EntityId)).Append(',')
        .Append(CsvEscape(r.Notes)).Append(',')
        .Append(CsvEscape(r.IpAddress)).Append(',')
        .Append(CsvEscape(r.UserAgent)).Append(',')
        .Append(CsvEscape(r.Before)).Append(',')
        .Append(CsvEscape(r.After))
        .Append('\n');
    }
    var bytes = Encoding.UTF8.GetBytes(sb.ToString());
    var fileName = $"audit-log-{DateTime.UtcNow:yyyyMMdd}.csv";
    return File(bytes, "text/csv; charset=utf-8", fileName);
  }

  private IQueryable<AuditLog> BuildAuditLogQuery(
    string? action,
    string? entityType,
    string? entityId,
    int? actorUserId,
    DateTime? fromDate,
    DateTime? toDate)
  {
    var q = _db.AuditLogs.Include(a => a.ActorUser).AsNoTracking().AsQueryable();
    if (!string.IsNullOrWhiteSpace(action))
      q = q.Where(a => a.Action.Contains(action));
    if (!string.IsNullOrWhiteSpace(entityType))
      q = q.Where(a => a.EntityType == entityType);
    if (!string.IsNullOrWhiteSpace(entityId))
      q = q.Where(a => a.EntityId == entityId);
    if (actorUserId.HasValue)
      q = q.Where(a => a.ActorUserId == actorUserId);
    if (fromDate.HasValue)
      q = q.Where(a => a.Timestamp >= fromDate);
    if (toDate.HasValue)
      q = q.Where(a => a.Timestamp <= toDate);
    return q;
  }

  private static string CsvEscape(string? value)
  {
    if (string.IsNullOrEmpty(value)) return string.Empty;
    var needsQuote = value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r');
    var escaped = value.Replace("\"", "\"\"");
    return needsQuote ? $"\"{escaped}\"" : escaped;
  }

  // --- Private helpers ---

  private async Task LoadInstitutionDropdowns()
  {
    ViewBag.Localities = await _db.Localities.Where(l => l.IsActive).OrderBy(l => l.Description).ToListAsync();
    ViewBag.Districts = await _db.Districts.Where(d => d.IsActive).OrderBy(d => d.Description).ToListAsync();
    ViewBag.Sectors = await _db.Sectors.Where(s => s.IsActive).OrderBy(s => s.Description).ToListAsync();
    ViewBag.EducationTypes = await _db.EducationTypes.Where(t => t.IsActive).OrderBy(t => t.Description).ToListAsync();
    ViewBag.EducationalStages = await _db.EducationalStages.Where(s => s.IsActive).OrderBy(s => s.Description).ToListAsync();
  }
}

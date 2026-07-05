using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Net;
using System.Text;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Web.Controllers;

[Authorize]
public class ReportController : Controller
{
  private static readonly HashSet<string> AllowedAttachmentExtensions =
    new(StringComparer.OrdinalIgnoreCase) { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };

  private const long MaxAttachmentBytes = 10 * 1024 * 1024;
  private const int MaxAttachmentDescriptionLength = 1000;

  internal const string DeadlinePassedMessage =
    "המועד האחרון לדיווח עבר. ניתן לערוך רק באמצעות מנהל מערכת, מנהל פרויקט או רכז פרויקט.";

  internal const string ConcurrencyConflictMessage =
    "השורה עודכנה במקביל על ידי משתמש אחר. יש לרענן ולנסות שוב.";

  private readonly AppDbContext _db;
  private readonly IReportValidationService _validator;
  private readonly IReportStatusService _statusService;
  private readonly IReportExcelImportService _excelImportService;
  private readonly IPdfReportService _pdfReportService;
  private readonly ICurrentUserService _currentUser;
  private readonly IEmailService _emailService;
  private readonly IAuditLogService _auditLog;
  private readonly ILogger<ReportController> _logger;

  // Hebrew tokens marking a Locality row that is really an institution/framework
  // (school, yeshiva, youth center...) rather than a city. Used to keep only real
  // cities in the manual-report locality dropdown.
  private static readonly string[] NonCityLocalityTokens = new string[47]
  {
    "בית ספר", "בתי ספר", "בי\"ס", "בי'ס", "אולפנ", "אורט", "מח\"ט", "מועדונית", "מרכז נוער", "מרכזי חינוך",
    "מרכזים לגיל הרך", "מרכז לגיל הרך", "גיל הרך", "עוגנים", "מסגרות", "כיתות", "על יסודי", "תיכון", "ישיבה", "ישיבת",
    "תורה", "תלמוד", "חינוך", "אמי\"ת", "אמי״ת", "עמל", "הילה ", "בית חם", "תעשית", "חברה וטבע",
    "ברסלב", "לצעירים", "מדרשה", "מכנובקא", "ק.הרצוג", "ברנקו", "משכן", "אהבת", "באר אברהם", "בית דוד",
    "בית אליהו", "בית צבי", "בית רבן", "בני אהרון", "אמרי", "אקרא", "היכל"
  };

  public ReportController(
    AppDbContext db,
    IReportValidationService validator,
    IReportStatusService statusService,
    IReportExcelImportService excelImportService,
    IPdfReportService pdfReportService,
    ICurrentUserService currentUser,
    IEmailService emailService,
    IAuditLogService auditLog,
    ILogger<ReportController> logger)
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
    var requestedReport = reportId.HasValue
      ? await _db.Reports
        .Include(r => r.ReportingMonth)
        .FirstOrDefaultAsync(r => r.Id == reportId.Value)
      : null;
    if (reportId.HasValue && requestedReport == null) return NotFound();

    // Archived reports are hidden from employees and inspectors (roles 1-3 only).
    if ((requestedReport?.IsArchived ?? false) &&
        _currentUser.UserRole is not (UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator))
      return NotFound();

    var targetUserId = requestedReport?.UserId ?? userId ?? _currentUser.UserId;
    if (!await CanViewEmployeeReportAsync(targetUserId)) return Forbid();

    // Month resolution: explicit report → requested month (history view) → active month.
    var activeMonth = requestedReport?.ReportingMonth;
    if (activeMonth == null && reportingMonthId.HasValue)
      activeMonth = await _db.ReportingMonths.FirstOrDefaultAsync(m => m.Id == reportingMonthId.Value);
    if (activeMonth == null)
      activeMonth = await _db.ReportingMonths.FirstOrDefaultAsync(m => m.IsActive);
    if (activeMonth == null)
    {
      ViewBag.Error = "אין חודש דיווח פעיל כרגע";
      return View("NoActiveMonth");
    }

    var employee = await _db.Users
      .Include(u => u.Role)
      .FirstOrDefaultAsync(u => u.Id == targetUserId);
    if (employee == null) return NotFound();

    var allocations = await _db.Allocations
      .Include(a => a.Project)
      .Where(a => a.UserId == targetUserId && a.IsActive)
      .ToListAsync();

    if (allocations.Count == 0)
    {
      ViewBag.Error = "אין הקצאה פעילה לעובד זה";
      return View("NoAllocation");
    }

    Allocation selectedAllocation;
    if (allocationId.HasValue)
      selectedAllocation = allocations.FirstOrDefault(a => a.Id == allocationId.Value) ?? allocations[0];
    else if (allocations.Count == 1)
      selectedAllocation = allocations[0];
    else
    {
      ViewBag.Employee = employee;
      ViewBag.Allocations = allocations;
      ViewBag.ActiveMonth = activeMonth;
      return View("SelectAllocation");
    }

    // Existing report for the month if any; new drafts are only created for the
    // ACTIVE month. Closed months without a report render a read-only placeholder.
    var report = requestedReport
      ?? await _db.Reports
        .Include(r => r.ReportingMonth)
        .FirstOrDefaultAsync(r => r.UserId == targetUserId && r.ReportingMonthId == activeMonth.Id);
    if (report == null && activeMonth.IsActive)
      report = await _statusService.GetOrCreateDraftAsync(targetUserId, activeMonth.Id);
    report ??= new Report
    {
      UserId = targetUserId,
      ReportingMonthId = activeMonth.Id,
      ReportingMonth = activeMonth,
      StatusId = 1
    };

    var rows = await _db.ReportRows
      .Include(r => r.ReportType)
      .Include(r => r.District)
      .Include(r => r.Locality)
      .Include(r => r.Framework)
      .Include(r => r.EducationalProgram)
      .Include(r => r.Domain)
      .Include(r => r.Subject1)
      .Include(r => r.Subject2)
      .Include(r => r.DiscussionCode)
      .Include(r => r.ConclusionClass)
      .Include(r => r.ConclusionFramework)
      .Include(r => r.ConclusionLocation)
      .Include(r => r.GradeLevel)
      .Include(r => r.Class)
      .Where(r => r.ReportId == report.Id && r.AllocationId == selectedAllocation.Id)
      .OrderBy(r => r.MeetingDate)
      .ThenBy(r => r.SequenceNumber)
      .ToListAsync();

    ApplyFrameworkLabels(rows, await BuildFrameworkLabelsAsync(
      rows.Select(r => (int?)r.FrameworkId).Concat(rows.Select(r => r.ConclusionFrameworkId))));

    var rowIds = rows.Select(r => r.Id).ToList();
    ViewBag.ReportAttachments = await _db.DocumentAttachments
      .Where(a => a.ReportId == report.Id ||
                  (a.ReportRowId.HasValue && rowIds.Contains(a.ReportRowId.Value)))
      .OrderByDescending(a => a.UploadedAt)
      .ToListAsync();

    var allocationWithJunctions = await _db.Allocations
      .Include(a => a.ReportType)
      .Include(a => a.AllocationDistricts).ThenInclude(x => x.District)
      .Include(a => a.AllocationLocalities).ThenInclude(x => x.Locality)
      .Include(a => a.AllocationFrameworks).ThenInclude(x => x.Framework)
      .Include(a => a.AllocationEducationalPrograms).ThenInclude(x => x.EducationalProgram)
      .Include(a => a.AllocationDomains).ThenInclude(x => x.Domain)
      .Include(a => a.AllocationSubjects).ThenInclude(x => x.Subject)
      .Include(a => a.AllocationDiscussionCodes).ThenInclude(x => x.DiscussionCode)
      .Include(a => a.AllocationClasses).ThenInclude(x => x.SchoolClass)
      .Include(a => a.AllocationGradeLevels).ThenInclude(x => x.GradeLevel)
      .Include(a => a.AllocationLocalityDistrictNationals).ThenInclude(x => x.LocalityDistrictNational)
      .AsSplitQuery()
      .FirstOrDefaultAsync(a => a.Id == selectedAllocation.Id);

    if (allocationWithJunctions != null)
    {
      var labels = await BuildFrameworkLabelsAsync(
        allocationWithJunctions.AllocationFrameworks.Select(x => (int?)x.FrameworkId));
      foreach (var af in allocationWithJunctions.AllocationFrameworks)
      {
        if (af.Framework != null && labels.TryGetValue(af.Framework.Id, out var label))
          af.Framework.Description = label;
      }
    }

    report.ReportingMonth ??= activeMonth;
    var deadlinePassed = IsDeadlinePassed(activeMonth);
    var isOverrideRole = IsDeadlineOverrideRole();

    ViewBag.Employee = employee;
    ViewBag.ActiveMonth = activeMonth;
    ViewBag.Report = report;
    ViewBag.Allocation = allocationWithJunctions;
    ViewBag.Allocations = allocations;
    ViewBag.AllocationId = selectedAllocation.Id;
    ViewBag.EditRowId = editRowId;
    ViewBag.ReturnUrl = NormalizeLocalReturnUrl(returnUrl);
    if (manual)
    {
      ViewData["ManualLocalities"] = (await _db.Localities
          .Where(l => l.IsActive)
          .OrderBy(l => l.Description)
          .ToListAsync())
        .Where(IsCityLocality)
        .ToList();
    }
    ViewBag.CanEdit = CanEditReport(report);
    ViewBag.RequiredReportFields = await GetRequiredReportFieldsAsync();
    ViewBag.DeadlinePassed = deadlinePassed;
    ViewBag.DeadlineOverrideActive = deadlinePassed && isOverrideRole;
    ViewBag.DeadlineBlockMessage = deadlinePassed && !isOverrideRole ? DeadlinePassedMessage : null;

    return View("Index", rows);
  }

  /// <summary>
  /// Read-only list of the employee's reports from previous months (excluding
  /// archived ones), rendered as a self-contained HTML page.
  /// </summary>
  [HttpGet]
  [Route("Report/History")]
  public async Task<IActionResult> History(int? userId = null)
  {
    var targetUserId = _currentUser.UserRole == UserRoleEnum.Employee
      ? _currentUser.UserId
      : userId ?? _currentUser.UserId;
    if (!await CanViewEmployeeReportAsync(targetUserId)) return Forbid();

    var employee = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == targetUserId);
    if (employee == null) return NotFound();

    var reports = await _db.Reports.AsNoTracking()
      .Include(r => r.ReportingMonth)
      .Include(r => r.Status)
      .Where(r => r.UserId == targetUserId && !r.IsArchived)
      .OrderByDescending(r => r.ReportingMonth!.Year)
      .ThenByDescending(r => r.ReportingMonth!.Month)
      .ThenByDescending(r => r.Id)
      .ToListAsync();

    var reportIds = reports.Select(r => r.Id).ToList();
    var rowStats = new Dictionary<int, (int RowCount, int? AllocationId)>();
    if (reportIds.Count > 0)
    {
      rowStats = (await _db.ReportRows.AsNoTracking()
          .Where(r => reportIds.Contains(r.ReportId))
          .Select(r => new { r.ReportId, r.AllocationId })
          .ToListAsync())
        .GroupBy(r => r.ReportId)
        .ToDictionary(
          g => g.Key,
          g => (g.Count(), g.Where(r => r.AllocationId.HasValue).Select(r => r.AllocationId).FirstOrDefault()));
    }

    var sb = new StringBuilder();
    var fullName = $"{employee.FirstName} {employee.LastName}".Trim();
    sb.Append("<!doctype html><html lang=\"he\" dir=\"rtl\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link rel=\"stylesheet\" href=\"/lib/bootstrap/dist/css/bootstrap.min.css\"><title>היסטוריית דיווחים</title></head><body><main class=\"container py-4\">");
    sb.Append("<div class=\"d-flex justify-content-between align-items-start gap-3 flex-wrap mb-3\"><div><h1 class=\"h3 mb-2\">היסטוריית דיווחים</h1><div class=\"text-muted\">");
    sb.Append(WebUtility.HtmlEncode(fullName));
    if (!string.IsNullOrWhiteSpace(employee.IdNumber))
    {
      sb.Append(" | ת.ז ");
      sb.Append(WebUtility.HtmlEncode(employee.IdNumber));
    }
    sb.Append("</div></div><div class=\"d-flex gap-2\"><a class=\"btn btn-outline-secondary btn-sm\" href=\"/MyAllocations\">פעילות חודשית</a><a class=\"btn btn-primary btn-sm\" href=\"/Report\">דיווח נוכחי</a></div></div>");
    sb.Append("<div class=\"alert alert-info\">ניתן לצפות כאן בדיווחים מחודשים קודמים. חודשים שאינם פעילים נפתחים לקריאה בלבד.</div>");

    if (reports.Count == 0)
    {
      sb.Append("<div class=\"card\"><div class=\"card-body text-muted\">אין דיווחים קודמים להצגה.</div></div>");
    }
    else
    {
      sb.Append("<div class=\"table-responsive\"><table class=\"table table-striped table-hover align-middle\"><thead class=\"table-light\"><tr><th>חודש דיווח</th><th>מצב חודש</th><th>סטטוס דיווח</th><th>שורות</th><th>עודכן</th><th class=\"text-nowrap\">פעולה</th></tr></thead><tbody>");
      foreach (var rep in reports)
      {
        var month = rep.ReportingMonth;
        rowStats.TryGetValue(rep.Id, out var stats);

        var link = "/Report?reportingMonthId=" + rep.ReportingMonthId.ToString(CultureInfo.InvariantCulture);
        if (stats.AllocationId.HasValue)
          link += "&allocationId=" + stats.AllocationId.Value.ToString(CultureInfo.InvariantCulture);
        if (_currentUser.UserRole != UserRoleEnum.Employee)
          link += "&userId=" + targetUserId.ToString(CultureInfo.InvariantCulture);

        var updated = rep.UpdatedAt ?? rep.CreatedAt;
        var updatedText = updated == default ? "" : updated.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        var monthText = month?.Description ?? ("חודש " + rep.ReportingMonthId.ToString(CultureInfo.InvariantCulture));
        var monthState = month != null && month.IsActive ? "פעיל" : "סגור";
        var statusText = rep.Status?.Description ?? rep.Status?.Name ?? rep.StatusId.ToString(CultureInfo.InvariantCulture);
        var actionText = month != null && month.IsActive ? "פתיחה" : "צפייה";

        sb.Append("<tr><td>").Append(WebUtility.HtmlEncode(monthText)).Append("</td><td>")
          .Append(WebUtility.HtmlEncode(monthState))
          .Append("</td><td>")
          .Append(WebUtility.HtmlEncode(statusText))
          .Append("</td><td>")
          .Append(stats.RowCount.ToString(CultureInfo.InvariantCulture))
          .Append("</td><td>")
          .Append(WebUtility.HtmlEncode(updatedText))
          .Append("</td><td><a class=\"btn btn-sm btn-outline-primary\" href=\"")
          .Append(WebUtility.HtmlEncode(link))
          .Append("\">")
          .Append(WebUtility.HtmlEncode(actionText))
          .Append("</a></td></tr>");
      }
      sb.Append("</tbody></table></div>");
    }

    sb.Append("</main><script src=\"/lib/bootstrap/dist/js/bootstrap.bundle.min.js\"></script></body></html>");
    return Content(sb.ToString(), "text/html; charset=utf-8");
  }

  private static bool IsDeadlinePassed(ReportingMonth? month)
  {
    if (month == null) return false;
    return DateTime.Today > month.LastReportingDate.Date;
  }

  /// <summary>Manual-report entry point: choose employee/allocation/month.</summary>
  [HttpGet]
  [Route("Report/Manual")]
  [Route("Report/Manuel")]
  public async Task<IActionResult> Manual()
  {
    var reportingMonths = await _db.ReportingMonths
      .OrderByDescending(m => m.Year)
      .ThenByDescending(m => m.Month)
      .ToListAsync();

    return View("~/Views/Report/Manual.cshtml", new ManualReportViewModel
    {
      ReportingMonths = reportingMonths
    });
  }

  [HttpGet]
  [Route("Report/ManualEmployeeSearch")]
  public async Task<IActionResult> ManualEmployeeSearch(string? idNumber, string? employeeCode, string? firstName, string? lastName)
  {
    var query = _db.Users.Where(u => u.StatusId == 1 && u.IsReportingEmployee);
    if (_currentUser.UserRole == UserRoleEnum.Employee)
      query = query.Where(u => u.Id == _currentUser.UserId);

    if (!string.IsNullOrWhiteSpace(idNumber))
    {
      var raw = idNumber.Trim();
      var digits = NormalizeDigits(raw);
      query = string.IsNullOrWhiteSpace(digits)
        ? query.Where(u => u.IdNumber.Contains(raw))
        : query.Where(u => u.IdNumber.Contains(raw) ||
                           u.IdNumber.Replace("-", "").Replace(" ", "").Contains(digits));
    }

    if (!string.IsNullOrWhiteSpace(employeeCode))
    {
      var code = employeeCode.Trim();
      query = query.Where(u => u.EmployeeCode.Contains(code));
    }

    if (!string.IsNullOrWhiteSpace(firstName))
    {
      var first = firstName.Trim();
      query = query.Where(u => u.FirstName.Contains(first));
    }

    if (!string.IsNullOrWhiteSpace(lastName))
    {
      var last = lastName.Trim();
      query = query.Where(u => u.LastName.Contains(last));
    }

    var employees = await query
      .OrderBy(u => u.LastName)
      .ThenBy(u => u.FirstName)
      .Take(30)
      .Select(u => new
      {
        id = u.Id,
        idNumber = u.IdNumber,
        employeeCode = u.EmployeeCode,
        firstName = u.FirstName,
        lastName = u.LastName
      })
      .ToListAsync();

    var employeeIds = employees.Select(e => e.id).ToList();
    var allocations = await _db.Allocations
      .Include(a => a.Project)
      .Where(a => a.IsActive && employeeIds.Contains(a.UserId))
      .OrderBy(a => a.Project!.Description)
      .Select(a => new
      {
        id = a.Id,
        userId = a.UserId,
        projectName = a.Project != null ? a.Project.Description : ""
      })
      .ToListAsync();

    return Json(new { employees, allocations });
  }

  [HttpGet]
  public async Task<IActionResult> ManualOpen(int userId, int allocationId, int reportingMonthId)
  {
    if (!await CanViewEmployeeReportAsync(userId)) return Forbid();

    var allocation = await _db.Allocations.FirstOrDefaultAsync(a => a.Id == allocationId && a.IsActive);
    if (allocation == null)
    {
      TempData["ManualError"] = "ההקצאה שנבחרה אינה פעילה או לא קיימת.";
      return RedirectToAction(nameof(Manual));
    }
    if (allocation.UserId != userId)
    {
      TempData["ManualError"] = "יש לבחור הקצאה ששייכת לעובד שנבחר.";
      return RedirectToAction(nameof(Manual));
    }

    var report = await _statusService.GetOrCreateDraftAsync(userId, reportingMonthId);
    return RedirectToAction(nameof(Index), new { userId, allocationId, reportId = report.Id, manual = true });
  }

  [HttpGet]
  [Route("Report/FrameworkLabels")]
  public async Task<IActionResult> FrameworkLabels(string? ids)
  {
    var frameworkIds = (ids ?? string.Empty)
      .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
      .Select(value => int.TryParse(value, out var parsed) ? (int?)parsed : null)
      .Where(id => id.HasValue)
      .Distinct()
      .ToList();

    return Json((await BuildFrameworkLabelsAsync(frameworkIds))
      .Select(kvp => new { id = kvp.Key, text = kvp.Value }));
  }

  [HttpGet]
  public IActionResult DownloadExcelTemplate()
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("דיווחים");
    ws.RightToLeft = true;

    var headers = new[]
    {
      "MeetingDate", "MeetingDuration", "DistrictId", "LocalityId", "FrameworkId",
      "EducationalProgramId", "DomainId", "Subject1Id", "Subject2Id", "DiscussionCodeId",
      "ConclusionClassId", "ConclusionFrameworkId", "ConclusionLocationId", "GradeLevelId",
      "ClassId", "Notes"
    };

    for (var i = 0; i < headers.Length; i++)
    {
      ws.Cell(1, i + 1).Value = headers[i];
      ws.Cell(1, i + 1).Style.Font.Bold = true;
    }

    ws.Cell(2, 1).Value = DateTime.Today;
    ws.Cell(2, 1).Style.DateFormat.Format = "dd/MM/yyyy";
    ws.Cell(2, 2).Value = 1.0;
    ws.Cell(2, 16).Value = "לדוגמה - יש למחוק לפני העלאת הקובץ";
    ws.Columns().AdjustToContents();

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    return File(
      stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      "report_upload_template.xlsx");
  }

  /// <summary>
  /// Personal export: the current user's report rows for one of their own
  /// allocations, as an RTL XLSX with an employee/month header block.
  /// </summary>
  [HttpGet]
  public async Task<IActionResult> ExportMine(int allocationId, int? reportingMonthId = null)
  {
    var allocation = await _db.Allocations
      .Include(a => a.User)
      .Include(a => a.Project)
      .Include(a => a.ReportType)
      .Include(a => a.AllocationPrograms).ThenInclude(ap => ap.Program)
      .FirstOrDefaultAsync(a => a.Id == allocationId && a.UserId == _currentUser.UserId && a.IsActive);
    if (allocation == null) return Forbid();

    var month = reportingMonthId.HasValue
      ? await _db.ReportingMonths.FirstOrDefaultAsync(m => m.Id == reportingMonthId.Value)
      : await _db.ReportingMonths
        .Where(m => m.IsActive)
        .OrderByDescending(m => m.Year)
        .ThenByDescending(m => m.Month)
        .FirstOrDefaultAsync();
    if (month == null) return NotFound();

    var report = await _db.Reports
      .Include(r => r.Status)
      .FirstOrDefaultAsync(r => r.UserId == _currentUser.UserId && r.ReportingMonthId == month.Id);
    if (report == null) return NotFound();

    var rows = await _db.ReportRows
      .Include(r => r.ReportType)
      .Include(r => r.District)
      .Include(r => r.Locality)
      .Include(r => r.Framework)
      .Include(r => r.EducationalProgram)
      .Include(r => r.Domain)
      .Include(r => r.Subject1)
      .Include(r => r.Subject2)
      .Include(r => r.DiscussionCode)
      .Include(r => r.ConclusionClass)
      .Include(r => r.ConclusionFramework)
      .Include(r => r.ConclusionLocation)
      .Include(r => r.GradeLevel)
      .Include(r => r.Class)
      .Where(r => r.ReportId == report.Id && r.AllocationId == allocationId)
      .OrderBy(r => r.SequenceNumber)
      .ThenBy(r => r.MeetingDate)
      .ToListAsync();

    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("דיווחים");
    ws.RightToLeft = true;
    ws.Style.Font.FontName = "Arial";
    ws.Style.Font.FontSize = 11.0;

    var employee = allocation.User;
    var fullName = $"{employee?.FirstName ?? string.Empty} {employee?.LastName ?? string.Empty}".Trim();
    ws.Cell(1, 1).Value = "עובד";
    ws.Cell(1, 2).Value = fullName;
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
    ws.Cell(3, 2).Value = string.Join(", ", allocation.AllocationPrograms
      .Select(ap => ap.Program?.Description)
      .Where(p => !string.IsNullOrWhiteSpace(p)));

    var headers = new[]
    {
      "מספר שורה", "סוג דיווח", "תאריך מפגש", "משך תפוקה", "מחוז", "יישוב", "מסגרת", "תוכנית חינוכית", "תחום", "נושא 1",
      "נושא 2", "קיום דיון", "מסקנה - כיתה", "מסקנה - מסגרת", "מסקנה - מיקום", "שכבה", "כיתה", "הערות"
    };
    const int headerRow = 5;
    for (var i = 0; i < headers.Length; i++)
    {
      ws.Cell(headerRow, i + 1).Value = headers[i];
      ws.Cell(headerRow, i + 1).Style.Font.Bold = true;
      ws.Cell(headerRow, i + 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#D9EAF7");
    }

    var rowNum = headerRow + 1;
    foreach (var row in rows)
    {
      ws.Cell(rowNum, 1).Value = row.SequenceNumber;
      ws.Cell(rowNum, 2).Value = row.ReportType?.Description ?? allocation.ReportType?.Description ?? string.Empty;
      ws.Cell(rowNum, 3).Value = row.MeetingDate;
      ws.Cell(rowNum, 3).Style.DateFormat.Format = "dd/MM/yyyy";
      ws.Cell(rowNum, 4).Value = row.MeetingDuration;
      ws.Cell(rowNum, 5).Value = row.District?.Description ?? string.Empty;
      ws.Cell(rowNum, 6).Value = row.Locality?.Description ?? string.Empty;
      ws.Cell(rowNum, 7).Value = row.Framework?.Description ?? string.Empty;
      ws.Cell(rowNum, 8).Value = row.EducationalProgram?.Description ?? string.Empty;
      ws.Cell(rowNum, 9).Value = row.Domain?.Description ?? string.Empty;
      ws.Cell(rowNum, 10).Value = row.Subject1?.Description ?? string.Empty;
      ws.Cell(rowNum, 11).Value = row.Subject2?.Description ?? string.Empty;
      ws.Cell(rowNum, 12).Value = row.DiscussionCode?.Description ?? string.Empty;
      ws.Cell(rowNum, 13).Value = row.ConclusionClass?.Description ?? string.Empty;
      ws.Cell(rowNum, 14).Value = row.ConclusionFramework?.Description ?? string.Empty;
      ws.Cell(rowNum, 15).Value = row.ConclusionLocation?.Description ?? string.Empty;
      ws.Cell(rowNum, 16).Value = row.GradeLevel?.Description ?? string.Empty;
      ws.Cell(rowNum, 17).Value = row.Class?.Description ?? string.Empty;
      ws.Cell(rowNum, 18).Value = row.Notes ?? string.Empty;
      rowNum++;
    }

    ws.Range(headerRow, 1, Math.Max(headerRow, rowNum - 1), headers.Length).SetAutoFilter();
    ws.SheetView.FreezeRows(headerRow);
    ws.Columns().AdjustToContents();

    await _auditLog.LogAsync("Report.ExportMine", "Report", report.Id.ToString(CultureInfo.InvariantCulture),
      null, null, $"allocationId={allocationId}; reportingMonthId={month.Id}");

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    var fileName = $"employee-report-{SafeFilePart(employee?.EmployeeCode ?? _currentUser.UserId.ToString(CultureInfo.InvariantCulture))}-{month.Year}-{month.Month:00}.xlsx";
    return File(
      stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      fileName);
  }

  /// <summary>Full-month export for a single report across all its allocations.</summary>
  [HttpGet]
  public async Task<IActionResult> ExportReportMonth(int reportId)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .Include(r => r.Status)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(report.UserId)) return Forbid();

    // Inspector roles may only export reports that have been approved.
    if (_currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval &&
        report.StatusId != 4)
      return Forbid();

    var rows = await _db.ReportRows
      .Include(r => r.ReportType)
      .Include(r => r.Allocation).ThenInclude(a => a!.Project)
      .Include(r => r.Allocation).ThenInclude(a => a!.ReportType)
      .Include(r => r.Allocation).ThenInclude(a => a!.AllocationPrograms).ThenInclude(ap => ap.Program)
      .Include(r => r.District)
      .Include(r => r.Locality)
      .Include(r => r.Framework)
      .Include(r => r.EducationalProgram)
      .Include(r => r.Domain)
      .Include(r => r.Subject1)
      .Include(r => r.Subject2)
      .Include(r => r.DiscussionCode)
      .Include(r => r.ConclusionClass)
      .Include(r => r.ConclusionFramework)
      .Include(r => r.ConclusionLocation)
      .Include(r => r.GradeLevel)
      .Include(r => r.Class)
      .AsSplitQuery()
      .Where(r => r.ReportId == report.Id)
      .OrderBy(r => r.AllocationId)
      .ThenBy(r => r.SequenceNumber)
      .ThenBy(r => r.MeetingDate)
      .ToListAsync();

    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("דיווחים");
    ws.RightToLeft = true;
    ws.Style.Font.FontName = "Arial";
    ws.Style.Font.FontSize = 11.0;

    var employee = report.User;
    var month = report.ReportingMonth;
    var fullName = $"{employee?.FirstName ?? string.Empty} {employee?.LastName ?? string.Empty}".Trim();
    ws.Cell(1, 1).Value = "עובד";
    ws.Cell(1, 2).Value = fullName;
    ws.Cell(1, 4).Value = "ת.ז.";
    ws.Cell(1, 5).Value = employee?.IdNumber ?? string.Empty;
    ws.Cell(2, 1).Value = "חודש דיווח";
    ws.Cell(2, 2).Value = month?.Description ?? string.Empty;
    ws.Cell(2, 4).Value = "סטטוס דיווח";
    ws.Cell(2, 5).Value = report.Status?.Description ?? report.Status?.Name ?? report.StatusId.ToString(CultureInfo.InvariantCulture);
    ws.Cell(3, 1).Value = "תוכניות";
    ws.Cell(3, 2).Value = string.Join(", ", rows
      .SelectMany(r => r.Allocation?.AllocationPrograms ?? new List<AllocationProgram>())
      .Select(ap => ap.Program?.Description)
      .Where(p => !string.IsNullOrWhiteSpace(p))
      .Distinct());
    ws.Cell(3, 4).Value = "סה\"כ משך תפוקה";
    ws.Cell(3, 5).Value = rows.Sum(r => r.MeetingDuration);

    var headers = new[]
    {
      "מספר שורה", "פרויקט", "סוג דיווח", "הקצאה", "תאריך מפגש", "משך תפוקה", "מחוז", "יישוב", "מסגרת", "תוכנית חינוכית",
      "תחום", "נושא 1", "נושא 2", "קיום דיון", "מסקנה - כיתה", "מסקנה - מסגרת", "מסקנה - מיקום", "שכבה/כיתה", "הערות"
    };
    const int headerRow = 5;
    for (var i = 0; i < headers.Length; i++)
    {
      ws.Cell(headerRow, i + 1).Value = headers[i];
      ws.Cell(headerRow, i + 1).Style.Font.Bold = true;
      ws.Cell(headerRow, i + 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#D9EAF7");
    }

    var rowNum = headerRow + 1;
    foreach (var row in rows)
    {
      ws.Cell(rowNum, 1).Value = row.SequenceNumber;
      ws.Cell(rowNum, 2).Value = row.Allocation?.Project?.Description ?? string.Empty;
      ws.Cell(rowNum, 3).Value = row.ReportType?.Description ?? row.Allocation?.ReportType?.Description ?? string.Empty;
      ws.Cell(rowNum, 4).Value = row.AllocationId?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
      ws.Cell(rowNum, 5).Value = row.MeetingDate;
      ws.Cell(rowNum, 5).Style.DateFormat.Format = "dd/MM/yyyy";
      ws.Cell(rowNum, 6).Value = row.MeetingDuration;
      ws.Cell(rowNum, 7).Value = row.District?.Description ?? string.Empty;
      ws.Cell(rowNum, 8).Value = row.Locality?.Description ?? string.Empty;
      ws.Cell(rowNum, 9).Value = row.Framework?.Description ?? string.Empty;
      ws.Cell(rowNum, 10).Value = row.EducationalProgram?.Description ?? string.Empty;
      ws.Cell(rowNum, 11).Value = row.Domain?.Description ?? string.Empty;
      ws.Cell(rowNum, 12).Value = row.Subject1?.Description ?? string.Empty;
      ws.Cell(rowNum, 13).Value = row.Subject2?.Description ?? string.Empty;
      ws.Cell(rowNum, 14).Value = row.DiscussionCode?.Description ?? string.Empty;
      ws.Cell(rowNum, 15).Value = row.ConclusionClass?.Description ?? string.Empty;
      ws.Cell(rowNum, 16).Value = row.ConclusionFramework?.Description ?? string.Empty;
      ws.Cell(rowNum, 17).Value = row.ConclusionLocation?.Description ?? string.Empty;
      ws.Cell(rowNum, 18).Value = row.GradeLevel?.Description ?? row.Class?.Description ?? string.Empty;
      ws.Cell(rowNum, 19).Value = row.Notes ?? string.Empty;
      rowNum++;
    }

    ws.Range(headerRow, 1, Math.Max(headerRow, rowNum - 1), headers.Length).SetAutoFilter();
    ws.SheetView.FreezeRows(headerRow);
    ws.Columns().AdjustToContents();

    await _auditLog.LogAsync("Report.ExportReportMonth", "Report", report.Id.ToString(CultureInfo.InvariantCulture),
      null, null, $"reportingMonthId={report.ReportingMonthId}; userId={report.UserId}");

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    var fileName = $"monthly-report-{SafeFilePart(employee?.EmployeeCode ?? report.UserId.ToString(CultureInfo.InvariantCulture))}-{month?.Year ?? 0}-{month?.Month ?? 0:00}.xlsx";
    return File(
      stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      fileName);
  }

  [HttpGet]
  public async Task<IActionResult> GetRow(int rowId)
  {
    var row = await _db.ReportRows
      .Include(r => r.Report)
      .FirstOrDefaultAsync(r => r.Id == rowId);
    if (row?.Report == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(row.Report.UserId)) return Forbid();

    return Json(new
    {
      row.Id,
      meetingDate = row.MeetingDate.ToString("yyyy-MM-dd"),
      row.MeetingDuration,
      row.DistrictId,
      row.LocalityId,
      row.FrameworkId,
      row.EducationalProgramId,
      row.DomainId,
      row.Subject1Id,
      row.Subject2Id,
      row.DiscussionCodeId,
      row.ConclusionClassId,
      row.ConclusionFrameworkId,
      row.ConclusionLocationId,
      row.GradeLevelId,
      row.ClassId,
      row.Notes,
      rowVersion = Convert.ToBase64String(row.RowVersion ?? Array.Empty<byte>())
    });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> SaveRow(ReportRow row, int reportId, int allocationId, string? rowVersion = null)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return Json(new { success = false, error = "דיווח לא נמצא" });
    if (!await CanViewEmployeeReportAsync(report.UserId) || !CanEditReport(report))
      return Json(new { success = false, error = EditBlockMessage(report) });

    var allocation = await _db.Allocations.FirstOrDefaultAsync(a =>
      a.Id == allocationId && a.UserId == report.UserId && a.IsActive);
    if (allocation == null)
      return Json(new { success = false, error = "הקצאה לא תקינה" });

    if (row.Id != 0 && !await _db.ReportRows.AnyAsync(r => r.Id == row.Id && r.ReportId == reportId))
      return Json(new { success = false, error = "שורה לא נמצאה" });

    var existingRows = await _db.ReportRows
      .Where(r => r.ReportId == reportId && r.Id != row.Id)
      .ToListAsync();

    row.AllocationId = allocationId;
    row.ReportId = reportId;
    // Default the row's report type from the allocation when not explicitly set.
    if (!row.ReportTypeId.HasValue && allocation.ReportTypeId.HasValue)
      row.ReportTypeId = allocation.ReportTypeId;

    var allRows = existingRows.Concat(new[] { row }).ToList();
    var validation = await _validator.ValidateRowAsync(row, report.User!, report.ReportingMonth!, allRows);
    if (!validation.IsValid)
      return Json(new { success = false, errors = validation.Errors });

    if (row.Id == 0)
    {
      var nextSeq = (await _db.ReportRows
        .Where(r => r.ReportId == reportId)
        .MaxAsync(r => (int?)r.SequenceNumber) ?? 0) + 1;
      row.SequenceNumber = nextSeq;
      row.CreatedAt = DateTime.UtcNow;
      _db.ReportRows.Add(row);
    }
    else
    {
      var existing = await _db.ReportRows.FirstAsync(r => r.Id == row.Id && r.ReportId == reportId);
      CopyEditableFields(row, existing);
      existing.AllocationId = allocationId;
      existing.UpdatedAt = DateTime.UtcNow;
      if (TryParseRowVersion(rowVersion, out var token))
        _db.Entry(existing).OriginalValues["RowVersion"] = token;
    }

    try
    {
      await _db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      return Json(new { success = false, error = ConcurrencyConflictMessage });
    }
    await _statusService.SaveDraftAsync(reportId);
    var savedRow = row.Id == 0
      ? row
      : await _db.ReportRows.AsNoTracking().FirstAsync(r => r.Id == row.Id && r.ReportId == reportId);
    return Json(new
    {
      success = true,
      warnings = validation.Warnings,
      rowId = savedRow.Id,
      rowVersion = Convert.ToBase64String(savedRow.RowVersion ?? Array.Empty<byte>())
    });
  }

  private static bool TryParseRowVersion(string? value, out byte[] bytes)
  {
    bytes = Array.Empty<byte>();
    if (string.IsNullOrWhiteSpace(value)) return false;
    try
    {
      bytes = Convert.FromBase64String(value);
      return bytes.Length > 0;
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
    var row = await _db.ReportRows
      .Include(r => r.Report).ThenInclude(rep => rep!.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == rowId);
    if (row?.Report == null) return Json(new { success = false });
    if (!await CanViewEmployeeReportAsync(row.Report.UserId) || !CanEditReport(row.Report))
      return Json(new { success = false, error = EditBlockMessage(row.Report) });

    if (TryParseRowVersion(rowVersion, out var token))
      _db.Entry(row).OriginalValues["RowVersion"] = token;
    _db.ReportRows.Remove(row);
    try
    {
      await _db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      return Json(new { success = false, error = ConcurrencyConflictMessage });
    }
    return Json(new { success = true });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Submit(int reportId, int? allocationId = null, string? rowVersion = null, string? returnUrl = null)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(report.UserId) || !CanEditReport(report))
    {
      TempData["Errors"] = EditBlockMessage(report);
      return RedirectToReport(report.UserId, allocationId ?? 0, reportId, returnUrl);
    }

    var validation = await _validator.ValidateSubmitAsync(report, report.User!, report.ReportingMonth!);
    if (!validation.IsValid)
    {
      TempData["Errors"] = string.Join("|", validation.Errors);
      return RedirectToReport(report.UserId, allocationId ?? 0, reportId, returnUrl);
    }

    if (TryParseRowVersion(rowVersion, out var token))
      _db.Entry(report).OriginalValues["RowVersion"] = token;

    try
    {
      await _statusService.SubmitReportAsync(reportId, _currentUser.UserId);
    }
    catch (DbUpdateConcurrencyException)
    {
      TempData["Errors"] = ConcurrencyConflictMessage;
      return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
    }
    TempData["Success"] = "הדיווח הוגש בהצלחה";
    return RedirectToReport(report.UserId, allocationId ?? 0, reportId, returnUrl);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.CanApproveReports)]
  public async Task<IActionResult> Approve(int reportId, string? returnUrl = null, string? rowVersion = null)
  {
    if (!await CanApproveReportAsync(reportId)) return Forbid();

    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();

    // Idempotent: approving an already-approved report succeeds silently.
    if (report.StatusId == 4)
    {
      TempData["Success"] = "הדיווח אושר";
      if (WantsJson())
        return Json(new { success = true, status = "Approved", reportId });
      return RedirectBackToDashboard(returnUrl);
    }

    if (TryParseRowVersion(rowVersion, out var token))
      _db.Entry(report).OriginalValues["RowVersion"] = token;

    var previousStatus = report.StatusId;
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
      TempData["Error"] = ConcurrencyConflictMessage;
      if (WantsJson())
        return Json(new { success = false, error = ConcurrencyConflictMessage });
      return RedirectBackToDashboard(returnUrl);
    }

    await _auditLog.LogAsync("Report.StatusChange", "Report", report.Id.ToString(CultureInfo.InvariantCulture),
      new { StatusId = previousStatus }, new { report.StatusId },
      $"approved by user {_currentUser.UserId}");
    await TrySendApprovalEmailAsync(report);

    TempData["Success"] = "הדיווח אושר";
    if (WantsJson())
      return Json(new { success = true, status = "Approved", reportId });
    return RedirectBackToDashboard(returnUrl);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.CanApproveReports)]
  public async Task<IActionResult> Reject(int reportId, string rejectionReason, string? returnUrl = null, string? rowVersion = null)
  {
    if (!await CanApproveReportAsync(reportId)) return Forbid();
    if (string.IsNullOrWhiteSpace(rejectionReason))
    {
      TempData["Error"] = "יש לציין סיבת דחייה";
      if (WantsJson())
        return Json(new { success = false, error = "יש לציין סיבת דחייה" });
      return RedirectBackToDashboard(returnUrl);
    }

    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();

    // Idempotent: rejecting again with the same reason succeeds silently.
    if (report.StatusId == 5 &&
        string.Equals(report.RejectionReason ?? string.Empty, rejectionReason, StringComparison.Ordinal))
    {
      TempData["Success"] = "הדיווח הוחזר לתיקון";
      if (WantsJson())
        return Json(new { success = true, status = "Rejected", reportId });
      return RedirectBackToDashboard(returnUrl);
    }

    if (TryParseRowVersion(rowVersion, out var token))
      _db.Entry(report).OriginalValues["RowVersion"] = token;

    var previousStatus = report.StatusId;
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
      TempData["Error"] = ConcurrencyConflictMessage;
      if (WantsJson())
        return Json(new { success = false, error = ConcurrencyConflictMessage });
      return RedirectBackToDashboard(returnUrl);
    }

    await _auditLog.LogAsync("Report.StatusChange", "Report", report.Id.ToString(CultureInfo.InvariantCulture),
      new { StatusId = previousStatus }, new { report.StatusId, rejectionReason },
      $"rejected by user {_currentUser.UserId}");
    await TrySendRejectionEmailAsync(report, rejectionReason);

    TempData["Success"] = "הדיווח הוחזר לתיקון";
    if (WantsJson())
      return Json(new { success = true, status = "Rejected", reportId });
    return RedirectBackToDashboard(returnUrl);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> UploadExcel(int reportId, int allocationId, IFormFile file, string? returnUrl = null)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(report.UserId) || !CanEditReport(report))
    {
      TempData["Errors"] = EditBlockMessage(report);
      return RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
    }

    if (file == null || file.Length == 0)
    {
      TempData["Errors"] = "לא נבחר קובץ אקסל";
      return RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
    }

    var extension = Path.GetExtension(file.FileName);
    if (!extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
    {
      TempData["Errors"] = "ניתן להעלות קובץ xlsx בלבד";
      return RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
    }

    await using var stream = file.OpenReadStream();
    var result = await _excelImportService.ImportAsync(reportId, allocationId, stream, _currentUser.UserId);
    if (!result.Success)
    {
      var errorId = Guid.NewGuid().ToString("N");
      var errorsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "excel-errors");
      Directory.CreateDirectory(errorsDir);
      await System.IO.File.WriteAllBytesAsync(
        Path.Combine(errorsDir, $"{errorId}.xlsx"),
        CreateImportErrorsExcel(result.Errors));
      TempData["Errors"] = BuildImportErrorSummary(result.Errors);
      TempData["ExcelErrorFile"] = Url?.Content($"~/uploads/excel-errors/{errorId}.xlsx")
        ?? $"/uploads/excel-errors/{errorId}.xlsx";

      await SendImportFailureEmailAsync(report, result.Errors);
      return RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
    }

    TempData["Success"] = $"יובאו {result.ImportedRows} שורות מאקסל";
    return RedirectToReport(report.UserId, allocationId, reportId, returnUrl);
  }

  private async Task SendImportFailureEmailAsync(Report report, IReadOnlyCollection<string> errors)
  {
    if (report.User == null) return;
    if (string.IsNullOrWhiteSpace(report.User.Email))
    {
      _logger.LogInformation(
        "Skipping BatchImportErrors email for user {UserId} - no email address on file",
        report.UserId);
      return;
    }

    // Errors collected by ReportExcelImportService already begin with "שורה {rowNum}: ...".
    // Forward them verbatim, one per line, to preserve the Hebrew row-number prefix.
    var errorList = string.Join("\n", errors);
    var month = report.ReportingMonth;

    try
    {
      await _emailService.SendAsync(
        report.User.Email,
        $"{report.User.FirstName} {report.User.LastName}",
        "BatchImportErrors",
        new Dictionary<string, string>
        {
          ["UploaderName"] = $"{report.User.FirstName} {report.User.LastName}",
          ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}",
          ["ErrorsCount"] = errors.Count.ToString(CultureInfo.InvariantCulture),
          ["ErrorList"] = errorList,
          ["Month"] = month?.Month.ToString(CultureInfo.InvariantCulture) ?? "",
          ["Year"] = month?.Year.ToString(CultureInfo.InvariantCulture) ?? ""
        });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
        "Failed to enqueue BatchImportErrors email for user {UserId}", report.UserId);
    }
  }

  private static string BuildImportErrorSummary(IReadOnlyCollection<string> errors)
  {
    var shown = errors.Take(3).Select(TrimImportError).ToArray();
    var summary = string.Join("|", shown);
    if (errors.Count > shown.Length)
      summary += $"|Excel import found {errors.Count} errors. Open the Excel error file for the full list.";
    return summary;
  }

  private static byte[] CreateImportErrorsExcel(IEnumerable<string> errors)
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("שגיאות יבוא");
    ws.RightToLeft = true;
    ws.Cell(1, 1).Value = "מספר";
    ws.Cell(1, 2).Value = "שגיאה";

    var row = 2;
    foreach (var error in errors)
    {
      ws.Cell(row, 1).Value = row - 1;
      ws.Cell(row, 2).Value = error ?? string.Empty;
      row++;
    }

    ws.Row(1).Style.Font.Bold = true;
    ws.Columns().AdjustToContents();

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    return stream.ToArray();
  }

  private static string TrimImportError(string value)
  {
    if (string.IsNullOrWhiteSpace(value)) return string.Empty;
    value = value.Trim();
    return value.Length > 220 ? value[..220] + "..." : value;
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> UploadAttachment(int reportId, IFormFile file, string? description)
  {
    var report = await _db.Reports
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return Json(new { success = false, error = "דיווח לא נמצא" });
    if (!await CanViewEmployeeReportAsync(report.UserId) || !CanEditReport(report))
      return Json(new { success = false, error = EditBlockMessage(report) });

    if (file == null || file.Length == 0)
      return Json(new { success = false, error = "לא נבחר קובץ" });
    if (file.Length > MaxAttachmentBytes)
      return Json(new { success = false, error = "גודל הקובץ חורג מהמותר" });

    var extension = Path.GetExtension(file.FileName);
    if (!AllowedAttachmentExtensions.Contains(extension))
      return Json(new { success = false, error = "סוג הקובץ אינו נתמך. ניתן להעלות PDF, Word או Excel בלבד" });

    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "attachments");
    Directory.CreateDirectory(uploadsDir);
    var fileName = $"{Guid.NewGuid()}{extension}";
    var filePath = Path.Combine(uploadsDir, fileName);

    await using (var stream = new FileStream(filePath, FileMode.CreateNew))
      await file.CopyToAsync(stream);

    var attachment = new DocumentAttachment
    {
      ReportId = reportId,
      FileName = Path.GetFileName(file.FileName),
      Description = NormalizeAttachmentDescription(description),
      FilePath = $"/uploads/attachments/{fileName}",
      FileSize = file.Length,
      MimeType = file.ContentType,
      UploadedAt = DateTime.UtcNow,
      UploadedBy = _currentUser.UserId
    };
    _db.DocumentAttachments.Add(attachment);
    await _db.SaveChangesAsync();

    return Json(new { success = true, id = attachment.Id, fileName = attachment.FileName, description = attachment.Description });
  }

  private static string? NormalizeAttachmentDescription(string? description)
  {
    if (string.IsNullOrWhiteSpace(description)) return null;
    description = description.Trim();
    return description.Length <= MaxAttachmentDescriptionLength
      ? description
      : description[..MaxAttachmentDescriptionLength];
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteAttachment(int attachmentId)
  {
    var attachment = await _db.DocumentAttachments
      .Include(a => a.Report).ThenInclude(rep => rep!.ReportingMonth)
      .Include(a => a.ReportRow).ThenInclude(r => r!.Report).ThenInclude(rep => rep!.ReportingMonth)
      .FirstOrDefaultAsync(a => a.Id == attachmentId);
    var report = attachment?.Report ?? attachment?.ReportRow?.Report;
    if (attachment == null || report == null) return Json(new { success = false });
    if (!await CanViewEmployeeReportAsync(report.UserId) || !CanEditReport(report))
      return Json(new { success = false, error = EditBlockMessage(report) });

    var fullPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      attachment.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
    if (System.IO.File.Exists(fullPath))
      System.IO.File.Delete(fullPath);

    _db.DocumentAttachments.Remove(attachment);
    await _db.SaveChangesAsync();
    return Json(new { success = true });
  }

  /// <summary>
  /// Builds "Locality, InstitutionSymbol, InstitutionName" labels for frameworks whose
  /// InstitutionSymbol matches an Institutions row. Falls back to the framework description.
  /// </summary>
  private async Task<Dictionary<int, string>> BuildFrameworkLabelsAsync(IEnumerable<int?> frameworkIds)
  {
    var ids = frameworkIds.Where(id => id.HasValue).Select(id => id!.Value).Distinct().ToList();
    if (ids.Count == 0) return new Dictionary<int, string>();

    var frameworks = await _db.Frameworks
      .Where(f => ids.Contains(f.Id))
      .Select(f => new { f.Id, f.Description, f.InstitutionSymbol })
      .ToListAsync();

    var symbols = frameworks
      .Select(f => int.TryParse(f.InstitutionSymbol, out var parsed) ? (int?)parsed : null)
      .Where(v => v.HasValue)
      .Select(v => v!.Value)
      .Distinct()
      .ToList();

    var institutions = await _db.Institutions
      .Include(i => i.Locality)
      .Where(i => symbols.Contains(i.InstitutionSymbol))
      .Select(i => new
      {
        i.InstitutionSymbol,
        i.Name,
        LocalityName = i.Locality != null ? i.Locality.Description : string.Empty
      })
      .ToListAsync();

    var labels = new Dictionary<int, string>();
    foreach (var framework in frameworks)
    {
      var institution = int.TryParse(framework.InstitutionSymbol, out var symbol)
        ? institutions.FirstOrDefault(i => i.InstitutionSymbol == symbol)
        : null;
      var name = !string.IsNullOrWhiteSpace(institution?.Name) ? institution!.Name : framework.Description;

      var parts = new List<string>();
      foreach (var part in new[] { institution?.LocalityName, framework.InstitutionSymbol, name }
        .Where(p => !string.IsNullOrWhiteSpace(p)))
      {
        var trimmed = part!.Trim();
        if (!parts.Any(existing => string.Equals(existing, trimmed, StringComparison.OrdinalIgnoreCase)))
          parts.Add(trimmed);
      }

      var label = string.Join(", ", parts);
      labels[framework.Id] = string.IsNullOrWhiteSpace(label) ? framework.Description : label;
    }

    return labels;
  }

  private static void ApplyFrameworkLabels(IEnumerable<ReportRow> rows, IReadOnlyDictionary<int, string> labels)
  {
    foreach (var row in rows)
    {
      if (row.Framework != null && labels.TryGetValue(row.Framework.Id, out var label))
        row.Framework.Description = label;
      if (row.ConclusionFramework != null && labels.TryGetValue(row.ConclusionFramework.Id, out var conclusionLabel))
        row.ConclusionFramework.Description = conclusionLabel;
    }
  }

  private static bool IsNumberOnly(string? value) => int.TryParse(value?.Trim(), out _);

  private static bool IsCityLocalityText(string? value)
  {
    var text = value?.Trim() ?? string.Empty;
    if (string.IsNullOrWhiteSpace(text) || IsNumberOnly(text)) return false;
    return !NonCityLocalityTokens.Any(token => text.Contains(token, StringComparison.OrdinalIgnoreCase));
  }

  private static bool IsCityLocality(Locality locality) =>
    locality != null && IsCityLocalityText(locality.Description);

  private static int NumberSortKey(string? value) =>
    int.TryParse(value?.Trim(), out var parsed) ? parsed : int.MaxValue;

  private bool CanEditReport(Report report)
  {
    // Placeholder reports (closed month without a saved report) are never editable.
    if (report.Id == 0) return false;
    if (report.IsArchived) return false;

    // Admin, PM and Coordinator may correct submitted/approved reports and override deadlines.
    if (IsDeadlineOverrideRole()) return true;

    if (report.ReportingMonth != null && !report.ReportingMonth.IsActive) return false;
    if (IsDeadlinePassed(report.ReportingMonth)) return false;

    // PendingApproval and Approved are locked for everyone else.
    if (report.StatusId is 3 or 4) return false;

    return _currentUser.UserRole == UserRoleEnum.ProjectCoordinator ||
           report.UserId == _currentUser.UserId;
  }

  private string EditBlockMessage(Report report) =>
    IsDeadlinePassed(report.ReportingMonth) && !IsDeadlineOverrideRole()
      ? DeadlinePassedMessage
      : "אין הרשאה לערוך דיווח זה";

  private bool IsDeadlineOverrideRole() =>
    _currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator;

  private bool WantsJson()
  {
    var accept = Request.Headers["Accept"].ToString();
    var requestedWith = Request.Headers["X-Requested-With"].ToString();
    return accept.IndexOf("application/json", StringComparison.OrdinalIgnoreCase) >= 0 ||
           string.Equals(requestedWith, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
  }

  private IActionResult RedirectBackToDashboard(string? returnUrl)
  {
    var normalizedReturnUrl = NormalizeLocalReturnUrl(returnUrl);
    if (!string.IsNullOrWhiteSpace(normalizedReturnUrl))
      return LocalRedirect(normalizedReturnUrl);

    return RedirectToAction("Index", "Dashboard");
  }

  private IActionResult RedirectToReport(int userId, int allocationId, int reportId, string? returnUrl)
  {
    return RedirectToAction(nameof(Index), new
    {
      userId,
      allocationId,
      reportId,
      returnUrl = NormalizeLocalReturnUrl(returnUrl)
    });
  }

  private async Task TrySendApprovalEmailAsync(Report report)
  {
    if (report.User?.Email == null) return;
    try
    {
      await _emailService.SendAsync(
        report.User.Email,
        $"{report.User.FirstName} {report.User.LastName}",
        "ReportApproved",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}",
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

  /// <summary>
  /// All lookup dropdown values scoped to a single allocation (used by the report
  /// form via fetch). In manual mode localities are the full city list.
  /// </summary>
  [HttpGet]
  public async Task<IActionResult> AllocationLookups(int allocationId, bool manual = false)
  {
    var allocation = await _db.Allocations.AsNoTracking()
      .FirstOrDefaultAsync(a => a.Id == allocationId && a.IsActive);
    if (allocation == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(allocation.UserId)) return Forbid();

    var allFrameworkRows = await _db.Set<AllocationFramework>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.Framework != null && x.Framework!.IsActive)
      .OrderBy(x => x.Framework!.Description)
      .Select(x => new
      {
        id = x.FrameworkId,
        text = x.Framework!.Description,
        institutionSymbol = x.Framework!.InstitutionSymbol
      })
      .ToListAsync();

    // Frameworks with a numeric institution symbol are "real" frameworks; the rest
    // serve the conclusion-framework dropdown.
    var numericFrameworkRows = allFrameworkRows.Where(x => IsNumberOnly(x.institutionSymbol)).ToList();
    var conclusionFrameworkRows = allFrameworkRows.Where(x => !IsNumberOnly(x.institutionSymbol)).ToList();
    var frameworkRows = numericFrameworkRows.Count > 0 ? numericFrameworkRows : allFrameworkRows;

    var frameworkLabels = await BuildFrameworkLabelsAsync(frameworkRows.Select(x => (int?)x.id));
    var conclusionFrameworkLabels = await BuildFrameworkLabelsAsync(conclusionFrameworkRows.Select(x => (int?)x.id));

    var localityRows = manual
      ? await _db.Localities.AsNoTracking()
        .Where(x => x.IsActive)
        .OrderBy(x => x.Description)
        .Select(x => new { id = x.Id, text = x.Description })
        .ToListAsync()
      : await _db.Set<AllocationLocality>().AsNoTracking()
        .Where(x => x.AllocationId == allocationId && x.Locality != null && x.Locality!.IsActive)
        .OrderBy(x => x.Locality!.Description)
        .Select(x => new { id = x.LocalityId, text = x.Locality!.Description })
        .ToListAsync();
    var localities = localityRows.Where(x => IsCityLocalityText(x.text)).ToList();

    var allClassRows = await _db.Set<AllocationClass>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.SchoolClass != null && x.SchoolClass!.IsActive)
      .Select(x => new { id = x.ClassId, text = x.SchoolClass!.Description })
      .ToListAsync();
    var classRows = allClassRows
      .Where(x => IsNumberOnly(x.text))
      .OrderBy(x => NumberSortKey(x.text))
      .ThenBy(x => x.text)
      .ToList();
    var conclusionClassRows = allClassRows
      .Where(x => !IsNumberOnly(x.text))
      .OrderBy(x => x.text)
      .ToList();

    var districts = await _db.Set<AllocationDistrict>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.District != null && x.District!.IsActive)
      .OrderBy(x => x.District!.Description)
      .Select(x => new { id = x.DistrictId, text = x.District!.Description })
      .ToListAsync();

    var frameworks = frameworkRows
      .Select(x => new { x.id, text = frameworkLabels.TryGetValue(x.id, out var label) ? label : x.text })
      .ToList();
    var conclusionFrameworks = conclusionFrameworkRows
      .Select(x => new { x.id, text = conclusionFrameworkLabels.TryGetValue(x.id, out var label) ? label : x.text })
      .ToList();

    var educationalPrograms = await _db.Set<AllocationEducationalProgram>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.EducationalProgram != null && x.EducationalProgram!.IsActive)
      .OrderBy(x => x.EducationalProgram!.Description)
      .Select(x => new { id = x.EducationalProgramId, text = x.EducationalProgram!.Description })
      .ToListAsync();

    var domains = await _db.Set<AllocationDomain>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.Domain != null && x.Domain!.IsActive)
      .OrderBy(x => x.Domain!.Description)
      .Select(x => new { id = x.DomainId, text = x.Domain!.Description })
      .ToListAsync();

    var subjects = await _db.Set<AllocationSubject>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.Subject != null && x.Subject!.IsActive)
      .OrderBy(x => x.Subject!.Description)
      .Select(x => new { id = x.SubjectId, text = x.Subject!.Description })
      .ToListAsync();

    var discussionCodes = await _db.Set<AllocationDiscussionCode>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.DiscussionCode != null && x.DiscussionCode!.IsActive)
      .OrderBy(x => x.DiscussionCode!.Description)
      .Select(x => new { id = x.DiscussionCodeId, text = x.DiscussionCode!.Description })
      .ToListAsync();

    var gradeLevels = await _db.Set<AllocationGradeLevel>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.GradeLevel != null && x.GradeLevel!.IsActive)
      .OrderBy(x => x.GradeLevel!.Description)
      .Select(x => new { id = x.GradeLevelId, text = x.GradeLevel!.Description })
      .ToListAsync();

    var locations = await _db.Set<AllocationLocalityDistrictNational>().AsNoTracking()
      .Where(x => x.AllocationId == allocationId && x.LocalityDistrictNational != null && x.LocalityDistrictNational!.IsActive)
      .OrderBy(x => x.LocalityDistrictNational!.Description)
      .Select(x => new { id = x.LocalityDistrictNationalId, text = x.LocalityDistrictNational!.Description })
      .ToListAsync();

    return Json(new
    {
      districts,
      localities,
      frameworks,
      conclusionFrameworks,
      educationalPrograms,
      domains,
      subjects,
      discussionCodes,
      classes = classRows,
      conclusionClasses = conclusionClassRows,
      gradeLevels,
      locations
    });
  }

  /// <summary>
  /// Lookup values scoped to the intersection of an allocation and a program via the
  /// ProjectProgram* mapping tables. When <paramref name="programId"/> is not one of the
  /// allocation's programs, it is treated as an educational program and resolved back to
  /// the mapped program ids.
  /// </summary>
  [HttpGet]
  public async Task<IActionResult> ScopedForProgram(int allocationId, int programId)
  {
    var allocation = await _db.Allocations.AsNoTracking()
      .FirstOrDefaultAsync(a => a.Id == allocationId && a.IsActive);
    if (allocation == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(allocation.UserId)) return Forbid();

    var programIds = new List<int>();
    if (await _db.Set<AllocationProgram>().AnyAsync(ap => ap.AllocationId == allocationId && ap.ProgramId == programId))
      programIds.Add(programId);
    else
      programIds = await ResolveProgramIdsForEducationalProgramAsync(allocationId, allocation.ProjectId, programId);
    if (programIds.Count == 0) return BadRequest();

    var projectId = allocation.ProjectId;
    var subjectIds = await ScopedAllocationIdsAsync("AllocationSubjects", "SubjectId", "ProjectProgramSubjects", allocationId, projectId, programIds);
    var domainIds = await ScopedAllocationIdsAsync("AllocationDomains", "DomainId", "ProjectProgramDomains", allocationId, projectId, programIds);
    var frameworkIds = await ScopedAllocationIdsAsync("AllocationFrameworks", "FrameworkId", "ProjectProgramFrameworks", allocationId, projectId, programIds);
    var discussionCodeIds = await ScopedAllocationIdsAsync("AllocationDiscussionCodes", "DiscussionCodeId", "ProjectProgramDiscussionCodes", allocationId, projectId, programIds);

    var scopedFrameworkRows = await _db.Frameworks
      .Where(x => x.IsActive && frameworkIds.Contains(x.Id))
      .OrderBy(x => x.Description)
      .Select(x => new { x.Id, x.Description, x.InstitutionSymbol })
      .ToListAsync();
    var numericFrameworkRows = scopedFrameworkRows.Where(x => IsNumberOnly(x.InstitutionSymbol)).ToList();
    var frameworkRows = numericFrameworkRows.Count > 0 ? numericFrameworkRows : scopedFrameworkRows;
    var frameworkLabels = await BuildFrameworkLabelsAsync(frameworkRows.Select(x => (int?)x.Id));

    var subjects = await _db.Subjects
      .Where(x => x.IsActive && subjectIds.Contains(x.Id))
      .OrderBy(x => x.Description)
      .Select(x => new { id = x.Id, description = x.Description })
      .ToListAsync();

    var domains = await _db.Domains
      .Where(x => x.IsActive && domainIds.Contains(x.Id))
      .OrderBy(x => x.Description)
      .Select(x => new { id = x.Id, description = x.Description })
      .ToListAsync();

    var frameworks = frameworkRows.Select(x => new
    {
      id = x.Id,
      description = frameworkLabels.TryGetValue(x.Id, out var label) ? label : x.Description
    });

    var discussionCodes = await _db.DiscussionCodes
      .Where(x => x.IsActive && discussionCodeIds.Contains(x.Id))
      .OrderBy(x => x.Description)
      .Select(x => new { id = x.Id, description = x.Description })
      .ToListAsync();

    return Json(new { subjects, domains, frameworks, discussionCodes });
  }

  private async Task TrySendRejectionEmailAsync(Report report, string rejectionReason)
  {
    if (report.User?.Email == null) return;
    try
    {
      await _emailService.SendAsync(
        report.User.Email,
        $"{report.User.FirstName} {report.User.LastName}",
        "ReportRejected",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}",
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
    foreach (var invalid in Path.GetInvalidFileNameChars())
      value = value.Replace(invalid, '_');
    return string.IsNullOrWhiteSpace(value) ? "employee" : value;
  }

  private static string NormalizeDigits(string value) =>
    new((value ?? string.Empty).Where(char.IsDigit).ToArray());

  private string? NormalizeLocalReturnUrl(string? returnUrl)
  {
    if (string.IsNullOrWhiteSpace(returnUrl) || !Url.IsLocalUrl(returnUrl))
      return null;

    var pathBase = HttpContext.Request.PathBase.Value;
    if (string.IsNullOrEmpty(pathBase) || returnUrl.StartsWith(pathBase, StringComparison.OrdinalIgnoreCase))
      return returnUrl;

    return returnUrl.StartsWith("/", StringComparison.Ordinal)
      ? pathBase + returnUrl
      : returnUrl;
  }

  private async Task<bool> CanViewEmployeeReportAsync(int employeeUserId)
  {
    if (_currentUser.UserRole == UserRoleEnum.Employee)
      return employeeUserId == _currentUser.UserId;

    if (_currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator)
      return true;

    return await IsEmployeeInInspectorScopeAsync(employeeUserId);
  }

  private async Task<bool> CanApproveReportAsync(int reportId)
  {
    var report = await _db.Reports.FindAsync(reportId);
    if (report == null || report.StatusId != 3) return false;

    if (_currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator)
      return true;

    return _currentUser.UserRole == UserRoleEnum.InspectorApproval &&
      await IsEmployeeInInspectorScopeAsync(report.UserId);
  }

  private async Task<bool> IsEmployeeInInspectorScopeAsync(int employeeUserId)
  {
    if (_currentUser.UserRole is not (UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval))
      return false;

    var assignments = await _db.InspectorAssignments
      .Where(a => a.InspectorUserId == _currentUser.UserId)
      .ToListAsync();
    if (!assignments.Any()) return false;

    foreach (var assignment in assignments)
    {
      var q = _db.Allocations.Where(a => a.UserId == employeeUserId && a.IsActive);

      if (assignment.DistrictId.HasValue)
      {
        var districtId = assignment.DistrictId.Value;
        q = q.Where(a => _db.Set<AllocationDistrict>()
          .Any(ad => ad.AllocationId == a.Id && ad.DistrictId == districtId));
      }

      if (assignment.SectorId.HasValue)
      {
        var sectorId = assignment.SectorId.Value;
        q = q.Where(a => _db.Set<AllocationSector>()
          .Any(s => s.AllocationId == a.Id && s.SectorId == sectorId));
      }

      if (assignment.ProgramId.HasValue)
      {
        var programId = assignment.ProgramId.Value;
        q = q.Where(a => _db.Set<AllocationProgram>()
          .Any(p => p.AllocationId == a.Id && p.ProgramId == programId));
      }

      if (await q.AnyAsync()) return true;
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
    const string defaultFields =
      "AllocationId,DistrictId,LocalityId,FrameworkId,EducationalProgramId,DomainId,Subject1Id,MeetingDate,MeetingDuration";

    var configured = await _db.SystemConstants
      .Where(c => c.Key == "RequiredReportFields")
      .Select(c => c.Value)
      .FirstOrDefaultAsync();

    return (string.IsNullOrWhiteSpace(configured) ? defaultFields : configured)
      .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
      .ToHashSet(StringComparer.OrdinalIgnoreCase);
  }

  /// <summary>
  /// Allocation lookup ids restricted to a (project, program) scope through the raw
  /// ProjectProgram* mapping tables (no EF entities exist for them).
  /// </summary>
  private async Task<List<int>> ScopedAllocationIdsAsync(string allocationTable, string idColumn, string scopeTable, int allocationId, int projectId, int programId)
  {
    var values = new List<int>();
    if (!_db.Database.IsRelational()) return values;
    var connection = _db.Database.GetDbConnection();
    var shouldClose = connection.State == ConnectionState.Closed;
    if (shouldClose)
      await connection.OpenAsync();

    try
    {
      using var command = connection.CreateCommand();
      command.CommandText = $"SELECT DISTINCT a.{idColumn} FROM dbo.{allocationTable} a INNER JOIN dbo.{scopeTable} s ON s.{idColumn} = a.{idColumn} WHERE a.AllocationId = @allocationId AND s.ProjectId = @projectId AND s.ProgramId = @programId";
      AddDbParameter(command, "@allocationId", allocationId);
      AddDbParameter(command, "@projectId", projectId);
      AddDbParameter(command, "@programId", programId);
      using var reader = await command.ExecuteReaderAsync();
      while (await reader.ReadAsync())
        values.Add(reader.GetInt32(0));
    }
    finally
    {
      if (shouldClose)
        await connection.CloseAsync();
    }

    return values;
  }

  private async Task<List<int>> ScopedAllocationIdsAsync(string allocationTable, string idColumn, string scopeTable, int allocationId, int projectId, IReadOnlyCollection<int> programIds)
  {
    var values = new HashSet<int>();
    foreach (var programId in programIds)
    {
      foreach (var id in await ScopedAllocationIdsAsync(allocationTable, idColumn, scopeTable, allocationId, projectId, programId))
        values.Add(id);
    }
    return values.ToList();
  }

  private async Task<List<int>> ResolveProgramIdsForEducationalProgramAsync(int allocationId, int projectId, int educationalProgramId)
  {
    var values = new List<int>();
    if (!_db.Database.IsRelational()) return values;
    var connection = _db.Database.GetDbConnection();
    var shouldClose = connection.State == ConnectionState.Closed;
    if (shouldClose)
      await connection.OpenAsync();

    try
    {
      using var command = connection.CreateCommand();
      command.CommandText = "SELECT DISTINCT ap.ProgramId FROM dbo.AllocationPrograms ap INNER JOIN dbo.ProjectProgramEducationalPrograms ppe ON ppe.ProjectId = @projectId AND ppe.ProgramId = ap.ProgramId AND ppe.EducationalProgramId = @educationalProgramId WHERE ap.AllocationId = @allocationId";
      AddDbParameter(command, "@allocationId", allocationId);
      AddDbParameter(command, "@projectId", projectId);
      AddDbParameter(command, "@educationalProgramId", educationalProgramId);
      using var reader = await command.ExecuteReaderAsync();
      while (await reader.ReadAsync())
        values.Add(reader.GetInt32(0));
    }
    finally
    {
      if (shouldClose)
        await connection.CloseAsync();
    }

    return values;
  }

  private static void AddDbParameter(DbCommand command, string name, object value)
  {
    var parameter = command.CreateParameter();
    parameter.ParameterName = name;
    parameter.Value = value;
    command.Parameters.Add(parameter);
  }
}

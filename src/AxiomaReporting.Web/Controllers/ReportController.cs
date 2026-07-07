using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
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
    "המועד האחרון לדיווח עבר. ניתן לערוך רק באמצעות מנהל מערכת.";

  internal const string ConcurrencyConflictMessage =
    "השורה עודכנה במקביל על ידי משתמש אחר. יש לרענן ולנסות שוב.";

  private readonly AppDbContext _db;
  private readonly IReportValidationService _validator;
  private readonly IReportStatusService _statusService;
  private readonly IReportExcelImportService _excelImportService;
  private readonly IPdfReportService _pdfReportService;
  private readonly ICurrentUserService _currentUser;
  private readonly IEmailService _emailService;
  private readonly ILogger<ReportController> _logger;

  public ReportController(
    AppDbContext db,
    IReportValidationService validator,
    IReportStatusService statusService,
    IReportExcelImportService excelImportService,
    IPdfReportService pdfReportService,
    ICurrentUserService currentUser,
    IEmailService emailService,
    ILogger<ReportController> logger)
  {
    _db = db;
    _validator = validator;
    _statusService = statusService;
    _excelImportService = excelImportService;
    _pdfReportService = pdfReportService;
    _currentUser = currentUser;
    _emailService = emailService;
    _logger = logger;
  }

  [HttpGet]
  public async Task<IActionResult> Index(int? userId = null, int? allocationId = null, int? reportId = null, int? editRowId = null, string? returnUrl = null)
  {
    var requestedReport = reportId.HasValue
      ? await _db.Reports
        .Include(r => r.ReportingMonth)
        .FirstOrDefaultAsync(r => r.Id == reportId.Value)
      : null;
    if (reportId.HasValue && requestedReport == null) return NotFound();

    // דיווח בארכיון נגיש רק לאדמין/מנהל פרויקט/רכז (יישור לגרסת השרת).
    if (requestedReport?.IsArchived == true &&
        _currentUser.UserRole is not (UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator))
      return NotFound();

    var targetUserId = requestedReport?.UserId ?? userId ?? _currentUser.UserId;
    if (!await CanViewEmployeeReportAsync(targetUserId)) return Forbid();

    var activeMonth = requestedReport?.ReportingMonth ?? await _db.ReportingMonths.FirstOrDefaultAsync(m => m.IsActive);
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

    var report = requestedReport ?? await _statusService.GetOrCreateDraftAsync(targetUserId, activeMonth.Id);
    if (report == null) return StatusCode(500);

    var rows = await _db.ReportRows
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

    var rowIds = rows.Select(r => r.Id).ToList();
    ViewBag.ReportAttachments = await _db.DocumentAttachments
      .Where(a => a.ReportId == report.Id ||
                  (a.ReportRowId.HasValue && rowIds.Contains(a.ReportRowId.Value)))
      .OrderByDescending(a => a.UploadedAt)
      .ToListAsync();

    var allocationWithJunctions = await _db.Allocations
      .AsSplitQuery()
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
      .FirstOrDefaultAsync(a => a.Id == selectedAllocation.Id);

    report.ReportingMonth ??= activeMonth;
    var deadlinePassed = IsDeadlinePassed(activeMonth);
    var isOverrideRole = _currentUser.UserRole == UserRoleEnum.SystemAdmin;

    // Class (כיתה) and the three מסקנה (conclusion) lists are global lookups — the same
    // for every report — so they come from their own tables, not the allocation junctions.
    ViewBag.AllClasses = await _db.Classes.Where(c => c.IsActive)
      .OrderBy(c => c.Description.Length).ThenBy(c => c.Description).ToListAsync();
    ViewBag.AllGradeLevels = await _db.GradeLevels.Where(g => g.IsActive)
      .OrderBy(g => g.Description.Length).ThenBy(g => g.Description).ToListAsync();
    ViewBag.ClassConclusions = await _db.ClassConclusions.Where(c => c.IsActive).OrderBy(c => c.Description).ToListAsync();
    ViewBag.FrameworkConclusions = await _db.FrameworkConclusions.Where(c => c.IsActive).OrderBy(c => c.Description).ToListAsync();
    ViewBag.LocationConclusions = await _db.LocalityDistrictNationals.Where(c => c.IsActive).OrderBy(c => c.Description).ToListAsync();

    // Composite framework labels — יישוב, סמל ושם מסגרת (משוב בטא B35/B39).
    var frameworkIds = allocationWithJunctions?.AllocationFrameworks.Select(x => x.FrameworkId).ToList() ?? new List<int>();
    foreach (var r in rows)
    {
      if (!frameworkIds.Contains(r.FrameworkId)) frameworkIds.Add(r.FrameworkId);
    }
    ViewBag.FrameworkLabels = await FrameworkLabelService.BuildLabelsAsync(_db, frameworkIds);

    ViewBag.Employee = employee;
    ViewBag.ActiveMonth = activeMonth;
    ViewBag.Report = report;
    ViewBag.Allocation = allocationWithJunctions;
    ViewBag.Allocations = allocations;
    ViewBag.AllocationId = selectedAllocation.Id;
    ViewBag.EditRowId = editRowId;
    ViewBag.ReturnUrl = NormalizeLocalReturnUrl(returnUrl);
    ViewBag.CanEdit = CanEditReport(report);
    ViewBag.RequiredReportFields = await GetRequiredReportFieldsAsync();
    ViewBag.DeadlinePassed = deadlinePassed;
    ViewBag.DeadlineOverrideActive = deadlinePassed && isOverrideRole;
    ViewBag.DeadlineBlockMessage = deadlinePassed && !isOverrideRole ? DeadlinePassedMessage : null;

    // דיווחים קודמים — כל הדוחות של העובד מחודשים אחרים, לצפייה (עריכה נחסמת
    // ממילא לפי מועד הסגירה והסטטוס של אותו חודש).
    ViewBag.PastReports = await _db.Reports
      .Where(r => r.UserId == targetUserId && r.Id != report.Id && !r.IsArchived)
      .OrderByDescending(r => r.ReportingMonth!.Year)
      .ThenByDescending(r => r.ReportingMonth!.Month)
      .Select(r => new PastReportListItem(
        r.Id,
        r.ReportingMonth!.Description,
        r.Status!.Name,
        r.StatusId,
        r.SubmittedAt,
        _db.ReportRows.Count(row => row.ReportId == r.Id)))
      .ToListAsync();

    return View("Index", rows);
  }

  private static bool IsDeadlinePassed(ReportingMonth? month)
  {
    if (month == null) return false;
    return DateTime.Today > month.LastReportingDate.Date;
  }

  [HttpGet]
  public IActionResult DownloadExcelTemplate()
  {
    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("דיווח");
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
    ws.Cell(2, 16).Value = "דוגמה - יש להחליף בערכי הדיווח";
    ws.Columns().AdjustToContents();

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    return File(
      stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      "report_upload_template.xlsx");
  }

  // GET /Report/ExportMyReport — ייצוא הדיווח לאקסל: כותרות בעברית ובסדר העמודות
  // של המסך (משוב בטא B24/B25), כולל תווית מסגרת משולבת. זמין גם לעובד עצמו (Sheet7 #4).
  [HttpGet]
  public async Task<IActionResult> ExportMyReport(int reportId, int allocationId)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(report.UserId)) return Forbid();

    var rows = await _db.ReportRows
      .AsSplitQuery()
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
      .Where(r => r.ReportId == reportId && r.AllocationId == allocationId)
      .OrderBy(r => r.MeetingDate).ThenBy(r => r.SequenceNumber)
      .ToListAsync();

    var frameworkLabels = await FrameworkLabelService.BuildLabelsAsync(
      _db, rows.Select(r => r.FrameworkId).Distinct().ToList());

    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("דיווח פעילות חודשית");
    ws.RightToLeft = true;

    // סדר העמודות זהה למסך הדיווח.
    var headers = new[]
    {
      "מס\"ד", "תאריך", "משך תפוקה", "מחוז", "ישוב", "מסגרת חינוכית", "תוכנית חינוכית",
      "תחום", "נושא 1", "נושא 2", "קיום דיון", "מסקנה-כיתה", "מסקנה-מסגרת",
      "מסקנה-מיקום", "שכבה", "כיתה", "הערות"
    };
    for (var i = 0; i < headers.Length; i++)
    {
      ws.Cell(1, i + 1).Value = headers[i];
      ws.Cell(1, i + 1).Style.Font.Bold = true;
    }

    var rowIdx = 2;
    foreach (var r in rows)
    {
      ws.Cell(rowIdx, 1).Value = r.SequenceNumber;
      ws.Cell(rowIdx, 2).Value = r.MeetingDate;
      ws.Cell(rowIdx, 2).Style.DateFormat.Format = "dd/MM/yyyy";
      ws.Cell(rowIdx, 3).Value = r.MeetingDuration;
      ws.Cell(rowIdx, 4).Value = r.District?.Description;
      ws.Cell(rowIdx, 5).Value = r.Locality?.Description;
      ws.Cell(rowIdx, 6).Value = frameworkLabels.TryGetValue(r.FrameworkId, out var fwLabel)
        ? fwLabel : r.Framework?.Description;
      ws.Cell(rowIdx, 7).Value = r.EducationalProgram?.Description;
      ws.Cell(rowIdx, 8).Value = r.Domain?.Description;
      ws.Cell(rowIdx, 9).Value = r.Subject1?.Description;
      ws.Cell(rowIdx, 10).Value = r.Subject2?.Description;
      ws.Cell(rowIdx, 11).Value = r.DiscussionCode?.Description;
      ws.Cell(rowIdx, 12).Value = r.ConclusionClass?.Description;
      ws.Cell(rowIdx, 13).Value = r.ConclusionFramework?.Description;
      ws.Cell(rowIdx, 14).Value = r.ConclusionLocation?.Description;
      ws.Cell(rowIdx, 15).Value = r.GradeLevel?.Description;
      ws.Cell(rowIdx, 16).Value = r.Class?.Description;
      ws.Cell(rowIdx, 17).Value = r.Notes;
      rowIdx++;
    }

    ws.Columns().AdjustToContents();

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    var month = report.ReportingMonth != null ? $"{report.ReportingMonth.Month:00}-{report.ReportingMonth.Year}" : "";
    return File(
      stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"activity_report_{month}.xlsx");
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
    // סוג דיווח יורש אוטומטית מההקצאה כשלא נבחר בשורה (משוב בטא B32).
    row.ReportTypeId ??= allocation.ReportTypeId;

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
  public async Task<IActionResult> Approve(int reportId, string? returnUrl = null)
  {
    if (!await CanApproveReportAsync(reportId)) return Forbid();

    await _statusService.ApproveReportAsync(reportId, _currentUser.UserId);
    TempData["Success"] = "הדיווח אושר";
    return RedirectBackToDashboard(returnUrl);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Reopen(int reportId, string? returnUrl = null)
  {
    // Status override is reserved for SystemAdmin / ProjectManager (role spec:
    // "PM overrides report status") — not the wider approve policy.
    if (_currentUser.UserRole is not (UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager))
      return Forbid();

    if (await _statusService.ReopenReportAsync(reportId, _currentUser.UserId))
      TempData["Success"] = "הדיווח הוחזר לעריכה — ניתן להוסיף שורות ולהגיש מחדש";
    else
      TempData["Error"] = "לא ניתן להחזיר לעריכה — רק דיווח מאושר ניתן לפתיחה מחדש";

    return RedirectBackToDashboard(returnUrl);
  }

  // ── דיווח ידני (יישור לגרסת השרת): מנהל מאתר עובד, בוחר הקצאה וחודש
  //    (כולל חודשי עבר — "אין הגבלה" לפי הבהרת הלקוח #6) ופותח את הדיווח. ──

  [HttpGet]
  [Route("Report/Manual")]
  public async Task<IActionResult> Manual()
  {
    if (_currentUser.UserRole is not (UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator))
      return Forbid();

    ViewBag.ReportingMonths = await _db.ReportingMonths
      .OrderByDescending(m => m.Year).ThenByDescending(m => m.Month)
      .ToListAsync();
    return View("Manual");
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
      query = query.Where(u => u.IdNumber.Replace("-", "").Replace(" ", "").Contains(raw));
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
      .OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
      .Take(30)
      .Select(u => new { id = u.Id, idNumber = u.IdNumber, employeeCode = u.EmployeeCode, firstName = u.FirstName, lastName = u.LastName })
      .ToListAsync();

    var employeeIds = employees.Select(e => e.id).ToList();
    var allocations = await _db.Allocations
      .Where(a => a.IsActive && employeeIds.Contains(a.UserId))
      .OrderBy(a => a.Project!.Description)
      .Select(a => new { id = a.Id, userId = a.UserId, projectName = a.Project != null ? a.Project.Description : "" })
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
      TempData["Error"] = "ההקצאה שנבחרה אינה פעילה או לא קיימת";
      return RedirectToAction(nameof(Manual));
    }
    if (allocation.UserId != userId)
    {
      TempData["Error"] = "יש לבחור הקצאה ששייכת לעובד שנבחר";
      return RedirectToAction(nameof(Manual));
    }

    var report = await _statusService.GetOrCreateDraftAsync(userId, reportingMonthId);
    if (report == null) return StatusCode(500);

    return RedirectToAction(nameof(Index), new { userId, allocationId, reportId = report.Id });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.CanApproveReports)]
  public async Task<IActionResult> Reject(int reportId, string rejectionReason, string? returnUrl = null)
  {
    if (!await CanApproveReportAsync(reportId)) return Forbid();
    if (string.IsNullOrWhiteSpace(rejectionReason))
    {
      TempData["Error"] = "יש לציין סיבת דחייה";
      return RedirectBackToDashboard(returnUrl);
    }

    await _statusService.RejectReportAsync(reportId, _currentUser.UserId, rejectionReason);
    TempData["Success"] = "הדיווח הוחזר לתיקון";
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
        Path.Combine(errorsDir, $"{errorId}.pdf"),
        _pdfReportService.CreateErrorReport(result.Errors));
      TempData["Errors"] = string.Join("|", result.Errors);
      TempData["ExcelErrorPdf"] = Url?.Content($"~/uploads/excel-errors/{errorId}.pdf")
        ?? $"/uploads/excel-errors/{errorId}.pdf";

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
          ["ErrorsCount"] = errors.Count.ToString(System.Globalization.CultureInfo.InvariantCulture),
          ["ErrorList"] = errorList,
          ["Month"] = month?.Month.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "",
          ["Year"] = month?.Year.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? ""
        });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
        "Failed to enqueue BatchImportErrors email for user {UserId}", report.UserId);
    }
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

  private bool CanEditReport(Report report)
  {
    // Only System Admin may correct reports that were already submitted/approved or override deadlines.
    if (_currentUser.UserRole == UserRoleEnum.SystemAdmin)
      return true;

    if (IsDeadlinePassed(report.ReportingMonth)) return false;

    // PendingApproval and Approved are locked for everyone except System Admin.
    if (report.StatusId is 3 or 4) return false;

    // Draft, InEntry and ReturnedForCorrection may still be edited by the owner/coordinator.
    return _currentUser.UserRole == UserRoleEnum.ProjectCoordinator ||
           report.UserId == _currentUser.UserId;
  }

  private string EditBlockMessage(Report report) =>
    IsDeadlinePassed(report.ReportingMonth) &&
    _currentUser.UserRole != UserRoleEnum.SystemAdmin
      ? DeadlinePassedMessage
      : "אין הרשאה לערוך דיווח זה";

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
}

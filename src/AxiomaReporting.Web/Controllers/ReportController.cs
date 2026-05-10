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
    new(StringComparer.OrdinalIgnoreCase) { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx", ".xls", ".xlsx" };

  private const long MaxAttachmentBytes = 10 * 1024 * 1024;

  internal const string DeadlinePassedMessage =
    "המועד האחרון לדיווח עבר — ניתן לעדכן רק דרך מנהל פרויקט או מנהל מערכת";

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
  public async Task<IActionResult> Index(int? userId = null, int? allocationId = null)
  {
    var targetUserId = userId ?? _currentUser.UserId;
    if (!await CanViewEmployeeReportAsync(targetUserId)) return Forbid();

    var activeMonth = await _db.ReportingMonths.FirstOrDefaultAsync(m => m.IsActive);
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

    var report = await _statusService.GetOrCreateDraftAsync(targetUserId, activeMonth.Id);
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
    var attachments = await _db.DocumentAttachments
      .Where(a => a.ReportRowId.HasValue && rowIds.Contains(a.ReportRowId.Value))
      .ToListAsync();
    ViewBag.Attachments = attachments
      .GroupBy(a => a.ReportRowId!.Value)
      .ToDictionary(g => g.Key, g => g.ToList());

    var allocationWithJunctions = await _db.Allocations
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
    var isOverrideRole = _currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager;

    ViewBag.Employee = employee;
    ViewBag.ActiveMonth = activeMonth;
    ViewBag.Report = report;
    ViewBag.Allocation = allocationWithJunctions;
    ViewBag.Allocations = allocations;
    ViewBag.AllocationId = selectedAllocation.Id;
    ViewBag.CanEdit = CanEditReport(report);
    ViewBag.RequiredReportFields = await GetRequiredReportFieldsAsync();
    ViewBag.DeadlinePassed = deadlinePassed;
    ViewBag.DeadlineOverrideActive = deadlinePassed && isOverrideRole;
    ViewBag.DeadlineBlockMessage = deadlinePassed && !isOverrideRole ? DeadlinePassedMessage : null;

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
    return Json(new { success = true, warnings = validation.Warnings });
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
  public async Task<IActionResult> Submit(int reportId, int? allocationId = null, string? rowVersion = null)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(report.UserId) || !CanEditReport(report))
    {
      TempData["Errors"] = EditBlockMessage(report);
      return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
    }

    var validation = await _validator.ValidateSubmitAsync(report, report.User!, report.ReportingMonth!);
    if (!validation.IsValid)
    {
      TempData["Errors"] = string.Join("|", validation.Errors);
      return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
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
    return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.CanApproveReports)]
  public async Task<IActionResult> Approve(int reportId)
  {
    if (!await CanApproveReportAsync(reportId)) return Forbid();

    await _statusService.ApproveReportAsync(reportId, _currentUser.UserId);
    TempData["Success"] = "הדיווח אושר";
    return RedirectToAction("Index", "Dashboard");
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.CanApproveReports)]
  public async Task<IActionResult> Reject(int reportId, string rejectionReason)
  {
    if (!await CanApproveReportAsync(reportId)) return Forbid();
    if (string.IsNullOrWhiteSpace(rejectionReason))
    {
      TempData["Error"] = "יש לציין סיבת דחייה";
      return RedirectToAction("Index", "Dashboard");
    }

    await _statusService.RejectReportAsync(reportId, _currentUser.UserId, rejectionReason);
    TempData["Success"] = "הדיווח הוחזר לתיקון";
    return RedirectToAction("Index", "Dashboard");
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> UploadExcel(int reportId, int allocationId, IFormFile file)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();
    if (!await CanViewEmployeeReportAsync(report.UserId) || !CanEditReport(report))
    {
      TempData["Errors"] = EditBlockMessage(report);
      return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
    }

    if (file == null || file.Length == 0)
    {
      TempData["Errors"] = "לא נבחר קובץ אקסל";
      return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
    }

    var extension = Path.GetExtension(file.FileName);
    if (!extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
    {
      TempData["Errors"] = "ניתן להעלות קובץ xlsx בלבד";
      return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
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
      TempData["ExcelErrorPdf"] = $"/uploads/excel-errors/{errorId}.pdf";

      await SendImportFailureEmailAsync(report, result.Errors);
      return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
    }

    TempData["Success"] = $"יובאו {result.ImportedRows} שורות מאקסל";
    return RedirectToAction(nameof(Index), new { userId = report.UserId, allocationId });
  }

  private async Task SendImportFailureEmailAsync(Report report, IReadOnlyCollection<string> errors)
  {
    if (report.User == null) return;
    if (string.IsNullOrWhiteSpace(report.User.Email))
    {
      _logger.LogInformation(
        "Skipping BatchImportErrors email for user {UserId} — no email address on file",
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
  public async Task<IActionResult> UploadAttachment(int reportRowId, IFormFile file)
  {
    var row = await _db.ReportRows
      .Include(r => r.Report).ThenInclude(rep => rep!.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportRowId);
    if (row?.Report == null) return Json(new { success = false, error = "אין הרשאה לצרף מסמך לשורה זו" });
    if (!await CanViewEmployeeReportAsync(row.Report.UserId) || !CanEditReport(row.Report))
      return Json(new { success = false, error = EditBlockMessage(row.Report) });

    if (file == null || file.Length == 0)
      return Json(new { success = false, error = "לא נבחר קובץ" });
    if (file.Length > MaxAttachmentBytes)
      return Json(new { success = false, error = "גודל הקובץ חורג מהמותר" });

    var extension = Path.GetExtension(file.FileName);
    if (!AllowedAttachmentExtensions.Contains(extension))
      return Json(new { success = false, error = "סוג הקובץ אינו נתמך" });

    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "attachments");
    Directory.CreateDirectory(uploadsDir);
    var fileName = $"{Guid.NewGuid()}{extension}";
    var filePath = Path.Combine(uploadsDir, fileName);

    await using (var stream = new FileStream(filePath, FileMode.CreateNew))
      await file.CopyToAsync(stream);

    var attachment = new DocumentAttachment
    {
      ReportRowId = reportRowId,
      FileName = Path.GetFileName(file.FileName),
      FilePath = $"/uploads/attachments/{fileName}",
      FileSize = file.Length,
      MimeType = file.ContentType,
      UploadedAt = DateTime.UtcNow,
      UploadedBy = _currentUser.UserId
    };
    _db.DocumentAttachments.Add(attachment);
    await _db.SaveChangesAsync();

    return Json(new { success = true, id = attachment.Id, fileName = attachment.FileName });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteAttachment(int attachmentId)
  {
    var attachment = await _db.DocumentAttachments
      .Include(a => a.ReportRow).ThenInclude(r => r!.Report).ThenInclude(rep => rep!.ReportingMonth)
      .FirstOrDefaultAsync(a => a.Id == attachmentId);
    if (attachment?.ReportRow?.Report == null) return Json(new { success = false });
    if (!await CanViewEmployeeReportAsync(attachment.ReportRow.Report.UserId) || !CanEditReport(attachment.ReportRow.Report))
      return Json(new { success = false, error = EditBlockMessage(attachment.ReportRow.Report) });

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
    // Admin and PM can override any status or deadline
    if (_currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager)
      return true;

    // After the reporting-month deadline, only Admin/PM can still edit.
    // Employees and Coordinators are blocked regardless of status.
    if (IsDeadlinePassed(report.ReportingMonth)) return false;

    // Approved reports cannot be edited by coordinators or employees
    if (report.StatusId == 4) return false;

    // PendingApproval — coordinators cannot edit (report was submitted, awaiting review)
    if (report.StatusId == 3)
      return _currentUser.UserRole == UserRoleEnum.ProjectCoordinator;

    // Draft (1), InEntry (2), ReturnedForCorrection (5) — coordinator or own employee
    return _currentUser.UserRole == UserRoleEnum.ProjectCoordinator ||
           report.UserId == _currentUser.UserId;
  }

  private string EditBlockMessage(Report report) =>
    IsDeadlinePassed(report.ReportingMonth) &&
    _currentUser.UserRole is not (UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager)
      ? DeadlinePassedMessage
      : "אין הרשאה לערוך דיווח זה";

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

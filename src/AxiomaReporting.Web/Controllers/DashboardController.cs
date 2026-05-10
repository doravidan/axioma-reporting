using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Policy = PolicyNames.CanViewDashboard)]
public class DashboardController : Controller
{
  private readonly IDashboardFilterService _filterService;
  private readonly ICurrentUserService _currentUser;
  private readonly IReportStatusService _reportStatusService;

  public DashboardController(
    IDashboardFilterService filterService,
    ICurrentUserService currentUser,
    IReportStatusService reportStatusService)
  {
    _filterService = filterService;
    _currentUser = currentUser;
    _reportStatusService = reportStatusService;
  }

  [HttpGet]
  public async Task<IActionResult> Index(DashboardFilter? filter = null)
  {
    filter ??= new DashboardFilter();
    await PopulateFilterDataAsync(filter);

    var showData = Request.Query.ContainsKey("show");
    ViewBag.ShowData = showData;

    var rows = new List<DashboardReportRow>();
    var total = 0;
    if (showData)
      (rows, total) = await _filterService.GetReportsAsync(filter, _currentUser.UserId, _currentUser.UserRole);

    ViewBag.Rows = rows;
    ViewBag.TotalCount = total;
    return View();
  }

  [HttpGet]
  public async Task<IActionResult> FilterOptions(string? selected = null)
  {
    DashboardFilter filter;
    try
    {
      filter = string.IsNullOrWhiteSpace(selected)
        ? new DashboardFilter()
        : JsonSerializer.Deserialize<DashboardFilter>(selected, new JsonSerializerOptions
          {
            PropertyNameCaseInsensitive = true
          }) ?? new DashboardFilter();
    }
    catch (JsonException)
    {
      filter = new DashboardFilter();
    }

    var options = await _filterService.GetCompatibleOptionsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    return Json(options);
  }

  [HttpGet]
  public async Task<IActionResult> ExportExcel(DashboardFilter filter)
  {
    if (_currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval)
      filter.StatusId = 4;

    filter.Page = 1;
    filter.PageSize = 10000;

    var (rows, _) = await _filterService.GetReportsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    using var wb = new XLWorkbook();
    var ws = wb.Worksheets.Add("דיווחים");
    ws.RightToLeft = true;

    var headers = new[] { "קוד עובד", "ת.ז", "שם עובד", "חודש", "סטטוס", "מס' שורות", "סך משך תפוקה", "תאריך הגשה" };
    for (var i = 0; i < headers.Length; i++)
      ws.Cell(1, i + 1).Value = headers[i];

    var row = 2;
    foreach (var r in rows)
    {
      ws.Cell(row, 1).Value = r.EmployeeCode;
      ws.Cell(row, 2).Value = r.IdNumber;
      ws.Cell(row, 3).Value = r.FullName;
      ws.Cell(row, 4).Value = r.MonthDescription;
      ws.Cell(row, 5).Value = r.StatusName;
      ws.Cell(row, 6).Value = r.RowCount;
      ws.Cell(row, 7).Value = (double)r.TotalDuration;
      ws.Cell(row, 8).Value = r.SubmittedAt?.ToString("dd/MM/yyyy") ?? string.Empty;
      row++;
    }

    ws.Columns().AdjustToContents();

    using var ms = new MemoryStream();
    wb.SaveAs(ms);
    return File(
      ms.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"reports_{DateTime.Now:yyyyMMdd}.xlsx");
  }

  [HttpGet]
  public async Task<IActionResult> SummaryExportExcel(DashboardFilter filter)
  {
    filter.StatusId ??= 3;
    filter.Page = 1;
    filter.PageSize = 10000;

    var (rows, _) = await _filterService.GetReportsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    using var wb = new XLWorkbook();
    var ws = wb.Worksheets.Add("סיכום");
    ws.RightToLeft = true;

    var headers = new[]
    {
      "קוד עובד", "ת.ז", "שם עובד", "חודש", "סטטוס", "מס' שורות", "סך משך תפוקה",
      "יתרת שורות", "מסמכים", "תאריך הגשה"
    };
    for (var i = 0; i < headers.Length; i++)
      ws.Cell(1, i + 1).Value = headers[i];

    var row = 2;
    foreach (var r in rows)
    {
      ws.Cell(row, 1).Value = r.EmployeeCode;
      ws.Cell(row, 2).Value = r.IdNumber;
      ws.Cell(row, 3).Value = r.FullName;
      ws.Cell(row, 4).Value = r.MonthDescription;
      ws.Cell(row, 5).Value = r.StatusName;
      ws.Cell(row, 6).Value = r.RowCount;
      ws.Cell(row, 7).Value = (double)r.TotalDuration;
      ws.Cell(row, 8).Value = r.MonthlyRowAllocation.HasValue ? r.RemainingRows : string.Empty;
      ws.Cell(row, 9).Value = r.HasAttachments ? "כן" : "לא";
      ws.Cell(row, 10).Value = r.SubmittedAt?.ToString("dd/MM/yyyy") ?? string.Empty;
      row++;
    }

    ws.Columns().AdjustToContents();

    using var ms = new MemoryStream();
    wb.SaveAs(ms);
    return File(
      ms.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"summary_{DateTime.Now:yyyyMMdd}.xlsx");
  }

  [HttpGet]
  public async Task<IActionResult> Summary(DashboardFilter? filter = null)
  {
    filter ??= new DashboardFilter();
    // Default to pending approval so summary shows actionable items
    filter.StatusId ??= 3;
    await PopulateFilterDataAsync(filter);

    var (rows, total) = await _filterService.GetReportsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    ViewBag.Rows = rows;
    ViewBag.TotalCount = total;
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.CanApproveReports)]
  public async Task<IActionResult> BulkApprove(List<int> reportIds)
  {
    var approved = 0;
    foreach (var id in reportIds.Distinct())
    {
      if (!await _filterService.CanAccessReportAsync(id, _currentUser.UserId, _currentUser.UserRole))
        continue;

      if (await _reportStatusService.ApproveReportAsync(id, _currentUser.UserId))
        approved++;
    }

    TempData["Success"] = $"{approved} דיווחים אושרו בהצלחה";
    return RedirectToAction(nameof(Summary));
  }

  private async Task PopulateFilterDataAsync(DashboardFilter filter)
  {
    ViewBag.Filter = filter;
    ViewBag.Districts = await _filterService.GetFilteredDistrictsAsync(
      _currentUser.UserId, _currentUser.UserRole);
    ViewBag.Sectors = await _filterService.GetFilteredSectorsAsync(
      _currentUser.UserId, _currentUser.UserRole, filter.DistrictId);
    ViewBag.Programs = await _filterService.GetFilteredProgramsAsync(
      _currentUser.UserId, _currentUser.UserRole, filter.DistrictId);
    ViewBag.IsInspector = _currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval;
    ViewBag.CanApprove = _currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager
      or UserRoleEnum.ProjectCoordinator or UserRoleEnum.InspectorApproval;

    using var scope = HttpContext.RequestServices.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AxiomaReporting.Infrastructure.Data.AppDbContext>();
    ViewBag.ReportingMonths = await db.ReportingMonths
      .OrderByDescending(m => m.Year).ThenByDescending(m => m.Month)
      .ToListAsync();
  }
}

using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

/// <summary>
/// Employee landing page: a "submenu" between the navbar and the actual reporting
/// flow. Shows the active reporting month banner and two big tile-style buttons:
/// "עדכון פעילות חודשית" (→ /Report/Index) and, conditionally, "העלאת אקסל חודשי"
/// (→ /Report/UploadExcel) when the user has at least one allocation with
/// AllowExcelUpload = true.
/// </summary>
[Authorize(Roles = "6")]
public class MyAllocationsController : Controller
{
  private readonly AppDbContext _db;
  private readonly ICurrentUserService _currentUser;

  public MyAllocationsController(AppDbContext db, ICurrentUserService currentUser)
  {
    _db = db;
    _currentUser = currentUser;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var activeMonth = await _db.ReportingMonths
      .AsNoTracking()
      .FirstOrDefaultAsync(m => m.IsActive);

    var userId = _currentUser.UserId;

    var allocations = await MyAllocationsQuery(userId)
      .OrderBy(a => a.Project!.Description)
      .ThenBy(a => a.Id)
      .ToListAsync();

    var uploadAllocations = allocations.Where(a => a.AllowExcelUpload).ToList();
    var allocationIds = allocations.Select(a => a.Id).ToList();
    var currentReportId = activeMonth == null
      ? 0
      : await _db.Reports
        .AsNoTracking()
        .Where(r => r.UserId == userId && r.ReportingMonthId == activeMonth.Id && !r.IsArchived)
        .Select(r => r.Id)
        .FirstOrDefaultAsync();
    var historyReports = await ReportController.BuildPastReportListAsync(
      _db,
      userId,
      currentReportId,
      selectedAllocationId: allocationIds.FirstOrDefault(),
      activeAllocationIds: allocationIds);

    var vm = new Models.MyAllocationsViewModel
    {
      ActiveMonth = activeMonth,
      AllocationCount = allocations.Count,
      // The Excel-upload tile is shown when ANY active allocation grants the right.
      AllowExcelUpload = uploadAllocations.Count > 0,
      // Never choose one allocation implicitly when more than one grants upload.
      // Report/Index will display the explicit allocation selector instead.
      ExcelUploadAllocationId = uploadAllocations.Count == 1 ? uploadAllocations[0].Id : null,
      Allocations = allocations,
      HistoryReports = historyReports
    };

    return View(vm);
  }

  [HttpGet]
  public async Task<IActionResult> Details(int id)
  {
    var allocation = await MyAllocationsQuery(_currentUser.UserId)
      .FirstOrDefaultAsync(a => a.Id == id);

    return allocation == null ? NotFound() : View(allocation);
  }

  [HttpGet]
  public async Task<IActionResult> ExportExcel()
  {
    var allocations = await MyAllocationsQuery(_currentUser.UserId)
      .OrderBy(a => a.Project!.Description)
      .ThenBy(a => a.Id)
      .ToListAsync();

    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("ההקצאות שלי");
    ws.RightToLeft = true;

    var headers = new[]
    {
      "פרויקט", "תוכנית", "מחוז", "מגזר", "היקף פעילות חודשי", "היקף פעילות יומי",
      "היקף פעילות שנתי", "משך תפוקה", "אפשר העלאת אקסל", "הערות"
    };

    for (var i = 0; i < headers.Length; i++)
      ws.Cell(1, i + 1).Value = headers[i];

    var row = 2;
    foreach (var allocation in allocations)
    {
      ws.Cell(row, 1).Value = allocation.Project?.Description;
      ws.Cell(row, 2).Value = JoinValues(allocation.AllocationPrograms.Select(x => x.Program?.Description));
      ws.Cell(row, 3).Value = JoinValues(allocation.AllocationDistricts.Select(x => x.District?.Description));
      ws.Cell(row, 4).Value = JoinValues(allocation.AllocationSectors.Select(x => x.Sector?.Description));
      ws.Cell(row, 5).Value = allocation.MonthlyEmploymentScope;
      ws.Cell(row, 6).Value = allocation.DailyEmploymentScope?.ToString("0.##") ?? "ללא הגבלה";
      ws.Cell(row, 7).Value = allocation.AnnualEmploymentScope;
      ws.Cell(row, 8).Value = allocation.OutputDuration;
      ws.Cell(row, 9).Value = allocation.AllowExcelUpload ? "כן" : "לא";
      ws.Cell(row, 10).Value = allocation.Notes;
      row++;
    }

    ws.Row(1).Style.Font.Bold = true;
    ws.Columns().AdjustToContents();

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);

    return File(
      stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"my_allocations_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
  }

  private IQueryable<Allocation> MyAllocationsQuery(int userId)
  {
    return _db.Allocations
      .AsNoTracking()
      // AsSplitQuery is essential here: without it the 13 collection Includes below
      // form one huge cartesian join (subjects × frameworks × localities × …) that
      // hangs the employee's monthly-activity screen for allocations with large
      // junction sets.
      .AsSplitQuery()
      .Include(a => a.User)
      .Include(a => a.Project)
      .Include(a => a.AllocationDistricts).ThenInclude(x => x.District)
      .Include(a => a.AllocationPrograms).ThenInclude(x => x.Program)
      .Include(a => a.AllocationSectors).ThenInclude(x => x.Sector)
      .Include(a => a.AllocationLocalities).ThenInclude(x => x.Locality)
      .Include(a => a.AllocationFrameworks).ThenInclude(x => x.Framework)
      .Include(a => a.AllocationSubjects).ThenInclude(x => x.Subject)
      .Include(a => a.AllocationDomains).ThenInclude(x => x.Domain)
      .Include(a => a.AllocationEducationalPrograms).ThenInclude(x => x.EducationalProgram)
      .Include(a => a.AllocationClasses).ThenInclude(x => x.SchoolClass)
      .Include(a => a.AllocationGradeLevels).ThenInclude(x => x.GradeLevel)
      .Include(a => a.AllocationDiscussionCodes).ThenInclude(x => x.DiscussionCode)
      .Include(a => a.AllocationLocalityDistrictNationals).ThenInclude(x => x.LocalityDistrictNational)
      .Where(a => a.UserId == userId && a.IsActive);
  }

  private static string JoinValues(IEnumerable<string?> values)
  {
    var items = values
      .Where(v => !string.IsNullOrWhiteSpace(v))
      .Distinct()
      .ToList();

    return items.Count == 0 ? "-" : string.Join(", ", items);
  }
}

using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

[Authorize]
[Route("allocations")]
public class AllocationsController : Controller
{
  private readonly AppDbContext _db;
  private readonly ICurrentUserService _currentUser;

  private static readonly decimal[] OUTPUT_DURATION_OPTIONS = { 0.5m, 1m, 1.5m, 2m, 2.5m, 3m };

  public AllocationsController(AppDbContext db, ICurrentUserService currentUser)
  {
    _db = db;
    _currentUser = currentUser;
  }

  [HttpGet("")]
  public async Task<IActionResult> Index(AllocationListFilterModel filter)
  {
    filter.Normalize();

    var scopedQuery = await BuildScopedAllocationQueryAsync(_db, _currentUser.UserId, _currentUser.UserRole);
    var total = await ApplyAllocationFilters(scopedQuery, filter).CountAsync();
    var page = Math.Max(filter.Page, 1);
    var pageSize = filter.PageSize is < 1 or > 500 ? 25 : filter.PageSize;

    var allocations = await ApplyAllocationSort(ApplyAllocationFilters(scopedQuery, filter), filter.SortBy, filter.SortDesc)
      .Skip((page - 1) * pageSize)
      .Take(pageSize)
      .ToListAsync();

    await PopulateViewBagsAsync(scopedQuery, filter, page, pageSize, total);
    return View("~/Views/Employee/AllocationList.cshtml", allocations);
  }

  // הוספת הקצאה מרשימת ההקצאות: איתור עובד ומעבר לטופס ההקצאה שלו
  // (יישור לגרסת השרת).
  [HttpGet("create")]
  public async Task<IActionResult> Create(string? idNumber = null, string? employeeCode = null, string? firstName = null, string? lastName = null)
  {
    if (_currentUser.UserRole is not (UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator))
      return Forbid();

    var query = _db.Users.Where(u => u.StatusId == 1 && u.IsReportingEmployee);
    if (!string.IsNullOrWhiteSpace(idNumber))
    {
      var value = idNumber.Trim();
      query = query.Where(u => u.IdNumber.Replace("-", "").Replace(" ", "").Contains(value));
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

    ViewBag.IdNumber = idNumber;
    ViewBag.EmployeeCode = employeeCode;
    ViewBag.FirstName = firstName;
    ViewBag.LastName = lastName;
    ViewBag.Employees = await query
      .OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
      .Take(100)
      .ToListAsync();
    return View("~/Views/Allocations/Create.cshtml");
  }

  [HttpGet("{allocationId:int}")]
  public async Task<IActionResult> Details(int allocationId)
  {
    var allocation = await (await BuildScopedAllocationQueryAsync(_db, _currentUser.UserId, _currentUser.UserRole, includeFullDetails: true))
      .FirstOrDefaultAsync(a => a.Id == allocationId);
    if (allocation == null) return NotFound();

    if (_currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator)
      return RedirectToAction("EditAllocation", "Employee", new { id = allocation.UserId, allocationId = allocation.Id });

    return View("~/Views/MyAllocations/Details.cshtml", allocation);
  }

  [HttpGet("export")]
  public async Task<IActionResult> ExportExcel(AllocationListFilterModel filter)
  {
    filter.Normalize();

    var allocations = await ApplyAllocationSort(
        ApplyAllocationFilters(
          await BuildScopedAllocationQueryAsync(_db, _currentUser.UserId, _currentUser.UserRole),
          filter),
        filter.SortBy,
        filter.SortDesc)
      .ToListAsync();

    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("הקצאות עובדים");
    ws.RightToLeft = true;
    var headers = new[]
    {
      "פרויקט", "תוכנית", "מחוז", "מגזר", "ת.ז", "קוד עובד", "שם פרטי", "שם משפחה",
      "היקף פעילות חודשי", "היקף פעילות יומי", "היקף פעילות שנתי", "הקצאת שורות חודשית",
      "הקצאת שורות שנתית", "משך תפוקה", "הערות", "אפשר העלאת קובץ דיווח"
    };
    for (var i = 0; i < headers.Length; i++) ws.Cell(1, i + 1).Value = headers[i];

    var row = 2;
    foreach (var allocation in allocations)
    {
      ws.Cell(row, 1).Value = allocation.Project?.Description;
      ws.Cell(row, 2).Value = JoinValues(allocation.AllocationPrograms.Select(x => x.Program?.Description));
      ws.Cell(row, 3).Value = JoinValues(allocation.AllocationDistricts.Select(x => x.District?.Description));
      ws.Cell(row, 4).Value = JoinValues(allocation.AllocationSectors.Select(x => x.Sector?.Description));
      ws.Cell(row, 5).Value = allocation.User?.IdNumber;
      ws.Cell(row, 6).Value = allocation.User?.EmployeeCode;
      ws.Cell(row, 7).Value = allocation.User?.FirstName;
      ws.Cell(row, 8).Value = allocation.User?.LastName;
      if (allocation.MonthlyEmploymentScope.HasValue) ws.Cell(row, 9).Value = (double)allocation.MonthlyEmploymentScope.Value;
      ws.Cell(row, 10).Value = allocation.DailyEmploymentScope?.ToString("0.##") ?? "ללא הגבלה";
      if (allocation.AnnualEmploymentScope.HasValue) ws.Cell(row, 11).Value = (double)allocation.AnnualEmploymentScope.Value;
      if (allocation.MonthlyRowAllocation.HasValue) ws.Cell(row, 12).Value = allocation.MonthlyRowAllocation.Value;
      if (allocation.AnnualRowAllocation.HasValue) ws.Cell(row, 13).Value = allocation.AnnualRowAllocation.Value;
      ws.Cell(row, 14).Value = allocation.OutputDuration;
      ws.Cell(row, 15).Value = allocation.Notes;
      ws.Cell(row, 16).Value = allocation.AllowExcelUpload ? "כן" : "לא";
      row++;
    }

    ws.Row(1).Style.Font.Bold = true;
    ws.Columns().AdjustToContents();

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);

    return File(
      stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"employee_allocations_{DateTime.Now:yyyy-MM-dd}.xlsx");
  }

  internal static async Task<IQueryable<Allocation>> BuildScopedAllocationQueryAsync(
    AppDbContext db,
    int currentUserId,
    UserRoleEnum role,
    bool includeFullDetails = false)
  {
    var query = includeFullDetails ? FullAllocationQuery(db) : BaseAllocationQuery(db);

    if (role == UserRoleEnum.SystemAdmin)
      return query;

    if (role == UserRoleEnum.Employee)
      return query.Where(a => a.UserId == currentUserId);

    var assignments = await db.InspectorAssignments
      .AsNoTracking()
      .Where(a => a.InspectorUserId == currentUserId)
      .ToListAsync();
    if (assignments.Count == 0)
      return query.Where(a => false);

    var scopedIds = new HashSet<int>();
    foreach (var assignment in assignments)
    {
      var scoped = db.Allocations.Where(a => a.IsActive);

      if (assignment.DistrictId.HasValue)
      {
        var districtId = assignment.DistrictId.Value;
        scoped = scoped.Where(a => db.Set<AllocationDistrict>()
          .Any(x => x.AllocationId == a.Id && x.DistrictId == districtId));
      }

      if (assignment.SectorId.HasValue)
      {
        var sectorId = assignment.SectorId.Value;
        scoped = scoped.Where(a => db.Set<AllocationSector>()
          .Any(x => x.AllocationId == a.Id && x.SectorId == sectorId));
      }

      if (assignment.ProgramId.HasValue)
      {
        var programId = assignment.ProgramId.Value;
        scoped = scoped.Where(a => db.Set<AllocationProgram>()
          .Any(x => x.AllocationId == a.Id && x.ProgramId == programId));
      }

      foreach (var id in await scoped.Select(a => a.Id).ToListAsync())
        scopedIds.Add(id);
    }

    return scopedIds.Count == 0
      ? query.Where(a => false)
      : query.Where(a => scopedIds.Contains(a.Id));
  }

  internal static IQueryable<Allocation> ApplyAllocationFilters(
    IQueryable<Allocation> query,
    AllocationListFilterModel filter)
  {
    if (filter.ProjectId.HasValue)
      query = query.Where(a => a.ProjectId == filter.ProjectId.Value);

    if (filter.EmployeeId.HasValue)
      query = query.Where(a => a.UserId == filter.EmployeeId.Value);

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

  private async Task PopulateViewBagsAsync(
    IQueryable<Allocation> scopedQuery,
    AllocationListFilterModel filter,
    int page,
    int pageSize,
    int total)
  {
    var scopedAllocationIds = scopedQuery.Select(a => a.Id);
    var projectIds = scopedQuery.Select(a => a.ProjectId).Distinct();
    var programIds = _db.Set<AllocationProgram>()
      .Where(x => scopedAllocationIds.Contains(x.AllocationId))
      .Select(x => x.ProgramId)
      .Distinct();
    var districtIds = _db.Set<AllocationDistrict>()
      .Where(x => scopedAllocationIds.Contains(x.AllocationId))
      .Select(x => x.DistrictId)
      .Distinct();
    var sectorIds = _db.Set<AllocationSector>()
      .Where(x => scopedAllocationIds.Contains(x.AllocationId))
      .Select(x => x.SectorId)
      .Distinct();

    ViewBag.Filter = filter;
    ViewBag.Search = filter.Search;
    ViewBag.ProjectId = filter.ProjectId;
    ViewBag.SortBy = filter.SortBy;
    ViewBag.SortDesc = filter.SortDesc;
    ViewBag.Page = page;
    ViewBag.PageSize = pageSize;
    ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
    ViewBag.EmployeeContext = filter.EmployeeId.HasValue
      ? await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == filter.EmployeeId.Value)
      : null;
    ViewBag.Projects = await _db.Projects.Where(p => p.IsActive && projectIds.Contains(p.Id)).OrderBy(p => p.Description).ToListAsync();
    ViewBag.AllPrograms = await _db.Programs.Where(p => p.IsActive && programIds.Contains(p.Id)).OrderBy(p => p.Description).ToListAsync();
    ViewBag.AllDistricts = await _db.Districts.Where(d => d.IsActive && districtIds.Contains(d.Id)).OrderBy(d => d.Description).ToListAsync();
    ViewBag.AllSectors = await _db.Sectors.Where(s => s.IsActive && sectorIds.Contains(s.Id)).OrderBy(s => s.Description).ToListAsync();
    ViewBag.OutputDurationOptions = OUTPUT_DURATION_OPTIONS;
    ViewBag.AllocationListController = "Allocations";
    ViewBag.AllocationListAction = "Index";
    ViewBag.AllocationExportAction = "ExportExcel";
    ViewBag.AllocationDetailController = "Allocations";
    ViewBag.AllocationDetailAction = "Details";
  }

  private static IQueryable<Allocation> BaseAllocationQuery(AppDbContext db) =>
    db.Allocations
      .AsSplitQuery()
      .Include(a => a.User)
      .Include(a => a.Project)
      .Include(a => a.AllocationDistricts).ThenInclude(x => x.District)
      .Include(a => a.AllocationPrograms).ThenInclude(x => x.Program)
      .Include(a => a.AllocationSectors).ThenInclude(x => x.Sector)
      .Where(a => a.IsActive);

  private static IQueryable<Allocation> FullAllocationQuery(AppDbContext db) =>
    db.Allocations
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
      .Where(a => a.IsActive);

  private static IOrderedQueryable<Allocation> ApplyAllocationSort(
    IQueryable<Allocation> query,
    string? sortBy,
    bool sortDesc)
  {
    return sortBy?.ToLowerInvariant() switch
    {
      "project" => sortDesc ? query.OrderByDescending(a => a.Project!.Description) : query.OrderBy(a => a.Project!.Description),
      "idnumber" => sortDesc ? query.OrderByDescending(a => a.User!.IdNumber) : query.OrderBy(a => a.User!.IdNumber),
      "code" => sortDesc ? query.OrderByDescending(a => a.User!.EmployeeCode) : query.OrderBy(a => a.User!.EmployeeCode),
      "firstname" => sortDesc ? query.OrderByDescending(a => a.User!.FirstName) : query.OrderBy(a => a.User!.FirstName),
      "lastname" => sortDesc ? query.OrderByDescending(a => a.User!.LastName) : query.OrderBy(a => a.User!.LastName),
      "monthlyscope" => sortDesc ? query.OrderByDescending(a => a.MonthlyEmploymentScope) : query.OrderBy(a => a.MonthlyEmploymentScope),
      "dailyscope" => sortDesc ? query.OrderByDescending(a => a.DailyEmploymentScope) : query.OrderBy(a => a.DailyEmploymentScope),
      "annualscope" => sortDesc ? query.OrderByDescending(a => a.AnnualEmploymentScope) : query.OrderBy(a => a.AnnualEmploymentScope),
      "monthlyrows" => sortDesc ? query.OrderByDescending(a => a.MonthlyRowAllocation) : query.OrderBy(a => a.MonthlyRowAllocation),
      "annualrows" => sortDesc ? query.OrderByDescending(a => a.AnnualRowAllocation) : query.OrderBy(a => a.AnnualRowAllocation),
      "outputduration" => sortDesc ? query.OrderByDescending(a => a.OutputDuration) : query.OrderBy(a => a.OutputDuration),
      "allowexcelupload" => sortDesc ? query.OrderByDescending(a => a.AllowExcelUpload) : query.OrderBy(a => a.AllowExcelUpload),
      "notes" => sortDesc ? query.OrderByDescending(a => a.Notes) : query.OrderBy(a => a.Notes),
      _ => query.OrderBy(a => a.User!.LastName).ThenBy(a => a.User!.FirstName).ThenBy(a => a.Project!.Description)
    };
  }

  private static string JoinValues(IEnumerable<string?> values)
  {
    var items = values.Where(v => !string.IsNullOrWhiteSpace(v)).Distinct().ToList();
    return items.Count == 0 ? "" : string.Join(", ", items);
  }
}

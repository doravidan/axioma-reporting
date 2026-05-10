using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class DashboardFilter
{
  public int? DistrictId { get; set; }
  public int? SectorId { get; set; }
  public int? ProgramId { get; set; }
  public string? EmployeeCode { get; set; }
  public string? IdNumber { get; set; }
  public string? EmployeeName { get; set; }
  public int? StatusId { get; set; }
  public int? FromMonthId { get; set; }
  public int? ToMonthId { get; set; }
  public int Page { get; set; } = 1;
  public int PageSize { get; set; } = 25;
  public string? SortBy { get; set; }
  public bool SortDesc { get; set; }
}

public class IdName
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
}

public class FilterOptionsDto
{
  public List<IdName> Districts { get; set; } = new();
  public List<IdName> Sectors { get; set; } = new();
  public List<IdName> Programs { get; set; } = new();
  public List<IdName> Employees { get; set; } = new();
  public List<IdName> Projects { get; set; } = new();
  public List<IdName> Months { get; set; } = new();
  public List<IdName> Statuses { get; set; } = new();
}

public class DashboardReportRow
{
  public int ReportId { get; set; }
  public int UserId { get; set; }
  public string EmployeeCode { get; set; } = string.Empty;
  public string IdNumber { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
  public string MonthDescription { get; set; } = string.Empty;
  public int MonthYear { get; set; }
  public int MonthMonth { get; set; }
  public string StatusName { get; set; } = string.Empty;
  public int StatusId { get; set; }
  public int RowCount { get; set; }
  public decimal TotalDuration { get; set; }
  public int? MonthlyRowAllocation { get; set; }
  public int RemainingRows => MonthlyRowAllocation.HasValue ? MonthlyRowAllocation.Value - RowCount : 0;
  public bool HasAttachments { get; set; }
  public DateTime? SubmittedAt { get; set; }
}

public interface IDashboardFilterService
{
  Task<(List<DashboardReportRow> Rows, int TotalCount)> GetReportsAsync(
    DashboardFilter filter, int currentUserId, UserRoleEnum currentUserRole);
  Task<List<District>> GetFilteredDistrictsAsync(int currentUserId, UserRoleEnum role);
  Task<List<Sector>> GetFilteredSectorsAsync(int currentUserId, UserRoleEnum role, int? districtId = null);
  Task<List<Program>> GetFilteredProgramsAsync(int currentUserId, UserRoleEnum role, int? districtId = null);
  Task<bool> CanAccessReportAsync(int reportId, int currentUserId, UserRoleEnum currentUserRole);
  Task<FilterOptionsDto> GetCompatibleOptionsAsync(
    DashboardFilter currentSelection, int currentUserId, UserRoleEnum currentUserRole);
}

public class DashboardFilterService : IDashboardFilterService
{
  private readonly AppDbContext _db;

  public DashboardFilterService(AppDbContext db) { _db = db; }

  public async Task<(List<DashboardReportRow> Rows, int TotalCount)> GetReportsAsync(
    DashboardFilter filter, int currentUserId, UserRoleEnum currentUserRole)
  {
    filter.Page = Math.Max(filter.Page, 1);
    filter.PageSize = filter.PageSize is < 1 or > 10000 ? 25 : filter.PageSize;

    var scopedUserIds = await GetScopedUserIdsAsync(currentUserId, currentUserRole);
    var allocationIds = await GetMatchingAllocationIdsAsync(filter, scopedUserIds);
    var employeeIds = await _db.Allocations
      .Where(a => allocationIds.Contains(a.Id))
      .Select(a => a.UserId)
      .Distinct()
      .ToListAsync();

    var employeeQuery = _db.Users
      .Where(u => u.IsReportingEmployee && u.StatusId == 1 && employeeIds.Contains(u.Id));

    if (!string.IsNullOrWhiteSpace(filter.EmployeeCode))
      employeeQuery = employeeQuery.Where(u => u.EmployeeCode.Contains(filter.EmployeeCode));
    if (!string.IsNullOrWhiteSpace(filter.IdNumber))
      employeeQuery = employeeQuery.Where(u => u.IdNumber.Contains(filter.IdNumber));
    if (!string.IsNullOrWhiteSpace(filter.EmployeeName))
      employeeQuery = employeeQuery.Where(u => (u.FirstName + " " + u.LastName).Contains(filter.EmployeeName));

    employeeIds = await employeeQuery.Select(u => u.Id).ToListAsync();

    if (filter.StatusId == 0)
      return await GetMissingReportsAsync(filter, allocationIds, employeeIds);

    var reports = _db.Reports
      .Include(r => r.User)
      .Include(r => r.Status)
      .Include(r => r.ReportingMonth)
      .Where(r => employeeIds.Contains(r.UserId));

    reports = await ApplyMonthRangeAsync(reports, filter);

    if (filter.StatusId.HasValue)
      reports = reports.Where(r => r.StatusId == filter.StatusId.Value);

    if (HasAllocationDimensionFilter(filter))
      reports = reports.Where(r => r.ReportRows.Any(rr =>
        rr.AllocationId.HasValue && allocationIds.Contains(rr.AllocationId.Value)));

    var total = await reports.CountAsync();
    var pageReports = await ApplyReportSort(reports, filter.SortBy, filter.SortDesc)
      .Skip((filter.Page - 1) * filter.PageSize)
      .Take(filter.PageSize)
      .Select(r => new
      {
        Report = r,
        Rows = HasAllocationDimensionFilter(filter)
          ? r.ReportRows.Where(rr => rr.AllocationId.HasValue && allocationIds.Contains(rr.AllocationId.Value))
          : r.ReportRows
      })
      .ToListAsync();

    var rows = pageReports.Select(x => new DashboardReportRow
    {
      ReportId = x.Report.Id,
      UserId = x.Report.UserId,
      EmployeeCode = x.Report.User!.EmployeeCode,
      IdNumber = x.Report.User.IdNumber,
      FullName = x.Report.User.FirstName + " " + x.Report.User.LastName,
      MonthDescription = x.Report.ReportingMonth!.Description,
      MonthYear = x.Report.ReportingMonth.Year,
      MonthMonth = x.Report.ReportingMonth.Month,
      StatusName = x.Report.Status!.Name,
      StatusId = x.Report.StatusId,
      RowCount = x.Rows.Count(),
      TotalDuration = x.Rows.Sum(row => row.MeetingDuration),
      MonthlyRowAllocation = GetMonthlyRowAllocation(x.Rows.Select(rr => rr.AllocationId), allocationIds),
      SubmittedAt = x.Report.SubmittedAt,
      HasAttachments = false
    }).ToList();

    await PopulateAttachmentFlagsAsync(rows);
    return (rows, total);
  }

  public async Task<bool> CanAccessReportAsync(int reportId, int currentUserId, UserRoleEnum currentUserRole)
  {
    var report = await _db.Reports.FindAsync(reportId);
    if (report == null) return false;
    if (currentUserRole == UserRoleEnum.Employee) return report.UserId == currentUserId;
    if (currentUserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator)
      return true;

    var scopedUserIds = await GetScopedUserIdsAsync(currentUserId, currentUserRole);
    return scopedUserIds.Contains(report.UserId);
  }

  public async Task<FilterOptionsDto> GetCompatibleOptionsAsync(
    DashboardFilter currentSelection, int currentUserId, UserRoleEnum currentUserRole)
  {
    currentSelection ??= new DashboardFilter();
    var scopedUserIds = await GetScopedUserIdsAsync(currentUserId, currentUserRole);

    // Load all reports in scope (join to report rows for allocation dimensions)
    var reportBase = _db.Reports
      .Include(r => r.ReportingMonth)
      .Include(r => r.User)
      .Where(r => scopedUserIds.Contains(r.UserId));

    async Task<List<int>> AllocationIdsForAsync(int? districtId, int? sectorId, int? programId)
    {
      var q = _db.Allocations.Where(a => a.IsActive && scopedUserIds.Contains(a.UserId));
      if (districtId.HasValue)
      {
        var dId = districtId.Value;
        q = q.Where(a => _db.Set<AllocationDistrict>().Any(ad => ad.AllocationId == a.Id && ad.DistrictId == dId));
      }
      if (sectorId.HasValue)
      {
        var sId = sectorId.Value;
        q = q.Where(a => _db.Set<AllocationSector>().Any(s => s.AllocationId == a.Id && s.SectorId == sId));
      }
      if (programId.HasValue)
      {
        var pId = programId.Value;
        q = q.Where(a => _db.Set<AllocationProgram>().Any(p => p.AllocationId == a.Id && p.ProgramId == pId));
      }
      return await q.Select(a => a.Id).ToListAsync();
    }

    async Task<IQueryable<Report>> ApplyFiltersExceptAsync(string excludeDimension)
    {
      var q = reportBase;

      // Month range
      if (excludeDimension != "months")
        q = await ApplyMonthRangeAsync(q, currentSelection);

      // Status
      if (excludeDimension != "status" && currentSelection.StatusId.HasValue && currentSelection.StatusId.Value != 0)
        q = q.Where(r => r.StatusId == currentSelection.StatusId.Value);

      // Employee text filters
      if (excludeDimension != "employees")
      {
        if (!string.IsNullOrWhiteSpace(currentSelection.EmployeeCode))
          q = q.Where(r => r.User!.EmployeeCode.Contains(currentSelection.EmployeeCode));
        if (!string.IsNullOrWhiteSpace(currentSelection.IdNumber))
          q = q.Where(r => r.User!.IdNumber.Contains(currentSelection.IdNumber));
        if (!string.IsNullOrWhiteSpace(currentSelection.EmployeeName))
          q = q.Where(r => (r.User!.FirstName + " " + r.User!.LastName).Contains(currentSelection.EmployeeName));
      }

      // Allocation dimensions (district / sector / program) — exclude the named dim
      var districtId = excludeDimension == "districts" ? null : currentSelection.DistrictId;
      var sectorId = excludeDimension == "sectors" ? null : currentSelection.SectorId;
      var programId = excludeDimension == "programs" ? null : currentSelection.ProgramId;

      if (districtId.HasValue || sectorId.HasValue || programId.HasValue)
      {
        var allocIds = await AllocationIdsForAsync(districtId, sectorId, programId);
        q = q.Where(r => r.ReportRows.Any(rr =>
          rr.AllocationId.HasValue && allocIds.Contains(rr.AllocationId.Value)));
      }

      return q;
    }

    // Districts: from allocations referenced by reports matching all filters except district.
    var qForDistricts = await ApplyFiltersExceptAsync("districts");
    var districtReportUserIds = await qForDistricts.Select(r => r.UserId).Distinct().ToListAsync();
    var districtAllocIds = await AllocationIdsForAsync(null, currentSelection.SectorId, currentSelection.ProgramId);
    var compatibleDistrictIds = await _db.Set<AllocationDistrict>()
      .Where(ad => districtAllocIds.Contains(ad.AllocationId)
                   && _db.Allocations.Any(a => a.Id == ad.AllocationId && districtReportUserIds.Contains(a.UserId)))
      .Select(ad => ad.DistrictId)
      .Distinct()
      .ToListAsync();
    var districts = await _db.Districts
      .Where(d => d.IsActive && compatibleDistrictIds.Contains(d.Id))
      .OrderBy(d => d.Description)
      .Select(d => new IdName { Id = d.Id, Name = d.Description })
      .ToListAsync();

    // Sectors
    var qForSectors = await ApplyFiltersExceptAsync("sectors");
    var sectorReportUserIds = await qForSectors.Select(r => r.UserId).Distinct().ToListAsync();
    var sectorAllocIds = await AllocationIdsForAsync(currentSelection.DistrictId, null, currentSelection.ProgramId);
    var compatibleSectorIds = await _db.Set<AllocationSector>()
      .Where(sx => sectorAllocIds.Contains(sx.AllocationId)
                   && _db.Allocations.Any(a => a.Id == sx.AllocationId && sectorReportUserIds.Contains(a.UserId)))
      .Select(sx => sx.SectorId)
      .Distinct()
      .ToListAsync();
    var sectors = await _db.Sectors
      .Where(s => s.IsActive && compatibleSectorIds.Contains(s.Id))
      .OrderBy(s => s.Description)
      .Select(s => new IdName { Id = s.Id, Name = s.Description })
      .ToListAsync();

    // Programs
    var qForPrograms = await ApplyFiltersExceptAsync("programs");
    var programReportUserIds = await qForPrograms.Select(r => r.UserId).Distinct().ToListAsync();
    var programAllocIds = await AllocationIdsForAsync(currentSelection.DistrictId, currentSelection.SectorId, null);
    var compatibleProgramIds = await _db.Set<AllocationProgram>()
      .Where(px => programAllocIds.Contains(px.AllocationId)
                   && _db.Allocations.Any(a => a.Id == px.AllocationId && programReportUserIds.Contains(a.UserId)))
      .Select(px => px.ProgramId)
      .Distinct()
      .ToListAsync();
    var programs = await _db.Programs
      .Where(p => p.IsActive && compatibleProgramIds.Contains(p.Id))
      .OrderBy(p => p.Description)
      .Select(p => new IdName { Id = p.Id, Name = p.Description })
      .ToListAsync();

    // Employees
    var qForEmployees = await ApplyFiltersExceptAsync("employees");
    var empIds = await qForEmployees.Select(r => r.UserId).Distinct().ToListAsync();
    var employees = await _db.Users
      .Where(u => empIds.Contains(u.Id))
      .OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
      .Select(u => new IdName { Id = u.Id, Name = u.FirstName + " " + u.LastName + " (" + u.EmployeeCode + ")" })
      .ToListAsync();

    // Projects — derived from allocations of reports matching all filters
    var qForProjects = await ApplyFiltersExceptAsync("projects");
    var projReportUserIds = await qForProjects.Select(r => r.UserId).Distinct().ToListAsync();
    var projAllocIds = await AllocationIdsForAsync(currentSelection.DistrictId, currentSelection.SectorId, currentSelection.ProgramId);
    var compatibleProjectIds = await _db.Allocations
      .Where(a => projAllocIds.Contains(a.Id) && projReportUserIds.Contains(a.UserId))
      .Select(a => a.ProjectId)
      .Distinct()
      .ToListAsync();
    var projects = await _db.Projects
      .Where(p => p.IsActive && compatibleProjectIds.Contains(p.Id))
      .OrderBy(p => p.Description)
      .Select(p => new IdName { Id = p.Id, Name = p.Description })
      .ToListAsync();

    // Months
    var qForMonths = await ApplyFiltersExceptAsync("months");
    var compatibleMonthIds = await qForMonths.Select(r => r.ReportingMonthId).Distinct().ToListAsync();
    var months = await _db.ReportingMonths
      .Where(m => compatibleMonthIds.Contains(m.Id))
      .OrderByDescending(m => m.Year).ThenByDescending(m => m.Month)
      .Select(m => new IdName { Id = m.Id, Name = m.Description })
      .ToListAsync();

    // Statuses
    var qForStatuses = await ApplyFiltersExceptAsync("status");
    var compatibleStatusIds = await qForStatuses.Select(r => r.StatusId).Distinct().ToListAsync();
    var statuses = await _db.ReportStatuses
      .Where(s => compatibleStatusIds.Contains(s.Id))
      .OrderBy(s => s.Id)
      .Select(s => new IdName { Id = s.Id, Name = s.Name })
      .ToListAsync();

    return new FilterOptionsDto
    {
      Districts = districts,
      Sectors = sectors,
      Programs = programs,
      Employees = employees,
      Projects = projects,
      Months = months,
      Statuses = statuses
    };
  }

  private async Task<(List<DashboardReportRow> Rows, int TotalCount)> GetMissingReportsAsync(
    DashboardFilter filter, List<int> allocationIds, List<int> employeeIds)
  {
    var month = await ResolveMissingReportMonthAsync(filter);
    if (month == null) return (new List<DashboardReportRow>(), 0);

    var employees = _db.Users
      .Where(u => employeeIds.Contains(u.Id) &&
                  !_db.Reports.Any(r => r.UserId == u.Id &&
                                        r.ReportingMonthId == month.Id &&
                                        r.StatusId != 1 &&
                                        r.StatusId != 2));

    var total = await employees.CountAsync();
    var page = await ApplyMissingReportSort(employees, filter.SortBy, filter.SortDesc)
      .Skip((filter.Page - 1) * filter.PageSize)
      .Take(filter.PageSize)
      .ToListAsync();

    var matchingAllocations = await _db.Allocations
      .Where(a => allocationIds.Contains(a.Id))
      .Select(a => new { a.UserId, a.MonthlyRowAllocation })
      .ToListAsync();

    var allocationLimits = matchingAllocations
      .GroupBy(a => a.UserId)
      .ToDictionary(g => g.Key, g => g.Min(a => a.MonthlyRowAllocation));

    var rows = page.Select(u => new DashboardReportRow
    {
      ReportId = 0,
      UserId = u.Id,
      EmployeeCode = u.EmployeeCode,
      IdNumber = u.IdNumber,
      FullName = u.FirstName + " " + u.LastName,
      MonthDescription = month.Description,
      MonthYear = month.Year,
      MonthMonth = month.Month,
      StatusName = "טרם דווח",
      StatusId = 0,
      MonthlyRowAllocation = allocationLimits.TryGetValue(u.Id, out var limit) ? limit : null
    }).ToList();

    return (rows, total);
  }

  private async Task<IQueryable<Report>> ApplyMonthRangeAsync(IQueryable<Report> reports, DashboardFilter filter)
  {
    if (filter.FromMonthId.HasValue)
    {
      var from = await _db.ReportingMonths.FindAsync(filter.FromMonthId.Value);
      if (from != null)
      {
        var fromKey = from.Year * 12 + from.Month;
        reports = reports.Where(r => r.ReportingMonth!.Year * 12 + r.ReportingMonth.Month >= fromKey);
      }
    }

    if (filter.ToMonthId.HasValue)
    {
      var to = await _db.ReportingMonths.FindAsync(filter.ToMonthId.Value);
      if (to != null)
      {
        var toKey = to.Year * 12 + to.Month;
        reports = reports.Where(r => r.ReportingMonth!.Year * 12 + r.ReportingMonth.Month <= toKey);
      }
    }

    return reports;
  }

  private async Task<ReportingMonth?> ResolveMissingReportMonthAsync(DashboardFilter filter)
  {
    if (filter.FromMonthId.HasValue && filter.ToMonthId.HasValue && filter.FromMonthId == filter.ToMonthId)
      return await _db.ReportingMonths.FindAsync(filter.FromMonthId.Value);

    if (filter.FromMonthId.HasValue && !filter.ToMonthId.HasValue)
      return await _db.ReportingMonths.FindAsync(filter.FromMonthId.Value);

    return await _db.ReportingMonths.FirstOrDefaultAsync(m => m.IsActive);
  }

  private int? GetMonthlyRowAllocation(IEnumerable<int?> rowAllocationIds, List<int> fallbackAllocationIds)
  {
    var ids = rowAllocationIds.Where(id => id.HasValue).Select(id => id!.Value).Distinct().ToList();
    if (!ids.Any()) ids = fallbackAllocationIds;
    return _db.Allocations
      .Where(a => ids.Contains(a.Id) && a.MonthlyRowAllocation.HasValue)
      .Select(a => a.MonthlyRowAllocation)
      .AsEnumerable()
      .Min();
  }

  private async Task PopulateAttachmentFlagsAsync(List<DashboardReportRow> rows)
  {
    if (!rows.Any()) return;

    var reportIds = rows.Where(r => r.ReportId != 0).Select(r => r.ReportId).ToList();
    var reportIdsWithAttachments = await _db.ReportRows
      .Where(rr => reportIds.Contains(rr.ReportId) &&
                   _db.DocumentAttachments.Any(da => da.ReportRowId == rr.Id))
      .Select(rr => rr.ReportId)
      .Distinct()
      .ToListAsync();

    var set = reportIdsWithAttachments.ToHashSet();
    foreach (var row in rows)
      row.HasAttachments = set.Contains(row.ReportId);
  }

  private async Task<List<int>> GetMatchingAllocationIdsAsync(DashboardFilter filter, List<int> scopedUserIds)
  {
    var q = _db.Allocations.Where(a => a.IsActive && scopedUserIds.Contains(a.UserId));

    if (filter.DistrictId.HasValue)
    {
      var districtId = filter.DistrictId.Value;
      q = q.Where(a => _db.Set<AllocationDistrict>()
        .Any(ad => ad.AllocationId == a.Id && ad.DistrictId == districtId));
    }

    if (filter.SectorId.HasValue)
    {
      var sectorId = filter.SectorId.Value;
      q = q.Where(a => _db.Set<AllocationSector>()
        .Any(s => s.AllocationId == a.Id && s.SectorId == sectorId));
    }

    if (filter.ProgramId.HasValue)
    {
      var programId = filter.ProgramId.Value;
      q = q.Where(a => _db.Set<AllocationProgram>()
        .Any(p => p.AllocationId == a.Id && p.ProgramId == programId));
    }

    return await q.Select(a => a.Id).ToListAsync();
  }

  private async Task<List<int>> GetScopedUserIdsAsync(int currentUserId, UserRoleEnum role)
  {
    if (role is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager or UserRoleEnum.ProjectCoordinator)
      return await _db.Users.Select(u => u.Id).ToListAsync();
    if (role == UserRoleEnum.Employee)
      return new List<int> { currentUserId };

    var assignments = await _db.InspectorAssignments
      .Where(a => a.InspectorUserId == currentUserId)
      .ToListAsync();
    if (!assignments.Any()) return new List<int>();

    var allUserIds = new HashSet<int>();
    foreach (var assignment in assignments)
    {
      var q = _db.Allocations.Where(a => a.IsActive);

      if (assignment.DistrictId.HasValue)
      {
        var dId = assignment.DistrictId.Value;
        q = q.Where(a => _db.Set<AllocationDistrict>()
          .Any(ad => ad.AllocationId == a.Id && ad.DistrictId == dId));
      }

      if (assignment.SectorId.HasValue)
      {
        var sId = assignment.SectorId.Value;
        q = q.Where(a => _db.Set<AllocationSector>()
          .Any(s => s.AllocationId == a.Id && s.SectorId == sId));
      }

      if (assignment.ProgramId.HasValue)
      {
        var pId = assignment.ProgramId.Value;
        q = q.Where(a => _db.Set<AllocationProgram>()
          .Any(p => p.AllocationId == a.Id && p.ProgramId == pId));
      }

      foreach (var uid in await q.Select(a => a.UserId).ToListAsync())
        allUserIds.Add(uid);
    }

    return allUserIds.ToList();
  }

  public async Task<List<District>> GetFilteredDistrictsAsync(int currentUserId, UserRoleEnum role)
  {
    var scopedUserIds = await GetScopedUserIdsAsync(currentUserId, role);
    var districtIds = _db.Set<AllocationDistrict>()
      .Where(ad => scopedUserIds.Contains(ad.Allocation!.UserId))
      .Select(ad => ad.DistrictId);

    return await _db.Districts
      .Where(d => d.IsActive && districtIds.Contains(d.Id))
      .OrderBy(d => d.Description)
      .ToListAsync();
  }

  public async Task<List<Sector>> GetFilteredSectorsAsync(
    int currentUserId, UserRoleEnum role, int? districtId = null)
  {
    var scopedUserIds = await GetScopedUserIdsAsync(currentUserId, role);
    var allocations = _db.Allocations.Where(a => a.IsActive && scopedUserIds.Contains(a.UserId));
    if (districtId.HasValue)
    {
      var dId = districtId.Value;
      allocations = allocations.Where(a => _db.Set<AllocationDistrict>()
        .Any(ad => ad.AllocationId == a.Id && ad.DistrictId == dId));
    }

    var sectorIds = _db.Set<AllocationSector>()
      .Where(s => allocations.Select(a => a.Id).Contains(s.AllocationId))
      .Select(s => s.SectorId);

    return await _db.Sectors
      .Where(s => s.IsActive && sectorIds.Contains(s.Id))
      .OrderBy(s => s.Description)
      .ToListAsync();
  }

  public async Task<List<Program>> GetFilteredProgramsAsync(
    int currentUserId, UserRoleEnum role, int? districtId = null)
  {
    var scopedUserIds = await GetScopedUserIdsAsync(currentUserId, role);
    var allocations = _db.Allocations.Where(a => a.IsActive && scopedUserIds.Contains(a.UserId));
    if (districtId.HasValue)
    {
      var dId = districtId.Value;
      allocations = allocations.Where(a => _db.Set<AllocationDistrict>()
        .Any(ad => ad.AllocationId == a.Id && ad.DistrictId == dId));
    }

    var programIds = _db.Set<AllocationProgram>()
      .Where(p => allocations.Select(a => a.Id).Contains(p.AllocationId))
      .Select(p => p.ProgramId);

    return await _db.Programs
      .Where(p => p.IsActive && programIds.Contains(p.Id))
      .OrderBy(p => p.Description)
      .ToListAsync();
  }

  private static bool HasAllocationDimensionFilter(DashboardFilter filter) =>
    filter.DistrictId.HasValue || filter.SectorId.HasValue || filter.ProgramId.HasValue;

  private static IOrderedQueryable<Report> ApplyReportSort(
    IQueryable<Report> reports, string? sortBy, bool sortDesc)
  {
    return sortBy?.ToLowerInvariant() switch
    {
      "code" => sortDesc
        ? reports.OrderByDescending(r => r.User!.EmployeeCode)
        : reports.OrderBy(r => r.User!.EmployeeCode),
      "idnumber" => sortDesc
        ? reports.OrderByDescending(r => r.User!.IdNumber)
        : reports.OrderBy(r => r.User!.IdNumber),
      "name" => sortDesc
        ? reports.OrderByDescending(r => r.User!.LastName).ThenByDescending(r => r.User!.FirstName)
        : reports.OrderBy(r => r.User!.LastName).ThenBy(r => r.User!.FirstName),
      "status" => sortDesc
        ? reports.OrderByDescending(r => r.Status!.Name)
        : reports.OrderBy(r => r.Status!.Name),
      "submitted" => sortDesc
        ? reports.OrderByDescending(r => r.SubmittedAt)
        : reports.OrderBy(r => r.SubmittedAt),
      "month" => sortDesc
        ? reports.OrderByDescending(r => r.ReportingMonth!.Year).ThenByDescending(r => r.ReportingMonth!.Month)
        : reports.OrderBy(r => r.ReportingMonth!.Year).ThenBy(r => r.ReportingMonth!.Month),
      _ => reports
        .OrderByDescending(r => r.ReportingMonth!.Year)
        .ThenByDescending(r => r.ReportingMonth!.Month)
        .ThenBy(r => r.User!.LastName)
        .ThenBy(r => r.User!.FirstName)
    };
  }

  private static IOrderedQueryable<User> ApplyMissingReportSort(
    IQueryable<User> users, string? sortBy, bool sortDesc)
  {
    return sortBy?.ToLowerInvariant() switch
    {
      "code" => sortDesc ? users.OrderByDescending(u => u.EmployeeCode) : users.OrderBy(u => u.EmployeeCode),
      "idnumber" => sortDesc ? users.OrderByDescending(u => u.IdNumber) : users.OrderBy(u => u.IdNumber),
      "name" => sortDesc
        ? users.OrderByDescending(u => u.LastName).ThenByDescending(u => u.FirstName)
        : users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName),
      _ => users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
    };
  }
}

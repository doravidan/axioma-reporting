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
  public int? LocalityId { get; set; }
  public int? FrameworkId { get; set; }
  public int? EducationalProgramId { get; set; }
  public int? DomainId { get; set; }
  public int? Subject1Id { get; set; }
  public int? Subject2Id { get; set; }
  public int? DiscussionCodeId { get; set; }
  public int? ClassId { get; set; }
  public int? GradeLevelId { get; set; }
  public int? ConclusionClassId { get; set; }
  public int? ConclusionFrameworkId { get; set; }
  public int? ConclusionLocationId { get; set; }
  public int? ReportTypeId { get; set; }
  public DateTime? MeetingDateFrom { get; set; }
  public DateTime? MeetingDateTo { get; set; }
  public decimal? MeetingDuration { get; set; }
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
  public int? AllocationId { get; set; }
  public string ProjectName { get; set; } = string.Empty;
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
  public int AttachmentCount { get; set; }
  public DateTime? SubmittedAt { get; set; }
}

public class DashboardReportDetailRow
{
  public int ReportId { get; set; }
  public int ReportRowId { get; set; }
  public int UserId { get; set; }
  public int? AllocationId { get; set; }
  public bool CanEdit { get; set; }
  public int SequenceNumber { get; set; }
  public string IdNumber { get; set; } = string.Empty;
  public string EmployeeCode { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
  public string StatusName { get; set; } = string.Empty;
  public int StatusId { get; set; }
  public string MonthDescription { get; set; } = string.Empty;
  public int MonthYear { get; set; }
  public int MonthMonth { get; set; }
  public string ProjectName { get; set; } = string.Empty;
  public string DistrictName { get; set; } = string.Empty;
  public string LocalityName { get; set; } = string.Empty;
  public string FrameworkName { get; set; } = string.Empty;
  public string EducationalProgramName { get; set; } = string.Empty;
  public string DomainName { get; set; } = string.Empty;
  public string Subject1Name { get; set; } = string.Empty;
  public string Subject2Name { get; set; } = string.Empty;
  public string DiscussionCodeName { get; set; } = string.Empty;
  public string ClassName { get; set; } = string.Empty;
  public string GradeLevelName { get; set; } = string.Empty;
  public string ConclusionClassName { get; set; } = string.Empty;
  public string ConclusionFrameworkName { get; set; } = string.Empty;
  public string ConclusionLocationName { get; set; } = string.Empty;
  public string ReportTypeName { get; set; } = string.Empty;
  public DateTime MeetingDate { get; set; }
  public decimal MeetingDuration { get; set; }
  public string Notes { get; set; } = string.Empty;
  public bool HasAttachments { get; set; }
  public int AttachmentCount { get; set; }
}

public interface IDashboardFilterService
{
  Task<(List<DashboardReportRow> Rows, int TotalCount)> GetReportsAsync(
    DashboardFilter filter, int currentUserId, UserRoleEnum currentUserRole);
  Task<(List<DashboardReportDetailRow> Rows, int TotalCount)> GetReportRowsAsync(
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

    var reportList = await reports
      .Include(r => r.ReportRows)
        .ThenInclude(rr => rr.Allocation)
        .ThenInclude(a => a!.Project)
      .ToListAsync();

    var rows = reportList
      .SelectMany(report => ToDashboardRows(report, filter, allocationIds))
      .ToList();

    var total = rows.Count;
    rows = ApplyDashboardRowSort(rows, filter.SortBy, filter.SortDesc)
      .Skip((filter.Page - 1) * filter.PageSize)
      .Take(filter.PageSize)
      .ToList();

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

  public async Task<(List<DashboardReportDetailRow> Rows, int TotalCount)> GetReportRowsAsync(
    DashboardFilter filter, int currentUserId, UserRoleEnum currentUserRole)
  {
    filter.Page = Math.Max(filter.Page, 1);
    filter.PageSize = filter.PageSize is < 1 or > 10000 ? 25 : filter.PageSize;

    var scopedUserIds = await GetScopedUserIdsAsync(currentUserId, currentUserRole);
    var allocationIds = await GetMatchingAllocationIdsAsync(filter, scopedUserIds);
    var employeeQuery = _db.Users
      .Where(u => scopedUserIds.Contains(u.Id) && u.IsReportingEmployee && u.StatusId == 1);

    if (!string.IsNullOrWhiteSpace(filter.EmployeeCode))
      employeeQuery = employeeQuery.Where(u => u.EmployeeCode.Contains(filter.EmployeeCode));
    if (!string.IsNullOrWhiteSpace(filter.IdNumber))
      employeeQuery = employeeQuery.Where(u => u.IdNumber.Contains(filter.IdNumber));
    if (!string.IsNullOrWhiteSpace(filter.EmployeeName))
      employeeQuery = employeeQuery.Where(u => (u.FirstName + " " + u.LastName).Contains(filter.EmployeeName));

    var activeEmployeeIds = await employeeQuery.Select(u => u.Id).ToListAsync();

    if (filter.StatusId == 0)
      return await GetMissingReportDetailRowsAsync(filter, allocationIds, activeEmployeeIds);

    var reports = _db.Reports
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .Where(r => activeEmployeeIds.Contains(r.UserId));

    reports = await ApplyMonthRangeAsync(reports, filter);

    if (filter.StatusId.HasValue)
      reports = reports.Where(r => r.StatusId == filter.StatusId.Value);

    var matchingReportIds = await reports.Select(r => r.Id).ToListAsync();

    var rows = _db.ReportRows
      .Include(rr => rr.Report).ThenInclude(r => r!.User)
      .Include(rr => rr.Report).ThenInclude(r => r!.Status)
      .Include(rr => rr.Report).ThenInclude(r => r!.ReportingMonth)
      .Include(rr => rr.Allocation).ThenInclude(a => a!.Project)
      .Include(rr => rr.District)
      .Include(rr => rr.Locality)
      .Include(rr => rr.Framework)
      .Include(rr => rr.EducationalProgram)
      .Include(rr => rr.Domain)
      .Include(rr => rr.Subject1)
      .Include(rr => rr.Subject2)
      .Include(rr => rr.DiscussionCode)
      .Include(rr => rr.Class)
      .Include(rr => rr.GradeLevel)
      .Include(rr => rr.ConclusionClass)
      .Include(rr => rr.ConclusionFramework)
      .Include(rr => rr.ConclusionLocation)
      .Include(rr => rr.ReportType)
      .Where(rr => matchingReportIds.Contains(rr.ReportId));

    if (HasAllocationDimensionFilter(filter))
      rows = rows.Where(rr => rr.AllocationId.HasValue && allocationIds.Contains(rr.AllocationId.Value));

    if (filter.DistrictId.HasValue)
      rows = rows.Where(rr => rr.DistrictId == filter.DistrictId.Value);
    if (filter.LocalityId.HasValue)
      rows = rows.Where(rr => rr.LocalityId == filter.LocalityId.Value);
    if (filter.FrameworkId.HasValue)
      rows = rows.Where(rr => rr.FrameworkId == filter.FrameworkId.Value);
    if (filter.EducationalProgramId.HasValue)
      rows = rows.Where(rr => rr.EducationalProgramId == filter.EducationalProgramId.Value);
    if (filter.DomainId.HasValue)
      rows = rows.Where(rr => rr.DomainId == filter.DomainId.Value);
    if (filter.Subject1Id.HasValue)
      rows = rows.Where(rr => rr.Subject1Id == filter.Subject1Id.Value);
    if (filter.Subject2Id.HasValue)
      rows = rows.Where(rr => rr.Subject2Id == filter.Subject2Id.Value);
    if (filter.DiscussionCodeId.HasValue)
      rows = rows.Where(rr => rr.DiscussionCodeId == filter.DiscussionCodeId.Value);
    if (filter.ClassId.HasValue)
      rows = rows.Where(rr => rr.ClassId == filter.ClassId.Value);
    if (filter.GradeLevelId.HasValue)
      rows = rows.Where(rr => rr.GradeLevelId == filter.GradeLevelId.Value);
    if (filter.ConclusionClassId.HasValue)
      rows = rows.Where(rr => rr.ConclusionClassId == filter.ConclusionClassId.Value);
    if (filter.ConclusionFrameworkId.HasValue)
      rows = rows.Where(rr => rr.ConclusionFrameworkId == filter.ConclusionFrameworkId.Value);
    if (filter.ConclusionLocationId.HasValue)
      rows = rows.Where(rr => rr.ConclusionLocationId == filter.ConclusionLocationId.Value);
    if (filter.ReportTypeId.HasValue)
      rows = rows.Where(rr => rr.ReportTypeId == filter.ReportTypeId.Value);
    if (filter.MeetingDateFrom.HasValue)
      rows = rows.Where(rr => rr.MeetingDate >= filter.MeetingDateFrom.Value.Date);
    if (filter.MeetingDateTo.HasValue)
    {
      var toDate = filter.MeetingDateTo.Value.Date.AddDays(1);
      rows = rows.Where(rr => rr.MeetingDate < toDate);
    }
    if (filter.MeetingDuration.HasValue)
      rows = rows.Where(rr => rr.MeetingDuration == filter.MeetingDuration.Value);

    var total = await rows.CountAsync();
    var pageRows = await ApplyReportRowSort(rows, filter.SortBy, filter.SortDesc)
      .Skip((filter.Page - 1) * filter.PageSize)
      .Take(filter.PageSize)
      .Select(rr => new DashboardReportDetailRow
      {
        ReportId = rr.ReportId,
        ReportRowId = rr.Id,
        UserId = rr.Report!.UserId,
        AllocationId = rr.AllocationId ?? _db.Allocations
          .Where(a => a.UserId == rr.Report.UserId && a.IsActive)
          .OrderBy(a => a.Id)
          .Select(a => (int?)a.Id)
          .FirstOrDefault(),
        CanEdit = currentUserRole == UserRoleEnum.SystemAdmin,
        SequenceNumber = rr.SequenceNumber,
        IdNumber = rr.Report.User!.IdNumber,
        EmployeeCode = rr.Report.User.EmployeeCode,
        FullName = rr.Report.User.FirstName + " " + rr.Report.User.LastName,
        StatusName = GetReportStatusText(rr.Report.Status!),
        StatusId = rr.Report.StatusId,
        MonthDescription = rr.Report.ReportingMonth!.Description,
        MonthYear = rr.Report.ReportingMonth.Year,
        MonthMonth = rr.Report.ReportingMonth.Month,
        ProjectName = rr.Allocation != null && rr.Allocation.Project != null ? rr.Allocation.Project.Description : string.Empty,
        DistrictName = rr.District != null ? rr.District.Description : string.Empty,
        LocalityName = rr.Locality != null ? rr.Locality.Description : string.Empty,
        FrameworkName = rr.Framework != null ? rr.Framework.Description : string.Empty,
        EducationalProgramName = rr.EducationalProgram != null ? rr.EducationalProgram.Description : string.Empty,
        DomainName = rr.Domain != null ? rr.Domain.Description : string.Empty,
        Subject1Name = rr.Subject1 != null ? rr.Subject1.Description : string.Empty,
        Subject2Name = rr.Subject2 != null ? rr.Subject2.Description : string.Empty,
        DiscussionCodeName = rr.DiscussionCode != null ? rr.DiscussionCode.Description : string.Empty,
        ClassName = rr.Class != null ? rr.Class.Description : string.Empty,
        GradeLevelName = rr.GradeLevel != null ? rr.GradeLevel.Description : string.Empty,
        ConclusionClassName = rr.ConclusionClass != null ? rr.ConclusionClass.Description : string.Empty,
        ConclusionFrameworkName = rr.ConclusionFramework != null ? rr.ConclusionFramework.Description : string.Empty,
        ConclusionLocationName = rr.ConclusionLocation != null ? rr.ConclusionLocation.Description : string.Empty,
        ReportTypeName = rr.ReportType != null ? rr.ReportType.Description : string.Empty,
        MeetingDate = rr.MeetingDate,
        MeetingDuration = rr.MeetingDuration,
        Notes = rr.Notes ?? string.Empty,
        HasAttachments = _db.DocumentAttachments.Any(da => da.ReportId == rr.ReportId || da.ReportRowId == rr.Id),
        // כמות מסמכים + גישה אליהם (משוב בטא B40)
        AttachmentCount = _db.DocumentAttachments.Count(da => da.ReportId == rr.ReportId || da.ReportRowId == rr.Id)
      })
      .ToListAsync();

    return (pageRows, total);
  }

  private async Task<(List<DashboardReportDetailRow> Rows, int TotalCount)> GetMissingReportDetailRowsAsync(
    DashboardFilter filter, List<int> allocationIds, List<int> employeeIds)
  {
    var month = await ResolveMissingReportMonthAsync(filter);
    if (month == null) return (new List<DashboardReportDetailRow>(), 0);

    var reportedUserIds = await _db.Reports
      .Where(r => r.ReportingMonthId == month.Id)
      .Select(r => r.UserId)
      .Distinct()
      .ToListAsync();

    var query = _db.Allocations
      .Include(a => a.User)
      .Include(a => a.Project)
      .Where(a => a.IsActive &&
                  allocationIds.Contains(a.Id) &&
                  employeeIds.Contains(a.UserId) &&
                  !reportedUserIds.Contains(a.UserId));

    var total = await query.CountAsync();
    var allocations = await query
      .OrderBy(a => a.User!.LastName)
      .ThenBy(a => a.User!.FirstName)
      .ThenBy(a => a.Project!.Description)
      .Skip((filter.Page - 1) * filter.PageSize)
      .Take(filter.PageSize)
      .ToListAsync();

    var rows = allocations.Select(a => new DashboardReportDetailRow
    {
      ReportId = 0,
      ReportRowId = 0,
      UserId = a.UserId,
      AllocationId = a.Id,
      CanEdit = false,
      SequenceNumber = 0,
      IdNumber = a.User?.IdNumber ?? string.Empty,
      EmployeeCode = a.User?.EmployeeCode ?? string.Empty,
      FullName = $"{a.User?.FirstName} {a.User?.LastName}".Trim(),
      StatusName = "טרם דווח",
      StatusId = 0,
      MonthDescription = month.Description,
      MonthYear = month.Year,
      MonthMonth = month.Month,
      ProjectName = a.Project?.Description ?? string.Empty,
      MeetingDate = DateTime.MinValue,
      MeetingDuration = 0
    }).ToList();

    return (rows, total);
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
    var statusEntities = await _db.ReportStatuses
      .Where(s => compatibleStatusIds.Contains(s.Id))
      .OrderBy(s => s.Id)
      .ToListAsync();
    var statuses = statusEntities
      .Select(s => new IdName { Id = s.Id, Name = GetReportStatusText(s) })
      .ToList();

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

  private async Task<IQueryable<ReportRow>> ApplyReportRowMonthRangeAsync(IQueryable<ReportRow> rows, DashboardFilter filter)
  {
    if (filter.FromMonthId.HasValue)
    {
      var from = await _db.ReportingMonths.FindAsync(filter.FromMonthId.Value);
      if (from != null)
      {
        var fromKey = from.Year * 12 + from.Month;
        rows = rows.Where(rr => rr.Report!.ReportingMonth!.Year * 12 + rr.Report.ReportingMonth.Month >= fromKey);
      }
    }

    if (filter.ToMonthId.HasValue)
    {
      var to = await _db.ReportingMonths.FindAsync(filter.ToMonthId.Value);
      if (to != null)
      {
        var toKey = to.Year * 12 + to.Month;
        rows = rows.Where(rr => rr.Report!.ReportingMonth!.Year * 12 + rr.Report.ReportingMonth.Month <= toKey);
      }
    }

    return rows;
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
    if (!ids.Any()) return null;
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
    var rowKeysWithAttachments = await _db.ReportRows
      .Where(rr => reportIds.Contains(rr.ReportId) &&
                   _db.DocumentAttachments.Any(da => da.ReportId == rr.ReportId || da.ReportRowId == rr.Id))
      .Select(rr => new { rr.ReportId, rr.AllocationId })
      .Distinct()
      .ToListAsync();

    // כמות מסמכים כוללת לכל דיווח (משוב בטא B40): מסמכים ברמת הדיווח + ברמת השורות.
    // SQL cannot GROUP BY a coalesced-subquery expression, so resolve the owning
    // report id in the projection and aggregate client-side (the page is small).
    var attachmentOwners = await _db.DocumentAttachments
      .Where(da => (da.ReportId != null && reportIds.Contains(da.ReportId.Value)) ||
                   (da.ReportRowId != null && _db.ReportRows.Any(rr => rr.Id == da.ReportRowId && reportIds.Contains(rr.ReportId))))
      .Select(da => new
      {
        ReportId = da.ReportId ?? _db.ReportRows.Where(rr => rr.Id == da.ReportRowId).Select(rr => rr.ReportId).FirstOrDefault()
      })
      .ToListAsync();
    var countsByReport = attachmentOwners
      .GroupBy(x => x.ReportId)
      .ToDictionary(g => g.Key, g => g.Count());

    var set = rowKeysWithAttachments
      .Select(x => (x.ReportId, x.AllocationId))
      .ToHashSet();
    foreach (var row in rows)
    {
      row.HasAttachments = row.AllocationId.HasValue
        ? set.Contains((row.ReportId, row.AllocationId))
        : set.Any(x => x.ReportId == row.ReportId);
      row.AttachmentCount = countsByReport.TryGetValue(row.ReportId, out var c) ? c : 0;
    }
  }

  private IEnumerable<DashboardReportRow> ToDashboardRows(
    Report report, DashboardFilter filter, List<int> allocationIds)
  {
    var reportRows = report.ReportRows.AsEnumerable();
    if (HasAllocationDimensionFilter(filter))
      reportRows = reportRows.Where(rr =>
        rr.AllocationId.HasValue && allocationIds.Contains(rr.AllocationId.Value));

    var groups = reportRows
      .GroupBy(rr => rr.AllocationId)
      .ToList();

    if (!groups.Any())
    {
      return new[]
      {
        ToDashboardRow(report, Enumerable.Empty<ReportRow>(), null, allocationIds)
      };
    }

    return groups.Select(group =>
    {
      var allocation = group.FirstOrDefault(rr => rr.Allocation != null)?.Allocation;
      return ToDashboardRow(report, group, allocation, allocationIds);
    });
  }

  private DashboardReportRow ToDashboardRow(
    Report report, IEnumerable<ReportRow> reportRows, Allocation? allocation, List<int> fallbackAllocationIds)
  {
    var rows = reportRows.ToList();
    return new DashboardReportRow
    {
      ReportId = report.Id,
      UserId = report.UserId,
      AllocationId = allocation?.Id ?? rows.FirstOrDefault()?.AllocationId,
      ProjectName = allocation?.Project?.Description ?? string.Empty,
      EmployeeCode = report.User!.EmployeeCode,
      IdNumber = report.User.IdNumber,
      FullName = report.User.FirstName + " " + report.User.LastName,
      MonthDescription = report.ReportingMonth!.Description,
      MonthYear = report.ReportingMonth.Year,
      MonthMonth = report.ReportingMonth.Month,
      StatusName = GetReportStatusText(report.Status!),
      StatusId = report.StatusId,
      RowCount = rows.Count,
      TotalDuration = rows.Sum(row => row.MeetingDuration),
      MonthlyRowAllocation = GetMonthlyRowAllocation(rows.Select(rr => rr.AllocationId), fallbackAllocationIds),
      SubmittedAt = report.SubmittedAt,
      HasAttachments = false
    };
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

  private static IOrderedQueryable<ReportRow> ApplyReportRowSort(
    IQueryable<ReportRow> rows, string? sortBy, bool sortDesc)
  {
    return sortBy?.ToLowerInvariant() switch
    {
      "sequence" => sortDesc ? rows.OrderByDescending(r => r.SequenceNumber) : rows.OrderBy(r => r.SequenceNumber),
      "idnumber" => sortDesc ? rows.OrderByDescending(r => r.Report!.User!.IdNumber) : rows.OrderBy(r => r.Report!.User!.IdNumber),
      "code" => sortDesc ? rows.OrderByDescending(r => r.Report!.User!.EmployeeCode) : rows.OrderBy(r => r.Report!.User!.EmployeeCode),
      "name" => sortDesc
        ? rows.OrderByDescending(r => r.Report!.User!.LastName).ThenByDescending(r => r.Report!.User!.FirstName)
        : rows.OrderBy(r => r.Report!.User!.LastName).ThenBy(r => r.Report!.User!.FirstName),
      "month" => sortDesc
        ? rows.OrderByDescending(r => r.Report!.ReportingMonth!.Year).ThenByDescending(r => r.Report!.ReportingMonth!.Month)
        : rows.OrderBy(r => r.Report!.ReportingMonth!.Year).ThenBy(r => r.Report!.ReportingMonth!.Month),
      "status" => sortDesc ? rows.OrderByDescending(r => r.Report!.Status!.Name) : rows.OrderBy(r => r.Report!.Status!.Name),
      "meetingdate" => sortDesc ? rows.OrderByDescending(r => r.MeetingDate) : rows.OrderBy(r => r.MeetingDate),
      "duration" => sortDesc ? rows.OrderByDescending(r => r.MeetingDuration) : rows.OrderBy(r => r.MeetingDuration),
      "district" => sortDesc ? rows.OrderByDescending(r => r.District!.Description) : rows.OrderBy(r => r.District!.Description),
      "locality" => sortDesc ? rows.OrderByDescending(r => r.Locality!.Description) : rows.OrderBy(r => r.Locality!.Description),
      "framework" => sortDesc ? rows.OrderByDescending(r => r.Framework!.Description) : rows.OrderBy(r => r.Framework!.Description),
      "program" => sortDesc ? rows.OrderByDescending(r => r.EducationalProgram!.Description) : rows.OrderBy(r => r.EducationalProgram!.Description),
      _ => rows
        .OrderByDescending(r => r.Report!.ReportingMonth!.Year)
        .ThenByDescending(r => r.Report!.ReportingMonth!.Month)
        .ThenBy(r => r.Report!.User!.LastName)
        .ThenBy(r => r.Report!.User!.FirstName)
        .ThenBy(r => r.SequenceNumber)
    };
  }

  private static IOrderedEnumerable<DashboardReportRow> ApplyDashboardRowSort(
    IEnumerable<DashboardReportRow> rows, string? sortBy, bool sortDesc)
  {
    return sortBy?.ToLowerInvariant() switch
    {
      "code" => sortDesc
        ? rows.OrderByDescending(r => r.EmployeeCode)
        : rows.OrderBy(r => r.EmployeeCode),
      "idnumber" => sortDesc
        ? rows.OrderByDescending(r => r.IdNumber)
        : rows.OrderBy(r => r.IdNumber),
      "name" => sortDesc
        ? rows.OrderByDescending(r => r.FullName)
        : rows.OrderBy(r => r.FullName),
      "status" => sortDesc
        ? rows.OrderByDescending(r => r.StatusName)
        : rows.OrderBy(r => r.StatusName),
      "project" => sortDesc
        ? rows.OrderByDescending(r => r.ProjectName)
        : rows.OrderBy(r => r.ProjectName),
      "submitted" => sortDesc
        ? rows.OrderByDescending(r => r.SubmittedAt)
        : rows.OrderBy(r => r.SubmittedAt),
      "month" => sortDesc
        ? rows.OrderByDescending(r => r.MonthYear).ThenByDescending(r => r.MonthMonth)
        : rows.OrderBy(r => r.MonthYear).ThenBy(r => r.MonthMonth),
      _ => rows
        .OrderByDescending(r => r.MonthYear)
        .ThenByDescending(r => r.MonthMonth)
        .ThenBy(r => r.FullName)
        .ThenBy(r => r.ProjectName)
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

  private static string GetReportStatusText(ReportStatus status)
  {
    return status.Id switch
    {
      1 => "טיוטה",
      2 => "בהזנה",
      3 => "ממתין לאישור",
      4 => "מאושר",
      5 => "הוחזר לתיקון",
      _ => status.Description ?? status.Name
    };
  }
}

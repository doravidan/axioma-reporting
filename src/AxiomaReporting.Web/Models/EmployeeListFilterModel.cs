namespace AxiomaReporting.Web.Models;

/// <summary>
/// Filters, sorting, and paging state for the employee list (EmployeeController.Index).
/// Mirrors <see cref="AllocationListFilterModel"/> for filter parity (client Fix #15).
/// </summary>
public class EmployeeListFilterModel
{
  // Free-text fast search across name, ID, code.
  public string? Search { get; set; }

  // Per-column text filters.
  public string? IdNumber { get; set; }
  public string? EmployeeCode { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? Notes { get; set; }

  // Lookup filters.
  public int? StatusId { get; set; }
  public int? RoleId { get; set; }
  public int? RestDay { get; set; }

  // Tri-state booleans (null = "all").
  public bool? AllowFutureReporting { get; set; }
  public bool? HasAllocations { get; set; }

  // Pure boolean toggles.
  public bool LockedOnly { get; set; }

  // Allocation-derived filters (applied via Allocations.Any(...)).
  public int? ProjectId { get; set; }
  public List<int> DistrictIds { get; set; } = new();
  public List<int> ProgramIds { get; set; } = new();
  public List<int> SectorIds { get; set; } = new();

  // Sort + paging.
  public string? SortBy { get; set; }
  public bool SortDesc { get; set; }
  public int Page { get; set; } = 1;
  public int PageSize { get; set; } = 25;

  /// <summary>
  /// Trims whitespace, collapses empty strings to null, and enforces sane page-size bounds.
  /// Call once at the top of the controller action before applying the filter.
  /// </summary>
  public void Normalize()
  {
    Search = string.IsNullOrWhiteSpace(Search) ? null : Search.Trim();
    IdNumber = string.IsNullOrWhiteSpace(IdNumber) ? null : IdNumber.Trim();
    EmployeeCode = string.IsNullOrWhiteSpace(EmployeeCode) ? null : EmployeeCode.Trim();
    FirstName = string.IsNullOrWhiteSpace(FirstName) ? null : FirstName.Trim();
    LastName = string.IsNullOrWhiteSpace(LastName) ? null : LastName.Trim();
    Notes = string.IsNullOrWhiteSpace(Notes) ? null : Notes.Trim();

    DistrictIds = DistrictIds?.Where(x => x > 0).Distinct().ToList() ?? new();
    ProgramIds = ProgramIds?.Where(x => x > 0).Distinct().ToList() ?? new();
    SectorIds = SectorIds?.Where(x => x > 0).Distinct().ToList() ?? new();

    if (Page < 1) Page = 1;
    if (PageSize is < 1 or > 500) PageSize = 25;
  }

  /// <summary>
  /// Build a route-values dictionary suitable for <c>RedirectToAction</c> that preserves the
  /// active filter querystring across POST → Redirect → GET (client Fix #14).
  /// </summary>
  public Dictionary<string, object?> ToRouteValues()
  {
    var rv = new Dictionary<string, object?>();
    if (!string.IsNullOrEmpty(Search)) rv["search"] = Search;
    if (!string.IsNullOrEmpty(IdNumber)) rv["idNumber"] = IdNumber;
    if (!string.IsNullOrEmpty(EmployeeCode)) rv["employeeCode"] = EmployeeCode;
    if (!string.IsNullOrEmpty(FirstName)) rv["firstName"] = FirstName;
    if (!string.IsNullOrEmpty(LastName)) rv["lastName"] = LastName;
    if (!string.IsNullOrEmpty(Notes)) rv["notes"] = Notes;
    if (StatusId.HasValue) rv["statusId"] = StatusId;
    if (RoleId.HasValue) rv["roleId"] = RoleId;
    if (RestDay.HasValue) rv["restDay"] = RestDay;
    if (AllowFutureReporting.HasValue) rv["allowFutureReporting"] = AllowFutureReporting;
    if (HasAllocations.HasValue) rv["hasAllocations"] = HasAllocations;
    if (LockedOnly) rv["lockedOnly"] = true;
    if (ProjectId.HasValue) rv["projectId"] = ProjectId;
    if (!string.IsNullOrEmpty(SortBy)) rv["sortBy"] = SortBy;
    if (SortDesc) rv["sortDesc"] = true;
    if (Page > 1) rv["page"] = Page;
    if (PageSize != 25) rv["pageSize"] = PageSize;

    var i = 0;
    foreach (var id in DistrictIds) rv[$"districtIds[{i++}]"] = id;
    i = 0;
    foreach (var id in ProgramIds) rv[$"programIds[{i++}]"] = id;
    i = 0;
    foreach (var id in SectorIds) rv[$"sectorIds[{i++}]"] = id;
    return rv;
  }
}

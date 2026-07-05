namespace AxiomaReporting.Web.Models;

/// <summary>
/// Filters, sorting, and paging state for the allocation dashboard (EmployeeController.AllocationList).
/// Backed by querystring; applied by EmployeeController.AllocationListQuery.
/// </summary>
public class AllocationListFilterModel
{
  public string? Search { get; set; }

  public int? EmployeeId { get; set; }
  public int? ProjectId { get; set; }
  public List<int> ProgramIds { get; set; } = new();
  public List<int> DistrictIds { get; set; } = new();
  public List<int> SectorIds { get; set; } = new();

  public string? IdNumber { get; set; }
  public string? EmployeeCode { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }

  public decimal? MonthlyEmploymentScope { get; set; }
  public decimal? AnnualEmploymentScope { get; set; }

  /// <summary>Raw string values (e.g. "0.5", "1", "Unlimited") — matched against Allocation.OutputDuration as substring.</summary>
  public List<string> OutputDurations { get; set; } = new();

  public string? Notes { get; set; }

  /// <summary>When true, multi-value columns render ALL associated lookup values. When false (default), they render only the intersection with the user's filter selection (or all if no filter is set).</summary>
  public bool ShowAll { get; set; }

  public string? SortBy { get; set; }
  public bool SortDesc { get; set; }

  public int Page { get; set; } = 1;
  public int PageSize { get; set; } = 25;

  public void Normalize()
  {
    Search = string.IsNullOrWhiteSpace(Search) ? null : Search.Trim();
    IdNumber = string.IsNullOrWhiteSpace(IdNumber) ? null : IdNumber.Trim();
    EmployeeCode = string.IsNullOrWhiteSpace(EmployeeCode) ? null : EmployeeCode.Trim();
    FirstName = string.IsNullOrWhiteSpace(FirstName) ? null : FirstName.Trim();
    LastName = string.IsNullOrWhiteSpace(LastName) ? null : LastName.Trim();
    Notes = string.IsNullOrWhiteSpace(Notes) ? null : Notes.Trim();

    if (EmployeeId <= 0) EmployeeId = null;

    ProgramIds = ProgramIds?.Where(x => x > 0).Distinct().ToList() ?? new();
    DistrictIds = DistrictIds?.Where(x => x > 0).Distinct().ToList() ?? new();
    SectorIds = SectorIds?.Where(x => x > 0).Distinct().ToList() ?? new();
    OutputDurations = (OutputDurations?.Where(s => !string.IsNullOrWhiteSpace(s)))
      .Select(s => s.Trim())
      .Distinct()
      .ToList() ?? new();

    if (MonthlyEmploymentScope.HasValue)
      MonthlyEmploymentScope = decimal.Truncate(MonthlyEmploymentScope.Value);
    if (AnnualEmploymentScope.HasValue)
      AnnualEmploymentScope = decimal.Truncate(AnnualEmploymentScope.Value);

    if (Page < 1) Page = 1;
    if (PageSize is < 1 or > 500) PageSize = 25;
  }
}

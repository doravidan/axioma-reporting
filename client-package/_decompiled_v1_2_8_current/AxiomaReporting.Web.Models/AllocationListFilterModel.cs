using System.Collections.Generic;
using System.Linq;

namespace AxiomaReporting.Web.Models;

public class AllocationListFilterModel
{
	public string? Search { get; set; }

	public int? EmployeeId { get; set; }

	public int? ProjectId { get; set; }

	public List<int> ProgramIds { get; set; } = new List<int>();


	public List<int> DistrictIds { get; set; } = new List<int>();


	public List<int> SectorIds { get; set; } = new List<int>();


	public string? IdNumber { get; set; }

	public string? EmployeeCode { get; set; }

	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public decimal? MonthlyEmploymentScope { get; set; }

	public decimal? AnnualEmploymentScope { get; set; }

	public List<string> OutputDurations { get; set; } = new List<string>();


	public string? Notes { get; set; }

	public bool ShowAll { get; set; }

	public string? SortBy { get; set; }

	public bool SortDesc { get; set; }

	public int Page { get; set; } = 1;


	public int PageSize { get; set; } = 25;


	public void Normalize()
	{
		Search = (string.IsNullOrWhiteSpace(Search) ? null : Search.Trim());
		IdNumber = (string.IsNullOrWhiteSpace(IdNumber) ? null : IdNumber.Trim());
		EmployeeCode = (string.IsNullOrWhiteSpace(EmployeeCode) ? null : EmployeeCode.Trim());
		FirstName = (string.IsNullOrWhiteSpace(FirstName) ? null : FirstName.Trim());
		LastName = (string.IsNullOrWhiteSpace(LastName) ? null : LastName.Trim());
		Notes = (string.IsNullOrWhiteSpace(Notes) ? null : Notes.Trim());
		if (EmployeeId <= 0)
		{
			EmployeeId = null;
		}
		ProgramIds = ProgramIds?.Where((int x) => x > 0).Distinct().ToList() ?? new List<int>();
		DistrictIds = DistrictIds?.Where((int x) => x > 0).Distinct().ToList() ?? new List<int>();
		SectorIds = SectorIds?.Where((int x) => x > 0).Distinct().ToList() ?? new List<int>();
		OutputDurations = (from s in OutputDurations?.Where((string s) => !string.IsNullOrWhiteSpace(s))
			select s.Trim()).Distinct().ToList() ?? new List<string>();
		if (MonthlyEmploymentScope.HasValue)
		{
			MonthlyEmploymentScope = decimal.Truncate(MonthlyEmploymentScope.Value);
		}
		if (AnnualEmploymentScope.HasValue)
		{
			AnnualEmploymentScope = decimal.Truncate(AnnualEmploymentScope.Value);
		}
		if (Page < 1)
		{
			Page = 1;
		}
		int pageSize = PageSize;
		if ((pageSize < 1 || pageSize > 500) ? true : false)
		{
			PageSize = 25;
		}
	}
}

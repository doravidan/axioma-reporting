using System.Collections.Generic;
using System.Linq;

namespace AxiomaReporting.Web.Models;

public class EmployeeListFilterModel
{
	public string? Search { get; set; }

	public string? IdNumber { get; set; }

	public string? EmployeeCode { get; set; }

	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public string? Notes { get; set; }

	public int? StatusId { get; set; }

	public int? RoleId { get; set; }

	public int? RestDay { get; set; }

	public bool? AllowFutureReporting { get; set; }

	public bool? HasAllocations { get; set; }

	public bool LockedOnly { get; set; }

	public int? ProjectId { get; set; }

	public List<int> DistrictIds { get; set; } = new List<int>();


	public List<int> ProgramIds { get; set; } = new List<int>();


	public List<int> SectorIds { get; set; } = new List<int>();


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
		DistrictIds = DistrictIds?.Where((int x) => x > 0).Distinct().ToList() ?? new List<int>();
		ProgramIds = ProgramIds?.Where((int x) => x > 0).Distinct().ToList() ?? new List<int>();
		SectorIds = SectorIds?.Where((int x) => x > 0).Distinct().ToList() ?? new List<int>();
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

	public Dictionary<string, object?> ToRouteValues()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		if (!string.IsNullOrEmpty(Search))
		{
			dictionary["search"] = Search;
		}
		if (!string.IsNullOrEmpty(IdNumber))
		{
			dictionary["idNumber"] = IdNumber;
		}
		if (!string.IsNullOrEmpty(EmployeeCode))
		{
			dictionary["employeeCode"] = EmployeeCode;
		}
		if (!string.IsNullOrEmpty(FirstName))
		{
			dictionary["firstName"] = FirstName;
		}
		if (!string.IsNullOrEmpty(LastName))
		{
			dictionary["lastName"] = LastName;
		}
		if (!string.IsNullOrEmpty(Notes))
		{
			dictionary["notes"] = Notes;
		}
		if (StatusId.HasValue)
		{
			dictionary["statusId"] = StatusId;
		}
		if (RoleId.HasValue)
		{
			dictionary["roleId"] = RoleId;
		}
		if (RestDay.HasValue)
		{
			dictionary["restDay"] = RestDay;
		}
		if (AllowFutureReporting.HasValue)
		{
			dictionary["allowFutureReporting"] = AllowFutureReporting;
		}
		if (HasAllocations.HasValue)
		{
			dictionary["hasAllocations"] = HasAllocations;
		}
		if (LockedOnly)
		{
			dictionary["lockedOnly"] = true;
		}
		if (ProjectId.HasValue)
		{
			dictionary["projectId"] = ProjectId;
		}
		if (!string.IsNullOrEmpty(SortBy))
		{
			dictionary["sortBy"] = SortBy;
		}
		if (SortDesc)
		{
			dictionary["sortDesc"] = true;
		}
		if (Page > 1)
		{
			dictionary["page"] = Page;
		}
		if (PageSize != 25)
		{
			dictionary["pageSize"] = PageSize;
		}
		int num = 0;
		foreach (int districtId in DistrictIds)
		{
			dictionary[$"districtIds[{num++}]"] = districtId;
		}
		num = 0;
		foreach (int programId in ProgramIds)
		{
			dictionary[$"programIds[{num++}]"] = programId;
		}
		num = 0;
		foreach (int sectorId in SectorIds)
		{
			dictionary[$"sectorIds[{num++}]"] = sectorId;
		}
		return dictionary;
	}
}

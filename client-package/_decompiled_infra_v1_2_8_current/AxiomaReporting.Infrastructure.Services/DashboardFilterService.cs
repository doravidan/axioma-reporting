using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class DashboardFilterService : IDashboardFilterService
{
	private readonly AppDbContext _db;

	public DashboardFilterService(AppDbContext db)
	{
		_db = db;
	}

	public async Task<(List<DashboardReportRow> Rows, int TotalCount)> GetReportsAsync(DashboardFilter filter, int currentUserId, UserRoleEnum currentUserRole)
	{
		DashboardFilter filter2 = filter;
		filter2.Page = Math.Max(filter2.Page, 1);
		DashboardFilter dashboardFilter = filter2;
		int pageSize = filter2.PageSize;
		bool flag = ((pageSize < 1 || pageSize > 10000) ? true : false);
		dashboardFilter.PageSize = (flag ? 25 : filter2.PageSize);
		List<int> scopedUserIds = await GetScopedUserIdsAsync(currentUserId, currentUserRole);
		List<int> allocationIds = await GetMatchingAllocationIdsAsync(filter2, scopedUserIds);
		List<int> employeeIds = await (from a in _db.Allocations
			where allocationIds.Contains(a.Id)
			select a.UserId).Distinct().ToListAsync();
		IQueryable<User> source = _db.Users.Where((User u) => u.IsReportingEmployee && u.StatusId == 1 && employeeIds.Contains(u.Id));
		if (!string.IsNullOrWhiteSpace(filter2.EmployeeCode))
		{
			source = source.Where((User u) => u.EmployeeCode.Contains(filter2.EmployeeCode));
		}
		if (!string.IsNullOrWhiteSpace(filter2.IdNumber))
		{
			source = ApplyUserIdNumberFilter(source, filter2.IdNumber);
		}
		if (!string.IsNullOrWhiteSpace(filter2.EmployeeName))
		{
			source = source.Where((User u) => string.Concat(u.FirstName + " ", u.LastName).Contains(filter2.EmployeeName));
		}
		employeeIds = await source.Select((User u) => u.Id).ToListAsync();
		if (filter2.StatusId == 0)
		{
			return await GetMissingReportsAsync(filter2, allocationIds, employeeIds);
		}
		IQueryable<Report> reports = from r in _db.Reports.Include((Report r) => r.User).Include((Report r) => r.Status).Include((Report r) => r.ReportingMonth)
			where employeeIds.Contains(r.UserId)
			select r;
		reports = ApplyArchiveFilter(reports, filter2, currentUserRole);
		reports = await ApplyMonthRangeAsync(reports, filter2);
		if (filter2.StatusId.HasValue)
		{
			reports = reports.Where((Report r) => r.StatusId == filter2.StatusId.Value);
		}
		if (HasAllocationDimensionFilter(filter2))
		{
			reports = reports.Where((Report r) => r.ReportRows.Any((ReportRow rr) => rr.AllocationId.HasValue && allocationIds.Contains(rr.AllocationId.Value)));
		}
		List<DashboardReportRow> rows2 = (await reports.Include((Report r) => r.ReportRows).ThenInclude((ReportRow rr) => rr.Allocation).ThenInclude((Allocation a) => a.Project)
			.ToListAsync()).SelectMany((Report report) => ToDashboardRows(report, filter2, allocationIds)).ToList();
		int total = rows2.Count;
		rows2 = ApplyDashboardRowSort(rows2, filter2.SortBy, filter2.SortDesc).Skip((filter2.Page - 1) * filter2.PageSize).Take(filter2.PageSize).ToList();
		await PopulateAttachmentFlagsAsync(rows2);
		return (Rows: rows2, TotalCount: total);
	}

	public async Task<bool> CanAccessReportAsync(int reportId, int currentUserId, UserRoleEnum currentUserRole)
	{
		Report report = await _db.Reports.FindAsync(reportId);
		if (report == null)
		{
			return false;
		}
		if (currentUserRole == UserRoleEnum.Employee)
		{
			if (report.IsArchived)
			{
				return false;
			}
			return report.UserId == currentUserId;
		}
		if (report.IsArchived && !CanIncludeArchived(currentUserRole))
		{
			return false;
		}
		if ((uint)(currentUserRole - 1) <= 2u)
		{
			return true;
		}
		return (await GetScopedUserIdsAsync(currentUserId, currentUserRole)).Contains(report.UserId);
	}

	public async Task<(List<DashboardReportDetailRow> Rows, int TotalCount)> GetReportRowsAsync(DashboardFilter filter, int currentUserId, UserRoleEnum currentUserRole)
	{
		DashboardFilter filter2 = filter;
		filter2.Page = Math.Max(filter2.Page, 1);
		DashboardFilter dashboardFilter = filter2;
		int pageSize = filter2.PageSize;
		bool flag = ((pageSize < 1 || pageSize > 10000) ? true : false);
		dashboardFilter.PageSize = (flag ? 25 : filter2.PageSize);
		List<int> scopedUserIds = await GetScopedUserIdsAsync(currentUserId, currentUserRole);
		List<int> allocationIds = await GetMatchingAllocationIdsAsync(filter2, scopedUserIds);
		IQueryable<User> source = _db.Users.Where((User u) => scopedUserIds.Contains(u.Id) && u.IsReportingEmployee && u.StatusId == 1);
		if (!string.IsNullOrWhiteSpace(filter2.EmployeeCode))
		{
			source = source.Where((User u) => u.EmployeeCode.Contains(filter2.EmployeeCode));
		}
		if (!string.IsNullOrWhiteSpace(filter2.IdNumber))
		{
			source = ApplyUserIdNumberFilter(source, filter2.IdNumber);
		}
		if (!string.IsNullOrWhiteSpace(filter2.EmployeeName))
		{
			source = source.Where((User u) => string.Concat(u.FirstName + " ", u.LastName).Contains(filter2.EmployeeName));
		}
		List<int> activeEmployeeIds = await source.Select((User u) => u.Id).ToListAsync();
		if (filter2.StatusId == 0)
		{
			return await GetMissingReportDetailRowsAsync(filter2, allocationIds, activeEmployeeIds);
		}
		IQueryable<Report> reports = from r in _db.Reports.Include((Report r) => r.User).Include((Report r) => r.ReportingMonth)
			where activeEmployeeIds.Contains(r.UserId)
			select r;
		reports = ApplyArchiveFilter(reports, filter2, currentUserRole);
		reports = await ApplyMonthRangeAsync(reports, filter2);
		if (filter2.StatusId.HasValue)
		{
			reports = reports.Where((Report r) => r.StatusId == filter2.StatusId.Value);
		}
		List<int> matchingReportIds = await reports.Select((Report r) => r.Id).ToListAsync();
		IQueryable<ReportRow> rows = from rr in _db.ReportRows.Include((ReportRow rr) => rr.Report).ThenInclude((Report r) => r.User).Include((ReportRow rr) => rr.Report)
				.ThenInclude((Report r) => r.Status)
				.Include((ReportRow rr) => rr.Report)
				.ThenInclude((Report r) => r.ReportingMonth)
				.Include((ReportRow rr) => rr.Allocation)
				.ThenInclude((Allocation a) => a.Project)
				.Include((ReportRow rr) => rr.District)
				.Include((ReportRow rr) => rr.Locality)
				.Include((ReportRow rr) => rr.Framework)
				.Include((ReportRow rr) => rr.EducationalProgram)
				.Include((ReportRow rr) => rr.Domain)
				.Include((ReportRow rr) => rr.Subject1)
				.Include((ReportRow rr) => rr.Subject2)
				.Include((ReportRow rr) => rr.DiscussionCode)
				.Include((ReportRow rr) => rr.Class)
				.Include((ReportRow rr) => rr.GradeLevel)
				.Include((ReportRow rr) => rr.ConclusionClass)
				.Include((ReportRow rr) => rr.ConclusionFramework)
				.Include((ReportRow rr) => rr.ConclusionLocation)
				.Include((ReportRow rr) => rr.ReportType)
			where matchingReportIds.Contains(rr.ReportId)
			select rr;
		if (HasAllocationDimensionFilter(filter2))
		{
			rows = rows.Where((ReportRow rr) => rr.AllocationId.HasValue && allocationIds.Contains(rr.AllocationId.Value));
		}
		if (filter2.DistrictId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.DistrictId == filter2.DistrictId.Value);
		}
		if (filter2.LocalityId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.LocalityId == filter2.LocalityId.Value);
		}
		if (filter2.FrameworkId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.FrameworkId == filter2.FrameworkId.Value);
		}
		if (filter2.EducationalProgramId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.EducationalProgramId == filter2.EducationalProgramId.Value);
		}
		if (filter2.DomainId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.DomainId == filter2.DomainId.Value);
		}
		if (filter2.Subject1Id.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.Subject1Id == filter2.Subject1Id.Value);
		}
		if (filter2.Subject2Id.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.Subject2Id == (int?)filter2.Subject2Id.Value);
		}
		if (filter2.DiscussionCodeId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.DiscussionCodeId == (int?)filter2.DiscussionCodeId.Value);
		}
		if (filter2.ClassId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.ClassId == (int?)filter2.ClassId.Value);
		}
		if (filter2.GradeLevelId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.GradeLevelId == (int?)filter2.GradeLevelId.Value);
		}
		if (filter2.ConclusionClassId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.ConclusionClassId == (int?)filter2.ConclusionClassId.Value);
		}
		if (filter2.ConclusionFrameworkId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.ConclusionFrameworkId == (int?)filter2.ConclusionFrameworkId.Value);
		}
		if (filter2.ConclusionLocationId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.ConclusionLocationId == (int?)filter2.ConclusionLocationId.Value);
		}
		if (filter2.ReportTypeId.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.ReportTypeId == (int?)filter2.ReportTypeId.Value);
		}
		if (filter2.MeetingDateFrom.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.MeetingDate >= filter2.MeetingDateFrom.Value.Date);
		}
		if (filter2.MeetingDateTo.HasValue)
		{
			DateTime toDate = filter2.MeetingDateTo.Value.Date.AddDays(1.0);
			rows = rows.Where((ReportRow rr) => rr.MeetingDate < toDate);
		}
		if (filter2.MeetingDuration.HasValue)
		{
			rows = rows.Where((ReportRow rr) => rr.MeetingDuration == filter2.MeetingDuration.Value);
		}
		int totalCount = await rows.CountAsync();
		List<ReportRow> pageRows = await ApplyReportRowSort(rows, filter2.SortBy, filter2.SortDesc).Skip((filter2.Page - 1) * filter2.PageSize).Take(filter2.PageSize).ToListAsync();
		List<int> pageUserIds = pageRows.Select((ReportRow rr) => rr.Report.UserId).Distinct().ToList();
		Dictionary<int, int> fallbackAllocationIds = (await (from a in _db.Allocations
				where a.IsActive && pageUserIds.Contains(a.UserId)
				orderby a.Id
				select new { a.UserId, a.Id }).ToListAsync()).GroupBy(a => a.UserId).ToDictionary(g => g.Key, g => g.First().Id);
		List<int> pageReportIds = pageRows.Select((ReportRow rr) => rr.ReportId).Distinct().ToList();
		List<int> pageRowIds = pageRows.Select((ReportRow rr) => rr.Id).Distinct().ToList();
		List<DocumentAttachment> attachmentRows = await (from da in _db.DocumentAttachments
			where (da.ReportId.HasValue && pageReportIds.Contains(da.ReportId.Value)) || (da.ReportRowId.HasValue && pageRowIds.Contains(da.ReportRowId.Value))
			select da).ToListAsync();
		List<DashboardReportDetailRow> detailRows = pageRows.Select(delegate(ReportRow rr)
		{
			int? allocationId = rr.AllocationId;
			if (!allocationId.HasValue && fallbackAllocationIds.TryGetValue(rr.Report.UserId, out var fallbackAllocationId))
			{
				allocationId = fallbackAllocationId;
			}
			return new DashboardReportDetailRow
			{
				ReportId = rr.ReportId,
				ReportRowId = rr.Id,
				UserId = rr.Report.UserId,
				AllocationId = allocationId,
				CanEdit = ((int)currentUserRole == 1),
				SequenceNumber = rr.SequenceNumber,
				IdNumber = rr.Report.User.IdNumber,
				EmployeeCode = rr.Report.User.EmployeeCode,
				FullName = string.Concat(rr.Report.User.FirstName + " ", rr.Report.User.LastName),
				StatusName = GetReportStatusText(rr.Report.Status),
				StatusId = rr.Report.StatusId,
				MonthDescription = rr.Report.ReportingMonth.Description,
				MonthYear = rr.Report.ReportingMonth.Year,
				MonthMonth = rr.Report.ReportingMonth.Month,
				ProjectName = ((rr.Allocation != null && rr.Allocation.Project != null) ? rr.Allocation.Project.Description : string.Empty),
				DistrictName = ((rr.District != null) ? rr.District.Description : string.Empty),
				LocalityName = ((rr.Locality != null) ? rr.Locality.Description : string.Empty),
				FrameworkName = ((rr.Framework != null) ? rr.Framework.Description : string.Empty),
				EducationalProgramName = ((rr.EducationalProgram != null) ? rr.EducationalProgram.Description : string.Empty),
				DomainName = ((rr.Domain != null) ? rr.Domain.Description : string.Empty),
				Subject1Name = ((rr.Subject1 != null) ? rr.Subject1.Description : string.Empty),
				Subject2Name = ((rr.Subject2 != null) ? rr.Subject2.Description : string.Empty),
				DiscussionCodeName = ((rr.DiscussionCode != null) ? rr.DiscussionCode.Description : string.Empty),
				ClassName = ((rr.Class != null) ? rr.Class.Description : string.Empty),
				GradeLevelName = ((rr.GradeLevel != null) ? rr.GradeLevel.Description : string.Empty),
				ConclusionClassName = ((rr.ConclusionClass != null) ? rr.ConclusionClass.Description : string.Empty),
				ConclusionFrameworkName = ((rr.ConclusionFramework != null) ? rr.ConclusionFramework.Description : string.Empty),
				ConclusionLocationName = ((rr.ConclusionLocation != null) ? rr.ConclusionLocation.Description : string.Empty),
				ReportTypeName = ((rr.ReportType != null) ? rr.ReportType.Description : string.Empty),
				MeetingDate = rr.MeetingDate,
				MeetingDuration = rr.MeetingDuration,
				Notes = (rr.Notes ?? string.Empty),
				HasAttachments = attachmentRows.Any((DocumentAttachment da) => da.ReportId == (int?)rr.ReportId || da.ReportRowId == (int?)rr.Id)
			};
		}).ToList();
		return (Rows: detailRows, TotalCount: totalCount);
	}

	private async Task<(List<DashboardReportDetailRow> Rows, int TotalCount)> GetMissingReportDetailRowsAsync(DashboardFilter filter, List<int> allocationIds, List<int> employeeIds)
	{
		List<int> allocationIds2 = allocationIds;
		List<int> employeeIds2 = employeeIds;
		ReportingMonth month = await ResolveMissingReportMonthAsync(filter);
		if (month == null)
		{
			return (Rows: new List<DashboardReportDetailRow>(), TotalCount: 0);
		}
		List<int> reportedUserIds = await (from r in _db.Reports
			where r.ReportingMonthId == month.Id && (filter.IncludeArchived || !r.IsArchived)
			select r.UserId).Distinct().ToListAsync();
		IQueryable<Allocation> query = from a in _db.Allocations.Include((Allocation a) => a.User).Include((Allocation a) => a.Project)
			where a.IsActive && allocationIds2.Contains(a.Id) && employeeIds2.Contains(a.UserId) && !reportedUserIds.Contains(a.UserId)
			select a;
		return new ValueTuple<List<DashboardReportDetailRow>, int>(item2: await query.CountAsync(), item1: (await (from a in query
			orderby a.User.LastName, a.User.FirstName, a.Project.Description
			select a).Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync()).Select((Allocation a) => new DashboardReportDetailRow
		{
			ReportId = 0,
			ReportRowId = 0,
			UserId = a.UserId,
			AllocationId = a.Id,
			CanEdit = false,
			SequenceNumber = 0,
			IdNumber = (a.User?.IdNumber ?? string.Empty),
			EmployeeCode = (a.User?.EmployeeCode ?? string.Empty),
			FullName = (a.User?.FirstName + " " + a.User?.LastName).Trim(),
			StatusName = "טרם דווח",
			StatusId = 0,
			MonthDescription = month.Description,
			MonthYear = month.Year,
			MonthMonth = month.Month,
			ProjectName = (a.Project?.Description ?? string.Empty),
			MeetingDate = DateTime.MinValue,
			MeetingDuration = 0m
		}).ToList());
	}

	public async Task<FilterOptionsDto> GetCompatibleOptionsAsync(DashboardFilter currentSelection, int currentUserId, UserRoleEnum currentUserRole)
	{
		DashboardFilter currentSelection2 = currentSelection;
		if (currentSelection2 == null)
		{
			currentSelection2 = new DashboardFilter();
		}
		List<int> scopedUserIds = await GetScopedUserIdsAsync(currentUserId, currentUserRole);
		IQueryable<Report> reportBase = from r in _db.Reports.Include((Report r) => r.ReportingMonth).Include((Report r) => r.User)
			where scopedUserIds.Contains(r.UserId)
			select r;
		reportBase = ApplyArchiveFilter(reportBase, currentSelection2, currentUserRole);
		List<int> districtReportUserIds = await (await ApplyFiltersExceptAsync("districts")).Select((Report r) => r.UserId).Distinct().ToListAsync();
		List<int> districtAllocIds = await AllocationIdsForAsync(null, currentSelection2.SectorId, currentSelection2.ProgramId);
		List<int> compatibleDistrictIds = await (from ad in _db.Set<AllocationDistrict>()
			where districtAllocIds.Contains(ad.AllocationId) && _db.Allocations.Any((Allocation a) => a.Id == ad.AllocationId && districtReportUserIds.Contains(a.UserId))
			select ad.DistrictId).Distinct().ToListAsync();
		List<IdName> districts = await (from d in _db.Districts
			where d.IsActive && compatibleDistrictIds.Contains(d.Id)
			orderby d.Description
			select new IdName
			{
				Id = d.Id,
				Name = d.Description
			}).ToListAsync();
		List<int> sectorReportUserIds = await (await ApplyFiltersExceptAsync("sectors")).Select((Report r) => r.UserId).Distinct().ToListAsync();
		List<int> sectorAllocIds = await AllocationIdsForAsync(currentSelection2.DistrictId, null, currentSelection2.ProgramId);
		List<int> compatibleSectorIds = await (from sx in _db.Set<AllocationSector>()
			where sectorAllocIds.Contains(sx.AllocationId) && _db.Allocations.Any((Allocation a) => a.Id == sx.AllocationId && sectorReportUserIds.Contains(a.UserId))
			select sx.SectorId).Distinct().ToListAsync();
		List<IdName> sectors = await (from s in _db.Sectors
			where s.IsActive && compatibleSectorIds.Contains(s.Id)
			orderby s.Description
			select new IdName
			{
				Id = s.Id,
				Name = s.Description
			}).ToListAsync();
		List<int> programReportUserIds = await (await ApplyFiltersExceptAsync("programs")).Select((Report r) => r.UserId).Distinct().ToListAsync();
		List<int> programAllocIds = await AllocationIdsForAsync(currentSelection2.DistrictId, currentSelection2.SectorId, null);
		List<int> compatibleProgramIds = await (from px in _db.Set<AllocationProgram>()
			where programAllocIds.Contains(px.AllocationId) && _db.Allocations.Any((Allocation a) => a.Id == px.AllocationId && programReportUserIds.Contains(a.UserId))
			select px.ProgramId).Distinct().ToListAsync();
		List<IdName> programs = await (from p in _db.Programs
			where p.IsActive && compatibleProgramIds.Contains(p.Id)
			orderby p.Description
			select new IdName
			{
				Id = p.Id,
				Name = p.Description
			}).ToListAsync();
		List<int> empIds = await (await ApplyFiltersExceptAsync("employees")).Select((Report r) => r.UserId).Distinct().ToListAsync();
		List<IdName> employees = await (from u in _db.Users
			where empIds.Contains(u.Id)
			orderby u.LastName, u.FirstName
			select new IdName
			{
				Id = u.Id,
				Name = string.Concat(string.Concat(string.Concat(string.Concat(u.FirstName + " ", u.LastName), " ("), u.EmployeeCode), ")")
			}).ToListAsync();
		List<int> projReportUserIds = await (await ApplyFiltersExceptAsync("projects")).Select((Report r) => r.UserId).Distinct().ToListAsync();
		List<int> projAllocIds = await AllocationIdsForAsync(currentSelection2.DistrictId, currentSelection2.SectorId, currentSelection2.ProgramId);
		List<int> compatibleProjectIds = await (from a in _db.Allocations
			where projAllocIds.Contains(a.Id) && projReportUserIds.Contains(a.UserId)
			select a.ProjectId).Distinct().ToListAsync();
		List<IdName> projects = await (from p in _db.Projects
			where p.IsActive && compatibleProjectIds.Contains(p.Id)
			orderby p.Description
			select new IdName
			{
				Id = p.Id,
				Name = p.Description
			}).ToListAsync();
		List<int> compatibleMonthIds = await (await ApplyFiltersExceptAsync("months")).Select((Report r) => r.ReportingMonthId).Distinct().ToListAsync();
		List<IdName> months = await (from m in _db.ReportingMonths
			where compatibleMonthIds.Contains(m.Id)
			orderby m.Year descending, m.Month descending
			select new IdName
			{
				Id = m.Id,
				Name = m.Description
			}).ToListAsync();
		List<int> compatibleStatusIds = await (await ApplyFiltersExceptAsync("status")).Select((Report r) => r.StatusId).Distinct().ToListAsync();
		List<IdName> statuses = (await (from s in _db.ReportStatuses
			where compatibleStatusIds.Contains(s.Id)
			orderby s.Id
			select s).ToListAsync()).Select((ReportStatus s) => new IdName
		{
			Id = s.Id,
			Name = GetReportStatusText(s)
		}).ToList();
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
		async Task<List<int>> AllocationIdsForAsync(int? districtId, int? sectorId, int? programId)
		{
			IQueryable<Allocation> source = _db.Allocations.Where((Allocation a) => a.IsActive && scopedUserIds.Contains(a.UserId));
			if (districtId.HasValue)
			{
				int dId = districtId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationDistrict>().Any((AllocationDistrict ad) => ad.AllocationId == a.Id && ad.DistrictId == dId));
			}
			if (sectorId.HasValue)
			{
				int sId = sectorId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationSector>().Any((AllocationSector s) => s.AllocationId == a.Id && s.SectorId == sId));
			}
			if (programId.HasValue)
			{
				int pId = programId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationProgram>().Any((AllocationProgram p) => p.AllocationId == a.Id && p.ProgramId == pId));
			}
			return await source.Select((Allocation a) => a.Id).ToListAsync();
		}
		async Task<IQueryable<Report>> ApplyFiltersExceptAsync(string excludeDimension)
		{
			IQueryable<Report> q = reportBase;
			if (excludeDimension != "months")
			{
				q = await ApplyMonthRangeAsync(q, currentSelection2);
			}
			if (excludeDimension != "status" && currentSelection2.StatusId.HasValue && currentSelection2.StatusId.Value != 0)
			{
				q = q.Where((Report r) => r.StatusId == currentSelection2.StatusId.Value);
			}
			if (excludeDimension != "employees")
			{
				if (!string.IsNullOrWhiteSpace(currentSelection2.EmployeeCode))
				{
					q = q.Where((Report r) => r.User.EmployeeCode.Contains(currentSelection2.EmployeeCode));
				}
				if (!string.IsNullOrWhiteSpace(currentSelection2.IdNumber))
				{
					q = ApplyReportIdNumberFilter(q, currentSelection2.IdNumber);
				}
				if (!string.IsNullOrWhiteSpace(currentSelection2.EmployeeName))
				{
					q = q.Where((Report r) => string.Concat(r.User.FirstName + " ", r.User.LastName).Contains(currentSelection2.EmployeeName));
				}
			}
			int? districtId2 = ((excludeDimension == "districts") ? null : currentSelection2.DistrictId);
			int? sectorId2 = ((excludeDimension == "sectors") ? null : currentSelection2.SectorId);
			int? programId2 = ((excludeDimension == "programs") ? null : currentSelection2.ProgramId);
			if (districtId2.HasValue || sectorId2.HasValue || programId2.HasValue)
			{
				List<int> allocIds = await AllocationIdsForAsync(districtId2, sectorId2, programId2);
				q = q.Where((Report r) => r.ReportRows.Any((ReportRow rr) => rr.AllocationId.HasValue && allocIds.Contains(rr.AllocationId.Value)));
			}
			return q;
		}
	}

	private async Task<(List<DashboardReportRow> Rows, int TotalCount)> GetMissingReportsAsync(DashboardFilter filter, List<int> allocationIds, List<int> employeeIds)
	{
		List<int> employeeIds2 = employeeIds;
		List<int> allocationIds2 = allocationIds;
		ReportingMonth month = await ResolveMissingReportMonthAsync(filter);
		if (month == null)
		{
			return (Rows: new List<DashboardReportRow>(), TotalCount: 0);
		}
		IQueryable<User> employees = _db.Users.Where((User u) => employeeIds2.Contains(u.Id) && !_db.Reports.Any((Report r) => r.UserId == u.Id && r.ReportingMonthId == month.Id && r.StatusId != 1 && r.StatusId != 2 && (filter.IncludeArchived || !r.IsArchived)));
		int total = await employees.CountAsync();
		List<User> page = await ApplyMissingReportSort(employees, filter.SortBy, filter.SortDesc).Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		Dictionary<int, int?> allocationLimits = (from a in await (from a in _db.Allocations
				where allocationIds2.Contains(a.Id)
				select new { a.UserId, a.MonthlyRowAllocation }).ToListAsync()
			group a by a.UserId).ToDictionary(g => g.Key, g => g.Min(a => a.MonthlyRowAllocation));
		int? value;
		return (Rows: page.Select((User u) => new DashboardReportRow
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
			MonthlyRowAllocation = (allocationLimits.TryGetValue(u.Id, out value) ? value : null)
		}).ToList(), TotalCount: total);
	}

	private async Task<IQueryable<Report>> ApplyMonthRangeAsync(IQueryable<Report> reports, DashboardFilter filter)
	{
		if (filter.FromMonthId.HasValue)
		{
			ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(filter.FromMonthId.Value);
			if (reportingMonth != null)
			{
				int fromKey = reportingMonth.Year * 12 + reportingMonth.Month;
				reports = reports.Where((Report r) => r.ReportingMonth.Year * 12 + r.ReportingMonth.Month >= fromKey);
			}
		}
		if (filter.ToMonthId.HasValue)
		{
			ReportingMonth reportingMonth2 = await _db.ReportingMonths.FindAsync(filter.ToMonthId.Value);
			if (reportingMonth2 != null)
			{
				int toKey = reportingMonth2.Year * 12 + reportingMonth2.Month;
				reports = reports.Where((Report r) => r.ReportingMonth.Year * 12 + r.ReportingMonth.Month <= toKey);
			}
		}
		return reports;
	}

	private static IQueryable<Report> ApplyArchiveFilter(IQueryable<Report> reports, DashboardFilter filter, UserRoleEnum currentUserRole)
	{
		if (filter.IncludeArchived && CanIncludeArchived(currentUserRole))
		{
			return reports;
		}
		return reports.Where((Report r) => !r.IsArchived);
	}

	private static bool CanIncludeArchived(UserRoleEnum role)
	{
		return (uint)(role - 1) <= 2u;
	}

	private async Task<IQueryable<ReportRow>> ApplyReportRowMonthRangeAsync(IQueryable<ReportRow> rows, DashboardFilter filter)
	{
		if (filter.FromMonthId.HasValue)
		{
			ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(filter.FromMonthId.Value);
			if (reportingMonth != null)
			{
				int fromKey = reportingMonth.Year * 12 + reportingMonth.Month;
				rows = rows.Where((ReportRow rr) => rr.Report.ReportingMonth.Year * 12 + rr.Report.ReportingMonth.Month >= fromKey);
			}
		}
		if (filter.ToMonthId.HasValue)
		{
			ReportingMonth reportingMonth2 = await _db.ReportingMonths.FindAsync(filter.ToMonthId.Value);
			if (reportingMonth2 != null)
			{
				int toKey = reportingMonth2.Year * 12 + reportingMonth2.Month;
				rows = rows.Where((ReportRow rr) => rr.Report.ReportingMonth.Year * 12 + rr.Report.ReportingMonth.Month <= toKey);
			}
		}
		return rows;
	}

	private async Task<ReportingMonth?> ResolveMissingReportMonthAsync(DashboardFilter filter)
	{
		if (filter.FromMonthId.HasValue && filter.ToMonthId.HasValue && filter.FromMonthId == filter.ToMonthId)
		{
			return await _db.ReportingMonths.FindAsync(filter.FromMonthId.Value);
		}
		if (filter.FromMonthId.HasValue && !filter.ToMonthId.HasValue)
		{
			return await _db.ReportingMonths.FindAsync(filter.FromMonthId.Value);
		}
		return await _db.ReportingMonths.FirstOrDefaultAsync((ReportingMonth m) => m.IsActive);
	}

	private static IQueryable<User> ApplyUserIdNumberFilter(IQueryable<User> source, string? value)
	{
		string raw = value?.Trim() ?? string.Empty;
		string digits = NormalizeDigits(raw);
		if (string.IsNullOrWhiteSpace(raw))
		{
			return source;
		}
		if (string.IsNullOrWhiteSpace(digits))
		{
			return source.Where((User u) => u.IdNumber.Contains(raw));
		}
		return source.Where((User u) => u.IdNumber.Contains(raw) || u.IdNumber.Replace("-", "").Replace(" ", "").Contains(digits));
	}

	private static IQueryable<Report> ApplyReportIdNumberFilter(IQueryable<Report> source, string? value)
	{
		string raw = value?.Trim() ?? string.Empty;
		string digits = NormalizeDigits(raw);
		if (string.IsNullOrWhiteSpace(raw))
		{
			return source;
		}
		if (string.IsNullOrWhiteSpace(digits))
		{
			return source.Where((Report r) => r.User.IdNumber.Contains(raw));
		}
		return source.Where((Report r) => r.User.IdNumber.Contains(raw) || r.User.IdNumber.Replace("-", "").Replace(" ", "").Contains(digits));
	}

	private static string NormalizeDigits(string value)
	{
		return new string(value.Where(char.IsDigit).ToArray());
	}

	private int? GetMonthlyRowAllocation(IEnumerable<int?> rowAllocationIds, List<int> fallbackAllocationIds)
	{
		List<int> ids = (from id in rowAllocationIds
			where id.HasValue
			select id.Value).Distinct().ToList();
		if (!ids.Any())
		{
			ids = fallbackAllocationIds;
		}
		if (!ids.Any())
		{
			return null;
		}
		return (from a in _db.Allocations
			where ids.Contains(a.Id) && a.MonthlyRowAllocation.HasValue
			select a.MonthlyRowAllocation).AsEnumerable().Min();
	}

	private async Task PopulateAttachmentFlagsAsync(List<DashboardReportRow> rows)
	{
		if (!rows.Any())
		{
			return;
		}
		List<int> reportIds = (from r in rows
			where r.ReportId != 0
			select r.ReportId).ToList();
		Dictionary<int, int> reportAttachmentCounts = (await (from da in _db.DocumentAttachments
			where da.ReportId.HasValue && reportIds.Contains(da.ReportId.Value)
			group da by da.ReportId.Value into g
			select new { ReportId = g.Key, Count = g.Count() }).ToListAsync()).ToDictionary(x => x.ReportId, x => x.Count);
		Dictionary<(int, int?), int> rowAttachmentCounts = (await (from rr in _db.ReportRows
			join da in _db.DocumentAttachments on rr.Id equals da.ReportRowId
			where reportIds.Contains(rr.ReportId)
			group da by new { rr.ReportId, rr.AllocationId } into g
			select new { g.Key.ReportId, g.Key.AllocationId, Count = g.Count() }).ToListAsync()).ToDictionary(x => (x.ReportId, x.AllocationId), x => x.Count);
		foreach (DashboardReportRow row in rows)
		{
			int count = reportAttachmentCounts.TryGetValue(row.ReportId, out int reportCount) ? reportCount : 0;
			if (row.AllocationId.HasValue)
			{
				count += rowAttachmentCounts.TryGetValue((row.ReportId, row.AllocationId), out int allocationCount) ? allocationCount : 0;
			}
			else
			{
				count += rowAttachmentCounts.Where(kvp => kvp.Key.Item1 == row.ReportId).Sum(kvp => kvp.Value);
			}
			row.DocumentCount = count;
			row.HasAttachments = count > 0;
		}
	}

	private IEnumerable<DashboardReportRow> ToDashboardRows(Report report, DashboardFilter filter, List<int> allocationIds)
	{
		List<int> allocationIds2 = allocationIds;
		Report report2 = report;
		IEnumerable<ReportRow> source = report2.ReportRows.AsEnumerable();
		if (HasAllocationDimensionFilter(filter))
		{
			source = source.Where((ReportRow rr) => rr.AllocationId.HasValue && allocationIds2.Contains(rr.AllocationId.Value));
		}
		List<IGrouping<int?, ReportRow>> source2 = (from rr in source
			group rr by rr.AllocationId).ToList();
		if (!source2.Any())
		{
			return new DashboardReportRow[1] { ToDashboardRow(report2, Enumerable.Empty<ReportRow>(), null, allocationIds2) };
		}
		return source2.Select(delegate(IGrouping<int?, ReportRow> group)
		{
			Allocation allocation = group.FirstOrDefault((ReportRow rr) => rr.Allocation != null)?.Allocation;
			return ToDashboardRow(report2, group, allocation, allocationIds2);
		});
	}

	private DashboardReportRow ToDashboardRow(Report report, IEnumerable<ReportRow> reportRows, Allocation? allocation, List<int> fallbackAllocationIds)
	{
		List<ReportRow> list = reportRows.ToList();
		return new DashboardReportRow
		{
			ReportId = report.Id,
			UserId = report.UserId,
			AllocationId = ((allocation != null) ? new int?(allocation.Id) : list.FirstOrDefault()?.AllocationId),
			ProjectName = (allocation?.Project?.Description ?? string.Empty),
			EmployeeCode = report.User.EmployeeCode,
			IdNumber = report.User.IdNumber,
			FullName = report.User.FirstName + " " + report.User.LastName,
			MonthDescription = report.ReportingMonth.Description,
			MonthYear = report.ReportingMonth.Year,
			MonthMonth = report.ReportingMonth.Month,
			StatusName = GetReportStatusText(report.Status),
			StatusId = report.StatusId,
			RowCount = list.Count,
			TotalDuration = list.Sum((ReportRow row) => row.MeetingDuration),
			MonthlyRowAllocation = GetMonthlyRowAllocation(list.Select((ReportRow rr) => rr.AllocationId), fallbackAllocationIds),
			SubmittedAt = report.SubmittedAt,
			HasAttachments = false
		};
	}

	private async Task<List<int>> GetMatchingAllocationIdsAsync(DashboardFilter filter, List<int> scopedUserIds)
	{
		List<int> scopedUserIds2 = scopedUserIds;
		IQueryable<Allocation> source = _db.Allocations.Where((Allocation a) => a.IsActive && scopedUserIds2.Contains(a.UserId));
		if (filter.DistrictId.HasValue)
		{
			int districtId = filter.DistrictId.Value;
			source = source.Where((Allocation a) => _db.Set<AllocationDistrict>().Any((AllocationDistrict ad) => ad.AllocationId == a.Id && ad.DistrictId == districtId));
		}
		if (filter.SectorId.HasValue)
		{
			int sectorId = filter.SectorId.Value;
			source = source.Where((Allocation a) => _db.Set<AllocationSector>().Any((AllocationSector s) => s.AllocationId == a.Id && s.SectorId == sectorId));
		}
		if (filter.ProgramId.HasValue)
		{
			int programId = filter.ProgramId.Value;
			source = source.Where((Allocation a) => _db.Set<AllocationProgram>().Any((AllocationProgram p) => p.AllocationId == a.Id && p.ProgramId == programId));
		}
		return await source.Select((Allocation a) => a.Id).ToListAsync();
	}

	private async Task<List<int>> GetScopedUserIdsAsync(int currentUserId, UserRoleEnum role)
	{
		if ((uint)(role - 1) <= 2u)
		{
			return await _db.Users.Select((User u) => u.Id).ToListAsync();
		}
		if (role == UserRoleEnum.Employee)
		{
			return new List<int> { currentUserId };
		}
		List<InspectorAssignment> list = await _db.InspectorAssignments.Where((InspectorAssignment a) => a.InspectorUserId == currentUserId).ToListAsync();
		if (!list.Any())
		{
			return new List<int>();
		}
		HashSet<int> allUserIds = new HashSet<int>();
		foreach (InspectorAssignment item in list)
		{
			IQueryable<Allocation> source = _db.Allocations.Where((Allocation a) => a.IsActive);
			if (item.DistrictId.HasValue)
			{
				int dId = item.DistrictId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationDistrict>().Any((AllocationDistrict ad) => ad.AllocationId == a.Id && ad.DistrictId == dId));
			}
			if (item.SectorId.HasValue)
			{
				int sId = item.SectorId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationSector>().Any((AllocationSector s) => s.AllocationId == a.Id && s.SectorId == sId));
			}
			if (item.ProgramId.HasValue)
			{
				int pId = item.ProgramId.Value;
				source = source.Where((Allocation a) => _db.Set<AllocationProgram>().Any((AllocationProgram p) => p.AllocationId == a.Id && p.ProgramId == pId));
			}
			foreach (int item2 in await source.Select((Allocation a) => a.UserId).ToListAsync())
			{
				allUserIds.Add(item2);
			}
		}
		return allUserIds.ToList();
	}

	public async Task<List<District>> GetFilteredDistrictsAsync(int currentUserId, UserRoleEnum role)
	{
		List<int> scopedUserIds = await GetScopedUserIdsAsync(currentUserId, role);
		IQueryable<int> districtIds = from ad in _db.Set<AllocationDistrict>()
			where scopedUserIds.Contains(ad.Allocation.UserId)
			select ad.DistrictId;
		return await (from d in _db.Districts
			where d.IsActive && districtIds.Contains(d.Id)
			orderby d.Description
			select d).ToListAsync();
	}

	public async Task<List<Sector>> GetFilteredSectorsAsync(int currentUserId, UserRoleEnum role, int? districtId = null)
	{
		List<int> scopedUserIds = await GetScopedUserIdsAsync(currentUserId, role);
		IQueryable<Allocation> allocations = _db.Allocations.Where((Allocation a) => a.IsActive && scopedUserIds.Contains(a.UserId));
		if (districtId.HasValue)
		{
			int dId = districtId.Value;
			allocations = allocations.Where((Allocation a) => _db.Set<AllocationDistrict>().Any((AllocationDistrict ad) => ad.AllocationId == a.Id && ad.DistrictId == dId));
		}
		IQueryable<int> sectorIds = from s in _db.Set<AllocationSector>()
			where allocations.Select((Allocation a) => a.Id).Contains(s.AllocationId)
			select s.SectorId;
		return await (from s in _db.Sectors
			where s.IsActive && sectorIds.Contains(s.Id)
			orderby s.Description
			select s).ToListAsync();
	}

	public async Task<List<Program>> GetFilteredProgramsAsync(int currentUserId, UserRoleEnum role, int? districtId = null)
	{
		List<int> scopedUserIds = await GetScopedUserIdsAsync(currentUserId, role);
		IQueryable<Allocation> allocations = _db.Allocations.Where((Allocation a) => a.IsActive && scopedUserIds.Contains(a.UserId));
		if (districtId.HasValue)
		{
			int dId = districtId.Value;
			allocations = allocations.Where((Allocation a) => _db.Set<AllocationDistrict>().Any((AllocationDistrict ad) => ad.AllocationId == a.Id && ad.DistrictId == dId));
		}
		IQueryable<int> programIds = from p in _db.Set<AllocationProgram>()
			where allocations.Select((Allocation a) => a.Id).Contains(p.AllocationId)
			select p.ProgramId;
		return await (from p in _db.Programs
			where p.IsActive && programIds.Contains(p.Id)
			orderby p.Description
			select p).ToListAsync();
	}

	private static bool HasAllocationDimensionFilter(DashboardFilter filter)
	{
		if (!filter.DistrictId.HasValue && !filter.SectorId.HasValue)
		{
			return filter.ProgramId.HasValue;
		}
		return true;
	}

	private static IOrderedQueryable<Report> ApplyReportSort(IQueryable<Report> reports, string? sortBy, bool sortDesc)
	{
		return sortBy?.ToLowerInvariant() switch
		{
			"code" => sortDesc ? reports.OrderByDescending((Report r) => r.User.EmployeeCode) : reports.OrderBy((Report r) => r.User.EmployeeCode), 
			"idnumber" => sortDesc ? reports.OrderByDescending((Report r) => r.User.IdNumber) : reports.OrderBy((Report r) => r.User.IdNumber), 
			"name" => sortDesc ? (from r in reports
				orderby r.User.LastName descending, r.User.FirstName descending
				select r) : (from r in reports
				orderby r.User.LastName, r.User.FirstName
				select r), 
			"status" => sortDesc ? reports.OrderByDescending((Report r) => r.Status.Name) : reports.OrderBy((Report r) => r.Status.Name), 
			"submitted" => sortDesc ? reports.OrderByDescending((Report r) => r.SubmittedAt) : reports.OrderBy((Report r) => r.SubmittedAt), 
			"month" => sortDesc ? (from r in reports
				orderby r.ReportingMonth.Year descending, r.ReportingMonth.Month descending
				select r) : (from r in reports
				orderby r.ReportingMonth.Year, r.ReportingMonth.Month
				select r), 
			_ => from r in reports
				orderby r.ReportingMonth.Year descending, r.ReportingMonth.Month descending, r.User.LastName, r.User.FirstName
				select r, 
		};
	}

	private static IOrderedQueryable<ReportRow> ApplyReportRowSort(IQueryable<ReportRow> rows, string? sortBy, bool sortDesc)
	{
		return sortBy?.ToLowerInvariant() switch
		{
			"sequence" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.SequenceNumber) : rows.OrderBy((ReportRow r) => r.SequenceNumber), 
			"idnumber" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.Report.User.IdNumber) : rows.OrderBy((ReportRow r) => r.Report.User.IdNumber), 
			"code" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.Report.User.EmployeeCode) : rows.OrderBy((ReportRow r) => r.Report.User.EmployeeCode), 
			"name" => sortDesc ? (from r in rows
				orderby r.Report.User.LastName descending, r.Report.User.FirstName descending
				select r) : (from r in rows
				orderby r.Report.User.LastName, r.Report.User.FirstName
				select r), 
			"month" => sortDesc ? (from r in rows
				orderby r.Report.ReportingMonth.Year descending, r.Report.ReportingMonth.Month descending
				select r) : (from r in rows
				orderby r.Report.ReportingMonth.Year, r.Report.ReportingMonth.Month
				select r), 
			"status" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.Report.Status.Name) : rows.OrderBy((ReportRow r) => r.Report.Status.Name), 
			"meetingdate" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.MeetingDate) : rows.OrderBy((ReportRow r) => r.MeetingDate), 
			"duration" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.MeetingDuration) : rows.OrderBy((ReportRow r) => r.MeetingDuration), 
			"district" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.District.Description) : rows.OrderBy((ReportRow r) => r.District.Description), 
			"locality" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.Locality.Description) : rows.OrderBy((ReportRow r) => r.Locality.Description), 
			"framework" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.Framework.Description) : rows.OrderBy((ReportRow r) => r.Framework.Description), 
			"program" => sortDesc ? rows.OrderByDescending((ReportRow r) => r.EducationalProgram.Description) : rows.OrderBy((ReportRow r) => r.EducationalProgram.Description), 
			_ => from r in rows
				orderby r.Report.ReportingMonth.Year descending, r.Report.ReportingMonth.Month descending, r.Report.User.LastName, r.Report.User.FirstName, r.SequenceNumber
				select r, 
		};
	}

	private static IOrderedEnumerable<DashboardReportRow> ApplyDashboardRowSort(IEnumerable<DashboardReportRow> rows, string? sortBy, bool sortDesc)
	{
		return sortBy?.ToLowerInvariant() switch
		{
			"code" => sortDesc ? rows.OrderByDescending((DashboardReportRow r) => r.EmployeeCode) : rows.OrderBy((DashboardReportRow r) => r.EmployeeCode), 
			"idnumber" => sortDesc ? rows.OrderByDescending((DashboardReportRow r) => r.IdNumber) : rows.OrderBy((DashboardReportRow r) => r.IdNumber), 
			"name" => sortDesc ? rows.OrderByDescending((DashboardReportRow r) => r.FullName) : rows.OrderBy((DashboardReportRow r) => r.FullName), 
			"status" => sortDesc ? rows.OrderByDescending((DashboardReportRow r) => r.StatusName) : rows.OrderBy((DashboardReportRow r) => r.StatusName), 
			"project" => sortDesc ? rows.OrderByDescending((DashboardReportRow r) => r.ProjectName) : rows.OrderBy((DashboardReportRow r) => r.ProjectName), 
			"submitted" => sortDesc ? rows.OrderByDescending((DashboardReportRow r) => r.SubmittedAt) : rows.OrderBy((DashboardReportRow r) => r.SubmittedAt), 
			"month" => sortDesc ? (from r in rows
				orderby r.MonthYear descending, r.MonthMonth descending
				select r) : (from r in rows
				orderby r.MonthYear, r.MonthMonth
				select r), 
			_ => from r in rows
				orderby r.MonthYear descending, r.MonthMonth descending, r.FullName, r.ProjectName
				select r, 
		};
	}

	private static IOrderedQueryable<User> ApplyMissingReportSort(IQueryable<User> users, string? sortBy, bool sortDesc)
	{
		return sortBy?.ToLowerInvariant() switch
		{
			"code" => sortDesc ? users.OrderByDescending((User u) => u.EmployeeCode) : users.OrderBy((User u) => u.EmployeeCode), 
			"idnumber" => sortDesc ? users.OrderByDescending((User u) => u.IdNumber) : users.OrderBy((User u) => u.IdNumber), 
			"name" => sortDesc ? (from u in users
				orderby u.LastName descending, u.FirstName descending
				select u) : (from u in users
				orderby u.LastName, u.FirstName
				select u), 
			_ => from u in users
				orderby u.LastName, u.FirstName
				select u, 
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
			_ => status.Description ?? status.Name, 
		};
	}
}

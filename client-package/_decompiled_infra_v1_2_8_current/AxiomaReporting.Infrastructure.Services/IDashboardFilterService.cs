using System.Collections.Generic;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;

namespace AxiomaReporting.Infrastructure.Services;

public interface IDashboardFilterService
{
	Task<(List<DashboardReportRow> Rows, int TotalCount)> GetReportsAsync(DashboardFilter filter, int currentUserId, UserRoleEnum currentUserRole);

	Task<(List<DashboardReportDetailRow> Rows, int TotalCount)> GetReportRowsAsync(DashboardFilter filter, int currentUserId, UserRoleEnum currentUserRole);

	Task<List<District>> GetFilteredDistrictsAsync(int currentUserId, UserRoleEnum role);

	Task<List<Sector>> GetFilteredSectorsAsync(int currentUserId, UserRoleEnum role, int? districtId = null);

	Task<List<Program>> GetFilteredProgramsAsync(int currentUserId, UserRoleEnum role, int? districtId = null);

	Task<bool> CanAccessReportAsync(int reportId, int currentUserId, UserRoleEnum currentUserRole);

	Task<FilterOptionsDto> GetCompatibleOptionsAsync(DashboardFilter currentSelection, int currentUserId, UserRoleEnum currentUserRole);
}

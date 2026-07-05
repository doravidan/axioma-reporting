using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Infrastructure.Services;

public interface IReportStatusService
{
	Task<Report?> GetOrCreateDraftAsync(int userId, int reportingMonthId);

	Task<bool> SubmitReportAsync(int reportId, int submittedByUserId);

	Task<bool> ApproveReportAsync(int reportId, int approvedByUserId);

	Task<bool> RejectReportAsync(int reportId, int rejectedByUserId, string rejectionReason);

	Task<bool> SaveDraftAsync(int reportId);
}

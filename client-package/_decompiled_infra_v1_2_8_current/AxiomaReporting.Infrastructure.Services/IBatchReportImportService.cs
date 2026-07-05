using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AxiomaReporting.Infrastructure.Services;

public interface IBatchReportImportService
{
	Task<BatchImportResult> ImportAsync(Stream xlsxStream, int reportingMonthId, int uploaderUserId, CancellationToken ct = default(CancellationToken), string? progressId = null);
}

using System.IO;
using System.Threading.Tasks;

namespace AxiomaReporting.Infrastructure.Services;

public interface IReportExcelImportService
{
	Task<ExcelImportResult> ImportAsync(int reportId, int allocationId, Stream stream, int currentUserId);
}

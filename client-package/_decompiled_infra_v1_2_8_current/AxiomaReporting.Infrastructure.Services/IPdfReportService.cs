using System.Collections.Generic;

namespace AxiomaReporting.Infrastructure.Services;

public interface IPdfReportService
{
	byte[] CreateErrorReport(IEnumerable<string> errors);
}

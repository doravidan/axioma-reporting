using System.Collections.Generic;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Infrastructure.Services;

public interface IReportValidationService
{
	Task<ValidationResult> ValidateRowAsync(ReportRow row, User employee, ReportingMonth month, List<ReportRow> allRowsInReport);

	Task<ValidationResult> ValidateSubmitAsync(Report report, User employee, ReportingMonth month);
}

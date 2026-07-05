using System.Collections.Generic;
using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Web.Models;

public class BatchReportImportFormViewModel
{
	public List<ReportingMonth> ReportingMonths { get; set; } = new List<ReportingMonth>();


	public int? SelectedReportingMonthId { get; set; }
}

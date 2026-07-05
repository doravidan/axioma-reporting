using System.Collections.Generic;
using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Web.Models;

public class ManualReportViewModel
{
	public List<ReportingMonth> ReportingMonths { get; set; } = new List<ReportingMonth>();
}

using System;
using System.Collections.Generic;
using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class ReportingMonth : BaseEntity
{
	public string Description { get; set; } = string.Empty;


	public int Month { get; set; }

	public int Year { get; set; }

	public DateTime LastReportingDate { get; set; }

	public bool IsActive { get; set; }

	public bool AllowFutureReporting { get; set; }

	public ICollection<Report> Reports { get; set; } = new List<Report>();

}

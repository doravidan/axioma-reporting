using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class ReportingMonth : BaseEntity
{
  public string Description { get; set; } = string.Empty;
  public int Month { get; set; }
  public int Year { get; set; }
  public DateTime LastReportingDate { get; set; }
  public bool IsActive { get; set; } = false;
  public bool AllowFutureReporting { get; set; } = false;
  public ICollection<Report> Reports { get; set; } = new List<Report>();
}

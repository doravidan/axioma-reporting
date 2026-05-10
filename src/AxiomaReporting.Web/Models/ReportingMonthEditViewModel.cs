namespace AxiomaReporting.Web.Models;

public class ReportingMonthEditViewModel
{
  public int Id { get; set; }
  public string Description { get; set; } = string.Empty;
  public int Month { get; set; }
  public int Year { get; set; }
  public DateTime LastReportingDate { get; set; }
  public bool AllowFutureReporting { get; set; }
  public bool IsActive { get; set; }

  /// <summary>
  /// True when the month is active and the current user is not Admin/PM.
  /// In that case <see cref="LastReportingDate"/> and <see cref="AllowFutureReporting"/>
  /// must be rendered as disabled inputs and ignored on POST.
  /// </summary>
  public bool LockNonAdminFields { get; set; }
}

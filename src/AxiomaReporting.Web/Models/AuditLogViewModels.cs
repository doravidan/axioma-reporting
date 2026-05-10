namespace AxiomaReporting.Web.Models;

public class AuditLogListItem
{
  public long Id { get; set; }
  public DateTime Timestamp { get; set; }
  public int? ActorUserId { get; set; }
  public string? ActorName { get; set; }
  public string Action { get; set; } = string.Empty;
  public string EntityType { get; set; } = string.Empty;
  public string? EntityId { get; set; }
  public string? Notes { get; set; }
  public string? IpAddress { get; set; }
  public string? UserAgent { get; set; }
  public string? Before { get; set; }
  public string? After { get; set; }
}

public class AuditLogListViewModel
{
  public List<AuditLogListItem> Items { get; set; } = new();
  public int Page { get; set; }
  public int PageSize { get; set; }
  public int TotalCount { get; set; }
  public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);

  public string? Action { get; set; }
  public string? EntityType { get; set; }
  public string? EntityId { get; set; }
  public int? ActorUserId { get; set; }
  public DateTime? FromDate { get; set; }
  public DateTime? ToDate { get; set; }
}

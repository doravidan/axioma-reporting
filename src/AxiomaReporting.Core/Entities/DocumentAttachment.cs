namespace AxiomaReporting.Core.Entities;

public class DocumentAttachment
{
  public int Id { get; set; }
  public int? UserId { get; set; }
  public User? User { get; set; }
  public int? ReportRowId { get; set; }
  public ReportRow? ReportRow { get; set; }
  public int? ReportId { get; set; }
  public Report? Report { get; set; }
  public string FileName { get; set; } = string.Empty;
  public string? Description { get; set; }
  public string FilePath { get; set; } = string.Empty;
  public long FileSize { get; set; }
  public string MimeType { get; set; } = string.Empty;
  public DateTime UploadedAt { get; set; }
  public int UploadedBy { get; set; }
  public User? UploadedByUser { get; set; }
}

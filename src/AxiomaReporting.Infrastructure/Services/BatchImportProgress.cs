namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// Snapshot of a batch Excel import's progress, published by
/// <see cref="BatchImportProgressStore"/> for polling from the UI.
/// </summary>
public class BatchImportProgress
{
  public int TotalRows { get; set; }
  public int ProcessedRows { get; set; }
  public int Percent { get; set; }
  public string Status { get; set; } = "waiting";
  public DateTime UpdatedAt { get; set; }
}

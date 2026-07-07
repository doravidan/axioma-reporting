using System.Collections.Concurrent;

namespace AxiomaReporting.Infrastructure.Services;

public record BatchImportProgress
{
  public int TotalRows { get; init; }
  public int ProcessedRows { get; init; }
  public int Percent { get; init; }
  public string Status { get; init; } = "processing";
  public DateTime UpdatedAt { get; init; }
}

/// <summary>
/// התקדמות ייבוא אקסל מרוכז לפי מזהה שנוצר בטופס — נדגם ע"י הדפדפן בזמן
/// שה-POST רץ (יישור לגרסת השרת). אחסון בזיכרון: תהליך יחיד ב-IIS.
/// </summary>
public static class BatchImportProgressStore
{
  private static readonly ConcurrentDictionary<string, BatchImportProgress> Items = new();
  private static readonly TimeSpan MaxAge = TimeSpan.FromMinutes(30);

  public static void Start(string? id, int totalRows)
  {
    if (string.IsNullOrWhiteSpace(id)) return;
    CleanupOldItems();
    Items[id] = new BatchImportProgress
    {
      TotalRows = totalRows,
      ProcessedRows = 0,
      Percent = 0,
      Status = "processing",
      UpdatedAt = DateTime.UtcNow
    };
  }

  public static void Update(string? id, int processedRows, int totalRows, string status = "processing")
  {
    if (string.IsNullOrWhiteSpace(id)) return;
    Items[id] = new BatchImportProgress
    {
      TotalRows = totalRows,
      ProcessedRows = processedRows,
      Percent = totalRows > 0 ? Math.Min(100, (int)(processedRows * 100L / totalRows)) : 0,
      Status = status,
      UpdatedAt = DateTime.UtcNow
    };
  }

  public static void Complete(string? id, int processedRows)
  {
    if (string.IsNullOrWhiteSpace(id)) return;
    Items[id] = new BatchImportProgress
    {
      TotalRows = processedRows,
      ProcessedRows = processedRows,
      Percent = 100,
      Status = "done",
      UpdatedAt = DateTime.UtcNow
    };
  }

  public static BatchImportProgress Get(string? id) =>
    !string.IsNullOrWhiteSpace(id) && Items.TryGetValue(id, out var progress)
      ? progress
      : new BatchImportProgress { Status = "unknown", UpdatedAt = DateTime.UtcNow };

  private static void CleanupOldItems()
  {
    var cutoff = DateTime.UtcNow - MaxAge;
    foreach (var (key, value) in Items)
    {
      if (value.UpdatedAt < cutoff) Items.TryRemove(key, out _);
    }
  }
}

using System.Collections.Concurrent;

namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// In-memory store for batch import progress, keyed by a client-supplied progress id.
/// Entries older than two hours are evicted lazily.
/// </summary>
public static class BatchImportProgressStore
{
  private static readonly ConcurrentDictionary<string, BatchImportProgress> Items = new();

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
    // Cap at 99% until Complete is called
    var percent = totalRows > 0 ? Math.Min(99, (int)Math.Round(processedRows * 100m / totalRows)) : 0;
    Items[id] = new BatchImportProgress
    {
      TotalRows = totalRows,
      ProcessedRows = processedRows,
      Percent = percent,
      Status = status,
      UpdatedAt = DateTime.UtcNow
    };
  }

  public static void Complete(string? id, int processedRows, int totalRows)
  {
    if (string.IsNullOrWhiteSpace(id)) return;
    Items[id] = new BatchImportProgress
    {
      TotalRows = totalRows,
      ProcessedRows = processedRows,
      Percent = 100,
      Status = "complete",
      UpdatedAt = DateTime.UtcNow
    };
  }

  public static BatchImportProgress Get(string? id)
  {
    CleanupOldItems();
    if (!string.IsNullOrWhiteSpace(id) && Items.TryGetValue(id, out var progress))
      return progress;
    return new BatchImportProgress { Status = "waiting", UpdatedAt = DateTime.UtcNow };
  }

  private static void CleanupOldItems()
  {
    var cutoff = DateTime.UtcNow.AddHours(-2);
    foreach (var item in Items)
    {
      if (item.Value.UpdatedAt < cutoff)
        Items.TryRemove(item.Key, out _);
    }
  }
}

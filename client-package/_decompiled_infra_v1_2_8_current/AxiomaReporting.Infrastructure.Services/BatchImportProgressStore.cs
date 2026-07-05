using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AxiomaReporting.Infrastructure.Services;

public static class BatchImportProgressStore
{
	private static readonly ConcurrentDictionary<string, BatchImportProgress> Items = new ConcurrentDictionary<string, BatchImportProgress>();

	public static void Start(string? id, int totalRows)
	{
		if (string.IsNullOrWhiteSpace(id))
		{
			return;
		}
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
		if (string.IsNullOrWhiteSpace(id))
		{
			return;
		}
		int percent = totalRows <= 0 ? 0 : Math.Min(99, (int)Math.Round(processedRows * 100m / totalRows));
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
		if (string.IsNullOrWhiteSpace(id))
		{
			return;
		}
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
		if (!string.IsNullOrWhiteSpace(id) && Items.TryGetValue(id, out BatchImportProgress progress))
		{
			return progress;
		}
		return new BatchImportProgress
		{
			Status = "waiting",
			UpdatedAt = DateTime.UtcNow
		};
	}

	private static void CleanupOldItems()
	{
		DateTime cutoff = DateTime.UtcNow.AddHours(-2);
		foreach (KeyValuePair<string, BatchImportProgress> item in Items)
		{
			if (item.Value.UpdatedAt < cutoff)
			{
				Items.TryRemove(item.Key, out var _);
			}
		}
	}
}

public class BatchImportProgress
{
	public int TotalRows { get; set; }

	public int ProcessedRows { get; set; }

	public int Percent { get; set; }

	public string Status { get; set; } = "waiting";

	public DateTime UpdatedAt { get; set; }
}

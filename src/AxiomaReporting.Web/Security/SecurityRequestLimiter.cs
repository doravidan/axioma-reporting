using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace AxiomaReporting.Web.Security;

public sealed class SecurityRequestLimiter
{
  private sealed class RequestWindow
  {
    internal object SyncRoot { get; } = new();
    internal Queue<DateTimeOffset> Requests { get; } = new();
    internal DateTimeOffset ExpiresAt { get; set; }
  }

  private readonly ConcurrentDictionary<string, RequestWindow> _windows = new();
  private int _operationCount;

  public bool TryAcquire(string purpose, string subject, int limit, TimeSpan window)
  {
    if (limit <= 0) throw new ArgumentOutOfRangeException(nameof(limit));
    if (window <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(window));

    var key = HashKey(purpose, subject);
    var now = DateTimeOffset.UtcNow;
    if (Interlocked.Increment(ref _operationCount) % 256 == 0)
      RemoveExpired(now);

    // Bound memory even when an attacker rotates identifiers continuously.
    if (!_windows.ContainsKey(key) && _windows.Count >= 10_000)
    {
      RemoveExpired(now);
      if (_windows.Count >= 10_000) return false;
    }

    var bucket = _windows.GetOrAdd(key, _ => new RequestWindow());
    var cutoff = now - window;

    lock (bucket.SyncRoot)
    {
      while (bucket.Requests.TryPeek(out var request) && request < cutoff)
        bucket.Requests.Dequeue();

      if (bucket.Requests.Count >= limit)
        return false;

      bucket.Requests.Enqueue(now);
      bucket.ExpiresAt = now + window;
      return true;
    }
  }

  public void Reset(string purpose, string subject) =>
    _windows.TryRemove(HashKey(purpose, subject), out _);

  private static string HashKey(string purpose, string subject)
  {
    var normalized = $"{purpose.Trim()}\n{subject.Trim().ToUpperInvariant()}";
    return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalized)));
  }

  private void RemoveExpired(DateTimeOffset now)
  {
    foreach (var pair in _windows)
    {
      if (pair.Value.ExpiresAt != default && pair.Value.ExpiresAt <= now)
        _windows.TryRemove(pair.Key, out _);
    }
  }
}

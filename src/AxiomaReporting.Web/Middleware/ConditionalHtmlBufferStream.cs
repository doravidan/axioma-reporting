namespace AxiomaReporting.Web.Middleware;

/// <summary>
/// Response body wrapper used by the Hebrew mojibake-repair middleware in
/// <c>Program.cs</c>. Only buffers the response into memory when the response
/// turns out to be <c>text/html</c> (decided lazily on the first write, once the
/// action result has set <see cref="HttpContext.Response"/>.ContentType). All other
/// responses — file downloads, Excel/PDF exports, JSON — stream straight through
/// to the real body untouched, avoiding double/triple in-memory buffering of large
/// binary payloads. See CLAUDE.md security/perf review finding C3.
/// </summary>
internal sealed class ConditionalHtmlBufferStream : Stream
{
  private readonly HttpContext _context;
  private readonly Stream _originalBody;
  private MemoryStream? _htmlBuffer;
  private bool _decided;

  public ConditionalHtmlBufferStream(HttpContext context, Stream originalBody)
  {
    _context = context;
    _originalBody = originalBody;
  }

  /// <summary>Non-null once a write occurred and the response was HTML; holds the buffered bytes.</summary>
  public MemoryStream? HtmlBuffer => _htmlBuffer;

  private void EnsureDecision()
  {
    if (_decided) return;
    _decided = true;
    var contentType = _context.Response.ContentType;
    if (!string.IsNullOrWhiteSpace(contentType) &&
        contentType.IndexOf("text/html", StringComparison.OrdinalIgnoreCase) >= 0)
    {
      _htmlBuffer = new MemoryStream();
    }
  }

  public override void Write(byte[] buffer, int offset, int count)
  {
    EnsureDecision();
    (_htmlBuffer as Stream ?? _originalBody).Write(buffer, offset, count);
  }

  public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
  {
    EnsureDecision();
    await (_htmlBuffer as Stream ?? _originalBody).WriteAsync(buffer, offset, count, cancellationToken);
  }

  public override async ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
  {
    EnsureDecision();
    await (_htmlBuffer as Stream ?? _originalBody).WriteAsync(buffer, cancellationToken);
  }

  public override void Flush() => (_htmlBuffer as Stream ?? _originalBody).Flush();

  public override Task FlushAsync(CancellationToken cancellationToken) =>
    (_htmlBuffer as Stream ?? _originalBody).FlushAsync(cancellationToken);

  public override bool CanRead => false;
  public override bool CanSeek => false;
  public override bool CanWrite => true;
  public override long Length => throw new NotSupportedException();
  public override long Position
  {
    get => throw new NotSupportedException();
    set => throw new NotSupportedException();
  }

  public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
  public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
  public override void SetLength(long value) => throw new NotSupportedException();
}

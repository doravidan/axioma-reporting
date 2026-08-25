using System.IO.Compression;

namespace AxiomaReporting.Web.Security;

public sealed record AttachmentValidationResult(bool IsValid, string ContentType)
{
  public static AttachmentValidationResult Invalid { get; } =
    new(false, "application/octet-stream");
}

public static class AttachmentFileSecurity
{
  private static readonly byte[] PdfSignature = { 0x25, 0x50, 0x44, 0x46, 0x2D };
  private static readonly byte[] OleSignature = { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };
  private static readonly byte[] PngSignature = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

  public static async Task<AttachmentValidationResult> ValidateAsync(
    Stream stream,
    string extension,
    CancellationToken cancellationToken = default)
  {
    if (!stream.CanRead || !stream.CanSeek)
      return AttachmentValidationResult.Invalid;

    extension = extension.ToLowerInvariant();
    stream.Position = 0;
    var header = new byte[8];
    var read = await ReadHeaderAsync(stream, header, cancellationToken);
    stream.Position = 0;

    try
    {
      return extension switch
      {
        ".pdf" when StartsWith(header, read, PdfSignature) => new(true, "application/pdf"),
        ".jpg" or ".jpeg" when read >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF
          => new(true, "image/jpeg"),
        ".png" when StartsWith(header, read, PngSignature) => new(true, "image/png"),
        ".doc" when StartsWith(header, read, OleSignature) => new(true, "application/msword"),
        ".xls" when StartsWith(header, read, OleSignature) => new(true, "application/vnd.ms-excel"),
        ".docx" when IsValidOpenXml(stream, "word/")
          => new(true, "application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
        ".xlsx" when IsValidOpenXml(stream, "xl/")
          => new(true, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
        _ => AttachmentValidationResult.Invalid
      };
    }
    catch (InvalidDataException)
    {
      return AttachmentValidationResult.Invalid;
    }
    finally
    {
      stream.Position = 0;
    }
  }

  public static string GetContentType(string fileName) =>
    Path.GetExtension(fileName).ToLowerInvariant() switch
    {
      ".pdf" => "application/pdf",
      ".jpg" or ".jpeg" => "image/jpeg",
      ".png" => "image/png",
      ".doc" => "application/msword",
      ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
      ".xls" => "application/vnd.ms-excel",
      ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      _ => "application/octet-stream"
    };

  public static bool CanDisplayInline(string fileName) =>
    Path.GetExtension(fileName).ToLowerInvariant() is ".pdf" or ".jpg" or ".jpeg" or ".png";

  public static string SafeOriginalFileName(string fileName)
  {
    var safe = new string(Path.GetFileName(fileName)
      .Where(c => !char.IsControl(c))
      .ToArray())
      .Trim();
    if (string.IsNullOrWhiteSpace(safe)) safe = "attachment";
    if (safe.Length <= 500) return safe;

    var extension = Path.GetExtension(safe);
    return safe[..Math.Max(1, 500 - extension.Length)] + extension;
  }

  public static string GetPrivateUploadDirectory(string contentRoot, string category)
  {
    if (category is not ("report-attachments" or "employee-attachments"))
      throw new ArgumentOutOfRangeException(nameof(category));

    // Keep the existing writable uploads ACL for compatibility. Static access
    // to this private subtree is blocked before UseStaticFiles in Program.cs.
    return Path.Combine(contentRoot, "wwwroot", "uploads", "private", category);
  }

  public static string GetStoredPrivatePath(string category, string storedFileName) =>
    $"/uploads/private/{category}/{storedFileName}";

  public static string? ResolveStoredPath(string contentRoot, string storedPath)
  {
    if (string.IsNullOrWhiteSpace(storedPath)) return null;

    var normalized = storedPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
    string allowedRoot;
    if (normalized.StartsWith($"uploads{Path.DirectorySeparatorChar}attachments{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase) ||
        normalized.StartsWith($"uploads{Path.DirectorySeparatorChar}employees{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase) ||
        normalized.StartsWith($"uploads{Path.DirectorySeparatorChar}private{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
    {
      allowedRoot = Path.GetFullPath(Path.Combine(contentRoot, "wwwroot", "uploads")) + Path.DirectorySeparatorChar;
      normalized = Path.Combine("wwwroot", normalized);
    }
    else if (normalized.StartsWith($"App_Data{Path.DirectorySeparatorChar}private-files{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
    {
      allowedRoot = Path.GetFullPath(Path.Combine(contentRoot, "App_Data", "private-files")) + Path.DirectorySeparatorChar;
    }
    else
    {
      return null;
    }

    var resolved = Path.GetFullPath(Path.Combine(contentRoot, normalized));
    return resolved.StartsWith(allowedRoot, StringComparison.OrdinalIgnoreCase) ? resolved : null;
  }

  private static async Task<int> ReadHeaderAsync(Stream stream, byte[] buffer, CancellationToken cancellationToken)
  {
    var total = 0;
    while (total < buffer.Length)
    {
      var read = await stream.ReadAsync(buffer.AsMemory(total, buffer.Length - total), cancellationToken);
      if (read == 0) break;
      total += read;
    }
    return total;
  }

  private static bool StartsWith(byte[] buffer, int bufferLength, byte[] signature) =>
    bufferLength >= signature.Length && buffer.AsSpan(0, signature.Length).SequenceEqual(signature);

  private static bool IsValidOpenXml(Stream stream, string requiredPrefix)
  {
    stream.Position = 0;
    using var archive = new ZipArchive(stream, ZipArchiveMode.Read, leaveOpen: true);
    if (archive.Entries.Count is 0 or > 5000) return false;

    var hasContentTypes = false;
    var hasRequiredPart = false;
    foreach (var entry in archive.Entries)
    {
      var name = entry.FullName.Replace('\\', '/');
      if (name.Equals("[Content_Types].xml", StringComparison.OrdinalIgnoreCase))
        hasContentTypes = true;
      if (name.StartsWith(requiredPrefix, StringComparison.OrdinalIgnoreCase))
        hasRequiredPart = true;
      if (hasContentTypes && hasRequiredPart) return true;
    }

    return false;
  }
}

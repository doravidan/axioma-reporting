using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// Reads and writes the active site logo path from the <c>SystemConstants</c>
/// table. The resolved value is cached on the instance to keep layout renders
/// (which call <see cref="GetLogoPathAsync"/> on every page) from hitting the
/// database more than once per request. Registered as Scoped in DI.
/// </summary>
public class BrandingService : IBrandingService
{
  private readonly AppDbContext _db;
  private string? _cachedPath;

  public BrandingService(AppDbContext db)
  {
    _db = db;
  }

  public async Task<string> GetLogoPathAsync(CancellationToken ct = default)
  {
    if (_cachedPath is not null)
    {
      return _cachedPath;
    }

    var row = await _db.SystemConstants
      .AsNoTracking()
      .FirstOrDefaultAsync(c => c.Key == IBrandingService.LogoPathConstantKey, ct);

    var value = row?.Value;
    _cachedPath = string.IsNullOrWhiteSpace(value)
      ? IBrandingService.DefaultLogoPath
      : value!;
    return _cachedPath;
  }

  public async Task SetLogoPathAsync(string publicPath, int? updatedByUserId, CancellationToken ct = default)
  {
    if (string.IsNullOrWhiteSpace(publicPath))
    {
      throw new ArgumentException("publicPath must not be empty.", nameof(publicPath));
    }

    var row = await _db.SystemConstants
      .FirstOrDefaultAsync(c => c.Key == IBrandingService.LogoPathConstantKey, ct);

    if (row is null)
    {
      row = new SystemConstant
      {
        Key = IBrandingService.LogoPathConstantKey,
        Value = publicPath,
        Description = "נתיב הלוגו של המערכת (תמונה ב-wwwroot)",
        CreatedAt = DateTime.UtcNow,
        UpdatedBy = updatedByUserId
      };
      _db.SystemConstants.Add(row);
    }
    else
    {
      row.Value = publicPath;
      row.UpdatedAt = DateTime.UtcNow;
      row.UpdatedBy = updatedByUserId;
    }

    await _db.SaveChangesAsync(ct);
    _cachedPath = publicPath;
  }
}

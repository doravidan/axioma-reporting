using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// Builds the client-requested composite display label for a framework:
/// "יישוב — סמל — שם מסגרת" (משוב בטא B35/B38/B39). The locality is resolved via the
/// Institution that shares the framework's InstitutionSymbol; frameworks with a
/// synthetic/unknown symbol fall back to the plain description.
/// The composite also disambiguates same-named frameworks (QA #6: "כל תאור מופיע פעמיים").
/// </summary>
public static class FrameworkLabelService
{
  public static async Task<Dictionary<int, string>> BuildLabelsAsync(
    AppDbContext db, IReadOnlyCollection<int> frameworkIds, CancellationToken ct = default)
  {
    if (frameworkIds.Count == 0) return new Dictionary<int, string>();

    var frameworks = await db.Frameworks.AsNoTracking()
      .Where(f => frameworkIds.Contains(f.Id))
      .Select(f => new { f.Id, f.Description, f.InstitutionSymbol })
      .ToListAsync(ct);

    return await ComposeAsync(db, frameworks.Select(f => (f.Id, f.Description, f.InstitutionSymbol)).ToList(), ct);
  }

  /// <summary>Label map for ALL active frameworks (admin screens / global dropdowns).</summary>
  public static async Task<Dictionary<int, string>> BuildAllActiveLabelsAsync(
    AppDbContext db, CancellationToken ct = default)
  {
    var frameworks = await db.Frameworks.AsNoTracking()
      .Where(f => f.IsActive)
      .Select(f => new { f.Id, f.Description, f.InstitutionSymbol })
      .ToListAsync(ct);

    return await ComposeAsync(db, frameworks.Select(f => (f.Id, f.Description, f.InstitutionSymbol)).ToList(), ct);
  }

  private static async Task<Dictionary<int, string>> ComposeAsync(
    AppDbContext db, List<(int Id, string Description, string Symbol)> frameworks, CancellationToken ct)
  {
    // Symbols are numeric for real institutions; synthetic (FW-*/QCAT-*) have no institution.
    var numericSymbols = frameworks
      .Select(f => int.TryParse(f.Symbol, out var s) ? (int?)s : null)
      .Where(s => s.HasValue)
      .Select(s => s!.Value)
      .Distinct()
      .ToList();

    var localityBySymbol = new Dictionary<int, string>();
    if (numericSymbols.Count > 0)
    {
      var pairs = await db.Institutions.AsNoTracking()
        .Where(i => numericSymbols.Contains(i.InstitutionSymbol) && i.LocalityId != null)
        .Select(i => new { i.InstitutionSymbol, Locality = i.Locality!.Description })
        .ToListAsync(ct);
      foreach (var p in pairs)
        localityBySymbol.TryAdd(p.InstitutionSymbol, p.Locality);
    }

    var labels = new Dictionary<int, string>(frameworks.Count);
    foreach (var f in frameworks)
    {
      string? locality = null;
      var hasSymbol = int.TryParse(f.Symbol, out var symbol);
      if (hasSymbol) localityBySymbol.TryGetValue(symbol, out locality);

      labels[f.Id] = hasSymbol
        ? (string.IsNullOrEmpty(locality)
            ? $"{symbol} — {f.Description}"
            : $"{locality} — {symbol} — {f.Description}")
        : f.Description;
    }
    return labels;
  }
}

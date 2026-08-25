using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Entities.Base;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// Centralized text-to-ID resolver for lookup tables. Accepts either a numeric ID string
/// or an exact description match (trimmed). Caches each lookup dictionary per request
/// (service lifetime = scoped).
/// </summary>
public interface ILookupResolver
{
  Task<int?> ResolveDistrictAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveSectorAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveLocalityAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveFrameworkAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveSubjectAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveDomainAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveEducationalProgramAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveProgramAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveProjectAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveClassAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveGradeLevelAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveDiscussionCodeAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveLocalityDistrictNationalAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveReportTypeAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveClassConclusionAsync(string? value, CancellationToken ct = default);
  Task<int?> ResolveFrameworkConclusionAsync(string? value, CancellationToken ct = default);
}

public class LookupResolver : ILookupResolver
{
  private readonly AppDbContext _db;

  // Per-request caches: description (lowercased, trimmed) -> Id, and id set for numeric validation
  private Dictionary<string, int>? _districts;
  private Dictionary<string, int>? _sectors;
  private Dictionary<string, int>? _localities;
  private Dictionary<string, int>? _frameworks;
  private Dictionary<string, int>? _frameworkSymbols;
  private Dictionary<string, int>? _subjects;
  private Dictionary<string, int>? _domains;
  private Dictionary<string, int>? _educationalPrograms;
  private Dictionary<string, int>? _programs;
  private Dictionary<string, int>? _projects;
  private Dictionary<string, int>? _classes;
  private Dictionary<string, int>? _gradeLevels;
  private Dictionary<string, int>? _discussionCodes;
  private Dictionary<string, int>? _localityDistrictNationals;
  private Dictionary<string, int>? _reportTypes;
  private Dictionary<string, int>? _classConclusions;
  private Dictionary<string, int>? _frameworkConclusions;

  public LookupResolver(AppDbContext db) { _db = db; }

  public Task<int?> ResolveDistrictAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _districts, d => _districts = d, _db.Districts);

  public Task<int?> ResolveSectorAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _sectors, d => _sectors = d, _db.Sectors);

  public Task<int?> ResolveLocalityAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _localities, d => _localities = d, _db.Localities);

  public async Task<int?> ResolveFrameworkAsync(string? value, CancellationToken ct = default)
  {
    if (string.IsNullOrWhiteSpace(value)) return null;
    var trimmed = value.Trim();

    // Framework allows match by institution symbol (numeric) as well as by description.
    if (_frameworks == null || _frameworkSymbols == null)
    {
      var data = await _db.Frameworks.AsNoTracking()
        .Select(f => new { f.Id, f.Description, f.InstitutionSymbol })
        .ToListAsync(ct);
      _frameworks = data
        .Where(x => !string.IsNullOrWhiteSpace(x.Description))
        .GroupBy(x => x.Description.Trim().ToLowerInvariant())
        .ToDictionary(g => g.Key, g => g.First().Id);
      _frameworkSymbols = data
        .Where(x => !string.IsNullOrWhiteSpace(x.InstitutionSymbol))
        .GroupBy(x => x.InstitutionSymbol!.Trim())
        .ToDictionary(g => g.Key, g => g.First().Id);
    }

    if (int.TryParse(trimmed, out _) && _frameworkSymbols.TryGetValue(trimmed, out var byId))
      return byId;
    if (_frameworks.TryGetValue(trimmed.ToLowerInvariant(), out var id))
      return id;

    // The UI/export format is often a composite label such as
    // "יישוב — 248013 — שם המסגרת". Resolve its institution symbol too.
    // Keep this after exact matches so ordinary descriptions containing digits
    // retain their existing behavior.
    var symbolMatch = Regex.Match(trimmed, @"(?<!\d)\d{3,}(?!\d)");
    if (symbolMatch.Success && _frameworkSymbols.TryGetValue(symbolMatch.Value, out var byCompositeSymbol))
      return byCompositeSymbol;

    return null;
  }

  public Task<int?> ResolveSubjectAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _subjects, d => _subjects = d, _db.Subjects);

  public Task<int?> ResolveDomainAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _domains, d => _domains = d, _db.Domains);

  public Task<int?> ResolveEducationalProgramAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _educationalPrograms, d => _educationalPrograms = d, _db.EducationalPrograms);

  public Task<int?> ResolveProgramAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _programs, d => _programs = d, _db.Programs);

  public Task<int?> ResolveProjectAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _projects, d => _projects = d, _db.Projects);

  public Task<int?> ResolveClassAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _classes, d => _classes = d, _db.Classes);

  public Task<int?> ResolveGradeLevelAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _gradeLevels, d => _gradeLevels = d, _db.GradeLevels);

  public Task<int?> ResolveDiscussionCodeAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _discussionCodes, d => _discussionCodes = d, _db.DiscussionCodes);

  public Task<int?> ResolveLocalityDistrictNationalAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _localityDistrictNationals, d => _localityDistrictNationals = d, _db.LocalityDistrictNationals);

  public Task<int?> ResolveReportTypeAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _reportTypes, d => _reportTypes = d, _db.ReportTypes);

  public Task<int?> ResolveClassConclusionAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _classConclusions, d => _classConclusions = d, _db.ClassConclusions);

  public Task<int?> ResolveFrameworkConclusionAsync(string? value, CancellationToken ct = default) =>
    ResolveAsync(value, ct, () => _frameworkConclusions, d => _frameworkConclusions = d, _db.FrameworkConclusions);

  private static async Task<int?> ResolveAsync<T>(
    string? value,
    CancellationToken ct,
    Func<Dictionary<string, int>?> cacheGetter,
    Action<Dictionary<string, int>> cacheSetter,
    IQueryable<T> source) where T : LookupEntity
  {
    if (string.IsNullOrWhiteSpace(value)) return null;
    var trimmed = value.Trim();

    var cache = cacheGetter();
    if (cache == null)
    {
      var data = await source.AsNoTracking()
        .Select(e => new { e.Id, e.Description })
        .ToListAsync(ct);
      cache = data
        .Where(x => !string.IsNullOrWhiteSpace(x.Description))
        .GroupBy(x => x.Description.Trim().ToLowerInvariant())
        .ToDictionary(g => g.Key, g => g.First().Id);
      cacheSetter(cache);
    }

    // Allow numeric id — but only if the id actually exists in this lookup.
    if (int.TryParse(trimmed, out var parsedId) && cache.Values.Contains(parsedId))
      return parsedId;

    return cache.TryGetValue(trimmed.ToLowerInvariant(), out var id) ? id : null;
  }
}

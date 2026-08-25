namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// Canonical handling for institution symbols at all write boundaries. The
/// stored value remains text so leading zeroes are never lost.
/// </summary>
public static class InstitutionSymbolNormalizer
{
  public const int MaxLength = 100;

  public static string Normalize(string? value) => value?.Trim() ?? string.Empty;

  public static bool TryNormalizeInstitution(string? value, out string normalized)
  {
    normalized = Normalize(value);
    return normalized.Length is > 0 and <= MaxLength && normalized.All(char.IsDigit);
  }

  public static bool TryNormalizeFramework(string? value, out string normalized)
  {
    normalized = Normalize(value);
    return normalized.Length is > 0 and <= MaxLength;
  }

  public static string NumericComparisonKey(string? value)
  {
    var normalized = Normalize(value);
    if (normalized.Length == 0 || !normalized.All(char.IsDigit)) return normalized;
    var withoutLeadingZeroes = normalized.TrimStart('0');
    return withoutLeadingZeroes.Length == 0 ? "0" : withoutLeadingZeroes;
  }
}

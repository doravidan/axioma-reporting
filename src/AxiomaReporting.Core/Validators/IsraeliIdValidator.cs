namespace AxiomaReporting.Core.Validators;

/// <summary>
/// Validates an Israeli national ID (Teudat Zehut) using the standard check-digit algorithm.
/// Pads to 9 digits with leading zeros. Rejects null/empty/non-digit input.
/// </summary>
public static class IsraeliIdValidator
{
  /// <summary>
  /// Returns true if the supplied value is a syntactically valid Israeli ID.
  /// </summary>
  public static bool IsValid(string? id)
  {
    if (string.IsNullOrWhiteSpace(id)) return false;

    var trimmed = id.Trim();
    foreach (var c in trimmed)
    {
      if (!char.IsDigit(c)) return false;
    }

    // Israeli IDs are up to 9 digits; left-pad shorter values with zeros.
    if (trimmed.Length > 9) return false;
    var padded = trimmed.PadLeft(9, '0');

    // Standard Luhn-like algorithm: each digit at index i is multiplied by (i % 2) + 1.
    // If the product exceeds 9, sum the digits of the product. The total sum mod 10 must equal 0.
    var sum = 0;
    for (var i = 0; i < 9; i++)
    {
      var digit = padded[i] - '0';
      var product = digit * ((i % 2) + 1);
      if (product > 9) product = (product / 10) + (product % 10);
      sum += product;
    }

    return sum % 10 == 0;
  }
}

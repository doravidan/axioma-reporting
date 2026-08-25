using System.Globalization;
using System.Text;
using ClosedXML.Excel;

namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// Shared normalization and duration parsing for personal and multi-employee
/// report imports. The business unit stored by the application is decimal hours.
/// </summary>
public static class ExcelReportParsing
{
  private static readonly char[] InvisibleCharacters =
    { '\u200B', '\u200C', '\u200D', '\u200E', '\u200F', '\uFEFF' };

  public const string DurationFormatDescription =
    "מספר שעות עשרוני (לדוגמה 1.5) או זמן בתבנית hh:mm";

  public static string NormalizeHeader(string? value)
  {
    if (string.IsNullOrWhiteSpace(value)) return string.Empty;

    var normalized = value.Normalize(NormalizationForm.FormKC)
      .Replace('\u00A0', ' ')
      .Replace("\"", string.Empty);
    foreach (var invisible in InvisibleCharacters)
      normalized = normalized.Replace(invisible.ToString(), string.Empty);

    var builder = new StringBuilder(normalized.Length);
    var lastWasWhitespace = false;
    foreach (var character in normalized)
    {
      if (char.IsWhiteSpace(character))
      {
        if (!lastWasWhitespace) builder.Append(' ');
        lastWasWhitespace = true;
      }
      else
      {
        builder.Append(character);
        lastWasWhitespace = false;
      }
    }

    return builder.ToString().Trim().ToLowerInvariant();
  }

  public static bool TryParseDuration(
    IXLCell cell,
    out decimal hours,
    out string rawValue,
    out string error)
  {
    hours = 0m;
    rawValue = cell.GetFormattedString().Trim();
    error = string.Empty;

    if (cell.IsEmpty() || string.IsNullOrWhiteSpace(rawValue))
    {
      error = $"נדרש ערך בפורמט {DurationFormatDescription}";
      return false;
    }

    var normalized = rawValue
      .Replace('\u00A0', ' ')
      .Replace("\u200B", string.Empty)
      .Replace("\uFEFF", string.Empty)
      .Trim();
    var timeFormats = new[] { @"h\:mm", @"hh\:mm", @"h\:mm\:ss", @"hh\:mm\:ss" };

    if (normalized.Contains(':') &&
        TimeSpan.TryParseExact(normalized, timeFormats, CultureInfo.InvariantCulture, out var parsedTime) &&
        parsedTime > TimeSpan.Zero)
    {
      hours = (decimal)parsedTime.TotalHours;
      return true;
    }

    if (cell.DataType == XLDataType.TimeSpan &&
        cell.TryGetValue<TimeSpan>(out var timeSpan))
    {
      hours = (decimal)timeSpan.TotalHours;
      if (hours > 0) return true;
    }

    if (cell.TryGetValue<decimal>(out var numericValue))
    {
      hours = numericValue;
      if (hours > 0) return true;
    }

    if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out hours) ||
        decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.GetCultureInfo("he-IL"), out hours))
    {
      if (hours > 0) return true;
    }

    if (TimeSpan.TryParseExact(normalized, timeFormats, CultureInfo.InvariantCulture, out timeSpan) &&
        timeSpan > TimeSpan.Zero)
    {
      hours = (decimal)timeSpan.TotalHours;
      return true;
    }

    hours = 0m;
    error = $"הערך '{rawValue}' אינו תקין; הפורמט הנדרש: {DurationFormatDescription}";
    return false;
  }
}

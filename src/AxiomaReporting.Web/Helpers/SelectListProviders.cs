using Microsoft.AspNetCore.Mvc.Rendering;

namespace AxiomaReporting.Web.Helpers;

/// <summary>
/// Static SelectListItem providers for dropdowns whose options are fixed (not table-driven).
/// </summary>
public static class SelectListProviders
{
  /// <summary>
  /// Rest day options per client feedback Fix #8: Sunday (0), Friday (5), Saturday (6) only,
  /// plus an empty "— ללא —" sentinel.
  /// Day-of-week numeric values match <see cref="System.DayOfWeek"/> (0 = Sunday, 6 = Saturday).
  /// </summary>
  public static IReadOnlyList<SelectListItem> RestDayOptions { get; } = new List<SelectListItem>
  {
    new() { Value = string.Empty, Text = "— ללא —" },
    new() { Value = "0", Text = "ראשון" },
    new() { Value = "5", Text = "שישי" },
    new() { Value = "6", Text = "שבת" }
  };

  /// <summary>
  /// Build a fresh List&lt;SelectListItem&gt; with the supplied value pre-selected. Returning a copy keeps
  /// the static <see cref="RestDayOptions"/> immutable across requests.
  /// </summary>
  public static List<SelectListItem> RestDayOptionsWithSelection(int? selectedValue)
  {
    var sel = selectedValue?.ToString() ?? string.Empty;
    return RestDayOptions
      .Select(o => new SelectListItem { Value = o.Value, Text = o.Text, Selected = o.Value == sel })
      .ToList();
  }

  /// <summary>Tri-state ("yes / no / all") select for nullable boolean filters.</summary>
  public static List<SelectListItem> TriStateBoolOptions(bool? selected, string allText = "-- הכל --",
    string yesText = "כן", string noText = "לא")
  {
    return new List<SelectListItem>
    {
      new() { Value = string.Empty, Text = allText, Selected = !selected.HasValue },
      new() { Value = "true", Text = yesText, Selected = selected == true },
      new() { Value = "false", Text = noText, Selected = selected == false }
    };
  }
}

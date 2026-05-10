using System.Net;
using System.Text.RegularExpressions;

namespace AxiomaReporting.Tests.TestSupport;

public static partial class HtmlForm
{
  public static string AntiForgeryToken(string html)
  {
    var match = AntiForgeryRegex().Match(html);
    if (!match.Success)
      throw new InvalidOperationException("Could not find antiforgery token in rendered form.");
    return WebUtility.HtmlDecode(match.Groups["value"].Value);
  }

  [GeneratedRegex("name=\"__RequestVerificationToken\"\\s+type=\"hidden\"\\s+value=\"(?<value>[^\"]+)\"|type=\"hidden\"\\s+value=\"(?<value>[^\"]+)\"\\s+name=\"__RequestVerificationToken\"", RegexOptions.IgnoreCase)]
  private static partial Regex AntiForgeryRegex();
}

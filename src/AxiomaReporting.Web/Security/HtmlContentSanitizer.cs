using Ganss.Xss;

namespace AxiomaReporting.Web.Security;

public interface IHtmlContentSanitizer
{
  string Sanitize(string? html);
}

public sealed class HtmlContentSanitizer : IHtmlContentSanitizer
{
  public string Sanitize(string? html)
  {
    if (string.IsNullOrWhiteSpace(html)) return string.Empty;

    // Preserve legal-notice and email formatting while removing executable
    // elements, event handlers, unsafe URLs, and dangerous CSS.
    var sanitizer = new HtmlSanitizer();
    sanitizer.AllowedAttributes.Add("class");
    sanitizer.AllowedAttributes.Add("dir");
    sanitizer.AllowedAttributes.Add("lang");
    sanitizer.AllowedAttributes.Add("target");
    return sanitizer.Sanitize(html);
  }
}

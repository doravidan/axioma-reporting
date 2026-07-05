using AxiomaReporting.Core.Interfaces;
using Ganss.Xss;

namespace AxiomaReporting.Infrastructure.Services;

/// <summary>
/// Wraps Ganss.Xss.HtmlSanitizer with an allow-list suited for admin-authored
/// content (privacy policy / terms of use / email template bodies): basic text
/// formatting, links, and lists, but no script, style, form, iframe, or event
/// handler attributes. See CLAUDE.md security review finding C1.
/// </summary>
public sealed class HtmlSanitizerService : IHtmlSanitizerService
{
  private readonly HtmlSanitizer _sanitizer;

  public HtmlSanitizerService()
  {
    _sanitizer = new HtmlSanitizer();
    _sanitizer.AllowedTags.Clear();
    foreach (var tag in new[]
    {
      "p", "br", "b", "strong", "i", "em", "u", "s", "span", "div",
      "ul", "ol", "li", "a", "h1", "h2", "h3", "h4", "h5", "h6",
      "blockquote", "hr", "table", "thead", "tbody", "tr", "td", "th"
    })
    {
      _sanitizer.AllowedTags.Add(tag);
    }

    _sanitizer.AllowedAttributes.Clear();
    foreach (var attr in new[] { "href", "dir", "style", "colspan", "rowspan" })
    {
      _sanitizer.AllowedAttributes.Add(attr);
    }

    _sanitizer.AllowedCssProperties.Clear();
    foreach (var prop in new[] { "text-align", "direction", "font-weight", "color" })
    {
      _sanitizer.AllowedCssProperties.Add(prop);
    }

    _sanitizer.AllowedSchemes.Clear();
    foreach (var scheme in new[] { "http", "https", "mailto" })
    {
      _sanitizer.AllowedSchemes.Add(scheme);
    }

    _sanitizer.KeepChildNodes = true;
  }

  public string Sanitize(string? html) =>
    string.IsNullOrWhiteSpace(html) ? string.Empty : _sanitizer.Sanitize(html);
}

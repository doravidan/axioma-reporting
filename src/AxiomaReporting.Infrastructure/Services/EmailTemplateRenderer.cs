using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AxiomaReporting.Infrastructure.Services;

public sealed record RenderedEmail(string Subject, string BodyHtml);

public class EmailTemplateRenderer
{
  private readonly AppDbContext _db;

  public EmailTemplateRenderer(AppDbContext db)
  {
    _db = db;
  }

  public async Task<RenderedEmail?> RenderAsync(
    string templateType,
    Dictionary<string, string> tokens,
    CancellationToken cancellationToken = default)
  {
    var template = await _db.EmailTemplates
      .FirstOrDefaultAsync(t => t.TypeDescription == templateType && t.IsActive, cancellationToken);
    if (template == null) return null;

    var subject = ReplaceTokens(template.Subject, tokens);
    var body = ReplaceTokens(template.Body, tokens);
    return new RenderedEmail(subject, $"<div dir='rtl'>{FormatBodyHtml(body, tokens)}</div>");
  }

  public static string ReplaceTokens(string template, Dictionary<string, string> tokens)
  {
    foreach (var (key, value) in tokens)
    {
      template = template.Replace($"{{{key}}}", value, StringComparison.OrdinalIgnoreCase);
      template = template.Replace($"{{{{{key}}}}}", value, StringComparison.OrdinalIgnoreCase);
    }
    return template;
  }

  private static string FormatBodyHtml(string body, Dictionary<string, string> tokens)
  {
    var html = WebUtility.HtmlEncode(body)
      .Replace("\r\n", "<br>")
      .Replace("\n", "<br>");

    foreach (var (key, value) in tokens)
    {
      if (!key.EndsWith("Link", StringComparison.OrdinalIgnoreCase) ||
          string.IsNullOrWhiteSpace(value))
        continue;

      var encodedUrl = WebUtility.HtmlEncode(value);
      var link = $"<a href=\"{encodedUrl}\" target=\"_blank\" rel=\"noopener\">{encodedUrl}</a>";
      html = html.Replace(encodedUrl, link, StringComparison.OrdinalIgnoreCase);
    }

    return html;
  }
}

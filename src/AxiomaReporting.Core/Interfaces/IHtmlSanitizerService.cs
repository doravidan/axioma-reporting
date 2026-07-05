namespace AxiomaReporting.Core.Interfaces;

/// <summary>
/// Sanitizes admin-authored rich-text HTML (privacy policy, terms of use, email
/// templates) before it is persisted, so it is safe to render with @Html.Raw to
/// anonymous or authenticated users. Strips script/event-handler/style-based XSS
/// vectors while preserving basic RTL-friendly formatting markup.
/// </summary>
public interface IHtmlSanitizerService
{
  string Sanitize(string? html);
}

namespace AxiomaReporting.Core.Interfaces;

public interface ISmtpSender
{
  /// <summary>
  /// Sends a pre-rendered email. Throws on SMTP/transient failure.
  /// Returns silently if no SMTP settings are configured or the recipient has no address.
  /// </summary>
  Task SendRenderedAsync(
    string toEmail,
    string toName,
    string subject,
    string bodyHtml,
    IReadOnlyList<EmailAttachment>? attachments = null,
    CancellationToken cancellationToken = default);
}

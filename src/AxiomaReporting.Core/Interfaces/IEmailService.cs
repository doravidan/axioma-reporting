namespace AxiomaReporting.Core.Interfaces;

public sealed record EmailAttachment(string FileName, byte[] Content, string MimeType);

public interface IEmailService
{
  Task SendAsync(string toEmail, string toName, string templateType,
    Dictionary<string, string> tokens,
    IReadOnlyList<EmailAttachment>? attachments = null,
    CancellationToken cancellationToken = default);
}

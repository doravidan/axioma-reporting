using AxiomaReporting.Core.Interfaces;

namespace AxiomaReporting.Tests.TestSupport;

public sealed class FakeEmailService : IEmailService
{
  private readonly List<SentEmail> _sent = new();

  public IReadOnlyList<SentEmail> Sent => _sent;

  public Task SendAsync(
    string toEmail,
    string toName,
    string templateType,
    Dictionary<string, string> tokens,
    IReadOnlyList<EmailAttachment>? attachments = null,
    CancellationToken cancellationToken = default)
  {
    _sent.Add(new SentEmail(
      toEmail,
      toName,
      templateType,
      new Dictionary<string, string>(tokens),
      attachments is null ? Array.Empty<EmailAttachment>() : attachments.ToArray()));
    return Task.CompletedTask;
  }
}

public sealed record SentEmail(
  string ToEmail,
  string ToName,
  string TemplateType,
  IReadOnlyDictionary<string, string> Tokens,
  IReadOnlyList<EmailAttachment> Attachments);

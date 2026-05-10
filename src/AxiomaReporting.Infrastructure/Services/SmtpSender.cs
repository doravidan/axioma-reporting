using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace AxiomaReporting.Infrastructure.Services;

public class SmtpSender : ISmtpSender
{
  private readonly AppDbContext _db;
  private readonly ILogger<SmtpSender> _logger;

  public SmtpSender(AppDbContext db, ILogger<SmtpSender> logger)
  {
    _db = db;
    _logger = logger;
  }

  public async Task SendRenderedAsync(
    string toEmail,
    string toName,
    string subject,
    string bodyHtml,
    IReadOnlyList<EmailAttachment>? attachments = null,
    CancellationToken cancellationToken = default)
  {
    if (string.IsNullOrWhiteSpace(toEmail))
    {
      _logger.LogWarning("EMAIL: No email address for recipient {Name}, skipping", toName);
      return;
    }

    var settings = await _db.EmailServerSettings.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    if (settings == null)
    {
      _logger.LogWarning("EMAIL: No SMTP settings configured");
      return;
    }

    var message = new MimeMessage();
    message.From.Add(new MailboxAddress(settings.FromName ?? "Axioma", settings.FromAddress));
    message.To.Add(new MailboxAddress(toName, toEmail));
    message.Subject = subject;

    var htmlBody = new TextPart("html") { Text = bodyHtml };
    if (attachments is { Count: > 0 })
    {
      var multipart = new Multipart("mixed") { htmlBody };
      foreach (var att in attachments)
      {
        var parts = att.MimeType.Split('/', 2);
        var mediaType = parts.Length == 2 ? parts[0] : "application";
        var mediaSubtype = parts.Length == 2 ? parts[1] : "octet-stream";
        var mimePart = new MimePart(mediaType, mediaSubtype)
        {
          Content = new MimeContent(new MemoryStream(att.Content)),
          ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment),
          ContentTransferEncoding = ContentEncoding.Base64,
          FileName = att.FileName
        };
        multipart.Add(mimePart);
      }
      message.Body = multipart;
    }
    else
    {
      message.Body = htmlBody;
    }

    using var client = new SmtpClient();
    var secureOption = settings.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
    await client.ConnectAsync(settings.SmtpServer, settings.Port, secureOption, cancellationToken);

    if (!string.IsNullOrEmpty(settings.Username))
      await client.AuthenticateAsync(settings.Username, settings.Password, cancellationToken);

    await client.SendAsync(message, cancellationToken);
    await client.DisconnectAsync(true, cancellationToken);

    _logger.LogInformation("EMAIL: Sent '{Subject}' to {Email}", subject, toEmail);
  }
}

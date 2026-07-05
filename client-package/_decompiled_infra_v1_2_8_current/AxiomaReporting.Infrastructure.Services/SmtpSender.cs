using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
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

	public async Task SendRenderedAsync(string toEmail, string toName, string subject, string bodyHtml, IReadOnlyList<EmailAttachment>? attachments = null, CancellationToken cancellationToken = default(CancellationToken))
	{
		if (string.IsNullOrWhiteSpace(toEmail))
		{
			_logger.LogWarning("EMAIL: No email address for recipient {Name}, skipping", toName);
			return;
		}
		EmailServerSetting settings = await _db.EmailServerSettings.FirstOrDefaultAsync(cancellationToken);
		if (settings == null)
		{
			_logger.LogWarning("EMAIL: No SMTP settings configured");
			return;
		}
		MimeMessage message = new MimeMessage
		{
			From = { (InternetAddress)new MailboxAddress(settings.FromName ?? "Axioma", settings.FromAddress) },
			To = { (InternetAddress)new MailboxAddress(toName, toEmail) },
			Subject = subject
		};
		TextPart textPart = new TextPart("html")
		{
			Text = bodyHtml
		};
		if (attachments != null && attachments.Count > 0)
		{
			Multipart multipart = new Multipart("mixed") { textPart };
			foreach (EmailAttachment attachment in attachments)
			{
				string[] array = attachment.MimeType.Split('/', 2);
				string mediaType = ((array.Length == 2) ? array[0] : "application");
				string mediaSubtype = ((array.Length == 2) ? array[1] : "octet-stream");
				MimePart entity = new MimePart(mediaType, mediaSubtype)
				{
					Content = new MimeContent(new MemoryStream(attachment.Content)),
					ContentDisposition = new ContentDisposition("attachment"),
					ContentTransferEncoding = ContentEncoding.Base64,
					FileName = attachment.FileName
				};
				multipart.Add(entity);
			}
			message.Body = multipart;
		}
		else
		{
			message.Body = textPart;
		}
		using SmtpClient client = new SmtpClient();
		SecureSocketOptions options = (settings.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
		await client.ConnectAsync(settings.SmtpServer, settings.Port, options, cancellationToken);
		if (!string.IsNullOrEmpty(settings.Username))
		{
			await client.AuthenticateAsync(settings.Username, settings.Password, cancellationToken);
		}
		await client.SendAsync(message, cancellationToken);
		await client.DisconnectAsync(quit: true, cancellationToken);
		_logger.LogInformation("EMAIL: Sent '{Subject}' to {Email}", subject, toEmail);
	}
}

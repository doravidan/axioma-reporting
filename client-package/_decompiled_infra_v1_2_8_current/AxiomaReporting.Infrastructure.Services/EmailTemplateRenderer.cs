using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class EmailTemplateRenderer
{
	private readonly AppDbContext _db;

	public EmailTemplateRenderer(AppDbContext db)
	{
		_db = db;
	}

	public async Task<RenderedEmail?> RenderAsync(string templateType, Dictionary<string, string> tokens, CancellationToken cancellationToken = default(CancellationToken))
	{
		string templateType2 = templateType;
		EmailTemplate emailTemplate = await _db.EmailTemplates.FirstOrDefaultAsync((EmailTemplate t) => t.TypeDescription == templateType2 && t.IsActive, cancellationToken);
		if (emailTemplate == null)
		{
			return null;
		}
		string subject = ReplaceTokens(emailTemplate.Subject, tokens);
		string body = ReplaceTokens(emailTemplate.Body, tokens);
		return new RenderedEmail(subject, "<div dir='rtl'>" + FormatBodyHtml(body, tokens) + "</div>");
	}

	public static string ReplaceTokens(string template, Dictionary<string, string> tokens)
	{
		foreach (KeyValuePair<string, string> token in tokens)
		{
			token.Deconstruct(out var key, out var value);
			string text = key;
			string newValue = value;
			template = template.Replace("{" + text + "}", newValue, StringComparison.OrdinalIgnoreCase);
			template = template.Replace("{{" + text + "}}", newValue, StringComparison.OrdinalIgnoreCase);
		}
		return template;
	}

	private static string FormatBodyHtml(string body, Dictionary<string, string> tokens)
	{
		string text = WebUtility.HtmlEncode(body).Replace("\r\n", "<br>").Replace("\n", "<br>");
		foreach (var (text4, value) in tokens)
		{
			if (text4.EndsWith("Link", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(value))
			{
				string text5 = WebUtility.HtmlEncode(value);
				string newValue = $"<a href=\"{text5}\" target=\"_blank\" rel=\"noopener\">{text5}</a>";
				text = text.Replace(text5, newValue, StringComparison.OrdinalIgnoreCase);
			}
		}
		return text;
	}
}

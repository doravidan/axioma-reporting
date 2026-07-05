using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/_NotificationLogDetails.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin__NotificationLogDetails : RazorPage<NotificationLog>
{
	[RazorInject]
	public IModelExpressionProvider ModelExpressionProvider { get; private set; }

	[RazorInject]
	public IUrlHelper Url { get; private set; }

	[RazorInject]
	public IViewComponentHelper Component { get; private set; }

	[RazorInject]
	public IJsonHelper Json { get; private set; }

	[RazorInject]
	public IHtmlHelper<NotificationLog> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		WriteLiteral("\r\n<dl class=\"row mb-3\">\r\n  <dt class=\"col-sm-3\">מזהה</dt>\r\n  <dd class=\"col-sm-9\">");
		Write(base.Model.Id);
		WriteLiteral("</dd>\r\n\r\n  <dt class=\"col-sm-3\">נוצר</dt>\r\n  <dd class=\"col-sm-9\">");
		Write(base.Model.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"));
		WriteLiteral("</dd>\r\n\r\n  <dt class=\"col-sm-3\">סוג</dt>\r\n  <dd class=\"col-sm-9\">");
		Write(TypeLabel(base.Model.NotificationType));
		WriteLiteral("</dd>\n\r\n  <dt class=\"col-sm-3\">תבנית</dt>\r\n  <dd class=\"col-sm-9\">");
		Write(TemplateLabel(base.Model.TemplateType));
		WriteLiteral("</dd>\n\r\n  <dt class=\"col-sm-3\">נמען</dt>\r\n  <dd class=\"col-sm-9\">\r\n    ");
		Write(base.Model.RecipientEmail);
		WriteLiteral("\r\n");
		if (base.Model.RecipientUser != null)
		{
			WriteLiteral("      <div class=\"text-muted small\">");
			Write(base.Model.RecipientUser.FirstName);
			WriteLiteral(" ");
			Write(base.Model.RecipientUser.LastName);
			WriteLiteral("</div>\r\n");
		}
		WriteLiteral("  </dd>\r\n\r\n  <dt class=\"col-sm-3\">סטטוס</dt>\r\n  <dd class=\"col-sm-9\">");
		Write(StatusLabel(base.Model.Status));
		WriteLiteral("</dd>\n\r\n  <dt class=\"col-sm-3\">ניסיונות</dt>\r\n  <dd class=\"col-sm-9\">");
		Write(base.Model.AttemptCount);
		WriteLiteral("</dd>\r\n\r\n");
		if (base.Model.LastAttemptAt.HasValue)
		{
			WriteLiteral("    <dt class=\"col-sm-3\">ניסיון אחרון</dt>\r\n    <dd class=\"col-sm-9\">");
			Write(base.Model.LastAttemptAt.Value.ToString("dd/MM/yyyy HH:mm:ss"));
			WriteLiteral("</dd>\r\n");
		}
		WriteLiteral("\r\n");
		if (base.Model.NextRetryAt.HasValue)
		{
			WriteLiteral("    <dt class=\"col-sm-3\">ניסיון הבא</dt>\r\n    <dd class=\"col-sm-9\">");
			Write(base.Model.NextRetryAt.Value.ToString("dd/MM/yyyy HH:mm:ss"));
			WriteLiteral("</dd>\r\n");
		}
		WriteLiteral("\r\n");
		if (!string.IsNullOrWhiteSpace(base.Model.FailureReason))
		{
			WriteLiteral("    <dt class=\"col-sm-3\">שגיאה</dt>\r\n    <dd class=\"col-sm-9\"><code>");
			Write(base.Model.FailureReason);
			WriteLiteral("</code></dd>\r\n");
		}
		WriteLiteral("</dl>\r\n\r\n<div class=\"mb-3\">\r\n  <h6>נושא</h6>\r\n  <div class=\"border rounded p-2 bg-light\">");
		Write(base.Model.Subject);
		WriteLiteral("</div>\r\n</div>\r\n\r\n<div class=\"mb-3\">\r\n  <h6>תוכן</h6>\r\n  <div class=\"border rounded p-2 bg-light\" style=\"max-height: 320px; overflow: auto;\">\r\n    ");
		Write(Html.Raw(base.Model.Body));
		WriteLiteral("\r\n  </div>\r\n</div>\r\n");
		static string StatusLabel(string? value)
		{
			return value switch
			{
				"Pending" => "ממתין", 
				"Sent" => "נשלח", 
				"Failed" => "נכשל", 
				"Abandoned" => "ננטש", 
				_ => value ?? "", 
			};
		}
		static string TemplateLabel(string? value)
		{
			return value switch
			{
				"ReportReceived" => "דיווח התקבל", 
				"ReportApproved" => "דיווח אושר", 
				"ReportRejected" => "דיווח הוחזר לתיקון", 
				"ReminderNotSubmitted" => "תזכורת לדיווח שלא הוגש", 
				"ReminderNeedsCorrection" => "תזכורת לדיווח לתיקון", 
				"PasswordReset" => "איפוס סיסמה", 
				"TwoFactorCode" => "קוד אימות", 
				"PasswordExpiryWarning" => "אזהרת תפוגת סיסמה", 
				"BatchImportSuccessUploader" => "קליטת קובץ מרוכז", 
				"BatchImportErrors" => "שגיאות בקליטת קובץ", 
				_ => value ?? "", 
			};
		}
		static string TypeLabel(string? value)
		{
			return value switch
			{
				"Report" => "דיווח", 
				"Reminder" => "תזכורת", 
				"Account" => "חשבון", 
				"Excel" => "אקסל", 
				"Other" => "אחר", 
				_ => value ?? "", 
			};
		}
	}
}

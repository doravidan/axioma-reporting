using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/EmailTemplates.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_EmailTemplates : RazorPage<List<EmailTemplate>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-controller", "Lookup", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("id", new HtmlString("editForm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private TagHelperScopeManager __tagHelperScopeManager
	{
		get
		{
			if (__backed__tagHelperScopeManager == null)
			{
				__backed__tagHelperScopeManager = new TagHelperScopeManager(base.StartTagHelperWritingScope, base.EndTagHelperWritingScope);
			}
			return __backed__tagHelperScopeManager;
		}
	}

	[RazorInject]
	public IModelExpressionProvider ModelExpressionProvider { get; private set; }

	[RazorInject]
	public IUrlHelper Url { get; private set; }

	[RazorInject]
	public IViewComponentHelper Component { get; private set; }

	[RazorInject]
	public IJsonHelper Json { get; private set; }

	[RazorInject]
	public IHtmlHelper<List<EmailTemplate>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "תבניות מייל";
		WriteLiteral("<div class=\"container-fluid mt-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>תבניות מייל</h3>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "7b69be2685dde4e47696724a8d28c28bf5fc9f0bdd8f2ceaee2534dbdc6b49006899", async delegate
		{
			WriteLiteral("חזרה לטבלאות עזר");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n  </div>\r\n\r\n");
		if (base.TempData["Success"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n      ");
			Write(base.TempData["Success"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n");
		foreach (EmailTemplate item in base.Model)
		{
			WriteLiteral("    <div class=\"card mb-3\">\r\n      <div class=\"card-header d-flex justify-content-between align-items-center\">\r\n        <strong>");
			Write(TemplateLabel(item.TypeDescription));
			WriteLiteral("</strong>\n        <button type=\"button\" class=\"btn btn-sm btn-outline-secondary btn-edit\"\r\n                data-id=\"");
			Write(item.Id);
			WriteLiteral("\"\r\n                data-type=\"");
			Write(TemplateLabel(item.TypeDescription));
			WriteLiteral("\"\n                data-subject=\"");
			Write(item.Subject);
			WriteLiteral("\"\r\n                data-body=\"");
			Write(item.Body);
			WriteLiteral("\">\r\n          ✏\ufe0f עריכה\r\n        </button>\r\n      </div>\r\n      <div class=\"card-body\">\r\n        <div class=\"mb-2\">\r\n          <strong>נושא:</strong> ");
			Write(item.Subject);
			WriteLiteral("\r\n        </div>\r\n        <div>\r\n          <strong>גוף:</strong>\r\n          <pre class=\"bg-light p-2 rounded small\" style=\"white-space:pre-wrap\">");
			Write(DisplayTemplateBody(item.Body));
			WriteLiteral("</pre>\n        </div>\r\n      </div>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n");
		if (!base.Model.Any())
		{
			WriteLiteral("    <div class=\"alert alert-info\">אין תבניות מוגדרות</div>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n<!-- Edit Modal -->\r\n<div class=\"modal fade\" id=\"editModal\" tabindex=\"-1\" aria-labelledby=\"editModalTitle\" aria-hidden=\"true\">\r\n  <div class=\"modal-dialog modal-lg\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"editModalTitle\">עריכת תבנית: <span id=\"editTypeLabel\"></span></h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "7b69be2685dde4e47696724a8d28c28bf5fc9f0bdd8f2ceaee2534dbdc6b490013464", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"modal-body\">\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">נושא <span class=\"text-danger\">*</span></label>\r\n            <input id=\"editSubject\" name=\"subject\" class=\"form-control\" required />\r\n          </div>\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">גוף ההודעה <span class=\"text-danger\">*</span></label>\r\n            <textarea id=\"editBody\" name=\"body\" class=\"form-control\" rows=\"10\" required></textarea>\r\n            <div class=\"form-text text-muted\">ניתן להשתמש ב-HTML ובמשתני תבנית</div>\r\n          </div>\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n          <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n          <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n        </div>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<script>\r\n  document.querySelectorAll('.btn-edit').forEach(function (btn) {\r\n    btn.addEventListener('click', function () {\r\n      document.getElementById('editTypeLabel').textContent = btn.getAttribute('data-type');\r\n      document.getElementById('editSubject').value = btn.getAttribute('data-subject');\r\n      document.getElementById('editBody').value = btn.getAttribute('data-body');\r\n      document.getElementById('editForm').action = '/Admin/UpdateEmailTemplate/' + btn.getAttribute('data-id');\r\n      new bootstrap.Modal(document.getElementById('editModal')).show();\r\n    });\r\n  });\r\n</script>\r\n");
		static string DisplayTemplateBody(string? value)
		{
			string text = value ?? "";
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				["{{EmployeeName}}"] = "{{שם עובד}}",
				["{{UploaderName}}"] = "{{שם מעלה הקובץ}}",
				["{{Month}}"] = "{{חודש}}",
				["{{Year}}"] = "{{שנה}}",
				["{{RejectionReason}}"] = "{{סיבת החזרה}}",
				["{{Deadline}}"] = "{{מועד אחרון}}",
				["{{ResetLink}}"] = "{{קישור איפוס}}",
				["{{Code}}"] = "{{קוד}}",
				["{{Minutes}}"] = "{{דקות}}",
				["{{DaysLeft}}"] = "{{ימים שנותרו}}",
				["{{ExpiryDate}}"] = "{{תאריך תפוגה}}",
				["{{RowsImported}}"] = "{{שורות שנקלטו}}",
				["{{EmployeesCount}}"] = "{{מספר עובדים}}",
				["{{ErrorsCount}}"] = "{{מספר שגיאות}}",
				["{{ErrorList}}"] = "{{רשימת שגיאות}}"
			};
			foreach (KeyValuePair<string, string> item2 in dictionary)
			{
				text = text.Replace(item2.Key, item2.Value);
			}
			return text;
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
	}
}

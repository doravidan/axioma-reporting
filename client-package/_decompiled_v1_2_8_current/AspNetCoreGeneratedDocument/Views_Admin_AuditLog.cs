using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AxiomaReporting.Web.Models;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/AuditLog.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_AuditLog : RazorPage<AuditLogListViewModel>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "ExportAuditLog", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-primary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "AuditLog", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("method", "get", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("class", new HtmlString("card card-body mb-3"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<AuditLogListViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "יומן ביקורת";
		WriteLiteral("<div class=\"container-fluid mt-3\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>יומן ביקורת</h3>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "687931f4f12c266cc20497f880a62a2a84967bc973b50017f110c0533f1b78d77217", async delegate
		{
			WriteLiteral("ייצוא ל-CSV");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-action", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(base.Model.Action);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["action"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["action"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(base.Model.EntityType);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["entityType"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-entityType", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["entityType"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(base.Model.EntityId);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["entityId"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-entityId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["entityId"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(base.Model.ActorUserId);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["actorUserId"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-actorUserId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["actorUserId"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(base.Model.FromDate?.ToString("yyyy-MM-dd"));
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fromDate"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fromDate", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fromDate"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(base.Model.ToDate?.ToString("yyyy-MM-dd"));
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["toDate"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-toDate", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["toDate"], HtmlAttributeValueStyle.DoubleQuotes);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n  </div>\r\n\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "687931f4f12c266cc20497f880a62a2a84967bc973b50017f110c0533f1b78d713151", async delegate
		{
			WriteLiteral("\r\n    <div class=\"row g-2\">\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-action\">פעולה מכילה</label>\r\n        <input id=\"f-action\" name=\"action\"");
			BeginWriteAttribute("value", " value=\"", 2301, "\"", 2322, 1);
			WriteAttributeValue("", 2309, base.Model.Action, 2309, 13, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-entityType\">סוג ישות</label>\r\n        <input id=\"f-entityType\" name=\"entityType\"");
			BeginWriteAttribute("value", " value=\"", 2530, "\"", 2555, 1);
			WriteAttributeValue("", 2538, base.Model.EntityType, 2538, 17, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-entityId\">מזהה ישות</label>\r\n        <input id=\"f-entityId\" name=\"entityId\"");
			BeginWriteAttribute("value", " value=\"", 2758, "\"", 2781, 1);
			WriteAttributeValue("", 2766, base.Model.EntityId, 2766, 15, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-actor\">מזהה משתמש</label>\r\n        <input id=\"f-actor\" type=\"number\" name=\"actorUserId\"");
			BeginWriteAttribute("value", " value=\"", 2996, "\"", 3022, 1);
			WriteAttributeValue("", 3004, base.Model.ActorUserId, 3004, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-from\">מתאריך</label>\r\n        <input id=\"f-from\" type=\"date\" name=\"fromDate\"");
			BeginWriteAttribute("value", " value=\"", 3226, "\"", 3275, 1);
			WriteAttributeValue("", 3234, base.Model.FromDate?.ToString("yyyy-MM-dd"), 3234, 41, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-to\">עד תאריך</label>\r\n        <input id=\"f-to\" type=\"date\" name=\"toDate\"");
			BeginWriteAttribute("value", " value=\"", 3475, "\"", 3522, 1);
			WriteAttributeValue("", 3483, base.Model.ToDate?.ToString("yyyy-MM-dd"), 3483, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n      </div>\r\n    </div>\r\n    <div class=\"mt-2 d-flex gap-2\">\r\n      <button type=\"submit\" class=\"btn btn-primary btn-sm\">סנן</button>\r\n      ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "687931f4f12c266cc20497f880a62a2a84967bc973b50017f110c0533f1b78d717530", async delegate
			{
				WriteLiteral("נקה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n\r\n  <div class=\"card\">\r\n    <div class=\"card-body p-0\">\r\n      <table class=\"table table-hover table-bordered mb-0\" aria-label=\"רשומות יומן ביקורת\">\r\n        <thead class=\"table-light\">\r\n          <tr>\r\n            <th scope=\"col\">זמן</th>\r\n            <th scope=\"col\">משתמש</th>\r\n            <th scope=\"col\">פעולה</th>\r\n            <th scope=\"col\">סוג ישות</th>\r\n            <th scope=\"col\">מזהה</th>\r\n            <th scope=\"col\">הערות</th>\r\n            <th scope=\"col\">IP</th>\r\n            <th scope=\"col\" style=\"width:90px\"></th>\r\n          </tr>\r\n        </thead>\r\n        <tbody>\r\n");
		if (!base.Model.Items.Any())
		{
			WriteLiteral("            <tr><td colspan=\"8\" class=\"text-center text-muted py-4\">אין רשומות</td></tr>\r\n");
		}
		foreach (AuditLogListItem item in base.Model.Items)
		{
			WriteLiteral("            <tr>\r\n              <td>");
			Write(item.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
			WriteLiteral("</td>\r\n              <td>\r\n");
			if (item.ActorUserId.HasValue)
			{
				WriteLiteral("                  <span>");
				Write(item.ActorUserId);
				WriteLiteral(" (");
				Write(item.ActorName);
				WriteLiteral(")</span>\r\n");
			}
			else
			{
				WriteLiteral("                  <span class=\"text-muted\">—</span>\r\n");
			}
			WriteLiteral("              </td>\r\n              <td>");
			Write(ActionLabel(item.Action));
			WriteLiteral("</td>\n              <td>");
			Write(EntityLabel(item.EntityType));
			WriteLiteral("</td>\n              <td>");
			Write(EntityIdLabel(item.EntityId));
			WriteLiteral("</td>\n              <td class=\"small text-muted\">");
			Write(NotesLabel(item.Notes));
			WriteLiteral("</td>\n              <td class=\"small\">");
			Write(item.IpAddress);
			WriteLiteral("</td>\r\n              <td>\r\n                <button type=\"button\" class=\"btn btn-sm btn-outline-secondary btn-details\"\r\n                        data-before=\"");
			Write(item.Before);
			WriteLiteral("\" data-after=\"");
			Write(item.After);
			WriteLiteral("\"\r\n                        data-action-text=\"");
			Write(ActionLabel(item.Action));
			WriteLiteral("\" data-entity=\"");
			Write(EntityLabel(item.EntityType) + " " + EntityIdLabel(item.EntityId));
			WriteLiteral("\"\n                        aria-label=\"הצג פרטים\">פרטים</button>\r\n              </td>\r\n            </tr>\r\n");
		}
		WriteLiteral("        </tbody>\r\n      </table>\r\n    </div>\r\n  </div>\r\n\r\n");
		if (base.Model.TotalPages > 1)
		{
			WriteLiteral("    <nav aria-label=\"ניווט עמודים\" class=\"mt-3\">\r\n      <ul class=\"pagination justify-content-center\">\r\n");
			for (int i = 1; i <= base.Model.TotalPages; i++)
			{
				WriteLiteral("          <li");
				BeginWriteAttribute("class", " class=\"", 6014, "\"", 6066, 2);
				WriteAttributeValue("", 6022, "page-item", 6022, 9, isLiteral: true);
				WriteAttributeValue(" ", 6031, (i == base.Model.Page) ? "active" : "", 6032, 34, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">\r\n            <a class=\"page-link\"");
				BeginWriteAttribute("href", " href=\"", 6102, "\"", 6374, 16);
				WriteAttributeValue("", 6109, "?page=", 6109, 6, isLiteral: true);
				WriteAttributeValue("", 6115, i, 6115, 2, isLiteral: false);
				WriteAttributeValue("", 6117, "&amp;pageSize=", 6117, 14, isLiteral: true);
				WriteAttributeValue("", 6131, base.Model.PageSize, 6131, 15, isLiteral: false);
				WriteAttributeValue("", 6146, "&amp;action=", 6146, 12, isLiteral: true);
				WriteAttributeValue("", 6158, base.Model.Action, 6158, 13, isLiteral: false);
				WriteAttributeValue("", 6171, "&amp;entityType=", 6171, 16, isLiteral: true);
				WriteAttributeValue("", 6187, base.Model.EntityType, 6187, 17, isLiteral: false);
				WriteAttributeValue("", 6204, "&amp;entityId=", 6204, 14, isLiteral: true);
				WriteAttributeValue("", 6218, base.Model.EntityId, 6218, 15, isLiteral: false);
				WriteAttributeValue("", 6233, "&amp;actorUserId=", 6233, 17, isLiteral: true);
				WriteAttributeValue("", 6250, base.Model.ActorUserId, 6250, 18, isLiteral: false);
				WriteAttributeValue("", 6268, "&amp;fromDate=", 6268, 14, isLiteral: true);
				WriteAttributeValue("", 6282, base.Model.FromDate?.ToString("yyyy-MM-dd"), 6282, 41, isLiteral: false);
				WriteAttributeValue("", 6323, "&amp;toDate=", 6323, 12, isLiteral: true);
				WriteAttributeValue("", 6335, base.Model.ToDate?.ToString("yyyy-MM-dd"), 6335, 39, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">");
				Write(i);
				WriteLiteral("</a>\r\n          </li>\r\n");
			}
			WriteLiteral("      </ul>\r\n    </nav>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n<!-- Details Modal -->\r\n<div class=\"modal fade\" id=\"detailsModal\" tabindex=\"-1\" aria-labelledby=\"detailsModalTitle\" aria-hidden=\"true\" dir=\"rtl\">\r\n  <div class=\"modal-dialog modal-lg\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"detailsModalTitle\">פרטי ביקורת</h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      <div class=\"modal-body\">\r\n        <p><strong>פעולה:</strong> <span id=\"modalAction\"></span></p>\r\n        <p><strong>ישות:</strong> <span id=\"modalEntity\"></span></p>\r\n        <div class=\"row\">\r\n          <div class=\"col-md-6\">\r\n            <h6>לפני</h6>\r\n            <pre id=\"modalBefore\" class=\"border rounded p-2 bg-light\" style=\"direction:ltr;white-space:pre-wrap;max-height:400px;overflow:auto\"></pre>\r\n          </div>\r\n          <div class=\"col-md-6\">\r\n            <h6>אחרי</h6>\r\n            <pre id=\"modalAfter\" class=\"bord");
		WriteLiteral("er rounded p-2 bg-light\" style=\"direction:ltr;white-space:pre-wrap;max-height:400px;overflow:auto\"></pre>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <div class=\"modal-footer\">\r\n        <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">סגור</button>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<script>\r\n  (function () {\r\n    function pretty(json) {\r\n      if (!json) return '—';\r\n      try { return JSON.stringify(JSON.parse(json), null, 2); }\r\n      catch (e) { return json; }\r\n    }\r\n    document.querySelectorAll('.btn-details').forEach(function (btn) {\r\n      btn.addEventListener('click', function () {\r\n        document.getElementById('modalAction').textContent = btn.getAttribute('data-action-text') || '';\r\n        document.getElementById('modalEntity').textContent = btn.getAttribute('data-entity') || '';\r\n        document.getElementById('modalBefore').textContent = pretty(btn.getAttribute('data-before'));\r\n        document.getElementById('modalAfter').textContent = pretty(b");
		WriteLiteral("tn.getAttribute('data-after'));\r\n        new bootstrap.Modal(document.getElementById('detailsModal')).show();\r\n      });\r\n    });\r\n  })();\r\n</script>\r\n");
		static string ActionLabel(string? value)
		{
			return value switch
			{
				"Auth.LoginSucceeded" => "כניסה הצליחה", 
				"Auth.LoginFailed" => "כניסה נכשלה", 
				"Auth.Lockout" => "נעילת חשבון", 
				"Auth.Unlock" => "שחרור נעילה", 
				"User.PasswordReset" => "איפוס סיסמה", 
				"User.PasswordChanged" => "שינוי סיסמה", 
				"Employee.Create" => "יצירת עובד", 
				"Employee.Update" => "עדכון עובד", 
				"Employee.Deactivate" => "השבתת עובד", 
				"Employee.Reactivate" => "הפעלת עובד", 
				"Allocation.Create" => "יצירת הקצאה", 
				"Allocation.Update" => "עדכון הקצאה", 
				"Allocation.Delete" => "מחיקת הקצאה", 
				"Report.StatusChange" => "שינוי סטטוס דיווח", 
				"Terms.Accept" => "אישור תנאי שימוש", 
				_ => string.IsNullOrWhiteSpace(value) ? "" : "פעולת מערכת", 
			};
		}
		static string EntityIdLabel(string? value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				if (!Regex.IsMatch(value, "[A-Za-z]"))
				{
					return value;
				}
				return "מזהה מערכת";
			}
			return "";
		}
		static string EntityLabel(string? value)
		{
			return value switch
			{
				"User" => "משתמש", 
				"Allocation" => "הקצאה", 
				"Report" => "דיווח", 
				"ReportRow" => "שורת דיווח", 
				"TermsOfUseVersion" => "גרסת תנאי שימוש", 
				_ => string.IsNullOrWhiteSpace(value) ? "" : "ישות מערכת", 
			};
		}
		static string NotesLabel(string? value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				if (!Regex.IsMatch(value, "[A-Za-z]"))
				{
					return value;
				}
				return "מידע מערכת";
			}
			return "";
		}
	}
}

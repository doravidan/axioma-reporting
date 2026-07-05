using System;
using System.Linq;
using System.Runtime.CompilerServices;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/NotificationLogs.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_NotificationLogs : RazorPage<NotificationLogListViewModel>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "NotificationLogs", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("method", "get", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("row g-2 align-items-end"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("asp-action", "ResendNotification", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("class", new HtmlString("d-inline"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("class", new HtmlString("page-link"), HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

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
	public IHtmlHelper<NotificationLogListViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "יומן התראות";
		string[] types = new string[6] { "", "Report", "Reminder", "Account", "Excel", "Other" };
		string[] statuses = new string[5] { "", "Pending", "Sent", "Failed", "Abandoned" };
		WriteLiteral("<div class=\"container-fluid mt-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>יומן התראות — שליחת מיילים</h3>\r\n  </div>\r\n\r\n  <div class=\"card mb-3\">\r\n    <div class=\"card-body\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "6c3f2daa737e24521763f97ed1d2e3eb9c205a7ef5fed7c0959b017c15fde8407900", async delegate
		{
			WriteLiteral("\r\n        <div class=\"col-md-2\">\r\n          <label class=\"form-label\" for=\"type\">סוג</label>\r\n          <select id=\"type\" name=\"type\" class=\"form-select form-select-sm\">\r\n");
			string[] array = types;
			foreach (string t in array)
			{
				WriteLiteral("              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "6c3f2daa737e24521763f97ed1d2e3eb9c205a7ef5fed7c0959b017c15fde8408634", async delegate
				{
					Write(TypeLabel(t));
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(t);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 1854, base.Model.Type == t, 1854, 18, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\n");
			}
			WriteLiteral("          </select>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <label class=\"form-label\" for=\"status\">סטטוס</label>\r\n          <select id=\"status\" name=\"status\" class=\"form-select form-select-sm\">\r\n");
			array = statuses;
			foreach (string s in array)
			{
				WriteLiteral("              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "6c3f2daa737e24521763f97ed1d2e3eb9c205a7ef5fed7c0959b017c15fde84011804", async delegate
				{
					Write(StatusLabel(s));
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(s);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 2226, base.Model.Status == s, 2226, 20, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\n");
			}
			WriteLiteral("          </select>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <label class=\"form-label\" for=\"templateType\">תבנית</label>\r\n          <input id=\"templateType\" name=\"templateType\"");
			BeginWriteAttribute("value", " value=\"", 2481, "\"", 2508, 1);
			WriteAttributeValue("", 2489, base.Model.TemplateType, 2489, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <label class=\"form-label\" for=\"fromDate\">מתאריך</label>\r\n          <input id=\"fromDate\" name=\"fromDate\" type=\"date\"");
			BeginWriteAttribute("value", " value=\"", 2724, "\"", 2773, 1);
			WriteAttributeValue("", 2732, base.Model.FromDate?.ToString("yyyy-MM-dd"), 2732, 41, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <label class=\"form-label\" for=\"toDate\">עד תאריך</label>\r\n          <input id=\"toDate\" name=\"toDate\" type=\"date\"");
			BeginWriteAttribute("value", " value=\"", 2985, "\"", 3032, 1);
			WriteAttributeValue("", 2993, base.Model.ToDate?.ToString("yyyy-MM-dd"), 2993, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <label class=\"form-label\" for=\"recipientEmail\">נמען (מייל)</label>\r\n          <input id=\"recipientEmail\" name=\"recipientEmail\"");
			BeginWriteAttribute("value", " value=\"", 3259, "\"", 3288, 1);
			WriteAttributeValue("", 3267, base.Model.RecipientEmail, 3267, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <label class=\"form-label\" for=\"userId\">מזהה משתמש</label>\r\n          <input id=\"userId\" name=\"userId\" type=\"number\"");
			BeginWriteAttribute("value", " value=\"", 3504, "\"", 3525, 1);
			WriteAttributeValue("", 3512, base.Model.UserId, 3512, 13, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control form-control-sm\" />\r\n        </div>\r\n        <div class=\"col-md-2 d-flex gap-2\">\r\n          <button type=\"submit\" class=\"btn btn-primary btn-sm\">סנן</button>\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "6c3f2daa737e24521763f97ed1d2e3eb9c205a7ef5fed7c0959b017c15fde84018046", async delegate
			{
				WriteLiteral("איפוס");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        </div>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n\r\n  <div class=\"card\">\r\n    <div class=\"card-body p-0\">\r\n      <table class=\"table table-hover table-bordered mb-0 small\">\r\n        <thead class=\"table-light\">\r\n          <tr>\r\n            <th scope=\"col\">נוצר</th>\r\n            <th scope=\"col\">סוג</th>\r\n            <th scope=\"col\">תבנית</th>\r\n            <th scope=\"col\">נמען</th>\r\n            <th scope=\"col\">נושא</th>\r\n            <th scope=\"col\">סטטוס</th>\r\n            <th scope=\"col\">ניסיונות</th>\r\n            <th scope=\"col\">ניסיון אחרון</th>\r\n            <th scope=\"col\">ניסיון הבא</th>\r\n            <th scope=\"col\">שגיאה</th>\r\n            <th scope=\"col\">פעולות</th>\r\n          </tr>\r\n        </thead>\r\n        <tbody>\r\n");
		if (!base.Model.Items.Any())
		{
			WriteLiteral("            <tr><td colspan=\"11\" class=\"text-center text-muted py-4\">אין רשומות להצגה</td></tr>\r\n");
		}
		foreach (NotificationLogListItem item in base.Model.Items)
		{
			WriteLiteral("            <tr>\r\n              <td>");
			Write(item.CreatedAt.ToString("dd/MM/yyyy HH:mm"));
			WriteLiteral("</td>\r\n              <td>");
			Write(TypeLabel(item.NotificationType));
			WriteLiteral("</td>\n              <td>");
			Write(TemplateLabel(item.TemplateType));
			WriteLiteral("</td>\n              <td>\r\n                ");
			Write(item.RecipientEmail);
			WriteLiteral("\r\n");
			if (!string.IsNullOrWhiteSpace(item.RecipientName))
			{
				WriteLiteral("                  <div class=\"text-muted small\">");
				Write(item.RecipientName);
				WriteLiteral("</div>\r\n");
			}
			WriteLiteral("              </td>\r\n              <td>");
			Write(item.Subject);
			WriteLiteral("</td>\r\n              <td>\r\n");
			switch (item.Status)
			{
			case "Sent":
				WriteLiteral("                    <span class=\"badge bg-success\">נשלח</span>\r\n");
				break;
			case "Failed":
				WriteLiteral("                    <span class=\"badge bg-warning text-dark\">נכשל</span>\r\n");
				break;
			case "Abandoned":
				WriteLiteral("                    <span class=\"badge bg-danger\">ננטש</span>\r\n");
				break;
			case "Pending":
				WriteLiteral("                    <span class=\"badge bg-secondary\">ממתין</span>\r\n");
				break;
			default:
				WriteLiteral("                    <span class=\"badge bg-light text-dark\">");
				Write(StatusLabel(item.Status));
				WriteLiteral("</span>\n");
				break;
			}
			WriteLiteral("              </td>\r\n              <td>");
			Write(item.AttemptCount);
			WriteLiteral("</td>\r\n              <td>");
			Write(item.LastAttemptAt?.ToString("dd/MM/yyyy HH:mm"));
			WriteLiteral("</td>\r\n              <td>");
			Write(item.NextRetryAt?.ToString("dd/MM/yyyy HH:mm"));
			WriteLiteral("</td>\r\n              <td class=\"text-truncate\" style=\"max-width: 220px;\"");
			BeginWriteAttribute("title", " title=\"", 6269, "\"", 6294, 1);
			WriteAttributeValue("", 6277, item.FailureReason, 6277, 17, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">");
			Write(item.FailureReason);
			WriteLiteral("</td>\r\n              <td>\r\n                <button type=\"button\" class=\"btn btn-sm btn-outline-primary\" data-bs-toggle=\"modal\"\r\n                        data-bs-target=\"#detailsModal\" data-id=\"");
			Write(item.Id);
			WriteLiteral("\" aria-label=\"הצג פרטי התראה\">\r\n                  פרטים\r\n                </button>\r\n");
			if (item.Status == "Failed" || item.Status == "Abandoned" || item.Status == "Sent")
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "6c3f2daa737e24521763f97ed1d2e3eb9c205a7ef5fed7c0959b017c15fde84029559", async delegate
				{
					WriteLiteral("\r\n                    ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n                    <button type=\"submit\" class=\"btn btn-sm btn-outline-warning\"\r\n                            onclick=\"return confirm('לשלוח את ההתראה שוב?')\">שלח שוב</button>\r\n                  ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(item.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			WriteLiteral("              </td>\r\n            </tr>\r\n");
		}
		WriteLiteral("        </tbody>\r\n      </table>\r\n    </div>\r\n  </div>\r\n\r\n");
		if (base.Model.TotalPages > 1)
		{
			WriteLiteral("    <nav aria-label=\"ניווט עמודים\" class=\"mt-3\">\r\n      <ul class=\"pagination pagination-sm\">\r\n");
			int p;
			for (p = 1; p <= base.Model.TotalPages; p++)
			{
				WriteLiteral("          <li");
				BeginWriteAttribute("class", " class=\"", 7409, "\"", 7461, 2);
				WriteAttributeValue("", 7417, "page-item", 7417, 9, isLiteral: true);
				WriteAttributeValue(" ", 7426, (p == base.Model.Page) ? "active" : "", 7427, 34, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">\r\n            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "6c3f2daa737e24521763f97ed1d2e3eb9c205a7ef5fed7c0959b017c15fde84034494", async delegate
				{
					Write(p);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-page", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(p);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["page"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-page", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["page"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.Model.PageSize);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageSize"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageSize", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageSize"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.Model.Type);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["type"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-type", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["type"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.Model.Status);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["status"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-status", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["status"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.Model.TemplateType);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["templateType"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-templateType", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["templateType"], HtmlAttributeValueStyle.DoubleQuotes);
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
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.Model.RecipientEmail);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["recipientEmail"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-recipientEmail", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["recipientEmail"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.Model.UserId);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["userId"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-userId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["userId"], HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n          </li>\r\n");
			}
			WriteLiteral("      </ul>\r\n    </nav>\r\n");
		}
		WriteLiteral("\r\n  <div class=\"text-muted small mt-2\">סה\"כ: ");
		Write(base.Model.TotalCount);
		WriteLiteral(" רשומות</div>\r\n</div>\r\n\r\n<div class=\"modal fade\" id=\"detailsModal\" tabindex=\"-1\" aria-labelledby=\"detailsModalTitle\" aria-hidden=\"true\">\r\n  <div class=\"modal-dialog modal-lg\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"detailsModalTitle\">פרטי התראה</h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      <div class=\"modal-body\" id=\"detailsModalBody\">\r\n        <div class=\"text-center text-muted py-4\">טוען...</div>\r\n      </div>\r\n      <div class=\"modal-footer\">\r\n        <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">סגור</button>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n");
		DefineSection("Scripts", (RenderAsyncDelegate)async delegate
		{
			WriteLiteral("\r\n  <script>\r\n    document.getElementById('detailsModal').addEventListener('show.bs.modal', function (event) {\r\n      var button = event.relatedTarget;\r\n      var id = button.getAttribute('data-id');\r\n      var body = document.getElementById('detailsModalBody');\r\n      body.innerHTML = '<div class=\"text-center text-muted py-4\">טוען...</div>';\r\n      fetch('");
			Write(Url.Action("NotificationLogDetails", "Admin"));
			WriteLiteral("?id=' + encodeURIComponent(id))\r\n        .then(r => r.text())\r\n        .then(html => { body.innerHTML = html; })\r\n        .catch(() => { body.innerHTML = '<div class=\"alert alert-danger\">שגיאה בטעינת פרטים</div>'; });\r\n    });\r\n  </script>\r\n");
		});
		static string StatusLabel(string? value)
		{
			return value switch
			{
				"Pending" => "ממתין", 
				"Sent" => "נשלח", 
				"Failed" => "נכשל", 
				"Abandoned" => "ננטש", 
				_ => string.IsNullOrWhiteSpace(value) ? "כל הסטטוסים" : value, 
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
				_ => string.IsNullOrWhiteSpace(value) ? "כל הסוגים" : value, 
			};
		}
	}
}

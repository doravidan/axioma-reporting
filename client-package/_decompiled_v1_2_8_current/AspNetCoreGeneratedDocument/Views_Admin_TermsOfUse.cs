using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Web.Models;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/TermsOfUse.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_TermsOfUse : RazorPage<List<TermsOfUseVersionListItem>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "PublishTermsOfUse", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

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
	public IHtmlHelper<List<TermsOfUseVersionListItem>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "תנאי שימוש";
		WriteLiteral("<div class=\"container-fluid mt-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>ניהול גרסאות תנאי שימוש</h3>\r\n  </div>\r\n\r\n  <div class=\"card mb-4\">\r\n    <div class=\"card-header\">\r\n      <h5 class=\"mb-0\">פרסום גרסה חדשה</h5>\r\n    </div>\r\n    <div class=\"card-body\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "425c37c0cc2bc797e31ee872411edffa8860edc26d996df0b402b2e8430edfe64465", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"mb-3\">\r\n          <label for=\"effectiveFrom\" class=\"form-label\">תוקף מתאריך</label>\r\n          <input type=\"date\" id=\"effectiveFrom\" name=\"effectiveFrom\" class=\"form-control\"");
			BeginWriteAttribute("value", "\r\n                 value=\"", 700, "\"", 764, 1);
			WriteAttributeValue("", 726, DateTime.Today.ToString("yyyy-MM-dd"), 726, 38, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n        </div>\r\n        <div class=\"mb-3\">\r\n          <label for=\"bodyHtml\" class=\"form-label\">תוכן תנאי השימוש <span class=\"text-danger\">*</span></label>\r\n          <textarea id=\"bodyHtml\" name=\"bodyHtml\" class=\"form-control\" rows=\"12\"\r\n                    aria-required=\"true\" required></textarea>\r\n          <div class=\"form-text\">התוכן מוצג כטקסט בטוח. פרסום גרסה חדשה יחייב את כל המשתמשים לאשר שוב בכניסה הבאה.</div>\r\n        </div>\r\n        <button type=\"submit\" class=\"btn btn-primary\">פרסם גרסה חדשה</button>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n\r\n  <div class=\"card\">\r\n    <div class=\"card-header\">\r\n      <h5 class=\"mb-0\">היסטוריית גרסאות</h5>\r\n    </div>\r\n    <div class=\"card-body p-0\">\r\n      <table class=\"table table-hover table-bordered mb-0\">\r\n        <thead class=\"table-light\">\r\n          <tr>\r\n            <th scope=\"col\" style=\"width:80px\">גרסה</th>\r\n            <th scope=\"col\">תוקף מ-</th>\r\n            <th scope=\"col\">נוצר בתאריך</th>\r\n            <th scope=\"col\">פורסם על ידי</th>\r\n            <th scope=\"col\" style=\"width:120px\">אישורים</th>\r\n            <th scope=\"col\">תוכן</th>\r\n          </tr>\r\n        </thead>\r\n        <tbody>\r\n");
		if (!base.Model.Any())
		{
			WriteLiteral("            <tr><td colspan=\"6\" class=\"text-center text-muted py-4\">אין גרסאות</td></tr>\r\n");
		}
		foreach (TermsOfUseVersionListItem item in base.Model)
		{
			WriteLiteral("            <tr>\r\n              <td>");
			Write(item.VersionNumber);
			WriteLiteral("</td>\r\n              <td>");
			Write(item.EffectiveFrom.ToString("dd/MM/yyyy"));
			WriteLiteral("</td>\r\n              <td>");
			Write(item.CreatedAt.ToString("dd/MM/yyyy HH:mm"));
			WriteLiteral("</td>\r\n              <td>");
			Write(item.PublishedByName);
			WriteLiteral("</td>\r\n              <td>");
			Write(item.AcceptanceCount);
			WriteLiteral("</td>\r\n              <td>\r\n                <button type=\"button\" class=\"btn btn-sm btn-outline-secondary btn-preview\"\r\n                        data-body=\"");
			Write(item.BodyHtml);
			WriteLiteral("\"\r\n                        data-version=\"");
			Write(item.VersionNumber);
			WriteLiteral("\">\r\n                  הצג\r\n                </button>\r\n              </td>\r\n            </tr>\r\n");
		}
		WriteLiteral("        </tbody>\r\n      </table>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<div class=\"modal fade\" id=\"previewModal\" tabindex=\"-1\" aria-labelledby=\"previewModalTitle\" aria-hidden=\"true\">\r\n  <div class=\"modal-dialog modal-lg\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"previewModalTitle\">תצוגת גרסה</h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      <div class=\"modal-body\" id=\"previewBody\" style=\"white-space:pre-wrap;\"></div>\r\n      <div class=\"modal-footer\">\r\n        <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">סגור</button>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<script>\r\n  document.querySelectorAll('.btn-preview').forEach(function (btn) {\r\n    btn.addEventListener('click', function () {\r\n      document.getElementById('previewModalTitle').textContent = 'תצוגת גרסה ' + btn.getAttribute('data-version');\r\n      docu");
		WriteLiteral("ment.getElementById('previewBody').textContent = btn.getAttribute('data-body') || '';\r\n      new bootstrap.Modal(document.getElementById('previewModal')).show();\r\n    });\r\n  });\r\n</script>\r\n");
	}
}

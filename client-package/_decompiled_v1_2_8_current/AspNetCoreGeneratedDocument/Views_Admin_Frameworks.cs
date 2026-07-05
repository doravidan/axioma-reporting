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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/Frameworks.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_Frameworks : RazorPage<List<Framework>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-controller", "Lookup", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("asp-action", "CreateFramework", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("id", new HtmlString("editForm"), HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;

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
	public IHtmlHelper<List<Framework>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "ניהול מסגרות";
		List<EducationalStage> educationalStages = (base.ViewBag.EducationalStages as List<EducationalStage>) ?? new List<EducationalStage>();
		Dictionary<int, string> frameworkLocalities = (base.ViewBag.FrameworkLocalities as Dictionary<int, string>) ?? new Dictionary<int, string>();
		WriteLiteral("<div class=\"container-fluid mt-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>מסגרות</h3>\r\n    <div class=\"d-flex gap-2\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "08f4badc30ddce98276e6e8746c7f78b6ade79f18e5443b4d86ac73030ba42906307", async delegate
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
		WriteLiteral("\r\n      <button class=\"btn btn-primary btn-sm\" data-bs-toggle=\"modal\" data-bs-target=\"#addModal\">הוסף מסגרת</button>\r\n    </div>\r\n  </div>\r\n\r\n");
		if (base.TempData["Success"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n      ");
			Write(base.TempData["Success"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		if (base.TempData["Error"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\r\n      ");
			Write(base.TempData["Error"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n  <div class=\"card\">\r\n    <div class=\"card-body p-0\">\r\n      <table class=\"table table-hover table-bordered mb-0\">\r\n        <thead class=\"table-light\">\r\n          <tr>\r\n            <th>יישוב</th>\r\n            <th>תיאור</th>\r\n            <th>סמל מוסד</th>\r\n            <th>שלב חינוך</th>\r\n            <th>פעיל</th>\r\n            <th>פעולות</th>\r\n          </tr>\r\n        </thead>\r\n        <tbody>\r\n");
		if (!base.Model.Any())
		{
			WriteLiteral("            <tr><td colspan=\"6\" class=\"text-center text-muted py-4\">אין מסגרות</td></tr>\r\n");
		}
		foreach (Framework item in base.Model)
		{
			WriteLiteral("            <tr>\r\n              <td>");
			Write(frameworkLocalities.TryGetValue(item.Id, out string localityName) ? localityName : string.Empty);
			WriteLiteral("</td>\r\n              <td>");
			Write(item.Description);
			WriteLiteral("</td>\r\n              <td>");
			Write(item.InstitutionSymbol);
			WriteLiteral("</td>\r\n              <td>");
			Write(item.EducationalStage?.Description ?? "—");
			WriteLiteral("</td>\r\n              <td>\r\n");
			if (item.IsActive)
			{
				WriteLiteral("                  <span class=\"badge bg-success\">כן</span>\r\n");
			}
			else
			{
				WriteLiteral("                  <span class=\"badge bg-secondary\">לא</span>\r\n");
			}
			WriteLiteral("              </td>\r\n              <td>\r\n                <button type=\"button\" class=\"btn btn-sm btn-outline-secondary btn-edit\"\r\n                        data-id=\"");
			Write(item.Id);
			WriteLiteral("\"\r\n                        data-description=\"");
			Write(item.Description);
			WriteLiteral("\"\r\n                        data-symbol=\"");
			Write(item.InstitutionSymbol);
			WriteLiteral("\"\r\n                        data-stage=\"");
			Write(item.EducationalStageId?.ToString() ?? "");
			WriteLiteral("\"\r\n                        data-active=\"");
			Write(item.IsActive.ToString().ToLower());
			WriteLiteral("\">\r\n                  ✏\ufe0f\r\n                </button>\r\n              </td>\r\n            </tr>\r\n");
		}
		WriteLiteral("        </tbody>\r\n      </table>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<!-- Add Modal -->\r\n<div class=\"modal fade\" id=\"addModal\" tabindex=\"-1\" aria-labelledby=\"addModalTitle\" aria-hidden=\"true\">\r\n  <div class=\"modal-dialog\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"addModalTitle\">הוסף מסגרת</h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "08f4badc30ddce98276e6e8746c7f78b6ade79f18e5443b4d86ac73030ba429015215", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"modal-body\">\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">תיאור <span class=\"text-danger\">*</span></label>\r\n            <input name=\"description\" class=\"form-control\" required maxlength=\"200\" />\r\n          </div>\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">סמל מוסד <span class=\"text-danger\">*</span></label>\r\n            <input name=\"institutionSymbol\" class=\"form-control\" required maxlength=\"50\" />\r\n          </div>\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">שלב חינוך</label>\r\n            <select name=\"educationalStageId\" class=\"form-select\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "08f4badc30ddce98276e6e8746c7f78b6ade79f18e5443b4d86ac73030ba429016474", async delegate
			{
				WriteLiteral("— בחר —");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (EducationalStage stage2 in educationalStages)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "08f4badc30ddce98276e6e8746c7f78b6ade79f18e5443b4d86ac73030ba429018004", async delegate
				{
					Write(stage2.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(stage2.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			WriteLiteral("            </select>\r\n          </div>\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n          <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n          <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n        </div>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<!-- Edit Modal -->\r\n<div class=\"modal fade\" id=\"editModal\" tabindex=\"-1\" aria-labelledby=\"editModalTitle\" aria-hidden=\"true\">\r\n  <div class=\"modal-dialog\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"editModalTitle\">עדכון מסגרת</h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "08f4badc30ddce98276e6e8746c7f78b6ade79f18e5443b4d86ac73030ba429022336", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"modal-body\">\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">תיאור <span class=\"text-danger\">*</span></label>\r\n            <input id=\"editDescription\" name=\"description\" class=\"form-control\" required maxlength=\"200\" />\r\n          </div>\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">סמל מוסד <span class=\"text-danger\">*</span></label>\r\n            <input id=\"editSymbol\" name=\"institutionSymbol\" class=\"form-control\" required maxlength=\"50\" />\r\n          </div>\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">שלב חינוך</label>\r\n            <select id=\"editStage\" name=\"educationalStageId\" class=\"form-select\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "08f4badc30ddce98276e6e8746c7f78b6ade79f18e5443b4d86ac73030ba429023655", async delegate
			{
				WriteLiteral("— בחר —");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (EducationalStage stage in educationalStages)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "08f4badc30ddce98276e6e8746c7f78b6ade79f18e5443b4d86ac73030ba429025185", async delegate
				{
					Write(stage.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(stage.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			WriteLiteral("            </select>\r\n          </div>\r\n          <div class=\"mb-3 form-check\">\r\n            <input id=\"editIsActive\" name=\"isActive\" type=\"checkbox\" class=\"form-check-input\" value=\"true\" />\r\n            <label class=\"form-check-label\" for=\"editIsActive\">פעיל</label>\r\n          </div>\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n          <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n          <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n        </div>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<script>\r\n  document.querySelectorAll('.btn-edit').forEach(function (btn) {\r\n    btn.addEventListener('click', function () {\r\n      document.getElementById('editDescription').value = btn.getAttribute('data-description');\r\n      document.getElementById('editSymbol').value = btn.getAttribute('data-symbol');\r\n      document.getElementById('editIsActive').checked = btn.getAttribute('data-active') === 'true';\r\n      var stageVal = btn.getAttribute('data-stage');\r\n      document.getElementById('editStage').value = stageVal || '';\r\n      document.getElementById('editForm').action = '/Admin/EditFramework/' + btn.getAttribute('data-id');\r\n      new bootstrap.Modal(document.getElementById('editModal')).show();\r\n    });\r\n  });\r\n</script>\r\n");
	}
}

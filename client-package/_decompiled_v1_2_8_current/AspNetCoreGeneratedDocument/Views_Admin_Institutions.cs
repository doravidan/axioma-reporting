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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/Institutions.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_Institutions : RazorPage<List<Institution>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-controller", "Lookup", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("asp-action", "CreateInstitution", HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<List<Institution>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "ניהול מוסדות";
		List<Locality> localities = (base.ViewBag.Localities as List<Locality>) ?? new List<Locality>();
		List<District> districts = (base.ViewBag.Districts as List<District>) ?? new List<District>();
		List<Sector> sectors = (base.ViewBag.Sectors as List<Sector>) ?? new List<Sector>();
		List<EducationType> educationTypes = (base.ViewBag.EducationTypes as List<EducationType>) ?? new List<EducationType>();
		List<EducationalStage> educationalStages = (base.ViewBag.EducationalStages as List<EducationalStage>) ?? new List<EducationalStage>();
		WriteLiteral("<div class=\"container-fluid mt-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>מוסדות</h3>\r\n    <div class=\"d-flex gap-2\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb6714", async delegate
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
		WriteLiteral("\r\n      <button class=\"btn btn-primary btn-sm\" data-bs-toggle=\"modal\" data-bs-target=\"#addModal\">הוסף מוסד</button>\r\n    </div>\r\n  </div>\r\n\r\n");
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
		WriteLiteral("\r\n  <div class=\"card\">\r\n    <div class=\"card-body p-0\">\r\n      <div class=\"table-responsive\">\r\n        <table class=\"table table-hover table-bordered mb-0 table-sm\">\r\n          <thead class=\"table-light\">\r\n            <tr>\r\n              <th>שם המוסד</th>\r\n              <th>סמל מוסד</th>\r\n              <th>ישוב</th>\r\n              <th>מחוז</th>\r\n              <th>מגזר</th>\r\n              <th>סוג חינוך</th>\r\n              <th>שלב חינוך</th>\r\n              <th>פעיל</th>\r\n              <th>פעולות</th>\r\n            </tr>\r\n          </thead>\r\n          <tbody>\r\n");
		if (!base.Model.Any())
		{
			WriteLiteral("              <tr><td colspan=\"9\" class=\"text-center text-muted py-4\">אין מוסדות</td></tr>\r\n");
		}
		foreach (Institution item11 in base.Model)
		{
			WriteLiteral("              <tr>\r\n                <td>");
			Write(item11.Name);
			WriteLiteral("</td>\r\n                <td>");
			Write(item11.InstitutionSymbol);
			WriteLiteral("</td>\r\n                <td>");
			Write(item11.Locality?.Description ?? "—");
			WriteLiteral("</td>\r\n                <td>");
			Write(item11.District?.Description ?? "—");
			WriteLiteral("</td>\r\n                <td>");
			Write(item11.Sector?.Description ?? "—");
			WriteLiteral("</td>\r\n                <td>");
			Write(item11.Type?.Description ?? "—");
			WriteLiteral("</td>\r\n                <td>");
			Write(item11.EducationalStage?.Description ?? "—");
			WriteLiteral("</td>\r\n                <td>\r\n");
			if (item11.IsActive)
			{
				WriteLiteral("                    <span class=\"badge bg-success\">כן</span>\r\n");
			}
			else
			{
				WriteLiteral("                    <span class=\"badge bg-secondary\">לא</span>\r\n");
			}
			WriteLiteral("                </td>\r\n                <td>\r\n                  <button type=\"button\" class=\"btn btn-sm btn-outline-secondary btn-edit\"\r\n                          data-id=\"");
			Write(item11.Id);
			WriteLiteral("\"\r\n                          data-name=\"");
			Write(item11.Name);
			WriteLiteral("\"\r\n                          data-symbol=\"");
			Write(item11.InstitutionSymbol);
			WriteLiteral("\"\r\n                          data-locality=\"");
			Write(item11.LocalityId?.ToString() ?? "");
			WriteLiteral("\"\r\n                          data-district=\"");
			Write(item11.DistrictId?.ToString() ?? "");
			WriteLiteral("\"\r\n                          data-sector=\"");
			Write(item11.SectorId?.ToString() ?? "");
			WriteLiteral("\"\r\n                          data-type=\"");
			Write(item11.TypeId?.ToString() ?? "");
			WriteLiteral("\"\r\n                          data-stage=\"");
			Write(item11.EducationalStageId?.ToString() ?? "");
			WriteLiteral("\"\r\n                          data-active=\"");
			Write(item11.IsActive.ToString().ToLower());
			WriteLiteral("\">\r\n                    ✏\ufe0f\r\n                  </button>\r\n                </td>\r\n              </tr>\r\n");
		}
		WriteLiteral("          </tbody>\r\n        </table>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<!-- Add Modal -->\r\n<div class=\"modal fade\" id=\"addModal\" tabindex=\"-1\" aria-labelledby=\"addModalTitle\" aria-hidden=\"true\">\r\n  <div class=\"modal-dialog modal-lg\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"addModalTitle\">הוסף מוסד</h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb18417", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"modal-body\">\r\n          <div class=\"row\">\r\n            <div class=\"col-md-8 mb-3\">\r\n              <label class=\"form-label\">שם המוסד <span class=\"text-danger\">*</span></label>\r\n              <input name=\"name\" class=\"form-control\" required />\r\n            </div>\r\n            <div class=\"col-md-4 mb-3\">\r\n              <label class=\"form-label\">סמל מוסד <span class=\"text-danger\">*</span></label>\r\n              <input name=\"institutionSymbol\" type=\"number\" class=\"form-control\" required />\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-6 mb-3\">\r\n              <label class=\"form-label\">ישוב</label>\r\n              <select name=\"localityId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb19775", async delegate
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
			foreach (Locality item10 in localities)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb21303", async delegate
				{
					Write(item10.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item10.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n            <div class=\"col-md-6 mb-3\">\r\n              <label class=\"form-label\">מחוז</label>\r\n              <select name=\"districtId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb23691", async delegate
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
			foreach (District item9 in districts)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb25218", async delegate
				{
					Write(item9.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item9.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-4 mb-3\">\r\n              <label class=\"form-label\">מגזר</label>\r\n              <select name=\"sectorId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb27644", async delegate
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
			foreach (Sector item8 in sectors)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb29169", async delegate
				{
					Write(item8.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item8.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n            <div class=\"col-md-4 mb-3\">\r\n              <label class=\"form-label\">סוג חינוך</label>\r\n              <select name=\"typeId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb31558", async delegate
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
			foreach (EducationType item7 in educationTypes)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb33090", async delegate
				{
					Write(item7.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item7.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n            <div class=\"col-md-4 mb-3\">\r\n              <label class=\"form-label\">שלב חינוך</label>\r\n              <select name=\"educationalStageId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb35491", async delegate
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
			foreach (EducationalStage item6 in educationalStages)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb37026", async delegate
				{
					Write(item6.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item6.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n          <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n          <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n        </div>\r\n      ");
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
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<!-- Edit Modal -->\r\n<div class=\"modal fade\" id=\"editModal\" tabindex=\"-1\" aria-labelledby=\"editModalTitle\" aria-hidden=\"true\">\r\n  <div class=\"modal-dialog modal-lg\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"editModalTitle\">עדכון מוסד</h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb41394", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"modal-body\">\r\n          <div class=\"row\">\r\n            <div class=\"col-md-8 mb-3\">\r\n              <label class=\"form-label\">שם המוסד <span class=\"text-danger\">*</span></label>\r\n              <input id=\"eName\" name=\"name\" class=\"form-control\" required />\r\n            </div>\r\n            <div class=\"col-md-4 mb-3\">\r\n              <label class=\"form-label\">סמל מוסד <span class=\"text-danger\">*</span></label>\r\n              <input id=\"eSymbol\" name=\"institutionSymbol\" type=\"number\" class=\"form-control\" required />\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-6 mb-3\">\r\n              <label class=\"form-label\">ישוב</label>\r\n              <select id=\"eLocality\" name=\"localityId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb42797", async delegate
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
			foreach (Locality item5 in localities)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb44325", async delegate
				{
					Write(item5.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item5.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n            <div class=\"col-md-6 mb-3\">\r\n              <label class=\"form-label\">מחוז</label>\r\n              <select id=\"eDistrict\" name=\"districtId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb46730", async delegate
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
			foreach (District item4 in districts)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb48257", async delegate
				{
					Write(item4.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item4.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-4 mb-3\">\r\n              <label class=\"form-label\">מגזר</label>\r\n              <select id=\"eSector\" name=\"sectorId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb50698", async delegate
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
			foreach (Sector item3 in sectors)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb52223", async delegate
				{
					Write(item3.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item3.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n            <div class=\"col-md-4 mb-3\">\r\n              <label class=\"form-label\">סוג חינוך</label>\r\n              <select id=\"eType\" name=\"typeId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb54625", async delegate
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
			foreach (EducationType item2 in educationTypes)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb56157", async delegate
				{
					Write(item2.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item2.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n            <div class=\"col-md-4 mb-3\">\r\n              <label class=\"form-label\">שלב חינוך</label>\r\n              <select id=\"eStage\" name=\"educationalStageId\" class=\"form-select\">\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb58572", async delegate
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
			foreach (EducationalStage item in educationalStages)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b2d9f680b8b9210ce6a7082465e8431451bb42ab2ccb5eb8dcfb5b1db1b431eb60107", async delegate
				{
					Write(item.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item.Id);
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
			WriteLiteral("              </select>\r\n            </div>\r\n          </div>\r\n          <div class=\"mb-3 form-check\">\r\n            <input id=\"eIsActive\" name=\"isActive\" type=\"checkbox\" class=\"form-check-input\" value=\"true\" />\r\n            <label class=\"form-check-label\" for=\"eIsActive\">פעיל</label>\r\n          </div>\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n          <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n          <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n        </div>\r\n      ");
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
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<script>\r\n  document.querySelectorAll('.btn-edit').forEach(function (btn) {\r\n    btn.addEventListener('click', function () {\r\n      document.getElementById('eName').value = btn.getAttribute('data-name');\r\n      document.getElementById('eSymbol').value = btn.getAttribute('data-symbol');\r\n      document.getElementById('eIsActive').checked = btn.getAttribute('data-active') === 'true';\r\n      setSelectVal('eLocality', btn.getAttribute('data-locality'));\r\n      setSelectVal('eDistrict', btn.getAttribute('data-district'));\r\n      setSelectVal('eSector', btn.getAttribute('data-sector'));\r\n      setSelectVal('eType', btn.getAttribute('data-type'));\r\n      setSelectVal('eStage', btn.getAttribute('data-stage'));\r\n      document.getElementById('editForm').action = '/Admin/EditInstitution/' + btn.getAttribute('data-id');\r\n      new bootstrap.Modal(document.getElementById('editModal')).show();\r\n    });\r\n  });\r\n\r\n  function setSelectVal(id, val) {\r\n    document.getElementById(id).value = v");
		WriteLiteral("al || '';\r\n  }\r\n</script>\r\n");
	}
}

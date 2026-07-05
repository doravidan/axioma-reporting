using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Entities.Base;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Lookup/List.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Lookup_List : RazorPage<List<LookupEntity>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "List", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("selected", new HtmlString("selected"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("method", "get", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("class", new HtmlString("mb-3 d-flex gap-2 align-items-center flex-wrap"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("asp-action", "Delete", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("style", new HtmlString("display:inline"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("class", new HtmlString("delete-form"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("class", new HtmlString("page-link"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("asp-action", "ImportExcel", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("enctype", new HtmlString("multipart/form-data"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("asp-action", "Create", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("id", new HtmlString("editForm"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<List<LookupEntity>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = $"ניהול {(object?)base.ViewBag.DisplayName}";
		bool isAdmin = base.ViewBag.IsAdmin ?? ((object)false);
		string tableName = base.ViewBag.TableName;
		int page = base.ViewBag.Page;
		int pageSize = base.ViewBag.PageSize;
		int total = base.ViewBag.TotalItems;
		int totalPages = (int)Math.Ceiling((double)total / (double)pageSize);
		bool isLocalities = string.Equals(tableName, "localities", StringComparison.OrdinalIgnoreCase);
		WriteLiteral("<div class=\"container-fluid mt-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>");
		Write(base.ViewBag.DisplayName);
		WriteLiteral("</h3>\r\n    <div class=\"d-flex gap-2\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b010230", async delegate
		{
			WriteLiteral("חזרה לרשימה");
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
		WriteLiteral("\r\n");
		if (isAdmin)
		{
			WriteLiteral("        <button class=\"btn btn-outline-success btn-sm\" data-bs-toggle=\"modal\" data-bs-target=\"#importModal\">\r\n          ייבוא אקסל\r\n        </button>\r\n        <button class=\"btn btn-primary btn-sm\" data-bs-toggle=\"modal\" data-bs-target=\"#addModal\">\r\n          הוסף רשומה\r\n        </button>\r\n");
		}
		WriteLiteral("    </div>\r\n  </div>\r\n\r\n");
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
		WriteLiteral("\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b014123", async delegate
		{
			WriteLiteral("\r\n    <input name=\"tableName\" type=\"hidden\"");
			BeginWriteAttribute("value", " value=\"", 1889, "\"", 1907, 1);
			WriteAttributeValue("", 1897, tableName, 1897, 10, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n    <input name=\"search\" type=\"text\" class=\"form-control\" style=\"max-width:300px\"\r\n           placeholder=\"חיפוש לפי תיאור...\"");
			BeginWriteAttribute("value", " value=\"", 2039, "\"", 2062, 1);
			WriteAttributeValue("", 2047, base.ViewBag.Search, 2047, 15, false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n    <button type=\"submit\" class=\"btn btn-outline-primary\">חפש</button>\r\n");
			if (!string.IsNullOrEmpty(base.ViewBag.Search as string))
			{
				WriteLiteral("      ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b015777", async delegate
				{
					WriteLiteral("נקה");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-tableName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(tableName);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-tableName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"], HtmlAttributeValueStyle.DoubleQuotes);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			WriteLiteral("    <div class=\"me-auto d-flex align-items-center gap-2\">\r\n      <label class=\"form-label mb-0\">שורות בעמוד:</label>\r\n      <select name=\"pageSize\" class=\"form-select form-select-sm\" style=\"max-width:80px\" onchange=\"this.form.submit()\">\r\n");
			int[] array = new int[4] { 10, 25, 50, 100 };
			foreach (int s in array)
			{
				if (s == pageSize)
				{
					WriteLiteral("            ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b018932", async delegate
					{
						Write(s);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(s);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n");
				}
				else
				{
					WriteLiteral("            ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b021158", async delegate
					{
						Write(s);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(s);
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
			}
			WriteLiteral("      </select>\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
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
		WriteLiteral("\r\n\r\n  <div class=\"card\">\r\n    <div class=\"card-body p-0\">\r\n      <table class=\"table table-hover table-bordered mb-0\">\r\n        <thead class=\"table-light\">\r\n          <tr>\r\n            <th style=\"width:80px\">מזהה</th>\r\n            <th>תיאור</th>\r\n");
		if (isLocalities)
		{
			WriteLiteral("              <th style=\"width:120px\">קוד ארצי</th>\r\n");
		}
		WriteLiteral("            <th style=\"width:80px\">פעיל</th>\r\n");
		if (isAdmin)
		{
			WriteLiteral("              <th style=\"width:110px\">פעולות</th>\r\n");
		}
		WriteLiteral("          </tr>\r\n        </thead>\r\n        <tbody>\r\n");
		int num = (isAdmin ? 4 : 3);
		int num2 = num + (isLocalities ? 1 : 0);
		if (!base.Model.Any())
		{
			WriteLiteral("            <tr>\r\n              <td");
			BeginWriteAttribute("colspan", " colspan=\"", 3656, "\"", 3675, 1);
			WriteAttributeValue("", 3666, num2, 3666, 9, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"text-center text-muted py-4\">אין רשומות להצגה</td>\r\n            </tr>\r\n");
		}
		foreach (LookupEntity item in base.Model)
		{
			Locality locality = item as Locality;
			WriteLiteral("            <tr>\r\n              <td>");
			Write(item.Id);
			WriteLiteral("</td>\r\n              <td>");
			Write(item.Description);
			WriteLiteral("</td>\r\n");
			if (isLocalities)
			{
				WriteLiteral("                <td>");
				Write(locality?.NationalCode?.ToString() ?? "—");
				WriteLiteral("</td>\r\n");
			}
			WriteLiteral("              <td>\r\n");
			if (item.IsActive)
			{
				WriteLiteral("                  <span class=\"badge bg-success\">כן</span>\r\n");
			}
			else
			{
				WriteLiteral("                  <span class=\"badge bg-secondary\">לא</span>\r\n");
			}
			WriteLiteral("              </td>\r\n");
			if (isAdmin)
			{
				WriteLiteral("                <td>\r\n                  <button type=\"button\" class=\"btn btn-sm btn-outline-secondary btn-edit\"\r\n                          data-id=\"");
				Write(item.Id);
				WriteLiteral("\"\r\n                          data-description=\"");
				Write(item.Description);
				WriteLiteral("\"\r\n                          data-active=\"");
				Write(item.IsActive.ToString().ToLower());
				WriteLiteral("\"\r\n                          data-nationalcode=\"");
				Write(locality?.NationalCode?.ToString() ?? "");
				WriteLiteral("\"\r\n                          title=\"עריכה\">\r\n                    ✏\ufe0f\r\n                  </button>\r\n                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b031290", async delegate
				{
					WriteLiteral("\r\n                    ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n                    <button type=\"submit\" class=\"btn btn-sm btn-outline-danger\" title=\"מחיקה\">\ud83d\uddd1\ufe0f</button>\r\n                  ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-tableName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(tableName);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["tableName"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-tableName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["tableName"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
				BeginWriteTagHelperAttribute();
				Write(item.Description);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__tagHelperExecutionContext.AddHtmlAttribute("data-description", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n                </td>\r\n");
			}
			WriteLiteral("            </tr>\r\n");
		}
		WriteLiteral("        </tbody>\r\n      </table>\r\n    </div>\r\n  </div>\r\n\r\n");
		if (totalPages > 1)
		{
			WriteLiteral("    <nav class=\"mt-3\" aria-label=\"דפדוף\">\r\n      <ul class=\"pagination\">\r\n");
			if (page > 1)
			{
				WriteLiteral("          <li class=\"page-item\">\r\n            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b036892", async delegate
				{
					WriteLiteral("הקודם");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-tableName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(tableName);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-tableName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(page - 1);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["page"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-page", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["page"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(pageSize);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageSize"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageSize", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageSize"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.ViewBag.Search);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["search"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-search", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["search"], HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n          </li>\r\n");
			}
			int i;
			for (i = Math.Max(1, page - 3); i <= Math.Min(totalPages, page + 3); i++)
			{
				WriteLiteral("          <li");
				BeginWriteAttribute("class", " class=\"", 6006, "\"", 6052, 2);
				WriteAttributeValue("", 6014, "page-item", 6014, 9, isLiteral: true);
				WriteAttributeValue(" ", 6023, (i == page) ? "active" : "", 6024, 28, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">\r\n            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b042283", async delegate
				{
					Write(i);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-tableName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(tableName);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-tableName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(i);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["page"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-page", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["page"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(pageSize);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageSize"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageSize", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageSize"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.ViewBag.Search);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["search"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-search", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["search"], HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n          </li>\r\n");
			}
			if (page < totalPages)
			{
				WriteLiteral("          <li class=\"page-item\">\r\n            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b047320", async delegate
				{
					WriteLiteral("הבא");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-tableName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(tableName);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-tableName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(page + 1);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["page"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-page", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["page"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(pageSize);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageSize"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageSize", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageSize"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.ViewBag.Search);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["search"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-search", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["search"], HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n          </li>\r\n");
			}
			WriteLiteral("      </ul>\r\n    </nav>\r\n    <p class=\"text-muted small\">");
			Write($"סה\"כ {total} רשומות, עמוד {page} מתוך {totalPages}");
			WriteLiteral("</p>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n");
		if (!isAdmin)
		{
			return;
		}
		WriteLiteral("    <div class=\"modal fade\" id=\"importModal\" tabindex=\"-1\" aria-labelledby=\"importModalTitle\" aria-modal=\"true\" role=\"dialog\">\r\n      <div class=\"modal-dialog\" role=\"dialog\" aria-modal=\"true\">\r\n        <div class=\"modal-content\">\r\n          <div class=\"modal-header\">\r\n            <h5 class=\"modal-title\" id=\"addModalTitle\">ייבוא אקסל</h5>\r\n            <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n          </div>\r\n          ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b053242", async delegate
		{
			WriteLiteral("\r\n            ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n            <div class=\"modal-body\">\r\n              <p class=\"text-muted\">הקובץ צריך להיות בפורמט xlsx. שורה ראשונה כותרת, עמודה ראשונה תיאור.</p>\r\n              <input type=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control\" required />\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n              <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n              <button type=\"submit\" class=\"btn btn-success\">ייבוא</button>\r\n            </div>\r\n          ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_12.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-tableName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(tableName);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["tableName"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-tableName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["tableName"], HtmlAttributeValueStyle.DoubleQuotes);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n        </div>\r\n      </div>\r\n    </div>\r\n");
		WriteLiteral("    <!-- Add Modal -->\r\n  <div class=\"modal fade\" id=\"addModal\" tabindex=\"-1\" aria-labelledby=\"addModalLabel\" aria-hidden=\"true\">\r\n    <div class=\"modal-dialog\" role=\"dialog\" aria-modal=\"true\">\r\n      <div class=\"modal-content\">\r\n        <div class=\"modal-header\">\r\n          <h5 class=\"modal-title\" id=\"addModalLabel\">הוסף רשומה — ");
		Write(base.ViewBag.DisplayName);
		WriteLiteral("</h5>\r\n          <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור\"></button>\r\n        </div>\r\n        ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b057648", async delegate
		{
			WriteLiteral("\r\n          ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n          <div class=\"modal-body\">\r\n            <div class=\"mb-3\">\r\n              <label class=\"form-label\" for=\"addDescription\">תיאור <span class=\"text-danger\">*</span></label>\r\n              <input id=\"addDescription\" name=\"description\" class=\"form-control\" required maxlength=\"200\" aria-required=\"true\" />\r\n            </div>\r\n");
			if (isLocalities)
			{
				WriteLiteral("              <div class=\"mb-3\">\r\n                <label class=\"form-label\" for=\"addNationalCode\">קוד ארצי</label>\r\n                <input id=\"addNationalCode\" name=\"nationalCode\" type=\"number\" class=\"form-control\" min=\"0\" />\r\n              </div>\r\n");
			}
			WriteLiteral("          </div>\r\n          <div class=\"modal-footer\">\r\n            <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n            <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n          </div>\r\n        ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_14.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-tableName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(tableName);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["tableName"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-tableName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["tableName"], HtmlAttributeValueStyle.DoubleQuotes);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n      </div>\r\n    </div>\r\n  </div>\r\n");
		WriteLiteral("  <!-- Edit Modal -->\r\n  <div class=\"modal fade\" id=\"editModal\" tabindex=\"-1\" aria-labelledby=\"editModalLabel\" aria-hidden=\"true\">\r\n    <div class=\"modal-dialog\" role=\"dialog\" aria-modal=\"true\">\r\n      <div class=\"modal-content\">\r\n        <div class=\"modal-header\">\r\n          <h5 class=\"modal-title\" id=\"editModalLabel\">עדכון רשומה</h5>\r\n          <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור\"></button>\r\n        </div>\r\n        ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a3b22c2b061995e51b2477ec71fad4461c791957284a52ede72ccf2bc9e2d0b062519", async delegate
		{
			WriteLiteral("\r\n          ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n          <div class=\"modal-body\">\r\n            <div class=\"mb-3\">\r\n              <label class=\"form-label\" for=\"editDescription\">תיאור <span class=\"text-danger\">*</span></label>\r\n              <input id=\"editDescription\" name=\"description\" class=\"form-control\" required maxlength=\"200\" aria-required=\"true\" />\r\n            </div>\r\n");
			if (isLocalities)
			{
				WriteLiteral("              <div class=\"mb-3\">\r\n                <label class=\"form-label\" for=\"editNationalCode\">קוד ארצי</label>\r\n                <input id=\"editNationalCode\" name=\"nationalCode\" type=\"number\" class=\"form-control\" min=\"0\" />\r\n              </div>\r\n");
			}
			WriteLiteral("            <div class=\"mb-3 form-check\">\r\n              <input id=\"editIsActive\" name=\"isActive\" type=\"checkbox\" class=\"form-check-input\" value=\"true\" />\r\n              <label class=\"form-check-label\" for=\"editIsActive\">פעיל</label>\r\n            </div>\r\n          </div>\r\n          <div class=\"modal-footer\">\r\n            <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n            <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n          </div>\r\n        ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_15);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n      </div>\r\n    </div>\r\n  </div>\r\n");
		WriteLiteral("  <script>\r\n    (function () {\r\n      var tableNameVal = ");
		Write(Json.Serialize(tableName));
		WriteLiteral(";\r\n\r\n      document.querySelectorAll('.btn-edit').forEach(function (btn) {\r\n        btn.addEventListener('click', function () {\r\n          var id = btn.getAttribute('data-id');\r\n          var desc = btn.getAttribute('data-description');\r\n          var active = btn.getAttribute('data-active') === 'true';\r\n          document.getElementById('editDescription').value = desc;\r\n          document.getElementById('editIsActive').checked = active;\r\n          var ncInput = document.getElementById('editNationalCode');\r\n          if (ncInput) {\r\n            ncInput.value = btn.getAttribute('data-nationalcode') || '';\r\n          }\r\n          document.getElementById('editForm').action = '/Lookup/' + tableNameVal + '/Edit/' + id;\r\n          new bootstrap.Modal(document.getElementById('editModal')).show();\r\n        });\r\n      });\r\n\r\n      document.querySelectorAll('.delete-form').forEach(function (form) {\r\n        form.addEventListener('submit', function (e) {\r\n          var desc = form.getAttribute('data-description');\r\n    ");
		WriteLiteral("      if (!confirm('האם למחוק את הרשומה \"' + desc + '\"?')) {\r\n            e.preventDefault();\r\n          }\r\n        });\r\n      });\r\n    })();\r\n  </script>\r\n");
	}
}

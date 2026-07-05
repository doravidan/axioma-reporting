using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Allocations/Create.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Allocations_Create : RazorPage<AllocationEmployeePickerViewModel>
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
	public IHtmlHelper<AllocationEmployeePickerViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.Layout = "_Layout";
		base.ViewData["Title"] = "הוספת הקצאה";
		WriteLiteral("<div class=\"container mt-4\" dir=\"rtl\"><div class=\"d-flex justify-content-between align-items-center mb-3 flex-wrap gap-2\"><h3 class=\"mb-0\">הוספת הקצאה לעובד</h3><a class=\"btn btn-outline-secondary\" href=\"/allocations\">חזרה להקצאות</a></div><form method=\"get\" action=\"/allocations/create\" class=\"card card-body mb-3\"><div class=\"row g-2 align-items-end\"><div class=\"col-md-3\"><label class=\"form-label\" for=\"allocation-picker-id\">ת.ז</label><input id=\"allocation-picker-id\" class=\"form-control\" name=\"idNumber\" value=\"");
		Write(Model.IdNumber ?? string.Empty);
		WriteLiteral("\" autocomplete=\"off\" /></div><div class=\"col-md-3\"><label class=\"form-label\" for=\"allocation-picker-code\">קוד עובד</label><input id=\"allocation-picker-code\" class=\"form-control\" name=\"employeeCode\" value=\"");
		Write(Model.EmployeeCode ?? string.Empty);
		WriteLiteral("\" autocomplete=\"off\" /></div><div class=\"col-md-3\"><label class=\"form-label\" for=\"allocation-picker-first\">שם פרטי</label><input id=\"allocation-picker-first\" class=\"form-control\" name=\"firstName\" value=\"");
		Write(Model.FirstName ?? string.Empty);
		WriteLiteral("\" autocomplete=\"off\" /></div><div class=\"col-md-3\"><label class=\"form-label\" for=\"allocation-picker-last\">שם משפחה</label><input id=\"allocation-picker-last\" class=\"form-control\" name=\"lastName\" value=\"");
		Write(Model.LastName ?? string.Empty);
		WriteLiteral("\" autocomplete=\"off\" /></div><div class=\"col-12\"><button class=\"btn btn-primary\">חפש</button></div></div></form><div class=\"card\"><div class=\"card-header\">בחר עובד</div><div class=\"table-responsive\"><table class=\"table table-sm table-hover mb-0\"><thead><tr><th scope=\"col\">ת.ז</th><th scope=\"col\">קוד עובד</th><th scope=\"col\">שם פרטי</th><th scope=\"col\">שם משפחה</th><th scope=\"col\"></th></tr></thead><tbody>");
		if (Model.Employees.Count == 0)
		{
			WriteLiteral("<tr><td colspan=\"5\" class=\"text-muted text-center py-4\">לא נמצאו עובדים תואמים</td></tr>");
		}
		else
		{
			foreach (var employee in Model.Employees)
			{
				WriteLiteral("<tr><td>");
				Write(employee.IdNumber);
				WriteLiteral("</td><td>");
				Write(employee.EmployeeCode);
				WriteLiteral("</td><td>");
				Write(employee.FirstName);
				WriteLiteral("</td><td>");
				Write(employee.LastName);
				WriteLiteral("</td><td><a class=\"btn btn-sm btn-success\" href=\"/Employee/");
				Write(employee.Id);
				WriteLiteral("/Allocations/Create\">הוסף הקצאה</a></td></tr>");
			}
		}
		WriteLiteral("</tbody></table></div></div></div>");
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/ProjectPrograms.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_ProjectPrograms : RazorPage<dynamic>
{
	[RazorInject]
	public IHtmlHelper<dynamic> Html { get; private set; }

	public override Task ExecuteAsync()
	{
		ViewData["Title"] = "ניהול תוכניות והיקפים לפי פרויקט";
		List<Project> projects = (List<Project>)ViewBag.Projects;
		List<AxiomaReporting.Core.Entities.Program> programs = (List<AxiomaReporting.Core.Entities.Program>)ViewBag.Programs;
		Dictionary<int, HashSet<int>> mapping = (Dictionary<int, HashSet<int>>)ViewBag.Mapping;
		Dictionary<string, HashSet<int>> scopeMapping = (Dictionary<string, HashSet<int>>)ViewBag.ScopeMapping;
		List<Subject> subjects = (List<Subject>)ViewBag.Subjects;
		List<Domain> domains = (List<Domain>)ViewBag.Domains;
		List<Framework> frameworks = (List<Framework>)ViewBag.Frameworks;
		List<EducationalProgram> educationalPrograms = (List<EducationalProgram>)ViewBag.EducationalPrograms;
		List<DiscussionCode> discussionCodes = (List<DiscussionCode>)ViewBag.DiscussionCodes;
		List<GradeLevel> gradeLevels = (List<GradeLevel>)ViewBag.GradeLevels;
		List<SchoolClass> classes = (List<SchoolClass>)ViewBag.Classes;

		WriteLiteral("<div class=\"container py-3\">\n");
		WriteLiteral("  <div class=\"d-flex justify-content-between align-items-center mb-3\">\n");
		WriteLiteral("    <h2>ניהול תוכניות והיקפים לפי פרויקט</h2>\n");
		WriteLiteral("    <a class=\"btn btn-outline-secondary btn-sm\" href=\"/Admin/DataMigration\">חזרה</a>\n");
		WriteLiteral("  </div>\n");
		WriteLiteral("  <div class=\"alert alert-info\" role=\"alert\">בחרו תוכניות לכל פרויקט, ולאחר מכן הגבילו את ערכי הבחירה הזמינים לכל צמד פרויקט-תוכנית.</div>\n");
		if (TempData["Success"] != null)
		{
			WriteLiteral("  <div class=\"alert alert-success\" role=\"alert\">");
			Write(TempData["Success"]);
			WriteLiteral("</div>\n");
		}
		if (TempData["Error"] != null)
		{
			WriteLiteral("  <div class=\"alert alert-danger\" role=\"alert\">");
			Write(TempData["Error"]);
			WriteLiteral("</div>\n");
		}
		if (projects.Count == 0)
		{
			WriteLiteral("  <div class=\"alert alert-warning\" role=\"alert\">לא נמצאו פרויקטים פעילים.</div>\n");
		}
		foreach (Project project in projects)
		{
			HashSet<int> selectedProgramIds = mapping.TryGetValue(project.Id, out HashSet<int>? projectPrograms) ? projectPrograms : new HashSet<int>();
			WriteLiteral("  <section class=\"border rounded-2 p-3 mb-3 bg-white project-program-card\" data-project-id=\"");
			Write(project.Id);
			WriteLiteral("\">\n");
			WriteLiteral("    <div class=\"d-flex justify-content-between align-items-center flex-wrap gap-2 mb-3\">\n");
			WriteLiteral("      <h3 class=\"h5 mb-0\"><span class=\"text-muted\">פרויקט:</span> ");
			Write(project.Description);
			WriteLiteral("</h3>\n");
			WriteLiteral("      <span class=\"badge bg-secondary\">קוד פרויקט ");
			Write(project.Id);
			WriteLiteral("</span>\n");
			WriteLiteral("    </div>\n");
			WriteLiteral("    <form method=\"post\" action=\"/Admin/ProjectPrograms/Save\" class=\"mb-3\">\n");
			Write(Html.AntiForgeryToken());
			WriteLiteral("      <input type=\"hidden\" name=\"projectId\" value=\"");
			Write(project.Id);
			WriteLiteral("\" />\n");
			WriteLiteral("      <label class=\"form-label\" for=\"programs_");
			Write(project.Id);
			WriteLiteral("\">תוכניות</label>\n");
			WriteLiteral("      <select id=\"programs_");
			Write(project.Id);
			WriteLiteral("\" name=\"programIds\" multiple class=\"form-select\" size=\"");
			Write(Math.Min(Math.Max(programs.Count, 3), 10));
			WriteLiteral("\">\n");
			foreach (AxiomaReporting.Core.Entities.Program program in programs)
			{
				WriteOption(program.Id, program.Description, selectedProgramIds.Contains(program.Id));
			}
			WriteLiteral("      </select>\n");
			WriteLiteral("      <div class=\"mt-2\"><button type=\"submit\" class=\"btn btn-primary btn-sm\">שמור תוכניות</button></div>\n");
			WriteLiteral("    </form>\n");

			List<AxiomaReporting.Core.Entities.Program> activeProjectPrograms = programs.Where((AxiomaReporting.Core.Entities.Program p) => selectedProgramIds.Contains(p.Id)).ToList();
			if (activeProjectPrograms.Count == 0)
			{
				WriteLiteral("    <p class=\"text-muted mb-0\">לא שויכו תוכניות לפרויקט זה.</p>\n");
			}
			foreach (AxiomaReporting.Core.Entities.Program program in activeProjectPrograms)
			{
				WriteLiteral("    <details class=\"border-top pt-3 mt-3\">\n");
				WriteLiteral("      <summary class=\"fw-semibold\">");
				Write(program.Description);
				WriteLiteral("</summary>\n");
				WriteLiteral("      <form method=\"post\" action=\"/Admin/ProjectPrograms/SaveScope\" class=\"mt-3\">\n");
				Write(Html.AntiForgeryToken());
				WriteLiteral("        <input type=\"hidden\" name=\"projectId\" value=\"");
				Write(project.Id);
				WriteLiteral("\" />\n");
				WriteLiteral("        <input type=\"hidden\" name=\"programId\" value=\"");
				Write(program.Id);
				WriteLiteral("\" />\n");
				WriteLiteral("        <div class=\"row g-3\">\n");
				WriteLookupSelect("נושאים", "subjectIds", subjects, Selected(scopeMapping, "subjects", project.Id, program.Id));
				WriteLookupSelect("תחומים", "domainIds", domains, Selected(scopeMapping, "domains", project.Id, program.Id));
				WriteLookupSelect("מסגרות", "frameworkIds", frameworks, Selected(scopeMapping, "frameworks", project.Id, program.Id));
				WriteLookupSelect("תוכניות חינוכיות", "educationalProgramIds", educationalPrograms, Selected(scopeMapping, "educationalPrograms", project.Id, program.Id));
				WriteLookupSelect("קיום דיון", "discussionCodeIds", discussionCodes, Selected(scopeMapping, "discussionCodes", project.Id, program.Id));
				WriteLookupSelect("שכבות", "gradeLevelIds", gradeLevels, Selected(scopeMapping, "gradeLevels", project.Id, program.Id));
				WriteLookupSelect("כיתות", "classIds", classes, Selected(scopeMapping, "classes", project.Id, program.Id));
				WriteLiteral("        </div>\n");
				WriteLiteral("        <div class=\"mt-3\"><button type=\"submit\" class=\"btn btn-outline-primary btn-sm\">שמור היקף</button></div>\n");
				WriteLiteral("      </form>\n");
				WriteLiteral("    </details>\n");
			}
			WriteLiteral("  </section>\n");
		}
		WriteLiteral("</div>\n");
		return Task.CompletedTask;
	}

	private void WriteLookupSelect<T>(string label, string name, IReadOnlyCollection<T> items, HashSet<int> selected) where T : AxiomaReporting.Core.Entities.Base.LookupEntity
	{
		WriteLiteral("          <div class=\"col-md-6 col-xl-4\">\n");
		WriteLiteral("            <label class=\"form-label\">");
		Write(label);
		WriteLiteral("</label>\n");
		WriteLiteral("            <select name=\"");
		Write(HtmlEncoder.Default.Encode(name));
		WriteLiteral("\" multiple class=\"form-select\" size=\"6\">\n");
		foreach (T item in items)
		{
			WriteOption(item.Id, item.Description, selected.Contains(item.Id));
		}
		WriteLiteral("            </select>\n");
		WriteLiteral("          </div>\n");
	}

	private void WriteOption(int id, string description, bool selected)
	{
		WriteLiteral("        <option value=\"");
		Write(id);
		WriteLiteral("\"");
		if (selected)
		{
			WriteLiteral(" selected");
		}
		WriteLiteral(">");
		Write(description);
		WriteLiteral("</option>\n");
	}

	private static HashSet<int> Selected(Dictionary<string, HashSet<int>> mapping, string scope, int projectId, int programId)
	{
		return mapping.TryGetValue($"{scope}:{projectId}:{programId}", out HashSet<int>? selected) ? selected : new HashSet<int>();
	}
}

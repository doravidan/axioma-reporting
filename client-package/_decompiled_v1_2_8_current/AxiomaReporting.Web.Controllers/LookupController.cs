using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Entities.Base;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

[Authorize]
public class LookupController : Controller
{
	private readonly AppDbContext _db;

	private readonly IAuditLogService _auditLog;

	private static readonly Dictionary<string, string> TableDisplayNames = new Dictionary<string, string>
	{
		["districts"] = "מחוזות",
		["sectors"] = "מגזרים",
		["localities"] = "ישובים",
		["authorities"] = "רשויות",
		["projects"] = "פרויקטים",
		["programs"] = "תוכניות",
		["educationalprograms"] = "תוכניות חינוכיות",
		["subjects"] = "נושאים",
		["domains"] = "תחומים",
		["classes"] = "כיתות",
		["gradelevels"] = "שכבות",
		["educationalstages"] = "שלבי חינוך",
		["educationtypes"] = "סוגי חינוך",
		["localitydistrictnational"] = "ישוב/מחוז/ארצי",
		["discussioncodes"] = "קיום דיון"
	};

	public LookupController(AppDbContext db, IAuditLogService auditLog)
	{
		_db = db;
		_auditLog = auditLog;
	}

	[HttpGet]
	[Route("Lookup")]
	[Authorize(Policy = "CanManageLookups")]
	public IActionResult Index()
	{
		base.ViewBag.Tables = TableDisplayNames;
		return View();
	}

	[HttpGet]
	[Route("Lookup/{tableName}")]
	[Authorize(Policy = "CanManageLookups")]
	public async Task<IActionResult> List(string tableName, string? search = null, int page = 1, int pageSize = 20)
	{
		if (!TableDisplayNames.TryGetValue(tableName.ToLower(), out string displayName))
		{
			return NotFound();
		}
		List<LookupEntity> list = await GetTableDataAsync(tableName.ToLower(), search);
		base.ViewBag.TableName = tableName;
		base.ViewBag.DisplayName = displayName;
		base.ViewBag.Search = search;
		base.ViewBag.Page = page;
		base.ViewBag.PageSize = pageSize;
		base.ViewBag.TotalItems = list.Count;
		base.ViewBag.IsAdmin = base.User.IsInRole("1");
		List<LookupEntity> model = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
		return View("List", model);
	}

	[HttpPost]
	[Route("Lookup/{tableName}/Create")]
	[Authorize(Policy = "CanManageLookups")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(string tableName, string description, int? nationalCode = null)
	{
		if (string.IsNullOrWhiteSpace(description))
		{
			base.TempData["Error"] = "תיאור לא יכול להיות ריק";
			return RedirectToAction("List", new { tableName });
		}
		int? num = await CreateItemAsync(tableName.ToLower(), description.Trim(), nationalCode);
		await _auditLog.LogAsync("Lookup.Create", tableName.ToLower(), num?.ToString(), null, new
		{
			description = description.Trim(),
			nationalCode = nationalCode
		});
		base.TempData["Success"] = "הרשומה נוצרה בהצלחה";
		return RedirectToAction("List", new { tableName });
	}

	[HttpPost]
	[Route("Lookup/{tableName}/Edit/{id}")]
	[Authorize(Policy = "CanManageLookups")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string tableName, int id, string description, bool isActive, int? nationalCode = null)
	{
		await EditItemAsync(tableName.ToLower(), id, description, isActive, nationalCode);
		await _auditLog.LogAsync("Lookup.Update", tableName.ToLower(), id.ToString(), null, new { description, isActive, nationalCode });
		base.TempData["Success"] = "הרשומה עודכנה בהצלחה";
		return RedirectToAction("List", new { tableName });
	}

	[HttpPost]
	[Route("Lookup/{tableName}/Delete/{id}")]
	[Authorize(Policy = "CanManageLookups")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(string tableName, int id)
	{
		var (flag, text) = await CanDeleteItemAsync(tableName.ToLower(), id);
		if (!flag)
		{
			base.TempData["Error"] = text ?? "לא ניתן למחוק — הערך בשימוש";
			return RedirectToAction("List", new { tableName });
		}
		await DeleteItemAsync(tableName.ToLower(), id);
		await _auditLog.LogAsync("Lookup.Delete", tableName.ToLower(), id.ToString());
		base.TempData["Success"] = "הרשומה נמחקה";
		return RedirectToAction("List", new { tableName });
	}

	[HttpPost]
	[Route("Lookup/{tableName}/ImportExcel")]
	[Authorize(Policy = "CanManageLookups")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ImportExcel(string tableName, IFormFile file)
	{
		tableName = tableName.ToLower();
		if (!TableDisplayNames.ContainsKey(tableName))
		{
			return NotFound();
		}
		if (file == null || file.Length == 0L)
		{
			base.TempData["Error"] = "לא נבחר קובץ";
			return RedirectToAction("List", new { tableName });
		}
		if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
		{
			base.TempData["Error"] = "ניתן לייבא קובץ xlsx בלבד";
			return RedirectToAction("List", new { tableName });
		}
		int imported = 0;
		int skipped = 0;
		Stream stream = file.OpenReadStream();
		IActionResult result;
		try
		{
			using XLWorkbook workbook = new XLWorkbook(stream);
			IXLWorksheet ws = workbook.Worksheets.FirstOrDefault();
			int lastRow = (ws?.LastRowUsed()?.RowNumber()).GetValueOrDefault();
			for (int row = 2; row <= lastRow; row++)
			{
				string description = ws.Row(row).Cell(1).GetString()
					.Trim();
				if (string.IsNullOrWhiteSpace(description))
				{
					skipped++;
					continue;
				}
				if (await DescriptionExistsAsync(tableName, description))
				{
					skipped++;
					continue;
				}
				await CreateItemAsync(tableName, description);
				imported++;
			}
			base.TempData["Success"] = $"יובאו {imported} רשומות. דולגו {skipped}.";
			result = RedirectToAction("List", new { tableName });
		}
		finally
		{
			if (stream != null)
			{
				await stream.DisposeAsync();
			}
		}
		return result;
	}

	private async Task<List<LookupEntity>> GetTableDataAsync(string tableName, string? search)
	{
		return tableName switch
		{
			"districts" => (await FilterAndSearch(_db.Districts, search)).Cast<LookupEntity>().ToList(), 
			"sectors" => (await FilterAndSearch(_db.Sectors, search)).Cast<LookupEntity>().ToList(), 
			"localities" => (await FilterAndSearch(_db.Localities, search)).Cast<LookupEntity>().ToList(), 
			"authorities" => (await FilterAndSearch(_db.Authorities, search)).Cast<LookupEntity>().ToList(), 
			"projects" => (await FilterAndSearch(_db.Projects, search)).Cast<LookupEntity>().ToList(), 
			"programs" => (await FilterAndSearch(_db.Programs, search)).Cast<LookupEntity>().ToList(), 
			"educationalprograms" => (await FilterAndSearch(_db.EducationalPrograms, search)).Cast<LookupEntity>().ToList(), 
			"subjects" => (await FilterAndSearch(_db.Subjects, search)).Cast<LookupEntity>().ToList(), 
			"domains" => (await FilterAndSearch(_db.Domains, search)).Cast<LookupEntity>().ToList(), 
			"classes" => (await FilterAndSearch(_db.Classes, search)).Cast<LookupEntity>().ToList(), 
			"gradelevels" => (await FilterAndSearch(_db.GradeLevels, search)).Cast<LookupEntity>().ToList(), 
			"educationalstages" => (await FilterAndSearch(_db.EducationalStages, search)).Cast<LookupEntity>().ToList(), 
			"educationtypes" => (await FilterAndSearch(_db.EducationTypes, search)).Cast<LookupEntity>().ToList(), 
			"localitydistrictnational" => (await FilterAndSearch(_db.LocalityDistrictNationals, search)).Cast<LookupEntity>().ToList(), 
			"discussioncodes" => (await FilterAndSearch(_db.DiscussionCodes, search)).Cast<LookupEntity>().ToList(), 
			_ => new List<LookupEntity>(), 
		};
	}

	private static async Task<List<T>> FilterAndSearch<T>(IQueryable<T> query, string? search) where T : LookupEntity
	{
		string search2 = search;
		if (!string.IsNullOrWhiteSpace(search2))
		{
			query = query.Where((T x) => EF.Functions.Like(x.Description, $"%{search2}%"));
		}
		return await query.OrderBy((T x) => x.Description).ToListAsync();
	}

	private async Task<int?> CreateItemAsync(string tableName, string description)
	{
		return await CreateItemAsync(tableName, description, null);
	}

	private async Task<int?> CreateItemAsync(string tableName, string description, int? nationalCode)
	{
		DateTime utcNow = DateTime.UtcNow;
		LookupEntity created = null;
		switch (tableName)
		{
		case "districts":
			created = new District
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.Districts.Add((District)created);
			break;
		case "sectors":
			created = new Sector
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.Sectors.Add((Sector)created);
			break;
		case "localities":
			created = new Locality
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow,
				NationalCode = nationalCode
			};
			_db.Localities.Add((Locality)created);
			break;
		case "authorities":
			created = new Authority
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.Authorities.Add((Authority)created);
			break;
		case "projects":
			created = new Project
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.Projects.Add((Project)created);
			break;
		case "programs":
			created = new AxiomaReporting.Core.Entities.Program
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.Programs.Add((AxiomaReporting.Core.Entities.Program)created);
			break;
		case "educationalprograms":
			created = new EducationalProgram
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.EducationalPrograms.Add((EducationalProgram)created);
			break;
		case "subjects":
			created = new Subject
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.Subjects.Add((Subject)created);
			break;
		case "domains":
			created = new Domain
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.Domains.Add((Domain)created);
			break;
		case "classes":
			created = new SchoolClass
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.Classes.Add((SchoolClass)created);
			break;
		case "gradelevels":
			created = new GradeLevel
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.GradeLevels.Add((GradeLevel)created);
			break;
		case "educationalstages":
			created = new EducationalStage
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.EducationalStages.Add((EducationalStage)created);
			break;
		case "educationtypes":
			created = new EducationType
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.EducationTypes.Add((EducationType)created);
			break;
		case "localitydistrictnational":
			created = new LocalityDistrictNational
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.LocalityDistrictNationals.Add((LocalityDistrictNational)created);
			break;
		case "discussioncodes":
			created = new DiscussionCode
			{
				Description = description,
				IsActive = true,
				CreatedAt = utcNow
			};
			_db.DiscussionCodes.Add((DiscussionCode)created);
			break;
		}
		await _db.SaveChangesAsync();
		return created?.Id;
	}

	private async Task<bool> DescriptionExistsAsync(string tableName, string description)
	{
		string description2 = description;
		return tableName switch
		{
			"districts" => await _db.Districts.AnyAsync((District x) => x.Description == description2), 
			"sectors" => await _db.Sectors.AnyAsync((Sector x) => x.Description == description2), 
			"localities" => await _db.Localities.AnyAsync((Locality x) => x.Description == description2), 
			"authorities" => await _db.Authorities.AnyAsync((Authority x) => x.Description == description2), 
			"projects" => await _db.Projects.AnyAsync((Project x) => x.Description == description2), 
			"programs" => await _db.Programs.AnyAsync((AxiomaReporting.Core.Entities.Program x) => x.Description == description2), 
			"educationalprograms" => await _db.EducationalPrograms.AnyAsync((EducationalProgram x) => x.Description == description2), 
			"subjects" => await _db.Subjects.AnyAsync((Subject x) => x.Description == description2), 
			"domains" => await _db.Domains.AnyAsync((Domain x) => x.Description == description2), 
			"classes" => await _db.Classes.AnyAsync((SchoolClass x) => x.Description == description2), 
			"gradelevels" => await _db.GradeLevels.AnyAsync((GradeLevel x) => x.Description == description2), 
			"educationalstages" => await _db.EducationalStages.AnyAsync((EducationalStage x) => x.Description == description2), 
			"educationtypes" => await _db.EducationTypes.AnyAsync((EducationType x) => x.Description == description2), 
			"localitydistrictnational" => await _db.LocalityDistrictNationals.AnyAsync((LocalityDistrictNational x) => x.Description == description2), 
			"discussioncodes" => await _db.DiscussionCodes.AnyAsync((DiscussionCode x) => x.Description == description2), 
			_ => true, 
		};
	}

	private async Task EditItemAsync(string tableName, int id, string description, bool isActive, int? nationalCode = null)
	{
		LookupEntity lookupEntity = tableName switch
		{
			"districts" => await _db.Districts.FindAsync(id), 
			"sectors" => await _db.Sectors.FindAsync(id), 
			"localities" => await _db.Localities.FindAsync(id), 
			"authorities" => await _db.Authorities.FindAsync(id), 
			"projects" => await _db.Projects.FindAsync(id), 
			"programs" => await _db.Programs.FindAsync(id), 
			"educationalprograms" => await _db.EducationalPrograms.FindAsync(id), 
			"subjects" => await _db.Subjects.FindAsync(id), 
			"domains" => await _db.Domains.FindAsync(id), 
			"classes" => await _db.Classes.FindAsync(id), 
			"gradelevels" => await _db.GradeLevels.FindAsync(id), 
			"educationalstages" => await _db.EducationalStages.FindAsync(id), 
			"educationtypes" => await _db.EducationTypes.FindAsync(id), 
			"localitydistrictnational" => await _db.LocalityDistrictNationals.FindAsync(id), 
			"discussioncodes" => await _db.DiscussionCodes.FindAsync(id), 
			_ => null, 
		};
		if (lookupEntity != null)
		{
			lookupEntity.Description = description;
			lookupEntity.IsActive = isActive;
			lookupEntity.UpdatedAt = DateTime.UtcNow;
			if (tableName == "localities" && lookupEntity is Locality locality)
			{
				locality.NationalCode = nationalCode;
			}
			await _db.SaveChangesAsync();
		}
	}

	internal async Task<(bool CanDelete, string? Reason)> CanDeleteItemAsync(string tableName, int id)
	{
		string text;
		switch (tableName)
		{
		case "districts":
		{
			string text15;
			if (await _db.ReportRows.AnyAsync((ReportRow r) => r.DistrictId == id))
			{
				text15 = "דיווחים";
			}
			else
			{
				string text16;
				if (await _db.Set<AllocationDistrict>().AnyAsync((AllocationDistrict a) => a.DistrictId == id))
				{
					text16 = "הקצאות";
				}
				else
				{
					string text17 = ((!(await _db.Institutions.AnyAsync((Institution i) => i.DistrictId == (int?)id))) ? ((await _db.InspectorAssignments.AnyAsync((InspectorAssignment a) => a.DistrictId == (int?)id)) ? "שיוכי פיקוח" : null) : "מוסדות");
					text16 = text17;
				}
				text15 = text16;
			}
			text = text15;
			break;
		}
		case "sectors":
		{
			string text6;
			if (await _db.Set<AllocationSector>().AnyAsync((AllocationSector a) => a.SectorId == id))
			{
				text6 = "הקצאות";
			}
			else
			{
				string text7 = ((!(await _db.Institutions.AnyAsync((Institution i) => i.SectorId == (int?)id))) ? ((await _db.InspectorAssignments.AnyAsync((InspectorAssignment a) => a.SectorId == (int?)id)) ? "שיוכי פיקוח" : null) : "מוסדות");
				text6 = text7;
			}
			text = text6;
			break;
		}
		case "localities":
		{
			string text13;
			if (await _db.ReportRows.AnyAsync((ReportRow r) => r.LocalityId == id))
			{
				text13 = "דיווחים";
			}
			else
			{
				string text14 = ((!(await _db.Set<AllocationLocality>().AnyAsync((AllocationLocality a) => a.LocalityId == id))) ? ((await _db.Institutions.AnyAsync((Institution i) => i.LocalityId == (int?)id)) ? "מוסדות" : null) : "הקצאות");
				text13 = text14;
			}
			text = text13;
			break;
		}
		case "projects":
		{
			string text11 = ((!(await _db.Allocations.AnyAsync((Allocation a) => a.ProjectId == id))) ? ((await _db.Set<ProjectProgram>().AnyAsync((ProjectProgram pp) => pp.ProjectId == id)) ? "שיוכי פרויקט-תוכנית" : null) : "הקצאות");
			text = text11;
			break;
		}
		case "programs":
		{
			string text3;
			if (await _db.Set<AllocationProgram>().AnyAsync((AllocationProgram a) => a.ProgramId == id))
			{
				text3 = "הקצאות";
			}
			else
			{
				string text4 = ((!(await _db.Set<ProjectProgram>().AnyAsync((ProjectProgram pp) => pp.ProgramId == id))) ? ((await _db.InspectorAssignments.AnyAsync((InspectorAssignment a) => a.ProgramId == (int?)id)) ? "שיוכי פיקוח" : null) : "שיוכי פרויקט-תוכנית");
				text3 = text4;
			}
			text = text3;
			break;
		}
		case "subjects":
		{
			string text8 = ((!(await _db.ReportRows.AnyAsync((ReportRow r) => r.Subject1Id == id || r.Subject2Id == (int?)id))) ? ((await _db.Set<AllocationSubject>().AnyAsync((AllocationSubject a) => a.SubjectId == id)) ? "הקצאות" : null) : "דיווחים");
			text = text8;
			break;
		}
		case "domains":
		{
			string text19 = ((!(await _db.ReportRows.AnyAsync((ReportRow r) => r.DomainId == id))) ? ((await _db.Set<AllocationDomain>().AnyAsync((AllocationDomain a) => a.DomainId == id)) ? "הקצאות" : null) : "דיווחים");
			text = text19;
			break;
		}
		case "educationalprograms":
		{
			string text5 = ((!(await _db.ReportRows.AnyAsync((ReportRow r) => r.EducationalProgramId == id))) ? ((await _db.Set<AllocationEducationalProgram>().AnyAsync((AllocationEducationalProgram a) => a.EducationalProgramId == id)) ? "הקצאות" : null) : "דיווחים");
			text = text5;
			break;
		}
		case "classes":
		{
			string text20 = ((!(await _db.ReportRows.AnyAsync((ReportRow r) => r.ClassId == (int?)id || r.ConclusionClassId == (int?)id))) ? ((await _db.Set<AllocationClass>().AnyAsync((AllocationClass a) => a.ClassId == id)) ? "הקצאות" : null) : "דיווחים");
			text = text20;
			break;
		}
		case "gradelevels":
		{
			string text12 = ((!(await _db.ReportRows.AnyAsync((ReportRow r) => r.GradeLevelId == (int?)id))) ? ((await _db.Set<AllocationGradeLevel>().AnyAsync((AllocationGradeLevel a) => a.GradeLevelId == id)) ? "הקצאות" : null) : "דיווחים");
			text = text12;
			break;
		}
		case "discussioncodes":
		{
			string text21 = ((!(await _db.ReportRows.AnyAsync((ReportRow r) => r.DiscussionCodeId == (int?)id))) ? ((await _db.Set<AllocationDiscussionCode>().AnyAsync((AllocationDiscussionCode a) => a.DiscussionCodeId == id)) ? "הקצאות" : null) : "דיווחים");
			text = text21;
			break;
		}
		case "frameworks":
		{
			string text9;
			if (await _db.ReportRows.AnyAsync((ReportRow r) => r.FrameworkId == id))
			{
				text9 = "דיווחים";
			}
			else
			{
				string text10 = ((!(await _db.ReportRows.AnyAsync((ReportRow r) => r.ConclusionFrameworkId == (int?)id))) ? ((await _db.Set<AllocationFramework>().AnyAsync((AllocationFramework a) => a.FrameworkId == id)) ? "הקצאות" : null) : "דיווחי סיכום");
				text9 = text10;
			}
			text = text9;
			break;
		}
		case "educationalstages":
		{
			string text18 = ((!(await _db.Frameworks.AnyAsync((Framework f) => f.EducationalStageId == (int?)id))) ? ((await _db.Institutions.AnyAsync((Institution i) => i.EducationalStageId == (int?)id)) ? "מוסדות" : null) : "מסגרות");
			text = text18;
			break;
		}
		case "educationtypes":
			text = ((await _db.Institutions.AnyAsync((Institution i) => i.TypeId == (int?)id)) ? "מוסדות" : null);
			break;
		case "localitydistrictnational":
		{
			string text2 = ((!(await _db.ReportRows.AnyAsync((ReportRow r) => r.ConclusionLocationId == (int?)id))) ? ((await _db.Set<AllocationLocalityDistrictNational>().AnyAsync((AllocationLocalityDistrictNational a) => a.LocalityDistrictNationalId == id)) ? "הקצאות" : null) : "דיווחים");
			text = text2;
			break;
		}
		case "institutions":
			text = await InstitutionInUseAsync(id);
			break;
		case "authorities":
			text = null;
			break;
		default:
			text = null;
			break;
		}
		string text22 = text;
		return (text22 == null) ? (CanDelete: true, Reason: null) : (CanDelete: false, Reason: "לא ניתן למחוק — הערך בשימוש במערכת: " + text22);
	}

	private async Task<string?> InstitutionInUseAsync(int id)
	{
		Institution institution = await _db.Institutions.FindAsync(id);
		if (institution == null)
		{
			return null;
		}
		string symbol = institution.InstitutionSymbol.ToString();
		int? stageId = institution.EducationalStageId;
		if (await _db.Frameworks.AnyAsync((Framework f) => f.InstitutionSymbol == symbol && f.EducationalStageId == stageId))
		{
			return "מסגרות";
		}
		return null;
	}

	private async Task DeleteItemAsync(string tableName, int id)
	{
		switch (tableName)
		{
		case "districts":
		{
			District district = await _db.Districts.FindAsync(id);
			if (district != null)
			{
				_db.Districts.Remove(district);
			}
			break;
		}
		case "sectors":
		{
			Sector sector = await _db.Sectors.FindAsync(id);
			if (sector != null)
			{
				_db.Sectors.Remove(sector);
			}
			break;
		}
		case "localities":
		{
			Locality locality = await _db.Localities.FindAsync(id);
			if (locality != null)
			{
				_db.Localities.Remove(locality);
			}
			break;
		}
		case "authorities":
		{
			Authority authority = await _db.Authorities.FindAsync(id);
			if (authority != null)
			{
				_db.Authorities.Remove(authority);
			}
			break;
		}
		case "projects":
		{
			Project project = await _db.Projects.FindAsync(id);
			if (project != null)
			{
				_db.Projects.Remove(project);
			}
			break;
		}
		case "programs":
		{
			AxiomaReporting.Core.Entities.Program program = await _db.Programs.FindAsync(id);
			if (program != null)
			{
				_db.Programs.Remove(program);
			}
			break;
		}
		case "educationalprograms":
		{
			EducationalProgram educationalProgram = await _db.EducationalPrograms.FindAsync(id);
			if (educationalProgram != null)
			{
				_db.EducationalPrograms.Remove(educationalProgram);
			}
			break;
		}
		case "subjects":
		{
			Subject subject = await _db.Subjects.FindAsync(id);
			if (subject != null)
			{
				_db.Subjects.Remove(subject);
			}
			break;
		}
		case "domains":
		{
			Domain domain = await _db.Domains.FindAsync(id);
			if (domain != null)
			{
				_db.Domains.Remove(domain);
			}
			break;
		}
		case "classes":
		{
			SchoolClass schoolClass = await _db.Classes.FindAsync(id);
			if (schoolClass != null)
			{
				_db.Classes.Remove(schoolClass);
			}
			break;
		}
		case "gradelevels":
		{
			GradeLevel gradeLevel = await _db.GradeLevels.FindAsync(id);
			if (gradeLevel != null)
			{
				_db.GradeLevels.Remove(gradeLevel);
			}
			break;
		}
		case "educationalstages":
		{
			EducationalStage educationalStage = await _db.EducationalStages.FindAsync(id);
			if (educationalStage != null)
			{
				_db.EducationalStages.Remove(educationalStage);
			}
			break;
		}
		case "educationtypes":
		{
			EducationType educationType = await _db.EducationTypes.FindAsync(id);
			if (educationType != null)
			{
				_db.EducationTypes.Remove(educationType);
			}
			break;
		}
		case "localitydistrictnational":
		{
			LocalityDistrictNational localityDistrictNational = await _db.LocalityDistrictNationals.FindAsync(id);
			if (localityDistrictNational != null)
			{
				_db.LocalityDistrictNationals.Remove(localityDistrictNational);
			}
			break;
		}
		case "discussioncodes":
		{
			DiscussionCode discussionCode = await _db.DiscussionCodes.FindAsync(id);
			if (discussionCode != null)
			{
				_db.DiscussionCodes.Remove(discussionCode);
			}
			break;
		}
		}
		await _db.SaveChangesAsync();
	}
}

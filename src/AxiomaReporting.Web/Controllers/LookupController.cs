using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Entities.Base;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Web.Authorization;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

[Authorize]
public class LookupController : Controller
{
  private readonly AppDbContext _db;
  private readonly IAuditLogService _auditLog;

  public LookupController(AppDbContext db, IAuditLogService auditLog)
  {
    _db = db;
    _auditLog = auditLog;
  }

  private static readonly Dictionary<string, string> TableDisplayNames = new()
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
    ["discussioncodes"] = "קיום דיון",
    ["roles"] = "תפקידים",
    ["classconclusions"] = "מסקנות כיתה",
    ["frameworkconclusions"] = "מסקנות מסגרת חינוכית",
  };

  [HttpGet]
  [Route("Lookup")]
  [Authorize(Policy = PolicyNames.CanManageLookups)]
  public IActionResult Index()
  {
    ViewBag.Tables = TableDisplayNames;
    return View();
  }

  [HttpGet]
  [Route("Lookup/ExportAll")]
  [Authorize(Policy = PolicyNames.CanManageLookups)]
  public async Task<IActionResult> ExportAllExcel()
  {
    using var workbook = new XLWorkbook();

    foreach (var table in TableDisplayNames)
    {
      var worksheet = AddWorksheet(workbook, table.Value);
      WriteLookupHeaders(worksheet);

      var items = await GetTableDataAsync(table.Key, search: null);
      var row = 2;
      foreach (var item in items.OrderBy(x => x.Id))
      {
        worksheet.Cell(row, 1).Value = item.Id;
        worksheet.Cell(row, 2).Value = item.Description;
        worksheet.Cell(row, 3).Value = item.IsActive;
        row++;
      }

      FormatLookupWorksheet(worksheet);
    }

    var frameworksWorksheet = AddWorksheet(workbook, "מסגרות חינוכיות");
    var frameworkHeaders = new[] { "Id", "Description", "IsActive", "InstitutionSymbol", "EducationalStage" };
    for (var column = 0; column < frameworkHeaders.Length; column++)
      frameworksWorksheet.Cell(1, column + 1).Value = frameworkHeaders[column];

    var frameworks = await _db.Frameworks
      .AsNoTracking()
      .Include(x => x.EducationalStage)
      .OrderBy(x => x.Id)
      .ToListAsync();
    var frameworkRow = 2;
    foreach (var framework in frameworks)
    {
      frameworksWorksheet.Cell(frameworkRow, 1).Value = framework.Id;
      frameworksWorksheet.Cell(frameworkRow, 2).Value = framework.Description;
      frameworksWorksheet.Cell(frameworkRow, 3).Value = framework.IsActive;
      frameworksWorksheet.Cell(frameworkRow, 4).Value = framework.InstitutionSymbol;
      frameworksWorksheet.Cell(frameworkRow, 5).Value = framework.EducationalStage?.Description;
      frameworkRow++;
    }
    FormatLookupWorksheet(frameworksWorksheet);

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    return File(
      stream.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"lookup_tables_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
  }

  [HttpGet]
  [Route("Lookup/{tableName}")]
  [Authorize(Policy = PolicyNames.CanManageLookups)]
  public async Task<IActionResult> List(string tableName, string? search = null, int page = 1, int pageSize = 20)
  {
    if (!TableDisplayNames.TryGetValue(tableName.ToLower(), out var displayName))
      return NotFound();

    var items = await GetTableDataAsync(tableName.ToLower(), search);

    ViewBag.TableName = tableName;
    ViewBag.DisplayName = displayName;
    ViewBag.Search = search;
    ViewBag.Page = page;
    ViewBag.PageSize = pageSize;
    ViewBag.TotalItems = items.Count;
    ViewBag.IsAdmin = User.IsInRole("1");

    var paged = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    return View("List", paged);
  }

  [HttpPost]
  [Route("Lookup/{tableName}/Create")]
  [Authorize(Policy = PolicyNames.CanManageLookups)]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(string tableName, string description, int? nationalCode = null)
  {
    if (string.IsNullOrWhiteSpace(description))
    {
      TempData["Error"] = "תיאור לא יכול להיות ריק";
      return RedirectToAction("List", new { tableName });
    }
    var newId = await CreateItemAsync(tableName.ToLower(), description.Trim(), nationalCode);
    await _auditLog.LogAsync("Lookup.Create", tableName.ToLower(), newId?.ToString(),
      after: new { description = description.Trim(), nationalCode });
    TempData["Success"] = "הרשומה נוצרה בהצלחה";
    return RedirectToAction("List", new { tableName });
  }

  [HttpPost]
  [Route("Lookup/{tableName}/Edit/{id}")]
  [Authorize(Policy = PolicyNames.CanManageLookups)]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(string tableName, int id, string description, bool isActive, int? nationalCode = null)
  {
    await EditItemAsync(tableName.ToLower(), id, description, isActive, nationalCode);
    await _auditLog.LogAsync("Lookup.Update", tableName.ToLower(), id.ToString(),
      after: new { description, isActive, nationalCode });
    TempData["Success"] = "הרשומה עודכנה בהצלחה";
    return RedirectToAction("List", new { tableName });
  }

  [HttpPost]
  [Route("Lookup/{tableName}/Delete/{id}")]
  [Authorize(Policy = PolicyNames.CanManageLookups)]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Delete(string tableName, int id)
  {
    var (canDelete, reason) = await CanDeleteItemAsync(tableName.ToLower(), id);
    if (!canDelete)
    {
      TempData["Error"] = reason ?? "לא ניתן למחוק — הערך בשימוש";
      return RedirectToAction("List", new { tableName });
    }
    await DeleteItemAsync(tableName.ToLower(), id);
    await _auditLog.LogAsync("Lookup.Delete", tableName.ToLower(), id.ToString());
    TempData["Success"] = "הרשומה נמחקה";
    return RedirectToAction("List", new { tableName });
  }

  [HttpPost]
  [Route("Lookup/{tableName}/ImportExcel")]
  [Authorize(Policy = PolicyNames.CanManageLookups)]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ImportExcel(string tableName, IFormFile file)
  {
    tableName = tableName.ToLower();
    if (!TableDisplayNames.ContainsKey(tableName)) return NotFound();

    if (file == null || file.Length == 0)
    {
      TempData["Error"] = "לא נבחר קובץ";
      return RedirectToAction("List", new { tableName });
    }

    if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
    {
      TempData["Error"] = "ניתן לייבא קובץ xlsx בלבד";
      return RedirectToAction("List", new { tableName });
    }

    var imported = 0;
    var skipped = 0;
    await using var stream = file.OpenReadStream();
    using var workbook = new XLWorkbook(stream);
    var ws = workbook.Worksheets.FirstOrDefault();
    var lastRow = ws?.LastRowUsed()?.RowNumber() ?? 0;

    for (var row = 2; row <= lastRow; row++)
    {
      var description = ws!.Row(row).Cell(1).GetString().Trim();
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

    TempData["Success"] = $"יובאו {imported} רשומות. דולגו {skipped}.";
    return RedirectToAction("List", new { tableName });
  }

  // --- Private dispatch methods ---

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
      "roles" => (await FilterAndSearch(_db.Roles, search)).Cast<LookupEntity>().ToList(),
      "classconclusions" => (await FilterAndSearch(_db.ClassConclusions, search)).Cast<LookupEntity>().ToList(),
      "frameworkconclusions" => (await FilterAndSearch(_db.FrameworkConclusions, search)).Cast<LookupEntity>().ToList(),
      _ => new List<LookupEntity>()
    };
  }

  private static async Task<List<T>> FilterAndSearch<T>(IQueryable<T> query, string? search) where T : LookupEntity
  {
    if (!string.IsNullOrWhiteSpace(search))
      query = query.Where(x => EF.Functions.Like(x.Description, $"%{search}%"));
    return await query.AsNoTracking().OrderBy(x => x.Description).ToListAsync();
  }

  private static IXLWorksheet AddWorksheet(XLWorkbook workbook, string requestedName)
  {
    const string invalidCharacters = "\\/?:*[]";
    var sanitized = new string(requestedName
      .Select(character => invalidCharacters.Contains(character) ? '-' : character)
      .ToArray());
    if (sanitized.Length > 31) sanitized = sanitized[..31];

    var name = sanitized;
    var suffix = 2;
    while (workbook.Worksheets.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
    {
      var suffixText = $" ({suffix++})";
      name = $"{sanitized[..Math.Min(sanitized.Length, 31 - suffixText.Length)]}{suffixText}";
    }

    var worksheet = workbook.Worksheets.Add(name);
    worksheet.RightToLeft = true;
    return worksheet;
  }

  private static void WriteLookupHeaders(IXLWorksheet worksheet)
  {
    worksheet.Cell(1, 1).Value = "Id";
    worksheet.Cell(1, 2).Value = "Description";
    worksheet.Cell(1, 3).Value = "IsActive";
  }

  private static void FormatLookupWorksheet(IXLWorksheet worksheet)
  {
    worksheet.Row(1).Style.Font.Bold = true;
    worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightBlue;
    worksheet.SheetView.FreezeRows(1);
    worksheet.RangeUsed()?.SetAutoFilter();
    worksheet.Columns().AdjustToContents(1d, 60d);
  }

  private async Task<int?> CreateItemAsync(string tableName, string description) =>
    await CreateItemAsync(tableName, description, null);

  private async Task<int?> CreateItemAsync(string tableName, string description, int? nationalCode)
  {
    var now = DateTime.UtcNow;
    LookupEntity? created = null;
    switch (tableName)
    {
      case "districts": created = new District { Description = description, IsActive = true, CreatedAt = now }; _db.Districts.Add((District)created); break;
      case "sectors": created = new Sector { Description = description, IsActive = true, CreatedAt = now }; _db.Sectors.Add((Sector)created); break;
      case "localities": created = new Locality { Description = description, IsActive = true, CreatedAt = now, NationalCode = nationalCode }; _db.Localities.Add((Locality)created); break;
      case "authorities": created = new Authority { Description = description, IsActive = true, CreatedAt = now }; _db.Authorities.Add((Authority)created); break;
      case "projects": created = new Project { Description = description, IsActive = true, CreatedAt = now }; _db.Projects.Add((Project)created); break;
      case "programs": created = new Core.Entities.Program { Description = description, IsActive = true, CreatedAt = now }; _db.Programs.Add((Core.Entities.Program)created); break;
      case "educationalprograms": created = new EducationalProgram { Description = description, IsActive = true, CreatedAt = now }; _db.EducationalPrograms.Add((EducationalProgram)created); break;
      case "subjects": created = new Subject { Description = description, IsActive = true, CreatedAt = now }; _db.Subjects.Add((Subject)created); break;
      case "domains": created = new Domain { Description = description, IsActive = true, CreatedAt = now }; _db.Domains.Add((Domain)created); break;
      case "classes": created = new SchoolClass { Description = description, IsActive = true, CreatedAt = now }; _db.Classes.Add((SchoolClass)created); break;
      case "gradelevels": created = new GradeLevel { Description = description, IsActive = true, CreatedAt = now }; _db.GradeLevels.Add((GradeLevel)created); break;
      case "educationalstages": created = new EducationalStage { Description = description, IsActive = true, CreatedAt = now }; _db.EducationalStages.Add((EducationalStage)created); break;
      case "educationtypes": created = new EducationType { Description = description, IsActive = true, CreatedAt = now }; _db.EducationTypes.Add((EducationType)created); break;
      case "localitydistrictnational": created = new LocalityDistrictNational { Description = description, IsActive = true, CreatedAt = now }; _db.LocalityDistrictNationals.Add((LocalityDistrictNational)created); break;
      case "discussioncodes": created = new DiscussionCode { Description = description, IsActive = true, CreatedAt = now }; _db.DiscussionCodes.Add((DiscussionCode)created); break;
      case "roles": created = new EmployeeRole { Description = description, IsActive = true, CreatedAt = now }; _db.Roles.Add((EmployeeRole)created); break;
      case "classconclusions": created = new ClassConclusion { Description = description, IsActive = true, CreatedAt = now }; _db.ClassConclusions.Add((ClassConclusion)created); break;
      case "frameworkconclusions": created = new FrameworkConclusion { Description = description, IsActive = true, CreatedAt = now }; _db.FrameworkConclusions.Add((FrameworkConclusion)created); break;
    }
    await _db.SaveChangesAsync();
    return created?.Id;
  }

  private async Task<bool> DescriptionExistsAsync(string tableName, string description)
  {
    return tableName switch
    {
      "districts" => await _db.Districts.AnyAsync(x => x.Description == description),
      "sectors" => await _db.Sectors.AnyAsync(x => x.Description == description),
      "localities" => await _db.Localities.AnyAsync(x => x.Description == description),
      "authorities" => await _db.Authorities.AnyAsync(x => x.Description == description),
      "projects" => await _db.Projects.AnyAsync(x => x.Description == description),
      "programs" => await _db.Programs.AnyAsync(x => x.Description == description),
      "educationalprograms" => await _db.EducationalPrograms.AnyAsync(x => x.Description == description),
      "subjects" => await _db.Subjects.AnyAsync(x => x.Description == description),
      "domains" => await _db.Domains.AnyAsync(x => x.Description == description),
      "classes" => await _db.Classes.AnyAsync(x => x.Description == description),
      "gradelevels" => await _db.GradeLevels.AnyAsync(x => x.Description == description),
      "educationalstages" => await _db.EducationalStages.AnyAsync(x => x.Description == description),
      "educationtypes" => await _db.EducationTypes.AnyAsync(x => x.Description == description),
      "localitydistrictnational" => await _db.LocalityDistrictNationals.AnyAsync(x => x.Description == description),
      "discussioncodes" => await _db.DiscussionCodes.AnyAsync(x => x.Description == description),
      "roles" => await _db.Roles.AnyAsync(x => x.Description == description),
      "classconclusions" => await _db.ClassConclusions.AnyAsync(x => x.Description == description),
      "frameworkconclusions" => await _db.FrameworkConclusions.AnyAsync(x => x.Description == description),
      _ => true
    };
  }

  private async Task EditItemAsync(string tableName, int id, string description, bool isActive, int? nationalCode = null)
  {
    LookupEntity? entity = tableName switch
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
      "roles" => await _db.Roles.FindAsync(id),
      "classconclusions" => await _db.ClassConclusions.FindAsync(id),
      "frameworkconclusions" => await _db.FrameworkConclusions.FindAsync(id),
      _ => null
    };
    if (entity == null) return;
    entity.Description = description;
    entity.IsActive = isActive;
    entity.UpdatedAt = DateTime.UtcNow;
    if (tableName == "localities" && entity is Locality loc)
    {
      loc.NationalCode = nationalCode;
    }
    await _db.SaveChangesAsync();
  }

  internal async Task<(bool CanDelete, string? Reason)> CanDeleteItemAsync(string tableName, int id)
  {
    string? context = tableName switch
    {
      "districts" => await _db.ReportRows.AnyAsync(r => r.DistrictId == id) ? "דיווחים"
                     : await _db.Set<AllocationDistrict>().AnyAsync(a => a.DistrictId == id) ? "הקצאות"
                     : await _db.Institutions.AnyAsync(i => i.DistrictId == id) ? "מוסדות"
                     : await _db.InspectorAssignments.AnyAsync(a => a.DistrictId == id) ? "שיוכי פיקוח"
                     : null,
      "sectors" => await _db.Set<AllocationSector>().AnyAsync(a => a.SectorId == id) ? "הקצאות"
                   : await _db.Institutions.AnyAsync(i => i.SectorId == id) ? "מוסדות"
                   : await _db.InspectorAssignments.AnyAsync(a => a.SectorId == id) ? "שיוכי פיקוח"
                   : null,
      "localities" => await _db.ReportRows.AnyAsync(r => r.LocalityId == id) ? "דיווחים"
                      : await _db.Set<AllocationLocality>().AnyAsync(a => a.LocalityId == id) ? "הקצאות"
                      : await _db.Institutions.AnyAsync(i => i.LocalityId == id) ? "מוסדות"
                      : null,
      "projects" => await _db.Allocations.AnyAsync(a => a.ProjectId == id) ? "הקצאות"
                    : await _db.Set<ProjectProgram>().AnyAsync(pp => pp.ProjectId == id) ? "שיוכי פרויקט-תוכנית"
                    : null,
      "programs" => await _db.Set<AllocationProgram>().AnyAsync(a => a.ProgramId == id) ? "הקצאות"
                    : await _db.Set<ProjectProgram>().AnyAsync(pp => pp.ProgramId == id) ? "שיוכי פרויקט-תוכנית"
                    : await _db.InspectorAssignments.AnyAsync(a => a.ProgramId == id) ? "שיוכי פיקוח"
                    : null,
      "subjects" => await _db.ReportRows.AnyAsync(r => r.Subject1Id == id || r.Subject2Id == id) ? "דיווחים"
                    : await _db.Set<AllocationSubject>().AnyAsync(a => a.SubjectId == id) ? "הקצאות"
                    : null,
      "domains" => await _db.ReportRows.AnyAsync(r => r.DomainId == id) ? "דיווחים"
                   : await _db.Set<AllocationDomain>().AnyAsync(a => a.DomainId == id) ? "הקצאות"
                   : null,
      "educationalprograms" => await _db.ReportRows.AnyAsync(r => r.EducationalProgramId == id) ? "דיווחים"
                               : await _db.Set<AllocationEducationalProgram>().AnyAsync(a => a.EducationalProgramId == id) ? "הקצאות"
                               : null,
      // ConclusionClassId FKs the separate ClassConclusions table, not SchoolClasses.
      "classes" => await _db.ReportRows.AnyAsync(r => r.ClassId == id) ? "דיווחים"
                   : await _db.Set<AllocationClass>().AnyAsync(a => a.ClassId == id) ? "הקצאות"
                   : null,
      "gradelevels" => await _db.ReportRows.AnyAsync(r => r.GradeLevelId == id) ? "דיווחים"
                       : await _db.Set<AllocationGradeLevel>().AnyAsync(a => a.GradeLevelId == id) ? "הקצאות"
                       : null,
      "discussioncodes" => await _db.ReportRows.AnyAsync(r => r.DiscussionCodeId == id) ? "דיווחים"
                           : await _db.Set<AllocationDiscussionCode>().AnyAsync(a => a.DiscussionCodeId == id) ? "הקצאות"
                           : null,
      // ConclusionFrameworkId FKs the separate FrameworkConclusions table, not Frameworks.
      "frameworks" => await _db.ReportRows.AnyAsync(r => r.FrameworkId == id) ? "דיווחים"
                      : await _db.Set<AllocationFramework>().AnyAsync(a => a.FrameworkId == id) ? "הקצאות"
                      : null,
      "educationalstages" => await _db.Frameworks.AnyAsync(f => f.EducationalStageId == id) ? "מסגרות"
                             : await _db.Institutions.AnyAsync(i => i.EducationalStageId == id) ? "מוסדות"
                             : null,
      "educationtypes" => await _db.Institutions.AnyAsync(i => i.TypeId == id) ? "מוסדות" : null,
      "localitydistrictnational" => await _db.ReportRows.AnyAsync(r => r.ConclusionLocationId == id) ? "דיווחים"
                                    : await _db.Set<AllocationLocalityDistrictNational>().AnyAsync(a => a.LocalityDistrictNationalId == id) ? "הקצאות"
                                    : null,
      "institutions" => await InstitutionInUseAsync(id),
      "authorities" => null,
      "roles" => await _db.Users.AnyAsync(u => u.RoleId == id) ? "עובדים" : null,
      "classconclusions" => await _db.ReportRows.AnyAsync(r => r.ConclusionClassId == id) ? "דיווחים" : null,
      "frameworkconclusions" => await _db.ReportRows.AnyAsync(r => r.ConclusionFrameworkId == id) ? "דיווחים" : null,
      _ => null
    };
    return context == null ? (true, null) : (false, $"לא ניתן למחוק — הערך בשימוש במערכת: {context}");
  }

  private async Task<string?> InstitutionInUseAsync(int id)
  {
    var inst = await _db.Institutions.FindAsync(id);
    if (inst == null) return null;
    var symbol = inst.InstitutionSymbol;
    var stageId = inst.EducationalStageId;
    if (await _db.Frameworks.AnyAsync(f => f.InstitutionSymbol == symbol && f.EducationalStageId == stageId))
      return "מסגרות";
    return null;
  }

  private async Task DeleteItemAsync(string tableName, int id)
  {
    switch (tableName)
    {
      case "districts":
        var d = await _db.Districts.FindAsync(id);
        if (d != null) _db.Districts.Remove(d);
        break;
      case "sectors":
        var s = await _db.Sectors.FindAsync(id);
        if (s != null) _db.Sectors.Remove(s);
        break;
      case "localities":
        var l = await _db.Localities.FindAsync(id);
        if (l != null) _db.Localities.Remove(l);
        break;
      case "authorities":
        var a = await _db.Authorities.FindAsync(id);
        if (a != null) _db.Authorities.Remove(a);
        break;
      case "projects":
        var p = await _db.Projects.FindAsync(id);
        if (p != null) _db.Projects.Remove(p);
        break;
      case "programs":
        var pr = await _db.Programs.FindAsync(id);
        if (pr != null) _db.Programs.Remove(pr);
        break;
      case "educationalprograms":
        var ep = await _db.EducationalPrograms.FindAsync(id);
        if (ep != null) _db.EducationalPrograms.Remove(ep);
        break;
      case "subjects":
        var sub = await _db.Subjects.FindAsync(id);
        if (sub != null) _db.Subjects.Remove(sub);
        break;
      case "domains":
        var dom = await _db.Domains.FindAsync(id);
        if (dom != null) _db.Domains.Remove(dom);
        break;
      case "classes":
        var c = await _db.Classes.FindAsync(id);
        if (c != null) _db.Classes.Remove(c);
        break;
      case "gradelevels":
        var g = await _db.GradeLevels.FindAsync(id);
        if (g != null) _db.GradeLevels.Remove(g);
        break;
      case "educationalstages":
        var es = await _db.EducationalStages.FindAsync(id);
        if (es != null) _db.EducationalStages.Remove(es);
        break;
      case "educationtypes":
        var et = await _db.EducationTypes.FindAsync(id);
        if (et != null) _db.EducationTypes.Remove(et);
        break;
      case "localitydistrictnational":
        var ldn = await _db.LocalityDistrictNationals.FindAsync(id);
        if (ldn != null) _db.LocalityDistrictNationals.Remove(ldn);
        break;
      case "discussioncodes":
        var dc = await _db.DiscussionCodes.FindAsync(id);
        if (dc != null) _db.DiscussionCodes.Remove(dc);
        break;
    }
    await _db.SaveChangesAsync();
  }
}

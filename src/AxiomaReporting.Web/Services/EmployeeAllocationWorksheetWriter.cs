using AxiomaReporting.Core.Entities;
using AxiomaReporting.Web.Helpers;
using ClosedXML.Excel;

namespace AxiomaReporting.Web.Services;

/// <summary>
/// Writes the shared long-format employee/allocation export used by both admin export entry points.
/// Every allocation selection receives its own row while employee and allocation details repeat for context.
/// </summary>
internal static class EmployeeAllocationWorksheetWriter
{
  private const int ExcelCellTextLimit = 32_767;

  public static void AddEmployeeExportWorksheets(
    XLWorkbook workbook,
    IReadOnlyCollection<User> employees,
    IReadOnlyCollection<Allocation> allocations,
    IReadOnlyDictionary<int, string> frameworkLabels)
  {
    AddEmployeeSummaryWorksheet(workbook, employees, allocations);
    AddAllocationDetailsWorksheet(workbook, employees, allocations, frameworkLabels);
    AddAllocationValuesWorksheet(workbook, employees, allocations, frameworkLabels);
  }

  public static void AddWorksheet(
    XLWorkbook workbook,
    string worksheetName,
    IReadOnlyCollection<User> employees,
    IReadOnlyCollection<Allocation> allocations,
    IReadOnlyDictionary<int, string> frameworkLabels,
    bool includeEmployeesWithoutAllocations)
  {
    var allocationsByEmployee = allocations
      .GroupBy(allocation => allocation.UserId)
      .ToDictionary(group => group.Key, group => group.ToList());

    var worksheet = workbook.Worksheets.Add(worksheetName);
    worksheet.RightToLeft = true;
    var headers = new[]
    {
      "מזהה עובד", "קוד עובד", "מספר זהות", "שם פרטי", "שם משפחה", "תפקיד במערכת",
      "תפקיד עובד", "סטטוס עובד", "עובד מדווח", "מייל", "טלפון", "יום מנוחה",
      "דיווח עתידי", "הערות עובד", "עובד נוצר בתאריך", "עובד עודכן בתאריך",
      "מזהה הקצאה", "הקצאה פעילה", "מזהה פרויקט", "פרויקט", "מזהה סוג דיווח", "סוג דיווח",
      "היקף פעילות חודשי", "היקף פעילות יומי", "היקף פעילות שנתי", "מכסת שורות חודשית",
      "מכסת שורות שנתית", "משך תפוקה", "אפשר העלאת אקסל", "הערות הקצאה",
      "הקצאה נוצרה בתאריך", "הקצאה עודכנה בתאריך", "סוג ערך בהקצאה", "מזהה ערך", "ערך בהקצאה"
    };
    for (var column = 0; column < headers.Length; column++)
      worksheet.Cell(1, column + 1).Value = headers[column];

    worksheet.Row(1).Style.Font.Bold = true;
    worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightBlue;

    var row = 2;
    foreach (var employee in employees)
    {
      if (!allocationsByEmployee.TryGetValue(employee.Id, out var employeeAllocations)
        || employeeAllocations.Count == 0)
      {
        if (includeEmployeesWithoutAllocations)
        {
          WriteRow(
            worksheet,
            row++,
            employee,
            allocation: null,
            new AllocationExportChoice("ללא הקצאה", null, string.Empty));
        }
        continue;
      }

      foreach (var allocation in employeeAllocations)
      {
        var choices = AllocationChoices(allocation, frameworkLabels);
        if (choices.Count == 0)
          choices.Add(new AllocationExportChoice("פרטי הקצאה בלבד", null, string.Empty));

        foreach (var choice in choices)
          WriteRow(worksheet, row++, employee, allocation, choice);
      }
    }

    if (row > 2)
    {
      worksheet.Range(2, 15, row - 1, 16).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
      worksheet.Range(2, 31, row - 1, 32).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
    }
    worksheet.SheetView.FreezeRows(1);
    worksheet.RangeUsed()?.SetAutoFilter();
    worksheet.Columns().AdjustToContents(1d, 60d);
  }

  private static void AddEmployeeSummaryWorksheet(
    XLWorkbook workbook,
    IReadOnlyCollection<User> employees,
    IReadOnlyCollection<Allocation> allocations)
  {
    var worksheet = AddWorksheetWithHeaders(workbook, "עובדים", new[]
    {
      "קוד עובד", "מספר זהות", "שם פרטי", "שם משפחה", "תפקיד", "סטטוס",
      "עובד מדווח", "מייל", "טלפון", "דיווח עתידי", "פרויקטים", "מחוזות",
      "תוכניות", "מגזרים", "הערות"
    });
    var allocationsByEmployee = AllocationsByEmployee(allocations);

    var row = 2;
    foreach (var employee in employees)
    {
      allocationsByEmployee.TryGetValue(employee.Id, out var employeeAllocations);
      employeeAllocations ??= new List<Allocation>();

      worksheet.Cell(row, 1).Value = employee.EmployeeCode;
      worksheet.Cell(row, 2).Value = employee.IdNumber;
      worksheet.Cell(row, 3).Value = employee.FirstName;
      worksheet.Cell(row, 4).Value = employee.LastName;
      worksheet.Cell(row, 5).Value = employee.UserRole?.DescriptionHebrew ?? employee.UserRole?.Name ?? string.Empty;
      worksheet.Cell(row, 6).Value = employee.Status?.DescriptionHebrew ?? employee.Status?.Name ?? string.Empty;
      worksheet.Cell(row, 7).Value = YesNo(employee.IsReportingEmployee);
      worksheet.Cell(row, 8).Value = employee.Email;
      worksheet.Cell(row, 9).Value = employee.Phone;
      worksheet.Cell(row, 10).Value = YesNo(employee.AllowFutureReporting);
      worksheet.Cell(row, 11).Value = JoinValues(employeeAllocations.Select(item => item.Project?.Description));
      worksheet.Cell(row, 12).Value = JoinValues(employeeAllocations.SelectMany(item => item.AllocationDistricts).Select(item => item.District?.Description));
      worksheet.Cell(row, 13).Value = JoinValues(employeeAllocations.SelectMany(item => item.AllocationPrograms).Select(item => item.Program?.Description));
      worksheet.Cell(row, 14).Value = JoinValues(employeeAllocations.SelectMany(item => item.AllocationSectors).Select(item => item.Sector?.Description));
      worksheet.Cell(row, 15).Value = employee.Notes;
      row++;
    }

    FinishWorksheet(worksheet);
  }

  private static void AddAllocationDetailsWorksheet(
    XLWorkbook workbook,
    IReadOnlyCollection<User> employees,
    IReadOnlyCollection<Allocation> allocations,
    IReadOnlyDictionary<int, string> frameworkLabels)
  {
    var worksheet = AddWorksheetWithHeaders(workbook, "פירוט הקצאות", new[]
    {
      "קוד עובד", "מספר זהות", "שם פרטי", "שם משפחה", "תפקיד במערכת", "תפקיד עובד",
      "סטטוס עובד", "עובד מדווח", "מייל", "טלפון", "יום מנוחה", "דיווח עתידי",
      "הערות עובד", "מזהה הקצאה", "מזהה פרויקט", "פרויקט", "מזהה סוג דיווח", "סוג דיווח",
      "היקף פעילות חודשי", "היקף פעילות יומי", "היקף פעילות שנתי", "מכסת שורות חודשית",
      "מכסת שורות שנתית", "משך תפוקה", "אפשר העלאת אקסל", "מחוזות", "תוכניות", "מגזרים",
      "יישובים", "מסגרות חינוכיות", "נושאים", "תחומים", "תוכניות חינוכיות", "כיתות",
      "שכבות", "קיום דיון", "יישוב/מחוז/ארצי", "הערות הקצאה", "נוצר בתאריך", "עודכן בתאריך"
    });
    var allocationsByEmployee = AllocationsByEmployee(allocations);

    var row = 2;
    foreach (var employee in employees)
    {
      if (!allocationsByEmployee.TryGetValue(employee.Id, out var employeeAllocations)) continue;
      foreach (var allocation in employeeAllocations)
      {
        worksheet.Cell(row, 1).Value = employee.EmployeeCode;
        worksheet.Cell(row, 2).Value = employee.IdNumber;
        worksheet.Cell(row, 3).Value = employee.FirstName;
        worksheet.Cell(row, 4).Value = employee.LastName;
        worksheet.Cell(row, 5).Value = employee.UserRole?.DescriptionHebrew ?? employee.UserRole?.Name ?? string.Empty;
        worksheet.Cell(row, 6).Value = employee.Role?.Description;
        worksheet.Cell(row, 7).Value = employee.Status?.DescriptionHebrew ?? employee.Status?.Name ?? string.Empty;
        worksheet.Cell(row, 8).Value = YesNo(employee.IsReportingEmployee);
        worksheet.Cell(row, 9).Value = employee.Email;
        worksheet.Cell(row, 10).Value = employee.Phone;
        worksheet.Cell(row, 11).Value = RestDayLabel(employee.RestDay);
        worksheet.Cell(row, 12).Value = YesNo(employee.AllowFutureReporting);
        worksheet.Cell(row, 13).Value = employee.Notes;
        worksheet.Cell(row, 14).Value = allocation.Id;
        worksheet.Cell(row, 15).Value = allocation.ProjectId;
        worksheet.Cell(row, 16).Value = allocation.Project?.Description;
        if (allocation.ReportTypeId.HasValue) worksheet.Cell(row, 17).Value = allocation.ReportTypeId.Value;
        worksheet.Cell(row, 18).Value = allocation.ReportType?.Description;
        SetDecimalCell(worksheet.Cell(row, 19), allocation.MonthlyEmploymentScope);
        SetDecimalCell(worksheet.Cell(row, 20), allocation.DailyEmploymentScope);
        SetDecimalCell(worksheet.Cell(row, 21), allocation.AnnualEmploymentScope);
        if (allocation.MonthlyRowAllocation.HasValue) worksheet.Cell(row, 22).Value = allocation.MonthlyRowAllocation.Value;
        if (allocation.AnnualRowAllocation.HasValue) worksheet.Cell(row, 23).Value = allocation.AnnualRowAllocation.Value;
        worksheet.Cell(row, 24).Value = allocation.OutputDuration;
        worksheet.Cell(row, 25).Value = YesNo(allocation.AllowExcelUpload);
        worksheet.Cell(row, 26).Value = JoinValues(allocation.AllocationDistricts.Select(item => item.District?.Description));
        worksheet.Cell(row, 27).Value = JoinValues(allocation.AllocationPrograms.Select(item => item.Program?.Description));
        worksheet.Cell(row, 28).Value = JoinValues(allocation.AllocationSectors.Select(item => item.Sector?.Description));
        worksheet.Cell(row, 29).Value = JoinValues(allocation.AllocationLocalities.Select(item => item.Locality?.Description));
        worksheet.Cell(row, 30).Value = JoinValues(allocation.AllocationFrameworks.Select(item =>
          frameworkLabels.TryGetValue(item.FrameworkId, out var label) ? label : item.Framework?.Description));
        worksheet.Cell(row, 31).Value = JoinValues(allocation.AllocationSubjects.Select(item => item.Subject?.Description));
        worksheet.Cell(row, 32).Value = JoinValues(allocation.AllocationDomains.Select(item => item.Domain?.Description));
        worksheet.Cell(row, 33).Value = JoinValues(allocation.AllocationEducationalPrograms.Select(item => item.EducationalProgram?.Description));
        worksheet.Cell(row, 34).Value = JoinValues(allocation.AllocationClasses.Select(item => item.SchoolClass?.Description));
        worksheet.Cell(row, 35).Value = JoinValues(allocation.AllocationGradeLevels.Select(item => item.GradeLevel?.Description));
        worksheet.Cell(row, 36).Value = JoinValues(allocation.AllocationDiscussionCodes.Select(item => item.DiscussionCode?.Description));
        worksheet.Cell(row, 37).Value = JoinValues(allocation.AllocationLocalityDistrictNationals.Select(item => item.LocalityDistrictNational?.Description));
        worksheet.Cell(row, 38).Value = allocation.Notes;
        worksheet.Cell(row, 39).Value = allocation.CreatedAt;
        if (allocation.UpdatedAt.HasValue) worksheet.Cell(row, 40).Value = allocation.UpdatedAt.Value;
        row++;
      }
    }

    if (row > 2) worksheet.Range(2, 39, row - 1, 40).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
    FinishWorksheet(worksheet);
  }

  private static void AddAllocationValuesWorksheet(
    XLWorkbook workbook,
    IReadOnlyCollection<User> employees,
    IReadOnlyCollection<Allocation> allocations,
    IReadOnlyDictionary<int, string> frameworkLabels)
  {
    var worksheet = AddWorksheetWithHeaders(workbook, "ערכי הקצאות", new[]
    {
      "קוד עובד", "מספר זהות", "שם פרטי", "שם משפחה",
      "מזהה הקצאה", "פרויקט", "סוג נתון", "ערך"
    });
    var allocationsByEmployee = AllocationsByEmployee(allocations);

    var row = 2;
    foreach (var employee in employees)
    {
      if (!allocationsByEmployee.TryGetValue(employee.Id, out var employeeAllocations)) continue;
      foreach (var allocation in employeeAllocations)
      {
        foreach (var choice in AllocationChoices(allocation, frameworkLabels))
        {
          worksheet.Cell(row, 1).Value = employee.EmployeeCode;
          worksheet.Cell(row, 2).Value = employee.IdNumber;
          worksheet.Cell(row, 3).Value = employee.FirstName;
          worksheet.Cell(row, 4).Value = employee.LastName;
          worksheet.Cell(row, 5).Value = allocation.Id;
          worksheet.Cell(row, 6).Value = allocation.Project?.Description;
          worksheet.Cell(row, 7).Value = choice.Type;
          worksheet.Cell(row, 8).Value = choice.Value;
          row++;
        }
      }
    }

    FinishWorksheet(worksheet);
  }

  private static Dictionary<int, List<Allocation>> AllocationsByEmployee(
    IReadOnlyCollection<Allocation> allocations) =>
    allocations.GroupBy(allocation => allocation.UserId)
      .ToDictionary(group => group.Key, group => group.ToList());

  private static IXLWorksheet AddWorksheetWithHeaders(
    XLWorkbook workbook,
    string worksheetName,
    IReadOnlyList<string> headers)
  {
    var worksheet = workbook.Worksheets.Add(worksheetName);
    worksheet.RightToLeft = true;
    for (var column = 0; column < headers.Count; column++)
      worksheet.Cell(1, column + 1).Value = headers[column];
    worksheet.Row(1).Style.Font.Bold = true;
    worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightBlue;
    return worksheet;
  }

  private static void FinishWorksheet(IXLWorksheet worksheet)
  {
    worksheet.SheetView.FreezeRows(1);
    worksheet.RangeUsed()?.SetAutoFilter();
    worksheet.Columns().AdjustToContents(1d, 60d);
  }

  private static string JoinValues(IEnumerable<string?> values)
  {
    var normalizedValues = values
      .Where(value => !string.IsNullOrWhiteSpace(value))
      .Select(value => value!.Trim())
      .Distinct(StringComparer.OrdinalIgnoreCase)
      .OrderBy(value => value, StringComparer.CurrentCulture)
      .ToList();

    var joined = string.Join(", ", normalizedValues);
    if (joined.Length <= ExcelCellTextLimit) return joined;

    // Excel cells cannot exceed 32,767 characters. The same allocation values
    // are exported losslessly in long format on the dedicated values sheet, so
    // use an explicit reference instead of throwing or silently truncating.
    return $"{normalizedValues.Count:N0} ערכים — הרשימה המלאה מופיעה בגיליון \"ערכי הקצאות\"";
  }

  private static void WriteRow(
    IXLWorksheet worksheet,
    int row,
    User employee,
    Allocation? allocation,
    AllocationExportChoice choice)
  {
    worksheet.Cell(row, 1).Value = employee.Id;
    worksheet.Cell(row, 2).Value = employee.EmployeeCode;
    worksheet.Cell(row, 3).Value = employee.IdNumber;
    worksheet.Cell(row, 4).Value = employee.FirstName;
    worksheet.Cell(row, 5).Value = employee.LastName;
    worksheet.Cell(row, 6).Value = employee.UserRole?.DescriptionHebrew ?? employee.UserRole?.Name ?? string.Empty;
    worksheet.Cell(row, 7).Value = employee.Role?.Description;
    worksheet.Cell(row, 8).Value = employee.Status?.DescriptionHebrew ?? employee.Status?.Name ?? string.Empty;
    worksheet.Cell(row, 9).Value = YesNo(employee.IsReportingEmployee);
    worksheet.Cell(row, 10).Value = employee.Email;
    worksheet.Cell(row, 11).Value = employee.Phone;
    worksheet.Cell(row, 12).Value = RestDayLabel(employee.RestDay);
    worksheet.Cell(row, 13).Value = YesNo(employee.AllowFutureReporting);
    worksheet.Cell(row, 14).Value = employee.Notes;
    worksheet.Cell(row, 15).Value = employee.CreatedAt;
    if (employee.UpdatedAt.HasValue) worksheet.Cell(row, 16).Value = employee.UpdatedAt.Value;

    if (allocation != null)
    {
      worksheet.Cell(row, 17).Value = allocation.Id;
      worksheet.Cell(row, 18).Value = YesNo(allocation.IsActive);
      worksheet.Cell(row, 19).Value = allocation.ProjectId;
      worksheet.Cell(row, 20).Value = allocation.Project?.Description;
      if (allocation.ReportTypeId.HasValue) worksheet.Cell(row, 21).Value = allocation.ReportTypeId.Value;
      worksheet.Cell(row, 22).Value = allocation.ReportType?.Description;
      SetDecimalCell(worksheet.Cell(row, 23), allocation.MonthlyEmploymentScope);
      SetDecimalCell(worksheet.Cell(row, 24), allocation.DailyEmploymentScope);
      SetDecimalCell(worksheet.Cell(row, 25), allocation.AnnualEmploymentScope);
      if (allocation.MonthlyRowAllocation.HasValue) worksheet.Cell(row, 26).Value = allocation.MonthlyRowAllocation.Value;
      if (allocation.AnnualRowAllocation.HasValue) worksheet.Cell(row, 27).Value = allocation.AnnualRowAllocation.Value;
      worksheet.Cell(row, 28).Value = allocation.OutputDuration;
      worksheet.Cell(row, 29).Value = YesNo(allocation.AllowExcelUpload);
      worksheet.Cell(row, 30).Value = allocation.Notes;
      worksheet.Cell(row, 31).Value = allocation.CreatedAt;
      if (allocation.UpdatedAt.HasValue) worksheet.Cell(row, 32).Value = allocation.UpdatedAt.Value;
    }

    worksheet.Cell(row, 33).Value = choice.Type;
    if (choice.Id.HasValue) worksheet.Cell(row, 34).Value = choice.Id.Value;
    worksheet.Cell(row, 35).Value = choice.Value;
  }

  private static List<AllocationExportChoice> AllocationChoices(
    Allocation allocation,
    IReadOnlyDictionary<int, string> frameworkLabels)
  {
    var choices = new List<AllocationExportChoice>();
    AddChoices(choices, "מחוז", allocation.AllocationDistricts,
      item => item.DistrictId, item => item.District?.Description);
    AddChoices(choices, "תוכנית", allocation.AllocationPrograms,
      item => item.ProgramId, item => item.Program?.Description);
    AddChoices(choices, "מגזר", allocation.AllocationSectors,
      item => item.SectorId, item => item.Sector?.Description);
    AddChoices(choices, "יישוב", allocation.AllocationLocalities,
      item => item.LocalityId, item => item.Locality?.Description);
    AddChoices(choices, "מסגרת חינוכית", allocation.AllocationFrameworks,
      item => item.FrameworkId,
      item => frameworkLabels.TryGetValue(item.FrameworkId, out var label) ? label : item.Framework?.Description);
    AddChoices(choices, "נושא", allocation.AllocationSubjects,
      item => item.SubjectId, item => item.Subject?.Description);
    AddChoices(choices, "תחום", allocation.AllocationDomains,
      item => item.DomainId, item => item.Domain?.Description);
    AddChoices(choices, "תוכנית חינוכית", allocation.AllocationEducationalPrograms,
      item => item.EducationalProgramId, item => item.EducationalProgram?.Description);
    AddChoices(choices, "כיתה", allocation.AllocationClasses,
      item => item.ClassId, item => item.SchoolClass?.Description);
    AddChoices(choices, "שכבה", allocation.AllocationGradeLevels,
      item => item.GradeLevelId, item => item.GradeLevel?.Description);
    AddChoices(choices, "קיום דיון", allocation.AllocationDiscussionCodes,
      item => item.DiscussionCodeId, item => item.DiscussionCode?.Description);
    AddChoices(choices, "יישוב/מחוז/ארצי", allocation.AllocationLocalityDistrictNationals,
      item => item.LocalityDistrictNationalId, item => item.LocalityDistrictNational?.Description);
    return choices;
  }

  private static void AddChoices<T>(
    ICollection<AllocationExportChoice> destination,
    string type,
    IEnumerable<T> source,
    Func<T, int> id,
    Func<T, string?> value)
  {
    foreach (var choice in source
      .Select(item => new AllocationExportChoice(type, id(item), value(item)?.Trim() ?? string.Empty))
      .Where(item => !string.IsNullOrWhiteSpace(item.Value))
      .GroupBy(item => (item.Id, item.Value), item => item)
      .Select(group => group.First())
      .OrderBy(item => item.Value, StringComparer.CurrentCulture))
    {
      destination.Add(choice);
    }
  }

  private static void SetDecimalCell(IXLCell cell, decimal? value)
  {
    if (value.HasValue) cell.Value = (double)value.Value;
  }

  private static string YesNo(bool value) => value ? "כן" : "לא";

  private static string RestDayLabel(int? restDay) =>
    SelectListProviders.RestDayOptions
      .FirstOrDefault(option => option.Value == restDay?.ToString())?.Text ?? string.Empty;

  private sealed record AllocationExportChoice(string Type, int? Id, string Value);
}

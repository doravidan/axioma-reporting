using System.Data;
using System.Globalization;
using System.Text;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using static Util;

Console.OutputEncoding = Encoding.UTF8;

const string DefaultConnection = "Server=.\\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

if (args.Length < 2)
{
    Console.Error.WriteLine("Usage: FwDataImport dry-run|apply <excel-folder> [connection-string]");
    Console.Error.WriteLine("       FwDataImport backup <backup-path> [connection-string]");
    return 2;
}

var mode = args[0].Trim().ToLowerInvariant();
var connectionString = args.Length >= 3 ? args[2] : DefaultConnection;
if (mode is not ("dry-run" or "apply" or "backup"))
{
    Console.Error.WriteLine("Mode must be dry-run, apply, or backup.");
    return 2;
}

using var connection = new SqlConnection(connectionString);
connection.Open();

if (mode == "backup")
{
    BackupDatabase(connection, args[1]);
    return 0;
}

var folder = args[1];
if (!Directory.Exists(folder))
{
    Console.Error.WriteLine($"Excel folder does not exist: {folder}");
    return 2;
}

var existingLocalities = ReadSingleColumn(connection, null, "SELECT Description FROM Localities");
var preservedIdNumbers = ReadSingleColumn(connection, null, "SELECT IdNumber FROM Users WHERE UserRoleId <> 6");
var userRoleByHebrew = ReadIdLookup(connection, null, "SELECT Id, DescriptionHebrew FROM UserRoles");
var userStatusByHebrew = ReadIdLookup(connection, null, "SELECT Id, DescriptionHebrew FROM UserStatuses");
var reportTypeByDescription = ReadIdLookup(connection, null, "SELECT Id, Description FROM ReportTypes");

var data = WorkbookLoader.Load(folder, existingLocalities);
data.FinalizeDerivedData();
var validation = data.Validate(preservedIdNumbers);

PrintSummary(data, validation);

if (validation.FatalErrors.Count > 0)
{
    Console.Error.WriteLine("Fatal validation errors found. Apply is blocked.");
    return 1;
}

if (mode == "dry-run")
{
    Console.WriteLine("Dry run completed. No database changes were made.");
    return 0;
}

using var tx = connection.BeginTransaction(IsolationLevel.Serializable);
try
{
    var importer = new DatabaseImporter(connection, tx, userRoleByHebrew, userStatusByHebrew, reportTypeByDescription);
    importer.ReplaceAll(data);
    tx.Commit();
    Console.WriteLine("Apply completed successfully.");
    return 0;
}
catch (Exception ex)
{
    tx.Rollback();
    Console.Error.WriteLine("Apply failed. Transaction rolled back.");
    Console.Error.WriteLine(ex);
    return 1;
}

static void PrintSummary(ImportData data, ValidationResult validation)
{
    Console.WriteLine("SUMMARY");
    Console.WriteLine($"Files: {data.FileCount}");
    Console.WriteLine($"Employees: {data.Employees.Count}");
    Console.WriteLine($"Allocations: {data.Allocations.Count}");
    Console.WriteLine($"Projects: {data.Projects.Count}");
    Console.WriteLine($"Programs: {data.Programs.Count}");
    Console.WriteLine($"ProjectPrograms: {data.ProjectProgramKeys.Count}");
    Console.WriteLine($"Districts: {data.Districts.Count}");
    Console.WriteLine($"Sectors: {data.Sectors.Count}");
    Console.WriteLine($"Localities: {data.Localities.Count}");
    Console.WriteLine($"Institutions: {data.Institutions.Count}");
    Console.WriteLine($"Frameworks: {data.Frameworks.Count}");
    Console.WriteLine($"EducationalPrograms: {data.EducationalPrograms.Count}");
    Console.WriteLine($"Domains: {data.Domains.Count}");
    Console.WriteLine($"Subjects: {data.Subjects.Count}");
    Console.WriteLine($"DiscussionCodes: {data.DiscussionCodes.Count}");
    Console.WriteLine($"SchoolClasses: {data.SchoolClasses.Count}");
    Console.WriteLine($"GradeLevels: {data.GradeLevels.Count}");
    Console.WriteLine($"LocalityDistrictNationals: {data.LocalityDistrictNationals.Count}");
    Console.WriteLine($"EducationTypes: {data.EducationTypes.Count}");
    Console.WriteLine($"EducationalStages: {data.EducationalStages.Count}");
    Console.WriteLine($"Framework assignment rows: {data.FrameworkAssignments.Count}");
    Console.WriteLine($"Warnings: {validation.Warnings.Count}");
    Console.WriteLine($"Fatal errors: {validation.FatalErrors.Count}");

    foreach (var warning in validation.Warnings.Take(80))
    {
        Console.WriteLine("WARNING\t" + warning);
    }
    if (validation.Warnings.Count > 80)
    {
        Console.WriteLine($"WARNING\t... {validation.Warnings.Count - 80} more warnings omitted");
    }
    foreach (var error in validation.FatalErrors)
    {
        Console.WriteLine("ERROR\t" + error);
    }
}

static void BackupDatabase(SqlConnection connection, string backupPath)
{
    var directory = Path.GetDirectoryName(Path.GetFullPath(backupPath));
    if (!string.IsNullOrWhiteSpace(directory))
    {
        Directory.CreateDirectory(directory);
    }

    var escapedPath = backupPath.Replace("'", "''", StringComparison.Ordinal);
    try
    {
        RunBackupCommand(connection, escapedPath, "WITH INIT, COMPRESSION, STATS=10");
    }
    catch (SqlException ex) when (ex.Message.Contains("COMPRESSION", StringComparison.OrdinalIgnoreCase))
    {
        RunBackupCommand(connection, escapedPath, "WITH INIT, STATS=10");
    }

    Console.WriteLine($"Backup completed: {backupPath}");
}

static void RunBackupCommand(SqlConnection connection, string escapedPath, string options)
{
    using var cmd = new SqlCommand($"BACKUP DATABASE [AxiomaReporting] TO DISK = N'{escapedPath}' {options};", connection);
    cmd.CommandTimeout = 900;
    cmd.ExecuteNonQuery();
}

static class Util
{
    public static HashSet<string> ReadSingleColumn(SqlConnection connection, SqlTransaction? tx, string sql)
    {
        using var cmd = new SqlCommand(sql, connection, tx);
        cmd.CommandTimeout = 180;
        using var reader = cmd.ExecuteReader();
        var values = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        while (reader.Read())
        {
            values.Add(Clean(reader.GetString(0)));
        }
        return values;
    }

    public static Dictionary<string, int> ReadIdLookup(SqlConnection connection, SqlTransaction? tx, string sql)
    {
        using var cmd = new SqlCommand(sql, connection, tx);
        cmd.CommandTimeout = 180;
        using var reader = cmd.ExecuteReader();
        var values = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        while (reader.Read())
        {
            if (!reader.IsDBNull(1))
            {
                values[Clean(reader.GetString(1))] = reader.GetInt32(0);
            }
        }
        return values;
    }

    public static string Clean(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }
        var chars = value.Where(ch => ch != '\u200e' && ch != '\u200f' && !char.IsControl(ch)).ToArray();
        return string.Join(" ", new string(chars).Trim().Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
    }

    public static string Key(string? value) => Clean(value).ToUpperInvariant();

    public static string LooseKey(string? value)
    {
        var cleaned = Key(value);
        return new string(cleaned.Where(char.IsLetterOrDigit).ToArray());
    }

    public static decimal? DecimalOrNull(string value)
    {
        value = Clean(value).Replace(",", "");
        if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
        {
            return result;
        }
        if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("he-IL"), out result))
        {
            return result;
        }
        return null;
    }

    public static int? IntOrNull(string value)
    {
        value = Clean(value);
        if (int.TryParse(new string(value.Where(char.IsDigit).ToArray()), out var result))
        {
            return result;
        }
        return null;
    }

    public static bool IsYes(string value)
    {
        value = Key(value);
        return value is "כן" or "YES" or "TRUE" or "1";
    }

    public static int? RestDay(string value)
    {
        value = Key(value);
        if (value.Contains("ראשון")) return 0;
        if (value.Contains("שישי")) return 5;
        if (value.Contains("שבת")) return 6;
        return null;
    }

    public static decimal? DailyScope(string value)
    {
        value = Clean(value);
        if (value.Length == 0 || value.Contains("ללא", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        if (value.Contains('9'))
        {
            return 9m;
        }
        return DecimalOrNull(value);
    }

    public static string CellText(IXLCell cell)
    {
        if (cell.IsEmpty())
        {
            return string.Empty;
        }
        if (cell.DataType == XLDataType.DateTime && cell.TryGetValue<DateTime>(out var date))
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        return Clean(cell.GetFormattedString());
    }
}

sealed class WorkbookLoader
{
    public static ImportData Load(string folder, HashSet<string> existingLocalities)
    {
        var data = new ImportData();
        var files = Directory.EnumerateFiles(folder, "*.xlsx", SearchOption.TopDirectoryOnly)
            .OrderBy(Path.GetFileName, StringComparer.CurrentCulture)
            .ToList();
        data.FileCount = files.Count;

        foreach (var file in files)
        {
            using var workbook = new XLWorkbook(file);
            var fileName = Path.GetFileName(file);
            var fileCode = new CodeSet();
            var fileInstitutionFrameworkKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var fileProjectPrograms = new HashSet<ProjectProgramKey>();

            foreach (var ws in workbook.Worksheets)
            {
                var range = ws.RangeUsed();
                if (range == null) continue;
                var headerRow = DetectHeaderRow(ws, range);
                var headers = HeaderMap.FromWorksheet(ws, headerRow, range.FirstColumn().ColumnNumber(), range.LastColumn().ColumnNumber());
                if (headers.Count == 0) continue;

                if (IsEmployeeSheet(ws.Name, headers))
                {
                    ReadEmployees(data, ws, headerRow, headers, fileName);
                }
                if (IsAllocationSheet(ws.Name, headers))
                {
                    foreach (var allocation in ReadAllocations(ws, headerRow, headers, fileName))
                    {
                        data.Allocations.Add(allocation);
                        data.AddLookup(data.Projects, allocation.Project);
                        data.AddLookup(data.Programs, allocation.Program);
                        data.AddLookup(data.Districts, allocation.District);
                        data.AddLookup(data.Sectors, allocation.Sector);
                        if (!string.IsNullOrWhiteSpace(allocation.Project) && !string.IsNullOrWhiteSpace(allocation.Program))
                        {
                            fileProjectPrograms.Add(new ProjectProgramKey(allocation.Project, allocation.Program));
                            data.ProjectProgramKeys.Add(new ProjectProgramKey(allocation.Project, allocation.Program));
                        }
                    }
                }
                if (IsFrameworkAssignmentSheet(headers))
                {
                    foreach (var assignment in ReadFrameworkAssignments(data, ws, headerRow, headers, fileName))
                    {
                        data.FrameworkAssignments.Add(assignment);
                        fileInstitutionFrameworkKeys.Add(assignment.FrameworkKey);
                    }
                }
                else if (IsInstitutionSheet(headers))
                {
                    foreach (var frameworkKey in ReadInstitutions(data, ws, headerRow, headers, fileName, existingLocalities))
                    {
                        fileInstitutionFrameworkKeys.Add(frameworkKey);
                    }
                }
                if (IsCodeSheet(ws.Name, headers))
                {
                    ReadCodeRows(data, fileCode, ws, headerRow, headers);
                }
            }

            fileCode.FrameworkKeys.UnionWith(fileInstitutionFrameworkKeys);
            foreach (var projectProgram in fileProjectPrograms)
            {
                data.MergeProjectProgramScope(projectProgram, fileCode);
                data.FileFrameworksByProjectProgram[projectProgram] = new HashSet<string>(fileInstitutionFrameworkKeys, StringComparer.OrdinalIgnoreCase);
            }
        }

        return data;
    }

    private static int DetectHeaderRow(IXLWorksheet ws, IXLRange range)
    {
        var firstRow = range.FirstRow().RowNumber();
        var lastRow = Math.Min(range.LastRow().RowNumber(), firstRow + 20);
        var firstCol = range.FirstColumn().ColumnNumber();
        var lastCol = range.LastColumn().ColumnNumber();
        var bestRow = firstRow;
        var bestScore = -1;
        for (var row = firstRow; row <= lastRow; row++)
        {
            var values = Enumerable.Range(firstCol, lastCol - firstCol + 1).Select(col => CellText(ws.Cell(row, col))).ToList();
            var nonEmpty = values.Count(v => !string.IsNullOrWhiteSpace(v));
            var keyHeaders = values.Count(IsKnownHeader);
            var score = nonEmpty + keyHeaders * 8;
            if (score > bestScore)
            {
                bestScore = score;
                bestRow = row;
            }
        }
        return bestRow;
    }

    private static bool IsKnownHeader(string value)
    {
        var key = Key(value);
        return KnownHeaders.Contains(key);
    }

    private static readonly HashSet<string> KnownHeaders = new(new[]
    {
        "מס",
        "מס\"ד",
        "ת.ז",
        "קוד עובד",
        "שם פרטי",
        "שם משפחה",
        "סמל מוסד",
        "שם מוסד",
        "שם הישוב",
        "יישוב",
        "מחוז",
        "מגזר",
        "תוכנית",
        "שם התוכנית",
        "תוכנית חינוכית",
        "תחום",
        "נושא 1",
        "נושא 2",
        "קיום דיון",
        "מסקנות כיתה",
        "מסקנות מסגרת חינוכית",
        "מסגרת חינוכית",
        "יישוב/מחוז/ארצי",
        "שכבה",
        "כיתה",
        "סוג חינוך",
        "שלב חינוך",
        "שיבוץ עובד -שם התוכנית במערכת"
    }.Select(Key), StringComparer.OrdinalIgnoreCase);

    private static bool IsEmployeeSheet(string sheetName, HeaderMap headers)
    {
        return sheetName.Contains("מאגר עובדים", StringComparison.OrdinalIgnoreCase)
            && headers.Has("ת.ז")
            && headers.Has("קוד עובד")
            && headers.Has("שם פרטי")
            && headers.Has("שם משפחה");
    }

    private static bool IsAllocationSheet(string sheetName, HeaderMap headers)
    {
        return sheetName.Contains("הקצאות", StringComparison.OrdinalIgnoreCase)
            && headers.Has("ת.ז")
            && headers.Has("פרוייקט")
            && headers.Has("תוכנית");
    }

    private static bool IsFrameworkAssignmentSheet(HeaderMap headers)
    {
        return headers.Has("שיבוץ עובד -שם התוכנית במערכת")
            && headers.Has("ת.ז")
            && headers.Has("סמל מוסד");
    }

    private static bool IsInstitutionSheet(HeaderMap headers)
    {
        return headers.Has("סמל מוסד")
            && !IsFrameworkAssignmentSheet(headers)
            && (headers.Has("שם מוסד") || headers.Has("מסגרת חינוכית"));
    }

    private static bool IsCodeSheet(string sheetName, HeaderMap headers)
    {
        if (sheetName.Contains("דיון", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return headers.Has("נושא 1")
            || headers.Has("תוכנית חינוכית")
            || headers.Has("תחום")
            || headers.Has("קיום דיון")
            || headers.Has("מסקנות כיתה")
            || headers.Has("מסקנות מסגרת חינוכית")
            || headers.Has("יישוב/מחוז/ארצי")
            || headers.Has("שכבה")
            || headers.Has("כיתה");
    }

    private static void ReadEmployees(ImportData data, IXLWorksheet ws, int headerRow, HeaderMap headers, string fileName)
    {
        for (var row = headerRow + 1; row <= (ws.LastRowUsed()?.RowNumber() ?? headerRow); row++)
        {
            var idNumber = headers.Get(ws, row, "ת.ז");
            if (string.IsNullOrWhiteSpace(idNumber)) continue;
            var employee = new EmployeeRecord
            {
                IdNumber = idNumber,
                EmployeeCode = headers.Get(ws, row, "קוד עובד"),
                FirstName = headers.Get(ws, row, "שם פרטי"),
                LastName = headers.Get(ws, row, "שם משפחה"),
                RestDay = RestDay(headers.Get(ws, row, "יום מנוחה")),
                Phone = headers.Get(ws, row, "טלפון"),
                Email = headers.Get(ws, row, "מייל"),
                IsReportingEmployee = IsYes(headers.Get(ws, row, "עובד מדווח כן / לא")),
                RoleDescription = headers.Get(ws, row, "תפקיד"),
                AllowFutureReporting = IsYes(headers.Get(ws, row, "אפשר דיווח עתידי כן / לא")),
                UserRoleDescription = headers.Get(ws, row, "הרשאות"),
                StatusDescription = headers.Get(ws, row, "סטטוס"),
                SourceFile = fileName,
                SourceRow = row
            };
            data.AddEmployee(employee);
            data.AddLookup(data.EmployeeRoles, employee.RoleDescription);
        }
    }

    private static IEnumerable<AllocationRecord> ReadAllocations(IXLWorksheet ws, int headerRow, HeaderMap headers, string fileName)
    {
        for (var row = headerRow + 1; row <= (ws.LastRowUsed()?.RowNumber() ?? headerRow); row++)
        {
            var idNumber = headers.Get(ws, row, "ת.ז");
            var project = headers.Get(ws, row, "פרוייקט");
            var program = headers.Get(ws, row, "תוכנית");
            if (string.IsNullOrWhiteSpace(idNumber) && string.IsNullOrWhiteSpace(project) && string.IsNullOrWhiteSpace(program)) continue;
            yield return new AllocationRecord
            {
                IdNumber = idNumber,
                EmployeeCode = headers.Get(ws, row, "קוד עובד"),
                Project = project,
                Program = program,
                District = headers.Get(ws, row, "מחוז"),
                Sector = headers.Get(ws, row, "מגזר"),
                MonthlyEmploymentScope = DecimalOrNull(headers.Get(ws, row, "היקף שעות העסקה חודשי")),
                AnnualEmploymentScope = DecimalOrNull(headers.Get(ws, row, "היקף שעות העסקה שנתי")),
                DailyEmploymentScope = DailyScope(headers.Get(ws, row, "היקף העסקה יומי עד  9 שעות/ ללא הגבלה", "היקף העסקה יומי עד 9 שעות/ ללא הגבלה")),
                OutputDuration = headers.Get(ws, row, "משך תפוקה"),
                ReportType = headers.Get(ws, row, "סיווג דיווח"),
                SourceFile = fileName,
                SourceRow = row
            };
        }
    }

    private static IEnumerable<FrameworkAssignment> ReadFrameworkAssignments(ImportData data, IXLWorksheet ws, int headerRow, HeaderMap headers, string fileName)
    {
        for (var row = headerRow + 1; row <= (ws.LastRowUsed()?.RowNumber() ?? headerRow); row++)
        {
            var symbol = headers.Get(ws, row, "סמל מוסד");
            if (string.IsNullOrWhiteSpace(symbol)) continue;
            var locality = headers.Get(ws, row, "שם הישוב");
            var name = headers.Get(ws, row, "מסגרת חינוכית", "שם מוסד");
            var district = headers.Get(ws, row, "מחוז");
            var sector = headers.Get(ws, row, "מגזר");
            var program = headers.Get(ws, row, "שיבוץ עובד -שם התוכנית במערכת");
            var frameworkKey = data.AddInstitutionFramework(symbol, name, locality, district, sector, null, null, fileName, row);
            yield return new FrameworkAssignment
            {
                IdNumber = headers.Get(ws, row, "ת.ז"),
                EmployeeCode = headers.Get(ws, row, "קוד עובד"),
                Program = program,
                FrameworkKey = frameworkKey,
                SourceFile = fileName,
                SourceRow = row
            };
        }
    }

    private static IEnumerable<string> ReadInstitutions(ImportData data, IXLWorksheet ws, int headerRow, HeaderMap headers, string fileName, HashSet<string> existingLocalities)
    {
        var symbolColumns = headers.Columns("סמל מוסד");
        if (symbolColumns.Count == 0) yield break;
        var symbolCol = symbolColumns[0];
        for (var row = headerRow + 1; row <= (ws.LastRowUsed()?.RowNumber() ?? headerRow); row++)
        {
            var symbol = CellText(ws.Cell(row, symbolCol));
            if (string.IsNullOrWhiteSpace(symbol)) continue;
            var district = headers.Get(ws, row, "מחוז");
            var sector = headers.Get(ws, row, "מגזר");
            var type = headers.Get(ws, row, "סוג חינוך");
            var stage = headers.Get(ws, row, "שלב חינוך");
            var (name, locality) = ResolveInstitutionNameAndLocality(ws, row, headers, symbolCol, existingLocalities);
            if (!LooksLikeInstitutionSymbol(symbol) && LooksLikeInstitutionSymbol(name))
            {
                var actualSymbol = name;
                var actualLocality = symbol;
                var actualName = locality;
                symbol = actualSymbol;
                locality = actualLocality;
                name = actualName;
            }
            var frameworkKey = data.AddInstitutionFramework(symbol, name, locality, district, sector, type, stage, fileName, row);
            yield return frameworkKey;
        }
    }

    private static bool LooksLikeInstitutionSymbol(string value)
    {
        value = Clean(value);
        var digitCount = value.Count(char.IsDigit);
        if (digitCount < 3)
        {
            return false;
        }
        var significantCount = value.Count(ch => char.IsLetterOrDigit(ch));
        return significantCount == 0 || digitCount >= significantCount - 1;
    }

    private static (string Name, string Locality) ResolveInstitutionNameAndLocality(IXLWorksheet ws, int row, HeaderMap headers, int symbolCol, HashSet<string> existingLocalities)
    {
        var name = headers.Get(ws, row, "שם מוסד", "מסגרת חינוכית");
        var locality = headers.Get(ws, row, "שם הישוב", "יישוב");

        if (symbolCol > 1)
        {
            var before = CellText(ws.Cell(row, symbolCol - 1));
            var after = CellText(ws.Cell(row, symbolCol + 1));
            if (!string.IsNullOrWhiteSpace(before) && !string.IsNullOrWhiteSpace(after))
            {
                var beforeLooksLocality = existingLocalities.Contains(Clean(before)) || LooksLikeLocality(before, after);
                var afterLooksInstitution = LooksLikeInstitution(after);
                if (beforeLooksLocality && afterLooksInstitution)
                {
                    locality = before;
                    name = after;
                }
            }
        }

        return (name, locality);
    }

    private static bool LooksLikeLocality(string first, string second)
    {
        var secondKey = Key(second);
        return secondKey.Contains("מקיף") || secondKey.Contains("תיכון") || secondKey.Contains("אורט") || secondKey.Contains("ישיבה") || secondKey.Contains("בית") || secondKey.Contains("אל");
    }

    private static bool LooksLikeInstitution(string value)
    {
        var key = Key(value);
        return key.Contains("מקיף") || key.Contains("תיכון") || key.Contains("אורט") || key.Contains("ישיבה") || key.Contains("בית") || key.Contains("אל") || key.Contains("חט");
    }

    private static void ReadCodeRows(ImportData data, CodeSet code, IXLWorksheet ws, int headerRow, HeaderMap headers)
    {
        for (var row = headerRow + 1; row <= (ws.LastRowUsed()?.RowNumber() ?? headerRow); row++)
        {
            var educationalPrograms = new[] { headers.Get(ws, row, "תוכנית חינוכית"), headers.Get(ws, row, "תוכנית") };
            foreach (var value in educationalPrograms) data.AddLookup(data.EducationalPrograms, value, code.EducationalPrograms);
            data.AddLookup(data.Domains, headers.Get(ws, row, "תחום"), code.Domains);
            data.AddLookup(data.Subjects, headers.Get(ws, row, "נושא 1"), code.Subjects);
            data.AddLookup(data.Subjects, headers.Get(ws, row, "נושא 2"), code.Subjects);
            data.AddLookup(data.DiscussionCodes, GetDiscussionCode(ws, row, headers), code.DiscussionCodes);
            data.AddLookup(data.LocalityDistrictNationals, headers.Get(ws, row, "יישוב/מחוז/ארצי"), code.LocalityDistrictNationals);
            data.AddLookup(data.GradeLevels, headers.Get(ws, row, "שכבה"), code.GradeLevels);
            data.AddLookup(data.Districts, headers.Get(ws, row, "מחוז"));
            data.AddLookup(data.Localities, headers.Get(ws, row, "יישוב"));

            var classValues = headers.GetAll(ws, row, "כיתה");
            foreach (var value in classValues) data.AddLookup(data.SchoolClasses, value, code.SchoolClasses);
            data.AddLookup(data.SchoolClasses, headers.Get(ws, row, "מסקנות כיתה"), code.SchoolClasses);

            foreach (var value in headers.GetAll(ws, row, "מסגרת חינוכית").Concat(new[] { headers.Get(ws, row, "מסקנות מסגרת חינוכית") }))
            {
                if (string.IsNullOrWhiteSpace(value)) continue;
                var frameworkKey = data.AddTextFramework(value);
                code.FrameworkKeys.Add(frameworkKey);
            }
        }
    }

    private static string GetDiscussionCode(IXLWorksheet ws, int row, HeaderMap headers)
    {
        var value = headers.Get(ws, row, "קיום דיון", "קודי דיון");
        if (!string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        var subject2Column = headers.Columns("נושא 2").FirstOrDefault();
        if (subject2Column > 0)
        {
            return CellText(ws.Cell(row, subject2Column + 1));
        }

        var subject1Column = headers.Columns("נושא 1").FirstOrDefault();
        if (subject1Column > 0)
        {
            return CellText(ws.Cell(row, subject1Column + 2));
        }

        if (ws.Name.Contains("דיון", StringComparison.OrdinalIgnoreCase))
        {
            var range = ws.RangeUsed();
            if (range != null)
            {
                return CellText(ws.Cell(row, range.FirstColumn().ColumnNumber()));
            }
        }

        return string.Empty;
    }
}

sealed class DatabaseImporter
{
    private readonly SqlConnection _connection;
    private readonly SqlTransaction _tx;
    private readonly Dictionary<string, int> _userRoleByHebrew;
    private readonly Dictionary<string, int> _userStatusByHebrew;
    private readonly Dictionary<string, int> _reportTypeByDescription;
    private readonly Dictionary<string, int> _employeeRoleIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _projectIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _programIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _districtIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _sectorIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _localityIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _educationTypeIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _educationalStageIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _educationalProgramIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _domainIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _subjectIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _discussionCodeIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _schoolClassIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _gradeLevelIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _ldnIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _frameworkIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _userIdsByIdNumber = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _allocationIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, PairBulkBuffer> _pairBuffers = new(StringComparer.OrdinalIgnoreCase);

    public DatabaseImporter(SqlConnection connection, SqlTransaction tx, Dictionary<string, int> userRoleByHebrew, Dictionary<string, int> userStatusByHebrew, Dictionary<string, int> reportTypeByDescription)
    {
        _connection = connection;
        _tx = tx;
        _userRoleByHebrew = userRoleByHebrew;
        _userStatusByHebrew = userStatusByHebrew;
        _reportTypeByDescription = reportTypeByDescription;
    }

    public void ReplaceAll(ImportData data)
    {
        DeleteExistingData();
        ImportLookup(data.EmployeeRoles, "EmployeeRoles", _employeeRoleIds);
        ImportLookup(data.Projects, "Projects", _projectIds);
        ImportLookup(data.Programs, "Programs", _programIds);
        ImportLookup(data.Districts, "Districts", _districtIds);
        ImportLookup(data.Sectors, "Sectors", _sectorIds);
        ImportLookup(data.Localities, "Localities", _localityIds, extraColumn: "NationalCode");
        ImportLookup(data.EducationTypes, "EducationTypes", _educationTypeIds);
        ImportLookup(data.EducationalStages, "EducationalStages", _educationalStageIds);
        ImportLookup(data.EducationalPrograms, "EducationalPrograms", _educationalProgramIds);
        ImportLookup(data.Domains, "Domains", _domainIds);
        ImportLookup(data.Subjects, "Subjects", _subjectIds);
        ImportLookup(data.DiscussionCodes, "DiscussionCodes", _discussionCodeIds);
        ImportLookup(data.SchoolClasses, "SchoolClasses", _schoolClassIds);
        ImportLookup(data.GradeLevels, "GradeLevels", _gradeLevelIds);
        ImportLookup(data.LocalityDistrictNationals, "LocalityDistrictNationals", _ldnIds);
        ImportInstitutions(data);
        ImportFrameworks(data);
        ImportProjectPrograms(data);
        ImportProjectProgramScopes(data);
        ImportUsers(data);
        ImportAllocations(data);
    }

    private void DeleteExistingData()
    {
        Exec("DELETE FROM AuditLogs");
        Exec("DELETE FROM NotificationLogs");
        Exec("DELETE FROM ReminderLogs");
        Exec("DELETE FROM PasswordResetTokens");
        Exec("DELETE FROM TwoFactorCodes");
        Exec("DELETE FROM PasswordHistories");
        Exec("DELETE FROM TermsOfUseAcceptances");
        Exec("DELETE FROM DocumentAttachments");
        Exec("DELETE FROM ReportRows");
        Exec("DELETE FROM Reports");
        Exec("DELETE FROM AllocationClasses");
        Exec("DELETE FROM AllocationDiscussionCodes");
        Exec("DELETE FROM AllocationDistricts");
        Exec("DELETE FROM AllocationDomains");
        Exec("DELETE FROM AllocationEducationalPrograms");
        Exec("DELETE FROM AllocationFrameworks");
        Exec("DELETE FROM AllocationGradeLevels");
        Exec("DELETE FROM AllocationLocalities");
        Exec("DELETE FROM AllocationLocalityDistrictNationals");
        Exec("DELETE FROM AllocationPrograms");
        Exec("DELETE FROM AllocationSectors");
        Exec("DELETE FROM AllocationSubjects");
        Exec("DELETE FROM Allocations");
        Exec("DELETE FROM InspectorAssignments");
        Exec("DELETE FROM ProjectProgramClasses");
        Exec("DELETE FROM ProjectProgramDiscussionCodes");
        Exec("DELETE FROM ProjectProgramDomains");
        Exec("DELETE FROM ProjectProgramEducationalPrograms");
        Exec("DELETE FROM ProjectProgramFrameworks");
        Exec("DELETE FROM ProjectProgramGradeLevels");
        Exec("DELETE FROM ProjectProgramSubjects");
        Exec("DELETE FROM ProjectPrograms");
        Exec("UPDATE Users SET CreatedBy = NULL WHERE CreatedBy IN (SELECT Id FROM Users WHERE UserRoleId = 6)");
        Exec("UPDATE Users SET UpdatedBy = NULL WHERE UpdatedBy IN (SELECT Id FROM Users WHERE UserRoleId = 6)");
        Exec("DELETE FROM Users WHERE UserRoleId = 6");
        Exec("DELETE FROM Frameworks");
        Exec("DELETE FROM Institutions");
        Exec("DELETE FROM Localities");
        Exec("DELETE FROM Districts");
        Exec("DELETE FROM Sectors");
        Exec("DELETE FROM EducationalPrograms");
        Exec("DELETE FROM Domains");
        Exec("DELETE FROM Subjects");
        Exec("DELETE FROM DiscussionCodes");
        Exec("DELETE FROM GradeLevels");
        Exec("DELETE FROM SchoolClasses");
        Exec("DELETE FROM LocalityDistrictNationals");
        Exec("DELETE FROM EducationalStages");
        Exec("DELETE FROM EducationTypes");
        Exec("DELETE FROM Projects");
        Exec("DELETE FROM Programs");
    }

    private void ImportLookup(IEnumerable<string> values, string table, Dictionary<string, int> ids, string? extraColumn = null)
    {
        foreach (var value in values.Where(v => !string.IsNullOrWhiteSpace(v)).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(v => v, StringComparer.CurrentCulture))
        {
            string sql = extraColumn == null
                ? $"INSERT INTO {table} (Description, IsActive, CreatedAt) OUTPUT INSERTED.Id VALUES (@description, 1, SYSUTCDATETIME())"
                : $"INSERT INTO {table} (Description, IsActive, CreatedAt, {extraColumn}) OUTPUT INSERTED.Id VALUES (@description, 1, SYSUTCDATETIME(), NULL)";
            ids[Key(value)] = ScalarInt(sql, ("@description", value));
        }
    }

    private void ImportInstitutions(ImportData data)
    {
        foreach (var institution in data.Institutions.Values.OrderBy(x => x.Symbol).ThenBy(x => x.Stage, StringComparer.CurrentCulture))
        {
            var symbol = IntOrNull(institution.Symbol);
            if (!symbol.HasValue) continue;
            ScalarInt(@"INSERT INTO Institutions (InstitutionSymbol, Name, IsActive, LocalityId, DistrictId, SectorId, TypeId, EducationalStageId, CreatedAt)
OUTPUT INSERTED.Id VALUES (@symbol, @name, 1, @localityId, @districtId, @sectorId, @typeId, @stageId, SYSUTCDATETIME())",
                ("@symbol", symbol.Value),
                ("@name", institution.Name),
                ("@localityId", IdOrNull(_localityIds, institution.Locality)),
                ("@districtId", IdOrNull(_districtIds, institution.District)),
                ("@sectorId", IdOrNull(_sectorIds, institution.Sector)),
                ("@typeId", IdOrNull(_educationTypeIds, institution.Type)),
                ("@stageId", IdOrNull(_educationalStageIds, institution.Stage)));
        }
    }

    private void ImportFrameworks(ImportData data)
    {
        foreach (var framework in data.Frameworks.Values.OrderBy(x => x.IsInstitution).ThenBy(x => x.Symbol, StringComparer.CurrentCulture))
        {
            _frameworkIds[framework.Key] = ScalarInt(@"INSERT INTO Frameworks (InstitutionSymbol, EducationalStageId, Description, IsActive, CreatedAt)
OUTPUT INSERTED.Id VALUES (@symbol, @stageId, @description, 1, SYSUTCDATETIME())",
                ("@symbol", framework.Symbol),
                ("@stageId", IdOrNull(_educationalStageIds, framework.Stage)),
                ("@description", framework.Description));
        }
    }

    private void ImportProjectPrograms(ImportData data)
    {
        foreach (var key in data.ProjectProgramKeys.OrderBy(k => k.Project, StringComparer.CurrentCulture).ThenBy(k => k.Program, StringComparer.CurrentCulture))
        {
            Exec("INSERT INTO ProjectPrograms (ProjectId, ProgramId) VALUES (@projectId, @programId)",
                ("@projectId", _projectIds[Key(key.Project)]),
                ("@programId", _programIds[Key(key.Program)]));
        }
    }

    private void ImportProjectProgramScopes(ImportData data)
    {
        foreach (var (key, scope) in data.ProjectProgramScopes)
        {
            var projectId = _projectIds[Key(key.Project)];
            var programId = _programIds[Key(key.Program)];
            InsertScope("ProjectProgramEducationalPrograms", projectId, programId, "EducationalProgramId", scope.EducationalPrograms, _educationalProgramIds);
            InsertScope("ProjectProgramDomains", projectId, programId, "DomainId", scope.Domains, _domainIds);
            InsertScope("ProjectProgramSubjects", projectId, programId, "SubjectId", scope.Subjects, _subjectIds);
            InsertScope("ProjectProgramDiscussionCodes", projectId, programId, "DiscussionCodeId", scope.DiscussionCodes, _discussionCodeIds);
            InsertScope("ProjectProgramClasses", projectId, programId, "ClassId", scope.SchoolClasses, _schoolClassIds);
            InsertScope("ProjectProgramGradeLevels", projectId, programId, "GradeLevelId", scope.GradeLevels, _gradeLevelIds);
            foreach (var frameworkKey in scope.FrameworkKeys)
            {
                if (_frameworkIds.TryGetValue(frameworkKey, out var frameworkId))
                {
                    Exec("INSERT INTO ProjectProgramFrameworks (ProjectId, ProgramId, FrameworkId) VALUES (@projectId, @programId, @valueId)",
                        ("@projectId", projectId), ("@programId", programId), ("@valueId", frameworkId));
                }
            }
        }
    }

    private void ImportUsers(ImportData data)
    {
        foreach (var employee in data.Employees.Values.OrderBy(e => e.EmployeeCode, StringComparer.CurrentCulture))
        {
            var roleId = IdOrDefault(_employeeRoleIds, employee.RoleDescription, IdOrDefault(_employeeRoleIds, "מנחה", 1));
            var userRoleId = IdOrDefault(_userRoleByHebrew, employee.UserRoleDescription, 6);
            var statusId = IdOrDefault(_userStatusByHebrew, employee.StatusDescription, 1);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(employee.IdNumber, 12);
            var userId = ScalarInt(@"INSERT INTO Users (EmployeeCode, IdNumber, FirstName, LastName, PasswordHash, RoleId, UserRoleId, StatusId, IsReportingEmployee, RestDay, AllowFutureReporting, Notes, Email, Phone, MustChangePassword, FailedLoginAttempts, AcceptedTermsOfUse, CreatedAt)
OUTPUT INSERTED.Id VALUES (@employeeCode, @idNumber, @firstName, @lastName, @passwordHash, @roleId, @userRoleId, @statusId, @isReportingEmployee, @restDay, @allowFutureReporting, NULL, @email, @phone, 1, 0, 0, SYSUTCDATETIME())",
                ("@employeeCode", employee.EmployeeCode),
                ("@idNumber", employee.IdNumber),
                ("@firstName", employee.FirstName),
                ("@lastName", employee.LastName),
                ("@passwordHash", passwordHash),
                ("@roleId", roleId),
                ("@userRoleId", userRoleId),
                ("@statusId", statusId),
                ("@isReportingEmployee", employee.IsReportingEmployee),
                ("@restDay", employee.RestDay),
                ("@allowFutureReporting", employee.AllowFutureReporting),
                ("@email", NullIfEmpty(employee.Email)),
                ("@phone", NullIfEmpty(employee.Phone)));
            _userIdsByIdNumber[Key(employee.IdNumber)] = userId;
            Exec("INSERT INTO PasswordHistories (UserId, PasswordHash, CreatedAt) VALUES (@userId, @passwordHash, SYSUTCDATETIME())",
                ("@userId", userId), ("@passwordHash", passwordHash));
        }
    }

    private void ImportAllocations(ImportData data)
    {
        var assignmentLookup = data.FrameworkAssignments
            .GroupBy(a => AssignmentKey(a.IdNumber, a.EmployeeCode, a.Program), StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.Select(a => a.FrameworkKey).ToHashSet(StringComparer.OrdinalIgnoreCase), StringComparer.OrdinalIgnoreCase);

        foreach (var allocation in data.Allocations)
        {
            if (!_userIdsByIdNumber.TryGetValue(Key(allocation.IdNumber), out var userId)) continue;
            if (!_projectIds.TryGetValue(Key(allocation.Project), out var projectId)) continue;
            if (!_programIds.TryGetValue(Key(allocation.Program), out var programId)) continue;
            var reportTypeId = ResolveReportTypeId(allocation.ReportType);
            var allocationId = ScalarInt(@"INSERT INTO Allocations (UserId, ProjectId, ReportTypeId, AnnualEmploymentScope, MonthlyEmploymentScope, DailyEmploymentScope, MonthlyRowAllocation, AnnualRowAllocation, OutputDuration, AllowExcelUpload, Notes, IsActive, CreatedAt)
OUTPUT INSERTED.Id VALUES (@userId, @projectId, @reportTypeId, @annualScope, @monthlyScope, @dailyScope, NULL, NULL, @outputDuration, 1, NULL, 1, SYSUTCDATETIME())",
                ("@userId", userId),
                ("@projectId", projectId),
                ("@reportTypeId", reportTypeId),
                ("@annualScope", allocation.AnnualEmploymentScope),
                ("@monthlyScope", allocation.MonthlyEmploymentScope),
                ("@dailyScope", allocation.DailyEmploymentScope),
                ("@outputDuration", NullIfEmpty(allocation.OutputDuration)));
            _allocationIds[$"{Key(allocation.IdNumber)}|{Key(allocation.Project)}|{Key(allocation.Program)}|{allocation.SourceRow}"] = allocationId;

            BufferPair("AllocationPrograms", "AllocationId", "ProgramId", allocationId, programId);
            BufferPairIfPresent("AllocationDistricts", "AllocationId", "DistrictId", allocationId, _districtIds, allocation.District);
            BufferPairIfPresent("AllocationSectors", "AllocationId", "SectorId", allocationId, _sectorIds, allocation.Sector);

            var projectProgram = new ProjectProgramKey(allocation.Project, allocation.Program);
            var scope = data.ProjectProgramScopes.GetValueOrDefault(projectProgram) ?? new CodeSet();
            InsertAllocationScopes(allocationId, scope);

            var assignmentKey = AssignmentKey(allocation.IdNumber, allocation.EmployeeCode, allocation.Program);
            var institutionFrameworks = assignmentLookup.TryGetValue(assignmentKey, out var assigned)
                ? assigned
                : data.FileFrameworksByProjectProgram.GetValueOrDefault(projectProgram) ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var frameworkKey in institutionFrameworks.Concat(scope.FrameworkKeys).Distinct(StringComparer.OrdinalIgnoreCase))
            {
                if (_frameworkIds.TryGetValue(frameworkKey, out var frameworkId))
                {
                    BufferPair("AllocationFrameworks", "AllocationId", "FrameworkId", allocationId, frameworkId);
                    if (data.Frameworks.TryGetValue(frameworkKey, out var framework) && framework.IsInstitution)
                    {
                        BufferPairIfPresent("AllocationLocalities", "AllocationId", "LocalityId", allocationId, _localityIds, framework.Locality);
                    }
                }
            }
        }
        FlushPairBuffers();
    }

    private void InsertAllocationScopes(int allocationId, CodeSet scope)
    {
        BufferAllocationScope("AllocationEducationalPrograms", "EducationalProgramId", allocationId, scope.EducationalPrograms, _educationalProgramIds);
        BufferAllocationScope("AllocationDomains", "DomainId", allocationId, scope.Domains, _domainIds);
        BufferAllocationScope("AllocationSubjects", "SubjectId", allocationId, scope.Subjects, _subjectIds);
        BufferAllocationScope("AllocationDiscussionCodes", "DiscussionCodeId", allocationId, scope.DiscussionCodes, _discussionCodeIds);
        BufferAllocationScope("AllocationClasses", "ClassId", allocationId, scope.SchoolClasses, _schoolClassIds);
        BufferAllocationScope("AllocationGradeLevels", "GradeLevelId", allocationId, scope.GradeLevels, _gradeLevelIds);
        BufferAllocationScope("AllocationLocalityDistrictNationals", "LocalityDistrictNationalId", allocationId, scope.LocalityDistrictNationals, _ldnIds);
    }

    private void InsertScope(string table, int projectId, int programId, string column, IEnumerable<string> values, Dictionary<string, int> ids)
    {
        foreach (var value in values)
        {
            if (ids.TryGetValue(Key(value), out var id))
            {
                Exec($"INSERT INTO {table} (ProjectId, ProgramId, {column}) VALUES (@projectId, @programId, @valueId)",
                    ("@projectId", projectId), ("@programId", programId), ("@valueId", id));
            }
        }
    }

    private void BufferAllocationScope(string table, string column, int allocationId, IEnumerable<string> values, Dictionary<string, int> ids)
    {
        foreach (var value in values)
        {
            if (ids.TryGetValue(Key(value), out var id))
            {
                BufferPair(table, "AllocationId", column, allocationId, id);
            }
        }
    }

    private void BufferPairIfPresent(string table, string leftColumn, string rightColumn, int leftId, Dictionary<string, int> ids, string value)
    {
        if (ids.TryGetValue(Key(value), out var rightId))
        {
            BufferPair(table, leftColumn, rightColumn, leftId, rightId);
        }
    }

    private void BufferPair(string table, string leftColumn, string rightColumn, int leftId, int rightId)
    {
        if (!_pairBuffers.TryGetValue(table, out var buffer))
        {
            buffer = new PairBulkBuffer(table, leftColumn, rightColumn);
            _pairBuffers[table] = buffer;
        }
        buffer.Add(leftId, rightId);
    }

    private void FlushPairBuffers()
    {
        foreach (var buffer in _pairBuffers.Values)
        {
            if (buffer.Rows.Rows.Count == 0) continue;
            using var bulk = new SqlBulkCopy(_connection, SqlBulkCopyOptions.CheckConstraints, _tx)
            {
                DestinationTableName = buffer.Table,
                BatchSize = 5000,
                BulkCopyTimeout = 900
            };
            bulk.ColumnMappings.Add(buffer.LeftColumn, buffer.LeftColumn);
            bulk.ColumnMappings.Add(buffer.RightColumn, buffer.RightColumn);
            bulk.WriteToServer(buffer.Rows);
        }
        _pairBuffers.Clear();
    }

    private void InsertPair(string table, string leftColumn, string rightColumn, int leftId, int rightId)
    {
        Exec($@"IF NOT EXISTS (SELECT 1 FROM {table} WHERE {leftColumn} = @leftId AND {rightColumn} = @rightId)
INSERT INTO {table} ({leftColumn}, {rightColumn}) VALUES (@leftId, @rightId)",
            ("@leftId", leftId), ("@rightId", rightId));
    }

    private int? ResolveReportTypeId(string value)
    {
        var key = Key(value);
        if (key.Contains("ארצי") && _reportTypeByDescription.TryGetValue("ארצי מחוזי", out var national)) return national;
        if ((key.Contains("יישובי") || key.Contains("ישובי") || key.Contains("מוסדי")) && _reportTypeByDescription.TryGetValue("יישובי מוסדי", out var local)) return local;
        return null;
    }

    private static string AssignmentKey(string idNumber, string employeeCode, string program) => $"{Key(idNumber)}|{Key(employeeCode)}|{Key(program)}";

    private int IdOrDefault(Dictionary<string, int> ids, string value, int fallback) => ids.TryGetValue(Key(value), out var id) ? id : fallback;
    private int? IdOrNull(Dictionary<string, int> ids, string value) => ids.TryGetValue(Key(value), out var id) ? id : null;
    private static object NullIfEmpty(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private int ScalarInt(string sql, params (string Name, object? Value)[] parameters)
    {
        using var cmd = CreateCommand(sql, parameters);
        return Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.InvariantCulture);
    }

    private void Exec(string sql, params (string Name, object? Value)[] parameters)
    {
        using var cmd = CreateCommand(sql, parameters);
        cmd.ExecuteNonQuery();
    }

    private SqlCommand CreateCommand(string sql, params (string Name, object? Value)[] parameters)
    {
        var cmd = new SqlCommand(sql, _connection, _tx) { CommandTimeout = 300 };
        foreach (var (name, value) in parameters)
        {
            cmd.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }
        return cmd;
    }
}

sealed class ImportData
{
    public int FileCount { get; set; }
    public Dictionary<string, EmployeeRecord> Employees { get; } = new(StringComparer.OrdinalIgnoreCase);
    public List<AllocationRecord> Allocations { get; } = new();
    public List<FrameworkAssignment> FrameworkAssignments { get; } = new();
    public HashSet<string> EmployeeRoles { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Projects { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Programs { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Districts { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Sectors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Localities { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> EducationTypes { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> EducationalStages { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> EducationalPrograms { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Domains { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Subjects { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> DiscussionCodes { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> GradeLevels { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> SchoolClasses { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> LocalityDistrictNationals { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, InstitutionRecord> Institutions { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, FrameworkRecord> Frameworks { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<ProjectProgramKey> ProjectProgramKeys { get; } = new();
    public Dictionary<ProjectProgramKey, CodeSet> ProjectProgramScopes { get; } = new();
    public Dictionary<ProjectProgramKey, HashSet<string>> FileFrameworksByProjectProgram { get; } = new();
    public List<string> Warnings { get; } = new();

    public void AddEmployee(EmployeeRecord employee)
    {
        var key = Key(employee.IdNumber);
        if (!Employees.TryGetValue(key, out var existing))
        {
            Employees[key] = employee;
            return;
        }
        if (!string.Equals(Key(existing.EmployeeCode), Key(employee.EmployeeCode), StringComparison.OrdinalIgnoreCase))
        {
            Warnings.Add($"Duplicate ID {employee.IdNumber}: employee code differs ({existing.EmployeeCode} / {employee.EmployeeCode})");
        }
        existing.FillBlanksFrom(employee);
    }

    public void AddLookup(HashSet<string> target, string value, HashSet<string>? scoped = null)
    {
        value = Clean(value);
        if (string.IsNullOrWhiteSpace(value)) return;
        target.Add(value);
        scoped?.Add(value);
    }

    public string AddTextFramework(string value)
    {
        value = Clean(value);
        var key = FrameworkKey(value, null);
        if (!Frameworks.ContainsKey(key))
        {
            Frameworks[key] = new FrameworkRecord(key, value, value, null, false, string.Empty, string.Empty, string.Empty);
        }
        return key;
    }

    public string AddInstitutionFramework(string symbol, string name, string locality, string district, string sector, string? type, string? stage, string file, int row)
    {
        symbol = Clean(symbol);
        name = Clean(name);
        locality = Clean(locality);
        district = Clean(district);
        sector = Clean(sector);
        type = Clean(type);
        stage = Clean(stage);
        symbol = NormalizeInstitutionSymbol(symbol);
        if (string.IsNullOrWhiteSpace(symbol) || string.IsNullOrWhiteSpace(name))
        {
            Warnings.Add($"{file}:{row}: institution row skipped because symbol or name is empty");
            return string.Empty;
        }
        AddLookup(Localities, locality);
        AddLookup(Districts, district);
        AddLookup(Sectors, sector);
        AddLookup(EducationTypes, type);
        AddLookup(EducationalStages, stage);
        var key = FrameworkKey(symbol, stage);
        if (!Institutions.ContainsKey(key))
        {
            Institutions[key] = new InstitutionRecord(symbol, name, locality, district, sector, type, stage);
        }
        if (!Frameworks.ContainsKey(key))
        {
            Frameworks[key] = new FrameworkRecord(key, symbol, name, stage, true, locality, district, sector);
        }
        return key;
    }

    private static string NormalizeInstitutionSymbol(string symbol)
    {
        symbol = Clean(symbol);
        var digits = new string(symbol.Where(char.IsDigit).ToArray());
        return digits.Length >= 3 && int.TryParse(digits, out _) ? digits : symbol;
    }

    public void MergeProjectProgramScope(ProjectProgramKey key, CodeSet source)
    {
        if (!ProjectProgramScopes.TryGetValue(key, out var target))
        {
            target = new CodeSet();
            ProjectProgramScopes[key] = target;
        }
        target.Merge(source);
    }

    public void FinalizeDerivedData()
    {
        foreach (var pp in ProjectProgramKeys)
        {
            if (!ProjectProgramScopes.ContainsKey(pp))
            {
                ProjectProgramScopes[pp] = new CodeSet();
            }
        }
    }

    public ValidationResult Validate(HashSet<string> preservedIdNumbers)
    {
        var result = new ValidationResult();
        result.Warnings.AddRange(Warnings);
        foreach (var employee in Employees.Values)
        {
            if (preservedIdNumbers.Contains(Clean(employee.IdNumber)))
            {
                result.FatalErrors.Add($"Excel employee ID conflicts with preserved non-employee user: {employee.IdNumber} {employee.FirstName} {employee.LastName}");
            }
            if (string.IsNullOrWhiteSpace(employee.EmployeeCode) || string.IsNullOrWhiteSpace(employee.FirstName) || string.IsNullOrWhiteSpace(employee.LastName))
            {
                result.FatalErrors.Add($"Employee row has missing required fields: {employee.SourceFile}:{employee.SourceRow} ID={employee.IdNumber}");
            }
        }
        foreach (var allocation in Allocations)
        {
            if (!Employees.ContainsKey(Key(allocation.IdNumber)))
            {
                result.FatalErrors.Add($"Allocation without matching employee: {allocation.SourceFile}:{allocation.SourceRow} ID={allocation.IdNumber}");
            }
            if (string.IsNullOrWhiteSpace(allocation.Project) || string.IsNullOrWhiteSpace(allocation.Program))
            {
                result.FatalErrors.Add($"Allocation missing project/program: {allocation.SourceFile}:{allocation.SourceRow} ID={allocation.IdNumber}");
            }
        }
        foreach (var assignment in FrameworkAssignments.Where(a => !string.IsNullOrWhiteSpace(a.FrameworkKey)))
        {
            if (!Employees.ContainsKey(Key(assignment.IdNumber)))
            {
                result.Warnings.Add($"Framework assignment without matching employee: {assignment.SourceFile}:{assignment.SourceRow} ID={assignment.IdNumber}");
            }
        }
        return result;
    }

    public static string FrameworkKey(string symbol, string? stage) => $"{Key(symbol)}|{Key(stage)}";
}

sealed class PairBulkBuffer
{
    private readonly HashSet<string> _keys = new(StringComparer.OrdinalIgnoreCase);

    public PairBulkBuffer(string table, string leftColumn, string rightColumn)
    {
        Table = table;
        LeftColumn = leftColumn;
        RightColumn = rightColumn;
        Rows = new DataTable();
        Rows.Columns.Add(leftColumn, typeof(int));
        Rows.Columns.Add(rightColumn, typeof(int));
    }

    public string Table { get; }
    public string LeftColumn { get; }
    public string RightColumn { get; }
    public DataTable Rows { get; }

    public void Add(int leftId, int rightId)
    {
        if (!_keys.Add($"{leftId}|{rightId}"))
        {
            return;
        }
        Rows.Rows.Add(leftId, rightId);
    }
}

sealed class CodeSet
{
    public HashSet<string> EducationalPrograms { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Domains { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Subjects { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> DiscussionCodes { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> SchoolClasses { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> GradeLevels { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> LocalityDistrictNationals { get; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> FrameworkKeys { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void Merge(CodeSet other)
    {
        EducationalPrograms.UnionWith(other.EducationalPrograms);
        Domains.UnionWith(other.Domains);
        Subjects.UnionWith(other.Subjects);
        DiscussionCodes.UnionWith(other.DiscussionCodes);
        SchoolClasses.UnionWith(other.SchoolClasses);
        GradeLevels.UnionWith(other.GradeLevels);
        LocalityDistrictNationals.UnionWith(other.LocalityDistrictNationals);
        FrameworkKeys.UnionWith(other.FrameworkKeys);
    }
}

sealed class HeaderMap
{
    private readonly Dictionary<string, List<(string Original, int Column)>> _columns = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, List<(string Original, int Column)>> _looseColumns = new(StringComparer.OrdinalIgnoreCase);
    public int Count => _columns.Count;
    public bool Has(string header) => TryGetColumns(header, out _);
    public List<int> Columns(string header) => TryGetColumns(header, out var list) ? list.Select(x => x.Column).ToList() : new List<int>();

    public static HeaderMap FromWorksheet(IXLWorksheet ws, int row, int firstCol, int lastCol)
    {
        var map = new HeaderMap();
        for (var col = firstCol; col <= lastCol; col++)
        {
            var header = Clean(CellText(ws.Cell(row, col)));
            if (string.IsNullOrWhiteSpace(header)) continue;
            var key = Key(header);
            if (!map._columns.TryGetValue(key, out var list))
            {
                list = new List<(string, int)>();
                map._columns[key] = list;
            }
            list.Add((header, col));

            var looseKey = LooseKey(header);
            if (!string.IsNullOrWhiteSpace(looseKey))
            {
                if (!map._looseColumns.TryGetValue(looseKey, out var looseList))
                {
                    looseList = new List<(string, int)>();
                    map._looseColumns[looseKey] = looseList;
                }
                looseList.Add((header, col));
            }
        }
        return map;
    }

    public string Get(IXLWorksheet ws, int row, params string[] headers)
    {
        foreach (var header in headers)
        {
            if (TryGetColumns(header, out var cols) && cols.Count > 0)
            {
                return CellText(ws.Cell(row, cols[0].Column));
            }
        }
        return string.Empty;
    }

    public IEnumerable<string> GetAll(IXLWorksheet ws, int row, string header)
    {
        if (!TryGetColumns(header, out var cols))
        {
            return Enumerable.Empty<string>();
        }
        return cols.Select(col => CellText(ws.Cell(row, col.Column))).Where(v => !string.IsNullOrWhiteSpace(v));
    }

    private bool TryGetColumns(string header, out List<(string Original, int Column)> columns)
    {
        if (_columns.TryGetValue(Key(header), out columns!))
        {
            return true;
        }
        return _looseColumns.TryGetValue(LooseKey(header), out columns!);
    }
}

sealed record EmployeeRecord
{
    public string IdNumber { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int? RestDay { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsReportingEmployee { get; set; }
    public string RoleDescription { get; set; } = string.Empty;
    public bool AllowFutureReporting { get; set; }
    public string UserRoleDescription { get; set; } = string.Empty;
    public string StatusDescription { get; set; } = string.Empty;
    public string SourceFile { get; set; } = string.Empty;
    public int SourceRow { get; set; }

    public void FillBlanksFrom(EmployeeRecord other)
    {
        if (string.IsNullOrWhiteSpace(EmployeeCode)) EmployeeCode = other.EmployeeCode;
        if (string.IsNullOrWhiteSpace(FirstName)) FirstName = other.FirstName;
        if (string.IsNullOrWhiteSpace(LastName)) LastName = other.LastName;
        if (!RestDay.HasValue) RestDay = other.RestDay;
        if (string.IsNullOrWhiteSpace(Phone)) Phone = other.Phone;
        if (string.IsNullOrWhiteSpace(Email)) Email = other.Email;
        if (string.IsNullOrWhiteSpace(RoleDescription)) RoleDescription = other.RoleDescription;
        if (string.IsNullOrWhiteSpace(UserRoleDescription)) UserRoleDescription = other.UserRoleDescription;
        if (string.IsNullOrWhiteSpace(StatusDescription)) StatusDescription = other.StatusDescription;
        IsReportingEmployee = IsReportingEmployee || other.IsReportingEmployee;
        AllowFutureReporting = AllowFutureReporting || other.AllowFutureReporting;
    }
}

sealed record AllocationRecord
{
    public string IdNumber { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string Program { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public decimal? MonthlyEmploymentScope { get; set; }
    public decimal? AnnualEmploymentScope { get; set; }
    public decimal? DailyEmploymentScope { get; set; }
    public string OutputDuration { get; set; } = string.Empty;
    public string ReportType { get; set; } = string.Empty;
    public string SourceFile { get; set; } = string.Empty;
    public int SourceRow { get; set; }
}

sealed record FrameworkAssignment
{
    public string IdNumber { get; init; } = string.Empty;
    public string EmployeeCode { get; init; } = string.Empty;
    public string Program { get; init; } = string.Empty;
    public string FrameworkKey { get; init; } = string.Empty;
    public string SourceFile { get; init; } = string.Empty;
    public int SourceRow { get; init; }
}

sealed record InstitutionRecord(string Symbol, string Name, string Locality, string District, string Sector, string Type, string Stage);
sealed record FrameworkRecord(string Key, string Symbol, string Description, string Stage, bool IsInstitution, string Locality, string District, string Sector);
sealed record ProjectProgramKey(string Project, string Program);
sealed record ValidationResult
{
    public List<string> Warnings { get; } = new();
    public List<string> FatalErrors { get; } = new();
}

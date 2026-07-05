using System.Data;
using System.Text;
using ExcelDataReader;
using Microsoft.Data.SqlClient;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var workbookPath = args.Length > 0
    ? args[0]
    : @"C:\Users\Administrator\Downloads\עותק של עותק של קובץ מרכז בסיס נתונים לתוכנית לצורכי בנייית אפיון- 15.03.26.xlsb";
var mode = args.Length > 1 ? args[1].ToLowerInvariant() : "inspect";
var connectionString = args.Length > 2
    ? args[2]
    : @"Server=.\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

using var stream = File.Open(workbookPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
using var reader = ExcelReaderFactory.CreateReader(stream);
var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
{
    ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = false }
});

if (mode == "sheets")
{
    foreach (DataTable table in dataSet.Tables)
    {
        Console.WriteLine($"{table.TableName}\trows={table.Rows.Count}\tcols={table.Columns.Count}");
    }
    return;
}

var center = FindTable(dataSet, "גליון מרכז רשימות לפי שדה")
    ?? FindTable(dataSet, "גיליון מרכז רשימות לפי שדות")
    ?? dataSet.Tables.Cast<DataTable>().OrderByDescending(t => t.Rows.Count * t.Columns.Count).FirstOrDefault()
    ?? throw new InvalidOperationException("No usable sheet found.");

var lookups = ExtractCenterLookups(center);

if (mode == "inspect")
{
    Console.WriteLine($"CENTER_SHEET\t{center.TableName}\trows={center.Rows.Count}\tcols={center.Columns.Count}");
    foreach (var item in lookups)
    {
        Console.WriteLine($"{item.HebrewName}\t{item.TableName}\t{item.Values.Count}");
        foreach (var value in item.Values.Take(12)) Console.WriteLine($"  - {value}");
        if (item.Values.Count > 12) Console.WriteLine("  ...");
    }
    return;
}

if (mode == "dump")
{
    var start = args.Length > 2 && int.TryParse(args[2], out var parsedStart) ? parsedStart : 0;
    var count = args.Length > 3 && int.TryParse(args[3], out var parsedCount) ? parsedCount : 90;
    var maxCol = args.Length > 4 && int.TryParse(args[4], out var parsedCol) ? parsedCol : Math.Min(center.Columns.Count, 18);
    Console.WriteLine($"CENTER_SHEET\t{center.TableName}\trows={center.Rows.Count}\tcols={center.Columns.Count}");
    for (var r = start; r < Math.Min(center.Rows.Count, start + count); r++)
    {
        var cells = new List<string> { (r + 1).ToString() };
        for (var c = 0; c < maxCol; c++)
        {
            cells.Add(Cell(center, r, c).Replace("\r", " ").Replace("\n", " "));
        }
        Console.WriteLine(string.Join("\t", cells));
    }
    return;
}

if (mode == "dbcounts")
{
    await using var countConnection = new SqlConnection(connectionString);
    await countConnection.OpenAsync();
    foreach (var lookup in lookups)
    {
        await using var command = countConnection.CreateCommand();
        command.CommandText = $"SELECT COUNT(*), SUM(CASE WHEN [IsActive] = 1 THEN 1 ELSE 0 END) FROM [{lookup.TableName.Replace("]", "]]")}]";
        await using var dbReader = await command.ExecuteReaderAsync();
        if (await dbReader.ReadAsync())
        {
            Console.WriteLine($"{lookup.HebrewName}\t{lookup.TableName}\ttotal={dbReader.GetInt32(0)}\tactive={dbReader.GetInt32(1)}\texpectedActive={lookup.Values.Count}");
        }
    }
    return;
}

if (mode != "replace")
{
    throw new ArgumentException("Mode must be sheets, inspect, dump, dbcounts, or replace.");
}

await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();

var backupName = "AxiomaReporting_lookup_replace_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
await using (var backup = connection.CreateCommand())
{
    backup.CommandTimeout = 120;
    backup.CommandText = $"BACKUP DATABASE [AxiomaReporting] TO DISK = N'C:\\webprojects\\Exioma\\{backupName}.bak' WITH INIT, COPY_ONLY";
    await backup.ExecuteNonQueryAsync();
}

await using var tx = (SqlTransaction)await connection.BeginTransactionAsync();
try
{
    var now = DateTime.UtcNow;
    foreach (var lookup in lookups)
    {
        var result = await ReplaceLookupAsync(connection, tx, lookup.TableName, lookup.Values, now);
        Console.WriteLine($"{lookup.HebrewName}\t{lookup.TableName}\tactive={lookup.Values.Count}\tinserted={result.Inserted}\treactivated={result.Reactivated}\tinactivated={result.Inactivated}");
    }

    var sync = await SyncActiveProjectProgramScopesAsync(connection, tx);
    Console.WriteLine($"שיוכי פרויקט-תוכנית\tlinked={sync.LinkedProjectPrograms}\tscopeRows={sync.ScopeRows}");

    await tx.CommitAsync();
    Console.WriteLine($"BACKUP\tC:\\webprojects\\Exioma\\{backupName}.bak");
}
catch
{
    await tx.RollbackAsync();
    throw;
}

static DataTable? FindTable(DataSet dataSet, string name)
{
    return dataSet.Tables.Cast<DataTable>().FirstOrDefault(t =>
        string.Equals(t.TableName, name, StringComparison.Ordinal) ||
        t.TableName.Contains(name, StringComparison.Ordinal));
}

static List<LookupImport> ExtractCenterLookups(DataTable table)
{
    var result = new List<LookupImport>();
    var sectors = new SortedSet<string>(StringComparer.Ordinal);
    var districts = new SortedSet<string>(StringComparer.Ordinal);
    var stages = new SortedSet<string>(StringComparer.Ordinal);
    var projects = new SortedSet<string>(StringComparer.Ordinal);
    var programs = new SortedSet<string>(StringComparer.Ordinal);
    var educationalPrograms = new SortedSet<string>(StringComparer.Ordinal);
    var domains = new SortedSet<string>(StringComparer.Ordinal);
    var subjects = new SortedSet<string>(StringComparer.Ordinal);
    var discussionCodes = new SortedSet<string>(StringComparer.Ordinal);
    var frameworks = new SortedSet<string>(StringComparer.Ordinal);
    var localityDistrictNationals = new SortedSet<string>(StringComparer.Ordinal);
    var gradeLevels = new SortedSet<string>(StringComparer.Ordinal);
    var classes = new SortedSet<string>(StringComparer.Ordinal);

    for (var i = 6; i <= 19 && i < table.Rows.Count; i++)
    {
        Add(projects, Cell(table, i, 6));
        Add(programs, Cell(table, i, 8));
    }

    for (var i = 26; i <= 34 && i < table.Rows.Count; i++)
    {
        Add(sectors, Cell(table, i, 2), "מגזר עובד", "None");
    }

    for (var i = 41; i <= 56 && i < table.Rows.Count; i++)
    {
        Add(districts, Cell(table, i, 4), "מחוז", "None");
        Add(sectors, Cell(table, i, 6), "מגזר", "None");
        Add(stages, Cell(table, i, 12), "שלב חינוך", "None");
        Add(gradeLevels, Cell(table, i, 14), "שכבות גיל", "None");
    }

    for (var i = 61; i < table.Rows.Count; i++)
    {
        Add(programs, Cell(table, i, 0));
        Add(educationalPrograms, Cell(table, i, 2));
        Add(domains, Cell(table, i, 4));
        Add(subjects, Cell(table, i, 6));
        Add(subjects, Cell(table, i, 8));
        Add(discussionCodes, Cell(table, i, 10));
        Add(classes, Cell(table, i, 12));
        Add(frameworks, Cell(table, i, 14));
        Add(localityDistrictNationals, Cell(table, i, 16));
        Add(gradeLevels, Cell(table, i, 18));
        Add(classes, Cell(table, i, 20));
    }

    result.Add(new("מחוזות", "Districts", districts.ToList()));
    result.Add(new("מגזרים", "Sectors", sectors.ToList()));
    result.Add(new("שלבי חינוך", "EducationalStages", stages.ToList()));
    result.Add(new("פרויקטים", "Projects", projects.ToList()));
    result.Add(new("תוכניות", "Programs", programs.ToList()));
    result.Add(new("תוכניות חינוכיות", "EducationalPrograms", educationalPrograms.ToList()));
    result.Add(new("תחומים", "Domains", domains.ToList()));
    result.Add(new("נושאים", "Subjects", subjects.ToList()));
    result.Add(new("קודי דיון", "DiscussionCodes", discussionCodes.ToList()));
    result.Add(new("מסגרות", "Frameworks", frameworks.ToList()));
    result.Add(new("יישוב/מחוז/ארצי", "LocalityDistrictNationals", localityDistrictNationals.ToList()));
    result.Add(new("שכבות", "GradeLevels", gradeLevels.ToList()));
    result.Add(new("כיתות", "SchoolClasses", classes.ToList()));
    return result;
}

static string Cell(DataTable table, int row, int col)
{
    if (row < 0 || row >= table.Rows.Count || col < 0 || col >= table.Columns.Count) return string.Empty;
    return table.Rows[row][col]?.ToString()?.Trim() ?? string.Empty;
}

static void Add(ISet<string> values, string value, params string[] excluded)
{
    value = value.Trim();
    if (value.Length == 0) return;
    if (excluded.Contains(value, StringComparer.Ordinal)) return;
    values.Add(value);
}

static async Task<(int Inserted, int Reactivated, int Inactivated)> ReplaceLookupAsync(
    SqlConnection connection,
    SqlTransaction tx,
    string tableName,
    IReadOnlyCollection<string> values,
    DateTime now)
{
    var quoted = "[" + tableName.Replace("]", "]]") + "]";
    var tempName = "#Desired_" + tableName.Replace("]", "").Replace("[", "");

    await using (var create = connection.CreateCommand())
    {
        create.Transaction = tx;
        create.CommandText = $"CREATE TABLE {tempName} ([Description] nvarchar(450) NOT NULL PRIMARY KEY);";
        await create.ExecuteNonQueryAsync();
    }

    foreach (var value in values)
    {
        await using var insertTemp = connection.CreateCommand();
        insertTemp.Transaction = tx;
        insertTemp.CommandText = $"INSERT INTO {tempName} ([Description]) VALUES (@description);";
        insertTemp.Parameters.AddWithValue("@description", value);
        await insertTemp.ExecuteNonQueryAsync();
    }

    var insertSql = tableName.Equals("Frameworks", StringComparison.OrdinalIgnoreCase)
        ? $@"
INSERT INTO {quoted} ([Description], [InstitutionSymbol], [IsActive], [CreatedAt])
SELECT d.[Description], N'XLSB-' + CONVERT(varchar(32), ABS(CHECKSUM(d.[Description]))), 1, @now
FROM {tempName} d
WHERE NOT EXISTS (SELECT 1 FROM {quoted} t WHERE t.[Description] = d.[Description]);
SELECT @@ROWCOUNT;"
        : $@"
INSERT INTO {quoted} ([Description], [IsActive], [CreatedAt])
SELECT d.[Description], 1, @now
FROM {tempName} d
WHERE NOT EXISTS (SELECT 1 FROM {quoted} t WHERE t.[Description] = d.[Description]);
SELECT @@ROWCOUNT;";

    var inserted = await ExecuteScalarIntAsync(connection, tx, insertSql, now);

    var reactivated = await ExecuteScalarIntAsync(connection, tx, $@"
UPDATE t
SET [IsActive] = 1
FROM {quoted} t
JOIN {tempName} d ON d.[Description] = t.[Description]
WHERE t.[IsActive] = 0;
SELECT @@ROWCOUNT;", now);

    var inactivated = await ExecuteScalarIntAsync(connection, tx, $@"
UPDATE t
SET [IsActive] = 0
FROM {quoted} t
WHERE t.[IsActive] = 1
  AND NOT EXISTS (SELECT 1 FROM {tempName} d WHERE d.[Description] = t.[Description]);
SELECT @@ROWCOUNT;", now);

    return (inserted, reactivated, inactivated);
}

static async Task<(int LinkedProjectPrograms, int ScopeRows)> SyncActiveProjectProgramScopesAsync(SqlConnection connection, SqlTransaction tx)
{
    var linked = await ExecuteScalarIntAsync(connection, tx, @"
INSERT INTO [ProjectPrograms] ([ProjectId], [ProgramId])
SELECT p.[Id], pr.[Id]
FROM [Projects] p
CROSS JOIN [Programs] pr
WHERE p.[IsActive] = 1
  AND pr.[IsActive] = 1
  AND NOT EXISTS (
      SELECT 1
      FROM [ProjectPrograms] pp
      WHERE pp.[ProjectId] = p.[Id] AND pp.[ProgramId] = pr.[Id]
  );
SELECT @@ROWCOUNT;", DateTime.UtcNow);

    var scopeRows = 0;
    scopeRows += await ReplaceActiveScopeAsync(connection, tx, "ProjectProgramSubjects", "SubjectId", "Subjects");
    scopeRows += await ReplaceActiveScopeAsync(connection, tx, "ProjectProgramDomains", "DomainId", "Domains");
    scopeRows += await ReplaceActiveScopeAsync(connection, tx, "ProjectProgramFrameworks", "FrameworkId", "Frameworks");
    scopeRows += await ReplaceActiveScopeAsync(connection, tx, "ProjectProgramEducationalPrograms", "EducationalProgramId", "EducationalPrograms");
    scopeRows += await ReplaceActiveScopeAsync(connection, tx, "ProjectProgramDiscussionCodes", "DiscussionCodeId", "DiscussionCodes");
    scopeRows += await ReplaceActiveScopeAsync(connection, tx, "ProjectProgramGradeLevels", "GradeLevelId", "GradeLevels");
    scopeRows += await ReplaceActiveScopeAsync(connection, tx, "ProjectProgramClasses", "ClassId", "SchoolClasses");

    return (linked, scopeRows);
}

static async Task<int> ReplaceActiveScopeAsync(SqlConnection connection, SqlTransaction tx, string scopeTable, string idColumn, string lookupTable)
{
    await using (var delete = connection.CreateCommand())
    {
        delete.Transaction = tx;
        delete.CommandText = $@"
DELETE s
FROM [{scopeTable}] s
JOIN [Projects] p ON p.[Id] = s.[ProjectId] AND p.[IsActive] = 1
JOIN [Programs] pr ON pr.[Id] = s.[ProgramId] AND pr.[IsActive] = 1;";
        await delete.ExecuteNonQueryAsync();
    }

    return await ExecuteScalarIntAsync(connection, tx, $@"
INSERT INTO [{scopeTable}] ([ProjectId], [ProgramId], [{idColumn}])
SELECT pp.[ProjectId], pp.[ProgramId], l.[Id]
FROM [ProjectPrograms] pp
JOIN [Projects] p ON p.[Id] = pp.[ProjectId] AND p.[IsActive] = 1
JOIN [Programs] pr ON pr.[Id] = pp.[ProgramId] AND pr.[IsActive] = 1
CROSS JOIN [{lookupTable}] l
WHERE l.[IsActive] = 1;
SELECT @@ROWCOUNT;", DateTime.UtcNow);
}

static async Task<int> ExecuteScalarIntAsync(SqlConnection connection, SqlTransaction tx, string commandText, DateTime now)
{
    await using var command = connection.CreateCommand();
    command.Transaction = tx;
    command.CommandText = commandText;
    command.Parameters.AddWithValue("@now", now);
    return Convert.ToInt32(await command.ExecuteScalarAsync());
}

record LookupImport(string HebrewName, string TableName, List<string> Values);

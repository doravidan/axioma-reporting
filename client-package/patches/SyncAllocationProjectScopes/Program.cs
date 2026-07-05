using Microsoft.Data.SqlClient;

var mode = args.Length > 0 ? args[0].ToLowerInvariant() : "inspect";
var connectionString = args.Length > 1
    ? args[1]
    : @"Server=.\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();

if (mode == "inspect")
{
    await PrintCountsAsync(connection);
    await PrintMismatchSummaryAsync(connection);
    return;
}

if (mode != "sync")
{
    throw new ArgumentException("Mode must be inspect or sync.");
}

var backupName = "AxiomaReporting_allocation_scope_sync_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
await using (var backup = connection.CreateCommand())
{
    backup.CommandTimeout = 180;
    backup.CommandText = $"BACKUP DATABASE [AxiomaReporting] TO DISK = N'C:\\webprojects\\Exioma\\{backupName}.bak' WITH INIT, COPY_ONLY";
    await backup.ExecuteNonQueryAsync();
}

await using var tx = (SqlTransaction)await connection.BeginTransactionAsync();
try
{
    var results = new List<TableSyncResult>();
    results.Add(await SyncProgramsAsync(connection, tx));
    results.Add(await SyncScopedTableAsync(connection, tx, "AllocationSubjects", "SubjectId", "ProjectProgramSubjects"));
    results.Add(await SyncScopedTableAsync(connection, tx, "AllocationDomains", "DomainId", "ProjectProgramDomains"));
    results.Add(await SyncScopedTableAsync(connection, tx, "AllocationFrameworks", "FrameworkId", "ProjectProgramFrameworks"));
    results.Add(await SyncScopedTableAsync(connection, tx, "AllocationEducationalPrograms", "EducationalProgramId", "ProjectProgramEducationalPrograms"));
    results.Add(await SyncScopedTableAsync(connection, tx, "AllocationDiscussionCodes", "DiscussionCodeId", "ProjectProgramDiscussionCodes"));
    results.Add(await SyncScopedTableAsync(connection, tx, "AllocationGradeLevels", "GradeLevelId", "ProjectProgramGradeLevels"));
    results.Add(await SyncScopedTableAsync(connection, tx, "AllocationClasses", "ClassId", "ProjectProgramClasses"));

    await tx.CommitAsync();

    Console.WriteLine($"BACKUP\tC:\\webprojects\\Exioma\\{backupName}.bak");
    foreach (var result in results)
    {
        Console.WriteLine($"{result.Table}\tbefore={result.Before}\tdeleted={result.Deleted}\tinserted={result.Inserted}\tafter={result.After}");
    }

    await PrintMismatchSummaryAsync(connection);
}
catch
{
    await tx.RollbackAsync();
    throw;
}

static async Task PrintCountsAsync(SqlConnection connection)
{
    string[] tables =
    {
        "Allocations",
        "ProjectPrograms",
        "AllocationPrograms",
        "ProjectProgramSubjects",
        "AllocationSubjects",
        "ProjectProgramDomains",
        "AllocationDomains",
        "ProjectProgramFrameworks",
        "AllocationFrameworks",
        "ProjectProgramEducationalPrograms",
        "AllocationEducationalPrograms",
        "ProjectProgramDiscussionCodes",
        "AllocationDiscussionCodes",
        "ProjectProgramGradeLevels",
        "AllocationGradeLevels",
        "ProjectProgramClasses",
        "AllocationClasses"
    };

    foreach (var table in tables)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = $"SELECT COUNT(*) FROM dbo.[{table}]";
        var count = Convert.ToInt32(await command.ExecuteScalarAsync());
        Console.WriteLine($"{table}\t{count}");
    }
}

static async Task PrintMismatchSummaryAsync(SqlConnection connection)
{
    Console.WriteLine("MISMATCHES");
    await PrintProgramMismatchAsync(connection);
    await PrintScopedMismatchAsync(connection, "AllocationSubjects", "SubjectId", "ProjectProgramSubjects");
    await PrintScopedMismatchAsync(connection, "AllocationDomains", "DomainId", "ProjectProgramDomains");
    await PrintScopedMismatchAsync(connection, "AllocationFrameworks", "FrameworkId", "ProjectProgramFrameworks");
    await PrintScopedMismatchAsync(connection, "AllocationEducationalPrograms", "EducationalProgramId", "ProjectProgramEducationalPrograms");
    await PrintScopedMismatchAsync(connection, "AllocationDiscussionCodes", "DiscussionCodeId", "ProjectProgramDiscussionCodes");
    await PrintScopedMismatchAsync(connection, "AllocationGradeLevels", "GradeLevelId", "ProjectProgramGradeLevels");
    await PrintScopedMismatchAsync(connection, "AllocationClasses", "ClassId", "ProjectProgramClasses");
}

static async Task PrintProgramMismatchAsync(SqlConnection connection)
{
    await using var command = connection.CreateCommand();
    command.CommandText = @"
WITH Desired AS (
    SELECT a.Id AS AllocationId, pp.ProgramId
    FROM dbo.Allocations a
    INNER JOIN dbo.ProjectPrograms pp ON pp.ProjectId = a.ProjectId
    WHERE a.IsActive = 1
),
Missing AS (
    SELECT d.AllocationId, d.ProgramId
    FROM Desired d
    EXCEPT
    SELECT ap.AllocationId, ap.ProgramId
    FROM dbo.AllocationPrograms ap
    INNER JOIN dbo.Allocations a ON a.Id = ap.AllocationId
    WHERE a.IsActive = 1
),
Extra AS (
    SELECT ap.AllocationId, ap.ProgramId
    FROM dbo.AllocationPrograms ap
    INNER JOIN dbo.Allocations a ON a.Id = ap.AllocationId
    WHERE a.IsActive = 1
    EXCEPT
    SELECT d.AllocationId, d.ProgramId
    FROM Desired d
)
SELECT 'AllocationPrograms' AS TableName, (SELECT COUNT(*) FROM Missing) AS MissingRows, (SELECT COUNT(*) FROM Extra) AS ExtraRows;";
    await using var reader = await command.ExecuteReaderAsync();
    if (await reader.ReadAsync())
    {
        Console.WriteLine($"{reader.GetString(0)}\tmissing={reader.GetInt32(1)}\textra={reader.GetInt32(2)}");
    }
}

static async Task PrintScopedMismatchAsync(SqlConnection connection, string allocationTable, string idColumn, string scopeTable)
{
    await using var command = connection.CreateCommand();
    command.CommandText = $@"
WITH Desired AS (
    SELECT DISTINCT a.Id AS AllocationId, s.{idColumn}
    FROM dbo.Allocations a
    INNER JOIN dbo.ProjectPrograms pp ON pp.ProjectId = a.ProjectId
    INNER JOIN dbo.{scopeTable} s ON s.ProjectId = pp.ProjectId AND s.ProgramId = pp.ProgramId
    WHERE a.IsActive = 1
),
Missing AS (
    SELECT d.AllocationId, d.{idColumn}
    FROM Desired d
    EXCEPT
    SELECT x.AllocationId, x.{idColumn}
    FROM dbo.{allocationTable} x
    INNER JOIN dbo.Allocations a ON a.Id = x.AllocationId
    WHERE a.IsActive = 1
),
Extra AS (
    SELECT x.AllocationId, x.{idColumn}
    FROM dbo.{allocationTable} x
    INNER JOIN dbo.Allocations a ON a.Id = x.AllocationId
    WHERE a.IsActive = 1
    EXCEPT
    SELECT d.AllocationId, d.{idColumn}
    FROM Desired d
)
SELECT '{allocationTable}' AS TableName, (SELECT COUNT(*) FROM Missing) AS MissingRows, (SELECT COUNT(*) FROM Extra) AS ExtraRows;";
    await using var reader = await command.ExecuteReaderAsync();
    if (await reader.ReadAsync())
    {
        Console.WriteLine($"{reader.GetString(0)}\tmissing={reader.GetInt32(1)}\textra={reader.GetInt32(2)}");
    }
}

static async Task<TableSyncResult> SyncProgramsAsync(SqlConnection connection, SqlTransaction tx)
{
    var before = await CountAsync(connection, tx, "AllocationPrograms");
    var deleted = await ExecuteAsync(connection, tx, @"
DELETE ap
FROM dbo.AllocationPrograms ap
INNER JOIN dbo.Allocations a ON a.Id = ap.AllocationId
WHERE a.IsActive = 1
  AND NOT EXISTS (
      SELECT 1
      FROM dbo.ProjectPrograms pp
      WHERE pp.ProjectId = a.ProjectId AND pp.ProgramId = ap.ProgramId
  );");
    var inserted = await ExecuteAsync(connection, tx, @"
INSERT INTO dbo.AllocationPrograms (AllocationId, ProgramId)
SELECT a.Id, pp.ProgramId
FROM dbo.Allocations a
INNER JOIN dbo.ProjectPrograms pp ON pp.ProjectId = a.ProjectId
WHERE a.IsActive = 1
  AND NOT EXISTS (
      SELECT 1
      FROM dbo.AllocationPrograms ap
      WHERE ap.AllocationId = a.Id AND ap.ProgramId = pp.ProgramId
  );");
    var after = await CountAsync(connection, tx, "AllocationPrograms");
    return new TableSyncResult("AllocationPrograms", before, deleted, inserted, after);
}

static async Task<TableSyncResult> SyncScopedTableAsync(SqlConnection connection, SqlTransaction tx, string allocationTable, string idColumn, string scopeTable)
{
    var before = await CountAsync(connection, tx, allocationTable);
    var deleted = await ExecuteAsync(connection, tx, $@"
DELETE x
FROM dbo.{allocationTable} x
INNER JOIN dbo.Allocations a ON a.Id = x.AllocationId
WHERE a.IsActive = 1
  AND NOT EXISTS (
      SELECT 1
      FROM dbo.ProjectPrograms pp
      INNER JOIN dbo.{scopeTable} s ON s.ProjectId = pp.ProjectId AND s.ProgramId = pp.ProgramId
      WHERE pp.ProjectId = a.ProjectId AND s.{idColumn} = x.{idColumn}
  );");
    var inserted = await ExecuteAsync(connection, tx, $@"
INSERT INTO dbo.{allocationTable} (AllocationId, {idColumn})
SELECT DISTINCT a.Id, s.{idColumn}
FROM dbo.Allocations a
INNER JOIN dbo.ProjectPrograms pp ON pp.ProjectId = a.ProjectId
INNER JOIN dbo.{scopeTable} s ON s.ProjectId = pp.ProjectId AND s.ProgramId = pp.ProgramId
WHERE a.IsActive = 1
  AND NOT EXISTS (
      SELECT 1
      FROM dbo.{allocationTable} x
      WHERE x.AllocationId = a.Id AND x.{idColumn} = s.{idColumn}
  );");
    var after = await CountAsync(connection, tx, allocationTable);
    return new TableSyncResult(allocationTable, before, deleted, inserted, after);
}

static async Task<int> CountAsync(SqlConnection connection, SqlTransaction tx, string table)
{
    await using var command = connection.CreateCommand();
    command.Transaction = tx;
    command.CommandText = $"SELECT COUNT(*) FROM dbo.{table}";
    return Convert.ToInt32(await command.ExecuteScalarAsync());
}

static async Task<int> ExecuteAsync(SqlConnection connection, SqlTransaction tx, string sql)
{
    await using var command = connection.CreateCommand();
    command.Transaction = tx;
    command.CommandTimeout = 180;
    command.CommandText = sql;
    return await command.ExecuteNonQueryAsync();
}

record TableSyncResult(string Table, int Before, int Deleted, int Inserted, int After);

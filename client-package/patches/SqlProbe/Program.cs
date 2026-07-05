using Microsoft.Data.SqlClient;

const string ConnectionString = "Server=.\\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

using var connection = new SqlConnection(ConnectionString);
connection.Open();

if (args.FirstOrDefault()?.Equals("counts", StringComparison.OrdinalIgnoreCase) == true)
{
    var countSql = @"
SELECT 'Users' AS TableName, COUNT(*) AS Cnt FROM Users
UNION ALL SELECT 'EmployeeUsers', COUNT(*) FROM Users WHERE UserRoleId = 6
UNION ALL SELECT 'ManagerUsers', COUNT(*) FROM Users WHERE UserRoleId <> 6
UNION ALL SELECT 'Allocations', COUNT(*) FROM Allocations
UNION ALL SELECT 'Reports', COUNT(*) FROM Reports
UNION ALL SELECT 'ReportRows', COUNT(*) FROM ReportRows
UNION ALL SELECT 'Projects', COUNT(*) FROM Projects
UNION ALL SELECT 'Programs', COUNT(*) FROM Programs
UNION ALL SELECT 'ProjectPrograms', COUNT(*) FROM ProjectPrograms
UNION ALL SELECT 'Localities', COUNT(*) FROM Localities
UNION ALL SELECT 'Institutions', COUNT(*) FROM Institutions
UNION ALL SELECT 'Frameworks', COUNT(*) FROM Frameworks
UNION ALL SELECT 'EducationalPrograms', COUNT(*) FROM EducationalPrograms
UNION ALL SELECT 'Domains', COUNT(*) FROM Domains
UNION ALL SELECT 'Subjects', COUNT(*) FROM Subjects
UNION ALL SELECT 'DiscussionCodes', COUNT(*) FROM DiscussionCodes
UNION ALL SELECT 'SchoolClasses', COUNT(*) FROM SchoolClasses
UNION ALL SELECT 'GradeLevels', COUNT(*) FROM GradeLevels;";
    using var countCmd = new SqlCommand(countSql, connection) { CommandTimeout = 120 };
    using var countReader = countCmd.ExecuteReader();
    while (countReader.Read())
    {
        Console.WriteLine($"{countReader.GetString(0)}\t{countReader.GetInt32(1)}");
    }
    return;
}

if (args.FirstOrDefault()?.Equals("framework-samples", StringComparison.OrdinalIgnoreCase) == true)
{
    var sampleSql = @"
SELECT TOP (40) InstitutionSymbol, Description
FROM Frameworks
WHERE TRY_CONVERT(int, InstitutionSymbol) IS NULL
  AND InstitutionSymbol <> Description
ORDER BY InstitutionSymbol, Description;";
    using var sampleCmd = new SqlCommand(sampleSql, connection) { CommandTimeout = 60 };
    using var sampleReader = sampleCmd.ExecuteReader();
    while (sampleReader.Read())
    {
        Console.WriteLine($"{sampleReader.GetString(0)}\t{sampleReader.GetString(1)}");
    }
    return;
}

if (args.FirstOrDefault()?.Equals("project-programs", StringComparison.OrdinalIgnoreCase) == true)
{
    var sampleSql = @"
SELECT p.Id AS ProjectId, p.Description AS Project, pr.Id AS ProgramId, pr.Description AS Program, pr.IsActive
FROM ProjectPrograms pp
JOIN Projects p ON p.Id = pp.ProjectId
JOIN Programs pr ON pr.Id = pp.ProgramId
ORDER BY p.Description, pr.Description;";
    using var sampleCmd = new SqlCommand(sampleSql, connection) { CommandTimeout = 60 };
    using var sampleReader = sampleCmd.ExecuteReader();
    while (sampleReader.Read())
    {
        Console.WriteLine($"{sampleReader["ProjectId"]}\t{sampleReader["Project"]}\t{sampleReader["ProgramId"]}\t{sampleReader["Program"]}\tactive={sampleReader["IsActive"]}");
    }
    return;
}

if (args.FirstOrDefault()?.Equals("employee", StringComparison.OrdinalIgnoreCase) == true)
{
    var id = args.Skip(1).FirstOrDefault() ?? "";
    var sampleSql = @"
SELECT Id, EmployeeCode, IdNumber, FirstName, LastName, UserRoleId, StatusId
FROM Users
WHERE Id = TRY_CONVERT(int, @id) OR EmployeeCode = @id OR IdNumber = @id;";
    using var sampleCmd = new SqlCommand(sampleSql, connection) { CommandTimeout = 60 };
    sampleCmd.Parameters.AddWithValue("@id", id);
    using var sampleReader = sampleCmd.ExecuteReader();
    while (sampleReader.Read())
    {
        Console.WriteLine($"{sampleReader["Id"]}\tcode={sampleReader["EmployeeCode"]}\tidn={sampleReader["IdNumber"]}\t{sampleReader["FirstName"]} {sampleReader["LastName"]}\trole={sampleReader["UserRoleId"]}\tstatus={sampleReader["StatusId"]}");
    }
    return;
}

var sql = @"
SELECT
    r.session_id,
    r.status,
    r.command,
    r.wait_type,
    r.wait_time,
    r.blocking_session_id,
    r.cpu_time,
    r.total_elapsed_time,
    r.reads,
    r.writes,
    r.logical_reads,
    SUBSTRING(t.text, (r.statement_start_offset / 2) + 1,
        CASE r.statement_end_offset
            WHEN -1 THEN LEN(CONVERT(nvarchar(max), t.text))
            ELSE (r.statement_end_offset - r.statement_start_offset) / 2 + 1
        END) AS statement_text
FROM sys.dm_exec_requests r
CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) t
WHERE r.database_id = DB_ID('AxiomaReporting')
ORDER BY r.total_elapsed_time DESC;";

using var cmd = new SqlCommand(sql, connection) { CommandTimeout = 30 };
using var reader = cmd.ExecuteReader();
var rows = 0;
while (reader.Read())
{
    rows++;
    Console.WriteLine($"session={reader["session_id"]} status={reader["status"]} command={reader["command"]} wait={reader["wait_type"]} wait_ms={reader["wait_time"]} blocking={reader["blocking_session_id"]} elapsed_ms={reader["total_elapsed_time"]} cpu_ms={reader["cpu_time"]} reads={reader["reads"]} writes={reader["writes"]} logical_reads={reader["logical_reads"]}");
    Console.WriteLine((reader["statement_text"]?.ToString() ?? string.Empty).ReplaceLineEndings(" ").Trim());
}

Console.WriteLine($"requests={rows}");

Console.WriteLine("TRANSACTIONS");
var txSql = @"
SELECT
    s.session_id,
    s.status,
    s.login_name,
    s.host_name,
    s.program_name,
    s.open_transaction_count,
    c.client_net_address,
    dt.database_transaction_begin_time,
    dt.database_transaction_log_record_count
FROM sys.dm_exec_sessions s
LEFT JOIN sys.dm_exec_connections c ON c.session_id = s.session_id
LEFT JOIN sys.dm_tran_session_transactions st ON st.session_id = s.session_id
LEFT JOIN sys.dm_tran_database_transactions dt
    ON dt.transaction_id = st.transaction_id
    AND dt.database_id = DB_ID('AxiomaReporting')
WHERE s.is_user_process = 1
  AND (s.open_transaction_count > 0 OR dt.database_id IS NOT NULL OR s.program_name LIKE '%SqlClient%')
ORDER BY s.open_transaction_count DESC, s.session_id;";
using (var txCmd = new SqlCommand(txSql, connection) { CommandTimeout = 30 })
using (var txReader = txCmd.ExecuteReader())
{
    while (txReader.Read())
    {
        Console.WriteLine($"session={txReader["session_id"]} status={txReader["status"]} open_tx={txReader["open_transaction_count"]} login={txReader["login_name"]} host={txReader["host_name"]} program={txReader["program_name"]} begin={txReader["database_transaction_begin_time"]} log_records={txReader["database_transaction_log_record_count"]}");
    }
}

Console.WriteLine("LOCKS");
var lockSql = @"
SELECT request_session_id, resource_type, request_mode, request_status, COUNT(*) AS lock_count
FROM sys.dm_tran_locks
WHERE resource_database_id = DB_ID('AxiomaReporting')
GROUP BY request_session_id, resource_type, request_mode, request_status
ORDER BY request_session_id, resource_type, request_mode;";
using (var lockCmd = new SqlCommand(lockSql, connection) { CommandTimeout = 30 })
using (var lockReader = lockCmd.ExecuteReader())
{
    while (lockReader.Read())
    {
        Console.WriteLine($"session={lockReader["request_session_id"]} resource={lockReader["resource_type"]} mode={lockReader["request_mode"]} status={lockReader["request_status"]} count={lockReader["lock_count"]}");
    }
}

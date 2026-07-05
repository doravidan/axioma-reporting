using Microsoft.Data.SqlClient;

var idNumber = args.FirstOrDefault(a => a.StartsWith("--id=", StringComparison.OrdinalIgnoreCase))?.Substring("--id=".Length) ?? "admin";
var password = args.FirstOrDefault(a => a.StartsWith("--password=", StringComparison.OrdinalIgnoreCase))?.Substring("--password=".Length) ?? "admin1234";
var connectionString = args.FirstOrDefault(a => a.StartsWith("--connection=", StringComparison.OrdinalIgnoreCase))?.Substring("--connection=".Length)
    ?? @"Server=.\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

var hash = BCrypt.Net.BCrypt.HashPassword(password, 12);

await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();
await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();

await using var command = connection.CreateCommand();
command.Transaction = transaction;
command.CommandText = @"
UPDATE dbo.Users
SET PasswordHash = @hash,
    MustChangePassword = 0,
    FailedLoginAttempts = 0,
    StatusId = 1,
    LastPasswordChange = SYSUTCDATETIME(),
    UpdatedAt = SYSUTCDATETIME()
OUTPUT inserted.Id, inserted.IdNumber, inserted.FirstName, inserted.LastName, inserted.UserRoleId, inserted.StatusId
WHERE IdNumber = @idNumber AND UserRoleId = 1;";
command.Parameters.AddWithValue("@hash", hash);
command.Parameters.AddWithValue("@idNumber", idNumber);

await using var reader = await command.ExecuteReaderAsync();
if (!await reader.ReadAsync())
{
    await transaction.RollbackAsync();
    Console.Error.WriteLine($"No system admin user found with IdNumber '{idNumber}'.");
    Environment.ExitCode = 2;
    return;
}

Console.WriteLine($"Id={reader.GetInt32(0)}");
Console.WriteLine($"IdNumber={reader.GetString(1)}");
Console.WriteLine($"Name={reader.GetString(2)} {reader.GetString(3)}");
Console.WriteLine($"UserRoleId={reader.GetInt32(4)}");
Console.WriteLine($"StatusId={reader.GetInt32(5)}");

await reader.CloseAsync();
await transaction.CommitAsync();
Console.WriteLine("Password reset committed.");

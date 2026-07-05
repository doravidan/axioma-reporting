string inputPath = GetArg("--input=") ?? throw new InvalidOperationException("Missing --input=<path>");
bool verify = args.Any(arg => string.Equals(arg, "--verify", StringComparison.OrdinalIgnoreCase));
string? outputPath = GetArg("--output=");

var rows = File.ReadAllLines(inputPath)
	.Select(line => line.Trim())
	.Where(line => !string.IsNullOrWhiteSpace(line))
	.Select(ParseRow)
	.ToList();

if (verify)
{
	int valid = rows.Count(row => !string.IsNullOrWhiteSpace(row.PasswordHash) && BCrypt.Net.BCrypt.Verify(row.IdNumber, row.PasswordHash));
	Console.WriteLine($"Verified hashes: {valid}/{rows.Count}");
	if (valid != rows.Count)
	{
		Environment.ExitCode = 1;
	}
	return;
}

if (string.IsNullOrWhiteSpace(outputPath))
{
	throw new InvalidOperationException("Missing --output=<path>");
}

await using var writer = new StreamWriter(outputPath, false);
await writer.WriteLineAsync("SET XACT_ABORT ON;");
await writer.WriteLineAsync("BEGIN TRANSACTION;");
await writer.WriteLineAsync("DECLARE @Now datetime2 = SYSUTCDATETIME();");

foreach (var row in rows)
{
	string hash = BCrypt.Net.BCrypt.HashPassword(row.IdNumber, 12);
	await writer.WriteLineAsync(
		$"UPDATE dbo.Users SET PasswordHash = N'{SqlEscape(hash)}', MustChangePassword = 1, LastPasswordChange = @Now, UpdatedAt = @Now WHERE Id = {row.Id};");
}

await writer.WriteLineAsync("COMMIT TRANSACTION;");
Console.WriteLine($"Generated reset SQL for {rows.Count} users: {outputPath}");

string? GetArg(string prefix)
{
	return args.FirstOrDefault(arg => arg.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))?.Substring(prefix.Length);
}

static UserRow ParseRow(string line)
{
	string[] parts = line.Split('|');
	if (parts.Length < 2 || !int.TryParse(parts[0].Trim(), out int id))
	{
		throw new InvalidOperationException($"Invalid input row: {line}");
	}

	return new UserRow(id, parts[1].Trim(), parts.Length >= 3 ? parts[2].Trim() : null);
}

static string SqlEscape(string value)
{
	return value.Replace("'", "''");
}

internal sealed record UserRow(int Id, string IdNumber, string? PasswordHash);

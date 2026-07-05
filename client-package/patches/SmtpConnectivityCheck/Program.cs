using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=.\\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;TrustServerCertificate=True;";

await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();

await using var command = new SqlCommand(
	"SELECT TOP 1 SmtpServer, Port, Username, Password, FromAddress, FromName, UseSsl " +
	"FROM dbo.EmailServerSettings " +
	"ORDER BY Id DESC", connection);

await using var reader = await command.ExecuteReaderAsync();
if (!await reader.ReadAsync())
{
	Console.WriteLine("FAIL: no SMTP settings configured");
	Environment.ExitCode = 2;
	return;
}

string host = reader.GetString(0);
int port = reader.GetInt32(1);
string username = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
string password = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
string fromAddress = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
string fromName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
bool useSsl = reader.GetBoolean(6);

Console.WriteLine($"SMTP settings: host={host}, port={port}, username-present={!string.IsNullOrWhiteSpace(username)}, password-present={!string.IsNullOrWhiteSpace(password)}, from={fromAddress}, from-name-present={!string.IsNullOrWhiteSpace(fromName)}, useSsl={useSsl}");

using var client = new SmtpClient();
client.Timeout = 20000;

SecureSocketOptions options = useSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
await client.ConnectAsync(host, port, options);
Console.WriteLine($"Connected: IsConnected={client.IsConnected}, IsSecure={client.IsSecure}, AuthMechanisms={string.Join(",", client.AuthenticationMechanisms)}");

if (!string.IsNullOrWhiteSpace(username))
{
	await client.AuthenticateAsync(username, password);
	Console.WriteLine($"Authenticated: IsAuthenticated={client.IsAuthenticated}");
}
else
{
	Console.WriteLine("Authenticated: skipped because username is empty");
}

await client.NoOpAsync();
Console.WriteLine("NOOP: OK");

await client.DisconnectAsync(quit: true);
Console.WriteLine("Disconnected: OK");

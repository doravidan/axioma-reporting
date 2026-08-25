namespace AxiomaReporting.Web.Security;

public static class AutomatedTestDatabaseGuard
{
  /// <summary>
  /// Automated tests must never reach SQL Server. Requiring the in-memory provider
  /// is intentionally stricter than trying to recognize every possible production
  /// server alias or connection-string spelling.
  /// </summary>
  public static void EnsureSafe(string environmentName, bool useInMemoryDatabase, string? connectionString)
  {
    if (!string.Equals(environmentName, "Testing", StringComparison.OrdinalIgnoreCase)) return;
    if (useInMemoryDatabase) return;

    var target = DescribeTarget(connectionString);
    throw new InvalidOperationException(
      $"Automated tests were blocked before database access: environment Testing attempted to use SQL Server ({target}). " +
      "Set AXIOMA_TEST_INMEMORY=true and use synthetic test data.");
  }

  internal static string DescribeTarget(string? connectionString)
  {
    if (string.IsNullOrWhiteSpace(connectionString)) return "missing connection string";
    var parts = connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries)
      .Select(part => part.Trim())
      .Where(part => part.StartsWith("Server=", StringComparison.OrdinalIgnoreCase) ||
                     part.StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase) ||
                     part.StartsWith("Database=", StringComparison.OrdinalIgnoreCase) ||
                     part.StartsWith("Initial Catalog=", StringComparison.OrdinalIgnoreCase));
    return string.Join(";", parts);
  }
}

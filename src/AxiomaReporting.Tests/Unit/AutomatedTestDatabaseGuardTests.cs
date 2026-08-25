using AxiomaReporting.Web.Security;
using FluentAssertions;

namespace AxiomaReporting.Tests.Unit;

public class AutomatedTestDatabaseGuardTests
{
  [Fact]
  public void TestingEnvironment_WithSqlConnection_FailsBeforeDatabaseAccess()
  {
    var action = () => AutomatedTestDatabaseGuard.EnsureSafe(
      "Testing", false, "Server=.\\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True");

    action.Should().Throw<InvalidOperationException>().WithMessage("*blocked before database access*");
  }

  [Fact]
  public void TestingEnvironment_WithInMemoryProvider_IsAllowed() =>
    AutomatedTestDatabaseGuard.EnsureSafe("Testing", true, "production-looking-value");
}

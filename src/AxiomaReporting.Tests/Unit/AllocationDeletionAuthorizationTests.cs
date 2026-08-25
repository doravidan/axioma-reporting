using AxiomaReporting.Web.Authorization;
using AxiomaReporting.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AxiomaReporting.Tests.Unit;

public class AllocationDeletionAuthorizationTests
{
  [Fact]
  public void DeleteAllocation_IsRestrictedToSystemAdminAndRequiresAntiforgeryToken()
  {
    var action = typeof(EmployeeController).GetMethod(nameof(EmployeeController.DeleteAllocation));

    action.Should().NotBeNull();
    action!.GetCustomAttributes(typeof(HttpPostAttribute), inherit: true).Should().ContainSingle();
    action.GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), inherit: true).Should().ContainSingle();
    action.GetCustomAttributes(typeof(AuthorizeAttribute), inherit: true)
      .Cast<AuthorizeAttribute>()
      .Should().ContainSingle(attribute => attribute.Policy == PolicyNames.AdminOnly);
  }
}

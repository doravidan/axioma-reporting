using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace AxiomaReporting.Tests.Unit;

public class AllocationFormPerformanceTests
{
  [Fact]
  public void AllocationPosts_AllowLargeScopeSetsWithoutChangingTheGlobalFormLimit()
  {
    var postActions = typeof(EmployeeController).GetMethods()
      .Where(method => method.GetParameters().Any(parameter => parameter.ParameterType == typeof(AllocationDto)))
      .Where(method => method.GetCustomAttributes(typeof(HttpPostAttribute), inherit: true).Any())
      .ToList();

    postActions.Select(method => method.Name)
      .Should().BeEquivalentTo(nameof(EmployeeController.CreateAllocation), nameof(EmployeeController.EditAllocation));
    postActions.Should().OnlyContain(method =>
      method.GetCustomAttributes(typeof(RequestFormLimitsAttribute), true)
        .Cast<RequestFormLimitsAttribute>()
        .Single()
        .ValueCountLimit >= 10_000);
  }

  [Fact]
  public void AllocationScopeLoader_UsesOneBulkChoicesRebuildAndCachesProgramRequests()
  {
    var root = FindSolutionRoot();
    var loader = File.ReadAllText(Path.Combine(
      root, "src", "AxiomaReporting.Web", "wwwroot", "js", "allocation-scope-loader.js"));
    var view = File.ReadAllText(Path.Combine(
      root, "src", "AxiomaReporting.Web", "Views", "Employee", "AllocationForm.cshtml"));

    loader.Should().Contain("choicesInstance.setChoices");
    loader.Should().Contain("choicesInstance.clearStore");
    loader.Should().NotContain("choicesInstance.setChoiceByValue(");
    view.Should().Contain("const programValuesCache = new Map()");
    view.Should().Contain("await nextAnimationFrame()");
    view.Should().Contain("addSelectionsInBatch");
  }

  private static string FindSolutionRoot()
  {
    var directory = new DirectoryInfo(AppContext.BaseDirectory);
    while (directory != null)
    {
      if (directory.GetFiles("*.sln").Length > 0) return directory.FullName;
      directory = directory.Parent;
    }

    throw new DirectoryNotFoundException("Could not locate the solution root.");
  }
}

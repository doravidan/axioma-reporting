using System.Text;
using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

public class AllocationScopePerformancePlaywrightTests
{
  [Fact]
  public async Task BulkLoader_AppliesMoreThanOneThousandDefaultsWithoutPerItemRendering()
  {
    var root = FindSolutionRoot();
    var choicesPath = Path.Combine(
      root, "src", "AxiomaReporting.Web", "wwwroot", "lib", "choices.js", "choices.min.js");
    var loaderPath = Path.Combine(
      root, "src", "AxiomaReporting.Web", "wwwroot", "js", "allocation-scope-loader.js");
    var specifications = new[]
    {
      new ScopeSpecification("frameworks", 700, 635),
      new ScopeSpecification("localities", 200, 173),
      new ScopeSpecification("subjects", 200, 172),
      new ScopeSpecification("domains", 10, 1),
      new ScopeSpecification("educational-programs", 10, 2),
      new ScopeSpecification("discussion-codes", 20, 12),
      new ScopeSpecification("grade-levels", 20, 13),
      new ScopeSpecification("classes", 20, 15),
      new ScopeSpecification("national-localities", 20, 16)
    };

    var html = new StringBuilder("<!doctype html><html><body>");
    foreach (var specification in specifications)
    {
      html.Append($"<select id=\"{specification.Id}\" multiple>");
      for (var id = 1; id <= specification.OptionCount; id++)
        html.Append($"<option value=\"{id}\">Value {id}</option>");
      html.Append($"<option value=\"manual-{specification.Id}\" selected>Manual</option></select>");
    }
    html.Append("</body></html>");

    using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    {
      Headless = true
    });
    var page = await browser.NewPageAsync();
    await page.SetContentAsync(html.ToString());
    await page.AddScriptTagAsync(new PageAddScriptTagOptions { Path = choicesPath });
    await page.AddScriptTagAsync(new PageAddScriptTagOptions { Path = loaderPath });

    var result = await page.EvaluateAsync<PerformanceResult>(
      @"specifications => {
        const widgets = specifications.map(specification => {
          const element = document.getElementById(specification.id);
          const catalog = window.AxiomaAllocationScope.createCatalog(element);
          const instance = new window.Choices(element, {
            removeItemButton: true,
            shouldSort: false,
            itemSelectText: ''
          });
          const originalSetChoices = instance.setChoices.bind(instance);
          let setChoicesCalls = 0;
          instance.setChoices = (...args) => {
            setChoicesCalls += 1;
            return originalSetChoices(...args);
          };
          return { specification, element, catalog, instance, getCalls: () => setChoicesCalls };
        });

        const startedAt = performance.now();
        let added = 0;
        for (const widget of widgets) {
          const ids = Array.from(
            { length: widget.specification.selectedCount },
            (_, index) => String(index + 1));
          added += window.AxiomaAllocationScope.batchAddSelections(
            widget.element, ids, widget.instance, widget.catalog).added;
        }
        const durationMs = performance.now() - startedAt;

        return {
          durationMs,
          added,
          selected: widgets.reduce(
            (total, widget) => total + widget.instance.getValue(true).length, 0),
          setChoicesCalls: widgets.reduce((total, widget) => total + widget.getCalls(), 0)
        };
      }",
      specifications.Select(specification => new
      {
        id = specification.Id,
        optionCount = specification.OptionCount,
        selectedCount = specification.SelectedCount
      }).ToArray());

    result.Added.Should().Be(1_039);
    result.Selected.Should().Be(1_048, "all nine manual selections must be preserved");
    result.SetChoicesCalls.Should().Be(9, "each widget must rebuild once, not once per selected value");
    Console.WriteLine(
      $"Allocation scope benchmark: {result.DurationMs:0} ms, " +
      $"{result.Added} defaults, {result.SetChoicesCalls} bulk rebuilds.");
    result.DurationMs.Should().BeLessThan(5_000,
      "applying the production-sized scope set must remain interactive");
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

  private sealed record ScopeSpecification(string Id, int OptionCount, int SelectedCount);

  private sealed class PerformanceResult
  {
    public double DurationMs { get; set; }
    public int Added { get; set; }
    public int Selected { get; set; }
    public int SetChoicesCalls { get; set; }
  }
}

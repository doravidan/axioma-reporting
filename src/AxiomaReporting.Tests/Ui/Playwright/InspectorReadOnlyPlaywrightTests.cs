using FluentAssertions;
using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

[Collection("Playwright")]
public class InspectorReadOnlyPlaywrightTests : PlaywrightTestBase
{
  private static string InspectorUsername =>
    Environment.GetEnvironmentVariable("AXIOMA_TEST_INSPECTOR_USERNAME") ?? "inspector";

  private static string InspectorPassword =>
    Environment.GetEnvironmentVariable("AXIOMA_TEST_INSPECTOR_PASSWORD") ?? "InspectorTest123!";

  [Fact]
  public async Task AdminCanPersistScope_AndInspectorReceivesReadOnlyDashboard()
  {
    var consoleErrors = new List<string>();
    var serverFailures = new List<string>();
    Page.Console += (_, message) =>
    {
      if (message.Type == "error") consoleErrors.Add(message.Text);
    };
    Page.Response += (_, response) =>
    {
      if (response.Status >= 500) serverFailures.Add($"{response.Status} {response.Url}");
    };

    await LoginAsync();
    var assignmentPage = await Page.GotoAsync("/Admin/InspectorAssignments");
    assignmentPage!.Ok.Should().BeTrue();

    var inspectorSelect = Page.Locator("select[name='inspectorUserId']");
    var inspectorValue = await inspectorSelect.EvaluateAsync<string?>(
      "(select, username) => Array.from(select.options).find(option => option.textContent.includes(username))?.value ?? null",
      InspectorUsername);
    inspectorValue.Should().NotBeNullOrWhiteSpace();
    await inspectorSelect.SelectOptionAsync(inspectorValue!);
    await Page.Locator("select[name='programId']").SelectOptionAsync(
      new SelectOptionValue { Label = "תוכנית א" });
    await Page.Locator("form[action*='CreateInspectorAssignment'] button[type='submit']").ClickAsync();
    await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

    var savedPageText = await GetPageTextAsync();
    savedPageText.Should().Contain("שיוך המפקח נוסף");
    savedPageText.Should().Contain("תוכנית א");
    (await inspectorSelect.InputValueAsync()).Should().Be(inspectorValue,
      because: "the selected inspector must still be selected after redirect and reload");

    await Context.ClearCookiesAsync();
    await LoginAsync(InspectorUsername, InspectorPassword);
    var dashboardResponse = await Page.GotoAsync("/Dashboard?show=1");
    dashboardResponse!.Ok.Should().BeTrue();
    (await Page.Locator("html").GetAttributeAsync("dir")).Should().Be("rtl");
    (await Page.Locator("a:has-text('צפה')").CountAsync()).Should().BeGreaterThan(0);
    (await Page.Locator(".dashboard-report-cb, .dashboard-single-delete, #bulkDeleteReportsBtn, #bulkApproveBtn").CountAsync())
      .Should().Be(0, because: "InspectorView is read-only");

    await Page.Locator("a:has-text('צפה')").First.ClickAsync();
    await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    (await Page.Locator("button:has-text('הוסף שורה'), button[aria-label^='ערוך שורה'], button[aria-label^='מחק שורה'], form[action*='UploadExcel']").CountAsync())
      .Should().Be(0, because: "report details must remain read-only for InspectorView");

    var artifactDirectory = ArtifactDirectory();
    Directory.CreateDirectory(artifactDirectory);
    await Page.SetViewportSizeAsync(1440, 1000);
    await Page.ScreenshotAsync(new PageScreenshotOptions
    {
      Path = Path.Combine(artifactDirectory, "inspector-read-only.png"),
      FullPage = true
    });

    consoleErrors.Should().BeEmpty();
    serverFailures.Should().BeEmpty();
  }

  private static string ArtifactDirectory()
  {
    var directory = new DirectoryInfo(AppContext.BaseDirectory);
    while (directory != null && directory.GetFiles("*.sln").Length == 0)
      directory = directory.Parent;
    if (directory == null) throw new DirectoryNotFoundException("Solution root was not found.");
    return Path.Combine(directory.FullName, "artifacts", "inspector-multi-allocation-20260810");
  }
}

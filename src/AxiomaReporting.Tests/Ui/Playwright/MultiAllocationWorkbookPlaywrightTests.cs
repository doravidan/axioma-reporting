using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

[Collection("Playwright")]
public class MultiAllocationWorkbookPlaywrightTests : PlaywrightTestBase
{
  [Fact]
  public async Task Employee_SelectsAllocation_AndUploadsBothClientWorkbooks()
  {
    WebServerFixture.ClientWorkbookFiles.Should().HaveCount(2,
      because: "the two client workbooks must be discoverable in an attachment or test-data root");

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
    Page.Dialog += async (_, dialog) => await dialog.AcceptAsync();
    Page.SetDefaultTimeout(60_000);

    await LoginAsync("111111111", "Password123");

    var cases = new[]
    {
      new { Program = "תוכנית שמיים", ExpectedRows = 54 },
      new { Program = "כיתות שח\"ר", ExpectedRows = 58 }
    };

    foreach (var testCase in cases)
    {
      var selectorResponse = await Page.GotoAsync("/Report?excelUpload=true");
      selectorResponse!.Ok.Should().BeTrue();
      (await Page.Locator("html").GetAttributeAsync("dir")).Should().Be("rtl");

      var allocationOption = Page.Locator("a.list-group-item")
        .Filter(new LocatorFilterOptions { HasText = testCase.Program });
      (await allocationOption.CountAsync()).Should().Be(1,
        because: $"the employee must explicitly select the allocation for {testCase.Program}");

      var href = await allocationOption.GetAttributeAsync("href");
      var allocationMatch = Regex.Match(href ?? string.Empty, @"(?:\?|&)allocationId=(\d+)");
      allocationMatch.Success.Should().BeTrue();
      var expectedAllocationId = allocationMatch.Groups[1].Value;

      await allocationOption.ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      (await GetPageTextAsync()).Should().Contain(testCase.Program);

      var uploadForm = Page.Locator("form[action*='UploadExcel']");
      (await uploadForm.CountAsync()).Should().Be(1,
        because: "AllowExcelUpload must be evaluated from the selected allocation");
      (await uploadForm.Locator("input[name='allocationId']").InputValueAsync())
        .Should().Be(expectedAllocationId);

      var workbookPath = WebServerFixture.ClientWorkbookFiles[testCase.Program];
      await uploadForm.Locator("input[type='file'][name='file']").SetInputFilesAsync(workbookPath);
      await uploadForm.Locator("button[type='submit']").ClickAsync();
      await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

      var pageText = await GetPageTextAsync();
      pageText.Should().Contain($"יובאו {testCase.ExpectedRows} שורות מאקסל");
      (await Page.Locator("#reportTable tbody tr[data-row-id]").CountAsync())
        .Should().Be(testCase.ExpectedRows);
      (await Page.Locator("input[name='allocationId']").Last.InputValueAsync())
        .Should().Be(expectedAllocationId,
          because: "the imported rows must remain in the allocation chosen by the employee");
    }

    var artifactDirectory = ArtifactDirectory();
    Directory.CreateDirectory(artifactDirectory);
    await Page.SetViewportSizeAsync(1440, 1000);
    await Page.ScreenshotAsync(new PageScreenshotOptions
    {
      Path = Path.Combine(artifactDirectory, "client-workbooks-upload.png"),
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

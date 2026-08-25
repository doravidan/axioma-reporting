using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

[Collection("Playwright")]
public class DashboardEnhancementsPlaywrightTests : PlaywrightTestBase
{
  [Fact]
  public async Task Dashboard_FilterSelectionPaginationExportsAndRtl_WorkEndToEnd()
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
    var response = await Page.GotoAsync("/Dashboard?show=1&PageSize=10");
    response!.Ok.Should().BeTrue();
    (await Page.Locator("body").InnerTextAsync()).Trim().Should().NotBeEmpty();
    (await Page.Locator("html").GetAttributeAsync("dir")).Should().Be("rtl");
    (await Page.Locator("[data-nextjs-dialog], .vite-error-overlay, #webpack-dev-server-client-overlay").CountAsync())
      .Should().Be(0);

    var program = Page.Locator("#filterForm select[name='ProgramId']");
    (await program.CountAsync()).Should().Be(1);
    var programId = await program.EvaluateAsync<string?>(
      "(select, label) => Array.from(select.options).find(option => option.textContent.trim() === label)?.value ?? null",
      "תוכנית א");
    programId.Should().NotBeNullOrEmpty();
    await program.SelectOptionAsync(programId!);
    await Page.Locator("#filterForm button[type='submit']").ClickAsync();
    await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    Page.Url.Should().Contain($"ProgramId={programId}");
    (await program.InputValueAsync()).Should().Be(programId);
    (await Page.Locator("#filterForm a:has-text('נקה')").CountAsync()).Should().Be(1);

    var nextLink = Page.Locator("nav[aria-label='דפדוף תוצאות'] a:has-text('הבא')");
    (await nextLink.CountAsync()).Should().Be(1, because: "the twelve seeded rows must be paginated when PageSize=10");
    (await Page.Locator("nav[aria-label='דפדוף תוצאות'] a:has-text('הראשון')").CountAsync()).Should().Be(1);
    (await Page.Locator("nav[aria-label='דפדוף תוצאות'] a:has-text('האחרון')").CountAsync()).Should().Be(1);
    (await Page.Locator("#bulkDeleteReportsBtn").CountAsync()).Should().Be(1);
    (await Page.Locator(".dashboard-single-delete").CountAsync()).Should().BeGreaterThan(0,
      because: "an administrator must have a clear single-report deletion action");

    var reportCheckboxes = Page.Locator(".dashboard-report-cb");
    (await reportCheckboxes.CountAsync()).Should().BeGreaterThan(1,
      because: "every report detail record on the page must expose its own checkbox");
    var selectionKeys = await reportCheckboxes.EvaluateAllAsync<string[]>(
      "boxes => boxes.map(box => box.dataset.selectionKey)");
    selectionKeys.Should().OnlyHaveUniqueItems(
      because: "each displayed record must have an independent selection key");
    await reportCheckboxes.First.CheckAsync();
    (await Page.Locator(".dashboard-report-cb:checked").CountAsync()).Should().Be(1,
      because: "selecting one report must not visually select other reports or detail rows");
    await reportCheckboxes.First.UncheckAsync();

    await Page.Locator("#selectCurrentPageBtn").ClickAsync();
    (await Page.Locator("#dashboardSelectedCount").InnerTextAsync()).Trim().Should().Be("1");
    (await Page.Locator(".dashboard-report-cb").First.IsCheckedAsync()).Should().BeTrue();

    await nextLink.ClickAsync();
    await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    (await Page.Locator("#dashboardSelectedCount").InnerTextAsync()).Trim().Should().Be("1",
      because: "selection is retained between pages for the same filter");
    (await Page.Locator(".dashboard-report-cb").First.IsCheckedAsync()).Should().BeFalse(
      because: "selection is independent per displayed record and must not spill into the next page");

    await Page.Locator("#clearDashboardSelectionBtn").ClickAsync();
    (await Page.Locator("#dashboardSelectedCount").InnerTextAsync()).Trim().Should().Be("0");
    await Page.Locator("#selectCurrentPageBtn").ClickAsync();
    string? confirmationText = null;
    Page.Dialog += async (_, dialog) =>
    {
      confirmationText = dialog.Message;
      await dialog.DismissAsync();
    };
    await Page.Locator("#bulkDeleteReportsBtn").ClickAsync();
    confirmationText.Should().Contain("1");
    (await Page.Locator(".dashboard-report-cb").CountAsync()).Should().BeGreaterThan(0,
      because: "cancelling the destructive confirmation must leave the report in place");

    var artifactDirectory = ArtifactDirectory();
    Directory.CreateDirectory(artifactDirectory);
    await Page.SetViewportSizeAsync(1440, 1000);
    await Page.ScreenshotAsync(new PageScreenshotOptions
    {
      Path = Path.Combine(artifactDirectory, "dashboard-desktop.png"), FullPage = true
    });

    var reportDownload = await Page.RunAndWaitForDownloadAsync(async () =>
      await Page.Locator("a[href*='ExportExcel']").First.ClickAsync());
    reportDownload.SuggestedFilename.Should().StartWith("reports_").And.EndWith(".xlsx");
    var reportPath = Path.Combine(artifactDirectory, "reports_filtered_example.xlsx");
    await reportDownload.SaveAsAsync(reportPath);
    using (var workbook = new XLWorkbook(reportPath))
    {
      var sheet = workbook.Worksheet(1);
      sheet.Cell(1, 10).GetString().Should().Be("מסגרת חינוכית");
      var usedRows = sheet.RowsUsed().Skip(1).ToList();
      usedRows.Count.Should().BeGreaterThanOrEqualTo(12,
        because: "export includes all filtered results rather than the current ten-row page");
      usedRows.Select(row => row.Cell(10).GetString())
        .Should().Contain("ירושלים — 0872903 — הילה ישיבה פרי הארץ");
    }

    await Page.SetViewportSizeAsync(390, 844);
    await Page.GotoAsync("/Dashboard?show=1&PageSize=10");
    await Page.ScreenshotAsync(new PageScreenshotOptions
    {
      Path = Path.Combine(artifactDirectory, "dashboard-mobile.png"), FullPage = true
    });
    var viewportOverflow = await Page.EvaluateAsync<bool>(
      "() => document.documentElement.scrollWidth > window.innerWidth + 1");
    viewportOverflow.Should().BeFalse(because: "wide tables and pagination must stay inside responsive containers");

    await Page.SetViewportSizeAsync(1440, 1000);
    await Page.GotoAsync("/Admin/Institutions?name=הילה");
    var institutionDownload = await Page.RunAndWaitForDownloadAsync(async () =>
      await Page.Locator("#exportInstitutionsBtn").ClickAsync());
    institutionDownload.SuggestedFilename.Should().Be($"institutions_{DateTime.Today:yyyy-MM-dd}.xlsx");
    var institutionPath = Path.Combine(artifactDirectory, "institutions_filtered_example.xlsx");
    await institutionDownload.SaveAsAsync(institutionPath);
    using (var workbook = new XLWorkbook(institutionPath))
    {
      var sheet = workbook.Worksheet(1);
      sheet.Cell(1, 1).GetString().Should().Be("שם המוסד");
      sheet.RowsUsed().Skip(1).Should().ContainSingle();
    }
    await Page.ScreenshotAsync(new PageScreenshotOptions
    {
      Path = Path.Combine(artifactDirectory, "institutions-export.png"), FullPage = true
    });

    await Context.ClearCookiesAsync();
    await LoginAsync("111111111", "Password123");
    await Page.GotoAsync("/Dashboard");
    (await Page.Locator("#bulkDeleteReportsBtn, #bulkSubmitBtn, #bulkApproveBtn").CountAsync())
      .Should().Be(0, because: "a reporting employee must not receive bulk controls");

    consoleErrors.Should().BeEmpty();
    serverFailures.Should().BeEmpty();
  }

  private static string ArtifactDirectory()
  {
    var directory = new DirectoryInfo(AppContext.BaseDirectory);
    while (directory != null && directory.GetFiles("*.sln").Length == 0)
      directory = directory.Parent;
    if (directory == null) throw new DirectoryNotFoundException("Solution root was not found.");
    return Path.Combine(directory.FullName, "artifacts", "client-feedback-20260805");
  }
}

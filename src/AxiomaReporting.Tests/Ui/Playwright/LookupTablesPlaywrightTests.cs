using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// E2E tests for the lookup table screens: page rendering, Hebrew content,
/// add-item form, delete button presence, and special table behaviour.
/// </summary>
[Collection("Playwright")]
public class LookupTablesPlaywrightTests : PlaywrightTestBase
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await LoginAsync();
    }

    // ─── Generic table rendering ──────────────────────────────────────────────

    [Theory]
    [InlineData("districts")]
    [InlineData("localities")]
    [InlineData("sectors")]
    [InlineData("programs")]
    [InlineData("educationalprograms")]
    [InlineData("subjects")]
    [InlineData("domains")]
    public async Task LookupTable_ReturnsOkStatus(string slug)
    {
        var response = await Page.GotoAsync($"/Lookup/{slug}");
        response!.Status.Should().Be(200);
    }

    [Theory]
    [InlineData("districts")]
    [InlineData("localities")]
    [InlineData("sectors")]
    public async Task LookupTable_PageContainsHebrewContent(string slug)
    {
        await Page.GotoAsync($"/Lookup/{slug}");
        var body = await GetPageTextAsync();
        body.Should().MatchRegex("[\u0590-\u05ff]");
    }

    [Theory]
    [InlineData("districts")]
    [InlineData("sectors")]
    [InlineData("programs")]
    public async Task LookupTable_HasAddButton(string tableName)
    {
        await Page.GotoAsync($"/Lookup/{tableName}");
        // Add button opens the #addModal
        var addButton = await Page.Locator("[data-bs-target='#addModal']").CountAsync();
        addButton.Should().BeGreaterThan(0,
            because: $"lookup table '{tableName}' must have an add button per spec");
    }

    [Theory]
    [InlineData("districts")]
    [InlineData("localities")]
    public async Task LookupTable_WhenNotEmpty_HasDeleteButton(string tableName)
    {
        await Page.GotoAsync($"/Lookup/{tableName}");
        // Delete button is only rendered when rows exist: button[title='מחיקה'] with 🗑️
        var rows = await Page.Locator("table tbody tr").CountAsync();
        if (rows == 0)
            return; // no data seeded in this environment — test not applicable

        var deleteButtons = await Page.Locator("button[title='מחיקה']").CountAsync();
        deleteButtons.Should().BeGreaterThan(0,
            because: $"table '{tableName}' must have trash-can delete icons per spec section 15.1");
    }

    // ─── Reporting Months (Admin route) ──────────────────────────────────────

    [Fact]
    public async Task ReportingMonths_AdminPage_RendersOk()
    {
        // Reporting months are managed at /Admin/ReportingMonths, not /Lookup/reportingmonths
        var response = await Page.GotoAsync("/Admin/ReportingMonths");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task ReportingMonthTable_HasIsActiveColumn()
    {
        await Page.GotoAsync("/Admin/ReportingMonths");
        var body = await GetPageTextAsync();
        body.Should().MatchRegex(new Regex("פעיל|פתוח|Active", RegexOptions.IgnoreCase));
    }

    [Fact]
    public async Task ReportingMonthTable_HasLastReportingDateColumn()
    {
        await Page.GotoAsync("/Admin/ReportingMonths");
        var body = await GetPageTextAsync();
        body.Should().MatchRegex(new Regex("תאריך|אחרון|הגשה", RegexOptions.IgnoreCase));
    }

    // ─── Frameworks (Admin route) ─────────────────────────────────────────────

    [Fact]
    public async Task FrameworkTable_RendersOk()
    {
        // Frameworks are managed at /Admin/Frameworks
        var response = await Page.GotoAsync("/Admin/Frameworks");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task FrameworkTable_HasInstitutionSymbolColumn()
    {
        await Page.GotoAsync("/Admin/Frameworks");
        var body = await GetPageTextAsync();
        body.Should().MatchRegex(new Regex("סמל|institution|מסגרת", RegexOptions.IgnoreCase));
    }

    [Fact]
    public async Task InstitutionDuplicateSymbol_IsRejectedWithHebrewMessage()
    {
        await Page.GotoAsync("/Admin/Institutions");
        await Page.Locator("[data-bs-target='#addModal']").First.ClickAsync();
        var modal = Page.Locator("#addModal");
        await modal.Locator("input[name='name']").FillAsync("מוסד כפול E2E");
        await modal.Locator("input[name='institutionSymbol']").FillAsync("872903");
        var stage = modal.Locator("select[name='educationalStageId']");
        var stageValue = await stage.EvaluateAsync<string?>(
            "select => Array.from(select.options).find(option => option.value)?.value ?? null");
        if (!string.IsNullOrEmpty(stageValue)) await stage.SelectOptionAsync(stageValue);
        await modal.Locator("button[type='submit']").ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        (await GetPageTextAsync()).Should().Contain(
            "לא ניתן ליצור את המוסד. מספר המוסד כבר קיים במערכת.");
    }

    [Fact]
    public async Task FrameworkDuplicateSymbol_IsRejectedWithHebrewMessage()
    {
        await Page.GotoAsync("/Admin/Frameworks");
        await Page.Locator("[data-bs-target='#addModal']").First.ClickAsync();
        var modal = Page.Locator("#addModal");
        await modal.Locator("input[name='description']").FillAsync("מסגרת כפולה E2E");
        await modal.Locator("input[name='institutionSymbol']").FillAsync("0872903");
        await modal.Locator("button[type='submit']").ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        (await GetPageTextAsync()).Should().Contain("סמל המוסד כבר קיים במערכת");
    }

    // ─── System tables are protected ─────────────────────────────────────────

    [Fact]
    public async Task ReportStatuses_GenericLookupRoute_ReturnsNon200()
    {
        var response = await Page.GotoAsync("/Lookup/reportstatuses");
        response!.Status.Should().NotBe(200);
    }

    [Fact]
    public async Task UserRoles_GenericLookupRoute_ReturnsNon200()
    {
        var response = await Page.GotoAsync("/Lookup/userroles");
        response!.Status.Should().NotBe(200);
    }
}

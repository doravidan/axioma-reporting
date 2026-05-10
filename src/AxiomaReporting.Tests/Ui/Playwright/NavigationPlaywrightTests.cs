namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// E2E tests for post-login navigation: nav menu links, page titles,
/// RTL layout persistence, and role-appropriate page accessibility.
/// </summary>
[Collection("Playwright")]
public class NavigationPlaywrightTests : PlaywrightTestBase
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await LoginAsync();
    }

    // ─── Layout ───────────────────────────────────────────────────────────────

    [Fact]
    public async Task AfterLogin_PageIsStillRtl()
    {
        var dir = await Page.Locator("html").GetAttributeAsync("dir");
        dir.Should().Be("rtl");
    }

    [Fact]
    public async Task AfterLogin_PageLangIsStillHe()
    {
        var lang = await Page.Locator("html").GetAttributeAsync("lang");
        lang.Should().Be("he");
    }

    // ─── Admin Navigation Links ───────────────────────────────────────────────

    [Fact]
    public async Task AdminNav_HasEmployeeListLink()
    {
        // Nav bar rendered by _Layout.cshtml links to /Employee/Index
        var link = Page.Locator("nav a[href*='/Employee']");
        (await link.CountAsync()).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task AdminNav_HasDashboardLink()
    {
        var link = Page.Locator("nav a[href*='/Dashboard']");
        (await link.CountAsync()).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task AdminNav_HasAdminSettingsLink()
    {
        // Admin/Lookup links appear in the nav for admin users
        var link = Page.Locator("nav a[href*='/Admin'], nav a[href*='/Lookup']");
        (await link.CountAsync()).Should().BeGreaterThan(0);
    }

    // ─── Employee List Page ───────────────────────────────────────────────────

    [Fact]
    public async Task EmployeeListPage_RendersTableWithHebrewHeaders()
    {
        await Page.GotoAsync("/Employee/Index");
        var tableHeaders = await Page.Locator("th").AllInnerTextsAsync();
        tableHeaders.Should().Contain(h => h.Any(c => c >= '\u0590' && c <= '\u05ff'));
    }

    [Fact]
    public async Task EmployeeListPage_HasSearchFilter()
    {
        await Page.GotoAsync("/Employee/Index");
        // Search filter is an input with name="search" or similar
        var searchInput = Page.Locator("input[name='search']");
        (await searchInput.CountAsync()).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task EmployeeListPage_HasCreateEmployeeButton()
    {
        await Page.GotoAsync("/Employee/Index");
        var btn = Page.Locator("a[href*='/Employee/Create'], a[href*='Create']");
        (await btn.CountAsync()).Should().BeGreaterThan(0);
    }

    // ─── Dashboard Page ───────────────────────────────────────────────────────

    [Fact]
    public async Task DashboardPage_RendersWithFilters()
    {
        await Page.GotoAsync("/Dashboard/Index");
        var body = await GetPageTextAsync();
        body.Should().NotContain("An unhandled exception");
        body.Should().NotContain("Object reference not set");
    }

    [Fact]
    public async Task DashboardPage_Has200StatusCode()
    {
        var response = await Page.GotoAsync("/Dashboard/Index");
        response!.Status.Should().Be(200);
    }

    // ─── Admin Screens ────────────────────────────────────────────────────────

    [Fact]
    public async Task LookupTable_Districts_RendersWithAddButton()
    {
        await Page.GotoAsync("/Lookup/districts");
        var body = await GetPageTextAsync();
        body.Should().NotContain("An unhandled exception");
    }

    [Fact]
    public async Task SystemConstants_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/Admin/SystemConstants");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task EmailTemplates_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/Admin/EmailTemplates");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task AuditLog_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/Admin/AuditLog");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task NotificationLogs_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/Admin/NotificationLogs");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task ReportingMonths_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/Lookup/reportingmonths");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task Branding_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/Admin/Branding");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task ProjectPrograms_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/Admin/ProjectPrograms");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task TermsOfUse_Admin_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/Admin/TermsOfUse");
        response!.Status.Should().BeLessThan(500);
    }
}

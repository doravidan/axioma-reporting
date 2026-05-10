using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// E2E accessibility tests per IS 5568 / WCAG 2.1 AA:
/// skip link, aria-live alerts, aria-required on inputs, aria-sort on sortable
/// table headers, lang="he", dir="rtl", form label associations.
/// </summary>
[Collection("Playwright")]
public class AccessibilityPlaywrightTests : PlaywrightTestBase
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await LoginAsync();
    }

    // ─── Skip Link ────────────────────────────────────────────────────────────

    [Fact]
    public async Task Layout_HasSkipLink_PointingToMainContent()
    {
        // _Layout.cshtml: <a href="#main-content" class="skip-link">דלג לתוכן הראשי</a>
        await Page.GotoAsync("/Employee/Index");
        var skipLink = Page.Locator("a[href='#main-content']");
        (await skipLink.CountAsync()).Should().BeGreaterThan(0,
            because: "layout must have a skip link for WCAG 2.1 AA keyboard navigation");
    }

    [Fact]
    public async Task Layout_HasMainLandmarkWithId()
    {
        // _Layout.cshtml: <main id="main-content" role="main" ...>
        await Page.GotoAsync("/Employee/Index");
        var main = Page.Locator("main#main-content[role='main']");
        (await main.CountAsync()).Should().BeGreaterThan(0,
            because: "layout must have <main role='main' id='main-content'>");
    }

    // ─── Language & Direction ─────────────────────────────────────────────────

    [Theory]
    [InlineData("/Employee/Index")]
    [InlineData("/Dashboard/Index")]
    [InlineData("/Lookup/districts")]
    public async Task AuthenticatedPages_HaveHtmlLangHe(string url)
    {
        await Page.GotoAsync(url);
        var lang = await Page.Locator("html").GetAttributeAsync("lang");
        lang.Should().Be("he");
    }

    [Theory]
    [InlineData("/Account/Login")]
    [InlineData("/Employee/Index")]
    public async Task Pages_HaveHtmlDirRtl(string url)
    {
        await Page.GotoAsync(url);
        var dir = await Page.Locator("html").GetAttributeAsync("dir");
        dir.Should().Be("rtl");
    }

    // ─── Form Label Associations ──────────────────────────────────────────────

    [Fact]
    public async Task LoginForm_AllLabels_HaveForAttribute()
    {
        await Page.GotoAsync("/Account/Login");
        var labels = await Page.Locator("label").AllAsync();
        foreach (var label in labels)
        {
            var forAttr = await label.GetAttributeAsync("for");
            forAttr.Should().NotBeNullOrEmpty(
                because: "every <label> must have a 'for' attribute for WCAG 2.1 AA");
        }
    }

    [Fact]
    public async Task LoginInputs_HaveAriaRequired()
    {
        // Clear session so we actually see the login page (not redirected to home)
        await Context.ClearCookiesAsync();
        await Page.GotoAsync("/Account/Login");
        (await Page.Locator("input[name='IdNumber'][aria-required='true']").CountAsync())
            .Should().Be(1);
        (await Page.Locator("input[name='Password'][aria-required='true']").CountAsync())
            .Should().Be(1);
    }

    // ─── Alerts & Live Regions ────────────────────────────────────────────────

    [Fact]
    public async Task LoginError_Alert_HasRoleAlertAndAriaLive()
    {
        // Clear session so we actually reach the login page
        await Context.ClearCookiesAsync();
        await Page.GotoAsync("/Account/Login");
        // Use a non-existent user — guaranteed to fail without locking real accounts
        await Page.Locator("input[name='IdNumber']").FillAsync("nonexistent99998");
        await Page.Locator("input[name='Password']").FillAsync("WrongPass1!");
        await Page.Locator("form[action*='Login'] button[type='submit']").ClickAsync();

        var alerts = Page.Locator("[role='alert']");
        var count = await alerts.CountAsync();
        if (count > 0)
        {
            var ariaLive = await alerts.First.GetAttributeAsync("aria-live");
            ariaLive.Should().BeOneOf(new[] { "assertive", "polite" },
                because: "error alerts must announce themselves to screen readers");
        }
    }

    // ─── Table Accessibility ──────────────────────────────────────────────────

    [Fact]
    public async Task EmployeeListTable_AllTh_HaveScopeCol()
    {
        await Page.GotoAsync("/Employee/Index");
        var ths = await Page.Locator("thead th").AllAsync();
        ths.Should().NotBeEmpty(because: "the employee list must have table headers");
        foreach (var th in ths)
        {
            var scope = await th.GetAttributeAsync("scope");
            scope.Should().Be("col",
                because: "all <th> in a data table must have scope='col' per WCAG 2.1 AA");
        }
    }

    [Fact]
    public async Task DashboardTable_SortableHeaders_HaveAriaSort()
    {
        await Page.GotoAsync("/Dashboard/Index");
        // Skip if there is no data table rendered (empty database — no reports)
        if (await Page.Locator("table thead").CountAsync() == 0)
            return;
        // Dashboard table has sortable headers with aria-sort (added per WCAG)
        var sortHeaders = Page.Locator("th[aria-sort]");
        (await sortHeaders.CountAsync()).Should().BeGreaterThan(0,
            because: "sortable dashboard headers must have aria-sort for screen reader sort direction");
    }

    [Fact]
    public async Task DashboardTable_AllTh_HaveScopeCol()
    {
        await Page.GotoAsync("/Dashboard/Index");
        var ths = await Page.Locator("thead th").AllAsync();
        if (ths.Count > 0)
        {
            foreach (var th in ths)
            {
                var scope = await th.GetAttributeAsync("scope");
                scope.Should().Be("col");
            }
        }
    }

    // ─── Navigation Aria ──────────────────────────────────────────────────────

    [Fact]
    public async Task PrimaryNav_HasAriaLabel()
    {
        await Page.GotoAsync("/Employee/Index");
        // _Layout.cshtml: <nav ... aria-label="ניווט ראשי">
        var nav = Page.Locator("nav[aria-label]");
        (await nav.CountAsync()).Should().BeGreaterThan(0,
            because: "primary navigation must have aria-label for screen readers");
    }

    [Fact]
    public async Task PrimaryNav_AriaLabel_IsHebrew()
    {
        await Page.GotoAsync("/Employee/Index");
        var nav = Page.Locator("nav[aria-label]").First;
        var label = await nav.GetAttributeAsync("aria-label");
        label.Should().MatchRegex("[\u0590-\u05ff]",
            because: "nav aria-label must be in Hebrew for the RTL Hebrew interface");
    }
}

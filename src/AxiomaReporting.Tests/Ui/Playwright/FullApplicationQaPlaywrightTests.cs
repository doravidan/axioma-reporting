using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// Broad QA smoke coverage for the main admin and employee surfaces. These tests
/// intentionally avoid destructive form submits, but they exercise every primary
/// page group, role-specific navigation, filters, modal buttons, export buttons,
/// and the employee monthly activity path.
/// </summary>
[Collection("Playwright")]
public class FullApplicationQaPlaywrightTests : PlaywrightTestBase
{
    private static readonly string[] AdminPages =
    {
        "/",
        "/Report/Index",
        "/Dashboard/Index",
        "/Dashboard/Summary",
        "/Employee/Index",
        "/Employee/Create",
        "/Employee/AllocationList",
        "/allocations",
        "/Lookup/Index",
        "/Lookup/districts",
        "/Lookup/localities",
        "/Lookup/sectors",
        "/Lookup/programs",
        "/Lookup/educationalprograms",
        "/Lookup/subjects",
        "/Lookup/domains",
        "/Admin/ReportingMonths",
        "/Admin/Frameworks",
        "/Admin/Institutions",
        "/Admin/SystemConstants",
        "/Admin/Branding",
        "/Admin/EmailTemplates",
        "/Admin/EmailServerSettings",
        "/Admin/InspectorAssignments",
        "/Admin/ProjectPrograms",
        "/Admin/DataMigration",
        "/Admin/BatchReportImport",
        "/Admin/TermsOfUse",
        "/Admin/NotificationLogs",
        "/Admin/AuditLog",
        "/Account/ChangePassword"
    };

    private static readonly string[] EmployeePages =
    {
        "/",
        "/MyAllocations",
        "/allocations",
        "/Report/Index",
        "/Account/ChangePassword"
    };

    [Theory]
    [MemberData(nameof(AdminPageData))]
    public async Task Admin_AllPrimaryPages_RenderWithoutServerOrClientErrors(string path)
    {
        await LoginAsync();

        await AssertHealthyPageAsync(path);
    }

    [Theory]
    [MemberData(nameof(EmployeePageData))]
    public async Task Employee_AllPrimaryPages_RenderWithoutServerOrClientErrors(string path)
    {
        await LoginAsync("111111111", "Password123");

        await AssertHealthyPageAsync(path);
    }

    [Fact]
    public async Task Admin_PrimaryNavigationLinks_OpenExpectedPages()
    {
        await LoginAsync();

        var navLinks = new[]
        {
            "/",
            "/Report",
            "/Dashboard",
            "/allocations",
            "/Employee",
            "/Admin/ReportingMonths"
        };

        foreach (var href in navLinks)
        {
            await Page.GotoAsync("/");
            var link = Page.Locator($"nav a[href='{href}'], nav a[href^='{href}/']").First;
            (await link.CountAsync()).Should().BeGreaterThan(0, because: $"nav should include {href}");
            await link.ClickAsync();
            await AssertCurrentPageHealthyAsync();
        }

        await Page.GotoAsync("/");
        await Page.Locator("nav .dropdown-toggle").First.ClickAsync();
        (await Page.Locator(".dropdown-menu.show").CountAsync()).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Employee_NavigationIsScopedAndPrimaryLinksWork()
    {
        await LoginAsync("111111111", "Password123");

        (await Page.Locator("nav a[href*='/Dashboard']").CountAsync()).Should().Be(0);
        (await Page.Locator("nav a[href*='/Employee']").CountAsync()).Should().Be(0);
        (await Page.Locator("nav a[href*='/Admin']").CountAsync()).Should().Be(0);

        foreach (var href in new[] { "/", "/MyAllocations", "/allocations" })
        {
            await Page.GotoAsync("/");
            var link = Page.Locator($"nav a[href='{href}'], nav a[href^='{href}/']").First;
            (await link.CountAsync()).Should().BeGreaterThan(0, because: $"employee nav should include {href}");
            await link.ClickAsync();
            await AssertCurrentPageHealthyAsync();
        }
    }

    [Theory]
    [InlineData("/Lookup/districts", "#addModal")]
    [InlineData("/Lookup/districts", "#importModal")]
    [InlineData("/Admin/ReportingMonths", "#addModal")]
    [InlineData("/Admin/Frameworks", "#addModal")]
    [InlineData("/Admin/Institutions", "#addModal")]
    public async Task Admin_ModalButtons_OpenTheirModals(string path, string modalSelector)
    {
        await LoginAsync();
        await Page.GotoAsync(path);

        var button = Page.Locator($"[data-bs-target='{modalSelector}']").First;
        (await button.CountAsync()).Should().BeGreaterThan(0);
        await button.ClickAsync();

        await Page.Locator($"{modalSelector}.show").WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 5_000
        });
    }

    [Fact]
    public async Task Admin_EmployeeFiltersSortingAndExport_Work()
    {
        await LoginAsync();
        await AssertHealthyPageAsync("/Employee/Index");

        await Page.Locator("input[name='employeeCode']:not([type='hidden'])").FillAsync("434");
        await Page.Locator("form[method='get'] button[type='submit']").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await AssertCurrentPageHealthyAsync();
        Page.Url.Should().Contain("employeeCode=434");

        await Page.Locator("thead a[href*='sortBy']").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await AssertCurrentPageHealthyAsync();

        var download = await Page.RunAndWaitForDownloadAsync(async () =>
        {
            await Page.Locator("a[href*='ExportExcel']").First.ClickAsync();
        });
        download.SuggestedFilename.Should().EndWith(".xlsx");
    }

    [Fact]
    public async Task Admin_AllocationFiltersDetailAndExport_Work()
    {
        await LoginAsync();
        await AssertHealthyPageAsync("/allocations");

        await Page.Locator("input[name='employeeCode']:not([type='hidden'])").FillAsync("434");
        await Page.Locator("form[method='get'] button[type='submit']").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await AssertCurrentPageHealthyAsync();

        var detailLink = Page.Locator("a[href^='/allocations/']").First;
        (await detailLink.CountAsync()).Should().BeGreaterThan(0);
        await detailLink.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await AssertCurrentPageHealthyAsync();

        await Page.GotoAsync("/allocations");
        var download = await Page.RunAndWaitForDownloadAsync(async () =>
        {
            await Page.Locator("a[href*='/allocations/export']").First.ClickAsync();
        });
        download.SuggestedFilename.Should().EndWith(".xlsx");
    }

    [Fact]
    public async Task Employee_MyAllocationsDetailsReportAndExport_Work()
    {
        await LoginAsync("111111111", "Password123");
        await AssertHealthyPageAsync("/MyAllocations");

        var detailsLink = Page.Locator("a[href*='/MyAllocations/Details']").First;
        (await detailsLink.CountAsync()).Should().BeGreaterThan(0);
        await detailsLink.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await AssertCurrentPageHealthyAsync();

        await Page.GotoAsync("/MyAllocations");
        // Exclude the report-history nav link added in v1.2.11 — the test needs the
        // report-entry tile itself.
        var reportLink = Page.Locator("a[href*='/Report']:not([href*='History'])").First;
        (await reportLink.CountAsync()).Should().BeGreaterThan(0);
        await reportLink.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await AssertCurrentPageHealthyAsync();
        (await Page.Locator("a[href*='DownloadExcelTemplate']").CountAsync()).Should().BeGreaterThan(0);

        await Page.GotoAsync("/MyAllocations");
        var download = await Page.RunAndWaitForDownloadAsync(async () =>
        {
            await Page.Locator("a[href*='ExportExcel']").First.ClickAsync();
        });
        download.SuggestedFilename.Should().EndWith(".xlsx");
    }

    [Fact]
    public async Task Employee_CannotOpenAdminAndEmployeeManagementPages()
    {
        await LoginAsync("111111111", "Password123");

        foreach (var path in new[] { "/Dashboard/Index", "/Employee/Index", "/Admin/ReportingMonths", "/Lookup/districts" })
        {
            var response = await Page.GotoAsync(path);
            response!.Status.Should().BeLessThan(500);
            Page.Url.Should().NotContain(path, because: $"employee should not remain on protected admin route {path}");
            var body = await GetPageTextAsync();
            body.Should().NotContain("An unhandled exception");
        }
    }

    [Fact]
    public async Task Admin_DashboardFiltersSummaryAndExports_Work()
    {
        await LoginAsync();
        await AssertHealthyPageAsync("/Dashboard/Index");

        var filterOptions = await Page.APIRequest.GetAsync($"{BaseUrl}/Dashboard/FilterOptions");
        filterOptions.Status.Should().Be(200);
        (await filterOptions.TextAsync()).TrimStart().Should().StartWith("{");

        var summaryLink = Page.Locator("a[href*='Summary']").First;
        if (await summaryLink.CountAsync() > 0)
        {
            await summaryLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await AssertCurrentPageHealthyAsync();
        }

        await AssertDownloadFromPageAsync("/Dashboard/Index", "a[href*='ExportExcel']");
        await AssertDownloadFromPageAsync("/Dashboard/Summary", "a[href*='SummaryExportExcel']");
    }

    public static IEnumerable<object[]> AdminPageData() =>
        AdminPages.Select(path => new object[] { path });

    public static IEnumerable<object[]> EmployeePageData() =>
        EmployeePages.Select(path => new object[] { path });

    private async Task AssertHealthyPageAsync(string path)
    {
        var pageErrors = new List<string>();
        Page.PageError += (_, error) => pageErrors.Add(error);

        var response = await Page.GotoAsync(path, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
        response.Should().NotBeNull();
        response!.Status.Should().BeLessThan(500, because: path);

        await AssertCurrentPageHealthyAsync();
        pageErrors.Should().BeEmpty(because: $"no JavaScript errors should be thrown on {path}");
    }

    private async Task AssertCurrentPageHealthyAsync()
    {
        var body = await GetPageTextAsync();
        body.Should().NotContain("An unhandled exception");
        body.Should().NotContain("InvalidOperationException");
        body.Should().NotContain("SqlException");
        body.Should().NotContain("NullReferenceException");
        body.Should().NotContain("Stack Query Cookies Headers Routing");
        body.Should().NotContain("Show raw exception details");
    }

    private async Task AssertDownloadFromPageAsync(string path, string selector)
    {
        await Page.GotoAsync(path);
        var link = Page.Locator(selector).First;
        if (await link.CountAsync() == 0)
            return;

        var download = await Page.RunAndWaitForDownloadAsync(async () => await link.ClickAsync());
        download.SuggestedFilename.Should().EndWith(".xlsx");
    }
}

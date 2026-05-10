using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// E2E tests for the Employee management screens:
/// create form field presence, allocation form, cascading project→program filter
/// (AJAX), allocation list, and accessibility attributes.
/// </summary>
[Collection("Playwright")]
public class EmployeeManagementPlaywrightTests : PlaywrightTestBase
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await LoginAsync();
    }

    // ─── Create Employee Form ─────────────────────────────────────────────────

    [Fact]
    public async Task CreateEmployee_FormRendersWithHebrewLabels()
    {
        var response = await Page.GotoAsync("/Employee/Create");
        response!.Status.Should().Be(200);

        var body = await GetPageTextAsync();
        // Hebrew labels: שם פרטי, שם משפחה, קוד עובד, תעודת זהות
        body.Should().MatchRegex(new Regex("שם|עובד|תעודת זהות|קוד", RegexOptions.None));
    }

    [Fact]
    public async Task CreateEmployee_FormHasFirstNameField()
    {
        await Page.GotoAsync("/Employee/Create");
        // asp-for="FirstName" renders as id="FirstName" name="FirstName"
        (await Page.Locator("input[name='FirstName']").CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task CreateEmployee_FormHasLastNameField()
    {
        await Page.GotoAsync("/Employee/Create");
        (await Page.Locator("input[name='LastName']").CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task CreateEmployee_FormHasIdNumberField()
    {
        await Page.GotoAsync("/Employee/Create");
        (await Page.Locator("input[name='IdNumber']").CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task CreateEmployee_FormHasEmployeeCodeField()
    {
        await Page.GotoAsync("/Employee/Create");
        // asp-for="EmployeeCode" renders as name="EmployeeCode"
        (await Page.Locator("input[name='EmployeeCode']").CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task CreateEmployee_FormHasIsReportingEmployeeCheckbox()
    {
        await Page.GotoAsync("/Employee/Create");
        (await Page.Locator("input[name='IsReportingEmployee'][type='checkbox']").CountAsync())
            .Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task CreateEmployee_FormHasRestDayDropdown()
    {
        await Page.GotoAsync("/Employee/Create");
        // asp-for="RestDay" renders as select name="RestDay"
        (await Page.Locator("select[name='RestDay']").CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task CreateEmployee_FormHasStatusDropdown()
    {
        await Page.GotoAsync("/Employee/Create");
        (await Page.Locator("select[name='StatusId']").CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task CreateEmployee_FormHasSubmitButton()
    {
        await Page.GotoAsync("/Employee/Create");
        // Use the main content form's submit button (not the logout buttons in the nav)
        var submitBtn = Page.Locator("main button[type='submit'], .card button[type='submit']").First;
        (await submitBtn.IsVisibleAsync()).Should().BeTrue();
    }

    [Fact]
    public async Task CreateEmployee_Form_IsRtl()
    {
        await Page.GotoAsync("/Employee/Create");
        var dir = await Page.Locator("html").GetAttributeAsync("dir");
        dir.Should().Be("rtl");
    }

    // ─── Allocation List ──────────────────────────────────────────────────────

    [Fact]
    public async Task AllocationList_Page_RendersWithoutError()
    {
        var response = await Page.GotoAsync("/allocations");
        response!.Status.Should().BeLessThan(500);
    }

    [Fact]
    public async Task AllocationList_HasFilterInputs()
    {
        await Page.GotoAsync("/Employee/AllocationList");

        var body = await GetPageTextAsync();
        body.Should().Contain("הקצאות עובדים");
        body.Should().Contain("סנן לפי");
        body.Should().Contain("פרויקט");
        body.Should().Contain("תוכנית");
        body.Should().Contain("משך תפוקה");
        body.Should().Contain("יצא לאקסל");
        body.Should().NotContain("לחיצה על האיקון");
        body.Should().NotContain("באם מדובר");

        var inputs = await Page.Locator("input[type='text'], select").CountAsync();
        inputs.Should().BeGreaterThan(0);
        (await Page.Locator(".detail-icon").CountAsync()).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task AllocationDetail_HasEditorControls()
    {
        var response = await Page.GotoAsync("/Employee/2/Allocations");
        response!.Status.Should().BeLessThan(500);

        var body = await GetPageTextAsync();
        body.Should().Contain("הקצאות");
        body.Should().Contain("פרטי הקצאה");
        body.Should().Contain("טבלת מחוזות בחירה");
        body.Should().Contain("טבלת תוכניות");
        body.Should().Contain("בחר טבלה לעריכה");
        body.Should().Contain("ערכים שנבחרו");
        body.Should().Contain("צא לטבלה");

        (await Page.Locator(".allocation-picker").CountAsync()).Should().BeGreaterThan(0);
        (await Page.Locator(".allocation-category").CountAsync()).Should().BeGreaterThan(0);
        (await Page.Locator(".allocation-search-available").CountAsync()).Should().BeGreaterThan(0);
        (await Page.Locator(".allocation-search-selected").CountAsync()).Should().BeGreaterThan(0);
    }

    // ─── Cascading Program Filter (AJAX) ─────────────────────────────────────

    [Fact]
    public async Task ProgramsForProject_Endpoint_ReturnsJsonArray()
    {
        // GET /Employee/ProgramsForProject?projectId=0 returns JSON array
        var response = await Page.APIRequest.GetAsync(
            $"{BaseUrl}/Employee/ProgramsForProject?projectId=0");
        response.Status.Should().Be(200);
        var text = await response.TextAsync();
        text.TrimStart().Should().StartWith("[",
            because: "ProgramsForProject must return a JSON array");
    }

    [Fact]
    public async Task AllocationForm_Page_RendersWithoutError()
    {
        // Allocation form for a potentially non-existent employee; controller should handle gracefully
        var response = await Page.GotoAsync("/Employee/AddAllocation/1");
        // Accept 200 (found) or 302 (redirect if employee not found)
        response!.Status.Should().BeLessThan(500);
    }

    // ─── Accessibility Attributes ─────────────────────────────────────────────

    [Fact]
    public async Task EmployeeList_TableHeaders_HaveScopeCol()
    {
        await Page.GotoAsync("/Employee/Index");
        var ths = await Page.Locator("thead th").AllAsync();
        ths.Should().NotBeEmpty();
        foreach (var th in ths)
        {
            var scope = await th.GetAttributeAsync("scope");
            scope.Should().Be("col");
        }
    }

    [Fact]
    public async Task Layout_HasSkipLinkOnEmployeePage()
    {
        await Page.GotoAsync("/Employee/Index");
        var skipLink = Page.Locator("a[href='#main-content']");
        (await skipLink.CountAsync()).Should().BeGreaterThan(0);
    }
}

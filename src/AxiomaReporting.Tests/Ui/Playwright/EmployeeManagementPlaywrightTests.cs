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
        // The allocation editor is the card-based AllocationForm (client-feedback
        // redesign) — the old table-picker UI this test originally asserted no
        // longer exists.
        var response = await Page.GotoAsync("/Employee/2/Allocations");
        response!.Status.Should().BeLessThan(500);

        var body = await GetPageTextAsync();
        body.Should().Contain("הקצאות");

        var addLink = Page.Locator("a[href*='/Allocations/Create']").First;
        (await addLink.CountAsync()).Should().BeGreaterThan(0,
            because: "the allocations page must offer adding a new allocation");
        await addLink.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        var form = await GetPageTextAsync();
        form.Should().Contain("פרטי הקצאה");
        form.Should().Contain("שיוכים");
        form.Should().Contain("משך תפוקה");

        (await Page.Locator("select[name='ProjectId'], #projectIdSelect").CountAsync()).Should().BeGreaterThan(0);
        (await Page.Locator("select[name='DistrictIds']").CountAsync()).Should().BeGreaterThan(0);
        (await Page.Locator("select[name='ProgramIds']").CountAsync()).Should().BeGreaterThan(0);
        (await Page.Locator("input[name='OutputDurationValues']").CountAsync()).Should().BeGreaterThan(0);
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
    public async Task AllocationForm_SelectingPrograms_AutoFillsAllDefaultsAndPersists()
    {
        var userId = await CreateReportingEmployeeAsync("990003", "990000036");

        await Page.GotoAsync($"/Employee/{userId}/Allocations/Create");
        await Page.EvaluateAsync(@"() => {
            const project = document.getElementById('projectIdSelect') || document.querySelector(""select[name='ProjectId']"");
            const value = Array.from(project.options).find(o => o.value)?.value;
            project.value = value;
            project.dispatchEvent(new Event('change', { bubbles: true }));
        }");

        await Page.WaitForFunctionAsync(
            $"() => {{ {SelectableValuesJs} return selectableValues(\"select[name='ProgramIds']\").length >= 2; }}",
            null,
            new PageWaitForFunctionOptions { Timeout = 10_000 });

        await SelectFromChoicesListAsync("ProgramIds", 0);
        await Page.WaitForFunctionAsync(
            $"() => {{ {SelectedValuesJs} return selectedValues(\"select[name='SubjectIds']\").length > 0 && selectedValues(\"select[name='FrameworkIds']\").length > 0 && selectedValues(\"select[name='ClassIds']\").length > 0 && selectedValues(\"select[name='LocalityDistrictNationalIds']\").length > 0; }}",
            null,
            new PageWaitForFunctionOptions { Timeout = 10_000 });

        var firstSubjectCount = await SelectedCountAsync("SubjectIds");
        var firstFrameworkCount = await SelectedCountAsync("FrameworkIds");
        var manualDistrict = await SelectFromChoicesListAsync("DistrictIds", 0);

        await SelectFromChoicesListAsync("ProgramIds", 1);
        await Page.WaitForFunctionAsync(
            $"([subjectCount, frameworkCount]) => {{ {SelectedValuesJs} return selectedValues(\"select[name='SubjectIds']\").length > subjectCount && selectedValues(\"select[name='FrameworkIds']\").length > frameworkCount; }}",
            new[] { firstSubjectCount, firstFrameworkCount },
            new PageWaitForFunctionOptions { Timeout = 10_000 });

        (await SelectedValuesAsync("DistrictIds")).Should().Contain(manualDistrict,
            because: "adding a second program must not clear manual selections");

        await SelectFromChoicesListAsync("SectorIds", 0);
        await SelectFromChoicesListAsync("LocalityIds", 0);
        await Page.Locator("input[name='AnnualEmploymentScope']").FillAsync("100");
        await Page.Locator("input[name='MonthlyEmploymentScope']").FillAsync("20");
        await Page.Locator("input[name='MonthlyRowAllocation']").FillAsync("10");
        await Page.Locator("input[name='AnnualRowAllocation']").FillAsync("100");
        await Page.Locator("input[name='OutputDurationValues'][value='1']").CheckAsync();

        var savedSubjects = await SelectedValuesAsync("SubjectIds");
        var savedFrameworks = await SelectedValuesAsync("FrameworkIds");
        var savedClasses = await SelectedValuesAsync("ClassIds");
        var savedLocations = await SelectedValuesAsync("LocalityDistrictNationalIds");

        await Page.Locator("form button[type='submit']:has-text('שמור')").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await ThrowOnValidationErrorsAsync("creating allocation with program defaults");

        var detailLinks = Page.Locator("a.detail-icon[href*='/allocations/']");
        if (await detailLinks.CountAsync() == 0)
            throw new Xunit.Sdk.XunitException($"Allocation was not saved or details link was not found. Url: {Page.Url}. Body: {await GetPageTextAsync()}");
        var detailHref = await detailLinks.First.GetAttributeAsync("href");
        detailHref.Should().NotBeNullOrEmpty();
        await Page.GotoAsync(detailHref!);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        Page.Url.Should().Contain("/Edit");

        var reopenedPrograms = await SelectedValuesAsync("ProgramIds");
        reopenedPrograms.Should().OnlyHaveUniqueItems();
        (await SelectedValuesAsync("SubjectIds")).Should().BeEquivalentTo(savedSubjects);
        (await SelectedValuesAsync("FrameworkIds")).Should().BeEquivalentTo(savedFrameworks);
        (await SelectedValuesAsync("ClassIds")).Should().BeEquivalentTo(savedClasses);
        (await SelectedValuesAsync("LocalityDistrictNationalIds")).Should().BeEquivalentTo(savedLocations);
    }

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

    private async Task<string> CreateReportingEmployeeAsync(string employeeCode, string idNumber)
    {
        await Page.GotoAsync("/Employee/Create");
        await Page.Locator("input[name='EmployeeCode']").FillAsync(employeeCode);
        await Page.Locator("input[name='IdNumber']").FillAsync(idNumber);
        await Page.Locator("input[name='FirstName']").FillAsync("בדיקה");
        await Page.Locator("input[name='LastName']").FillAsync($"AutoFill {employeeCode}");
        await SelectFirstRealOptionAsync("select[name='RoleId']");
        await Page.Locator("select[name='UserRoleId']").SelectOptionAsync("6");
        await SelectFirstRealOptionAsync("select[name='StatusId']");
        var reporting = Page.Locator("input[name='IsReportingEmployee']");
        if (await reporting.CountAsync() > 0 && !await reporting.First.IsCheckedAsync())
            await reporting.First.CheckAsync();

        await Page.Locator("form button[type='submit']:has-text('שמור')").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await ThrowOnValidationErrorsAsync($"creating employee {employeeCode}");

        await Page.GotoAsync($"/Employee/Index?employeeCode={employeeCode}");
        var editHref = await Page.Locator($"tr:has-text('{employeeCode}') a[href*='/Edit']").First.GetAttributeAsync("href");
        editHref.Should().NotBeNullOrEmpty();
        return System.Text.RegularExpressions.Regex.Match(editHref!, @"\d+").Value;
    }

    private const string SelectableValuesJs = @"
        const selectableValues = (selector) => {
            const sel = document.querySelector(selector);
            if (!sel) return [];
            const inst = window._axChoices && window._axChoices.get(sel);
            if (inst) {
                const store = (inst._store && inst._store.choices)
                    || (inst._currentState && inst._currentState.choices) || [];
                return store.filter(c => c.value !== '' && !c.disabled && !c.placeholder).map(c => String(c.value));
            }
            return Array.from(sel.options).filter(o => o.value).map(o => o.value);
        };";

    private const string SelectedValuesJs = @"
        const selectedValues = (selector) => {
            const sel = document.querySelector(selector);
            if (!sel) return [];
            const inst = window._axChoices && window._axChoices.get(sel);
            if (inst) return inst.getValue(true).map(v => String(v));
            return Array.from(sel.selectedOptions).map(o => String(o.value));
        };";

    private async Task<string> SelectFromChoicesListAsync(string selectName, int index)
    {
        return await Page.EvaluateAsync<string>($@"([name, index]) => {{
            {SelectableValuesJs}
            const selector = `select[name='${{name}}']`;
            const sel = document.querySelector(selector);
            const values = selectableValues(selector);
            const value = values[index];
            if (!sel || !value) return '';
            const inst = window._axChoices && window._axChoices.get(sel);
            if (inst) inst.setChoiceByValue(String(value));
            else Array.from(sel.options).forEach(o => {{ if (o.value === String(value)) o.selected = true; }});
            sel.dispatchEvent(new Event('change', {{ bubbles: true }}));
            return String(value);
        }}", new object[] { selectName, index });
    }

    private async Task<int> SelectedCountAsync(string selectName) =>
        (await SelectedValuesAsync(selectName)).Length;

    private async Task<string[]> SelectedValuesAsync(string selectName) =>
        await Page.EvaluateAsync<string[]>($@"(name) => {{
            {SelectedValuesJs}
            return selectedValues(`select[name='${{name}}']`);
        }}", selectName);

    private async Task SelectFirstRealOptionAsync(string selector)
    {
        var select = Page.Locator(selector);
        if (await select.CountAsync() == 0) return;
        var value = await select.First.EvaluateAsync<string?>(
            "sel => Array.from(sel.options).find(o => o.value)?.value ?? null");
        if (!string.IsNullOrEmpty(value))
            await select.First.SelectOptionAsync(value);
    }

    private async Task ThrowOnValidationErrorsAsync(string context)
    {
        var errors = await Page.Locator(".text-danger, .validation-summary-errors li").AllInnerTextsAsync();
        var joined = string.Join(" | ", errors.Select(e => e.Trim()).Where(e => e.Length > 0));
        if (joined.Length > 0)
            throw new Xunit.Sdk.XunitException($"Validation errors while {context}: {joined} (url: {Page.Url})");
    }
}

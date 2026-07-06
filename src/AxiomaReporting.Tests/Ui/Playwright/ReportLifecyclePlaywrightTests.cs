using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// Full end-to-end coverage of the reporting lifecycle — the destructive flows the
/// smoke suite deliberately avoids: employee creation, allocation creation,
/// admin-reports-for-employee, employee self-reporting, submit, reject via the
/// Summary modal, resubmit, SINGLE approve (regression for the nested-form bug that
/// dumped users on an empty /Dashboard/BulkApprove), and BULK approve.
///
/// The test creates its own employees so it never mutates the demo employee
/// (111111111) state that other suites rely on; the one phase that uses the demo
/// employee ends by rejecting + deleting the row, which leaves the report editable.
/// </summary>
[Collection("Playwright")]
public class ReportLifecyclePlaywrightTests : PlaywrightTestBase
{
    private const string DemoEmployeeId = "111111111";
    private const string DemoEmployeePassword = "Password123";

    [Fact]
    public async Task ReportLifecycle_SubmitRejectResubmitApproveBulk_EndToEnd()
    {
        // Auto-accept all confirm() dialogs (submit report, delete row).
        Page.Dialog += async (_, dialog) => await dialog.AcceptAsync();

        await LoginAsync();

        // ── Phase 1: single-approve path on a dedicated employee ────────────
        // IdNumbers must pass the Israeli-ID checksum validation.
        var emp1 = await CreateEmployeeWithAllocationAsync("990001", "990000010");

        await AddRowAndSubmitAsync($"/Report?userId={emp1}");

        await Page.GotoAsync("/Dashboard/Summary");
        await AssertSummaryMarkupSafeAsync();

        // Reject via the modal — must open in place, not navigate/submit anything.
        var urlBeforeReject = Page.Url;
        var row1 = SummaryRow("990001");
        await row1.Locator("button:has-text('דחה')").ClickAsync();
        await Page.Locator("#rejectModal.show").WaitForAsync(new LocatorWaitForOptions { Timeout = 5_000 });
        Page.Url.Should().Be(urlBeforeReject, because: "the reject button must open the modal without submitting any form");

        await Page.Locator("#rejectModal textarea[name='rejectionReason']").FillAsync("בדיקת E2E — נא לתקן");
        await Page.Locator("#rejectModal button[type='submit']").ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        Page.Url.Should().NotContain("BulkApprove");
        (await GetPageTextAsync()).Should().Contain("הדיווח הוחזר");

        // Returned report must be editable again — resubmit it.
        await SubmitExistingReportAsync($"/Report?userId={emp1}");

        // SINGLE approve — the regression: must land back on Summary, never on
        // an empty /Dashboard/BulkApprove.
        await Page.GotoAsync("/Dashboard/Summary");
        await SummaryRow("990001").Locator("button[formaction*='Approve']:has-text('אשר')").ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        Page.Url.Should().Contain("/Dashboard/Summary", because: "single approve must redirect back to the summary screen");
        Page.Url.Should().NotContain("BulkApprove");
        var afterSingle = await GetPageTextAsync();
        afterSingle.Should().Contain("הדיווח אושר");
        (await SummaryRow("990001").Locator(".badge:has-text('מאושר')").CountAsync())
            .Should().BeGreaterThan(0, because: "the approved report should show a מאושר badge");

        // ── Phase 2: bulk-approve path on a second dedicated employee ───────
        var emp2 = await CreateEmployeeWithAllocationAsync("990002", "990000028");
        await AddRowAndSubmitAsync($"/Report?userId={emp2}");

        await Page.GotoAsync("/Dashboard/Summary");
        var row2 = SummaryRow("990002");
        await row2.Locator("input.report-cb").CheckAsync();
        var bulkBtn = Page.Locator("#bulkApproveBtn");
        (await bulkBtn.IsDisabledAsync()).Should().BeFalse(because: "selecting a report should enable the bulk approve button");
        (await Page.Locator("#selectedCount").InnerTextAsync()).Trim().Should().Be("1");

        await bulkBtn.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        Page.Url.Should().Contain("/Dashboard/Summary");
        (await GetPageTextAsync()).Should().Contain("דיווחים אושרו בהצלחה");
        (await SummaryRow("990002").Locator(".badge:has-text('מאושר')").CountAsync()).Should().BeGreaterThan(0);

        // ── Phase 2b: reopen an approved report (status override) ───────────
        // An approved report is locked for the employee; admin/PM can return it
        // to editing so more rows can be reported within the same month.
        await SummaryRow("990002").Locator("button:has-text('החזר לעריכה'), form button:has-text('החזר לעריכה')").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        Page.Url.Should().Contain("/Dashboard/Summary");
        (await GetPageTextAsync()).Should().Contain("הדיווח הוחזר לעריכה");
        (await SummaryRow("990002").Locator(".badge:has-text('הוחזר לתיקון')").CountAsync())
            .Should().BeGreaterThan(0, because: "reopening must return the report to the editable returned-for-correction state");

        // The reopened report accepts a new row and can be resubmitted.
        await AddRowAndSubmitAsync($"/Report?userId={emp2}");

        // ── Phase 3: employee self-service flow on the demo employee ────────
        // Ends with reject + row deletion so the demo report stays editable for
        // the other Playwright suites.
        await Context.ClearCookiesAsync();
        await LoginAsync(DemoEmployeeId, DemoEmployeePassword);
        await AddRowAndSubmitAsync("/Report/Index");
        (await GetPageTextAsync()).Should().Contain("ממתין לאישור");

        await Context.ClearCookiesAsync();
        await LoginAsync();
        await Page.GotoAsync("/Dashboard/Summary");
        var demoRow = SummaryRow("4343343");
        await demoRow.Locator("button:has-text('דחה')").ClickAsync();
        await Page.Locator("#rejectModal.show").WaitForAsync(new LocatorWaitForOptions { Timeout = 5_000 });
        await Page.Locator("#rejectModal textarea[name='rejectionReason']").FillAsync("בדיקת E2E — החזרה לעריכה");
        await Page.Locator("#rejectModal button[type='submit']").ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Context.ClearCookiesAsync();
        await LoginAsync(DemoEmployeeId, DemoEmployeePassword);
        await Page.GotoAsync("/Report/Index");
        var employeeView = await GetPageTextAsync();
        employeeView.Should().Contain("הוחזר לתיקון");
        (await Page.Locator("button:has-text('הוסף שורה')").CountAsync())
            .Should().BeGreaterThan(0, because: "a returned report must be editable by its owner");

        // Delete the E2E row to restore the demo report to its original shape.
        var deleteButtons = Page.Locator("#reportTable tbody button:has-text('מחק')");
        var rowCountBefore = await Page.Locator("#reportTable tbody tr[data-row-id]").CountAsync();
        if (await deleteButtons.CountAsync() > 0)
        {
            await deleteButtons.First.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            (await Page.Locator("#reportTable tbody tr[data-row-id]").CountAsync())
                .Should().BeLessThan(Math.Max(rowCountBefore, 1));
        }
    }

    /// <summary>
    /// Regression guards for the nested-form bug: the per-row approve/reject
    /// controls live INSIDE the bulk form, so they must never be forms themselves.
    /// </summary>
    private async Task AssertSummaryMarkupSafeAsync()
    {
        (await Page.Locator("#bulkForm form").CountAsync())
            .Should().Be(0, because: "nested forms get dropped by the browser and hijack the bulk form submit");
        (await Page.Locator("#bulkForm button:has-text('אשר'):not([formaction]):not(#bulkApproveBtn)").CountAsync())
            .Should().Be(0, because: "row-level approve buttons must target Report/Approve via formaction");
        (await Page.Locator("#bulkForm button:has-text('דחה'):not([type='button'])").CountAsync())
            .Should().Be(0, because: "the reject button must not submit the surrounding bulk form");
    }

    // Not scoped to #bulkForm: once no reports are pending, Summary renders the
    // read-only table instead of the bulk-approve form.
    private ILocator SummaryRow(string employeeCode) =>
        Page.Locator("tbody tr").Filter(new LocatorFilterOptions { HasTextString = employeeCode });

    /// <summary>Creates an active reporting employee + a full allocation, returns the user id.</summary>
    private async Task<string> CreateEmployeeWithAllocationAsync(string employeeCode, string idNumber)
    {
        await Page.GotoAsync("/Employee/Create");
        await Page.Locator("input[name='EmployeeCode']").FillAsync(employeeCode);
        await Page.Locator("input[name='IdNumber']").FillAsync(idNumber);
        await Page.Locator("input[name='FirstName']").FillAsync("בדיקה");
        await Page.Locator("input[name='LastName']").FillAsync($"E2E {employeeCode}");
        await SelectFirstRealOptionAsync("select[name='RoleId']");
        await Page.Locator("select[name='UserRoleId']").SelectOptionAsync("6");
        await SelectFirstRealOptionAsync("select[name='StatusId']");
        var reporting = Page.Locator("input[name='IsReportingEmployee']");
        if (await reporting.CountAsync() > 0 && !await reporting.First.IsCheckedAsync())
            await reporting.First.CheckAsync();
        await Page.Locator("form button[type='submit']:has-text('שמור')").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await ThrowOnValidationErrorsAsync($"creating employee {employeeCode}");

        // Resolve the new user's id from the employee list edit link.
        await Page.GotoAsync($"/Employee/Index?employeeCode={employeeCode}");
        var editHref = await Page.Locator($"tr:has-text('{employeeCode}') a[href*='/Edit']").First.GetAttributeAsync("href");
        editHref.Should().NotBeNull(because: $"employee {employeeCode} should appear in the list after creation");
        var userId = System.Text.RegularExpressions.Regex.Match(editHref!, @"\d+").Value;
        userId.Should().NotBeNullOrEmpty();

        // Allocation with one value in every reporting-relevant scope list.
        await Page.GotoAsync($"/Employee/{userId}/Allocations/Create");
        await Page.EvaluateAsync(@"() => {
            const proj = document.getElementById('projectIdSelect') || document.querySelector(""select[name='ProjectId']"");
            if (proj && proj.options.length > 1) {
                proj.value = Array.from(proj.options).find(o => o.value)?.value ?? proj.value;
                proj.dispatchEvent(new Event('change', { bubbles: true }));
            }
        }");

        // תוכנית is mandatory (AllocationValidator) and its list loads after the
        // project is chosen. NOTE: Choices.js multi-selects remove non-selected
        // <option> elements from the DOM, so available values must be read from
        // the Choices store, not sel.options.
        await Page.WaitForFunctionAsync(
            $"() => {{ {SelectableValuesJs} return selectableValues(\"select[name='ProgramIds']\").length > 0; }}",
            null, new PageWaitForFunctionOptions { Timeout = 10_000 });
        await SelectFirstFromListAsync("ProgramIds");
        await Page.Locator("input[name='AnnualEmploymentScope']").FillAsync("100");
        await Page.Locator("input[name='MonthlyEmploymentScope']").FillAsync("20");
        await Page.Locator("input[name='MonthlyRowAllocation']").FillAsync("10");
        await Page.Locator("input[name='AnnualRowAllocation']").FillAsync("100");
        await Page.Locator("input[name='OutputDurationValues'][value='0.5']").CheckAsync();
        await Page.Locator("input[name='OutputDurationValues'][value='1']").CheckAsync();

        foreach (var listName in new[]
        {
            "DistrictIds", "SectorIds", "LocalityIds", "FrameworkIds", "SubjectIds",
            "DomainIds", "EducationalProgramIds", "GradeLevelIds", "DiscussionCodeIds"
        })
        {
            await SelectFirstFromListAsync(listName);
        }

        await Page.Locator("form button[type='submit']:has-text('שמור')").First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await ThrowOnValidationErrorsAsync($"creating allocation for employee {employeeCode}");
        (await GetPageTextAsync()).Should().NotContain("An unhandled exception");

        return userId;
    }

    /// <summary>
    /// JS snippet defining selectableValues(selector): returns the selectable
    /// values of a select whether it is native (reads options) or wrapped by
    /// Choices.js (reads the Choices store — the DOM options are stripped).
    /// </summary>
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

    /// <summary>Selects the first available value in a (possibly Choices-wrapped) select by name.</summary>
    private async Task SelectFirstFromListAsync(string selectName)
    {
        await Page.EvaluateAsync($@"(name) => {{
            {SelectableValuesJs}
            const sel = document.querySelector(`select[name='${{name}}']`);
            if (!sel) return;
            const values = selectableValues(`select[name='${{name}}']`);
            if (!values.length) return;
            const inst = window._axChoices && window._axChoices.get(sel);
            if (inst) inst.setChoiceByValue(String(values[0]));
            else Array.from(sel.options).forEach(o => o.selected = o.value === values[0]);
            sel.dispatchEvent(new Event('change', {{ bubbles: true }}));
        }}", selectName);
    }

    private async Task ThrowOnValidationErrorsAsync(string context)
    {
        var errors = await Page.Locator(".text-danger, .validation-summary-errors li").AllInnerTextsAsync();
        var joined = string.Join(" | ", errors.Select(e => e.Trim()).Where(e => e.Length > 0));
        if (joined.Length > 0)
            throw new Xunit.Sdk.XunitException($"Validation errors while {context}: {joined} (url: {Page.Url})");
    }

    /// <summary>Opens the report editor, adds one fully-populated row, and submits the report.</summary>
    private async Task AddRowAndSubmitAsync(string reportUrl)
    {
        await Page.GotoAsync(reportUrl);
        await Page.Locator("button:has-text('הוסף שורה')").First.ClickAsync();
        await Page.Locator("#rowModal.show").WaitForAsync(new LocatorWaitForOptions { Timeout = 5_000 });

        await Page.Locator("#fieldDate").FillAsync(DateTime.Today.ToString("yyyy-MM-dd"));
        // Fill every select in the modal that has a real option and no value yet
        // (works for both native and Choices-wrapped selects: form submission
        // reads the underlying <select> element).
        await Page.EvaluateAsync($@"() => {{
            {SelectableValuesJs}
            document.querySelectorAll('#rowForm select').forEach(sel => {{
                if (sel.value) return;
                const inst = window._axChoices && window._axChoices.get(sel);
                const store = inst
                    ? ((inst._store && inst._store.choices) || (inst._currentState && inst._currentState.choices) || [])
                        .filter(c => c.value !== '' && !c.disabled && !c.placeholder).map(c => String(c.value))
                    : Array.from(sel.options).filter(o => o.value).map(o => o.value);
                if (!store.length) return;
                if (inst) inst.setChoiceByValue(String(store[0]));
                else sel.value = store[0];
                sel.dispatchEvent(new Event('change', {{ bubbles: true }}));
            }});
        }}");
        await Page.Locator("#fieldNotes").FillAsync($"שורת בדיקה E2E {Guid.NewGuid():N}");

        await Page.Locator("#rowModal button:has-text('שמור שורה')").ClickAsync();

        // saveRow() reloads on success or reveals #rowErrors on validation failure.
        try
        {
            await Page.Locator("#reportTable tbody tr[data-row-id]").First.WaitForAsync(
                new LocatorWaitForOptions { Timeout = 15_000 });
        }
        catch (TimeoutException)
        {
            var errors = await Page.Locator("#rowErrors").InnerTextAsync();
            throw new Xunit.Sdk.XunitException($"Report row was not saved. Validation errors: {errors}");
        }

        await SubmitExistingReportAsync(null);
    }

    /// <summary>Submits the currently open (or given) report and asserts it went to pending.</summary>
    private async Task SubmitExistingReportAsync(string? reportUrl)
    {
        if (reportUrl != null)
            await Page.GotoAsync(reportUrl);

        await Page.Locator("button:has-text('הגשת דיווח')").ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        (await GetPageTextAsync()).Should().Contain("ממתין לאישור",
            because: "submitting a report should move it to pending-approval");
    }

    private async Task SelectFirstRealOptionAsync(string selector)
    {
        var select = Page.Locator(selector);
        if (await select.CountAsync() == 0) return;
        var value = await select.First.EvaluateAsync<string?>(
            "sel => Array.from(sel.options).find(o => o.value)?.value ?? null");
        if (!string.IsNullOrEmpty(value))
            await select.First.SelectOptionAsync(value);
    }
}

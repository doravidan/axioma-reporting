# Client Feedback Fix Plan — 2026-04-29/30

---

## Executive Summary

The client tested v1.0 and flagged 20 issues. Breakdown:

| Severity | Count | Meaning |
|----------|-------|---------|
| ?? **Blocker** | 2 | Actual broken behavior — must fix before go-live |
| ?? **Business logic** | 5 | Behavior diverges from spec |
| ?? **Validation** | 3 | Missing input validation |
| ?? **UI/UX** | 10 | Visual and usability improvements |

All 20 items have a locked fix path (see "Decisions Locked" at the bottom — 5 items previously flagged as needing client clarification were resolved against `SPEC.md`, `SPEC_DOCX_AUDIT.md`, and existing code).

**Total estimate:** 4 dev days + 1 QA/repackage day = one work week before re-delivering as v1.1.

---

## Detailed Tasks

### ?? Blockers — Fix First

#### Fix #20 — Forgot-password email not sent
**Client note (Hebrew):** "עדכנתי שרת מייל במערכת ועשיתי שכחתי סיסמא ולא קיבלתי מייל"
**Translation:** "I updated the email server settings, did 'forgot password', and didn't receive an email."

**Diagnosis steps:**
1. Check `/Admin/NotificationLogs` — was a `PasswordReset` row created?
2. If yes and `Failed` — read the `FailureReason` column.
3. If no row at all — bug in `AccountController.ForgotPassword`: it isn't calling `NotificationDispatcher`.

**Likely fixes:**
- No row ? ensure the action calls `_emailService.SendAsync` with the `PasswordReset` template.
- Row exists but failed ? likely an SMTP misconfiguration the client just entered (port/SSL/auth).
- Possibly the user the client tried doesn't exist — show a friendly message either way (don't leak existence).

**Files:** `src/AxiomaReporting.Web/Controllers/AccountController.cs`, `src/AxiomaReporting.Infrastructure/Services/NotificationDispatcher.cs`

**Estimate:** 0.5 day (including end-to-end smoke test of the ForgotPassword flow)

---

#### Fix #2 — Terms of Use not shown on first launch
**Client note (Hebrew):** "בעת פתיחת היישום פעם ראשונה יש להציג את תנאי ההסכם"
**Translation:** "On the very first launch of the application, the Terms of Use must be displayed."

**Current state:** `RequireTermsAcceptedFilter` exists but apparently doesn't catch the first post-install login.

**Diagnosis:** Likely `SeedData.cs` marks the seeded Admin with `AcceptedTermsOfUse=true`, so the filter never triggers for the first admin login.

**Fix:**
- Remove `AcceptedTermsOfUse=true` from the seeded Admin in `SeedData.cs`.
- Make sure the seeded `TermsOfUseVersion` v1 has real body text (not a placeholder) so the page renders something to accept.
- Verify `RequireTermsAcceptedFilter` actually `RedirectToAction("TermsOfUse", "Account")` and isn't returning an empty result.

**Files:** `src/AxiomaReporting.Infrastructure/Data/SeedData.cs`, `src/AxiomaReporting.Web/Authorization/RequireTermsAcceptedFilter.cs`

**Estimate:** 0.5 day

---

### ?? Business Logic Gaps

#### Fix #3 — Lock active-month settings after activation
**Client note (Hebrew):** "לאחר פתיחת חודש חדש והפעלתו לא ניתן לשנות את הערכים כגון דיווח עתידי או תאריך אחרון לדווח. רק ע\"י מנהל פרויקט ומנהל מערכת"
**Translation:** "After a new reporting month has been opened and activated, values like 'allow future reporting' or 'last reporting date' must not be editable. Only Project Manager and System Admin should be allowed to edit."

**Fix:**
- In the reporting-month edit form, disable `LastReportingDate` and `AllowFutureReporting` when `IsActive=true`.
- Exception: if the user is Admin or PM — allow editing but show a confirm dialog.
- Add a ?? lock icon next to locked fields with a tooltip explaining why.

**Files:** `src/AxiomaReporting.Web/Views/ReportingMonth/Edit.cshtml`, `src/AxiomaReporting.Web/Controllers/ReportingMonthController.cs`

**Estimate:** 0.5 day

---

#### Fix #8 — Rest-day options: Sunday / Friday / Saturday only
**Client note (Hebrew):** "יום מנוחה צ\"י רק שישי שבת ראשון"
**Translation:** "Rest day should be only Friday, Saturday, or Sunday."

**Fix:**
In `Views/Employee/Card.cshtml` (the blue card), restrict the `RestDay` `<select>` to three values:

```html
<select asp-for="RestDay" class="form-select">
  <option value="">— None —</option>
  <option value="0">Sunday (ראשון)</option>
  <option value="5">Friday (שישי)</option>
  <option value="6">Saturday (שבת)</option>
</select>
```

**Files:** `src/AxiomaReporting.Web/Views/Employee/Card.cshtml` (and any other view that renders RestDay).

**Estimate:** 0.25 day

---

#### Fix #17 — Monthly/annual hours: integers only (no decimals)
**Client note (Hebrew):** "מספר שעות חודשיות ושנתיות לא לתת ספרות אחרי הנקודה"
**Translation:** "Monthly and annual hours fields must not allow digits after the decimal point."

**Fix:**
- Keep `MonthlyEmploymentScope` and `AnnualEmploymentScope` as `decimal` in the DB (preserves data integrity), but render as integer in the UI.
- In the form: `<input type="number" step="1" min="0" />`.
- In `AllocationListFilterModel`: drop fractional digits in the table render.
- Validation: reject non-integer values with the message `"יש להזין מספר שלם"` ("Enter a whole number").

**Files:** `src/AxiomaReporting.Web/Views/Employee/Allocation.cshtml`, `Views/Employee/AllocationList.cshtml`, `src/AxiomaReporting.Infrastructure/Validators/AllocationValidator.cs`

**Estimate:** 0.5 day

---

#### Fix #19 — Default password = ID, force change on first login, reset ? ID + force change again
**Client note (Hebrew):** "ברירת מחדל של סיסמא צ\"ל ת.זהות ודרישה לשנות סיסמא בכניסה ראשונה. איפוס סיסמא יחזיר לתעודת זהות וידרוש חלפה שוב"
**Translation:** "Default password should be the national ID number, with forced change on first login. A password reset should set it back to the ID number and force another change."

**Current state:** Default = ID is correct. The reset flow needs to be verified.

**Fix:**
- In `AdminController.ResetPassword` and `EmployeeController.ResetPassword` (admin-initiated reset): new password = `IdNumber`, set `MustChangePassword=true`.
- In `AccountController.ResetPassword` (token-from-email self-service flow): password is whatever the user typed — this is a **different** flow, not the same.
- Make the distinction explicit in code and in UI copy so it doesn't confuse users.

**Files:** `src/AxiomaReporting.Web/Controllers/AdminController.cs`, `src/AxiomaReporting.Web/Controllers/EmployeeController.cs`

**Estimate:** 0.25 day

---

### ?? Missing Validations

#### Fix #5 — Israeli ID validation
**Client note (Hebrew):** "יש לעשות בקרת תקינות ת\"ז"
**Translation:** "Add Israeli national-ID validation."

**Fix:**
- Create `IsraeliIdValidator` (Israeli check-digit algorithm).
- Add to `UserValidator` (FluentValidation): `RuleFor(u => u.IdNumber).Must(IsraeliIdValidator.IsValid).WithMessage("מספר תעודת זהות אינו תקין")`.
- Add client-side validation in `wwwroot/js/validation.js`.

**Algorithm:** 9 digits, Luhn-like check digit. Each digit at an odd index multiplied by 1, even-indexed digit multiplied by 2 (if > 9 ? sum its digits). Total sum must be divisible by 10.

**Files:** `src/AxiomaReporting.Core/Validators/IsraeliIdValidator.cs` (new), `src/AxiomaReporting.Infrastructure/Validators/UserValidator.cs`

**Estimate:** 0.5 day

---

#### Fix #6 — Israeli phone validation
**Client note (Hebrew):** "בקרת תקינות על טלפון"
**Translation:** "Add phone-number validation."

**Fix:**
- Israeli phone regex: `^0(2|3|4|8|9|5[02-9]|7[2-9])\d{7}$`.
- Add to `UserValidator`: `RuleFor(u => u.Phone).Matches(IsraeliPhoneRegex).WithMessage("מספר טלפון אינו תקין")`.
- Allow empty (phone is optional).

**Files:** `src/AxiomaReporting.Infrastructure/Validators/UserValidator.cs`

**Estimate:** 0.25 day

---

#### Fix #7 — All validation messages in Hebrew
**Client note (Hebrew):** "ככלל כל בקרות התקינות בעברית"
**Translation:** "As a rule, all validation messages must be in Hebrew."

**Diagnosis:** Audit every `WithMessage` in FluentValidation and every `[Required(ErrorMessage = ...)]` Data Annotation.

**Fix:**
- Walk every validator under `Infrastructure/Validators/` and translate English messages to Hebrew.
- Walk every `[Required(ErrorMessage = ...)]`, `[StringLength(...)]`, etc. across all Models.
- Define a global `ErrorMessageResource` in Hebrew.
- For jQuery validation: include `wwwroot/lib/jquery-validation/localization/messages_he.js`.

**Files:** all `Validators/`, all `Models/`, `_ViewImports.cshtml`

**Estimate:** 1 day (depends on volume)

---

### ?? UI / UX

#### Fix #1 — Polish the design and use logo colors
**Client note (Hebrew):** "ליפות קצת את העיצוב, להשתדל להשתמש בצבעי הלוגו שלהם"
**Translation:** "Beautify the design a bit, try to use their logo colors."

**Resolution:** No brand colors specified anywhere in spec or docs. The logo file (`wwwroot/images/logo.png`) is the authoritative source. We will sample colors programmatically and propose a palette for client approval before applying broadly.

**Fix:**
- Run a one-shot Python script (`scripts/extract_logo_colors.py`) that uses Pillow to extract the top 5 dominant non-white/non-black colors from `logo.png`.
- Pick a primary (most saturated dominant), a secondary (complementary), and an accent.
- Send the client a screenshot mock of the navbar+header in the proposed palette **before** applying repo-wide.
- After approval, create `wwwroot/css/theme.css`:
  ```css
  :root {
    --axioma-primary: #<approved>;
    --axioma-secondary: #<approved>;
    --axioma-accent: #<approved>;
  }
  .navbar-axioma { background-color: var(--axioma-primary); }
  .btn-axioma-primary { background-color: var(--axioma-primary); border-color: var(--axioma-primary); }
  /* card-header overrides for blue/green cards if client wants brand-aligned variants */
  ```
- Apply to navbar, primary buttons, page headers, and active-state highlights. Keep Bootstrap defaults for status colors (success/danger/warning).

**Files:** `scripts/extract_logo_colors.py` (new, one-time), `src/AxiomaReporting.Web/wwwroot/css/theme.css` (new), `Views/Shared/_Layout.cshtml`, `site.css`

**Estimate:** 1 day (sampling is fast; iteration on the palette mockup may take a round or two)

---

#### Fix #4 — Graphical menu per spec (My Allocations submenu)
**Client note (Hebrew):** "אין תפריט גראפי כמו באפיון והזרימה לא כפי שצויין"
**Translation:** "The graphical menu shown in the spec is missing, and the navigation flow doesn't match the spec."

**Resolution:** SPEC.md §5.4 (lines 158-166) is explicit:

> *Clicking "Monthly Activity" reveals: My Allocations (ההקצאות שלי) ? opens two sub-options: Monthly Activity Update Screen + Monthly Excel File Upload. After clicking My Allocations, the relevant salary month must be displayed.*

`SPEC_DOCX_AUDIT.md` had deferred this as cosmetic ("functionally equivalent"), but the client now explicitly asks for it (#4) — **the audit's deferral is overridden**. Implement per spec.

**Fix:**
- Replace the current flat "פעילות חודשית" link with a dropdown on the navbar that opens a submenu page (`/MyAllocations/Index`).
- Submenu page shows:
  - Active reporting month banner at the top (large, prominent).
  - Two large icon tiles:
    - ?? **עדכון פעילות חודשית** ? `/Report/Index` (current report form).
    - ?? **העלאת אקסל חודשי** ? `/Report/UploadExcel` (existing upload flow).
- The Excel-upload tile is hidden if the user lacks `AllowExcelUpload` permission.
- For Admin/PM/Coordinator: keep current direct routes accessible (they have their own dashboards and don't need this submenu).

**Files:** `src/AxiomaReporting.Web/Controllers/MyAllocationsController.cs` (new), `Views/MyAllocations/Index.cshtml` (new), `Views/Shared/_Layout.cshtml` (replace nav link), `Views/Home/Index.cshtml` (optional sidebar entry)

**Estimate:** 1 day

---

#### Fix #9 + #12 — Status / Roles tables in Hebrew
**Client notes (Hebrew):** "טבלת תפקיד מערכת" / "טבלת סטאטוס עובד בעברית"
**Translation:** "System roles table" / "Employee status table should be in Hebrew."

**Diagnosis:** The `Roles` and `UserStatuses` seed data may include English names alongside the Hebrew description.

**Fix:**
- Ensure `Views/Lookup/List.cshtml` (and `Admin/Roles`, `Admin/UserStatuses`) show only the Hebrew `Description`.
- Hide the English `Name` field (if present) from the UI; keep it for code only.
- If the `Description` column in the live DB is in English — update both the seed and run a manual UPDATE in the client environment.

**Files:** `src/AxiomaReporting.Infrastructure/Data/SeedData.cs`, `Views/Admin/Roles.cshtml`, `Views/Admin/UserStatuses.cshtml`

**Estimate:** 0.25 day

---

#### Fix #10 — Description column on file upload
**Client note (Hebrew):** "בהעלאת קובץ להוסיף עמודת תיאור"
**Translation:** "On the file upload [screen], add a description column."

**Resolution (from doc review):**
- Lookup-table imports (`/Lookup/{name}`) already have a description column — not the target.
- Item #10 sits between client notes #9 (system roles) and #11 (card titles) — context suggests employee/admin upload screens.
- Most likely target: **`/Admin/BatchReportImport` result table** — currently shows row #, employee code, reporter name, error message. Client wants an extra **"description"** column that explains *what the system did* per row (e.g., "matched allocation X", "skipped — duplicate", "updated existing report", "rejected — no matching allocation").

**Fix:**
- Add `string ResultDescription` to the per-row result DTO in `BatchReportImportService`.
- Populate during processing: matched allocation, action taken, or rejection reason.
- Render as a new column in `Views/Admin/BatchReportImportResult.cshtml`.
- Confirm exact wording with client during the call, but the column itself ships in this round.

**Files:** `src/AxiomaReporting.Infrastructure/Services/BatchReportImportService.cs`, `src/AxiomaReporting.Core/Dtos/BatchImportRowResult.cs`, `Views/Admin/BatchReportImportResult.cshtml`

**Estimate:** 0.5 day

---

#### Fix #11 — Card titles: color as background, not in the title text
**Client note (Hebrew):** "כותרות הדפים 'עריכת עובד כרטיס כחול' / 'עריכת עובד כרטיס ירוק' — הצבעים אמורים להיות הרקע ולא חלק מהכותרת"
**Translation:** "Page titles 'Edit Employee — Blue Card' / 'Edit Employee — Green Card' — the colors should be the background, not part of the title text."

**Fix:**
- Change titles from `<h3>Edit Employee — Blue Card</h3>` to `<h3>Edit Employee</h3>`, with the colored card body around it.
- Blue card ? `<div class="card border-primary"><div class="card-header bg-primary text-white">Employee Details</div>`
- Green card ? `<div class="card border-success"><div class="card-header bg-success text-white">Allocation Details</div>`

**Files:** `src/AxiomaReporting.Web/Views/Employee/Card.cshtml`, `Views/Employee/Allocation.cshtml`

**Estimate:** 0.25 day

---

#### Fix #13 — Action buttons on the right side of the employee list
**Client note (Hebrew):** "רשימת עובדים — כפתורי הפעולות צריכים להיות מצד ימין כי לא רואים אותם בלי גלילה"
**Translation:** "Employee list — action buttons should be on the right side, because you can't see them without scrolling."

**Fix:**
- In `Views/Employee/List.cshtml`: move the "Actions" column from left to right (first column in RTL layout).
- This way it's always visible even on wide tables.

**Files:** `src/AxiomaReporting.Web/Views/Employee/List.cshtml`, `Views/Employee/AllocationList.cshtml`

**Estimate:** 0.25 day

---

#### Fix #14 — Preserve filter after refresh / row action
**Client note (Hebrew):** "סיננתי לפי שם עובד, השבתתי אותו, רענו ללא הסינון אמור להישאר הפילטר"
**Translation:** "I filtered by employee name, deactivated [an employee], refreshed without the filter — the filter should have been retained."

**Fix:**
- After a `POST` action (deactivate, approve, etc.), `RedirectToAction` with a querystring that includes the current filter values.
- Approach: stash `Request.Query` in `TempData["LastFilter"]`, or pass it as a hidden `returnUrl` in the form.
- Make sure this pattern is consistent across every list screen.

**Files:** `EmployeeController.SetActive`, `EmployeeController.ResetPassword`, `EmployeeController.UnlockAccount`, and every similar POST action.

**Estimate:** 0.5 day

---

#### Fix #15 — Missing filters per spec (Employee List)
**Client note (Hebrew):** "אין את כל הסינונים שבאפיון"
**Translation:** "Not all filters from the spec are present."

**Resolution (from doc review):**
SPEC.md §7.2-7.3 enumerates filters for the Employee List. Current `EmployeeController.Index` (lines 57-95) supports only **4** filters: `search`, `statusId`, `roleId`, `projectId`. The Allocation List (`AllocationListFilterModel.cs`) already has the full **12-filter** set. Goal: bring Employee List to parity.

**Fix — add these filters:**
1. ID Number (text, exact / contains)
2. Employee Code (text)
3. First Name (text)
4. Last Name (text)
5. District (multi-select via `AllocationDistricts`)
6. Sector (multi-select via `AllocationSectors`)
7. Program (multi-select via `AllocationPrograms`)
8. Locked-only toggle (`StatusId = Locked`)
9. Rest Day (single dropdown)
10. Allows Future Reporting (yes/no/all)
11. Notes (text contains)
12. Has Allocations (yes/no/all)

**Implementation steps:**
- Create new `Models/EmployeeListFilterModel.cs` (mirror of `AllocationListFilterModel.cs` structure with `Normalize()` method).
- Refactor `EmployeeController.Index` to take the model.
- Update `Views/Employee/Index.cshtml` filter bar to mirror `Views/Employee/AllocationList.cshtml`.
- Use the same Choices.js widget (Fix #18) for multi-selects.

**Files:** `src/AxiomaReporting.Web/Models/EmployeeListFilterModel.cs` (new), `Views/Employee/Index.cshtml`, `EmployeeController.cs:57-95`

**Estimate:** 1 day

---

#### Fix #16 — Sort on every column
**Client note (Hebrew):** "לא כל העמודות מאפשרות מיון"
**Translation:** "Not all columns are sortable."

**Fix:**
- In `Views/Employee/List.cshtml` and `AllocationList.cshtml`: add `<a asp-route-sortBy="..." asp-route-sortDesc="...">` to every column header.
- In the controller: add a `case` for every column name in `switch (sortBy)`.
- Add `aria-sort` for accessibility.

**Files:** every list view + their controllers.

**Estimate:** 0.5 day

---

#### Fix #18 — Multi-select on allocations
**Client note (Hebrew):** "בחירות מרובות להקצאות לא ברור איך עושים עדכון לבחירות -תראה לי בשיחה"
**Translation:** "Multi-select on allocations — it's not clear how to update the selections — show me in a call."

**Resolution:** Spec is silent on widget choice (only mentions "multi-select allowed" in the data sense). Client confusion is a UX symptom of the native `<select multiple>` (requires Ctrl+Click, not obvious on touch devices). **Decision: replace with `Choices.js`** (no jQuery, ~50KB minified, RTL-friendly, accessible, modern tag-style widget with type-to-search and chip removal).

**Fix:**
- Add `wwwroot/lib/choices.js/choices.min.js` + `choices.min.css` (or via CDN).
- Init globally on every `select[multiple]` via a small script in `_Layout.cshtml`:
  ```html
  <script src="~/lib/choices.js/choices.min.js"></script>
  <script>
    document.querySelectorAll('select[multiple]').forEach(el =>
      new Choices(el, {removeItemButton: true, searchPlaceholderValue: 'חיפוש…', noResultsText: 'לא נמצאו תוצאות'})
    );
  </script>
  ```
- Affects: Allocation card (Green), Allocation List filters, Employee List filters (Fix #15), Dashboard filters.

**Files:** `wwwroot/lib/choices.js/` (new), `Views/Shared/_Layout.cshtml`, `wwwroot/css/site.css` (RTL tweaks)

**Estimate:** 0.5 day

---

## Recommended Work Plan

### Day 1 — Blockers + business-logic
- ? #20 — forgot-password / SMTP diagnosis & fix
- ? #2 — terms of use on first login (un-seed admin's `AcceptedTermsOfUse=true`)
- ? #19 — admin password reset ? ID + force change
- ? #3 — lock active-month settings (PM/Admin override)
- ? #8 — rest day (Sun/Fri/Sat only)
- ? #17 — integer-only employment scope

### Day 2 — Validations + Hebrew errors
- ? #5 — Israeli ID checksum validator
- ? #6 — Israeli phone regex validator
- ? #7 — all FluentValidation + Data Annotation messages translated to Hebrew
- ? Add `messages_he.js` for jQuery validation client-side
- ? #9 + #12 — verify and fix Hebrew text in `Roles` and `UserStatuses` seed/views

### Day 3 — Lists & filtering
- ? #13 — move actions column to right (RTL-first)
- ? #14 — preserve filter querystring across POST ? Redirect
- ? #16 — make every column sortable + add `aria-sort`
- ? #11 — card titles refactored: color as background, not in title
- ? #15 — sync employee list filters to 12-filter parity with allocation list
- ? #10 — `BatchReportImport` result table: add "תיאור" column with per-row outcome

### Day 4 — UI widgets + visual theme
- ? #18 — install Choices.js, init globally on `select[multiple]`, RTL CSS tweaks
- ? #1 — sample logo colors (Pillow script), email mock to client, after approval apply `theme.css`
- ? #4 — implement `/MyAllocations` submenu page (active-month banner + 2 tiles), update navbar

### Day 5 — QA, build, redeliver
- ? Run `dotnet test` — ensure 132+ tests pass; add tests for: Israeli ID validator, phone validator, password-reset flow, terms-on-first-login redirect, employee-list filter parity, BatchImport description column
- ? Manual smoke test of all 20 fixes against the client's checklist
- ? `dotnet publish` ? repackage `AxiomaReporting-Delivery-v1.1.zip`
- ? Update `IMPLEMENTATION_STATUS.md` and `CLIENT_DELIVERY.md` with v1.1 changes
- ? Send the client an updated `CLIENT_DELIVERY_EMAIL.md` with the change log

---

## Decisions Locked (from spec/docs review, no client call needed)

| # | Decision | Source |
|---|----------|--------|
| 1 | Sample logo colors via Pillow, propose 3-color palette as mock for approval before applying | No brand colors in spec; logo is authoritative |
| 4 | Implement spec §5.4 submenu structure: `/MyAllocations` with active-month banner + 2 tiles (Update + Upload). Audit's "deferred as cosmetic" overridden by client #4 | SPEC.md §5.4 lines 158-166; SPEC_DOCX_AUDIT.md lines 49-52 (now overridden) |
| 10 | Add a "תיאור" column to `/Admin/BatchReportImport` result table that explains the per-row outcome (matched/skipped/updated/rejected). Confirm exact wording with client during demo | Inferred from client-note context (between #9 and #11); no other upload screen lacks descriptions |
| 15 | Sync Employee List to the 12 filters already present in `AllocationListFilterModel` | SPEC.md §7.2-7.3; current code has only 4 filters |
| 18 | Install Choices.js (no jQuery, RTL, accessible) globally on `select[multiple]` | Spec silent on widget; client confusion warrants modern tag widget |

---

*All open questions resolved from spec + doc review. Plan is ready to execute. Axioma — 2026-04-30.*

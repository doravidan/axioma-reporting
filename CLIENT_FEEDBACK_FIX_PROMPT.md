# Prompt: Complete v1.1 — Client Feedback Fixes, Production Verification, SMTP End-to-End

You are picking up a **partial** v1.1 fix pass on the Axioma Employee Reporting System. A previous attempt applied DB hotfixes and static JS/CSS patches, but most server-side work and end-to-end verification remain. **You must work in the source tree (`src/AxiomaReporting.*`) and finish the job through deployed-and-verified, not through client-side workarounds.**

## Mandatory Ground Rules

1. **No client-side-only fixes.** Every validation, default, redirect, and lookup change must live in C#. JS may augment, never replace, server-side rules.
2. **Every fix must end with green proof:** a passing automated test (unit or integration) **and** a manual smoke test against the deployed production-mode build.
3. **Static assets must load on every page**, including `/Account/Login` and `/Account/ForgotPassword`. If the login page doesn't include `site.css`/`site.js` today, **fix the layout chain** so those assets load — or move the asset references into the partial layout used by the login page. This is a prerequisite for several fixes below; do it first.
4. **Don't rely on the prior pass.** Re-verify each "partially done" item from scratch in the source — the previous client-side patch may be incomplete, brittle, or invisible to certain pages.
5. **Use the existing test project (`tests/AxiomaReporting.Tests`).** Don't introduce a new test framework. The baseline is 132 tests passing. End at ≥150.
6. **Don't break existing tests.** If you must change behavior covered by an existing test, update the test in the same commit and explain why in the PR/commit message.
7. **Hebrew is the UI language.** Every user-facing string you add/change is in Hebrew. Code identifiers stay English.

---

## Phase 0 — Repo & environment baseline (do this first)

1. From `f:\דווח עובדים אקסיומא`, confirm `dotnet build AxiomaReporting.sln` returns **0 errors** before you touch anything. If it doesn't, fix the build first and report what you fixed.
2. `dotnet test` — record the baseline pass count. Anything that's red at the start, fix or document before proceeding.
3. Read these files end-to-end before editing:
   - `CLIENT_FEEDBACK_FIX_PLAN.md` (the locked plan — this is the source of truth)
   - `src/AxiomaReporting.Web/Program.cs`
   - `src/AxiomaReporting.Web/Views/Shared/_Layout.cshtml` and any `_LoginLayout.cshtml`
   - `src/AxiomaReporting.Web/Controllers/AccountController.cs`
   - `src/AxiomaReporting.Web/Controllers/EmployeeController.cs`
   - `src/AxiomaReporting.Web/Controllers/AdminController.cs`
   - `src/AxiomaReporting.Web/Controllers/ReportingMonthController.cs`
   - `src/AxiomaReporting.Infrastructure/Data/SeedData.cs`
   - `src/AxiomaReporting.Infrastructure/Validators/UserValidator.cs` (and siblings)
   - `src/AxiomaReporting.Infrastructure/Services/NotificationDispatcher.cs`
   - `src/AxiomaReporting.Web/Models/AllocationListFilterModel.cs`
4. **Asset-loading fix (prerequisite):** ensure `_Layout.cshtml`, the login layout, and any other root layout all reference `~/css/site.css`, `~/js/site.js`, **and** the new `~/lib/choices.js/choices.min.css` + `choices.min.js`. Verify by viewing the page source of `/Account/Login` and `/Account/ForgotPassword` against a running build before continuing.

---

## Phase 1 — Blockers (Day 1, must finish today)

### #20 — Forgot-password email
**Status carried over:** an existing `NotificationLog` row showed `SMTP DNS: No such host is known`. Bad `smtp@gmail.com` was corrected to `smtp.gmail.com` in the DB. **Real SMTP credentials test still pending.**

Definition of done:
- `AccountController.ForgotPassword` always writes a `NotificationLog` row before returning (success message identical whether or not the user exists — no enumeration leak).
- The `EmailServerSettings` test-send button at `/Admin/EmailServerSettings` succeeds against the live SMTP relay the client provided.
- Submit `/Account/ForgotPassword` for a known user → mail arrives in the inbox within 60 seconds → `NotificationLog.Status = 'Sent'`.
- Submit for an unknown user → user-facing message identical → no `NotificationLog` row OR a row with `Status='Skipped'` (do **not** send mail to unknown addresses).
- Add an integration test that asserts the controller writes the log row and calls `IEmailService.SendAsync` with the `PasswordReset` template for a real user, and skips for a missing user.

### #2 — Terms of Use on first login
**Status carried over:** live-DB admin's `AcceptedTermsOfUse=0` and Terms body replaced. `RequireTermsAcceptedFilter` not yet verified.

Definition of done:
- `SeedData.cs` no longer seeds the admin with `AcceptedTermsOfUse=true`.
- `TermsOfUseVersion` v1 in seed has real Hebrew body text (not a placeholder).
- `RequireTermsAcceptedFilter` redirects to `Account/TermsOfUse` for any authenticated user whose `AcceptedTermsOfUse=false`, except when the request **is** for `Account/TermsOfUse`, `Account/Logout`, or static assets — write the exemption list explicitly.
- After accepting, the user is redirected to whichever page they originally requested (preserve `returnUrl`).
- Integration test: fresh admin → `/Dashboard` → 302 to `/Account/TermsOfUse`. After POSTing acceptance, GET to `/Dashboard` → 200.

---

## Phase 2 — Business logic (Day 1 continued)

### #3 — Lock active-month settings
**Status carried over:** missed entirely. Needs source.

Definition of done:
- `ReportingMonthController.Edit` (GET): when the month being edited has `IsActive=true` AND the current user is **not** Admin/PM, hydrate the view model with `LockNonAdminFields=true`.
- `ReportingMonthController.Edit` (POST): when the same condition holds, ignore inbound `LastReportingDate` and `AllowFutureReporting` from the form; reload them from the DB before saving.
- View: render `LastReportingDate` and `AllowFutureReporting` as disabled inputs with a 🔒 icon and `title="ניתן לשינוי רק ע&quot;י מנהל פרויקט או מנהל מערכת"` when locked.
- Admin/PM see the fields as editable but with a `confirm()` dialog: `"שינוי שדות חודש פעיל יכול להשפיע על דיווחים פתוחים. להמשיך?"`.
- Tests: two new tests — Coordinator role POSTing changed `LastReportingDate` does **not** persist; Admin role POSTing changed value **does** persist.

### #8 — Rest-day options
**Status carried over:** client-side patch in `site.js`. Brittle — any view that doesn't load `site.js` reverts to the full 7-day list.

Definition of done:
- Add a `RestDayOptions` static `IReadOnlyList<SelectListItem>` somewhere reusable (e.g. `Web/Helpers/SelectListProviders.cs`) with **only** Sunday=0, Friday=5, Saturday=6 plus a "— ללא —" empty option.
- Both `Views/Employee/Card.cshtml` (create + edit) bind to that list, not a hand-written `<option>` block.
- Server-side validator on `EmployeeDto.RestDay` rejects values outside `{null,0,5,6}` with message `"יום מנוחה חייב להיות ראשון, שישי או שבת"`.
- Remove the `site.js` workaround once the server fix is in.
- Test: a request that POSTs `RestDay=2` returns `ModelState.IsValid=false` with the Hebrew message.

### #17 — Integer monthly/annual hours
**Status carried over:** client-side `step="1"` only.

Definition of done:
- `AllocationValidator` rejects non-integer `MonthlyEmploymentScope` or `AnnualEmploymentScope` with `"יש להזין מספר שלם"`.
- Razor inputs use `<input type="number" step="1" min="0" />`.
- Display formatting in the allocation list and dashboards trims trailing `.00`.
- Test: POST allocation with `MonthlyEmploymentScope=80.5` → invalid with the Hebrew message.

### #19 — Reset password to ID + force change
**Status carried over:** missed.

Definition of done:
- `AdminController.ResetPassword` and `EmployeeController.ResetPassword` set `PasswordHash = BCrypt(IdNumber)`, `MustChangePassword=true`, `FailedLoginAttempts=0`, `LastPasswordChange=DateTime.UtcNow`, push current hash into `PasswordHistory`.
- Each writes an `AuditLog` entry: `Action="User.PasswordReset"`, `Notes="reset-by={CurrentUser.IdNumber}"`.
- After reset, the next login of that user must hit the change-password screen before any other route.
- The token-from-email self-service flow in `AccountController.ResetPassword` is **not** changed (user-typed password is honored).
- Tests: admin-initiated reset on a target user → DB shows `MustChangePassword=true` and login with `IdNumber` succeeds and forces change. Self-service reset with a typed password does **not** force change.

---

## Phase 3 — Validations + Hebrew error messages (Day 2)

### #5 — Israeli ID validator
- New: `src/AxiomaReporting.Core/Validators/IsraeliIdValidator.cs` — `public static bool IsValid(string id)` implementing the standard Luhn-like Israeli check digit. Pad to 9 digits with leading zeros before checking. Reject empty/null/non-digits.
- Wire into `UserValidator` with `WithMessage("מספר תעודת זהות אינו תקין")`.
- Add to a JS file that **is** loaded everywhere (not only `site.js`) — or accept that client-side is best-effort and server-side is the gate.
- Tests: valid IDs (e.g. `000000018`, `123456782`) pass; invalid ID `123456789` fails; non-numeric fails; null fails.

### #6 — Israeli phone validator
- Regex `^0(2|3|4|8|9|5[02-9]|7[2-9])\d{7}$`. Empty/null OK (phone optional).
- Wire into `UserValidator` with `"מספר טלפון אינו תקין"`.
- Test: `0501234567` valid, `054-1234567` invalid (we don't allow dashes — or, if you choose to allow them, normalize before validation), `12345` invalid.

### #7 — All Hebrew error messages
- Audit **every** `WithMessage(...)` under `src/AxiomaReporting.Infrastructure/Validators/`. Translate any English to Hebrew.
- Audit every `[Required]`, `[StringLength]`, `[Range]`, `[EmailAddress]` across `Core` and `Web` projects. Add Hebrew `ErrorMessage`. Don't rely on resource files for v1.1; inline Hebrew is fine.
- Add `wwwroot/lib/jquery-validation/localization/messages_he.js` reference in the layout used by every page (login included).
- Acceptance: produce a list of every changed message in the PR description so QA can spot-check.

### #9 + #12 — Roles / statuses in Hebrew
**Status carried over:** DB `UserStatuses.Name` still English because the table only has `Name`, no Hebrew column.

Pick one of these approaches and commit to it; do **not** half-do both:
- **Option A (preferred):** add a `Description` (nvarchar 200) column to `UserRoles` and `UserStatuses` via a new EF migration. Seed Hebrew descriptions. Update views to bind to `Description`. Keep `Name` for code lookups only.
- **Option B (fallback):** keep schema; render Hebrew via a static `Dictionary<string,string>` keyed by the English `Name` in a view-component / tag-helper. This is brittle — only choose if the migration is blocked.

Tests: render `/Admin/Users` → assert no English `Active`/`Locked` strings appear in the HTML, only Hebrew equivalents.

---

## Phase 4 — Lists & filtering (Day 3)

### #11 — Card titles: color as background, not in title
- `Views/Employee/Card.cshtml` and `Views/Employee/Allocation.cshtml` (or whatever the green card view is named): page `<h3>` becomes plain `עריכת עובד` / `עריכת הקצאה`. The colored background lives on `<div class="card-header bg-primary text-white">פרטי עובד</div>` (blue) and `<div class="card-header bg-success text-white">פרטי הקצאה</div>` (green).
- Manual visual check: take a screenshot before/after, attach to the change.

### #13 — Action buttons on the right
- `Views/Employee/List.cshtml` and `AllocationList.cshtml`: move the actions `<th>`/`<td>` from last to first position. Test on viewport 1024px wide → buttons visible without horizontal scroll.

### #14 — Preserve filters across POST → Redirect
- For every POST in `EmployeeController` that ends in `RedirectToAction(nameof(Index))`, change to `RedirectToAction(nameof(Index), filterValues)` where `filterValues` is rebuilt from `Request.Query` or stashed in `TempData["LastFilter"]`.
- Same pattern for `AllocationList` POSTs and any other list with row actions.
- Test: integration test posts `SetActive` with `?search=פלוני` in the referrer → redirect target preserves the search.

### #15 — Employee list filter parity
- New `src/AxiomaReporting.Web/Models/EmployeeListFilterModel.cs` mirroring `AllocationListFilterModel`. Include `Normalize()`.
- Refactor `EmployeeController.Index` (currently lines 57-95 with 4 filters) to take the model.
- Filters required: ID, Code, FirstName, LastName, District (multi), Sector (multi), Program (multi), Status, Role, RestDay, AllowFutureReporting tri-state, Notes contains, HasAllocations tri-state, LockedOnly toggle.
- View: copy the structure from `AllocationList.cshtml`. Ensure all multi-selects pick up Choices.js (Phase 5).
- Test: each filter narrows the result set on its own and combines with `AND` semantics across filters.

### #16 — Sort every column
- Every header in `Views/Employee/Index.cshtml`, `AllocationList.cshtml`, dashboard tables, and lookup tables becomes a sort link with `aria-sort` reflecting current state.
- Add a `case` in each controller's sort switch for every visible column.
- Test: sort by each column ascending then descending; assert order changes.

### #10 — Batch import description column
**Status carried over:** missed.

- Add `string ResultDescription` to `BatchImportRowResult` DTO.
- `BatchReportImportService` populates it: `"שורה {row}: התאמה להקצאה {alloc}"` / `"דולגה — דוח כפול"` / `"עודכן דוח קיים"` / `"נדחה — אין הקצאה תואמת"`.
- `Views/Admin/BatchReportImportResult.cshtml` shows the column.
- Test: feeding a known good row shows the matched-allocation message; a known duplicate shows the skipped message.

---

## Phase 5 — UI widgets & visual theme (Day 4)

### #18 — Choices.js global init
**Status carried over:** files added; site.js initializes; only some pages benefit.

- Confirm `choices.min.css`/`choices.min.js` are referenced from a layout that **every** page uses, including login.
- Init via inline IIFE at the bottom of the shared layout (don't depend on `site.js` order):
  ```html
  <script>
    document.querySelectorAll('select[multiple]').forEach(el =>
      new Choices(el, { removeItemButton: true, searchPlaceholderValue: 'חיפוש…', noResultsText: 'לא נמצאו תוצאות' })
    );
  </script>
  ```
- RTL CSS: ensure the Choices container respects `direction: rtl` (custom rule in `site.css` if Choices renders LTR by default).
- Manual check: open allocation card → district widget shows chips with X removal, type-to-search works.

### #1 — Logo colors → theme.css
**Status carried over:** generic CSS polish only; no logo sampling, no client mock.

- New script `scripts/extract_logo_colors.py` using Pillow to extract dominant colors. Save the proposed palette as `scripts/logo-palette.png` (a small image showing the 3 swatches with hex labels).
- Build a one-page mock of the navbar + a sample dashboard with the proposed colors applied (a static HTML at `scripts/palette-mock.html` is fine).
- Apply the palette via `wwwroot/css/theme.css` with CSS variables; reference from `_Layout.cshtml` after `site.css` so it overrides.
- Replace **only** the navbar background, primary buttons, and active state highlights. Leave Bootstrap success/danger/warning alone.
- Don't ship before the client has approved the palette via screenshot — **but** prepare the PR so applying takes one merge after approval.

### #4 — `/MyAllocations` submenu page
**Status carried over:** missed.

- New `MyAllocationsController` with `Index` action, `[Authorize(Roles = "Employee")]` (or whatever the employee policy is named).
- New `Views/MyAllocations/Index.cshtml`: full-width banner showing active reporting month name + last reporting date, then two tile-style buttons (Bootstrap card / Tailwind tile / Razor — match existing UI patterns):
  - 📋 `עדכון פעילות חודשית` → `/Report/Index`
  - 📤 `העלאת אקסל חודשי` → `/Report/UploadExcel` (hidden if `User.AllowExcelUpload == false`)
- Update `_Layout.cshtml` so the existing flat "פעילות חודשית" link points to `/MyAllocations` for Employee role; Admin/PM keep direct routing as today.
- Test: as Employee role, `/MyAllocations` returns 200 and the page contains the active month name. With `AllowExcelUpload=false`, the upload tile is not rendered.

---

## Phase 6 — Tests, publish, redeliver (Day 5)

1. `dotnet test` — must be **green**. Add the new tests called out in each phase. Target ≥ 150 passing.
2. `dotnet build -c Release` — 0 errors, 0 new warnings.
3. `dotnet publish src/AxiomaReporting.Web/AxiomaReporting.Web.csproj -c Release -r win-x64 --no-self-contained -o publish-staging/app`.
4. Repackage: copy `publish-staging/app/`, the existing `database/`, `docs/`, `config/`, and root `README.txt` into a fresh staging folder. Zip as `AxiomaReporting-Delivery-v1.1.zip`. Verify it sits next to `v1.0.zip` and is ≤ 30 MB.
5. Update `docs/CLIENT_DELIVERY.md` with a v1.1 changelog section listing every one of the 20 fixes (Hebrew is fine; bullet per item; cross-reference the original client note number).
6. Update `IMPLEMENTATION_STATUS.md` with the v1.1 work.
7. Update `CLIENT_DELIVERY_EMAIL.md` so it covers v1.1 (don't replace the v1.0 history — append).

---

## Phase 7 — Production verification (the part that's been skipped)

This phase is **not optional**. Without it the work isn't done. Use a real production-mode build, not the dev server.

1. **Deploy to a fresh local "production" environment** that mirrors the client setup:
   - SQL Server Express with `AxiomaReporting` DB created from `database/schema.sql` (don't reuse a dev DB).
   - IIS site (or `dotnet AxiomaReporting.Web.dll` with `ASPNETCORE_ENVIRONMENT=Production` if IIS isn't available — note which you used).
   - `appsettings.Production.json` from the template, with the SQL connection string filled in.
2. **First-launch admin flow:** browse to the site → login as `ADMIN` / `Admin123` → forced password change → forced terms acceptance → land on dashboard. Capture screenshots of every step. **All three gates must trigger** (#2, #19).
3. **SMTP end-to-end:**
   - `/Admin/EmailServerSettings` → enter the client's actual SMTP relay (or a Gmail App Password if the client provided one) → "שלח מייל בדיקה" → mail arrives. If it fails, capture the `NotificationLog.FailureReason` and fix before continuing.
   - `/Account/ForgotPassword` for a real user → mail arrives → click the link → reset password successfully.
   - `/Admin/EmployeeController.ResetPassword` (admin-initiated) → user gets a "your password was reset to your ID" notification (if such an email exists in templates) → user logs in with ID → forced change.
4. **Reminder service:** flip `ReminderIntervalDays=0` and `ReminderStartDaysBeforeDeadline=999` temporarily in `SystemConstants` (or use a test reporting month with a near deadline). Verify the next `ReminderService` tick produces a `NotificationLog` row of type `ReminderNotSubmitted` and the mail arrives. Restore the constants.
5. **Background job retry:** stop the SMTP server (or set bad credentials), trigger a notification, observe `NotificationLog.Status=Failed` with `NextRetryAt` set, restore SMTP, watch the next `NotificationRetryService` tick flip the row to `Sent`.
6. **Concurrency:** open the same employee record in two browser sessions. Save in one. Save in the other → user-friendly Hebrew error: `"הרשומה עודכנה ע&quot;י משתמש אחר. רענן את הדף וערוך מחדש."`.
7. **Excel upload:**
   - Upload a known-bad employee report Excel → on-screen Hebrew errors + downloadable Hebrew RTL PDF.
   - Upload a known-good report → success + `NotificationLog` row of type `BatchImportSuccessUploader` arrives in the inbox.
8. **Audit log:** every action above produced an `AuditLog` row. Browse to `/Admin/AuditLog` and confirm row counts grew. Export to CSV; open in Excel — Hebrew renders correctly.
9. **All 20 client items** — go down the original list and tick each one off against the live deployment. If any item can't be verified, document it and propose how to fix in a follow-up ticket.
10. **Performance smoke:** dashboard with 1000 employees seeded → page loads ≤ 2s on a local SQL Express. If slower, add the missing index (most likely on `Reports.UserId, ReportingMonthId`).

---

## Reporting

Produce a final **VERIFICATION_REPORT_v1.1.md** at the repo root with:
- Per-item status: ✅ shipped & verified · ⚠️ shipped but verification blocked (with reason) · ❌ not done (with reason).
- The smoke-test screenshots (or links to them).
- The SMTP test result with mail-server header info if shareable.
- The final `dotnet test` count.
- The size and SHA256 of `AxiomaReporting-Delivery-v1.1.zip`.
- A short list of any new follow-up tickets discovered along the way.

Don't summarize what you intended to do. Summarize what actually happened. If something didn't work, say so plainly — partial honesty beats false green.

---

## What's explicitly out of scope

- New features beyond the 20 client items.
- Refactors of working code that already passes tests.
- UI changes outside the screens called out in the plan.
- Switching frameworks, ORMs, or test runners.
- Breaking the v1.0 delivery zip (keep both side-by-side).

If you find a bug that's not in the 20 items, log it in the verification report; don't fix it silently.

---

*This prompt supersedes any earlier "fix plan" instructions. Execute it end-to-end.*

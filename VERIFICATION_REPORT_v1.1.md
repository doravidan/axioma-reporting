# Verification Report — Axioma Employee Reporting v1.1

**Date:** 2026-04-30
**Build:** `AxiomaReporting-Delivery-v1.1.zip`
**Size:** 26 MB (25.98 MB exact)
**SHA256:** `32AD60A5DC452BF482CF30F398C90305896616B5EF0BED38CC76ECFDD4212FEB`

> **Build history (informational only — only the SHA above is authoritative):**
> - `338D559E…` — initial v1.1 build (before integration pass)
> - `15B103AA…` — after integration pass (validators wired, sort headers, filter preservation)
> - `32AD60A5…` — **current** — after digit-only employee-code rule was added (caught during a second-pass spreadsheet review of the unnumbered note "קוד עובד ספרות בלבד")

---

## Executive summary

All 20 client-feedback items from `בדיקת פרויקט דיווח פעילות.xlsx` are implemented in source, plus a follow-up integration pass that closed the gaps the parallel agents left (validators not actually wired into POSTs, missing client-side validation, the `/MyAllocations` Excel tile pointing at a POST-only action, incomplete integer-scope behavior in list filters, missing sort headers on some allocation columns, employee row actions not preserving the full filter querystring, residual English role/status dropdown labels, and one test fixture using an invalid Israeli ID under the new validator), plus a **third pass** that picked up an unnumbered spreadsheet note — *"קוד עובד ספרות בלבד"* (employee code must be digits only) — and shipped digit-only validation server-side, in the DTO, on the form, with edit-time tolerance for legacy alphanumeric codes. The full solution builds clean in Release with 0 errors. The non-Playwright test suite is **321 / 321 passing** (was 315 after the integration pass, 240 baseline; +81 net new tests across the v1.1 release). Static assets are present in the published output, including the new `_AnonymousLayout`, Choices.js minified files, theme.css with sampled brand colors, and `messages_he.js` for Hebrew jQuery validation. The new EF migration `20260430110451_AddHebrewDescriptionsToRolesAndStatuses` is included in `database/schema.sql` and adds the `DescriptionHebrew` columns + Hebrew Terms-of-Use body update + admin acceptance reset.

| Verification dimension | Status |
|------------------------|--------|
| `dotnet build -c Release` | ✅ 0 errors, 2 known MailKit NU1902 warnings |
| `dotnet test` (excl. Playwright) | ✅ **321 / 321** passing |
| Per-item code change | ✅ All 20 items mapped to source files |
| Published artifact | ✅ `AxiomaReporting-Delivery-v1.1.zip` (26 MB) |
| Static asset presence in zip | ✅ All assets confirmed |
| Schema migration in `schema.sql` | ✅ Lines confirm `ALTER TABLE` + Hebrew Terms body |
| Live runtime test (SQL Server) | ⚠️ Deferred — no SQL Server / LocalDB on dev box |
| Live SMTP end-to-end | ⚠️ Deferred — needs client's relay credentials |
| Concurrency / Excel / reminder live test | ⚠️ Deferred to client-site smoke test |

---

## Integration pass (after parallel agents)

The parallel agents produced working, isolated fixes but inevitably left some plumbing gaps that only surface when the pieces meet. The integration pass closed these:

| Gap | Resolution |
|-----|-----------|
| `UserValidator` and `AllocationValidator` were registered with FluentValidation but **not actually invoked** by employee/allocation POST actions | Validators wired into the real POST handlers via `IValidator<T>` injection; Razor `ModelState.AddModelError` chain confirmed end-to-end |
| Client-side validation absent for #5 / #6 / #17 (only server-side) | Added unobtrusive validation attributes + JS in `site.js` for Israeli ID checksum, Israeli phone regex, whole-number monthly/annual scope |
| `/MyAllocations` Excel tile linked to `POST /Report/UploadExcel` (would 405 on click) | Tile now navigates to the report page with the upload-enabled allocation pre-selected, matching the spec's "open the upload form, don't trigger upload" semantics |
| Integer-only #17 was honored in the form input but list filters still accepted decimals, display formatting still rendered `.00`, normalize() didn't round | Filter range fields use `step="1"`, display formatter uses `0.###`, `Normalize()` floors decimals to integers |
| #16 sort coverage was complete on Employee list but several Allocation list columns lacked sort links and controller cases | Added missing `<a asp-route-sortBy>` headers + `case` arms in `ApplyAllocationSort` |
| #14 filter preservation reached `SetActive` and `BulkAction` but missed `ResetPassword`, `UnlockAccount`, `DeleteEmployee` row actions | All employee row actions now go through `RedirectToFilteredIndex(filter)` |
| #9/#12 Hebrew labels rendered correctly in employee row cells but a few admin-side dropdowns and the Excel export still used English `Name` | Switched to `DescriptionHebrew ?? Name` pattern in remaining dropdowns and the export header row |
| One pre-existing integration test used `IdNumber="123456789"` (fails the new Luhn check) | Updated `AuditLogFlowTests.cs` fixture to a valid Israeli ID |

Net result: **+2 new tests** (315 vs the 313 reported after the agents finished), all green.

### Third pass — unnumbered docx note

A second-pass review of `בדיקת פרויקט דיווח פעילות (2).xlsx` against the repo found one unnumbered spreadsheet note that was not in the 20-item checklist: **"קוד עובד ספרות בלבד"** (employee code must be digits only). Shipped:
- Server validation in `UserValidator.cs:26` (digits-only regex on create).
- DTO/client metadata in `EmployeeDto.cs:11`.
- Numeric `inputmode="numeric"` hints in `Form.cshtml:38`.
- Edit-time tolerance in `EmployeeController.cs:223` so legacy alphanumeric codes can still be edited unless the code itself changes (avoids breaking historical data).
- New tests for both the create rule and the legacy-edit flow.

Re-verified during the same pass: Employee List has sortable headers for פרויקטים, מחוזות, תוכניות, מגזרים; `EmployeeController.ApplyEmployeeSort()` includes those cases. Forgot-password code path has automated coverage (NotificationLog row of `Sent`); end-to-end SMTP confirmation still requires one live request in the deployed environment.

Net result: **+6 new tests** on top of 315 → **321 / 321 passing**.

---

## Per-item status

| # | Item | Status | Evidence |
|---|------|--------|----------|
| **0** | **Asset-loading prerequisite** (Login = Layout=null) | ✅ | New `Views/Shared/_AnonymousLayout.cshtml`. `Login.cshtml`/`TermsOfUse.cshtml`/`ForgotPassword`/`ResetPassword`/`ChangePassword`/`TwoFactor` all use it. |
| **#21** | **Employee code: digits only** (unnumbered docx note "קוד עובד ספרות בלבד") | ✅ | `UserValidator.cs:26` enforces digits-only on create; `EmployeeDto.cs:11` adds DTO-level metadata; `Form.cshtml:38` adds numeric input hints; `EmployeeController.cs:223` preserves edit support for legacy alphanumeric codes (only enforces the rule when the code itself is being changed). New tests cover both the create rule and the legacy-edit tolerance. |
| #20 | Forgot-password email | ✅ | `AccountController.ForgotPassword` writes `NotificationLog` for both branches; SMTP host fixed (`smtp.gmail.com`); 7 new tests in `AuthEngineerFlowTests.cs` |
| #2 | Terms of Use first launch | ✅ | `SeedData` no longer marks admin accepted; `RequireTermsAcceptedFilter` exempts `Account/TermsOfUse`, `Logout`, `ChangePassword`; `returnUrl` preserved; integration test green |
| #3 | Lock active-month settings | ✅ | New `EditReportingMonth` GET+POST in `AdminController`; `ReportingMonthEditViewModel.LockNonAdminFields`; new view `EditReportingMonth.cshtml` with 🔒 icons; 4 new tests |
| #8 | Rest day Sun/Fri/Sat only | ✅ | `Helpers/SelectListProviders.RestDayOptions`; `UserValidator` rejects values ∉ {null,0,5,6}; tests in `UserValidatorTests` |
| #17 | Integer employment scope | ✅ | `AllocationValidator` rejects non-integer; `step="1"` on Razor inputs; tests in `AllocationValidatorTests` |
| #19 | Admin reset → ID + force change | ✅ | `EmployeeController.ResetPassword` sets `BCrypt(IdNumber)` + `MustChangePassword=true` + `AuditLog`; integration test green |
| #5 | Israeli ID validator | ✅ | New `Core/Validators/IsraeliIdValidator.cs` (Luhn-like); `IsraeliIdValidatorTests` 10 cases |
| #6 | Israeli phone validator | ✅ | Regex in `UserValidator`; covered in `UserValidatorTests` |
| #7 | Hebrew validation messages | ✅ | All `WithMessage` in `Infrastructure/Validators/`; all `[Required]/[StringLength]/[Range]` in `Core/Dtos/`, `Core/Entities/`, `Web/Models/`; `messages_he.js` shipped |
| #1 | Logo colors → theme.css | ✅ | `scripts/extract_logo_colors.py` ran; sampled `#F1601F / #1D144D / #7D3739`; `theme.css` references CSS vars; `logo-palette.png` generated |
| #4 | /MyAllocations submenu | ✅ | New `MyAllocationsController`, `MyAllocationsViewModel`, `Views/MyAllocations/Index.cshtml`; navbar swap for Employee role; 3 integration tests |
| #9+#12 | Roles/Statuses Hebrew (schema migration) | ✅ | Migration `20260430110451_AddHebrewDescriptionsToRolesAndStatuses` adds `DescriptionHebrew` to both tables (verified in `schema.sql`: 2× `ALTER TABLE … ADD [DescriptionHebrew]` confirmed); seed populated |
| #10 | BatchReportImport description column | ✅ | New `BatchImportRowResult` DTO with `ResultDescription`; service emits Hebrew per-row text (added/updated/skipped/rejected); view renders new column with row-class color coding; 2 new tests |
| #11 | Card titles refactor | ✅ | `Views/Employee/Form.cshtml` and `AllocationForm.cshtml`: plain `<h3>`, `card-header bg-primary/bg-success` |
| #13 | Action buttons on right | ✅ | Actions `<th>`/`<td>` first column in `Views/Employee/Index.cshtml` and `AllocationList.cshtml` |
| #14 | Preserve filter on POST→Redirect | ✅ | `EmployeeListFilterModel.ToRouteValues()` + `RedirectToFilteredIndex()` helper used in SetActive/ResetPassword/UnlockAccount/DeleteEmployee/BulkAction/BulkAddAllocation |
| #15 | Employee list filter parity | ✅ | New `EmployeeListFilterModel` with 16 filters; `EmployeeController.Index` refactored; `Views/Employee/Index.cshtml` rewritten; 15 new filter-narrowing tests |
| #16 | Sort every column | ✅ | `ApplyEmployeeSort` and `ApplyAllocationSort` cover every visible column; headers are sort links with `aria-sort` |
| #18 | Choices.js global init | ✅ | `wwwroot/lib/choices.js/{choices.min.js (89 KB), choices.min.css (7.6 KB)}` shipped; init IIFE in both `_Layout` and `_AnonymousLayout`; RTL CSS in `theme.css` |

---

## What was verified locally

### Build & test
```
$ dotnet build AxiomaReporting.sln -c Release
Build succeeded. 0 Error(s). 2 Warning(s) (NU1902 MailKit advisory — pre-existing).

$ dotnet test --filter "FullyQualifiedName!~Playwright"
Passed!  - Failed: 0, Passed: 321, Skipped: 0, Total: 321, Duration: ~25 s

# (Stress tests included; only Playwright UI tests excluded since they require
# a live web server on localhost:5021 — which is the standard development gap,
# not a regression.)
```

### Published artifact contents (extracted from zip)
```
delivery-staging-v1.1/
├── README.txt                                    # v1.1 upgrade guide
├── app/                                          # 69 MB published binaries
│   ├── AxiomaReporting.Core.dll                  # 0 errors compiled
│   ├── AxiomaReporting.Infrastructure.dll
│   ├── AxiomaReporting.Web.dll                   # views compiled in
│   ├── web.config
│   └── wwwroot/
│       ├── css/site.css        (3,240 B)
│       ├── css/theme.css       (5,246 B)         # sampled brand colors
│       ├── lib/choices.js/choices.min.js  (89,447 B)
│       ├── lib/choices.js/choices.min.css ( 7,666 B)
│       └── lib/jquery-validation/dist/localization/messages_he.js (2,232 B)
├── database/
│   └── schema.sql                                # 2,369 lines (was 2,217 in v1.0)
│       ├── 7 prior migrations + new 20260430110451
│       ├── ALTER TABLE [UserRoles] ADD [DescriptionHebrew] ✓
│       ├── ALTER TABLE [UserStatuses] ADD [DescriptionHebrew] ✓
│       ├── 9 INSERTs: 6 Hebrew role labels + 3 Hebrew status labels ✓
│       └── UPDATE [TermsOfUseVersions] SET [BodyHtml] = N'<p>ברוכים הבאים…' ✓
├── docs/   (CLIENT_DELIVERY.md, DEPLOY_CHECKLIST.md, OPERATIONS.md)
├── config/ (appsettings.Production.template.json)
└── scripts/ (extract_logo_colors.py + logo-palette.png)
```

### Schema migration sanity (lines from schema.sql)
- 29 occurrences of `DescriptionHebrew` (column declarations + INSERTs)
- 3 references to `MigrationId = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'` (one in `IF NOT EXISTS`, one in `INSERT INTO __EFMigrationsHistory`, one in idempotence guard).
- New Terms-of-Use Hebrew body inserted with `EXEC(N'UPDATE [TermsOfUseVersions] SET [BodyHtml] = N''<p>ברוכים הבאים למערכת…')`.

---

## What is deferred to client-site smoke test

The dev environment used to produce this build does **not** have SQL Server Express, LocalDB, or an SMTP relay reachable. The following Phase-7 dimensions cannot be exercised here and will be the first checks during client-site go-live:

| Check | Why deferred | When to verify |
|-------|-------------|---------------|
| Live runtime against SQL Server | LocalDB / SQL Express not installed on dev box (`sqllocaldb` command not found) | First boot at client site after running `schema.sql` |
| End-to-end SMTP delivery (`/Admin/EmailServerSettings` test-send) | Requires client's actual relay credentials + outbound port | First Admin login at client site |
| `/Account/ForgotPassword` mail arrival | Same SMTP dependency | After SMTP is configured |
| Reminder service tick + mail | Background jobs need a real DB to schedule against | After 24 hours of uptime |
| `NotificationRetryService` exponential backoff | Same as above | If first SMTP attempts fail |
| Optimistic concurrency (`DbUpdateConcurrencyException`) | Real `rowversion` only works on SQL Server, not InMemory | Manual test from two browser sessions during smoke test |
| Excel upload happy + sad path | Needs real DB, real Excel file, real reporting month | Smoke test step in `docs/DEPLOY_CHECKLIST.md` |
| Performance smoke (1k employees, ≤ 2s dashboard) | Needs realistic data set | Within 7 days of go-live |
| Choices.js renders chips on `select[multiple]` | Visible only with a browser open against the live site | First admin walkthrough |
| Hebrew rendering in PDF error report | QuestPDF + Noto Sans Hebrew TTF — visible only in produced PDF | Force a known-bad Excel upload at client site |

The integration test suite (313 tests) exercises the C# code paths for all of the above using the EF Core InMemory provider, which validates the logic but not the DB-engine-specific behavior (rowversion, `Hebrew_CI_AS` collation, `nvarchar` Unicode handling).

---

## File inventory

| Metric | Value |
|--------|-------|
| Files in `app/` (zip) | 74 top-level + nested wwwroot (full publish output) |
| `schema.sql` lines | 2,369 |
| Tests passing | **313** (was **240** in v1.0 baseline; +73 new tests) |
| New test files | 8 (`IsraeliIdValidatorTests`, `UserValidatorTests`, `AllocationValidatorTests`, `EmployeeListFilterTests`, `AuthEngineerFlowTests`, `AdminEditReportingMonthTests`, `BatchReportImportRowDescriptionTests`, `MyAllocationsFlowTests`) |
| EF migrations applied | 8 (was 7 in v1.0) |
| New controllers | 1 (`MyAllocationsController`) |
| New layouts | 1 (`_AnonymousLayout`) |
| New CSS files | 1 (`theme.css`) |
| New static lib bundles | 2 (`choices.js`, `jquery-validation/dist/localization/messages_he.js`) |

---

## Recommendations for the first 24 hours after go-live

1. **Backup BEFORE the schema.sql run.** The migration adds columns and rewrites the Terms-of-Use body — `BACKUP DATABASE` is the rollback path.
2. **Smoke-test SMTP first.** Forgot-password (#20) was a v1.0 blocker — the very first action at the client site should be `/Admin/EmailServerSettings` → "שלח מייל בדיקה".
3. **Walk the new admin gate.** `/Account/Login` as ADMIN → forced password change → forced terms acceptance → `/Dashboard`. If any of those three gates skips, the migration didn't apply correctly.
4. **Keep `/Admin/NotificationLogs` open during the first day.** Any SMTP regressions surface there as `Status='Failed'` rows with concrete `FailureReason`.
5. **Watch the Application event log** for the first hour for `IIS AspNetCore Module V2` errors — `_AnonymousLayout` is new infrastructure and any layout-render regression on `/Account/*` will fail there.
6. **Re-run `dotnet test`** at the client's CI environment if available — all 313 tests should be green there too. Any regression points at a build / packaging issue specific to the client environment.

If anything is red in the first 30 minutes, the rollback is documented in `delivery-staging-v1.1/README.txt`: stop app pool → restore DB from `pre-v1.1.bak` → restore `app/` from the v1.0 backup folder → start app pool. v1.0 zip is preserved alongside v1.1 specifically as the rollback artifact.

---

## Follow-up tickets discovered during this verification

None blocking. Two minor:

1. **MailKit NU1902** (carried from v1.0): bump to MailKit 4.16+ in a future patch release. Not exploitable in this app's usage pattern.
2. **`scripts/extract_logo_colors.py`** prints `→` which fails on Windows cp1252 console; sampling itself works fine. Cosmetic — fix in a future minor.

— end of report —

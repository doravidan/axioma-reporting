# Implementation Status

Updated: 2026-04-26

## Snapshot

- **Tests:** 240 passing / 0 failing / 0 skipped for the in-process suite (`dotnet test AxiomaReporting.sln --no-build -m:1 --filter "FullyQualifiedName!~UI.Playwright"`).
- **Playwright:** browser suite is separate and requires the web app running at `https://localhost:7021`; without the app it fails with connection refused.
- **Build:** `dotnet build AxiomaReporting.sln` → 0 errors, 2 warnings (pre-existing MailKit NU1902 advisory).
- **Gap plan:** [GAP_IMPLEMENTATION_PLAN.md](GAP_IMPLEMENTATION_PLAN.md) — Gaps 1–11 and 13 implemented. Gap 12 (draft partial-row persistence) deferred per plan.

- **Current build note:** latest verification used `dotnet build AxiomaReporting.sln -m:1`; build passed with 0 errors. Warnings were the pre-existing MailKit NU1902 advisory plus a local MSBuild cache-write warning under `AxiomaReporting.Tests\obj`.

## Completed in Code (2026-04-23, Gap Plan Wave)

- **Gap 1 — Terms of Use versioning.** New `TermsOfUseVersion`/`TermsOfUseAcceptance` entities; `RequireTermsAcceptedFilter` forces acceptance of the latest version on every login; `/Admin/TermsOfUse` lets admins publish new versions (resets everyone's `AcceptedTermsOfUse`).
- **Gap 2 — Employee edit block after deadline.** `ReportController.CanEditReport` now blocks Employee/Coordinator saves, deletes, attachments, submissions, and Excel uploads after `ReportingMonth.LastReportingDate`. Admin/PM keep their override with a visual warning.
- **Gap 3 — Optimistic concurrency.** `RowVersion` on `Report`, `ReportRow`, `User`, `Allocation`; hidden fields round-trip Base64 tokens; `DbUpdateConcurrencyException` returns Hebrew "השורה עודכנה במקביל" message.
- **Gap 4 — Excel upload failure email.** On failed `UploadExcel`, `SendImportFailureEmailAsync` sends the `BatchImportErrors` template (with `{ErrorList}`) to the employee; migration `20260423102722_UpdateBatchImportErrorsTemplate` updates the seed body.
- **Gap 5 — Live cascading dashboard filters.** New `/Dashboard/FilterOptions` JSON endpoint + inline JS in Dashboard and Summary views; bidirectional cascading with toast for invalidated selections.
- **Gap 6 — Persistent notification logs + retry.** `NotificationDispatcher` (new `IEmailService`) writes a `NotificationLog` for every send; `NotificationRetryService` (`BackgroundService`, every 5 min, exponential backoff, abandon after 5 tries); admin screen `/Admin/NotificationLogs` with filters, details modal, and re-send action.
- **Gap 7 — General audit trail.** `IAuditLogService` / `AuditLogService` writes `AuditLog` rows with actor, IP, UA, and JSON before/after. Instrumented: employee CRUD, allocation CRUD, report status transitions, lookup CRUD, auth events (login success/failure/lockout/unlock/password change/reset), admin edits to system constants / email templates / SMTP settings (password redacted) / inspector assignments / terms publish, and terms accept. Admin screen `/Admin/AuditLog` with filters, before/after modal, CSV export.
- **Gap 8 — Client logo.** `IBrandingService` + `SiteLogoViewComponent` drive every logo slot from `SystemConstant.SiteLogoPath`. Default `/images/logo.png` shipped; admin can hot-swap at `/Admin/Branding` (PNG/SVG/JPG, 2 MB). Login + navbar both render via the component.
- **Gap 9 — Complete lookup delete FK checks.** `LookupController.CanDeleteItemAsync` now covers all 17 lookup tables including `frameworks`, `institutions`, `authorities`, `educationalstages`, `educationtypes`, `localitydistrictnational`; Hebrew context strings on block.
- **Gap 10 — Hebrew/RTL PDF.** `PdfReportService` rewritten with QuestPDF + bundled Noto Sans Hebrew; right-aligned RTL layout, two-column error table, page footer.
- **Gap 11 — Special-field UI.** Localities modal supports `NationalCode`; Framework admin UI supports `InstitutionSymbol` + `EducationalStageId` with uniqueness message.
- **Gap 13 — Operations runbook.** See [docs/OPERATIONS.md](docs/OPERATIONS.md) for deployment, backup/restore, SMTP rotation, incident playbooks, monitoring, known limitations.

## Completed in Code (2026-04-23 earlier additions, pre-gap)

- Added `ReportType` lookup with seeded values "ארצי מחוזי" and "יישובי מוסדי"; `ReportRow.ReportTypeId` is now a nullable FK.
- Added `ProjectPrograms` junction table to map programs to projects; cascading filter on the allocation form; admin screen `/Admin/ProjectPrograms` to manage the mapping.
- New `ILookupResolver` service centralizes text→ID lookup resolution for imports (handles numeric IDs and exact descriptions, caches per request, covers all lookup entities including `ReportType`).
- New `IBatchReportImportService` imports an Excel containing reports from many employees. Features: dynamic header-row detection (scans rows 1–15 for "קוד עובד"), whitespace-tolerant Hebrew header aliases, per-row validation via `IReportValidationService`, automatic allocation resolution, find-or-create Report per employee per month, `ReportReceived` email sent once per affected employee.
- Admin-only controller actions + RTL views for `BatchReportImport` upload and result screens; per-employee breakdown and scrollable error table with file row #, employee code, reporter name, and Hebrew error message.
- `IEmailService.SendAsync` now takes an optional `IReadOnlyList<EmailAttachment>` parameter; the batch import errors email attaches the errors PDF directly.
- Two new email templates: `BatchImportSuccessUploader` and `BatchImportErrors`.
- Allocation dashboard (`/Employee/AllocationList`) rewritten with a full filter bar (project, program, district, sector, id, code, first name, last name, monthly/annual scope, output duration, notes), a "הצג הכל" toggle for multi-value columns, and a per-row pencil icon for navigation to the allocation detail screen. Excel export respects the new filters.
- New migration `20260423093119_AddReportTypeAndProjectPrograms`.

## Completed in Code

- Report mutations now enforce user ownership, staff permissions, inspector scope, and editable report status before save, delete, submit, approve, reject, or attachment changes.
- Report rows now load existing values into the edit modal before saving.
- Report-row attachments are visible in the report grid and support upload/delete with file type and size checks.
- Per-allocation validation is enforced for monthly and annual row limits.
- Per-allocation monthly hour validation is enforced against `MonthlyEmploymentScope` in row, submit, and Excel import paths.
- Allocation `OutputDuration` values are enforced by validation and shown as selectable duration options in the report form.
- Report date validation now rejects dates after the reporting month unless future reporting is explicitly allowed.
- Required report fields are controlled by the `RequiredReportFields` system constant for forward-only developer-level changes.
- Daily duration validation is scoped to the row allocation; a null allocation daily scope means unlimited, otherwise it falls back to `MaxDailyHoursDefault` only when no allocation scope exists.
- Duplicate detection now compares the full row identity, including allocation, duration, subject 2, discussion code, conclusion fields, grade level, and class.
- Dashboard filtering now starts from scoped employees/allocations, supports "Not Yet Reported" including Draft/In Entry reports, and applies district/sector/program filters to allocation context.
- Bulk approval now checks report access before approving each posted report id.
- Reminder sending now uses `ReminderLogs` and honors `ReminderIntervalDays`.
- Email-based TFA is implemented behind `TfaEmailEnabled`; SMS is not used for login/TFA.
- Self-service forgot-password is implemented with a time-limited email reset link.
- Email token replacement supports both `{Token}` and `{{Token}}`, and report/reminder tokens include month, year, and deadline values.
- Employee-level attachments are implemented on the employee edit screen with upload/delete validation.
- Lookup table Excel import is implemented for admins from each lookup list screen.
- Excel import validation can produce a downloadable PDF error report.
- Admin/PM allocation Excel upload is implemented from the employee allocations screen with selected reporting month support.
- Inspector assignment management is implemented for admins with AND-within-row, OR-across-rows scope rules.
- `AllocationLocalityDistrictNational` is now exposed in allocation DTOs, forms, service loading, and junction sync.
- Report conclusion framework/location fields now have EF navigation properties, FK constraints, and allocation-scoped dropdown/display behavior.
- Report form visual required markers now follow the `RequiredReportFields` system constant.
- Report Excel upload includes a downloadable `.xlsx` template with the expected columns.
- Successful report Excel imports send a confirmation email using the report-received template.
- Employee list now includes Blue Card fields, allocation multi-values, project filtering, sorting, page-size selection, locked indicator, notes, and expanded Excel export.
- Admin/PM can explicitly unlock a locked user account from the employee list.
- Admin/PM can soft-delete employees by deactivating them.
- A global allocation list screen and allocation Excel export are implemented.
- Bulk selected-employee allocation creation is implemented for Admin/PM.
- Dashboard and summary screens apply requested sorting, expose sortable headers, and summary has its own Excel export.
- Dashboard export is restricted to approved reports for both inspector roles.
- Initial data migration imports lookup tables, frameworks, employees, institutions, and allocations from Excel.
- The provided questionnaire catalog `.xlsx` can be imported directly from the admin data-migration screen using the `כללי - מאוחד` sheet, including the `מסגרת חינוכית` conclusion values.
- The provided `טבלאות.xlsb` lookup file can be imported directly from the admin data-migration screen.
- The provided `BASE DATA.xlsb` historical report file is mapped to the one-time Python seed script in `database/seed-data`; it assigns `AllocationId` when the historical row maps to exactly one active allocation.
- Project managers can no longer assign the system-admin role.
- Employee Excel import is wired from the report screen for allocations with `AllowExcelUpload`; it replaces only editable rows for that allocation and validates imported rows before save.

## Database Changes

- Added `ReminderLogs` entity, EF configuration, `AppDbContext` DbSet, and migration `20260412134615_AddReminderLogs`.
- Added `PasswordResetTokens` and `TwoFactorCodes` entities, EF configurations, `AppDbContext` DbSets, seed data for `TfaEmailEnabled`, `PasswordReset`, `TwoFactorCode`, and migration `20260412154401_AddAccountRecoveryAndEmailTfa`.
- Added `RequiredReportFields` seed data and ReportRow conclusion FK/index migration `20260412164437_AddReportRequiredFieldsAndConclusionRelations`.
- Added `ReportType` lookup table, `ProjectPrograms` junction table, and `ReportRow.ReportTypeId` nullable FK; seeded 2 `ReportType` rows and 2 new `EmailTemplate` rows (`BatchImportSuccessUploader`, `BatchImportErrors`). Migration `20260423093119_AddReportTypeAndProjectPrograms`.
- Added `TermsOfUseVersion`, `TermsOfUseAcceptance`, `NotificationLog`, `AuditLog` entities plus `RowVersion` columns on `Report`, `ReportRow`, `User`, `Allocation` (optimistic concurrency). Migration `20260423101845_AddTermsAuditNotificationLogsAndConcurrency`.
- Updated `BatchImportErrors` email template body to include `{ErrorList}` token. Migration `20260423102722_UpdateBatchImportErrorsTemplate`.
- Added `SystemConstant` key `SiteLogoPath` with default `/images/logo.png`. Migration `20260423102908_AddSiteLogoPathConstant`.

## Excel Import Template

The implemented Excel import expects `.xlsx` with headers in row 1 and data starting on row 2. Values are IDs for lookup fields.

| Column | Field |
|--------|-------|
| A | MeetingDate |
| B | MeetingDuration |
| C | DistrictId |
| D | LocalityId |
| E | FrameworkId |
| F | EducationalProgramId |
| G | DomainId |
| H | Subject1Id |
| I | Subject2Id |
| J | DiscussionCodeId |
| K | ConclusionClassId |
| L | ConclusionFrameworkId |
| M | ConclusionLocationId |
| N | GradeLevelId |
| O | ClassId |
| P | Notes |

## Client Data Files

See `DATA_IMPORT_MAPPING.md` for the reviewed mapping of:

- `טבלאות.xlsb`
- `BASE DATA.xlsb`
- `קובץ משותף שאלונים לכל התוכניות 12.3.26.xlsx`

The normalized MVC upload screens accept `.xlsx` only. The client `טבלאות.xlsb` workbook has a dedicated admin import action. The historical `BASE DATA.xlsb` workbook is handled by `database/seed-data/seed_reports.py`.

## Remaining External Decisions

- TFA decision is closed: login TFA uses email only. SMS remains out of scope for TFA.
- SMS reminders remain out of scope unless the client later selects a provider and approves the additional implementation.
- Single-allocation monthly report Excel upload still expects lookup IDs. The initial data migration import and the batch multi-employee import both support lookup IDs or exact descriptions via `ILookupResolver`.
- Client-final Terms of Use text and real logo asset — placeholder and admin-managed swap in place.
- Historical report bulk import (Q13) — client still owes separate spec; the existing batch-import feature already handles their `ריכוז כולל` workbook format after the ClosedXML workaround.

## Known Limitations

Documented fully in [docs/OPERATIONS.md §9](docs/OPERATIONS.md):

1. **ClosedXML v0.105 data-validation bug.** Files with data-validation list strings > 255 characters (e.g. `0000ריכוז כולל...xlsx`) throw on import. Workaround: re-save in Excel or upgrade once fixed upstream.
2. **Batch-import allocation resolution is pragmatic.** Exactly-one-match wins; rows with zero/multiple matches become import errors. Adjust via single-method change in `BatchReportImportService.ResolveAllocation`.
3. **`NotificationLog` does not persist attachments.** Retrying a failed `BatchImportErrors` email re-sends body only; body already contains full text error list.
4. **`ReminderLogs` coexists with `NotificationLogs`.** Do not drop `ReminderLogs` — it is the dedupe source for the daily reminder job.
5. **EF InMemory provider ignores rowversion.** Production SQL Server enforces it normally.

## Deferred / Optional

| Item | Status |
|------|--------|
| Gap 12 — Draft partial-row persistence | Deferred per plan; current Draft behavior is acceptable |
| Email template rich-text editor | Plain textarea acceptable for now |
| `AuditLogs` automated archival job | Add only when table exceeds ~1 M rows (see OPERATIONS.md §8) |

## Latest Traceability Audit

See `SPEC_TRACEABILITY_AUDIT.md` for the full spec-to-code audit. After this cycle, all high/medium priority items from the plan are implemented; remaining work is client-dependent (final terms text, real logo asset) or deferred (Gap 12, rich-text editor, archival job).

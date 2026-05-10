# Spec Traceability Audit

Updated: 2026-04-26

This audit compares `SPEC.md`, `IMPLEMENTATION_PLAN.md`, `prd.json`, and the current code under `src/`.

## Verification Performed

- Reviewed spec and implementation-plan requirements across authentication, employees, allocations, reporting, Excel import/export, dashboard, reminders, lookup tables, and admin settings.
- Checked controllers, services, entities, EF configurations, migrations, and views.
- Ran automated tests.
- Reviewed the three client-provided seed/catalog files in `database/seed-data` and mapped them to implemented import paths.

Verification command:

```powershell
dotnet test AxiomaReporting.sln --no-build -m:1 --filter "FullyQualifiedName!~UI.Playwright"
```

Result: 240 passed, 0 failed. The Playwright browser suite is separate and requires the web app to be running at `https://localhost:7021`.

Unit coverage gate:

```powershell
.\scripts\test-unit-coverage.ps1
```

Result: 89.24% line coverage on the deterministic service layer, above the 80% threshold.

## Implemented And Aligned

| Area | Status | Evidence |
|------|--------|----------|
| MVC/Razor architecture | Implemented | `Program.cs`, `Controllers/`, `Views/` |
| Email-only TFA | Implemented | `AccountController`, `TwoFactorCodes`, `TfaEmailEnabled` |
| Forgot password/reset link | Implemented | `PasswordResetTokens`, `AccountController.ResetPassword` |
| Password lockout after 3 failures | Implemented | `AuthService` |
| Password expiry/history/strength | Implemented | `PasswordService`, `AuthService` |
| First-login terms then password change | Implemented behavior | `AccountController.PostLoginRedirectAsync` |
| Role-based menus and policies | Implemented | `PolicyNames`, `Program.cs`, `_Layout.cshtml` |
| One allocation per employee/project | Implemented | unique index in `AllocationConfiguration` |
| Allocation-scoped report rows | Implemented | `ReportRows.AllocationId`, validation/import save paths |
| Employee and allocation CRUD basics | Implemented | `EmployeeController`, `EmployeeService` |
| Employee-level attachments | Implemented | `DocumentAttachments.UserId`, employee edit view |
| Report-row attachments | Implemented | `ReportController.UploadAttachment`, report view |
| Report status workflow basics | Implemented | draft, in-entry, pending, approved, returned |
| Rejection preserves rows for correction | Implemented | status 5 stays editable |
| Levenshtein notes similarity | Implemented | `ReportValidationService` |
| Daily hour limit per allocation | Implemented | `DailyEmploymentScope`, fallback `MaxDailyHoursDefault` |
| Unlimited daily allocation scope | Implemented | null `DailyEmploymentScope` on an allocation is treated as unlimited |
| Output duration options | Implemented | allocation `OutputDuration` drives report input options and validation |
| Monthly/annual row limit per allocation | Implemented | `ReportValidationService.ValidateSubmitAsync` |
| Date validation month boundary | Implemented | `ReportValidationService.ValidateRowAsync`, Excel import uses row validator |
| Monthly hour allocation validation | Implemented | per-allocation `MeetingDuration` validation in row and submit paths |
| Developer-level required report fields | Implemented | `RequiredReportFields` system constant read by `ReportValidationService` |
| Report-row conclusion relationships | Implemented | `ConclusionFramework` and `ConclusionLocation` navigations/FKs |
| Allocation-scoped report dropdowns | Implemented | report view uses selected allocation junction values for grade/class/conclusion fields |
| Employee list spec expansion | Implemented | Blue Card fields, allocation multi-values, project filter, sorting, page size, export |
| Bulk employee allocation change | Implemented | `EmployeeController.BulkAddAllocation` |
| Global allocation list/export | Implemented | `EmployeeController.AllocationList`, `ExportAllocationsExcel` |
| Account unlock UX | Implemented | `EmployeeController.UnlockAccount`, employee list action |
| Excel template download | Implemented | `ReportController.DownloadExcelTemplate` |
| Dashboard sorting | Implemented | `DashboardFilterService` applies `SortBy`/`SortDesc`, views expose sortable headers |
| Summary Excel export | Implemented | `DashboardController.SummaryExportExcel` |
| Employee Excel upload for active month | Implemented | `ReportController.UploadExcel` |
| Admin/PM employee-month Excel upload | Implemented | `EmployeeController.UploadAllocationExcel` |
| PDF export for Excel errors | Implemented minimally | `PdfReportService` |
| Dashboard filters and not-yet-reported | Implemented | `DashboardFilterService` |
| Not-yet-reported includes Draft/In Entry | Implemented | status 0 filter includes no report, Draft, and In Entry |
| Dashboard Excel export | Implemented | `DashboardController.ExportExcel` |
| Summary screen and bulk approval | Implemented | `DashboardController.Summary`, `BulkApprove` |
| Reminder scheduler and reminder log | Implemented | `ReminderService`, `ReminderLogs` |
| Lookup CRUD and simple lookup Excel import | Implemented for many simple lookup tables | `LookupController` |
| Framework/institution uniqueness checks | Implemented | `AdminController` |
| Inspector assignments | Implemented | `AdminController.InspectorAssignments` |
| Inspector export restriction | Implemented | both inspector roles export approved reports only from the dashboard |
| Initial data migration/import tool | Implemented | `AdminController.DataMigration`, lookup/employee/institution/allocation import actions |
| Client questionnaire catalog import | Implemented | `AdminController.ImportQuestionnaireCatalog`, including column H conclusion framework values |
| Provided `.xlsb` seed files | Implemented as one-time scripts | `database/seed-data/seed_lookups.py`, `database/seed-data/seed_reports.py` |
| Direct client lookup `.xlsb` admin import | Implemented | `AdminController.ImportClientLookupXlsb` for `טבלאות.xlsb` |
| Employee delete/deactivation | Implemented | soft deactivation through `EmployeeController.DeleteEmployee` |
| Excel upload success email | Implemented | successful report Excel import sends `ReportReceived` email |
| SMTP settings and email templates | Implemented | `AdminController`, `EmailService` |

## Functional Gaps

After the 2026-04-23 gap-plan wave, the table below reflects current state. See [GAP_IMPLEMENTATION_PLAN.md](GAP_IMPLEMENTATION_PLAN.md) for how each item was addressed.

| Priority | Gap | Current State |
|----------|-----|---------------|
| ✅ Closed | Client logo | `IBrandingService` + `SiteLogoViewComponent` drive every logo from `SystemConstant.SiteLogoPath`; `/Admin/Branding` hot-swap. |
| ✅ Closed | Terms of Use versioning | `TermsOfUseVersion` + `TermsOfUseAcceptance` + `RequireTermsAcceptedFilter` force re-acceptance on new version; `/Admin/TermsOfUse` publishes. |
| Accepted | TFA scope | System-wide `TfaEmailEnabled` is the confirmed client decision. |
| Open (client) | Employee report Excel lookup values | Monthly report Excel still expects numeric IDs; batch multi-employee import resolves text/codes via `ILookupResolver`. |
| ✅ Closed | PDF error report quality | `PdfReportService` rewritten on QuestPDF + Noto Sans Hebrew; right-aligned RTL table. |
| ✅ Closed | Lookup UI special fields | Locality `NationalCode` and Framework `InstitutionSymbol`/`EducationalStageId` editable in UI. |
| ✅ Closed | Lookup delete checks | `LookupController.CanDeleteItemAsync` covers all 17 tables. |
| ✅ Closed | Dashboard cascading behavior | `/Dashboard/FilterOptions` JSON endpoint + live client-side cascading on Dashboard and Summary. |
| Deferred | Draft partial row saving | Gap 12 — current whole-report Draft behavior is acceptable per plan. |
| ✅ Closed | Email failure audit | `NotificationLog` table + `NotificationDispatcher` writes + `NotificationRetryService` retries + `/Admin/NotificationLogs`. |
| Low / Open | Email template editor | Plain textarea acceptable; rich-text editor deferred. |
| ✅ Closed | Optimistic concurrency | `RowVersion` on `Report`, `ReportRow`, `User`, `Allocation`; Hebrew conflict message. |
| ✅ Closed | General audit trail | `AuditLog` + `AuditLogService` + instrumentation across sensitive paths + `/Admin/AuditLog` with CSV export. |
| ✅ Closed | Operational runbook | [docs/OPERATIONS.md](docs/OPERATIONS.md). |
| ✅ Closed | Employee edit block after deadline | `ReportController.CanEditReport` blocks Employee/Coordinator after `LastReportingDate`; Admin/PM override. |
| ✅ Closed | Excel upload failure email | `SendImportFailureEmailAsync` sends `BatchImportErrors` template with `{ErrorList}` on every failed import. |

## Explicitly Out Of Scope Or Client-Dependent

| Item | Status |
|------|--------|
| SMS for TFA | Closed: email-only TFA. |
| SMS reminders | Future optional add-on only if provider/scope approved. |
| Bulk upload for all employees/all programs/month X | Spec says separate quote; not included in current implementation. |
| Client SITE logo asset | Waiting for client-provided file. |
| Client final Terms of Use text | Waiting for client-provided text/decision on versioning. |
| Client initial Excel formats | Reviewed and mapped in `DATA_IMPORT_MAPPING.md`. `טבלאות.xlsb` and questionnaire `.xlsx` have admin import actions; `BASE DATA.xlsb` remains a historical seed script. |

## Documentation Drift Found

- `IMPLEMENTATION_PLAN.md` final checklist still marks some implemented items as unchecked or partial.
- `IMPLEMENTATION_STATUS.md` is closer to the code but should reference this audit for remaining gaps.
- `prd.json` still lists some future/phase-complete expectations as acceptance criteria even though they are not fully implemented.

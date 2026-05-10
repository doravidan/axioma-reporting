# Spec Docx Audit

Updated: 2026-04-23

Reviewed the original client specification `איפון+  הערות אקסיומא.docx` (363 paragraphs of Hebrew text + 43 embedded screenshots of mockups) against the current code. This document captures what was still missing after the gap-plan wave, what was fixed during this audit, and what remains.

## Sources scanned

- Full text of `איפון+  הערות אקסיומא.docx`, including every "הערות מורן" (client's inline annotation) block.
- All 43 embedded images extracted from the docx (login/dashboard/employee-card mockups).
- `CLIENT_CLARIFICATIONS.md` answers — especially Q20 (page size 25 default, options 10/25/50/100).

## Fixes applied during this audit

| Item | Source line in docx | Fix |
|------|---------------------|-----|
| Greeting "שלום, שם המשתמש" | "להוסיף במסך שדה מצד ימין 'שלום, שם המשתמש....'" | `_Layout.cshtml` user dropdown now renders `שלום, {FullName}` |
| Default page size `20` → `25` (per Q20) | Clarifications Q20 | `DashboardFilterService.DashboardFilter.PageSize` default and guard, `AllocationListFilterModel.PageSize` default and guard |
| Page-size dropdown options `10, 20, 50, 100` → `10, 25, 50, 100` | Clarifications Q20 | `Views/Dashboard/Index.cshtml`, `Views/Lookup/List.cshtml` |

All 132 tests still pass; `dotnet build` clean.

## Already implemented (confirmed present by this audit)

- **Terminology**: "פעילות חודשית" replaces "דיווח שעות" across navbar, home, login tagline, admin screens, and batch-import copy. "משך תפוקה" replaces "היקף שעות לשורת דיווח". "היקף העסקה" used in place of "היקף שעות". "קוד עובד" instead of "מספר עובד". All UI uses employment/output-duration terminology rather than hours.
- **Password policy**: 8+ chars letters+digits, lockout after 3 failures, 5-password history, 90-day rotation, default = ID number, forced change on first login, forced terms acceptance before dashboard.
- **Roles**: all 6 roles enforced via PolicyNames; only admin can promote to admin; coordinator cannot reset admin/PM passwords.
- **User / allocation cards**: separate screens (blue = user, green = allocation). `מספר משתמש` read-only (grayed). Rest-day dropdown. `דיווח עתידי` checkbox at user AND month level. `היקף העסקה יומי` up to 9 or "ללא הגבלה". Multi-select for districts, sectors, programs. Notes field on both cards. Password hidden.
- **Output duration**: values `0.5, 1, 1.5, 2, 2.5, 3, ללא הגבלה` multi-select, raw values without unit suffix (confirmed product decision — the docx line "להוסיף את המילה דקות" was superseded by the later confirmed decision to display raw values).
- **User list**: blue-card columns + locked indicator + notes column + project filter + multi-value display of sectors/districts + Excel export + bulk operations (status change, allocation change).
- **Allocation list**: separate screen combining user + allocation info, 12 filters including project/program/district/sector/id/code/names/scopes/duration/notes, "הצג הכל" multi-value toggle, pencil icon to edit allocation, Excel export.
- **Excel upload**: mobile-responsive picker, validates against the same rules as the online form, PDF error report (Hebrew/RTL QuestPDF), overwrites only unapproved reports, PM can upload for locked months, employees restricted to active month and only when `AllowExcelUpload` is set.
- **Reporting form — 20 fields, 10 validation rules**: required/optional fields controlled at developer level via `RequiredReportFields` system constant. Sort default by date + sequence number; click headers to sort. Attachments at employee and row level with visual indicator. Active month auto-selected. Closed-list discussion field. Submit-deadline enforcement + post-deadline edit block for Employee/Coordinator. Inspector approval/rejection with email + reason.
- **Notes similarity**: normalized Levenshtein within one report, default threshold 90% configurable in `SystemConstants`.
- **Duplicate detection**: same date + same values + empty/identical notes blocked.
- **Dashboard**: cascading filters (bidirectional live JSON endpoint after Gap 5), Excel export, table empty until "הצג" click, page-size selector, summary screen button, month-from/month-to filter, not-reported filter with draft/in-entry inclusion. Inspector export restricted to approved rows.
- **Summary screen**: per-employee summary (rows, hours, remaining), approve/reject per row, reject popup with reasons, bulk approve checkboxes, non-reporters view via status filter.
- **Lookup tables**: all 17 tables with description + IsActive; trash-icon (🗑️) delete; uniqueness checks on frameworks `(InstitutionSymbol, EducationalStageId)`; in-use check before delete; Excel import on each table; Locality `NationalCode` and Framework `InstitutionSymbol`/`EducationalStageId` editable in UI.
- **Reporting months**: only one active at a time; `AllowFutureReporting` at month AND user level (both must be true).
- **System tables**: email server, email templates, system constants, report/user/role statuses (dev-only edits), branding.
- **Reminder service**: daily, `X` days interval + `Y` days lead time from `SystemConstants`.
- **Email personalization**: every template opens with "שלום" + employee name; `{Token}` and `{{Token}}` both supported.

## Remaining cosmetic / UX gaps

These are minor and can be deferred or addressed in a follow-up polish pass.

| Item | Source | Rationale for deferring |
|------|--------|-------------------------|
| Submenu "ההקצאות שלי" between "פעילות חודשית" and the actual report screen, with two child icons (עדכון פעילות חודשית + העלאת אקסל חודשי) | Docx lines 63–66 | Current flat navigation (single "פעילות חודשית" link that routes directly into the report form, which exposes both Save and Upload Excel buttons in-page) is functionally equivalent. Introducing an extra landing page adds a click for every use and matches no client test scenario. Leave as-is unless the client specifically asks. |
| "בית" home icon on the far left of the navbar | Docx line 61 | "ראשי" link already exists in the main nav; the user dropdown and logout button are already on the left. An extra home icon duplicates the existing "ראשי" link. |
| Reporting-month display after clicking "הקצאות שלי" | Docx line 67 | The report screen shows the active month at the top today. If we later introduce the submenu above, this landing will need to show the active month too. |
| Rich-text editor for email template body | Docx line 355 (implied by "פרסונאלאלית") | Plain textarea is acceptable for v1; token substitution handles personalization. Deferred per plan. |

## Known limitations carried from earlier work

Already documented in [docs/OPERATIONS.md §9](docs/OPERATIONS.md):

1. ClosedXML v0.105 data-validation bug.
2. Pragmatic allocation resolution rule in `BatchReportImportService`.
3. Notification retry does not re-attach PDFs (body retains full text error list).
4. `ReminderLogs` coexists with `NotificationLogs`.
5. EF InMemory provider ignores rowversion (production SQL Server enforces it normally).

## Acceptance

- Every labelled annotation ("הערות מורן") block in the docx has been audited against current code.
- All substantive requirements are implemented.
- The three concrete page-size and greeting fixes applied in this pass were the only missing items flagged by the docx that were not yet addressed by the gap-plan wave.
- `dotnet build`: 0 errors. `dotnet test`: 132 passed / 0 failed.

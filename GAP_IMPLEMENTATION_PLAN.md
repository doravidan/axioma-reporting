# Gap Implementation Plan — Post CLIENT_CLARIFICATIONS Answers

**Date:** 2026-04-23
**Source:** [CLIENT_CLARIFICATIONS.md](CLIENT_CLARIFICATIONS.md) (all 21 questions answered) cross-checked against current `src/` and [SPEC_TRACEABILITY_AUDIT.md](SPEC_TRACEABILITY_AUDIT.md).

This plan lists only the items that still require work. Items already implemented are summarized in [IMPLEMENTATION_STATUS.md](IMPLEMENTATION_STATUS.md) and not repeated here.

---

## Already Aligned With Clarification Answers (no work needed)

| # | Client Answer | Evidence in Code |
|---|---------------|------------------|
| Q1 | Unlock by Coordinator+ | [EmployeeController.UnlockAccount](src/AxiomaReporting.Web/Controllers/EmployeeController.cs#L199) uses `AdminPMOrCoordinator` |
| Q2 | Password reset by Coordinator+ | [EmployeeController.ResetPassword](src/AxiomaReporting.Web/Controllers/EmployeeController.cs#L178) uses `AdminPMOrCoordinator` |
| Q4 | Daily allocation scope enforced; unlimited clears limit | `DailyEmploymentScope` logic in `ReportValidationService` |
| Q5 | Use default multi-value display (until client sends mock) | Existing list views |
| Q6 | Future reporting requires month **and** user checkbox | `AllowFutureReporting` on `ReportingMonths` and `Users` |
| Q8 | "Not reported" is per-allocation | `DashboardFilterService` |
| Q10 | Excel format per client examples | `DATA_IMPORT_MAPPING.md` |
| Q12 | Client-format importers | `AdminController.ImportClientLookupXlsb`, `ImportQuestionnaireCatalog` |
| Q13 | Historical report import is out of current scope | Waiting on client spec |
| Q15 | No bulk rejection | Reject action is per-report |
| Q16 | Inspector assignments by Admin+PM | `AdminController.InspectorAssignments` uses `AdminOrPM` |
| Q20 | 25 default, options 10/25/50/100 | Page-size selector in list views |

---

## Gap 1 — Terms of Use Versioning And Re-Acceptance (Q3)

**Priority:** High (client explicitly requires version control and re-acceptance)

**Current state:** `User.AcceptedTermsOfUse` is a single boolean. The terms text is a static placeholder. There is no version table, no acceptance history, no re-prompt on change.

**Work:**
1. New entities:
   - `TermsOfUseVersion` { `Id`, `VersionNumber` (int, monotonic), `BodyHtml` (nvarchar(max)), `EffectiveFrom` (datetime2), `PublishedBy` (FK Users), `CreatedAt` }.
   - `TermsOfUseAcceptance` { `Id`, `UserId` (FK), `VersionId` (FK), `AcceptedAt`, unique(UserId,VersionId) }.
2. EF configurations + migration.
3. Modify first-login flow in `AccountController.PostLoginRedirectAsync`: require acceptance of the latest published version; block access until accepted.
4. Remove `User.AcceptedTermsOfUse` (or keep as denormalized cache of "accepted latest").
5. Admin screen under Admin/System: list versions, publish new version (rich-text editor; minimum plain textarea if editor is deferred).
6. Seed an initial version (placeholder text) so existing users auto-accept on first migration.

**Acceptance:** Publishing a new version forces every user's next login into the terms acceptance screen before any other page.

---

## Gap 2 — Block All Employee Edits After Deadline (Q7)

**Priority:** High

**Current state:** `CanEditReport` in [ReportController.cs:432](src/AxiomaReporting.Web/Controllers/ReportController.cs#L432) checks role and status, but does **not** block employees from editing Draft / In Entry / Returned reports after `ReportingMonth.LastReportingDate`. Only the submit path is blocked (in `ReportValidationService.ValidateSubmitAsync`).

**Work:**
1. Extend `CanEditReport` to load the `ReportingMonth.LastReportingDate`; if `today > LastReportingDate` and user is Employee or Coordinator → return false (Admin and PM keep the override).
2. Surface a Hebrew message "המועד האחרון לדיווח עבר — ניתן לעדכן רק דרך מנהל פרויקט או מנהל מערכת" in the report screen when block is active.
3. Apply the same guard to `UploadExcel`, `UploadAttachment`, and row Save / Delete endpoints.
4. Add unit test for the post-deadline branch in each role.

**Acceptance:** After the `LastReportingDate`, an Employee cannot save, edit, delete, upload attachments, or upload Excel. An Admin/PM still can.

---

## Gap 3 — Optimistic Concurrency And Conflict Message (Q9)

**Priority:** Medium

**Current state:** No `RowVersion` tokens on any entity.

**Work:**
1. Add `byte[] RowVersion` (IsRowVersion) to `Report`, `ReportRow`, `User`, `Allocation` entities.
2. EF Fluent configuration + migration (SQL Server `rowversion`).
3. Pass the token through report view forms (hidden input) and row Ajax calls.
4. Catch `DbUpdateConcurrencyException` in `ReportController` and `EmployeeController` save paths; return Hebrew error "השורה עודכנה במקביל על ידי משתמש אחר. יש לרענן ולנסות שוב." and a refresh prompt.
5. Integration test: simulate two saves from the same base version → second one fails with the expected message.

**Acceptance:** Concurrent editing of a single report row surfaces a non-500 conflict message and the second writer does not silently overwrite.

---

## Gap 4 — Excel Upload Failure Email (Q11)

**Priority:** Medium

**Current state:** On `UploadExcel` failure ([ReportController.cs:351](src/AxiomaReporting.Web/Controllers/ReportController.cs#L351)) errors are only shown on-screen and as a downloadable PDF. The `BatchImportErrors` email template is seeded but never sent.

**Work:**
1. After a failed `UploadExcel`, if the employee has an email address, enqueue an email using the `BatchImportErrors` template with a formatted error list (row number + message).
2. Template tokens already exist; add `{ErrorList}` token if missing.
3. Route through `EmailService` so it participates in the NotificationLogs from Gap 6.
4. Unit test: failed import produces a queued email with the expected body.

**Acceptance:** When a user uploads a bad Excel file, they receive an email containing the same detailed error list shown on screen.

---

## Gap 5 — Dashboard Cascading Filters JSON Endpoints (Q14)

**Priority:** Medium

**Current state:** Filters cascade only after a full page submit. Q14 confirmed bidirectional behavior — should feel live.

**Work:**
1. Add `GET /Dashboard/FilterOptions?selected={json}` returning JSON `{ districts:[], sectors:[], programs:[], employees:[] }` computed from the current selection.
2. Reuse existing scoping logic in `DashboardFilterService` (extract a method that, given a partial `DashboardFilter`, returns the compatible option lists per field).
3. Wire `/Dashboard/Index` page to fetch on `change` of each filter (debounced) and repopulate dropdowns without losing user selection when still valid.
4. Apply the same endpoint to the Summary screen filters.

**Acceptance:** Changing any filter updates every other filter's option set without a page reload, and currently-selected-but-now-invalid values are cleared.

---

## Gap 6 — Persistent NotificationLogs + Retry (Q17, Q18)

**Priority:** High

**Current state:** `ReminderLogs` exists for the daily reminder job only. General email send/failure is logged to app logs, not a DB table, and there is no retry.

**Work:**
1. New entity `NotificationLog` { `Id`, `NotificationType` (enum: Report, Reminder, Account, etc.), `TemplateType`, `RecipientUserId?`, `RecipientEmail`, `RelatedReportId?`, `RelatedReportingMonthId?`, `Subject`, `Body`, `Status` (Pending/Sent/Failed), `AttemptCount`, `LastAttemptAt`, `NextRetryAt?`, `FailureReason?`, `CreatedAt` }.
2. Replace direct `EmailService.SendAsync` call sites with a queue-then-send pattern: write a Pending `NotificationLog` row, then attempt send, update row with result.
3. New `IHostedService` `NotificationRetryService`: every 5 minutes, pick Pending/Failed rows where `AttemptCount < MaxAttempts` (constant, e.g., 5) and `NextRetryAt <= now`; retry with exponential backoff.
4. Admin screen `/Admin/NotificationLogs` with filters (type, status, date range, recipient) and re-send action.
5. Migrate existing `ReminderLog` write to also append to `NotificationLogs` (keep `ReminderLogs` for the "already sent today" check to avoid changing that semantics).

**Acceptance:** Every email send is recorded. Failed sends are retried automatically; Admin can inspect the log and manually resend.

---

## Gap 7 — General Audit Trail (Q21)

**Priority:** High

**Current state:** Only `ReminderLogs` exist. There is no audit of sensitive changes.

**Work:**
1. New entity `AuditLog` { `Id`, `Timestamp`, `ActorUserId`, `Action` (string enum), `EntityType`, `EntityId`, `Before` (json), `After` (json), `IpAddress`, `UserAgent`, `Notes` }.
2. `IAuditLogService.LogAsync(action, entityType, entityId, before, after, notes)`. Inject `IHttpContextAccessor` for IP/UA.
3. Instrument:
   - `EmployeeService` — Create, Edit, Deactivate, Reactivate.
   - Allocation changes (create, update, add/remove lookup junctions).
   - `ReportStatusService` — every status transition, including admin overrides.
   - `LookupController` — create/update/delete on each base table.
   - `PasswordService` / `AuthService` — password reset (self or admin), lockout, unlock, failed login burst.
   - `AdminController` — system-constant and template edits.
4. Admin screen `/Admin/AuditLog` with filters (actor, action, entity type, date range), paginated, CSV/Excel export.
5. Retention policy: keep all rows; add index on `(Timestamp DESC)` and `(EntityType, EntityId)` for fast lookup.

**Acceptance:** Any sensitive change writes an `AuditLog` row; the admin screen shows before/after for user-directed actions.

---

## Gap 8 — Client Logo Integration (Q19)

**Priority:** Medium

**Current state:** [Login.cshtml:24](src/AxiomaReporting.Web/Views/Account/Login.cshtml#L24) and `_Layout.cshtml` use the text "אקסיומא".

**Work:**
1. Drop the supplied logo file at `src/AxiomaReporting.Web/wwwroot/images/site-logo.png` (and `.svg` if provided).
2. Replace heading text in `Login.cshtml` and `_Layout.cshtml` with `<img src="~/images/site-logo.png" alt="לוגו אקסיומא" class="login-logo-img" />` (keep `aria-label`/`alt` for a11y).
3. Add CSS sizing in `site.css` (max-height 80px on login, 36px in top bar).
4. Admin system-settings screen: upload/replace logo file (store in `wwwroot/uploads/branding/` and persist a `SystemConstant` with the active path).

**Acceptance:** Login and all layout screens show the client's logo at the right resolution; Admin can replace it without a redeploy.

---

## Gap 9 — Complete Lookup Delete FK Checks

**Priority:** Medium

**Current state:** [LookupController.CanDeleteItemAsync:272](src/AxiomaReporting.Web/Controllers/LookupController.cs#L272) covers 11 tables. Still missing:

- `authorities` (used by Localities?).
- `educationalstages` (referenced by Frameworks and Institutions).
- `educationtypes` (referenced by Institutions).
- `localitydistrictnational` (referenced by `ReportRow.ConclusionLocationId` and allocation junction).
- `frameworks` (referenced by `ReportRow.FrameworkId`, `ReportRow.ConclusionFrameworkId`, `AllocationFramework`, and `Institutions.EducationalStageId` pair).
- `institutions` (referenced by Frameworks via `InstitutionSymbol` + `EducationalStageId`).

**Work:** Add each missing `AnyAsync` check in `CanDeleteItemAsync`; add a unit test per branch using an in-memory provider.

**Acceptance:** Attempting to delete any in-use lookup value returns the Hebrew "הערך בשימוש" message instead of a raw FK exception.

---

## Gap 10 — Hebrew/RTL PDF Error Report Quality

**Priority:** Medium

**Current state:** `PdfReportService` uses a minimal writer that does not reliably render Hebrew or right-align.

**Work:**
1. Replace with QuestPDF (MIT/community) targeting .NET 8.
2. Register a Hebrew-capable font (David, Alef, or Frank Ruehl — bundle in `wwwroot/fonts` and load via QuestPDF `TextStyle.FontFamily`).
3. Template: header with logo + file name + timestamp, table "שורה / עמודה / הודעת שגיאה" right-to-left, footer with page numbers.
4. Reuse from Excel upload failure path (Gap 4 email attachment).

**Acceptance:** Opened PDF shows Hebrew correctly, right-aligned, across Acrobat, Edge, Chrome built-in viewer.

---

## Gap 11 — Lookup UI Special-Field Coverage

**Priority:** Medium (functional)

**Current state:** Generic UI handles description-only fields. Special fields exist in schema/import but not in manual-edit UI:
- `Localities.NationalCode` (int).
- `Frameworks.InstitutionSymbol`, `Frameworks.EducationalStageId`.
- `Institutions` has a dedicated screen (already).

**Work:**
1. `Localities` edit modal: add numeric `NationalCode` field with uniqueness hint.
2. `Frameworks` edit modal: add `InstitutionSymbol` text + `EducationalStageId` dropdown; enforce `UNIQUE (InstitutionSymbol, EducationalStageId)` server-side (already in DB — add friendly message).
3. Cover both in in-memory tests.

**Acceptance:** An Admin can create/edit Localities and Frameworks from the lookup UI without the Excel importer.

---

## Gap 12 — Draft Partial-Row Persistence (Optional)

**Priority:** Low (spec says "possible"; current system keeps whole-report drafts but not partially invalid rows)

**Work (only if the client insists):** Add a separate `DraftReportRows` table that mirrors `ReportRows` with all columns nullable + a raw JSON snapshot field, loaded back into the form on next entry; discarded on successful submit.

**Decision:** Defer until client pushes back. Current behavior — keep the report in Draft status with a possibly empty or partial row set that must validate before individual rows are saved — is acceptable.

---

## Gap 13 — Operational Runbook (Deployment-Ops)

**Priority:** Low (production readiness)

**Work:** Add `docs/OPERATIONS.md` covering: IIS + SQL Server Express install, SSL renewal, SMTP credentials rotation, daily DB backup + restore drill, reminder-job health check, NotificationLogs + AuditLogs retention, incident playbook (locked admin, bulk unlock, disabling TFA, reverting terms version).

---

## Sequencing & Agent Ownership

| Wave | Tasks | Best Agent(s) |
|------|-------|---------------|
| 1 (schema) | Gaps 1, 3, 6, 7 entities + migrations | db-architect |
| 2 (auth/flow) | Gap 1 terms flow, Gap 2 deadline edit block | auth-engineer, reporting-engine |
| 3 (reporting) | Gap 3 concurrency, Gap 4 failure email | reporting-engine, excel-handler |
| 4 (dashboard) | Gap 5 AJAX filter endpoints | dashboard-builder |
| 5 (ops) | Gap 6 retry service + admin screen, Gap 7 AuditLog service + admin screen | background-services, qa-security |
| 6 (UI) | Gap 8 logo, Gap 10 PDF quality, Gap 11 special-field UI | ui-polish, excel-handler |
| 7 (lookups) | Gap 9 FK checks | lookup-tables |
| 8 (docs) | Gap 13 runbook | deployment-ops |

All waves except Wave 1 can be parallelized across worktrees once the schema migration from Wave 1 is merged.

---

## Acceptance — System Complete When

- Every question in `CLIENT_CLARIFICATIONS.md` has a code path enforcing the answered behavior.
- Gaps 1, 2, 6, 7 (the four high-priority items) are covered by automated tests.
- `SPEC_TRACEABILITY_AUDIT.md` Functional Gaps table shows only items that are waiting on the client or explicitly out of scope.
- `dotnet test` is green and the 80% coverage gate on the deterministic service layer still holds.

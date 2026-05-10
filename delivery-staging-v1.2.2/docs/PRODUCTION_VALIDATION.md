# Axioma Reporting — Production Validation Test Plan

Comprehensive end-to-end validation script for a fresh production install on the client server. Work through it top-to-bottom — each section gates the next.

**Server:** `https://your-domain/`
**Test date:** ___________
**Tested by:** ___________

---

## 1. Infrastructure smoke (5 min)

- [ ] **HTTPS loads** — Browse to `https://your-domain/` → login page renders, no certificate warning
- [ ] **HTTP redirects to HTTPS** — Browse to `http://your-domain/` → automatic redirect to `https://`
- [ ] **Static assets load** — View page source: logo image, CSS, JS files all return 200 (check Network tab in DevTools)
- [ ] **Hebrew renders correctly** — All text right-to-left, no `???` boxes, no missing glyphs
- [ ] **Logo appears** — Organization logo visible top-right
- [ ] **Page title in Hebrew** — Browser tab shows "אקסיומא" or similar
- [ ] **No console errors** — F12 → Console → no red errors on login page

## 2. Database & app pool health

- [ ] **App pool running** — IIS Manager → AxiomaReporting pool is `Started`, identity is correct
- [ ] **App pool AlwaysRunning** — Advanced Settings → Start Mode = `AlwaysRunning`, Idle Timeout = `0`
- [ ] **Database connectivity** — Login page submits without DB-error (negative path proves connection works)
- [ ] **`ASPNETCORE_ENVIRONMENT=Production`** — Verify in IIS site config → Configuration Editor

## 3. First-time admin login

- [ ] **Login as `ADMIN` / `Admin123`** → forced to password change page
- [ ] **Change password** — Use a strong password (16+ chars, letters+digits). Save it in password vault.
- [ ] **Terms of Use prompt** — Accept terms → redirected to home
- [ ] **No errors after login** — Home page loads, top nav shows Hebrew menu items

## 4. SMTP / Email pipeline

- [ ] `/Admin/EmailServerSettings` → fill SMTP host/port/user/password, toggle SSL/TLS as needed
- [ ] **"שלח מייל בדיקה"** → success message shown
- [ ] **Test email arrives** in admin's inbox within 60 seconds, sender shows correctly, Hebrew renders, logo embedded
- [ ] `/Admin/NotificationLogs` → test email has status `Sent` (not `Failed`/`Pending`)

## 5. Branding & content setup

- [ ] `/Admin/Branding` → upload organization logo → preview correct → save → reload site → new logo on every page
- [ ] `/Admin/TermsOfUse` → publish final Terms of Use text in Hebrew
- [ ] `/Admin/EmailTemplates` → at least these templates exist & have non-empty bodies: Welcome, ReminderToReport, ReportApproved, ReportRejected, PasswordReset, PasswordExpiryWarning

## 6. Lookup tables seeded

- [ ] `/Lookup/districts` — at least one row exists
- [ ] `/Lookup/localities` — populated
- [ ] `/Lookup/sectors`, `/Lookup/programs`, `/Lookup/educationalprograms`, `/Lookup/subjects`, `/Lookup/domains` — all populated
- [ ] `/Admin/Frameworks` — institutions/frameworks list populated
- [ ] `/Admin/ReportingMonths` — **exactly one** month is active (`פעיל = כן`), correct year/month for current period
- [ ] `/Admin/SystemConstants` — `ReminderStartDaysBeforeDeadline`, `ReminderIntervalDays`, `PasswordExpiryWarningDays`, `NotesSimilarityThreshold`, `MaxDailyHours` all have non-null values

## 7. Create test users (one per role)

For each of the 6 roles, create a test user:

- [ ] System Admin (`test_admin`)
- [ ] Project Manager (`test_pm`)
- [ ] Project Coordinator (`test_coord`)
- [ ] Inspector-View (`test_inspview`)
- [ ] Inspector-Approval (`test_inspapp`)
- [ ] Employee (`test_emp`)

For each: username + temp password set → welcome email arrives → first login forces password change.

## 8. Role-based access control (critical)

Open an **incognito window** for each role test:

- [ ] **Anonymous** → `/Employee/Index` → redirects to `/Account/Login`
- [ ] **Employee** logs in → can see only own data → `/Admin/Branding` returns 403/AccessDenied
- [ ] **Inspector-View** → `/Dashboard` works (read only) → trying to approve a report fails (button hidden or 403)
- [ ] **Inspector-View** → can export approved reports only → cannot export draft/pending
- [ ] **Inspector-Approval** → can approve & reject from dashboard
- [ ] **Project Coordinator** → can create employees and allocations → cannot edit *approved* reports
- [ ] **Project Manager** → can open/close reporting months, override report status
- [ ] **Project Manager** → trying to promote another user to Admin → blocked (only Admin can do this — business rule #2)

## 9. Employee & allocation creation

Logged in as PM or Admin:

- [ ] `/Employee/Create` → fill all fields → save → success message → employee appears in list
- [ ] **Hebrew validation message** appears when required field is missing
- [ ] **Duplicate ID number** rejected with Hebrew error
- [ ] **Edit employee** → change name → save → list reflects change
- [ ] **Add allocation** → select project, program, framework, etc. → save
- [ ] **Second allocation same project for same employee** → rejected (business rule #15: `UNIQUE (UserId, ProjectId)`)
- [ ] **Employee dropdowns are scoped** — when filling allocation, program list updates by selected project (AJAX cascade — business rule #6)
- [ ] **Soft-delete allocation** → marked inactive but kept in DB for history

## 10. Report submission flow (golden path)

Logged in as the Employee created in step 7:

- [ ] `/Report/Index` → opens with current active month → row table appears
- [ ] **Add report row** with valid data → save → row appears in table
- [ ] Row total updates correctly
- [ ] **Save as draft** → leave page → return → row still there
- [ ] **Submit for approval** → status changes to "Pending Approval" → email sent to coordinator
- [ ] **Inspector-Approval can see this report** in `/Dashboard`
- [ ] **Inspector approves** → employee receives "ReportApproved" email
- [ ] **Approved report is read-only for employee** — no edit button, attempting `/Report/SaveRow` returns 403/error

## 11. Validation rules (10 business rules)

Stay logged in as Employee. Try each — all should be **blocked** with Hebrew error message:

- [ ] **Rule 7** — Pick employee's rest day → block: "אין לדווח על יום מנוחה"
- [ ] **Rule 8** — Daily total > 9 hours (without "Unlimited" allocation) → block
- [ ] **Rule 9** — Two rows: same date + same values + identical/empty notes → second row blocked
- [ ] **Rule 10** — Two rows with notes >90% similar → block (configurable threshold)
- [ ] **Rule 16** — Annual row count exceeds `AnnualRowAllocation` for that allocation → block
- [ ] **Field-level required** — Save with empty required field → block with Hebrew message + the field is highlighted
- [ ] **Future date** — Date in next month → block (per spec)
- [ ] **Date for closed month** — Date for past inactive month → block (PM role can override)
- [ ] **Activity-based terminology** — UI text uses "פעילות חודשית" / "משך תפוקה", **NOT** "שעות" anywhere in employee-facing screens (rule #14)

## 12. Approval workflow

Logged in as Inspector-Approval:

- [ ] `/Dashboard` → filter by month, employee, project → list updates
- [ ] **Sort by clicking column header** → arrow indicator + URL changes
- [ ] **Bulk select & approve** → checkboxes work → confirm dialog in Hebrew → approve → emails sent
- [ ] **Reject report** → must enter Hebrew rejection reason → reason appears in employee's "Returned for Correction" view → email sent

## 13. Excel import / export

- [ ] **Export employee list** → file downloads → opens in Excel → Hebrew renders, columns correct, RTL layout
- [ ] **Export approved reports** → file downloads → only approved rows present → Hebrew correct
- [ ] **Inspector-View tries to export draft reports** → blocked (rule #13)
- [ ] **Upload valid employee Excel** → preview shows new/changed rows → confirm → DB updated
- [ ] **Upload Excel with errors** (mistype a number, leave a required cell blank, use invalid lookup value) → on-screen error list AND downloadable PDF error report → no partial DB write
- [ ] **Employee Excel upload for current month** → succeeds → unapproved reports overwritten, approved reports preserved (rule #11)
- [ ] **Employee tries to upload for previous (locked) month** → blocked
- [ ] **PM uploads for locked month** → succeeds (rule #12)

## 14. Background services

- [ ] **App pool process running for >5 min** — check `w3wp.exe` in Task Manager has uptime > 5 min
- [ ] **Application log shows reminder cycle** — Event Viewer → Application log → look for `ReminderService started` and at least one `ReminderService: Cycle complete` entry
- [ ] **Manually trigger reminder window** — temporarily set `ReminderStartDaysBeforeDeadline` to a value that forces "now" → wait one cycle → check `/Admin/NotificationLogs` for new reminder rows → revert constant
- [ ] **Notification retry** — temporarily break SMTP password → send a test → log shows `Failed` → fix SMTP → next retry cycle changes status to `Sent`

## 15. Password policy & lockout

- [ ] **Password < 8 chars** → rejected on change
- [ ] **Password all letters / all digits** → rejected (must be mixed)
- [ ] **Reuse last 5 passwords** → rejected (history check)
- [ ] **3 wrong logins** → account locked → Hebrew "חשבון נעול" message
- [ ] **Locked user receives email** with reset/unlock instructions (or admin can unlock from `/Admin/Users`)
- [ ] **Password rotation** — temporarily set last-changed to >90 days ago in DB → next login forces change

## 16. Accessibility (IS 5568 / WCAG 2.1 AA)

Use Chrome DevTools → Lighthouse → Accessibility audit on these pages:

- [ ] `/Account/Login` → score ≥ 95
- [ ] `/Employee/Index` → score ≥ 95
- [ ] `/Dashboard/Index` → score ≥ 95
- [ ] `/Report/Index` → score ≥ 95

Manual checks:

- [ ] **Tab through login page** → focus visible (yellow outline) → "Skip to main content" link appears as first focus
- [ ] **Screen reader (NVDA on Windows)** — navigate the dashboard table → headers announced correctly, sort direction announced
- [ ] **Page zoom 200%** → all text readable, no horizontal scroll, layout still RTL

## 17. Performance & resilience

- [ ] **Page load < 2 sec** for dashboard with 100+ reports (use DevTools Network tab)
- [ ] **Logout & log back in** 5 times → no degradation
- [ ] **Restart App Pool** in IIS → site recovers within 10 sec → background services restart (check log)
- [ ] **Reboot SQL Server service** → app shows graceful error → recovers when SQL is back

## 18. Backup verification

- [ ] **Run `C:\scripts\backup-axioma.ps1` manually** → `.bak` file created in `D:\backups\` with today's timestamp
- [ ] **Restore the .bak to a scratch DB** on the same server (different name like `AxiomaReporting_RestoreTest`) → restore completes → run `SELECT COUNT(*) FROM Users` on restored DB → matches production count
- [ ] **Drop scratch DB** after verification
- [ ] **Scheduled Task** for nightly backup is enabled, runs as SYSTEM, last-run time recent
- [ ] **Off-server copy destination reachable** (NAS path or Azure Blob) → today's backup present there

## 19. Final cleanup

- [ ] **Delete all `test_*` users** (or disable, depending on client preference)
- [ ] **Delete all test reports** they created
- [ ] **Verify reporting month state** — only one active, correct period
- [ ] **Confirm no test data in `/Admin/AuditLog` filters real activity**

## 20. Hand-off documentation

- [ ] Client has been given:
  - Admin credentials (in sealed envelope or password vault)
  - URL of the system
  - SMTP credentials documentation
  - Backup retrieval procedure
  - Operations runbook ([OPERATIONS.md](OPERATIONS.md))
  - Support contact info

---

## Sign-off

| Role | Name | Signature | Date |
|------|------|-----------|------|
| Tester | | | |
| Client representative | | | |
| Project lead | | | |

---

**Pro tip:** Print this and check off as you go. Capture screenshots for any failure into a folder named `validation-YYYY-MM-DD/` before fixing — that becomes your "what changed" evidence if questions come later.

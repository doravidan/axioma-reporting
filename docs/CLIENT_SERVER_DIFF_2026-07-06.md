# Client-Server Build vs Repo Master — Full Diff Report (2026-07-06)

> **UPDATE 2026-07-07:** All seven server-only Features & Screens (§1 rows 1–7)
> were ported into repo master (commit "Align master with client-server
> features"): manual reporting, privacy-policy versioning, dashboard archive
> toggle, dashboard documents modal, batch-import progress + errors-as-Excel,
> ProjectProgram scope editor (all 7 value types incl. the newly mapped
> frameworks/grade-levels/classes tables), and allocations quick-create.
> Remaining gaps vs the server branch: PathBase/reverse-proxy URL handling
> (§ "Fixes & Infrastructure") and the data merge (§5/§6).

Comparison of the client's live-server package (`Exioma-dev-setup-20260705-164307`:
published v1.2.11 binaries + decompiled v1.2.8 sources + live-DB backup, restored
locally as `AxiomaReportingClient`, app on :5000) against repo `master` @ `1ce7dc5`
(app on :5121, DB `AxiomaReporting`).

## TL;DR — the two lineages forked at ~v1.2.8/9

The server was **developed further in place** (decompiled-source workflow, patches
compiled on the box, releases v1.2.9 → v1.2.11) while this repo advanced
independently (client-docs gap batch, UI redesign, report history, reopen,
lifecycle E2E). **Both sides implemented many of the same client requests twice,
differently.** Deploying repo master to the server as-is would drop several
server-side features/fixes and meet a database whose live data is richer than
ours in some tables and poorer in others.

---

## 1. Features that exist ONLY on the client-server build

| Feature | Where | Notes |
|---|---|---|
| **Manual reporting screen** (`/Report/Manual`, `ManualEmployeeSearch`, `ManualOpen`) | ReportController + `Views/Report/Manual` | Admin picks an employee from a live-search list (ת.ז/קוד/שם) and opens their report. Our equivalent is `/Report?userId=` via dashboard "ערוך" — no picker UI. |
| **Privacy-policy management** (`/Admin/PrivacyPolicy`, `PublishPrivacyPolicy`) + `PrivacyPolicyVersions` table (6 published versions) + top-nav "מדיניות פרטיות" | AdminController, DB | Ours is a static Privacy page. |
| **Dashboard archive toggle** ("הצג דיווחים בארכיון") | Dashboard/Index | Uses `Reports.IsArchived` (column exists in BOTH DBs). We closed gap I12 as "no UI exists" — true for our branch, **wrong for theirs**. |
| **Dashboard document endpoints** (`ReportDocuments`, `DocumentAttachment`) | DashboardController | Documents open in a modal from the dashboard; ours links into the report page. |
| **Batch import progress + errors-as-Excel** (`BatchReportImportProgress`, `BatchReportImportErrorsExcel`) | AdminController | Progress polling during import; error list downloadable as xlsx (ours: on-screen + PDF). |
| **ProjectProgram scope editor** (`SaveProjectProgramScope`) | AdminController | Admin UI to edit program→values mappings. Ours only imports/reads them. |
| **Allocations quick-create** (`/Allocations/Create` + view, `ScopedLookups`) | AllocationsController | Create allocation from the allocations list; ours goes through Employee → Allocations. |
| **Rich client-side JS** (site.js = 1,102 lines vs our 82) | wwwroot/js | Subject autocomplete with scoring, framework label rewriting client-side, dynamic per-row scoped lookups (`AllocationLookups` endpoint), program-scoped list filtering (`ScopedForProgram`). |
| **PathBase / reverse-proxy support** (v1.2.8–v1.2.11) | Views/JS/controllers | All fetch/redirect URLs are server-generated so the app works under an IIS virtual path. **Our master still has absolute `/Report/...` URLs in places** — matters if production hosts under a sub-path. |
| **SecurityPatch.dll** (`RequireTermsAndPasswordFilterPatch`) | Extra assembly from `patches/ForcePasswordChangeGuard` | Their forced-password-change + terms guard. We have the same guard natively (`RequirePasswordChangedFilter` + terms filter) — theirs becomes redundant under master, but the patched assembly must not be carried over blindly. |
| Ops toolkit (`patches/`) | Server | ResetPasswordsToUsername, ResetAdminPassword, SmtpConnectivityCheck, SqlProbe, FwDataImport, SyncAllocationProjectScopes, XlsbLookupReplace + ~30 Playwright check scripts. |

## 2. Features that exist ONLY on repo master

- **Full UI redesign** (design-token system, Heebo font, navy/orange, WCAG-AA button contrast, dropdown word-break fix). Their build is the old flat-orange Bootstrap look; their dashboard footer even shows **"סה"כ שעות בעמוד"** — banned "שעות" terminology we scrubbed.
- **Report reopen** ("החזר לעריכה", `Reopen` action) — return an approved report to editing.
- **Employee report history** ("דיווחים קודמים" card + read-only past-month view). Theirs has a `History` action too (parallel implementation) — behavior likely differs.
- **Approve-from-summary nested-form fix** (formaction; their v1.2.11 fixed adjacent PathBase symptoms, but their Summary markup still nests forms).
- **Separate conclusion lookups** (`ClassConclusions`/`FrameworkConclusions` tables). Their ReportRows conclusion FKs still point at `SchoolClasses`/`Frameworks` (the original mis-model).
- **Frameworks admin suite** (search/filter/bulk-activate/export as we built it), **dashboard cascade filters**, **סוג דיווח on allocation + export column**, **program auto-fill on allocation form** (`ValuesForProgram`), **K5 help texts**.
- **Full test suite in repo**: 353 unit/integration + 140 Playwright incl. destructive lifecycle E2E. (Their branch reports "488 passed" in v1.2.11 notes but tests live only on the server.)
- **Deploy package** (runbook, publish script, schema.sql, .bak/.bacpac).

## 3. Parallel implementations (same ask, built twice — merge decisions needed)

| Client ask | Their implementation | Ours |
|---|---|---|
| Export my report | `ExportMine`, `ExportReportMonth` | `ExportMyReport` |
| Framework labels "יישוב—סמל—שם" | client-side relabel + `FrameworkLabels` JSON | server-side `FrameworkLabelService` |
| Program→values conditioning | `ScopedForProgram` + `ScopedLookups` + client JS | `ValuesForProgram` + allocation-form auto-fill |
| Admin reports for employee | `/Report/Manual` picker | dashboard ערוך → `/Report?userId=` |
| Past reports | `Report/History` action | PastReports card in Report/Index |
| Forced password change | SecurityPatch.dll filter | `RequirePasswordChangedFilter` |
| Frameworks export | `ExportFrameworks` | `ExportFrameworksExcel` |

## 4. Database schema

Column-level: **identical on all common tables** (their upgrade-v1.2.x.sql scripts
mirror most of our migrations; they record 13 EF migrations vs our 19).
Differences:

- **Theirs only**: `PrivacyPolicyVersions`, `EmailServerSettings_NoEmailTestBackup_20260506` (manual backup table).
- **Ours only**: `ClassConclusions`, `FrameworkConclusions`.
- **Semantic difference**: ReportRows conclusion FKs — theirs → `SchoolClasses`/`Frameworks`; ours → dedicated conclusion tables. Any data merge must remap these IDs.

## 5. Data (live client DB vs our dev DB — biggest deltas)

| Table | Client live | Ours | Meaning |
|---|---|---|---|
| Users | 456 | 446 | Client added ~10 users on live |
| Frameworks | 4,380 | 3,193 | Client imported/added ~1,200 frameworks |
| AllocationFrameworks | 132,483 | 45,438 | Live allocations scoped to ~3× more frameworks |
| AllocationClasses / GradeLevels | 12,818 / 6,202 | 0 / 3,933 | **Our allocations have no class scoping at all** |
| ProjectProgramFrameworks | 5,052 | 1,420 | Their program→framework mapping is populated (we excluded frameworks from the cascade per QA #4 — theirs conditions them) |
| ProjectProgramClasses / GradeLevels | 435 / 199 | 0 / 0 | Scope types we never imported |
| Localities | 639 | 1,482 | Ours carries duplicates/unused rows they cleaned |
| Districts / Sectors / Programs / Projects | 12/8/15/1 | 17/12/31/3 | Same — theirs was cleaned to the real project |
| ReportingMonths | 8 | 3 | Live months opened Jan–Aug |
| Reports / ReportRows | 5 / 0 | 18 / 452 | **No real reporting has happened on live yet** |
| EmailServerSettings | smtp.gmail.com:587 (test acct) | empty | Live SMTP is a test Gmail |
| PasswordHistories | 442 | 3 | They ran their password-reset-to-ID on live |
| EmployeeRoles | 12 | 7 | Extra roles added on live |

## 6. What this means for the delivery

1. **Do not deploy repo master over the server as-is.** It would remove: manual
   reporting, privacy-policy management, archive toggle, dashboard document modal,
   import progress, scope editor, allocations quick-create — features the client
   may already use — and could break under a virtual path (PathBase).
2. **The live DB is the source of truth for data** (frameworks, allocation scopes,
   program mappings, users, months) — our dev DB is the source of truth for the
   conclusion-lookup schema. A merge needs: restore live DB → apply our 6 missing
   migrations (incl. conclusion tables + FK remap) → keep their
   PrivacyPolicyVersions.
3. **Feature merge direction**: port their server-only features into the repo (or
   consciously drop each with the client's sign-off), keep our versions where both
   exist (redesign, reopen, history, nested-form fix), and adopt their PathBase
   handling.
4. Their ops scripts under `patches/` are worth importing into `scripts/`.

Screenshots: scratchpad `shots/cl_*.png` (manual reporting, dashboard with archive
toggle, privacy admin, summary, frameworks). Local test creds for the restored
copy (`AxiomaReportingClient` only, never the real server): `admin` / `admin1234`.

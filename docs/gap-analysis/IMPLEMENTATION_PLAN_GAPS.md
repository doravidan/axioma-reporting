# Axioma / Site&Sound — Prioritized Delivery Plan (Gap Closure)

## 1. Executive Summary

The system is substantially built end-to-end: authentication, password policy, the reporting form with its validation engine, allocations, the reports dashboard, Excel import/export, and the generic lookup-table admin all exist and function. The 52 verified gaps are **residual** rather than foundational — **35 are PARTIAL** (the feature exists but is incomplete or mis-configured) and only **17 are MISSING** entirely. Critically, only ~8 items actually block the client from operating (three auth/security holes, three report/upload validation-rule violations, and the employee onboarding + end-to-end walkthrough). The bulk of the work is spec features the client explicitly requested (framework display, program-driven auto-fill, dashboard cascading, document handling) plus a thick layer of terminology/cosmetic polish that is individually trivial. Two large workstreams (program→field association `K10`+`E8` and the framework-composite display chain) drive most of the risk and effort; almost everything else is S-effort.

---

## 2. Phase P0 — Blocking Bugs, Security & Validation Holes

These stop the client from operating safely or violate hard business rules (#8, #9, #11). Ship first.

| Area+ID | Requirement | Gap | Effort |
|---|---|---|---|
| A9 | Forced change-password not bypassable | No global filter; `MustChangePassword`/expiry enforced only at login redirect, so a user on ChangePassword can click any menu link through | S |
| A12 | After forgot-password reset, don't re-force another change | `ResetPassword` POST sets `MustChangePassword=true` (line 369), forcing a second change at next login | S |
| A8 | 30-min inactivity auto-logout, configurable | Timeout is a hardcoded 8h sliding expiry; must be 30 min bound to appsettings/SystemConstant | S |
| G5 | No exceeding 9 daily hours unless "ללא הגבלה" | Allocation with `DailyEmploymentScope=NULL` early-returns and bypasses the 9h cap (rule #8 hole) | S |
| G7 | Duplicate detection excluding hours field (BETA B26) | Line-263 `MeetingDuration` clause is still in the equality key, so rows differing only in hours aren't blocked (rule #9) | S |
| H4 | Overwrite ONLY unapproved reports (rule #11) | Batch import appends rows to StatusId 3/4 reports with no guard; single-import path is correct | S |
| N2 | Reset all existing users' passwords to ת.ז | Import only sets password on newly-inserted rows; existing users never reset → they cannot log in as onboarding expects | S |
| N3 | Employee end-to-end UX must work (BETA B37) | Umbrella verification: login → my allocations → report → submit must be exercised once dependents land | L |

**Sequencing:** A8/A9/A12 are independent auth fixes — do together. G5/G7/H4 are one-line predicate/guard fixes in the validation/import services — batch them. **N2 unblocks employee login and is a prerequisite for the N3 walkthrough.** N3 is a *gate*, not a build task — run it last, after the P0 fixes plus the P1 employee-facing items (A6, C1, D2, F1, G15, G20/G21, G28, H1) land.

---

## 3. Phase P1 — Missing Spec Features the Client Explicitly Asked For

| Area+ID | Requirement | Gap | Effort |
|---|---|---|---|
| A6 | Password eye toggle on Login/ChangePassword/Reset | Plain `type=password` inputs, no toggle JS | S |
| A7 | TFA optional (email/phone) | Only global email channel; no SMS/phone path, no per-user opt-in field | M |
| A11 | Forgot-password email actually delivered | Flow + clickable anchor confirmed in code; only SMTP delivery is env-dependent — verify in target environment | M |
| B2 | Privacy policy visible to ALL users | Page reachable only by URL; body is a placeholder sentence; no nav/footer link | S |
| C1 | Role-based top menu | `הקצאות` and `ראשי` not role-gated; employees (role 6) still see הקצאות | S |
| C6 | Graphical landing menu | Index is a plain welcome banner; no role-aware tile menu | M |
| D2 | Internal user number gray/read-only | No field surfaces `User.Id` on the employee card | S |
| D13 | Roles lookup table (תפקידים) | `EmployeeRole` not exposed in generic Lookup CRUD; admin can't add/edit/delete role values | S |
| E3 | משך תפוקה: add "דקות" + "ללא הגבלה", multi-select | Values/multi-select present, but create/edit form lacks "ללא הגבלה" and the word "דקות" | S |
| E10 | Add-allocation entry on allocations screen | "הוסף הקצאה" renders only with employee context; standalone screen has no add button | S |
| E11 | "סוג דיווח" on allocation → report + export col B | No `ReportTypeId` on Allocation/DTO/form; exists only on ReportRow + dashboard | M |
| F1 | Filter by every column | Email, Phone, IsReportingEmployee, and job-role columns are sortable but unfilterable | S |
| G15 | Actions column FIRST from right in report grid | פעולות rendered last in thead/tbody | S |
| G23 | Attachments per-employee AND per-report | Report-level upload only; no per-row upload UI, employee-level (UserId) docs never surfaced on report screen | M |
| G28 | Employee downloads own report as Excel | Only empty template + allocations export exist; no export of the employee's own report rows | M |
| H1 | Mobile-friendly upload | Single-report inner control lacks `flex-wrap`; doesn't stack on very narrow screens (batch screen is fine) | S |
| I1 | Cascading filters (מחוז surfaces related values) | Only sector/program cascade server-side; FilterOptions AJAX not wired into Index; ~15 lookups don't cascade | M |
| I2 | Table empty until "הצג" pressed | Controller hardcodes `showData=true`; ignores `show=1`, eagerly loads all scoped data | S |
| I13 | Documents count column + access from dashboard | Boolean כן/לא only; no count and no link/modal to view/download | M |
| I15 | "קיום דיון" closed list per project | DiscussionCode has no project association, loaded unconditionally; label inconsistent (קוד דיון vs קיום דיון) | M |
| K4 | Frameworks table: search/export/bulk toggle | No search form, no Excel export action, no bulk active/inactive; import buried in data-migration tool | M |
| K13 | Inspector assignments: add ת.ז + first/last name | Only a combined-name dropdown; no IdNumber or separate name fields | S |
| K16 | Lookup/admin single-select type-to-search | Choices.js init targets only `select[multiple]`; single selects get native jump only | M |
| N1 | Clean old/test reports from DB | No purge tooling; only seed/recover scripts exist | S |
| **Framework-composite workstream (do as one migration + shared resolver):** | | | |
| E14 | Framework display = יישוב+סמל+שם in allocation | Framework has no locality link; shows Description only | M |
| G21 | Framework value = יישוב+סמל+שם + type-ahead in report | Option text is `x.Description` in every path; no composite, no type-ahead | M |
| N4 | Framework value composite across report/dashboard/export | Same root cause; locality lives on Institution, never joined into label | M |
| G20 | All report combos support type-to-search | Single `<select>` combos not covered by Choices init | M |
| **Program-cascade workstream (largest lift; K10 → E8 → E9):** | | | |
| K10 | Manage code-table values per project/program | No association entities exist; only project↔program join. This is the data source E8 needs | L |
| E8 | Program selection auto-populates שיוכים | No per-program association tables/nav; only Project→Program cascade wired | L |
| E9 | תוכנית first-from-right + fills שיוכים | Field present but placed 2nd (left of Project in RTL); population depends on E8 | M |

**Sequencing / dependencies:**
- **Framework chain (E14, G21, N4, K4-locality):** add locality to `Framework` (or resolve via `Institution` by symbol) **once**, expose a shared composite resolver, then apply to allocation form, report dropdown/grid, dashboard, and Excel export. Do this migration before the individual display fixes.
- **Type-to-search (G20, G21, K16):** all share extending Choices.js to single-selects — one JS change plus per-screen wiring; do together.
- **Attachments (G23, I13):** share the document-surfacing model; build the per-row + employee-level query once, consume in both report screen and dashboard.
- **Program cascade (K10 → E8 → E9):** strictly sequential. K10 (new association entities + admin UI) must exist before E8's auto-fill; E9's wiring depends on E8. Per QA #4, frameworks must stay filtered by employee allocation, **not** program-conditioned.
- **I15** partially shares the "per-project association" concept with K10 — schedule after or alongside K10.

---

## 4. Phase P2 — UX / Terminology / Cosmetic Polish

Individually trivial; batch into one or two sweeps. Rebrand (C7) is low-effort but high-visibility (appears on the landing page to every user) — treat as an early quick win despite the P2 label.

| Area+ID | Requirement | Gap | Effort |
|---|---|---|---|
| C7 | Rebrand אקסיומא → סייט אנד סאונד | Live remnants: Home/Index.cshtml:5, PdfReportService.cs:67, EmailServerSettings.cshtml:59, SeedData email templates, stale site-logo.svg | S |
| C4 | Home + Logout icons on the LEFT | Logout is left; Home is not — add a Home affordance to the left cluster | S |
| C5 | Menu order (RTL): ניהול, ראשי, עובדים, הקצאות, חודשי דיווח, דשבורד | Markup order wrong; reorder the `<li>` elements | S |
| E2 | "היקף העסקה" not "היקף פעילות" | Annual/monthly labels read היקף פעילות across DTO/form/list/filters/export | S |
| F9 | Rename "השבתה" → "לא פעיל" | Button label, confirm dialog, and success text still use השבתה/הושבת | S |
| G18 | Framework header = "מסגרת חינוכית" in manual report | Grid/modal/inline labels say only "מסגרת" | S |
| I14 | Move page-size control to LEFT | Embedded inline in filter grid at col-md-1, not screen-left | S |
| K6 | Rename "קוד דיון" → "קיום דיון" | Still old term in Report/Index (277/461/799), Dashboard/Index (157/216), LookupController:41, DataMigration:200 | S |
| K14 | Trash-can icon for delete in all tables | InspectorAssignments uses text "מחק"; Frameworks/Institutions have no delete control at all | S |
| G16 | "שורות בעמוד" control consistent across screens | Report grid is entirely unpaginated (no control) — either add matching control or confirm exemption | M |

**Sequencing:** C5 (menu order), C4 (Home icon), C7 (rebrand), and C1 (role-gating, in P1) all touch `_Layout.cshtml` — do in a single navigation sweep. Terminology renames (E2, F9, G18, K6) are find-and-replace; verify no accessibility labels break. G16 may reveal the report grid needs pagination — coordinate with G28 (report export) and confirm scope.

---

## 5. Phase P3 — Clarifications & Confirm-Before-Build

Do **not** build until the client answers. These carry design ambiguity or conflict with existing rules.

| Area+ID | Requirement | Why it's blocked | Effort |
|---|---|---|---|
| E7 | Remove monthly-rows field from allocation screen | Client marked it "????"; **conflicts with business rule #16** (per-allocation row limits store/validate against this field). Confirm with PM before removing the underlying field | S |
| I12 | "הצג דיווחים בארכיון" meaning | Client's own question ("מה מהות השדה הזה?"); no archive concept exists in the model. Clarify then build a flag or mark out-of-scope | M |
| K5 | Distinction: רשימת מוסדות vs רשימת מסגרות | Data-model design ambiguity code can't resolve; needs a product decision + in-app help text | S |

---

## 6. Highest-Risk Items & Required Clarifications

**Highest risk (schedule with buffer, own migrations, regression-test):**
- **K10 + E8 + E9 (program→field cascade, 2×L + M):** net-new association entities, migrations, admin UI, and client-side auto-fill logic that mutates the allocation form. Biggest single lift and the deepest dependency chain. Must not break QA #4 (frameworks filtered by allocation, not program).
- **Framework-composite chain (E14/G21/N4/K4):** requires a schema change to link `Framework` to a locality (or a join-via-`Institution`-by-symbol strategy) and must align with the import/`LookupResolver` matching. Touches allocation, report, dashboard, and export simultaneously — regression surface is wide.
- **N3 (employee E2E):** an integration risk, not a build — its "done" depends on ~8 other items landing correctly. Gate it, don't estimate it in isolation.
- **A9 (forced-change bypass):** security — currently an authenticated user can escape the forced-change screen entirely.
- **H4 (approved-report overwrite):** silent data-integrity violation of rule #11 on the batch path.

**Needs client clarification before work starts:** E7 (rule #16 conflict), I12 (archive semantics), K5 (institutions vs frameworks). Additionally, **A11** cannot be code-verified — confirm SMTP delivery in the target environment. For **E8/K10**, confirm exactly which code-tables each program should drive.

---

## 7. Effort Roll-Up

| Phase | S | M | L | Total |
|---|---|---|---|---|
| P0 — Blocking / security / validation | 7 | 0 | 1 | 8 |
| P1 — Missing spec features | 13 | 16 | 2 | 31 |
| P2 — Terminology / cosmetic polish | 9 | 1 | 0 | 10 |
| P3 — Clarifications / confirm-first | 2 | 1 | 0 | 3 |
| **Total** | **31** | **18** | **3** | **52** |

**Read:** the program of work is dominated by small items (31 S). The three L items (N3, E8, K10) plus the framework-composite M-cluster carry nearly all the schedule risk. A pragmatic cut: land **P0 (7×S + the N3 gate)** and the **P2 polish sweep** first for fast, visible client wins, run the P3 clarifications in parallel to unblock later work, then tackle P1 with the two large workstreams (framework-composite, then program-cascade) sequenced last.
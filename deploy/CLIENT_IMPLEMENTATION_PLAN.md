# תוכנית יישום — מערכת דיווח פעילות חודשית (סייט&סאונד חינוך)
# Client Implementation Plan — Install & Go-Live

**Version:** 2026-07-08 · **Delivery build:** git `master` (commit of 2026-07-08) · **Data snapshot:** 2026-07-08

This is the end-to-end plan for installing the system on the client's Windows server and
taking it live. It orchestrates *what happens, in what order, who does it, and how we know
each step succeeded*. The exact copy-paste commands for every technical step live in the
companion runbook — **`deploy/CODEX_DEPLOY_RUNBOOK.md`** — which an IT person or an AI agent
can execute top-to-bottom. Hebrew narrative companions: `docs/CLIENT_DELIVERY.md`,
`docs/DEPLOY_CHECKLIST.md`, `docs/OPERATIONS.md`.

---

## 1. What is being delivered

| # | Item | Where | Notes |
|---|------|-------|-------|
| 1 | Application source + publish script | GitHub `doravidan/axioma-reporting`, branch `master` | Built with `deploy/publish.ps1` → self-contained publish folder for IIS |
| 2 | Full database backup (**structure + all data**) | `AxiomaReporting_delivery_2026-07-08.bak` (33 MB) | **Delivered out-of-band — contains employee PII, never in git.** Requires SQL Server 2022+ |
| 3 | Version-independent data export (same content) | `AxiomaReporting_delivery_2026-07-08.bacpac` (0.7 MB) | For SQL Server 2019 servers; round-trip verified |
| 4 | Schema-only script (empty-start fallback) | `database/schema.sql` in git | Idempotent; only if the client wants to start with no data |
| 5 | Deploy runbook (commands + VERIFY steps) | `deploy/CODEX_DEPLOY_RUNBOOK.md` in git | The execution document for this plan |
| 6 | Go-live scripts | `scripts/reset_passwords_to_id.py`, `scripts/purge_old_reports.sql` in git | Password normalization; optional pilot-report purge |

### What the data snapshot contains (2026-07-08)

- **492 employees** (including the שמיים-חטיבות-ביניים onboarding of 56 employees imported 2026-07-07), **489 allocations**, full lookup data: 3,222 frameworks, 4,297 institutions, 1,448 localities, 12 email templates, 22 EF migrations.
- Client fixes of 2026-07-08: duplicate programs merged (17 clean programs), full per-program default-scope data for the allocation auto-fill, history view fix.
- Pilot reports from December 2025 + January 2026 (16 reports / 451 rows) — kept until the client decides on a purge cutoff.
- 5 July-2026 test reports — **deleted during go-live normalization** (runbook §2.2a).
- Active reporting month: **יולי 2026** (deadline 31/07/2026).

---

## 2. Roles and responsibilities

| Role | Who | Responsibilities |
|------|-----|------------------|
| Server/IT owner | Client IT (or agent executing the runbook) | Server access, installs, IIS, SQL, certificate, DNS, firewall, backups |
| Application owner | Delivery team (Dor) | Publish package, database snapshot, go-live support, defect fixes |
| System administrator | Client-appointed admin user | First-run configuration (SMTP, reporting month, branding, terms), user support |
| Decision maker | Client PM | Go-live date, pilot-report purge cutoff, HTTPS certificate procurement, SMTP account |

### Decisions the client must make **before** installation day

1. **Domain name** for the site (e.g. `reports.example.co.il`) + who updates DNS.
2. **HTTPS certificate**: purchased certificate or free Let's Encrypt via win-acme.
3. **SMTP account** for system emails (server, port, user, password, from-address).
4. **Go-live reporting month** — the snapshot ships with יולי 2026 active; if going live later, the admin activates the correct month on day one.
5. **Pilot reports**: keep the December/January pilot data or purge it (affects nothing functionally; purely historical).

---

## 3. Server prerequisites

Minimum: Windows Server 2019+ (2022 recommended), 4 GB RAM, 20 GB free disk, outbound
internet during install (downloads), inbound 80/443 from the organization's network.

| Component | Version | Note |
|-----------|---------|------|
| IIS | built-in role | With ASP.NET Core Module v2 (from Hosting Bundle) |
| .NET Hosting Bundle | **8.0.x** | App rolls forward from net6.0 automatically — do **not** install EOL .NET 6 |
| SQL Server Express | **2022 preferred; 2019 supported** | 2022 → restore the `.bak` (fastest). 2019 → import the `.bacpac` (runbook §2 Option A2) |
| SSMS (optional) | latest | Only if a human will manage the DB |

> ⚠️ The single most common pitfall: the `.bak` was produced by SQL Server 2022 and
> **cannot** be restored on SQL 2019. That is exactly why the `.bacpac` is in the package —
> same data, works on 2019. Check `SELECT SERVERPROPERTY('ProductVersion')` first.

---

## 4. Implementation phases

Total hands-on time: **~half a day** for phases A–D, plus DNS/certificate lead time.

### Phase A — Infrastructure (1–2 hours) → runbook §1

1. Install IIS + .NET 8 Hosting Bundle; `iisreset`.
2. Install SQL Server Express (Basic preset).
3. Create folders: `C:\inetpub\AxiomaReporting`, `C:\deploy`, backup drive folder.
4. **VERIFY:** AspNetCoreModuleV2 listed; SQL responds with its version.

### Phase B — Database (30–60 minutes) → runbook §2

1. Copy the delivery backup to the server (`C:\deploy\`).
2. **SQL 2022+:** restore the `.bak` (Option A). **SQL 2019:** pre-create the DB with
   `COLLATE Hebrew_CI_AS`, then import the `.bacpac` with sqlpackage (Option A2 —
   the collation pre-create step is mandatory, otherwise Hebrew sorting breaks).
3. Create the least-privilege SQL login `AxiomaWeb` (§2.1) — the app must not run as `sa`.
4. **Go-live normalization (§2.2) — required, in this order:**
   a. Delete the 5 July-2026 test reports (expect `deleted_reports = 5`).
   b. Reset every employee password to their ID number with forced change at first login
      (`reset_passwords_to_id.py` — dry-run first, then `--commit`).
   c. Force admin password rotation (`MustChangePassword = 1`).
   d. Confirm/replace the active reporting month for the actual go-live month.
5. **VERIFY:** `Users=492, Allocations=489, Frameworks=3222, Migrations=22, ForcedChanges=492`.

### Phase C — Application + IIS (30–60 minutes) → runbook §3–§4

1. Build the publish folder (`deploy/publish.ps1` on the dev machine, or on the server if
   the .NET 8 SDK is present) and copy it to `C:\inetpub\AxiomaReporting`.
2. Grant IIS_IUSRS modify rights on `wwwroot\uploads` and `wwwroot\images`.
3. Create `appsettings.Production.json` with the `AxiomaWeb` connection string (this file
   is the only secret on disk; it is never committed to git).
4. Create the app pool (No Managed Code, AlwaysRunning) + site on port 80, environment
   `Production`.
5. Bind HTTPS once the certificate is in the store (win-acme automates Let's Encrypt);
   add the HTTP→HTTPS redirect.
6. **VERIFY:** `/Account/Login` returns 200 with the Hebrew login page over HTTPS.

### Phase D — First-run configuration (30 minutes, in the browser) → runbook §5

Performed by the system administrator:

1. Login `admin` / `admin1234` → forced password change → set the real admin password.
2. Accept the terms-of-use screen.
3. `/Admin/EmailServerSettings` → enter SMTP details → **send a test email** → confirm
   receipt and a `Sent` row in `/Admin/NotificationLogs`.
4. `/Admin/ReportingMonths` → confirm the active month (only one may be active).
5. `/Admin/Branding` → confirm the organization logo.
6. `/Admin/TermsOfUse` → publish final terms (all users re-accept at next login).
7. `/Admin/PrivacyPolicy` → confirm the published privacy-policy version.

### Phase E — Acceptance smoke test (15 minutes) → runbook §6

Run through as admin and as one employee:

```
1. Admin login → home tiles load
2. /Dashboard → rows load immediately (no filters) → Excel export downloads
3. /Dashboard/Summary → KPI cards + approval table render
4. /Employee/Index → open + save an employee card
5. /Report/Index → add a dummy row, save, delete
6. /Report/Manual → search an employee → דווח → picker → editor opens
7. /Admin/AuditLog → shows the actions just performed
8. Employee login (ID number as both username and password) → forced password
   change → terms → פעילות חודשית loads with the employee's allocation values only
9. Logout → redirected to login
```

### Phase F — Operations setup (30 minutes) → runbook §7

1. Nightly SQL backup + uploads-folder copy via Scheduled Task (02:00, 14-day retention).
2. Run the task once now; confirm a `.bak` lands in the backup folder.
3. Configure an off-server copy (NAS/cloud) of the backup folder.
4. Agree the monitoring/on-call arrangement (Event Log → `IIS AspNetCore Module V2`).

### Phase G — Rollout to employees

1. Announce go-live: employees log in with **ID number as username and initial password**
   and are forced to set a real password at first login (3 failed attempts lock the
   account; admin unlocks via the employee card or the SQL one-liner in the runbook).
2. Coordinators/inspectors receive their role-appropriate walkthrough
   (dashboard, approvals, exports).
3. First reporting cycle happens with the delivery team on standby for same-day fixes.

---

## 5. Acceptance criteria (sign-off checklist)

- [ ] Site serves over HTTPS at the agreed domain; HTTP redirects.
- [ ] DB VERIFY counts match (§ Phase B.5) and collation is `Hebrew_CI_AS`.
- [ ] Admin completed first-run configuration; test email received.
- [ ] Smoke test (Phase E) passes end-to-end for admin + employee.
- [ ] All 492 users have `MustChangePassword = 1` at go-live.
- [ ] Nightly backup ran at least once and the file is restorable.
- [ ] `appsettings.Production.json` holds the only secret; app runs as `AxiomaWeb`, not `sa`.

## 6. Rollback & support

- **App rollback:** stop the app pool → restore the previous publish folder → start.
  The DB schema is idempotent/backward-safe within this delivery.
- **DB rollback:** restore the latest nightly `.bak` (or the delivery backup for a
  full reset — repeat §2.2 normalization afterwards).
- **Common failures:** see the troubleshooting table at the end of the runbook
  (500.30 = connection string/Hosting Bundle; login DB error = AxiomaWeb login;
  version error on restore = use the `.bacpac`; admin lockout = SQL one-liner).
- **Defects/questions:** delivery team (Dor) — same-day response during the first
  reporting cycle.

## 7. Explicitly out of scope for installation day

- **Pilot-report purge** (`scripts/purge_old_reports.sql`) — only after the client
  decides the cutoff month; defaults to dry-run.
- **Program auto-fill scope** (`/Admin/ProjectPrograms`) — populated for the imported
  questionnaire catalog; additional program associations are maintained by the admin
  through that screen, including the new "תוכנית שמיים - חטיבות ביניים".
- **Future onboarding workbooks** ("קובץ נתוני עובדים מערכת חדשה- …") — imported by the
  delivery team with `scripts/import_fw_seed.py` (dry-run → commit) against the live DB,
  or via the admin import screens; NOT part of server installation.

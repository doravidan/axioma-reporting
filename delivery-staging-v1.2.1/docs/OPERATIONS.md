# Operations Runbook — Axioma Employee Reporting System

**Audience:** system administrators and on-call operators. Hebrew-speaking UI users are out of scope for this document.

## 1. Deployment on Windows Server + IIS

### 1.1 Prerequisites

- Windows Server 2019 or newer, fully patched.
- .NET 8 **Hosting Bundle** installed (includes runtime + ASP.NET Core Module v2). Verify via `dotnet --info`.
- IIS role installed with `Web-Common-Http`, `Web-Http-Redirect`, `Web-Static-Content`, and `Web-Windows-Auth` features. ASP.NET Core Module v2 must be registered (`appcmd list modules | find "AspNetCoreModuleV2"`).
- SQL Server Express 2019 or newer on the same host or a LAN-local host.
- SMTP relay reachable on the internal network with a dedicated service mailbox.
- SSL certificate for the public hostname (Let's Encrypt via `win-acme` or an organizational CA).

### 1.2 IIS site setup

1. Create an application pool named `AxiomaReporting`.
   - **.NET CLR version:** `No Managed Code`.
   - **Start mode:** `AlwaysRunning`.
   - **Identity:** a domain service account (or local `ApplicationPoolIdentity`) with read/write permissions on the site folder and `wwwroot\uploads\`.
   - **Idle Time-out:** `0` (disable). Background jobs run inside the process and must not be recycled by idle timeout.
   - **Regular Time Interval:** `0` (or schedule recycle at 03:00 when traffic is low).
2. Create the site `AxiomaReporting` pointing at the published folder (e.g. `C:\inetpub\AxiomaReporting`).
3. Bind HTTPS with the SSL certificate; disable HTTP or redirect to HTTPS via URL Rewrite.
4. Set the application-pool environment variable `ASPNETCORE_ENVIRONMENT=Production` (via IIS Manager → Configuration Editor → `system.webServer/aspNetCore/environmentVariables`).
5. Grant the app-pool identity `Modify` on `wwwroot\uploads\` (attachments, branding, Excel error PDFs) and the `fonts\` folder read access.

### 1.3 Secrets and configuration

- `appsettings.Production.json` (not committed) must set:
  - `ConnectionStrings:DefaultConnection` — SQL Express connection with least-privilege login (see §2.2).
  - Optional: logging overrides, Kestrel settings if hosted without IIS.
- SMTP credentials live in the DB table `EmailServerSettings`, encrypted at rest — do **not** duplicate in appsettings.
- System toggles such as `TfaEmailEnabled`, `ReminderIntervalDays`, `NotesSimilarityThresholdPercent`, `SiteLogoPath` live in the DB table `SystemConstants`.

---

## 2. Database setup

### 2.1 Create the database

```sql
CREATE DATABASE AxiomaReporting
  COLLATE Hebrew_CI_AS;
GO
ALTER DATABASE AxiomaReporting SET RECOVERY SIMPLE;
GO
```

### 2.2 Least-privilege SQL login

```sql
USE [master];
CREATE LOGIN [AxiomaWeb] WITH PASSWORD = 'REDACTED', CHECK_POLICY = ON;
USE [AxiomaReporting];
CREATE USER [AxiomaWeb] FOR LOGIN [AxiomaWeb];
ALTER ROLE db_datareader ADD MEMBER [AxiomaWeb];
ALTER ROLE db_datawriter ADD MEMBER [AxiomaWeb];
GRANT EXECUTE ON SCHEMA::dbo TO [AxiomaWeb];
```

Do **not** grant `sysadmin`, `db_owner`, or `db_ddladmin`. Schema changes go through EF migrations run out-of-band with an elevated account.

### 2.3 Running migrations

**First deploy (recommended, manual):**

```powershell
# from a jump host or the server itself, once
cd C:\inetpub\AxiomaReporting
dotnet AxiomaReporting.Web.dll --migrate-only
# or, using EF CLI against the prod connection string:
dotnet ef database update --project src\AxiomaReporting.Infrastructure --connection "Server=...;Database=AxiomaReporting;..."
```

**Subsequent deploys (optional automatic):** run `context.Database.Migrate()` during `Program.cs` startup. Document the switch in `appsettings.Production.json` (`Database:AutoMigrate=true/false`).

### 2.4 Initial seed

- `SeedData.Run()` (invoked at startup in Development) seeds lookups, statuses, email templates, system constants, the default admin, and `TermsOfUseVersion` v1. In Production, seeding is idempotent on first run; subsequent app starts skip rows that already exist.
- Load client data via `/Admin/DataMigration` (Excel importers) or the one-time scripts under `database/seed-data/` (`seed_lookups.py`, `seed_reports.py`).
- Change the default admin password immediately after first login (first-login flow forces this).

---

## 3. SMTP configuration & rotation

- Credentials stored in `EmailServerSettings` (SmtpServer, Port, Username, encrypted Password, FromAddress, FromName, UseSsl).
- **Rotate:** sign in as admin → `/Admin/EmailServerSettings` → update fields → save → click "שלח מייל בדיקה" (test-send button) → monitor `/Admin/NotificationLogs` for the next 24 h.
- If rotation breaks delivery, revert via `/Admin/AuditLog` (`EmailServerSetting.Update` entries show before/after — the password field is intentionally redacted from the audit payload).

---

## 4. Backup & restore

### 4.1 Nightly backup (SQL Express has no Agent — use Scheduled Tasks)

```powershell
# C:\scripts\backup-axioma.ps1
$stamp = Get-Date -Format "yyyyMMdd-HHmm"
$sql = @"
BACKUP DATABASE [AxiomaReporting]
  TO DISK = N'D:\backups\AxiomaReporting-$stamp.bak'
  WITH INIT, COMPRESSION, CHECKSUM, STATS = 10;
"@
sqlcmd -S .\SQLEXPRESS -Q $sql
# mirror attachments
robocopy "C:\inetpub\AxiomaReporting\wwwroot\uploads" "D:\backups\uploads-$stamp" /MIR /R:2 /W:5
```

Schedule via `schtasks /create /tn AxiomaBackup /tr "powershell C:\scripts\backup-axioma.ps1" /sc daily /st 02:00 /ru SYSTEM`.

### 4.2 Retention

- 14 daily, 12 weekly (Sunday-labeled), 12 monthly (1st-of-month) copies.
- Off-server copy: sync `D:\backups\` to a separate storage target nightly (e.g. Azure Blob via `AzCopy`, a NAS share, or tape).

### 4.3 Restore

```sql
RESTORE DATABASE [AxiomaReporting]
  FROM DISK = N'D:\backups\AxiomaReporting-20260423-0200.bak'
  WITH REPLACE, RECOVERY;
```

Then restore the matching `uploads-<stamp>` folder over `wwwroot\uploads\`.

### 4.4 Quarterly restore drill

Once a quarter, restore the most recent backup to a `AxiomaReportingStaging` database on a staging instance and smoke-test: login as admin, open a report, submit a test row, trigger a reminder send, confirm the `NotificationLog` shows `Sent`. Record the drill in the ops log.

---

## 5. Background jobs & health

Two `IHostedService` workers run inside the web process:

| Service | Cadence | Source |
|---------|---------|--------|
| `ReminderService` | daily 08:00 local | `src/AxiomaReporting.Infrastructure/BackgroundJobs/ReminderService.cs` |
| `NotificationRetryService` | every 5 min | `src/AxiomaReporting.Infrastructure/BackgroundJobs/NotificationRetryService.cs` |

When IIS recycles the app pool, both workers are re-started automatically. They do not rely on SQL Agent or external schedulers.

### Health checks

- `/Admin/NotificationLogs?status=Failed` — chronic SMTP trouble.
- `/Admin/NotificationLogs?status=Abandoned` — permanently failed (after 5 retries); investigate the recipient address / SMTP credentials.
- `/Admin/AuditLog?action=Auth.LoginFailed` within the last hour — possible brute-force attempt.
- Windows Event Log → Application → sources `IIS AspNetCore Module V2`, `Microsoft-Windows-IIS-WAS`.

### Stuck retries

If `NotificationLogs` shows many `Failed` rows with recent `LastAttemptAt`:

1. Verify SMTP credentials at `/Admin/EmailServerSettings`, then use test-send.
2. Wait one retry tick (≤5 min) or click "שלח שוב" on an individual row — the retry service picks up `Status=Pending, NextRetryAt<=now` immediately.
3. If still failing, inspect `FailureReason` column and address the underlying cause (firewall, TLS version, throttling).

---

## 6. Account management incident playbook

### 6.1 Admin locked out

```sql
UPDATE Users
SET StatusId = 1, FailedLoginAttempts = 0
WHERE IdNumber = '<admin-id>';
```

### 6.2 Admin forgot password

Generate a BCrypt hash locally (do **not** paste plain passwords into the server):

```powershell
# using BCrypt.Net-Next via a scratch .NET console
dotnet run --project tools\BcryptHasher -- "<new-plain-password>"
# copy the hash output
```

Then:

```sql
UPDATE Users
SET PasswordHash = '<hash>', MustChangePassword = 1, FailedLoginAttempts = 0, StatusId = 1
WHERE IdNumber = '<admin-id>';
```

### 6.3 Bulk unlock all locked accounts

```sql
UPDATE Users SET StatusId = 1, FailedLoginAttempts = 0 WHERE StatusId = 3;
```

### 6.4 Temporarily disable TFA

```sql
UPDATE SystemConstants SET Value = 'false' WHERE [Key] = 'TfaEmailEnabled';
```

Re-enable after the incident. Because TFA applies at login, the change affects next logins only.

### 6.5 Revert a Terms of Use change

Do **not** delete a `TermsOfUseVersion` row — acceptance records must stay intact for audit. Instead, publish a new version that restores the old body:

- `/Admin/TermsOfUse` → "Publish new version" → paste old body → save. All users will be prompted to accept on next login.

---

## 7. Rollback

### 7.1 Code rollback

- Stop the `AxiomaReporting` app pool in IIS.
- Swap the published folder with the previous artifact (keep the last three deploys on disk for quick rollback).
- Start the app pool.
- Run the post-deploy smoke test (§10).

### 7.2 Database rollback

There is no automatic `down` path. Options:

- **Preferred:** restore the most recent pre-deploy backup (§4.3). Acceptance: lose any work done since the backup.
- **Targeted:** if only a migration misbehaves, write a one-off SQL script that reverses the offending DDL. Do not delete migration rows from `__EFMigrationsHistory` without also reverting the schema changes.

---

## 8. Monitoring

- **IIS app pool health** — check in `aspnet_state` / `w3wp` performance counters.
- **Windows Event Log** — Application log, sources `IIS AspNetCore Module V2` and `.NET Runtime`.
- **DB size growth** — `AuditLogs` grows the fastest (~100–1000 rows/day). Monitor via:

```sql
SELECT OBJECT_NAME(object_id) AS TableName,
       SUM(row_count) AS Rows,
       SUM(reserved_page_count) * 8 / 1024 AS ReservedMB
FROM sys.dm_db_partition_stats
GROUP BY object_id
ORDER BY ReservedMB DESC;
```

### AuditLogs retention

No automatic purge. If the table exceeds ~1 M rows, add a monthly scheduled script that archives rows older than 2 years into a cold-store table (`AuditLogsArchive`) and deletes them from the hot table. Keep indexes in sync.

---

## 9. Known limitations

Operators will run into these — recognize them early:

1. **ClosedXML v0.105 data-validation bug.** Files with a data-validation list string longer than 255 characters throw on import. Observed on the client's `0000ריכוז כולל...xlsx` sample. **Workaround:** re-save the file in Excel (which normalizes the DV list) or upgrade ClosedXML once a fixed version ships. Does not affect normal monthly uploads.

2. **Batch-import allocation resolution rule is pragmatic.** `BatchReportImportService.ResolveAllocation` picks the allocation that matches district/locality/framework/educational-program unambiguously. Rows that resolve to zero or to more than one allocation are reported as import errors. If the client wants a different rule (e.g. first-match wins), change the single method.

3. **Notification retry does not re-attach PDFs.** `NotificationLog` persists subject + body but not attachments (to keep the DB small). If an email with a PDF attachment — currently only `BatchImportErrors` — fails the first send and succeeds on retry, the recipient receives the full text error list in the body but no PDF copy. Acceptable because the body already contains `{ErrorList}`.

4. **`ReminderLogs` and `NotificationLogs` coexist.** `ReminderLogs` is the dedupe source for the daily reminder job ("already sent today"); `NotificationLogs` is the authoritative per-message audit. Do not drop `ReminderLogs` — doing so would re-send reminders already delivered that day.

5. **Optimistic concurrency uses SQL rowversion.** Tests use `Microsoft.EntityFrameworkCore.InMemory`, which does not enforce rowversion automatically. Integration tests for `DbUpdateConcurrencyException` bypass the InMemory provider where needed; production uses `rowversion` columns normally.

---

## 10. Change window & release cadence

- **Preferred window:** Sunday 22:00–23:00 local time (non-school hours, low reporting traffic).
- **Pre-deploy checklist:**
  - Backup runs green within the last 24 h (see `D:\backups\`).
  - Staging smoke test from the latest build completed.
  - Release notes updated in `RELEASE_NOTES.md` (or equivalent).
- **Post-deploy smoke test** (≤10 min, in order):
  1. Sign in as admin; accept terms if prompted.
  2. Open `/Dashboard` — list loads.
  3. Open any employee → open current month report → add a dummy row → save → delete.
  4. `/Admin/EmailServerSettings` → click test-send; confirm inbox receives it.
  5. `/Admin/NotificationLogs` → new row in `Sent` status from step 4.
  6. `/Admin/AuditLog` → new rows for `Auth.LoginSucceeded`, `Report.StatusChange` (if any), `EmailServerSetting.Update` (if changed in step 4).
  7. Upload a bad-format Excel file to `/Report/UploadExcel` → verify on-screen error list + PDF link + `NotificationLog` `BatchImportErrors` row.
  8. Log out.

Record the smoke-test result and any anomalies in the ops channel before closing the change ticket.

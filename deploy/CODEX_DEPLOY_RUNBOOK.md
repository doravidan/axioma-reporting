# Axioma Reporting — Server Deploy Runbook (agent-executable)

Self-contained instructions for deploying **סייט&סאונד חינוך — מערכת דיווח פעילות חודשית**
onto a fresh Windows server. Written so an AI agent (or a human) can execute it top-to-bottom
with copy-paste PowerShell. Every phase ends with a **VERIFY** step — do not continue past a
failed verify.

Companion docs (Hebrew, more narrative): `docs/CLIENT_DELIVERY.md`, `docs/DEPLOY_CHECKLIST.md`,
`docs/OPERATIONS.md`.

---

## 0. Inputs — fill these in before starting

| Placeholder | Meaning | Example |
|---|---|---|
| `{{DOMAIN}}` | Public host name of the site | `reports.example.co.il` |
| `{{DB_PASSWORD}}` | New strong password (16+ chars) for the `AxiomaWeb` SQL login | — |
| `{{ADMIN_PASSWORD}}` | New strong password for the application `admin` user | 8+ chars, letters+digits |
| `{{BAK_PATH}}` | Where the delivery backup file was copied on the server | `C:\deploy\AxiomaReporting_delivery_2026-07-13.bak` |
| `{{SMTP_*}}` | SMTP relay: server, port, user, password, from-address | configured later via the admin UI, not in files |

## Package contents

| Item | Where | In git? |
|---|---|---|
| Source code (build on dev machine or on server) | `https://github.com/doravidan/axioma-reporting.git`, branch `master` | yes |
| Publish script | `deploy/publish.ps1` | yes |
| Full schema, idempotent (structure only) | `database/schema.sql` (~2,100 lines, generated from EF migrations) | yes |
| **Full database backup — structure + all client data** (SQL 2022+ only) | `AxiomaReporting_delivery_2026-07-13.bak` (34 MB, taken 2026-07-13; includes the שמיים-חטיבות-ביניים onboarding + the duplicate-programs merge and program default-scope data) | **NO — contains PII; delivered separately alongside this repo. Never commit it.** |
| **Version-independent data export — same content, works on SQL 2019** | `AxiomaReporting_delivery_2026-07-13.bacpac` (0.7 MB, round-trip verified) | **NO — same PII rule; delivered separately.** |
| Password-normalization script (go-live step §2.2) | `scripts/reset_passwords_to_id.py` | yes |
| This runbook | `deploy/CODEX_DEPLOY_RUNBOOK.md` | yes |

The `.bak` contains the fully imported client dataset. Expected row counts (used in VERIFY steps):

| Table | Rows |
|---|---|
| Users | 492 |
| Allocations | 489 |
| Frameworks | 3,222 |
| Institutions | 4,297 |
| Localities | 1,448 |
| Reports / ReportRows | 29 / 552 (incl. 13 July-2026 test reports — removed in §2.2) |
| ReportingMonths | 3 (יולי 2026 is the active month) |
| EmailTemplates | 12 |
| SystemConstants | 9 |
| Programs | 17 (duplicates merged 2026-07-08) |
| ProjectProgramSubjects | 7,784 |
| `__EFMigrationsHistory` | 25 |

---

## 1. Prerequisites (one-time)

Server: Windows Server 2019+ (2022 recommended), 4 GB+ RAM, 20 GB+ free disk.

```powershell
# 1.1 IIS
Install-WindowsFeature -Name Web-Server, Web-Common-Http, Web-Static-Content,
  Web-Default-Doc, Web-Http-Errors, Web-Http-Logging, Web-Filtering,
  Web-Stat-Compression, Web-Mgmt-Console -IncludeManagementTools

# 1.2 .NET 8 Hosting Bundle (includes ASP.NET Core Module v2 for IIS).
# Download "ASP.NET Core 8.0 Runtime — Hosting Bundle" from
# https://dotnet.microsoft.com/download/dotnet/8.0 and run it, then:
iisreset /restart

# The app targets net6.0 but is published with RollForward=Major, so the
# .NET 8 runtime is sufficient. Do NOT install the EOL .NET 6 runtime.

# 1.3 SQL Server Express — 2022 preferred, 2019 supported.
# The delivery .bak was produced by SQL Server 2022 (16.0) and CANNOT be
# restored on SQL 2019 or older (native backups never downgrade). On a
# SQL 2019 server use §2 Option A2 — import the delivered
# AxiomaReporting_delivery_2026-07-13.bacpac instead (identical data,
# version-independent).
# Install with the "Basic" preset; also install SSMS if a human will manage it.

# 1.4 App folders
New-Item -ItemType Directory -Force -Path "C:\inetpub\AxiomaReporting" | Out-Null
New-Item -ItemType Directory -Force -Path "C:\deploy" | Out-Null
New-Item -ItemType Directory -Force -Path "D:\backups\AxiomaReporting" | Out-Null   # or C:\backups if no D:
```

**VERIFY:**

```powershell
& "$env:windir\system32\inetsrv\appcmd.exe" list modules /name:AspNetCoreModuleV2   # must print the module
sqlcmd -S .\SQLEXPRESS -E -Q "SELECT SERVERPROPERTY('ProductVersion')"              # must be 16.x+ for Option A
```

---

## 2. Database

### Option A — restore the delivery backup (RECOMMENDED: structure + all client data in one step)

```powershell
# Find SQL's default data directory
$paths = sqlcmd -S .\SQLEXPRESS -E -h -1 -W -Q "SET NOCOUNT ON; SELECT CONVERT(nvarchar(500), SERVERPROPERTY('InstanceDefaultDataPath'))"
$data = $paths.Trim()

sqlcmd -S .\SQLEXPRESS -E -Q "RESTORE DATABASE AxiomaReporting FROM DISK = N'{{BAK_PATH}}' WITH MOVE 'AxiomaReporting' TO N'$($data)AxiomaReporting.mdf', MOVE 'AxiomaReporting_log' TO N'$($data)AxiomaReporting_log.ldf', STATS = 25"

sqlcmd -S .\SQLEXPRESS -E -Q "ALTER DATABASE AxiomaReporting SET RECOVERY SIMPLE"
```

> If `MOVE` fails with a logical-name error, list the names first:
> `sqlcmd -S .\SQLEXPRESS -E -Q "RESTORE FILELISTONLY FROM DISK = N'{{BAK_PATH}}'"`

### Option A2 — SQL Server 2019: import the .bacpac (same data, version-independent)

A native `.bak` can never be restored onto an older SQL Server. If the server runs
SQL 2019 (or anything older than 2022), import the delivered
`AxiomaReporting_delivery_2026-07-13.bacpac` instead — identical content,
round-trip verified against the same row counts.

```powershell
# 1) Get sqlpackage (self-contained zip — no .NET SDK needed; do NOT use the
#    "dotnet tool" variant, it may demand a newer .NET patch than installed)
Invoke-WebRequest -Uri "https://aka.ms/sqlpackage-windows" -OutFile "C:\deploy\sqlpackage.zip" -UseBasicParsing
Expand-Archive -Path "C:\deploy\sqlpackage.zip" -DestinationPath "C:\deploy\sqlpackage" -Force

# 2) Pre-create the EMPTY database with the Hebrew collation — importing into a
#    nonexistent DB would silently create it with the server default collation
sqlcmd -S .\SQLEXPRESS -E -Q "CREATE DATABASE AxiomaReporting COLLATE Hebrew_CI_AS; ALTER DATABASE AxiomaReporting SET RECOVERY SIMPLE"

# 3) Import (replace the path with wherever the .bacpac was copied)
& "C:\deploy\sqlpackage\sqlpackage.exe" /Action:Import `
  /SourceFile:"C:\deploy\AxiomaReporting_delivery_2026-07-13.bacpac" `
  /TargetServerName:.\SQLEXPRESS /TargetDatabaseName:AxiomaReporting /TargetTrustServerCertificate:True
```

**VERIFY (A2):**

```powershell
sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -I -W -Q "SET NOCOUNT ON; SELECT (SELECT COUNT(*) FROM Users) Users, (SELECT COUNT(*) FROM Frameworks) Frameworks, (SELECT CONVERT(varchar(50), DATABASEPROPERTYEX(DB_NAME(),'Collation'))) Collation"
# Expect: Users=492, Frameworks=3222, Collation=Hebrew_CI_AS
```

Then continue with §2.1 and §2.2 exactly as for Option A.

### Option B — fresh schema, no data (only if an empty start is wanted)

```powershell
sqlcmd -S .\SQLEXPRESS -E -Q "CREATE DATABASE AxiomaReporting COLLATE Hebrew_CI_AS; ALTER DATABASE AxiomaReporting SET RECOVERY SIMPLE"
sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -I -i "C:\deploy\schema.sql" -o "C:\deploy\schema_log.txt"
# Check C:\deploy\schema_log.txt for errors. schema.sql is idempotent (safe to re-run).
# Data must then be imported through the app: lookup tables via /Lookup ("ייבוא אקסל"),
# employees/allocations via the admin screens or database/seed-data/ Python scripts —
# see docs/CLIENT_DELIVERY.md §9.
```

### 2.1 Application SQL login (both options)

The app must NOT run as `sa` or db_owner:

```powershell
sqlcmd -S .\SQLEXPRESS -E -Q "IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name='AxiomaWeb') CREATE LOGIN [AxiomaWeb] WITH PASSWORD = N'{{DB_PASSWORD}}', CHECK_POLICY = ON, CHECK_EXPIRATION = OFF"
sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -Q "IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name='AxiomaWeb') CREATE USER [AxiomaWeb] FOR LOGIN [AxiomaWeb]; ALTER ROLE db_datareader ADD MEMBER [AxiomaWeb]; ALTER ROLE db_datawriter ADD MEMBER [AxiomaWeb]; GRANT EXECUTE ON SCHEMA::dbo TO [AxiomaWeb]"
```

### 2.2 Post-restore go-live normalization (Option A only — REQUIRED)

The backup comes from the development environment and needs four fixups:

**a) Remove the July-2026 test reports** (created during pre-delivery testing —
the December/January pilot reports stay pending the client's purge decision):

```powershell
sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -I -Q "SET QUOTED_IDENTIFIER ON; DECLARE @ids TABLE (Id int); INSERT INTO @ids SELECT r.Id FROM Reports r JOIN ReportingMonths m ON m.Id = r.ReportingMonthId WHERE m.Year = 2026 AND m.Month = 7; DELETE FROM DocumentAttachments WHERE ReportId IN (SELECT Id FROM @ids) OR ReportRowId IN (SELECT rr.Id FROM ReportRows rr WHERE rr.ReportId IN (SELECT Id FROM @ids)); DELETE FROM ReportRows WHERE ReportId IN (SELECT Id FROM @ids); DELETE FROM Reports WHERE Id IN (SELECT Id FROM @ids); SELECT @@ROWCOUNT AS deleted_reports"
# Expect: deleted_reports = 13
```

**b) Normalize every employee password to their ID number with a forced change
at first login** (the intended onboarding state; a few accounts were used for
dev testing and have other passwords). Requires Python 3.8+ on the machine
running it (can also be run from the dev machine against the server DB by
editing CONN_STR in the script):

```powershell
pip install bcrypt pyodbc
python C:\deploy\source\scripts\reset_passwords_to_id.py            # dry-run first, review the output
python C:\deploy\source\scripts\reset_passwords_to_id.py --commit   # then write
```

**c) Force the admin to rotate immediately** (dev password is `admin1234`):

```powershell
sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -Q "UPDATE Users SET MustChangePassword = 1 WHERE IdNumber = 'admin'"
```

(First login as `admin` / `admin1234` will then demand a new password — use `{{ADMIN_PASSWORD}}`.)

**d) Confirm the active reporting month.** The backup ships with **יולי 2026**
active (deadline 31/07/2026). If go-live happens in a later month, create and
activate the correct month via `/Admin/ReportingMonths` after first login.

**VERIFY (Option A):**

```powershell
sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -I -W -Q "SET NOCOUNT ON; SELECT (SELECT COUNT(*) FROM Users) AS Users, (SELECT COUNT(*) FROM Allocations) AS Allocations, (SELECT COUNT(*) FROM Frameworks) AS Frameworks, (SELECT COUNT(*) FROM __EFMigrationsHistory) AS Migrations, (SELECT COUNT(*) FROM Users WHERE MustChangePassword = 1) AS ForcedChanges"
# Expect: Users=492, Allocations=489, Frameworks=3222, Migrations=25, ForcedChanges=492
```

---

## 3. Application

### 3.1 Build the publish folder

On the dev machine (or on the server if the .NET 8 **SDK** is installed there):

```powershell
git clone https://github.com/doravidan/axioma-reporting.git C:\deploy\source
powershell -ExecutionPolicy Bypass -File C:\deploy\source\deploy\publish.ps1
# Output: C:\deploy\source\deploy\publish\
```

### 3.2 Copy to the server + permissions

```powershell
Copy-Item C:\deploy\source\deploy\publish\* C:\inetpub\AxiomaReporting\ -Recurse -Force
New-Item -ItemType Directory -Force -Path "C:\inetpub\AxiomaReporting\wwwroot\uploads" | Out-Null

icacls "C:\inetpub\AxiomaReporting\wwwroot\uploads" /grant "IIS_IUSRS:(OI)(CI)(M)"
icacls "C:\inetpub\AxiomaReporting\wwwroot\images"  /grant "IIS_IUSRS:(OI)(CI)(M)"   # logo uploads land here
```

> On updates: replace everything EXCEPT `appsettings.Production.json` and `wwwroot\uploads\`.

### 3.3 Production settings file

Create `C:\inetpub\AxiomaReporting\appsettings.Production.json` (this file holds the only
secret in the deployment; it is not in git and must never be):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=AxiomaReporting;User Id=AxiomaWeb;Password={{DB_PASSWORD}};MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  },
  "AllowedHosts": "{{DOMAIN}}",
  "Session": { "TimeoutMinutes": 30 }
}
```

SMTP is NOT configured in this file — it lives in the database and is set through
`/Admin/EmailServerSettings` after first login (§5).

---

## 4. IIS site

```powershell
Import-Module WebAdministration

# App pool — No Managed Code, always running
if (-not (Test-Path IIS:\AppPools\AxiomaReporting)) { New-WebAppPool -Name "AxiomaReporting" | Out-Null }
Set-ItemProperty IIS:\AppPools\AxiomaReporting managedRuntimeVersion ""
Set-ItemProperty IIS:\AppPools\AxiomaReporting startMode AlwaysRunning
Set-ItemProperty IIS:\AppPools\AxiomaReporting processModel.idleTimeout ([TimeSpan]::Zero)

# Site — start on port 80; add the HTTPS binding once the certificate is in the store
if (-not (Get-Website -Name "AxiomaReporting" -ErrorAction SilentlyContinue)) {
  New-Website -Name "AxiomaReporting" -PhysicalPath "C:\inetpub\AxiomaReporting" -ApplicationPool "AxiomaReporting" -Port 80 -HostHeader "{{DOMAIN}}" | Out-Null
}

# Production environment for the site
$env = @{ name = 'ASPNETCORE_ENVIRONMENT'; value = 'Production' }
Add-WebConfigurationProperty -PSPath "IIS:\Sites\AxiomaReporting" -Filter "system.webServer/aspNetCore/environmentVariables" -Name "." -Value $env -ErrorAction SilentlyContinue

Restart-WebAppPool -Name "AxiomaReporting"
```

**HTTPS (required for production):** import the certificate, then:

```powershell
# After importing the cert into LocalMachine\My — find its thumbprint:
Get-ChildItem Cert:\LocalMachine\My | Select-Object Subject, Thumbprint
New-WebBinding -Name "AxiomaReporting" -Protocol https -Port 443 -HostHeader "{{DOMAIN}}" -SslFlags 1
(Get-ChildItem Cert:\LocalMachine\My\<THUMBPRINT>).Thumbprint | ForEach-Object {
  netsh http add sslcert hostnameport="{{DOMAIN}}:443" certhash=$_ appid='{4dc3e181-e14b-4a21-b022-59fc669b0914}' certstorename=MY
}
```

For a free certificate use win-acme (`https://www.win-acme.com`) — it automates issuance +
renewal + the IIS binding for Let's Encrypt. Add the HTTP→HTTPS redirect from
`docs/CLIENT_DELIVERY.md` §6.4 once HTTPS works.

**VERIFY:**

```powershell
Invoke-WebRequest -Uri "http://{{DOMAIN}}/Account/Login" -UseBasicParsing | Select-Object StatusCode
# Expect 200 and a Hebrew login page. If 500.30/500.31: check the Windows Event Log
# (Application → "IIS AspNetCore Module V2") — usually a bad connection string or missing
# Hosting Bundle. If 500.30 mentions framework: confirm .NET 8 Hosting Bundle is installed
# (the app rolls forward from net6.0 automatically).
```

---

## 5. First-run configuration (in the browser)

1. `https://{{DOMAIN}}/Account/Login` → login `admin` / `admin1234` → system forces a
   password change → set `{{ADMIN_PASSWORD}}`.
2. Accept the terms-of-use screen.
3. `/Admin/EmailServerSettings` → enter SMTP server/port/user/password/from → save →
   click **"שלח מייל בדיקה"** → confirm the test email arrives → `/Admin/NotificationLogs`
   should show the row as `Sent`.
4. `/Admin/ReportingMonths` → create the current reporting month and mark it **active**
   (only one month may be active).
5. `/Admin/Branding` → confirm/replace the organization logo.
6. `/Admin/TermsOfUse` → publish the final terms text (all users re-accept on next login).

Data notes (Option A restore):

- Employees log in with **ID number as both username and initial password**, and are forced
  to set a real password on first login.
- The restored data includes 16 reports / 451 report rows from the pilot imports
  (December 2025 + January 2026, after §2.2a removes the July test reports). If the
  client wants a clean start for reports, run `scripts/purge_old_reports.sql` **after** an
  explicit decision on the cutoff month (the script defaults to dry-run).
- The 2026-07-08 backup already includes the שמיים-חטיבות-ביניים onboarding
  (56 employees + allocations, program "תוכנית שמיים - חטיבות ביניים", 78 frameworks) —
  do not re-import that workbook on the server.
- Program→values auto-fill associations (`ProjectProgram*` tables) are currently populated
  for project 1 only; other projects' associations are loaded via
  `/Admin/ProjectPrograms` or the questionnaire-catalog import.

---

## 6. Smoke test (10 minutes, every release)

```
1. Login as admin — home tiles load, no errors
2. /Dashboard → "הצג" → report rows appear; export Excel downloads
3. /Dashboard/Summary → KPI cards + table render
4. /Employee/Index → employee list loads; edit one employee card and save
5. /Report/Index → open, add a dummy row, save, delete it
6. /Admin/AuditLog → shows the login + actions just performed
7. Login as an employee (any ID from the client list, password = same ID) →
   forced password change → terms → פעילות חודשית loads
8. Log out → redirected to the login page
```

---

## 7. Backups (one-time setup)

```powershell
$script = @'
$stamp = Get-Date -Format "yyyy-MM-dd_HHmm"
sqlcmd -S .\SQLEXPRESS -E -Q "BACKUP DATABASE AxiomaReporting TO DISK = N'D:\backups\AxiomaReporting\AxiomaReporting_$stamp.bak' WITH INIT"
Copy-Item -Recurse -Force "C:\inetpub\AxiomaReporting\wwwroot\uploads" "D:\backups\AxiomaReporting\uploads_$stamp"
Get-ChildItem "D:\backups\AxiomaReporting\*.bak" | Sort-Object LastWriteTime -Descending | Select-Object -Skip 14 | Remove-Item -Force
'@
New-Item -ItemType Directory -Force -Path "C:\scripts" | Out-Null
Set-Content -Path "C:\scripts\backup-axioma.ps1" -Value $script -Encoding utf8

Register-ScheduledTask -TaskName "AxiomaBackup" -User "SYSTEM" `
  -Trigger (New-ScheduledTaskTrigger -Daily -At 02:00) `
  -Action (New-ScheduledTaskAction -Execute "powershell.exe" -Argument "-ExecutionPolicy Bypass -File C:\scripts\backup-axioma.ps1")

# Run once now and confirm a .bak lands in D:\backups\AxiomaReporting\
Start-ScheduledTask -TaskName "AxiomaBackup"
```

Configure an off-server copy (NAS / cloud) of `D:\backups\AxiomaReporting\`.

---

## Troubleshooting quick table

| Symptom | Fix |
|---|---|
| HTTP 500.30 / app won't start | Event Log → `IIS AspNetCore Module V2`; usually bad `appsettings.Production.json` connection string, or Hosting Bundle missing |
| Login page loads but login fails with DB error | `AxiomaWeb` login/user missing or wrong password — rerun §2.1 |
| `.bak` restore fails "database version" | Server SQL is older than 2022 → import the `.bacpac` instead (§2 Option A2) |
| Admin locked out | `UPDATE Users SET StatusId=1, FailedLoginAttempts=0 WHERE IdNumber='admin'` |
| Emails not sending | `/Admin/EmailServerSettings` → test button; `/Admin/NotificationLogs` for the error |
| Rollback a release | Stop app pool → restore previous publish folder → start app pool (DB schema is idempotent/backward-safe) |

Ongoing operations (monitoring, updates, restore drills): `docs/OPERATIONS.md`.

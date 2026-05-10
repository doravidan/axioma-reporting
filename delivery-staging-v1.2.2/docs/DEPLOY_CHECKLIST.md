# Deploy Checklist — Axioma Employee Reporting System

Use this for every fresh install or update. Tick each box before moving to the next.

## Pre-install (one-time)

- [ ] Windows Server 2019+ fully patched
- [ ] IIS role installed (`Install-WindowsFeature -Name Web-Server ...`)
- [ ] .NET 8 Hosting Bundle installed → `iisreset` → `dotnet --version` shows `8.0.x`
- [ ] SQL Server Express 2019+ installed, TCP/IP enabled on port 1433
- [ ] SSL certificate obtained and imported into IIS certificate store
- [ ] SMTP relay credentials available (server, port, user, password)

## Database (one-time)

- [ ] `CREATE DATABASE AxiomaReporting COLLATE Hebrew_CI_AS` executed
- [ ] `ALTER DATABASE AxiomaReporting SET RECOVERY SIMPLE` executed
- [ ] SQL login `AxiomaWeb` created with strong password (16+ chars)
- [ ] Roles granted: `db_datareader`, `db_datawriter`, `EXECUTE ON SCHEMA::dbo`
- [ ] `database/schema.sql` executed → no errors in output log

## Application deploy

- [ ] `dotnet publish` run in Release mode → output folder ready
- [ ] Published files copied to `C:\inetpub\AxiomaReporting\`
- [ ] `appsettings.Production.json` created with correct connection string
- [ ] `wwwroot\uploads\` folder has `Modify` permission for App Pool identity
- [ ] `wwwroot\fonts\` and `wwwroot\images\` have `Read` permission for App Pool identity

## IIS configuration

- [ ] App Pool `AxiomaReporting` created — No Managed Code, AlwaysRunning, Idle=0
- [ ] Site created pointing to publish folder, bound to HTTPS + SSL cert
- [ ] Environment variable `ASPNETCORE_ENVIRONMENT=Production` set in site config
- [ ] HTTP → HTTPS redirect rule configured (URL Rewrite module)

## First-run steps

- [ ] Browse to `https://your-domain/` — login page loads without errors
- [ ] Login with `ADMIN` / `Admin123` → forced password change → change to strong password
- [ ] Accept Terms of Use when prompted
- [ ] `/Admin/EmailServerSettings` → fill SMTP details → "שלח מייל בדיקה" → email received
- [ ] `/Admin/Branding` → upload organization logo
- [ ] `/Admin/TermsOfUse` → publish final Terms text (users will accept on next login)
- [ ] `/Admin/ReportingMonths` → create and activate current month
- [ ] Import lookup tables via `/Lookup?tableName=<name>` → "ייבוא אקסל" for each table
- [ ] Create employee records and allocations (or import via batch Excel)
- [ ] Create additional Admin/PM users as needed

## Post-deploy smoke test (every release)

- [ ] Login as Admin — dashboard loads
- [ ] `/Dashboard` with "הצג" — report list appears
- [ ] Add a dummy report row → save → delete — no errors
- [ ] `/Admin/NotificationLogs` — recent rows in `Sent` status
- [ ] `/Admin/AuditLog` — login and action rows present
- [ ] Upload malformed Excel to `/Report/UploadExcel` — error list + PDF shown
- [ ] Log out → redirected to login page

## Backup setup (one-time)

- [ ] `C:\scripts\backup-axioma.ps1` created (see `docs/OPERATIONS.md` §4)
- [ ] Scheduled Task created: daily 02:00 as SYSTEM
- [ ] First backup run manually and verified (`.bak` file in `D:\backups\`)
- [ ] Off-server copy destination configured (NAS / Azure Blob / tape)

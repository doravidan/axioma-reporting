# Exioma / AxiomaReporting Runbook

This guide documents how to run the published Exioma application on this Windows server without IIS, using Kestrel directly.

## Current Working Setup

Application folder:

```powershell
C:\webprojects\Exioma
```

Executable:

```powershell
C:\webprojects\Exioma\AxiomaReporting.Web.exe
```

Working URL:

```text
http://127.0.0.1:5080
```

Working SQL Server:

```text
.\SQLEXPRESS
```

Working database:

```text
AxiomaReporting
```

The previous incomplete `AxiomaReporting` database was preserved as `AxiomaReporting_IncompleteBackup_20260428_143203`. The active working database is now `AxiomaReporting`.

## What This App Is

This folder is a published ASP.NET Core MVC application, not the full source project.

The app is an employee reporting system with:

- Hebrew and RTL UI support.
- SQL Server / Entity Framework Core persistence.
- Monthly reporting workflows.
- Employee, allocation, lookup, institution, framework, and project-program management.
- Excel import/export using ClosedXML and ExcelDataReader.
- PDF generation using QuestPDF.
- Email notifications using MailKit.
- Authentication, password recovery, optional email TFA, audit logs, and terms-of-use tracking.

## Verified Server Prerequisites

The following are already installed/running on this server:

- Windows Server 2019.
- .NET host/runtime `8.0.12`.
- ASP.NET Core runtime `8.0.12`.
- SQL Server Express `.\SQLEXPRESS`.
- SQL Server command-line tool `sqlcmd`.
- SQL Server database `AxiomaReporting` with the schema applied.

Useful verification commands:

```powershell
dotnet --info
```

```powershell
Get-Service MSSQL`$SQLEXPRESS
```

```powershell
sqlcmd -S .\SQLEXPRESS -E -Q "SELECT @@SERVERNAME, @@VERSION;"
```

## Files Of Interest

Main app files:

```text
AxiomaReporting.Web.exe
AxiomaReporting.Web.dll
AxiomaReporting.Core.dll
AxiomaReporting.Infrastructure.dll
appsettings.json
appsettings.Development.json
schema.sql
web.config
wwwroot\
```

Local launcher created for this server:

```text
start-exioma-kestrel.ps1
```

Important static assets:

```text
wwwroot\images\logo.png
wwwroot\fonts\NotoSansHebrew-Regular.ttf
```

The Hebrew font is required for correct Hebrew rendering in generated PDFs.

## Start The App

Open PowerShell and run:

```powershell
cd C:\webprojects\Exioma
.\start-exioma-kestrel.ps1
```

Keep this PowerShell window open. If the window is closed, the app stops.

The script starts `AxiomaReporting.Web.exe` with these settings:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Production"
$env:ASPNETCORE_URLS = "http://127.0.0.1:5080"
$env:ConnectionStrings__DefaultConnection = "Server=.\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

## Open The App

On the server, open:

```text
http://127.0.0.1:5080
```

The root URL should redirect to:

```text
http://127.0.0.1:5080/Account/Login?ReturnUrl=%2F
```

Direct login URL:

```text
http://127.0.0.1:5080/Account/Login
```

## Verify The App Is Running

Check the login page:

```powershell
curl.exe -I http://127.0.0.1:5080/Account/Login
```

Expected result:

```text
HTTP/1.1 200 OK
Server: Kestrel
```

Check root redirect:

```powershell
curl.exe -I http://127.0.0.1:5080/
```

Expected result:

```text
HTTP/1.1 302 Found
Location: http://127.0.0.1:5080/Account/Login?ReturnUrl=%2F
```

Check static files:

```powershell
curl.exe -I http://127.0.0.1:5080/images/logo.png
```

Expected result:

```text
HTTP/1.1 200 OK
Content-Type: image/png
```

Check process:

```powershell
Get-Process AxiomaReporting.Web
```

Check listening port:

```powershell
Get-NetTCPConnection -State Listen | Where-Object { $_.LocalPort -eq 5080 }
```

Expected listener:

```text
127.0.0.1:5080
```

## Stop The App

If running in the foreground PowerShell window, press:

```text
Ctrl+C
```

Or stop it from another PowerShell window:

```powershell
Get-Process AxiomaReporting.Web | Stop-Process
```

## Restart The App

```powershell
cd C:\webprojects\Exioma
.\start-exioma-kestrel.ps1
```

If port `5080` is already in use:

```powershell
Get-NetTCPConnection -State Listen | Where-Object { $_.LocalPort -eq 5080 }
```

If the existing process is the app, stop it:

```powershell
Get-Process AxiomaReporting.Web | Stop-Process
```

Then start again.

## Database Setup

The verified working database is:

```text
AxiomaReporting
```

It was created from:

```text
C:\webprojects\Exioma\schema.sql
```

The schema requires `QUOTED_IDENTIFIER ON` when applied through `sqlcmd`. Use `sqlcmd -I`.

Create and apply the schema:

```powershell
cd C:\webprojects\Exioma

sqlcmd -S .\SQLEXPRESS -E -Q "IF DB_ID(N'AxiomaReporting') IS NULL CREATE DATABASE AxiomaReporting COLLATE Hebrew_CI_AS; ALTER DATABASE AxiomaReporting SET RECOVERY SIMPLE;"

sqlcmd -S .\SQLEXPRESS -E -I -b -f 65001 -d AxiomaReporting -i schema.sql
```

Verify the schema:

```powershell
sqlcmd -S .\SQLEXPRESS -E -d AxiomaReporting -Q "SELECT COUNT(*) AS TableCount FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'; SELECT COUNT(*) AS MigrationCount FROM __EFMigrationsHistory;"
```

Expected result:

```text
TableCount: 54
MigrationCount: 9
```

## Seeded Admin User

The schema seeds one admin user:

```text
ID number: admin
Password: admin123
Role: SystemAdmin
Status: Active
MustChangePassword: true
```

Check seeded users:

```powershell
sqlcmd -S .\SQLEXPRESS -E -d AxiomaReporting -Q "SELECT Id, IdNumber, FirstName, LastName, Email, UserRoleId, StatusId, MustChangePassword FROM Users;"
```

The schema stores the password as a BCrypt hash. The seeded default password is `admin123`, and the user is forced to change it after login.

## Run On A Network-Accessible Port

The current launcher binds only to localhost:

```text
http://127.0.0.1:5080
```

That means the app is only accessible from the server itself.

To allow access from other machines, edit `start-exioma-kestrel.ps1`:

```powershell
$env:ASPNETCORE_URLS = "http://0.0.0.0:5080"
```

Then restart the app.

If Windows Firewall blocks the port, add an inbound rule:

```powershell
New-NetFirewallRule -DisplayName "Exioma Kestrel 5080" -Direction Inbound -Protocol TCP -LocalPort 5080 -Action Allow
```

Then access from another machine:

```text
http://SERVER-IP:5080
```

Use this only for internal/testing access unless HTTPS, reverse proxying, and production security settings are configured.

## Configuration Notes

The direct launcher uses environment variables to override `appsettings.json`.

This avoids two known bad states:

- `appsettings.Development.json` uses SQL login `sa`, which failed on this server.
- `C:\WebSites\Exioma\config\appsettings.Production.json` contains placeholder values such as `Password=CHANGE_ME` and `AllowedHosts=your.domain.co.il`.

The direct run should use:

```text
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=Server=.\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

## Known IIS State

IIS is intentionally ignored for this runbook.

The server has an IIS site named `exioma`, but it is currently not the recommended way to run the app because:

- Its `web.config` has duplicate `system.webServer/aspNetCore` sections.
- One block points to `dotnet .\AxiomaReporting.dll`, but that file does not exist.
- The site uses an app pool configured for CLR `v4.0`.
- The site binding is currently `https://postybell.co.il:443`.

Running directly with Kestrel avoids those IIS issues.

## Troubleshooting

### Login Page Returns 500 With `Invalid object name 'SystemConstants'`

Cause: the app is connected to the wrong/incomplete database.

Fix: confirm the launcher uses `AxiomaReporting`:

```powershell
$env:ConnectionStrings__DefaultConnection = "Server=.\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

### Login Fails For SQL User `sa`

Cause: the app is running in `Development` mode or using `appsettings.Development.json`.

Fix: use `start-exioma-kestrel.ps1`, which sets:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Production"
```

### App Does Not Respond On Port 5080

Check whether the process is running:

```powershell
Get-Process AxiomaReporting.Web
```

Check whether the port is listening:

```powershell
Get-NetTCPConnection -State Listen | Where-Object { $_.LocalPort -eq 5080 }
```

Start the app again:

```powershell
cd C:\webprojects\Exioma
.\start-exioma-kestrel.ps1
```

### Port 5080 Is Already In Use

Find the process:

```powershell
$conn = Get-NetTCPConnection -State Listen | Where-Object { $_.LocalPort -eq 5080 } | Select-Object -First 1
Get-Process -Id $conn.OwningProcess
```

If it is the existing Exioma app, stop it:

```powershell
Get-Process AxiomaReporting.Web | Stop-Process
```

### Schema Apply Fails With `CREATE INDEX failed because QUOTED_IDENTIFIER`

Use `sqlcmd -I` and `-f 65001` so Hebrew seed text is preserved:

```powershell
sqlcmd -S .\SQLEXPRESS -E -I -b -f 65001 -d AxiomaReporting -i schema.sql
```

### Static Logo Or PDF Hebrew Text Is Missing

Confirm these files exist:

```powershell
Test-Path C:\webprojects\Exioma\wwwroot\images\logo.png
Test-Path C:\webprojects\Exioma\wwwroot\fonts\NotoSansHebrew-Regular.ttf
```

## Minimal Daily Run Procedure

Use this when the server is already configured:

```powershell
cd C:\webprojects\Exioma
.\start-exioma-kestrel.ps1
```

Then open:

```text
http://127.0.0.1:5080
```

Verify:

```powershell
curl.exe -I http://127.0.0.1:5080/Account/Login
```

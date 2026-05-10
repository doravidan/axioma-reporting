============================================================
Axioma Employee Reporting System - Delivery Package v1.2
Version: 1.2   |   Date: 2026-05-06
Supersedes: v1.1
============================================================

WHAT'S NEW IN v1.2:

  Allocations:
    - Added scoped allocations dashboard for admins/managers/employees.
    - System admin sees all allocations.
    - Manager/coordinator/inspector users see only allocations assigned to them.
    - Reporting employee sees only their own allocations and can open row details.
    - Allocation pages use the same application visual style, not PDF wireframe styling.

  Monthly activity / Excel upload:
    - Fixed employee Excel upload header detection so second header rows are not imported as data.
    - Fixed district validation during employee Excel upload against the employee allocation.
    - Fixed EF InMemory GroupBy crash when monthly activity rows have attachments.
    - Fixed the same query-shape issue in dashboard missing-report allocation limits.

  Security and QA:
    - Lookup table pages are now admin-only for direct URL access.
    - Added broad admin/employee Playwright QA coverage for primary pages, buttons, filters,
      exports, modal buttons, navigation, and role boundaries.
    - Full automated test suite passed: 480 / 480.

CONTENTS:

  app/                        Published ASP.NET Core 8 application v1.2
                              (deploy to C:\inetpub\AxiomaReporting\)

  database/
    schema.sql                Full idempotent DB schema + seed data
    seed-data/                Python scripts + Excel files for one-time data import

  config/
    appsettings.Production.template.json
                              Rename to appsettings.Production.json,
                              place in app folder, fill in connection string

  docs/
    CLIENT_DELIVERY.md        Full Hebrew installation guide
    DEPLOY_CHECKLIST.md       Tick-box checklist for every install
    OPERATIONS.md             Ongoing operations runbook
    PRODUCTION_VALIDATION.md  Production validation checklist

  scripts/                    Helper scripts used during delivery/validation

============================================================
QUICK START - UPGRADE FROM v1.1:
============================================================

 1. BACKUP first:
      sqlcmd -S .\SQLEXPRESS -Q "BACKUP DATABASE AxiomaReporting
        TO DISK = N'D:\backups\AxiomaReporting-pre-v1.2.bak' WITH INIT"
      robocopy C:\inetpub\AxiomaReporting D:\backups\app-pre-v1.2 /MIR

 2. Stop the AxiomaReporting IIS app pool.

 3. Apply DB schema if not already current:
      sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -i database\schema.sql

 4. Replace C:\inetpub\AxiomaReporting\ contents with app/ from this zip.
    PRESERVE these files/folders:
      - appsettings.Production.json (real DB password)
      - wwwroot\uploads\             (employee attachments)

 5. Start the AxiomaReporting IIS app pool.

 6. Smoke test:
    - Login as admin and open Dashboard, Allocations, Employees, Lookup tables.
    - Login as employee and open My Allocations + Monthly Activity.
    - Upload an employee Excel report and verify no header-row/DistrictId errors.
    - Verify employee cannot open /Lookup/districts directly.

============================================================
ROLLBACK (if needed):
============================================================

  1. Stop app pool
  2. Restore C:\inetpub\AxiomaReporting\ from D:\backups\app-pre-v1.2
  3. Restore DB backup from AxiomaReporting-pre-v1.2.bak
  4. Start app pool

============================================================
SUPPORT
============================================================

For ongoing operations, monitoring, and incident response,
see: docs/OPERATIONS.md

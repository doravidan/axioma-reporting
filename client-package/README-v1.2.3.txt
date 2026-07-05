============================================================
Axioma Employee Reporting System - Delivery Package v1.2.3
Version: 1.2.3   |   Date: 2026-05-10
Supersedes: v1.2.2
============================================================

HOTFIX v1.2.3:

  Allocation and reporting fixes:
    - Employee allocation pages now use the same working allocation UI for create/edit/list flows.
    - Multiple allocations per employee/project are supported.
    - Allocation numeric values display as whole numbers where required.

  Reports dashboard fixes:
    - Dashboard shows detailed report rows across employees, including multiple reports per employee.
    - Removed the status column from the dashboard table.
    - Added edit actions for system admins and scoped inspector users.

  Employee report page fixes:
    - Added direct table-cell editing per report row column.
    - Added report-level document upload.
    - Removed per-row document upload controls.
    - Existing legacy row-attached files are still displayed in the report document list.

DATABASE UPDATE:

  Before replacing the application files, back up the production database.

  Then run the idempotent SQL upgrade script on the server database:

    sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -i database\scripts\upgrade-v1.2.3.sql

  If using SQL authentication, replace -E with:

    -U <user> -P <password>

APPLICATION UPDATE:

  1. Stop the IIS app pool / site.
  2. Back up the current application folder.
  3. Replace the server app files with the contents of app/ from this package.
  4. Keep or restore the server-specific appsettings.Production.json.
  5. Start the IIS app pool / site.
  6. Log in and verify:
       - /Report?allocationId=1 shows one report-level document upload section.
       - report row cells can be edited directly in the table.
       - /Dashboard shows edit actions for system admin.

CONTENTS:

  app/                         Published ASP.NET Core application
  database/schema.sql          Full idempotent EF database script
  database/scripts/upgrade-v1.2.3.sql
                               Idempotent upgrade script for server execution
  config/                      appsettings.Production template
  docs/                        Deployment and operations docs
  scripts/                     Helper scripts

NOTES:

  The package is framework-dependent. Use the same Windows/IIS hosting runtime already used by v1.2.2.
  The SQL script is idempotent and safe to rerun, but still back up the DB first.
============================================================

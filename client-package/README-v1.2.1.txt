============================================================
Axioma Employee Reporting System - Delivery Package v1.2.1
Version: 1.2.1   |   Date: 2026-05-06
Supersedes: v1.2
============================================================

HOTFIX v1.2.1:

  Client verification follow-up:
    - Fresh/demo seeded admin is no longer marked as having accepted Terms of Use.
      First launch now proves the Terms gate before dashboard access.
    - Self-service reset-link password reset now also sets MustChangePassword=true,
      so every reset path forces the next-login password-change screen.
    - Allocation pages and allocation exports no longer use "היקף שעות" or "משך תקופה".
      User-facing allocation wording now uses "היקף פעילות" and "משך תפוקה".
    - Added regression tests for seeded admin Terms defaults, reset-link force-change,
      allocation UI terminology, and allocation Excel export headers.

  Still environment-only verification:
    - Real SMTP delivery requires the client's SMTP settings and permission to send a test email.
      Code path and notification logging are tested, but live delivery cannot be verified while
      SMTP remains disabled or pointed at 127.0.0.1:1.
    - Full production-data Excel upload should be verified on a copy/staging dataset unless the
      client approves report mutations and notification side effects on production.

  Automated tests:
    - Full test suite passed: 483 / 483.

CONTENTS:

  app/                        Published ASP.NET Core 8 application v1.2.1
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
QUICK START - UPGRADE FROM v1.2:
============================================================

 1. BACKUP first:
      sqlcmd -S .\SQLEXPRESS -Q "BACKUP DATABASE AxiomaReporting
        TO DISK = N'D:\backups\AxiomaReporting-pre-v1.2.1.bak' WITH INIT"
      robocopy C:\inetpub\AxiomaReporting D:\backups\app-pre-v1.2.1 /MIR

 2. Stop the AxiomaReporting IIS app pool.

 3. Replace C:\inetpub\AxiomaReporting\ contents with app/ from this zip.
    PRESERVE these files/folders:
      - appsettings.Production.json (real DB password)
      - wwwroot\uploads\             (employee attachments)

 4. Start the AxiomaReporting IIS app pool.

 5. Smoke test:
    - Login as a never-accepted seeded/admin account and confirm Terms appears.
    - Run forgot-password reset link and confirm next login goes to Change Password.
    - Open allocation list/detail/export and verify "היקף פעילות" / "משך תפוקה" wording.
    - Do not test real email or production Excel upload unless the client approves side effects.

============================================================
ROLLBACK (if needed):
============================================================

  1. Stop app pool
  2. Restore C:\inetpub\AxiomaReporting\ from D:\backups\app-pre-v1.2.1
  3. Restore DB backup from AxiomaReporting-pre-v1.2.1.bak if needed
  4. Start app pool

============================================================
SUPPORT
============================================================

For ongoing operations, monitoring, and incident response,
see: docs/OPERATIONS.md

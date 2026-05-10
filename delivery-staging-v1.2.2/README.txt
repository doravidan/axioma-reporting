============================================================
Axioma Employee Reporting System - Delivery Package v1.2.2
Version: 1.2.2   |   Date: 2026-05-06
Supersedes: v1.2.1
============================================================

HOTFIX v1.2.2:

  Admin recovery:
    - Added database/scripts/recover_admin.sql for emergency admin recovery.
    - The seeded admin password hash is verified by automated test as admin1234.
    - Important: forgot-password links can only be sent to users with Email populated.
      The seeded admin account has no real email by default, so update the Email field
      before expecting /Account/ForgotPassword to send a link for admin.

  Previous v1.2.1 fixes included:
    - Fresh/demo seeded admin is not pre-accepted for Terms of Use.
    - Self-service reset-link password reset sets MustChangePassword=true.
    - Allocation wording uses "היקף פעילות" and "משך תפוקה".

  Automated tests:
    - Admin seed/password regression test passed.

ADMIN RECOVERY QUICK COMMAND:

  1. Edit database/scripts/recover_admin.sql and replace:
       admin@example.co.il
     with the real mailbox that should receive reset links.

  2. Run on the client SQL Server:
       sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -i database\scripts\recover_admin.sql

  3. Login:
       user: admin
       password: admin1234

  4. The system will force password change on next login.

CONTENTS:

  app/                        Published ASP.NET Core 8 application
  database/                   Schema, seed data, and recovery scripts
  config/                     appsettings.Production template
  docs/                       Deployment and operations docs
  scripts/                    Helper scripts

============================================================
NOTE ABOUT EMAIL RESET
============================================================

If SMTP is disabled or points to 127.0.0.1:1, no real reset email can be sent.
If the user has no Email value, no reset email can be sent.
Both conditions must be fixed on the server before Forgot Password can deliver a link.

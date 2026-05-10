/*
  Emergency admin recovery script.

  Use only when the seeded admin account has no email address or is locked and
  the forgot-password email flow cannot send a reset link.

  Before running:
  1. Replace admin@example.co.il with the real mailbox that should receive reset links.
  2. Confirm the database name in the USE statement.

  After running:
  - Login with:
      user: admin
      password: admin1234
  - The system will force password change on first login.
  - The admin can also use "Forgot password" because Email is now populated.
*/

USE [AxiomaReporting];
GO

UPDATE [Users]
SET
    [Email] = N'admin@example.co.il',
    [PasswordHash] = N'$2a$12$4MIlxeD2MhS0aLHvy9Gx5.on9xw87chJAN76m8ifdsBb7FvNuMw36',
    [MustChangePassword] = CAST(1 AS bit),
    [AcceptedTermsOfUse] = CAST(0 AS bit),
    [FailedLoginAttempts] = 0,
    [StatusId] = 1,
    [UpdatedAt] = SYSUTCDATETIME()
WHERE [IdNumber] = N'admin';
GO

SELECT [Id], [EmployeeCode], [IdNumber], [Email], [StatusId], [MustChangePassword], [AcceptedTermsOfUse]
FROM [Users]
WHERE [IdNumber] = N'admin';
GO

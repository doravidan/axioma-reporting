SET XACT_ABORT ON;
BEGIN TRANSACTION;
DECLARE @Now datetime2 = SYSUTCDATETIME();
UPDATE dbo.Users SET PasswordHash = N'$2a$12$DmcV9huEiYHMrIgw8EjnkutalbSQxwwALs.5/48dPSWJvaguONwEW', MustChangePassword = 1, LastPasswordChange = @Now, UpdatedAt = @Now WHERE Id = 57;
COMMIT TRANSACTION;

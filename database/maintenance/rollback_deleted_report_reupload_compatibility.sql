/*
  Emergency rollback for the filtered report uniqueness index.
  Default: Preview only. Prefer restoring the verified pre-deployment backup.
  Rollback is intentionally blocked once an employee/month has more than one
  report header (for example an archived report plus a new active report).
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @Apply bit = 0;

SELECT UserId, ReportingMonthId, COUNT_BIG(*) AS HeaderCount
FROM Reports
GROUP BY UserId, ReportingMonthId
HAVING COUNT_BIG(*) > 1;

IF @Apply = 0
BEGIN
  SELECT 'DRY_RUN_ONLY' AS Result;
  RETURN;
END;

IF EXISTS (
  SELECT 1
  FROM Reports
  GROUP BY UserId, ReportingMonthId
  HAVING COUNT_BIG(*) > 1
)
  THROW 51200, 'Rollback stopped: duplicate employee/month headers exist. Restore the backup instead.', 1;

BEGIN TRANSACTION;

IF EXISTS (
  SELECT 1 FROM sys.indexes
  WHERE object_id = OBJECT_ID(N'dbo.Reports')
    AND name = N'IX_Reports_UserId_ReportingMonthId'
)
  DROP INDEX IX_Reports_UserId_ReportingMonthId ON dbo.Reports;

CREATE UNIQUE INDEX IX_Reports_UserId_ReportingMonthId
  ON dbo.Reports(UserId, ReportingMonthId);

COMMIT TRANSACTION;

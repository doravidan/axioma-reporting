/*
  Repair compatibility for logically deleted reports.

  Default: Preview/Dry Run only.
  Apply requires @Apply = 1 and the exact expected archived header/row counts.
  This script does not delete or update Reports, ReportRows, attachments, or
  AuditLogs. It changes only the report uniqueness index so an archived header
  no longer blocks a fresh active report for the same employee/month.

  Deploy the application query-filter fix in the same approved maintenance
  window. Do not run against Production without a verified backup and approval.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @Apply bit = 0;
DECLARE @ExpectedArchivedReportCount bigint = NULL; -- required when @Apply = 1
DECLARE @ExpectedArchivedReportRowCount bigint = NULL; -- required when @Apply = 1

DECLARE @ArchivedReportCount bigint = (
  SELECT COUNT_BIG(*) FROM Reports WHERE IsArchived = 1
);
DECLARE @ArchivedReportRowCount bigint = (
  SELECT COUNT_BIG(*)
  FROM ReportRows rr
  JOIN Reports r ON r.Id = rr.ReportId
  WHERE r.IsArchived = 1
);

SELECT @Apply AS ApplyMode,
       @ArchivedReportCount AS ArchivedReportCount,
       @ArchivedReportRowCount AS ArchivedReportRows,
       (SELECT COUNT_BIG(*)
        FROM ReportRows rr
        LEFT JOIN Reports r ON r.Id = rr.ReportId
        WHERE r.Id IS NULL) AS OrphanReportRows,
       (SELECT COUNT_BIG(*)
        FROM Reports r
        WHERE r.IsArchived = 1
          AND NOT EXISTS (
            SELECT 1 FROM AuditLogs a
            WHERE a.Action = 'Report.BulkArchive'
              AND a.EntityId = CONVERT(nvarchar(200), r.Id)
          )) AS ArchivedReportsWithoutBulkArchiveAudit;

SELECT i.name AS IndexName,
       i.is_unique AS IsUnique,
       i.filter_definition AS FilterDefinition
FROM sys.indexes i
WHERE i.object_id = OBJECT_ID(N'dbo.Reports')
  AND i.name = N'IX_Reports_UserId_ReportingMonthId';

SELECT rm.Year AS ReportingYear, rm.Month AS ReportingMonth,
       COUNT_BIG(DISTINCT r.Id) AS ArchivedReportHeaders,
       COUNT_BIG(DISTINCT rr.Id) AS LinkedReportRows
FROM Reports r
JOIN ReportingMonths rm ON rm.Id = r.ReportingMonthId
LEFT JOIN ReportRows rr ON rr.ReportId = r.Id
WHERE r.IsArchived = 1
GROUP BY rm.Year, rm.Month
ORDER BY rm.Year, rm.Month;

IF @Apply = 0
BEGIN
  SELECT 'DRY_RUN_ONLY' AS Result,
         'Set @Apply=1 and both expected counts to the reviewed values only after backup and approval.' AS Details;
  RETURN;
END;

IF @ExpectedArchivedReportCount IS NULL OR @ExpectedArchivedReportRowCount IS NULL
  THROW 51100, 'Apply stopped: both expected archived header/row counts are required.', 1;

IF @ExpectedArchivedReportCount <> @ArchivedReportCount
  THROW 51101, 'Apply stopped: archived-report count differs from the approved expected count.', 1;

IF @ExpectedArchivedReportRowCount <> @ArchivedReportRowCount
  THROW 51104, 'Apply stopped: archived report-row count differs from the approved expected count.', 1;

IF EXISTS (
  SELECT 1
  FROM Reports
  WHERE IsArchived = 0
  GROUP BY UserId, ReportingMonthId
  HAVING COUNT_BIG(*) > 1
)
  THROW 51102, 'Apply stopped: duplicate active reports already exist for an employee/month.', 1;

BEGIN TRANSACTION;

DECLARE @LockResult int;
EXEC @LockResult = sys.sp_getapplock
  @Resource = N'AxiomaReporting.ReportArchiveRepair',
  @LockMode = N'Exclusive',
  @LockOwner = N'Transaction',
  @LockTimeout = 10000;
IF @LockResult < 0
  THROW 51103, 'Apply stopped: could not acquire the maintenance lock.', 1;

IF NOT EXISTS (
  SELECT 1
  FROM sys.indexes i
  WHERE i.object_id = OBJECT_ID(N'dbo.Reports')
    AND i.name = N'IX_Reports_UserId_ReportingMonthId'
    AND i.is_unique = 1
    AND i.filter_definition LIKE N'%IsArchived%0%'
)
BEGIN
  IF EXISTS (
    SELECT 1 FROM sys.indexes i
    WHERE i.object_id = OBJECT_ID(N'dbo.Reports')
      AND i.name = N'IX_Reports_UserId_ReportingMonthId'
  )
    DROP INDEX IX_Reports_UserId_ReportingMonthId ON dbo.Reports;

  CREATE UNIQUE INDEX IX_Reports_UserId_ReportingMonthId
    ON dbo.Reports(UserId, ReportingMonthId)
    WHERE IsArchived = 0;
END;

COMMIT TRANSACTION;

SELECT 'APPLIED' AS Result,
       @ArchivedReportCount AS ReviewedArchivedReportCount,
       @ArchivedReportRowCount AS ReviewedArchivedReportRowCount,
       i.name AS IndexName,
       i.filter_definition AS FilterDefinition
FROM sys.indexes i
WHERE i.object_id = OBJECT_ID(N'dbo.Reports')
  AND i.name = N'IX_Reports_UserId_ReportingMonthId';

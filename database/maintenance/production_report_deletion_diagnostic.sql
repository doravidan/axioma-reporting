/*
  Read-only diagnostic for logically deleted reports.
  Safety: this script contains SELECT statements only. It does not start a
  write transaction and does not modify Production data.
  Output is deliberately limited to aggregates and internal identifiers.
*/
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @SkyLabel nvarchar(5) =
  NCHAR(1513) + NCHAR(1502) + NCHAR(1497) + NCHAR(1497) + NCHAR(1501);
DECLARE @SkyProgramId int = (
  SELECT TOP (1) Id
  FROM Programs
  WHERE RIGHT(LTRIM(RTRIM(Description)), 5) = @SkyLabel
  ORDER BY Id
);

SELECT 'SkyProgramId' AS Metric,
       COALESCE(CONVERT(varchar(20), @SkyProgramId), 'NOT_FOUND') AS Value;

SELECT 'ArchivedReports' AS Metric, COUNT_BIG(*) AS Value
FROM Reports WHERE IsArchived = 1
UNION ALL
SELECT 'RowsBelongingToArchivedReports', COUNT_BIG(*)
FROM ReportRows rr
JOIN Reports r ON r.Id = rr.ReportId
WHERE r.IsArchived = 1
UNION ALL
SELECT 'ReportLevelAttachmentsBelongingToArchivedReports', COUNT_BIG(*)
FROM DocumentAttachments a
JOIN Reports r ON r.Id = a.ReportId
WHERE r.IsArchived = 1
UNION ALL
SELECT 'RowLevelAttachmentsBelongingToArchivedReports', COUNT_BIG(*)
FROM DocumentAttachments a
JOIN ReportRows rr ON rr.Id = a.ReportRowId
JOIN Reports r ON r.Id = rr.ReportId
WHERE r.IsArchived = 1
UNION ALL
SELECT 'OrphanReportRows', COUNT_BIG(*)
FROM ReportRows rr
LEFT JOIN Reports r ON r.Id = rr.ReportId
WHERE r.Id IS NULL
UNION ALL
SELECT 'BulkArchiveAuditEntries', COUNT_BIG(*)
FROM AuditLogs WHERE Action = 'Report.BulkArchive';

SELECT COUNT(DISTINCT r.Id) AS ArchivedReportsLinkedToSky,
       COUNT(DISTINCT rr.Id) AS ArchivedRowsLinkedToSky,
       COUNT(DISTINCT r.UserId) AS AffectedInternalUsers,
       MIN(r.UpdatedAt) AS FirstUpdatedUtc,
       MAX(r.UpdatedAt) AS LastUpdatedUtc
FROM Reports r
JOIN ReportRows rr ON rr.ReportId = r.Id
JOIN AllocationPrograms ap ON ap.AllocationId = rr.AllocationId
WHERE r.IsArchived = 1
  AND ap.ProgramId = @SkyProgramId;

SELECT TOP (100) r.Id AS InternalReportId,
       r.ReportingMonthId,
       r.StatusId,
       COUNT(DISTINCT rr.Id) AS RowsCount,
       COUNT(DISTINCT CASE WHEN ap.ProgramId = @SkyProgramId THEN rr.Id END) AS SkyLinkedRowsCount
FROM Reports r
LEFT JOIN ReportRows rr ON rr.ReportId = r.Id
LEFT JOIN AllocationPrograms ap ON ap.AllocationId = rr.AllocationId
WHERE r.IsArchived = 1
GROUP BY r.Id, r.ReportingMonthId, r.StatusId
ORDER BY r.Id;

SELECT COUNT_BIG(*) AS ArchivedReportsWithoutBulkArchiveAudit
FROM Reports r
WHERE r.IsArchived = 1
  AND NOT EXISTS (
    SELECT 1
    FROM AuditLogs a
    WHERE a.Action = 'Report.BulkArchive'
      AND a.EntityId = CONVERT(nvarchar(200), r.Id)
  );

SELECT COUNT_BIG(*) AS BulkArchiveAuditsWhoseReportIsNotArchived
FROM AuditLogs a
WHERE a.Action = 'Report.BulkArchive'
  AND NOT EXISTS (
    SELECT 1
    FROM Reports r
    WHERE r.Id = TRY_CONVERT(int, a.EntityId)
      AND r.IsArchived = 1
  );

/*
  Read-only diagnostic for the April 2026 bulk deletion reported for Program
  "שמיים". SELECT statements only; no Production data or schema is changed.

  Important: ReportRows store AllocationId, not a historical ProgramId snapshot.
  A later allocation-program change can therefore make a report disappear from
  a query based only on the allocation's current program. The stable candidate
  set below uses ReportingMonth + Report.BulkArchive Audit and reports current
  program links separately.
*/
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DECLARE @ReportingYear int = 2026;
DECLARE @ReportingMonth int = 4;

;WITH ExampleEmployees(EmployeeCode) AS (
  SELECT EmployeeCode
  FROM (VALUES ('8638'), ('8640'), ('8732'), ('6777')) v(EmployeeCode)
), ExampleReports AS (
  SELECT u.Id AS InternalUserId, u.EmployeeCode, r.Id AS InternalReportId,
         r.ReportingMonthId, r.StatusId, r.IsArchived
  FROM ExampleEmployees e
  JOIN Users u ON u.EmployeeCode = e.EmployeeCode
  JOIN Reports r ON r.UserId = u.Id
  JOIN ReportingMonths rm ON rm.Id = r.ReportingMonthId
  WHERE rm.Month = @ReportingMonth AND rm.Year = @ReportingYear
)
SELECT e.EmployeeCode, e.InternalUserId, e.InternalReportId,
       e.ReportingMonthId, e.StatusId, e.IsArchived,
       COUNT(DISTINCT rr.Id) AS LinkedReportRows,
       COUNT(DISTINCT CASE WHEN MONTH(rr.MeetingDate) = @ReportingMonth THEN rr.Id END) AS AprilActivityRows,
       COUNT(DISTINCT CASE WHEN MONTH(rr.MeetingDate) <> @ReportingMonth THEN rr.Id END) AS OtherActivityMonthRows,
       MIN(rr.MeetingDate) AS FirstActivityDate,
       MAX(rr.MeetingDate) AS LastActivityDate,
       COUNT(DISTINCT reportAttachment.Id) AS ReportAttachments,
       COUNT(DISTINCT rowAttachment.Id) AS RowAttachments,
       COUNT(DISTINCT CASE WHEN audit.Action = 'Report.BulkArchive' THEN audit.Id END) AS BulkArchiveAudits,
       CASE WHEN e.IsArchived = 1 THEN 1 ELSE 0 END AS LegacyEmployeeLookupWouldReturnWithoutArchiveFilter
FROM ExampleReports e
LEFT JOIN ReportRows rr ON rr.ReportId = e.InternalReportId
LEFT JOIN DocumentAttachments reportAttachment ON reportAttachment.ReportId = e.InternalReportId
LEFT JOIN DocumentAttachments rowAttachment ON rowAttachment.ReportRowId = rr.Id
LEFT JOIN AuditLogs audit
  ON audit.EntityType = 'Report'
 AND audit.EntityId = CONVERT(nvarchar(200), e.InternalReportId)
GROUP BY e.EmployeeCode, e.InternalUserId, e.InternalReportId,
         e.ReportingMonthId, e.StatusId, e.IsArchived
ORDER BY e.EmployeeCode;

;WITH AprilBulkArchived AS (
  SELECT DISTINCT r.Id
  FROM Reports r
  JOIN ReportingMonths rm ON rm.Id = r.ReportingMonthId
  JOIN AuditLogs audit
    ON audit.Action = 'Report.BulkArchive'
   AND audit.EntityId = CONVERT(nvarchar(200), r.Id)
  WHERE r.IsArchived = 1
    AND rm.Month = @ReportingMonth
    AND rm.Year = @ReportingYear
)
SELECT
  (SELECT COUNT_BIG(*) FROM AprilBulkArchived) AS ArchivedAprilReportHeaders,
  (SELECT COUNT_BIG(*) FROM ReportRows rr JOIN AprilBulkArchived c ON c.Id = rr.ReportId) AS LinkedReportRows,
  (SELECT COUNT_BIG(*) FROM DocumentAttachments a JOIN AprilBulkArchived c ON c.Id = a.ReportId) AS ReportAttachments,
  (SELECT COUNT_BIG(*)
   FROM DocumentAttachments a
   JOIN ReportRows rr ON rr.Id = a.ReportRowId
   JOIN AprilBulkArchived c ON c.Id = rr.ReportId) AS RowAttachments,
  (SELECT COUNT_BIG(*)
   FROM ReportRows rr
   LEFT JOIN Reports r ON r.Id = rr.ReportId
   WHERE r.Id IS NULL) AS OrphanReportRows;

;WITH ExampleEmployees(EmployeeCode) AS (
  SELECT EmployeeCode
  FROM (VALUES ('8638'), ('8640'), ('8732'), ('6777')) v(EmployeeCode)
)
SELECT u.EmployeeCode, r.Id AS InternalReportId, rr.AllocationId,
       allocation.ProjectId, allocation.IsActive AS AllocationIsActive,
       program.Id AS CurrentProgramId, program.Description AS CurrentProgramName,
       COUNT_BIG(*) AS RowsUsingAllocation
FROM ExampleEmployees e
JOIN Users u ON u.EmployeeCode = e.EmployeeCode
JOIN Reports r ON r.UserId = u.Id
JOIN ReportingMonths rm ON rm.Id = r.ReportingMonthId
JOIN ReportRows rr ON rr.ReportId = r.Id
LEFT JOIN Allocations allocation ON allocation.Id = rr.AllocationId
LEFT JOIN AllocationPrograms allocationProgram ON allocationProgram.AllocationId = allocation.Id
LEFT JOIN Programs program ON program.Id = allocationProgram.ProgramId
WHERE rm.Month = @ReportingMonth AND rm.Year = @ReportingYear
GROUP BY u.EmployeeCode, r.Id, rr.AllocationId, allocation.ProjectId,
         allocation.IsActive, program.Id, program.Description
ORDER BY u.EmployeeCode, rr.AllocationId, program.Id;

-- Confirms whether an archived header is the only employee/month header and
-- would have blocked a new header under the former unfiltered unique index.
;WITH ExampleEmployees(EmployeeCode) AS (
  SELECT EmployeeCode
  FROM (VALUES ('8638'), ('8640'), ('8732'), ('6777')) v(EmployeeCode)
)
SELECT u.EmployeeCode,
       COUNT_BIG(*) AS SameEmployeeAprilHeaders,
       SUM(CASE WHEN r.IsArchived = 0 THEN 1 ELSE 0 END) AS ActiveHeaders,
       SUM(CASE WHEN r.IsArchived = 1 THEN 1 ELSE 0 END) AS ArchivedHeaders
FROM ExampleEmployees e
JOIN Users u ON u.EmployeeCode = e.EmployeeCode
JOIN Reports r ON r.UserId = u.Id
JOIN ReportingMonths rm ON rm.Id = r.ReportingMonthId
WHERE rm.Month = @ReportingMonth AND rm.Year = @ReportingYear
GROUP BY u.EmployeeCode
ORDER BY u.EmployeeCode;

SELECT t.name AS RelevantPhysicalTable
FROM sys.tables t
WHERE t.name LIKE '%Report%'
   OR t.name LIKE '%Activit%'
   OR t.name LIKE '%Import%'
   OR t.name LIKE '%Status%'
   OR t.name LIKE '%Attachment%'
ORDER BY t.name;

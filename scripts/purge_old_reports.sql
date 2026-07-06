-- ניקוי דיווחים ישנים / דיווחי בדיקה (בקשת לקוח: "לנקות דיווחים ישנים")
-- ============================================================================
-- USAGE: set @CutoffYear/@CutoffMonth below, review the SELECT output, and only
-- then flip @DryRun to 0. Deletes reports (and their rows + row-level document
-- attachments) belonging to reporting months BEFORE the cutoff.
-- Take a backup first:
--   BACKUP DATABASE AxiomaReporting TO DISK = N'E:\axioma-reporting\database\backups\before-purge.bak' WITH INIT;
-- Run with: sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -C -I -i scripts\purge_old_reports.sql
-- ============================================================================
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @DryRun bit = 1;          -- 1 = preview only, 0 = actually delete
DECLARE @CutoffYear int = 2025;   -- delete months BEFORE this year/month
DECLARE @CutoffMonth int = 1;

BEGIN TRAN;

DECLARE @months TABLE (Id int PRIMARY KEY);
INSERT INTO @months (Id)
SELECT Id FROM ReportingMonths
WHERE (Year < @CutoffYear) OR (Year = @CutoffYear AND Month < @CutoffMonth);

DECLARE @reports TABLE (Id int PRIMARY KEY);
INSERT INTO @reports (Id)
SELECT r.Id FROM Reports r WHERE r.ReportingMonthId IN (SELECT Id FROM @months);

DECLARE @reportCount int = (SELECT COUNT(*) FROM @reports);
DECLARE @rowCount int = (SELECT COUNT(*) FROM ReportRows WHERE ReportId IN (SELECT Id FROM @reports));
PRINT 'months before cutoff: ' + CAST((SELECT COUNT(*) FROM @months) AS varchar(10));
PRINT 'reports to delete:    ' + CAST(@reportCount AS varchar(10));
PRINT 'report rows to delete:' + CAST(@rowCount AS varchar(10));

IF @DryRun = 1
BEGIN
  PRINT 'DRY-RUN: nothing deleted. Set @DryRun = 0 to purge.';
  ROLLBACK TRAN;
  RETURN;
END

-- row-level attachments -> rows -> report-level attachments -> reports
DELETE FROM DocumentAttachments
WHERE ReportRowId IN (SELECT Id FROM ReportRows WHERE ReportId IN (SELECT Id FROM @reports));
PRINT 'row attachments deleted: ' + CAST(@@ROWCOUNT AS varchar(10));

DELETE FROM ReportRows WHERE ReportId IN (SELECT Id FROM @reports);
PRINT 'rows deleted: ' + CAST(@@ROWCOUNT AS varchar(10));

DELETE FROM DocumentAttachments WHERE ReportId IN (SELECT Id FROM @reports);
PRINT 'report attachments deleted: ' + CAST(@@ROWCOUNT AS varchar(10));

DELETE FROM Reports WHERE Id IN (SELECT Id FROM @reports);
PRINT 'reports deleted: ' + CAST(@@ROWCOUNT AS varchar(10));

COMMIT TRAN;
PRINT 'COMMITTED.';

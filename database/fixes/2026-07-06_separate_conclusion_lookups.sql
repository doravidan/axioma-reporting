-- One-time data correction: separate מסקנה (conclusion) values out of the base
-- SchoolClasses / Frameworks lookups into the dedicated ClassConclusions /
-- FrameworkConclusions tables. The earlier FW import wrongly merged them, which made
-- the כיתה and מסגרת dropdowns show conclusion text.
--
-- RUN ORDER: apply EF migration 20260706072727_SeparateConclusionLookups FIRST
-- (creates the two tables + repoints the ReportRow FKs), THEN run this script.
-- Run with: sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -C -I -i <thisfile>
-- Idempotent and transactional; safe to re-run.

SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRAN;

-- Predicates identifying the mis-imported conclusion rows:
--   SchoolClasses conclusions = non-numeric descriptions (real classes are "1".."15")
--   Frameworks conclusions    = synthetic FW-* symbols (real institutions have numeric symbols)

-- 1) Copy conclusion values into their own tables (idempotent on Description).
INSERT INTO ClassConclusions (Description, IsActive, CreatedAt)
SELECT s.Description, s.IsActive, GETUTCDATE()
FROM SchoolClasses s
WHERE TRY_CONVERT(int, LTRIM(RTRIM(s.Description))) IS NULL
  AND NOT EXISTS (SELECT 1 FROM ClassConclusions c WHERE c.Description = s.Description);

INSERT INTO FrameworkConclusions (Description, IsActive, CreatedAt)
SELECT f.Description, f.IsActive, GETUTCDATE()
FROM Frameworks f
WHERE f.InstitutionSymbol LIKE 'FW-%'
  AND NOT EXISTS (SELECT 1 FROM FrameworkConclusions c WHERE c.Description = f.Description);

DECLARE @cc int = (SELECT COUNT(*) FROM ClassConclusions);
DECLARE @fc int = (SELECT COUNT(*) FROM FrameworkConclusions);
PRINT 'ClassConclusions inserted total   = ' + CAST(@cc AS varchar(10));
PRINT 'FrameworkConclusions inserted total= ' + CAST(@fc AS varchar(10));

-- 2) Drop junction links that point at the conclusion rows (they are not real classes/frameworks).
DELETE FROM AllocationClasses
WHERE ClassId IN (SELECT Id FROM SchoolClasses WHERE TRY_CONVERT(int, LTRIM(RTRIM(Description))) IS NULL);
PRINT 'AllocationClasses (conclusion links) deleted = ' + CAST(@@ROWCOUNT AS varchar(10));

DELETE FROM ProjectProgramClasses
WHERE ClassId IN (SELECT Id FROM SchoolClasses WHERE TRY_CONVERT(int, LTRIM(RTRIM(Description))) IS NULL);
PRINT 'ProjectProgramClasses (conclusion links) deleted = ' + CAST(@@ROWCOUNT AS varchar(10));

DELETE FROM AllocationFrameworks
WHERE FrameworkId IN (SELECT Id FROM Frameworks WHERE InstitutionSymbol LIKE 'FW-%');
PRINT 'AllocationFrameworks (conclusion links) deleted = ' + CAST(@@ROWCOUNT AS varchar(10));

DELETE FROM ProjectProgramFrameworks
WHERE FrameworkId IN (SELECT Id FROM Frameworks WHERE InstitutionSymbol LIKE 'FW-%');
PRINT 'ProjectProgramFrameworks (conclusion links) deleted = ' + CAST(@@ROWCOUNT AS varchar(10));

-- 3) Safety: abort if any ReportRow still references a to-be-deleted row as a base field.
IF EXISTS (
  SELECT 1 FROM ReportRows r JOIN SchoolClasses s ON r.ClassId = s.Id
  WHERE TRY_CONVERT(int, LTRIM(RTRIM(s.Description))) IS NULL)
  OR EXISTS (
  SELECT 1 FROM ReportRows r JOIN Frameworks f ON r.FrameworkId = f.Id
  WHERE f.InstitutionSymbol LIKE 'FW-%')
BEGIN
  PRINT 'ABORT: a ReportRow still references a conclusion row as a base field.';
  ROLLBACK TRAN;
  RETURN;
END

-- 4) Remove the conclusion rows from the base tables.
DELETE FROM SchoolClasses WHERE TRY_CONVERT(int, LTRIM(RTRIM(Description))) IS NULL;
PRINT 'SchoolClasses conclusion rows deleted = ' + CAST(@@ROWCOUNT AS varchar(10));

DELETE FROM Frameworks WHERE InstitutionSymbol LIKE 'FW-%';
PRINT 'Frameworks conclusion rows deleted = ' + CAST(@@ROWCOUNT AS varchar(10));

-- 5) Report final state.
DECLARE @scLeft int = (SELECT COUNT(*) FROM SchoolClasses);
DECLARE @fwLeft int = (SELECT COUNT(*) FROM Frameworks);
DECLARE @ccFin int = (SELECT COUNT(*) FROM ClassConclusions);
DECLARE @fcFin int = (SELECT COUNT(*) FROM FrameworkConclusions);
PRINT '--- FINAL ---';
PRINT 'SchoolClasses remaining      = ' + CAST(@scLeft AS varchar(10));
PRINT 'Frameworks remaining         = ' + CAST(@fwLeft AS varchar(10));
PRINT 'ClassConclusions             = ' + CAST(@ccFin AS varchar(10));
PRINT 'FrameworkConclusions         = ' + CAST(@fcFin AS varchar(10));

COMMIT TRAN;
PRINT 'COMMITTED.';

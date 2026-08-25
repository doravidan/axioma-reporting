/*
  Read-only institution/framework symbol diagnostic.
  Safety: SELECT only. Names and raw symbols are not returned. Example groups
  expose hashes plus internal record identifiers so the result can be shared
  without disclosing institution details.
*/
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

SELECT
  (SELECT COUNT(*) FROM Institutions) AS TotalInstitutions,
  (SELECT COUNT(*) FROM (
     SELECT InstitutionSymbol
     FROM Institutions
     GROUP BY InstitutionSymbol
     HAVING COUNT(*) > 1
   ) d) AS SymbolsDuplicatedGlobally,
  (SELECT COUNT(*) FROM (
     SELECT InstitutionSymbol, EducationalStageId
     FROM Institutions
     GROUP BY InstitutionSymbol, EducationalStageId
     HAVING COUNT(*) > 1
   ) d) AS SymbolsDuplicatedWithinCurrentScope,
  (SELECT COUNT(*) FROM Institutions WHERE EducationalStageId IS NULL) AS NullStageRecords;

;WITH DuplicateInstitutionNumbers AS (
  SELECT InstitutionSymbol, COUNT_BIG(*) AS DuplicateCount
  FROM Institutions
  GROUP BY InstitutionSymbol
  HAVING COUNT_BIG(*) > 1
)
SELECT TOP (100)
  CONVERT(varchar(64), HASHBYTES('SHA2_256', CONVERT(nvarchar(100), d.InstitutionSymbol)), 2) AS NumberHash,
  d.DuplicateCount,
  STRING_AGG(CONVERT(varchar(max), i.Id), ',') WITHIN GROUP (ORDER BY i.Id) AS InternalInstitutionIds,
  STRING_AGG(COALESCE(CONVERT(varchar(max), i.EducationalStageId), 'NULL'), ',')
    WITHIN GROUP (ORDER BY i.Id) AS EducationalStageIds
FROM DuplicateInstitutionNumbers d
JOIN Institutions i ON i.InstitutionSymbol = d.InstitutionSymbol
GROUP BY d.InstitutionSymbol, d.DuplicateCount
ORDER BY d.DuplicateCount DESC;

;WITH FrameworkSymbols AS (
  SELECT Id,
         EducationalStageId,
         InstitutionSymbol,
         LTRIM(RTRIM(InstitutionSymbol)) AS Trimmed,
         REPLACE(REPLACE(LTRIM(RTRIM(InstitutionSymbol)), N'-', N''), N' ', N'') AS Compact
  FROM Frameworks
)
SELECT
  (SELECT COUNT(*) FROM FrameworkSymbols) AS TotalFrameworks,
  (SELECT COUNT(*) FROM (
     SELECT Trimmed FROM FrameworkSymbols GROUP BY Trimmed HAVING COUNT(*) > 1
   ) d) AS TrimmedSymbolsDuplicatedGlobally,
  (SELECT COUNT(*) FROM (
     SELECT Trimmed, EducationalStageId
     FROM FrameworkSymbols
     GROUP BY Trimmed, EducationalStageId
     HAVING COUNT(*) > 1
   ) d) AS TrimmedSymbolsDuplicatedWithinCurrentScope,
  (SELECT COUNT(*) FROM (
     SELECT Compact FROM FrameworkSymbols GROUP BY Compact HAVING COUNT(*) > 1
   ) d) AS CompactSymbolsDuplicatedGlobally,
  (SELECT COUNT(*) FROM (
     SELECT Compact, EducationalStageId
     FROM FrameworkSymbols
     GROUP BY Compact, EducationalStageId
     HAVING COUNT(*) > 1
   ) d) AS CompactSymbolsDuplicatedWithinCurrentScope,
  (SELECT COUNT(*) FROM FrameworkSymbols WHERE EducationalStageId IS NULL) AS NullStageRecords,
  (SELECT COUNT(*) FROM FrameworkSymbols WHERE Trimmed <> InstitutionSymbol) AS SymbolsWithOuterWhitespace,
  (SELECT COUNT(*) FROM FrameworkSymbols WHERE Compact <> Trimmed) AS SymbolsWithSpaceOrHyphen;

;WITH FrameworkSymbols AS (
  SELECT Id,
         EducationalStageId,
         LTRIM(RTRIM(InstitutionSymbol)) AS NormalizedSymbol
  FROM Frameworks
), DuplicateGroups AS (
  SELECT NormalizedSymbol, EducationalStageId, COUNT(*) AS DuplicateCount
  FROM FrameworkSymbols
  GROUP BY NormalizedSymbol, EducationalStageId
  HAVING COUNT(*) > 1
)
SELECT TOP (100)
  CONVERT(varchar(64), HASHBYTES('SHA2_256', g.NormalizedSymbol), 2) AS SymbolHash,
  g.EducationalStageId,
  g.DuplicateCount,
  STRING_AGG(CONVERT(varchar(max), f.Id), ',') WITHIN GROUP (ORDER BY f.Id) AS InternalFrameworkIds
FROM DuplicateGroups g
JOIN FrameworkSymbols f
  ON f.NormalizedSymbol = g.NormalizedSymbol
 AND (f.EducationalStageId = g.EducationalStageId
      OR (f.EducationalStageId IS NULL AND g.EducationalStageId IS NULL))
GROUP BY g.NormalizedSymbol, g.EducationalStageId, g.DuplicateCount
ORDER BY g.DuplicateCount DESC;

SELECT COUNT_BIG(*) AS CrossTableNumericSymbolMatches
FROM Institutions i
JOIN Frameworks f
  ON TRY_CONVERT(bigint,
       REPLACE(REPLACE(LTRIM(RTRIM(f.InstitutionSymbol)), N'-', N''), N' ', N'')) =
     CONVERT(bigint, i.InstitutionSymbol);

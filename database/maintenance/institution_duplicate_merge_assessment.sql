/*
  Read-only assessment for globally duplicated Institution numbers.
  Produces aggregate conflict counts only; it does not expose names or update data.
*/
SET NOCOUNT ON;

;WITH DuplicateSymbols AS (
  SELECT InstitutionSymbol, COUNT_BIG(*) AS DuplicateRowCount
  FROM dbo.Institutions
  GROUP BY InstitutionSymbol
  HAVING COUNT_BIG(*) > 1
)
SELECT COUNT_BIG(*) AS DuplicateGroups,
       SUM(DuplicateRowCount) AS RowsInDuplicateGroups,
       SUM(DuplicateRowCount - 1) AS ExtraRowsBeyondOneCanonical,
       MAX(DuplicateRowCount) AS LargestGroup
FROM DuplicateSymbols;

;WITH DuplicateSymbols AS (
  SELECT InstitutionSymbol
  FROM dbo.Institutions
  GROUP BY InstitutionSymbol
  HAVING COUNT_BIG(*) > 1
), GroupConflicts AS (
  SELECT i.InstitutionSymbol,
         COUNT_BIG(*) AS DuplicateRowCount,
         COUNT(DISTINCT LTRIM(RTRIM(i.Name))) AS NameVariants,
         COUNT(DISTINCT ISNULL(CONVERT(nvarchar(30), i.LocalityId), N'<NULL>')) AS LocalityVariants,
         COUNT(DISTINCT ISNULL(CONVERT(nvarchar(30), i.DistrictId), N'<NULL>')) AS DistrictVariants,
         COUNT(DISTINCT ISNULL(CONVERT(nvarchar(30), i.SectorId), N'<NULL>')) AS SectorVariants,
         COUNT(DISTINCT ISNULL(CONVERT(nvarchar(30), i.TypeId), N'<NULL>')) AS TypeVariants,
         COUNT(DISTINCT ISNULL(CONVERT(nvarchar(30), i.EducationalStageId), N'<NULL>')) AS StageVariants,
         COUNT(DISTINCT CONVERT(int, i.IsActive)) AS ActiveVariants
  FROM dbo.Institutions AS i
  INNER JOIN DuplicateSymbols AS d ON d.InstitutionSymbol = i.InstitutionSymbol
  GROUP BY i.InstitutionSymbol
)
SELECT COUNT_BIG(*) AS DuplicateGroups,
       SUM(CASE WHEN NameVariants > 1 THEN 1 ELSE 0 END) AS GroupsWithNameConflict,
       SUM(CASE WHEN LocalityVariants > 1 THEN 1 ELSE 0 END) AS GroupsWithLocalityConflict,
       SUM(CASE WHEN DistrictVariants > 1 THEN 1 ELSE 0 END) AS GroupsWithDistrictConflict,
       SUM(CASE WHEN SectorVariants > 1 THEN 1 ELSE 0 END) AS GroupsWithSectorConflict,
       SUM(CASE WHEN TypeVariants > 1 THEN 1 ELSE 0 END) AS GroupsWithTypeConflict,
       SUM(CASE WHEN StageVariants > 1 THEN 1 ELSE 0 END) AS GroupsWithStageConflict,
       SUM(CASE WHEN ActiveVariants > 1 THEN 1 ELSE 0 END) AS GroupsWithActiveStateConflict,
       SUM(CASE WHEN NameVariants = 1 AND LocalityVariants = 1 AND DistrictVariants = 1
                     AND SectorVariants = 1 AND TypeVariants = 1 AND ActiveVariants = 1
                THEN 1 ELSE 0 END) AS GroupsDifferingOnlyByStageOrExactDuplicate
FROM GroupConflicts;

;WITH DuplicateSymbols AS (
  SELECT InstitutionSymbol, COUNT_BIG(*) AS DuplicateRowCount
  FROM dbo.Institutions
  GROUP BY InstitutionSymbol
  HAVING COUNT_BIG(*) > 1
)
SELECT DuplicateRowCount AS RowsPerGroup,
       COUNT_BIG(*) AS GroupCount
FROM DuplicateSymbols
GROUP BY DuplicateRowCount
ORDER BY DuplicateRowCount;

SELECT fk.name AS ForeignKeyName,
       OBJECT_SCHEMA_NAME(fk.parent_object_id) AS ReferencingSchema,
       OBJECT_NAME(fk.parent_object_id) AS ReferencingTable,
       parentColumn.name AS ReferencingColumn,
       referencedColumn.name AS ReferencedColumn
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fkc ON fkc.constraint_object_id = fk.object_id
INNER JOIN sys.columns AS parentColumn
  ON parentColumn.object_id = fkc.parent_object_id
 AND parentColumn.column_id = fkc.parent_column_id
INNER JOIN sys.columns AS referencedColumn
  ON referencedColumn.object_id = fkc.referenced_object_id
 AND referencedColumn.column_id = fkc.referenced_column_id
WHERE fk.referenced_object_id = OBJECT_ID(N'dbo.Institutions')
ORDER BY ReferencingSchema, ReferencingTable, ForeignKeyName;

SELECT 'READ_ONLY_DUPLICATE_MERGE_ASSESSMENT' AS Mode,
       CAST(0 AS bit) AS WritesPerformed;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <summary>
    /// Merges duplicate Programs rows (client fix 07/2026 #1): the lookup contained an
    /// old generation of program names next to the canonical "תוכנית X" rows, and
    /// ProjectPrograms linked projects to both — so the allocation screen's program
    /// dropdown showed the same program twice. Mapping is by description (id-independent),
    /// every referencing table is remapped with dedup before the old row is deleted, and
    /// an old row is only deleted once nothing references it. Idempotent — re-running or
    /// running against a DB where the merge already happened is a no-op.
    /// </summary>
    public partial class MergeDuplicatePrograms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET QUOTED_IDENTIFIER ON;

DECLARE @map TABLE (OldDesc nvarchar(200), NewDesc nvarchar(200));
INSERT INTO @map (OldDesc, NewDesc) VALUES
  (N'ועגנים יישוביים',                N'תוכנית עוגנים ישובים רווחה ושיקום'),
  (N'עוגנים ישובים רווחה ושיקום',      N'תוכנית עוגנים ישובים רווחה ושיקום'),
  (N'כיתות שחר',                      N'תוכנית מנחי כיתות שח""ר'),
  (N'מועדוניות ואור בגנים',            N'תוכנית מועדוניות משפחתיות ואור בגנים'),
  (N'מזרח ירושלים',                   N'תוכנית מזרח ירושלים'),
  (N'מחטים וקידום נוער',               N'תוכנית מחטים, קידום נוער ומרכזי נוער'),
  (N'מחטים, קידום נוער ומרכזי נוער',   N'תוכנית מחטים, קידום נוער ומרכזי נוער'),
  (N'מרכזים לגיל הרך',                 N'תוכנית מרכזים לגיל הרך'),
  (N'משיבים',                         N'תוכנית משיבים'),
  (N'קבסים',                          N'תוכנית הנחיית קבסים- ביקור סדיר'),
  (N'שמים',                           N'תוכנית שמיים'),
  (N'תוכניות האגף',                    N'תוכניות האגף- כללי'),
  (N'תוכנית הזנה',                     N'תוכנית ההזנה הלאומית'),
  (N'תוכנית חינוך טכנולגי',            N'תוכנית חינוך טכנולוגי'),
  (N'מניעת נשירה- ג''סר א-זרקא',       N'תוכנית מניעת נשירה- ג''סר א זרקה');

-- Resolve to ids; only pairs where BOTH rows exist (and differ) take part.
DECLARE @m TABLE (OldId int PRIMARY KEY, NewId int);
INSERT INTO @m (OldId, NewId)
SELECT po.Id, pn.Id
FROM @map map
JOIN dbo.Programs po ON po.Description = map.OldDesc
JOIN dbo.Programs pn ON pn.Description = map.NewDesc
WHERE po.Id <> pn.Id;

-- 1) AllocationPrograms: drop the old link when the canonical one already exists,
--    otherwise repoint it.
DELETE ap
FROM dbo.AllocationPrograms ap
JOIN @m m ON m.OldId = ap.ProgramId
WHERE EXISTS (SELECT 1 FROM dbo.AllocationPrograms x
              WHERE x.AllocationId = ap.AllocationId AND x.ProgramId = m.NewId);
UPDATE ap SET ProgramId = m.NewId
FROM dbo.AllocationPrograms ap
JOIN @m m ON m.OldId = ap.ProgramId;

-- 2) InspectorAssignments: repoint (duplicate assignment rows are harmless —
--    scoping is OR across rows).
UPDATE ia SET ProgramId = m.NewId
FROM dbo.InspectorAssignments ia
JOIN @m m ON m.OldId = ia.ProgramId;

-- 3) ProjectPrograms + its seven scope children. Ensure the canonical
--    (ProjectId, NewId) parent exists before moving children onto it.
INSERT INTO dbo.ProjectPrograms (ProjectId, ProgramId)
SELECT DISTINCT pp.ProjectId, m.NewId
FROM dbo.ProjectPrograms pp
JOIN @m m ON m.OldId = pp.ProgramId
WHERE NOT EXISTS (SELECT 1 FROM dbo.ProjectPrograms x
                  WHERE x.ProjectId = pp.ProjectId AND x.ProgramId = m.NewId);

DELETE c FROM dbo.ProjectProgramSubjects c JOIN @m m ON m.OldId = c.ProgramId
WHERE EXISTS (SELECT 1 FROM dbo.ProjectProgramSubjects x
              WHERE x.ProjectId = c.ProjectId AND x.ProgramId = m.NewId AND x.SubjectId = c.SubjectId);
UPDATE c SET ProgramId = m.NewId FROM dbo.ProjectProgramSubjects c JOIN @m m ON m.OldId = c.ProgramId;

DELETE c FROM dbo.ProjectProgramDomains c JOIN @m m ON m.OldId = c.ProgramId
WHERE EXISTS (SELECT 1 FROM dbo.ProjectProgramDomains x
              WHERE x.ProjectId = c.ProjectId AND x.ProgramId = m.NewId AND x.DomainId = c.DomainId);
UPDATE c SET ProgramId = m.NewId FROM dbo.ProjectProgramDomains c JOIN @m m ON m.OldId = c.ProgramId;

DELETE c FROM dbo.ProjectProgramEducationalPrograms c JOIN @m m ON m.OldId = c.ProgramId
WHERE EXISTS (SELECT 1 FROM dbo.ProjectProgramEducationalPrograms x
              WHERE x.ProjectId = c.ProjectId AND x.ProgramId = m.NewId AND x.EducationalProgramId = c.EducationalProgramId);
UPDATE c SET ProgramId = m.NewId FROM dbo.ProjectProgramEducationalPrograms c JOIN @m m ON m.OldId = c.ProgramId;

DELETE c FROM dbo.ProjectProgramDiscussionCodes c JOIN @m m ON m.OldId = c.ProgramId
WHERE EXISTS (SELECT 1 FROM dbo.ProjectProgramDiscussionCodes x
              WHERE x.ProjectId = c.ProjectId AND x.ProgramId = m.NewId AND x.DiscussionCodeId = c.DiscussionCodeId);
UPDATE c SET ProgramId = m.NewId FROM dbo.ProjectProgramDiscussionCodes c JOIN @m m ON m.OldId = c.ProgramId;

DELETE c FROM dbo.ProjectProgramFrameworks c JOIN @m m ON m.OldId = c.ProgramId
WHERE EXISTS (SELECT 1 FROM dbo.ProjectProgramFrameworks x
              WHERE x.ProjectId = c.ProjectId AND x.ProgramId = m.NewId AND x.FrameworkId = c.FrameworkId);
UPDATE c SET ProgramId = m.NewId FROM dbo.ProjectProgramFrameworks c JOIN @m m ON m.OldId = c.ProgramId;

DELETE c FROM dbo.ProjectProgramGradeLevels c JOIN @m m ON m.OldId = c.ProgramId
WHERE EXISTS (SELECT 1 FROM dbo.ProjectProgramGradeLevels x
              WHERE x.ProjectId = c.ProjectId AND x.ProgramId = m.NewId AND x.GradeLevelId = c.GradeLevelId);
UPDATE c SET ProgramId = m.NewId FROM dbo.ProjectProgramGradeLevels c JOIN @m m ON m.OldId = c.ProgramId;

DELETE c FROM dbo.ProjectProgramClasses c JOIN @m m ON m.OldId = c.ProgramId
WHERE EXISTS (SELECT 1 FROM dbo.ProjectProgramClasses x
              WHERE x.ProjectId = c.ProjectId AND x.ProgramId = m.NewId AND x.ClassId = c.ClassId);
UPDATE c SET ProgramId = m.NewId FROM dbo.ProjectProgramClasses c JOIN @m m ON m.OldId = c.ProgramId;

-- Old parent rows are now childless — remove them.
DELETE pp FROM dbo.ProjectPrograms pp JOIN @m m ON m.OldId = pp.ProgramId;

-- 4) Delete the old Programs rows — but never a row something still references
--    (business rule: lookup values in use are never deleted).
DELETE p
FROM dbo.Programs p
JOIN @m m ON m.OldId = p.Id
WHERE NOT EXISTS (SELECT 1 FROM dbo.AllocationPrograms x WHERE x.ProgramId = p.Id)
  AND NOT EXISTS (SELECT 1 FROM dbo.ProjectPrograms x WHERE x.ProgramId = p.Id)
  AND NOT EXISTS (SELECT 1 FROM dbo.InspectorAssignments x WHERE x.ProgramId = p.Id);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Data merge — not reversible (the duplicate rows are intentionally gone).
        }
    }
}

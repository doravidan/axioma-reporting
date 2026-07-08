using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260708104500_SeedProjectSixFrameworkScopeBySymbol")]
    public partial class SeedProjectSixFrameworkScopeBySymbol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET NOCOUNT ON;

DECLARE @FrameworkSeed TABLE (ProgramId int NOT NULL, InstitutionSymbol nvarchar(32) NOT NULL);
INSERT INTO @FrameworkSeed (ProgramId, InstitutionSymbol) VALUES
(100, N'442087'),
(100, N'715797'),
(100, N'761379'),
(100, N'540708'),
(100, N'722132'),
(100, N'361550'),
(100, N'641225'),
(100, N'672568'),
(100, N'338277'),
(100, N'141481'),
(100, N'366864'),
(100, N'580528032'),
(100, N'39491'),
(100, N'632216'),
(100, N'657379'),
(100, N'747337'),
(100, N'541748'),
(100, N'42516'),
(100, N'540526'),
(100, N'544379'),
(100, N'541128'),
(100, N'580338366'),
(100, N'541854'),
(100, N'10541201'),
(100, N'361451'),
(100, N'540963'),
(100, N'541056'),
(100, N'541102'),
(100, N'541151'),
(100, N'541185'),
(100, N'541284'),
(100, N'541631'),
(100, N'541896'),
(100, N'544247'),
(100, N'55120'),
(100, N'580085447'),
(100, N'648410'),
(100, N'544239'),
(100, N'675934'),
(100, N'346031'),
(100, N'441774'),
(100, N'140814'),
(100, N'140921'),
(100, N'141572'),
(100, N'160366'),
(100, N'346098'),
(100, N'366880'),
(100, N'580294437'),
(100, N'633263'),
(100, N'758193'),
(100, N'580432375'),
(100, N'140541'),
(100, N'140673'),
(100, N'140780'),
(100, N'140798'),
(100, N'141044'),
(100, N'184093'),
(100, N'27056'),
(100, N'390590'),
(100, N'53196'),
(100, N'580026383'),
(100, N'580319489'),
(100, N'647206'),
(100, N'722025'),
(100, N'732081'),
(100, N'745968'),
(100, N'747584'),
(100, N'711556'),
(100, N'460162'),
(100, N'160523'),
(100, N'363879'),
(100, N'234047'),
(100, N'738575'),
(100, N'676361'),
(100, N'520317'),
(100, N'580726313'),
(100, N'140681'),
(100, N'770719'),
(100, N'440768'),
(100, N'440800'),
(100, N'580342921'),
(100, N'722058'),
(100, N'444604'),
(97, N'148080'),
(97, N'347047'),
(97, N'348235'),
(97, N'348243'),
(97, N'342337'),
(97, N'248112'),
(97, N'247239'),
(97, N'540617'),
(97, N'448050'),
(97, N'448316'),
(97, N'800128'),
(97, N'648337'),
(97, N'378075'),
(97, N'247155'),
(97, N'248138'),
(97, N'448134'),
(97, N'448209'),
(97, N'448019'),
(97, N'478016'),
(97, N'442566'),
(97, N'448118'),
(97, N'448183'),
(97, N'249169'),
(97, N'548016'),
(97, N'573105'),
(97, N'610006'),
(97, N'800037'),
(97, N'448340'),
(97, N'248013'),
(97, N'800094'),
(97, N'248765'),
(97, N'448167'),
(97, N'648261'),
(97, N'247221'),
(97, N'660233'),
(97, N'248641'),
(97, N'338657'),
(97, N'248146'),
(97, N'247064'),
(97, N'472332'),
(97, N'800052'),
(97, N'648345'),
(97, N'800078'),
(97, N'442822'),
(97, N'247247'),
(97, N'248575'),
(97, N'249284'),
(97, N'348060'),
(97, N'800102'),
(97, N'348227'),
(97, N'248047'),
(97, N'640797'),
(97, N'648303'),
(97, N'248070'),
(97, N'248344'),
(94, N'662296'),
(94, N'662452'),
(94, N'650028'),
(94, N'148247'),
(94, N'641407'),
(94, N'714204'),
(94, N'729871'),
(94, N'540567'),
(94, N'148155');

IF EXISTS (SELECT 1 FROM dbo.Projects WHERE Id = 6)
BEGIN
    INSERT INTO dbo.ProjectPrograms (ProjectId, ProgramId)
    SELECT DISTINCT 6, seed.ProgramId
    FROM @FrameworkSeed seed
    JOIN dbo.Programs program ON program.Id = seed.ProgramId AND program.IsActive = 1
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.ProjectPrograms existing
        WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId
    );

    INSERT INTO dbo.ProjectProgramFrameworks (ProjectId, ProgramId, FrameworkId)
    SELECT DISTINCT 6, seed.ProgramId, framework.Id
    FROM @FrameworkSeed seed
    JOIN dbo.Programs program ON program.Id = seed.ProgramId AND program.IsActive = 1
    JOIN dbo.Frameworks framework ON framework.IsActive = 1
      AND (
        framework.InstitutionSymbol = seed.InstitutionSymbol
        OR TRY_CONVERT(int, framework.InstitutionSymbol) = TRY_CONVERT(int, seed.InstitutionSymbol)
      )
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.ProjectProgramFrameworks existing
        WHERE existing.ProjectId = 6
          AND existing.ProgramId = seed.ProgramId
          AND existing.FrameworkId = framework.Id
    );
END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Additive client-catalog seed. Do not remove framework scope rows that may have been curated after deployment.
        }
    }
}

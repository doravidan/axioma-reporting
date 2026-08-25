using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    public partial class AddProjectProgramLocalities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectProgramLocalities",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    LocalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProgramLocalities", x => new { x.ProjectId, x.ProgramId, x.LocalityId });
                    table.ForeignKey(
                        name: "FK_ProjectProgramLocalities_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgramLocalities_LocalityId",
                table: "ProjectProgramLocalities",
                column: "LocalityId");

            // Preserve the effective locality scope already implied by assigned
            // frameworks. Administrators can refine these values in the new field.
            migrationBuilder.Sql(@"
INSERT INTO dbo.ProjectProgramLocalities (ProjectId, ProgramId, LocalityId)
SELECT scopeRow.ProjectId, scopeRow.ProgramId, institution.LocalityId
FROM dbo.ProjectProgramFrameworks scopeRow
INNER JOIN dbo.Frameworks framework ON framework.Id = scopeRow.FrameworkId
INNER JOIN dbo.Institutions institution
    ON institution.InstitutionSymbol = TRY_CONVERT(int, framework.InstitutionSymbol)
INNER JOIN dbo.Localities locality
    ON locality.Id = institution.LocalityId
   AND locality.IsActive = 1
WHERE institution.LocalityId IS NOT NULL
GROUP BY scopeRow.ProjectId, scopeRow.ProgramId, institution.LocalityId;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProgramLocalities");
        }
    }
}

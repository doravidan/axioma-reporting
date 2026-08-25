using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    public partial class PreserveInstitutionSymbolsAndActiveReportUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS (
  SELECT 1
  FROM Reports
  WHERE IsArchived = 0
  GROUP BY UserId, ReportingMonthId
  HAVING COUNT_BIG(*) > 1
)
  THROW 51000, 'Migration stopped: duplicate active reports exist for an employee/month.', 1;

IF EXISTS (
  SELECT 1
  FROM Frameworks
  GROUP BY LTRIM(RTRIM(InstitutionSymbol)), EducationalStageId
  HAVING COUNT_BIG(*) > 1
)
  THROW 51002, 'Migration stopped: duplicate normalized framework symbols exist in the current educational-stage scope.', 1;");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserId_ReportingMonthId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Institutions_InstitutionSymbol_EducationalStageId",
                table: "Institutions");

            migrationBuilder.DropIndex(
                name: "IX_Frameworks_InstitutionSymbol_EducationalStageId",
                table: "Frameworks");

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionSymbol",
                table: "Institutions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId_ReportingMonthId",
                table: "Reports",
                columns: new[] { "UserId", "ReportingMonthId" },
                unique: true,
                filter: "[IsArchived] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_InstitutionSymbol",
                table: "Institutions",
                column: "InstitutionSymbol");

            // Production contains legacy institution-number duplicates. They
            // must not be merged automatically, but they also must not prevent
            // the unrelated archived-report fix from being deployed. Until the
            // reviewed merge plan is applied, this trigger prevents inserts or
            // number changes from creating any additional duplicates while
            // still allowing non-key edits to legacy duplicate rows.
            migrationBuilder.Sql(@"
EXEC(N'CREATE OR ALTER TRIGGER dbo.TR_Institutions_PreventNewDuplicateSymbol
ON dbo.Institutions
AFTER INSERT, UPDATE
AS
BEGIN
  SET NOCOUNT ON;

  IF EXISTS (
    SELECT 1
    FROM inserted AS currentRow
    LEFT JOIN deleted AS previousRow ON previousRow.Id = currentRow.Id
    WHERE
      (previousRow.Id IS NULL OR
       LTRIM(RTRIM(currentRow.InstitutionSymbol)) <> LTRIM(RTRIM(previousRow.InstitutionSymbol)))
      AND EXISTS (
        SELECT 1
        FROM dbo.Institutions AS otherRow
        WHERE otherRow.Id <> currentRow.Id
          AND LTRIM(RTRIM(otherRow.InstitutionSymbol)) = LTRIM(RTRIM(currentRow.InstitutionSymbol))
      )
  )
    THROW 51011, ''Institution number already exists.'', 1;
END;');");

            migrationBuilder.CreateIndex(
                name: "IX_Frameworks_InstitutionSymbol_EducationalStageId",
                table: "Frameworks",
                columns: new[] { "InstitutionSymbol", "EducationalStageId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.TR_Institutions_PreventNewDuplicateSymbol', N'TR') IS NOT NULL
  DROP TRIGGER dbo.TR_Institutions_PreventNewDuplicateSymbol;");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserId_ReportingMonthId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Institutions_InstitutionSymbol",
                table: "Institutions");

            migrationBuilder.DropIndex(
                name: "IX_Frameworks_InstitutionSymbol_EducationalStageId",
                table: "Frameworks");

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionSymbol",
                table: "Institutions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId_ReportingMonthId",
                table: "Reports",
                columns: new[] { "UserId", "ReportingMonthId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_InstitutionSymbol_EducationalStageId",
                table: "Institutions",
                columns: new[] { "InstitutionSymbol", "EducationalStageId" },
                unique: true,
                filter: "[EducationalStageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Frameworks_InstitutionSymbol_EducationalStageId",
                table: "Frameworks",
                columns: new[] { "InstitutionSymbol", "EducationalStageId" },
                unique: true,
                filter: "[EducationalStageId] IS NOT NULL");
        }
    }
}

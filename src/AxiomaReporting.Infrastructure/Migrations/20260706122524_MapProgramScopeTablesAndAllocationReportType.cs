using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <summary>
    /// Maps the ProjectProgram* scope tables (which already exist on databases that ran
    /// the out-of-branch AddProjectProgramScopeTables migration, complete with imported
    /// data) into the EF model, and adds Allocations.ReportTypeId (client note B32).
    /// All operations are guarded so the migration is safe on both fresh databases and
    /// databases where the physical objects already exist.
    /// </summary>
    public partial class MapProgramScopeTablesAndAllocationReportType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH('Allocations', 'ReportTypeId') IS NULL
BEGIN
    ALTER TABLE [Allocations] ADD [ReportTypeId] int NULL;
END");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Allocations_ReportTypeId' AND object_id = OBJECT_ID('Allocations'))
BEGIN
    CREATE INDEX [IX_Allocations_ReportTypeId] ON [Allocations] ([ReportTypeId]);
END");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Allocations_ReportTypes_ReportTypeId')
BEGIN
    ALTER TABLE [Allocations] ADD CONSTRAINT [FK_Allocations_ReportTypes_ReportTypeId]
        FOREIGN KEY ([ReportTypeId]) REFERENCES [ReportTypes] ([Id]) ON DELETE SET NULL;
END");

            CreateScopeTable(migrationBuilder, "ProjectProgramSubjects", "SubjectId", "Subjects");
            CreateScopeTable(migrationBuilder, "ProjectProgramDomains", "DomainId", "Domains");
            CreateScopeTable(migrationBuilder, "ProjectProgramEducationalPrograms", "EducationalProgramId", "EducationalPrograms");
            CreateScopeTable(migrationBuilder, "ProjectProgramDiscussionCodes", "DiscussionCodeId", "DiscussionCodes");
        }

        private static void CreateScopeTable(
            MigrationBuilder migrationBuilder, string table, string valueColumn, string principalTable)
        {
            migrationBuilder.Sql($@"
IF OBJECT_ID('{table}', 'U') IS NULL
BEGIN
    CREATE TABLE [{table}] (
        [ProjectId] int NOT NULL,
        [ProgramId] int NOT NULL,
        [{valueColumn}] int NOT NULL,
        CONSTRAINT [PK_{table}] PRIMARY KEY ([ProjectId], [ProgramId], [{valueColumn}]),
        CONSTRAINT [FK_{table}_{principalTable}_{valueColumn}]
            FOREIGN KEY ([{valueColumn}]) REFERENCES [{principalTable}] ([Id]) ON DELETE CASCADE
    );
    CREATE INDEX [IX_{table}_{valueColumn}] ON [{table}] ([{valueColumn}]);
END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Allocations_ReportTypes_ReportTypeId')
    ALTER TABLE [Allocations] DROP CONSTRAINT [FK_Allocations_ReportTypes_ReportTypeId];
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Allocations_ReportTypeId' AND object_id = OBJECT_ID('Allocations'))
    DROP INDEX [IX_Allocations_ReportTypeId] ON [Allocations];
IF COL_LENGTH('Allocations', 'ReportTypeId') IS NOT NULL
    ALTER TABLE [Allocations] DROP COLUMN [ReportTypeId];");
            // Scope tables are deliberately NOT dropped on rollback — they may predate
            // this migration and hold imported data.
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    public partial class SeparateConclusionLookups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportRows_Frameworks_ConclusionFrameworkId",
                table: "ReportRows");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportRows_SchoolClasses_ConclusionClassId",
                table: "ReportRows");

            migrationBuilder.CreateTable(
                name: "ClassConclusions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassConclusions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkConclusions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkConclusions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRows_ClassConclusions_ConclusionClassId",
                table: "ReportRows",
                column: "ConclusionClassId",
                principalTable: "ClassConclusions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRows_FrameworkConclusions_ConclusionFrameworkId",
                table: "ReportRows",
                column: "ConclusionFrameworkId",
                principalTable: "FrameworkConclusions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportRows_ClassConclusions_ConclusionClassId",
                table: "ReportRows");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportRows_FrameworkConclusions_ConclusionFrameworkId",
                table: "ReportRows");

            migrationBuilder.DropTable(
                name: "ClassConclusions");

            migrationBuilder.DropTable(
                name: "FrameworkConclusions");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRows_Frameworks_ConclusionFrameworkId",
                table: "ReportRows",
                column: "ConclusionFrameworkId",
                principalTable: "Frameworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRows_SchoolClasses_ConclusionClassId",
                table: "ReportRows",
                column: "ConclusionClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id");
        }
    }
}

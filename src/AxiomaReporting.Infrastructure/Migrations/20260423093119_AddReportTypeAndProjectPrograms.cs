using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReportTypeAndProjectPrograms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportTypeId",
                table: "ReportRows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectPrograms",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPrograms", x => new { x.ProjectId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_ProjectPrograms_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPrograms_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTypes",
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
                    table.PrimaryKey("PK_ReportTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedAt", "IsActive", "Subject", "TypeDescription", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, "שלום {{UploaderName}},\n\nקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נקלט בהצלחה.\n\nסה\"כ דיווחים שנקלטו: {{RowsImported}}\nסה\"כ עובדים: {{EmployeesCount}}\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "קובץ דיווח מרוכז נקלט בהצלחה", "BatchImportSuccessUploader", null },
                    { 10, "שלום {{UploaderName}},\n\nבקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.\nשורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.\n\nרשימת השגיאות המפורטת מצורפת כקובץ PDF.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "שגיאות בקובץ דיווח מרוכז", "BatchImportErrors", null }
                });

            migrationBuilder.InsertData(
                table: "ReportTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ארצי מחוזי", true, null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "יישובי מוסדי", true, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_ReportTypeId",
                table: "ReportRows",
                column: "ReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPrograms_ProgramId",
                table: "ProjectPrograms",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRows_ReportTypes_ReportTypeId",
                table: "ReportRows",
                column: "ReportTypeId",
                principalTable: "ReportTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportRows_ReportTypes_ReportTypeId",
                table: "ReportRows");

            migrationBuilder.DropTable(
                name: "ProjectPrograms");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.DropIndex(
                name: "IX_ReportRows_ReportTypeId",
                table: "ReportRows");

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "ReportTypeId",
                table: "ReportRows");
        }
    }
}

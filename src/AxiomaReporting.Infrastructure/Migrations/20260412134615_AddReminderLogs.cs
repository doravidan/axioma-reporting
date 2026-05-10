using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReminderLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReportingMonthId = table.Column<int>(type: "int", nullable: false),
                    TemplateType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReminderLogs_ReportingMonths_ReportingMonthId",
                        column: x => x.ReportingMonthId,
                        principalTable: "ReportingMonths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReminderLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReminderLogs_ReportingMonthId",
                table: "ReminderLogs",
                column: "ReportingMonthId");

            migrationBuilder.CreateIndex(
                name: "IX_ReminderLogs_UserId_ReportingMonthId_TemplateType_SentAt",
                table: "ReminderLogs",
                columns: new[] { "UserId", "ReportingMonthId", "TemplateType", "SentAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReminderLogs");
        }
    }
}

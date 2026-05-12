using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    public partial class AddReportLevelDocumentAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "DocumentAttachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAttachments_ReportId",
                table: "DocumentAttachments",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAttachments_Reports_ReportId",
                table: "DocumentAttachments",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAttachments_Reports_ReportId",
                table: "DocumentAttachments");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAttachments_ReportId",
                table: "DocumentAttachments");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "DocumentAttachments");
        }
    }
}

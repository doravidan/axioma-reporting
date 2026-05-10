using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReportRequiredFieldsAndConclusionRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SystemConstants",
                columns: new[] { "Id", "CreatedAt", "Description", "Key", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[] { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Developer-level required report fields. Applies to new validation from the point the value is changed.", "RequiredReportFields", null, null, "AllocationId,DistrictId,LocalityId,FrameworkId,EducationalProgramId,DomainId,Subject1Id,MeetingDate,MeetingDuration" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_ConclusionFrameworkId",
                table: "ReportRows",
                column: "ConclusionFrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_ConclusionLocationId",
                table: "ReportRows",
                column: "ConclusionLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRows_Frameworks_ConclusionFrameworkId",
                table: "ReportRows",
                column: "ConclusionFrameworkId",
                principalTable: "Frameworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRows_LocalityDistrictNational_ConclusionLocationId",
                table: "ReportRows",
                column: "ConclusionLocationId",
                principalTable: "LocalityDistrictNationals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportRows_Frameworks_ConclusionFrameworkId",
                table: "ReportRows");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportRows_LocalityDistrictNational_ConclusionLocationId",
                table: "ReportRows");

            migrationBuilder.DropIndex(
                name: "IX_ReportRows_ConclusionFrameworkId",
                table: "ReportRows");

            migrationBuilder.DropIndex(
                name: "IX_ReportRows_ConclusionLocationId",
                table: "ReportRows");

            migrationBuilder.DeleteData(
                table: "SystemConstants",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}

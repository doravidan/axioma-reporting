using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSiteLogoPathConstant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SystemConstants",
                columns: new[] { "Id", "CreatedAt", "Description", "Key", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[] { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "נתיב הלוגו של המערכת (תמונה ב-wwwroot). ניתן להחלפה דרך מסך 'לוגו המערכת'.", "SiteLogoPath", null, null, "/images/logo.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemConstants",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}

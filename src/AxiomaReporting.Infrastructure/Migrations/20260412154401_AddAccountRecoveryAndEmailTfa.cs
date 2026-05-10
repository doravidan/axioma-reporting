using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountRecoveryAndEmailTfa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CodeHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TwoFactorCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedAt", "IsActive", "Subject", "TypeDescription", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, "שלום {{EmployeeName}},\n\nלאיפוס הסיסמה לחץ על הקישור הבא:\n{{ResetLink}}\n\nהקישור תקף לזמן מוגבל.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "איפוס סיסמה", "PasswordReset", null },
                    { 7, "שלום {{EmployeeName}},\n\nקוד האימות שלך הוא: {{Code}}\n\nהקוד תקף ל-{{Minutes}} דקות.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "קוד אימות לכניסה למערכת", "TwoFactorCode", null }
                });

            migrationBuilder.InsertData(
                table: "SystemConstants",
                columns: new[] { "Id", "CreatedAt", "Description", "Key", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[] { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "הפעלת אימות דו-שלבי באמצעות מייל", "TfaEmailEnabled", null, null, "false" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_TokenHash",
                table: "PasswordResetTokens",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserId_ExpiresAt",
                table: "PasswordResetTokens",
                columns: new[] { "UserId", "ExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_TwoFactorCodes_UserId_ExpiresAt",
                table: "TwoFactorCodes",
                columns: new[] { "UserId", "ExpiresAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "TwoFactorCodes");

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SystemConstants",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

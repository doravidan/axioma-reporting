using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordExpiryAndConfigurableReminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReminderLogs_ReportingMonths_ReportingMonthId",
                table: "ReminderLogs");

            migrationBuilder.AlterColumn<int>(
                name: "ReportingMonthId",
                table: "ReminderLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedAt", "IsActive", "Subject", "TypeDescription", "UpdatedAt" },
                values: new object[] { 8, "שלום {{EmployeeName}},\n\nסיסמתך תפוג בעוד {{DaysLeft}} ימים (בתאריך {{ExpiryDate}}).\n\nנא להתחבר למערכת ולשנות את הסיסמה לפני מועד הפקיעה.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "התראה: סיסמתך עומדת לפוג", "PasswordExpiryWarning", null });

            migrationBuilder.InsertData(
                table: "SystemConstants",
                columns: new[] { "Id", "CreatedAt", "Description", "Key", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "כמה שעות בין כל ריצה של שירות התזכורות", "ReminderCheckIntervalHours", null, null, "1" },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "כמה ימים לפני פקיעת הסיסמה לשלוח אזהרה למשתמש", "PasswordExpiryWarningDays", null, null, "14" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ReminderLogs_ReportingMonths_ReportingMonthId",
                table: "ReminderLogs",
                column: "ReportingMonthId",
                principalTable: "ReportingMonths",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReminderLogs_ReportingMonths_ReportingMonthId",
                table: "ReminderLogs");

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SystemConstants",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SystemConstants",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<int>(
                name: "ReportingMonthId",
                table: "ReminderLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReminderLogs_ReportingMonths_ReportingMonthId",
                table: "ReminderLogs",
                column: "ReportingMonthId",
                principalTable: "ReportingMonths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

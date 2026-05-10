using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBatchImportErrorsTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 10,
                column: "Body",
                value: "שלום {{UploaderName}},\n\nבקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.\nשורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.\n\nשורות שלא עברו בדיקת תקינות:\n{{ErrorList}}\n\nרשימת השגיאות המפורטת מצורפת גם כקובץ PDF.\n\nבברכה,\nמערכת אקסיומא");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 10,
                column: "Body",
                value: "שלום {{UploaderName}},\n\nבקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.\nשורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.\n\nרשימת השגיאות המפורטת מצורפת כקובץ PDF.\n\nבברכה,\nמערכת אקסיומא");
        }
    }
}

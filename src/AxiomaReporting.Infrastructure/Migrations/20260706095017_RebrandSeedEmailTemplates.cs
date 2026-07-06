using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    public partial class RebrandSeedEmailTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.\n\nסיבת ההחזרה: {{RejectionReason}}\n\nנא לתקן ולהגיש מחדש.\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nנא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.\n\nהמועד האחרון להגשה: {{Deadline}}.\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.\n\nנא לתקן ולהגיש לפני: {{Deadline}}.\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nלאיפוס הסיסמה לחץ על הקישור הבא:\n{{ResetLink}}\n\nהקישור תקף לזמן מוגבל.\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 7,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nקוד האימות שלך הוא: {{Code}}\n\nהקוד תקף ל-{{Minutes}} דקות.\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 8,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nסיסמתך תפוג בעוד {{DaysLeft}} ימים (בתאריך {{ExpiryDate}}).\n\nנא להתחבר למערכת ולשנות את הסיסמה לפני מועד הפקיעה.\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 9,
                column: "Body",
                value: "שלום {{UploaderName}},\n\nקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נקלט בהצלחה.\n\nסה\"כ דיווחים שנקלטו: {{RowsImported}}\nסה\"כ עובדים: {{EmployeesCount}}\n\nבברכה,\nמערכת סייט&סאונד חינוך");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 10,
                column: "Body",
                value: "שלום {{UploaderName}},\n\nבקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.\nשורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.\n\nשורות שלא עברו בדיקת תקינות:\n{{ErrorList}}\n\nרשימת השגיאות המפורטת מצורפת גם כקובץ PDF.\n\nבברכה,\nמערכת סייט&סאונד חינוך");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.\n\nסיבת ההחזרה: {{RejectionReason}}\n\nנא לתקן ולהגיש מחדש.\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nנא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.\n\nהמועד האחרון להגשה: {{Deadline}}.\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.\n\nנא לתקן ולהגיש לפני: {{Deadline}}.\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nלאיפוס הסיסמה לחץ על הקישור הבא:\n{{ResetLink}}\n\nהקישור תקף לזמן מוגבל.\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 7,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nקוד האימות שלך הוא: {{Code}}\n\nהקוד תקף ל-{{Minutes}} דקות.\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 8,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nסיסמתך תפוג בעוד {{DaysLeft}} ימים (בתאריך {{ExpiryDate}}).\n\nנא להתחבר למערכת ולשנות את הסיסמה לפני מועד הפקיעה.\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 9,
                column: "Body",
                value: "שלום {{UploaderName}},\n\nקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נקלט בהצלחה.\n\nסה\"כ דיווחים שנקלטו: {{RowsImported}}\nסה\"כ עובדים: {{EmployeesCount}}\n\nבברכה,\nמערכת אקסיומא");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 10,
                column: "Body",
                value: "שלום {{UploaderName}},\n\nבקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.\nשורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.\n\nשורות שלא עברו בדיקת תקינות:\n{{ErrorList}}\n\nרשימת השגיאות המפורטת מצורפת גם כקובץ PDF.\n\nבברכה,\nמערכת אקסיומא");
        }
    }
}

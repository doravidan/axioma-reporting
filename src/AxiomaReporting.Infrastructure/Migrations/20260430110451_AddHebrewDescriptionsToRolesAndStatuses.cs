using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHebrewDescriptionsToRolesAndStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TermsOfUseAcceptances",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionHebrew",
                table: "UserStatuses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionHebrew",
                table: "UserRoles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TermsOfUseVersions",
                keyColumn: "Id",
                keyValue: 1,
                column: "BodyHtml",
                value: "<p>ברוכים הבאים למערכת דיווח הפעילות החודשית של סייט&amp;סאונד חינוך.</p><p>השימוש במערכת מותנה בהסכמה לתנאי השימוש הבאים. אנא קראו אותם בעיון לפני האישור.</p><p>1. השימוש במערכת מיועד לעובדים מורשים בלבד, לצורך דיווח פעילות חודשית בלבד. אין להעביר את פרטי הכניסה לאדם אחר ואין להשתמש במערכת בשם משתמש שאינו שלך.</p><p>2. כל הנתונים המוזנים במערכת מהווים דיווח רשמי. עליך לוודא שכל המידע המוזן נכון, מדויק ומשקף את הפעילות שבוצעה בפועל. דיווח כוזב מהווה הפרה של נהלי הארגון.</p><p>3. הארגון רשאי לבצע ביקורת על הדיווחים בכל עת. דיווחים אשר אושרו ננעלים לעריכה ולא ניתן יהיה לשנותם ללא אישור מנהל.</p><p>4. המערכת שומרת יומן ביקורת של כל הפעולות. הגישה למידע מותנית בהרשאות ובהתאם לתפקיד המוגדר במערכת.</p><p>5. הסיסמה שלך אישית וסודית. יש להחליפה כל 90 יום ולא לחזור על 5 הסיסמאות האחרונות. לאחר 3 ניסיונות כניסה כושלים החשבון יינעל אוטומטית.</p><p>גרסה זו של תנאי השימוש מהווה גרסת ביניים — הגרסה המחייבת תפורסם על ידי מנהל המערכת דרך מסך 'תנאי שימוש' תחת תפריט הניהול.</p>");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "DescriptionHebrew",
                value: "מנהל מערכת");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "DescriptionHebrew",
                value: "מנהל פרויקט");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "DescriptionHebrew",
                value: "רכז פרויקט");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "DescriptionHebrew",
                value: "מפקח-צפייה");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "DescriptionHebrew",
                value: "מפקח-אישור");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "DescriptionHebrew",
                value: "עובד");

            migrationBuilder.UpdateData(
                table: "UserStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DescriptionHebrew",
                value: "פעיל");

            migrationBuilder.UpdateData(
                table: "UserStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DescriptionHebrew",
                value: "לא פעיל");

            migrationBuilder.UpdateData(
                table: "UserStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DescriptionHebrew",
                value: "נעול");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$12$4MIlxeD2MhS0aLHvy9Gx5.on9xw87chJAN76m8ifdsBb7FvNuMw36");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionHebrew",
                table: "UserStatuses");

            migrationBuilder.DropColumn(
                name: "DescriptionHebrew",
                table: "UserRoles");

            migrationBuilder.InsertData(
                table: "TermsOfUseAcceptances",
                columns: new[] { "Id", "AcceptedAt", "IpAddress", "UserId", "VersionId" },
                values: new object[] { 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, 1 });

            migrationBuilder.UpdateData(
                table: "TermsOfUseVersions",
                keyColumn: "Id",
                keyValue: 1,
                column: "BodyHtml",
                value: "תנאי שימוש — יסופקו על ידי הלקוח");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewdGADNUvDdAfY2.");
        }
    }
}

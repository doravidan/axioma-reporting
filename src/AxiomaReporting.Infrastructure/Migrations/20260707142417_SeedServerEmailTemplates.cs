using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <summary>
    /// Seeds the two email templates that exist on the client-server build but
    /// not here: Welcome (first-login credentials note) and ReminderToReport.
    /// Guarded inserts — the client's live DB already has both.
    /// </summary>
    public partial class SeedServerEmailTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM dbo.EmailTemplates WHERE TypeDescription = 'Welcome')
    INSERT INTO dbo.EmailTemplates (TypeDescription, Subject, Body, IsActive, CreatedAt)
    VALUES (N'Welcome',
            N'ברוכים הבאים למערכת סייט אנד סאונד',
            N'שלום {{EmployeeName}}, חשבונך נוצר במערכת סייט אנד סאונד. יש להתחבר ולהחליף סיסמה ראשונית.',
            1, SYSUTCDATETIME());

IF NOT EXISTS (SELECT 1 FROM dbo.EmailTemplates WHERE TypeDescription = 'ReminderToReport')
    INSERT INTO dbo.EmailTemplates (TypeDescription, Subject, Body, IsActive, CreatedAt)
    VALUES (N'ReminderToReport',
            N'תזכורת: דיווח פעילות חודשית',
            N'שלום {{EmployeeName}}, נא להשלים את דיווח הפעילות החודשית במערכת.',
            1, SYSUTCDATETIME());
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM dbo.EmailTemplates WHERE TypeDescription IN ('Welcome', 'ReminderToReport');
");
        }
    }
}

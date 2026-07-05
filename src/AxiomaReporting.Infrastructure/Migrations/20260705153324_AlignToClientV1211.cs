using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    public partial class AlignToClientV1211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // All schema operations are guarded so the migration is idempotent:
            // the production DB already has these columns/tables (applied by the
            // client via ad-hoc SQL), while local dev DBs do not.
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Reports', N'IsArchived') IS NULL
BEGIN
    ALTER TABLE [Reports] ADD [IsArchived] bit NOT NULL DEFAULT CAST(0 AS bit);
END;");

            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Allocations', N'ReportTypeId') IS NULL
BEGIN
    ALTER TABLE [Allocations] ADD [ReportTypeId] int NULL;
END;");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'PrivacyPolicyVersions', N'U') IS NULL
BEGIN
    CREATE TABLE [PrivacyPolicyVersions] (
        [Id] int NOT NULL IDENTITY,
        [VersionNumber] int NOT NULL,
        [BodyHtml] nvarchar(max) NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [PublishedByUserId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_PrivacyPolicyVersions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PrivacyPolicyVersions_Users_PublishedByUserId] FOREIGN KEY ([PublishedByUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.\n\nסיבת ההחזרה: {{RejectionReason}}\n\nנא לתקן ולהגיש מחדש.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nנא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.\n\nהמועד האחרון להגשה: {{Deadline}}.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.\n\nנא לתקן ולהגיש לפני: {{Deadline}}.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nלאיפוס הסיסמה לחץ על הקישור הבא:\n{{ResetLink}}\n\nהקישור תקף לזמן מוגבל.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 7,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nקוד האימות שלך הוא: {{Code}}\n\nהקוד תקף ל-{{Minutes}} דקות.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 8,
                column: "Body",
                value: "שלום {{EmployeeName}},\n\nסיסמתך תפוג בעוד {{DaysLeft}} ימים (בתאריך {{ExpiryDate}}).\n\nנא להתחבר למערכת ולשנות את הסיסמה לפני מועד הפקיעה.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 9,
                column: "Body",
                value: "שלום {{UploaderName}},\n\nקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נקלט בהצלחה.\n\nסה\"כ דיווחים שנקלטו: {{RowsImported}}\nסה\"כ עובדים: {{EmployeesCount}}\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 10,
                column: "Body",
                value: "שלום {{UploaderName}},\n\nבקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.\nשורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.\n\nשורות שלא עברו בדיקת תקינות:\n{{ErrorList}}\n\nרשימת השגיאות המפורטת מצורפת גם כקובץ Excel.\n\nבברכה,\nמערכת סייט אנד סאונד");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Allocations_ReportTypeId' AND object_id = OBJECT_ID(N'Allocations'))
BEGIN
    CREATE INDEX [IX_Allocations_ReportTypeId] ON [Allocations] ([ReportTypeId]);
END;");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_PrivacyPolicyVersion_VersionNumber' AND object_id = OBJECT_ID(N'PrivacyPolicyVersions'))
BEGIN
    CREATE UNIQUE INDEX [IX_PrivacyPolicyVersion_VersionNumber] ON [PrivacyPolicyVersions] ([VersionNumber]);
END;");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_PrivacyPolicyVersions_PublishedByUserId' AND object_id = OBJECT_ID(N'PrivacyPolicyVersions'))
BEGIN
    CREATE INDEX [IX_PrivacyPolicyVersions_PublishedByUserId] ON [PrivacyPolicyVersions] ([PublishedByUserId]);
END;");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Allocations_ReportTypes_ReportTypeId' AND parent_object_id = OBJECT_ID(N'Allocations'))
BEGIN
    ALTER TABLE [Allocations] ADD CONSTRAINT [FK_Allocations_ReportTypes_ReportTypeId] FOREIGN KEY ([ReportTypeId]) REFERENCES [ReportTypes] ([Id]) ON DELETE NO ACTION;
END;");

            // Project/Program scope tables used by the client build through ad-hoc SQL
            // (no EF entities). Composite PK (ProjectId, ProgramId, <ScopeCol>) with FKs
            // to Projects/Programs and the relevant lookup table.
            CreateProjectProgramScopeTable(migrationBuilder, "ProjectProgramSubjects", "SubjectId", "Subjects");
            CreateProjectProgramScopeTable(migrationBuilder, "ProjectProgramDomains", "DomainId", "Domains");
            CreateProjectProgramScopeTable(migrationBuilder, "ProjectProgramFrameworks", "FrameworkId", "Frameworks");
            CreateProjectProgramScopeTable(migrationBuilder, "ProjectProgramEducationalPrograms", "EducationalProgramId", "EducationalPrograms");
            CreateProjectProgramScopeTable(migrationBuilder, "ProjectProgramDiscussionCodes", "DiscussionCodeId", "DiscussionCodes");
            CreateProjectProgramScopeTable(migrationBuilder, "ProjectProgramGradeLevels", "GradeLevelId", "GradeLevels");
            CreateProjectProgramScopeTable(migrationBuilder, "ProjectProgramClasses", "ClassId", "SchoolClasses");
        }

        private static void CreateProjectProgramScopeTable(MigrationBuilder migrationBuilder, string tableName, string scopeColumn, string lookupTable)
        {
            migrationBuilder.Sql($@"
IF OBJECT_ID(N'{tableName}', N'U') IS NULL
BEGIN
    CREATE TABLE [{tableName}] (
        [ProjectId] int NOT NULL,
        [ProgramId] int NOT NULL,
        [{scopeColumn}] int NOT NULL,
        CONSTRAINT [PK_{tableName}] PRIMARY KEY ([ProjectId], [ProgramId], [{scopeColumn}]),
        CONSTRAINT [FK_{tableName}_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_{tableName}_Programs_ProgramId] FOREIGN KEY ([ProgramId]) REFERENCES [Programs] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_{tableName}_{lookupTable}_{scopeColumn}] FOREIGN KEY ([{scopeColumn}]) REFERENCES [{lookupTable}] ([Id]) ON DELETE NO ACTION
    );
    CREATE INDEX [IX_{tableName}_{scopeColumn}] ON [{tableName}] ([{scopeColumn}]);
END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allocations_ReportTypes_ReportTypeId",
                table: "Allocations");

            migrationBuilder.DropTable(
                name: "PrivacyPolicyVersions");

            migrationBuilder.DropIndex(
                name: "IX_Allocations_ReportTypeId",
                table: "Allocations");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportTypeId",
                table: "Allocations");

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

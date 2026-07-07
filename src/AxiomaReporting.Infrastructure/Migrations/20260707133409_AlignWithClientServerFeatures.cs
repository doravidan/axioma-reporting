using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <summary>
    /// Aligns the schema with the client-server build (v1.2.x line):
    /// PrivacyPolicyVersions, Reports.IsArchived, and the three ProjectProgram
    /// scope tables that only existed there (Frameworks/GradeLevels/Classes).
    /// Written as guarded idempotent SQL because every one of these objects may
    /// already exist — the dev DB got some via out-of-repo migrations and the
    /// client's live DB got all of them via its own upgrade scripts.
    /// </summary>
    public partial class AlignWithClientServerFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Reports', 'IsArchived') IS NULL
    ALTER TABLE dbo.Reports ADD IsArchived bit NOT NULL CONSTRAINT DF_Reports_IsArchived DEFAULT(0);
");

            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.PrivacyPolicyVersions', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PrivacyPolicyVersions (
        Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PrivacyPolicyVersions PRIMARY KEY,
        VersionNumber int NOT NULL,
        BodyHtml nvarchar(max) NOT NULL,
        EffectiveFrom datetime2 NOT NULL,
        PublishedByUserId int NOT NULL,
        CreatedAt datetime2 NOT NULL,
        UpdatedAt datetime2 NULL,
        CONSTRAINT FK_PrivacyPolicyVersions_Users_PublishedByUserId
            FOREIGN KEY (PublishedByUserId) REFERENCES dbo.Users (Id) ON DELETE NO ACTION
    );
    CREATE UNIQUE INDEX IX_PrivacyPolicyVersions_VersionNumber ON dbo.PrivacyPolicyVersions (VersionNumber);
    CREATE INDEX IX_PrivacyPolicyVersions_PublishedByUserId ON dbo.PrivacyPolicyVersions (PublishedByUserId);
END
");

            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.ProjectProgramFrameworks', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProjectProgramFrameworks (
        ProjectId int NOT NULL,
        ProgramId int NOT NULL,
        FrameworkId int NOT NULL,
        CONSTRAINT PK_ProjectProgramFrameworks PRIMARY KEY (ProjectId, ProgramId, FrameworkId),
        CONSTRAINT FK_ProjectProgramFrameworks_Frameworks_FrameworkId
            FOREIGN KEY (FrameworkId) REFERENCES dbo.Frameworks (Id) ON DELETE CASCADE
    );
    CREATE INDEX IX_ProjectProgramFrameworks_FrameworkId ON dbo.ProjectProgramFrameworks (FrameworkId);
END
");

            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.ProjectProgramGradeLevels', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProjectProgramGradeLevels (
        ProjectId int NOT NULL,
        ProgramId int NOT NULL,
        GradeLevelId int NOT NULL,
        CONSTRAINT PK_ProjectProgramGradeLevels PRIMARY KEY (ProjectId, ProgramId, GradeLevelId),
        CONSTRAINT FK_ProjectProgramGradeLevels_GradeLevels_GradeLevelId
            FOREIGN KEY (GradeLevelId) REFERENCES dbo.GradeLevels (Id) ON DELETE CASCADE
    );
    CREATE INDEX IX_ProjectProgramGradeLevels_GradeLevelId ON dbo.ProjectProgramGradeLevels (GradeLevelId);
END
");

            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.ProjectProgramClasses', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProjectProgramClasses (
        ProjectId int NOT NULL,
        ProgramId int NOT NULL,
        ClassId int NOT NULL,
        CONSTRAINT PK_ProjectProgramClasses PRIMARY KEY (ProjectId, ProgramId, ClassId),
        CONSTRAINT FK_ProjectProgramClasses_SchoolClasses_ClassId
            FOREIGN KEY (ClassId) REFERENCES dbo.SchoolClasses (Id) ON DELETE CASCADE
    );
    CREATE INDEX IX_ProjectProgramClasses_ClassId ON dbo.ProjectProgramClasses (ClassId);
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // The scope tables may hold imported client data — Down only removes
            // the objects that are exclusively ours.
            migrationBuilder.Sql(@"
IF OBJECT_ID('dbo.PrivacyPolicyVersions', 'U') IS NOT NULL
    DROP TABLE dbo.PrivacyPolicyVersions;
");
        }
    }
}

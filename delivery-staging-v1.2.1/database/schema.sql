-- =============================================================================
-- Axioma Employee Reporting System — Full Database Schema (v1.1)
-- Generated: 2026-04-30  |  EF Core idempotent script (safe to re-run)
-- Target: SQL Server Express 2019+  |  Collation: Hebrew_CI_AS
--
-- Prerequisites (run once before this script):
--   CREATE DATABASE AxiomaReporting COLLATE Hebrew_CI_AS;
--   ALTER DATABASE AxiomaReporting SET RECOVERY SIMPLE;
--
-- v1.1 adds:
--   * UserRoles.DescriptionHebrew (nvarchar 200) — Hebrew labels for the 6 roles
--   * UserStatuses.DescriptionHebrew (nvarchar 200) — Hebrew labels for 3 statuses
--   * Replaces placeholder Terms-of-Use body with real Hebrew content
--   * Removes auto-seeded admin TermsOfUseAcceptance row (so first launch shows the gate)
--
-- Run as a user with db_ddladmin + db_datawriter on AxiomaReporting.
-- The script is idempotent — every CREATE TABLE and INSERT is guarded
-- by IF NOT EXISTS so it is safe to re-execute on an existing database.
-- =============================================================================

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Authorities] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Authorities] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [DiscussionCodes] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_DiscussionCodes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Districts] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Districts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Domains] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Domains] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [EducationalPrograms] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_EducationalPrograms] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [EducationalStages] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_EducationalStages] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [EducationTypes] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_EducationTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [EmailServerSettings] (
        [Id] int NOT NULL IDENTITY,
        [SmtpServer] nvarchar(500) NOT NULL,
        [Port] int NOT NULL,
        [Username] nvarchar(500) NOT NULL,
        [Password] nvarchar(500) NOT NULL,
        [FromAddress] nvarchar(500) NOT NULL,
        [FromName] nvarchar(500) NULL,
        [UseSsl] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_EmailServerSettings] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [EmailTemplates] (
        [Id] int NOT NULL IDENTITY,
        [TypeDescription] nvarchar(200) NOT NULL,
        [Subject] nvarchar(500) NOT NULL,
        [Body] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_EmailTemplates] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [EmployeeRoles] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_EmployeeRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [GradeLevels] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_GradeLevels] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Localities] (
        [Id] int NOT NULL IDENTITY,
        [NationalCode] int NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Localities] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [LocalityDistrictNationals] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_LocalityDistrictNationals] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Programs] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Programs] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Projects] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [ReportingMonths] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(500) NOT NULL,
        [Month] int NOT NULL,
        [Year] int NOT NULL,
        [LastReportingDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        [AllowFutureReporting] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_ReportingMonths] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [ReportStatuses] (
        [Id] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NULL,
        CONSTRAINT [PK_ReportStatuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [SchoolClasses] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_SchoolClasses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Sectors] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Sectors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Subjects] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Subjects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [SystemConstants] (
        [Id] int NOT NULL IDENTITY,
        [Key] nvarchar(200) NOT NULL,
        [Value] nvarchar(1000) NOT NULL,
        [Description] nvarchar(500) NULL,
        [UpdatedBy] int NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_SystemConstants] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [UserRoles] (
        [Id] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [UserStatuses] (
        [Id] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_UserStatuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Frameworks] (
        [Id] int NOT NULL IDENTITY,
        [InstitutionSymbol] nvarchar(100) NOT NULL,
        [EducationalStageId] int NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Frameworks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Frameworks_EducationalStages_EducationalStageId] FOREIGN KEY ([EducationalStageId]) REFERENCES [EducationalStages] ([Id]) ON DELETE SET NULL
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Institutions] (
        [Id] int NOT NULL IDENTITY,
        [InstitutionSymbol] int NOT NULL,
        [Name] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        [LocalityId] int NULL,
        [DistrictId] int NULL,
        [SectorId] int NULL,
        [TypeId] int NULL,
        [EducationalStageId] int NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Institutions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Institutions_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE SET NULL,
        CONSTRAINT [FK_Institutions_EducationTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [EducationTypes] ([Id]) ON DELETE SET NULL,
        CONSTRAINT [FK_Institutions_EducationalStages_EducationalStageId] FOREIGN KEY ([EducationalStageId]) REFERENCES [EducationalStages] ([Id]) ON DELETE SET NULL,
        CONSTRAINT [FK_Institutions_Localities_LocalityId] FOREIGN KEY ([LocalityId]) REFERENCES [Localities] ([Id]) ON DELETE SET NULL,
        CONSTRAINT [FK_Institutions_Sectors_SectorId] FOREIGN KEY ([SectorId]) REFERENCES [Sectors] ([Id]) ON DELETE SET NULL
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [EmployeeCode] nvarchar(50) NOT NULL,
        [IdNumber] nvarchar(20) NOT NULL,
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        [PasswordHash] nvarchar(500) NOT NULL,
        [RoleId] int NOT NULL,
        [UserRoleId] int NOT NULL,
        [StatusId] int NOT NULL,
        [IsReportingEmployee] bit NOT NULL,
        [RestDay] int NULL,
        [AllowFutureReporting] bit NOT NULL,
        [Notes] nvarchar(1000) NULL,
        [Email] nvarchar(500) NULL,
        [Phone] nvarchar(50) NULL,
        [MustChangePassword] bit NOT NULL,
        [FailedLoginAttempts] int NOT NULL,
        [LastPasswordChange] datetime2 NULL,
        [AcceptedTermsOfUse] bit NOT NULL,
        [CreatedBy] int NULL,
        [UpdatedBy] int NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Users_EmployeeRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [EmployeeRoles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Users_UserRoles_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRoles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Users_UserStatuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [UserStatuses] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Users_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [Users] ([Id]),
        CONSTRAINT [FK_Users_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Allocations] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ProjectId] int NOT NULL,
        [AnnualEmploymentScope] decimal(18,4) NULL,
        [MonthlyEmploymentScope] decimal(18,4) NULL,
        [DailyEmploymentScope] decimal(18,4) NULL,
        [MonthlyRowAllocation] int NULL,
        [AnnualRowAllocation] int NULL,
        [OutputDuration] nvarchar(500) NULL,
        [AllowExcelUpload] bit NOT NULL,
        [Notes] nvarchar(1000) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Allocations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Allocations_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Allocations_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [InspectorAssignments] (
        [Id] int NOT NULL IDENTITY,
        [InspectorUserId] int NOT NULL,
        [ProgramId] int NULL,
        [DistrictId] int NULL,
        [SectorId] int NULL,
        CONSTRAINT [PK_InspectorAssignments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InspectorAssignments_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]),
        CONSTRAINT [FK_InspectorAssignments_Programs_ProgramId] FOREIGN KEY ([ProgramId]) REFERENCES [Programs] ([Id]),
        CONSTRAINT [FK_InspectorAssignments_Sectors_SectorId] FOREIGN KEY ([SectorId]) REFERENCES [Sectors] ([Id]),
        CONSTRAINT [FK_InspectorAssignments_Users_InspectorUserId] FOREIGN KEY ([InspectorUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [PasswordHistories] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [PasswordHash] nvarchar(500) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_PasswordHistories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PasswordHistories_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [Reports] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ReportingMonthId] int NOT NULL,
        [StatusId] int NOT NULL,
        [SubmittedAt] datetime2 NULL,
        [ApprovedAt] datetime2 NULL,
        [ApprovedBy] int NULL,
        [RejectionReason] nvarchar(1000) NULL,
        [RejectedAt] datetime2 NULL,
        [RejectedBy] int NULL,
        [ImportedFromExcel] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Reports] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reports_ReportStatuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [ReportStatuses] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Reports_ReportingMonths_ReportingMonthId] FOREIGN KEY ([ReportingMonthId]) REFERENCES [ReportingMonths] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Reports_Users_ApprovedBy] FOREIGN KEY ([ApprovedBy]) REFERENCES [Users] ([Id]),
        CONSTRAINT [FK_Reports_Users_RejectedBy] FOREIGN KEY ([RejectedBy]) REFERENCES [Users] ([Id]),
        CONSTRAINT [FK_Reports_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationClasses] (
        [AllocationId] int NOT NULL,
        [ClassId] int NOT NULL,
        CONSTRAINT [PK_AllocationClasses] PRIMARY KEY ([AllocationId], [ClassId]),
        CONSTRAINT [FK_AllocationClasses_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationClasses_SchoolClasses_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [SchoolClasses] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationDiscussionCodes] (
        [AllocationId] int NOT NULL,
        [DiscussionCodeId] int NOT NULL,
        CONSTRAINT [PK_AllocationDiscussionCodes] PRIMARY KEY ([AllocationId], [DiscussionCodeId]),
        CONSTRAINT [FK_AllocationDiscussionCodes_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationDiscussionCodes_DiscussionCodes_DiscussionCodeId] FOREIGN KEY ([DiscussionCodeId]) REFERENCES [DiscussionCodes] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationDistricts] (
        [AllocationId] int NOT NULL,
        [DistrictId] int NOT NULL,
        CONSTRAINT [PK_AllocationDistricts] PRIMARY KEY ([AllocationId], [DistrictId]),
        CONSTRAINT [FK_AllocationDistricts_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationDistricts_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationDomains] (
        [AllocationId] int NOT NULL,
        [DomainId] int NOT NULL,
        CONSTRAINT [PK_AllocationDomains] PRIMARY KEY ([AllocationId], [DomainId]),
        CONSTRAINT [FK_AllocationDomains_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationDomains_Domains_DomainId] FOREIGN KEY ([DomainId]) REFERENCES [Domains] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationEducationalPrograms] (
        [AllocationId] int NOT NULL,
        [EducationalProgramId] int NOT NULL,
        CONSTRAINT [PK_AllocationEducationalPrograms] PRIMARY KEY ([AllocationId], [EducationalProgramId]),
        CONSTRAINT [FK_AllocationEducationalPrograms_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationEducationalPrograms_EducationalPrograms_EducationalProgramId] FOREIGN KEY ([EducationalProgramId]) REFERENCES [EducationalPrograms] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationFrameworks] (
        [AllocationId] int NOT NULL,
        [FrameworkId] int NOT NULL,
        CONSTRAINT [PK_AllocationFrameworks] PRIMARY KEY ([AllocationId], [FrameworkId]),
        CONSTRAINT [FK_AllocationFrameworks_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationFrameworks_Frameworks_FrameworkId] FOREIGN KEY ([FrameworkId]) REFERENCES [Frameworks] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationGradeLevels] (
        [AllocationId] int NOT NULL,
        [GradeLevelId] int NOT NULL,
        CONSTRAINT [PK_AllocationGradeLevels] PRIMARY KEY ([AllocationId], [GradeLevelId]),
        CONSTRAINT [FK_AllocationGradeLevels_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationGradeLevels_GradeLevels_GradeLevelId] FOREIGN KEY ([GradeLevelId]) REFERENCES [GradeLevels] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationLocalities] (
        [AllocationId] int NOT NULL,
        [LocalityId] int NOT NULL,
        CONSTRAINT [PK_AllocationLocalities] PRIMARY KEY ([AllocationId], [LocalityId]),
        CONSTRAINT [FK_AllocationLocalities_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationLocalities_Localities_LocalityId] FOREIGN KEY ([LocalityId]) REFERENCES [Localities] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationLocalityDistrictNationals] (
        [AllocationId] int NOT NULL,
        [LocalityDistrictNationalId] int NOT NULL,
        CONSTRAINT [PK_AllocationLocalityDistrictNationals] PRIMARY KEY ([AllocationId], [LocalityDistrictNationalId]),
        CONSTRAINT [FK_AllocationLocalityDistrictNationals_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationLocalityDistrictNationals_LocalityDistrictNationals_LocalityDistrictNationalId] FOREIGN KEY ([LocalityDistrictNationalId]) REFERENCES [LocalityDistrictNationals] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationPrograms] (
        [AllocationId] int NOT NULL,
        [ProgramId] int NOT NULL,
        CONSTRAINT [PK_AllocationPrograms] PRIMARY KEY ([AllocationId], [ProgramId]),
        CONSTRAINT [FK_AllocationPrograms_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationPrograms_Programs_ProgramId] FOREIGN KEY ([ProgramId]) REFERENCES [Programs] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationSectors] (
        [AllocationId] int NOT NULL,
        [SectorId] int NOT NULL,
        CONSTRAINT [PK_AllocationSectors] PRIMARY KEY ([AllocationId], [SectorId]),
        CONSTRAINT [FK_AllocationSectors_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationSectors_Sectors_SectorId] FOREIGN KEY ([SectorId]) REFERENCES [Sectors] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [AllocationSubjects] (
        [AllocationId] int NOT NULL,
        [SubjectId] int NOT NULL,
        CONSTRAINT [PK_AllocationSubjects] PRIMARY KEY ([AllocationId], [SubjectId]),
        CONSTRAINT [FK_AllocationSubjects_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AllocationSubjects_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [ReportRows] (
        [Id] int NOT NULL IDENTITY,
        [ReportId] int NOT NULL,
        [AllocationId] int NULL,
        [SequenceNumber] int NOT NULL,
        [MeetingDate] datetime2 NOT NULL,
        [MeetingDuration] decimal(18,4) NOT NULL,
        [DistrictId] int NOT NULL,
        [LocalityId] int NOT NULL,
        [FrameworkId] int NOT NULL,
        [EducationalProgramId] int NOT NULL,
        [DomainId] int NOT NULL,
        [Subject1Id] int NOT NULL,
        [Subject2Id] int NULL,
        [DiscussionCodeId] int NULL,
        [ConclusionClassId] int NULL,
        [ConclusionFrameworkId] int NULL,
        [ConclusionLocationId] int NULL,
        [GradeLevelId] int NULL,
        [ClassId] int NULL,
        [Notes] nvarchar(2000) NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_ReportRows] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ReportRows_Allocations_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [Allocations] ([Id]),
        CONSTRAINT [FK_ReportRows_DiscussionCodes_DiscussionCodeId] FOREIGN KEY ([DiscussionCodeId]) REFERENCES [DiscussionCodes] ([Id]),
        CONSTRAINT [FK_ReportRows_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReportRows_Domains_DomainId] FOREIGN KEY ([DomainId]) REFERENCES [Domains] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReportRows_EducationalPrograms_EducationalProgramId] FOREIGN KEY ([EducationalProgramId]) REFERENCES [EducationalPrograms] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReportRows_Frameworks_FrameworkId] FOREIGN KEY ([FrameworkId]) REFERENCES [Frameworks] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReportRows_GradeLevels_GradeLevelId] FOREIGN KEY ([GradeLevelId]) REFERENCES [GradeLevels] ([Id]),
        CONSTRAINT [FK_ReportRows_Localities_LocalityId] FOREIGN KEY ([LocalityId]) REFERENCES [Localities] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReportRows_Reports_ReportId] FOREIGN KEY ([ReportId]) REFERENCES [Reports] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ReportRows_SchoolClasses_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [SchoolClasses] ([Id]),
        CONSTRAINT [FK_ReportRows_SchoolClasses_ConclusionClassId] FOREIGN KEY ([ConclusionClassId]) REFERENCES [SchoolClasses] ([Id]),
        CONSTRAINT [FK_ReportRows_Subjects_Subject1Id] FOREIGN KEY ([Subject1Id]) REFERENCES [Subjects] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReportRows_Subjects_Subject2Id] FOREIGN KEY ([Subject2Id]) REFERENCES [Subjects] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE TABLE [DocumentAttachments] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NULL,
        [ReportRowId] int NULL,
        [FileName] nvarchar(500) NOT NULL,
        [FilePath] nvarchar(1000) NOT NULL,
        [FileSize] bigint NOT NULL,
        [MimeType] nvarchar(200) NOT NULL,
        [UploadedAt] datetime2 NOT NULL,
        [UploadedBy] int NOT NULL,
        CONSTRAINT [PK_DocumentAttachments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DocumentAttachments_ReportRows_ReportRowId] FOREIGN KEY ([ReportRowId]) REFERENCES [ReportRows] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DocumentAttachments_Users_UploadedBy] FOREIGN KEY ([UploadedBy]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DocumentAttachments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] ON;
    EXEC(N'INSERT INTO [EmailTemplates] ([Id], [Body], [CreatedAt], [IsActive], [Subject], [TypeDescription], [UpdatedAt])
    VALUES (1, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''דיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''דיווח פעילות חודשית התקבל'', N''ReportReceived'', NULL),
    (2, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''דיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''דיווח פעילות חודשית אושר'', N''ReportApproved'', NULL),
    (3, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''דיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.'', nchar(10), nchar(10), N''סיבת ההחזרה: {{RejectionReason}}'', nchar(10), nchar(10), N''נא לתקן ולהגיש מחדש.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''דיווח פעילות חודשית הוחזר לתיקון'', N''ReportRejected'', NULL),
    (4, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''נא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.'', nchar(10), nchar(10), N''המועד האחרון להגשה: {{Deadline}}.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''תזכורת: דיווח פעילות חודשית טרם הוגש'', N''ReminderNotSubmitted'', NULL),
    (5, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''דיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.'', nchar(10), nchar(10), N''נא לתקן ולהגיש לפני: {{Deadline}}.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''תזכורת: דיווח פעילות חודשית ממתין לתיקון'', N''ReminderNeedsCorrection'', NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'IsActive', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmployeeRoles]'))
        SET IDENTITY_INSERT [EmployeeRoles] ON;
    EXEC(N'INSERT INTO [EmployeeRoles] ([Id], [CreatedAt], [Description], [IsActive], [UpdatedAt])
    VALUES (1, ''2026-01-01T00:00:00.0000000Z'', N''מורה'', CAST(1 AS bit), NULL),
    (2, ''2026-01-01T00:00:00.0000000Z'', N''מנהל'', CAST(1 AS bit), NULL),
    (3, ''2026-01-01T00:00:00.0000000Z'', N''רכז'', CAST(1 AS bit), NULL),
    (4, ''2026-01-01T00:00:00.0000000Z'', N''יועץ'', CAST(1 AS bit), NULL),
    (5, ''2026-01-01T00:00:00.0000000Z'', N''מפקח'', CAST(1 AS bit), NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'IsActive', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmployeeRoles]'))
        SET IDENTITY_INSERT [EmployeeRoles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[ReportStatuses]'))
        SET IDENTITY_INSERT [ReportStatuses] ON;
    EXEC(N'INSERT INTO [ReportStatuses] ([Id], [Description], [Name])
    VALUES (1, N''טיוטה - הדוח נוצר אך לא הוגש'', N''Draft''),
    (2, N''בהקלדה - הדוח נמצא בתהליך הקלדה'', N''InEntry''),
    (3, N''ממתין לאישור - הדוח הוגש וממתין לאישור'', N''PendingApproval''),
    (4, N''מאושר - הדוח אושר'', N''Approved''),
    (5, N''הוחזר לתיקון - הדוח הוחזר לעובד לתיקון'', N''ReturnedForCorrection''),
    (6, N''נעול - הדוח נעול ואינו ניתן לעריכה'', N''Locked'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[ReportStatuses]'))
        SET IDENTITY_INSERT [ReportStatuses] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] ON;
    EXEC(N'INSERT INTO [SystemConstants] ([Id], [CreatedAt], [Description], [Key], [UpdatedAt], [UpdatedBy], [Value])
    VALUES (1, ''2026-01-01T00:00:00.0000000Z'', N''מרווח בין תזכורות בימים'', N''ReminderIntervalDays'', NULL, NULL, N''3''),
    (2, ''2026-01-01T00:00:00.0000000Z'', N''כמה ימים לפני הדדליין מתחילות התזכורות'', N''ReminderStartDaysBeforeDeadline'', NULL, NULL, N''7''),
    (3, ''2026-01-01T00:00:00.0000000Z'', N''סף אחוז דמיון בהערות (Levenshtein normalized)'', N''NotesSimilarityThresholdPercent'', NULL, NULL, N''90''),
    (4, ''2026-01-01T00:00:00.0000000Z'', N''מקסימום שעות יומי ברירת מחדל לשורת דיווח'', N''MaxDailyHoursDefault'', NULL, NULL, N''9'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[UserRoles]'))
        SET IDENTITY_INSERT [UserRoles] ON;
    EXEC(N'INSERT INTO [UserRoles] ([Id], [Description], [Name])
    VALUES (1, N''מנהל מערכת - גישה מלאה לכל הפונקציות'', N''SystemAdmin''),
    (2, N''מנהל פרויקט - ניהול עובדים, הקצאות ופתיחת חודשים'', N''ProjectManager''),
    (3, N''רכז פרויקט - יצירת עובדים, הקצאות ואישור דיווחים'', N''ProjectCoordinator''),
    (4, N''מפקח צפייה - צפייה בלבד בהיקף מוגדר, ייצוא מאושרים'', N''InspectorView''),
    (5, N''מפקח אישור - צפייה + אישור/דחיית דיווחים'', N''InspectorApproval''),
    (6, N''עובד - צפייה בנתוניו האישיים ומילוי דיווחים'', N''Employee'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[UserRoles]'))
        SET IDENTITY_INSERT [UserRoles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[UserStatuses]'))
        SET IDENTITY_INSERT [UserStatuses] ON;
    EXEC(N'INSERT INTO [UserStatuses] ([Id], [Name])
    VALUES (1, N''Active''),
    (2, N''Inactive''),
    (3, N''Locked'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[UserStatuses]'))
        SET IDENTITY_INSERT [UserStatuses] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AcceptedTermsOfUse', N'AllowFutureReporting', N'CreatedAt', N'CreatedBy', N'Email', N'EmployeeCode', N'FailedLoginAttempts', N'FirstName', N'IdNumber', N'IsReportingEmployee', N'LastName', N'LastPasswordChange', N'MustChangePassword', N'Notes', N'PasswordHash', N'Phone', N'RestDay', N'RoleId', N'StatusId', N'UpdatedAt', N'UpdatedBy', N'UserRoleId') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [AcceptedTermsOfUse], [AllowFutureReporting], [CreatedAt], [CreatedBy], [Email], [EmployeeCode], [FailedLoginAttempts], [FirstName], [IdNumber], [IsReportingEmployee], [LastName], [LastPasswordChange], [MustChangePassword], [Notes], [PasswordHash], [Phone], [RestDay], [RoleId], [StatusId], [UpdatedAt], [UpdatedBy], [UserRoleId])
    VALUES (1, CAST(0 AS bit), CAST(0 AS bit), ''2026-01-01T00:00:00.0000000Z'', NULL, NULL, N''ADMIN001'', 0, N''מנהל'', N''admin'', CAST(0 AS bit), N''מערכת'', NULL, CAST(1 AS bit), NULL, N''$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewdGADNUvDdAfY2.'', NULL, NULL, 1, 1, NULL, NULL, 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AcceptedTermsOfUse', N'AllowFutureReporting', N'CreatedAt', N'CreatedBy', N'Email', N'EmployeeCode', N'FailedLoginAttempts', N'FirstName', N'IdNumber', N'IsReportingEmployee', N'LastName', N'LastPasswordChange', N'MustChangePassword', N'Notes', N'PasswordHash', N'Phone', N'RestDay', N'RoleId', N'StatusId', N'UpdatedAt', N'UpdatedBy', N'UserRoleId') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationClasses_ClassId] ON [AllocationClasses] ([ClassId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationDiscussionCodes_DiscussionCodeId] ON [AllocationDiscussionCodes] ([DiscussionCodeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationDistricts_DistrictId] ON [AllocationDistricts] ([DistrictId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationDomains_DomainId] ON [AllocationDomains] ([DomainId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationEducationalPrograms_EducationalProgramId] ON [AllocationEducationalPrograms] ([EducationalProgramId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationFrameworks_FrameworkId] ON [AllocationFrameworks] ([FrameworkId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationGradeLevels_GradeLevelId] ON [AllocationGradeLevels] ([GradeLevelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationLocalities_LocalityId] ON [AllocationLocalities] ([LocalityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationLocalityDistrictNationals_LocalityDistrictNationalId] ON [AllocationLocalityDistrictNationals] ([LocalityDistrictNationalId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationPrograms_ProgramId] ON [AllocationPrograms] ([ProgramId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Allocations_ProjectId] ON [Allocations] ([ProjectId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Allocations_UserId_ProjectId] ON [Allocations] ([UserId], [ProjectId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationSectors_SectorId] ON [AllocationSectors] ([SectorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AllocationSubjects_SubjectId] ON [AllocationSubjects] ([SubjectId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_DocumentAttachments_ReportRowId] ON [DocumentAttachments] ([ReportRowId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_DocumentAttachments_UploadedBy] ON [DocumentAttachments] ([UploadedBy]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_DocumentAttachments_UserId] ON [DocumentAttachments] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Frameworks_EducationalStageId] ON [Frameworks] ([EducationalStageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Frameworks_InstitutionSymbol_EducationalStageId] ON [Frameworks] ([InstitutionSymbol], [EducationalStageId]) WHERE [EducationalStageId] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_InspectorAssignments_DistrictId] ON [InspectorAssignments] ([DistrictId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_InspectorAssignments_InspectorUserId] ON [InspectorAssignments] ([InspectorUserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_InspectorAssignments_ProgramId] ON [InspectorAssignments] ([ProgramId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_InspectorAssignments_SectorId] ON [InspectorAssignments] ([SectorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Institutions_DistrictId] ON [Institutions] ([DistrictId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Institutions_EducationalStageId] ON [Institutions] ([EducationalStageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Institutions_InstitutionSymbol_EducationalStageId] ON [Institutions] ([InstitutionSymbol], [EducationalStageId]) WHERE [EducationalStageId] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Institutions_LocalityId] ON [Institutions] ([LocalityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Institutions_SectorId] ON [Institutions] ([SectorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Institutions_TypeId] ON [Institutions] ([TypeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_PasswordHistories_UserId] ON [PasswordHistories] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_AllocationId] ON [ReportRows] ([AllocationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_ClassId] ON [ReportRows] ([ClassId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_ConclusionClassId] ON [ReportRows] ([ConclusionClassId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_DiscussionCodeId] ON [ReportRows] ([DiscussionCodeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_DistrictId] ON [ReportRows] ([DistrictId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_DomainId] ON [ReportRows] ([DomainId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_EducationalProgramId] ON [ReportRows] ([EducationalProgramId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_FrameworkId] ON [ReportRows] ([FrameworkId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_GradeLevelId] ON [ReportRows] ([GradeLevelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_LocalityId] ON [ReportRows] ([LocalityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_ReportId] ON [ReportRows] ([ReportId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_Subject1Id] ON [ReportRows] ([Subject1Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ReportRows_Subject2Id] ON [ReportRows] ([Subject2Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reports_ApprovedBy] ON [Reports] ([ApprovedBy]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reports_RejectedBy] ON [Reports] ([RejectedBy]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reports_ReportingMonthId] ON [Reports] ([ReportingMonthId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reports_StatusId] ON [Reports] ([StatusId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Reports_UserId_ReportingMonthId] ON [Reports] ([UserId], [ReportingMonthId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_SystemConstants_Key] ON [SystemConstants] ([Key]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Users_CreatedBy] ON [Users] ([CreatedBy]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Users_IdNumber] ON [Users] ([IdNumber]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Users_StatusId] ON [Users] ([StatusId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Users_UpdatedBy] ON [Users] ([UpdatedBy]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Users_UserRoleId] ON [Users] ([UserRoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412124943_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412124943_InitialCreate', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412134615_AddReminderLogs'
)
BEGIN
    CREATE TABLE [ReminderLogs] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ReportingMonthId] int NOT NULL,
        [TemplateType] nvarchar(100) NOT NULL,
        [SentAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ReminderLogs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ReminderLogs_ReportingMonths_ReportingMonthId] FOREIGN KEY ([ReportingMonthId]) REFERENCES [ReportingMonths] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ReminderLogs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412134615_AddReminderLogs'
)
BEGIN
    CREATE INDEX [IX_ReminderLogs_ReportingMonthId] ON [ReminderLogs] ([ReportingMonthId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412134615_AddReminderLogs'
)
BEGIN
    CREATE INDEX [IX_ReminderLogs_UserId_ReportingMonthId_TemplateType_SentAt] ON [ReminderLogs] ([UserId], [ReportingMonthId], [TemplateType], [SentAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412134615_AddReminderLogs'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412134615_AddReminderLogs', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa'
)
BEGIN
    CREATE TABLE [PasswordResetTokens] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [TokenHash] nvarchar(128) NOT NULL,
        [ExpiresAt] datetime2 NOT NULL,
        [UsedAt] datetime2 NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_PasswordResetTokens] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PasswordResetTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa'
)
BEGIN
    CREATE TABLE [TwoFactorCodes] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [CodeHash] nvarchar(128) NOT NULL,
        [ExpiresAt] datetime2 NOT NULL,
        [UsedAt] datetime2 NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_TwoFactorCodes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TwoFactorCodes_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] ON;
    EXEC(N'INSERT INTO [EmailTemplates] ([Id], [Body], [CreatedAt], [IsActive], [Subject], [TypeDescription], [UpdatedAt])
    VALUES (6, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''לאיפוס הסיסמה לחץ על הקישור הבא:'', nchar(10), N''{{ResetLink}}'', nchar(10), nchar(10), N''הקישור תקף לזמן מוגבל.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''איפוס סיסמה'', N''PasswordReset'', NULL),
    (7, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''קוד האימות שלך הוא: {{Code}}'', nchar(10), nchar(10), N''הקוד תקף ל-{{Minutes}} דקות.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''קוד אימות לכניסה למערכת'', N''TwoFactorCode'', NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] ON;
    EXEC(N'INSERT INTO [SystemConstants] ([Id], [CreatedAt], [Description], [Key], [UpdatedAt], [UpdatedBy], [Value])
    VALUES (5, ''2026-01-01T00:00:00.0000000Z'', N''הפעלת אימות דו-שלבי באמצעות מייל'', N''TfaEmailEnabled'', NULL, NULL, N''false'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa'
)
BEGIN
    CREATE UNIQUE INDEX [IX_PasswordResetTokens_TokenHash] ON [PasswordResetTokens] ([TokenHash]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa'
)
BEGIN
    CREATE INDEX [IX_PasswordResetTokens_UserId_ExpiresAt] ON [PasswordResetTokens] ([UserId], [ExpiresAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa'
)
BEGIN
    CREATE INDEX [IX_TwoFactorCodes_UserId_ExpiresAt] ON [TwoFactorCodes] ([UserId], [ExpiresAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412154401_AddAccountRecoveryAndEmailTfa', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] ON;
    EXEC(N'INSERT INTO [SystemConstants] ([Id], [CreatedAt], [Description], [Key], [UpdatedAt], [UpdatedBy], [Value])
    VALUES (6, ''2026-01-01T00:00:00.0000000Z'', N''Developer-level required report fields. Applies to new validation from the point the value is changed.'', N''RequiredReportFields'', NULL, NULL, N''AllocationId,DistrictId,LocalityId,FrameworkId,EducationalProgramId,DomainId,Subject1Id,MeetingDate,MeetingDuration'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations'
)
BEGIN
    CREATE INDEX [IX_ReportRows_ConclusionFrameworkId] ON [ReportRows] ([ConclusionFrameworkId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations'
)
BEGIN
    CREATE INDEX [IX_ReportRows_ConclusionLocationId] ON [ReportRows] ([ConclusionLocationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations'
)
BEGIN
    ALTER TABLE [ReportRows] ADD CONSTRAINT [FK_ReportRows_Frameworks_ConclusionFrameworkId] FOREIGN KEY ([ConclusionFrameworkId]) REFERENCES [Frameworks] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations'
)
BEGIN
    ALTER TABLE [ReportRows] ADD CONSTRAINT [FK_ReportRows_LocalityDistrictNational_ConclusionLocationId] FOREIGN KEY ([ConclusionLocationId]) REFERENCES [LocalityDistrictNationals] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412164437_AddReportRequiredFieldsAndConclusionRelations', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder'
)
BEGIN
    ALTER TABLE [ReminderLogs] DROP CONSTRAINT [FK_ReminderLogs_ReportingMonths_ReportingMonthId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ReminderLogs]') AND [c].[name] = N'ReportingMonthId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ReminderLogs] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [ReminderLogs] ALTER COLUMN [ReportingMonthId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] ON;
    EXEC(N'INSERT INTO [EmailTemplates] ([Id], [Body], [CreatedAt], [IsActive], [Subject], [TypeDescription], [UpdatedAt])
    VALUES (8, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''סיסמתך תפוג בעוד {{DaysLeft}} ימים (בתאריך {{ExpiryDate}}).'', nchar(10), nchar(10), N''נא להתחבר למערכת ולשנות את הסיסמה לפני מועד הפקיעה.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''התראה: סיסמתך עומדת לפוג'', N''PasswordExpiryWarning'', NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] ON;
    EXEC(N'INSERT INTO [SystemConstants] ([Id], [CreatedAt], [Description], [Key], [UpdatedAt], [UpdatedBy], [Value])
    VALUES (7, ''2026-01-01T00:00:00.0000000Z'', N''כמה שעות בין כל ריצה של שירות התזכורות'', N''ReminderCheckIntervalHours'', NULL, NULL, N''1''),
    (8, ''2026-01-01T00:00:00.0000000Z'', N''כמה ימים לפני פקיעת הסיסמה לשלוח אזהרה למשתמש'', N''PasswordExpiryWarningDays'', NULL, NULL, N''14'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder'
)
BEGIN
    ALTER TABLE [ReminderLogs] ADD CONSTRAINT [FK_ReminderLogs_ReportingMonths_ReportingMonthId] FOREIGN KEY ([ReportingMonthId]) REFERENCES [ReportingMonths] ([Id]) ON DELETE SET NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412170550_AddPasswordExpiryAndConfigurableReminder', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    ALTER TABLE [ReportRows] ADD [ReportTypeId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    CREATE TABLE [ProjectPrograms] (
        [ProjectId] int NOT NULL,
        [ProgramId] int NOT NULL,
        CONSTRAINT [PK_ProjectPrograms] PRIMARY KEY ([ProjectId], [ProgramId]),
        CONSTRAINT [FK_ProjectPrograms_Programs_ProgramId] FOREIGN KEY ([ProgramId]) REFERENCES [Programs] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ProjectPrograms_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    CREATE TABLE [ReportTypes] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_ReportTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] ON;
    EXEC(N'INSERT INTO [EmailTemplates] ([Id], [Body], [CreatedAt], [IsActive], [Subject], [TypeDescription], [UpdatedAt])
    VALUES (9, CONCAT(CAST(N''שלום {{UploaderName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''קובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נקלט בהצלחה.'', nchar(10), nchar(10), N''סה"כ דיווחים שנקלטו: {{RowsImported}}'', nchar(10), N''סה"כ עובדים: {{EmployeesCount}}'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''קובץ דיווח מרוכז נקלט בהצלחה'', N''BatchImportSuccessUploader'', NULL),
    (10, CONCAT(CAST(N''שלום {{UploaderName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''בקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.'', nchar(10), N''שורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.'', nchar(10), nchar(10), N''רשימת השגיאות המפורטת מצורפת כקובץ PDF.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''שגיאות בקובץ דיווח מרוכז'', N''BatchImportErrors'', NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'IsActive', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[ReportTypes]'))
        SET IDENTITY_INSERT [ReportTypes] ON;
    EXEC(N'INSERT INTO [ReportTypes] ([Id], [CreatedAt], [Description], [IsActive], [UpdatedAt])
    VALUES (1, ''2026-01-01T00:00:00.0000000Z'', N''ארצי מחוזי'', CAST(1 AS bit), NULL),
    (2, ''2026-01-01T00:00:00.0000000Z'', N''יישובי מוסדי'', CAST(1 AS bit), NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'IsActive', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[ReportTypes]'))
        SET IDENTITY_INSERT [ReportTypes] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    CREATE INDEX [IX_ReportRows_ReportTypeId] ON [ReportRows] ([ReportTypeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    CREATE INDEX [IX_ProjectPrograms_ProgramId] ON [ProjectPrograms] ([ProgramId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    ALTER TABLE [ReportRows] ADD CONSTRAINT [FK_ReportRows_ReportTypes_ReportTypeId] FOREIGN KEY ([ReportTypeId]) REFERENCES [ReportTypes] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260423093119_AddReportTypeAndProjectPrograms', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    ALTER TABLE [Users] ADD [RowVersion] rowversion NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    ALTER TABLE [Reports] ADD [RowVersion] rowversion NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    ALTER TABLE [ReportRows] ADD [RowVersion] rowversion NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    ALTER TABLE [Allocations] ADD [RowVersion] rowversion NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE TABLE [AuditLogs] (
        [Id] bigint NOT NULL IDENTITY,
        [Timestamp] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [ActorUserId] int NULL,
        [Action] nvarchar(100) NOT NULL,
        [EntityType] nvarchar(100) NOT NULL,
        [EntityId] nvarchar(100) NULL,
        [Before] nvarchar(max) NULL,
        [After] nvarchar(max) NULL,
        [IpAddress] nvarchar(64) NULL,
        [UserAgent] nvarchar(500) NULL,
        [Notes] nvarchar(1000) NULL,
        CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AuditLogs_Users_ActorUserId] FOREIGN KEY ([ActorUserId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE TABLE [NotificationLogs] (
        [Id] int NOT NULL IDENTITY,
        [NotificationType] nvarchar(50) NOT NULL,
        [TemplateType] nvarchar(100) NOT NULL,
        [RecipientUserId] int NULL,
        [RecipientEmail] nvarchar(500) NOT NULL,
        [RelatedReportId] int NULL,
        [RelatedReportingMonthId] int NULL,
        [Subject] nvarchar(500) NOT NULL,
        [Body] nvarchar(max) NOT NULL,
        [Status] nvarchar(20) NOT NULL DEFAULT N'Pending',
        [AttemptCount] int NOT NULL DEFAULT 0,
        [LastAttemptAt] datetime2 NULL,
        [NextRetryAt] datetime2 NULL,
        [FailureReason] nvarchar(2000) NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_NotificationLogs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_NotificationLogs_ReportingMonths_RelatedReportingMonthId] FOREIGN KEY ([RelatedReportingMonthId]) REFERENCES [ReportingMonths] ([Id]),
        CONSTRAINT [FK_NotificationLogs_Reports_RelatedReportId] FOREIGN KEY ([RelatedReportId]) REFERENCES [Reports] ([Id]),
        CONSTRAINT [FK_NotificationLogs_Users_RecipientUserId] FOREIGN KEY ([RecipientUserId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE TABLE [TermsOfUseVersions] (
        [Id] int NOT NULL IDENTITY,
        [VersionNumber] int NOT NULL,
        [BodyHtml] nvarchar(max) NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [PublishedByUserId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_TermsOfUseVersions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TermsOfUseVersions_Users_PublishedByUserId] FOREIGN KEY ([PublishedByUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE TABLE [TermsOfUseAcceptances] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [VersionId] int NOT NULL,
        [AcceptedAt] datetime2 NOT NULL,
        [IpAddress] nvarchar(64) NULL,
        CONSTRAINT [PK_TermsOfUseAcceptances] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TermsOfUseAcceptances_TermsOfUseVersions_VersionId] FOREIGN KEY ([VersionId]) REFERENCES [TermsOfUseVersions] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_TermsOfUseAcceptances_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BodyHtml', N'CreatedAt', N'EffectiveFrom', N'PublishedByUserId', N'UpdatedAt', N'VersionNumber') AND [object_id] = OBJECT_ID(N'[TermsOfUseVersions]'))
        SET IDENTITY_INSERT [TermsOfUseVersions] ON;
    EXEC(N'INSERT INTO [TermsOfUseVersions] ([Id], [BodyHtml], [CreatedAt], [EffectiveFrom], [PublishedByUserId], [UpdatedAt], [VersionNumber])
    VALUES (1, N''תנאי שימוש — יסופקו על ידי הלקוח'', ''2026-04-23T00:00:00.0000000Z'', ''2026-04-23T00:00:00.0000000Z'', 1, NULL, 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BodyHtml', N'CreatedAt', N'EffectiveFrom', N'PublishedByUserId', N'UpdatedAt', N'VersionNumber') AND [object_id] = OBJECT_ID(N'[TermsOfUseVersions]'))
        SET IDENTITY_INSERT [TermsOfUseVersions] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AcceptedAt', N'IpAddress', N'UserId', N'VersionId') AND [object_id] = OBJECT_ID(N'[TermsOfUseAcceptances]'))
        SET IDENTITY_INSERT [TermsOfUseAcceptances] ON;
    EXEC(N'INSERT INTO [TermsOfUseAcceptances] ([Id], [AcceptedAt], [IpAddress], [UserId], [VersionId])
    VALUES (1, ''2026-04-23T00:00:00.0000000Z'', NULL, 1, 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AcceptedAt', N'IpAddress', N'UserId', N'VersionId') AND [object_id] = OBJECT_ID(N'[TermsOfUseAcceptances]'))
        SET IDENTITY_INSERT [TermsOfUseAcceptances] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_AuditLogs_Action_Timestamp] ON [AuditLogs] ([Action], [Timestamp] DESC);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_AuditLogs_ActorUserId_Timestamp] ON [AuditLogs] ([ActorUserId], [Timestamp] DESC);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_AuditLogs_EntityType_EntityId] ON [AuditLogs] ([EntityType], [EntityId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_AuditLogs_Timestamp] ON [AuditLogs] ([Timestamp] DESC);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_NotificationLogs_RecipientUserId_CreatedAt] ON [NotificationLogs] ([RecipientUserId], [CreatedAt] DESC);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_NotificationLogs_RelatedReportId] ON [NotificationLogs] ([RelatedReportId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_NotificationLogs_RelatedReportingMonthId] ON [NotificationLogs] ([RelatedReportingMonthId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_NotificationLogs_Status_NextRetryAt] ON [NotificationLogs] ([Status], [NextRetryAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_NotificationLogs_TemplateType_CreatedAt] ON [NotificationLogs] ([TemplateType], [CreatedAt] DESC);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TermsOfUseAcceptances_UserId_VersionId] ON [TermsOfUseAcceptances] ([UserId], [VersionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_TermsOfUseAcceptances_VersionId] ON [TermsOfUseAcceptances] ([VersionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TermsOfUseVersion_VersionNumber] ON [TermsOfUseVersions] ([VersionNumber]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    CREATE INDEX [IX_TermsOfUseVersions_PublishedByUserId] ON [TermsOfUseVersions] ([PublishedByUserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423102722_UpdateBatchImportErrorsTemplate'
)
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{UploaderName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''בקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.'', nchar(10), N''שורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.'', nchar(10), nchar(10), N''שורות שלא עברו בדיקת תקינות:'', nchar(10), N''{{ErrorList}}'', nchar(10), nchar(10), N''רשימת השגיאות המפורטת מצורפת גם כקובץ PDF.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא'')
    WHERE [Id] = 10;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423102722_UpdateBatchImportErrorsTemplate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260423102722_UpdateBatchImportErrorsTemplate', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423102908_AddSiteLogoPathConstant'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] ON;
    EXEC(N'INSERT INTO [SystemConstants] ([Id], [CreatedAt], [Description], [Key], [UpdatedAt], [UpdatedBy], [Value])
    VALUES (9, ''2026-01-01T00:00:00.0000000Z'', N''נתיב הלוגו של המערכת (תמונה ב-wwwroot). ניתן להחלפה דרך מסך ''''לוגו המערכת''''.'', N''SiteLogoPath'', NULL, NULL, N''/images/logo.png'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260423102908_AddSiteLogoPathConstant'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260423102908_AddSiteLogoPathConstant', N'8.0.26');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'DELETE FROM [TermsOfUseAcceptances]
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    ALTER TABLE [UserStatuses] ADD [DescriptionHebrew] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    ALTER TABLE [UserRoles] ADD [DescriptionHebrew] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [TermsOfUseVersions] SET [BodyHtml] = N''<p>ברוכים הבאים למערכת דיווח הפעילות החודשית של סייט&amp;סאונד חינוך.</p><p>השימוש במערכת מותנה בהסכמה לתנאי השימוש הבאים. אנא קראו אותם בעיון לפני האישור.</p><p>1. השימוש במערכת מיועד לעובדים מורשים בלבד, לצורך דיווח פעילות חודשית בלבד. אין להעביר את פרטי הכניסה לאדם אחר ואין להשתמש במערכת בשם משתמש שאינו שלך.</p><p>2. כל הנתונים המוזנים במערכת מהווים דיווח רשמי. עליך לוודא שכל המידע המוזן נכון, מדויק ומשקף את הפעילות שבוצעה בפועל. דיווח כוזב מהווה הפרה של נהלי הארגון.</p><p>3. הארגון רשאי לבצע ביקורת על הדיווחים בכל עת. דיווחים אשר אושרו ננעלים לעריכה ולא ניתן יהיה לשנותם ללא אישור מנהל.</p><p>4. המערכת שומרת יומן ביקורת של כל הפעולות. הגישה למידע מותנית בהרשאות ובהתאם לתפקיד המוגדר במערכת.</p><p>5. הסיסמה שלך אישית וסודית. יש להחליפה כל 90 יום ולא לחזור על 5 הסיסמאות האחרונות. לאחר 3 ניסיונות כניסה כושלים החשבון יינעל אוטומטית.</p><p>גרסה זו של תנאי השימוש מהווה גרסת ביניים — הגרסה המחייבת תפורסם על ידי מנהל המערכת דרך מסך ''''תנאי שימוש'''' תחת תפריט הניהול.</p>''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''מנהל מערכת''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''מנהל פרויקט''
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''רכז פרויקט''
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''מפקח-צפייה''
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''מפקח-אישור''
    WHERE [Id] = 5;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''עובד''
    WHERE [Id] = 6;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserStatuses] SET [DescriptionHebrew] = N''פעיל''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserStatuses] SET [DescriptionHebrew] = N''לא פעיל''
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [UserStatuses] SET [DescriptionHebrew] = N''נעול''
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    EXEC(N'UPDATE [Users] SET [PasswordHash] = N''$2a$12$4MIlxeD2MhS0aLHvy9Gx5.on9xw87chJAN76m8ifdsBb7FvNuMw36''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses', N'8.0.26');
END;
GO

COMMIT;
GO


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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE TABLE [ReportStatuses] (
        [Id] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NULL,
        CONSTRAINT [PK_ReportStatuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE TABLE [UserRoles] (
        [Id] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE TABLE [UserStatuses] (
        [Id] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_UserStatuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AcceptedTermsOfUse', N'AllowFutureReporting', N'CreatedAt', N'CreatedBy', N'Email', N'EmployeeCode', N'FailedLoginAttempts', N'FirstName', N'IdNumber', N'IsReportingEmployee', N'LastName', N'LastPasswordChange', N'MustChangePassword', N'Notes', N'PasswordHash', N'Phone', N'RestDay', N'RoleId', N'StatusId', N'UpdatedAt', N'UpdatedBy', N'UserRoleId') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [AcceptedTermsOfUse], [AllowFutureReporting], [CreatedAt], [CreatedBy], [Email], [EmployeeCode], [FailedLoginAttempts], [FirstName], [IdNumber], [IsReportingEmployee], [LastName], [LastPasswordChange], [MustChangePassword], [Notes], [PasswordHash], [Phone], [RestDay], [RoleId], [StatusId], [UpdatedAt], [UpdatedBy], [UserRoleId])
    VALUES (1, CAST(0 AS bit), CAST(0 AS bit), ''2026-01-01T00:00:00.0000000Z'', NULL, NULL, N''ADMIN001'', 0, N''מנהל'', N''admin'', CAST(0 AS bit), N''מערכת'', NULL, CAST(1 AS bit), NULL, N''$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewdGADNUvDdAfY2.'', NULL, NULL, 1, 1, NULL, NULL, 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AcceptedTermsOfUse', N'AllowFutureReporting', N'CreatedAt', N'CreatedBy', N'Email', N'EmployeeCode', N'FailedLoginAttempts', N'FirstName', N'IdNumber', N'IsReportingEmployee', N'LastName', N'LastPasswordChange', N'MustChangePassword', N'Notes', N'PasswordHash', N'Phone', N'RestDay', N'RoleId', N'StatusId', N'UpdatedAt', N'UpdatedBy', N'UserRoleId') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationClasses_ClassId] ON [AllocationClasses] ([ClassId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationDiscussionCodes_DiscussionCodeId] ON [AllocationDiscussionCodes] ([DiscussionCodeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationDistricts_DistrictId] ON [AllocationDistricts] ([DistrictId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationDomains_DomainId] ON [AllocationDomains] ([DomainId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationEducationalPrograms_EducationalProgramId] ON [AllocationEducationalPrograms] ([EducationalProgramId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationFrameworks_FrameworkId] ON [AllocationFrameworks] ([FrameworkId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationGradeLevels_GradeLevelId] ON [AllocationGradeLevels] ([GradeLevelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationLocalities_LocalityId] ON [AllocationLocalities] ([LocalityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationLocalityDistrictNationals_LocalityDistrictNationalId] ON [AllocationLocalityDistrictNationals] ([LocalityDistrictNationalId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationPrograms_ProgramId] ON [AllocationPrograms] ([ProgramId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Allocations_ProjectId] ON [Allocations] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Allocations_UserId_ProjectId] ON [Allocations] ([UserId], [ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationSectors_SectorId] ON [AllocationSectors] ([SectorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_AllocationSubjects_SubjectId] ON [AllocationSubjects] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_DocumentAttachments_ReportRowId] ON [DocumentAttachments] ([ReportRowId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_DocumentAttachments_UploadedBy] ON [DocumentAttachments] ([UploadedBy]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_DocumentAttachments_UserId] ON [DocumentAttachments] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Frameworks_EducationalStageId] ON [Frameworks] ([EducationalStageId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Frameworks_InstitutionSymbol_EducationalStageId] ON [Frameworks] ([InstitutionSymbol], [EducationalStageId]) WHERE [EducationalStageId] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_InspectorAssignments_DistrictId] ON [InspectorAssignments] ([DistrictId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_InspectorAssignments_InspectorUserId] ON [InspectorAssignments] ([InspectorUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_InspectorAssignments_ProgramId] ON [InspectorAssignments] ([ProgramId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_InspectorAssignments_SectorId] ON [InspectorAssignments] ([SectorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Institutions_DistrictId] ON [Institutions] ([DistrictId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Institutions_EducationalStageId] ON [Institutions] ([EducationalStageId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Institutions_InstitutionSymbol_EducationalStageId] ON [Institutions] ([InstitutionSymbol], [EducationalStageId]) WHERE [EducationalStageId] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Institutions_LocalityId] ON [Institutions] ([LocalityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Institutions_SectorId] ON [Institutions] ([SectorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Institutions_TypeId] ON [Institutions] ([TypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_PasswordHistories_UserId] ON [PasswordHistories] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_AllocationId] ON [ReportRows] ([AllocationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_ClassId] ON [ReportRows] ([ClassId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_ConclusionClassId] ON [ReportRows] ([ConclusionClassId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_DiscussionCodeId] ON [ReportRows] ([DiscussionCodeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_DistrictId] ON [ReportRows] ([DistrictId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_DomainId] ON [ReportRows] ([DomainId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_EducationalProgramId] ON [ReportRows] ([EducationalProgramId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_FrameworkId] ON [ReportRows] ([FrameworkId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_GradeLevelId] ON [ReportRows] ([GradeLevelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_LocalityId] ON [ReportRows] ([LocalityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_ReportId] ON [ReportRows] ([ReportId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_Subject1Id] ON [ReportRows] ([Subject1Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportRows_Subject2Id] ON [ReportRows] ([Subject2Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Reports_ApprovedBy] ON [Reports] ([ApprovedBy]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Reports_RejectedBy] ON [Reports] ([RejectedBy]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Reports_ReportingMonthId] ON [Reports] ([ReportingMonthId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Reports_StatusId] ON [Reports] ([StatusId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Reports_UserId_ReportingMonthId] ON [Reports] ([UserId], [ReportingMonthId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_SystemConstants_Key] ON [SystemConstants] ([Key]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Users_CreatedBy] ON [Users] ([CreatedBy]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Users_IdNumber] ON [Users] ([IdNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Users_StatusId] ON [Users] ([StatusId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Users_UpdatedBy] ON [Users] ([UpdatedBy]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    CREATE INDEX [IX_Users_UserRoleId] ON [Users] ([UserRoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412124943_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412124943_InitialCreate', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412134615_AddReminderLogs')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412134615_AddReminderLogs')
BEGIN
    CREATE INDEX [IX_ReminderLogs_ReportingMonthId] ON [ReminderLogs] ([ReportingMonthId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412134615_AddReminderLogs')
BEGIN
    CREATE INDEX [IX_ReminderLogs_UserId_ReportingMonthId_TemplateType_SentAt] ON [ReminderLogs] ([UserId], [ReportingMonthId], [TemplateType], [SentAt]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412134615_AddReminderLogs')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412134615_AddReminderLogs', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] ON;
    EXEC(N'INSERT INTO [SystemConstants] ([Id], [CreatedAt], [Description], [Key], [UpdatedAt], [UpdatedBy], [Value])
    VALUES (5, ''2026-01-01T00:00:00.0000000Z'', N''הפעלת אימות דו-שלבי באמצעות מייל'', N''TfaEmailEnabled'', NULL, NULL, N''false'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa')
BEGIN
    CREATE UNIQUE INDEX [IX_PasswordResetTokens_TokenHash] ON [PasswordResetTokens] ([TokenHash]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa')
BEGIN
    CREATE INDEX [IX_PasswordResetTokens_UserId_ExpiresAt] ON [PasswordResetTokens] ([UserId], [ExpiresAt]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa')
BEGIN
    CREATE INDEX [IX_TwoFactorCodes_UserId_ExpiresAt] ON [TwoFactorCodes] ([UserId], [ExpiresAt]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412154401_AddAccountRecoveryAndEmailTfa')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412154401_AddAccountRecoveryAndEmailTfa', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] ON;
    EXEC(N'INSERT INTO [SystemConstants] ([Id], [CreatedAt], [Description], [Key], [UpdatedAt], [UpdatedBy], [Value])
    VALUES (6, ''2026-01-01T00:00:00.0000000Z'', N''Developer-level required report fields. Applies to new validation from the point the value is changed.'', N''RequiredReportFields'', NULL, NULL, N''AllocationId,DistrictId,LocalityId,FrameworkId,EducationalProgramId,DomainId,Subject1Id,MeetingDate,MeetingDuration'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations')
BEGIN
    CREATE INDEX [IX_ReportRows_ConclusionFrameworkId] ON [ReportRows] ([ConclusionFrameworkId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations')
BEGIN
    CREATE INDEX [IX_ReportRows_ConclusionLocationId] ON [ReportRows] ([ConclusionLocationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations')
BEGIN
    ALTER TABLE [ReportRows] ADD CONSTRAINT [FK_ReportRows_Frameworks_ConclusionFrameworkId] FOREIGN KEY ([ConclusionFrameworkId]) REFERENCES [Frameworks] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations')
BEGIN
    ALTER TABLE [ReportRows] ADD CONSTRAINT [FK_ReportRows_LocalityDistrictNational_ConclusionLocationId] FOREIGN KEY ([ConclusionLocationId]) REFERENCES [LocalityDistrictNationals] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412164437_AddReportRequiredFieldsAndConclusionRelations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412164437_AddReportRequiredFieldsAndConclusionRelations', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder')
BEGIN
    ALTER TABLE [ReminderLogs] DROP CONSTRAINT [FK_ReminderLogs_ReportingMonths_ReportingMonthId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] ON;
    EXEC(N'INSERT INTO [EmailTemplates] ([Id], [Body], [CreatedAt], [IsActive], [Subject], [TypeDescription], [UpdatedAt])
    VALUES (8, CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''סיסמתך תפוג בעוד {{DaysLeft}} ימים (בתאריך {{ExpiryDate}}).'', nchar(10), nchar(10), N''נא להתחבר למערכת ולשנות את הסיסמה לפני מועד הפקיעה.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא''), ''2026-01-01T00:00:00.0000000Z'', CAST(1 AS bit), N''התראה: סיסמתך עומדת לפוג'', N''PasswordExpiryWarning'', NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'IsActive', N'Subject', N'TypeDescription', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[EmailTemplates]'))
        SET IDENTITY_INSERT [EmailTemplates] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder')
BEGIN
    ALTER TABLE [ReminderLogs] ADD CONSTRAINT [FK_ReminderLogs_ReportingMonths_ReportingMonthId] FOREIGN KEY ([ReportingMonthId]) REFERENCES [ReportingMonths] ([Id]) ON DELETE SET NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260412170550_AddPasswordExpiryAndConfigurableReminder')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260412170550_AddPasswordExpiryAndConfigurableReminder', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
BEGIN
    ALTER TABLE [ReportRows] ADD [ReportTypeId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
BEGIN
    CREATE INDEX [IX_ReportRows_ReportTypeId] ON [ReportRows] ([ReportTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
BEGIN
    CREATE INDEX [IX_ProjectPrograms_ProgramId] ON [ProjectPrograms] ([ProgramId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
BEGIN
    ALTER TABLE [ReportRows] ADD CONSTRAINT [FK_ReportRows_ReportTypes_ReportTypeId] FOREIGN KEY ([ReportTypeId]) REFERENCES [ReportTypes] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423093119_AddReportTypeAndProjectPrograms')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260423093119_AddReportTypeAndProjectPrograms', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    ALTER TABLE [Users] ADD [RowVersion] rowversion NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    ALTER TABLE [Reports] ADD [RowVersion] rowversion NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    ALTER TABLE [ReportRows] ADD [RowVersion] rowversion NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    ALTER TABLE [Allocations] ADD [RowVersion] rowversion NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BodyHtml', N'CreatedAt', N'EffectiveFrom', N'PublishedByUserId', N'UpdatedAt', N'VersionNumber') AND [object_id] = OBJECT_ID(N'[TermsOfUseVersions]'))
        SET IDENTITY_INSERT [TermsOfUseVersions] ON;
    EXEC(N'INSERT INTO [TermsOfUseVersions] ([Id], [BodyHtml], [CreatedAt], [EffectiveFrom], [PublishedByUserId], [UpdatedAt], [VersionNumber])
    VALUES (1, N''תנאי שימוש — יסופקו על ידי הלקוח'', ''2026-04-23T00:00:00.0000000Z'', ''2026-04-23T00:00:00.0000000Z'', 1, NULL, 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BodyHtml', N'CreatedAt', N'EffectiveFrom', N'PublishedByUserId', N'UpdatedAt', N'VersionNumber') AND [object_id] = OBJECT_ID(N'[TermsOfUseVersions]'))
        SET IDENTITY_INSERT [TermsOfUseVersions] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AcceptedAt', N'IpAddress', N'UserId', N'VersionId') AND [object_id] = OBJECT_ID(N'[TermsOfUseAcceptances]'))
        SET IDENTITY_INSERT [TermsOfUseAcceptances] ON;
    EXEC(N'INSERT INTO [TermsOfUseAcceptances] ([Id], [AcceptedAt], [IpAddress], [UserId], [VersionId])
    VALUES (1, ''2026-04-23T00:00:00.0000000Z'', NULL, 1, 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AcceptedAt', N'IpAddress', N'UserId', N'VersionId') AND [object_id] = OBJECT_ID(N'[TermsOfUseAcceptances]'))
        SET IDENTITY_INSERT [TermsOfUseAcceptances] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_AuditLogs_Action_Timestamp] ON [AuditLogs] ([Action], [Timestamp]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_AuditLogs_ActorUserId_Timestamp] ON [AuditLogs] ([ActorUserId], [Timestamp]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_AuditLogs_EntityType_EntityId] ON [AuditLogs] ([EntityType], [EntityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_AuditLogs_Timestamp] ON [AuditLogs] ([Timestamp]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_NotificationLogs_RecipientUserId_CreatedAt] ON [NotificationLogs] ([RecipientUserId], [CreatedAt]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_NotificationLogs_RelatedReportId] ON [NotificationLogs] ([RelatedReportId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_NotificationLogs_RelatedReportingMonthId] ON [NotificationLogs] ([RelatedReportingMonthId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_NotificationLogs_Status_NextRetryAt] ON [NotificationLogs] ([Status], [NextRetryAt]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_NotificationLogs_TemplateType_CreatedAt] ON [NotificationLogs] ([TemplateType], [CreatedAt]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE UNIQUE INDEX [IX_TermsOfUseAcceptances_UserId_VersionId] ON [TermsOfUseAcceptances] ([UserId], [VersionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_TermsOfUseAcceptances_VersionId] ON [TermsOfUseAcceptances] ([VersionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE UNIQUE INDEX [IX_TermsOfUseVersion_VersionNumber] ON [TermsOfUseVersions] ([VersionNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    CREATE INDEX [IX_TermsOfUseVersions_PublishedByUserId] ON [TermsOfUseVersions] ([PublishedByUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260423101845_AddTermsAuditNotificationLogsAndConcurrency', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423102722_UpdateBatchImportErrorsTemplate')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{UploaderName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''בקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.'', nchar(10), N''שורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.'', nchar(10), nchar(10), N''שורות שלא עברו בדיקת תקינות:'', nchar(10), N''{{ErrorList}}'', nchar(10), nchar(10), N''רשימת השגיאות המפורטת מצורפת גם כקובץ PDF.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת אקסיומא'')
    WHERE [Id] = 10;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423102722_UpdateBatchImportErrorsTemplate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260423102722_UpdateBatchImportErrorsTemplate', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423102908_AddSiteLogoPathConstant')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] ON;
    EXEC(N'INSERT INTO [SystemConstants] ([Id], [CreatedAt], [Description], [Key], [UpdatedAt], [UpdatedBy], [Value])
    VALUES (9, ''2026-01-01T00:00:00.0000000Z'', N''נתיב הלוגו של המערכת (תמונה ב-wwwroot). ניתן להחלפה דרך מסך ''''לוגו המערכת''''.'', N''SiteLogoPath'', NULL, NULL, N''/images/logo.png'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Key', N'UpdatedAt', N'UpdatedBy', N'Value') AND [object_id] = OBJECT_ID(N'[SystemConstants]'))
        SET IDENTITY_INSERT [SystemConstants] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260423102908_AddSiteLogoPathConstant')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260423102908_AddSiteLogoPathConstant', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'DELETE FROM [TermsOfUseAcceptances]
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    ALTER TABLE [UserStatuses] ADD [DescriptionHebrew] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    ALTER TABLE [UserRoles] ADD [DescriptionHebrew] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [TermsOfUseVersions] SET [BodyHtml] = N''<p>ברוכים הבאים למערכת דיווח הפעילות החודשית של סייט&amp;סאונד חינוך.</p><p>השימוש במערכת מותנה בהסכמה לתנאי השימוש הבאים. אנא קראו אותם בעיון לפני האישור.</p><p>1. השימוש במערכת מיועד לעובדים מורשים בלבד, לצורך דיווח פעילות חודשית בלבד. אין להעביר את פרטי הכניסה לאדם אחר ואין להשתמש במערכת בשם משתמש שאינו שלך.</p><p>2. כל הנתונים המוזנים במערכת מהווים דיווח רשמי. עליך לוודא שכל המידע המוזן נכון, מדויק ומשקף את הפעילות שבוצעה בפועל. דיווח כוזב מהווה הפרה של נהלי הארגון.</p><p>3. הארגון רשאי לבצע ביקורת על הדיווחים בכל עת. דיווחים אשר אושרו ננעלים לעריכה ולא ניתן יהיה לשנותם ללא אישור מנהל.</p><p>4. המערכת שומרת יומן ביקורת של כל הפעולות. הגישה למידע מותנית בהרשאות ובהתאם לתפקיד המוגדר במערכת.</p><p>5. הסיסמה שלך אישית וסודית. יש להחליפה כל 90 יום ולא לחזור על 5 הסיסמאות האחרונות. לאחר 3 ניסיונות כניסה כושלים החשבון יינעל אוטומטית.</p><p>גרסה זו של תנאי השימוש מהווה גרסת ביניים — הגרסה המחייבת תפורסם על ידי מנהל המערכת דרך מסך ''''תנאי שימוש'''' תחת תפריט הניהול.</p>''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''מנהל מערכת''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''מנהל פרויקט''
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''רכז פרויקט''
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''מפקח-צפייה''
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''מפקח-אישור''
    WHERE [Id] = 5;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserRoles] SET [DescriptionHebrew] = N''עובד''
    WHERE [Id] = 6;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserStatuses] SET [DescriptionHebrew] = N''פעיל''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserStatuses] SET [DescriptionHebrew] = N''לא פעיל''
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [UserStatuses] SET [DescriptionHebrew] = N''נעול''
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    EXEC(N'UPDATE [Users] SET [PasswordHash] = N''$2a$12$4MIlxeD2MhS0aLHvy9Gx5.on9xw87chJAN76m8ifdsBb7FvNuMw36''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260430110451_AddHebrewDescriptionsToRolesAndStatuses', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510111431_AddDocumentAttachmentDescriptionColumn')
BEGIN
    ALTER TABLE [DocumentAttachments] ADD [Description] nvarchar(1000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510111431_AddDocumentAttachmentDescriptionColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260510111431_AddDocumentAttachmentDescriptionColumn', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510124639_AddReportLevelDocumentAttachments')
BEGIN
    ALTER TABLE [DocumentAttachments] ADD [ReportId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510124639_AddReportLevelDocumentAttachments')
BEGIN
    CREATE INDEX [IX_DocumentAttachments_ReportId] ON [DocumentAttachments] ([ReportId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510124639_AddReportLevelDocumentAttachments')
BEGIN
    ALTER TABLE [DocumentAttachments] ADD CONSTRAINT [FK_DocumentAttachments_Reports_ReportId] FOREIGN KEY ([ReportId]) REFERENCES [Reports] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510124639_AddReportLevelDocumentAttachments')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260510124639_AddReportLevelDocumentAttachments', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510143000_AllowMultipleAllocationsPerEmployeeProject')
BEGIN
    DROP INDEX [IX_Allocations_UserId_ProjectId] ON [Allocations];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510143000_AllowMultipleAllocationsPerEmployeeProject')
BEGIN
    CREATE INDEX [IX_Allocations_UserId_ProjectId] ON [Allocations] ([UserId], [ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260510143000_AllowMultipleAllocationsPerEmployeeProject')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260510143000_AllowMultipleAllocationsPerEmployeeProject', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706072727_SeparateConclusionLookups')
BEGIN
    ALTER TABLE [ReportRows] DROP CONSTRAINT [FK_ReportRows_Frameworks_ConclusionFrameworkId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706072727_SeparateConclusionLookups')
BEGIN
    ALTER TABLE [ReportRows] DROP CONSTRAINT [FK_ReportRows_SchoolClasses_ConclusionClassId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706072727_SeparateConclusionLookups')
BEGIN
    CREATE TABLE [ClassConclusions] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_ClassConclusions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706072727_SeparateConclusionLookups')
BEGIN
    CREATE TABLE [FrameworkConclusions] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] datetime2 NULL,
        [Description] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_FrameworkConclusions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706072727_SeparateConclusionLookups')
BEGIN
    ALTER TABLE [ReportRows] ADD CONSTRAINT [FK_ReportRows_ClassConclusions_ConclusionClassId] FOREIGN KEY ([ConclusionClassId]) REFERENCES [ClassConclusions] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706072727_SeparateConclusionLookups')
BEGIN
    ALTER TABLE [ReportRows] ADD CONSTRAINT [FK_ReportRows_FrameworkConclusions_ConclusionFrameworkId] FOREIGN KEY ([ConclusionFrameworkId]) REFERENCES [FrameworkConclusions] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706072727_SeparateConclusionLookups')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260706072727_SeparateConclusionLookups', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''דיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''דיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''דיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.'', nchar(10), nchar(10), N''סיבת ההחזרה: {{RejectionReason}}'', nchar(10), nchar(10), N''נא לתקן ולהגיש מחדש.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''נא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.'', nchar(10), nchar(10), N''המועד האחרון להגשה: {{Deadline}}.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''דיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.'', nchar(10), nchar(10), N''נא לתקן ולהגיש לפני: {{Deadline}}.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 5;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''לאיפוס הסיסמה לחץ על הקישור הבא:'', nchar(10), N''{{ResetLink}}'', nchar(10), nchar(10), N''הקישור תקף לזמן מוגבל.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 6;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''קוד האימות שלך הוא: {{Code}}'', nchar(10), nchar(10), N''הקוד תקף ל-{{Minutes}} דקות.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 7;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{EmployeeName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''סיסמתך תפוג בעוד {{DaysLeft}} ימים (בתאריך {{ExpiryDate}}).'', nchar(10), nchar(10), N''נא להתחבר למערכת ולשנות את הסיסמה לפני מועד הפקיעה.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 8;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{UploaderName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''קובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נקלט בהצלחה.'', nchar(10), nchar(10), N''סה"כ דיווחים שנקלטו: {{RowsImported}}'', nchar(10), N''סה"כ עובדים: {{EmployeesCount}}'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 9;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    EXEC(N'UPDATE [EmailTemplates] SET [Body] = CONCAT(CAST(N''שלום {{UploaderName}},'' AS nvarchar(max)), nchar(10), nchar(10), N''בקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.'', nchar(10), N''שורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.'', nchar(10), nchar(10), N''שורות שלא עברו בדיקת תקינות:'', nchar(10), N''{{ErrorList}}'', nchar(10), nchar(10), N''רשימת השגיאות המפורטת מצורפת גם כקובץ PDF.'', nchar(10), nchar(10), N''בברכה,'', nchar(10), N''מערכת סייט&סאונד חינוך'')
    WHERE [Id] = 10;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706095017_RebrandSeedEmailTemplates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260706095017_RebrandSeedEmailTemplates', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706122524_MapProgramScopeTablesAndAllocationReportType')
BEGIN

    IF COL_LENGTH('Allocations', 'ReportTypeId') IS NULL
    BEGIN
        ALTER TABLE [Allocations] ADD [ReportTypeId] int NULL;
    END
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706122524_MapProgramScopeTablesAndAllocationReportType')
BEGIN

    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Allocations_ReportTypeId' AND object_id = OBJECT_ID('Allocations'))
    BEGIN
        CREATE INDEX [IX_Allocations_ReportTypeId] ON [Allocations] ([ReportTypeId]);
    END
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706122524_MapProgramScopeTablesAndAllocationReportType')
BEGIN

    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Allocations_ReportTypes_ReportTypeId')
    BEGIN
        ALTER TABLE [Allocations] ADD CONSTRAINT [FK_Allocations_ReportTypes_ReportTypeId]
            FOREIGN KEY ([ReportTypeId]) REFERENCES [ReportTypes] ([Id]) ON DELETE SET NULL;
    END
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706122524_MapProgramScopeTablesAndAllocationReportType')
BEGIN

    IF OBJECT_ID('ProjectProgramSubjects', 'U') IS NULL
    BEGIN
        CREATE TABLE [ProjectProgramSubjects] (
            [ProjectId] int NOT NULL,
            [ProgramId] int NOT NULL,
            [SubjectId] int NOT NULL,
            CONSTRAINT [PK_ProjectProgramSubjects] PRIMARY KEY ([ProjectId], [ProgramId], [SubjectId]),
            CONSTRAINT [FK_ProjectProgramSubjects_Subjects_SubjectId]
                FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE CASCADE
        );
        CREATE INDEX [IX_ProjectProgramSubjects_SubjectId] ON [ProjectProgramSubjects] ([SubjectId]);
    END
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706122524_MapProgramScopeTablesAndAllocationReportType')
BEGIN

    IF OBJECT_ID('ProjectProgramDomains', 'U') IS NULL
    BEGIN
        CREATE TABLE [ProjectProgramDomains] (
            [ProjectId] int NOT NULL,
            [ProgramId] int NOT NULL,
            [DomainId] int NOT NULL,
            CONSTRAINT [PK_ProjectProgramDomains] PRIMARY KEY ([ProjectId], [ProgramId], [DomainId]),
            CONSTRAINT [FK_ProjectProgramDomains_Domains_DomainId]
                FOREIGN KEY ([DomainId]) REFERENCES [Domains] ([Id]) ON DELETE CASCADE
        );
        CREATE INDEX [IX_ProjectProgramDomains_DomainId] ON [ProjectProgramDomains] ([DomainId]);
    END
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706122524_MapProgramScopeTablesAndAllocationReportType')
BEGIN

    IF OBJECT_ID('ProjectProgramEducationalPrograms', 'U') IS NULL
    BEGIN
        CREATE TABLE [ProjectProgramEducationalPrograms] (
            [ProjectId] int NOT NULL,
            [ProgramId] int NOT NULL,
            [EducationalProgramId] int NOT NULL,
            CONSTRAINT [PK_ProjectProgramEducationalPrograms] PRIMARY KEY ([ProjectId], [ProgramId], [EducationalProgramId]),
            CONSTRAINT [FK_ProjectProgramEducationalPrograms_EducationalPrograms_EducationalProgramId]
                FOREIGN KEY ([EducationalProgramId]) REFERENCES [EducationalPrograms] ([Id]) ON DELETE CASCADE
        );
        CREATE INDEX [IX_ProjectProgramEducationalPrograms_EducationalProgramId] ON [ProjectProgramEducationalPrograms] ([EducationalProgramId]);
    END
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706122524_MapProgramScopeTablesAndAllocationReportType')
BEGIN

    IF OBJECT_ID('ProjectProgramDiscussionCodes', 'U') IS NULL
    BEGIN
        CREATE TABLE [ProjectProgramDiscussionCodes] (
            [ProjectId] int NOT NULL,
            [ProgramId] int NOT NULL,
            [DiscussionCodeId] int NOT NULL,
            CONSTRAINT [PK_ProjectProgramDiscussionCodes] PRIMARY KEY ([ProjectId], [ProgramId], [DiscussionCodeId]),
            CONSTRAINT [FK_ProjectProgramDiscussionCodes_DiscussionCodes_DiscussionCodeId]
                FOREIGN KEY ([DiscussionCodeId]) REFERENCES [DiscussionCodes] ([Id]) ON DELETE CASCADE
        );
        CREATE INDEX [IX_ProjectProgramDiscussionCodes_DiscussionCodeId] ON [ProjectProgramDiscussionCodes] ([DiscussionCodeId]);
    END
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260706122524_MapProgramScopeTablesAndAllocationReportType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260706122524_MapProgramScopeTablesAndAllocationReportType', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260707133409_AlignWithClientServerFeatures')
BEGIN

    IF COL_LENGTH('dbo.Reports', 'IsArchived') IS NULL
        ALTER TABLE dbo.Reports ADD IsArchived bit NOT NULL CONSTRAINT DF_Reports_IsArchived DEFAULT(0);

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260707133409_AlignWithClientServerFeatures')
BEGIN

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

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260707133409_AlignWithClientServerFeatures')
BEGIN

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

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260707133409_AlignWithClientServerFeatures')
BEGIN

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

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260707133409_AlignWithClientServerFeatures')
BEGIN

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

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260707133409_AlignWithClientServerFeatures')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260707133409_AlignWithClientServerFeatures', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260707142417_SeedServerEmailTemplates')
BEGIN

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

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260707142417_SeedServerEmailTemplates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260707142417_SeedServerEmailTemplates', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260708090000_MergeDuplicateProgramsAndSeedProjectProgramScopes')
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF OBJECT_ID(N'dbo.ProjectProgramLocalityDistrictNationals', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ProjectProgramLocalityDistrictNationals (
            ProjectId int NOT NULL,
            ProgramId int NOT NULL,
            LocalityDistrictNationalId int NOT NULL,
            CONSTRAINT PK_ProjectProgramLocalityDistrictNationals PRIMARY KEY (ProjectId, ProgramId, LocalityDistrictNationalId),
            CONSTRAINT FK_ProjectProgramLocalityDistrictNationals_LocalityDistrictNationals_LocalityDistrictNationalId
                FOREIGN KEY (LocalityDistrictNationalId) REFERENCES dbo.LocalityDistrictNationals (Id) ON DELETE CASCADE
        );
        CREATE INDEX IX_ProjectProgramLocalityDistrictNationals_LocalityDistrictNationalId
            ON dbo.ProjectProgramLocalityDistrictNationals (LocalityDistrictNationalId);
    END;

    DECLARE @ProgramMerge TABLE (OldProgramId int NOT NULL PRIMARY KEY, NewProgramId int NOT NULL);
    INSERT INTO @ProgramMerge (OldProgramId, NewProgramId) VALUES
    (1, 89),
    (2, 95),
    (3, 93),
    (4, 94),
    (5, 87),
    (6, 96),
    (7, 100),
    (8, 92),
    (9, 97),
    (10, 90),
    (11, 91),
    (85, 104),
    (101, 89),
    (102, 88),
    (103, 87);

    DELETE oldRow
    FROM dbo.AllocationPrograms oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.AllocationPrograms canonical
        WHERE canonical.AllocationId = oldRow.AllocationId
          AND canonical.ProgramId = mergeMap.NewProgramId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.AllocationPrograms oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    INSERT INTO dbo.ProjectPrograms (ProjectId, ProgramId)
    SELECT DISTINCT oldRow.ProjectId, mergeMap.NewProgramId
    FROM dbo.ProjectPrograms oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    JOIN dbo.Programs canonicalProgram ON canonicalProgram.Id = mergeMap.NewProgramId
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.ProjectPrograms canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
    );

    DELETE oldRow
    FROM dbo.ProjectProgramFrameworks oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.ProjectProgramFrameworks canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
          AND canonical.FrameworkId = oldRow.FrameworkId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.ProjectProgramFrameworks oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    DELETE oldRow
    FROM dbo.ProjectProgramGradeLevels oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.ProjectProgramGradeLevels canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
          AND canonical.GradeLevelId = oldRow.GradeLevelId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.ProjectProgramGradeLevels oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    DELETE oldRow
    FROM dbo.ProjectProgramClasses oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.ProjectProgramClasses canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
          AND canonical.ClassId = oldRow.ClassId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.ProjectProgramClasses oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    DELETE oldRow
    FROM dbo.ProjectProgramSubjects oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.ProjectProgramSubjects canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
          AND canonical.SubjectId = oldRow.SubjectId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.ProjectProgramSubjects oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    DELETE oldRow
    FROM dbo.ProjectProgramDomains oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.ProjectProgramDomains canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
          AND canonical.DomainId = oldRow.DomainId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.ProjectProgramDomains oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    DELETE oldRow
    FROM dbo.ProjectProgramEducationalPrograms oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.ProjectProgramEducationalPrograms canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
          AND canonical.EducationalProgramId = oldRow.EducationalProgramId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.ProjectProgramEducationalPrograms oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    DELETE oldRow
    FROM dbo.ProjectProgramDiscussionCodes oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.ProjectProgramDiscussionCodes canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
          AND canonical.DiscussionCodeId = oldRow.DiscussionCodeId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.ProjectProgramDiscussionCodes oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    DELETE oldRow
    FROM dbo.ProjectProgramLocalityDistrictNationals oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId
    WHERE EXISTS (
        SELECT 1 FROM dbo.ProjectProgramLocalityDistrictNationals canonical
        WHERE canonical.ProjectId = oldRow.ProjectId
          AND canonical.ProgramId = mergeMap.NewProgramId
          AND canonical.LocalityDistrictNationalId = oldRow.LocalityDistrictNationalId
    );

    UPDATE oldRow
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.ProjectProgramLocalityDistrictNationals oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    DELETE oldRow
    FROM dbo.ProjectPrograms oldRow
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldRow.ProgramId;

    UPDATE assignment
    SET ProgramId = mergeMap.NewProgramId
    FROM dbo.InspectorAssignments assignment
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = assignment.ProgramId;

    DELETE oldProgram
    FROM dbo.Programs oldProgram
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldProgram.Id
    WHERE NOT EXISTS (SELECT 1 FROM dbo.AllocationPrograms x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectPrograms x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectProgramFrameworks x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectProgramGradeLevels x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectProgramClasses x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectProgramSubjects x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectProgramDomains x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectProgramEducationalPrograms x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectProgramDiscussionCodes x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.ProjectProgramLocalityDistrictNationals x WHERE x.ProgramId = oldProgram.Id)
      AND NOT EXISTS (SELECT 1 FROM dbo.InspectorAssignments x WHERE x.ProgramId = oldProgram.Id);

    UPDATE oldProgram
    SET IsActive = 0
    FROM dbo.Programs oldProgram
    JOIN @ProgramMerge mergeMap ON mergeMap.OldProgramId = oldProgram.Id
    WHERE oldProgram.IsActive = 1;
    DECLARE @ScopeSeed TABLE (ProgramId int NOT NULL, ScopeType nvarchar(64) NOT NULL, Description nvarchar(1000) NOT NULL);
    INSERT INTO @ScopeSeed (ProgramId, ScopeType, Description) VALUES
    (93, N'EducationalProgram', N'׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€˜׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ'),
    (93, N'EducationalProgram', N'׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ג‚×׳³ג€”׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Domain', N'׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (93, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (93, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (93, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (93, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€÷׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (93, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢  ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (93, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ-  ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (93, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (93, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (93, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (93, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (93, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (93, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (93, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ג€׳³ֳ—׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ©׳³ג€˜׳³ֲ¨׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג€¢׳³ג€׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֻ׳³ג„¢׳³ג€˜׳³ֲ¦׳³ג„¢׳³ג€'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ¦׳³ג„¢׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג€¢׳³ג€׳³ֲ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ¨׳³ג‚×׳³ֲ׳³ֲ§׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ֲ¢׳³ֲ§׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ— ׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— - ׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ— ׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ֲ§׳³ֲ©׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (93, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (93, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (93, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (93, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (93, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (93, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (93, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ¨׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֳ—  ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֳ—׳³ֲ¦׳³ג‚×׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ֲ ׳³ג€”׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ֲ ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—  ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (93, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (93, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (93, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ¥ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ֳ— ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ— ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¡׳³ֳ—׳³ג€™׳³ֲ׳³ג€¢׳³ֳ— ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€”׳³ג€׳³ֲ©׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—  ׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ג€“׳³ג€¢׳³ֲ§ ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¥'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ¢ ׳³ֲ©׳³ֲ ׳³ג€'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ©׳³ֲ ׳³ג€'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ¢׳³ג€'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ©׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ  ׳³ֲ ׳³ג„¢׳³ֲ¦׳³ֲ ׳³ג„¢׳³ֲ-׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ֳ—׳³ג„¢׳³ג€”׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ—'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (93, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (93, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (93, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (93, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (93, N'Class', N'1'),
    (93, N'Class', N'10'),
    (93, N'Class', N'11'),
    (93, N'Class', N'12'),
    (93, N'Class', N'13'),
    (93, N'Class', N'14'),
    (93, N'Class', N'15'),
    (93, N'Class', N'2'),
    (93, N'Class', N'3'),
    (93, N'Class', N'4'),
    (93, N'Class', N'5'),
    (93, N'Class', N'6'),
    (93, N'Class', N'7'),
    (93, N'Class', N'8'),
    (93, N'Class', N'9'),
    (93, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (93, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (93, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (93, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (93, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (93, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (93, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (93, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (93, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (93, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (93, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (93, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (93, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (93, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (93, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (93, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (93, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (93, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (93, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (93, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (93, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (93, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (93, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (93, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (93, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (93, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (93, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (93, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (93, N'GradeLevel', N'׳³ֲ'),
    (93, N'GradeLevel', N'׳³ג€˜'),
    (93, N'GradeLevel', N'׳³ג€™'),
    (93, N'GradeLevel', N'׳³ג€'),
    (93, N'GradeLevel', N'׳³ג€'),
    (93, N'GradeLevel', N'׳³ג€¢'),
    (93, N'GradeLevel', N'׳³ג€“'),
    (93, N'GradeLevel', N'׳³ג€”'),
    (93, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (93, N'GradeLevel', N'׳³ֻ'),
    (93, N'GradeLevel', N'׳³ג„¢'),
    (93, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (93, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (95, N'EducationalProgram', N'׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€˜׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ""׳³ֲ׳³ֲ¥'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ֳ—׳³ג€™׳³ֲ¨'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג„¢""׳³ֲ¡ ׳³ג€˜׳³ֲ׳³ֲ¢׳³ג€˜׳³ֲ¨'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€˜""׳³ֲ¨'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€“׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ֳ—׳³ֲ ׳³ג„¢׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֳ—׳³ֲ""׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ֲ¨׳³ג€¢׳³ג€”'),
    (95, N'EducationalProgram', N'׳³ג€÷׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג„¢""׳³ֲ¡ ׳³ג„¢׳³ג„¢׳³ג€”׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ג‚×׳³ג€”׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (95, N'EducationalProgram', N'׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ֲ¢׳³ג€¢׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ-׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'EducationalProgram', N'׳³ג‚×׳³ג€¢׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ¢׳³ֳ—׳³ג„¢׳³ג€'),
    (95, N'EducationalProgram', N'׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (95, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֳ—'),
    (95, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (95, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ֲ¢׳³ֲ¨'),
    (95, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֲ - ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ג€׳³ג„¢'),
    (95, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ- ׳³ֲ ׳³ֲ§׳³ג€¢׳³ג€׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¨'),
    (95, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ'),
    (95, N'EducationalProgram', N'׳³ֳ—׳³ֲ""׳³ֲ-׳³ֲ ׳³ג€”׳³ֲ©׳³ג€¢׳³ֲ'),
    (95, N'Domain', N'׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢'),
    (95, N'Domain', N'׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (95, N'Domain', N'׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ג€”׳³ג€¢׳³ג€׳³ג„¢׳³ג€¢׳³ֳ—'),
    (95, N'Domain', N'׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (95, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (95, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (95, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€÷׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (95, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ-  ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (95, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (95, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (95, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (95, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (95, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (95, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (95, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (95, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (95, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (95, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (95, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (95, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (95, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (95, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (95, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (95, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (95, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—  ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (95, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (95, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (95, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ג€“׳³ג€¢׳³ֲ§ ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¥'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ¢ ׳³ֲ©׳³ֲ ׳³ג€'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ©׳³ֲ ׳³ג€'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ¢׳³ג€'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ©׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€“׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ֳ—׳³ג„¢׳³ג€”׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (95, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (95, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (95, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (95, N'Class', N'1'),
    (95, N'Class', N'10'),
    (95, N'Class', N'11'),
    (95, N'Class', N'12'),
    (95, N'Class', N'13'),
    (95, N'Class', N'14'),
    (95, N'Class', N'15'),
    (95, N'Class', N'2'),
    (95, N'Class', N'3'),
    (95, N'Class', N'4'),
    (95, N'Class', N'5'),
    (95, N'Class', N'6'),
    (95, N'Class', N'7'),
    (95, N'Class', N'8'),
    (95, N'Class', N'9'),
    (95, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (95, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (95, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (95, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (95, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (95, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (95, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (95, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (95, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (95, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (95, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (95, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (95, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (95, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (95, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (95, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (95, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (95, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (95, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (95, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (95, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (95, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (95, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (95, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (95, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (95, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (95, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (95, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (95, N'GradeLevel', N'׳³ֲ'),
    (95, N'GradeLevel', N'׳³ג€˜'),
    (95, N'GradeLevel', N'׳³ג€™'),
    (95, N'GradeLevel', N'׳³ג€'),
    (95, N'GradeLevel', N'׳³ג€'),
    (95, N'GradeLevel', N'׳³ג€¢'),
    (95, N'GradeLevel', N'׳³ג€“'),
    (95, N'GradeLevel', N'׳³ג€”'),
    (95, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (95, N'GradeLevel', N'׳³ֻ'),
    (95, N'GradeLevel', N'׳³ג„¢'),
    (95, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (95, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (100, N'Framework', N'׳³ֲ׳³ֲ׳³ֲ¢׳³ג€ 442087 ׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ג„¢׳³ג€”׳³ג€“׳³ֲ§׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ֲ׳³ֲ׳³ֲ¢׳³ג€ 715797 ׳³ֲ©׳³ֲ¢׳³ֲ¨׳³ג„¢ ׳³ֳ—׳³ג€˜׳³ג€¢׳³ֲ ׳³ג€'),
    (100, N'Framework', N'׳³ֲ׳³ֲ׳³ֲ¢׳³ג€ 761379 ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ֲ׳³ֲ׳³ֲ¢׳³ג€, 540708, ׳³ג€˜׳³ֲ׳³ֲ¨ ׳³ֲ׳³ג€˜׳³ֲ¨׳³ג€׳³ֲ'),
    (100, N'Framework', N'׳³ֲ׳³ֲ׳³ֲ¢׳³ג€, 715797, ׳³ֲ©׳³ֲ¢׳³ֲ¨׳³ג„¢ ׳³ֳ—׳³ג€˜׳³ג€¢׳³ֲ ׳³ג€'),
    (100, N'Framework', N'׳³ֲ׳³ֲ׳³ֲ¢׳³ג€, 722132, ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ֳ—׳³ג‚×׳³ֲ׳³ֲ¨׳³ֳ—׳³ג€'),
    (100, N'Framework', N'׳³ֲ׳³ֲ׳³ֲ¢׳³ג€, ׳³ֲ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֳ—׳³ֲ 361550'),
    (100, N'Framework', N'׳³ֲ׳³ֲ©׳³ג€׳³ג€¢׳³ג€, 641225, ׳³ג€׳³ג€¢׳³ג€˜׳³ֲ¨ ׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€”׳³ֲ׳³ֲ§׳³ג„¢׳³ג€, 672568, ׳³ֲ©׳³ֲ¢׳³ֲ¨׳³ג„¢ ׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ׳³ֲ© - ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ג€“׳³ֲ׳³ג€˜, 338277'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ׳³ֲ© 141481 ׳³ג€˜׳³ֲ׳³ֲ¨ ׳³ג€׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ׳³ֲ© 366864 ׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ׳³ֲ© ׳³ֲ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€ 580528032'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ׳³ֲ©,39491, ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€¢'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ—׳³ֲ¨ ׳³ֲ¢׳³ג„¢׳³ֲ׳³ג„¢׳³ֳ—,632216, ׳³ֲ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢ ׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ׳³ג€'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ—׳³ֲ¨ ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—, 657379 ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€™׳³ג€׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג€“׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ—׳³ֲ¨, 747337, ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ§׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§ - ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€”׳³ג€¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€, 541748'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§ , 42516, ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€¢׳³ג€'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§ ,540526 ׳³ֲ ׳³ג€”׳³ֲ׳³ֳ— ׳³ג€׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§ 540526 ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ׳³ג€׳³ֲ¨׳³ֲ© ׳³ֲ¢׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§ 544379 ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ׳³ֲ-׳³ֳ—׳³ג‚×׳³ֲ׳³ֲ¨׳³ֳ— ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§ ׳³ֲ ׳³ג€׳³ג€˜׳³ג€¢׳³ֲ¨׳³ֲ ׳³ֲ, 541128'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§ ׳³ֲ§.׳³ג€׳³ֲ¨׳³ֲ¦׳³ג€¢׳³ג€™  580338366 ׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¨'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§,  541854, ׳³ג€”׳³ג€“׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€”׳³ג€¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 10541201, ׳³ג€”׳³ג€“׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€”׳³ג€¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 361451, ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 540963, ׳³ֲ׳³ֲ׳³ֲ¨׳³ג„¢ ׳³ֲ׳³ֲ׳³ֳ—'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 541056, ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ¨'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 541102, ׳³ֲ׳³ג€׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¡׳³ֲ£'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 541151 , ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג€“׳³ֲ ׳³ג„¢׳³ֲ¥'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 541185, ׳³ֲ׳³ֲ׳³ֲ¨׳³ג„¢ ׳³ֲ׳³ֲ©׳³ג€'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 541284, ׳³ג€™׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¦׳³ג€˜׳³ג„¢'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 541631, ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€”׳³ֲ¡׳³ג„¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ֲ¨׳³ֲ׳³ג€™'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 541854, ׳³ג€”׳³ג€“׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€”׳³ג€¢׳³ֲ - ׳³ֻ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 541896, ׳³ג‚×׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ג„¢׳³ג€“'''),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 544247, ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג„¢׳³ג€¢׳³ֲ¡׳³ֲ£'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 55120, ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג„¢׳³ֲ©׳³ֲ¨׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 580085447, ׳³ג€˜׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 648410, ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ ׳³ג€”׳³ֲ׳³ג„¢׳³ג€'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§, 657379 ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€™׳³ג€׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג€“׳³ֲ'),
    (100, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§,544239, ׳³ג„¢׳³ֲ©׳³ֲ¨׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ג€™׳³ג€˜׳³ֲ¢׳³ֳ— ׳³ג€“׳³ֲ׳³ג€˜, 675934, ׳³ֲ׳³ֲ¨׳³ג€”׳³ג€¢׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ֲ§׳³ג€˜'),
    (100, N'Framework', N'׳³ג€”׳³ג„¢׳³ג‚×׳³ג€, 346031 , ׳³ג„¢׳³ג€”׳³ֲ ׳³ג„¢׳³ֲ©׳³ֲ¨׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ג€”׳³ֲ׳³ג€, 441774, ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג„¢׳³ֲ¦׳³ג€”׳³ֲ§ ׳³ג€”׳³ֲ׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 140814 ׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 140921 ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ׳³ג€˜׳³ֲ¨׳³ג€׳³ֲ ׳³ֲ¡׳³ֲ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 141572 ׳³ֲ׳³ֲ©׳³ג€÷׳³ֲ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 160366 ׳³ג€˜׳³ֲ׳³ֲ¨ ׳³ג„¢׳³ג€׳³ג€¢׳³ג€׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 346098 ׳³ֲ׳³ֲ© ׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 366880 ׳³ג€˜׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג„¢׳³ֲ©׳³ֲ¨׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 5802944379 ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג€˜׳³ֲ¨׳³ג€׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 633263 ׳³ג‚×׳³ֲ ׳³ג„¢ ׳³ֲ׳³ֲ ׳³ג€”׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ 758193 ׳³ג€׳³ֲ¢׳³ֳ— ׳³ֲ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ג€׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¡׳³ֲ£, 580432375'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 140541, ׳³ֲ¢׳³ֻ׳³ֲ¨׳³ֳ— ׳³ג„¢׳³ֲ©׳³ֲ¨׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 140673, ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ג€”׳³ֲ¡׳³ג„¢׳³ג€׳³ג„¢ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג€“׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 140780, ׳³ג€÷׳³ג€¢׳³ג€÷׳³ג€˜ ׳³ג„¢׳³ֲ¢׳³ֲ§׳³ג€˜'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 140798, ׳³ֲ׳³ג€׳³ֲ ׳³ֲ©׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ¨׳³ֲ׳³ג€¢׳³ג„¢'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 141044, ׳³ֲ§׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 184093, ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ג„¢׳³ג€¢׳³ֲ¡׳³ֲ£'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 27056, ׳³ֲ§׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֲ§׳³ג€˜'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 390590, ׳³ֲ׳³ג€˜ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€¢'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 53196, ׳³ֲ׳³ג€׳³ג€˜׳³ֳ— ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 580026383, ׳³ֲ׳³ג€¢׳³ג€׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 580319489, ׳³ג€׳³ֲ¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 647206, ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€“׳³ֲ¨׳³ג„¢׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 722025, ׳³ֲ¢׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 732081 ׳³ֲ ׳³ג€”׳³ֲ׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 745968, ׳³ג€׳³ג„¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€”׳³ֲ§'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, 747584, ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג„¢׳³ֲ¦׳³ג€”׳³ֲ§ ׳³ֲ§׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¥'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, ׳³ֲ¢׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€, 722025'),
    (100, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ, ׳³ג‚×׳³ג„¢׳³ֲ ׳³ֲ§׳³ֲ - ׳³ֲ׳³ג€¢׳³ֲ¦׳³ֲ¨ ׳³ג€׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€, 711556'),
    (100, N'Framework', N'׳³ג€÷׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ׳³ֲ, 460162, ׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Framework', N'׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¢׳³ג„¢׳³ֲ׳³ג„¢׳³ֳ— 160523 ׳³ֲ׳³ג„¢׳³ֲ¨ ׳³ג€˜׳³ֲ¨׳³ג€÷׳³ג‚×׳³ֲ׳³ג€'),
    (100, N'Framework', N'׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¢׳³ג„¢׳³ֲ׳³ג„¢׳³ֳ— 363879 ׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ג„¢׳³ֲ¦׳³ג€”׳³ֲ§'),
    (100, N'Framework', N'׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¢׳³ג„¢׳³ֲ׳³ג„¢׳³ֳ—, 234047, ׳³ג€¢׳³ג„¢׳³ג€“׳³ֲ ׳³ג„¢׳³ֲ¥'),
    (100, N'Framework', N'׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¢׳³ג„¢׳³ֲ׳³ג„¢׳³ֳ—, 738575, ׳³ֳ—׳³ג‚×׳³ֲ׳³ֲ¨׳³ֳ— ׳³ג„¢׳³ֲ©׳³ֲ¨׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—, 738575, ׳³ֳ—׳³ג‚×׳³ֲ׳³ֲ¨׳³ֳ— ׳³ג„¢׳³ֲ©׳³ֲ¨׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—,676361, ׳³ֲ ׳³ג€”׳³ֲ׳³ֳ— ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ׳³ֲ׳³ֲ ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ— ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ©׳³ֲ¢ 520317'),
    (100, N'Framework', N'׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—, 580726313, ׳³ֲ ׳³ג€¢׳³ֲ£ ׳³ג€׳³ג€™׳³ֲ׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ—, 140681 ׳³ג€˜׳³ֲ׳³ֲ¨ ׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג€'),
    (100, N'Framework', N'׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ—, 770719, ׳³ֲ©׳³ג€÷׳³ֲ¨ ׳³ֲ©׳³ג€÷׳³ג„¢׳³ֲ¨'),
    (100, N'Framework', N'׳³ֲ ׳³ֳ—׳³ֲ ׳³ג„¢׳³ג€, 440768, ׳³ג€׳³ג€˜׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Framework', N'׳³ֲ¢׳³ג€¢׳³ֲ¦׳³ֲ, 541748, ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ ׳³ֲ¨ ׳³ג€“׳³ֲ¨׳³ג€”'),
    (100, N'Framework', N'׳³ג‚×׳³ֳ—׳³ג€” ׳³ֳ—׳³ֲ§׳³ג€¢׳³ג€¢׳³ג€, 440800, ׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג„¢׳³ֲ©׳³ֲ¨׳³ֲ׳³ֲ'),
    (100, N'Framework', N'׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֻ׳³ֲ׳³ג€“׳³ֲ¡׳³ֻ׳³ג€¢׳³ֲ 580342921 ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¨ ׳³ג„¢׳³ֲ¦׳³ג€”׳³ֲ§'),
    (100, N'Framework', N'׳³ֲ¨׳³ֲ׳³ֲ© ׳³ג€׳³ֲ¢׳³ג„¢׳³ֲ, 361550, ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֳ—׳³ֲ'),
    (100, N'Framework', N'׳³ֲ¨׳³ֲ׳³ֲ©׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ, 722058, ׳³ֲ¢׳³ֻ׳³ֲ¨׳³ֳ— ׳³ֲ©׳³ֲ׳³ֲ׳³ג€'),
    (100, N'Framework', N'׳³ֲ¨׳³ג€”׳³ג€¢׳³ג€˜׳³ג€¢׳³ֳ—, 444604, ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג€'),
    (100, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ'),
    (100, N'Domain', N'׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (100, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ֳ— ׳³ג€™׳³ֲ׳³ֲ¨׳³ֲ'),
    (100, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֳ—׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (100, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ©׳³ג€˜׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ג€׳³ֲ ׳³ג€¢׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ֲ§׳³ֲ©׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (100, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ֲ'),
    (100, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג€˜'),
    (100, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (100, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (100, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢'),
    (100, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¢׳³ג€¢""׳³ֲ¡'),
    (100, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ-׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ֳ— ׳³ג€™׳³ֲ׳³ֲ¨׳³ֲ - ׳³ֲ׳³ֳ—׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ֳ— ׳³ג€™׳³ֲ׳³ֲ¨׳³ֲ - ׳³ֲ©׳³ג„¢׳³ֻ׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ג€׳³ֳ—׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ©׳³ג€˜׳³ֲ¨׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג€¢׳³ג€׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֻ׳³ג„¢׳³ג€˜׳³ֲ¦׳³ג„¢׳³ג€'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ¦׳³ג„¢׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג€¢׳³ג€׳³ֲ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ¨׳³ג‚×׳³ֲ׳³ֲ§׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— - ׳³ֲ¢׳³ֲ§׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ— ׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— - ׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ— ׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ֲ§׳³ֲ©׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (100, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֳ—׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ֲ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€׳³ֳ—׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֳ—׳³ֲ§׳³ֲ©׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (100, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (100, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“  ׳³ג€׳³ֳ—׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨ / ׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג„¢׳³ג‚×׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (100, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ֲ׳³ֲ© ׳³ג€׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€'),
    (100, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€- ׳³ֲ׳³ֲ¡׳³ג„¢׳³ֲ¨׳³ֳ— ׳³ג€¢׳³ֲ¢׳³ג€/׳³ג€”׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€'),
    (100, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (100, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (100, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€/ ׳³ֲ׳³ג€™׳³ג„¢׳³ג€ ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨- ׳³ֲ¨׳³ֲ׳³ֲ© ׳³ג€׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ ׳³ֲ©׳³ג„¢ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (100, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€'),
    (100, N'Class', N'1'),
    (100, N'Class', N'10'),
    (100, N'Class', N'11'),
    (100, N'Class', N'12'),
    (100, N'Class', N'13'),
    (100, N'Class', N'14'),
    (100, N'Class', N'15'),
    (100, N'Class', N'2'),
    (100, N'Class', N'3'),
    (100, N'Class', N'4'),
    (100, N'Class', N'5'),
    (100, N'Class', N'6'),
    (100, N'Class', N'7'),
    (100, N'Class', N'8'),
    (100, N'Class', N'9'),
    (100, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (100, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (100, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (100, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (100, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (100, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (100, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (100, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (100, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (100, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (100, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (100, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (100, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (100, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (100, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (100, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (100, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (100, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (100, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (100, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (100, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (100, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (100, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (100, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (100, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (100, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (100, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (100, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (100, N'GradeLevel', N'׳³ֲ'),
    (100, N'GradeLevel', N'׳³ג€˜'),
    (100, N'GradeLevel', N'׳³ג€™'),
    (100, N'GradeLevel', N'׳³ג€'),
    (100, N'GradeLevel', N'׳³ג€'),
    (100, N'GradeLevel', N'׳³ג€¢'),
    (100, N'GradeLevel', N'׳³ג€“'),
    (100, N'GradeLevel', N'׳³ג€”'),
    (100, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (100, N'GradeLevel', N'׳³ֻ'),
    (100, N'GradeLevel', N'׳³ג„¢'),
    (100, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (100, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (100, N'GradeLevel', N'׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ֲ'),
    (100, N'GradeLevel', N'׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג€˜'),
    (96, N'Framework', N'׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג‚×׳³ֲ׳³ג€”׳³ֲ - ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ג€¢׳³ג‚×׳³ֲ§׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ג€÷׳³ֲ¡׳³ֲ׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ֲ©׳³ג€÷׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ֲ׳³ֲ¨ ׳³ג„¢׳³ֲ¢׳³ֲ§׳³ג€˜  ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ֲ׳³ֲ¨ ׳³ֲ©׳³ג€˜׳³ֲ¢- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€ ׳³ֲ ׳³ג€™''׳³ג„¢׳³ג€׳³ֲ׳³ֳ—  ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ג€¢׳³ֲ§׳³ֲ¢׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֲ¨ ׳³ֲ׳³ֲ׳³ֲ׳³ג€÷׳³ֲ¡׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€™׳³ֲ³׳³ֲ׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ׳³ֲ©- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ—׳³ֲ¨ ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ֲ ׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ§ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ֲ¡׳³ֲ׳³ֳ— ׳³ֻ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€™''׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ¡ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€™''׳³ֲ׳³ג€™''׳³ג€¢׳³ֲ׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€™׳³ֲ¡׳³ֲ¨ ׳³ֲ ׳³ג€“׳³ֲ¨׳³ֲ§׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ ׳³ג€÷׳³ֲ¨׳³ֲ׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€¢׳³ֲ¨׳³ֲ¡ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€׳³ֲ©׳³ג€¢׳³ֳ— - ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€, ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג€¢׳³ג‚×׳³ג€¢׳³ג€¢׳³ג„¢׳³ג€“׳³ֲ'),
    (96, N'Framework', N'׳³ג€“׳³ג„¢׳³ֲ׳³ֲ¨  ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€“׳³ֲ¨׳³ג€“׳³ג„¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€”׳³ג€¢׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€”׳³ג€¢׳³ֲ£ ׳³ֲ׳³ֲ©׳³ֲ§׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€”׳³ג€¢׳³ֲ¨׳³ג‚×׳³ג„¢׳³ֲ© ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€”׳³ג„¢׳³ג‚×׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€”׳³ֲ¦׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֻ׳³ג€˜׳³ֲ¨׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֻ׳³ג€¢׳³ג€˜׳³ֲ ׳³ג€“׳³ֲ ׳³ג€™׳³ֲ¨׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€” ׳³ג€™''׳³ֳ—  ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ג‚×׳³ג„¢׳³ֲ¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ג€”׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ג€™׳³ֲ -׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ ׳³ג€™׳³ג„¢׳³ֲ׳³ג€- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ¨ ׳³ג€”׳³ג€¢׳³ֲ׳³ג€- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ ׳³ג€¢׳³ג€¢׳³ג€ ׳³ג„¢׳³ֲ¢׳³ֲ§׳³ג€˜- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ֲ¡׳³ג€™׳³ֳ— ׳³ג€“׳³ֲ׳³ג€˜- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג„¢׳³ג€¢׳³ג€˜׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ ׳³ג€”׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¨׳³ג€¢׳³ֲ׳³ֲ׳³ג€- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¨׳³ֲ׳³ג€¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג„¢׳³ֲ¨׳³ג€÷׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€÷׳³ֲ¡׳³ֲ¨׳³ֲ ׳³ֲ¡׳³ֲ׳³ג„¢׳³ֲ¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€÷׳³ֲ¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ג€÷׳³ֲ׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ג€™''׳³ֲ׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ג€™׳³ג€׳³ֲ ׳³ג€׳³ֲ¢׳³ֲ׳³ֲ§ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ג€™׳³ג€׳³ֲ ׳³ֲ©׳³ֲ׳³ֲ¡ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ג€“׳³ֲ¨׳³ֲ¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ֻ׳³ג€ ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ֲ¢׳³ֲ׳³ג€ ׳³ֲ׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ֲ¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֳ—׳³ֲ¨׳³ֲ©׳³ג„¢׳³ג€”׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ֲ¦׳³ג‚×׳³ג€ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€׳³ג€™׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ׳³ֲ¨׳³ג€”׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ ׳³ג€¢׳³ג€¢׳³ג€ ׳³ֲ׳³ג€׳³ג€˜׳³ֲ¨  ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¡׳³ֲ׳³ג€™׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¡׳³ג€”׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ג€׳³ֲ  ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¢׳³ֲ¨׳³ֲ׳³ג€˜׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¢׳³ֲ¨׳³ג€-׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג‚×׳³ֲ§׳³ג„¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ג‚×׳³ֳ—׳³ג€” ׳³ֳ—׳³ֲ§׳³ג€¢׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¦׳³ג‚×׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ§׳³ֲ׳³ֲ׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€˜׳³ֲ¢- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֳ— ׳³ג€™׳³ֳ— -׳³ג€÷׳³ֲ¨׳³ֲ׳³ג„¢ ׳³ג€™׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ׳³ֲ׳³ג€÷׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¨׳³ֲ׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¨׳³ג€׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¨׳³ג„¢׳³ג€”׳³ֲ ׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ¨׳³ֲ׳³ֲ׳³ג€- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ג€˜׳³ֲ׳³ג„¢ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ׳³ג€™׳³ֲ ׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ג€™׳³ג€˜ ׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ג€׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€™׳³ג€˜ - ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ג€׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ג€¢׳³ֲ¢׳³ג‚×׳³ֲ׳³ֻ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ֲ¢׳³ג€˜ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ג‚×׳³ג„¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Framework', N'׳³ֲ©׳³ג‚×׳³ֲ¨׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'EducationalProgram', N'׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Domain', N'׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ג€”׳³ג€¢׳³ג€׳³ג„¢׳³ג€¢׳³ֳ—'),
    (96, N'Domain', N'׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (96, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€÷׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (96, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג‚×׳³ֲ׳³ֲ¨׳³ֲ-׳³ֲ¨׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢  ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€- ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— 360'),
    (96, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (96, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (96, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (96, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (96, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (96, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (96, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (96, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (96, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (96, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (96, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€-׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (96, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (96, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (96, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (96, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (96, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ֲ ׳³ג€׳³ֲ/׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (96, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (96, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ֳ— 360 ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֲ¨׳³ֲ ׳³ֲ¨׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ');
    INSERT INTO @ScopeSeed (ProgramId, ScopeType, Description) VALUES
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—  ׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (96, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€¢׳³ג€”׳³ֲ'),
    (96, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (96, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (96, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (96, N'Class', N'1'),
    (96, N'Class', N'10'),
    (96, N'Class', N'11'),
    (96, N'Class', N'12'),
    (96, N'Class', N'13'),
    (96, N'Class', N'14'),
    (96, N'Class', N'15'),
    (96, N'Class', N'2'),
    (96, N'Class', N'3'),
    (96, N'Class', N'4'),
    (96, N'Class', N'5'),
    (96, N'Class', N'6'),
    (96, N'Class', N'7'),
    (96, N'Class', N'8'),
    (96, N'Class', N'9'),
    (96, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (96, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (96, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (96, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (96, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (96, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (96, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (96, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (96, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (96, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (96, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (96, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (96, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (96, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (96, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (96, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (96, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (96, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (96, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (96, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (96, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (96, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (96, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (96, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (96, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (96, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (96, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (96, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (96, N'GradeLevel', N'׳³ֲ'),
    (96, N'GradeLevel', N'׳³ג€˜'),
    (96, N'GradeLevel', N'׳³ג€™'),
    (96, N'GradeLevel', N'׳³ג€'),
    (96, N'GradeLevel', N'׳³ג€'),
    (96, N'GradeLevel', N'׳³ג€¢'),
    (96, N'GradeLevel', N'׳³ג€“'),
    (96, N'GradeLevel', N'׳³ג€”'),
    (96, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (96, N'GradeLevel', N'׳³ֻ'),
    (96, N'GradeLevel', N'׳³ג„¢'),
    (96, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (96, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (97, N'Framework', N'׳³ֲ׳³ג€˜׳³ג€¢ ׳³ג€™׳³ג€¢׳³ֲ© ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 148080 ׳³ֲ׳³ג€˜׳³ג€¢ ׳³ג€™׳³ג€¢׳³ֲ© ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ג€˜׳³ג€¢ ׳³ג€™׳³ג€¢׳³ֲ©'),
    (97, N'Framework', N'׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג‚×׳³ֲ׳³ג€”׳³ֲ  ׳³ג€”׳³ֻ""׳³ג€˜ 347047 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ׳³ֲ׳³ֲ¨׳³ֲ׳³ג€“׳³ג„¢ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ-׳³ג‚×׳³ג€”׳³ֲ'),
    (97, N'Framework', N'׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג‚×׳³ֲ׳³ג€”׳³ֲ  ׳³ג€”׳³ֻ""׳³ג€˜ 348235 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ׳³ֲ ׳³ג€™׳³ג€“׳³ֲ׳³ֲ׳³ג„¢ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ-׳³ג‚×׳³ג€”׳³ֲ'),
    (97, N'Framework', N'׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג‚×׳³ֲ׳³ג€”׳³ֲ  ׳³ג€”׳³ֻ""׳³ג€˜ 348243 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ-׳³ג‚×׳³ג€”׳³ֲ'),
    (97, N'Framework', N'׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג‚×׳³ֲ׳³ג€”׳³ֲ  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 342337 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ׳³ֲ¡׳³ג€÷׳³ֲ ׳³ג€׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ-׳³ג‚×׳³ג€”׳³ֲ'),
    (97, N'Framework', N'׳³ֲ׳³ג€÷׳³ֲ¡׳³ֲ׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 248112 ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ֲ׳³ג€÷׳³ֲ¡׳³ֲ׳³ֲ ׳³ֲ׳³ג€÷׳³ֲ¡׳³ֲ׳³ֲ 248112'),
    (97, N'Framework', N'׳³ֲ׳³ֲ¢׳³ג€˜׳³ֲ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 247239 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ֲ¢׳³ג€˜׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ג€˜׳³ֲ׳³ג„¢׳³ֲ'),
    (97, N'Framework', N'׳³ג€™׳³ֲ׳³ג€˜׳³ג€¢׳³ֲ¢  ׳³ג€”׳³ֻ""׳³ג€˜  540617 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג„¢׳³ג„¢׳³ג€˜׳³ֲ׳³ג€ ׳³ג€׳³ג€™׳³ֲ׳³ג€˜׳³ג€¢׳³ֲ¢'),
    (97, N'Framework', N'׳³ג€™''׳³ֲ׳³ג€™''׳³ג€¢׳³ֲ׳³ג„¢׳³ג€  ׳³ג€”׳³ֻ""׳³ג€˜ 448050 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ׳³ג€׳³ֲ¢׳³ג„¢ ׳³ג€™''׳³ֲ׳³ג€™''׳³ג€¢׳³ֲ׳³ג„¢׳³ג€'),
    (97, N'Framework', N'׳³ג€™''׳³ֲ׳³ג€™''׳³ג€¢׳³ֲ׳³ג„¢׳³ג€  ׳³ג€”׳³ֻ""׳³ג€˜ 448316 ׳³ֲ׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€“׳³ג„¢ ׳³ג€™''׳³ֲ׳³ג€™''׳³ג€¢׳³ֲ׳³ג„¢׳³ג€'),
    (97, N'Framework', N'׳³ג€׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג€  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 800128 ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨ ׳³ג€׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֲ¢׳³ֲ׳³ֲ ׳³ג€׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג€ ׳³ג€׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג€'),
    (97, N'Framework', N'׳³ג€”׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 648337 ׳³ֲ׳³ֲ׳³ֲ¡׳³ֲ׳³ֲ׳³ֲ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ג€'),
    (97, N'Framework', N'׳³ג€”׳³ג„¢׳³ג‚×׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 378075 ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ֲ©׳³ג„¢׳³ג€“׳³ֲ׳³ֲ£ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (97, N'Framework', N'׳³ֻ׳³ג€¢׳³ֲ¨׳³ֲ¢׳³ֲ׳³ֲ  ׳³ג€”׳³ֻ""׳³ג€˜ 247155 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ¢""׳³ֲ© ׳³ג€׳³ֲ¨'' ׳³ג€™. ׳³ג€”׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ֻ׳³ג€¢׳³ֲ¨׳³ֲ¢׳³ֲ׳³ֲ'),
    (97, N'Framework', N'׳³ֻ׳³ג€¢׳³ֲ¨׳³ֲ¢׳³ֲ׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 248138 ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ֻ׳³ג€¢׳³ֲ¨׳³ֲ¢׳³ֲ׳³ֲ'),
    (97, N'Framework', N'׳³ֻ׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€”׳³ֻ""׳³ג€˜ 448134 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ¢׳³ֳ—׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ׳³ֲ ׳³ג€™׳³ֲ׳³ג€” ׳³ֲ׳³ֲ׳³ג€׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€'),
    (97, N'Framework', N'׳³ֻ׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€”׳³ֻ""׳³ג€˜ 448209 ׳³ֲ׳³ֲ ׳³ֲ¡׳³ֲ׳³ֲ׳³ֲ׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€'),
    (97, N'Framework', N'׳³ֻ׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 448019 ׳³ֲ׳³ֲ ׳³ֲ׳³ֲ׳³ג€™''׳³ג€ -׳³ֲ¢׳³ֳ—׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€'),
    (97, N'Framework', N'׳³ֻ׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 478016 ׳³ֲ׳³ֲ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€ ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֲ¢׳³ֲ׳³ֲ'),
    (97, N'Framework', N'׳³ֻ׳³ג„¢׳³ֲ¨׳³ג€  ׳³ג€”׳³ֻ""׳³ג€˜ 442566 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ג€™'' -׳³ֲ¢׳³ג€˜׳³ג€ ׳³ֲ׳³ֲ׳³ֲ¨׳³ֲ׳³ג€¢׳³ֲ£ ׳³ֲ¡׳³ֲ׳³ֲ׳³ֲ¨׳³ג€'),
    (97, N'Framework', N'׳³ֻ׳³ג„¢׳³ֲ¨׳³ג€  ׳³ג€”׳³ֻ""׳³ג€˜ 448118 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ'' ׳³ֻ׳³ג„¢׳³ֲ¨׳³ג€'),
    (97, N'Framework', N'׳³ֻ׳³ג„¢׳³ֲ¨׳³ג€  ׳³ג€”׳³ֻ""׳³ג€˜ 448183 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ג€˜'' ׳³ֻ׳³ג„¢׳³ֲ¨׳³ג€'),
    (97, N'Framework', N'׳³ֻ׳³ֲ׳³ֲ¨׳³ג€  ׳³ג€”׳³ֻ""׳³ג€˜ 249169 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ׳³ֲ׳³ג‚×׳³ֲ¨׳³ֲ׳³ג€˜׳³ג„¢ ׳³ֻ׳³ֲ׳³ֲ¨׳³ג€ 249169'),
    (97, N'Framework', N'׳³ג„¢׳³ג‚×׳³ג€¢  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 548016 ׳³ֲ¢׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג„¢""׳³ג€˜ ׳³ג„¢׳³ג‚×׳³ג€¢'),
    (97, N'Framework', N'׳³ג„¢׳³ג‚×׳³ג€¢  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 573105 ׳³ֲ׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ֳ—׳³ֲ§׳³ג€˜׳³ֲ ׳³ג„¢׳³ג‚×׳³ג€¢'),
    (97, N'Framework', N'׳³ג€÷׳³ֲ¡׳³ג„¢׳³ג„¢׳³ג‚×׳³ג€  ׳³ג€”׳³ֻ""׳³ג€˜ 610006 ׳³ֲ׳³ג€¢׳³ֲ¨׳³ֻ ׳³ֲ׳³ֲ׳³ֲ׳³ֲ ׳³ֳ—׳³ג€˜׳³ג„¢  ׳³ג€÷׳³ֲ¡׳³ג„¢׳³ג„¢׳³ג‚×׳³ג€'),
    (97, N'Framework', N'׳³ג€÷׳³ֲ¡׳³ג„¢׳³ג„¢׳³ג‚×׳³ג€  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 800037 ׳³ֲ׳³ג€¢׳³ֲ¨׳³ֻ ׳³ֲ׳³ג€˜׳³ג€¢ ׳³ֲ¨׳³ג€˜׳³ג„¢׳³ֲ¢׳³ג€'),
    (97, N'Framework', N'׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ג€˜׳³ֲ¨׳³ֲ ׳³ג€”׳³ֻ""׳³ג€˜ 448340 ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€”׳³ֻ׳³ג€˜ ׳³ֲ׳³ֲ׳³ֲ ׳³ג€׳³ג€׳³ג€'),
    (97, N'Framework', N'׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ג„¢׳³ֲ׳³ֲ¡׳³ג„¢׳³ֲ£  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 248013 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ¢""׳³ֲ© ׳³ג„¢׳³ֲ ׳³ג„¢ ׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ג„¢׳³ֲ׳³ֲ¡׳³ג„¢׳³ֲ£'),
    (97, N'Framework', N'׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ג€÷׳³ֲ ׳³ֲ  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 800094 ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ג€÷׳³ֲ ׳³ֲ'),
    (97, N'Framework', N'׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€”׳³ֻ""׳³ג€˜  248765 ׳³ג€”׳³ֻ׳³ג€˜ ׳³ג€˜'' ׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ'),
    (97, N'Framework', N'׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ֲ§׳³ֲ׳³ֲ¡׳³ֲ ׳³ג€”׳³ֻ""׳³ג€˜ 448167  ׳³ג€”׳³ֻ""׳³ג€˜  ׳³ֲ׳³ג€˜׳³ֲ ׳³ֲ¡׳³ג„¢׳³ֲ ׳³ֲ ׳³ג€÷׳³ג‚×׳³ֲ¨ ׳³ֲ§׳³ֲ׳³ֲ¡׳³ֲ'),
    (97, N'Framework', N'׳³ֲ׳³ֲ§׳³ג„¢׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 648261 ׳³ֲ׳³ֲ§׳³ֲ¨׳³ֲ ׳³ֲ׳³ֲ§׳³ג„¢׳³ג€ 648261'),
    (97, N'Framework', N'׳³ֲ׳³ֻ׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ¨ ׳³ג€”׳³ֻ""׳³ג€˜ 247221 ׳³ג€˜׳³ג„¢""׳³ֲ¡ ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ג€׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ'),
    (97, N'Framework', N'׳³ֲ ׳³ג€¢׳³ג€¢׳³ג€ ׳³ֲ׳³ג€׳³ג€˜׳³ֲ¨ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 660233 ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ֲ׳³ג€˜׳³ג€¢ ׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ג€¢׳³ג€ ׳³ֲ׳³ג€׳³ג€˜׳³ֲ¨'),
    (97, N'Framework', N'׳³ֲ ׳³ג€”׳³ֲ£  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 248641 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ג„¢׳³ג€˜׳³ֲ ׳³ֲ¡׳³ג„¢׳³ֲ ׳³ֲ ׳³ֲ ׳³ג€”׳³ֲ£'),
    (97, N'Framework', N'׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—  ׳³ג€”׳³ֻ""׳³ג€˜  338657 ׳³ֲ׳³ֲ׳³ג€”׳³ג€÷׳³ֲ׳³ג€ ׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—'),
    (97, N'Framework', N'׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—  ׳³ג€”׳³ֻ""׳³ג€˜ 248146 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ¢""׳³ֲ© ׳³ֳ—׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§ ׳³ג€“׳³ג„¢׳³ֲ׳³ג€ ׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—'),
    (97, N'Framework', N'׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 247064 ׳³ֻ׳³ֲ¨׳³ג€ ׳³ֲ¡׳³ֲ ׳³ֻ׳³ג€ ׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—'),
    (97, N'Framework', N'׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 338657 ׳³ֲ׳³ֲ׳³ג€”׳³ג€÷׳³ֲ׳³ג€ ׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—'),
    (97, N'Framework', N'׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ— ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 472332 ׳³ג€˜׳³ג„¢""׳³ֲ¡ ׳³ֲ ׳³ג€“׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ֲ¡׳³ֲ׳³ג„¢׳³ג€“׳³ג„¢׳³ֲ׳³ֲ ׳³ֲ ׳³ֲ¦׳³ֲ¨׳³ֳ—'),
    (97, N'Framework', N'׳³ֲ¡׳³ג€”''׳³ֲ ׳³ג„¢׳³ֲ  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 800052 ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ© ׳³ג€™''׳³ֲ׳³ֲ׳³ֲ ׳³ֻ׳³ֲ¨׳³ג€˜׳³ג„¢׳³ג€ ׳³ֲ¡׳³ג€”''׳³ֲ ׳³ג„¢׳³ֲ'),
    (97, N'Framework', N'׳³ֲ¡׳³ֲ¢׳³ג€¢׳³ג€¢׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 648345 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֻ׳³ג€¢׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג€'),
    (97, N'Framework', N'׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ג€׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 800078  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ג€׳³ֲ'),
    (97, N'Framework', N'׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג‚×׳³ג€  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 442822 ׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג‚×׳³ג€ ׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ ׳³ֲ§׳³ג€¢׳³ג€˜׳³ג€  ׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג‚×׳³ג€'),
    (97, N'Framework', N'׳³ֲ¢׳³ֲ¨׳³ֲ׳³ג€˜׳³ג€  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 247247 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ֲ׳³ג€˜׳³ג€¢׳³ג€÷׳³ֲ׳³ֲ¨׳³ג„¢ ׳³ֲ¢׳³ֲ¨׳³ֲ׳³ג€˜׳³ג€'),
    (97, N'Framework', N'׳³ֲ¢׳³ֲ¨׳³ֲ׳³ג€˜׳³ג€  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 248575 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ג€˜׳³ֲ ׳³ג€”''׳³ֲ׳³ג€׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ¨׳³ֲ׳³ג€˜׳³ג€'),
    (97, N'Framework', N'׳³ֲ¢׳³ֲ¨׳³ֲ׳³ג€˜׳³ג€  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 249284 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ֲ׳³ג€˜׳³ֻ׳³ג€¢׳³ֲ£ - ׳³ֲ¢׳³ֲ¨׳³ֲ׳³ג€˜׳³ג€'),
    (97, N'Framework', N'׳³ֲ¢׳³ֲ¨׳³ֲ¢׳³ֲ¨׳³ג€ ׳³ג€”׳³ֻ""׳³ג€˜ 348060  ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ¢׳³ֲ¨׳³ֲ¢׳³ֲ¨׳³ג€'),
    (97, N'Framework', N'׳³ֲ¢׳³ֲ¨׳³ֲ¢׳³ֲ¨׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 800102 ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ¨׳³ֲ¢׳³ֲ¨׳³ג€  ׳³ֲ¢׳³ֲ¨׳³ֲ¢׳³ֲ¨׳³ג€'),
    (97, N'Framework', N'׳³ג‚×׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ג€׳³ֲ¡ ׳³ג€”׳³ֻ""׳³ג€˜ 348227 ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ג‚×׳³ֲ¨׳³ג€׳³ג„¢׳³ֲ¡ ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ג€׳³ג„¢׳³ֲ¡'),
    (97, N'Framework', N'׳³ֲ¨׳³ֲ׳³ֲ׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 248047 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ¨׳³ֲ׳³ֲ׳³ג€'),
    (97, N'Framework', N'׳³ֲ¨׳³ג€׳³ֻ ׳³ג€”׳³ֻ""׳³ג€˜ 640797 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ג€˜׳³ג€¢ ׳³ֲ¨׳³ֲ׳³ֲ©׳³ג€ ׳³ֲ¨׳³ג€׳³ֻ'),
    (97, N'Framework', N'׳³ֲ©׳³ג€™׳³ג€˜ ׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ  ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 648303 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¡׳³ֲ׳³ֲ׳³ֲ ׳³ֲ©׳³ג€™׳³ג€˜ ׳³ֲ©׳³ֲ׳³ג€¢׳³ֲ'),
    (97, N'Framework', N'׳³ֲ©׳³ג‚×׳³ֲ¨׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 248070 ׳³ֲ¢׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ©׳³ג‚×׳³ֲ¨׳³ֲ¢׳³ֲ'),
    (97, N'Framework', N'׳³ֲ©׳³ג‚×׳³ֲ¨׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ 248344 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ג€”׳³ֻ""׳³ג€˜ ׳³ג€™'''),
    (97, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Domain', N'׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (97, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (97, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (97, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (97, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (97, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (97, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (97, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (97, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (97, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (97, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (97, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (97, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (97, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (97, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (97, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (97, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (97, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (97, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (97, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (97, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—  ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (97, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (97, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (97, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ג€“׳³ג€¢׳³ֲ§ ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¥'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ¢ ׳³ֲ©׳³ֲ ׳³ג€'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ©׳³ֲ ׳³ג€'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ¢׳³ג€'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ©׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ֳ—׳³ג„¢׳³ג€”׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ—'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (97, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (97, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (97, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (97, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (97, N'Class', N'1'),
    (97, N'Class', N'10'),
    (97, N'Class', N'11'),
    (97, N'Class', N'12'),
    (97, N'Class', N'13'),
    (97, N'Class', N'14'),
    (97, N'Class', N'15'),
    (97, N'Class', N'2'),
    (97, N'Class', N'3'),
    (97, N'Class', N'4'),
    (97, N'Class', N'5'),
    (97, N'Class', N'6'),
    (97, N'Class', N'7'),
    (97, N'Class', N'8'),
    (97, N'Class', N'9'),
    (97, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (97, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (97, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (97, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (97, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (97, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (97, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (97, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (97, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (97, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (97, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (97, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (97, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (97, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (97, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (97, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (97, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (97, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (97, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (97, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (97, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (97, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (97, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (97, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (97, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (97, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (97, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (97, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (97, N'GradeLevel', N'׳³ֲ'),
    (97, N'GradeLevel', N'׳³ג€˜'),
    (97, N'GradeLevel', N'׳³ג€™'),
    (97, N'GradeLevel', N'׳³ג€'),
    (97, N'GradeLevel', N'׳³ג€'),
    (97, N'GradeLevel', N'׳³ג€¢'),
    (97, N'GradeLevel', N'׳³ג€“'),
    (97, N'GradeLevel', N'׳³ג€”'),
    (97, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (97, N'GradeLevel', N'׳³ֻ'),
    (97, N'GradeLevel', N'׳³ג„¢'),
    (97, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (97, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (89, N'EducationalProgram', N'׳³ֲ¢׳³ג€¢׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ-׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ'),
    (89, N'Domain', N'׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (89, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (89, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (89, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€÷׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (89, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ-  ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (89, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (89, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (89, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (89, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (89, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (89, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (89, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (89, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (89, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (89, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (89, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (89, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (89, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (89, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (89, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (89, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (89, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—  ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (89, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (89, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (89, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ג€“׳³ג€¢׳³ֲ§ ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¥'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ¢ ׳³ֲ©׳³ֲ ׳³ג€'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ©׳³ֲ ׳³ג€'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ¢׳³ג€'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ©׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€“׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ֳ—׳³ג„¢׳³ג€”׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (89, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (89, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (89, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (89, N'Class', N'1'),
    (89, N'Class', N'10'),
    (89, N'Class', N'11'),
    (89, N'Class', N'12'),
    (89, N'Class', N'13'),
    (89, N'Class', N'14'),
    (89, N'Class', N'15'),
    (89, N'Class', N'2'),
    (89, N'Class', N'3'),
    (89, N'Class', N'4'),
    (89, N'Class', N'5'),
    (89, N'Class', N'6'),
    (89, N'Class', N'7'),
    (89, N'Class', N'8'),
    (89, N'Class', N'9'),
    (89, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (89, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (89, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (89, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (89, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (89, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (89, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (89, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (89, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (89, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (89, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (89, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (89, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (89, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (89, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (89, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (89, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (89, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (89, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (89, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (89, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (89, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (89, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (89, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (89, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (89, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (89, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (89, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (89, N'GradeLevel', N'׳³ֲ'),
    (89, N'GradeLevel', N'׳³ג€˜'),
    (89, N'GradeLevel', N'׳³ג€™'),
    (89, N'GradeLevel', N'׳³ג€'),
    (89, N'GradeLevel', N'׳³ג€'),
    (89, N'GradeLevel', N'׳³ג€¢'),
    (89, N'GradeLevel', N'׳³ג€“'),
    (89, N'GradeLevel', N'׳³ג€”'),
    (89, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (89, N'GradeLevel', N'׳³ֻ'),
    (89, N'GradeLevel', N'׳³ג„¢'),
    (89, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (89, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (92, N'Framework', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨ ׳³ֲ¡׳³ג€׳³ג„¢׳³ֲ¨ ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ג„¢׳³ֲ'),
    (92, N'Framework', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨ ׳³ֲ¡׳³ג€׳³ג„¢׳³ֲ¨ ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ג„¢׳³ֲ'),
    (92, N'EducationalProgram', N'׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ג€˜׳³ג‚×׳³ֲ¨׳³ֻ - ׳³ֲ§׳³ג€˜""׳³ֲ¡׳³ג„¢׳³ֲ'),
    (92, N'Domain', N'׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨ ׳³ֲ¡׳³ג€׳³ג„¢׳³ֲ¨'),
    (92, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (92, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ג€“׳³ֲ ׳³ג€'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (92, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ©׳³ג‚×׳³ֻ׳³ג„¢'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (92, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (92, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (92, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (92, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (92, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (92, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (92, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (92, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (92, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€-׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (92, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (92, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (92, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (92, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (92, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (92, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (92, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (92, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ§׳³ג€˜""׳³ֲ¡'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨ ׳³ֲ¡׳³ג€׳³ג„¢׳³ֲ¨'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—  ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (92, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (92, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ -׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (92, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (92, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ג€“׳³ג€¢׳³ֲ§ ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¥'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ¢ ׳³ֲ©׳³ֲ ׳³ג€'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ©׳³ֲ ׳³ג€'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ג€׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג„¢׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€ ׳³ֲ׳³ג€׳³ֲ׳³ֲ¨׳³ֲ¥ - ׳³ג€™׳³ג€”׳³ֲ׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ¦׳³ֻ׳³ג„¢׳³ג„¢׳³ג€׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ¢׳³ג€'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג‚×׳³ֻ׳³ג€¢׳³ֲ¨'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ©׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€”׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ֲ׳³ֲ ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ֲ ׳³ֲ¢'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ֳ—׳³ג„¢׳³ג€”׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ—'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (92, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (92, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (92, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (92, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (92, N'Class', N'1'),
    (92, N'Class', N'10'),
    (92, N'Class', N'11'),
    (92, N'Class', N'12'),
    (92, N'Class', N'13'),
    (92, N'Class', N'14'),
    (92, N'Class', N'15'),
    (92, N'Class', N'2'),
    (92, N'Class', N'3'),
    (92, N'Class', N'4'),
    (92, N'Class', N'5'),
    (92, N'Class', N'6'),
    (92, N'Class', N'7'),
    (92, N'Class', N'8'),
    (92, N'Class', N'9'),
    (92, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (92, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (92, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (92, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (92, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (92, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (92, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (92, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (92, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (92, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (92, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (92, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (92, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (92, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (92, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (92, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (92, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (92, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (92, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (92, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (92, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (92, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (92, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (92, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (92, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (92, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (92, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (92, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (92, N'GradeLevel', N'׳³ֲ'),
    (92, N'GradeLevel', N'׳³ג€˜'),
    (92, N'GradeLevel', N'׳³ג€™'),
    (92, N'GradeLevel', N'׳³ג€'),
    (92, N'GradeLevel', N'׳³ג€'),
    (92, N'GradeLevel', N'׳³ג€¢'),
    (92, N'GradeLevel', N'׳³ג€“'),
    (92, N'GradeLevel', N'׳³ג€”'),
    (92, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (92, N'GradeLevel', N'׳³ֻ'),
    (92, N'GradeLevel', N'׳³ג„¢'),
    (92, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (92, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (91, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Domain', N'׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (91, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (91, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ג€™׳³ג€“׳³ֲ¨ ׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג„¢'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ֻ׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ ׳³ג„¢׳³ֲ¦׳³ֲ ׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ§׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ג„¢׳³ג€ (׳³ֲ§׳³ֲ""׳³ֲ¢)'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ג€“׳³ֲ ׳³ג€'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ג€”׳³ֲ ""׳³ֲ'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ֲ ׳³ג€׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ג€“׳³ֲ ׳³ג€'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (91, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֳ—׳³ג„¢ ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ֲ׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (91, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (91, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (91, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (91, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (91, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (91, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (91, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—');
    INSERT INTO @ScopeSeed (ProgramId, ScopeType, Description) VALUES
    (91, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (91, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (91, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€-׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (91, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (91, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (91, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (91, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (91, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (91, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (91, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (91, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ -׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ֲ©׳³ֻ׳³ג€”'),
    (91, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֳ—׳³ֲ¦׳³ג‚×׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (91, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (91, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (91, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—, ׳³ג€׳³ֲ ׳³ג€™׳³ג€“׳³ֲ¨׳³ֳ— ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ.'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— (׳³ֲ¨׳³ֲ׳³ֲ© ׳³ג€׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€) ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“  ׳³ג€׳³ֳ—׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֻ׳³ג€¢׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֻ׳³ג€¢׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ§׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ¨, ׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ג€, ׳³ג€¢׳³ֳ—׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ—/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—/׳³ֲ׳³ֲ©׳³ג‚×׳³ג€”׳³ֳ—׳³ג„¢׳³ֳ—/׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ֳ—׳³ג„¢׳³ֳ—.'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ג„¢׳³ג€¢׳³ג€¢׳³ג€” (׳³ֲ׳³ג€”׳³ֳ— ׳³ֲ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©), ׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ג€¢׳³ֲ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€÷׳³ֲ¨׳³ג€“.'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ-  ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ג€÷׳³ֳ—׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€”׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€  ׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€”׳³ֲ¨׳³ג„¢׳³ג€™׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ  ׳³ֲ ׳³ג„¢׳³ֲ¦׳³ֲ ׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ  ׳³ֲ ׳³ג„¢׳³ֲ¦׳³ֲ ׳³ג„¢׳³ֲ-׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ׳³ג€˜׳³ֻ׳³ג€”'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ׳³ג€™׳³ג€“׳³ֲ¨ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ׳³ג€™׳³ג€“׳³ֲ¨ ׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ֲ׳³ג€”׳³ֻ׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ¡׳³ֲ§׳³ֲ¨ ׳³ֲ©׳³ג€˜׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ֲ¦׳³ג€¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ג‚×׳³ג„¢׳³ֲ׳³ג€¢׳³ֻ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€”׳³ג€ (׳³ג€”׳³ֲ ""׳³ֲ)'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ§׳³ֲ׳³ג„¢׳³ֻ׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ג€ (׳³ֲ§׳³ֲ׳³ֲ¢)'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ¡׳³ג‚×׳³ֲ§׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€”׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג€¢׳³ג€˜ ׳³ג€׳³ג„¢׳³ג‚×׳³ֲ¨׳³ֲ ׳³ֲ¦׳³ג„¢׳³ֲ׳³ֲ׳³ג„¢'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (91, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€/׳³ג€˜׳³ֲ§׳³ֲ¨׳³ֳ— ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ג‚×׳³ֲ§׳³ג„¢׳³ֲ'),
    (91, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (91, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (91, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (91, N'Class', N'1'),
    (91, N'Class', N'10'),
    (91, N'Class', N'11'),
    (91, N'Class', N'12'),
    (91, N'Class', N'13'),
    (91, N'Class', N'14'),
    (91, N'Class', N'15'),
    (91, N'Class', N'2'),
    (91, N'Class', N'3'),
    (91, N'Class', N'4'),
    (91, N'Class', N'5'),
    (91, N'Class', N'6'),
    (91, N'Class', N'7'),
    (91, N'Class', N'8'),
    (91, N'Class', N'9'),
    (91, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (91, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (91, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (91, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (91, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (91, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (91, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (91, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (91, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (91, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (91, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (91, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (91, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (91, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (91, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (91, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (91, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (91, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (91, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (91, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (91, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (91, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (91, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (91, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (91, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (91, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (91, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (91, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (91, N'GradeLevel', N'׳³ֲ'),
    (91, N'GradeLevel', N'׳³ג€˜'),
    (91, N'GradeLevel', N'׳³ג€™'),
    (91, N'GradeLevel', N'׳³ג€'),
    (91, N'GradeLevel', N'׳³ג€'),
    (91, N'GradeLevel', N'׳³ג€¢'),
    (91, N'GradeLevel', N'׳³ג€“'),
    (91, N'GradeLevel', N'׳³ג€”'),
    (91, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (91, N'GradeLevel', N'׳³ֻ'),
    (91, N'GradeLevel', N'׳³ג„¢'),
    (91, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (91, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (94, N'Framework', N'׳³ֲ׳³ג€¢׳³ֲ ׳³ֻ׳³ג€¢׳³ג€˜׳³ֲ 662296 ׳³ֲ׳³ֲ ׳³ֻ׳³ג€¢׳³ג€˜׳³ֲ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'Framework', N'׳³ֲ׳³ֲ׳³ֳ—''׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ֲ׳³ג€˜׳³ג€¢ ׳³ֻ׳³ג€¢׳³ֲ¨ 662452 ׳³ֲ׳³ג€”׳³ֲ׳³ג€ ׳³ֲ¡׳³ֲ׳³ֲ׳³ג€” ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'Framework', N'׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€”׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ 650028 ׳³ֲ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ׳³ג€'),
    (94, N'Framework', N'׳³ֻ׳³ג€¢׳³ֲ¨ 148247 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ׳³ֲ׳³ֻ׳³ג€¢׳³ֲ¨ ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'Framework', N'׳³ֲ׳³ג€”׳³ֲ ׳³ג€ ׳³ֲ©׳³ג€¢׳³ג„¢׳³ג‚×׳³ֲ׳³ֻ 641407 ׳³ֲ׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ ׳³ג€˜׳³ג„¢'),
    (94, N'Framework', N'׳³ֲ¡׳³ג€¢׳³ג€¢׳³ֲ׳³ג€”׳³ֲ¨׳³ג€ 714204 ׳³ֲ¡׳³ג€¢׳³ג€¢׳³ֲ׳³ג€”׳³ֲ¨׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'Framework', N'׳³ֲ¢׳³ג„¢׳³ֲ¡׳³ֲ׳³ג€¢׳³ג€¢׳³ג„¢׳³ג€ 729871 ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג„¢׳³ֲ¡׳³ֲ׳³ג€¢׳³ג€¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'Framework', N'׳³ֲ¨׳³ֲ׳³ֲ¡ ׳³ֲ׳³ֲ׳³ֲ¢׳³ֲ׳³ג€¢׳³ג€ 540567 ׳³ֲ¨׳³ֲ׳³ֲ¡ ׳³ֲ׳³ֲ׳³ֲ¢׳³ֲ׳³ג€¢׳³ג€ ׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'Framework', N'׳³ֲ©׳³ג€¢׳³ֲ¢׳³ג‚×׳³ֲ׳³ֻ 148155 ׳³ֲ׳³ֲ§׳³ג„¢׳³ֲ£ ׳³ֲ©׳³ג€¢׳³ֲ¢׳³ג‚×׳³ֲ׳³ֻ ׳³ג€˜׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'EducationalProgram', N'׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€- ׳³ֲ׳³ג€“׳³ֲ¨׳³ג€” ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (94, N'Domain', N'׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (94, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (94, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (94, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (94, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (94, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (94, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (94, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (94, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (94, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (94, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (94, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (94, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (94, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (94, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (94, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (94, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (94, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (94, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (94, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—  ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (94, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (94, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (94, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ג€“׳³ג€¢׳³ֲ§ ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¥'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ¢ ׳³ֲ©׳³ֲ ׳³ג€'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ©׳³ֲ ׳³ג€'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ¢׳³ג€'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ©׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ֳ—׳³ג„¢׳³ג€”׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ—'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (94, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (94, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (94, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (94, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (94, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (94, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (94, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (94, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (94, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (94, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (94, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (94, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (94, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (94, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (94, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (94, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (94, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (94, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (87, N'EducationalProgram', N'׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€˜׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ""׳³ֲ׳³ֲ¥'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ֳ—׳³ג€™׳³ֲ¨'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג„¢""׳³ֲ¡ ׳³ג€˜׳³ֲ׳³ֲ¢׳³ג€˜׳³ֲ¨'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€˜""׳³ֲ¨'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€“׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ֳ—׳³ֲ ׳³ג„¢׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֳ—׳³ֲ""׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ֲ¨׳³ג€¢׳³ג€”'),
    (87, N'EducationalProgram', N'׳³ג€÷׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג„¢""׳³ֲ¡ ׳³ג„¢׳³ג„¢׳³ג€”׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ג‚×׳³ג€”׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (87, N'EducationalProgram', N'׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ֲ¢׳³ג€¢׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ-׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'EducationalProgram', N'׳³ג‚×׳³ג€¢׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ¢׳³ֳ—׳³ג„¢׳³ג€'),
    (87, N'EducationalProgram', N'׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (87, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֳ—'),
    (87, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (87, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ֲ¢׳³ֲ¨'),
    (87, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֲ - ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ג€׳³ג„¢'),
    (87, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ- ׳³ֲ ׳³ֲ§׳³ג€¢׳³ג€׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¨'),
    (87, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ'),
    (87, N'EducationalProgram', N'׳³ֳ—׳³ֲ""׳³ֲ-׳³ֲ ׳³ג€”׳³ֲ©׳³ג€¢׳³ֲ'),
    (87, N'Domain', N'׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢'),
    (87, N'Domain', N'׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (87, N'Domain', N'׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ג€”׳³ג€¢׳³ג€׳³ג„¢׳³ג€¢׳³ֳ—'),
    (87, N'Domain', N'׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (87, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (87, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (87, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€÷׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (87, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ-  ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (87, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (87, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (87, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (87, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (87, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (87, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (87, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (87, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (87, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (87, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (87, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (87, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (87, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (87, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (87, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (87, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (87, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—  ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (87, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (87, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (87, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ג€“׳³ג€¢׳³ֲ§ ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¥'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ¢ ׳³ֲ©׳³ֲ ׳³ג€'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ©׳³ֲ ׳³ג€'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ¢׳³ג€'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ©׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€“׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ֳ—׳³ג„¢׳³ג€”׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (87, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (87, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (87, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (87, N'Class', N'1'),
    (87, N'Class', N'10'),
    (87, N'Class', N'11'),
    (87, N'Class', N'12'),
    (87, N'Class', N'13'),
    (87, N'Class', N'14'),
    (87, N'Class', N'15'),
    (87, N'Class', N'2'),
    (87, N'Class', N'3'),
    (87, N'Class', N'4'),
    (87, N'Class', N'5'),
    (87, N'Class', N'6'),
    (87, N'Class', N'7'),
    (87, N'Class', N'8'),
    (87, N'Class', N'9'),
    (87, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (87, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (87, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (87, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (87, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (87, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (87, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (87, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (87, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (87, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (87, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (87, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (87, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (87, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (87, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (87, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (87, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (87, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (87, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (87, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (87, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (87, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (87, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (87, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (87, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (87, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (87, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (87, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (87, N'GradeLevel', N'׳³ֲ'),
    (87, N'GradeLevel', N'׳³ג€˜'),
    (87, N'GradeLevel', N'׳³ג€™'),
    (87, N'GradeLevel', N'׳³ג€'),
    (87, N'GradeLevel', N'׳³ג€'),
    (87, N'GradeLevel', N'׳³ג€¢'),
    (87, N'GradeLevel', N'׳³ג€“'),
    (87, N'GradeLevel', N'׳³ג€”'),
    (87, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (87, N'GradeLevel', N'׳³ֻ'),
    (87, N'GradeLevel', N'׳³ג„¢'),
    (87, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (87, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (90, N'EducationalProgram', N'׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€˜׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'EducationalProgram', N'׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'EducationalProgram', N'׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— -׳³ֲ׳³ֲ§׳³ג€׳³ֲ׳³ג„¢׳³ג€'),
    (90, N'EducationalProgram', N'׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ג€˜׳³ג‚×׳³ֲ¨׳³ֻ - ׳³ֲ§׳³ג€˜""׳³ֲ¡׳³ג„¢׳³ֲ'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ""׳³ֲ׳³ֲ¥'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ֳ—׳³ג€™׳³ֲ¨'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג„¢""׳³ֲ¡ ׳³ג€˜׳³ֲ׳³ֲ¢׳³ג€˜׳³ֲ¨'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€˜""׳³ֲ¨'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֲ'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ֳ—׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֳ—׳³ֲ""׳³ֲ'),
    (90, N'EducationalProgram', N'׳³ג€÷׳³ֳ—׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג„¢""׳³ֲ¡ ׳³ג„¢׳³ג„¢׳³ג€”׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'EducationalProgram', N'׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ג‚×׳³ג€”׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'EducationalProgram', N'׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ'),
    (90, N'EducationalProgram', N'׳³ֲ¢׳³ג€¢׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ-׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ'),
    (90, N'EducationalProgram', N'׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'EducationalProgram', N'׳³ג‚×׳³ג€¢׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ¢׳³ֳ—׳³ג„¢׳³ג€'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֳ—'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ג€÷׳³ֲ׳³ֲ׳³ג„¢'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג„¢׳³ג€”׳³ג€'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֲ - ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ג€׳³ג„¢'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ- ׳³ֲ ׳³ֲ§׳³ג€¢׳³ג€׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¨'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ""׳³ֲ©'),
    (90, N'EducationalProgram', N'׳³ֳ—׳³ֲ""׳³ֲ-׳³ֲ ׳³ג€”׳³ֲ©׳³ג€¢׳³ֲ'),
    (90, N'Domain', N'׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨ ׳³ֲ¡׳³ג€׳³ג„¢׳³ֲ¨'),
    (90, N'Domain', N'׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢'),
    (90, N'Domain', N'׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (90, N'Domain', N'׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ג€”׳³ג€¢׳³ג€׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Domain', N'׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (90, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (90, N'Subject', N'׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (90, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€÷׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—/׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¢׳³ֲ׳³ג„¢ ׳³ֲ׳³ֲ¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€˜׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ - ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ - ׳³ֻ׳³ג€÷׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— (׳³ֻ׳³ג€¢""׳³ג€˜)'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ג€™׳³ג€“׳³ֲ¨ ׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג„¢'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ֻ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ ׳³ג„¢׳³ֲ¦׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ§׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ג„¢׳³ג€ (׳³ֲ§׳³ֲ""׳³ֲ¢)'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג€“'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג€”'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ג‚×׳³ג€™׳³ג€ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢ -׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ג‚×׳³ג€™׳³ג€ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ג‚×׳³ג€™׳³ג€ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜ -׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ג‚×׳³ג€™׳³ג€ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¦׳³ג€'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג€¢׳³ֳ—-׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ג€“׳³ֲ ׳³ג€'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ג€”׳³ֲ ""׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€-׳³ֲ ׳³ג€׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ג€“׳³ֲ ׳³ג€'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢  ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ׳³ֳ— ׳³ג€™׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֻ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€- ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— 360'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€׳³ֳ—׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ©׳³ג€˜׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ג€׳³ֲ ׳³ג€¢׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€- ׳³ֳ—׳³ֲ§׳³ֲ©׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€˜׳³ֲ§׳³ֲ¨׳³ֳ— ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ג€™׳³ג€׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֻ׳³ֲ¨׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ׳³ג‚×׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג€˜׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ¨׳³ג€˜-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ׳³ג€”׳³ֲ ׳³ֲ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¦׳³ֲ¢׳³ג€ ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ֲ¨׳³ֻ'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ-׳³ֳ—׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢.'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ-  ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ג€ ׳³ג€¢׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ©׳³ג‚×׳³ֻ׳³ג„¢'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ¨׳³ג€ ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¢׳³ג€¢""׳³ֲ¡'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֳ—׳³ג„¢ ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ׳³ֲ׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ©׳³ֳ—""׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ-׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ©׳³ֳ—''''׳³ג‚× ׳³ֲ¢׳³ֲ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ֲ¡׳³ֲ§׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€.'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ג€¢׳³ג€¢׳³ֲ ׳³ג€, ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€¢׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ג€˜׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ג€׳³ג„¢׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ג€÷׳³ֲ ׳³ג€ ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ג€˜׳³ג€׳³ג€÷׳³ֲ©׳³ֲ¨׳³ג€/׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג€¢׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (90, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— (׳³ג‚×׳³ֲ¨""׳³ג€”, ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—, ׳³ֲ׳³ֳ—׳³ֲ ׳³ג€׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ג€¢׳³ג€), ׳³ֲ¢׳³ֲ ׳³ג‚×׳³ג„¢ ׳³ג€׳³ֲ¦׳³ג€¢׳³ֲ¨׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ֳ— ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢/׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢.'),
    (90, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (90, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֻ׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ׳³ג€¢׳³ג€™׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€¢/׳³ֲ׳³ג€¢ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ— ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ג€׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ׳³ג€'),
    (90, N'Subject', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¢׳³ֲ¨׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֲ¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ¡׳³ג€˜׳³ֲ¨׳³ג€, ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג„¢׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€, ׳³ֲ׳³ג‚×׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—.'),
    (90, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ג€׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€׳³ג„¢׳³ג€¢ (׳³ג€˜׳³ג€÷׳³ֳ—׳³ג€˜, ׳³ג€˜׳³ֲ¢''''׳³ג‚×) ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€.'),
    (90, N'Subject', N'׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג€¢׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ©׳³ֲ ׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€, ׳³ֲ׳³ֳ—׳³ֲ ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€¢'),
    (90, N'Subject', N'׳³ג€׳³ג‚×׳³ֲ¢׳³ֲ׳³ֳ— ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ— ׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— - ׳³ג€÷׳³ֲ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֻ׳³ג€÷׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— (׳³ֻ׳³ג€¢""׳³ג€˜)'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ—/׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“/׳³ג€˜׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€-׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ ׳³ֲ׳³ג€“׳³ג€¢׳³ֲ¨׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ג€˜ ׳³ֳ—׳³ג‚×׳³ֲ§׳³ג„¢׳³ג€׳³ג„¢/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ—-׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (90, N'Subject', N'׳³ג€“׳³ג„¢׳³ג€׳³ג€¢׳³ג„¢ ׳³ג€÷׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ—׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€”׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (90, N'Subject', N'׳³ג€”׳³ֲ©׳³ג„¢׳³ג‚×׳³ֳ— ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ¦׳³ֻ׳³ג€˜׳³ֲ¨ ׳³ֲ׳³ג€÷׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ג€˜׳³ֲ ׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€¢׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ֳ—׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (90, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (90, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ג€׳³ג€™׳³ֲ׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג‚×׳³ֳ—׳³ג€¢׳³ג€”'),
    (90, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ג€׳³ֳ—׳³ג„¢׳³ֲ¢׳³ֲ¦׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ§׳³ג€¢׳³ֲ׳³ג€™׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€”׳³ֲ¨׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ - ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ֲ¡׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (90, N'Subject', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ -׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ֲ©׳³ֻ׳³ג€”'),
    (90, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ§׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ג€¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€ ׳³ֲ׳³ג€”׳³ֻ׳³ג„¢׳³ג€˜׳³ג€ ׳³ֲ¢׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€ -׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ג€ ׳³ֲ׳³ג€”׳³ֻ׳³ג„¢׳³ג€˜׳³ֳ— ׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€ ׳³ג€˜׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ֲ ׳³ג€׳³ֲ/׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֲ¨׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€¢׳³ֳ—  ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  - ׳³ֳ—׳³ֲ¦׳³ג‚×׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג€“'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג€”'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ֻ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ֲ ׳³ג€”׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ֲ ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ—  ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֳ—׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ׳³ֳ—׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€¢׳³ג„¢ ׳³ג„¢׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¢ ׳³ֲ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— - ׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€˜׳³ג„¢׳³ֲ§׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג€׳³ֳ—׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ׳³ֲ¢׳³ג€¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—  ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ¢׳³ג€׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג€¢׳³ג€˜ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢׳³ֳ— -׳³ֳ—׳³ֲ§׳³ֲ©׳³ג€¢׳³ֲ¨׳³ֳ— ׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€׳³ֲ׳³ג„¢׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ—-׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ'' ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨ ׳³ג€˜׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ—׳³ג€ ׳³ֲ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€.'),
    (90, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”, ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ֲ׳³ֲ׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (90, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ -׳³ֲ׳³ג„¢׳³ֻ׳³ג€˜'),
    (90, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ -׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ©׳³ג€¢׳³ג€˜'),
    (90, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ -׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¨׳³ֻ'),
    (90, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ -׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€÷""׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¢׳³ג€׳³ג€÷׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ג€˜׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€׳³ֲ¢ ׳³ג€¢׳³ג„¢׳³ג€׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ -׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (90, N'Subject', N'׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ׳³ֳ— ׳³ג€׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג€׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ¦׳³ֲ¨׳³ג€÷׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (90, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢  ׳³ֻ׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ׳³ג€™׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€ ׳³ג€׳³ג„¢׳³ג€™׳³ג„¢׳³ֻ׳³ֲ׳³ֲ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—, ׳³ג€׳³ֲ ׳³ג€™׳³ג€“׳³ֲ¨׳³ֳ— ׳³ֲ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ— ׳³ג€¢׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ.'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ׳³ג€˜׳³ג€”׳³ג„¢׳³ֲ ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ§׳³ג€˜׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ג€׳³ֲ¨׳³ג€÷׳³ג„¢ ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€ ׳³ג€¢׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ""׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ֳ— ׳³ֲ¢׳³ג€¢׳³ֲ׳³ֲ§"" ׳³ֲ¢׳³ֲ ׳³ֲ ׳³ֲ¦׳³ג„¢׳³ג€™׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€”׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— (׳³ֲ¨׳³ֲ׳³ֲ© ׳³ג€׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€) ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ג€׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ¨׳³ֲ¦׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג‚×׳³ֲ§׳³ג€” ׳³ג€÷׳³ג€¢׳³ֲ׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“  ׳³ג€׳³ֳ—׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ג„¢׳³ֲ׳³ֲ׳³ג€¢׳³ג€™ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ©׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ© ׳³ג€˜׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ׳³ג€”׳³ג€¢׳³ג€“'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€˜׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ׳³ֳ— 360 ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ג„¢׳³ֲ©׳³ג€™׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€׳³ֲ׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨ / ׳³ג€׳³ֲ©׳³ֳ—׳³ֳ—׳³ג‚×׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג„¢׳³ג‚×׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג€¢׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ¥ ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¦/׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ֳ— ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ— ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ׳³ֲ׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג€”׳³ג„¢ ׳³ג„¢׳³ג€”׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ ׳³ֲ©׳³ג„¢׳³ֲ¨׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג‚×׳³ֲ׳³ֲ¨׳³ֲ ׳³ֲ¨׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ֲ׳³ֲ© ׳³ג€׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג€¢׳³ֳ—׳³ג‚×׳³ג„¢׳³ֲ ׳³ג€”׳³ג€¢׳³ֲ¥ ׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג„¢׳³ג€˜׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—- ׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ׳³ג„¢ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€˜׳³ֳ—׳³ג€”׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג„¢׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ¨ ׳³ג€׳³ג€׳³ג€¢׳³ֲ ׳³ג€׳³ֲ׳³ֲ ׳³ג€¢׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ ׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—-  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ¨ ׳³ֲ׳³ג€¢׳³ֲ§׳³ג€׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¡׳³ֳ—׳³ג€™׳³ֲ׳³ג€¢׳³ֳ— ׳³ג„¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג€”׳³ג€׳³ֲ©׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ג€- ׳³ֲ׳³ֲ¡׳³ג„¢׳³ֲ¨׳³ֳ— ׳³ג€¢׳³ֲ¢׳³ג€/׳³ג€”׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג„¢׳³ֲ¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג€¢׳³ֲ¡׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ג€“׳³ג€¢׳³ֲ§ ׳³ֲ׳³ג„¢׳³ג€¢׳³ֲ׳³ֲ ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ ׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ¨׳³ֲ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ג€”׳³ֻ""׳³ג€˜'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ©׳³ג€÷׳³ג€˜׳³ג€ ׳³ג„¢׳³ג€˜'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ג€”׳³ֻ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֳ— ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ג€ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ג„¢׳³ֲ©׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ֲ¥'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ׳³ג‚×׳³ג€™׳³ֲ© ׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ג€ ׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—- ׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ג‚×׳³ג„¢ ׳³ג‚×׳³ֲ¢׳³ג€¢׳³ֲ׳³ג€ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€¢׳³ג€¢׳³ג€”׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֻ׳³ג€¢׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ֲ׳³ג€™׳³ג€˜׳³ג„¢ ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֻ׳³ג€¢׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ג€׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ג€¢׳³ֲ׳³ג€˜׳³ג€”׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ג€ ׳³ֲ§׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ¨, ׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ג€, ׳³ג€¢׳³ֳ—׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€¢׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ֳ— ׳³ג‚×׳³ֲ¨׳³ֻ׳³ֲ ׳³ג„¢׳³ֳ—/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ֳ—׳³ג„¢׳³ֳ—/׳³ֲ׳³ֲ©׳³ג‚×׳³ג€”׳³ֳ—׳³ג„¢׳³ֳ—/׳³ֲ§׳³ג€׳³ג„¢׳³ֲ׳³ֳ—׳³ג„¢׳³ֳ—.');
    INSERT INTO @ScopeSeed (ProgramId, ScopeType, Description) VALUES
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¨׳³ֲ׳³ג€¢׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ג„¢׳³ג€¢׳³ג€¢׳³ג€” (׳³ֲ׳³ג€”׳³ֳ— ׳³ֲ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©), ׳³ג€˜׳³ג€׳³ֳ—׳³ֲ׳³ֲ ׳³ֲ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€¢׳³ֲ׳³ג€׳³ג€”׳³ֲ׳³ֻ׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€™׳³ֲ£ ׳³ג€¢׳³ֲ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€׳³ֲ׳³ג€÷׳³ֲ¨׳³ג€“.'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¡׳³ֲ׳³ֲ ׳³ֲ ׳³ג€׳³ֲ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ-  ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ג€÷׳³ֳ—׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€”׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ-  ׳³ֲ׳³ג€”׳³ֲ§׳³ֲ¨ ׳³ֲ׳³ג„¢׳³ֲ ׳³ֻ׳³ֲ¨׳³ֲ ׳³ֻ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ-  ׳³ֲ¡׳³ג‚×׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ֲ׳³ג€”׳³ֲ§׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ-  ׳³ג‚×׳³ֲ¨׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ׳³ֲ׳³ֲ¦׳³ֲ¢ ׳³ֲ©׳³ֲ ׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ©׳³ֲ ׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ֲ¡׳³ג€¢׳³ֲ£ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ג€”׳³ג„¢׳³ֲ׳³ֳ— ׳³ֲ©׳³ֲ ׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ§׳³ג€׳³ֲ׳³ג„¢׳³ג€ ׳³ג€˜׳³ֳ—׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ג€÷׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ג€׳³ג„¢׳³ֲ§׳³ג€¢׳³ֳ— ׳³ג„¢׳³ֲ¦׳³ג„¢׳³ֲ׳³ג€ ׳³ֲ׳³ג€׳³ֲ׳³ֲ¨׳³ֲ¥ - ׳³ג€™׳³ג€”׳³ֲ׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֻ׳³ג€¢׳³ג‚×׳³ֲ¡ ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֻ׳³ג€¢׳³ג‚×׳³ֲ¡ ׳³ֳ—׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ£'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ¨׳³ֳ— ׳³ג€׳³ֲ¦׳³ֻ׳³ג„¢׳³ג„¢׳³ג€׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג€™׳³ֲ¨׳³ג„¢׳³ֲ¢׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֲ§׳³ֲ©׳³ג€¢׳³ֳ— ׳³ג‚×׳³ֻ׳³ג€¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֳ—׳³ג„¢ ׳³ֲ¡׳³ג‚×׳³ֲ¨ ׳³ג€˜׳³ֲ׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€˜׳³ֳ—׳³ג„¢ ׳³ֲ¡׳³ג‚×׳³ֲ¨ ׳³ג‚×׳³ג€¢׳³ֲ¨׳³ֲ¦׳³ג„¢ ׳³ג€׳³ֲ¨׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€” ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ— ׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢ ׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ©׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג€ ׳³ֲ׳³ג€”׳³ֲ׳³ֲ§׳³ֳ—׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€  ׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“ ׳³ג‚×׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€”׳³ֲ¨׳³ג„¢׳³ג€™׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ -׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ  ׳³ֲ ׳³ג„¢׳³ֲ¦׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ  ׳³ֲ ׳³ג„¢׳³ֲ¦׳³ֲ ׳³ג„¢׳³ֲ-׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ׳³ג€˜׳³ֻ׳³ג€”'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ׳³ג€™׳³ג€“׳³ֲ¨ ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ׳³ג€™׳³ג€“׳³ֲ¨ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ׳³ג€™׳³ג€“׳³ֲ¨ ׳³ֲ¢׳³ֲ¨׳³ג€˜׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ ׳³ֲ׳³ג€”׳³ֻ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ¡׳³ֲ§׳³ֲ¨ ׳³ֲ©׳³ג€˜׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ֲ¦׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ג‚×׳³ג„¢׳³ֲ׳³ג€¢׳³ֻ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ג„¢׳³ג€¢׳³ג€”׳³ג€ (׳³ג€”׳³ֲ ""׳³ֲ)'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ׳³ג€׳³ג„¢׳³ֲ -׳³ֲ§׳³ֲ׳³ג„¢׳³ֻ׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ג€ (׳³ֲ§׳³ֲ׳³ֲ¢)'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ג€“׳³ֲ ׳³ג€ ׳³ֲ¡׳³ג‚×׳³ֲ§׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€¢׳³ֲ¡׳³ג€׳³ג„¢׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ׳³ֲ׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ג€ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג„¢׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֻ׳³ֲ׳³ֲ¢׳³ֳ— ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€׳³ֲ¡׳³ֲ׳³ג€÷׳³ג€¢׳³ֳ— ׳³ֻ׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ׳³ג€¢׳³ג€™׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€“׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג€¢׳³ֲ¦׳³ֲ׳³ג„¢ ׳³ֲ׳³ֳ—׳³ג„¢׳³ג€¢׳³ג‚×׳³ג„¢׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€“׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€÷׳³ֲ׳³ֲ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€“׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€÷׳³ג‚×׳³ֲ¨׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€”׳³ג€׳³ֲ¨׳³ג„¢ ׳³ֲ׳³ֲ""׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ׳³ג€¢׳³ג€™׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג„¢׳³ג€¢׳³ג€”""׳³ֲ- ׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ¨׳³ג€¢׳³ֲ ׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢ ׳³ג€׳³ֲ¢׳³ֲ©׳³ֲ¨׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ— ׳³ֲ©׳³ג€”""׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ג€”׳³ג„¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ֳ— ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג€÷׳³ֳ—׳³ג„¢׳³ג€˜׳³ג€ ׳³ג€¢׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€¢׳³ֲ¢׳³ג€׳³ג€¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”""׳³ֻ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֲ¨׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€׳³ֳ—׳³ג„¢׳³ֲ©׳³ג€˜׳³ג€¢׳³ֳ—׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ג„¢׳³ג‚×׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג€”׳³ֲ¨׳³ג€׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ©׳³ֲ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“ ׳³ֲ¦׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€”׳³ג€¢׳³ֲ׳³ֲ ׳³ג€׳³ג€¢׳³ג€”׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג€™׳³ג€“׳³ֲ¨ ׳³ג€˜׳³ג€׳³ג€¢׳³ֲ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ¨׳³ֲ©׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג„¢׳³ג‚×׳³ג€¢׳³ג„¢ ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ ׳³ג€¢׳³ג€÷׳³ג€”׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ§׳³ג€˜ ׳³ֲ¦׳³ג„¢׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¢׳³ֲ¨׳³ג€÷׳³ֳ— ׳³ֲ§׳³ג€˜׳³ֲ¡׳³ֲ ׳³ֻ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¦׳³ג€˜׳³ֳ— ׳³ג€÷׳³ג€¢׳³ג€” ׳³ֲ׳³ג€׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¦׳³ג€™׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ¢׳³ג€˜׳³ג€¢׳³ג€׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ג€”׳³ג„¢׳³ֲ¨׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ ׳³ֲ©׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ©׳³ג€¢׳³ג€˜ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ֲ׳³ג€™׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€“׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ ׳³ג€™׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ג€“׳³ג€÷׳³ֲ׳³ג€¢׳³ֳ— ׳³ֲ׳³ֳ—׳³ֲ׳³ֻ׳³ג„¢׳³ֲ§׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢ ׳³ֲ׳³ֲ ׳³ג€™׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢ ׳³ֲ׳³ֳ—׳³ֲ׳³ֻ׳³ג„¢׳³ֲ§׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ג€׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג€™׳³ג€ ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֻ׳³ֻ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ֲ ׳³ֲ¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ג„¢׳³ג€÷׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€”׳³ג€¢׳³ג€׳³ֲ©׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ֲ§׳³ֲ¨׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¢׳³ג€¢׳³ג€™׳³ֲ ׳³ג„¢׳³ֲ ׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¢׳³ג€¢׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג€׳³ֲ©׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ֳ—׳³ג„¢׳³ג€”׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ§׳³ג„¢׳³ג€׳³ג€¢׳³ֲ ׳³ֲ ׳³ג€¢׳³ֲ¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ§׳³ֲ¨׳³ג€˜׳³ג€ ׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€™׳³ג€˜׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ג€”׳³ג€¢׳³ֲ¨׳³ֲ£'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ׳³ג€¢׳³ֲ¨׳³ג„¢ ׳³ֲ׳³ֲ§׳³ֲ¨׳³ג€ ׳³ג€˜׳³ג€¢׳³ג€”׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§ ׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג„¢׳³ֲ§׳³ג„¢ ׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ׳³ג„¢ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ג„¢ ׳³ֻ׳³ג€÷׳³ֲ ׳³ֲ׳³ג€¢׳³ֳ— ׳³ג€¢׳³ג€˜׳³ג€™׳³ֲ¨׳³ג€¢׳³ֳ— (׳³ֻ׳³ג€¢""׳³ג€˜)'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג€¢׳³ג€˜ ׳³ג€׳³ג„¢׳³ג‚×׳³ֲ¨׳³ֲ ׳³ֲ¦׳³ג„¢׳³ֲ׳³ֲ׳³ג„¢'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֲ׳³ג€”׳³ג€¢׳³ג€“׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€/׳³ג€˜׳³ֲ§׳³ֲ¨׳³ֳ— ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€” ׳³ג€”׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ֲ¨׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג„¢׳³ג€÷׳³ג€¢׳³ג€“/׳³ֲ ׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€”/׳³ֲ¢׳³ג„¢׳³ג€˜׳³ג€¢׳³ג€/׳³ג€˜׳³ֲ§׳³ֲ¨׳³ֳ— ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ֲ¡׳³ג‚×׳³ֲ§׳³ג„¢׳³ֲ'),
    (90, N'Subject', N'׳³ֲ¨׳³ג€÷׳³ג€¢׳³ג€“ ׳³ֲ ׳³ֳ—׳³ג€¢׳³ֲ ׳³ג„¢׳³ֲ- ׳³ג‚×׳³ג„¢׳³ֳ—׳³ג€¢׳³ג€” ׳³ֲ©׳³ֲ׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ׳³ג€¢׳³ג€׳³ֲ ׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (90, N'Subject', N'׳³ֳ—׳³ג€÷׳³ֲ ׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ֲ ׳³ג€¢׳³ג€˜׳³ֲ§׳³ֲ¨׳³ג€ ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ©׳³ֲ ׳³ֳ—׳³ג„¢׳³ֳ—, ׳³ג‚×׳³ֲ¨׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ§׳³ֻ׳³ג„¢׳³ֲ ׳³ג€¢׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€”׳³ג„¢׳³ֲ ׳³ג€¢׳³ג€÷׳³ג„¢׳³ג€¢׳³ֳ— ׳³ג€˜׳³ֲ׳³ֲ¡׳³ג€™׳³ֲ¨׳³ֳ—'),
    (90, N'Subject', N'׳³ֳ—׳³ֲ׳³ג„¢׳³ג€÷׳³ג€ ׳³ֲ¨׳³ג€™׳³ֲ©׳³ג„¢׳³ֳ—-׳³ֲ§׳³ג€¢׳³ג€™׳³ֲ ׳³ֻ׳³ג„¢׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€˜׳³ֲ¢׳³ג€¢׳³ג€˜׳³ג€ ׳³ג€˜׳³ֲ׳³ֲ¦׳³ג€˜׳³ג„¢ ׳³ג€׳³ֳ—׳³ֲ׳³ג€¢׳³ג€׳³ג€׳³ג€¢׳³ֳ—/׳³ֲ§׳³ג€¢׳³ֲ ׳³ג‚×׳³ֲ׳³ג„¢׳³ֲ§׳³ֻ ׳³ֲ׳³ֲ¨׳³ג€™׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ/׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ  ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ג„¢׳³ג€¢׳³ֲ¢׳³ֲ¥'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ג€¢׳³ֲ¨׳³ג€/ ׳³ֲ׳³ג€™׳³ג„¢׳³ג€ ׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€׳³ֲ ׳³ג€˜׳³ג„¢׳³ֳ— ׳³ג€׳³ֲ¡׳³ג‚×׳³ֲ¨'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ׳³ֲ ׳³ג€”׳³ג€ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג€׳³ג€׳³ֲ¨׳³ג€÷׳³ג€'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג€׳³ג‚×׳³ג„¢׳³ֲ§׳³ג€¢׳³ג€”'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ג„¢׳³ג„¢׳³ֲ©׳³ג€¢׳³ג€˜׳³ג„¢'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ- ׳³ֲ׳³ֲ ׳³ֲ©׳³ג„¢ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (90, N'DiscussionCode', N'׳³ג€׳³ג„¢׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֲ¨׳³ג€÷׳³ג€“ ׳³ג€׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ—'),
    (90, N'Class', N'1'),
    (90, N'Class', N'10'),
    (90, N'Class', N'11'),
    (90, N'Class', N'12'),
    (90, N'Class', N'13'),
    (90, N'Class', N'14'),
    (90, N'Class', N'15'),
    (90, N'Class', N'2'),
    (90, N'Class', N'3'),
    (90, N'Class', N'4'),
    (90, N'Class', N'5'),
    (90, N'Class', N'6'),
    (90, N'Class', N'7'),
    (90, N'Class', N'8'),
    (90, N'Class', N'9'),
    (90, N'Class', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (90, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€׳³ֳ—׳³ֲ ׳³ג€׳³ג€™׳³ג€¢׳³ֳ—׳³ג„¢׳³ֳ—'),
    (90, N'Class', N'׳³ג€˜׳³ֲ ׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ֲ'),
    (90, N'Class', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ§׳³ֲ£ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ—'),
    (90, N'Class', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (90, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ׳³ג€¢׳³ג€÷׳³ֲ׳³ג€¢׳³ֲ¡׳³ג„¢׳³ֳ— ׳³ג€׳³ג„¢׳³ֲ¢׳³ג€'),
    (90, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€”׳³ג€˜׳³ֲ¨׳³ֳ—׳³ג„¢׳³ֳ—'),
    (90, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ֻ׳³ג„¢׳³ג‚×׳³ג€¢׳³ֲ׳³ג„¢׳³ֳ—'),
    (90, N'Class', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ֳ— ׳³ג€÷׳³ג„¢׳³ֳ—׳³ֳ—׳³ג„¢׳³ֳ—'),
    (90, N'Class', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (90, N'Class', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ— ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (90, N'Class', N'׳³ֲ ׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ֲ©׳³ֲ¢׳³ג€¢׳³ֳ— ׳³ֲ ׳³ג€÷׳³ג€¢׳³ֲ'),
    (90, N'Class', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'Class', N'׳³ֲ©׳³ג„¢׳³ג€”׳³ג€ ׳³ֲ¢׳³ֲ ׳³ג€׳³ג€÷׳³ֳ—׳³ג€/׳³ֲ§׳³ג€˜׳³ג€¢׳³ֲ¦׳³ג€'),
    (90, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ג€™׳³ג€¢׳³ֲ ׳³ֲ׳³ֲ©׳³ֲ׳³ג€˜׳³ג„¢׳³ֲ'),
    (90, N'LocalityDistrictNational', N'׳³ֲ׳³ג„¢׳³ֲ ׳³ג€׳³ֲ¨׳³ג„¢׳³ֲ©׳³ג€¢׳³ֳ—'),
    (90, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€˜׳³ג€¢׳³ֲ© ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ ׳³ֲ¨׳³ג€˜ ׳³ֲ׳³ֲ§׳³ֲ¦׳³ג€¢׳³ֲ¢׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'LocalityDistrictNational', N'׳³ג€™׳³ג„¢׳³ג€¢׳³ֲ¡ ׳³ֳ—׳³ֲ¨׳³ג€¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (90, N'LocalityDistrictNational', N'׳³ג€׳³ג€™׳³ג€׳³ֲ׳³ֳ— ׳³ֳ—׳³ֲ§׳³ֲ¦׳³ג„¢׳³ג€˜'),
    (90, N'LocalityDistrictNational', N'׳³ג€׳³ג€÷׳³ֲ ׳³ֲ¡׳³ֳ— ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢׳³ג„¢׳³ֲ ׳³ג€˜׳³ג€˜׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ¢'),
    (90, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ג€׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ'),
    (90, N'LocalityDistrictNational', N'׳³ג€׳³ֲ ׳³ג€”׳³ג„¢׳³ג„¢׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (90, N'LocalityDistrictNational', N'׳³ג€׳³ֲ¢׳³ֲ¦׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג€¢׳³ֲ¨׳³ֲ׳³ג€ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (90, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ֲ׳³ֲ׳³ֳ— ׳³ֲ¦׳³ג€¢׳³ג€¢׳³ֳ—׳³ג„¢ ׳³ג€׳³ג„¢׳³ג€™׳³ג€¢׳³ג„¢ ׳³ֲ׳³ֲ׳³ֲ©׳³ג„¢׳³ֲ׳³ג€'),
    (90, N'LocalityDistrictNational', N'׳³ג€׳³ֳ—׳³ג„¢׳³ג€”׳³ֲ¡׳³ג€¢׳³ֳ— ׳³ֲ׳³ֲ¡׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢ ׳³ֲ©׳³ג„¢׳³ֲ ׳³ג€¢׳³ג„¢ ׳³ֲ ׳³ג€¢׳³ֲ¡׳³ג‚×׳³ג„¢׳³ֲ'),
    (90, N'LocalityDistrictNational', N'׳³ג€÷׳³ג„¢׳³ֲ ׳³ג€¢׳³ֲ¡ ׳³ֲ׳³ג€¢׳³ֲ¢׳³ֲ¦׳³ג€ ׳³ג‚×׳³ג€׳³ג€™׳³ג€¢׳³ג€™׳³ג„¢׳³ֳ—'),
    (90, N'LocalityDistrictNational', N'׳³ֲ׳³ֲ׳³ג„¢׳³ג€׳³ֳ—  ׳³ֲ¢׳³ֲ׳³ג„¢׳³ֳ—׳³ג„¢׳³ֲ'),
    (90, N'LocalityDistrictNational', N'׳³ֲ¡׳³ג„¢׳³ג€¢׳³ֲ¨׳³ג„¢׳³ֲ ׳³ֲ׳³ג„¢׳³ֲ׳³ג€¢׳³ג€׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'LocalityDistrictNational', N'׳³ֲ§׳³ג„¢׳³ג€¢׳³ֲ ׳³ג€׳³ֲ©׳³ֳ—׳³ֲ׳³ֲ׳³ג€¢׳³ג„¢׳³ג€¢׳³ֳ—'),
    (90, N'LocalityDistrictNational', N'׳³ֲ©׳³ג„¢׳³ֳ—׳³ג€¢׳³ֲ£ ׳³ג€™׳³ג€¢׳³ֲ¨׳³ֲ׳³ג„¢׳³ֲ ׳³ג€”׳³ג„¢׳³ֲ¦׳³ג€¢׳³ֲ ׳³ג„¢׳³ג„¢׳³ֲ'),
    (90, N'LocalityDistrictNational', N'׳³ֳ—׳³ֲ׳³ג€¢׳³ֲ ׳³ֲ¢׳³ֲ ׳³ֳ—׳³ג€¢׳³ג€÷׳³ֲ ׳³ג„¢׳³ג€¢׳³ֳ— ׳³ֲ§׳³ג„¢׳³ג„¢׳³ֲ׳³ג€¢׳³ֳ—'),
    (90, N'GradeLevel', N'׳³ֲ'),
    (90, N'GradeLevel', N'׳³ג€˜'),
    (90, N'GradeLevel', N'׳³ג€™'),
    (90, N'GradeLevel', N'׳³ג€'),
    (90, N'GradeLevel', N'׳³ג€'),
    (90, N'GradeLevel', N'׳³ג€¢'),
    (90, N'GradeLevel', N'׳³ג€“'),
    (90, N'GradeLevel', N'׳³ג€”'),
    (90, N'GradeLevel', N'׳³ג€”׳³ג€¢׳³ג€˜׳³ג€'),
    (90, N'GradeLevel', N'׳³ֻ'),
    (90, N'GradeLevel', N'׳³ג„¢'),
    (90, N'GradeLevel', N'׳³ג„¢׳³ֲ'),
    (90, N'GradeLevel', N'׳³ג„¢׳³ג€˜'),
    (90, N'GradeLevel', N'׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ֲ'),
    (90, N'GradeLevel', N'׳³ֲ©׳³ג„¢׳³ֲ¢׳³ג€¢׳³ֲ¨ ׳³ג€˜');

    IF EXISTS (SELECT 1 FROM dbo.Projects WHERE Id = 6)
    BEGIN
        INSERT INTO dbo.ProjectPrograms (ProjectId, ProgramId)
        SELECT DISTINCT 6, seed.ProgramId
        FROM @ScopeSeed seed
        JOIN dbo.Programs program ON program.Id = seed.ProgramId AND program.IsActive = 1
        WHERE NOT EXISTS (
            SELECT 1 FROM dbo.ProjectPrograms existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId
        );

        INSERT INTO dbo.ProjectProgramFrameworks (ProjectId, ProgramId, FrameworkId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.Frameworks lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'Framework'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramFrameworks existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.FrameworkId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramEducationalPrograms (ProjectId, ProgramId, EducationalProgramId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.EducationalPrograms lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'EducationalProgram'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramEducationalPrograms existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.EducationalProgramId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramDomains (ProjectId, ProgramId, DomainId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.Domains lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'Domain'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramDomains existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.DomainId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramSubjects (ProjectId, ProgramId, SubjectId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.Subjects lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'Subject'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramSubjects existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.SubjectId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramDiscussionCodes (ProjectId, ProgramId, DiscussionCodeId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.DiscussionCodes lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'DiscussionCode'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramDiscussionCodes existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.DiscussionCodeId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramClasses (ProjectId, ProgramId, ClassId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.SchoolClasses lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'Class'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramClasses existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.ClassId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramGradeLevels (ProjectId, ProgramId, GradeLevelId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.GradeLevels lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'GradeLevel'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramGradeLevels existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.GradeLevelId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramLocalityDistrictNationals (ProjectId, ProgramId, LocalityDistrictNationalId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.LocalityDistrictNationals lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'LocalityDistrictNational'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramLocalityDistrictNationals existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.LocalityDistrictNationalId = lookupRow.Id
          );
    END;

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260708090000_MergeDuplicateProgramsAndSeedProjectProgramScopes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260708090000_MergeDuplicateProgramsAndSeedProjectProgramScopes', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260708103000_SeedProjectSixProgramScopeDefaults')
BEGIN

    SET NOCOUNT ON;

    IF OBJECT_ID(N'dbo.ProjectProgramLocalityDistrictNationals', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ProjectProgramLocalityDistrictNationals (
            ProjectId int NOT NULL,
            ProgramId int NOT NULL,
            LocalityDistrictNationalId int NOT NULL,
            CONSTRAINT PK_ProjectProgramLocalityDistrictNationals PRIMARY KEY (ProjectId, ProgramId, LocalityDistrictNationalId),
            CONSTRAINT FK_ProjectProgramLocalityDistrictNationals_LocalityDistrictNationals_LocalityDistrictNationalId
                FOREIGN KEY (LocalityDistrictNationalId) REFERENCES dbo.LocalityDistrictNationals (Id) ON DELETE CASCADE
        );
        CREATE INDEX IX_ProjectProgramLocalityDistrictNationals_LocalityDistrictNationalId
            ON dbo.ProjectProgramLocalityDistrictNationals (LocalityDistrictNationalId);
    END;

    DECLARE @ScopeSeed TABLE (ProgramId int NOT NULL, ScopeType nvarchar(64) NOT NULL, Description nvarchar(1000) NOT NULL);
    INSERT INTO @ScopeSeed (ProgramId, ScopeType, Description) VALUES
    (93, N'EducationalProgram', N'אור בגנים'),
    (93, N'EducationalProgram', N'מועדוניות משפחתיות'),
    (93, N'Domain', N'רווחה וקהילה'),
    (93, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (93, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (93, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי המרכזים לגיל הרך'),
    (93, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי התוכנית- איתור כח אדם'),
    (93, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי מועדוניות'),
    (93, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי מרכזים לגיל הרך'),
    (93, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (93, N'Subject', N'בניית תוכנית הנחייה- מועדוניות'),
    (93, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (93, N'Subject', N'בניית תוכנית הנחייה- מרכזים לגיל הרך'),
    (93, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (93, N'Subject', N'בניית תוכנית הנחייה- שכבה ט'),
    (93, N'Subject', N'בניית תוכנית הנחייה- שכבה י'),
    (93, N'Subject', N'בניית תוכנית הנחייה- שכבה יא'),
    (93, N'Subject', N'בניית תוכנית הנחייה- שכבה יב'),
    (93, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (93, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (93, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (93, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי  תלמידי המרכזים לגיל הרך'),
    (93, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (93, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (93, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (93, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (93, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (93, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (93, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (93, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (93, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (93, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (93, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (93, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (93, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים-  גורמי רווחה'),
    (93, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- רשות'),
    (93, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (93, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (93, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (93, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (93, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (93, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (93, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (93, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (93, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (93, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (93, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (93, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (93, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום חינוכית-טיפולית - התבגרות וחוסן'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום חינוכית-טיפולית - כלים לזיהוי משברים'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום חינוכית-טיפולית - מודלים לבניית חוסן ושימור מוטיבציה'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום פדגוגיה טיפולית - כלים ליצירת דיאלוג טיפולי חינוכי'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום פדגוגיה טיפולית - מודל עבודה רפלקטיבי'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום פדגוגיה טיפולית - עקרונות מרכזיים'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום תמיכה רגשית קוגנטיבית - כלים וטיפול'),
    (93, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום תמיכה רגשית קוגנטיבית- תקשורת אמון וגבולות'),
    (93, N'Subject', N'השתתפות בהשתלמות פיתוח מקצועי לצוותי מועדוניות'),
    (93, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (93, N'Subject', N'השתתפות במפגש וועדת היגוי ברשות המקומית'),
    (93, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (93, N'Subject', N'השתתפות במפגש מנחים מרכזים לגיל הרך אזוריים וארציים'),
    (93, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (93, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (93, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (93, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (93, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (93, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (93, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (93, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (93, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (93, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (93, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (93, N'Subject', N'מפגש הנחיה אישית  - מנהלת מועדונית'),
    (93, N'Subject', N'מפגש הנחיה אישית  - ראיונות  ילדים'),
    (93, N'Subject', N'מפגש הנחיה אישית  - תצפיות ומעקב'),
    (93, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (93, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (93, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (93, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (93, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (93, N'Subject', N'מפגש הנחיה אישית  מנחת מועדנית'),
    (93, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (93, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (93, N'Subject', N'מפגש הנחייה אישית - טיפול ספירלי'),
    (93, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (93, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (93, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (93, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (93, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (93, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (93, N'Subject', N'מפגש הנחייה אישית -נוכחות  והיעדרויות'),
    (93, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (93, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (93, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (93, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (93, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (93, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (93, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (93, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (93, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (93, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (93, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (93, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- התאמת מסגרת לילד'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- וועדות שיבוץ ילדים'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- מועצת פדגוגית ילדים'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- מניעת נשירה'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (93, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית-  בתחום מניעת אלימות'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית-  למידת עמיתים'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית-  צוות מועדוניות'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית-  צוות מרכז לגיל הרך'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- הסתגלות ילדים חדשים'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- התאמת  מסגרת לילד'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- חוסן'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- חיזוק מיומנויות'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- חינוך טיפול'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- טיפול ספירלי'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי חט"ב'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה י'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יא'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יב'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- מנהלי מרכזים לגיל הרך'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- מרכז למידה'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- סדנת העשרה'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- פעילות חוץ'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- קידום נוער'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (93, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (93, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים אמצע שנה'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים סוף שנה'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים תחילת שנה'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות גריעה'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דשבורד מחלקתי'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים  ניצנים-רשות'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב נוכחות'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב ציונים'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי נוער'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- פתיחת כיתות'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תגבורי חורף'),
    (93, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (93, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (93, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (93, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (93, N'DiscussionCode', N'דיון עם יועץ'),
    (93, N'DiscussionCode', N'דיון עם מורה'),
    (93, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (93, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (93, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (93, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (93, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (93, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (93, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (93, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (93, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (93, N'Class', N'1'),
    (93, N'Class', N'10'),
    (93, N'Class', N'11'),
    (93, N'Class', N'12'),
    (93, N'Class', N'13'),
    (93, N'Class', N'14'),
    (93, N'Class', N'15'),
    (93, N'Class', N'2'),
    (93, N'Class', N'3'),
    (93, N'Class', N'4'),
    (93, N'Class', N'5'),
    (93, N'Class', N'6'),
    (93, N'Class', N'7'),
    (93, N'Class', N'8'),
    (93, N'Class', N'9'),
    (93, N'Class', N'אין דרישות'),
    (93, N'Class', N'בניית תוכנית התנהגותית'),
    (93, N'Class', N'בניית תוכנית לימודים'),
    (93, N'Class', N'הגדלת היקף שעות'),
    (93, N'Class', N'הכנסת שינויים בביצוע'),
    (93, N'Class', N'התאמת אוכלוסית היעד'),
    (93, N'Class', N'התאמת תוכנית חברתית'),
    (93, N'Class', N'התאמת תוכנית טיפולית'),
    (93, N'Class', N'התאמת תוכנית כיתתית'),
    (93, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (93, N'Class', N'למידת עמיתים'),
    (93, N'Class', N'ניצול שעות נכון'),
    (93, N'Class', N'סיורים לימודיים'),
    (93, N'Class', N'שיחה עם הכתה/קבוצה'),
    (93, N'LocalityDistrictNational', N'איגום משאבים'),
    (93, N'LocalityDistrictNational', N'אין דרישות'),
    (93, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (93, N'LocalityDistrictNational', N'גיוס תרומות'),
    (93, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (93, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (93, N'LocalityDistrictNational', N'הנחיית הורים'),
    (93, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (93, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (93, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (93, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (93, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (93, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (93, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (93, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (93, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (93, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (93, N'GradeLevel', N'א'),
    (93, N'GradeLevel', N'ב'),
    (93, N'GradeLevel', N'ג'),
    (93, N'GradeLevel', N'ד'),
    (93, N'GradeLevel', N'ה'),
    (93, N'GradeLevel', N'ו'),
    (93, N'GradeLevel', N'ז'),
    (93, N'GradeLevel', N'ח'),
    (93, N'GradeLevel', N'חובה'),
    (93, N'GradeLevel', N'ט'),
    (93, N'GradeLevel', N'י'),
    (93, N'GradeLevel', N'יא'),
    (93, N'GradeLevel', N'יב'),
    (95, N'EducationalProgram', N'אור בגנים'),
    (95, N'EducationalProgram', N'כיתות א"מץ'),
    (95, N'EducationalProgram', N'כיתות אתגר'),
    (95, N'EducationalProgram', N'כיתות במרכזי חינוך ונוער'),
    (95, N'EducationalProgram', N'כיתות בתי"ס במעבר'),
    (95, N'EducationalProgram', N'כיתות מב"ר'),
    (95, N'EducationalProgram', N'כיתות מיזם'),
    (95, N'EducationalProgram', N'כיתות מל"א'),
    (95, N'EducationalProgram', N'כיתות מפתנים'),
    (95, N'EducationalProgram', N'כיתות שח"ר'),
    (95, N'EducationalProgram', N'כיתות תל"ם'),
    (95, N'EducationalProgram', N'כנפי רוח'),
    (95, N'EducationalProgram', N'כתות בתי"ס ייחודיים'),
    (95, N'EducationalProgram', N'מועדוניות משפחתיות'),
    (95, N'EducationalProgram', N'מרכזי חירום'),
    (95, N'EducationalProgram', N'עוגנים יישוביים-רווחה ושיקום'),
    (95, N'EducationalProgram', N'פדגוגיה טיפולית'),
    (95, N'EducationalProgram', N'פותחים עתיד'),
    (95, N'EducationalProgram', N'תגבורי חורף'),
    (95, N'EducationalProgram', N'תוכנית אמ"ת'),
    (95, N'EducationalProgram', N'תוכנית הילה'),
    (95, N'EducationalProgram', N'תוכנית חנוך לנער'),
    (95, N'EducationalProgram', N'תוכנית מל"א - יסודי'),
    (95, N'EducationalProgram', N'תוכנית מלא ליסודיים- נקודת אור'),
    (95, N'EducationalProgram', N'תוכנית משיבים'),
    (95, N'EducationalProgram', N'תל"ם-נחשון'),
    (95, N'Domain', N'מוסדי'),
    (95, N'Domain', N'מניעת נשירה'),
    (95, N'Domain', N'מסגרות ייחודיות'),
    (95, N'Domain', N'רווחה וקהילה'),
    (95, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (95, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (95, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי התוכנית- איתור כח אדם'),
    (95, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (95, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (95, N'Subject', N'בניית תוכנית הנחייה- עבור מרכז נוער'),
    (95, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (95, N'Subject', N'בניית תוכנית הנחייה- שכבה ט'),
    (95, N'Subject', N'בניית תוכנית הנחייה- שכבה י'),
    (95, N'Subject', N'בניית תוכנית הנחייה- שכבה יא'),
    (95, N'Subject', N'בניית תוכנית הנחייה- שכבה יב'),
    (95, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (95, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (95, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (95, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (95, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (95, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (95, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (95, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (95, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (95, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (95, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (95, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (95, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (95, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (95, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (95, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים-  גורמי רווחה'),
    (95, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- מנהל מרכז נוער'),
    (95, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- רשות'),
    (95, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (95, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (95, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (95, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (95, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (95, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (95, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (95, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (95, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (95, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (95, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (95, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (95, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (95, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (95, N'Subject', N'השתתפות בהשתלמות מנחי מרכזי נוער'),
    (95, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (95, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה- סטטוס תקציב'),
    (95, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (95, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (95, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (95, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (95, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (95, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (95, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (95, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (95, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (95, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (95, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (95, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (95, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (95, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (95, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (95, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (95, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (95, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (95, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (95, N'Subject', N'מפגש הנחייה אישית - טיפול ספירלי'),
    (95, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (95, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (95, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (95, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (95, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (95, N'Subject', N'מפגש הנחייה אישית -מנהל מוסד'),
    (95, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (95, N'Subject', N'מפגש הנחייה אישית -נוכחות  והיעדרויות'),
    (95, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (95, N'Subject', N'מפגש הנחייה אישית -רכז פדגוגי'),
    (95, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (95, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (95, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (95, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (95, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (95, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (95, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (95, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (95, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (95, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (95, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- מניעת נשירה'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (95, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית-  בתחום מניעת אלימות'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- חוסן'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- חיזוק מיומנויות'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- חינוך טיפול'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- טיפול ספירלי'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי חט"ב'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה י'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יא'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יב'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- מרכז למידה'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- סדנת העשרה'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- פעילות חוץ'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- קידום נוער'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (95, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (95, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים אמצע שנה'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים סוף שנה'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים תחילת שנה'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות גריעה'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דשבורד מחלקתי'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- בקרה ופיקוח'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כיתות מיזם'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב נוכחות'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב ציונים'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי נוער'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- פתיחת כיתות'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תגבורי חורף'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב מחוזות'),
    (95, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב תוכניות'),
    (95, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (95, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (95, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (95, N'DiscussionCode', N'דיון עם יועץ'),
    (95, N'DiscussionCode', N'דיון עם מורה'),
    (95, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (95, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (95, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (95, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (95, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (95, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (95, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (95, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (95, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (95, N'Class', N'1'),
    (95, N'Class', N'10'),
    (95, N'Class', N'11'),
    (95, N'Class', N'12'),
    (95, N'Class', N'13'),
    (95, N'Class', N'14'),
    (95, N'Class', N'15'),
    (95, N'Class', N'2'),
    (95, N'Class', N'3'),
    (95, N'Class', N'4'),
    (95, N'Class', N'5'),
    (95, N'Class', N'6'),
    (95, N'Class', N'7'),
    (95, N'Class', N'8'),
    (95, N'Class', N'9'),
    (95, N'Class', N'אין דרישות'),
    (95, N'Class', N'בניית תוכנית התנהגותית'),
    (95, N'Class', N'בניית תוכנית לימודים'),
    (95, N'Class', N'הגדלת היקף שעות'),
    (95, N'Class', N'הכנסת שינויים בביצוע'),
    (95, N'Class', N'התאמת אוכלוסית היעד'),
    (95, N'Class', N'התאמת תוכנית חברתית'),
    (95, N'Class', N'התאמת תוכנית טיפולית'),
    (95, N'Class', N'התאמת תוכנית כיתתית'),
    (95, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (95, N'Class', N'למידת עמיתים'),
    (95, N'Class', N'ניצול שעות נכון'),
    (95, N'Class', N'סיורים לימודיים'),
    (95, N'Class', N'שיחה עם הכתה/קבוצה'),
    (95, N'LocalityDistrictNational', N'איגום משאבים'),
    (95, N'LocalityDistrictNational', N'אין דרישות'),
    (95, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (95, N'LocalityDistrictNational', N'גיוס תרומות'),
    (95, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (95, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (95, N'LocalityDistrictNational', N'הנחיית הורים'),
    (95, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (95, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (95, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (95, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (95, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (95, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (95, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (95, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (95, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (95, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (95, N'GradeLevel', N'א'),
    (95, N'GradeLevel', N'ב'),
    (95, N'GradeLevel', N'ג'),
    (95, N'GradeLevel', N'ד'),
    (95, N'GradeLevel', N'ה'),
    (95, N'GradeLevel', N'ו'),
    (95, N'GradeLevel', N'ז'),
    (95, N'GradeLevel', N'ח'),
    (95, N'GradeLevel', N'חובה'),
    (95, N'GradeLevel', N'ט'),
    (95, N'GradeLevel', N'י'),
    (95, N'GradeLevel', N'יא'),
    (95, N'GradeLevel', N'יב'),
    (100, N'Framework', N'אלעד 442087 כנסת יחזקאל'),
    (100, N'Framework', N'אלעד 715797 שערי תבונה'),
    (100, N'Framework', N'אלעד 761379 תורת חיים'),
    (100, N'Framework', N'אלעד, 540708, באר אברהם'),
    (100, N'Framework', N'אלעד, 715797, שערי תבונה'),
    (100, N'Framework', N'אלעד, 722132, תורה בתפארתה'),
    (100, N'Framework', N'אלעד, נהורא דאורייתא 361550'),
    (100, N'Framework', N'אשדוד, 641225, דובר שלום'),
    (100, N'Framework', N'בית חלקיה, 672568, שערי שמועות'),
    (100, N'Framework', N'בית שמש - תורת זאב, 338277'),
    (100, N'Framework', N'בית שמש 141481 באר התורה'),
    (100, N'Framework', N'בית שמש 366864 נתיבות חיים'),
    (100, N'Framework', N'בית שמש מבקשי תורה 580528032'),
    (100, N'Framework', N'בית שמש,39491, בית אליהו'),
    (100, N'Framework', N'ביתר עילית,632216, משנתו שלימה'),
    (100, N'Framework', N'ביתר עלית, 657379 ישיבה גדולה בעלזא'),
    (100, N'Framework', N'ביתר, 747337, ישיבת קרלין'),
    (100, N'Framework', N'בני ברק - אורחות תורה, 541748'),
    (100, N'Framework', N'בני ברק , 42516, תורת דוד'),
    (100, N'Framework', N'בני ברק ,540526 נחלת דן'),
    (100, N'Framework', N'בני ברק 540526 בית מדרש עליון'),
    (100, N'Framework', N'בני ברק 544379 קרית מלך-תפארת ציון'),
    (100, N'Framework', N'בני ברק נדבורנא, 541128'),
    (100, N'Framework', N'בני ברק ק.הרצוג  580338366 אור אליצור'),
    (100, N'Framework', N'בני ברק,  541854, חזון נחום'),
    (100, N'Framework', N'בני ברק, 10541201, חזון נחום'),
    (100, N'Framework', N'בני ברק, 361451, תורת אהרון'),
    (100, N'Framework', N'בני ברק, 540963, אמרי אמת'),
    (100, N'Framework', N'בני ברק, 541056, בית מאיר'),
    (100, N'Framework', N'בני ברק, 541102, אהל יוסף'),
    (100, N'Framework', N'בני ברק, 541151 , ישיבת ויזניץ'),
    (100, N'Framework', N'בני ברק, 541185, אמרי משה'),
    (100, N'Framework', N'בני ברק, 541284, גאון צבי'),
    (100, N'Framework', N'בני ברק, 541631, ישיבה חסידי דאראג'),
    (100, N'Framework', N'בני ברק, 541854, חזון נחום - טל תורה'),
    (100, N'Framework', N'בני ברק, 541896, פונוביז'''),
    (100, N'Framework', N'בני ברק, 544247, בית יוסף'),
    (100, N'Framework', N'בני ברק, 55120, בית ישראל'),
    (100, N'Framework', N'בני ברק, 580085447, ברכת אפרים'),
    (100, N'Framework', N'בני ברק, 648410, מוסדות בית נחמיה'),
    (100, N'Framework', N'בני ברק, 657379 ישיבה גדולה בעלזא'),
    (100, N'Framework', N'בני ברק,544239, ישראל'),
    (100, N'Framework', N'גבעת זאב, 675934, ארחות יעקב'),
    (100, N'Framework', N'חיפה, 346031 , יחל ישראל'),
    (100, N'Framework', N'חמד, 441774, מאור יצחק חמד'),
    (100, N'Framework', N'ירושלים 140814 אור אלחנן'),
    (100, N'Framework', N'ירושלים 140921 בית אברהם סלונים'),
    (100, N'Framework', N'ירושלים 141572 משכן ציון'),
    (100, N'Framework', N'ירושלים 160366 באר יהודה'),
    (100, N'Framework', N'ירושלים 346098 אש התלמוד'),
    (100, N'Framework', N'ירושלים 366880 ברכת ישראל'),
    (100, N'Framework', N'ירושלים 5802944379 תורת אברהם'),
    (100, N'Framework', N'ירושלים 633263 פני מנחם לעמלי תורה'),
    (100, N'Framework', N'ירושלים 758193 דעת אהרון'),
    (100, N'Framework', N'ירושלים אוהל יוסף, 580432375'),
    (100, N'Framework', N'ירושלים, 140541, עטרת ישראל'),
    (100, N'Framework', N'ירושלים, 140673, ישיבת חסידי בעלזא'),
    (100, N'Framework', N'ירושלים, 140780, כוכב יעקב'),
    (100, N'Framework', N'ירושלים, 140798, אהל שמעון ערלוי'),
    (100, N'Framework', N'ירושלים, 141044, קול תורה'),
    (100, N'Framework', N'ירושלים, 184093, פורת יוסף'),
    (100, N'Framework', N'ירושלים, 27056, קול יעקב'),
    (100, N'Framework', N'ירושלים, 390590, לב אליהו'),
    (100, N'Framework', N'ירושלים, 53196, אהבת תורה'),
    (100, N'Framework', N'ירושלים, 580026383, אוהל תורה'),
    (100, N'Framework', N'ירושלים, 580319489, דעת חיים'),
    (100, N'Framework', N'ירושלים, 647206, בית עזריאל'),
    (100, N'Framework', N'ירושלים, 722025, עמלה של תורה'),
    (100, N'Framework', N'ירושלים, 732081 נחלי התורה'),
    (100, N'Framework', N'ירושלים, 745968, היכל יצחק'),
    (100, N'Framework', N'ירושלים, 747584, בית יצחק קמניץ'),
    (100, N'Framework', N'ירושלים, עמלה של תורה, 722025'),
    (100, N'Framework', N'ירושלים, פינקל - אוצר התורה, 711556'),
    (100, N'Framework', N'כרמיאל, 460162, רינה של תורה'),
    (100, N'Framework', N'מודיעין עילית 160523 מיר ברכפלד'),
    (100, N'Framework', N'מודיעין עילית 363879 כנסת יצחק'),
    (100, N'Framework', N'מודיעין עילית, 234047, ויזניץ'),
    (100, N'Framework', N'מודיעין עילית, 738575, תפארת ישראל'),
    (100, N'Framework', N'מודיעין עלית, 738575, תפארת ישראל'),
    (100, N'Framework', N'מודיעין עלית,676361, נחלת בנימין'),
    (100, N'Framework', N'מוסדות ביאלא חלקת יהושע 520317'),
    (100, N'Framework', N'נצרת, 580726313, נוף הגליל'),
    (100, N'Framework', N'נתיבות, 140681 באר התלמוד'),
    (100, N'Framework', N'נתיבות, 770719, שכר שכיר'),
    (100, N'Framework', N'נתניה, 440768, דברי חיים'),
    (100, N'Framework', N'עוצם, 541748, ישיבת נר זרח'),
    (100, N'Framework', N'פתח תקווה, 440800, אור ישראל'),
    (100, N'Framework', N'קרית יערים טלזסטון 580342921 ישיבת באר יצחק'),
    (100, N'Framework', N'ראש העין, 361550, דאורייתא'),
    (100, N'Framework', N'ראשון לציון, 722058, עטרת שלמה'),
    (100, N'Framework', N'רחובות, 444604, מאור התלמוד'),
    (100, N'EducationalProgram', N'תוכנית משיבים'),
    (100, N'Domain', N'מניעת נשירה'),
    (100, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (100, N'Subject', N'בנית תוכנית הנחייה- הוראת גמרא'),
    (100, N'Subject', N'בנית תוכנית הנחייה- התבגרות וחוסן'),
    (100, N'Subject', N'בנית תוכנית הנחייה- זיהוי משברים מודל וכלים לטיפול'),
    (100, N'Subject', N'בנית תוכנית הנחייה- תקשורת אמון וגבולות'),
    (100, N'Subject', N'בנית תוכנית הנחייה-שיעור א'),
    (100, N'Subject', N'בנית תוכנית הנחייה-שיעור ב'),
    (100, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (100, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (100, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- חינוך טיפול'),
    (100, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- טיפול רגשי'),
    (100, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- יועץ חינוכי'),
    (100, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- עו"ס'),
    (100, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים-פעיל ארגון'),
    (100, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום הוראת גמרא - אתגרים'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום הוראת גמרא - שיטות הוראה'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום חינוכית-טיפולית - התבגרות וחוסן'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום חינוכית-טיפולית - כלים לזיהוי משברים'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום חינוכית-טיפולית - מודלים לבניית חוסן ושימור מוטיבציה'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום פדגוגיה טיפולית - כלים ליצירת דיאלוג טיפולי חינוכי'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום פדגוגיה טיפולית - מודל עבודה רפלקטיבי'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום פדגוגיה טיפולית - עקרונות מרכזיים'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום תמיכה רגשית קוגנטיבית - כלים וטיפול'),
    (100, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום תמיכה רגשית קוגנטיבית- תקשורת אמון וגבולות'),
    (100, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (100, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (100, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (100, N'Subject', N'מפגש הנחייה אישית - טיפול באתגרים חברתיים'),
    (100, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (100, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (100, N'Subject', N'מפגש הנחייה אישית - סיוע למניעת נשירה'),
    (100, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (100, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (100, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (100, N'Subject', N'מפגש הנחייה אישית -התבגרות וחוסן'),
    (100, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (100, N'Subject', N'מפגש הנחייה אישית -תקשורת אמון וגבולות'),
    (100, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (100, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (100, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (100, N'Subject', N'קיום דיאלוג עם רכז  התכנית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (100, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר / השתתפות באסיפת צוות'),
    (100, N'Subject', N'קיום ישיבה פדגוגית- ראש הישיבה'),
    (100, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה- מסירת ועד/חבורה'),
    (100, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (100, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (100, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (100, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (100, N'DiscussionCode', N'דיון עם יועץ'),
    (100, N'DiscussionCode', N'דיון עם מורה/ מגיד שיעור'),
    (100, N'DiscussionCode', N'דיון עם מנהל בית הספר- ראש הישיבה'),
    (100, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (100, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (100, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (100, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (100, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (100, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (100, N'DiscussionCode', N'דיון עם צוות עמיתים- אנשי מקצוע'),
    (100, N'DiscussionCode', N'דיון עם רכז התוכנית בישיבה'),
    (100, N'Class', N'1'),
    (100, N'Class', N'10'),
    (100, N'Class', N'11'),
    (100, N'Class', N'12'),
    (100, N'Class', N'13'),
    (100, N'Class', N'14'),
    (100, N'Class', N'15'),
    (100, N'Class', N'2'),
    (100, N'Class', N'3'),
    (100, N'Class', N'4'),
    (100, N'Class', N'5'),
    (100, N'Class', N'6'),
    (100, N'Class', N'7'),
    (100, N'Class', N'8'),
    (100, N'Class', N'9'),
    (100, N'Class', N'אין דרישות'),
    (100, N'Class', N'בניית תוכנית התנהגותית'),
    (100, N'Class', N'בניית תוכנית לימודים'),
    (100, N'Class', N'הגדלת היקף שעות'),
    (100, N'Class', N'הכנסת שינויים בביצוע'),
    (100, N'Class', N'התאמת אוכלוסית היעד'),
    (100, N'Class', N'התאמת תוכנית חברתית'),
    (100, N'Class', N'התאמת תוכנית טיפולית'),
    (100, N'Class', N'התאמת תוכנית כיתתית'),
    (100, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (100, N'Class', N'למידת עמיתים'),
    (100, N'Class', N'ניצול שעות נכון'),
    (100, N'Class', N'סיורים לימודיים'),
    (100, N'Class', N'שיחה עם הכתה/קבוצה'),
    (100, N'LocalityDistrictNational', N'איגום משאבים'),
    (100, N'LocalityDistrictNational', N'אין דרישות'),
    (100, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (100, N'LocalityDistrictNational', N'גיוס תרומות'),
    (100, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (100, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (100, N'LocalityDistrictNational', N'הנחיית הורים'),
    (100, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (100, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (100, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (100, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (100, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (100, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (100, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (100, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (100, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (100, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (100, N'GradeLevel', N'א'),
    (100, N'GradeLevel', N'ב'),
    (100, N'GradeLevel', N'ג'),
    (100, N'GradeLevel', N'ד'),
    (100, N'GradeLevel', N'ה'),
    (100, N'GradeLevel', N'ו'),
    (100, N'GradeLevel', N'ז'),
    (100, N'GradeLevel', N'ח'),
    (100, N'GradeLevel', N'חובה'),
    (100, N'GradeLevel', N'ט'),
    (100, N'GradeLevel', N'י'),
    (100, N'GradeLevel', N'יא'),
    (100, N'GradeLevel', N'יב'),
    (100, N'GradeLevel', N'שיעור א'),
    (100, N'GradeLevel', N'שיעור ב'),
    (96, N'Framework', N'אום אל פאחם - מרכזים לגיל הרך'),
    (96, N'Framework', N'אופקים מרכזים לגיל הרך'),
    (96, N'Framework', N'אכסאל מרכזים לגיל הרך'),
    (96, N'Framework', N'אלעד מרכזים לגיל הרך'),
    (96, N'Framework', N'אשכול מרכזים לגיל הרך'),
    (96, N'Framework', N'באר יעקב  מרכזים לגיל הרך'),
    (96, N'Framework', N'באר שבע- מרכזים לגיל הרך'),
    (96, N'Framework', N'בועינה נג''ידאת  מרכזים לגיל הרך'),
    (96, N'Framework', N'בוקעתא מרכזים לגיל הרך'),
    (96, N'Framework', N'ביר אלמכסור מרכזים לגיל הרך'),
    (96, N'Framework', N'בית ג׳אן מרכזים לגיל הרך'),
    (96, N'Framework', N'בית שמש- מרכז לגיל הרך'),
    (96, N'Framework', N'ביתר עלית- מרכז לגיל הרך'),
    (96, N'Framework', N'בני ברק מרכזים לגיל הרך'),
    (96, N'Framework', N'בסמת טבעון מרכזים לגיל הרך'),
    (96, N'Framework', N'בת ים מרכזים לגיל הרך'),
    (96, N'Framework', N'ג''וליס מרכזים לגיל הרך'),
    (96, N'Framework', N'ג''לג''וליה מרכזים לגיל הרך'),
    (96, N'Framework', N'גסר א זרקא מרכזים לגיל הרך'),
    (96, N'Framework', N'דימונה מרכזים לגיל הרך'),
    (96, N'Framework', N'דלית אל כרמל מרכזים לגיל הרך'),
    (96, N'Framework', N'הנחייה והטמעה ארצית- מרכזים לגיל הרך'),
    (96, N'Framework', N'הנחייה והטמעה מוסדית- מרכזים לגיך הרך'),
    (96, N'Framework', N'הנחייה והטמעה מחוזית- מרכזים לגיל הרך'),
    (96, N'Framework', N'הנחייה קורס מנהלות חדשות - מרכזים לגיל הרך'),
    (96, N'Framework', N'הנחייה, הטמעה ופיתוח- תוכנית סופוויזן'),
    (96, N'Framework', N'זימר  מרכזים לגיל הרך'),
    (96, N'Framework', N'זרזיר מרכזים לגיל הרך'),
    (96, N'Framework', N'חולון מרכזים לגיל הרך'),
    (96, N'Framework', N'חוף אשקלון מרכזים לגיל הרך'),
    (96, N'Framework', N'חורפיש מרכזים לגיל הרך'),
    (96, N'Framework', N'חיפה מרכזים לגיל הרך'),
    (96, N'Framework', N'חצור מרכזים לגיל הרך'),
    (96, N'Framework', N'טבריה מרכזים לגיל הרך'),
    (96, N'Framework', N'טובא זנגריה מרכזים לגיל הרך'),
    (96, N'Framework', N'ינוח ג''ת  מרכזים לגיל הרך'),
    (96, N'Framework', N'יפיע מרכזים לגיל הרך'),
    (96, N'Framework', N'ירוחם- מרכזים לגיל הרך'),
    (96, N'Framework', N'ירושלים בית וגן -מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים גוננים- מרכזים לגיל הרך'),
    (96, N'Framework', N'ירושלים גילה- מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים- הר חומה- מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים- נווה יעקב- מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים פסגת זאב- מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים קריית יובל- מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים קריית מנחם- מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים- רוממה- מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים- רמות- מרכז לגיל הרך'),
    (96, N'Framework', N'ירושלים- שמואל הנביא- מרכז לגיל הרך'),
    (96, N'Framework', N'ירכא מרכזים לגיל הרך'),
    (96, N'Framework', N'כסרא סמיע מרכזים לגיל הרך'),
    (96, N'Framework', N'כעבייה מרכזים לגיל הרך'),
    (96, N'Framework', N'כפר כמא מרכזים לגיל הרך'),
    (96, N'Framework', N'להבים מרכזים לגיל הרך'),
    (96, N'Framework', N'מג''אר מרכזים לגיל הרך'),
    (96, N'Framework', N'מגדל העמק מרכזים לגיל הרך'),
    (96, N'Framework', N'מגדל שמס מרכזים לגיל הרך'),
    (96, N'Framework', N'מזרעה מרכזים לגיל הרך'),
    (96, N'Framework', N'מטה בנימין- מרכז לגיל הרך'),
    (96, N'Framework', N'מעלה אדומים- מרכז לגיל הרך'),
    (96, N'Framework', N'מעלות תרשיחא מרכזים לגיל הרך'),
    (96, N'Framework', N'מצפה רמון- מרכזים לגיל הרך'),
    (96, N'Framework', N'מרום הגליל מרכזים לגיל הרך'),
    (96, N'Framework', N'מרחבים מרכזים לגיל הרך'),
    (96, N'Framework', N'נהריה מרכזים לגיל הרך'),
    (96, N'Framework', N'נווה מדבר  מרכזים לגיל הרך'),
    (96, N'Framework', N'נתיבות מרכזים לגיל הרך'),
    (96, N'Framework', N'סאגור מרכזים לגיל הרך'),
    (96, N'Framework', N'סחנין מרכזים לגיל הרך'),
    (96, N'Framework', N'עוספייא מרכזים לגיל הרך'),
    (96, N'Framework', N'עין מאהל  מרכזים לגיל הרך'),
    (96, N'Framework', N'עראבה מרכזים לגיל הרך'),
    (96, N'Framework', N'ערד-מרכזים לגיל הרך'),
    (96, N'Framework', N'פקיעין מרכזים לגיל הרך'),
    (96, N'Framework', N'פתח תקווה מרכזים לגיל הרך'),
    (96, N'Framework', N'צפת מרכזים לגיל הרך'),
    (96, N'Framework', N'קלאנסווה מרכזים לגיל הרך'),
    (96, N'Framework', N'קרית ארבע- מרכזים לגיל הרך'),
    (96, N'Framework', N'קרית גת -כרמי גת מרכזים לגיל הרך'),
    (96, N'Framework', N'קרית מלאכי מרכזים לגיל הרך'),
    (96, N'Framework', N'קרית שמונה מרכזים לגיל הרך'),
    (96, N'Framework', N'ראמה מרכזים לגיל הרך'),
    (96, N'Framework', N'רהט מרכזים לגיל הרך'),
    (96, N'Framework', N'ריחניה מרכזים לגיל הרך'),
    (96, N'Framework', N'רמלה- מרכז לגיל הרך'),
    (96, N'Framework', N'שבלי אום אלגנם מרכזים לגיל הרך'),
    (96, N'Framework', N'שגב שלום מרכזים לגיל הרך'),
    (96, N'Framework', N'שדות נגב - מרכזים לגיל הרך'),
    (96, N'Framework', N'שדרות מרכזים לגיל הרך'),
    (96, N'Framework', N'שועפאט- מרכז לגיל הרך'),
    (96, N'Framework', N'שלומי מרכזים לגיל הרך'),
    (96, N'Framework', N'שעב מרכזים לגיל הרך'),
    (96, N'Framework', N'שפיר מרכזים לגיל הרך'),
    (96, N'Framework', N'שפרעם מרכזים לגיל הרך'),
    (96, N'EducationalProgram', N'מרכזים לגיל הרך'),
    (96, N'Domain', N'מסגרות ייחודיות'),
    (96, N'Domain', N'רווחה וקהילה'),
    (96, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי התוכנית- איתור כח אדם'),
    (96, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי מרכזים לגיל הרך'),
    (96, N'Subject', N'בניית תוכנית הנחייה- בתחום הטיפולי רגשי פארא-רפואי'),
    (96, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (96, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (96, N'Subject', N'בניית תוכנית הנחייה- מרכזים לגיל הרך'),
    (96, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (96, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (96, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (96, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי  תלמידי המרכזים לגיל הרך'),
    (96, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (96, N'Subject', N'בנית פלטפורמה לשיתופי פעולה- התוכנית הלאומית 360'),
    (96, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (96, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (96, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (96, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (96, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (96, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (96, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (96, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (96, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (96, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (96, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (96, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (96, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (96, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (96, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת- מרכז לגיל הרך'),
    (96, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (96, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (96, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (96, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (96, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה-וועדות היגוי'),
    (96, N'Subject', N'השתתפות במפגש וועדת היגוי ברשות המקומית'),
    (96, N'Subject', N'השתתפות במפגש מנחים מרכזים לגיל הרך אזוריים וארציים'),
    (96, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (96, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי מרכזים לגיל הרך'),
    (96, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (96, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (96, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (96, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (96, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (96, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (96, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (96, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (96, N'Subject', N'מפגש הנחיה אישית  - איגום משאבים'),
    (96, N'Subject', N'מפגש הנחיה אישית  - מנהל/ת מרכז לגיל הרך'),
    (96, N'Subject', N'מפגש הנחיה אישית  - ניהול משאבים ושימור הון אנושי'),
    (96, N'Subject', N'מפגש הנחיה אישית  - ניהול משאבים תקציבים מרכז לגיל הרך'),
    (96, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (96, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (96, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (96, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (96, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (96, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (96, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (96, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (96, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (96, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (96, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (96, N'Subject', N'קביעת תוכנית עבודה חודשית- מרכזים לגיל הרך'),
    (96, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (96, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (96, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (96, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- בשיתוף מנהלת 360 תוכנית הלאומית'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- צוות פארא רפואי'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (96, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן');
    INSERT INTO @ScopeSeed (ProgramId, ScopeType, Description) VALUES
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית-  למידת עמיתים'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית-  ניהול משאבים ושימור ההון האנושי'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית-  ניהול משאבים תקציבים'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית-  צוות מרכז לגיל הרך'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית- התאמת  מסגרת טיפולית לילד'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית- מנהלי מרכזים לגיל הרך'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (96, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מצבת כוח אדם'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים תקציבים'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משאבים שונים'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב ובקרה מרכז לגיל הרך'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (96, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תיאורי מקרה בוחן'),
    (96, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (96, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (96, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (96, N'DiscussionCode', N'דיון עם יועץ'),
    (96, N'DiscussionCode', N'דיון עם מורה'),
    (96, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (96, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (96, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (96, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (96, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (96, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (96, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (96, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (96, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (96, N'Class', N'1'),
    (96, N'Class', N'10'),
    (96, N'Class', N'11'),
    (96, N'Class', N'12'),
    (96, N'Class', N'13'),
    (96, N'Class', N'14'),
    (96, N'Class', N'15'),
    (96, N'Class', N'2'),
    (96, N'Class', N'3'),
    (96, N'Class', N'4'),
    (96, N'Class', N'5'),
    (96, N'Class', N'6'),
    (96, N'Class', N'7'),
    (96, N'Class', N'8'),
    (96, N'Class', N'9'),
    (96, N'Class', N'אין דרישות'),
    (96, N'Class', N'בניית תוכנית התנהגותית'),
    (96, N'Class', N'בניית תוכנית לימודים'),
    (96, N'Class', N'הגדלת היקף שעות'),
    (96, N'Class', N'הכנסת שינויים בביצוע'),
    (96, N'Class', N'התאמת אוכלוסית היעד'),
    (96, N'Class', N'התאמת תוכנית חברתית'),
    (96, N'Class', N'התאמת תוכנית טיפולית'),
    (96, N'Class', N'התאמת תוכנית כיתתית'),
    (96, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (96, N'Class', N'למידת עמיתים'),
    (96, N'Class', N'ניצול שעות נכון'),
    (96, N'Class', N'סיורים לימודיים'),
    (96, N'Class', N'שיחה עם הכתה/קבוצה'),
    (96, N'LocalityDistrictNational', N'איגום משאבים'),
    (96, N'LocalityDistrictNational', N'אין דרישות'),
    (96, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (96, N'LocalityDistrictNational', N'גיוס תרומות'),
    (96, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (96, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (96, N'LocalityDistrictNational', N'הנחיית הורים'),
    (96, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (96, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (96, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (96, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (96, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (96, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (96, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (96, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (96, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (96, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (96, N'GradeLevel', N'א'),
    (96, N'GradeLevel', N'ב'),
    (96, N'GradeLevel', N'ג'),
    (96, N'GradeLevel', N'ד'),
    (96, N'GradeLevel', N'ה'),
    (96, N'GradeLevel', N'ו'),
    (96, N'GradeLevel', N'ז'),
    (96, N'GradeLevel', N'ח'),
    (96, N'GradeLevel', N'חובה'),
    (96, N'GradeLevel', N'ט'),
    (96, N'GradeLevel', N'י'),
    (96, N'GradeLevel', N'יא'),
    (96, N'GradeLevel', N'יב'),
    (97, N'Framework', N'אבו גוש תיכון 148080 אבו גוש מקיף אבו גוש'),
    (97, N'Framework', N'אום אל פאחם  חט"ב 347047 חט"ב אלראזי אום אל-פחם'),
    (97, N'Framework', N'אום אל פאחם  חט"ב 348235 חט"ב אל גזאלי אום אל-פחם'),
    (97, N'Framework', N'אום אל פאחם  חט"ב 348243 חט"ב ואדי אלנסור אום אל-פחם'),
    (97, N'Framework', N'אום אל פאחם  תיכון 342337 חט"ב אסכנדר אום אל-פחם'),
    (97, N'Framework', N'אכסאל תיכון 248112 תיכון אכסאל אכסאל 248112'),
    (97, N'Framework', N'אעבלין תיכון 247239 מקיף אעבלין אעבלין'),
    (97, N'Framework', N'גלבוע  חט"ב  540617 חט"ב מוקייבלה הגלבוע'),
    (97, N'Framework', N'ג''לג''וליה  חט"ב 448050 חט"ב מדעי ג''לג''וליה'),
    (97, N'Framework', N'ג''לג''וליה  חט"ב 448316 אל ראזי ג''לג''וליה'),
    (97, N'Framework', N'דבוריה  תיכון 800128 בית הספר הרב תחומי עמל דבוריה דבוריה'),
    (97, N'Framework', N'חורה תיכון 648337 אלסאלם חורה'),
    (97, N'Framework', N'חיפה תיכון 378075 תיכון שיזאף חיפה'),
    (97, N'Framework', N'טורעאן  חט"ב 247155 חט"ב ע"ש דר'' ג. חורי טורעאן'),
    (97, N'Framework', N'טורעאן תיכון 248138 תיכון טורעאן'),
    (97, N'Framework', N'טייבה חט"ב 448134 מקיף עתיד אלנגאח למדעים טייבה'),
    (97, N'Framework', N'טייבה חט"ב 448209 אל סאלאם טייבה'),
    (97, N'Framework', N'טייבה תיכון 448019 אל מאג''ד -עתיד טייבה'),
    (97, N'Framework', N'טייבה תיכון 478016 אל אחווה טייבה רב תחומי עמל'),
    (97, N'Framework', N'טירה  חט"ב 442566 חט"ב ג'' -עבד אלראוף סמארה'),
    (97, N'Framework', N'טירה  חט"ב 448118 חט"ב א'' טירה'),
    (97, N'Framework', N'טירה  חט"ב 448183 חט"ב ב'' טירה'),
    (97, N'Framework', N'טמרה  חט"ב 249169 חט"ב אלפראבי טמרה 249169'),
    (97, N'Framework', N'יפו  תיכון 548016 עירוני י"ב יפו'),
    (97, N'Framework', N'יפו  תיכון 573105 אל מוסתקבל יפו'),
    (97, N'Framework', N'כסייפה  חט"ב 610006 אורט אלמנתבי  כסייפה'),
    (97, N'Framework', N'כסייפה  תיכון 800037 אורט אבו רביעה'),
    (97, N'Framework', N'כפר ברא חט"ב 448340 בית חטב אלנהדה'),
    (97, N'Framework', N'כפר יאסיף  תיכון 248013 מקיף ע"ש יני כפר יאסיף'),
    (97, N'Framework', N'כפר כנא  תיכון 800094 תיכון כפר כנא'),
    (97, N'Framework', N'כפר מנדא חט"ב  248765 חטב ב'' כפר מנדא'),
    (97, N'Framework', N'כפר קאסם חט"ב 448167  חט"ב  אבן סינא כפר קאסם'),
    (97, N'Framework', N'לקיה תיכון 648261 אקרא לקיה 648261'),
    (97, N'Framework', N'מטה אשר חט"ב 247221 בי"ס מקיף השלום'),
    (97, N'Framework', N'נווה מדבר תיכון 660233 תיכון אבו תלול נווה מדבר'),
    (97, N'Framework', N'נחף  תיכון 248641 מקיף איבן סינא נחף'),
    (97, N'Framework', N'נצרת  חט"ב  338657 אלחכמה נצרת'),
    (97, N'Framework', N'נצרת  חט"ב 248146 חט"ב ע"ש תאופיק זיאד נצרת'),
    (97, N'Framework', N'נצרת  תיכון 247064 טרה סנטה נצרת'),
    (97, N'Framework', N'נצרת  תיכון 338657 אלחכמה נצרת'),
    (97, N'Framework', N'נצרת תיכון 472332 בי"ס נזירות סליזיאן נצרת'),
    (97, N'Framework', N'סח''נין  תיכון 800052 תיכון עש ג''מאל טרביה סח''נין'),
    (97, N'Framework', N'סעווה תיכון 648345 מקיף טומשין מולדה'),
    (97, N'Framework', N'עין מאהל תיכון 800078  תיכון עין מאהל'),
    (97, N'Framework', N'עין ראפה  תיכון 442822 עין ראפה עין נקובה  עין ראפה'),
    (97, N'Framework', N'עראבה  תיכון 247247 מקיף אלבוכארי עראבה'),
    (97, N'Framework', N'עראבה  תיכון 248575 מקיף אבן ח''לדון עראבה'),
    (97, N'Framework', N'עראבה  תיכון 249284 מקיף אלבטוף - עראבה'),
    (97, N'Framework', N'ערערה חט"ב 348060  חט"ב ערערה'),
    (97, N'Framework', N'ערערה תיכון 800102 תיכון ערערה  ערערה'),
    (97, N'Framework', N'פוריידס חט"ב 348227 חט"ב מקיף פרדיס פוריידיס'),
    (97, N'Framework', N'ראמה תיכון 248047 מקיף ראמה'),
    (97, N'Framework', N'רהט חט"ב 640797 מקיף אבו ראשד רהט'),
    (97, N'Framework', N'שגב שלום  תיכון 648303 מקיף אלסאלם שגב שלום'),
    (97, N'Framework', N'שפרעם תיכון 248070 עירוני מקיף שפרעם'),
    (97, N'Framework', N'שפרעם תיכון 248344 מקיף חט"ב ג'''),
    (97, N'EducationalProgram', N'תוכנית שמיים'),
    (97, N'Domain', N'מניעת נשירה'),
    (97, N'Subject', N'אבחון מערכת שח"ר במסגרת החינוכית ואיתור מוקדי ההדרכה'),
    (97, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (97, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (97, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (97, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (97, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (97, N'Subject', N'בניית תוכנית הנחייה- שכבה ט'),
    (97, N'Subject', N'בניית תוכנית הנחייה- שכבה י'),
    (97, N'Subject', N'בניית תוכנית הנחייה- שכבה יא'),
    (97, N'Subject', N'בניית תוכנית הנחייה- שכבה יב'),
    (97, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (97, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (97, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (97, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (97, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (97, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (97, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (97, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (97, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (97, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (97, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (97, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (97, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (97, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (97, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (97, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (97, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (97, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (97, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (97, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (97, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (97, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (97, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (97, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (97, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (97, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (97, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (97, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (97, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (97, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (97, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (97, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (97, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (97, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (97, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (97, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (97, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (97, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (97, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (97, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (97, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (97, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (97, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (97, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (97, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (97, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (97, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (97, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (97, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (97, N'Subject', N'מפגש הנחייה אישית - טיפול ספירלי'),
    (97, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (97, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (97, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (97, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (97, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (97, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (97, N'Subject', N'מפגש הנחייה אישית -נוכחות  והיעדרויות'),
    (97, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (97, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (97, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (97, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (97, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (97, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (97, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (97, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (97, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (97, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (97, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (97, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- מניעת נשירה'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (97, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית-  בתחום מניעת אלימות'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- חוסן'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- חיזוק מיומנויות'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- חינוך טיפול'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- טיפול ספירלי'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי חט"ב'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה י'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יא'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יב'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- מרכז למידה'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- סדנת העשרה'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- פעילות חוץ'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- קידום נוער'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (97, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (97, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים אמצע שנה'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים סוף שנה'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים תחילת שנה'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות גריעה'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דשבורד מחלקתי'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב נוכחות'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב ציונים'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי נוער'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- פתיחת כיתות'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תגבורי חורף'),
    (97, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (97, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (97, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (97, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (97, N'DiscussionCode', N'דיון עם יועץ'),
    (97, N'DiscussionCode', N'דיון עם מורה'),
    (97, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (97, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (97, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (97, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (97, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (97, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (97, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (97, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (97, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (97, N'Class', N'1'),
    (97, N'Class', N'10'),
    (97, N'Class', N'11'),
    (97, N'Class', N'12'),
    (97, N'Class', N'13'),
    (97, N'Class', N'14'),
    (97, N'Class', N'15'),
    (97, N'Class', N'2'),
    (97, N'Class', N'3'),
    (97, N'Class', N'4'),
    (97, N'Class', N'5'),
    (97, N'Class', N'6'),
    (97, N'Class', N'7'),
    (97, N'Class', N'8'),
    (97, N'Class', N'9'),
    (97, N'Class', N'אין דרישות'),
    (97, N'Class', N'בניית תוכנית התנהגותית'),
    (97, N'Class', N'בניית תוכנית לימודים'),
    (97, N'Class', N'הגדלת היקף שעות'),
    (97, N'Class', N'הכנסת שינויים בביצוע'),
    (97, N'Class', N'התאמת אוכלוסית היעד'),
    (97, N'Class', N'התאמת תוכנית חברתית'),
    (97, N'Class', N'התאמת תוכנית טיפולית'),
    (97, N'Class', N'התאמת תוכנית כיתתית'),
    (97, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (97, N'Class', N'למידת עמיתים'),
    (97, N'Class', N'ניצול שעות נכון'),
    (97, N'Class', N'סיורים לימודיים'),
    (97, N'Class', N'שיחה עם הכתה/קבוצה'),
    (97, N'LocalityDistrictNational', N'איגום משאבים'),
    (97, N'LocalityDistrictNational', N'אין דרישות'),
    (97, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (97, N'LocalityDistrictNational', N'גיוס תרומות'),
    (97, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (97, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (97, N'LocalityDistrictNational', N'הנחיית הורים'),
    (97, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (97, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (97, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (97, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (97, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (97, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (97, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (97, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (97, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (97, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (97, N'GradeLevel', N'א'),
    (97, N'GradeLevel', N'ב'),
    (97, N'GradeLevel', N'ג'),
    (97, N'GradeLevel', N'ד'),
    (97, N'GradeLevel', N'ה'),
    (97, N'GradeLevel', N'ו'),
    (97, N'GradeLevel', N'ז'),
    (97, N'GradeLevel', N'ח'),
    (97, N'GradeLevel', N'חובה'),
    (97, N'GradeLevel', N'ט'),
    (97, N'GradeLevel', N'י'),
    (97, N'GradeLevel', N'יא'),
    (97, N'GradeLevel', N'יב'),
    (89, N'EducationalProgram', N'עוגנים יישוביים-רווחה ושיקום'),
    (89, N'Domain', N'רווחה וקהילה'),
    (89, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (89, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (89, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי התוכנית- איתור כח אדם'),
    (89, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (89, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (89, N'Subject', N'בניית תוכנית הנחייה- עבור מרכז נוער'),
    (89, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (89, N'Subject', N'בניית תוכנית הנחייה- שכבה ט'),
    (89, N'Subject', N'בניית תוכנית הנחייה- שכבה י'),
    (89, N'Subject', N'בניית תוכנית הנחייה- שכבה יא'),
    (89, N'Subject', N'בניית תוכנית הנחייה- שכבה יב'),
    (89, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (89, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (89, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (89, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (89, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (89, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (89, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (89, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (89, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (89, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (89, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (89, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (89, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (89, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (89, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (89, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים-  גורמי רווחה'),
    (89, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- מנהל מרכז נוער'),
    (89, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- רשות'),
    (89, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (89, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (89, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (89, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (89, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (89, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (89, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (89, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (89, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (89, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (89, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (89, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (89, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (89, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (89, N'Subject', N'השתתפות בהשתלמות מנחי מרכזי נוער'),
    (89, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (89, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה- סטטוס תקציב'),
    (89, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (89, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (89, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (89, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (89, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (89, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (89, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (89, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (89, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (89, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (89, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (89, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (89, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (89, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (89, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (89, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (89, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (89, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (89, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (89, N'Subject', N'מפגש הנחייה אישית - טיפול ספירלי'),
    (89, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (89, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (89, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (89, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (89, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (89, N'Subject', N'מפגש הנחייה אישית -מנהל מוסד'),
    (89, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (89, N'Subject', N'מפגש הנחייה אישית -נוכחות  והיעדרויות'),
    (89, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (89, N'Subject', N'מפגש הנחייה אישית -רכז פדגוגי'),
    (89, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (89, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (89, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (89, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (89, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (89, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (89, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (89, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (89, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (89, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (89, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- מניעת נשירה'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (89, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית-  בתחום מניעת אלימות'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- חוסן'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- חיזוק מיומנויות'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- חינוך טיפול'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- טיפול ספירלי'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי חט"ב'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה י'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יא'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יב'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- מרכז למידה'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- סדנת העשרה'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- פעילות חוץ'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- קידום נוער'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (89, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (89, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים אמצע שנה'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים סוף שנה'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים תחילת שנה'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות גריעה'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דשבורד מחלקתי'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- בקרה ופיקוח'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כיתות מיזם'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב נוכחות'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב ציונים'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי נוער'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- פתיחת כיתות'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תגבורי חורף'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב מחוזות'),
    (89, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב תוכניות'),
    (89, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (89, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (89, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (89, N'DiscussionCode', N'דיון עם יועץ'),
    (89, N'DiscussionCode', N'דיון עם מורה'),
    (89, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (89, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (89, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (89, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (89, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (89, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (89, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (89, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (89, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (89, N'Class', N'1'),
    (89, N'Class', N'10'),
    (89, N'Class', N'11'),
    (89, N'Class', N'12'),
    (89, N'Class', N'13'),
    (89, N'Class', N'14'),
    (89, N'Class', N'15'),
    (89, N'Class', N'2'),
    (89, N'Class', N'3'),
    (89, N'Class', N'4'),
    (89, N'Class', N'5'),
    (89, N'Class', N'6'),
    (89, N'Class', N'7'),
    (89, N'Class', N'8'),
    (89, N'Class', N'9'),
    (89, N'Class', N'אין דרישות'),
    (89, N'Class', N'בניית תוכנית התנהגותית'),
    (89, N'Class', N'בניית תוכנית לימודים'),
    (89, N'Class', N'הגדלת היקף שעות'),
    (89, N'Class', N'הכנסת שינויים בביצוע'),
    (89, N'Class', N'התאמת אוכלוסית היעד'),
    (89, N'Class', N'התאמת תוכנית חברתית'),
    (89, N'Class', N'התאמת תוכנית טיפולית'),
    (89, N'Class', N'התאמת תוכנית כיתתית'),
    (89, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (89, N'Class', N'למידת עמיתים'),
    (89, N'Class', N'ניצול שעות נכון'),
    (89, N'Class', N'סיורים לימודיים'),
    (89, N'Class', N'שיחה עם הכתה/קבוצה'),
    (89, N'LocalityDistrictNational', N'איגום משאבים'),
    (89, N'LocalityDistrictNational', N'אין דרישות'),
    (89, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (89, N'LocalityDistrictNational', N'גיוס תרומות'),
    (89, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (89, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (89, N'LocalityDistrictNational', N'הנחיית הורים'),
    (89, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (89, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (89, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (89, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (89, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (89, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (89, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (89, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (89, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (89, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (89, N'GradeLevel', N'א'),
    (89, N'GradeLevel', N'ב'),
    (89, N'GradeLevel', N'ג'),
    (89, N'GradeLevel', N'ד'),
    (89, N'GradeLevel', N'ה'),
    (89, N'GradeLevel', N'ו'),
    (89, N'GradeLevel', N'ז'),
    (89, N'GradeLevel', N'ח'),
    (89, N'GradeLevel', N'חובה'),
    (89, N'GradeLevel', N'ט'),
    (89, N'GradeLevel', N'י'),
    (89, N'GradeLevel', N'יא'),
    (89, N'GradeLevel', N'יב'),
    (92, N'Framework', N'הנחייה והטמעה ארצית- ביקור סדיר קבסים'),
    (92, N'Framework', N'הנחייה והטמעה מחוזית- ביקור סדיר קבסים'),
    (92, N'EducationalProgram', N'טיפול בפרט - קב"סים'),
    (92, N'Domain', N'ביקור סדיר'),
    (92, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (92, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (92, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (92, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (92, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (92, N'Subject', N'בניית תוכנית הנחייה- שכבה ט'),
    (92, N'Subject', N'בניית תוכנית הנחייה- שכבה י'),
    (92, N'Subject', N'בניית תוכנית הנחייה- שכבה יא'),
    (92, N'Subject', N'בניית תוכנית הנחייה- שכבה יב'),
    (92, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (92, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (92, N'Subject', N'בניית תוכנית הנחייה-בקרה ופיקוח תוכנית ההזנה'),
    (92, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (92, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (92, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (92, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (92, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (92, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (92, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (92, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (92, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (92, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (92, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (92, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (92, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (92, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- ייעוץ משפטי'),
    (92, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (92, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (92, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (92, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (92, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (92, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (92, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (92, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (92, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (92, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (92, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (92, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (92, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (92, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (92, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (92, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה- סטטוס תקציב'),
    (92, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה-וועדות היגוי'),
    (92, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (92, N'Subject', N'השתתפות במפגש עבודה עם הפיקוח ומנחה ארצית'),
    (92, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (92, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (92, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (92, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (92, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (92, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (92, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (92, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (92, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (92, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (92, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (92, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (92, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (92, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (92, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (92, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (92, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (92, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (92, N'Subject', N'מפגש הנחייה אישית - טיפול ספירלי'),
    (92, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (92, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (92, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (92, N'Subject', N'מפגש הנחייה אישית - קב"ס'),
    (92, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (92, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (92, N'Subject', N'מפגש הנחייה אישית -הטמעה נהלי ביקור סדיר'),
    (92, N'Subject', N'מפגש הנחייה אישית -הנחייה מערכת קבסנט'),
    (92, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (92, N'Subject', N'מפגש הנחייה אישית -נוכחות  והיעדרויות'),
    (92, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (92, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (92, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (92, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (92, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים -צוות פיקוח'),
    (92, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (92, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (92, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (92, N'Subject', N'קיום דיאלוג עם הפיקוח'),
    (92, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (92, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (92, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (92, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- מניעת נשירה'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (92, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית-  בתחום מניעת אלימות'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- הנחיית מערכת קבסנט'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- חוסן'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- חיזוק מיומנויות'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- חינוך טיפול'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- טיפול ספירלי'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי חט"ב'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה י'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יא'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יב'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- מרכז למידה'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- סדנת העשרה'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- פעילות חוץ'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- קידום נוער'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (92, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (92, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים אמצע שנה'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים סוף שנה'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים תחילת שנה'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בדיקות יציאה מהארץ - גחלת'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקרת הצטיידות'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות גריעה'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות פטור'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דשבורד מחלקתי'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- בקרה ופיקוח'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת קבסנט'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתבי התחייבות רשויות'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחולל דוחות'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב נוכחות'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב ציונים'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מערכת קבסנט'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי נוער'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סטטוס מנע'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- פתיחת כיתות'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תגבורי חורף'),
    (92, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (92, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (92, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (92, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (92, N'DiscussionCode', N'דיון עם יועץ'),
    (92, N'DiscussionCode', N'דיון עם מורה'),
    (92, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (92, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (92, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (92, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (92, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (92, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (92, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (92, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (92, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (92, N'Class', N'1'),
    (92, N'Class', N'10'),
    (92, N'Class', N'11'),
    (92, N'Class', N'12'),
    (92, N'Class', N'13'),
    (92, N'Class', N'14'),
    (92, N'Class', N'15'),
    (92, N'Class', N'2'),
    (92, N'Class', N'3'),
    (92, N'Class', N'4'),
    (92, N'Class', N'5'),
    (92, N'Class', N'6'),
    (92, N'Class', N'7'),
    (92, N'Class', N'8'),
    (92, N'Class', N'9'),
    (92, N'Class', N'אין דרישות'),
    (92, N'Class', N'בניית תוכנית התנהגותית'),
    (92, N'Class', N'בניית תוכנית לימודים'),
    (92, N'Class', N'הגדלת היקף שעות'),
    (92, N'Class', N'הכנסת שינויים בביצוע'),
    (92, N'Class', N'התאמת אוכלוסית היעד'),
    (92, N'Class', N'התאמת תוכנית חברתית'),
    (92, N'Class', N'התאמת תוכנית טיפולית'),
    (92, N'Class', N'התאמת תוכנית כיתתית'),
    (92, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (92, N'Class', N'למידת עמיתים'),
    (92, N'Class', N'ניצול שעות נכון'),
    (92, N'Class', N'סיורים לימודיים'),
    (92, N'Class', N'שיחה עם הכתה/קבוצה'),
    (92, N'LocalityDistrictNational', N'איגום משאבים'),
    (92, N'LocalityDistrictNational', N'אין דרישות'),
    (92, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (92, N'LocalityDistrictNational', N'גיוס תרומות'),
    (92, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (92, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (92, N'LocalityDistrictNational', N'הנחיית הורים'),
    (92, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (92, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (92, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (92, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (92, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (92, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (92, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (92, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (92, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (92, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (92, N'GradeLevel', N'א'),
    (92, N'GradeLevel', N'ב'),
    (92, N'GradeLevel', N'ג'),
    (92, N'GradeLevel', N'ד'),
    (92, N'GradeLevel', N'ה'),
    (92, N'GradeLevel', N'ו'),
    (92, N'GradeLevel', N'ז'),
    (92, N'GradeLevel', N'ח'),
    (92, N'GradeLevel', N'חובה'),
    (92, N'GradeLevel', N'ט'),
    (92, N'GradeLevel', N'י'),
    (92, N'GradeLevel', N'יא'),
    (92, N'GradeLevel', N'יב'),
    (91, N'EducationalProgram', N'תוכנית ההזנה הלאומית'),
    (91, N'EducationalProgram', N'תוכנית הזנה לאומית'),
    (91, N'Domain', N'רווחה וקהילה'),
    (91, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (91, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי התוכנית'),
    (91, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (91, N'Subject', N'בניית תוכנית בשיתופי פעולה בין גורמים שונים'),
    (91, N'Subject', N'בניית תוכנית הנחייה- הזנה יוח"א'),
    (91, N'Subject', N'בניית תוכנית הנחייה- הזנה מגזר ערבי'),
    (91, N'Subject', N'בניית תוכנית הנחייה- הזנה מחטים'),
    (91, N'Subject', N'בניית תוכנית הנחייה- הזנה ניצנים'),
    (91, N'Subject', N'בניית תוכנית הנחייה- הזנה קלית עלייה (קל"ע)'),
    (91, N'Subject', N'בניית תוכנית הנחייה-בקרה ופיקוח תוכנית ההזנה'),
    (91, N'Subject', N'בניית תוכנית הנחייה-הזנה -חנ"מ'),
    (91, N'Subject', N'בניית תוכנית הנחייה-נהלי תוכנית ההזנה'),
    (91, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (91, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי התוכנית'),
    (91, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (91, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (91, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (91, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי התוכנית'),
    (91, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (91, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (91, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (91, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (91, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (91, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (91, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (91, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (91, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- גורמי חברה וקהילה'),
    (91, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- ממונה משרד החינוך'),
    (91, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- צוותי חינוך בלתי פורמאליים'),
    (91, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- רשות'),
    (91, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (91, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (91, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (91, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (91, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (91, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (91, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (91, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (91, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (91, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (91, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (91, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (91, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (91, N'Subject', N'השתתפות בהשתלמות ארצית');
    INSERT INTO @ScopeSeed (ProgramId, ScopeType, Description) VALUES
    (91, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (91, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה- סטטוס תקציב'),
    (91, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה-וועדות היגוי'),
    (91, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (91, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (91, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (91, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (91, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (91, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (91, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (91, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (91, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (91, N'Subject', N'למידת עמיתים -סיורי שטח'),
    (91, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (91, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (91, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (91, N'Subject', N'מפגש הנחיה אישית  - הזנה -ליווי מנהל מסגרת'),
    (91, N'Subject', N'מפגש הנחיה אישית  - תצפיות ומעקב'),
    (91, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (91, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (91, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (91, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (91, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (91, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (91, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (91, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (91, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (91, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (91, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (91, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (91, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (91, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (91, N'Subject', N'קביעת תוכנית עבודה חודשית, הנגזרת מתוכנית עבודה שנתית ויעדים.'),
    (91, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (91, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (91, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית (ראש הישיבה) לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (91, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (91, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (91, N'Subject', N'קיום דיאלוג עם רכז  התכנית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (91, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (91, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (91, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (91, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (91, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (91, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (91, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח מקצועי'),
    (91, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (91, N'Subject', N'ריכוז נתונים כמותיים ואיכותיים רלוונטיים לגבי אוכלוסיית המטופלים של העובד'),
    (91, N'Subject', N'ריכוז נתונים כמותיים ואיכותיים רלוונטיים לגבי אוכלוסיית המטופלים של העובד ניתוח ואבחון ובהתאמה קישור, בנייה, ותכלול של תוכניות התערבות מותאמות פרטנית/קבוצתית/משפחתית/קהילתית.'),
    (91, N'Subject', N'ריכוז נתונים רלוונטיים ודיווח (אחת לחודש), בהתאם לבקשות ולהחלטות האגף ולדרישות המכרז.'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים-  הזנה -כתבי התחייבות רשויות'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה  ריכוז פניות וחריגות'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- בקרה ופיקוח'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה -מחוז דרום'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה -מחוז חיפה'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- מחוז חרדי'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- מחוז ירושלים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה -מחוז מרכז'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- מחוז צפון'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים  ניצנים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים  ניצנים-רשות'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים יוח"א'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -לבטח'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -מגזר חרדי'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -מגזר ערבי'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים מחטים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -סקר שביעות רצון'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -פילוט חינוך מיוחד (חנ"מ)'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -קליטת עליה (קלע)'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה נתונים תקציביים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה ספקים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתבי התחייבות רשויות'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סיכום פעילות חודשית'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקצוב דיפרנציאלי'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב מחוזות'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב תוכניות'),
    (91, N'Subject', N'ריכוז/ניתוח/עיבוד/בקרת נתונים- ספקים'),
    (91, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (91, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (91, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (91, N'DiscussionCode', N'דיון עם יועץ'),
    (91, N'DiscussionCode', N'דיון עם מורה'),
    (91, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (91, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (91, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (91, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (91, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (91, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (91, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (91, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (91, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (91, N'Class', N'1'),
    (91, N'Class', N'10'),
    (91, N'Class', N'11'),
    (91, N'Class', N'12'),
    (91, N'Class', N'13'),
    (91, N'Class', N'14'),
    (91, N'Class', N'15'),
    (91, N'Class', N'2'),
    (91, N'Class', N'3'),
    (91, N'Class', N'4'),
    (91, N'Class', N'5'),
    (91, N'Class', N'6'),
    (91, N'Class', N'7'),
    (91, N'Class', N'8'),
    (91, N'Class', N'9'),
    (91, N'Class', N'אין דרישות'),
    (91, N'Class', N'בניית תוכנית התנהגותית'),
    (91, N'Class', N'בניית תוכנית לימודים'),
    (91, N'Class', N'הגדלת היקף שעות'),
    (91, N'Class', N'הכנסת שינויים בביצוע'),
    (91, N'Class', N'התאמת אוכלוסית היעד'),
    (91, N'Class', N'התאמת תוכנית חברתית'),
    (91, N'Class', N'התאמת תוכנית טיפולית'),
    (91, N'Class', N'התאמת תוכנית כיתתית'),
    (91, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (91, N'Class', N'למידת עמיתים'),
    (91, N'Class', N'ניצול שעות נכון'),
    (91, N'Class', N'סיורים לימודיים'),
    (91, N'Class', N'שיחה עם הכתה/קבוצה'),
    (91, N'LocalityDistrictNational', N'איגום משאבים'),
    (91, N'LocalityDistrictNational', N'אין דרישות'),
    (91, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (91, N'LocalityDistrictNational', N'גיוס תרומות'),
    (91, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (91, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (91, N'LocalityDistrictNational', N'הנחיית הורים'),
    (91, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (91, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (91, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (91, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (91, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (91, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (91, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (91, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (91, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (91, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (91, N'GradeLevel', N'א'),
    (91, N'GradeLevel', N'ב'),
    (91, N'GradeLevel', N'ג'),
    (91, N'GradeLevel', N'ד'),
    (91, N'GradeLevel', N'ה'),
    (91, N'GradeLevel', N'ו'),
    (91, N'GradeLevel', N'ז'),
    (91, N'GradeLevel', N'ח'),
    (91, N'GradeLevel', N'חובה'),
    (91, N'GradeLevel', N'ט'),
    (91, N'GradeLevel', N'י'),
    (91, N'GradeLevel', N'יא'),
    (91, N'GradeLevel', N'יב'),
    (94, N'Framework', N'אום טובא 662296 אם טובא תיכון בנים'),
    (94, N'Framework', N'אלת''ורי אבו טור 662452 אחמד סאמח תיכון בנים'),
    (94, N'Framework', N'בית חנינא 650028 אלקימה'),
    (94, N'Framework', N'טור 148247 מקיף אלטור בנים'),
    (94, N'Framework', N'מחנה שויפאט 641407 אלמותנבי'),
    (94, N'Framework', N'סוואחרה 714204 סוואחרה תיכון בנים'),
    (94, N'Framework', N'עיסאוויה 729871 תיכון עיסאוויה בנים'),
    (94, N'Framework', N'ראס אלעמוד 540567 ראס אלעמוד תיכון בנים'),
    (94, N'Framework', N'שועפאט 148155 מקיף שועפאט בנים'),
    (94, N'EducationalProgram', N'מניעת נשירה- מזרח ירושלים'),
    (94, N'Domain', N'מניעת נשירה'),
    (94, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (94, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (94, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (94, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (94, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (94, N'Subject', N'בניית תוכנית הנחייה- שכבה ט'),
    (94, N'Subject', N'בניית תוכנית הנחייה- שכבה י'),
    (94, N'Subject', N'בניית תוכנית הנחייה- שכבה יא'),
    (94, N'Subject', N'בניית תוכנית הנחייה- שכבה יב'),
    (94, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (94, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (94, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (94, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (94, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (94, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (94, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (94, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (94, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (94, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (94, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (94, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (94, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (94, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (94, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (94, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (94, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (94, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (94, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (94, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (94, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (94, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (94, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (94, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (94, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (94, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (94, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (94, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (94, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (94, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (94, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (94, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (94, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (94, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (94, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (94, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (94, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (94, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (94, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (94, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (94, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (94, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (94, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (94, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (94, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (94, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (94, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (94, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (94, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (94, N'Subject', N'מפגש הנחייה אישית - טיפול ספירלי'),
    (94, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (94, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (94, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (94, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (94, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (94, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (94, N'Subject', N'מפגש הנחייה אישית -נוכחות  והיעדרויות'),
    (94, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (94, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (94, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (94, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (94, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (94, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (94, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (94, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (94, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (94, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (94, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (94, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- מניעת נשירה'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (94, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית-  בתחום מניעת אלימות'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- חוסן'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- חיזוק מיומנויות'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- חינוך טיפול'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- טיפול ספירלי'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי חט"ב'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה י'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יא'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יב'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- מרכז למידה'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- סדנת העשרה'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- פעילות חוץ'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- קידום נוער'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (94, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (94, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים אמצע שנה'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים סוף שנה'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים תחילת שנה'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות גריעה'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דשבורד מחלקתי'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב נוכחות'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב ציונים'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי נוער'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- פתיחת כיתות'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תגבורי חורף'),
    (94, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (94, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (94, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (94, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (94, N'DiscussionCode', N'דיון עם יועץ'),
    (94, N'DiscussionCode', N'דיון עם מורה'),
    (94, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (94, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (94, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (94, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (94, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (94, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (94, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (94, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (94, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (94, N'Class', N'אין דרישות'),
    (94, N'Class', N'בניית תוכנית התנהגותית'),
    (94, N'Class', N'בניית תוכנית לימודים'),
    (94, N'Class', N'הגדלת היקף שעות'),
    (94, N'Class', N'הכנסת שינויים בביצוע'),
    (94, N'Class', N'התאמת אוכלוסית היעד'),
    (94, N'Class', N'התאמת תוכנית חברתית'),
    (94, N'Class', N'התאמת תוכנית טיפולית'),
    (94, N'Class', N'התאמת תוכנית כיתתית'),
    (94, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (94, N'Class', N'למידת עמיתים'),
    (94, N'Class', N'ניצול שעות נכון'),
    (94, N'Class', N'סיורים לימודיים'),
    (94, N'Class', N'שיחה עם הכתה/קבוצה'),
    (87, N'EducationalProgram', N'אור בגנים'),
    (87, N'EducationalProgram', N'כיתות א"מץ'),
    (87, N'EducationalProgram', N'כיתות אתגר'),
    (87, N'EducationalProgram', N'כיתות במרכזי חינוך ונוער'),
    (87, N'EducationalProgram', N'כיתות בתי"ס במעבר'),
    (87, N'EducationalProgram', N'כיתות מב"ר'),
    (87, N'EducationalProgram', N'כיתות מיזם'),
    (87, N'EducationalProgram', N'כיתות מל"א'),
    (87, N'EducationalProgram', N'כיתות מפתנים'),
    (87, N'EducationalProgram', N'כיתות שח"ר'),
    (87, N'EducationalProgram', N'כיתות תל"ם'),
    (87, N'EducationalProgram', N'כנפי רוח'),
    (87, N'EducationalProgram', N'כתות בתי"ס ייחודיים'),
    (87, N'EducationalProgram', N'מועדוניות משפחתיות'),
    (87, N'EducationalProgram', N'מרכזי חירום'),
    (87, N'EducationalProgram', N'עוגנים יישוביים-רווחה ושיקום'),
    (87, N'EducationalProgram', N'פדגוגיה טיפולית'),
    (87, N'EducationalProgram', N'פותחים עתיד'),
    (87, N'EducationalProgram', N'תגבורי חורף'),
    (87, N'EducationalProgram', N'תוכנית אמ"ת'),
    (87, N'EducationalProgram', N'תוכנית הילה'),
    (87, N'EducationalProgram', N'תוכנית חנוך לנער'),
    (87, N'EducationalProgram', N'תוכנית מל"א - יסודי'),
    (87, N'EducationalProgram', N'תוכנית מלא ליסודיים- נקודת אור'),
    (87, N'EducationalProgram', N'תוכנית משיבים'),
    (87, N'EducationalProgram', N'תל"ם-נחשון'),
    (87, N'Domain', N'מוסדי'),
    (87, N'Domain', N'מניעת נשירה'),
    (87, N'Domain', N'מסגרות ייחודיות'),
    (87, N'Domain', N'רווחה וקהילה'),
    (87, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (87, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (87, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי התוכנית- איתור כח אדם'),
    (87, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (87, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (87, N'Subject', N'בניית תוכנית הנחייה- עבור מרכז נוער'),
    (87, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (87, N'Subject', N'בניית תוכנית הנחייה- שכבה ט'),
    (87, N'Subject', N'בניית תוכנית הנחייה- שכבה י'),
    (87, N'Subject', N'בניית תוכנית הנחייה- שכבה יא'),
    (87, N'Subject', N'בניית תוכנית הנחייה- שכבה יב'),
    (87, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (87, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (87, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (87, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (87, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (87, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (87, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (87, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (87, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (87, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (87, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (87, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (87, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (87, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (87, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (87, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים-  גורמי רווחה'),
    (87, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- מנהל מרכז נוער'),
    (87, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- רשות'),
    (87, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (87, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (87, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (87, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (87, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (87, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (87, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (87, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (87, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (87, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (87, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (87, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (87, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (87, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (87, N'Subject', N'השתתפות בהשתלמות מנחי מרכזי נוער'),
    (87, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (87, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה- סטטוס תקציב'),
    (87, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (87, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (87, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (87, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (87, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (87, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (87, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (87, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (87, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (87, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (87, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (87, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (87, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (87, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (87, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (87, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (87, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (87, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (87, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (87, N'Subject', N'מפגש הנחייה אישית - טיפול ספירלי'),
    (87, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (87, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (87, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (87, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (87, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (87, N'Subject', N'מפגש הנחייה אישית -מנהל מוסד'),
    (87, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (87, N'Subject', N'מפגש הנחייה אישית -נוכחות  והיעדרויות'),
    (87, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (87, N'Subject', N'מפגש הנחייה אישית -רכז פדגוגי'),
    (87, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (87, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (87, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (87, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (87, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (87, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (87, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (87, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (87, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (87, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (87, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- מניעת נשירה'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (87, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית-  בתחום מניעת אלימות'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- חוסן'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- חיזוק מיומנויות'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- חינוך טיפול'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- טיפול ספירלי'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי חט"ב'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה י'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יא'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יב'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- מרכז למידה'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- סדנת העשרה'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- פעילות חוץ'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- קידום נוער'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (87, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (87, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים אמצע שנה'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים סוף שנה'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים תחילת שנה'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות גריעה'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דשבורד מחלקתי'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- בקרה ופיקוח'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כיתות מיזם'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב נוכחות'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב ציונים'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי נוער'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- פתיחת כיתות'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תגבורי חורף'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב מחוזות'),
    (87, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב תוכניות'),
    (87, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (87, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (87, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (87, N'DiscussionCode', N'דיון עם יועץ'),
    (87, N'DiscussionCode', N'דיון עם מורה'),
    (87, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (87, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (87, N'DiscussionCode', N'דיון עם מרכז התוכנית'),
    (87, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (87, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (87, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (87, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (87, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (87, N'DiscussionCode', N'דיון עם צוות עמיתים'),
    (87, N'Class', N'1'),
    (87, N'Class', N'10'),
    (87, N'Class', N'11'),
    (87, N'Class', N'12'),
    (87, N'Class', N'13'),
    (87, N'Class', N'14'),
    (87, N'Class', N'15'),
    (87, N'Class', N'2'),
    (87, N'Class', N'3'),
    (87, N'Class', N'4'),
    (87, N'Class', N'5'),
    (87, N'Class', N'6'),
    (87, N'Class', N'7'),
    (87, N'Class', N'8'),
    (87, N'Class', N'9'),
    (87, N'Class', N'אין דרישות'),
    (87, N'Class', N'בניית תוכנית התנהגותית'),
    (87, N'Class', N'בניית תוכנית לימודים'),
    (87, N'Class', N'הגדלת היקף שעות'),
    (87, N'Class', N'הכנסת שינויים בביצוע'),
    (87, N'Class', N'התאמת אוכלוסית היעד'),
    (87, N'Class', N'התאמת תוכנית חברתית'),
    (87, N'Class', N'התאמת תוכנית טיפולית'),
    (87, N'Class', N'התאמת תוכנית כיתתית'),
    (87, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (87, N'Class', N'למידת עמיתים'),
    (87, N'Class', N'ניצול שעות נכון'),
    (87, N'Class', N'סיורים לימודיים'),
    (87, N'Class', N'שיחה עם הכתה/קבוצה'),
    (87, N'LocalityDistrictNational', N'איגום משאבים'),
    (87, N'LocalityDistrictNational', N'אין דרישות'),
    (87, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (87, N'LocalityDistrictNational', N'גיוס תרומות'),
    (87, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (87, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (87, N'LocalityDistrictNational', N'הנחיית הורים'),
    (87, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (87, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (87, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (87, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (87, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (87, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (87, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (87, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (87, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (87, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (87, N'GradeLevel', N'א'),
    (87, N'GradeLevel', N'ב'),
    (87, N'GradeLevel', N'ג'),
    (87, N'GradeLevel', N'ד'),
    (87, N'GradeLevel', N'ה'),
    (87, N'GradeLevel', N'ו'),
    (87, N'GradeLevel', N'ז'),
    (87, N'GradeLevel', N'ח'),
    (87, N'GradeLevel', N'חובה'),
    (87, N'GradeLevel', N'ט'),
    (87, N'GradeLevel', N'י'),
    (87, N'GradeLevel', N'יא'),
    (87, N'GradeLevel', N'יב'),
    (90, N'EducationalProgram', N'אור בגנים'),
    (90, N'EducationalProgram', N'חסות הנוער'),
    (90, N'EducationalProgram', N'טיפוח מיומנויות -אקדמיה'),
    (90, N'EducationalProgram', N'טיפול בפרט - קב"סים'),
    (90, N'EducationalProgram', N'כיתות א"מץ'),
    (90, N'EducationalProgram', N'כיתות אתגר'),
    (90, N'EducationalProgram', N'כיתות במרכזי חינוך ונוער'),
    (90, N'EducationalProgram', N'כיתות בתי"ס במעבר'),
    (90, N'EducationalProgram', N'כיתות מב"ר'),
    (90, N'EducationalProgram', N'כיתות מל"א'),
    (90, N'EducationalProgram', N'כיתות מפתנים'),
    (90, N'EducationalProgram', N'כיתות שח"ר'),
    (90, N'EducationalProgram', N'כיתות תל"ם'),
    (90, N'EducationalProgram', N'כתות בתי"ס ייחודיים'),
    (90, N'EducationalProgram', N'מועדוניות משפחתיות'),
    (90, N'EducationalProgram', N'מרכזי חירום'),
    (90, N'EducationalProgram', N'עוגנים יישוביים-רווחה ושיקום'),
    (90, N'EducationalProgram', N'פדגוגיה טיפולית'),
    (90, N'EducationalProgram', N'פותחים עתיד'),
    (90, N'EducationalProgram', N'תגבורי חורף'),
    (90, N'EducationalProgram', N'תוכנית אמ"ת'),
    (90, N'EducationalProgram', N'תוכנית האגף כללי'),
    (90, N'EducationalProgram', N'תוכנית הזנה לאומית'),
    (90, N'EducationalProgram', N'תוכנית הילה'),
    (90, N'EducationalProgram', N'תוכנית יחד'),
    (90, N'EducationalProgram', N'תוכנית מל"א - יסודי'),
    (90, N'EducationalProgram', N'תוכנית מלא ליסודיים- נקודת אור'),
    (90, N'EducationalProgram', N'תוכנית ממ"ש'),
    (90, N'EducationalProgram', N'תל"ם-נחשון'),
    (90, N'Domain', N'ביקור סדיר'),
    (90, N'Domain', N'מוסדי'),
    (90, N'Domain', N'מניעת נשירה'),
    (90, N'Domain', N'מסגרות ייחודיות'),
    (90, N'Domain', N'רווחה וקהילה'),
    (90, N'Subject', N'אבחון מערכת שח"ר במסגרת החינוכית ואיתור מוקדי ההדרכה'),
    (90, N'Subject', N'אבחון מערכת שח"ר במסגרת היישובית ואיתור מוקדי ההדרכה'),
    (90, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי התוכנית'),
    (90, N'Subject', N'איתור גורמים בית ספריים/יישובים בעלי מענה לצרכי כיתות שח"ר'),
    (90, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי התוכנית- איתור כח אדם'),
    (90, N'Subject', N'איתור גורמים רשות/יישובים בעלי מענה לצרכי מועדוניות'),
    (90, N'Subject', N'ביית תוכנית הנחייה - התנהגותית'),
    (90, N'Subject', N'בניית תוכנית בשיתופי פעולה בין גורמים שונים'),
    (90, N'Subject', N'בניית תוכנית הנחייה - טכנאות ובגרות (טו"ב)'),
    (90, N'Subject', N'בניית תוכנית הנחייה- הזנה יוח"א'),
    (90, N'Subject', N'בניית תוכנית הנחייה- הזנה מגזר ערבי'),
    (90, N'Subject', N'בניית תוכנית הנחייה- הזנה מחטים'),
    (90, N'Subject', N'בניית תוכנית הנחייה- הזנה ניצנים'),
    (90, N'Subject', N'בניית תוכנית הנחייה- הזנה קלית עלייה (קל"ע)'),
    (90, N'Subject', N'בניית תוכנית הנחייה- העצמה ופיתוח אישי'),
    (90, N'Subject', N'בניית תוכנית הנחייה- מועדוניות'),
    (90, N'Subject', N'בניית תוכנית הנחייה- מיומנויות למידה'),
    (90, N'Subject', N'בניית תוכנית הנחייה- עבור מרכז נוער'),
    (90, N'Subject', N'בניית תוכנית הנחייה- פדגוגיה טיפולית'),
    (90, N'Subject', N'בניית תוכנית הנחייה- פיתוח כלים'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה ז'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה ח'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה ט'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה ט- סדנת הפגה חינוך טיפול'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה י'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה י -סדנת הפגה חינוך טיפול'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה יא'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה יא- סדנת הפגה חינוך טיפול'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה יב'),
    (90, N'Subject', N'בניית תוכנית הנחייה- שכבה יב -סדנת הפגה חינוך טיפול'),
    (90, N'Subject', N'בניית תוכנית הנחייה- תוכניות האצה'),
    (90, N'Subject', N'בניית תוכנית הנחייה- תוכניות רגשיות-חברתיות'),
    (90, N'Subject', N'בניית תוכנית הנחייה- תחומי חינוך טיפול'),
    (90, N'Subject', N'בניית תוכנית הנחייה-בקרה ופיקוח תוכנית ההזנה'),
    (90, N'Subject', N'בניית תוכנית הנחייה-הזנה -חנ"מ'),
    (90, N'Subject', N'בניית תוכנית הנחייה-נהלי תוכנית ההזנה'),
    (90, N'Subject', N'בניית תוכנית הערכה ובקרה להישגי תלמידים'),
    (90, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי  תלמידי המרכזים לגיל הרך'),
    (90, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי התוכנית'),
    (90, N'Subject', N'בניית תוכנית התערבות הכוללת גיוון והתאמת דרכי הוראה לצורכי תלמידי שח"ר'),
    (90, N'Subject', N'בנית פלטפורמה לשיתופי פעולה בין גורמים שונים'),
    (90, N'Subject', N'בנית פלטפורמה לשיתופי פעולה- התוכנית הלאומית 360'),
    (90, N'Subject', N'בנית תוכנית הנחייה- התבגרות וחוסן'),
    (90, N'Subject', N'בנית תוכנית הנחייה- זיהוי משברים מודל וכלים לטיפול'),
    (90, N'Subject', N'בנית תוכנית הנחייה- תקשורת אמון וגבולות'),
    (90, N'Subject', N'בנית תוכנית עבודה מותאמת להישגי תלמידים'),
    (90, N'Subject', N'בקרת נתונים'),
    (90, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי התוכנית'),
    (90, N'Subject', N'הגדרת הקריטריונים המאפיינים את תלמידי שח"ר'),
    (90, N'Subject', N'הדרכה בפדגוגיה טיפולית'),
    (90, N'Subject', N'הכוונה וייעוץ בנושא המפגש הרב-תרבותי'),
    (90, N'Subject', N'הכוונה וייעוץ בנושאי הכשרה והשתלמויות'),
    (90, N'Subject', N'הכוונה וייעוץ למורה בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (90, N'Subject', N'הכוונה וייעוץ למחנך בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (90, N'Subject', N'הכוונה וייעוץ לעובד בהכנת הצעה לתוכנית ההתערבות עם הפרט'),
    (90, N'Subject', N'הכוונה לרב תרבותיות וייעוץ בנושא המפגש הבין-תרבותי.'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים-  גורמי רווחה'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- גורמי חברה וקהילה'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- חינוך טיפול'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- טיפול רגשי'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- יועץ חינוכי'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- ייעוץ משפטי'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- ממונה משרד החינוך'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- מנהל מרכז נוער'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- עו"ס'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- צוותי חינוך בלתי פורמאליים'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- רשות'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים- תנועות נוער'),
    (90, N'Subject', N'הכוונה לשת"פ עם גורמים מתאימים-פעיל ארגון'),
    (90, N'Subject', N'הכוונה לתיווך ולשת''''פ עם גורמים ושירותים רלוונטיים העוסקים באוכלוסיה.'),
    (90, N'Subject', N'הכוונה, ייעוץ ותמיכה בהטמעת השימוש במדיה דיגיטאלית'),
    (90, N'Subject', N'הכנה וסיוע בהכשרה/השתלמות לפיתוח צוותי ההוראה וההדרכה'),
    (90, N'Subject', N'הנחיה של המערך המסייע ברשות המקומית (פר"ח, מורות חיילות, מתנדבים ועוד), על פי הצורך ובהתאם להחלטת מפקח ממונה מחוזי/מחלקתי.'),
    (90, N'Subject', N'הנחייה בנושאי ניהול הידע המצטבר בתחום ההדרכה'),
    (90, N'Subject', N'הנחייה והטמעה טכנולוגיות למידה דיגיטאליות'),
    (90, N'Subject', N'הנחייה פרטנית ו/או צוותית לפיתוח תוכנית בתחום ההנחייה ויישומה'),
    (90, N'Subject', N'הנחיית המערך המסייע ברשות המקומית'),
    (90, N'Subject', N'הסברה, ניתוח, עיבוד והתאמה של מדיניות האגף למציאות המסגרת המונחית'),
    (90, N'Subject', N'הערכה, איפיון ומתן משוב לגבי הפעילות הניהולית של מנהל המסגרת'),
    (90, N'Subject', N'הערכה, אפיון ומתן משוב לגבי הפעילות הניהולית של המנהל המסגרת.'),
    (90, N'Subject', N'הערכת התפקוד המקצועי של עובדיו (בכתב, בע''''פ) מתן משוב ועיבוד.'),
    (90, N'Subject', N'הערכת תפקוד מקצועי של עובד, מתן משוב ועיבודו'),
    (90, N'Subject', N'הפעלת סדנאות למידה והתנסות'),
    (90, N'Subject', N'השתתפות בהשתלמות ארצית'),
    (90, N'Subject', N'השתתפות בהשתלמות ארצית- בתחום תמיכה רגשית קוגנטיבית - כלים וטיפול'),
    (90, N'Subject', N'השתתפות בהשתלמות מנחי מרכזי נוער'),
    (90, N'Subject', N'השתתפות בהשתלמות פיתוח מקצועי לצוותי מועדוניות'),
    (90, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה'),
    (90, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה- סטטוס תקציב'),
    (90, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה- סטטוס תקציב טכנאות ובגרות (טו"ב)'),
    (90, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה- קידום נוער'),
    (90, N'Subject', N'השתתפות בהשתלמות/ישיבת צוות במחוז/במחלקה-וועדות היגוי'),
    (90, N'Subject', N'השתתפות במפגש וועדת היגוי ברשות המקומית'),
    (90, N'Subject', N'השתתפות במפגש מנחים אזוריים וארציים'),
    (90, N'Subject', N'השתתפות במפגש מנחים מועדוניות אזוריים וארציים'),
    (90, N'Subject', N'השתתפות במפגש מנחים מרכזים לגיל הרך אזוריים וארציים'),
    (90, N'Subject', N'השתתפות במפגש עבודה עם הפיקוח ומנחה ארצית'),
    (90, N'Subject', N'השתתפות בצוות רב תפקידי/מקצועי'),
    (90, N'Subject', N'זיהוי כוחות הוראה פנים בית-ספרים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (90, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי מרכזים לגיל הרך'),
    (90, N'Subject', N'זיהוי כוחות הוראה פנים יישוביים והעצמתם כמובילים פדגוגים מומחי שח"ר'),
    (90, N'Subject', N'חשיפת ידע מצטבר לכלל המורים לפיתוח הצוות'),
    (90, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי'),
    (90, N'Subject', N'ייעוץ בנושאי פיתוח אירגוני וכוח אדם מקצועי בתכניות הרווחה'),
    (90, N'Subject', N'יישום תוכניות ברשויות השונות'),
    (90, N'Subject', N'יישום תוכניות התערבות ותוכניות למידה'),
    (90, N'Subject', N'למידת עמיתים - הדגמת שיעור פתוח'),
    (90, N'Subject', N'למידת עמיתים - התיעצות מול קולגות במחוזות אחרים'),
    (90, N'Subject', N'למידת עמיתים - ניתוח מקרים והתנסויות בהוראה'),
    (90, N'Subject', N'למידת עמיתים -סיורי שטח'),
    (90, N'Subject', N'מימוש יעדי האגף מול האופי והיעדים של הרשות המקומית'),
    (90, N'Subject', N'מימוש יעדי האגף מול תוכניות הפעולה של המסגרת'),
    (90, N'Subject', N'מיפוי צרכים במסגרת החינוכית'),
    (90, N'Subject', N'מפגש הנחיה אישית  - איגום משאבים'),
    (90, N'Subject', N'מפגש הנחיה אישית  - הזנה -ליווי מנהל מסגרת'),
    (90, N'Subject', N'מפגש הנחיה אישית  - מנהל מחלקה לחטיבה עליונה -ארצי'),
    (90, N'Subject', N'מפגש הנחיה אישית  - מנהל מחלקה לחטיבת ביינים במחוז'),
    (90, N'Subject', N'מפגש הנחיה אישית  - מנהל רווחה ברשות'),
    (90, N'Subject', N'מפגש הנחיה אישית  - מנהל/ת מרכז לגיל הרך'),
    (90, N'Subject', N'מפגש הנחיה אישית  - מנהלת מועדונית'),
    (90, N'Subject', N'מפגש הנחיה אישית  - מרכז למידה'),
    (90, N'Subject', N'מפגש הנחיה אישית  - ניהול משאבים ושימור הון אנושי'),
    (90, N'Subject', N'מפגש הנחיה אישית  - ניהול משאבים תקציבים מרכז לגיל הרך'),
    (90, N'Subject', N'מפגש הנחיה אישית  - ראיונות  ילדים'),
    (90, N'Subject', N'מפגש הנחיה אישית  - תצפיות ומעקב'),
    (90, N'Subject', N'מפגש הנחיה אישית  לגבי חט"ב'),
    (90, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ז'),
    (90, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ח'),
    (90, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה ט'),
    (90, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה י'),
    (90, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יא'),
    (90, N'Subject', N'מפגש הנחיה אישית  לגבי שכבה יב'),
    (90, N'Subject', N'מפגש הנחיה אישית  מנהל מרכז נוער'),
    (90, N'Subject', N'מפגש הנחיה אישית  מנחת מועדנית'),
    (90, N'Subject', N'מפגש הנחיה אישית  מרכז נוער'),
    (90, N'Subject', N'מפגש הנחייה אישית - העצמה ופיתוח אישי'),
    (90, N'Subject', N'מפגש הנחייה אישית - חינוך טיפול- טיפול עומק'),
    (90, N'Subject', N'מפגש הנחייה אישית - טיפול באומנות'),
    (90, N'Subject', N'מפגש הנחייה אישית - טיפול באתגרים חברתיים'),
    (90, N'Subject', N'מפגש הנחייה אישית - טיפול מתרים במניעת נשירה'),
    (90, N'Subject', N'מפגש הנחייה אישית - טיפול ספירלי'),
    (90, N'Subject', N'מפגש הנחייה אישית - טיפול עומק'),
    (90, N'Subject', N'מפגש הנחייה אישית - ליווי ייעוץ מקצועי'),
    (90, N'Subject', N'מפגש הנחייה אישית - סיוע למניעת נשירה'),
    (90, N'Subject', N'מפגש הנחייה אישית - פדגוגיה טיפולית'),
    (90, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית'),
    (90, N'Subject', N'מפגש הנחייה אישית - תמיכה רגשית-קוגנטיבית'),
    (90, N'Subject', N'מפגש הנחייה אישית -ביקורי בית'),
    (90, N'Subject', N'מפגש הנחייה אישית -הנחייה מערכת קבסנט'),
    (90, N'Subject', N'מפגש הנחייה אישית -התבגרות וחוסן'),
    (90, N'Subject', N'מפגש הנחייה אישית -מעורבות הורים'),
    (90, N'Subject', N'מפגש הנחייה אישית -נוכחות  והיעדרויות'),
    (90, N'Subject', N'מפגש הנחייה אישית -פיתוח מקצועי'),
    (90, N'Subject', N'מפגש הנחייה אישית -תקצוב בית ספרי'),
    (90, N'Subject', N'מפגש הנחייה אישית -תקשורת אמון וגבולות'),
    (90, N'Subject', N'ניתוח ועיבוד אירועים ותהליכים של התערבות חינוכית-טיפולית'),
    (90, N'Subject', N'ניתוח ועיבוד המדיניות של אגף א'' חינוך ילדים ונוער בסיכון והתאמתה לאוכלוסיה ולמציאות של העובד.'),
    (90, N'Subject', N'ניתוח נתונים'),
    (90, N'Subject', N'ניתוח, עיבוד והתאמת מדיניות האגף למציאות העובד'),
    (90, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים'),
    (90, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים -מיטב'),
    (90, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים -מינהלת תקשוב'),
    (90, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים -צוות אורט'),
    (90, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים -צוות אכ"א'),
    (90, N'Subject', N'עדכון והעברת מידע וידע מנהליים ומקצועיים -צוות פיקוח'),
    (90, N'Subject', N'עיבוד נתונים'),
    (90, N'Subject', N'פיתוח יכולת המורה והרכז למיפוי צרכים של תלמידי שח"ר'),
    (90, N'Subject', N'פיתוח מקצועי  טכנולגיות למידה דיגיטאליות'),
    (90, N'Subject', N'פיתוח מקצועי קהילות לומדות'),
    (90, N'Subject', N'קביעת תוכנית עבודה חודשית'),
    (90, N'Subject', N'קביעת תוכנית עבודה חודשית- מרכזים לגיל הרך'),
    (90, N'Subject', N'קביעת תוכנית עבודה חודשית, הנגזרת מתוכנית עבודה שנתית ויעדים.'),
    (90, N'Subject', N'קיום "ישיבת עומק" לבחינת הישגים וקביעת דרכי עבודה ולמידה'),
    (90, N'Subject', N'קיום "ישיבת עומק" עם נציגי חינוך מיוחד'),
    (90, N'Subject', N'קיום דיאלוג עם הפיקוח'),
    (90, N'Subject', N'קיום דיאלוג עם מורה לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (90, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית (ראש הישיבה) לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (90, N'Subject', N'קיום דיאלוג עם מנהל המסגרת החינוכית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (90, N'Subject', N'קיום דיאלוג עם מנחה ארצי'),
    (90, N'Subject', N'קיום דיאלוג עם מפקח כולל לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (90, N'Subject', N'קיום דיאלוג עם רכז  התכנית לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (90, N'Subject', N'קיום דיאלוג עם רכז לשם שימוש בממצאי הערכה פנימיים וחיצוניים'),
    (90, N'Subject', N'קיום ישיבה פדגוגית'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- במחוז'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- בשיתוף מנהלת 360 תוכנית הלאומית'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- הישגים והערכה'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- הנהלת בית ספר / השתתפות באסיפת צוות'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- התאמת מסגרת לילד'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- וועדות שיבוץ ילדים'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- יועצ/ת בית ספר'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- מועצת פדגוגית ילדים'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- מורים'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- מלמדים'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- מנחי יחד'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- מניעת נשירה'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- מעקב תלמידים'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- סטטוס חודשי'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- צוות מקצועי בית ספרי'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- צוות פארא רפואי'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- ראש הישיבה'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- שותפים חוץ מסגרתיים'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- תוכנית שנתית'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- תחומי חינוך טיפול'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- תחומי פדגוגיה'),
    (90, N'Subject', N'קיום ישיבה פדגוגית- תחומי תוכן'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  בתחום מניעת אלימות'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  הורים ותלמידים'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  למידת עמיתים'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  ניהול משאבים ושימור ההון האנושי'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  ניהול משאבים תקציבים'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  עיבוד רגשי'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  פרקטיקות ופיתוח מקצועי'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  צוות מועדוניות'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  צוות מרכז לגיל הרך'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית-  צוות מרכז נוער'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- איתור מוקדם'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- הנחיית מערכת קבסנט'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- הסתגלות ילדים חדשים'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- העצמה- מסירת ועד/חבורה'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- הצבת יעדים'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- חוסן'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- חיזוק מיומנויות'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- חינוך טיפול'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- טיפול ספירלי'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי חט"ב'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה י'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יא'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- לגבי שכבה יב'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- מחטים'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- מרכז למידה'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- סדנת העשרה'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- עיבוד רגשי'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- פדגוגיה טיפולית'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח אישי'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- פיתוח מקצועי'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- פעילות חוץ'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- קידום נוער'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- רגשי התנהגותי'),
    (90, N'Subject', N'קיום מפגש הנחייה קבוצתית- שיתופי פעולה עם רווחה'),
    (90, N'Subject', N'ריכוז ועיבוד נתונים- חסות הנוער'),
    (90, N'Subject', N'ריכוז נתונים'),
    (90, N'Subject', N'ריכוז נתונים כמותיים ואיכותיים רלוונטיים לגבי אוכלוסיית המטופלים של העובד'),
    (90, N'Subject', N'ריכוז נתונים כמותיים ואיכותיים רלוונטיים לגבי אוכלוסיית המטופלים של העובד ניתוח ואבחון ובהתאמה קישור, בנייה, ותכלול של תוכניות התערבות מותאמות פרטנית/קבוצתית/משפחתית/קהילתית.');
    INSERT INTO @ScopeSeed (ProgramId, ScopeType, Description) VALUES
    (90, N'Subject', N'ריכוז נתונים רלוונטיים ודיווח (אחת לחודש), בהתאם לבקשות ולהחלטות האגף ולדרישות המכרז.'),
    (90, N'Subject', N'ריכוז/ נתונים- מסמך נהלים'),
    (90, N'Subject', N'ריכוז/ נתונים- תיקי תלמידים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים-  הזנה -כתבי התחייבות רשויות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים-  מחקר אינטרנטי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים-  ספרות מחקר'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים-  פרקטיקות ופיתוח מקצועי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים אמצע שנה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים סוף שנה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- איסוף ציונים תחילת שנה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- אקדמיה בתיכון'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- אתיופים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בגרות איכותית'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בדיקות יציאה מהארץ - גחלת'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בניית טופס בקשה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בניית טופס תעדוף'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקרת הצטיידות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות גריעה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בקשות פטור'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בתי ספר במיקוד'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- בתי ספר פורצי דרך'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוח תכנון תקציבי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דוחות ביצוע כוח אדם'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- דשבורד מחלקתי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה  ריכוז פניות וחריגות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- בקרה ופיקוח'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה -מחוז דרום'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה -מחוז חיפה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- מחוז חרדי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- מחוז ירושלים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה -מחוז מרכז'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה- מחוז צפון'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים  ניצנים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים  ניצנים-רשות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים יוח"א'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -לבטח'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -מגזר בדואי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -מגזר חרדי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -מגזר ערבי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים מחטים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -סקר שביעות רצון'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -פילוט חינוך מיוחד (חנ"מ)'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה מס לומדים -קליטת עליה (קלע)'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה נתונים תקציביים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הזנה ספקים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית התוכנית הלאומית'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה ישובית קבסנט'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית התוכנית הלאומית'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מוסדית קבסנט'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית התוכנית הלאומית'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעה מחוזית קבסנט'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הטמעת מערכת קבסנט'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- הסמכות טכנולוגיות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- זכאות יוצאי אתיופיה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- זכאות כללי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- זכאות כפרי נוער'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- חדרי מל"א'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- חינוך טכנולוגי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- יוח"א- יום חינוך ארוך ולימודי העשרה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כיתות שח"ר'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתבי התחייבות רשויות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- כתיבה ופיתוח מקצועי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מועדוניות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מח"טים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז דרום'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז התישבותי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חיפה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז חרדי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז ירושלים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז מרכז'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחוז צפון'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מחולל דוחות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיגזר בדואי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי לרשות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי קבסים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מיפוי תלמידים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב נוכחות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מעקב ציונים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מערכת קבסנט'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מצבת כוח אדם'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מצגת תוכנית עבודה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי חירום'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכזי נוער'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים חברתיים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים לימודים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים רגשיים והתנהגותיים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- מרכיבים תקציבים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משאבים שונים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- משוב ובקרה מרכז לגיל הרך'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- נתוני זכאות אנגלית'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- נתוני זכאות מתמטיקה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- נתוני מקצוע אנגלית'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- נתוני מקצוע מתמטיקה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סדנאות הפגה חינוך טיפול'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סטטוס מנע'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סיכום פעילות חודשית'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- סקרים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- עוגנים ישובים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- עולים חדשים'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- פתיחת כיתות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- קידום נוער'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- קרבה א'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תגבורי חורף'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תיאורי מקרה בוחן'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תיק סיור'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תיקי סיור'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכלול התוכנית'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תכנון ימי הדרכה'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תלמידי טכנאות ובגרות (טו"ב)'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקצוב דיפרנציאלי'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב מחוזות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד נתונים- תקציב תוכניות'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד/בקרת נתונים- אורח חיים בריא'),
    (90, N'Subject', N'ריכוז/ניתוח/עיבוד/בקרת נתונים- ספקים'),
    (90, N'Subject', N'רכוז נתונים- פיתוח שאלון מודל הדרכה'),
    (90, N'Subject', N'תכנון יישום ובקרה של תוכנית שנתית, פרוייקטים ותוכניות חינוכיות במסגרת'),
    (90, N'Subject', N'תמיכה רגשית-קוגנטיבית בעובד במצבי התמודדות/קונפליקט ארגוניים/מקצועיים'),
    (90, N'DiscussionCode', N'דיון עם  צוות ההוראה'),
    (90, N'DiscussionCode', N'דיון עם יועץ'),
    (90, N'DiscussionCode', N'דיון עם מורה/ מגיד שיעור'),
    (90, N'DiscussionCode', N'דיון עם מנהל בית הספר'),
    (90, N'DiscussionCode', N'דיון עם מנחה מקצועי'),
    (90, N'DiscussionCode', N'דיון עם צוות ההדרכה'),
    (90, N'DiscussionCode', N'דיון עם צוות היגוי'),
    (90, N'DiscussionCode', N'דיון עם צוות הפיקוח'),
    (90, N'DiscussionCode', N'דיון עם צוות יישובי'),
    (90, N'DiscussionCode', N'דיון עם צוות מקצועי'),
    (90, N'DiscussionCode', N'דיון עם צוות עמיתים- אנשי מקצוע'),
    (90, N'DiscussionCode', N'דיון עם רכז התוכנית'),
    (90, N'Class', N'1'),
    (90, N'Class', N'10'),
    (90, N'Class', N'11'),
    (90, N'Class', N'12'),
    (90, N'Class', N'13'),
    (90, N'Class', N'14'),
    (90, N'Class', N'15'),
    (90, N'Class', N'2'),
    (90, N'Class', N'3'),
    (90, N'Class', N'4'),
    (90, N'Class', N'5'),
    (90, N'Class', N'6'),
    (90, N'Class', N'7'),
    (90, N'Class', N'8'),
    (90, N'Class', N'9'),
    (90, N'Class', N'אין דרישות'),
    (90, N'Class', N'בניית תוכנית התנהגותית'),
    (90, N'Class', N'בניית תוכנית לימודים'),
    (90, N'Class', N'הגדלת היקף שעות'),
    (90, N'Class', N'הכנסת שינויים בביצוע'),
    (90, N'Class', N'התאמת אוכלוסית היעד'),
    (90, N'Class', N'התאמת תוכנית חברתית'),
    (90, N'Class', N'התאמת תוכנית טיפולית'),
    (90, N'Class', N'התאמת תוכנית כיתתית'),
    (90, N'Class', N'התיחסות לסוכני שינוי נוספים'),
    (90, N'Class', N'למידת עמיתים'),
    (90, N'Class', N'ניצול שעות נכון'),
    (90, N'Class', N'סיורים לימודיים'),
    (90, N'Class', N'שיחה עם הכתה/קבוצה'),
    (90, N'LocalityDistrictNational', N'איגום משאבים'),
    (90, N'LocalityDistrictNational', N'אין דרישות'),
    (90, N'LocalityDistrictNational', N'גיבוש צוותים רב מקצועיים'),
    (90, N'LocalityDistrictNational', N'גיוס תרומות'),
    (90, N'LocalityDistrictNational', N'הגדלת תקציב'),
    (90, N'LocalityDistrictNational', N'הכנסת שינויים בביצוע'),
    (90, N'LocalityDistrictNational', N'הנחיית הורים'),
    (90, N'LocalityDistrictNational', N'הנחיית צוותים'),
    (90, N'LocalityDistrictNational', N'העצמת צוותי הוראה למשימה'),
    (90, N'LocalityDistrictNational', N'התאמת צוותי היגוי למשימה'),
    (90, N'LocalityDistrictNational', N'התיחסות לסוכני שינוי נוספים'),
    (90, N'LocalityDistrictNational', N'כינוס מועצה פדגוגית'),
    (90, N'LocalityDistrictNational', N'למידת  עמיתים'),
    (90, N'LocalityDistrictNational', N'סיורים לימודיים'),
    (90, N'LocalityDistrictNational', N'קיום השתלמויות'),
    (90, N'LocalityDistrictNational', N'שיתוף גורמים חיצוניים'),
    (90, N'LocalityDistrictNational', N'תאום עם תוכניות קיימות'),
    (90, N'GradeLevel', N'א'),
    (90, N'GradeLevel', N'ב'),
    (90, N'GradeLevel', N'ג'),
    (90, N'GradeLevel', N'ד'),
    (90, N'GradeLevel', N'ה'),
    (90, N'GradeLevel', N'ו'),
    (90, N'GradeLevel', N'ז'),
    (90, N'GradeLevel', N'ח'),
    (90, N'GradeLevel', N'חובה'),
    (90, N'GradeLevel', N'ט'),
    (90, N'GradeLevel', N'י'),
    (90, N'GradeLevel', N'יא'),
    (90, N'GradeLevel', N'יב'),
    (90, N'GradeLevel', N'שיעור א'),
    (90, N'GradeLevel', N'שיעור ב');

    IF EXISTS (SELECT 1 FROM dbo.Projects WHERE Id = 6)
    BEGIN
        INSERT INTO dbo.ProjectPrograms (ProjectId, ProgramId)
        SELECT DISTINCT 6, seed.ProgramId
        FROM @ScopeSeed seed
        JOIN dbo.Programs program ON program.Id = seed.ProgramId AND program.IsActive = 1
        WHERE NOT EXISTS (
            SELECT 1 FROM dbo.ProjectPrograms existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId
        );

        INSERT INTO dbo.ProjectProgramFrameworks (ProjectId, ProgramId, FrameworkId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.Frameworks lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'Framework'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramFrameworks existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.FrameworkId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramEducationalPrograms (ProjectId, ProgramId, EducationalProgramId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.EducationalPrograms lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'EducationalProgram'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramEducationalPrograms existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.EducationalProgramId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramDomains (ProjectId, ProgramId, DomainId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.Domains lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'Domain'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramDomains existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.DomainId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramSubjects (ProjectId, ProgramId, SubjectId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.Subjects lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'Subject'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramSubjects existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.SubjectId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramDiscussionCodes (ProjectId, ProgramId, DiscussionCodeId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.DiscussionCodes lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'DiscussionCode'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramDiscussionCodes existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.DiscussionCodeId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramClasses (ProjectId, ProgramId, ClassId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.SchoolClasses lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'Class'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramClasses existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.ClassId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramGradeLevels (ProjectId, ProgramId, GradeLevelId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.GradeLevels lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'GradeLevel'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramGradeLevels existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.GradeLevelId = lookupRow.Id
          );

        INSERT INTO dbo.ProjectProgramLocalityDistrictNationals (ProjectId, ProgramId, LocalityDistrictNationalId)
        SELECT DISTINCT 6, seed.ProgramId, lookupRow.Id
        FROM @ScopeSeed seed
        JOIN dbo.LocalityDistrictNationals lookupRow ON lookupRow.Description = seed.Description AND lookupRow.IsActive = 1
        WHERE seed.ScopeType = N'LocalityDistrictNational'
          AND NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramLocalityDistrictNationals existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId AND existing.LocalityDistrictNationalId = lookupRow.Id
          );
    END;

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260708103000_SeedProjectSixProgramScopeDefaults')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260708103000_SeedProjectSixProgramScopeDefaults', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260708104500_SeedProjectSixFrameworkScopeBySymbol')
BEGIN

    SET NOCOUNT ON;

    DECLARE @FrameworkSeed TABLE (ProgramId int NOT NULL, InstitutionSymbol nvarchar(32) NOT NULL);
    INSERT INTO @FrameworkSeed (ProgramId, InstitutionSymbol) VALUES
    (100, N'442087'),
    (100, N'715797'),
    (100, N'761379'),
    (100, N'540708'),
    (100, N'722132'),
    (100, N'361550'),
    (100, N'641225'),
    (100, N'672568'),
    (100, N'338277'),
    (100, N'141481'),
    (100, N'366864'),
    (100, N'580528032'),
    (100, N'39491'),
    (100, N'632216'),
    (100, N'657379'),
    (100, N'747337'),
    (100, N'541748'),
    (100, N'42516'),
    (100, N'540526'),
    (100, N'544379'),
    (100, N'541128'),
    (100, N'580338366'),
    (100, N'541854'),
    (100, N'10541201'),
    (100, N'361451'),
    (100, N'540963'),
    (100, N'541056'),
    (100, N'541102'),
    (100, N'541151'),
    (100, N'541185'),
    (100, N'541284'),
    (100, N'541631'),
    (100, N'541896'),
    (100, N'544247'),
    (100, N'55120'),
    (100, N'580085447'),
    (100, N'648410'),
    (100, N'544239'),
    (100, N'675934'),
    (100, N'346031'),
    (100, N'441774'),
    (100, N'140814'),
    (100, N'140921'),
    (100, N'141572'),
    (100, N'160366'),
    (100, N'346098'),
    (100, N'366880'),
    (100, N'580294437'),
    (100, N'633263'),
    (100, N'758193'),
    (100, N'580432375'),
    (100, N'140541'),
    (100, N'140673'),
    (100, N'140780'),
    (100, N'140798'),
    (100, N'141044'),
    (100, N'184093'),
    (100, N'27056'),
    (100, N'390590'),
    (100, N'53196'),
    (100, N'580026383'),
    (100, N'580319489'),
    (100, N'647206'),
    (100, N'722025'),
    (100, N'732081'),
    (100, N'745968'),
    (100, N'747584'),
    (100, N'711556'),
    (100, N'460162'),
    (100, N'160523'),
    (100, N'363879'),
    (100, N'234047'),
    (100, N'738575'),
    (100, N'676361'),
    (100, N'520317'),
    (100, N'580726313'),
    (100, N'140681'),
    (100, N'770719'),
    (100, N'440768'),
    (100, N'440800'),
    (100, N'580342921'),
    (100, N'722058'),
    (100, N'444604'),
    (97, N'148080'),
    (97, N'347047'),
    (97, N'348235'),
    (97, N'348243'),
    (97, N'342337'),
    (97, N'248112'),
    (97, N'247239'),
    (97, N'540617'),
    (97, N'448050'),
    (97, N'448316'),
    (97, N'800128'),
    (97, N'648337'),
    (97, N'378075'),
    (97, N'247155'),
    (97, N'248138'),
    (97, N'448134'),
    (97, N'448209'),
    (97, N'448019'),
    (97, N'478016'),
    (97, N'442566'),
    (97, N'448118'),
    (97, N'448183'),
    (97, N'249169'),
    (97, N'548016'),
    (97, N'573105'),
    (97, N'610006'),
    (97, N'800037'),
    (97, N'448340'),
    (97, N'248013'),
    (97, N'800094'),
    (97, N'248765'),
    (97, N'448167'),
    (97, N'648261'),
    (97, N'247221'),
    (97, N'660233'),
    (97, N'248641'),
    (97, N'338657'),
    (97, N'248146'),
    (97, N'247064'),
    (97, N'472332'),
    (97, N'800052'),
    (97, N'648345'),
    (97, N'800078'),
    (97, N'442822'),
    (97, N'247247'),
    (97, N'248575'),
    (97, N'249284'),
    (97, N'348060'),
    (97, N'800102'),
    (97, N'348227'),
    (97, N'248047'),
    (97, N'640797'),
    (97, N'648303'),
    (97, N'248070'),
    (97, N'248344'),
    (94, N'662296'),
    (94, N'662452'),
    (94, N'650028'),
    (94, N'148247'),
    (94, N'641407'),
    (94, N'714204'),
    (94, N'729871'),
    (94, N'540567'),
    (94, N'148155');

    IF EXISTS (SELECT 1 FROM dbo.Projects WHERE Id = 6)
    BEGIN
        INSERT INTO dbo.ProjectPrograms (ProjectId, ProgramId)
        SELECT DISTINCT 6, seed.ProgramId
        FROM @FrameworkSeed seed
        JOIN dbo.Programs program ON program.Id = seed.ProgramId AND program.IsActive = 1
        WHERE NOT EXISTS (
            SELECT 1 FROM dbo.ProjectPrograms existing
            WHERE existing.ProjectId = 6 AND existing.ProgramId = seed.ProgramId
        );

        INSERT INTO dbo.ProjectProgramFrameworks (ProjectId, ProgramId, FrameworkId)
        SELECT DISTINCT 6, seed.ProgramId, framework.Id
        FROM @FrameworkSeed seed
        JOIN dbo.Programs program ON program.Id = seed.ProgramId AND program.IsActive = 1
        JOIN dbo.Frameworks framework ON framework.IsActive = 1
          AND (
            framework.InstitutionSymbol = seed.InstitutionSymbol
            OR TRY_CONVERT(int, framework.InstitutionSymbol) = TRY_CONVERT(int, seed.InstitutionSymbol)
          )
        WHERE NOT EXISTS (
            SELECT 1 FROM dbo.ProjectProgramFrameworks existing
            WHERE existing.ProjectId = 6
              AND existing.ProgramId = seed.ProgramId
              AND existing.FrameworkId = framework.Id
        );
    END;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260708104500_SeedProjectSixFrameworkScopeBySymbol')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260708104500_SeedProjectSixFrameworkScopeBySymbol', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260720132512_AddProjectProgramLocalities')
BEGIN
    CREATE TABLE [ProjectProgramLocalities] (
        [ProjectId] int NOT NULL,
        [ProgramId] int NOT NULL,
        [LocalityId] int NOT NULL,
        CONSTRAINT [PK_ProjectProgramLocalities] PRIMARY KEY ([ProjectId], [ProgramId], [LocalityId]),
        CONSTRAINT [FK_ProjectProgramLocalities_Localities_LocalityId] FOREIGN KEY ([LocalityId]) REFERENCES [Localities] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260720132512_AddProjectProgramLocalities')
BEGIN
    CREATE INDEX [IX_ProjectProgramLocalities_LocalityId] ON [ProjectProgramLocalities] ([LocalityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260720132512_AddProjectProgramLocalities')
BEGIN

    INSERT INTO dbo.ProjectProgramLocalities (ProjectId, ProgramId, LocalityId)
    SELECT scopeRow.ProjectId, scopeRow.ProgramId, institution.LocalityId
    FROM dbo.ProjectProgramFrameworks scopeRow
    INNER JOIN dbo.Frameworks framework ON framework.Id = scopeRow.FrameworkId
    INNER JOIN dbo.Institutions institution
        ON institution.InstitutionSymbol = TRY_CONVERT(int, framework.InstitutionSymbol)
    INNER JOIN dbo.Localities locality
        ON locality.Id = institution.LocalityId
       AND locality.IsActive = 1
    WHERE institution.LocalityId IS NOT NULL
    GROUP BY scopeRow.ProjectId, scopeRow.ProgramId, institution.LocalityId;

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260720132512_AddProjectProgramLocalities')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260720132512_AddProjectProgramLocalities', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN

    IF EXISTS (
      SELECT 1
      FROM Reports
      WHERE IsArchived = 0
      GROUP BY UserId, ReportingMonthId
      HAVING COUNT_BIG(*) > 1
    )
      THROW 51000, 'Migration stopped: duplicate active reports exist for an employee/month.', 1;

    IF EXISTS (
      SELECT 1
      FROM Institutions
      GROUP BY InstitutionSymbol
      HAVING COUNT_BIG(*) > 1
    )
      THROW 51001, 'Migration stopped: duplicate institution numbers exist globally. Review the read-only duplicate report before applying.', 1;

    IF EXISTS (
      SELECT 1
      FROM Frameworks
      GROUP BY LTRIM(RTRIM(InstitutionSymbol)), EducationalStageId
      HAVING COUNT_BIG(*) > 1
    )
      THROW 51002, 'Migration stopped: duplicate normalized framework symbols exist in the current educational-stage scope.', 1;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN
    DROP INDEX [IX_Reports_UserId_ReportingMonthId] ON [Reports];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN
    DROP INDEX [IX_Institutions_InstitutionSymbol_EducationalStageId] ON [Institutions];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN
    DROP INDEX [IX_Frameworks_InstitutionSymbol_EducationalStageId] ON [Frameworks];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Institutions]') AND [c].[name] = N'InstitutionSymbol');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Institutions] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Institutions] ALTER COLUMN [InstitutionSymbol] nvarchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Reports_UserId_ReportingMonthId] ON [Reports] ([UserId], [ReportingMonthId]) WHERE [IsArchived] = 0');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN
    CREATE UNIQUE INDEX [IX_Institutions_InstitutionSymbol] ON [Institutions] ([InstitutionSymbol]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN
    CREATE UNIQUE INDEX [IX_Frameworks_InstitutionSymbol_EducationalStageId] ON [Frameworks] ([InstitutionSymbol], [EducationalStageId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260807113407_PreserveInstitutionSymbolsAndActiveReportUniqueness', N'6.0.36');
END;
GO

COMMIT;
GO


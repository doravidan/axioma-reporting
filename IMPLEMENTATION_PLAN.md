# Employee Activity Reporting System - Implementation Plan

**Reference:** [SPEC.md](SPEC.md)

---

## Phase 0: Project Setup & Infrastructure

### 0.1 Solution Architecture

```
┌─────────────────────────────────────────────────────┐
│                    Client (Browser)                  │
│  ┌───────────────────────────────────────────────┐  │
│  │ Responsive Web UI (ASP.NET Core MVC/Razor)    │  │
│  │  - Desktop optimized                          │  │
│  │  - Smartphone optimized (Excel upload)        │  │
│  └───────────────────────────────────────────────┘  │
└──────────────────────┬──────────────────────────────┘
                       │ HTTPS (SSL)
┌──────────────────────▼──────────────────────────────┐
│                   Web Server (IIS)                   │
│  ┌───────────────────────────────────────────────┐  │
│  │            ASP.NET Core Web API                │  │
│  │  - Authentication & Authorization             │  │
│  │  - Business Logic Layer                       │  │
│  │  - Validation Engine                          │  │
│  │  - Excel Import/Export (EPPlus/ClosedXML)     │  │
│  │  - Email Service (SMTP)                       │  │
│  │  - Background Services (Hosted Services)      │  │
│  └───────────────────────────────────────────────┘  │
└──────────────────────┬──────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────┐
│              SQL Server Express Database             │
│  - All lookup tables                                │
│  - Employee/User data                               │
│  - Allocations                                      │
│  - Reports & report rows                            │
│  - Audit trail                                      │
│  - Password history                                 │
│  - System constants                                 │
└─────────────────────────────────────────────────────┘
```

### 0.2 Project Structure

```
AxiomaReporting/
├── AxiomaReporting.sln
├── src/
│   ├── AxiomaReporting.Web/              # ASP.NET Core MVC app with Razor Views + JavaScript
│   │   ├── Controllers/
│   │   ├── Views/ (or Pages/)
│   │   ├── wwwroot/
│   │   │   ├── css/
│   │   │   ├── js/
│   │   │   └── images/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   ├── AxiomaReporting.Core/             # Domain models, interfaces, DTOs
│   │   ├── Entities/
│   │   ├── Interfaces/
│   │   ├── DTOs/
│   │   └── Enums/
│   ├── AxiomaReporting.Infrastructure/   # EF Core, repositories, services
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs
│   │   │   └── Migrations/
│   │   ├── Repositories/
│   │   ├── Services/
│   │   └── BackgroundJobs/
│   └── AxiomaReporting.Tests/
│       ├── Unit/
│       └── Integration/
├── database/
│   ├── seed-data/                        # Initial Excel files from client
│   └── scripts/                          # SQL scripts, stored procedures
└── docs/
    ├── SPEC.md
    └── IMPLEMENTATION_PLAN.md
```

### 0.3 Initial Setup Tasks

| Task | Details |
|------|---------|
| Create .NET solution | `dotnet new sln`, add projects |
| Configure SQL Server Express | Install, create database `AxiomaReporting` |
| Set up EF Core | Add DbContext, configure connection string |
| Configure SSL | Obtain and install SSL certificate |
| Set up domain | Client purchases domain; configure DNS |
| Set up email | Dedicated email address for system notifications |
| Set up SMS provider (optional) | Not used for TFA. Only needed if the client later approves SMS reminders |
| Configure firewall | Server security setup |

---

### 0.4 Client Data File Handling

The provided client workbooks are implemented through two separate paths because the formats differ.

| File | Format | Implementation | Status |
|------|--------|----------------|--------|
| `database/seed-data/טבלאות.xlsb` | XLSB | Admin data-migration action `ImportClientLookupXlsb`; seed script remains available | Implemented |
| `database/seed-data/BASE DATA.xlsb` | XLSB | One-time Python seed script `database/seed-data/seed_reports.py`; assigns `AllocationId` when unambiguous | Implemented |
| `database/seed-data/קובץ משותף שאלונים לכל התוכניות 12.3.26.xlsx` | XLSX | Admin data-migration action `ImportQuestionnaireCatalog` reads `כללי - מאוחד`, including column H conclusion framework values | Implemented |

The normalized MVC admin upload screens accept `.xlsx` only. The supplied `טבלאות.xlsb` file has a dedicated browser upload path because its sheet layout is client-specific. The historical `BASE DATA.xlsb` file stays as a controlled seed script because it creates approved historical reports.

Detailed mapping is documented in `DATA_IMPORT_MAPPING.md`.

---

## Phase 1: Database Schema & Entity Framework

### 1.1 Lookup Tables (create ALL before anything else)

Each lookup table follows a common pattern. Create a generic base and then specific tables.

#### Base Pattern

```sql
CREATE TABLE [dbo].[LookupTableName] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Description] NVARCHAR(500) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL
);
```

#### Tables to Create

| # | Table Name | Extra Columns Beyond Base | Notes |
|---|-----------|--------------------------|-------|
| 1 | Districts (מחוזות) | — | |
| 2 | Sectors (מגזרים) | — | |
| 3 | Localities (ישובים) | `NationalCode INT` | Code from national table |
| 4 | Authorities (רשויות) | — | |
| 5 | Projects (פרויקטים) | — | |
| 6 | Programs (תוכניות) | — | |
| 7 | EducationalPrograms (תוכניות חינוכיות) | — | |
| 8 | Subjects (נושאים) | — | |
| 9 | Domains (תחומים) | — | |
| 10 | Frameworks (מסגרות) | `InstitutionSymbol NVARCHAR(100) NOT NULL`, `EducationalStageId INT NULL` | Unique per educational stage |
| 11 | SchoolClasses (כיתות) | — | UI label can remain "Classes" |
| 12 | GradeLevels (שכבות) | — | |
| 13 | EmployeeRoles (תפקידים) | — | Employee roles (Teacher, etc.) |
| 14 | EducationalStages (שלבי חינוך) | — | |
| 15 | EducationTypes (סוגי חינוך) | — | |
| 16 | LocalityDistrictNationals (איתור ישוב/מחוז/ארצי) | — | |
| 17 | DiscussionCodes (קוד דיון) | — | |

#### Institutions Table (Complex)

```sql
CREATE TABLE [dbo].[Institutions] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [InstitutionSymbol] INT NOT NULL,
    [Name] NVARCHAR(500) NOT NULL,
    [LocalityId] INT FOREIGN KEY REFERENCES Localities(Id),
    [DistrictId] INT FOREIGN KEY REFERENCES Districts(Id),
    [SectorId] INT FOREIGN KEY REFERENCES Sectors(Id),
    [TypeId] INT FOREIGN KEY REFERENCES EducationTypes(Id),
    [EducationalStageId] INT FOREIGN KEY REFERENCES EducationalStages(Id),
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL,
    CONSTRAINT UQ_Institution_Symbol_Stage UNIQUE (InstitutionSymbol, EducationalStageId)
);
```

### 1.2 System Tables

#### Report Statuses

```sql
CREATE TABLE [dbo].[ReportStatuses] (
    [Id] INT PRIMARY KEY,  -- NOT identity, predefined values
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(500)
);

-- Seed data:
-- 1 = Draft (טיוטא)
-- 2 = In Entry (בהזנה)
-- 3 = Pending Approval (ממתין לאישור)
-- 4 = Approved (מאושר)
-- 5 = Returned for Correction (הוחזר לתיקון)
-- 6 = Locked (נעול)
```

#### User Statuses

```sql
CREATE TABLE [dbo].[UserStatuses] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL
);

-- Seed: 1=Active, 2=Inactive, 3=Locked
```

#### User Roles/Levels

```sql
CREATE TABLE [dbo].[UserRoles] (
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(500)
);

-- Seed:
-- 1 = System Administrator
-- 2 = Project Manager
-- 3 = Project Coordinator
-- 4 = Inspector — View Only
-- 5 = Inspector — Activity Approval
-- 6 = Employee
```

#### System Constants

```sql
CREATE TABLE [dbo].[SystemConstants] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Key] NVARCHAR(200) NOT NULL UNIQUE,
    [Value] NVARCHAR(1000) NOT NULL,
    [Description] NVARCHAR(500),
    [UpdatedAt] DATETIME2 NULL,
    [UpdatedBy] INT NULL
);

-- Seed:
-- ReminderIntervalDays = 3
-- ReminderStartDaysBeforeDeadline = 7
-- NotesSimilarityThresholdPercent = 90
-- MaxDailyHoursDefault = 9
```

#### Email Server Settings

```sql
CREATE TABLE [dbo].[EmailServerSettings] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [SmtpServer] NVARCHAR(500) NOT NULL,
    [Port] INT NOT NULL,
    [Username] NVARCHAR(500) NOT NULL,
    [Password] NVARCHAR(500) NOT NULL, -- Encrypted
    [FromAddress] NVARCHAR(500) NOT NULL,
    [FromName] NVARCHAR(500),
    [UseSsl] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL
);
```

#### Email Templates

```sql
CREATE TABLE [dbo].[EmailTemplates] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [TypeDescription] NVARCHAR(200) NOT NULL,
    [Subject] NVARCHAR(500) NOT NULL,
    [Body] NVARCHAR(MAX) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1
);

-- Seed: ReportReceived, ReportApproved, ReportRejected, ReminderNotSubmitted, ReminderNeedsCorrection
```

### 1.3 Core Business Tables

#### Users/Employees

```sql
CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,        -- מספר משתמש (auto, read-only)
    [EmployeeCode] NVARCHAR(50) NOT NULL,       -- קוד עובד
    [IdNumber] NVARCHAR(20) NOT NULL UNIQUE,    -- ת.ז (also username)
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [PasswordHash] NVARCHAR(500) NOT NULL,
    [RoleId] INT NOT NULL FOREIGN KEY REFERENCES EmployeeRoles(Id), -- תפקיד (Teacher, etc.)
    [UserRoleId] INT NOT NULL FOREIGN KEY REFERENCES UserRoles(Id), -- System role (Admin, etc.)
    [StatusId] INT NOT NULL FOREIGN KEY REFERENCES UserStatuses(Id),
    [IsReportingEmployee] BIT NOT NULL DEFAULT 0,  -- עובד מדווח
    [RestDay] INT NULL,                            -- יום מנוחה (0=Sunday...6=Saturday)
    [AllowFutureReporting] BIT NOT NULL DEFAULT 0, -- דיווח עתידי
    [Notes] NVARCHAR(1000) NULL,
    [Email] NVARCHAR(500) NULL,
    [Phone] NVARCHAR(50) NULL,
    [MustChangePassword] BIT NOT NULL DEFAULT 1,
    [FailedLoginAttempts] INT NOT NULL DEFAULT 0,
    [LastPasswordChange] DATETIME2 NULL,
    [AcceptedTermsOfUse] BIT NOT NULL DEFAULT 0,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL,
    [CreatedBy] INT NULL,
    [UpdatedBy] INT NULL
);
```

#### Password History

```sql
CREATE TABLE [dbo].[PasswordHistories] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    [PasswordHash] NVARCHAR(500) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE()
);
```

#### Password Reset Tokens

```sql
CREATE TABLE [dbo].[PasswordResetTokens] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    [TokenHash] NVARCHAR(128) NOT NULL UNIQUE,
    [ExpiresAt] DATETIME2 NOT NULL,
    [UsedAt] DATETIME2 NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_PasswordResetTokens_UserId_ExpiresAt
ON [dbo].[PasswordResetTokens] ([UserId], [ExpiresAt]);
```

#### Two-Factor Codes

```sql
CREATE TABLE [dbo].[TwoFactorCodes] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    [CodeHash] NVARCHAR(128) NOT NULL,
    [ExpiresAt] DATETIME2 NOT NULL,
    [UsedAt] DATETIME2 NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_TwoFactorCodes_UserId_ExpiresAt
ON [dbo].[TwoFactorCodes] ([UserId], [ExpiresAt]);
```

#### Allocations

```sql
CREATE TABLE [dbo].[Allocations] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    [ProjectId] INT NOT NULL FOREIGN KEY REFERENCES Projects(Id),
    [AnnualEmploymentScope] DECIMAL(18,4) NULL,   -- היקף העסקה שנתי
    [MonthlyEmploymentScope] DECIMAL(18,4) NULL,   -- היקף העסקה חודשי
    [DailyEmploymentScope] DECIMAL(18,4) NULL,     -- היקף העסקה יומי (NULL = unlimited)
    [MonthlyRowAllocation] INT NULL,               -- הקצאת שורות חודשית
    [AnnualRowAllocation] INT NULL,                -- הקצאת שורות שנתית
    [OutputDuration] NVARCHAR(500) NULL,           -- משך תפוקה (comma-separated values)
    [AllowExcelUpload] BIT NOT NULL DEFAULT 0,
    [Notes] NVARCHAR(1000) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL,
    CONSTRAINT UQ_Allocation_User_Project UNIQUE (UserId, ProjectId)
);
```

**Allocation cardinality decision**: An employee can have multiple allocations across projects, but only one allocation per `(UserId, ProjectId)`. If an employee has more than one allocation, the report form must show a project/allocation selector before loading allocation-scoped dropdowns.

#### Allocation-Lookup Junction Tables (Many-to-Many)

For each lookup that can be multi-selected per allocation:

```sql
-- Pattern repeated for every allocation-scoped lookup used by report dropdowns:
-- Districts, Programs, Sectors, Localities, Frameworks, Subjects, Domains,
-- EducationalPrograms, SchoolClasses, GradeLevels, DiscussionCodes,
-- LocalityDistrictNationals.

CREATE TABLE [dbo].[AllocationDistricts] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [DistrictId] INT NOT NULL FOREIGN KEY REFERENCES Districts(Id),
    PRIMARY KEY (AllocationId, DistrictId)
);

CREATE TABLE [dbo].[AllocationPrograms] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [ProgramId] INT NOT NULL FOREIGN KEY REFERENCES Programs(Id),
    PRIMARY KEY (AllocationId, ProgramId)
);

CREATE TABLE [dbo].[AllocationSectors] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [SectorId] INT NOT NULL FOREIGN KEY REFERENCES Sectors(Id),
    PRIMARY KEY (AllocationId, SectorId)
);

CREATE TABLE [dbo].[AllocationLocalities] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [LocalityId] INT NOT NULL FOREIGN KEY REFERENCES Localities(Id),
    PRIMARY KEY (AllocationId, LocalityId)
);

CREATE TABLE [dbo].[AllocationFrameworks] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [FrameworkId] INT NOT NULL FOREIGN KEY REFERENCES Frameworks(Id),
    PRIMARY KEY (AllocationId, FrameworkId)
);

CREATE TABLE [dbo].[AllocationSubjects] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [SubjectId] INT NOT NULL FOREIGN KEY REFERENCES Subjects(Id),
    PRIMARY KEY (AllocationId, SubjectId)
);

CREATE TABLE [dbo].[AllocationDomains] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [DomainId] INT NOT NULL FOREIGN KEY REFERENCES Domains(Id),
    PRIMARY KEY (AllocationId, DomainId)
);

CREATE TABLE [dbo].[AllocationEducationalPrograms] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [EducationalProgramId] INT NOT NULL FOREIGN KEY REFERENCES EducationalPrograms(Id),
    PRIMARY KEY (AllocationId, EducationalProgramId)
);

CREATE TABLE [dbo].[AllocationClasses] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [ClassId] INT NOT NULL FOREIGN KEY REFERENCES SchoolClasses(Id),
    PRIMARY KEY (AllocationId, ClassId)
);

CREATE TABLE [dbo].[AllocationGradeLevels] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [GradeLevelId] INT NOT NULL FOREIGN KEY REFERENCES GradeLevels(Id),
    PRIMARY KEY (AllocationId, GradeLevelId)
);

CREATE TABLE [dbo].[AllocationDiscussionCodes] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [DiscussionCodeId] INT NOT NULL FOREIGN KEY REFERENCES DiscussionCodes(Id),
    PRIMARY KEY (AllocationId, DiscussionCodeId)
);

CREATE TABLE [dbo].[AllocationLocalityDistrictNationals] (
    [AllocationId] INT NOT NULL FOREIGN KEY REFERENCES Allocations(Id),
    [LocalityDistrictNationalId] INT NOT NULL FOREIGN KEY REFERENCES LocalityDistrictNationals(Id),
    PRIMARY KEY (AllocationId, LocalityDistrictNationalId)
);
```

#### Reporting Months

```sql
CREATE TABLE [dbo].[ReportingMonths] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Description] NVARCHAR(500) NOT NULL,
    [Month] INT NOT NULL,              -- 1-12
    [Year] INT NOT NULL,
    [LastReportingDate] DATETIME2 NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 0,
    [AllowFutureReporting] BIT NOT NULL DEFAULT 0,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL
);
```

#### Reports (Monthly report per employee)

```sql
CREATE TABLE [dbo].[Reports] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    [ReportingMonthId] INT NOT NULL FOREIGN KEY REFERENCES ReportingMonths(Id),
    [StatusId] INT NOT NULL FOREIGN KEY REFERENCES ReportStatuses(Id),
    [SubmittedAt] DATETIME2 NULL,
    [ApprovedAt] DATETIME2 NULL,
    [ApprovedBy] INT NULL FOREIGN KEY REFERENCES Users(Id),
    [RejectionReason] NVARCHAR(1000) NULL,
    [RejectedAt] DATETIME2 NULL,
    [RejectedBy] INT NULL FOREIGN KEY REFERENCES Users(Id),
    [ImportedFromExcel] BIT NOT NULL DEFAULT 0,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL,
    CONSTRAINT UQ_UserReportMonth UNIQUE (UserId, ReportingMonthId)
);
```

#### Report Rows (Individual activity lines)

```sql
CREATE TABLE [dbo].[ReportRows] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [ReportId] INT NOT NULL FOREIGN KEY REFERENCES Reports(Id),
    [AllocationId] INT NULL FOREIGN KEY REFERENCES Allocations(Id), -- nullable for migration/backfill; required for new rows
    [SequenceNumber] INT NOT NULL,           -- מס"ד - auto per employee per report
    [MeetingDate] DATETIME2 NOT NULL,        -- תאריך המפגש
    [MeetingDuration] DECIMAL(18,4) NOT NULL, -- משך המפגש (hours, decimal)
    [DistrictId] INT NOT NULL FOREIGN KEY REFERENCES Districts(Id),
    [LocalityId] INT NOT NULL FOREIGN KEY REFERENCES Localities(Id),
    [FrameworkId] INT NOT NULL FOREIGN KEY REFERENCES Frameworks(Id),
    [EducationalProgramId] INT NOT NULL FOREIGN KEY REFERENCES EducationalPrograms(Id),
    [DomainId] INT NOT NULL FOREIGN KEY REFERENCES Domains(Id),
    [Subject1Id] INT NOT NULL FOREIGN KEY REFERENCES Subjects(Id),
    [Subject2Id] INT NULL FOREIGN KEY REFERENCES Subjects(Id),
    [DiscussionCodeId] INT NULL FOREIGN KEY REFERENCES DiscussionCodes(Id),
    [ConclusionClassId] INT NULL FOREIGN KEY REFERENCES SchoolClasses(Id),
    [ConclusionFrameworkId] INT NULL FOREIGN KEY REFERENCES Frameworks(Id), -- Educational framework conclusion
    [ConclusionLocationId] INT NULL FOREIGN KEY REFERENCES LocalityDistrictNationals(Id), -- Locality/District/National conclusion
    [GradeLevelId] INT NULL FOREIGN KEY REFERENCES GradeLevels(Id),
    [ClassId] INT NULL FOREIGN KEY REFERENCES SchoolClasses(Id),
    [Notes] NVARCHAR(2000) NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UpdatedAt] DATETIME2 NULL
);
```

**Inspector assignment logic**: Within one assignment row, all non-null dimensions are combined with AND. NULL means wildcard for that dimension. Multiple rows for the same inspector are OR/unioned.

#### Document Attachments

```sql
CREATE TABLE [dbo].[DocumentAttachments] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NULL FOREIGN KEY REFERENCES Users(Id),          -- Employee-level attachment
    [ReportRowId] INT NULL FOREIGN KEY REFERENCES ReportRows(Id), -- Row-level attachment
    [FileName] NVARCHAR(500) NOT NULL,
    [FilePath] NVARCHAR(1000) NOT NULL,
    [FileSize] BIGINT NOT NULL,
    [MimeType] NVARCHAR(200) NOT NULL,
    [UploadedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [UploadedBy] INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
);
```

#### Inspector-Employee Group Mapping

```sql
CREATE TABLE [dbo].[InspectorAssignments] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [InspectorUserId] INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    [ProgramId] INT NULL FOREIGN KEY REFERENCES Programs(Id),
    [DistrictId] INT NULL FOREIGN KEY REFERENCES Districts(Id),
    [SectorId] INT NULL FOREIGN KEY REFERENCES Sectors(Id)
);
```

#### Reminder Logs

```sql
CREATE TABLE [dbo].[ReminderLogs] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    [ReportingMonthId] INT NULL FOREIGN KEY REFERENCES ReportingMonths(Id),
    [TemplateType] NVARCHAR(100) NOT NULL,
    [SentAt] DATETIME2 NOT NULL
);

CREATE INDEX IX_ReminderLogs_User_Month_Template_SentAt
ON [dbo].[ReminderLogs] ([UserId], [ReportingMonthId], [TemplateType], [SentAt]);
```

### 1.4 EF Core Setup

| Task | Details |
|------|---------|
| Create entity classes | One class per table in `Core/Entities/` |
| Configure relationships | Fluent API in `AppDbContext.OnModelCreating` |
| Create initial migration | `dotnet ef migrations add InitialCreate` |
| Seed system data | Statuses, EmployeeRoles, UserRoles, default admin user |
| Create repository interfaces | In `Core/Interfaces/` |
| Implement repositories | In `Infrastructure/Repositories/` |

---

## Phase 2: Authentication & Authorization

### 2.1 Tasks

| # | Task | Details | Acceptance Criteria |
|---|------|---------|-------------------|
| 1 | Login page | Branded with client logo, ID + password fields | Logo displays, fields validate, RTL layout |
| 2 | Password hashing | BCrypt or Argon2 | Passwords never stored in plain text |
| 3 | Login logic | Validate credentials, check lockout, check status | Correct error messages for each scenario |
| 4 | Failed attempt tracking | Increment counter; lock after 3 failures | Account locked after 3 wrong attempts |
| 5 | Password expiry check | Compare `LastPasswordChange` + 90 days | Force change if expired |
| 6 | First-login flow | Show Terms of Use → Force password change | Cannot proceed without accepting + changing |
| 7 | Password change screen | Validate: min 8 chars, letters+digits, not in last 5 | Clear validation messages |
| 8 | Password history | Store last 5 hashes; reject reuse | Cannot reuse last 5 passwords |
| 9 | Forgot password screen | Send reset link/code to email | Secure token, time-limited |
| 10 | TFA (optional) | Email code after password auth, controlled by `TfaEmailEnabled` | Code sent and verified correctly |
| 11 | Role-based authorization | `[Authorize(Roles = "...")]` on all controllers | Each role sees only permitted pages/actions |
| 12 | Session management | Secure cookies, timeout | Sessions expire appropriately |
| 13 | Admin password reset | Admin/PM can reset passwords for coordinators | Reset to default (ID number), force change on next login |

### 2.2 Authorization Matrix (implement as middleware/policy)

| Action | Admin | PM | Coordinator | Inspector-View | Inspector-Approve | Employee |
|--------|-------|-----|-------------|----------------|-------------------|----------|
| Manage lookup tables | CRUD | View | View | - | - | - |
| Create users | Yes | Yes | Yes | - | - | - |
| Edit users | Yes | Yes | - | - | - | - |
| Delete users | Yes | - | - | - | - | - |
| Create allocations | Yes | Yes | Yes | - | - | - |
| Open salary months | Yes | Yes | - | - | - | - |
| View all reports | Yes | Yes | Yes | Scoped | Scoped | Own only |
| Edit any report | Yes | Yes | Non-approved only | - | - | Own only |
| Approve reports | Yes | Yes | Yes | - | Yes (scoped) | - |
| Reject reports | Yes | Yes | Yes | - | Yes (scoped) | - |
| Export Excel | Yes | Yes | Yes | Approved only | Approved only | - |
| Manage system tables | Yes | - | - | - | - | - |
| Bulk operations | Yes | Yes | - | - | - | - |
| Upload Excel (any employee) | Yes | Yes | - | - | - | - |

---

## Phase 3: Lookup Table Management (Admin Module)

### 3.1 Generic Lookup Table CRUD

Build a **reusable component** since 17+ tables share the same pattern.

| # | Task | Details |
|---|------|---------|
| 1 | Generic list view | Paginated table with search, sort, scroll |
| 2 | Add record | Modal or inline form; auto-generate code |
| 3 | Edit record | Inline or modal editing |
| 4 | Delete record | Check if in use → block with message; if unused → confirm dialog |
| 5 | Excel import | Bulk upload from Excel file per table |
| 6 | Delete icon | Use trash can icon (not text button) |

### 3.2 Special Table: Frameworks (מסגרות)

Additional logic:
- Validate institution symbol uniqueness.
- Institution symbol + educational stage must be unique combination.
- Display error if duplicate symbol attempted.

### 3.3 Special Table: Institutions (מוסדות)

Complex form with multiple foreign key dropdowns (Locality, District, Sector, Type, Educational Stage).

### 3.4 Special Table: Reporting Months (חודשי דיווח)

- Calendar picker for month/year and last reporting date.
- **Active month toggle**: Only ONE month can be active. Activating one deactivates the previous.
- Future reporting flag per month.
- Default last reporting date = fixed day in following month.
- Permissions exception: System Admin and Project Manager can create/edit/open/activate reporting months. Project Coordinator has view-only access unless separately granted.

### 3.5 System Tables (restricted)

- Email Server Settings: form with SMTP config fields.
- Email Templates: rich-text editor for message body; personalization tokens ({EmployeeName}, etc.).
- System Constants: key-value editor; edit only, no delete.
- Report Statuses / User Statuses / User Roles: **read-only in UI** — changes only by developer.

---

## Phase 4: Employee & Allocation Management

### 4.1 Employee Card (Blue Card)

| # | Task | Details | Validation |
|---|------|---------|------------|
| 1 | Create screen layout | Two-panel: Blue (employee) + Green (allocation) — separate screens | Responsive, RTL |
| 2 | User Number field | Auto-generated, grayed out, read-only | Cannot be edited |
| 3 | Employee Code field | Numeric input | Required |
| 4 | Name fields | First + Last name text inputs | Required |
| 5 | ID Number field | Text input, used as username | Required, unique |
| 6 | Role dropdown | From EmployeeRoles lookup table | Required |
| 7 | Reporting Employee checkbox | Toggle showing/hiding reporting panels | Conditional UI |
| 8 | Password field | Hidden (dots/asterisks) | Never visible |
| 9 | Notes field | Free text textarea | Optional |
| 10 | Rest Day selector | Dropdown (Sunday-Saturday) | Used in validation |
| 11 | Future Reporting checkbox | Boolean | Affects date validation |
| 12 | Status dropdown | Active/Inactive/Locked | Required |
| 13 | Employee-level documents | Upload/list/delete employee attachments with visual indicator | Stored in DocumentAttachments.UserId |

### 4.2 Allocation Details (Green Card — separate screen)

| # | Task | Details | Validation |
|---|------|---------|------------|
| 1 | Project dropdown | From Projects table | Per allocation |
| 2 | District multi-select | From Districts table | Multi-select allowed |
| 3 | Program multi-select | From Programs table | Multi-select allowed |
| 4 | Sector multi-select | From Sectors table | Multi-select allowed |
| 5 | Annual Employment Scope | Numeric input | Decimal |
| 6 | Monthly Employment Scope | Numeric input | Per agreement |
| 7 | Daily Employment Scope | Numeric input or "Unlimited" | Up to 9 or unlimited |
| 8 | Monthly Row Allocation | Numeric input | Used by monthly row-limit validation |
| 9 | Annual Row Allocation | Numeric input | Used by annual row-limit validation |
| 10 | Output Duration | Multi-select: 0.5, 1, 1.5, 2, 2.5, 3, Unlimited | Multi-select; display raw number without unit suffix |
| 11 | Allow Excel Upload | Checkbox | Boolean |
| 12 | Notes | Free text | Optional |

### 4.3 Allocation Assignment Panel

| # | Task | Details |
|---|------|---------|
| 1 | Table selector (right panel) | Dropdown to pick which lookup table to assign from |
| 2 | Value picker (middle panel) | Show available values from selected table |
| 3 | Add value | Click to add to employee's allocations |
| 4 | Remove value | Red X icon to remove |
| 5 | Initial data import | Import from client-provided Excel files |

### 4.4 Employee List Screen

| # | Task | Details |
|---|------|---------|
| 1 | List all employees | Paginated table with all Blue Card fields |
| 2 | Filter bar | Filter by any displayed column |
| 3 | Sort by columns | Click header to sort |
| 4 | Open employee card | Blue button per row |
| 5 | Add new employee | Button at top |
| 6 | Export to Excel | Export filtered results |
| 7 | Locked indicator | Show when employee is locked |
| 8 | Notes columns | Employee notes + Allocation notes |
| 9 | Bulk operations | Checkbox selection → Change status, Change allocation |
| 10 | Multi-value display | Handle displaying multiple sectors/districts per employee |
| 11 | View by project | Filter/group employees by project |

### 4.5 Allocation List Screen (separate)

- Displays user details + allocation details combined.
- Same filtering/sorting/export capabilities.

---

## Phase 5: Reporting — Online Form

### 5.1 Report Screen Layout

| # | Task | Details |
|---|------|---------|
| 1 | Month display | Auto-show the active (non-locked) salary month |
| 2 | Employee info header | ID, Name, Employee Code (read-only from employee card) |
| 3 | Report table | Editable grid with all 20 fields from Spec Section 10 plus persisted AllocationId context |
| 4 | Add row button | Add new report row |
| 5 | Delete row button | Remove a row |
| 6 | Save (Draft) button | Save partial report as "Draft" |
| 7 | Submit button | Submit for approval; changes status |
| 8 | Allocation summary | Show: total rows reported, total hours, remaining balance |

If the employee has more than one active allocation, show a project/allocation selector before report rows are edited. The selected allocation controls dropdown values, row limits, scopes, and the `ReportRows.AllocationId` value for new rows.

### 5.2 Field Implementation (per row)

| # | Field | Implementation | Notes |
|---|-------|---------------|-------|
| 1 | Serial Number | Auto-generated | Per employee, ascending by date |
| 2 | ID Number | Read-only from employee card | |
| 3 | Reporter Name | Read-only (Last + First) | Split display |
| 4 | Employee Code | Read-only | Updates if employee card changes |
| 5 | District | Dropdown filtered by allocation | Required |
| 6 | Locality | Dropdown filtered by allocation | Required |
| 7 | Framework Name | Dropdown filtered by allocation | Required |
| 8 | Meeting Date | Date picker or manual (YYYY/MM/DD) | Validated per rules |
| 9 | Meeting Duration | Numeric with decimal | Validated against monthly scope |
| 10 | Educational Program | Dropdown filtered by project allocation | Required |
| 11 | Domain | Dropdown filtered by project allocation | Required |
| 12 | Subject 1 | Dropdown filtered by allocation | Required |
| 13 | Subject 2 | Dropdown filtered by allocation | Optional |
| 14 | Discussion Held | Dropdown from closed list (NOT yes/no) | Optional |
| 15 | Conclusions — Class | Dropdown from SchoolClasses table | Optional |
| 16 | Conclusions — Ed. Framework | Dropdown | Optional |
| 17 | Conclusions — Location | Dropdown | Optional |
| 18 | Grade Level | Dropdown from Grade Levels | Optional |
| 19 | Class | Dropdown from SchoolClasses | Optional |
| 20 | Notes | Free text (unlimited) | Used in similarity check |

### 5.3 Validation Engine

Implement a dedicated `ReportValidationService` that runs ALL rules:

```
ReportValidationService
├── ValidateRequiredFields(row)
├── ValidateDate(row, employee)
│   ├── Must be in current or previous month
│   ├── Unless employee.AllowFutureReporting AND month.AllowFutureReporting
│   └── Cannot be employee's RestDay
├── ValidateMonthlyRowLimit(report, allocation)
├── ValidateDailyHourLimit(row, employee, existingRows)
│   └── Max 9 hours/day unless "Unlimited"
├── ValidateAnnualRowLimit(employee, allReports)
├── ValidateDuplicateRows(row, existingRows)
│   ├── Same date + same values + empty notes = DUPLICATE
│   └── Same date + same values + identical notes = DUPLICATE
├── ValidateSubmissionDeadline(report, reportingMonth)
│   └── Cannot submit after LastReportingDate
├── ValidateNotesSimilarity(row, existingRows, threshold)
│   └── Compare Notes using normalized Levenshtein similarity within the same report; flag if similarity > threshold%
└── ValidateMonthlyHours(report, allocation)
    └── Total hours ≤ MonthlyEmploymentScope (warn, don't block if under)
```

Notes similarity formula:

```
similarity = (1 - levenshteinDistance / maxLength) * 100
```

Compare only rows within the same report (same employee and salary month). This is character-based and works for Hebrew without language-specific tokenization. Default threshold is `NotesSimilarityThresholdPercent = 90`.

### 5.4 Status Transitions

| From | To | Triggered By | Side Effects |
|------|-----|-------------|-------------|
| (new) | Draft | Employee saves partial report | — |
| Draft | In Entry | Employee fills all required fields and saves | — |
| In Entry | Pending Approval | Employee submits | — |
| Pending Approval | Approved | Inspector/PM/Admin approves | Email to employee |
| Pending Approval | Returned for Correction | Inspector rejects | Email with reasons to employee |
| Returned for Correction | Pending Approval | Employee resubmits | — |
| Any | Any | Admin/PM override | No restrictions on status |

---

## Phase 6: Reporting — Excel Upload

### 6.1 Excel Template

| # | Task | Details |
|---|------|---------|
| 1 | Define Excel template | Columns matching all 20 report fields |
| 2 | Template download | Button to download blank template |
| 3 | Responsive upload screen | Mobile-friendly file picker |

### 6.2 Import Engine

| # | Task | Details |
|---|------|---------|
| 1 | Parse Excel file | Use EPPlus or ClosedXML library |
| 2 | Map columns to fields | Validate column structure |
| 3 | Resolve allocation context | Each row must resolve to exactly one AllocationId; ambiguous/missing allocation is a validation error |
| 4 | Run full validation | Same `ReportValidationService` as online form |
| 5 | Generate error report | List all validation errors with row numbers |
| 6 | PDF export of errors | Generate downloadable PDF with error list |
| 7 | Import valid data | Insert into ReportRows table with AllocationId |
| 8 | Overwrite logic | If report for same month exists (unapproved) → delete old data, import new |
| 9 | Success notification | On-screen message + confirmation email |

### 6.3 Rules

| Rule | Implementation |
|------|---------------|
| Employee upload = current month only | Check active ReportingMonth |
| PM can upload for locked months | Permission check bypasses month lock for PM role |
| Overwrite unapproved data | DELETE existing rows for same user+month if status != Approved |
| Post-import display | Data visible in online form after import |

---

## Phase 7: Dashboard & Reports

### 7.1 Dashboard Screen

| # | Task | Details |
|---|------|---------|
| 1 | Filter bar | All fields: District, Sector, Program, Employee Code, ID, Name, Status, Month range (from-to) |
| 2 | Cascading filters | Selecting a district filters all other dropdowns to show only related values |
| 3 | "Show" button | Table empty until clicked |
| 4 | Results table | All report rows matching filters |
| 5 | Export to Excel | Export filtered results |
| 6 | "Summary Screen" button | Navigate to approval screen |
| 7 | Page size selector | Left side of screen |
| 8 | Document attachment indicator | Visual indicator per row |
| 9 | Status filter | Reported / Not Yet Reported / All |
| 10 | Column sorting | Clickable headers |

### 7.2 Summary & Approval Screen

| # | Task | Details |
|---|------|---------|
| 1 | Summary rows | One row per employee: total rows, total hours, remaining balance |
| 2 | Approve button | Per row; changes status to Approved; sends email |
| 3 | Reject button | Opens popup for rejection reasons; sends email with reasons |
| 4 | Bulk approve | Checkbox: Select All / Deselect All / Multi-select |
| 5 | Not-reported view | Show employees who haven't submitted for the month |

### 7.3 Inspector Scoping

| # | Task | Details |
|---|------|---------|
| 1 | Scope filter | Inspector sees only employees matching their assigned program/district/sector |
| 2 | View-only inspector | Can export approved reports only |
| 3 | Approval inspector | Same scope + can approve/reject |
| 4 | Assignment rule semantics | Non-null fields within one InspectorAssignments row are AND; multiple rows are OR/union; NULL is wildcard |

---

## Phase 8: Background Services

### 8.1 Reminder Service

| # | Task | Details |
|---|------|---------|
| 1 | Implement `IHostedService` | .NET Background Service |
| 2 | Daily execution | Run once per day (configurable time) |
| 3 | Find unreported employees | Query for employees with no report for active month |
| 4 | Find rejected reports | Query for reports in "Returned for Correction" status |
| 5 | Check reminder schedule | Start Y days before deadline; send every X days |
| 6 | Send emails | Use Email Service with personalized templates |
| 7 | Logging | Log all sent reminders for audit |

### 8.2 Email Service

| # | Task | Details |
|---|------|---------|
| 1 | SMTP client | Configurable from EmailServerSettings table |
| 2 | Template engine | Replace tokens ({EmployeeName}, {MonthName}, {RejectionReason}) |
| 3 | Personalization | "Hello" + Employee Name prefix on all messages |
| 4 | Queue/retry | Handle transient SMTP failures |

---

## Phase 9: Excel Export

### 9.1 Export Capabilities

| Screen | What Gets Exported | Who Can Export |
|--------|-------------------|----------------|
| Employee List | Filtered employee list | Admin, PM, Coordinator |
| Allocation List | Filtered allocations | Admin, PM, Coordinator |
| Dashboard | Filtered report data | Admin, PM, Coordinator; Inspector (approved only) |
| Summary Screen | Summary data | Admin, PM, Coordinator |

### 9.2 Implementation

| # | Task | Details |
|---|------|---------|
| 1 | EPPlus/ClosedXML integration | Server-side Excel generation |
| 2 | Apply current filters | Export exactly what's displayed |
| 3 | Format columns | Proper types (dates, numbers, text) |
| 4 | RTL support | Hebrew text renders correctly |
| 5 | Download response | Browser download prompt |

---

## Phase 10: UI/UX & Responsive Design

### 10.1 Global UI Requirements

| # | Task | Details |
|---|------|---------|
| 1 | RTL layout | Full right-to-left support for Hebrew |
| 2 | Branded login | Client logo (SITE logo) |
| 3 | Top bar | "Hello, [Name]" (right), Home + Logout icons (left) |
| 4 | Role-based menu | Different menu items per role |
| 5 | Mobile responsive | Especially Excel upload screen |
| 6 | Consistent terminology | Apply all renamed terms from Spec Section 18 |
| 7 | Loading states | Spinners for async operations |
| 8 | Error messages | Clear, Hebrew-language error messages |
| 9 | Confirmation dialogs | For destructive actions (delete, overwrite) |

### 10.2 Accessibility

| # | Task |
|---|------|
| 1 | Keyboard navigation |
| 2 | Screen reader labels |
| 3 | Sufficient color contrast |
| 4 | Focus indicators |

### 10.3 Frontend Architecture Decision

- Use ASP.NET Core MVC with Razor Views and JavaScript/AJAX.
- Do not use Blazor.
- Use JSON endpoints for dynamic behaviors such as cascading filters, inline report-row editing, and bulk checkbox actions.
- For the 20-field interactive report grid, use a JavaScript table/grid approach such as DataTables.net or a lightweight equivalent, integrated with MVC controllers.

---

## Phase 11: Testing

### 11.1 Unit Tests

| Area | Tests |
|------|-------|
| Validation Engine | All 10 validation rules with edge cases |
| Password policy | Length, complexity, history, expiry |
| Authorization | Each role's permissions |
| Report status transitions | Valid/invalid transitions |
| Duplicate detection | Exact match, notes similarity |
| Date validation | Rest days, future dates, month boundaries |
| Hour calculations | Daily, monthly, annual limits |

### 11.2 Integration Tests

| Area | Tests |
|------|-------|
| Excel import | Valid file, invalid file, overwrite, error report |
| Email sending | Template rendering, SMTP delivery |
| Login flow | Success, lockout, first login, password change |
| Report workflow | Submit → Approve, Submit → Reject → Resubmit |
| Bulk operations | Multi-employee status change, bulk approval |

### 11.3 E2E Tests

| Scenario | Steps |
|----------|-------|
| Employee full cycle | Login → Fill report → Submit → Get approved |
| Excel upload cycle | Login → Upload → Validate → Import → View in online form |
| Admin cycle | Login → Create employee → Set allocations → Open month → View reports |
| Inspector cycle | Login → View scoped reports → Approve/Reject → Email verification |

---

## Phase 12: Data Migration & Initial Setup

### 12.1 Initial Data Load

| # | Task | Source |
|---|------|--------|
| 1 | Load all lookup tables | Client-provided Excel files |
| 2 | Load employee data | Client-provided Excel files |
| 3 | Load allocation data | Client-provided Excel files |
| 4 | Create admin user | Manual setup (ID + default password) |
| 5 | Configure email server | Client provides SMTP details |
| 6 | Set system constants | Default values, adjustable by admin |
| 7 | Load institutions data | Client-provided institutions table |
| 8 | Set up email templates | Default templates, editable by admin |

### 12.2 Data Import Tool

Build a one-time import tool that:
1. Reads client Excel files.
2. Maps columns to database fields.
3. Validates data integrity.
4. Inserts into database with proper foreign keys.
5. Generates import report (success/failure counts).

---

## Phase 13: Deployment

| # | Task | Details |
|---|------|---------|
| 1 | Server setup | Windows Server with IIS |
| 2 | SQL Server Express | Install and configure |
| 3 | SSL certificate | Install on domain |
| 4 | Deploy application | Publish .NET app to IIS |
| 5 | Configure firewall | Restrict access as needed |
| 6 | DNS configuration | Point client domain to server |
| 7 | Backup strategy | Database backup schedule |
| 8 | Monitoring | Application health monitoring |

---

## Implementation Order (Recommended)

| Sprint | Phase | Duration | Dependencies |
|--------|-------|----------|-------------|
| 1 | Phase 0: Setup + Phase 1: Database | Week 1-2 | None |
| 2 | Phase 2: Authentication | Week 3 | Phase 1 |
| 3 | Phase 3: Lookup Tables | Week 4 | Phase 1, 2 |
| 4 | Phase 4: Employee & Allocations | Week 5-6 | Phase 3 |
| 5 | Phase 5: Online Reporting | Week 7-8 | Phase 4 |
| 6 | Phase 6: Excel Upload | Week 9 | Phase 5 |
| 7 | Phase 7: Dashboard & Approvals | Week 10-11 | Phase 5 |
| 8 | Phase 8: Background Services | Week 12 | Phase 7 |
| 9 | Phase 9: Excel Export | Week 12 | Phase 7 |
| 10 | Phase 10: UI/UX Polish | Week 13 | All above |
| 11 | Phase 12: Data Migration | Week 13 | Database + lookup/employee/allocation schema |
| 12 | Phase 11: Integrated Testing | Week 14-15 | All application stories + Data Migration |
| 13 | Phase 13: Deployment | Week 16 | Integrated Testing |

---

## Story & Agent Ownership

| Story | Owner Agent | Notes |
|-------|-------------|-------|
| AX-001 | scaffolding-lead | Solution/project setup and package baseline |
| AX-002-004 | db-architect | Schema, EF entities, migrations, seed data |
| AX-005-007 | auth-engineer | Login, password policy, TFA, RBAC |
| AX-008-010 | lookup-tables | Lookup/system tables, reporting-month exception for PM |
| AX-011-014 | employee-manager | Employee/allocation screens, row allocation fields, employee attachments |
| AX-015-017 | reporting-engine | Report form, validation, workflow; consumes email service |
| AX-018, AX-022 | excel-handler | Excel import/export and PDF error reports |
| AX-019-020 | dashboard-builder | Dashboard, summary, approvals, inspector scoping |
| AX-021 | background-services | Reminder service and concrete email service |
| AX-023 | ui-polish | RTL, branding, terminology, responsive polish |
| AX-024 | data-migration | One-time import tooling and initial data load |
| AX-025 | qa-security | Integrated QA, E2E, accessibility, security review |
| AX-026 | deployment-ops | IIS, SQL Server Express, SSL, backups, monitoring, runbook |

---

## Confirmed Architectural Decisions

| Question | Decision | Implementation Impact |
|----------|----------|-----------------------|
| Q2.1 Allocation cardinality | One allocation per employee per project | `Allocations.ProjectId` is required; `UNIQUE (UserId, ProjectId)` |
| Q2.2 Output Duration units | Values align with duration increments; UI displays raw values without unit suffix | Remove "minutes" label from UI |
| Q3.3 Notes similarity | Normalized Levenshtein similarity within same report, default threshold 90% | Implement in `ReportValidationService` |
| Q3.4 Row limit scope | Row limits are per allocation | `ReportRows.AllocationId` persists allocation context |
| Q6.1 Inspector assignment logic | AND within one assignment row; OR across rows; NULL is wildcard | Apply in all inspector-scoped queries |
| Q8.1 Frontend stack | ASP.NET Core MVC with Razor Views + JavaScript/AJAX | Do not use Blazor |

---

## Needed Implementation From Traceability Audit

The latest spec/code audit is documented in `SPEC_TRACEABILITY_AUDIT.md`. The following items are required to close the remaining implementation gaps.

### High Priority

- [x] Date validation month boundary: enforce the reporting-month rule in `ReportValidationService` and Excel import.
- [x] Monthly hour allocation validation: validate total monthly `MeetingDuration` per allocation against `Allocations.MonthlyEmploymentScope`.
- [x] Developer-level required/optional field configuration: replace hard-coded required report fields with configurable field metadata/constants and keep forward-only behavior.
- [x] Report-row conclusion fields: add proper FK/navigation/configuration for `ConclusionFrameworkId` and `ConclusionLocationId`, fix display and dropdown sources.
- [x] Allocation-scoped dropdown completeness: filter grade levels, classes, and conclusion fields from selected allocation where allocation junctions exist.
- [x] Employee list spec completion: all Blue Card fields, allocation multi-value display, page-size selector, column sorting, locked indicator, notes columns, and project view.
- [x] Bulk employee allocation change: add selected-employee allocation update workflow or explicitly remove from accepted scope.
- [x] Separate global allocation list: combined employee/allocation list with filtering, sorting, pagination, and Excel export.
- [x] Initial client data migration/import tool: import lookup, employee, allocation, framework/institution data from client Excel files with validation report.

### Medium Priority

- [x] Client logo: `IBrandingService` + `SiteLogoViewComponent` drive every logo slot from `SystemConstant.SiteLogoPath`; `/Admin/Branding` hot-swap (Gap 8). Real client asset still pending.
- [x] Terms of Use versioning: `TermsOfUseVersion` + `TermsOfUseAcceptance` + `RequireTermsAcceptedFilter` forces re-acceptance on new version; `/Admin/TermsOfUse` publishes (Gap 1). Final client text still pending.
- [x] Account unlock UX: add explicit Admin/PM unlock action and clarify self-service unlock.
- [x] Excel template download: add blank `.xlsx` template download for report upload.
- [~] Employee report Excel lookup resolution: batch multi-employee import resolves text/codes via `ILookupResolver`; single-employee upload still expects numeric IDs (by client preference).
- [x] Hebrew/RTL PDF quality: `PdfReportService` rewritten on QuestPDF + Noto Sans Hebrew; right-aligned RTL table (Gap 10).
- [x] Lookup UI special-field coverage: Locality `NationalCode` and Framework `InstitutionSymbol`/`EducationalStageId` editable in UI (Gap 11).
- [x] Lookup delete checks: `LookupController.CanDeleteItemAsync` covers all 17 lookup tables including frameworks, institutions, authorities, educationalstages, educationtypes, localitydistrictnational (Gap 9).
- [x] Dashboard cascading behavior: `/Dashboard/FilterOptions` JSON endpoint + live bidirectional cascading on Dashboard and Summary (Gap 5).
- [x] Dashboard sorting: apply `SortBy`/`SortDesc` and clickable headers.
- [x] Summary Excel export: add export for the summary/approval screen.
- [x] Inspector export restriction: both inspector roles export approved-only from the dashboard.
- [x] Employee delete: implement soft-delete/status deactivation.
- [x] Email failure audit: `NotificationLog` + `NotificationDispatcher` writes + `NotificationRetryService` retry + `/Admin/NotificationLogs` admin screen (Gap 6).

### Low Priority / Production Hardening

- [ ] Email template rich text editor (deferred — plain textarea acceptable per plan).
- [x] Optimistic concurrency tokens on reports/users/allocations: `RowVersion` on `Report`, `ReportRow`, `User`, `Allocation`; Hebrew conflict message (Gap 3).
- [x] General audit trail for sensitive changes: `AuditLog` + `AuditLogService` + instrumentation across employee/allocation/report/auth/lookup/admin/terms + `/Admin/AuditLog` with CSV export (Gap 7).
- [x] Operational runbook for deployment, rollback, backup restore, SMTP, scheduled jobs: [docs/OPERATIONS.md](docs/OPERATIONS.md) (Gap 13).

---

## Critical Business Rules Checklist

This checklist ensures no rule is missed during implementation. **Each must be verified with a test.**

- [x] Password: min 8 chars, letters+digits
- [x] Password: lock after 3 failed attempts
- [x] Password: history of last 5
- [x] Password: force change every 3 months
- [x] Password: force change on first login
- [x] Terms of Use: shown on first login
- [x] Only admin can promote to admin
- [x] Lookup table deletion: check usage before allowing (all 17 lookup tables — Gap 9)
- [x] Only one reporting month active at a time
- [x] Institution symbol unique per educational stage
- [x] Employee dropdown values filtered by their allocations
- [~] Draft status for incomplete reports: empty reports remain Draft; partially filled invalid rows are not persisted because `ReportRows` requires complete rows
- [x] All dates in current or previous months (unless future reporting enabled)
- [x] No reporting on employee's rest day
- [x] Daily max 9 hours (unless unlimited)
- [x] Monthly row limit per allocation
- [x] Annual row limit per allocation
- [x] Allocation stores MonthlyRowAllocation and AnnualRowAllocation used by validation
- [x] ReportRows stores AllocationId so row limits are validated per allocation
- [x] Duplicate row detection (same date + same values + empty/identical notes)
- [x] Notes similarity percentage check uses normalized Levenshtein similarity within the same report
- [x] Submission deadline enforcement
- [x] Excel overwrite: only editable/unapproved reports
- [x] Employee Excel upload: active reporting month and only when allocation allows upload
- [x] PM Excel upload: can include locked months
- [x] Approval email sent to employee
- [x] Rejection email with reasons sent to employee
- [x] Inspector scoping by program/district/sector: AND within one assignment row, OR across assignment rows, NULL as wildcard
- [x] View-only inspector: export approved reports only
- [x] Reminder service: every X days, starting Y days before deadline
- [x] Cascading filters on dashboard
- [x] Bulk approval with checkboxes
- [x] Bulk employee status change
- [x] Document attachment at employee and report-row level
- [x] All user-facing report/allocation fields use employment/output-duration terminology rather than "hours"
- [x] Field required/optional: developer-level toggle, forward-only changes
- [x] Employee card changes propagate to report display fields

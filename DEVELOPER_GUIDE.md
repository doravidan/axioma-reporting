# Axioma Employee Reporting System — Developer Guide

> **Target audience:** Any developer picking up this project cold.  
> **Language:** System UI and all data is in Hebrew (RTL). This guide is in English.

---

## Table of Contents

1. [What the System Does](#1-what-the-system-does)
2. [Technology Stack](#2-technology-stack)
3. [Repository Layout](#3-repository-layout)
4. [Getting Started (Local Dev Setup)](#4-getting-started-local-dev-setup)
5. [Architecture Overview](#5-architecture-overview)
6. [Database Schema](#6-database-schema)
7. [User Roles and Authorization](#7-user-roles-and-authorization)
8. [Authentication Flow](#8-authentication-flow)
9. [Core Business Concepts](#9-core-business-concepts)
10. [Service Layer Reference](#10-service-layer-reference)
11. [Controller and Route Reference](#11-controller-and-route-reference)
12. [Background Services](#12-background-services)
13. [Email System](#13-email-system)
14. [System Constants (Runtime Config)](#14-system-constants-runtime-config)
15. [Excel Import / Export](#15-excel-import--export)
16. [Data Migration (Initial Load)](#16-data-migration-initial-load)
17. [Testing](#17-testing)
18. [Accessibility Compliance (IS 5568 / WCAG 2.1 AA)](#18-accessibility-compliance-is-5568--wcag-21-aa)
19. [Critical Business Rules — Never Break These](#19-critical-business-rules--never-break-these)
20. [Terminology Glossary](#20-terminology-glossary)
21. [Known Gaps and Future Work](#21-known-gaps-and-future-work)

---

## 1. What the System Does

Axioma is a **monthly activity reporting web application** for an Israeli educational organization with hundreds of employees spread across multiple districts. It replaces a manual Excel-based workflow.

**Core workflow:**
1. Admin/PM opens a reporting month.
2. Employees log in and fill their monthly activity report (a list of dated activity rows).
3. Each row describes a meeting/activity: date, duration, location (district + locality), educational framework, domain, subjects, etc.
4. Employees submit their report when complete.
5. Coordinators/Inspectors review and approve or return for correction.
6. Approved reports feed into dashboard analytics and Excel exports.

The system enforces strict per-allocation validation (different employees have different allowed values, duration limits, row limits), duplicate-row detection, notes similarity checking, and an automated email reminder service.

---

## 2. Technology Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core 8 MVC |
| ORM | Entity Framework Core 8 (Code-First) |
| Database | SQL Server Express (local) / SQL Server (production) |
| UI | Razor Views + Bootstrap 5 RTL + vanilla JS/AJAX |
| Excel | ClosedXML (write), ExcelDataReader (read `.xlsb`) |
| Email | MailKit |
| Password Hashing | BCrypt.Net-Next |
| Validation | FluentValidation + custom service-layer validation |
| PDF Generation | PdfReportService (custom, using iText or similar) |
| Testing | xUnit + FluentAssertions + ASP.NET TestHost |
| Auth | Cookie authentication (ASP.NET Core built-in) |

---

## 3. Repository Layout

```
AxiomaReporting.sln              ← Visual Studio solution entry point

src/
  AxiomaReporting.Core/          ← Domain layer (no external dependencies)
    Entities/                    ← EF Core entity classes
    Entities/Base/               ← BaseEntity, LookupEntity base classes
    DTOs/                        ← Data transfer objects (LoginDto, EmployeeDto, …)
    Interfaces/                  ← Service interfaces (IAuthService, IEmailService, …)
    Enums/                       ← UserRoleEnum, UserStatusEnum

  AxiomaReporting.Infrastructure/ ← Data + services layer
    Data/
      AppDbContext.cs             ← EF Core DbContext (all DbSets, Fluent API via IEntityTypeConfiguration)
      SeedData.cs                 ← Seed: roles, statuses, system constants, email templates, admin user
      Configurations/             ← One IEntityTypeConfiguration<T> per entity
    Migrations/                  ← EF Core migration files
    Services/                    ← Business logic services
    BackgroundJobs/              ← ReminderService (IHostedService)

  AxiomaReporting.Web/           ← ASP.NET Core MVC presentation layer
    Controllers/                 ← 7 controllers (see §11)
    Views/                       ← Razor .cshtml views (35 files, RTL Hebrew)
    Authorization/               ← PolicyNames constants
    Models/                      ← ErrorViewModel
    wwwroot/css/site.css         ← Global styles incl. IS 5568 accessibility rules
    Program.cs                   ← DI wiring, auth config, middleware pipeline
    appsettings.json             ← Connection strings (SQL Express + Docker variants)

  AxiomaReporting.Tests/         ← xUnit test project
    Unit/                        ← Service-layer unit tests
    Integration/                 ← Full MVC pipeline tests (in-memory DB)
    Ui/                          ← Rendered HTML smoke tests
    Stress/                      ← Concurrent HTTP health checks
    TestSupport/                 ← CustomWebApplicationFactory, FakeEmailService, TestData, HtmlForm

database/
  seed-data/                     ← Python one-time import scripts
    seed_lookups.py              ← Imports lookup tables from טבלאות.xlsb
    seed_reports.py              ← Imports historical reports from BASE DATA.xlsb
    טבלאות.xlsb                  ← Client lookup data file
    BASE DATA.xlsb               ← Client historical report data file
    קובץ משותף שאלונים…xlsx      ← Client questionnaire catalogue (conclusion values)

tools/
  db-schema-viewer.html          ← Interactive HTML schema browser (open in browser)

docs/
  screenshots/                   ← App screenshots

scripts/
  test-unit-coverage.ps1         ← Coverage gate script (80% line coverage)

CLAUDE.md                        ← AI assistant instructions
SPEC.md                          ← Full functional specification
IMPLEMENTATION_PLAN.md           ← Detailed implementation plan with DDL
IMPLEMENTATION_STATUS.md         ← Living record of what is built
SPEC_TRACEABILITY_AUDIT.md       ← Spec-to-code coverage matrix
CLIENT_CLARIFICATIONS.md         ← Client Q&A log
DATA_IMPORT_MAPPING.md           ← Excel seed data column mapping reference
TESTING.md                       ← Test suite overview
prd.json                         ← RALPH PRD with 24 user stories
```

---

## 4. Getting Started (Local Dev Setup)

### Prerequisites

- .NET 8 SDK
- SQL Server Express (instance name `.\SQLEXPRESS`) — or modify the connection string
- Visual Studio 2022 / Rider / VS Code with C# Dev Kit

### Steps

```powershell
# 1. Clone / open the repo
cd "f:\דווח עובדים אקסיומא"

# 2. Apply all EF Core migrations (creates the DB and seeds initial data)
dotnet ef database update --project src/AxiomaReporting.Infrastructure --startup-project src/AxiomaReporting.Web

# 3. Run the web app
dotnet run --project src/AxiomaReporting.Web

# 4. Open https://localhost:5001 (or the port shown in terminal)
```

### Default Admin Credentials

| Field | Value |
|-------|-------|
| ID Number (username) | `admin` |
| Password | `admin1234` |
| Role | System Admin |

> **You will be forced to change the password on first login** (`MustChangePassword = true`).

### Connection Strings

`appsettings.json` ships with two named strings:

| Name | Target |
|------|--------|
| `DefaultConnection` | `.\SQLEXPRESS` (Windows Auth) |
| `Docker` | `localhost,1433` with `sa` / `Axioma@2024!` |

The active string used by the app is `DefaultConnection`. To use Docker SQL Server, change the `AddDbContext` call in `Program.cs` or override in `appsettings.Development.json`.

---

## 5. Architecture Overview

The solution follows **Clean Architecture** with unidirectional dependencies:

```
Web (MVC Controllers + Razor Views)
  ↓ depends on
Infrastructure (Services, EF Core, Background Jobs)
  ↓ depends on
Core (Entities, Interfaces, DTOs, Enums)
```

**Key design decisions:**

- **No repository pattern over EF Core** — controllers and services use `AppDbContext` directly. The context _is_ the unit of work.
- **Service interfaces live in Core** (`IAuthService`, `IEmailService`, `IPasswordService`, `ICurrentUserService`). Implementations live in Infrastructure.
- **EF Fluent API only** — entity classes have no data annotations. All constraints, indexes, and relationships are configured in `Data/Configurations/`.
- **Seed data in migrations** — `SeedData.Seed(modelBuilder)` is called from `AppDbContext.OnModelCreating`, so all seed data is applied with `database update`.
- **Authorization via policies** — six named policies in `PolicyNames.cs`; role IDs (1–6) are stored as claims.

---

## 6. Database Schema

### Core Tables

#### `Users`
The central user table. One row per person in the system (both employees and admin/management roles).

| Column | Type | Notes |
|--------|------|-------|
| `Id` | int PK | |
| `EmployeeCode` | nvarchar | Unique employee code ("קוד עובד") |
| `IdNumber` | nvarchar | Israeli ID number — used as login username |
| `FirstName` / `LastName` | nvarchar | |
| `PasswordHash` | nvarchar | BCrypt hash (work factor 12) |
| `RoleId` | int FK → `EmployeeRoles` | Job role (teacher, manager, etc.) |
| `UserRoleId` | int FK → `UserRoles` | System access role (1=Admin…6=Employee) |
| `StatusId` | int FK → `UserStatuses` | 1=Active, 2=Inactive, 3=Locked |
| `IsReportingEmployee` | bit | Whether this user submits monthly reports |
| `RestDay` | int? | Day of week (1=Sun…7=Sat) when reporting is blocked |
| `AllowFutureReporting` | bit | Allow dates after the reporting month |
| `Email` / `Phone` | nvarchar? | For email notifications |
| `MustChangePassword` | bit | Forces password change on next login |
| `FailedLoginAttempts` | int | Resets on success; account locks at 3 |
| `LastPasswordChange` | datetime? | For 90-day expiry enforcement |
| `AcceptedTermsOfUse` | bit | Terms acceptance gate on first login |
| `CreatedBy` / `UpdatedBy` | int? | Audit FK to Users |

#### `Allocations`
Links a user to a project with their employment scope and allowed field values.

| Column | Type | Notes |
|--------|------|-------|
| `Id` | int PK | |
| `UserId` | int FK → Users | **UNIQUE with ProjectId** |
| `ProjectId` | int FK → Projects | |
| `AnnualEmploymentScope` | decimal? | Max annual hours/units |
| `MonthlyEmploymentScope` | decimal? | Max monthly hours/units — validated per row |
| `DailyEmploymentScope` | decimal? | Max daily duration per row; null = unlimited |
| `MonthlyRowAllocation` | int? | Max report rows per month |
| `AnnualRowAllocation` | int? | Max report rows per year |
| `OutputDuration` | nvarchar? | Comma-separated allowed duration values |
| `AllowExcelUpload` | bit | Whether employee can upload Excel for this allocation |
| `IsActive` | bit | |

**Allocation junction tables** (many-to-many scope filters):

| Table | Filters rows by |
|-------|----------------|
| `AllocationDistricts` | Allowed districts |
| `AllocationLocalities` | Allowed localities |
| `AllocationPrograms` | Allowed programs |
| `AllocationSectors` | Allowed sectors |
| `AllocationFrameworks` | Allowed educational frameworks |
| `AllocationSubjects` | Allowed subjects |
| `AllocationDomains` | Allowed domains |
| `AllocationEducationalPrograms` | Allowed educational programs |
| `AllocationClasses` | Allowed school classes |
| `AllocationGradeLevels` | Allowed grade levels |
| `AllocationDiscussionCodes` | Allowed discussion codes |
| `AllocationLocalityDistrictNationals` | Allowed locality-district-national values |

These tables drive the cascading dropdowns in the report form — only values linked to the employee's allocation appear.

#### `Reports`
One report per user per reporting month.

| Column | Type | Notes |
|--------|------|-------|
| `Id` | int PK | |
| `UserId` | int FK → Users | |
| `ReportingMonthId` | int FK → ReportingMonths | |
| `StatusId` | int FK → ReportStatuses | 1=Draft, 2=InEntry, 3=PendingApproval, 4=Approved, 5=ReturnedForCorrection, 6=Locked |
| `SubmittedAt` | datetime? | |
| `ApprovedAt` / `ApprovedBy` | | |
| `RejectedAt` / `RejectedBy` / `RejectionReason` | | |
| `ImportedFromExcel` | bit | |

#### `ReportRows`
Individual activity rows within a report.

| Column | Type | Notes |
|--------|------|-------|
| `Id` | int PK | |
| `ReportId` | int FK → Reports | |
| `AllocationId` | int? FK → Allocations | Which allocation this row belongs to |
| `SequenceNumber` | int | Display order |
| `MeetingDate` | date | Must be within reporting month (unless future reporting allowed) |
| `MeetingDuration` | decimal | Duration of activity (not "hours" — see terminology) |
| `DistrictId` | int FK → Districts | |
| `LocalityId` | int FK → Localities | |
| `FrameworkId` | int FK → Frameworks | Educational framework |
| `EducationalProgramId` | int FK → EducationalPrograms | |
| `DomainId` | int FK → Domains | |
| `Subject1Id` | int FK → Subjects | Primary subject |
| `Subject2Id` | int? FK → Subjects | Optional secondary subject |
| `DiscussionCodeId` | int? FK → DiscussionCodes | |
| `ConclusionClassId` | int? FK → SchoolClasses | |
| `ConclusionFrameworkId` | int? FK → Frameworks | |
| `ConclusionLocationId` | int? FK → LocalityDistrictNationals | |
| `GradeLevelId` | int? FK → GradeLevels | |
| `ClassId` | int? FK → SchoolClasses | |
| `Notes` | nvarchar? | Free text; similarity-checked within report |

#### `ReportingMonths`
Controls which month is currently open for reporting. **Only one may be active at a time.**

| Column | Notes |
|--------|-------|
| `Month` / `Year` | Calendar month/year |
| `LastReportingDate` | Submission deadline |
| `IsActive` | Only one row can be true; enforced in service layer |
| `AllowFutureReporting` | If true, rows with future dates are allowed |

#### `SystemConstants`
Runtime-configurable key/value settings editable by Admin in the UI.

| Key | Default | Meaning |
|-----|---------|---------|
| `ReminderIntervalDays` | 3 | Days between email reminders |
| `ReminderStartDaysBeforeDeadline` | 7 | Days before deadline to start reminders |
| `ReminderCheckIntervalHours` | 1 | How often the reminder background service runs |
| `NotesSimilarityThresholdPercent` | 90 | Levenshtein similarity % that triggers duplicate warning |
| `MaxDailyHoursDefault` | 9 | Daily duration cap when allocation has no specific scope |
| `TfaEmailEnabled` | false | Toggle email 2FA on login |
| `RequiredReportFields` | (CSV list) | Which report row fields are mandatory |
| `PasswordExpiryWarningDays` | 14 | Days before password expiry to send warning email |

#### `EmailTemplates`
Stored email bodies editable by Admin. Supports `{{PlaceholderName}}` and `{PlaceholderName}` syntax.

| TypeDescription | Trigger |
|----------------|---------|
| `ReportReceived` | After successful Excel upload |
| `ReportApproved` | On approve action |
| `ReportRejected` | On reject/return-for-correction action |
| `ReminderNotSubmitted` | Automated reminder — report not yet submitted |
| `ReminderNeedsCorrection` | Automated reminder — returned report not fixed |
| `PasswordReset` | Forgot-password link email |
| `TwoFactorCode` | TFA code email |
| `PasswordExpiryWarning` | Password about to expire |

#### Lookup Tables
All lookup tables extend `LookupEntity` (Id, Description, IsActive, CreatedAt).

| Table | Hebrew name | Notes |
|-------|-------------|-------|
| `Districts` | מחוזות | |
| `Localities` | ישובים | |
| `Frameworks` | מסגרות חינוכיות | |
| `EducationalPrograms` | תוכניות חינוכיות | |
| `Domains` | תחומים | |
| `Subjects` | נושאים | |
| `DiscussionCodes` | קודי דיון | |
| `SchoolClasses` | כיתות | |
| `GradeLevels` | שכבות גיל | |
| `Sectors` | מגזרים | |
| `Programs` | תוכניות | |
| `Projects` | פרויקטים | |
| `Authorities` | רשויות | |
| `EducationalStages` | שלבי חינוך | |
| `EducationTypes` | סוגי חינוך | |
| `LocalityDistrictNationals` | ישוב/מחוז/ארצי | For conclusion location field |
| `EmployeeRoles` | תפקידי עובד | Job role (not system role) |

#### Security / Auth Tables

| Table | Purpose |
|-------|---------|
| `PasswordHistories` | Last 5 password hashes per user — prevents reuse |
| `PasswordResetTokens` | Time-limited tokens for forgot-password flow |
| `TwoFactorCodes` | Time-limited codes for email TFA (when enabled) |
| `ReminderLogs` | Tracks sent reminders to enforce `ReminderIntervalDays` |

#### Other Tables

| Table | Purpose |
|-------|---------|
| `DocumentAttachments` | File attachments — linked to either a `ReportRow` or a `User` |
| `InspectorAssignments` | Defines which Program+District+Sector scope each Inspector can see |
| `EmailServerSettings` | SMTP server config stored in DB (editable by Admin) |
| `UserRoles` | 6 system roles (lookup) |
| `UserStatuses` | Active / Inactive / Locked (lookup) |
| `ReportStatuses` | 6 workflow statuses (lookup) |

### EF Migrations (in order)

| Migration | What it adds |
|-----------|-------------|
| `20260412124943_InitialCreate` | All tables, indexes, seed data |
| `20260412134615_AddReminderLogs` | `ReminderLogs` table |
| `20260412154401_AddAccountRecoveryAndEmailTfa` | `PasswordResetTokens`, `TwoFactorCodes`, TFA/reset seed constants |
| `20260412164437_AddReportRequiredFieldsAndConclusionRelations` | `RequiredReportFields` constant, FK + index for conclusion fields |
| `20260412170550_AddPasswordExpiryAndConfigurableReminder` | Password expiry columns, `ReminderCheckIntervalHours` + `PasswordExpiryWarningDays` constants |

---

## 7. User Roles and Authorization

### Six System Roles

| ID | Name | Hebrew | Access |
|----|------|--------|--------|
| 1 | SystemAdmin | מנהל מערכת | Full access to everything including lookup admin, system constants, email templates, user management, data migration |
| 2 | ProjectManager | מנהל פרויקט | Manage employees/allocations, open/close reporting months, override report statuses, can't assign Admin role |
| 3 | ProjectCoordinator | רכז פרויקט | Create employees/allocations, approve/reject reports, view dashboard |
| 4 | InspectorView | מפקח צפייה | Read-only dashboard scoped to assigned group; can only export **approved** reports |
| 5 | InspectorApproval | מפקח אישור | Same as InspectorView + can approve/reject reports |
| 6 | Employee | עובד | Own data only: fill/submit their own report, view their own dashboard row |

### Authorization Policies (in `PolicyNames.cs`)

| Policy | Roles allowed | Used for |
|--------|--------------|---------|
| `AdminOnly` | 1 | Lookup management, system constants, email templates, data migration |
| `AdminOrPM` | 1, 2 | Reporting months, inspector assignments, admin panel |
| `AdminPMOrCoordinator` | 1, 2, 3 | Employee management, allocation management |
| `CanApproveReports` | 1, 2, 3, 5 | Report approve/reject actions |
| `CanViewDashboard` | 1, 2, 3, 4, 5 | Dashboard access |
| `CanManageLookups` | 1 | Lookup CRUD |

Role IDs are stored as `ClaimTypes.Role` claims in the auth cookie.

### Inspector Scoping Logic

Inspector assignments use AND-within-row, OR-across-rows logic:
- A single `InspectorAssignment` row can have Program + District + Sector. `NULL` means wildcard (match any).
- An employee's report row matches if **any one** of the inspector's assignment rows is fully satisfied.
- Multiple assignment rows are ORed together.

---

## 8. Authentication Flow

### Normal Login
1. User submits `LoginDto` (IdNumber + Password) to `POST /Account/Login`
2. `AuthService.ValidateLoginAsync` checks: status (Locked/Inactive), BCrypt password hash, failed attempt count
3. On 3 failed attempts → account status set to Locked
4. On success → reset failed attempts
5. If `TfaEmailEnabled = true`: generate 6-digit code, store in `TwoFactorCodes`, send email, redirect to `/Account/TwoFactor`
6. TFA verified → issue auth cookie with claims: NameIdentifier (userId), Name (fullName), Role (roleId as string)
7. Cookie: 8-hour sliding expiration

### First Login
- `MustChangePassword = true` → redirect to `/Account/ChangePassword` before any other page
- `AcceptedTermsOfUse = false` → redirect to `/Account/TermsOfUse`

### Forgot Password
1. User enters IdNumber on `/Account/ForgotPassword`
2. System generates HMAC-SHA256 token, stores in `PasswordResetTokens` with 60-minute expiry
3. Sends email with reset link to `user.Email`
4. User clicks link → `/Account/ResetPassword?token=…`
5. Token validated (not expired, not used) → allow new password
6. Token marked used after successful reset

### Password Policy
- Minimum 8 characters, must include both letters and digits
- Cannot reuse last 5 passwords (checked via `PasswordHistories`)
- Expires after 90 days → warning email 14 days before expiry (`PasswordExpiryWarningDays`)
- Lock on 3 consecutive failed login attempts

---

## 9. Core Business Concepts

### Reporting Month
- Only **one** `ReportingMonth` may have `IsActive = true` at a time
- Enforced in `AdminController.ActivateReportingMonth`: deactivates all others before activating the new one
- `LastReportingDate` is the submission deadline for employee reminders
- Opening a new month does NOT create reports — reports are created lazily when an employee first visits the report page

### Report Lifecycle

```
[not yet created]
       ↓ (employee opens report page for active month)
    Draft (1)
       ↓ (employee starts entering rows)
    InEntry (2)
       ↓ (employee clicks Submit)
    PendingApproval (3)
       ↓                    ↓
  Approved (4)     ReturnedForCorrection (5)
                           ↓ (employee resubmits)
                    PendingApproval (3)
```

`Locked (6)` is set by Admin/PM to prevent any further edits.

Excel upload also creates/updates reports; imported reports go to `PendingApproval` directly.

### Allocation-Scoped Validation
Each report row is validated against the employee's specific allocation for that row:

1. **Required fields** — from `RequiredReportFields` system constant (CSV of field names)
2. **Duration options** — `Allocation.OutputDuration` is a CSV of allowed values; if set, `MeetingDuration` must be one of them
3. **Daily duration cap** — `Allocation.DailyEmploymentScope`; if null → use `MaxDailyHoursDefault` constant (9)
4. **Monthly row limit** — `Allocation.MonthlyRowAllocation`; counts non-deleted rows in the current month
5. **Annual row limit** — `Allocation.AnnualRowAllocation`; counts across all months in the year
6. **Monthly employment scope** — total `MeetingDuration` sum for the month cannot exceed `Allocation.MonthlyEmploymentScope`

### Duplicate Row Detection
A row is a duplicate if another row in the same report has identical values for:
- MeetingDate, AllocationId, MeetingDuration, DistrictId, LocalityId, FrameworkId, EducationalProgramId, DomainId, Subject1Id, Subject2Id, DiscussionCodeId, ConclusionClassId, ConclusionFrameworkId, ConclusionLocationId, GradeLevelId, ClassId

AND either both notes are empty OR notes are identical.

Additionally, notes are checked for similarity using **Levenshtein normalized distance**. If similarity ≥ `NotesSimilarityThresholdPercent` (default 90%), a duplicate warning is raised.

### Rest Day
If `User.RestDay` is set (1=Sunday…7=Saturday), report rows with `MeetingDate` on that weekday are rejected.

### Dropdown Cascading
All report form dropdowns are filtered to only values listed in the employee's allocation junction tables. When AllocationId is selected in the row form, a JS call fetches the allowed values for that allocation and rebuilds all other dropdowns.

---

## 10. Service Layer Reference

### `AuthService` (Infrastructure/Services)
| Method | Purpose |
|--------|---------|
| `ValidateLoginAsync(idNumber, password)` | Core login — returns (Success, ErrorMessage, User) |
| `ChangePasswordAsync(userId, current, new)` | Validates current, checks history, updates hash |
| `RecordFailedLoginAsync(idNumber)` | Increments counter, locks at 3 |
| `ResetFailedLoginsAsync(userId)` | Called on successful login |
| `IsPasswordInHistoryAsync(userId, password)` | Checks last 5 hashes |
| `ForgotPasswordAsync(idNumber)` | Creates reset token, sends email |
| `ResetPasswordAsync(token, newPassword)` | Validates token, applies new password |

### `PasswordService` (Infrastructure/Services)
Thin wrapper around BCrypt.Net. Handles `HashPassword` and `VerifyPassword`.

### `ReportValidationService` (Infrastructure/Services)
| Method | Purpose |
|--------|---------|
| `ValidateRowAsync(row, employee, month, allRows)` | Validates a single row: required fields, date, duration cap, monthly scope, row limits, duplicates, rest day |
| `ValidateSubmitAsync(report, employee, month)` | Validates the whole report before submission |

### `ReportStatusService` (Infrastructure/Services)
Handles report status transitions: Submit, Approve, Reject, ReturnForCorrection. Sends notification emails on each transition.

### `ReportExcelImportService` (Infrastructure/Services)
Parses uploaded `.xlsx` files. Validates each row. Replaces only unapproved rows for the given allocation. Returns a list of errors that can be downloaded as a PDF error report.

### `DashboardFilterService` (Infrastructure/Services)
Builds filtered report summaries for the Dashboard. Starts from scoped employees/allocations, supports district/sector/program filters, "Not Yet Reported" logic (includes Draft and InEntry), and sorting.

### `EmailService` (Infrastructure/Services)
Sends emails via MailKit using SMTP settings from `EmailServerSettings` table. Resolves `EmailTemplate` by `TypeDescription`, replaces tokens (`{{Name}}` or `{Name}`), sends.

### `EmployeeService` (Infrastructure/Services)
Employee CRUD: create, update, deactivate, unlock, reset password, manage allocations and allocation junctions.

### `PdfReportService` (Infrastructure/Services)
Generates PDF error reports for Excel import validation failures.

### `GenericLookupService` (Infrastructure/Services)
CRUD for all simple lookup tables via generic interface. Checks for in-use values before deletion.

### `CurrentUserService` (Infrastructure/Services)
Reads `UserId`, `UserRoleId`, `FullName` from the current HTTP context claims.

### `ReminderService` (Infrastructure/BackgroundJobs)
`IHostedService` (runs as background thread). Every `ReminderCheckIntervalHours` hours:
1. Finds the active reporting month
2. Checks if within reminder window (within `ReminderStartDaysBeforeDeadline` days of `LastReportingDate`)
3. For each employee with an active allocation: if report is not submitted or returned-for-correction and no reminder was sent within `ReminderIntervalDays`, sends email and logs to `ReminderLogs`

---

## 11. Controller and Route Reference

All routes follow the default convention: `/{Controller}/{Action}/{id?}`.  
All controllers except `AccountController` require `[Authorize]`.

### `AccountController`
| Route | Auth | Purpose |
|-------|------|---------|
| `GET /Account/Login` | Anonymous | Login page |
| `POST /Account/Login` | Anonymous | Validate credentials, issue cookie |
| `GET /Account/Logout` | — | Sign out |
| `GET /Account/TwoFactor` | Anonymous | Enter TFA code |
| `POST /Account/TwoFactor` | Anonymous | Verify TFA code |
| `GET /Account/ChangePassword` | Any | Change password form |
| `POST /Account/ChangePassword` | Any | Apply password change |
| `GET /Account/ForgotPassword` | Anonymous | Forgot password form |
| `POST /Account/ForgotPassword` | Anonymous | Send reset link |
| `GET /Account/ResetPassword` | Anonymous | Reset password form (via token) |
| `POST /Account/ResetPassword` | Anonymous | Apply reset |
| `GET /Account/TermsOfUse` | Any | Terms of use acceptance |
| `POST /Account/TermsOfUse` | Any | Accept terms |
| `GET /Account/AccessDenied` | Any | 403 page |

### `ReportController`
| Route | Auth | Purpose |
|-------|------|---------|
| `GET /Report` | Any authenticated | Report entry form for active month |
| `POST /Report/SaveRow` | Any authenticated | AJAX save or update a row |
| `POST /Report/DeleteRow` | Any authenticated | AJAX delete a row |
| `POST /Report/Submit` | Any authenticated | Submit report for approval |
| `POST /Report/UploadAttachment` | Any authenticated | Upload file attachment to a row |
| `POST /Report/DeleteAttachment` | Any authenticated | Delete attachment |
| `GET /Report/DownloadAttachment` | Any authenticated | Download attachment file |
| `POST /Report/UploadExcel` | Any authenticated | Import rows from Excel file |
| `GET /Report/DownloadTemplate` | Any authenticated | Download Excel import template |
| `POST /Report/Approve` | CanApproveReports | Approve a report |
| `POST /Report/Reject` | CanApproveReports | Return for correction |
| `POST /Report/BulkApprove` | CanApproveReports | Approve multiple selected reports |

### `DashboardController`
| Route | Auth | Purpose |
|-------|------|---------|
| `GET /Dashboard` | CanViewDashboard | Dashboard overview with filters |
| `GET /Dashboard/Summary` | CanViewDashboard | Summary by employee/allocation |
| `GET /Dashboard/Export` | CanViewDashboard | Excel export (Inspector roles: approved only) |

### `EmployeeController`
| Route | Auth | Purpose |
|-------|------|---------|
| `GET /Employee` | AdminPMOrCoordinator | Employee list with filters, sorting |
| `GET /Employee/Create` | AdminPMOrCoordinator | New employee form |
| `POST /Employee/Create` | AdminPMOrCoordinator | Save new employee |
| `GET /Employee/Edit/{id}` | AdminPMOrCoordinator | Edit employee form |
| `POST /Employee/Edit/{id}` | AdminPMOrCoordinator | Save employee changes |
| `POST /Employee/Deactivate/{id}` | AdminPMOrCoordinator | Soft-delete (deactivate) |
| `POST /Employee/Unlock/{id}` | AdminOrPM | Unlock locked account |
| `GET /Employee/Allocations/{id}` | AdminPMOrCoordinator | Manage employee's allocations |
| `POST /Employee/SaveAllocation` | AdminPMOrCoordinator | Save allocation + junction tables |
| `POST /Employee/DeleteAllocation/{id}` | AdminPMOrCoordinator | Delete allocation |
| `POST /Employee/BulkCreateAllocation` | AdminOrPM | Create allocation for selected employees |
| `GET /Employee/AllocationList` | AdminOrPM | Global allocation list |
| `GET /Employee/AllocationExport` | AdminOrPM | Export allocations to Excel |
| `POST /Employee/UploadAttachment/{id}` | AdminPMOrCoordinator | Upload file to employee profile |
| `POST /Employee/DeleteAttachment/{id}` | AdminPMOrCoordinator | Delete employee file |

### `AdminController`
All actions require `AdminOrPM` unless noted.

| Route | Auth | Purpose |
|-------|------|---------|
| `GET/POST /Admin/ReportingMonths` | AdminOrPM | Manage reporting months |
| `POST /Admin/ActivateReportingMonth/{id}` | AdminOrPM | Set active month |
| `GET/POST /Admin/SystemConstants` | AdminOnly | Edit system constants |
| `GET/POST /Admin/EmailTemplates` | AdminOnly | Edit email templates |
| `GET/POST /Admin/EmailServerSettings` | AdminOnly | Configure SMTP |
| `GET /Admin/InspectorAssignments` | AdminOrPM | Manage inspector scope |
| `POST /Admin/SaveInspectorAssignment` | AdminOrPM | Save assignment row |
| `POST /Admin/DeleteInspectorAssignment` | AdminOrPM | Delete assignment row |
| `GET /Admin/DataMigration` | AdminOnly | One-time data import screen |
| `POST /Admin/ImportLookups` | AdminOnly | Import from טבלאות.xlsb |
| `POST /Admin/ImportQuestionnaire` | AdminOnly | Import questionnaire catalogue |
| `POST /Admin/ImportAllocationExcel` | AdminOrPM | Bulk allocation upload |
| `GET /Admin/Frameworks` | AdminOnly | Manage frameworks (special: has institution link) |
| `GET /Admin/Institutions` | AdminOnly | Manage institutions |

### `LookupController`
| Route | Auth | Purpose |
|-------|------|---------|
| `GET /Lookup` | AdminOnly | List of all lookup tables with links |
| `GET /Lookup/List?type={name}` | AdminOnly | CRUD list for a specific lookup table |
| `POST /Lookup/Save` | AdminOnly | Create or update a lookup value |
| `POST /Lookup/Delete` | AdminOnly | Delete (checks if in use first) |
| `POST /Lookup/ImportExcel` | AdminOnly | Bulk import lookup values from Excel |

### `HomeController`
| Route | Auth | Purpose |
|-------|------|---------|
| `GET /` or `GET /Home` | Any authenticated | Redirects to Report or Dashboard |
| `GET /Home/Error` | — | Generic error page |

---

## 12. Background Services

### `ReminderService`
- Registered as `IHostedService` in `Program.cs`
- Runs in a loop, sleeping `ReminderCheckIntervalHours` hours between cycles
- Each cycle opens its own DI scope (required for scoped services from a singleton-like hosted service)
- Sends to employees whose report is `Draft`, `InEntry`, or `ReturnedForCorrection` and are within the reminder window
- Checks `ReminderLogs` to avoid sending more than once per `ReminderIntervalDays` period
- Also sends `PasswordExpiryWarning` emails to users whose password expires within `PasswordExpiryWarningDays` days

---

## 13. Email System

### Configuration
SMTP settings are stored in the `EmailServerSettings` database table and editable in the admin UI. Typical fields:
- `Host`, `Port`, `UseSsl`
- `Username`, `Password` (SMTP credentials)
- `FromAddress`, `FromName`

### Template Token Substitution
Templates support both `{{Name}}` and `{Name}` syntax. Available tokens vary by template type:

| Token | Available in |
|-------|-------------|
| `{{EmployeeName}}` | All employee-targeted templates |
| `{{Month}}` / `{{Year}}` | Report-related templates |
| `{{Deadline}}` | Reminder templates |
| `{{RejectionReason}}` | ReportRejected |
| `{{ResetLink}}` | PasswordReset |
| `{{Code}}` / `{{Minutes}}` | TwoFactorCode |
| `{{DaysLeft}}` / `{{ExpiryDate}}` | PasswordExpiryWarning |

---

## 14. System Constants (Runtime Config)

Editable via `Admin → System Constants`. Changes take effect immediately (no restart needed).

| Key | Default | Description |
|-----|---------|-------------|
| `ReminderIntervalDays` | `3` | Minimum days between reminders to the same employee |
| `ReminderStartDaysBeforeDeadline` | `7` | Days before deadline when reminders start |
| `ReminderCheckIntervalHours` | `1` | Background service sleep interval |
| `NotesSimilarityThresholdPercent` | `90` | Levenshtein threshold for duplicate notes warning |
| `MaxDailyHoursDefault` | `9` | Daily duration cap fallback when allocation has no specific scope |
| `TfaEmailEnabled` | `false` | Toggle on/off email 2FA for all logins |
| `RequiredReportFields` | (CSV) | Fields that are mandatory in report rows; developer-level, forward-only |
| `PasswordExpiryWarningDays` | `14` | Days before expiry to send warning |

> **`RequiredReportFields`** is a developer-level constant. Changing it forward affects only new validation — existing saved rows are not retroactively re-validated.

---

## 15. Excel Import / Export

### Report Row Import (Employee)
**File:** `.xlsx` only  
**Trigger:** Employee clicks "Upload Excel" on the report form (only for allocations with `AllowExcelUpload = true`)  
**Behavior:** Replaces **only** rows for that specific allocation. Rows for other allocations are untouched. Only replaces rows in reports with editable statuses (Draft, InEntry, ReturnedForCorrection). PM can upload for locked months.

**Expected column layout** (row 1 = headers, row 2+ = data):

| Column | Field | Format |
|--------|-------|--------|
| A | MeetingDate | Date |
| B | MeetingDuration | Number |
| C | DistrictId | Integer ID |
| D | LocalityId | Integer ID |
| E | FrameworkId | Integer ID |
| F | EducationalProgramId | Integer ID |
| G | DomainId | Integer ID |
| H | Subject1Id | Integer ID |
| I | Subject2Id | Integer ID (optional) |
| J | DiscussionCodeId | Integer ID (optional) |
| K | ConclusionClassId | Integer ID (optional) |
| L | ConclusionFrameworkId | Integer ID (optional) |
| M | ConclusionLocationId | Integer ID (optional) |
| N | GradeLevelId | Integer ID (optional) |
| O | ClassId | Integer ID (optional) |
| P | Notes | Text (optional) |

A downloadable template (with correct column headers) is available at `GET /Report/DownloadTemplate`.

### Allocation Import (Admin/PM)
Bulk creates or updates allocations from Excel. Available from `Employee → Allocation List`.

### Dashboard Export
- All roles with dashboard access: exports filtered report data to Excel
- Inspector roles (4 and 5): **approved reports only**

### Lookup Table Import
Admin can bulk-import any lookup table values from Excel (one column: Description).

### Data Migration Imports
One-time admin actions on `Admin → Data Migration`:
- Import lookup tables from `טבלאות.xlsb` (client file)
- Import questionnaire catalogue from `קובץ משותף שאלונים` (uses `כללי - מאוחד` sheet)

---

## 16. Data Migration (Initial Load)

The Python scripts in `database/seed-data/` are **one-time** scripts for importing the client's historical data.

```powershell
# Activate virtual env first (if needed)
python database/seed-data/seed_lookups.py    # Import lookup tables from טבלאות.xlsb
python database/seed-data/seed_reports.py   # Import historical reports from BASE DATA.xlsb
```

`seed_reports.py` matches historical rows to allocations. If a historical row maps to exactly one active allocation, `AllocationId` is assigned automatically. Multi-match and no-match rows are logged.

**Do not run these scripts more than once** — they assume a clean database and will create duplicates if run again.

---

## 17. Testing

```powershell
# Run all tests
dotnet test

# Run with coverage gate (enforces 80% line coverage on service layer)
.\scripts\test-unit-coverage.ps1
```

### Test Organization

| Folder | Purpose |
|--------|---------|
| `Unit/` | Fast service tests: password, auth, validation, Excel import, PDF, dashboard filtering, status transitions |
| `Integration/` | Full MVC pipeline with in-memory DB and fake email: account flows, TFA, forgot-password |
| `Ui/` | Rendered HTML smoke tests: RTL login page, forgot-password form, anonymous redirect |
| `Stress/` | Concurrent HTTP health check against test host |

### Test Infrastructure
- `CustomWebApplicationFactory` — boots real app with in-memory EF Core + disabled background service + fake email sender
- `TestData` — seeds minimal users, roles, statuses, and constants for tests
- `HtmlForm` — extracts `__RequestVerificationToken` from rendered HTML so POST tests go through the full antiforgery pipeline
- `FakeEmailService` — captures sent emails for assertion in tests; never calls SMTP

---

## 18. Accessibility Compliance (IS 5568 / WCAG 2.1 AA)

The system is required to comply with **Israeli Standard IS 5568**, which is based on WCAG 2.1 AA. This is legally mandatory for Israeli systems serving 100+ employees.

### What is Implemented

**All 35 Razor views have:**
- `lang="he" dir="rtl"` on `<html>` element (or inherited from `_Layout`)
- All alert divs: `role="alert"`, `aria-live="polite"` (non-critical) or `aria-live="assertive"` (errors), `aria-atomic="true"`
- All `.btn-close[data-bs-dismiss="alert"]`: `aria-label="סגור הודעה"`
- All `.btn-close[data-bs-dismiss="modal"]`: `aria-label="סגור חלון"`

**`_Layout.cshtml`:**
- Skip navigation link: `<a href="#main-content" class="skip-link">דלג לתוכן הראשי</a>`
- `<nav aria-label="ניווט ראשי">`
- `<main id="main-content" role="main">`

**`site.css`:**
- `.skip-link` positioned off-screen, revealed on `:focus`
- Focus indicators: `outline: 2px solid #ffcc00` + blue halo (`box-shadow: 0 0 0 4px rgba(15,95,215,0.4)`) on all interactive elements

**Report/Index.cshtml (most complex view):**
- Table: `<table aria-labelledby="reportTableCaption">` with `id` on heading
- All `<th scope="col">` with `aria-sort="none/ascending/descending"` (updated by JS on sort)
- Sort headers: `tabindex="0"` + keyboard handler (Enter/Space triggers sort)
- Row action buttons: `aria-label="ערוך שורה N"` / `aria-label="מחק שורה N"` (with row number)
- File inputs: `<label class="visually-hidden">` + matching `id`/`for`
- Modal: `role="dialog" aria-modal="true" aria-labelledby="rowModalTitle"`
- All form labels wired with `for` → `id` matching

**All modals across the app:**
- `role="dialog"`, `aria-modal="true"`, `aria-labelledby="{modal}Title"`
- Modal `h5` has matching `id`

### WCAG 2.1 AA Criteria Covered
1.3.1 Info & Relationships · 2.1.1 Keyboard · 2.4.1 Bypass Blocks · 2.4.6 Labels · 2.4.7 Focus Visible · 3.1.1 Language of Page · 3.3.1 Error Identification · 3.3.2 Labels or Instructions · 4.1.2 Name/Role/Value · 4.1.3 Status Messages

---

## 19. Critical Business Rules — Never Break These

1. **Password rules:** min 8 chars (letters + digits), lock after 3 fails, history of 5, rotate every 90 days
2. **Only Admin can promote to Admin** — PM cannot assign role 1
3. **Lookup deletion:** always check if value is in use before deleting
4. **One active reporting month** at a time — enforced in `ActivateReportingMonth`
5. **Institution symbol unique** per educational stage
6. **Dropdown values filtered by allocation** — employee only sees what their allocation allows
7. **No reporting on rest day** (`User.RestDay`)
8. **Daily max 9 units** unless `DailyEmploymentScope = null` (unlimited) is set on the allocation
9. **Duplicate row detection** — full field comparison + notes similarity check (Levenshtein)
10. **Notes similarity threshold** — configurable via `NotesSimilarityThresholdPercent` (default 90%)
11. **Excel upload overwrites only unapproved reports** — approved rows are never overwritten
12. **Employee Excel upload = current month only**; PM can upload for locked months
13. **Inspector roles (4 and 5) can ONLY export approved reports**
14. **No "שעות" (hours) terminology** — always use activity-based terms (see glossary)
15. **One allocation per employee per project** — `UNIQUE (UserId, ProjectId)` on Allocations
16. **AllocationId stored on rows** — row limits are validated per allocation, not per report

---

## 20. Terminology Glossary

The system uses specific Hebrew terminology. Using the wrong terms in the UI is a client requirement violation.

| Do NOT use | Use instead | English meaning |
|-----------|-------------|-----------------|
| דיווח שעות | פעילות חודשית | Monthly activity report |
| מספר עובד | קוד עובד | Employee code |
| פרטי פרויקט | פרטי הקצאה | Allocation details |
| היקף שעות שנתי | היקף העסקה שנתי | Annual employment scope |
| היקף שעות חודשי | היקף העסקה חודשי | Monthly employment scope |
| היקף שעות לשורת דיווח | משך תפוקה | Output duration (per row) |

---

## 21. Known Gaps and Future Work

The following items are documented in `SPEC_TRACEABILITY_AUDIT.md` but not yet implemented:

| Area | Gap |
|------|-----|
| Audit history | No general-purpose audit log for entity changes |
| Optimistic concurrency | No `RowVersion` or concurrency tokens on entities |
| SMS reminders | Out of scope unless client selects a provider |
| Excel import lookup format | Import currently requires numeric IDs; client wants name-based lookup |
| Hebrew PDF output | Current PDF error reports are basic; richer Hebrew PDF not yet done |
| Draft persistence | Partially filled invalid rows are not saved as drafts |
| Logo / branding | Client logo and custom terms page pending client decision |
| Persistent notification log | No permanent log of all sent notifications beyond reminder logs |

---

*Last updated: 2026-04-13*

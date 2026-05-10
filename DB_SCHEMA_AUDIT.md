# Database Schema Audit

Updated: 2026-04-13

Source of truth used for this audit:

- EF Core model and migrations under `src/AxiomaReporting.Infrastructure/Data`
- Live SQL Server database `AxiomaReporting` on `.\SQLEXPRESS`
- Static schema viewer: `tools/db-schema-viewer.html`
- Schema sections in `SPEC.md` and `IMPLEMENTATION_PLAN.md`

## Result

The implemented EF model and live SQL database are aligned. The live database contains 47 application tables plus `__EFMigrationsHistory`.

The static schema viewer now matches the implemented table names and columns at the application-table level.

## Application Tables

### Lookup Tables

| Table | Notes |
|-------|-------|
| `Districts` | Base lookup: `Id`, `Description`, `IsActive`, timestamps |
| `Sectors` | Base lookup |
| `Localities` | Base lookup plus nullable `NationalCode` |
| `Authorities` | Base lookup |
| `Projects` | Base lookup |
| `Programs` | Base lookup |
| `EducationalPrograms` | Base lookup |
| `Subjects` | Base lookup |
| `Domains` | Base lookup |
| `Frameworks` | Base lookup plus required `InstitutionSymbol nvarchar(100)` and nullable `EducationalStageId`; unique `(InstitutionSymbol, EducationalStageId)` |
| `SchoolClasses` | Base lookup; UI may label this as "Classes" |
| `GradeLevels` | Base lookup |
| `EmployeeRoles` | Base lookup; UI may label this as "Roles" |
| `EducationalStages` | Base lookup |
| `EducationTypes` | Base lookup |
| `LocalityDistrictNationals` | Base lookup |
| `DiscussionCodes` | Base lookup |
| `Institutions` | `InstitutionSymbol int`, `Name`, nullable locality/district/sector/type/stage FKs, unique `(InstitutionSymbol, EducationalStageId)` |

### System Tables

| Table | Notes |
|-------|-------|
| `ReportStatuses` | Fixed ids, `Name`, optional `Description` |
| `UserStatuses` | Fixed ids, `Name` |
| `UserRoles` | Fixed ids, `Name`, optional `Description` |
| `SystemConstants` | Unique `Key nvarchar(200)`, required `Value nvarchar(1000)` |
| `EmailServerSettings` | SMTP settings with timestamps |
| `EmailTemplates` | `TypeDescription`, `Subject`, `Body`, `IsActive`, timestamps |

### Core Tables

| Table | Notes |
|-------|-------|
| `Users` | Employee/account table; unique `IdNumber`; `RoleId` FK to `EmployeeRoles`; `UserRoleId` FK to `UserRoles` |
| `PasswordHistories` | Password history records |
| `PasswordResetTokens` | Email reset token hashes with unique `TokenHash` |
| `TwoFactorCodes` | Email TFA code hashes |
| `Allocations` | Unique `(UserId, ProjectId)`; employment scopes are `decimal(18,4)`; `OutputDuration nvarchar(500)` |
| `ReportingMonths` | Active month metadata; single-active behavior is enforced by application logic |
| `Reports` | Unique `(UserId, ReportingMonthId)` |
| `ReportRows` | Allocation-scoped activity rows; nullable conclusion FKs; `MeetingDuration decimal(18,4)` |
| `DocumentAttachments` | Employee-level and row-level attachments |
| `InspectorAssignments` | Inspector scope rules |
| `ReminderLogs` | Reminder history keyed by user/month/template/sent time |

### Allocation Junction Tables

| Table | Key |
|-------|-----|
| `AllocationDistricts` | `(AllocationId, DistrictId)` |
| `AllocationSectors` | `(AllocationId, SectorId)` |
| `AllocationLocalities` | `(AllocationId, LocalityId)` |
| `AllocationPrograms` | `(AllocationId, ProgramId)` |
| `AllocationFrameworks` | `(AllocationId, FrameworkId)` |
| `AllocationSubjects` | `(AllocationId, SubjectId)` |
| `AllocationDomains` | `(AllocationId, DomainId)` |
| `AllocationEducationalPrograms` | `(AllocationId, EducationalProgramId)` |
| `AllocationClasses` | `(AllocationId, ClassId)` where `ClassId` references `SchoolClasses` |
| `AllocationGradeLevels` | `(AllocationId, GradeLevelId)` |
| `AllocationDiscussionCodes` | `(AllocationId, DiscussionCodeId)` |
| `AllocationLocalityDistrictNationals` | `(AllocationId, LocalityDistrictNationalId)` |

## Drift Fixed In This Audit

| Previous Doc/Viewer Drift | Corrected To |
|---------------------------|--------------|
| `Classes` as physical table name | `SchoolClasses` physical table; "Classes" is UI wording |
| `Roles` as physical employee-role table | `EmployeeRoles` physical table |
| Singular `LocalityDistrictNational` / `AllocationLocalityDistrictNational` | Plural physical tables `LocalityDistrictNationals` and `AllocationLocalityDistrictNationals` |
| `PasswordHistory` singular | `PasswordHistories` physical table |
| Missing `PasswordResetTokens`, `TwoFactorCodes`, `ReminderLogs` DDL in implementation plan | Added |
| `Allocations` decimal precision shown as `decimal(10,2)` | `decimal(18,4)` |
| `ReportRows.MeetingDuration` shown as `decimal(5,2)` | `decimal(18,4)` |
| `SystemConstants.Key/Value` lengths shown as 100/500 | `nvarchar(200)` / `nvarchar(1000)` |
| `Users.RoleId` FK shown to `Roles` | FK to `EmployeeRoles` |
| `ReportingMonths` documented with DB unique `(Month, Year)` | No DB unique index currently; app logic controls active-month behavior |


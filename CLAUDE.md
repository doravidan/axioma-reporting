# Axioma Employee Reporting System (מערכת דיווח עובדים אקסיומא)

## Project Overview

Web-based employee activity reporting system for an organization with hundreds of employees across multiple districts.
Replaces manual Excel-based workflows with an automated web platform.

- **Technology:** ASP.NET Core (.NET 8+), Entity Framework Core, SQL Server Express
- **UI:** ASP.NET Core MVC with Razor Views + JavaScript/AJAX, fully responsive, full RTL Hebrew support
- **Key Libraries:** ClosedXML (Excel), MailKit (email), FluentValidation, BCrypt.Net

## Key Documents

- [DEVELOPER_GUIDE.md](DEVELOPER_GUIDE.md) — **Start here** — comprehensive onboarding doc: architecture, DB schema, routes, services, auth, business rules
- [SPEC.md](SPEC.md) — Full system specification (all requirements, rules, tables, fields)
- [IMPLEMENTATION_PLAN.md](IMPLEMENTATION_PLAN.md) — Comprehensive implementation plan with database DDL, phases, acceptance criteria
- [IMPLEMENTATION_STATUS.md](IMPLEMENTATION_STATUS.md) — Living record of what is built and what remains
- [SPEC_TRACEABILITY_AUDIT.md](SPEC_TRACEABILITY_AUDIT.md) — Spec-to-code coverage matrix
- [CLIENT_CLARIFICATIONS.md](CLIENT_CLARIFICATIONS.md) — Open questions and answers from the client
- [DATA_IMPORT_MAPPING.md](DATA_IMPORT_MAPPING.md) — Excel seed-data column mapping reference
- [TESTING.md](TESTING.md) — Test infrastructure overview
- [prd.json](prd.json) — RALPH PRD with 24 user stories and dependencies
- [tools/db-schema-viewer.html](tools/db-schema-viewer.html) — Interactive HTML schema browser
- [docs/screenshots/](docs/screenshots/) — App screenshots
- [.claude/agents/](.claude/agents/) — 13 specialized agent definitions for parallel worktree development

## Architecture

```
Clean Architecture:
  AxiomaReporting.Core           → Entities, Interfaces, DTOs, Enums
  AxiomaReporting.Infrastructure → EF Core DbContext, Repositories, Services, Background Jobs
  AxiomaReporting.Web            → ASP.NET Core MVC/Razor, Controllers, Views, wwwroot
  AxiomaReporting.Tests          → xUnit + FluentAssertions
```

## Compliance

- **IS 5568 / WCAG 2.1 AA** — Mandatory Israeli web accessibility standard. Already implemented:
  skip-link, `lang="he"`, `aria-label` on all nav/buttons/modals, `role="alert"` + `aria-live` on all
  alerts, `scope="col"` on tables, `aria-sort` on sortable headers, `for` on all modal form labels,
  `aria-required` on required inputs, visible focus indicators (yellow outline). Any new UI must maintain compliance.

## Critical Business Rules (NEVER skip these)

1. Password: min 8 chars (letters+digits), lock after 3 fails, history of 5, rotate every 3 months
2. Only admin can promote another user to admin
3. Lookup table deletion: ALWAYS check if value is in use before allowing
4. Only ONE reporting month can be active at any time
5. Institution symbol must be unique per educational stage
6. All employee dropdown values MUST be filtered by their specific allocations
7. No reporting on employee's defined rest day
8. Daily max 9 hours unless "Unlimited" is set
9. Duplicate row detection: same date + same values + empty/identical notes = blocked
10. Notes similarity check with configurable threshold percentage
11. Excel upload overwrites ONLY unapproved reports
12. Employee Excel upload = current month only; PM can upload for locked months
13. Inspector-View can ONLY export approved reports
14. All "שעות" (hours) terminology REMOVED from UI — use activity-based terms
15. One allocation per employee per project: `UNIQUE (UserId, ProjectId)`
16. Report rows store `AllocationId`; row limits are validated per allocation
17. Inspector scoping: AND within one assignment row, OR across rows, NULL as wildcard
18. Notes similarity: normalized Levenshtein similarity within the same report, default threshold 90%

## Terminology (ALWAYS use these)

| DO NOT USE | USE INSTEAD |
|-----------|-------------|
| דיווח שעות | פעילות חודשית |
| מספר עובד | קוד עובד |
| פרטי פרויקט | פרטי הקצאה |
| היקף שעות שנתי | היקף העסקה שנתי |
| היקף שעות חודשי | היקף העסקה חודשי |
| היקף שעות לשורת דיווח | משך תפוקה |

## Coding Patterns

- **Entities** go in `Core/Entities/` with data annotations minimal — prefer Fluent API
- **Repositories** implement interfaces from `Core/Interfaces/`
- **Services** in `Infrastructure/Services/` for business logic
- **Validation** via FluentValidation validators in `Infrastructure/Validators/`
- **Background jobs** as `IHostedService` in `Infrastructure/BackgroundJobs/`
- **Excel operations** via ClosedXML in `Infrastructure/Services/ExcelService.cs`
- **Email** via MailKit in `Infrastructure/Services/EmailService.cs`

## User Roles (6 levels)

1. **System Admin** — full access, manages everything
2. **Project Manager** — manages employees/allocations, opens months, overrides report status
3. **Project Coordinator** — creates employees/allocations, approves reports (can't edit approved)
4. **Inspector-View** — read-only scoped to assigned group, exports approved only
5. **Inspector-Approval** — same as view + can approve/reject
6. **Employee** — sees only their own data, fills reports

## Skills Available

### .NET Development (from dotnet-skills)
- `efcore-patterns` — EF Core best practices
- `csharp-coding-standards` — C# conventions
- `csharp-api-design` — API design patterns
- `database-performance` — Query optimization
- `project-structure` — .NET project organization
- `mjml-email-templates` — Email templates
- `microsoft-extensions-dependency-injection` — DI patterns

### Engineering (from alirezarezvani/claude-skills)
- `database-designer` — Schema design
- `database-schema-designer` — Detailed schema
- `sql-database-assistant` — SQL queries
- `spec-driven-workflow` — Spec-to-code workflow
- `migration-architect` — Database migrations
- `api-design-reviewer` — API review
- `senior-backend` — Backend architecture
- `senior-qa` — Testing strategy
- `senior-security` — Security audit
- `senior-architect` — Architecture decisions
- `code-reviewer` — Code review
- `tdd-guide` — Test-driven development
- `senior-fullstack` — Full-stack patterns
- `git-worktree-manager` — Parallel worktree setup and cleanup

### Skill Coverage by Workstream

| Workstream | Primary Skills |
|------------|----------------|
| Scaffolding | `project-structure`, `csharp-coding-standards`, `microsoft-extensions-dependency-injection` |
| Database/schema | `database-designer`, `database-schema-designer`, `efcore-patterns`, `sql-database-assistant` |
| Migrations/data load | `migration-architect`, `database-designer`, `sql-database-assistant` |
| Backend/API/services | `senior-backend`, `csharp-api-design`, `api-design-reviewer`, `microsoft-extensions-dependency-injection` |
| Auth/security | `senior-security`, `csharp-coding-standards`, `code-reviewer` |
| UI/full-stack | `senior-fullstack`, `csharp-coding-standards`, `code-reviewer` |
| Email/templates | `mjml-email-templates`, `senior-backend` |
| QA/testing | `senior-qa`, `tdd-guide`, `code-reviewer` |
| Parallel worktrees | `git-worktree-manager` |

## Running RALPH

```bash
# Check status
/ralph --status

# Run autonomous development (sequential)
/ralph-run

# Run with parallel agents
/ralph-run --parallel 4

# Run specific story
/ralph-run --task "Implement AX-008: Generic lookup table CRUD"
```

## Parallel Agents (in `.claude/agents/`)

Each agent is a separate file with focused instructions and assigned stories:

| Agent | File | Stories | Description |
|-------|------|---------|-------------|
| scaffolding-lead | `scaffolding-lead.md` | AX-001 | Solution scaffolding, project references, package baseline |
| db-architect | `db-architect.md` | AX-002,003,004 | Database schema, EF Core entities, migrations |
| auth-engineer | `auth-engineer.md` | AX-005,006,007 | Login, password policy, TFA, RBAC |
| lookup-tables | `lookup-tables.md` | AX-008,009,010 | Generic CRUD, special tables, system tables |
| employee-manager | `employee-manager.md` | AX-011-014 | Employee card, allocations, lists, bulk ops |
| reporting-engine | `reporting-engine.md` | AX-015,016,017 | Report form, 10 validation rules, workflow |
| excel-handler | `excel-handler.md` | AX-018,022 | Import, export, error reporting |
| dashboard-builder | `dashboard-builder.md` | AX-019,020 | Dashboard, cascading filters, approvals |
| background-services | `background-services.md` | AX-021 | Reminder service, email service |
| ui-polish | `ui-polish.md` | AX-023 | RTL, branding, terminology, responsive polish |
| data-migration | `data-migration.md` | AX-024 ✅ | One-time import tooling and initial data load |
| qa-security | `qa-security.md` | AX-025 | Integrated QA, E2E, accessibility, security review |
| deployment-ops | `deployment-ops.md` | AX-026 | IIS deployment, database, SSL, backups, monitoring |

### Parallel Execution Waves

```
Wave 0: scaffolding-lead (AX-001)
Wave 1: db-architect (AX-002, AX-003)
Wave 2: db-architect (AX-004) + auth-engineer + lookup-tables
Wave 3: employee-manager + reporting-engine
Wave 4: excel-handler + dashboard-builder + background-services
Wave 5: ui-polish (AX-023) + data-migration (AX-024)
Wave 6: qa-security (AX-025)
Wave 7: deployment-ops (AX-026)
```

### Running Agents

```bash
# Start isolated agents in worktrees
claude --worktree db-schema      # Database work
claude --worktree auth-system    # Authentication
claude --worktree lookup-crud    # Lookup tables

# Each agent works on its own branch, merge when done
```

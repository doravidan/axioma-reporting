---
name: lookup-tables
description: Lookup table CRUD specialist — builds the generic reusable CRUD component for 17+ tables and special table screens (Frameworks, Institutions, Reporting Months, System Tables) in the Axioma Employee Reporting System.
---

You are building the admin module for managing all lookup and system tables in the Axioma Employee Reporting System — an ASP.NET Core web application.

## Context

Read these files for full requirements:
- `SPEC.md` — Sections 15 (Lookup Tables) and 16 (System Tables)
- `IMPLEMENTATION_PLAN.md` — Phase 3: Lookup Table Management

## Your Responsibilities

### Generic Lookup Table CRUD (reusable for 17+ tables)

Build a SINGLE reusable component that works for all simple lookup tables:

- **List view**: Paginated table with right-side scrollbar, page size selector
- **Search**: Free-text search box filtering by description
- **Sort**: Click column headers to sort
- **Add**: Modal form with auto-generated code
- **Edit**: Inline or modal editing
- **Delete**:
  1. Check if value is currently in use (in allocations, reports, etc.)
  2. If in use → error message "Cannot delete — value is in use"
  3. If NOT in use → confirmation dialog "Are you sure you want to delete?"
  4. Delete icon must be a **trash can** icon (not text button)
- **Excel import**: Bulk upload from Excel file per table (ClosedXML)
- **Permissions**: Only System Admin can add/edit/delete. PM and Coordinator can view.

### Special Table: Frameworks (מסגרות)
- Fields: Code (auto), Framework Name, Institution Symbol
- Validation: institution symbol must not already exist for the same educational stage
- Unique constraint: (InstitutionSymbol, EducationalStageId)
- Show error message "Already exists" on duplicate attempt

### Special Table: Institutions (מוסדות)
- Complex form with multiple FK dropdowns: Locality, District, Sector, Type, Educational Stage
- InstitutionSymbol is numeric and unique per educational stage

### Special Table: Reporting Months (חודשי דיווח)
- Calendar picker for Month/Year and Last Reporting Date
- **Active month toggle**: Only ONE month active at a time — activating one auto-deactivates the previous
- Future Reporting flag (Yes/No per month)
- Default Last Reporting Date = fixed day in following month
- Link future reporting flag to employee-level setting
- Permissions exception: System Admin and Project Manager can create/edit/open/activate reporting months. Project Coordinator has view-only access unless separately granted.

### System Tables UI
- **Email Server Settings**: SMTP configuration form (server, port, username, encrypted password, from address, SSL toggle)
- **Email Templates**: List + editor with Subject + Body fields; personalization tokens ({EmployeeName}, {MonthName}, {RejectionReason}). All messages start with "Hello" + Employee Name
- **System Constants**: Key-value table. Admin can EDIT values but NOT delete rows
- **Report Statuses / User Statuses / User Roles**: Read-only display (no editing in UI)

## Where to Write Code

- Generic CRUD controller: `src/AxiomaReporting.Web/Controllers/LookupController.cs`
- Generic CRUD views: `src/AxiomaReporting.Web/Views/Lookup/`
- Special table controllers: `src/AxiomaReporting.Web/Controllers/Admin/`
- Services: `src/AxiomaReporting.Infrastructure/Services/LookupService.cs`
- Validation: `src/AxiomaReporting.Infrastructure/Validators/`

## Stories Assigned
- AX-008: Generic lookup table CRUD
- AX-009: Special tables (Frameworks, Institutions, Reporting Months)
- AX-010: System tables management UI

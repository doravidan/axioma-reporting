---
name: dashboard-builder
description: Dashboard and approval screens specialist — builds the reports dashboard with cascading filters and the summary/approval screen with bulk operations for the Axioma Employee Reporting System.
---

You are building the dashboard and approval screens for the Axioma Employee Reporting System — an ASP.NET Core web application with full RTL Hebrew support.

## Context

Read these files for full requirements:
- `SPEC.md` — Section 12 (Dashboard) and Section 13 (Summary & Approval Screen)
- `IMPLEMENTATION_PLAN.md` — Phase 7: Dashboard & Reports

## Your Responsibilities

### Dashboard — Reports View (דשבורד דיווחים)

**Filter Bar** with all dimensions:
- District (מחוז)
- Sector (מגזר)
- Program (תוכנית)
- Employee Code (קוד עובד)
- ID Number (ת.ז)
- Employee Name
- Report Status
- **Month range**: from salary month X to salary month Y

**Cascading Filters**: Selecting a district automatically filters all other dropdowns to show only values that exist under that district (employees, sectors, programs, etc.).

**Behavior**:
- Table is EMPTY on page load — data shows only after clicking "Show" (הצג) button
- Page size selector on the LEFT side of screen
- Column sorting by clicking headers
- Document attachment indicator per row
- Status filter: Reported / Not Yet Reported / All

**Allocation Context**:
- Show monthly row allocation and remaining row count per employee (from Allocation.MonthlyRowAllocation minus rows already reported)

**Actions**:
- Export filtered results to Excel
- "Summary Screen" (מסך סיכום) navigation button

### Summary & Approval Screen (מסך סיכומים ואישור דיווחים)

Accessed from Dashboard's "Summary Screen" button.

**Display**: One row per employee's report:
- Total rows reported
- Total hours reported
- Remaining balance to report

**Actions per row**:
- **Approve**: status → "Approved", confirmation email sent to employee
- **Reject**: opens popup requiring rejection reasons text → status "Returned for Correction", email sent with reasons

**Bulk Operations**:
- Checkbox per row
- "Select All" button
- "Deselect All" button
- Multi-select for batch approve

**Not-Reported View**:
- Filter/view showing employees who have NOT submitted for the selected month
- Status filter: Reported / Not Yet Reported / All

### Inspector Scoping

- **Inspector-View**: sees ONLY employees in their assigned group (by program/district/sector via InspectorAssignments table). Can export approved reports only.
- **Inspector-Approval**: same scope + can approve/reject reports within that scope.
- All queries MUST filter by InspectorAssignments for these roles.
- Assignment semantics: non-null fields within one InspectorAssignments row are AND, NULL is wildcard, and multiple rows for the same inspector are OR/unioned.

## Where to Write Code

- Dashboard controller: `src/AxiomaReporting.Web/Controllers/DashboardController.cs`
- Dashboard views: `src/AxiomaReporting.Web/Views/Dashboard/`
- Summary controller: `src/AxiomaReporting.Web/Controllers/SummaryController.cs`
- Summary views: `src/AxiomaReporting.Web/Views/Summary/`
- Filter service: `src/AxiomaReporting.Infrastructure/Services/DashboardFilterService.cs`

## Stories Assigned
- AX-019: Dashboard with cascading filters
- AX-020: Summary and approval screen with bulk operations

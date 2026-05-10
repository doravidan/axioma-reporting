---
name: employee-manager
description: Employee and allocation management specialist — builds employee card (Blue Card), allocation screen (Green Card), employee list with bulk operations, and allocation list for the Axioma Employee Reporting System.
---

You are building the employee and allocation management module for the Axioma Employee Reporting System — an ASP.NET Core web application with full RTL Hebrew support.

## Context

Read these files for full requirements:
- `SPEC.md` — Sections 6 (Employee/User Card) and 7 (Employee/User List Screen)
- `IMPLEMENTATION_PLAN.md` — Phase 4: Employee & Allocation Management

## Your Responsibilities

### Employee Card — Blue Card (separate screen)

All fields per SPEC Section 6.1:

| Field | Type | Notes |
|-------|------|-------|
| User Number (מספר משתמש) | Auto-generated, grayed out | Read-only |
| Employee Code (קוד עובד) | Numeric input | Required |
| First Name | Text | Required |
| Last Name | Text | Required |
| ID Number (ת.ז) | Text | Required, unique, used as username |
| Role (תפקיד) | Dropdown from Roles table | e.g., Teacher, Manager |
| Reporting Employee (עובד מדווח) | Checkbox | If checked: show green allocation panel |
| Password | Hidden field | Never visible |
| Notes (הערות) | Free text textarea | Optional |
| Rest Day (יום מנוחה) | Dropdown (Sunday-Saturday) | Used in validation rules |
| Future Reporting (דיווח עתידי) | Checkbox | Allows future date reporting |
| Status | Dropdown (Active/Inactive/Locked) | Required |
| Employee Documents | File upload/list/delete | Stored through DocumentAttachments.UserId with visual indicator |

### Allocation Details — Green Card (SEPARATE screen from employee)

| Field | Type | Notes |
|-------|------|-------|
| Project (פרויקט) | Dropdown from Projects table | Per allocation |
| District (מחוז) | **Multi-select** from Districts | |
| Program (תוכנית) | **Multi-select** from Programs | |
| Sector (מגזר) | **Multi-select** from Sectors | |
| Annual Employment Scope (היקף העסקה שנתי) | Decimal | |
| Monthly Employment Scope (היקף העסקה חודשי) | Decimal | Per employment agreement |
| Daily Employment Scope (היקף העסקה יומי) | Numeric up to 9 OR "Unlimited" | |
| Monthly Row Allocation (הקצאת שורות חודשית) | Integer | Used by monthly row-limit validation |
| Annual Row Allocation (הקצאת שורות שנתית) | Integer | Used by annual row-limit validation |
| Output Duration (משך תפוקה) | Multi-select: 0.5, 1, 1.5, 2, 2.5, 3, Unlimited | Display raw values without unit suffix |
| Allow Excel Upload | Checkbox | |
| Notes (הערות) | Free text | Optional |

Allocation cardinality: one allocation per employee per project. Do not allow duplicate `(UserId, ProjectId)` allocations in the UI; show/edit a separate allocation record for each project.

### Allocation Assignment Panel (lower-right of allocation screen)
- Right panel: dropdown to select which lookup table to assign from
- Middle panel: show available values from selected table
- Click to add value to employee's allocations
- Red X icon to remove assigned value
- Supports initial data import from Excel

### Employee List Screen (רשימת משתמשים / עובדים)
- Paginated table showing all Blue Card fields
- Filter bar: search by any displayed column
- Sort by clicking any column header
- Blue button per row to open employee card
- "Add New Employee" button
- Export filtered results to Excel (ClosedXML)
- "Locked" indicator column when employee is locked
- Employee Notes + Allocation Notes columns
- **Bulk operations**: checkbox selection → change status / change allocation for multiple employees
- Multi-value display for employees with multiple sectors/districts
- Filter/group employees by project view

### Allocation List Screen (separate)
- Displays user details + allocation details combined
- Same filtering, sorting, export capabilities as employee list
- Allocation Notes column reflected

## Where to Write Code

- Controllers: `src/AxiomaReporting.Web/Controllers/EmployeeController.cs`, `AllocationController.cs`
- Views/Pages: `src/AxiomaReporting.Web/Views/Employee/`, `Views/Allocation/`
- Services: `src/AxiomaReporting.Infrastructure/Services/EmployeeService.cs`, `AllocationService.cs`
- DTOs: `src/AxiomaReporting.Core/DTOs/`

## Stories Assigned
- AX-011: Employee card (Blue Card)
- AX-012: Allocation details (Green Card)
- AX-013: Employee list with bulk operations
- AX-014: Allocation list screen

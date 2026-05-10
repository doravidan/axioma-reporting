# Employee Activity Reporting System - Full Specification

**Client:** Axioma (אקסיומא)
**Date:** 20/03/2026
**System Name:** Employee Activity Reporting System (מערכת דיווח עובדים)

---

## 1. System Overview

### 1.1 Problem Statement

The company has hundreds of employees spread across multiple districts and localities. Currently, hour reporting is done via Excel files sent by email. The process of checking, validating, and merging these Excel files into a single report is manual, tedious (Sisyphean), and extremely time-consuming.

### 1.2 Proposed Solution

A **WEB-based system** to receive employee activity reports and process them as automatically as possible, providing an accurate, clear, fast, and up-to-date picture of all reports.

### 1.3 Core Capabilities

1. **Aggregated data display** across multiple dimensions (program name, district, sector, salary month) — viewable in-browser and exportable to Excel based on defined filters.
2. **Clear separation between reporting months** per reporting employee, and when generating cumulative data for all employees and/or all defined months.
3. **Configurable employee profiles** with categorization fields (sector, district, program, activity hours scope, etc.).
4. **Configurable field types**: required/optional, numeric-only, free text (unlimited characters), selection from a defined list per employee/employee group.
5. **Business rules and restrictions** (e.g., no reporting on rest days per employee sector, no duplicate report rows, mandatory fields).
6. **Allocation management**: define per employee the number of rows to report monthly/yearly, with real-time display of count, sum, and remaining balance from their allocation.
7. **Excel upload interface** adapted for both desktop and smartphone.
8. **Branded login page** with the program name/company logo.

---

## 2. Technology Stack

| Component | Technology |
|-----------|-----------|
| Backend | .NET (Microsoft) |
| Database | SQL Server Express |
| Frontend | Web (responsive — desktop + smartphone) |
| Hosting | Secured server with firewall |
| SSL | Domain encrypted with SSL certificate (included in hosting costs, renewed annually) |

---

## 3. Authentication & Security

### 3.1 Password Policy

| Rule | Value |
|------|-------|
| Minimum length | 8 characters (letters + digits) |
| Failed attempts before lockout | 3 |
| Password history | Last 5 passwords (stored in password history table) |
| Password rotation | Every 3 months |
| Default username | Employee ID number (ת.ז) |
| Default password | Employee ID number (ת.ז) |
| First login | Must change password; display Terms of Use message |
| Terms of Use text | To be provided by client |

### 3.2 Two-Factor Authentication (TFA) — Optional

- Implementation decision, 2026-04-12: TFA uses email only.
- After password authentication, a code is sent via email.
- User must enter the code to complete login.
- SMS authentication is out of scope. SMS reminders remain a future optional add-on only if the client selects a provider and approves that scope.

### 3.3 Password Recovery

- "Forgot password" screen (wireframe required).
- System Admin / Project Manager can reset passwords for employees at "Field Coordinator" (רכז מטה) permission level.

---

## 4. User Roles & Permissions

### 4.1 Role: System Administrator (מנהל מערכת)

| Capability | Details |
|-----------|---------|
| CRUD operations | Full — create, edit, save, delete everything |
| Code/lookup tables | Can add, edit values in ALL lookup tables |
| Table deletion | Only after system verifies the value/table is not in use |
| User management | Can create/edit/view/reset password for users at Field Coordinator level |
| Reports | Full access — all reports, all data, no field restrictions |
| Report status | Can modify reports regardless of status |
| Admin promotion | **Only an admin can promote another user to admin** |

### 4.2 Role: Project Manager (מנהל פרויקט)

| Capability | Details |
|-----------|---------|
| Employee records | Create, update, delete employee records and allocations |
| Salary months | Opens salary months for reporting |
| Lookup tables | View-only access |
| User management | Can create/edit/view/reset password for users at Field Coordinator level |
| Reports | Full access — all reports, all data, no field restrictions |
| Report status | Can modify reports regardless of status |
| Excel upload | Can upload Excel to any employee's environment, even for locked months |

### 4.3 Role: Project Coordinator (רכז פרויקט)

| Capability | Details |
|-----------|---------|
| Reports | Full view + export for all reports, no field restrictions |
| Report editing | Can edit/delete reports **only** in "Entry" or "Pending Approval" status. **Cannot** edit/delete approved reports |
| Allocations | Can create allocations for employees |
| Employee creation | Can create employees |
| Report approval | Can approve reports |
| Report rejection | Can return reports for correction |

### 4.4 Role: Inspector — View Only (מפקח הרשאת צפייה)

| Capability | Details |
|-----------|---------|
| Visibility scope | Only employees in their assigned group (by program AND/OR district AND/OR sector) |
| View reports | Can view reports for their employees across different months |
| Editing | **Cannot** add, delete, or edit anything in the system |
| Excel export | Only for reports in "Approved" status, filtered by available months and screen filters |

### 4.5 Role: Inspector — Activity Approval (מפקח הרשאת אישור פעילות)

| Capability | Details |
|-----------|---------|
| Same as View Inspector | Plus ability to **approve reports** for their assigned employee group (by program AND/OR district AND/OR sector) |

### 4.6 Role: Employee (עובד)

| Capability | Details |
|-----------|---------|
| Visibility | Only their own allocation rows and reports across different months |
| Reporting | Can fill and submit their own monthly activity reports |

---

## 5. Navigation & Menu Structure

### 5.1 Login Screen

- Company/program logo only (SITE logo — provided by client via email).
- "Login to System" (כניסה למערכת) label.
- "Forgot Password" link/screen.
- Client will purchase their own domain for the site.
- Dedicated email address for system notifications.
- Dedicated phone number if SMS integration is implemented.

### 5.2 Main Menu (varies by role)

| Role | Visible Menu Bars |
|------|------------------|
| Admin | Monthly Activity + Dashboard + Admin |
| Regular Employee | Monthly Activity only |
| Management (non-admin) | Monthly Activity + Dashboard |

### 5.3 Top Bar

- Right side: "Hello, [Username]..." greeting.
- Left side: Home icon + Logout icon.

### 5.4 Monthly Activity Sub-Menu (פעילות חודשית)

Clicking "Monthly Activity" reveals:
1. **My Allocations** (ההקצאות שלי) — clicking opens two sub-options:
   - **Monthly Activity Update Screen** (מסך עדכון פעילות חודשית)
   - **Monthly Excel File Upload** (העלאת קובץ אקסל חודשי)
   - **NOTE**: After clicking "My Allocations", the relevant salary month must be displayed so the employee knows which month they are reporting for.
2. Excel upload is visible only to employees with upload permission.

### 5.5 Admin Sub-Menu

Opens a sub-menu with administrative options:
- User/employee management
- Lookup table management
- Email server settings (updating outgoing email mailbox details)
- System constants

---

## 6. Employee/User Card (כרטיס משתמש/עובד)

### 6.1 Employee Details (Blue Card)

| Field | Type | Notes |
|-------|------|-------|
| User Number (מספר משתמש) | Auto-generated | Read-only (grayed out), unique sequential ID |
| Employee Code (קוד עובד) | Numeric | Company employee number (renamed from "מספר עובד") |
| First Name (שם פרטי) | Text | Required |
| Last Name (שם משפחה) | Text | Required |
| ID Number (ת.ז) | Text | Used as username |
| Role (תפקיד) | Selection from EmployeeRoles table | e.g., Teacher, Manager |
| Reporting Employee (עובד מדווח) | Checkbox | If checked: employee must fill monthly report; shows green allocation panel and lower-right reporting panel |
| Password | Hidden | Not displayed in clear text |
| Notes (הערות) | Free text | Added field |
| Rest Day (יום מנוחה) | Selection | Added field — determines which day the employee cannot report |
| Future Reporting (דיווח עתידי) | Checkbox | Added field — allows this employee to report for future dates |
| Status | Selection | Active, Inactive, Locked |

### 6.2 Allocation Details (Green Card — renamed from "Project Details" to "Allocation Details")

Allocation cardinality: an employee may have multiple allocations across projects, but only one allocation per employee per project.

| Field | Type | Notes |
|-------|------|-------|
| Project (פרויקט) | Selection from table per allocation | Multi-select allowed |
| District (מחוז) | Selection from table per allocation | **Multi-select allowed** |
| Program (תוכנית) | Selection from table per allocation | **Multi-select allowed** |
| Sector (מגזר) | Selection from table per allocation | **Multi-select allowed** |
| Annual Employment Scope (היקף העסקה שנתי) | Numeric | Renamed from "היקף שעות שנתי" |
| Monthly Employment Scope (היקף העסקה חודשי) | Numeric | Per employment agreement |
| Daily Employment Scope (היקף העסקה יומי) | Numeric | **Added field** — values up to 9 hours/day OR "Unlimited" |
| Monthly Row Allocation (הקצאת שורות חודשית) | Numeric | Maximum report rows allowed per salary month for this allocation |
| Annual Row Allocation (הקצאת שורות שנתית) | Numeric | Maximum report rows allowed per year for this allocation |
| Output Duration (משך תפוקה) | Selection | Renamed from "היקף שעות לשורת דיווח". Values: 0.5, 1, 1.5, 2, 2.5, 3, Unlimited. **Multi-select allowed**. Values align with Meeting Duration increments and are displayed without a unit suffix due terminology requirements. Set by Manager/Coordinator per employment scope |
| Allow Excel Upload (אפשר העלאת קובץ דיווח) | Checkbox | Whether employee can report via Excel |
| Notes (הערות) | Free text | Added field |

### 6.3 Allocation Assignment (Lower-right panel)

- Right panel: Select a lookup table.
- Middle table: Select a value to add.
- Remove values by clicking the red X icon.
- Initial data loaded from Excel files provided by client during setup.

### 6.3.1 Project → Program Cascading Filter

- The Program dropdown on an allocation is filtered to the Programs mapped to the currently selected Project via the `ProjectPrograms` junction.
- If a Project has no `ProjectPrograms` rows, all active Programs are shown (backward-compatible default).
- Admin/Project Manager manages Project→Programs mappings on the **"ניהול תוכניות לפי פרויקט"** screen (`/Admin/ProjectPrograms`).
- The AJAX endpoint `GET /Employee/ProgramsForProject?projectId=N` returns the filtered set as JSON for the client-side cascade. On first render the server pre-filters the options so no flash of "all programs" is seen.

### 6.4 Screen Separation

The client requested clear separation between:
- **User Details Screen** — displays all fields from the Blue Card.
- **Allocation Details Screen** — displays user details + their allocation information from the Green Card.

---

## 7. Employee/User List Screen (רשימת משתמשים / עובדים)

### 7.1 Features

- Filter and search by any value in the filter bar.
- Sort by any column by clicking column header.
- Click blue button to open employee card.
- Export filtered list to Excel.
- Add new employee button.
- **Bulk operations**: change status, change allocation for multiple employees at once (e.g., set 10 employees from "Active" to "Inactive").

### 7.2 Columns

- Employee Code (קוד עובד) — renamed from "מספר עובד"
- All fields from the Blue Card
- "Locked" status indicator — visible when employee is locked
- Notes field (reflected from employee card)
- Notes field (reflected from allocation details)

### 7.3 Display for Multi-Value Fields

- When an employee has multiple sectors, districts, etc., the display must handle showing multiple values per row.
- Display employees per project view is needed.

### 7.4 Allocation Dashboard (דשבורד הקצאות)

- Dedicated screen at `/Employee/AllocationList` listing every allocation in the system.
- Filter bar (per-column): Project (dropdown), Program (multi-select), District (multi-select), Sector (multi-select), ID, Employee Code, First Name, Last Name, Monthly Employment Scope, Annual Employment Scope, Output Duration (multi-select substring), Notes.
- **"הצג הכל" (Show All) toggle** above the filter bar: when off, multi-value columns are intersected with the filter selection; when on, every defined value for that row is shown comma-separated.
- Per-row pencil icon (`aria-label="פרטי הקצאה"`) → navigates to the allocation edit screen.
- Excel export respects all active filters and the Show All toggle.

---

## 8. Reporting — Excel Upload

### 8.1 Flow

1. Employee (if permitted) selects "Excel Upload" option.
2. Screen adapted for smartphone.
3. Employee selects file for upload.
4. File is validated against all defined rules (see Section 10).
5. **If valid**: data is imported to database; user gets on-screen success message + confirmation email.
6. **If invalid**: list of issues displayed on screen; option to export issues as PDF.
7. Uploading a new file for an unapproved month **overwrites** the previous file and its data.
8. After successful import, data is also displayed in the standard online reporting screen.

### 8.2 Rules

- Employee upload is for the **current open month only**.
- Project Manager can upload Excel to an employee's environment **even for locked months**.
- **Batch multi-employee Excel upload** (client-provided template with Hebrew text values, not IDs) is available to System Admin and Project Manager only. See §8.3.

### 8.3 Batch Multi-Employee Excel Upload

Flow: Admin/PM uploads one Excel containing monthly rows for many employees. Rows are validated individually; valid rows are imported per employee (creating the target employee's monthly `Report` in status "In Entry" if missing), invalid rows are rejected.

**Input format (tolerant):**
- Single sheet with Hebrew headers. The header row is detected dynamically by scanning rows 1–15 for the cell "קוד עובד".
- Lookup columns carry **text descriptions** (e.g., district "צפון"), not numeric IDs. A shared `ILookupResolver` resolves text to IDs with case-insensitive exact-description match; numeric input is also accepted.
- A new lookup "סוג דיווח" (ReportType) is supported with seeded values "ארצי מחוזי" and "יישובי מוסדי", persisted as `ReportRow.ReportTypeId`.

**Allocation resolution per row:** the service picks the single active allocation for the employee whose District/Locality/Framework/EducationalProgram sets all contain the row's resolved IDs. If zero or multiple match, the row is rejected with a specific Hebrew error.

**On success per row:** a `ReportReceived` email is sent to the employee (one per affected employee, not per row).

**On completion:**
- `BatchImportSuccessUploader` email to the uploader with `RowsImported`, `EmployeesCount`, `Month`, `Year`.
- If any errors occurred, a `BatchImportErrors` email with a PDF attachment listing every error (file row #, employee code, reporter name, Hebrew error text) is also sent to the uploader.
- The results screen shows: summary counts, per-employee breakdown, a scrollable error table, and a "הורד רשימת שגיאות (PDF)" button.

---

## 9. Reporting — Online Form

### 9.1 General Rules

1. **Lookup selections** are always filtered to what was allocated to the specific employee (e.g., only their assigned localities appear in the locality dropdown).
2. **Draft saving**: Employee can partially fill a report, save, and return later. Reports with incomplete data get status "Draft" (טיוטא).
3. **Default sort**: By date and sequential number.
4. **Column sorting**: Clickable column headers for sorting.
5. **Salary month**: Automatically shows the month that is NOT in "Locked" status.
6. **Document upload**: Allow document attachment at both employee level and individual report row level, with visual indicator.

### 9.2 Approval Workflow

```
[Employee fills report] → Status: "In Entry" (בהזנה)
        ↓
[Employee submits] → Status: "Pending Approval" (ממתין לאישור)
        ↓
    ┌─── [Inspector reviews] ───┐
    ↓                           ↓
[Approved]                 [Rejected]
Status: "Approved"         Status: "Returned for Correction" (הוחזר למדווח)
Email sent to employee     Inspector writes rejection reason
                           Email sent with rejection details
                           Employee corrects and resubmits
```

### 9.3 Approval via Summary Screen

- Inspector can approve or reject from the summary screen.
- Bulk approval: checkboxes with "Select All", "Deselect All", and multi-select options.
- Rejection opens a popup requiring rejection reasons before sending.

---

## 10. Report Fields Definition

### 10.1 Field Specifications

| # | Field Name (Hebrew) | Field Name (English) | Type | Required | Source | Notes |
|---|---------------------|---------------------|------|----------|--------|-------|
| 1 | מס"ד | Serial Number | Auto-numeric | Yes | System | Auto-generated per employee, ascending by report date |
| 2 | ת.ז | ID Number | Numeric | Yes | Employee card | Read-only |
| 3 | שם המדווח | Reporter Name | Text | Yes | Employee card | Last name + First name (split as in employee card) |
| 4 | קוד עובד | Employee Code | Numeric | Yes | Employee card | Read-only. Updates if employee card changes. **Note**: changes to employee card fields (ID, name, code) propagate to report display. Assumption: no mid-month changes to these fields. |
| 5 | מחוז | District | Selection | Yes | Allocation table | From districts allocated to employee |
| 6 | ישוב | Locality | Selection | Yes | Allocation table | From localities allocated to employee, per row |
| 7 | שם מסגרת | Framework Name | Selection | Yes | Allocation table | From frameworks allocated to employee, per row |
| 8 | תאריך המפגש | Meeting Date | Date | Yes | User input | Calendar picker or manual entry (YYYY/MM/DD). Can enter previous months. Future dates only if "Future Reporting" enabled |
| 9 | משך המפגש | Meeting Duration | Numeric (decimal) | Yes | User input | In hours (e.g., 1.5). Validated against monthly hourly allocation. Allow save if under limit |
| 10 | תוכנית חינוכית | Educational Program | Selection | Yes | Allocation table | From programs allocated to the project |
| 11 | תחום | Domain | Selection | Yes | Allocation table | From domains allocated to the project |
| 12 | נושא 1 | Subject 1 | Selection | Yes | Allocation table | From subjects allocated to employee's project |
| 13 | נושא 2 | Subject 2 | Selection | No | Allocation table | From subjects allocated to employee's project |
| 14 | קיום דיון | Discussion Held | Selection | No | Allocation table | From a closed list per project definitions (NOT a simple yes/no) |
| 15 | מסקנות - כיתה | Conclusions — Class | Selection | No | SchoolClasses table | |
| 16 | מסקנות - מסגרת חינוכית | Conclusions — Educational Framework | Selection | No | Educational Frameworks table | |
| 17 | מסקנות - ישוב/מחוז/ארצי | Conclusions — Locality/District/National | Selection | No | Lookup table | |
| 18 | שכבה | Grade Level | Selection | No | Grade Levels table | |
| 19 | כיתה | Class | Selection | No | SchoolClasses table | |
| 20 | הערות | Notes | Free text | No | User input | Unlimited characters. Included in duplicate detection via similarity percentage |

### 10.2 Field Configuration

- **Any field can be configured as required/optional** — this is done at the developer level.
- When a field's required status changes, the change applies from that point forward without affecting previously saved data.

---

## 11. Validation Rules

### 11.1 Report Row Validations

| # | Rule | Details |
|---|------|---------|
| 1 | Required fields | All required fields must be filled |
| 2 | Date validation | All dates must be in the current month or previous months, UNLESS "Future Reporting" is enabled for the employee |
| 3 | Monthly row limit | Cannot exceed the monthly row allocation for the employee |
| 4 | Daily hour limit | Cannot exceed 9 hours per day, UNLESS "Unlimited" is set for the employee |
| 5 | Annual row limit | Cannot exceed the annual row allocation for the employee |
| 6 | No duplicate rows | Duplicate = same date + same field values + empty Notes field |
| 7 | No duplicate rows with notes | Duplicate = same date + same field values + identical Notes field |
| 8 | Submission deadline | Report can only be submitted until the deadline date defined in the Reporting Months table |
| 9 | Rest day restriction | Employee **cannot** report on their defined rest day |
| 10 | Notes similarity check | A configurable similarity percentage threshold detects near-duplicate Notes content. Use normalized Levenshtein similarity within the same report (same employee + salary month). The percentage is adjustable by Admin |

### 11.2 Monthly Hour Validation

- Meeting duration is validated against the employee's monthly hourly allocation.
- If the reported amount is **under** the limit, allow saving and continue.
- Monthly and annual row limits are validated per allocation. Each persisted report row stores its allocation context so limits can be calculated correctly.

---

## 12. Dashboard — Reports View (דשבורד דיווחים)

### 12.1 Features

- Search/filter by any field in the filter bar.
- **Cascading filters**: selecting a district shows all related values (employees, IDs, codes, sectors, programs) under that district.
- Export filtered results to Excel.
- "Summary Screen" button — leads to the summary and approval screen.
- Table is empty on entry — data loads only after clicking "Show" (הצג).
- Filter: **salary month range** (from month X to month Y) — added.

### 12.2 Dashboard Columns

- Employee Code (קוד עובד) — renamed from "מספר עובד"
- Page row count selector — moved to the left side of the screen.
- Discussion Held (קיום דיון) — from a closed list per project, NOT yes/no.
- Document attachment — with indicator.
- Filter by report status: Reported, Not Yet Reported, All.
- Monthly Row Allocation and remaining row count per employee (from allocation — display as summary info, not a per-report-row field).

---

## 13. Summary & Approval Screen (מסך סיכומים ואישור דיווחים)

### 13.1 Access

Accessed via the "Summary Screen" button in the Dashboard.

### 13.2 Display

Each row represents one employee's report summary:
- Total rows reported
- Total hours reported
- Remaining balance to report

### 13.3 Actions

| Action | Behavior |
|--------|----------|
| Approve | Report status → "Approved"; confirmation email sent to employee |
| Reject | Popup opens requesting rejection reasons; after submit → status "Returned for Correction"; email sent to employee with rejection notes |
| Bulk Approve | Checkbox-based: Select All / Deselect All / Multi-select |

### 13.4 Missing Reports

- Filter/view for employees who have **not yet reported** — added.
- Status filter: Reported / Not Yet Reported / All.

### 13.5 Inspector Scope Semantics

- In a single inspector assignment row, non-empty program/district/sector fields are combined with AND.
- Empty fields act as wildcards.
- Multiple assignment rows for the same inspector are combined with OR.

---

## 14. Background Services

### 14.1 Reminder Service

A background service responsible for sending reminder notifications to employees who:
- Have not yet submitted their report, OR
- Had their report rejected and need to correct it.

**Schedule**:
- Runs once per day.
- Sends reminder every **X** days.
- Starts sending **Y** days before the reporting deadline.
- Parameters X and Y are configurable in the System Constants table.
- **Channel**: Reminders sent via email. If SMS provider is configured (see Section 3.2), reminders can also be sent via SMS. Channel preference is a system constant.

---

## 15. Lookup Tables (טבלאות אינדקס)

### 15.1 General Rules

1. Every table includes: auto-generated code (ID) + text description.
2. Every table (except Statuses) has: Add button, Edit button, Search box (free text).
3. Right-side scrollbar + pagination for large datasets.
4. These are **master tables** from which values are derived for each employee's allocations.
5. Values shown in this spec are **examples only** — actual data loaded from client-provided Excel files during setup.
6. **Deletion rules**: Before deletion, system checks if value is in use. If yes → error message "Cannot delete". If not in use → confirmation prompt "Are you sure you want to delete?".
7. Delete icon should be a **trash can** icon (across all tables).
8. All tables support **Excel file import** for bulk data loading.

### 15.2 Table: Reporting Months (טבלת חודשי דיווח)

| Field | Type | Notes |
|-------|------|-------|
| Code | Auto-numeric | |
| Description | Text | |
| Month & Year | Calendar picker or manual entry | |
| Last Reporting Date | Calendar picker or manual entry | Default: fixed day in the following month |
| Active | Checkbox | **Only one month can be active at a time**. Activating a month automatically deactivates the previous one |
| Future Reporting | Yes/No | Per calendar month; must be linked to the employee-level future reporting setting |

### 15.3 Table: Frameworks (טבלת מסגרות)

| Field | Type | Notes |
|-------|------|-------|
| Framework Code | Auto-numeric | |
| Framework Name | Text | Renamed from "תאור מסגרת" |
| Institution Symbol (סמל מוסד/מסגרת) | Text | Renamed from "הערות" |

**Rules**:
- Use the institutions table provided by client.
- Validate on add: institution symbol must not already exist.
- Institution symbol can appear only once per educational stage (שלב חינוך) combination.
- If duplicate symbol attempted → display message "Already exists".

### 15.4 Table: Subjects (טבלת נושאים)

| Field | Type |
|-------|------|
| Subject Code | Auto-numeric |
| Subject Description | Text |

### 15.5 Table: Domains (טבלת תחומים)

| Field | Type |
|-------|------|
| Domain Code | Auto-numeric |
| Domain Description | Text |

### 15.6 Table: Authorities/Localities (טבלת רשויות)

| Field | Type |
|-------|------|
| Authority Code | Auto-numeric |
| Authority Description | Text |

### 15.7 Table: SchoolClasses / Classes UI (טבלת כיתות)

| Field | Type |
|-------|------|
| Class Code | Auto-numeric |
| Class Description | Text |

### 15.8 Table: EmployeeRoles / Roles UI (טבלת תפקידים)

| Field | Type |
|-------|------|
| Role Code | Auto-numeric |
| Role Description | Text |

### 15.9 Table: Grade Levels (טבלת שכבות)

| Field | Type |
|-------|------|
| Grade Code | Auto-numeric |
| Grade Description | Text |

### 15.10 Table: Sectors (טבלת מגזרים)

| Field | Type |
|-------|------|
| Sector Code | Auto-numeric |
| Sector Description | Text |

### 15.11 Table: Districts (טבלת מחוזות)

| Field | Type |
|-------|------|
| District Code | Auto-numeric |
| District Description | Text |

### 15.12 Table: Projects (טבלת פרויקטים)

| Field | Type |
|-------|------|
| Project Code | Auto-numeric |
| Project Description | Text |

### 15.13 Table: Educational Programs (טבלת תוכניות חינוכיות)

| Field | Type |
|-------|------|
| Program Code | Auto-numeric |
| Program Description | Text |

### 15.14 Table: Programs (טבלת תוכניות)

| Field | Type |
|-------|------|
| Program Code | Auto-numeric |
| Program Description | Text |

### 15.15 Table: Localities (טבלת ישובים)

| Field | Type | Notes |
|-------|------|-------|
| Locality Code | Numeric | From the national localities table |
| Locality Name | Text | From the national localities table |

### 15.16 Table: Institutions (טבלת מוסדות)

| Field | Type | Notes |
|-------|------|-------|
| Institution Symbol (סמל מוסד) | Numeric | |
| Institution Name (שם מוסד) | Alphanumeric | |
| Locality (ישוב) | Selection | From Localities table |
| District (מחוז) | Selection | From Districts table |
| Sector (מגזר) | Selection | From Sectors table |
| Type (סוג) | Selection | From Types table |
| Educational Stage (שלב חינוך) | Selection | From Educational Stages table |

### 15.17 Table: Educational Stages (טבלת שלבי חינוך)

| Field | Type |
|-------|------|
| Stage Code | Auto-numeric |
| Stage Description | Text |

### 15.18 Table: Education Types (טבלת סוגי חינוך)

| Field | Type |
|-------|------|
| Type Code | Auto-numeric |
| Type Description | Text |

### 15.19 Table: LocalityDistrictNationals / Locality-District-National Lookup (טבלת איתור ישוב/מחוז/ארצי)

| Field | Type |
|-------|------|
| Code | Auto-numeric |
| Description | Text |

### 15.20 Table: Discussion Code (טבלת קוד דיון)

| Field | Type |
|-------|------|
| Code | Auto-numeric |
| Description | Text |

---

## 16. System Tables (טבלאות מערכת)

> **Critical**: These tables are critical to system operation. Only the **developer** has permission to delete/modify records in these tables.

### 16.1 Table: Email Server Settings (טבלת נתוני שרת דואר)

Contains outgoing email server configuration and the mailbox from which automatic system notifications are sent.

### 16.2 Table: Fixed Email Messages (טבלת הודעות מייל קבועות)

Manages the format of fixed system messages.

| Field | Type | Notes |
|-------|------|-------|
| Message Code | Auto-numeric | |
| Message Type Description | Text | Internal use — describes the message purpose |
| Email Subject | Text | Appears in the email subject line |
| Message Content | Text | Appears in the email body |

**Rules**:
- Every message is personalized: "Hello" + Employee Name.
- Message types include: Report received, Report rejected, Reminder to fill report.

### 16.3 Table: Report Status (טבלת סטאטוס דיווח)

Predefined statuses:
- Draft (טיוטא)
- In Entry (בהזנה)
- Pending Approval (ממתין לאישור)
- Approved (מאושר)
- Returned for Correction (הוחזר לתיקון / נשלח לתיקון)
- Locked (נעול)

### 16.4 Table: User Status (טבלת סטאטוס משתמש)

Predefined statuses:
- Active (פעיל)
- Inactive (לא פעיל)
- Locked (נעול)

### 16.5 Table: User Roles/Levels (טבלת תפקיד/רמת משתמשים)

Predefined roles:
- System Administrator (מנהל מערכת)
- Project Manager (מנהל פרויקט)
- Project Coordinator (רכז פרויקט)
- Inspector — View Only (מפקח צפייה)
- Inspector — Activity Approval (מפקח אישור פעילות)
- Employee (עובד)

### 16.6 Table: System Constants (טבלת קבועי המערכת)

Contains various fixed system parameters. System Admin can **edit** but **not delete** records.

| Constant | Description |
|----------|-------------|
| Reminder interval (X days) | How often to send reminders |
| Reminder start (Y days before deadline) | When to start sending reminders |
| Notes similarity threshold (%) | Percentage for duplicate Notes detection |
| Other system-wide parameters | As defined during development |

---

## 17. Email Notifications

| Trigger | Recipient | Content |
|---------|-----------|---------|
| Report submitted successfully | Employee | Confirmation of receipt |
| Report approved | Employee | Approval notification |
| Report rejected | Employee | Rejection reasons included |
| Reminder — report not submitted | Employee | Sent per schedule (X days interval, starting Y days before deadline) |
| Reminder — report needs correction | Employee | Sent per same schedule |
| Batch Excel import — success (per row) | Each affected employee | Standard `ReportReceived` template |
| Batch Excel import — uploader summary | Uploader (Admin/PM) | `BatchImportSuccessUploader` — rows imported, employees affected, month/year |
| Batch Excel import — errors | Uploader (Admin/PM) | `BatchImportErrors` — summary body + attached PDF listing every rejected row |

---

## 18. Terminology Changes (Summary)

| Original Term | New Term |
|---------------|----------|
| דיווח שעות (Hours Reporting) | פעילות חודשית (Monthly Activity) |
| מספר עובד (Employee Number) | קוד עובד (Employee Code) |
| פרטי פרויקט (Project Details) | פרטי הקצאה (Allocation Details) |
| היקף שעות שנתי (Annual Hours Scope) | היקף העסקה שנתי (Annual Employment Scope) |
| היקף שעות חודשי (Monthly Hours Scope) | היקף העסקה חודשי (Monthly Employment Scope) |
| היקף שעות לשורת דיווח (Hours per Report Row) | משך תפוקה (Output Duration) |
| Remove "שעות" (hours) from all fields/screens | System-wide terminology update |

---

## 19. Additional Requirements & Notes

1. **Separate screens**: Employee details (Blue Card) and Allocation details (Green Card) should be separate screens (client request).
2. **Allocation screen list**: Show users per project.
3. **Database table menus**: Need a clear menu entry for database lookup tables.
4. **Notes similarity engine**: Configurable percentage threshold for detecting near-duplicate notes content. Implementation uses normalized Levenshtein similarity within the same report (same employee + salary month).
5. **Field required/optional toggle**: Done at developer level; changes apply forward without retroactive impact.
6. **Final database schema**: To be finalized after client approval of this specification.
7. **Initial data import**: All lookup tables populated from client-provided Excel files as a one-time setup.
8. **Bulk employee Excel upload** (all employees, all programs, month X): Requires additional development (separate quote from vendor).

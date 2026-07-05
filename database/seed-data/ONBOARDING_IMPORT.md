# Client Onboarding Import

`import_onboarding.py` loads the client's onboarding workbooks (16 program files +
one helper/lookup-tables file) into the AxiomaReporting database. It is **idempotent**:
every insert is "insert-if-missing" keyed on a natural key, so it can be re-run safely.

> **PII warning:** the source `*.xlsx` workbooks contain employee ID numbers, phone
> numbers, emails, and birth dates. They are git-ignored (`database/seed-data/onboarding/`)
> and must never be committed.

## Layout expected

```
database/seed-data/onboarding/
  00_helper_lookups.xlsx        # טבלאות עזר — canonical lookup lists
  prog_<program>.xlsx           # one file per program (16 of them)
```

The helper file is auto-detected by its `מסכם טבלאות עזר` / `מה צריך להיות` sheets;
every other file is treated as a program workbook. File names are cosmetic — the
program is read from the `תוכנית` column of each file's allocations sheet.

Each program workbook is expected to contain (sheet titles vary; detection is by
header content, not title):

| Role | Detected by headers | Used for |
|------|--------------------|----------|
| employees | `ת.ז` + `יום מנוחה` + `תפקיד` | Users |
| allocations | `פרוייקט` + `היקף שעות העסקה חודשי`, no `יום מנוחה` | Allocations + scope junctions |
| code_values | `נושא 1` + `קיום דיון` | per-program lookup scope (educational programs, domains, subjects, discussion codes, classes, grade levels, locations) |
| institutions | `סמל מוסד` + (`שם מוסד`/`מסגרת חינוכית`), no `ת.ז` | Institutions + report-selectable Frameworks |
| per_employee_frameworks | `ת.ז` + `סמל מוסד` + `מסגרת חינוכית` | per-employee AllocationFrameworks |

Programs with a `per_employee_frameworks` sheet assign each employee their own
framework list; programs with only an `institutions` sheet share the full program
institution list across all their employees ("רשימה לכל עובד" vs "כלל העובדים").

## Running

```bash
export AXIOMA_CONN_STR="DRIVER={ODBC Driver 18 for SQL Server};SERVER=localhost,1433;DATABASE=AxiomaReporting;UID=sa;PWD=***;TrustServerCertificate=yes;"
export AXIOMA_ONBOARD_DIR="/path/to/onboarding"
python import_onboarding.py            # all stages, in order
python import_onboarding.py lookups    # a single stage
```

Stages (run in this order): `lookups`, `institutions`, `projects`, `employees`,
`allocations`, `frameworks`.

Dependencies: `pyodbc`, `openpyxl`, `bcrypt` (plus an ODBC SQL Server driver).

## What it creates

- **Project**: the single project `חינוך ילדים ונוער בסיכון`.
- **Programs** + **ProjectPrograms** (one per file) + the 6 `ProjectProgram*` scope tables.
- **Lookups**: districts, sectors, educational stages, education types, educational
  programs, domains, subjects, discussion codes, classes, grade levels,
  locality/district/national, localities, report types, employee roles.
- **Institutions** (by symbol, deduped; symbols that overflow int32 are skipped and
  logged) and matching report-selectable **Frameworks**.
- **Users** (reporting employees, `UserRoleId = 6`). Seeded with a shared placeholder
  password and `MustChangePassword = 1`, `AcceptedTermsOfUse = 0` — every employee
  must set their own password and accept terms on first login.
- **Allocations** (one per employee) with monthly/annual scope, daily limit
  (`עד 9 שעות` → 9, `ללא הגבלה` → unlimited/NULL), output durations, report type,
  district, sector, program link, and the per-program lookup scope junctions.
- **AllocationFrameworks** + **AllocationLocalities** (per-employee or shared, per the
  program type; localities derived from each assigned institution's locality).

## Known data caveats (from the delivered files)

- One institution symbol in `משיבים` (`5802944379`, 10 digits) exceeds the int32
  `InstitutionSymbol` column and is skipped.
- One employee (`305702151`) appears in two programs (`חנוך לנער` and
  `מנחי כיתות שח"ר`); a single allocation is created and linked to the
  first-processed program.
- A small number of employees in per-employee-framework programs have no rows in
  the framework-mapping sheet and therefore no assigned frameworks until one is set.

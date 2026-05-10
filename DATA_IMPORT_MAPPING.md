# Client Data File Import Mapping

Updated: 2026-04-12

This document maps the three client-provided data files to the implemented import paths.

## Files Reviewed

| File | Format | Purpose | Implemented Path |
|------|--------|---------|------------------|
| `database/seed-data/טבלאות.xlsb` | XLSB | Base lookup lists, localities, institutions | Admin screen: `ייבוא נתונים ראשוני` -> `ייבוא טבלאות לקוח xlsb`; also supported by `database/seed-data/seed_lookups.py` |
| `database/seed-data/BASE DATA.xlsb` | XLSB | Historical approved report rows | `database/seed-data/seed_reports.py` |
| `database/seed-data/קובץ משותף שאלונים לכל התוכניות 12.3.26.xlsx` | XLSX | Questionnaire/report dropdown catalog by program | Admin screen: `ייבוא נתונים ראשוני` -> `ייבוא קטלוג שאלונים` |

## `טבלאות.xlsb`

The workbook contains the source tables needed before operational use.

| Sheet | Implemented Handling | Target Tables |
|-------|----------------------|---------------|
| `גיליון מרכז רשימות לפי שדות` | Parsed by `seed_lookups.py` | `Districts`, `Sectors`, `EducationalStages`, `Programs`, `EducationalPrograms`, `Domains`, `Subjects`, `DiscussionCodes`, `SchoolClasses` |
| `יישוב` | Parsed by `seed_lookups.py` | `Localities` |
| `מוסדות` | Parsed by `seed_lookups.py` | `EducationTypes`, `Institutions` |
| Status/report sample sheets | Not used for lookup seeding | Reference/sample data only |

Important: this file is `.xlsb`. It has a dedicated admin upload action because the normalized `.xlsx` lookup importer is meant for manually prepared workbooks, not the client's combined source workbook.

## `BASE DATA.xlsb`

This workbook contains historical report examples in sheets named `דיווח מספר 1` through `דיווח מספר 5`.

Implemented handling:

- `seed_reports.py` reads the reporting sheets.
- It creates missing reporting months from row dates.
- It creates reports as approved historical reports.
- It creates stub users if a historical employee code does not already exist.
- It resolves or creates simple lookup rows for values found in historical rows.
- It creates frameworks by institution symbol when possible, with a synthetic symbol fallback for name-only framework values.
- It resolves `ReportRows.AllocationId` when the row's educational program maps to exactly one active employee allocation, or when the employee has exactly one active allocation.

Limitations:

- Historical rows remain without `AllocationId` only when the source data is ambiguous and cannot be mapped to exactly one active allocation.
- This is a one-time seed/migration script, not an end-user upload flow.
- It should be run after `seed_lookups.py`.

## Questionnaire Catalog XLSX

The questionnaire workbook contains a unified catalog sheet plus program-specific sheets.

Implemented handling:

- The admin action `ImportQuestionnaireCatalog` reads the `כללי - מאוחד` sheet.
- Existing rows are skipped by exact description match.
- The import fills only lookup/catalog tables; it does not create employee allocations.

| Column | Source Header | Target Table |
|--------|---------------|--------------|
| A | `תוכנית` | `Projects` |
| B | `תוכנית חינוכית` | `EducationalPrograms` |
| C | `תחום` | `Domains` |
| D | `נושא 1` | `Subjects` |
| E | `נושא 2` | `Subjects` |
| F | `קיום דיון` | `DiscussionCodes` |
| G | `כיתה` | `SchoolClasses` |
| H | `מסגרת חינוכית` | `Frameworks` with synthetic `QCAT-*` symbols for conclusion-category values |
| I | `יישוב/מחוז/ארצי` | `LocalityDistrictNationals` |
| J | `שכבה` | `GradeLevels` |
| K | `כיתה` | `SchoolClasses` |

Column H is imported into `Frameworks` because the implemented report-row conclusion field already references `Frameworks`. Values from the questionnaire receive deterministic synthetic symbols prefixed with `QCAT-` so they do not collide with real institution symbols.

## Current Gaps

| Gap | Status |
|-----|--------|
| Direct `.xlsb` upload through the MVC admin screen | Implemented for `טבלאות.xlsb` through `ImportClientLookupXlsb` |
| Allocation creation from the questionnaire catalog | Not applicable: the file is a catalog only and does not contain employee/allocation ownership |
| Historical row allocation assignment | Implemented when unambiguous; ambiguous historical rows remain null by design |
| Questionnaire column H dedicated lookup | Implemented using the existing report conclusion framework path with `QCAT-*` symbols |

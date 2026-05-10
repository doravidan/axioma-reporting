"""
AX-024 Historical Report Seed — imports report rows from BASE DATA.xlsb into AxiomaReporting DB.
Run after seed_lookups.py has populated all lookup tables.
"""
import sys, io, pyodbc, pyxlsb
from datetime import datetime, timedelta

sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')

CONN_STR = (
    "DRIVER={ODBC Driver 17 for SQL Server};"
    "SERVER=.\\SQLEXPRESS;"
    "DATABASE=AxiomaReporting;"
    "Trusted_Connection=yes;"
)

BASE     = r"F:\דווח עובדים אקסיומא\database\seed-data"
BASE_XL  = f"{BASE}\\BASE DATA.xlsb"
NOW      = datetime.utcnow().strftime("%Y-%m-%d %H:%M:%S")

# StatusId=4 = Approved (historical data is already approved)
STATUS_APPROVED = 4

EXCEL_EPOCH = datetime(1899, 12, 30)

def connect():
    return pyodbc.connect(CONN_STR)

def excel_serial_to_date(serial):
    """Convert Excel date serial number to Python date."""
    if serial is None:
        return None
    try:
        return (EXCEL_EPOCH + timedelta(days=int(float(str(serial))))).date()
    except Exception:
        return None

def val(row, col):
    if col < len(row) and row[col].v is not None:
        return str(row[col].v).strip()
    return ""

def fval(row, col):
    """Return float value or None."""
    if col < len(row) and row[col].v is not None:
        try:
            return float(row[col].v)
        except Exception:
            return None
    return None

def parse_institution_symbol(raw):
    """Extract numeric symbol from raw string.
    Handles prefix format '640086 מקיף...' and suffix format 'חט"ב... 248146'.
    Returns (symbol_int, name_str) or (None, raw_str).
    """
    if not raw:
        return None, ""
    parts = raw.strip().split()
    if not parts:
        return None, raw.strip()
    # Try first token as symbol
    try:
        sym = int(float(parts[0]))
        name = " ".join(parts[1:])
        return sym, name
    except (ValueError, TypeError):
        pass
    # Try last token as symbol
    try:
        sym = int(float(parts[-1]))
        name = " ".join(parts[:-1])
        return sym, name
    except (ValueError, TypeError):
        pass
    return None, raw.strip()

# ─────────────────────────────────────────────
# Lookup helpers
# ─────────────────────────────────────────────
def build_lookup(cur, table, desc_col="Description"):
    rows = cur.execute(f"SELECT {desc_col}, Id FROM {table}").fetchall()
    return {r[0]: r[1] for r in rows}

def get_or_create_simple(cur, table, desc, cache):
    """Find or create a simple lookup row (Description, IsActive, CreatedAt). Returns Id or 0."""
    if not desc:
        return 0
    if desc in cache:
        return cache[desc]
    row = cur.execute(f"SELECT Id FROM {table} WHERE Description = ?", desc).fetchone()
    if row:
        cache[desc] = row[0]
        return row[0]
    cur.execute(
        f"INSERT INTO {table} (Description, IsActive, CreatedAt) VALUES (?, 1, ?)", desc, NOW
    )
    row = cur.execute(f"SELECT Id FROM {table} WHERE Description = ?", desc).fetchone()
    lid = row[0] if row else 0
    cache[desc] = lid
    return lid


def get_or_create_locality(cur, desc, locality_cache):
    """Find or create a Locality by description."""
    if not desc:
        return None
    if desc in locality_cache:
        return locality_cache[desc]
    row = cur.execute("SELECT Id FROM Localities WHERE Description = ?", desc).fetchone()
    if row:
        locality_cache[desc] = row[0]
        return row[0]
    cur.execute(
        "INSERT INTO Localities (Description, IsActive, CreatedAt) VALUES (?, 1, ?)", desc, NOW
    )
    row = cur.execute("SELECT Id FROM Localities WHERE Description = ?", desc).fetchone()
    lid = row[0] if row else None
    locality_cache[desc] = lid
    return lid


def build_framework_lookup(cur):
    """Map InstitutionSymbol -> Framework Id."""
    rows = cur.execute("SELECT InstitutionSymbol, Id FROM Frameworks WHERE InstitutionSymbol IS NOT NULL").fetchall()
    return {r[0]: r[1] for r in rows}

def get_or_create_framework(cur, inst_symbol, inst_name, framework_cache):
    """Find or create a Framework entry by symbol (preferred) or by name."""
    # Key for cache: use symbol if available, else the name string
    cache_key = inst_symbol if inst_symbol is not None else inst_name
    if not cache_key:
        return None
    if cache_key in framework_cache:
        return framework_cache[cache_key]

    if inst_symbol is not None:
        row = cur.execute(
            "SELECT Id FROM Frameworks WHERE InstitutionSymbol = ?", inst_symbol
        ).fetchone()
        if row:
            framework_cache[cache_key] = row[0]
            return row[0]
        desc = inst_name if inst_name else str(inst_symbol)
        cur.execute(
            "INSERT INTO Frameworks (InstitutionSymbol, Description, IsActive, CreatedAt) VALUES (?, ?, 1, ?)",
            inst_symbol, desc, NOW
        )
        row = cur.execute("SELECT Id FROM Frameworks WHERE InstitutionSymbol = ?", inst_symbol).fetchone()
    else:
        # Name-only framework — assign a synthetic symbol (900000001+) to satisfy NOT NULL
        row = cur.execute(
            "SELECT Id FROM Frameworks WHERE Description = ?", inst_name
        ).fetchone()
        if row:
            framework_cache[cache_key] = row[0]
            return row[0]
        # Increment synthetic counter stored in cache under a reserved key
        syn_sym = framework_cache.get("__next_synthetic__", 900000001)
        framework_cache["__next_synthetic__"] = syn_sym + 1
        cur.execute(
            "INSERT INTO Frameworks (InstitutionSymbol, Description, IsActive, CreatedAt) VALUES (?, ?, 1, ?)",
            syn_sym, inst_name, NOW
        )
        row = cur.execute(
            "SELECT Id FROM Frameworks WHERE Description = ?", inst_name
        ).fetchone()

    fid = row[0] if row else None
    framework_cache[cache_key] = fid
    return fid

def get_or_create_user(cur, employee_code, name):
    """Find user by EmployeeCode, or create a stub if not found."""
    row = cur.execute("SELECT Id FROM Users WHERE EmployeeCode = ?", employee_code).fetchone()
    if row:
        return row[0]

    # Create stub: split name into first/last
    parts = str(name).strip().split()
    first = parts[0] if parts else str(employee_code)
    last  = " ".join(parts[1:]) if len(parts) > 1 else ""

    # Minimal stub — use EmployeeCode as IdNumber placeholder
    cur.execute("""
        INSERT INTO Users
          (EmployeeCode, IdNumber, FirstName, LastName, PasswordHash, RoleId, UserRoleId,
           StatusId, MustChangePassword, IsReportingEmployee, AllowFutureReporting,
           AcceptedTermsOfUse, FailedLoginAttempts, CreatedAt)
        VALUES (?, ?, ?, ?, ?, 1, 6, 1, 1, 1, 0, 0, 0, ?)
    """, employee_code, employee_code, first, last, "IMPORTED_STUB", NOW)
    row = cur.execute("SELECT Id FROM Users WHERE EmployeeCode = ?", employee_code).fetchone()
    return row[0] if row else None

def get_or_create_reporting_month(cur, year, month):
    """Find or create a reporting month for the given year/month."""
    row = cur.execute(
        "SELECT Id FROM ReportingMonths WHERE Year = ? AND Month = ?", year, month
    ).fetchone()
    if row:
        return row[0]

    import calendar
    month_names = ["ינואר","פברואר","מרץ","אפריל","מאי","יוני","יולי","אוגוסט","ספטמבר","אוקטובר","נובמבר","דצמבר"]
    description = f"{month_names[month-1]} {year}"
    last_day = calendar.monthrange(year, month)[1]
    last_date = f"{year}-{month:02d}-{last_day}"
    cur.execute("""
        INSERT INTO ReportingMonths (Year, Month, Description, IsActive, LastReportingDate, AllowFutureReporting, CreatedAt)
        VALUES (?, ?, ?, 0, ?, 0, ?)
    """, year, month, description, last_date, NOW)
    row = cur.execute(
        "SELECT Id FROM ReportingMonths WHERE Year = ? AND Month = ?", year, month
    ).fetchone()
    return row[0] if row else None

def get_or_create_report(cur, user_id, month_id):
    """Find or create a Report for this user+month. Historical = Approved."""
    row = cur.execute(
        "SELECT Id FROM Reports WHERE UserId = ? AND ReportingMonthId = ?", user_id, month_id
    ).fetchone()
    if row:
        return row[0]

    cur.execute("""
        INSERT INTO Reports (UserId, ReportingMonthId, StatusId, ImportedFromExcel, CreatedAt)
        VALUES (?, ?, ?, 0, ?)
    """, user_id, month_id, STATUS_APPROVED, NOW)
    row = cur.execute(
        "SELECT Id FROM Reports WHERE UserId = ? AND ReportingMonthId = ?", user_id, month_id
    ).fetchone()
    return row[0] if row else None

def resolve_allocation_id(cur, user_id, edu_prog_str, allocation_cache):
    """Resolve an allocation for historical rows when the source data is specific enough."""
    cache_key = (user_id, edu_prog_str or "")
    if cache_key in allocation_cache:
        return allocation_cache[cache_key]

    allocation_id = None
    if edu_prog_str:
        rows = cur.execute("""
            SELECT DISTINCT a.Id
            FROM Allocations a
            INNER JOIN AllocationEducationalPrograms aep ON aep.AllocationId = a.Id
            INNER JOIN EducationalPrograms ep ON ep.Id = aep.EducationalProgramId
            WHERE a.UserId = ? AND a.IsActive = 1 AND ep.Description = ?
        """, user_id, edu_prog_str).fetchall()
        if len(rows) == 1:
            allocation_id = rows[0][0]

    if allocation_id is None:
        rows = cur.execute("""
            SELECT Id
            FROM Allocations
            WHERE UserId = ? AND IsActive = 1
        """, user_id).fetchall()
        if len(rows) == 1:
            allocation_id = rows[0][0]

    allocation_cache[cache_key] = allocation_id
    return allocation_id

# ─────────────────────────────────────────────
# Main import
# ─────────────────────────────────────────────
def import_sheet(cur, ws, sheet_name, lookups, stats, framework_cache, locality_cache,
                 edu_prog_cache, domain_cache, subject_cache, district_cache, allocation_cache):
    rows = list(ws.rows())
    if len(rows) < 2:
        return

    # Track next SequenceNumber per report_id
    seq_counters = {}

    # Row index 1 is header, data starts at index 2
    for i, row in enumerate(rows[2:], start=2):
        if not row or not row[0].v:
            continue

        try:
            employee_code_raw = fval(row, 1)
            if employee_code_raw is None:
                continue
            employee_code = str(int(employee_code_raw))
            name = val(row, 2)

            date_serial = fval(row, 6)
            meeting_date = excel_serial_to_date(date_serial)
            if meeting_date is None:
                stats["skipped"] += 1
                continue

            duration_raw = fval(row, 7)
            if duration_raw is None:
                stats["skipped"] += 1
                continue
            duration = round(duration_raw, 4)

            district_str     = val(row, 3)
            locality_str     = val(row, 4)
            inst_raw         = val(row, 5)
            edu_prog_str     = val(row, 8)
            domain_str       = val(row, 9)
            subject1_str     = val(row, 10)
            subject2_str     = val(row, 11)
            discussion_str   = val(row, 12)
            concl_class_str  = val(row, 13)
            # col 14: מסגרת חינוכית (framework = locality/district/national type)
            loc_dist_nat_str = val(row, 15)

            # Resolve FKs
            user_id  = get_or_create_user(cur, employee_code, name)
            if not user_id:
                stats["skipped"] += 1
                continue

            month_id  = get_or_create_reporting_month(cur, meeting_date.year, meeting_date.month)
            report_id = get_or_create_report(cur, user_id, month_id)
            allocation_id = resolve_allocation_id(cur, user_id, edu_prog_str, allocation_cache)

            district_id  = get_or_create_simple(cur, "Districts",           district_str,  district_cache)
            locality_id  = get_or_create_simple(cur, "Localities",           locality_str,  locality_cache)
            edu_prog_id  = get_or_create_simple(cur, "EducationalPrograms",  edu_prog_str,  edu_prog_cache)
            domain_id    = get_or_create_simple(cur, "Domains",              domain_str,    domain_cache)
            subject1_id  = get_or_create_simple(cur, "Subjects",             subject1_str,  subject_cache)
            subject2_id  = (get_or_create_simple(cur, "Subjects", subject2_str, subject_cache)
                            if subject2_str else None)
            discussion_id  = lookups["discussion_codes"].get(discussion_str) if discussion_str else None
            concl_class_id = lookups["conclusion_classes"].get(concl_class_str) if concl_class_str else None

            # Parse institution name — handles "symbol name", "name symbol", and name-only formats
            inst_symbol, inst_name = parse_institution_symbol(inst_raw)
            framework_id = get_or_create_framework(cur, inst_symbol, inst_name, framework_cache)
            if framework_id is None:
                print(f"  Row {i} skipped in '{sheet_name}': no framework resolved from '{inst_raw}'")
                stats["skipped"] += 1
                continue

            # FrameworkId → Frameworks table (keyed by InstitutionSymbol)
            # ConclusionLocationId → LocalityDistrictNationals table
            loc_dist_nat_id = lookups["loc_dist_nat"].get(loc_dist_nat_str) if loc_dist_nat_str else None

            seq_counters[report_id] = seq_counters.get(report_id, 0) + 1
            seq_num = seq_counters[report_id]

            cur.execute("""
                INSERT INTO ReportRows
                  (ReportId, AllocationId, SequenceNumber, DistrictId, LocalityId, FrameworkId, EducationalProgramId,
                   DomainId, Subject1Id, Subject2Id, DiscussionCodeId, ConclusionClassId,
                   ConclusionLocationId, MeetingDate, MeetingDuration, CreatedAt)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
            """,
                report_id,
                allocation_id,
                seq_num,
                district_id or 0,
                locality_id,
                framework_id,
                edu_prog_id,
                domain_id,
                subject1_id,
                subject2_id,
                discussion_id,
                concl_class_id,
                loc_dist_nat_id,
                meeting_date.strftime("%Y-%m-%d"),
                duration,
                NOW
            )
            stats["inserted"] += 1

        except Exception as e:
            print(f"  Row {i} error in '{sheet_name}': {e}")
            stats["errors"] += 1


def main():
    print("Connecting to database...")
    con = connect()
    cur = con.cursor()

    print("Building lookup maps...")
    lookups = {
        "districts":            build_lookup(cur, "Districts"),
        "localities":           build_lookup(cur, "Localities"),
        "edu_programs":         build_lookup(cur, "EducationalPrograms"),
        "domains":              build_lookup(cur, "Domains"),
        "subjects":             build_lookup(cur, "Subjects"),
        "discussion_codes":     build_lookup(cur, "DiscussionCodes"),
        "conclusion_classes":   build_lookup(cur, "SchoolClasses"),
        "loc_dist_nat":         build_lookup(cur, "LocalityDistrictNationals"),
    }
    framework_cache = build_framework_lookup(cur)
    locality_cache  = build_lookup(cur, "Localities")
    edu_prog_cache  = build_lookup(cur, "EducationalPrograms")
    domain_cache    = build_lookup(cur, "Domains")
    subject_cache   = build_lookup(cur, "Subjects")
    district_cache  = build_lookup(cur, "Districts")
    allocation_cache = {}

    print(f"  Districts: {len(district_cache)}")
    print(f"  Localities: {len(locality_cache)}")
    print(f"  Edu Programs: {len(edu_prog_cache)}")
    print(f"  Domains: {len(domain_cache)}")
    print(f"  Subjects: {len(subject_cache)}")

    total_stats = {"inserted": 0, "skipped": 0, "errors": 0}

    print(f"\nOpening {BASE_XL}...")
    with pyxlsb.open_workbook(BASE_XL) as wb:
        sheet_names = wb.sheets
        print(f"  Sheets: {sheet_names}")
        for sheet_name in sheet_names:
            print(f"\n  Processing sheet: '{sheet_name}'...")
            stats = {"inserted": 0, "skipped": 0, "errors": 0}
            with wb.get_sheet(sheet_name) as ws:
                import_sheet(cur, ws, sheet_name, lookups, stats, framework_cache,
                             locality_cache, edu_prog_cache, domain_cache, subject_cache, district_cache,
                             allocation_cache)
            con.commit()
            total_stats["inserted"] += stats["inserted"]
            total_stats["skipped"]  += stats["skipped"]
            total_stats["errors"]   += stats["errors"]
            print(f"    Inserted: {stats['inserted']}, Skipped: {stats['skipped']}, Errors: {stats['errors']}")

    cur.close()
    con.close()
    print(f"\nDone. Total inserted: {total_stats['inserted']}, skipped: {total_stats['skipped']}, errors: {total_stats['errors']}")


if __name__ == "__main__":
    main()

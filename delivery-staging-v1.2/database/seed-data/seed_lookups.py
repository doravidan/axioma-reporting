"""
AX-024 Seed Script — loads all lookup tables from the client Excel files into AxiomaReporting DB.
Run once after the database is created and migrations applied.
"""
import sys, io, pyodbc, pyxlsb, openpyxl
from datetime import datetime

sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')

CONN_STR = (
    "DRIVER={ODBC Driver 17 for SQL Server};"
    "SERVER=.\\SQLEXPRESS;"
    "DATABASE=AxiomaReporting;"
    "Trusted_Connection=yes;"
)

BASE   = r"F:\דווח עובדים אקסיומא\database\seed-data"
TABLOT = f"{BASE}\\טבלאות.xlsb"
NOW    = datetime.utcnow().strftime("%Y-%m-%d %H:%M:%S")

def connect():
    return pyodbc.connect(CONN_STR)

def insert_if_missing(cur, table, desc_col, desc_val, extra_cols=None):
    """Insert a row if description not already present. Returns True if inserted."""
    cur.execute(f"SELECT COUNT(1) FROM {table} WHERE {desc_col} = ?", desc_val)
    if cur.fetchone()[0] > 0:
        return False
    if extra_cols:
        cols = f"{desc_col}, " + ", ".join(extra_cols.keys())
        vals = "?, " + ", ".join(["?"] * len(extra_cols))
        cur.execute(f"INSERT INTO {table} ({cols}) VALUES ({vals})",
                    desc_val, *extra_cols.values())
    else:
        cur.execute(f"INSERT INTO {table} ({desc_col}) VALUES (?)", desc_val)
    return True

def get_id(cur, table, desc_col, desc_val):
    cur.execute(f"SELECT Id FROM {table} WHERE {desc_col} = ?", desc_val)
    row = cur.fetchone()
    return row[0] if row else None

# ─────────────────────────────────────────────
# 1. Load lists sheet from טבלאות.xlsb
# ─────────────────────────────────────────────
def load_lists_sheet():
    """Parse the גיליון מרכז רשימות לפי שדות sheet and return dict of lists."""
    data = {
        "districts": set(), "sectors": set(), "edu_stages": set(),
        "programs": set(), "edu_programs": set(), "domains": set(),
        "subjects": set(), "discussion_codes": set(), "conclusion_classes": set(),
    }

    with pyxlsb.open_workbook(TABLOT) as wb:
        with wb.get_sheet("גיליון מרכז רשימות לפי שדות") as ws:
            rows = list(ws.rows())

    def val(row, col):
        if col < len(row) and row[col].v is not None:
            return str(row[col].v).strip()
        return ""

    # Employee section rows (22-35): col2=sectors, col0=unused
    for i in range(22, 57):
        row = rows[i]
        # sectors at col 2
        v = val(row, 2)
        if v and v not in ("מגזר עובד", "None"):
            data["sectors"].add(v)

    # Institution section rows (37-56): col4=districts, col12=edu_stages
    for i in range(37, 57):
        row = rows[i]
        d = val(row, 4)
        if d and d not in ("מחוז", "None"):
            data["districts"].add(d)
        e = val(row, 12)
        if e and e not in ("שלב חינוך", "None"):
            data["edu_stages"].add(e)

    # Report-level section starts at row 60 (header), data from row 61+
    header_row = 60
    for i in range(header_row + 1, len(rows)):
        row = rows[i]
        prog = val(row, 0)
        edu_prog = val(row, 2)
        domain = val(row, 4)
        subj = val(row, 6)
        subj2 = val(row, 8)
        disc = val(row, 10)
        cc = val(row, 12)

        if prog: data["programs"].add(prog)
        if edu_prog: data["edu_programs"].add(edu_prog)
        if domain: data["domains"].add(domain)
        if subj: data["subjects"].add(subj)
        if subj2: data["subjects"].add(subj2)
        if disc: data["discussion_codes"].add(disc)
        if cc: data["conclusion_classes"].add(cc)

    # Also add known districts from report data (col0 of קטגוריות כלליות)
    for d in ["ארצי", "מטה", "כללי", "דרום", "תל אביב", "חיפה", "צפון",
              "מרכז", "התישבותי", "מנח\"י", "ירושלים", "חרדי", "אחר"]:
        data["districts"].add(d)

    return data


# ─────────────────────────────────────────────
# 2. Seed simple lookup tables
# ─────────────────────────────────────────────
def seed_simple(cur, table, items, is_active=True):
    inserted = 0
    for item in sorted(items):
        if not item or item in ("None", ""):
            continue
        cur.execute(f"SELECT COUNT(1) FROM {table} WHERE Description = ?", item)
        if cur.fetchone()[0] == 0:
            cur.execute(
                f"INSERT INTO {table} (Description, IsActive, CreatedAt) VALUES (?, ?, ?)",
                item, 1 if is_active else 0, NOW
            )
            inserted += 1
    return inserted


def seed_localities(cur):
    with pyxlsb.open_workbook(TABLOT) as wb:
        with wb.get_sheet("יישוב") as ws:
            rows = list(ws.rows())

    inserted = 0
    for row in rows[1:]:  # skip header
        if not row or not row[0].v:
            continue
        desc = str(row[0].v).strip()
        if not desc or desc == "יישוב":
            continue
        cur.execute("SELECT COUNT(1) FROM Localities WHERE Description = ?", desc)
        if cur.fetchone()[0] == 0:
            cur.execute(
                "INSERT INTO Localities (Description, IsActive, CreatedAt) VALUES (?, 1, ?)",
                desc, NOW
            )
            inserted += 1
    return inserted


def seed_institutions(cur):
    with pyxlsb.open_workbook(TABLOT) as wb:
        with wb.get_sheet("מוסדות") as ws:
            rows = list(ws.rows())

    # Build lookup maps
    dist_map  = {r[0]: r[1] for r in cur.execute("SELECT Description, Id FROM Districts").fetchall()}
    sec_map   = {r[0]: r[1] for r in cur.execute("SELECT Description, Id FROM Sectors").fetchall()}
    loc_map   = {r[0]: r[1] for r in cur.execute("SELECT Description, Id FROM Localities").fetchall()}
    stage_map = {r[0]: r[1] for r in cur.execute("SELECT Description, Id FROM EducationalStages").fetchall()}
    etype_map = {r[0]: r[1] for r in cur.execute("SELECT Description, Id FROM EducationTypes").fetchall()}

    inserted = 0
    for row in rows[1:]:  # skip header
        if not row or not row[0].v:
            continue
        try:
            symbol_raw = row[0].v
            if symbol_raw is None:
                continue
            symbol = int(float(str(symbol_raw)))
            name   = str(row[1].v).strip() if row[1].v else ""
            if not name:
                continue
            city_str  = str(row[2].v).strip() if len(row) > 2 and row[2].v else ""
            dist_str  = str(row[3].v).strip() if len(row) > 3 and row[3].v else ""
            sec_str   = str(row[4].v).strip() if len(row) > 4 and row[4].v else ""
            etype_str = str(row[5].v).strip() if len(row) > 5 and row[5].v else ""
            stage_str = str(row[6].v).strip() if len(row) > 6 and row[6].v else ""

            loc_id   = loc_map.get(city_str)
            dist_id  = dist_map.get(dist_str)
            sec_id   = sec_map.get(sec_str)
            stage_id = stage_map.get(stage_str)
            etype_id = etype_map.get(etype_str)

            cur.execute("SELECT COUNT(1) FROM Institutions WHERE InstitutionSymbol = ?", symbol)
            if cur.fetchone()[0] == 0:
                cur.execute("""
                    INSERT INTO Institutions
                      (InstitutionSymbol, Name, LocalityId, DistrictId, SectorId,
                       TypeId, EducationalStageId, IsActive, CreatedAt)
                    VALUES (?, ?, ?, ?, ?, ?, ?, 1, ?)
                """, symbol, name, loc_id, dist_id, sec_id, etype_id, stage_id, NOW)
                inserted += 1
        except Exception as e:
            print(f"  Institution row error: {e} — {[str(c.v) for c in row[:4]]}")

    return inserted


def seed_education_types(cur):
    """Seed EducationTypes from institution type column."""
    types = set()
    with pyxlsb.open_workbook(TABLOT) as wb:
        with wb.get_sheet("מוסדות") as ws:
            for i, row in enumerate(ws.rows()):
                if i == 0: continue
                if row and len(row) > 5 and row[5].v:
                    v = str(row[5].v).strip()
                    if v: types.add(v)
    return seed_simple(cur, "EducationTypes", types)


# ─────────────────────────────────────────────
# 3. Main
# ─────────────────────────────────────────────
def main():
    print("Connecting to database...")
    con = connect()
    cur = con.cursor()

    print("\nParsing lists sheet...")
    lists = load_lists_sheet()

    steps = [
        ("Districts",          "Districts",          lists["districts"]),
        ("Sectors",            "Sectors",            lists["sectors"]),
        ("Educational Stages", "EducationalStages",  lists["edu_stages"]),
        ("Programs",           "Programs",           lists["programs"]),
        ("Educational Programs","EducationalPrograms", lists["edu_programs"]),
        ("Domains",            "Domains",            lists["domains"]),
        ("Subjects",           "Subjects",           lists["subjects"]),
        ("Discussion Codes",   "DiscussionCodes",    lists["discussion_codes"]),
        ("School Classes",     "SchoolClasses",      lists["conclusion_classes"]),
    ]

    for label, table, items in steps:
        n = seed_simple(cur, table, items)
        con.commit()
        print(f"  {label}: {n} inserted ({len(items)} total)")

    print("\n  Education Types (from מוסדות)...")
    n = seed_education_types(cur)
    con.commit()
    print(f"  Education Types: {n} inserted")

    print("\n  Localities (1334 Israeli towns)...")
    n = seed_localities(cur)
    con.commit()
    print(f"  Localities: {n} inserted")

    print("\n  Institutions (1133 schools/frameworks)...")
    n = seed_institutions(cur)
    con.commit()
    print(f"  Institutions: {n} inserted")

    cur.close()
    con.close()
    print("\nDone.")

if __name__ == "__main__":
    main()

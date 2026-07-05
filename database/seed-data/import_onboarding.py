"""
Client onboarding import — loads the 16 program workbooks + helper-tables workbook
into the AxiomaReporting database.

Idempotent: every insert is "insert if missing" keyed on a natural key, so the
script can be re-run safely. Connection string and source folder are taken from
environment variables (same convention as seed_lookups.py):

    AXIOMA_CONN_STR   ODBC connection string
    AXIOMA_ONBOARD_DIR folder containing 00_helper_lookups.xlsx and prog_*.xlsx

Project: all programs belong to the single project "חינוך ילדים ונוער בסיכון".

Stages (run all by default; pass a stage name to run just one):
    lookups        simple lookup tables + employee roles + report types
    institutions   institutions + report-selectable frameworks (by symbol)
    projects       project, programs, project-programs, ProjectProgram* scope
    employees      users (by IdNumber)
    allocations    allocations (by user+project) + junction scope tables
    frameworks     per-employee / shared framework assignments (AllocationFrameworks)
"""
import sys, io, os, glob, re
from datetime import datetime, timezone

import pyodbc, openpyxl, bcrypt

sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding="utf-8")

CONN_STR = os.environ["AXIOMA_CONN_STR"]
DIR = os.environ.get("AXIOMA_ONBOARD_DIR", "/workspace/database/seed-data/onboarding")
NOW = datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S")
PROJECT_NAME = "חינוך ילדים ונוער בסיכון"
DEFAULT_PASSWORD = "Axioma2026!"  # employees must change on first login

# One BCrypt hash reused for every seeded employee (MustChangePassword forces a reset).
_PW_HASH = bcrypt.hashpw(DEFAULT_PASSWORD.encode(), bcrypt.gensalt(rounds=12)).decode()

REST_DAY = {  # Hebrew day -> System.DayOfWeek int (0=Sun..6=Sat); "עצמאי" => no fixed day
    "ראשון": 0, "שני": 1, "שלישי": 2, "רביעי": 3, "חמישי": 4, "שישי": 5, "שבת": 6,
}


def norm(v):
    return "" if v is None else str(v).replace("\n", " ").strip()


def connect():
    return pyodbc.connect(CONN_STR, autocommit=False)


# ---------------------------------------------------------------------------
# Generic lookup upsert helpers (cached id maps to avoid per-row queries)
# ---------------------------------------------------------------------------
class Lookups:
    """Insert-if-missing cache for a LookupEntity table keyed on Description."""

    def __init__(self, cur, table, desc_col="Description", has_active=True):
        self.cur = cur
        self.table = table
        self.desc_col = desc_col
        self.has_active = has_active
        self.map = {}
        cur.execute(f"SELECT Id, {desc_col} FROM {table}")
        for row in cur.fetchall():
            if row[1] is not None:
                self.map[str(row[1]).strip()] = row[0]
        self.inserted = 0

    def get(self, desc):
        desc = norm(desc)
        if not desc:
            return None
        if desc in self.map:
            return self.map[desc]
        if self.has_active:
            self.cur.execute(
                f"INSERT INTO {self.table} ({self.desc_col}, IsActive, CreatedAt) "
                f"OUTPUT INSERTED.Id VALUES (?, 1, ?)", desc, NOW)
        else:
            self.cur.execute(
                f"INSERT INTO {self.table} ({self.desc_col}, CreatedAt) "
                f"OUTPUT INSERTED.Id VALUES (?, ?)", desc, NOW)
        new_id = self.cur.fetchone()[0]
        self.map[desc] = new_id
        self.inserted += 1
        return new_id


def sheet_rows(ws):
    return list(ws.iter_rows(values_only=True))


def find_header(rows, must_have, max_scan=4):
    """Return (index, [normalized headers]) of the header row containing all must_have tokens."""
    for i in range(min(max_scan, len(rows))):
        cells = [norm(c) for c in rows[i]]
        if all(any(tok == c for c in cells) for tok in must_have):
            return i, cells
    return None, None


def col_index(headers, *names):
    for name in names:
        for j, h in enumerate(headers):
            if h == name:
                return j
    # relaxed: startswith
    for name in names:
        for j, h in enumerate(headers):
            if h.startswith(name):
                return j
    return None


def program_files():
    return sorted(glob.glob(os.path.join(DIR, "prog_*.xlsx")))


def open_wb(f):
    return openpyxl.load_workbook(f, read_only=True, data_only=True)


def classify_sheet(headers):
    h = set(headers)
    if "נושא 1" in h and "קיום דיון" in h:
        return "code_values"
    if "ת.ז" in h and "סמל מוסד" in h and "מסגרת חינוכית" in h:
        return "per_employee_frameworks"
    if "סמל מוסד" in h and ("שם מוסד" in h or "מסגרת חינוכית" in h) and "ת.ז" not in h:
        return "institutions"
    # Dedicated allocations sheet: has project + scope columns but NOT personal fields.
    if "פרוייקט" in h and "היקף שעות העסקה חודשי" in h and "יום מנוחה" not in h:
        return "allocations"
    # Employee personal data (dedicated מאגר sheet OR combined first sheet).
    if "ת.ז" in h and "יום מנוחה" in h and "תפקיד" in h:
        return "employees"
    return None


def iter_classified(f, want):
    """Yield (sheet_title, header_index, headers, rows) for EVERY sheet matching `want`."""
    wb = open_wb(f)
    try:
        for ws in wb.worksheets:
            rows = sheet_rows(ws)
            hi, hdr = None, None
            for i in range(min(4, len(rows))):
                cells = [norm(c) for c in rows[i]]
                if classify_sheet(cells) == want:
                    hi, hdr = i, cells
                    break
            if hdr is not None:
                yield ws.title, hi, hdr, rows
    finally:
        wb.close()


# ---------------------------------------------------------------------------
# Institution / framework column resolution (layouts vary across files)
# ---------------------------------------------------------------------------
def resolve_institution_cols(headers):
    return {
        "symbol": col_index(headers, "סמל מוסד"),
        "name": col_index(headers, "שם מוסד", "מסגרת חינוכית"),
        "locality": col_index(headers, "שם הישוב", "יישוב"),
        "district": col_index(headers, "מחוז"),
        "sector": col_index(headers, "מגזר"),
        "type": col_index(headers, "סוג חינוך"),
        "stage": col_index(headers, "שלב חינוך"),
    }


def digits_only(s):
    s = norm(s)
    return s if re.fullmatch(r"\d+", s) else None


INT32_MAX = 2147483647


def parse_symbol(sym_cell, name_cell):
    """Return (symbol:int, name:str) handling files where the two columns are swapped.
    Institution symbols are stored as int32; values that overflow (data-entry errors,
    real symbols are <= 7 digits) are rejected."""
    s_sym, s_name = norm(sym_cell), norm(name_cell)
    if digits_only(s_sym) and int(s_sym) <= INT32_MAX:
        return int(s_sym), s_name
    if digits_only(s_name) and int(s_name) <= INT32_MAX:  # swapped
        return int(s_name), s_sym
    if digits_only(s_sym) or digits_only(s_name):
        print(f"  WARN: skipping out-of-range institution symbol '{s_sym or s_name}' (name='{s_name or s_sym}')")
    return None, s_name or s_sym


# ---------------------------------------------------------------------------
# STAGE: lookups
# ---------------------------------------------------------------------------
def stage_lookups(con):
    cur = con.cursor()
    L = {
        "districts": Lookups(cur, "Districts"),
        "sectors": Lookups(cur, "Sectors"),
        "stages": Lookups(cur, "EducationalStages"),
        "types": Lookups(cur, "EducationTypes"),
        "eduprograms": Lookups(cur, "EducationalPrograms"),
        "domains": Lookups(cur, "Domains"),
        "subjects": Lookups(cur, "Subjects"),
        "discussion": Lookups(cur, "DiscussionCodes"),
        "classes": Lookups(cur, "SchoolClasses"),
        "grades": Lookups(cur, "GradeLevels"),
        "ldn": Lookups(cur, "LocalityDistrictNationals"),
        "localities": Lookups(cur, "Localities"),
        "reporttypes": Lookups(cur, "ReportTypes"),
        "emproles": Lookups(cur, "EmployeeRoles"),
    }

    # 1) Helper workbook dedicated single-column sheets
    helper = os.path.join(DIR, "00_helper_lookups.xlsx")
    if os.path.exists(helper):
        wb = open_wb(helper)
        helper_map = {
            "מחוזות": "districts", "תוכנית חינוכית": "eduprograms", "תחום": "domains",
            "נושאים": "subjects", "קיום דיון": "discussion", "מסקנות כיתה": "classes",
            "מסקנות מסגרת חינוכית": None, "יישוב-מחוז-ארצי": "ldn", "שכבה": "grades",
            "כיתה": "classes",
        }
        for ws in wb.worksheets:
            target = helper_map.get(ws.title)
            if not target:
                continue
            for i, row in enumerate(ws.iter_rows(values_only=True)):
                if i == 0:
                    continue  # header
                val = norm(row[0]) if row else ""
                if val:
                    L[target].get(val)
        wb.close()

    # 2) Per-program code_values sheets
    for f in program_files():
        for _title, hi, hdr, rows in iter_classified(f, "code_values"):
            idx = {
                "eduprograms": col_index(hdr, "תוכנית"),
                "domains": col_index(hdr, "תחום"),
                "subj1": col_index(hdr, "נושא 1"),
                "subj2": col_index(hdr, "נושא 2"),
                "discussion": col_index(hdr, "קיום דיון"),
                "grades": col_index(hdr, "שכבה"),
                "ldn": col_index(hdr, "יישוב/מחוז/ארצי"),
            }
            # both 'כיתה' columns -> SchoolClasses (numeric + conclusion text)
            class_cols = [j for j, h in enumerate(hdr) if h == "כיתה"]
            for r in rows[hi + 1:]:
                cells = [norm(c) for c in r]
                def g(j):
                    return cells[j] if j is not None and j < len(cells) else ""
                if idx["eduprograms"] is not None: L["eduprograms"].get(g(idx["eduprograms"]))
                if idx["domains"] is not None: L["domains"].get(g(idx["domains"]))
                if idx["subj1"] is not None: L["subjects"].get(g(idx["subj1"]))
                if idx["subj2"] is not None: L["subjects"].get(g(idx["subj2"]))
                if idx["discussion"] is not None: L["discussion"].get(g(idx["discussion"]))
                if idx["grades"] is not None: L["grades"].get(g(idx["grades"]))
                if idx["ldn"] is not None: L["ldn"].get(g(idx["ldn"]))
                for cj in class_cols:
                    L["classes"].get(g(cj))

    # 3) Institution sheets -> localities, districts, sectors, types, stages
    for f in program_files():
        for _title, hi, hdr, rows in iter_classified(f, "institutions"):
            c = resolve_institution_cols(hdr)
            for r in rows[hi + 1:]:
                cells = [norm(x) for x in r]
                def g(j):
                    return cells[j] if j is not None and j < len(cells) else ""
                L["localities"].get(g(c["locality"]))
                L["districts"].get(g(c["district"]))
                L["sectors"].get(g(c["sector"]))
                L["types"].get(g(c["type"]))
                L["stages"].get(g(c["stage"]))

    # 4) Allocation sheets -> districts, sectors, report types
    for f in program_files():
        for _title, hi, hdr, rows in iter_classified(f, "allocations"):
            dcol = col_index(hdr, "מחוז")
            scol = col_index(hdr, "מגזר")
            rcol = col_index(hdr, "סיווג דיווח")
            for r in rows[hi + 1:]:
                cells = [norm(x) for x in r]
                def g(j):
                    return cells[j] if j is not None and j < len(cells) else ""
                L["districts"].get(g(dcol))
                L["sectors"].get(g(scol))
                L["reporttypes"].get(g(rcol))

    # employee job roles
    role_map = Lookups(cur, "EmployeeRoles")
    for f in program_files():
        for _title, hi, hdr, rows in iter_classified(f, "employees"):
            rj = col_index(hdr, "תפקיד")
            for r in rows[hi + 1:]:
                cells = [norm(x) for x in r]
                if rj is not None and rj < len(cells) and cells[rj]:
                    role_map.get(cells[rj])

    con.commit()
    for name, lk in L.items():
        print(f"  lookup {name:12s}: +{lk.inserted} inserted, {len(lk.map)} total")
    print(f"  employee roles: +{role_map.inserted}")


# ---------------------------------------------------------------------------
# STAGE: institutions (+ report-selectable frameworks by symbol)
# ---------------------------------------------------------------------------
def stage_institutions(con):
    cur = con.cursor()
    districts = Lookups(cur, "Districts")
    sectors = Lookups(cur, "Sectors")
    types = Lookups(cur, "EducationTypes")
    stages = Lookups(cur, "EducationalStages")
    localities = Lookups(cur, "Localities")

    # existing institutions by symbol
    cur.execute("SELECT InstitutionSymbol FROM Institutions")
    existing_inst = {row[0] for row in cur.fetchall()}
    # existing frameworks by numeric symbol
    cur.execute("SELECT InstitutionSymbol FROM Frameworks")
    existing_fw = {str(row[0]).strip() for row in cur.fetchall() if row[0] is not None}

    inst_added = fw_added = 0
    seen = set()
    for f in program_files():
        for _title, hi, hdr, rows in iter_classified(f, "institutions"):
            c = resolve_institution_cols(hdr)
            for r in rows[hi + 1:]:
                cells = [norm(x) for x in r]
                def g(j):
                    return cells[j] if j is not None and j < len(cells) else ""
                symbol, name = parse_symbol(g(c["symbol"]), g(c["name"]))
                if symbol is None or symbol in seen:
                    continue
                seen.add(symbol)
                stage_id = stages.get(g(c["stage"]))
                if symbol not in existing_inst:
                    cur.execute(
                        "INSERT INTO Institutions (InstitutionSymbol, Name, LocalityId, DistrictId, "
                        "SectorId, TypeId, EducationalStageId, IsActive, CreatedAt) "
                        "VALUES (?, ?, ?, ?, ?, ?, ?, 1, ?)",
                        symbol, name or str(symbol), localities.get(g(c["locality"])),
                        districts.get(g(c["district"])), sectors.get(g(c["sector"])),
                        types.get(g(c["type"])), stage_id, NOW)
                    existing_inst.add(symbol)
                    inst_added += 1
                sym_str = str(symbol)
                if sym_str not in existing_fw:
                    cur.execute(
                        "INSERT INTO Frameworks (Description, InstitutionSymbol, EducationalStageId, "
                        "IsActive, CreatedAt) VALUES (?, ?, ?, 1, ?)",
                        name or sym_str, sym_str, stage_id, NOW)
                    existing_fw.add(sym_str)
                    fw_added += 1
    con.commit()
    print(f"  institutions: +{inst_added} (total symbols seen {len(seen)})")
    print(f"  frameworks (numeric symbol): +{fw_added}")


# ---------------------------------------------------------------------------
# STAGE: projects / programs / project-programs (+ scope tables)
# ---------------------------------------------------------------------------
SCOPE_TABLES = {
    "eduprograms": ("ProjectProgramEducationalPrograms", "EducationalProgramId"),
    "domains": ("ProjectProgramDomains", "DomainId"),
    "subjects": ("ProjectProgramSubjects", "SubjectId"),
    "discussion": ("ProjectProgramDiscussionCodes", "DiscussionCodeId"),
    "classes": ("ProjectProgramClasses", "ClassId"),
    "grades": ("ProjectProgramGradeLevels", "GradeLevelId"),
}


def program_scope_sets(cur):
    """For each program name, gather the lookup id sets from its code_values sheet."""
    L = {
        "eduprograms": Lookups(cur, "EducationalPrograms"),
        "domains": Lookups(cur, "Domains"),
        "subjects": Lookups(cur, "Subjects"),
        "discussion": Lookups(cur, "DiscussionCodes"),
        "classes": Lookups(cur, "SchoolClasses"),
        "grades": Lookups(cur, "GradeLevels"),
        "ldn": Lookups(cur, "LocalityDistrictNationals"),
    }
    result = {}  # program_name -> {key -> set(ids)}
    for f in program_files():
        prog = program_name_of(f)
        acc = result.setdefault(prog, {k: set() for k in list(L.keys())})
        for _title, hi, hdr, rows in iter_classified(f, "code_values"):
            idx = {
                "eduprograms": col_index(hdr, "תוכנית"),
                "domains": col_index(hdr, "תחום"),
                "subj1": col_index(hdr, "נושא 1"),
                "subj2": col_index(hdr, "נושא 2"),
                "discussion": col_index(hdr, "קיום דיון"),
                "grades": col_index(hdr, "שכבה"),
                "ldn": col_index(hdr, "יישוב/מחוז/ארצי"),
            }
            class_cols = [j for j, h in enumerate(hdr) if h == "כיתה"]
            for r in rows[hi + 1:]:
                cells = [norm(c) for c in r]
                def g(j):
                    return cells[j] if j is not None and j < len(cells) else ""
                if g(idx["eduprograms"]): acc["eduprograms"].add(L["eduprograms"].get(g(idx["eduprograms"])))
                if g(idx["domains"]): acc["domains"].add(L["domains"].get(g(idx["domains"])))
                if g(idx["subj1"]): acc["subjects"].add(L["subjects"].get(g(idx["subj1"])))
                if g(idx["subj2"]): acc["subjects"].add(L["subjects"].get(g(idx["subj2"])))
                if g(idx["discussion"]): acc["discussion"].add(L["discussion"].get(g(idx["discussion"])))
                if g(idx["grades"]): acc["grades"].add(L["grades"].get(g(idx["grades"])))
                if g(idx["ldn"]): acc["ldn"].add(L["ldn"].get(g(idx["ldn"])))
                for cj in class_cols:
                    if g(cj): acc["classes"].add(L["classes"].get(g(cj)))
        for k in acc:
            acc[k].discard(None)
    return result


def program_name_of(f):
    """The program (תוכנית) name as used in the allocation sheet's תוכנית column."""
    for _title, hi, hdr, rows in iter_classified(f, "allocations"):
        pj = col_index(hdr, "תוכנית")
        for r in rows[hi + 1:]:
            cells = [norm(x) for x in r]
            if pj is not None and pj < len(cells) and cells[pj]:
                return cells[pj]
    # fallback: first sheet title
    wb = open_wb(f); t = wb.worksheets[0].title; wb.close()
    return t


def stage_projects(con):
    cur = con.cursor()
    projects = Lookups(cur, "Projects")
    programs = Lookups(cur, "Programs")
    project_id = projects.get(PROJECT_NAME)

    # existing project-programs
    cur.execute("SELECT ProjectId, ProgramId FROM ProjectPrograms")
    existing_pp = {(r[0], r[1]) for r in cur.fetchall()}

    prog_ids = {}
    for f in program_files():
        prog = program_name_of(f)
        pid = programs.get(prog)
        prog_ids[prog] = pid
        if (project_id, pid) not in existing_pp:
            cur.execute("INSERT INTO ProjectPrograms (ProjectId, ProgramId) VALUES (?, ?)", project_id, pid)
            existing_pp.add((project_id, pid))
    con.commit()

    # scope tables
    scopes = program_scope_sets(cur)
    added = 0
    for prog, sets in scopes.items():
        pid = prog_ids.get(prog)
        if pid is None:
            continue
        for key, (table, col) in SCOPE_TABLES.items():
            ids = sets.get(key, set())
            if not ids:
                continue
            cur.execute(f"SELECT {col} FROM {table} WHERE ProjectId=? AND ProgramId=?", project_id, pid)
            have = {r[0] for r in cur.fetchall()}
            for vid in ids:
                if vid not in have:
                    cur.execute(f"INSERT INTO {table} (ProjectId, ProgramId, {col}) VALUES (?, ?, ?)",
                                project_id, pid, vid)
                    added += 1
    con.commit()
    print(f"  project '{PROJECT_NAME}' id={project_id}; programs +{programs.inserted}; project-programs {len(existing_pp)}")
    print(f"  scope rows added: +{added}")


# ---------------------------------------------------------------------------
# STAGE: employees (users)
# ---------------------------------------------------------------------------
def parse_bool_yesno(v):
    return norm(v) in ("כן", "כ", "yes", "true", "1")


def stage_employees(con):
    cur = con.cursor()
    emproles = Lookups(cur, "EmployeeRoles")

    cur.execute("SELECT IdNumber FROM Users")
    existing = {norm(r[0]) for r in cur.fetchall()}

    added = 0
    seen = set()
    for f in program_files():
        for _title, hi, hdr, rows in iter_classified(f, "employees"):
            ci = {
                "id": col_index(hdr, "ת.ז"),
                "code": col_index(hdr, "קוד עובד"),
                "first": col_index(hdr, "שם פרטי"),
                "last": col_index(hdr, "שם משפחה"),
                "rest": col_index(hdr, "יום מנוחה"),
                "phone": col_index(hdr, "טלפון"),
                "email": col_index(hdr, "מייל"),
                "role": col_index(hdr, "תפקיד"),
                "reporting": col_index(hdr, "עובד מדווח"),
                "future": col_index(hdr, "אפשר דיווח עתידי"),
                "status": col_index(hdr, "סטטוס"),
            }
            for r in rows[hi + 1:]:
                cells = [norm(x) for x in r]
                def g(j):
                    return cells[j] if j is not None and j < len(cells) else ""
                idnum = g(ci["id"])
                if not idnum or idnum in seen:
                    continue
                seen.add(idnum)
                if idnum in existing:
                    continue
                role_id = emproles.get(g(ci["role"])) or 1
                rest = REST_DAY.get(g(ci["rest"]))
                status_id = 1  # פעיל
                cur.execute(
                    "INSERT INTO Users (EmployeeCode, IdNumber, FirstName, LastName, PasswordHash, "
                    "RoleId, UserRoleId, StatusId, IsReportingEmployee, RestDay, AllowFutureReporting, "
                    "Email, Phone, MustChangePassword, FailedLoginAttempts, AcceptedTermsOfUse, CreatedAt) "
                    "VALUES (?, ?, ?, ?, ?, ?, 6, ?, ?, ?, ?, ?, ?, 1, 0, 0, ?)",
                    g(ci["code"]), idnum, g(ci["first"]), g(ci["last"]), _PW_HASH,
                    role_id, status_id,
                    1 if parse_bool_yesno(g(ci["reporting"])) else 0,
                    rest,
                    1 if parse_bool_yesno(g(ci["future"])) else 0,
                    g(ci["email"]) or None, g(ci["phone"]) or None, NOW)
                existing.add(idnum)
                added += 1
    con.commit()
    print(f"  users: +{added} (distinct id numbers seen {len(seen)})")


# ---------------------------------------------------------------------------
# STAGE: allocations (+ junction scope from program sets)
# ---------------------------------------------------------------------------
def parse_daily(v):
    v = norm(v)
    if "ללא הגבלה" in v:
        return None  # unlimited
    m = re.search(r"(\d+(?:\.\d+)?)", v)
    return float(m.group(1)) if m else 9


def clean_output_duration(v):
    v = norm(v)
    parts = [p.strip() for p in v.split(",") if p.strip()]
    return ",".join(parts) if parts else None


def stage_allocations(con):
    cur = con.cursor()
    projects = Lookups(cur, "Projects")
    programs = Lookups(cur, "Programs")
    districts = Lookups(cur, "Districts")
    sectors = Lookups(cur, "Sectors")
    reporttypes = Lookups(cur, "ReportTypes")
    project_id = projects.get(PROJECT_NAME)

    cur.execute("SELECT Id, IdNumber FROM Users")
    user_by_id = {norm(r[1]): r[0] for r in cur.fetchall()}

    # existing allocations by (user, project) -> id
    cur.execute("SELECT Id, UserId, ProjectId FROM Allocations")
    alloc_key = {(r[1], r[2]): r[0] for r in cur.fetchall()}

    scopes = program_scope_sets(cur)
    added = 0
    alloc_ids_by_program = {}  # program name -> list of allocation ids (for framework stage)

    for f in program_files():
        prog = program_name_of(f)
        program_id = programs.get(prog)
        for _title, hi, hdr, rows in iter_classified(f, "allocations"):
            ci = {
                "id": col_index(hdr, "ת.ז"),
                "district": col_index(hdr, "מחוז"),
                "sector": col_index(hdr, "מגזר"),
                "monthly": col_index(hdr, "היקף שעות העסקה חודשי"),
                "annual": col_index(hdr, "היקף שעות העסקה שנתי"),
                "daily": col_index(hdr, "היקף העסקה יומי"),
                "output": col_index(hdr, "משך תפוקה"),
                "reptype": col_index(hdr, "סיווג דיווח"),
            }
            for r in rows[hi + 1:]:
                cells = [norm(x) for x in r]
                def g(j):
                    return cells[j] if j is not None and j < len(cells) else ""
                idnum = g(ci["id"])
                uid = user_by_id.get(idnum)
                if not uid:
                    continue
                key = (uid, project_id)
                if key in alloc_key:
                    aid = alloc_key[key]
                else:
                    def num(x):
                        x = g(x); m = re.search(r"\d+(?:\.\d+)?", x); return float(m.group()) if m else None
                    monthly = num(ci["monthly"]); annual = num(ci["annual"])
                    daily = parse_daily(g(ci["daily"]))
                    cur.execute(
                        "INSERT INTO Allocations (UserId, ProjectId, ReportTypeId, AnnualEmploymentScope, "
                        "MonthlyEmploymentScope, DailyEmploymentScope, OutputDuration, AllowExcelUpload, "
                        "IsActive, CreatedAt) OUTPUT INSERTED.Id VALUES (?, ?, ?, ?, ?, ?, ?, 1, 1, ?)",
                        uid, project_id, reporttypes.get(g(ci["reptype"])),
                        annual, monthly, daily, clean_output_duration(g(ci["output"])), NOW)
                    aid = cur.fetchone()[0]
                    alloc_key[key] = aid
                    added += 1
                    # program link
                    cur.execute("INSERT INTO AllocationPrograms (AllocationId, ProgramId) VALUES (?, ?)", aid, program_id)
                    # district / sector single values
                    did = districts.get(g(ci["district"]))
                    if did:
                        cur.execute("INSERT INTO AllocationDistricts (AllocationId, DistrictId) VALUES (?, ?)", aid, did)
                    sid = sectors.get(g(ci["sector"]))
                    if sid:
                        cur.execute("INSERT INTO AllocationSectors (AllocationId, SectorId) VALUES (?, ?)", aid, sid)
                    # program scope junctions
                    s = scopes.get(prog, {})
                    for vid in s.get("eduprograms", ()):
                        cur.execute("INSERT INTO AllocationEducationalPrograms (AllocationId, EducationalProgramId) VALUES (?, ?)", aid, vid)
                    for vid in s.get("domains", ()):
                        cur.execute("INSERT INTO AllocationDomains (AllocationId, DomainId) VALUES (?, ?)", aid, vid)
                    for vid in s.get("subjects", ()):
                        cur.execute("INSERT INTO AllocationSubjects (AllocationId, SubjectId) VALUES (?, ?)", aid, vid)
                    for vid in s.get("discussion", ()):
                        cur.execute("INSERT INTO AllocationDiscussionCodes (AllocationId, DiscussionCodeId) VALUES (?, ?)", aid, vid)
                    for vid in s.get("classes", ()):
                        cur.execute("INSERT INTO AllocationClasses (AllocationId, ClassId) VALUES (?, ?)", aid, vid)
                    for vid in s.get("grades", ()):
                        cur.execute("INSERT INTO AllocationGradeLevels (AllocationId, GradeLevelId) VALUES (?, ?)", aid, vid)
                    for vid in s.get("ldn", ()):
                        cur.execute("INSERT INTO AllocationLocalityDistrictNationals (AllocationId, LocalityDistrictNationalId) VALUES (?, ?)", aid, vid)
                alloc_ids_by_program.setdefault(prog, []).append(aid)
        con.commit()
    print(f"  allocations: +{added}")


# ---------------------------------------------------------------------------
# STAGE: framework + locality assignment per allocation
# ---------------------------------------------------------------------------
def stage_frameworks(con):
    cur = con.cursor()
    projects = Lookups(cur, "Projects")
    programs = Lookups(cur, "Programs")
    project_id = projects.get(PROJECT_NAME)

    cur.execute("SELECT Id, IdNumber FROM Users")
    user_by_id = {norm(r[1]): r[0] for r in cur.fetchall()}
    cur.execute("SELECT Id, UserId FROM Allocations WHERE ProjectId=?", project_id)
    alloc_by_user = {r[1]: r[0] for r in cur.fetchall()}
    cur.execute("SELECT Id, InstitutionSymbol FROM Frameworks")
    fw_by_symbol = {str(r[1]).strip(): r[0] for r in cur.fetchall() if r[1] is not None}
    cur.execute("SELECT InstitutionSymbol, LocalityId FROM Institutions")
    loc_by_symbol = {str(r[0]).strip(): r[1] for r in cur.fetchall()}

    # allocations belonging to each program
    cur.execute("SELECT ap.ProgramId, ap.AllocationId FROM AllocationPrograms ap "
                "INNER JOIN Allocations a ON a.Id = ap.AllocationId AND a.ProjectId=?", project_id)
    allocs_by_program = {}
    for pid, aid in cur.fetchall():
        allocs_by_program.setdefault(pid, []).append(aid)

    # existing junction pairs to stay idempotent
    cur.execute("SELECT AllocationId, FrameworkId FROM AllocationFrameworks")
    have_fw = {(r[0], r[1]) for r in cur.fetchall()}
    cur.execute("SELECT AllocationId, LocalityId FROM AllocationLocalities")
    have_loc = {(r[0], r[1]) for r in cur.fetchall()}

    fw_added = loc_added = 0

    def assign(aid, symbol):
        nonlocal fw_added, loc_added
        sym = str(symbol).strip()
        fwid = fw_by_symbol.get(sym)
        if fwid and (aid, fwid) not in have_fw:
            cur.execute("INSERT INTO AllocationFrameworks (AllocationId, FrameworkId) VALUES (?, ?)", aid, fwid)
            have_fw.add((aid, fwid)); fw_added += 1
        locid = loc_by_symbol.get(sym)
        if locid and (aid, locid) not in have_loc:
            cur.execute("INSERT INTO AllocationLocalities (AllocationId, LocalityId) VALUES (?, ?)", aid, locid)
            have_loc.add((aid, locid)); loc_added += 1

    for f in program_files():
        prog = program_name_of(f)
        program_id = programs.get(prog)
        per_emp = list(iter_classified(f, "per_employee_frameworks"))
        if per_emp:
            for _title, hi, hdr, rows in per_emp:
                idj = col_index(hdr, "ת.ז")
                symj = col_index(hdr, "סמל מוסד")
                for r in rows[hi + 1:]:
                    cells = [norm(x) for x in r]
                    def g(j):
                        return cells[j] if j is not None and j < len(cells) else ""
                    uid = user_by_id.get(g(idj))
                    if not uid:
                        continue
                    aid = alloc_by_user.get(uid)
                    if not aid:
                        continue
                    sym, _ = parse_symbol(g(symj), "")
                    if sym is not None:
                        assign(aid, sym)
        else:
            # shared: every institution in the program applies to every allocation
            symbols = set()
            for _title, hi, hdr, rows in iter_classified(f, "institutions"):
                c = resolve_institution_cols(hdr)
                for r in rows[hi + 1:]:
                    cells = [norm(x) for x in r]
                    sym, _ = parse_symbol(
                        cells[c["symbol"]] if c["symbol"] is not None and c["symbol"] < len(cells) else "",
                        cells[c["name"]] if c["name"] is not None and c["name"] < len(cells) else "")
                    if sym is not None:
                        symbols.add(sym)
            for aid in allocs_by_program.get(program_id, ()):
                for sym in symbols:
                    assign(aid, sym)
        con.commit()
    print(f"  allocation frameworks: +{fw_added}; allocation localities: +{loc_added}")


STAGES = {
    "lookups": stage_lookups,
    "institutions": stage_institutions,
    "projects": stage_projects,
    "employees": stage_employees,
    "allocations": stage_allocations,
    "frameworks": stage_frameworks,
}
ORDER = ["lookups", "institutions", "projects", "employees", "allocations", "frameworks"]


def main():
    which = sys.argv[1:] or ORDER
    con = connect()
    try:
        for name in which:
            if name not in STAGES:
                print(f"Unknown stage: {name}"); continue
            print(f"\n=== STAGE: {name} ===")
            STAGES[name](con)
        print("\nDone.")
    finally:
        con.close()


if __name__ == "__main__":
    main()

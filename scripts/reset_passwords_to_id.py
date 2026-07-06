"""
Reset every employee's password to their ID number (ת.ז) and require a change
on next login — the onboarding state the client expects after the employee
Excel intake (בדיקת פרויקט, Sheet6 #4).

Default mode is dry-run. Pass --commit to write changes.

Skips: the admin account and any user whose IdNumber is not all digits.
Also clears lockouts (FailedLoginAttempts=0) so everyone can log in fresh.
"""
from __future__ import annotations

import argparse
import sys
from datetime import datetime

import bcrypt
import pyodbc

sys.stdout.reconfigure(encoding="utf-8")

CONN_STR = (
    "DRIVER={ODBC Driver 17 for SQL Server};"
    "SERVER=.\\SQLEXPRESS;"
    "DATABASE=AxiomaReporting;"
    "Trusted_Connection=yes;"
    "TrustServerCertificate=yes;"
)


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--commit", action="store_true", help="write changes (default: dry-run)")
    parser.add_argument("--only-role", type=int, default=None,
                        help="restrict to a UserRoleId (e.g. 6 = employee); default: all non-admin users")
    args = parser.parse_args()

    con = pyodbc.connect(CONN_STR)
    cur = con.cursor()

    sql = "SELECT Id, IdNumber, FirstName, LastName, UserRoleId FROM Users WHERE IdNumber <> 'admin'"
    params: list = []
    if args.only_role is not None:
        sql += " AND UserRoleId = ?"
        params.append(args.only_role)
    users = cur.execute(sql, params).fetchall()

    now = datetime.utcnow()
    updated = skipped = 0
    for u in users:
        id_number = (u.IdNumber or "").strip()
        if not id_number.isdigit():
            print(f"skip (non-numeric id): #{u.Id} {u.FirstName} {u.LastName} [{id_number}]")
            skipped += 1
            continue
        pw_hash = bcrypt.hashpw(id_number.encode("utf-8"), bcrypt.gensalt(rounds=12)).decode("utf-8")
        cur.execute(
            """
            UPDATE Users
            SET PasswordHash = ?, MustChangePassword = 1, FailedLoginAttempts = 0,
                LastPasswordChange = ?, UpdatedAt = ?
            WHERE Id = ?
            """,
            pw_hash, now, now, u.Id,
        )
        updated += 1

    print(f"\nMode: {'COMMIT' if args.commit else 'DRY-RUN'}")
    print(f"users reset to ID password + must-change: {updated}")
    print(f"skipped: {skipped}")

    if args.commit:
        con.commit()
    else:
        con.rollback()
    cur.close()
    con.close()
    return 0


if __name__ == "__main__":
    raise SystemExit(main())

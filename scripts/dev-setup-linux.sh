#!/usr/bin/env bash
#
# dev-setup-linux.sh — one-shot local dev environment bootstrap for the
# Axioma Employee Reporting System on Ubuntu (tested on 24.04 "noble").
#
# It installs the .NET toolchain, a local SQL Server 2022 Express instance,
# applies the EF Core migrations (which seed roles/admin/constants/templates)
# and then loads the client lookup + historical report data from the Excel
# files in database/seed-data.
#
# The Windows workflow documented in DEVELOPER_GUIDE.md still applies for
# developers on Windows with SQL Server Express; this script only exists so
# the project can also be built and run on a Linux box / CI / cloud agent.
#
# Usage:
#   ./scripts/dev-setup-linux.sh
#
# Environment overrides:
#   SA_PASSWORD   SQL Server 'sa' password (default: Axioma@2024!)
#   SKIP_SEED     set to 1 to skip the Excel lookup/report data load
#
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SA_PASSWORD="${SA_PASSWORD:-Axioma@2024!}"
DOTNET_DIR="/usr/share/dotnet"

log() { printf '\n\033[1;36m==> %s\033[0m\n' "$*"; }

# ---------------------------------------------------------------------------
# 1. .NET SDK 8 + ASP.NET Core runtime 6 (projects target net6.0)
# ---------------------------------------------------------------------------
if ! command -v dotnet >/dev/null 2>&1; then
  log "Installing .NET SDK 8 + ASP.NET Core runtime 6"
  curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
  chmod +x /tmp/dotnet-install.sh
  sudo /tmp/dotnet-install.sh --channel 8.0 --install-dir "$DOTNET_DIR"
  sudo /tmp/dotnet-install.sh --channel 6.0 --runtime aspnetcore --install-dir "$DOTNET_DIR"
  sudo ln -sf "$DOTNET_DIR/dotnet" /usr/local/bin/dotnet
fi
export DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_NOLOGO=1
# The ASP.NET Core host enables inotify file watchers on startup. Default
# container inotify instance limits (often 128) are easily exhausted when the
# test suite spins up many hosts, which makes the app/tests crash on boot.
# Raise the limit and prefer polling watchers to make startup deterministic.
sudo sysctl -w fs.inotify.max_user_instances=1024 >/dev/null 2>&1 || true
sudo sysctl -w fs.inotify.max_user_watches=524288 >/dev/null 2>&1 || true
export DOTNET_USE_POLLING_FILE_WATCHER=1
dotnet --version

# ---------------------------------------------------------------------------
# 2. SQL Server 2022 Express + command line tools
# ---------------------------------------------------------------------------
if [ ! -x /opt/mssql/bin/sqlservr ]; then
  log "Installing SQL Server 2022 Express"
  curl -fsSL https://packages.microsoft.com/keys/microsoft.asc \
    | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc >/dev/null
  curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list \
    | sudo tee /etc/apt/sources.list.d/mssql-server-2022.list >/dev/null
  curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/prod.list \
    | sudo tee /etc/apt/sources.list.d/msprod.list >/dev/null
  sudo apt-get update
  sudo ACCEPT_EULA=Y apt-get install -y mssql-server mssql-tools18 unixodbc-dev

  # Ubuntu 24.04 ships OpenLDAP 2.6, but the SQL Server 2022 build links against
  # the 2.5 sonames. Drop the 2.5 libraries next to sqlservr so it can start.
  if [ ! -f /opt/mssql/lib/libldap-2.5.so.0 ]; then
    log "Adding OpenLDAP 2.5 compatibility libraries for SQL Server"
    tmp=$(mktemp -d)
    (cd "$tmp"
     deb=$(curl -s http://security.ubuntu.com/ubuntu/pool/main/o/openldap/ \
            | grep -oE 'libldap-2.5-0_[^"]+_amd64.deb' | sort -u | tail -1)
     curl -sO "http://security.ubuntu.com/ubuntu/pool/main/o/openldap/${deb}"
     dpkg-deb -x "$deb" x
     sudo cp -av x/usr/lib/x86_64-linux-gnu/lib*-2.5.so.0* /opt/mssql/lib/)
    rm -rf "$tmp"
  fi
fi

log "Configuring SQL Server (Express edition)"
sudo MSSQL_SA_PASSWORD="$SA_PASSWORD" ACCEPT_EULA=Y MSSQL_PID=Express \
  /opt/mssql/bin/mssql-conf -n set-sa-password >/dev/null 2>&1 || true

# Start SQL Server. Prefer systemd; fall back to launching sqlservr directly
# (e.g. inside containers / cloud agents where systemd is not PID 1).
if pidof sqlservr >/dev/null 2>&1; then
  log "SQL Server already running"
elif command -v systemctl >/dev/null 2>&1 && systemctl is-system-running >/dev/null 2>&1; then
  sudo systemctl enable --now mssql-server
else
  log "Starting SQL Server directly (no systemd)"
  sudo -u mssql bash -c "LD_LIBRARY_PATH=/opt/mssql/lib \
    MSSQL_SA_PASSWORD='$SA_PASSWORD' ACCEPT_EULA=Y MSSQL_PID=Express \
    nohup /opt/mssql/bin/sqlservr >/tmp/mssql.log 2>&1 &"
fi

log "Waiting for SQL Server to accept connections"
for i in $(seq 1 30); do
  if /opt/mssql-tools18/bin/sqlcmd -S localhost,1433 -U sa -P "$SA_PASSWORD" -C \
       -Q "SELECT 1" >/dev/null 2>&1; then
    echo "SQL Server is up."; break
  fi
  sleep 2
done

# ---------------------------------------------------------------------------
# 3. Build + apply EF Core migrations (creates & seeds the database)
# ---------------------------------------------------------------------------
log "Restoring and building the solution"
cd "$REPO_ROOT"
dotnet restore AxiomaReporting.sln
dotnet build AxiomaReporting.sln -c Debug --no-restore

log "Applying EF Core migrations"
dotnet tool install --global dotnet-ef --version '6.*' 2>/dev/null || true
export PATH="$PATH:$HOME/.dotnet/tools"
ASPNETCORE_ENVIRONMENT=Development dotnet ef database update \
  --project src/AxiomaReporting.Infrastructure \
  --startup-project src/AxiomaReporting.Web

# ---------------------------------------------------------------------------
# 4. Load client lookup + historical report data
# ---------------------------------------------------------------------------
if [ "${SKIP_SEED:-0}" != "1" ]; then
  log "Loading lookup + report data from Excel"
  SEED_ENV=/tmp/axioma-seedenv
  if [ ! -x "$SEED_ENV/bin/python" ]; then
    python3 -m venv "$SEED_ENV"
    curl -fsSL https://bootstrap.pypa.io/get-pip.py -o /tmp/get-pip.py
    "$SEED_ENV/bin/python" /tmp/get-pip.py
    "$SEED_ENV/bin/pip" install pyodbc pyxlsb openpyxl
  fi
  export AXIOMA_CONN_STR="DRIVER={ODBC Driver 18 for SQL Server};SERVER=localhost,1433;DATABASE=AxiomaReporting;UID=sa;PWD=${SA_PASSWORD};TrustServerCertificate=yes;"
  export AXIOMA_SEED_DIR="$REPO_ROOT/database/seed-data"
  ( cd "$REPO_ROOT/database/seed-data"
    "$SEED_ENV/bin/python" seed_lookups.py
    "$SEED_ENV/bin/python" seed_reports.py )
fi

# ---------------------------------------------------------------------------
# 5. (Optional) run the test suite — set RUN_TESTS=1 to enable
# ---------------------------------------------------------------------------
if [ "${RUN_TESTS:-0}" = "1" ]; then
  log "Installing Playwright browsers for the E2E tests"
  PW_DIR="$REPO_ROOT/src/AxiomaReporting.Tests/bin/Debug/net6.0/.playwright"
  if [ -f "$PW_DIR/package/cli.js" ]; then
    # OS-level browser dependencies (needs sudo); browsers into the user cache.
    sudo "$PW_DIR/node/linux-x64/node" "$PW_DIR/package/cli.js" install --with-deps chromium || true
    "$PW_DIR/node/linux-x64/node" "$PW_DIR/package/cli.js" install chromium || true
  fi
  log "Running the full test suite"
  dotnet test AxiomaReporting.sln -c Debug --no-build --settings "$REPO_ROOT/tests.runsettings"
fi

log "Done. Run the app with:"
echo "  ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/AxiomaReporting.Web"
echo "Then open the URL printed by Kestrel (default http://localhost:5121)."
echo "Login: admin / admin1234  (you will be forced to change the password)."

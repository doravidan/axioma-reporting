# Builds the release publish folder for the Axioma Reporting web app.
# Run from anywhere: powershell -ExecutionPolicy Bypass -File deploy\publish.ps1
# Output: deploy\publish\  (copy its CONTENTS to C:\inetpub\AxiomaReporting on the server)

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$out = Join-Path $PSScriptRoot 'publish'

if (Test-Path $out) { Remove-Item -Recurse -Force $out }

dotnet publish (Join-Path $root 'src\AxiomaReporting.Web\AxiomaReporting.Web.csproj') `
  --configuration Release `
  --output $out `
  --self-contained false `
  --runtime win-x64

if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed" }

# Never ship local secrets / dev settings
Remove-Item -Force -ErrorAction SilentlyContinue (Join-Path $out 'appsettings.Development.json')

Write-Host ""
Write-Host "Publish ready: $out"
Write-Host "Next: copy its contents to the server (see deploy\CODEX_DEPLOY_RUNBOOK.md)"

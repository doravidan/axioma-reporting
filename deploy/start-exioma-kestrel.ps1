$ErrorActionPreference = "Stop"

$env:ASPNETCORE_ENVIRONMENT = "Production"
$env:ASPNETCORE_URLS = "http://127.0.0.1:5080"
$env:ConnectionStrings__DefaultConnection = "Server=.\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"

Write-Host "Starting AxiomaReporting.Web on $env:ASPNETCORE_URLS"
Write-Host "Database: .\SQLEXPRESS / AxiomaReporting"

& "$PSScriptRoot\AxiomaReporting.Web.exe"

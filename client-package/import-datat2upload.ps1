param(
    [string]$DataDir = 'C:\WebSites\Exioma\datat2upload',
    [string]$ConnectionString = 'Server=.\SQLEXPRESS;Database=AxiomaReporting;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True',
    [switch]$DryRun
)

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data

$DefaultPasswordHash = '$2a$12$nA0pP6vU/kvXauydNU2TGO2cR03puYxsy.ceB0Fm2ebS2v2Zk6bu.'
$NowSql = 'SYSUTCDATETIME()'

function Normalize-Text([object]$Value) {
    if ($null -eq $Value -or $Value -is [DBNull]) { return $null }
    $s = ([string]$Value).Trim()
    $s = [regex]::Replace($s, '\s+', ' ')
    if ([string]::IsNullOrWhiteSpace($s)) { return $null }
    return $s
}

function Get-Cell($Row, [int]$Index) {
    if ($Index -ge $Row.Table.Columns.Count) { return $null }
    return Normalize-Text $Row[$Index]
}

function Get-Decimal($Value) {
    $s = Normalize-Text $Value
    if (-not $s) { return $null }
    $s = $s.Replace(',', '.')
    $d = 0.0
    if ([double]::TryParse($s, [Globalization.NumberStyles]::Any, [Globalization.CultureInfo]::InvariantCulture, [ref]$d)) { return $d }
    return $null
}

function Get-DateValue($Value) {
    if ($null -eq $Value -or $Value -is [DBNull]) { return $null }
    if ($Value -is [datetime]) { return [datetime]$Value }
    $s = Normalize-Text $Value
    if (-not $s) { return $null }
    $formats = @('dd/MM/yyyy','d/M/yyyy','dd/MM/yy','d/M/yy','MM/dd/yyyy','M/d/yyyy','yyyy-MM-dd')
    $dt = [datetime]::MinValue
    if ([datetime]::TryParseExact($s, $formats, [Globalization.CultureInfo]::InvariantCulture, [Globalization.DateTimeStyles]::None, [ref]$dt)) { return $dt }
    if ([datetime]::TryParse($s, [Globalization.CultureInfo]::GetCultureInfo('he-IL'), [Globalization.DateTimeStyles]::None, [ref]$dt)) { return $dt }
    return $null
}

function Read-ExcelSheet([string]$Path, [string]$SheetName) {
    $props = if ([IO.Path]::GetExtension($Path) -eq '.xlsb') { 'Excel 12.0;HDR=NO;IMEX=1' } else { 'Excel 12.0 Xml;HDR=NO;IMEX=1' }
    $conn = [System.Data.OleDb.OleDbConnection]::new("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=$Path;Extended Properties=`"$props`";")
    $conn.Open()
    try {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = "SELECT * FROM [$SheetName]"
        $adapter = [System.Data.OleDb.OleDbDataAdapter]::new($cmd)
        $table = [System.Data.DataTable]::new()
        [void]$adapter.Fill($table)
        return ,$table
    }
    finally {
        $conn.Close()
    }
}

function Get-SheetNames([string]$Path) {
    $props = if ([IO.Path]::GetExtension($Path) -eq '.xlsb') { 'Excel 12.0;HDR=NO;IMEX=1' } else { 'Excel 12.0 Xml;HDR=NO;IMEX=1' }
    $conn = [System.Data.OleDb.OleDbConnection]::new("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=$Path;Extended Properties=`"$props`";")
    $conn.Open()
    try {
        $schema = $conn.GetOleDbSchemaTable([System.Data.OleDb.OleDbSchemaGuid]::Tables, $null)
        return ,@($schema.Rows | ForEach-Object { [string]$_['TABLE_NAME'] } | Where-Object { $_ -like '*$*' -and $_ -notlike '*FilterDatabase*' })
    }
    finally {
        $conn.Close()
    }
}

function Exec-Scalar($Conn, $Tx, [string]$Sql, [hashtable]$Params = @{}) {
    $cmd = $Conn.CreateCommand()
    $cmd.Transaction = $Tx
    $cmd.CommandText = $Sql
    foreach ($key in $Params.Keys) { [void]$cmd.Parameters.AddWithValue($key, $(if ($null -eq $Params[$key]) { [DBNull]::Value } else { $Params[$key] })) }
    return $cmd.ExecuteScalar()
}

function Exec-NonQuery($Conn, $Tx, [string]$Sql, [hashtable]$Params = @{}) {
    $cmd = $Conn.CreateCommand()
    $cmd.Transaction = $Tx
    $cmd.CommandText = $Sql
    foreach ($key in $Params.Keys) { [void]$cmd.Parameters.AddWithValue($key, $(if ($null -eq $Params[$key]) { [DBNull]::Value } else { $Params[$key] })) }
    return $cmd.ExecuteNonQuery()
}

function Ensure-Description($Conn, $Tx, [string]$Table, [string]$Description) {
    $d = Normalize-Text $Description
    if (-not $d) { return $null }
    $id = Exec-Scalar $Conn $Tx "SELECT TOP 1 Id FROM [$Table] WHERE Description = @d" @{ '@d' = $d }
    if ($id) { return [int]$id }
    if (-not $DryRun) {
        $id = Exec-Scalar $Conn $Tx "INSERT INTO [$Table] (CreatedAt, UpdatedAt, Description, IsActive) OUTPUT INSERTED.Id VALUES ($NowSql, NULL, @d, 1)" @{ '@d' = $d }
    }
    return [int]$id
}

function Ensure-Framework($Conn, $Tx, [string]$Description) {
    $d = Normalize-Text $Description
    if (-not $d) { return $null }
    $id = Exec-Scalar $Conn $Tx 'SELECT TOP 1 Id FROM Frameworks WHERE Description = @d' @{ '@d' = $d }
    if ($id) { return [int]$id }
    $m = [regex]::Match($d, '\d{5,9}')
    $symbol = if ($m.Success) { $m.Value } else { ($d.Substring(0, [Math]::Min(100, $d.Length))) }
    if (-not $DryRun) {
        $id = Exec-Scalar $Conn $Tx "INSERT INTO Frameworks (InstitutionSymbol, EducationalStageId, CreatedAt, UpdatedAt, Description, IsActive) OUTPUT INSERTED.Id VALUES (@symbol, NULL, $NowSql, NULL, @d, 1)" @{ '@symbol' = $symbol; '@d' = $d }
    }
    return [int]$id
}

function Ensure-User($Conn, $Tx, [string]$EmployeeCode, [string]$IdNumber, [string]$FirstName, [string]$LastName, [string]$Phone, [string]$Email, [string]$RoleDescription, [bool]$IsReporting, [int]$RestDay, [bool]$AllowFuture) {
    $employeeCode = Normalize-Text $EmployeeCode
    if (-not $employeeCode) { return $null }
    $id = Exec-Scalar $Conn $Tx 'SELECT TOP 1 Id FROM Users WHERE EmployeeCode = @code' @{ '@code' = $employeeCode }
    if ($id) { return [int]$id }

    $idNumber = Normalize-Text $IdNumber
    if (-not $idNumber) { $idNumber = "EMP$employeeCode" }
    $firstName = Normalize-Text $FirstName
    $lastName = Normalize-Text $LastName
    if (-not $firstName) { $firstName = $employeeCode }
    if (-not $lastName) { $lastName = '-' }
    $roleId = Ensure-Description $Conn $Tx 'EmployeeRoles' ($(if ($RoleDescription) { $RoleDescription } else { 'מנחה' }))
    $statusId = 1
    $userRoleId = 6
    if (-not $DryRun) {
        $id = Exec-Scalar $Conn $Tx "INSERT INTO Users (EmployeeCode, IdNumber, FirstName, LastName, PasswordHash, RoleId, UserRoleId, StatusId, IsReportingEmployee, RestDay, AllowFutureReporting, Notes, Email, Phone, MustChangePassword, FailedLoginAttempts, LastPasswordChange, AcceptedTermsOfUse, CreatedBy, UpdatedBy, CreatedAt, UpdatedAt) OUTPUT INSERTED.Id VALUES (@code, @idn, @fn, @ln, @hash, @role, @userRole, @status, @isReporting, @restDay, @allowFuture, NULL, @email, @phone, 1, 0, NULL, 0, 1, NULL, $NowSql, NULL)" @{
            '@code' = $employeeCode; '@idn' = $idNumber; '@fn' = $firstName; '@ln' = $lastName; '@hash' = $DefaultPasswordHash; '@role' = $roleId; '@userRole' = $userRoleId; '@status' = $statusId; '@isReporting' = $IsReporting; '@restDay' = $(if ($RestDay -ge 0) { $RestDay } else { $null }); '@allowFuture' = $AllowFuture; '@email' = $(Normalize-Text $Email); '@phone' = $(Normalize-Text $Phone)
        }
    }
    return [int]$id
}

function Ensure-Report($Conn, $Tx, [int]$UserId, [int]$ReportingMonthId) {
    $id = Exec-Scalar $Conn $Tx 'SELECT TOP 1 Id FROM Reports WHERE UserId = @u AND ReportingMonthId = @m' @{ '@u' = $UserId; '@m' = $ReportingMonthId }
    if ($id) { return [int]$id }
    if (-not $DryRun) {
        $id = Exec-Scalar $Conn $Tx "INSERT INTO Reports (UserId, ReportingMonthId, StatusId, SubmittedAt, ApprovedAt, ApprovedBy, RejectionReason, RejectedAt, RejectedBy, ImportedFromExcel, CreatedAt, UpdatedAt) OUTPUT INSERTED.Id VALUES (@u, @m, 3, $NowSql, NULL, NULL, NULL, NULL, NULL, 1, $NowSql, NULL)" @{ '@u' = $UserId; '@m' = $ReportingMonthId }
    }
    return [int]$id
}

function DayToInt([string]$Day) {
    switch (Normalize-Text $Day) {
        'ראשון' { 0 }
        'שני' { 1 }
        'שלישי' { 2 }
        'רביעי' { 3 }
        'חמישי' { 4 }
        'שישי' { 5 }
        'שבת' { 6 }
        default { -1 }
    }
}

function Split-Name([string]$FullName) {
    $n = Normalize-Text $FullName
    if (-not $n) { return @($null, $null) }
    $parts = @($n -split ' ')
    if ($parts.Count -eq 1) { return @($parts[0], '-') }
    return @($parts[0], (($parts | Select-Object -Skip 1) -join ' '))
}

$files = @{
    Base = Join-Path $DataDir 'BASE DATA.xlsb'
    Shared = Join-Path $DataDir 'קובץ משותף שאלונים לכל התוכניות 12.3.26.xlsx'
    Employees = Join-Path $DataDir 'קובץ נתוני עובדים מערכת חדשה- שמיים.xlsx'
}

$conn = [System.Data.SqlClient.SqlConnection]::new($ConnectionString)
$conn.Open()
$tx = $conn.BeginTransaction()

$stats = [ordered]@{
    LookupValuesSeen = 0
    UsersSeen = 0
    AllocationsSeen = 0
    ReportRowsSeen = 0
    ReportRowsInserted = 0
}

try {
    $jan2026 = Exec-Scalar $conn $tx 'SELECT TOP 1 Id FROM ReportingMonths WHERE [Month] = 1 AND [Year] = 2026 ORDER BY Id' @{}
    if (-not $jan2026) { throw 'Reporting month January 2026 was not found.' }
    $projectId = Ensure-Description $conn $tx 'Projects' 'חינוך ילדים ונוער בסיכון'
    $programId = Ensure-Description $conn $tx 'Programs' 'שמיים'
    if (-not $DryRun) {
        [void](Exec-NonQuery $conn $tx 'IF NOT EXISTS (SELECT 1 FROM ProjectPrograms WHERE ProjectId = @p AND ProgramId = @g) INSERT INTO ProjectPrograms (ProjectId, ProgramId) VALUES (@p, @g)' @{ '@p' = $projectId; '@g' = $programId })
    }

    $lookupSources = @(
        @{ Path = $files.Shared; Sheet = "'כללי - מאוחד$'"; Start = 1; Map = @{ 1='EducationalPrograms'; 2='Domains'; 3='Subjects'; 4='Subjects'; 5='DiscussionCodes'; 6='SchoolClasses'; 7='Frameworks'; 8='LocalityDistrictNationals'; 9='GradeLevels'; 10='SchoolClasses' } },
        @{ Path = $files.Employees; Sheet = "'שמים - ערכי טבלאות קוד$'"; Start = 1; Map = @{ 0='Localities'; 1='Frameworks'; 2='Domains'; 3='Subjects'; 4='Subjects'; 5='DiscussionCodes'; 6='SchoolClasses'; 7='Frameworks'; 8='LocalityDistrictNationals'; 9='GradeLevels'; 10='SchoolClasses' } }
    )
    foreach ($source in $lookupSources) {
        $table = Read-ExcelSheet $source.Path $source.Sheet
        for ($r = $source.Start; $r -lt $table.Rows.Count; $r++) {
            $row = $table.Rows[$r]
            foreach ($idx in $source.Map.Keys) {
                $value = Get-Cell $row ([int]$idx)
                if (-not $value) { continue }
                $stats.LookupValuesSeen++
                $target = $source.Map[$idx]
                if ($target -eq 'Frameworks') { [void](Ensure-Framework $conn $tx $value) }
                else { [void](Ensure-Description $conn $tx $target $value) }
            }
        }
    }

    $users = Read-ExcelSheet $files.Employees "'שמים - מאגר עובדים$'"
    for ($r = 2; $r -lt $users.Rows.Count; $r++) {
        $row = $users.Rows[$r]
        $code = Get-Cell $row 2
        if (-not $code) { continue }
        $stats.UsersSeen++
        $isReporting = (Get-Cell $row 10) -eq 'כן'
        $allowFuture = (Get-Cell $row 12) -eq 'כן'
        [void](Ensure-User $conn $tx $code (Get-Cell $row 1) (Get-Cell $row 3) (Get-Cell $row 4) (Get-Cell $row 8) (Get-Cell $row 9) (Get-Cell $row 11) $isReporting (DayToInt (Get-Cell $row 6)) $allowFuture)
    }

    $allocations = Read-ExcelSheet $files.Employees "'שמים - הקצאות$'"
    for ($r = 1; $r -lt $allocations.Rows.Count; $r++) {
        $row = $allocations.Rows[$r]
        $code = Get-Cell $row 2
        if (-not $code) { continue }
        $stats.AllocationsSeen++
        $uid = Ensure-User $conn $tx $code (Get-Cell $row 1) (Get-Cell $row 3) (Get-Cell $row 4) $null $null 'מנחה' $true -1 $false
        $proj = Ensure-Description $conn $tx 'Projects' (Get-Cell $row 5)
        $prog = Ensure-Description $conn $tx 'Programs' (Get-Cell $row 6)
        $district = Ensure-Description $conn $tx 'Districts' (Get-Cell $row 7)
        $sector = Ensure-Description $conn $tx 'Sectors' (Get-Cell $row 8)
        $allocationId = Exec-Scalar $conn $tx 'SELECT TOP 1 Id FROM Allocations WHERE UserId = @u AND ProjectId = @p' @{ '@u' = $uid; '@p' = $proj }
        if (-not $allocationId -and -not $DryRun) {
            $allocationId = Exec-Scalar $conn $tx "INSERT INTO Allocations (UserId, ProjectId, AnnualEmploymentScope, MonthlyEmploymentScope, DailyEmploymentScope, MonthlyRowAllocation, AnnualRowAllocation, OutputDuration, AllowExcelUpload, Notes, IsActive, CreatedAt, UpdatedAt) OUTPUT INSERTED.Id VALUES (@u, @p, @annual, @monthly, NULL, NULL, NULL, @duration, 1, @dailyText, 1, $NowSql, NULL)" @{
                '@u' = $uid; '@p' = $proj; '@annual' = (Get-Decimal (Get-Cell $row 10)); '@monthly' = (Get-Decimal (Get-Cell $row 9)); '@duration' = (Get-Cell $row 12); '@dailyText' = (Get-Cell $row 11)
            }
        }
        if (-not $DryRun) {
            if ($prog) { [void](Exec-NonQuery $conn $tx 'IF NOT EXISTS (SELECT 1 FROM ProjectPrograms WHERE ProjectId=@p AND ProgramId=@g) INSERT INTO ProjectPrograms(ProjectId,ProgramId) VALUES(@p,@g)' @{ '@p'=$proj; '@g'=$prog }) }
            if ($allocationId -and $prog) { [void](Exec-NonQuery $conn $tx 'IF NOT EXISTS (SELECT 1 FROM AllocationPrograms WHERE AllocationId=@a AND ProgramId=@x) INSERT INTO AllocationPrograms(AllocationId,ProgramId) VALUES(@a,@x)' @{ '@a'=$allocationId; '@x'=$prog }) }
            if ($allocationId -and $district) { [void](Exec-NonQuery $conn $tx 'IF NOT EXISTS (SELECT 1 FROM AllocationDistricts WHERE AllocationId=@a AND DistrictId=@x) INSERT INTO AllocationDistricts(AllocationId,DistrictId) VALUES(@a,@x)' @{ '@a'=$allocationId; '@x'=$district }) }
            if ($allocationId -and $sector) { [void](Exec-NonQuery $conn $tx 'IF NOT EXISTS (SELECT 1 FROM AllocationSectors WHERE AllocationId=@a AND SectorId=@x) INSERT INTO AllocationSectors(AllocationId,SectorId) VALUES(@a,@x)' @{ '@a'=$allocationId; '@x'=$sector }) }
        }
    }

    $baseSheets = Get-SheetNames $files.Base | Where-Object { $_ -like '*דיווח מספר*' -or $_ -like '*דייווח מספר*' }
    $nextSeqByReport = @{}
    foreach ($sheet in $baseSheets) {
        $table = Read-ExcelSheet $files.Base $sheet
        for ($r = 2; $r -lt $table.Rows.Count; $r++) {
            $row = $table.Rows[$r]
            $code = Get-Cell $row 1
            $date = Get-DateValue (Get-Cell $row 6)
            $duration = Get-Decimal (Get-Cell $row 7)
            if (-not $code -or -not $date -or -not $duration) { continue }
            $stats.ReportRowsSeen++
            $nameParts = Split-Name (Get-Cell $row 2)
            $uid = Ensure-User $conn $tx $code $null $nameParts[0] $nameParts[1] $null $null 'מנחה' $true -1 $false
            $reportId = Ensure-Report $conn $tx $uid ([int]$jan2026)
            if (-not $nextSeqByReport.ContainsKey($reportId)) {
                $maxSeq = Exec-Scalar $conn $tx 'SELECT ISNULL(MAX(SequenceNumber),0) FROM ReportRows WHERE ReportId=@r' @{ '@r'=$reportId }
                $nextSeqByReport[$reportId] = [int]$maxSeq
            }
            $district = Ensure-Description $conn $tx 'Districts' (Get-Cell $row 3)
            $locality = Ensure-Description $conn $tx 'Localities' (Get-Cell $row 4)
            $framework = Ensure-Framework $conn $tx (Get-Cell $row 5)
            $eduProgram = Ensure-Description $conn $tx 'EducationalPrograms' (Get-Cell $row 8)
            $domain = Ensure-Description $conn $tx 'Domains' (Get-Cell $row 9)
            $subject1 = Ensure-Description $conn $tx 'Subjects' (Get-Cell $row 10)
            $subject2 = Ensure-Description $conn $tx 'Subjects' (Get-Cell $row 11)
            $discussion = Ensure-Description $conn $tx 'DiscussionCodes' (Get-Cell $row 12)
            $conclusionClass = Ensure-Description $conn $tx 'SchoolClasses' (Get-Cell $row 13)
            $conclusionFramework = Ensure-Framework $conn $tx (Get-Cell $row 14)
            $conclusionLocation = Ensure-Description $conn $tx 'LocalityDistrictNationals' (Get-Cell $row 15)
            $grade = Ensure-Description $conn $tx 'GradeLevels' (Get-Cell $row 16)
            $class = Ensure-Description $conn $tx 'SchoolClasses' (Get-Cell $row 17)
            $notes = Get-Cell $row 18
            $allocationId = Exec-Scalar $conn $tx 'SELECT TOP 1 Id FROM Allocations WHERE UserId=@u AND ProjectId=@p' @{ '@u'=$uid; '@p'=$projectId }
            $exists = Exec-Scalar $conn $tx @'
SELECT TOP 1 Id
FROM ReportRows
WHERE ReportId=@report
  AND MeetingDate=@date
  AND MeetingDuration=@duration
  AND DistrictId=@district
  AND LocalityId=@locality
  AND FrameworkId=@framework
  AND EducationalProgramId=@eduProgram
  AND DomainId=@domain
  AND Subject1Id=@subject1
  AND ISNULL(Subject2Id, -1)=ISNULL(@subject2, -1)
  AND ISNULL(DiscussionCodeId, -1)=ISNULL(@discussion, -1)
  AND ISNULL(ConclusionClassId, -1)=ISNULL(@conclusionClass, -1)
  AND ISNULL(ConclusionFrameworkId, -1)=ISNULL(@conclusionFramework, -1)
  AND ISNULL(ConclusionLocationId, -1)=ISNULL(@conclusionLocation, -1)
  AND ISNULL(GradeLevelId, -1)=ISNULL(@grade, -1)
  AND ISNULL(ClassId, -1)=ISNULL(@class, -1)
  AND ISNULL(Notes, N'')=ISNULL(@notes, N'')
'@ @{
                '@report'=$reportId; '@date'=$date; '@duration'=$duration; '@district'=$district; '@locality'=$locality; '@framework'=$framework; '@eduProgram'=$eduProgram; '@domain'=$domain; '@subject1'=$subject1; '@subject2'=$subject2; '@discussion'=$discussion; '@conclusionClass'=$conclusionClass; '@conclusionFramework'=$conclusionFramework; '@conclusionLocation'=$conclusionLocation; '@grade'=$grade; '@class'=$class; '@notes'=$notes
            }
            if ($exists) { continue }
            $nextSeqByReport[$reportId] = [int]$nextSeqByReport[$reportId] + 1
            if (-not $DryRun) {
                [void](Exec-NonQuery $conn $tx "INSERT INTO ReportRows (ReportId, AllocationId, SequenceNumber, MeetingDate, MeetingDuration, DistrictId, LocalityId, FrameworkId, EducationalProgramId, DomainId, Subject1Id, Subject2Id, DiscussionCodeId, ConclusionClassId, ConclusionFrameworkId, ConclusionLocationId, GradeLevelId, ClassId, Notes, CreatedAt, UpdatedAt, ReportTypeId) VALUES (@report, @allocation, @seq, @date, @duration, @district, @locality, @framework, @eduProgram, @domain, @subject1, @subject2, @discussion, @conclusionClass, @conclusionFramework, @conclusionLocation, @grade, @class, @notes, $NowSql, NULL, @reportType)" @{
                    '@report'=$reportId; '@allocation'=$allocationId; '@seq'=$nextSeqByReport[$reportId]; '@date'=$date; '@duration'=$duration; '@district'=$district; '@locality'=$locality; '@framework'=$framework; '@eduProgram'=$eduProgram; '@domain'=$domain; '@subject1'=$subject1; '@subject2'=$subject2; '@discussion'=$discussion; '@conclusionClass'=$conclusionClass; '@conclusionFramework'=$conclusionFramework; '@conclusionLocation'=$conclusionLocation; '@grade'=$grade; '@class'=$class; '@notes'=$notes; '@reportType'=$(if ((Get-Cell $row 15) -match 'ארצי|מחוז') { 1 } else { 2 })
                })
                $stats.ReportRowsInserted++
            }
        }
    }

    if ($DryRun) { $tx.Rollback() } else { $tx.Commit() }
}
catch {
    $tx.Rollback()
    throw
}
finally {
    $conn.Close()
}

$stats.GetEnumerator() | ForEach-Object { '{0}: {1}' -f $_.Key, $_.Value }

<#
.SYNOPSIS
    Creates NuGet packages.

.DESCRIPTION
    Packs the solution into NuGet packages.

.PARAMETER Configuration
    The build configuration (Debug or Release). Default is 'Release'.

.PARAMETER NoBuild
    If specified, skips the build step.

.EXAMPLE
    .\Pack.ps1
    .\Pack.ps1 -Configuration Debug
#>

[CmdletBinding()]
param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release',
    
    [switch]$NoBuild
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$solutionFile = Get-ChildItem -Path $repoRoot -Filter '*.sln' -File | Select-Object -First 1

if (-not $solutionFile) {
    throw "No solution file found in $repoRoot"
}

$solutionPath = $solutionFile.FullName
$solutionName = $solutionFile.BaseName

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Packing: $solutionName" -ForegroundColor Cyan
Write-Host "  Configuration: $Configuration" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Push-Location $repoRoot
try {
    $buildArg = if ($NoBuild) { '--no-build' } else { '' }
    
    Write-Host "[Pack] Creating NuGet packages..." -ForegroundColor Yellow
    dotnet pack $solutionPath -c $Configuration $buildArg
    
    Write-Host ""
    Write-Host "[Pack] Packages created successfully." -ForegroundColor Green
    
    # List created packages
    $artifactsDir = Join-Path $repoRoot 'artifacts'
    if (Test-Path $artifactsDir) {
        Write-Host ""
        Write-Host "Created packages:" -ForegroundColor Cyan
        Get-ChildItem -Path $artifactsDir -Filter '*.nupkg' | ForEach-Object {
            Write-Host "  - $($_.Name)" -ForegroundColor White
        }
    }
}
finally {
    Pop-Location
}

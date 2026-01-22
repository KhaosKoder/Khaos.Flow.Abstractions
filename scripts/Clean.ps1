<#
.SYNOPSIS
    Cleans build artifacts.

.DESCRIPTION
    Removes bin, obj, artifacts, and TestResults folders.

.PARAMETER Configuration
    The build configuration to clean (Debug or Release). If not specified, cleans all configurations.

.EXAMPLE
    .\Clean.ps1
    .\Clean.ps1 -Configuration Release
#>

[CmdletBinding()]
param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Cleaning: $(Split-Path $repoRoot -Leaf)" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$foldersToClean = @('bin', 'obj', 'artifacts', 'TestResults')

foreach ($folder in $foldersToClean) {
    $paths = Get-ChildItem -Path $repoRoot -Directory -Recurse -Filter $folder -ErrorAction SilentlyContinue
    foreach ($path in $paths) {
        Write-Host "[Clean] Removing: $($path.FullName)" -ForegroundColor Yellow
        Remove-Item -Path $path.FullName -Recurse -Force -ErrorAction SilentlyContinue
    }
}

Write-Host ""
Write-Host "[Clean] Clean completed." -ForegroundColor Green

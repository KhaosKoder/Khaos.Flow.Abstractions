<#
.SYNOPSIS
    Gets the current version based on Git tags.

.DESCRIPTION
    Uses MinVer to determine the current version from Git tags.

.EXAMPLE
    .\Get-Version.ps1
#>

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$srcDir = Join-Path $repoRoot 'src'

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Version: $(Split-Path $repoRoot -Leaf)" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Push-Location $repoRoot
try {
    # Find any packable project
    $project = Get-ChildItem -Path $srcDir -Filter '*.csproj' -Recurse | Select-Object -First 1
    
    if ($project) {
        Write-Host "[Version] Querying version via MinVer..." -ForegroundColor Yellow
        $version = dotnet msbuild $project.FullName -getProperty:MinVerVersion 2>$null
        
        if ($version) {
            Write-Host ""
            Write-Host "Current version: $version" -ForegroundColor Green
        }
        else {
            Write-Host "Could not determine version. Ensure MinVer is configured and you have Git tags." -ForegroundColor Yellow
        }
    }
    else {
        Write-Host "No project found to query version." -ForegroundColor Yellow
    }
}
finally {
    Pop-Location
}

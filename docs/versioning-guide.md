# Khaos.Flow.Abstractions – Versioning Guide

This document describes how versions are managed for the Flow Abstractions package.

## Version Strategy

This package uses **MinVer** for automatic semantic versioning based on Git tags.

### Configuration

In `Directory.Build.props`:

```xml
<MinVerTagPrefix>Khaos.Flow.Abstractions/v</MinVerTagPrefix>
<MinVerDefaultPreReleaseIdentifiers>alpha.0</MinVerDefaultPreReleaseIdentifiers>
```

### Tag Format

Tags follow the pattern: `Khaos.Flow.Abstractions/vX.Y.Z`

Examples:
- `Khaos.Flow.Abstractions/v1.0.0` → Version 1.0.0
- `Khaos.Flow.Abstractions/v1.1.0` → Version 1.1.0
- `Khaos.Flow.Abstractions/v2.0.0` → Version 2.0.0 (breaking change)

## Semantic Versioning for Abstractions

Since this is an abstractions package, version changes have specific implications:

### Major Version (X.0.0)
- Breaking changes to existing interfaces
- Removing interface members
- Changing method signatures
- Changing generic constraints

### Minor Version (0.X.0)
- New interfaces added
- New methods added to interfaces (with default implementations)
- New types added

### Patch Version (0.0.X)
- Documentation updates
- XML comment improvements
- Non-breaking bug fixes

## Release Workflow

### 1. Check Current Version

```powershell
cd Khaos.Flow.Abstractions
.\scripts\Get-Version.ps1
```

### 2. Create Release Tag

```powershell
# For a new minor release
git tag Khaos.Flow.Abstractions/v1.1.0
git push origin Khaos.Flow.Abstractions/v1.1.0

# For a patch release
git tag Khaos.Flow.Abstractions/v1.0.1
git push origin Khaos.Flow.Abstractions/v1.0.1
```

### 3. Build and Pack

```powershell
.\scripts\Build.ps1
.\scripts\Pack.ps1
```

### 4. Publish

```powershell
dotnet nuget push artifacts/*.nupkg --source nuget.org --api-key YOUR_KEY
```

## Pre-release Versions

Between tags, MinVer automatically generates pre-release versions:

- After `v1.0.0` tag: `1.0.1-alpha.0.1` (commit count since tag)
- Pattern: `{next-patch}-alpha.0.{commits}`

## Version Compatibility

### Implementers

Packages implementing these interfaces (e.g., `KhaosCode.Flow`) should:
- Reference a specific minor version range: `[1.0, 2.0)`
- Update promptly when new minor versions are released

### Consumers

Applications using flows should:
- Reference the implementation package, not abstractions directly
- Let the implementation package control abstraction versions

## Guidelines

1. **Never manually set Version** in project files
2. **Tag releases** from the main branch only
3. **Document breaking changes** in release notes
4. **Coordinate with implementation packages** when making changes

# Khaos.Flow.Abstractions – Developer Guide

This document explains how to extend and maintain the flow abstractions package. It complements the user-facing guide and the versioning reference found in `docs/versioning-guide.md`.

## Solution Layout

- `src/Khaos.Flow.Abstractions`: Core interfaces and types. Keep the API surface minimal and stable.
- `tests/Khaos.Flow.Abstractions.Tests`: xUnit test suite for all public types.
- `scripts/`: PowerShell helper scripts for common workflows.
- `docs/`: Markdown documentation bundled inside the NuGet package.

## Key Types

| Type | Description |
|------|-------------|
| `FlowOutcome` | String-based outcome (Success, Failure, or custom) that determines flow direction |
| `IFlowContext` | Marker interface with `IServiceProvider Services` property |
| `IFlowStep<TContext>` | A single step that returns a `FlowOutcome` |
| `IFlowStepDefinition<TContext>` | Step with transition mappings to other steps |
| `IFlowDefinition<TContext>` | Immutable description of a complete flow |
| `IFlowExecutor<TContext>` | Executes a flow definition |
| `IFlowBuilder<TContext>` | Fluent builder for constructing flows |

## Coding Guidelines

1. **Stability First**
   - This is an abstractions package—changes here affect all consumers.
   - Prefer adding new interfaces/methods over modifying existing ones.
   - Use `[Obsolete]` attributes before removing anything.

2. **Minimal Dependencies**
   - Only depend on `Microsoft.Extensions.DependencyInjection.Abstractions`.
   - Avoid adding runtime dependencies that implementers must also take.

3. **Generic Constraints**
   - `IFlowStep<TContext>` requires `TContext : IFlowContext`.
   - This enables DI integration via the `Services` property.

4. **Outcome Design**
   - `FlowOutcome` uses strings for extensibility (custom outcomes).
   - Predefined `Success` and `Failure` are static properties.
   - Equality comparison is ordinal (case-sensitive).

## Testing

- Run tests: `pwsh ./scripts/Test.ps1`
- All public types should have corresponding test coverage.
- Focus on interface contracts and edge cases for value types like `FlowOutcome`.

## Build & Packaging

- `pwsh ./scripts/Build.ps1`: Restore + build in Release.
- `pwsh ./scripts/Clean.ps1`: Remove TestResults, artifacts, run `dotnet clean`.
- `pwsh ./scripts/Pack.ps1`: Create NuGet package.
- Uses **MinVer** with prefix `Khaos.Flow.Abstractions/v` for versioning.

## Versioning

- Follow SemVer strictly for abstractions:
  - **Major**: Breaking changes to interfaces.
  - **Minor**: New interfaces or methods.
  - **Patch**: Documentation or non-breaking fixes.
- See `docs/versioning-guide.md` for tagging workflow.

## Related Packages

- **KhaosCode.Flow**: Default implementation of these interfaces.
- **KhaosCode.Pipeline.Abstractions**: Pipeline interfaces (can be combined with flows).

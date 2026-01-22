# Khaos.Flow.Abstractions

Core abstractions for state-machine style workflow orchestration with branching and transitions.

## Overview

This package provides the interfaces and core types for defining flows - state-machine workflows where each step returns an outcome that determines the next step to execute.

**Key characteristics of Flows:**
- **Branching**: Steps return outcomes (Success, Failure, custom) that map to different transitions
- **Orchestration**: Control complex workflows with conditional paths
- **Context-based**: All steps share a context (`IFlowContext`) for state

## Key Types

| Type | Description |
|------|-------------|
| `IFlowStep<TContext>` | A single step in a flow that returns a `FlowOutcome` |
| `IFlowDefinition<TContext>` | Immutable description of a flow with steps and transitions |
| `IFlowExecutor<TContext>` | Executes a flow definition with a given context |
| `FlowOutcome` | The result of a step (Success, Failure, or custom) |
| `IFlowContext` | Marker interface for flow contexts (must provide `IServiceProvider`) |

## Usage

```csharp
// Define a flow step
public class ValidateDatabaseStep : IFlowStep<StartupContext>
{
    public Task<FlowOutcome> ExecuteAsync(StartupContext context, CancellationToken ct)
    {
        // Validate database connection
        return Task.FromResult(isValid ? FlowOutcome.Success : FlowOutcome.Failure);
    }
}
```

## When to Use Flows vs Pipelines

| Use Flows When | Use Pipelines When |
|----------------|-------------------|
| Need branching logic | Need linear transformation |
| Orchestrating startup/shutdown | Processing messages/records |
| Steps may take different paths | Every input produces an output |
| State-machine workflows | Data transformation chains |

## Related Packages

- `KhaosCode.Flow` - Implementation of flow execution
- `KhaosCode.Pipeline.Abstractions` - Pipeline abstractions (complementary pattern)
- `KhaosCode.Pipeline` - Pipeline implementation

## License

MIT License - see [LICENSE.md](LICENSE.md)

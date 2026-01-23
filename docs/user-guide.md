# Khaos.Flow.Abstractions – User Guide

This package provides the core interfaces for building outcome-driven workflow orchestration in .NET.

## Installation

```bash
dotnet add package KhaosCode.Flow.Abstractions
```

## Overview

Flows are state-machine style workflows where each step returns an outcome that determines which step executes next. This enables complex branching logic and conditional execution paths.

### Key Concepts

| Concept | Description |
|---------|-------------|
| **FlowOutcome** | The result of a step (Success, Failure, or custom) |
| **Flow Step** | A single unit of work that returns an outcome |
| **Flow Definition** | A complete workflow with steps and transitions |
| **Flow Executor** | Runs a flow definition with a given context |

## Core Types

### FlowOutcome

A string-based result type:

```csharp
// Predefined outcomes
FlowOutcome.Success   // Step completed successfully
FlowOutcome.Failure   // Step encountered an error

// Custom outcomes for branching
var retry = FlowOutcome.Custom("Retry");
var needsApproval = new FlowOutcome("NeedsApproval");
```

### IFlowContext

All flow contexts must implement this interface:

```csharp
public interface IFlowContext
{
    IServiceProvider Services { get; }
}

// Example implementation
public class OrderFlowContext : IFlowContext
{
    public IServiceProvider Services { get; }
    public Order Order { get; set; }
    
    public OrderFlowContext(IServiceProvider services)
    {
        Services = services;
    }
}
```

### IFlowStep&lt;TContext&gt;

Implement this to create flow steps:

```csharp
public interface IFlowStep<TContext> where TContext : IFlowContext
{
    string Name { get; }
    ValueTask<FlowOutcome> ExecuteAsync(TContext context, CancellationToken cancellationToken);
}

// Example
public class ValidateOrderStep : IFlowStep<OrderFlowContext>
{
    public string Name => "ValidateOrder";
    
    public ValueTask<FlowOutcome> ExecuteAsync(OrderFlowContext ctx, CancellationToken ct)
    {
        if (ctx.Order.Items.Count == 0)
            return ValueTask.FromResult(FlowOutcome.Failure);
            
        return ValueTask.FromResult(FlowOutcome.Success);
    }
}
```

### IFlowExecutor&lt;TContext&gt;

Executes a flow definition:

```csharp
public interface IFlowExecutor<TContext> where TContext : IFlowContext
{
    ValueTask<FlowOutcome> ExecuteAsync(
        IFlowDefinition<TContext> flow,
        TContext context,
        CancellationToken cancellationToken);
}
```

### IFlowBuilder&lt;TContext&gt;

Build flows fluently:

```csharp
public interface IFlowBuilder<TContext> where TContext : IFlowContext
{
    IFlowStepBuilder<TContext> AddStep(IFlowStep<TContext> step);
    IFlowStepBuilder<TContext> AddStep(string name, Func<TContext, CancellationToken, ValueTask<FlowOutcome>> execute);
    IFlowDefinition<TContext> Build();
}
```

## Usage Pattern

```csharp
// 1. Define your context
public class MyContext : IFlowContext
{
    public IServiceProvider Services { get; }
    public MyContext(IServiceProvider sp) => Services = sp;
}

// 2. Implement steps
public class Step1 : IFlowStep<MyContext> { /* ... */ }
public class Step2 : IFlowStep<MyContext> { /* ... */ }

// 3. Build and execute (using KhaosCode.Flow implementation)
var flow = builder
    .AddStep(new Step1())
        .OnOutcome(FlowOutcome.Success, "Step2")
        .OnOutcome(FlowOutcome.Failure, "ErrorHandler")
    .AddStep(new Step2())
    .Build();

var outcome = await executor.ExecuteAsync(flow, context, ct);
```

## Best Practices

1. **Keep steps focused** – Each step should do one thing well.
2. **Use meaningful outcome names** – Custom outcomes should clearly indicate the condition.
3. **Inject dependencies** – Use `context.Services` to resolve services in steps.
4. **Handle cancellation** – Always respect the `CancellationToken`.

## Related Packages

- **KhaosCode.Flow** – Default implementation of flow execution.
- **KhaosCode.Pipeline.Abstractions** – For linear data transformation pipelines.

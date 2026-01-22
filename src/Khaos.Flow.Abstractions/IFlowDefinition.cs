namespace Khaos.Flow.Abstractions;

/// <summary>
/// Immutable description of a flow with its steps and transitions.
/// </summary>
/// <typeparam name="TContext">The flow context type.</typeparam>
public interface IFlowDefinition<TContext> where TContext : IFlowContext
{
    /// <summary>
    /// Gets the name of the flow.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the key of the starting step.
    /// </summary>
    string StartStepKey { get; }

    /// <summary>
    /// Gets all step definitions keyed by step key.
    /// </summary>
    IReadOnlyDictionary<string, IFlowStepDefinition> Steps { get; }
}

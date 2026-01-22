namespace Khaos.Flow.Abstractions;

/// <summary>
/// Describes a step within a flow, including its type and transitions.
/// </summary>
public interface IFlowStepDefinition
{
    /// <summary>
    /// Gets the type of the step implementation.
    /// </summary>
    Type StepType { get; }

    /// <summary>
    /// Gets the mapping from outcomes to the next step keys.
    /// </summary>
    IReadOnlyDictionary<FlowOutcome, string> Transitions { get; }
}

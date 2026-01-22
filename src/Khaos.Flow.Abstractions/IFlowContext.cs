namespace Khaos.Flow.Abstractions;

/// <summary>
/// Marker interface for flow execution contexts.
/// A flow context provides shared state for all steps in a flow.
/// </summary>
public interface IFlowContext
{
    /// <summary>
    /// Gets the service provider for resolving dependencies.
    /// </summary>
    IServiceProvider Services { get; }
}

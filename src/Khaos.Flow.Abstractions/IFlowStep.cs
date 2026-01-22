namespace Khaos.Flow.Abstractions;

/// <summary>
/// Represents a unit of work within a flow.
/// Each step executes and returns an outcome that determines the next step.
/// </summary>
/// <typeparam name="TContext">The flow context type.</typeparam>
public interface IFlowStep<in TContext> where TContext : IFlowContext
{
    /// <summary>
    /// Executes the step asynchronously.
    /// </summary>
    /// <param name="context">The flow context containing shared state.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The outcome of this step, used to determine the next transition.</returns>
    Task<FlowOutcome> ExecuteAsync(TContext context, CancellationToken cancellationToken);
}

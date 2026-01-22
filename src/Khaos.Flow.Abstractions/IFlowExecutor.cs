namespace Khaos.Flow.Abstractions;

/// <summary>
/// Executes flow definitions using their step definitions and transitions.
/// </summary>
/// <typeparam name="TContext">The flow context type.</typeparam>
public interface IFlowExecutor<TContext> where TContext : IFlowContext
{
    /// <summary>
    /// Executes a flow with the given context.
    /// </summary>
    /// <param name="flow">The flow definition to execute.</param>
    /// <param name="context">The context to pass to each step.</param>
    /// <param name="cancellationToken">A token to cancel execution.</param>
    /// <returns>The outcome of the last executed step.</returns>
    Task<FlowOutcome> ExecuteAsync(
        IFlowDefinition<TContext> flow,
        TContext context,
        CancellationToken cancellationToken);
}

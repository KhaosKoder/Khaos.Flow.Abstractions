namespace Khaos.Flow.Abstractions;

/// <summary>
/// Builds flow definitions using a fluent API.
/// </summary>
/// <typeparam name="TContext">The flow context type.</typeparam>
public interface IFlowBuilder<TContext> where TContext : IFlowContext
{
    /// <summary>
    /// Sets the starting step for the flow.
    /// </summary>
    /// <typeparam name="TStep">The step type.</typeparam>
    /// <returns>A step builder for further configuration.</returns>
    IFlowStepBuilder<TContext> BeginWith<TStep>() where TStep : class, IFlowStep<TContext>;

    /// <summary>
    /// Builds the flow definition.
    /// </summary>
    /// <returns>The completed flow definition.</returns>
    IFlowDefinition<TContext> Build();
}

/// <summary>
/// Configures transitions for a flow step.
/// </summary>
/// <typeparam name="TContext">The flow context type.</typeparam>
public interface IFlowStepBuilder<TContext> where TContext : IFlowContext
{
    /// <summary>
    /// Specifies the outcome to configure a transition for.
    /// </summary>
    /// <param name="outcome">The outcome to match.</param>
    /// <returns>The step builder for chaining.</returns>
    IFlowStepBuilder<TContext> On(FlowOutcome outcome);

    /// <summary>
    /// Adds a transition to the next step on success (default outcome).
    /// </summary>
    /// <typeparam name="TStep">The next step type.</typeparam>
    /// <returns>A step builder for the next step.</returns>
    IFlowStepBuilder<TContext> Then<TStep>() where TStep : class, IFlowStep<TContext>;

    /// <summary>
    /// Adds a transition to the specified step for the current outcome.
    /// </summary>
    /// <typeparam name="TStep">The target step type.</typeparam>
    /// <returns>A step builder for the current step.</returns>
    IFlowStepBuilder<TContext> GoTo<TStep>() where TStep : class, IFlowStep<TContext>;

    /// <summary>
    /// Ends the flow after this step.
    /// </summary>
    /// <returns>The flow builder for completion.</returns>
    IFlowBuilder<TContext> End();

    /// <summary>
    /// Completes the flow configuration.
    /// </summary>
    /// <returns>The completed flow definition.</returns>
    IFlowDefinition<TContext> Build();
}

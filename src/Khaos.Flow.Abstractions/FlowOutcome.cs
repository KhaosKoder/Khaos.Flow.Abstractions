namespace Khaos.Flow.Abstractions;

/// <summary>
/// Represents the outcome of executing a flow step.
/// Outcomes are used to determine transitions between steps.
/// </summary>
public readonly struct FlowOutcome : IEquatable<FlowOutcome>
{
    private static readonly StringComparer Comparer = StringComparer.Ordinal;

    /// <summary>
    /// Gets the name of this outcome.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the standard success outcome.
    /// </summary>
    public static FlowOutcome Success { get; } = new("Success");

    /// <summary>
    /// Gets the standard failure outcome.
    /// </summary>
    public static FlowOutcome Failure { get; } = new("Failure");

    /// <summary>
    /// Initializes a new outcome with the specified name.
    /// </summary>
    /// <param name="name">The outcome name.</param>
    /// <exception cref="ArgumentException">Thrown when name is null or whitespace.</exception>
    public FlowOutcome(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Outcome name cannot be null or whitespace.", nameof(name));
        }

        Name = name;
    }

    /// <summary>
    /// Creates a custom outcome with the specified name.
    /// </summary>
    /// <param name="name">The custom outcome name.</param>
    /// <returns>A new <see cref="FlowOutcome"/> instance.</returns>
    public static FlowOutcome Custom(string name) => new(name);

    /// <inheritdoc />
    public bool Equals(FlowOutcome other) => Comparer.Equals(Name, other.Name);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is FlowOutcome other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => Name is not null ? Comparer.GetHashCode(Name) : 0;

    /// <summary>
    /// Determines whether two outcomes are equal.
    /// </summary>
    public static bool operator ==(FlowOutcome left, FlowOutcome right) => left.Equals(right);

    /// <summary>
    /// Determines whether two outcomes are not equal.
    /// </summary>
    public static bool operator !=(FlowOutcome left, FlowOutcome right) => !left.Equals(right);

    /// <inheritdoc />
    public override string ToString() => Name ?? string.Empty;
}

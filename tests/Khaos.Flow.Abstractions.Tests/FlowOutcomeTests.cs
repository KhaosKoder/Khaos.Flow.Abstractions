using Khaos.Flow.Abstractions;

namespace Khaos.Flow.Abstractions.Tests;

public class FlowOutcomeTests
{
    [Fact]
    public void Success_ReturnsSuccessOutcome()
    {
        var outcome = FlowOutcome.Success;
        Assert.Equal("Success", outcome.Name);
    }

    [Fact]
    public void Failure_ReturnsFailureOutcome()
    {
        var outcome = FlowOutcome.Failure;
        Assert.Equal("Failure", outcome.Name);
    }

    [Fact]
    public void Custom_CreatesOutcomeWithGivenName()
    {
        var outcome = FlowOutcome.Custom("Retry");
        Assert.Equal("Retry", outcome.Name);
    }

    [Fact]
    public void Constructor_ThrowsWhenNameIsNull()
    {
        Assert.Throws<ArgumentException>(() => new FlowOutcome(null!));
    }

    [Fact]
    public void Constructor_ThrowsWhenNameIsWhitespace()
    {
        Assert.Throws<ArgumentException>(() => new FlowOutcome("   "));
    }

    [Fact]
    public void Equals_ReturnsTrueForSameNames()
    {
        var outcome1 = new FlowOutcome("Test");
        var outcome2 = new FlowOutcome("Test");
        Assert.True(outcome1.Equals(outcome2));
        Assert.True(outcome1 == outcome2);
    }

    [Fact]
    public void Equals_ReturnsFalseForDifferentNames()
    {
        var outcome1 = new FlowOutcome("Test1");
        var outcome2 = new FlowOutcome("Test2");
        Assert.False(outcome1.Equals(outcome2));
        Assert.True(outcome1 != outcome2);
    }

    [Fact]
    public void GetHashCode_ReturnsSameHashForEqualOutcomes()
    {
        var outcome1 = new FlowOutcome("Test");
        var outcome2 = new FlowOutcome("Test");
        Assert.Equal(outcome1.GetHashCode(), outcome2.GetHashCode());
    }

    [Fact]
    public void ToString_ReturnsName()
    {
        var outcome = new FlowOutcome("MyOutcome");
        Assert.Equal("MyOutcome", outcome.ToString());
    }

    [Fact]
    public void Equals_WithObject_ReturnsTrueForEqualOutcome()
    {
        var outcome1 = new FlowOutcome("Test");
        object outcome2 = new FlowOutcome("Test");
        Assert.True(outcome1.Equals(outcome2));
    }

    [Fact]
    public void Equals_WithObject_ReturnsFalseForNonOutcome()
    {
        var outcome = new FlowOutcome("Test");
        Assert.False(outcome.Equals("Test"));
    }
}

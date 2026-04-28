using NSubstitute;
using FluentAssertions;
using HWGA.ReadmeUpdater.Abstractions;
using HWGA.ReadmeUpdater.Strategies;

namespace HWGA.ReadmeUpdater.Tests.Unit.Strategies;

public class CompositeStrategyTests
{
    private readonly ITaskResolutionStrategy _trueMock = Substitute.For<ITaskResolutionStrategy>();
    private readonly ITaskResolutionStrategy _falseMock = Substitute.For<ITaskResolutionStrategy>();
    private const string Dir = "any";
    private const string Theme = "theme";
    private const string Task = "task";

    public CompositeStrategyTests()
    {
        _trueMock.IsResolved(Dir, Theme, Task).Returns(true);
        _falseMock.IsResolved(Dir, Theme, Task).Returns(false);
    }

    [Fact]
    public void AllRequirements_ShouldReturnTrue_OnlyIfAllAreTrue()
    {
        // Arrange
        var sut = new AllRequirementsStrategy(_trueMock, _trueMock);

        // Act & Assert
        sut.IsResolved(Dir, Theme, Task).Should().BeTrue();
    }

    [Fact]
    public void AllRequirements_ShouldReturnFalse_IfAtLeastOneIsFalse()
    {
        // Arrange
        var sut = new AllRequirementsStrategy(_trueMock, _falseMock);

        // Act & Assert
        sut.IsResolved(Dir, Theme, Task).Should().BeFalse();
    }

    [Fact]
    public void AnyRequirement_ShouldReturnTrue_IfAtLeastOneIsTrue()
    {
        // Arrange
        var sut = new AnyRequirementStrategy(_falseMock, _trueMock);

        // Act & Assert
        sut.IsResolved(Dir, Theme, Task).Should().BeTrue();
    }

    [Fact]
    public void AnyRequirement_ShouldReturnFalse_IfAllAreFalse()
    {
        // Arrange
        var sut = new AnyRequirementStrategy(_falseMock, _falseMock);

        // Act & Assert
        sut.IsResolved(Dir, Theme, Task).Should().BeFalse();
    }
}

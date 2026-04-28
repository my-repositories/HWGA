using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests;

public class HelloWorldStringTests
{
    [Fact]
    public void Constructor_ShouldSetInitialValue()
    {
        // Arrange
        const string expected = "Initial Value";

        // Act
        var model = new HelloWorldString(expected);

        // Assert
        model.S.Should().Be(expected);
    }

    [Fact]
    public void PropertyS_ShouldBeSettable()
    {
        // Arrange
        var model = new HelloWorldString("Old");
        const string newValue = "New";

        // Act
        model.S = newValue;

        // Assert
        model.S.Should().Be(newValue);
    }

    [Theory]
    [InlineData("Data")]
    [InlineData("")]
    [InlineData(null)]
    public void GetHelloWorldString_ShouldReturnCurrentValue(string? input)
    {
        // Arrange
        var model = new HelloWorldString(input);

        // Act & Assert
        model.GetHelloWorldString().Should().Be(input);
    }
}

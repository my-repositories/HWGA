using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests;

public class StringFactoryTests
{
    [Fact]
    public void Instance_ShouldBeSingleton()
    {
        // Act & Assert
        StringFactory.Instance.Should().BeSameAs(StringFactory.Instance);
    }

    [Theory]
    [InlineData("Hello")]
    [InlineData("")]
    [InlineData(null)]
    public void CreateHelloWorldString_ShouldReturnCorrectObject(string? input)
    {
        // Act
        var result = StringFactory.Instance.CreateHelloWorldString(input!);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<HelloWorldString>();
        result.GetHelloWorldString().Should().Be(input);
    }
}

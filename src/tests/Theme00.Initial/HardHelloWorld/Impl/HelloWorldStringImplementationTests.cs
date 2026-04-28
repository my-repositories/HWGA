using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests;

public class HelloWorldStringImplementationTests
{
    [Fact]
    public void GetHelloWorldString_ShouldReturnCorrectContent_FromFactory()
    {
        // Arrange
        var sut = new HelloWorldStringImplementation();
        const string expectedMessage = "Hello, World!";

        // Act
        var result = sut.GetHelloWorldString();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<HelloWorldString>();
        result.GetHelloWorldString().Should().Be(expectedMessage);
    }

    [Fact]
    public void GetHelloWorldString_ShouldReturnNewInstance_EachTime()
    {
        // Arrange
        var sut = new HelloWorldStringImplementation();

        // Act
        var first = sut.GetHelloWorldString();
        var second = sut.GetHelloWorldString();

        // Assert
        first.Should().NotBeSameAs(second);
    }
}

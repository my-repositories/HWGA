using NSubstitute;
using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;
using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests;

public class HelloWorldImplementationTests
{
    private readonly HelloWorldImplementation _sut = new();

    [Fact]
    public void GetHelloWorld_ShouldReturnValidImplementation()
    {
        // Act
        var result = _sut.GetHelloWorld();

        // Assert
        result.Should().NotBeNull()
              .And.BeOfType<HelloWorldStringImplementation>();
    }

    [Fact]
    public void GetPrintStrategy_ShouldReturnStrategyFromFactory()
    {
        // Act
        var result = _sut.GetPrintStrategy();

        // Assert
        result.Should().NotBeNull()
              .And.BeAssignableTo<IPrintStrategy>();
        
        // Assert
        result.Should().BeOfType<PrintStrategyImplementation>();
    }

    [Fact]
    public void Print_ShouldDelegateExecutionToStrategy()
    {
        // Arrange
        var mockStrategy = Substitute.For<IPrintStrategy>();
        var mockString = Substitute.For<IHelloWorldString>();
        var expectedStatus = new StatusCodeImplementation(42);

        mockStrategy.Print(mockString).Returns(expectedStatus);

        // Act
        var result = _sut.Print(mockStrategy, mockString);

        // Assert
        result.Should().Be(expectedStatus);
        mockStrategy.Received(1).Print(mockString);
    }

    [Fact]
    public void FullFlow_Integration_ShouldWorkWithoutExceptions()
    {
        // Act & Assert
        var action = () => {
            var strategy = _sut.GetPrintStrategy();
            var str = _sut.GetHelloWorld();
            _sut.Print(strategy, str);
        };

        action.Should().NotThrow();
    }
}

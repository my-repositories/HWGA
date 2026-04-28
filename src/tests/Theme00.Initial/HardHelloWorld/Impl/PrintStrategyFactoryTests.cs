using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests;

public class PrintStrategyFactoryTests
{
    [Fact]
    public void Instance_ShouldBeSingleton()
    {
        // Act & Assert
        PrintStrategyFactory.Instance.Should().BeSameAs(PrintStrategyFactory.Instance);
    }

    [Fact]
    public void CreateIPrintStrategy_ShouldReturnStrategy_WhenEverythingIsOk()
    {
        // Act
        var strategy = PrintStrategyFactory.Instance.CreateIPrintStrategy();

        // Assert
        strategy.Should().NotBeNull()
                .And.BeOfType<PrintStrategyImplementation>();
    }

    [Fact]
    public void CreateIPrintStrategy_ShouldSuccessfullyInitializeStandardOutput()
    {
        // Act
        var strategy = PrintStrategyFactory.Instance.CreateIPrintStrategy();
        var status = strategy.SetupPrinting();
        
        // Assert
        status.StatusCode.Should().Be(0);
    }
}

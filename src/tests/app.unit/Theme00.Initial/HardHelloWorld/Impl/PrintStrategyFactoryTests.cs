using FluentAssertions;
using HWGA.Tests;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;
using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;
using NSubstitute;

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

    [Fact]
    public void CreateIPrintStrategy_ShouldThrow_WhenSetupReturnsError()
    {
        // Arrange
        var sut = PrintStrategyFactory.Instance;
        var mockStrategy = Substitute.For<IPrintStrategy>();
        
        // Заставляем SetupPrinting вернуть -1 (или любой не 0)
        mockStrategy.SetupPrinting().Returns(new StatusCodeImplementation(-1));

        // Act
        // Передаем наш мок в качестве перегрузки
        var action = () => sut.CreateIPrintStrategy(null, mockStrategy);

        // Assert
        // ВОТ ОНО! Теперь мы точно попадем в throw
        action.Should().Throw<Exception>().WithMessage("Failed to initialize strategy");
    }

}

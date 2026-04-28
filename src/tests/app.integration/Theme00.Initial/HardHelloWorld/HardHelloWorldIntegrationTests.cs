using System.Text;
using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;
using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests.Integration;

public class HardHelloWorldIntegrationTests
{
    [Fact]
    public void FullSystem_FromFactoryToOutput_ShouldWorkCorrectly()
    {
        // 1. Arrange
        var factory = HelloWorldFactory.Instance;
        var helloWorld = factory.CreateHelloWorld();
        using var outputStream = new StringWriter();
        var strategy = new PrintStrategyImplementation(outputStream);
        
        // 2. Act
        var setupStatus = strategy.SetupPrinting();
        var messageObj = helloWorld.GetHelloWorld();
        var printStatus = helloWorld.Print(strategy, messageObj);
        var resultText = outputStream.ToString();

        // 3. Assert
        setupStatus.StatusCode.Should().Be(0);
        printStatus.StatusCode.Should().Be(0);
        resultText.Should().Be($"Hello, World!{Environment.NewLine}");
    }

    [Fact]
    public void System_ShouldReturnValidComponents_WhenUsingDefaultFactories()
    {
        // Arrange
        var helloWorld = HelloWorldFactory.Instance.CreateHelloWorld();
        
        // Act
        var strategy = helloWorld.GetPrintStrategy();
        var content = helloWorld.GetHelloWorld();

        // Assert
        strategy.Should().NotBeNull()
                .And.BeOfType<PrintStrategyImplementation>();
                
        content.Should().NotBeNull()
               .And.BeOfType<HelloWorldStringImplementation>();
    }
}

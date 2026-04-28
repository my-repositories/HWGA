using HWGA.Core;
using FluentAssertions;
using NSubstitute;
using System.Text.RegularExpressions;

namespace HWGA.Tests.Integration;

public class RunnerIntegrationTests
{
    [Fact]
    public async Task FullSystem_FromDiscoveryToExecution_ShouldWorkCorrectly()
    {
        // 1. Arrange
        var provider = new AssemblyTypeProvider();
        using var sw = new StringWriter();
        
        var types = provider.GetProgramTypes();
        var targetIndex = types.ToList().FindIndex(t => t.Name.Contains("HelloWorld")) + 1;
        
        if (targetIndex <= 0)
        {
            targetIndex = 1;
        }

        using var reader = new StringReader($"{targetIndex}{Environment.NewLine}exit{Environment.NewLine}");
        
        var app = new App(provider, sw, reader, ["exit"]);
        var programRunner = new MainProgram();

        // 2. Act
        await programRunner.Run(app, sw, ["exit"]);

        // 3. Assert
        var result = sw.ToString();

        result.Should().Contain("Enter a number of program:");
        result.Should().Contain("Run HWGA.Theme00.Initial");
        result.Should().Contain("Hello, World!");
        result.Should().Contain("Terminate");
        result.Should().Contain("Goodbye!");
    }

    [Fact]
    public async Task FullSystem_ShouldHandleInvalidInputSequence_AndStillExitCleanly()
    {
        // Arrange
        var provider = new AssemblyTypeProvider();
        using var sw = new StringWriter();
        using var reader = new StringReader($"abc{Environment.NewLine}999{Environment.NewLine}exit{Environment.NewLine}");
        
        var app = new App(provider, sw, reader, ["exit"]);
        var sut = new MainProgram();

        // Act
        await sut.Run(app, sw, ["exit"]);

        // Assert
        var result = sw.ToString();
        var menuCount = Regex.Matches(result, "Enter a number of program:").Count;
        
        result.Should().Contain("Incorrect id!"); 
        result.Should().Contain("Goodbye!");
        menuCount.Should().BeInRange(2, 4);
    }

    public class ExplodingProgram(TextWriter output) : BaseProgram(output)
    {
        protected override Task Run(string[]? args) => throw new Exception("KABOOM!");
    }

    [Fact]
    public async Task FullSystem_ShouldRecover_WhenProgramThrowsException()
    {
        // Arrange
        var provider = Substitute.For<ITypeProvider>();
        provider.GetProgramTypes().Returns(new List<Type> { typeof(ExplodingProgram) });
        
        using var sw = new StringWriter();
        using var reader = new StringReader($"1{Environment.NewLine}exit{Environment.NewLine}");
        
        var app = new App(provider, sw, reader, ["exit"]);
        var sut = new MainProgram();

        // Act
        await sut.Run(app, sw, ["exit"]);

        // Assert
        var result = sw.ToString();
        result.Should().Contain("ERROR with");
        result.Should().Contain("KABOOM!");
        result.Should().Contain("Goodbye!");
    }
}

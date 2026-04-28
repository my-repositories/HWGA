using HWGA.Core;
using FluentAssertions;
using System.Text;

namespace HWGA.Tests.Unit;

public class BaseProgramTests
{
    private readonly StringBuilder _outputBuffer;
    private readonly TextWriter _writer;

    public BaseProgramTests()
    {
        _outputBuffer = new StringBuilder();
        _writer = new StringWriter(_outputBuffer);
    }

    private class SuccessProgram(TextWriter output) : BaseProgram(output)
    {
        protected override Task Run(string[]? args) => Task.CompletedTask;
    }

    private class FailingProgram(TextWriter output) : BaseProgram(output)
    {
        protected override Task Run(string[]? args) => throw new InvalidOperationException("Boom!");
    }

    private class AsyncProgram(TextWriter output) : BaseProgram(output)
    {
        protected override async Task Run(string[]? args)
        {
            await Task.Delay(100);
            await Output.WriteLineAsync("WorkInProgress");
        }
    }

    [Fact]
    public async Task Start_ShouldAwaitRunMethod_BeforePrintingTerminate()
    {
        // Arrange
        var sut = new AsyncProgram(_writer);

        // Act
        await sut.Start();

        // Assert
        var result = _outputBuffer.ToString();
        var lines = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        lines.Should().ContainInOrder(
            lines.First(l => l.Contains("Run")),
            "WorkInProgress",
            lines.First(l => l.Contains("Terminate"))
        );
    }

    [Fact]
    public async Task Start_ShouldPrintRunAndTerminate_OnSuccess()
    {
        // Arrange
        var sut = new SuccessProgram(_writer);

        // Act
        await sut.Start();

        // Assert
        var result = _outputBuffer.ToString();
        result.Should().Contain("Run HWGA.Tests.Unit.BaseProgramTests+SuccessProgram");
        result.Should().Contain("Terminate HWGA.Tests.Unit.BaseProgramTests+SuccessProgram");
    }

    [Fact]
    public async Task Start_ShouldHandleException_AndStillPrintTerminate()
    {
        // Arrange
        var sut = new FailingProgram(_writer);

        // Act
        await sut.Start();

        // Assert
        var result = _outputBuffer.ToString();
        result.Should().Contain("ERROR with HWGA.Tests.Unit.BaseProgramTests+FailingProgram: Boom!");
        result.Should().Contain("Terminate");
    }
}

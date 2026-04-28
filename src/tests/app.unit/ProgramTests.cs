using HWGA.Core;
using NSubstitute;


namespace HWGA.Tests.Unit;

public class ProgramTests
{
    [Fact]
    public async Task Run_ShouldExecuteProgram_AndThenExit_WhenExitCommandReceived()
    {
        // 1. Arrange
        var mockApp = Substitute.For<IApp>();
        var commands = new[] { "exit" };
        var sut = new MainProgram();
        using var sw = new StringWriter();

        mockApp.AskProgramName().Returns("1", "exit");

        // 2. Act
        await sut.Run(mockApp, sw, commands);

        // 3. Assert
        await mockApp.Received(1).StartProgram("1");
        mockApp.Received(2).AskProgramName();
    }

    [Fact]
    public async Task Run_ShouldExitImmediately_IfFirstCommandIsExit()
    {
        // Arrange
        var mockApp = Substitute.For<IApp>();
        var commands = new[] { "quit" };
        var sut = new MainProgram();
        using var sw = new StringWriter();

        mockApp.AskProgramName().Returns("quit");

        // Act
        await sut.Run(mockApp, sw, commands);

        // Assert
        await mockApp.DidNotReceive().StartProgram(Arg.Any<string>());
    }
}

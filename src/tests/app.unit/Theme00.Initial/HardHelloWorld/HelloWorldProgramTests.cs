using HWGA.Theme00.Initial.HardHelloWorld;
using FluentAssertions;

namespace HWGA.Tests.Unit.Theme00;

public class HelloWorldProgramTests
{
    [Fact]
    public async Task Run_ShouldPrintSuccessfully()
    {
        // Arrange
        using var writer = new StringWriter();
        var sut = new HelloWorldProgram(writer);

        // Act
        await sut.Start();

        // Assert
        writer.ToString().Should().Contain("Hello, World!");
    }

    [Fact]
    public async Task Run_ShouldThrowException_WhenPrintingFails()
    {
        // Arrange
        using var faultyWriter = new FaultyWriter(); 
        var sut = new HelloWorldProgram(faultyWriter);

        // Act & Assert
        var action = () => sut.Start();
        
        await action.Should().ThrowAsync<IOException>().WithMessage("Disk full");
    }
}

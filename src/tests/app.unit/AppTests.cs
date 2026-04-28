using HWGA.Core;
using NSubstitute;
using FluentAssertions;
using System.Text;

namespace HWGA.Tests.Unit;

public class AppTests
{
    private readonly ITypeProvider _typeProvider;
    private readonly StringBuilder _outputBuffer;
    private readonly TextWriter _writer;
    private readonly TextReader _reader;
    private readonly IProgram _mockProgram;

    public AppTests()
    {
        _typeProvider = Substitute.For<ITypeProvider>();
        _mockProgram = Substitute.For<IProgram>();
        _outputBuffer = new StringBuilder();
        _writer = new StringWriter(_outputBuffer);
        _reader = Substitute.For<TextReader>();
    }

    private App CreateSut() => new App(
        _typeProvider, 
        _writer, 
        _reader, 
        factory: t => _mockProgram);

    [Fact]
    public async Task StartProgram_ShouldRunProgram_WhenValidNumberProvided()
    {
        // Arrange
        _typeProvider.GetProgramTypes().Returns([typeof(object)]);
        var sut = CreateSut();

        // Act
        await sut.StartProgram("1");

        // Assert
        await _mockProgram.Received(1).Start();
    }

    [Theory]
    [InlineData("0")]
    [InlineData("2")]
    [InlineData("not-a-number")]
    public async Task StartProgram_ShouldPrintErrorMessage_WhenInputIsInvalid(string input)
    {
        // Arrange
        _typeProvider.GetProgramTypes().Returns([typeof(object)]);
        var sut = CreateSut();

        // Act
        await sut.StartProgram(input);

        // Assert
        await _mockProgram.DidNotReceive().Start();
        
        var output = _outputBuffer.ToString();
        output.Should().Contain("Incorrect id!");
    }

    [Fact]
    public async Task StartProgram_ShouldRunProgram_WhenFullNameProvided()
    {
        // Arrange
        var targetType = typeof(string);
        _typeProvider.GetProgramTypes().Returns([targetType]);
        var sut = CreateSut();

        // Act
        await sut.StartProgram(targetType.FullName!);

        // Assert
        await _mockProgram.Received(1).Start();
    }

    [Fact]
    public async Task AskProgramName_ShouldReturnLoweredTrimmedInput()
    {
        // Arrange
        using var inputReader = new StringReader("  ExIT  \n");
        var sut = new App(_typeProvider, _writer, inputReader);

        // Act
        var result = await sut.AskProgramName();

        // Assert
        result.Should().Be("exit");
        _outputBuffer.ToString().Should().Contain("Enter a number of program:");
    }
}

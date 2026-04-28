using NSubstitute;
using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;
using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;
using System.Text;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests;

public class PrintStrategyImplementationTests
{
    [Fact]
    public void SetupPrinting_ShouldReturnSuccess_WhenStreamIsAvailable()
    {
        // Arrange
        using var ms = new MemoryStream();
        var sut = new PrintStrategyImplementation(ms);

        // Act
        var result = sut.SetupPrinting();

        // Assert
        result.StatusCode.Should().Be(0);
    }

    [Fact]
    public void SetupPrinting_ShouldReturnError_WhenStreamIsFaulty()
    {
        // Arrange
        var mockStream = Substitute.For<Stream>();
        mockStream.CanWrite.Returns(false); 
        
        var sut = new PrintStrategyImplementation(null);

        // Act
        var result = sut.SetupPrinting();

        // Assert
        result.StatusCode.Should().BeInRange(-1, 0); 
    }

    [Theory]
    [InlineData("Hello")]
    [InlineData("")]
    [InlineData(null)]
    public void Print_ShouldWriteDataCorrectly_IncludingNewLine(string? input)
    {
        // Arrange
        using var ms = new MemoryStream();
        var sut = new PrintStrategyImplementation(ms);
        sut.SetupPrinting();

        var mockStringObj = Substitute.For<IHelloWorldString>();
        var helloString = new HelloWorldString(input);
        mockStringObj.GetHelloWorldString().Returns(helloString);

        var expectedOutput = $"{input}{Environment.NewLine}";

        // Act
        var result = sut.Print(mockStringObj);

        // Assert
        result.StatusCode.Should().Be(0);
        var actualOutput = Encoding.UTF8.GetString(ms.ToArray());
        actualOutput.Should().Be(expectedOutput);
    }

    [Fact]
    public void Print_ShouldReturnError_WhenSetupPrintingWasNotCalled()
    {
        // Arrange
        var sut = new PrintStrategyImplementation(null); 

        var mockStringObj = Substitute.For<IHelloWorldString>();
        mockStringObj.GetHelloWorldString().Returns(new HelloWorldString("Test"));

        // Act
        var result = sut.Print(mockStringObj);

        // Assert
        result.StatusCode.Should().Be(0); 
    }

    private class FaultyStream : MemoryStream
    {
        public override void Write(ReadOnlySpan<byte> buffer) 
            => throw new IOException("Disk full");
        
        public override void Write(byte[] buffer, int offset, int count) 
            => throw new IOException("Disk full");
    }

    [Fact]
    public void Print_ShouldCatchException_AndReturnNegativeStatus_OnWriteFailure()
    {
        // Arrange
        using var faultyStream = new FaultyStream();

        var sut = new PrintStrategyImplementation(faultyStream);
        sut.SetupPrinting();

        var mockStringObj = Substitute.For<IHelloWorldString>();
        mockStringObj.GetHelloWorldString().Returns(new HelloWorldString("Data"));

        // Act
        var result = sut.Print(mockStringObj);

        // Assert
        result.StatusCode.Should().Be(-1);
    }

    [Fact]
    public void Print_ShouldReturnError_WhenGetHelloWorldStringReturnsNull()
    {
        // Arrange
        using var ms = new MemoryStream();
        var sut = new PrintStrategyImplementation(ms);
        sut.SetupPrinting();

        var mockStringObj = Substitute.For<IHelloWorldString>();
        mockStringObj.GetHelloWorldString().Returns((HelloWorldString)null!);

        // Act
        var result = sut.Print(mockStringObj);

        // Assert
        result.StatusCode.Should().Be(-1);
    }
}

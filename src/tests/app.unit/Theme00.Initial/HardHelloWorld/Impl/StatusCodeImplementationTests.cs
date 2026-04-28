using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests;

public class StatusCodeImplementationTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(100)]
    public void StatusCode_ShouldStoreValueCorrectly(int expectedCode)
    {
        // Act
        var sut = new StatusCodeImplementation(expectedCode);

        // Assert
        sut.StatusCode.Should().Be(expectedCode);
    }

    [Fact]
    public void Records_WithSameCode_ShouldBeEqual()
    {
        // Arrange
        var first = new StatusCodeImplementation(0);
        var second = new StatusCodeImplementation(0);

        // Act & Assert
        first.Should().Be(second);
        (first == second).Should().BeTrue();
    }

    [Fact]
    public void Records_WithDifferentCode_ShouldNotBeEqual()
    {
        // Arrange
        var first = new StatusCodeImplementation(0);
        var second = new StatusCodeImplementation(-1);

        // Act & Assert
        first.Should().NotBe(second);
    }
}

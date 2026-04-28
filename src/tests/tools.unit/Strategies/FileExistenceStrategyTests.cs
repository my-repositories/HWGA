using FluentAssertions;
using HWGA.ReadmeUpdater.Strategies;

namespace HWGA.ReadmeUpdater.Tests.Unit.Strategies;

public class FileExistenceStrategyTests : IDisposable
{
    private readonly string _tempDir;

    public FileExistenceStrategyTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(_tempDir);
    }

    [Theory]
    [InlineData("cs")]
    [InlineData(".cs")]
    public void IsResolved_ShouldReturnTrue_WhenFileExists(string ext)
    {
        // Arrange
        var sut = new FileExistenceStrategy(ext);
        File.WriteAllText(Path.Combine(_tempDir, "Solution.cs"), "content");

        // Act
        var result = sut.IsResolved(_tempDir, "Theme", "Task");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsResolved_ShouldSearchRecursively()
    {
        // Arrange
        var sut = new FileExistenceStrategy("cs");
        var subDir = Path.Combine(_tempDir, "src", "Impl");
        Directory.CreateDirectory(subDir);
        File.WriteAllText(Path.Combine(subDir, "Service.cs"), "content");

        // Act
        var result = sut.IsResolved(_tempDir, "Theme", "Task");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsResolved_ShouldReturnFalse_WhenNoTargetFiles()
    {
        // Arrange
        var sut = new FileExistenceStrategy("cs");
        File.WriteAllText(Path.Combine(_tempDir, "Notes.txt"), "content");

        // Act
        var result = sut.IsResolved(_tempDir, "Theme", "Task");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsResolved_ShouldHandleMissingDirectory()
    {
        // Arrange
        var sut = new FileExistenceStrategy("cs");
        var fakePath = Path.Combine(_tempDir, "missing");

        // Act
        var result = sut.IsResolved(fakePath, "Theme", "Task");

        // Assert
        result.Should().BeFalse();
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, true);
    }
}

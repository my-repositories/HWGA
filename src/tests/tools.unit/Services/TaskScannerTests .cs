using HWGA.ReadmeUpdater.Services;
using HWGA.ReadmeUpdater.Abstractions;
using NSubstitute;
using FluentAssertions;

namespace HWGA.ReadmeUpdater.Tests.Unit.Services;

public class TaskScannerTests : IDisposable
{
    private readonly string _tempRoot;
    private readonly ITaskResolutionStrategy _mockStrategy;
    private readonly string[] _levels = ["Easy", "Medium", "Hard"];

    public TaskScannerTests()
    {
        _tempRoot = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        _mockStrategy = Substitute.For<ITaskResolutionStrategy>();
        
        Directory.CreateDirectory(Path.Combine(_tempRoot, "src", "app", "Theme01.Basics", "Easy_Task1"));
        Directory.CreateDirectory(Path.Combine(_tempRoot, "src", "app", "Theme01.Basics", "Medium_Task1"));
    }

    [Fact]
    public void ScanThemes_ShouldFindAndGroupTasksCorrectly()
    {
        // Arrange
        _mockStrategy.IsResolved(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
        var sut = new TaskScanner(_mockStrategy, _levels);

        // Act
        var results = sut.ScanThemes(_tempRoot).ToList();

        // Assert
        results.Should().HaveCount(1);
        var theme = results[0];
        theme.Name.Should().Be("Theme01.Basics");
        theme.Stats["Easy"].Total.Should().Be(1);
        theme.Stats["Easy"].Done.Should().Be(1);
        theme.Stats["Medium"].Total.Should().Be(1);
        theme.Stats["Hard"].Total.Should().Be(0);
    }

    [Fact]
    public void ScanThemes_ShouldReturnEmpty_WhenAppDirectoryMissing()
    {
        // Arrange
        var emptyRoot = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(emptyRoot);
        var sut = new TaskScanner(_mockStrategy, _levels);

        // Act
        var results = sut.ScanThemes(emptyRoot);

        // Assert
        results.Should().BeEmpty();
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempRoot))
            Directory.Delete(_tempRoot, true);
    }
}

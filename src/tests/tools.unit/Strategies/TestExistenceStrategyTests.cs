using FluentAssertions;
using HWGA.ReadmeUpdater.Strategies;

namespace HWGA.ReadmeUpdater.Tests.Unit.Strategies;

public class TestExistenceStrategyTests : IDisposable
{
    private readonly string _tempTestsRoot;

    public TestExistenceStrategyTests()
    {
        _tempTestsRoot = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(_tempTestsRoot);
    }

    [Fact]
    public void IsResolved_ShouldReturnTrue_WhenTestFileExistsInMirrorDirectory()
    {
        // Arrange
        var theme = "Theme01.Basics";
        var task = "Easy01_Sum";
        var sut = new TestExistenceStrategy(_tempTestsRoot);

        var targetDir = Path.Combine(_tempTestsRoot, theme, task);
        Directory.CreateDirectory(targetDir);
        File.WriteAllText(Path.Combine(targetDir, "SomeTests.cs"), "content");

        // Act
        var result = sut.IsResolved("any/task/dir", theme, task);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsResolved_ShouldReturnFalse_WhenDirectoryExistsButIsEmpty()
    {
        // Arrange
        var theme = "Theme01";
        var task = "Task01";
        var sut = new TestExistenceStrategy(_tempTestsRoot);
        
        Directory.CreateDirectory(Path.Combine(_tempTestsRoot, theme, task));

        // Act
        var result = sut.IsResolved("any", theme, task);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsResolved_ShouldReturnFalse_WhenDirectoryDoesNotExist()
    {
        // Arrange
        var sut = new TestExistenceStrategy(_tempTestsRoot);

        // Act
        var result = sut.IsResolved("any", "NonExistentTheme", "AnyTask");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsResolved_ShouldSearchRecursivelyInTestTaskFolder()
    {
        // Arrange
        var theme = "Theme01";
        var task = "Task01";
        var sut = new TestExistenceStrategy(_tempTestsRoot);
        
        var deepDir = Path.Combine(_tempTestsRoot, theme, task, "Integration", "Mocks");
        Directory.CreateDirectory(deepDir);
        File.WriteAllText(Path.Combine(deepDir, "MyTest.cs"), "content");

        // Act
        var result = sut.IsResolved("any", theme, task);

        // Assert
        result.Should().BeTrue();
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempTestsRoot)) 
            Directory.Delete(_tempTestsRoot, true);
    }
}

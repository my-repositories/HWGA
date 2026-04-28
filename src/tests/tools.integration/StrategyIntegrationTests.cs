using HWGA.ReadmeUpdater.Services;
using HWGA.ReadmeUpdater.Strategies;
using FluentAssertions;

namespace HWGA.ReadmeUpdater.Tests.Integration.Strategies;

public class StrategyIntegrationTests
{
    [Fact]
    public void TestExistenceStrategy_ShouldFindActualTests_OnDisk()
    {
        // Arrange
        var root = FileService.FindProjectRoot("*.slnx");
        var unitTestsPath = Path.Combine(root, "src", "tests", "app.unit");
        var sut = new TestExistenceStrategy(unitTestsPath);

        const string theme = "Theme00.Initial";
        const string task = "HardHelloWorld";
        
        var taskDir = Path.Combine(root, "src", "app", theme, task);

        // Act
        var result = sut.IsResolved(taskDir, theme, task);

        // Assert
        result.Should().BeTrue($"тесты для {task} должны находиться по пути {unitTestsPath}/{theme}/{task}");
    }

    [Fact]
    public void FileExistenceStrategy_ShouldFindActualSourceCode_OnDisk()
    {
        // Arrange
        var root = FileService.FindProjectRoot("*.slnx");
        var sut = new FileExistenceStrategy("cs");

        const string theme = "Theme00.Initial";
        const string task = "HardHelloWorld";
        var taskDir = Path.Combine(root, "src", "app", theme, task);

        // Act
        var result = sut.IsResolved(taskDir, theme, task);

        // Assert
        result.Should().BeTrue($"исходный код (.cs) для {task} должен быть в {taskDir}");
    }
}

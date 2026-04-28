using HWGA.ReadmeUpdater.Services;
using HWGA.ReadmeUpdater.Strategies;
using FluentAssertions;

namespace HWGA.ReadmeUpdater.Tests.Integration.Services;

public class ScannerIntegrationTests
{
    [Fact]
    public void Scanner_ShouldIdentifyRealProjectStructure()
    {
        // Arrange
        var root = FileService.FindProjectRoot("*.slnx");
        var levels = new[] { "Easy", "Medium", "Hard" };
        
        var unitTestsPath = Path.Combine(root, "src", "tests", "app.unit");

        var strategy = new AllRequirementsStrategy(
            new FileExistenceStrategy("cs"),
            new TestExistenceStrategy(unitTestsPath)
        );
        
        var scanner = new TaskScanner(strategy, levels);

        // Act
        var results = scanner.ScanThemes(root).ToList();

        // Assert
        results.Should().NotBeEmpty("папка src/app должна содержать хотя бы одну тему");
        
        var initialTheme = results.FirstOrDefault(t => t.Name == "Theme00.Initial");
        initialTheme.Should().NotBeNull("тема Theme00.Initial должна быть обнаружена в src/app");

        initialTheme!.Stats["Hard"].Total.Should().BeGreaterThanOrEqualTo(1);
    }
}

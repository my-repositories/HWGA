using HWGA.ReadmeUpdater.Services;
using FluentAssertions;

namespace HWGA.ReadmeUpdater.Tests.Integration.Services;

public class FileServiceIntegrationTests : IDisposable
{
    private readonly string _tempReadme;
    private readonly string _originalReadmePath;

    public FileServiceIntegrationTests()
    {
        var root = FileService.FindProjectRoot("*.slnx");
        _originalReadmePath = Path.Combine(root, "README.md");
        _tempReadme = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}_README.md");
        
        if (File.Exists(_originalReadmePath))
        {
            File.Copy(_originalReadmePath, _tempReadme, true);
        }
    }

    [Fact]
    public void UpdateReadmeTable_ShouldWorkOnRealFileStructure_WithoutCorruption()
    {
        // Arrange
        var sut = new FileService();
        const string testTable = "| Интеграционный | Тест | Пройден |\n| --- | --- | --- |";

        // Act
        sut.UpdateReadmeTable(_tempReadme, testTable);

        // Assert
        var result = File.ReadAllText(_tempReadme);
        
        result.Should().Contain("<!-- PROGRESS_TABLE_START -->");
        result.Should().Contain(testTable);
        result.Should().Contain("<!-- PROGRESS_TABLE_END -->");
        result.Length.Should().BeGreaterThan(testTable.Length);
    }

    public void Dispose()
    {
        if (File.Exists(_tempReadme))
        {
            File.Delete(_tempReadme);
        }
    }
}

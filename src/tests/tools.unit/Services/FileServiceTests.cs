using FluentAssertions;
using HWGA.ReadmeUpdater.Services;

namespace HWGA.ReadmeUpdater.Tests.Unit.Services;

public class FileServiceTests : IDisposable
{
    private readonly string _tempFile;
    private readonly FileService _sut;

    public FileServiceTests()
    {
        _sut = new FileService();
        _tempFile = Path.GetTempFileName();
    }

    [Fact]
    public void UpdateReadmeTable_ShouldReplaceContentBetweenMarkers()
    {
        // Arrange
        const string initial = "Intro\n<!-- PROGRESS_TABLE_START -->old<!-- PROGRESS_TABLE_END -->\nOutro";
        const string newTable = "| New | Table |";
        File.WriteAllText(_tempFile, initial);

        // Act
        _sut.UpdateReadmeTable(_tempFile, newTable);

        // Assert
        var result = File.ReadAllText(_tempFile);
        result.Should().Contain("Intro");
        result.Should().Contain("Outro");
        result.Should().Contain($"<!-- PROGRESS_TABLE_START -->\n{newTable}\n<!-- PROGRESS_TABLE_END -->");
        result.Should().NotContain("old");
    }

    [Fact]
    public void UpdateReadmeTable_ShouldDoNothing_WhenFileDoesNotExist()
    {
        // Act & Assert
        var action = () => _sut.UpdateReadmeTable("non_existent.md", "data");
        action.Should().NotThrow();
    }

    [Fact]
    public void UpdateReadmeTable_ShouldHandleEmptyContentBetweenMarkers()
    {
        // Arrange
        const string initial = "Start\n<!-- PROGRESS_TABLE_START --><!-- PROGRESS_TABLE_END -->\nEnd";
        const string newTable = "Content";
        File.WriteAllText(_tempFile, initial);

        // Act
        _sut.UpdateReadmeTable(_tempFile, newTable);

        // Assert
        var result = File.ReadAllText(_tempFile);
        result.Should().Contain($"<!-- PROGRESS_TABLE_START -->\n{newTable}\n<!-- PROGRESS_TABLE_END -->");
    }

    [Fact]
    public void FindProjectRoot_ShouldReturnDirectory_WhenSlnxExists()
    {
        // Act
        var root = FileService.FindProjectRoot("*.slnx");
        var srcPath = Path.Combine(root, "src");

        // Assert
        root.Should().NotBeNullOrEmpty();
        Directory.Exists(root).Should().BeTrue();
        Directory.GetFiles(srcPath, "*.slnx").Should().NotBeEmpty();
    }

    public void Dispose()
    {
        if (File.Exists(_tempFile)) File.Delete(_tempFile);
    }
}

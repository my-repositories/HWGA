using HWGA.ReadmeUpdater.Services;
using FluentAssertions;

namespace HWGA.ReadmeUpdater.Tests.Integration;

public class ProgramIntegrationTests : IDisposable
{
    private readonly string _tempRoot;
    private readonly string _readmePath;

    public ProgramIntegrationTests()
    {
        _tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        _readmePath = Path.Combine(_tempRoot, "README.md");

        var taskPath = Path.Combine(_tempRoot, "src", "app", "Theme01.CLR", "Easy_StackVsHeap");
        var testPath = Path.Combine(_tempRoot, "src", "tests", "app.unit", "Theme01.CLR", "Easy_StackVsHeap");
        
        Directory.CreateDirectory(taskPath);
        Directory.CreateDirectory(testPath);

        File.WriteAllText(Path.Combine(taskPath, "Solution.cs"), "public class Demo {}");
        File.WriteAllText(Path.Combine(testPath, "SolutionTests.cs"), "[Fact] public void Test() {}");
        
        File.WriteAllText(_readmePath, "Intro\n<!-- PROGRESS_TABLE_START --><!-- PROGRESS_TABLE_END -->\nOutro");
    }

    [Fact]
    public void Run_WithRealFileSystem_ShouldProduceCorrectMarkdown()
    {
        // Arrange
        var sut = new Program(
            new FileService(),
            (strategy, levels) => new TaskScanner(strategy, levels),
            new MarkdownGenerator()
        );

        string[] levels = ["Easy", "Medium", "Hard"];

        // Act
        sut.Run(_tempRoot, levels);

        // Assert
        var result = File.ReadAllText(_readmePath);

        result.Should().Contain("Theme01.CLR");
        result.Should().Contain("1/1");
        result.Should().Contain("✅");
        result.Should().Contain("Intro").And.Contain("Outro");
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempRoot)) 
            Directory.Delete(_tempRoot, true);
    }
}

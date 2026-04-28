using HWGA.ReadmeUpdater.Abstractions;
using HWGA.ReadmeUpdater.Models;
using NSubstitute;
using FluentAssertions;

namespace HWGA.ReadmeUpdater.Tests.Unit;

public class ProgramTests
{
    private readonly IFileService _fileService;
    private readonly IMarkdownGenerator _generator;
    private readonly ITaskScanner _scanner;
    private readonly Program _sut;

    public ProgramTests()
    {
        _fileService = Substitute.For<IFileService>();
        _generator = Substitute.For<IMarkdownGenerator>();
        _scanner = Substitute.For<ITaskScanner>();
        _sut = new Program
        (
            _fileService, 
            (strategy, levels) => _scanner, 
            _generator
        );
    }

    [Fact]
    public void Run_ShouldExecuteFullPipelineWithCorrectParameters()
    {
        // Arrange
        const string root = "/fake/root";
        string[] levels = ["Easy", "Hard"];
        var fakeThemes = new List<ThemeProgress> 
        { 
            new("Theme01", new Dictionary<string, DifficultyStats>()) 
        };

        _scanner.ScanThemes(root).Returns(fakeThemes);
        _generator.GenerateTable(fakeThemes, levels).Returns("| Fake | Table |");

        // Act
        _sut.Run(root, levels);

        // Assert
        _scanner.Received(1).ScanThemes(root);
        _generator.Received(1).GenerateTable(fakeThemes, levels);
        _fileService.Received(1).UpdateReadmeTable(
            Path.Combine(root, "README.md"), 
            "| Fake | Table |"
        );
    }

    [Fact]
    public void Run_ShouldBuildStrategiesInternaly()
    {
        var action = () => _sut.Run("/any", ["Easy"]);
        
        action.Should().NotThrow();
    }
}

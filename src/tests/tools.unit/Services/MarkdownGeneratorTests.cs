using FluentAssertions;
using HWGA.ReadmeUpdater.Models;
using HWGA.ReadmeUpdater.Services;

namespace HWGA.ReadmeUpdater.Tests.Unit.Services;

public class MarkdownGeneratorTests
{
    private readonly string[] _levels = ["Easy", "Medium", "Hard"];
    private readonly MarkdownGenerator _sut;

    public MarkdownGeneratorTests()
    {
        _sut = new MarkdownGenerator(_levels);
    }

    [Fact]
    public void GenerateTable_ShouldReturnCorrectHeaderAndSeparator()
    {
        // Arrange
        var themes = Enumerable.Empty<ThemeProgress>();

        // Act
        var result = _sut.GenerateTable(themes);

        // Assert
        var lines = result.Split(Environment.NewLine);
        lines[0].Should().Be("| Тема | Easy | Medium | Hard | Status |");
        lines[1].Should().Be("| :--- | :---: | :---: | :---: | :--- |");
    }

    [Fact]
    public void GenerateTable_ShouldRenderThemeRowCorrectly()
    {
        // Arrange
        var stats = new Dictionary<string, DifficultyStats>
        {
            { "Easy", new DifficultyStats(1, 1) },
            { "Medium", new DifficultyStats(0, 2) },
            { "Hard", new DifficultyStats(0, 0) }
        };
        var themes = new[] { new ThemeProgress("Theme01.Test", stats) };

        // Act
        var result = _sut.GenerateTable(themes);

        // Assert
        // Ожидаем: | Theme01.Test | 1/1 | 0/2 | 0/0 | 🟡 1/3 |
        result.Should().Contain("| Theme01.Test | 1/1 | 0/2 | 0/0 | 🟡 1/3 |");
    }

    [Fact]
    public void GenerateTable_ShouldShowCompletedIcon_WhenAllTasksAreDone()
    {
        // Arrange
        var stats = new Dictionary<string, DifficultyStats>
        {
            { "Easy", new DifficultyStats(1, 1) },
            { "Medium", new DifficultyStats(1, 1) },
            { "Hard", new DifficultyStats(1, 1) }
        };
        var themes = new[] { new ThemeProgress("Theme02.Done", stats) };

        // Act
        var result = _sut.GenerateTable(themes);

        // Assert
        result.Should().Contain("✅ 3/3");
    }

    [Fact]
    public void GenerateTable_ShouldHandleEmptyThemes()
    {
        // Act
        var result = _sut.GenerateTable(Enumerable.Empty<ThemeProgress>());

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Split(Environment.NewLine).Length.Should().Be(2); // Только хедер и сепаратор
    }
}

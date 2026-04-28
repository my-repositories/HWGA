using FluentAssertions;
using HWGA.ReadmeUpdater.Models;

namespace HWGA.ReadmeUpdater.Tests.Unit.Models;

public class ThemeProgressTests
{
    [Fact]
    public void Properties_ShouldCalculateCorrectTotals()
    {
        // Arrange
        var stats = new Dictionary<string, DifficultyStats>
        {
            { "Easy", new DifficultyStats(2, 2) },
            { "Medium", new DifficultyStats(1, 3) },
            { "Hard", new DifficultyStats(0, 1) }
        };
        var sut = new ThemeProgress("Theme01", stats);

        // Act & Assert
        sut.AllDone.Should().Be(3);
        sut.AllTotal.Should().Be(6);
        sut.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public void IsCompleted_ShouldBeTrue_WhenAllTasksAreDone()
    {
        // Arrange
        var stats = new Dictionary<string, DifficultyStats>
        {
            { "Easy", new DifficultyStats(1, 1) },
            { "Hard", new DifficultyStats(5, 5) }
        };
        var sut = new ThemeProgress("Theme02", stats);

        // Act & Assert
        sut.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public void IsCompleted_ShouldBeFalse_WhenTotalIsZero()
    {
        // Arrange
        var stats = new Dictionary<string, DifficultyStats>
        {
            { "Easy", new DifficultyStats(0, 0) }
        };
        var sut = new ThemeProgress("Empty", stats);

        // Act & Assert
        sut.IsCompleted.Should().BeFalse();
    }
}

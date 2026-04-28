using FluentAssertions;
using HWGA.ReadmeUpdater.Models;

namespace HWGA.ReadmeUpdater.Tests.Unit.Models;

public class DifficultyStatsTests
{
    [Fact]
    public void DifficultyStats_ShouldStoreValues()
    {
        // Act
        var stats = new DifficultyStats(5, 10);

        // Assert
        stats.Done.Should().Be(5);
        stats.Total.Should().Be(10);
    }

    [Fact]
    public void DifficultyStats_ValueEquality_ShouldWork()
    {
        // Arrange
        var stats1 = new DifficultyStats(1, 1);
        var stats2 = new DifficultyStats(1, 1);

        // Act & Assert
        stats1.Should().Be(stats2);
        (stats1 == stats2).Should().BeTrue();
    }
}

using System.Text.RegularExpressions;
using HWGA.ReadmeUpdater.Abstractions;
using HWGA.ReadmeUpdater.Models;

namespace HWGA.ReadmeUpdater.Services;

public class TaskScanner(ITaskResolutionStrategy strategy, string[] levels)
{
    private static readonly Regex ThemeRegex = new(@"Theme\d+\..+", RegexOptions.Compiled);

    public IEnumerable<ThemeProgress> ScanThemes(string rootPath)
    {
        var appPath = Path.Combine(rootPath, "src", "app");
        if (!Directory.Exists(appPath)) return [];

        return Directory.EnumerateDirectories(appPath)
            .Where(dir => ThemeRegex.IsMatch(Path.GetFileName(dir)))
            .Select(themePath => new ThemeProgress(
                Path.GetFileName(themePath),
                levels.ToDictionary(l => l, l => GetStats(themePath, l))));
    }

    private DifficultyStats GetStats(string themePath, string level)
    {
        var taskDirs = Directory.GetDirectories(themePath)
            .Where(d => Path.GetFileName(d).StartsWith(level, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return new DifficultyStats
        (
            taskDirs.Count(d => strategy.IsResolved(d, Path.GetFileName(d))), 
            taskDirs.Count
        );
    }
}

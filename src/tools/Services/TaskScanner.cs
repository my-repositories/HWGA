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
                levels.ToDictionary(l => l, l => GetStats(themePath, l))))
            .OrderBy(t => t.Name);
    }

    private DifficultyStats GetStats(string themePath, string level)
    {
        string themeName = Path.GetFileName(themePath);
        var taskDirs = Directory.GetDirectories(themePath)
            .Where(d => Path.GetFileName(d).StartsWith(level, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return new DifficultyStats
        (
            taskDirs.Count(d =>
            {
                var taskName = Path.GetFileName(d);
                return strategy.IsResolved(d, themeName, taskName);
            }), 
            taskDirs.Count
        );
    }
}

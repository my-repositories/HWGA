using HWGA.ReadmeUpdater.Models;

namespace HWGA.ReadmeUpdater.Abstractions;

public interface ITaskScanner
{
    IEnumerable<ThemeProgress> ScanThemes(string rootPath);
}
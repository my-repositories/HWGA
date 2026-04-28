using HWGA.ReadmeUpdater.Models;

namespace HWGA.ReadmeUpdater.Abstractions;

public interface IMarkdownGenerator
{
    string GenerateTable(IEnumerable<ThemeProgress> themes, string[] levels);
}
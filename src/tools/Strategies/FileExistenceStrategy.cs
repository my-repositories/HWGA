using HWGA.ReadmeUpdater.Abstractions;

namespace HWGA.ReadmeUpdater.Strategies;

public class FileExistenceStrategy(string extension) : ITaskResolutionStrategy
{
    private readonly string _searchPattern = extension.StartsWith('.') ? $"*{extension}" : $"*.{extension}";

    public bool IsResolved(string taskDirectory, string taskName) =>
        Directory.Exists(taskDirectory) && 
        Directory.EnumerateFiles(taskDirectory, _searchPattern, SearchOption.AllDirectories).Any();
}

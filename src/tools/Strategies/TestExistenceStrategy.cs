using HWGA.ReadmeUpdater.Abstractions;

namespace HWGA.ReadmeUpdater.Strategies;

public class TestExistenceStrategy(string testsPath) : ITaskResolutionStrategy
{
    public bool IsResolved(string taskDirectory, string themeName, string taskName)
    {
        var expectedPath = Path.Combine(testsPath, themeName, taskName);

        return Directory.Exists(expectedPath) && 
               Directory.EnumerateFiles(expectedPath, "*.cs", SearchOption.AllDirectories).Any();
    }
}

using HWGA.ReadmeUpdater.Abstractions;

namespace HWGA.ReadmeUpdater.Strategies;

public class TestExistenceStrategy(string testFolderRelativePath) : ITaskResolutionStrategy
{
    public bool IsResolved(string taskDirectory, string taskName)
    {
        var testPath = Path.Combine(taskDirectory, testFolderRelativePath);
        
        if (!Directory.Exists(testPath))
        {
            return false;
        }

        return Directory.EnumerateFiles
        (
            testPath, 
            $"*{taskName}*.cs", SearchOption.AllDirectories
        ).Any();
    }
}

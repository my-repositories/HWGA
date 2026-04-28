using HWGA.ReadmeUpdater.Abstractions;

namespace HWGA.ReadmeUpdater.Strategies;

public class AllRequirementsStrategy(params ITaskResolutionStrategy[] strategies) : ITaskResolutionStrategy
{
    public bool IsResolved(string taskDirectory, string taskName)
    {
        return strategies.All(s => s.IsResolved(taskDirectory, taskName));
    }
}

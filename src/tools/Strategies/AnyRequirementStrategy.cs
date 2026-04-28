using HWGA.ReadmeUpdater.Abstractions;

namespace HWGA.ReadmeUpdater.Strategies;

public class AnyRequirementStrategy(params ITaskResolutionStrategy[] strategies) : ITaskResolutionStrategy
{
    public bool IsResolved(string taskDirectory, string themeName, string taskName)
    {
        return strategies.Any(s => s.IsResolved(taskDirectory, themeName, taskName));
    }
}

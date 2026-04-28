namespace HWGA.ReadmeUpdater.Abstractions;

public interface ITaskResolutionStrategy
{
    bool IsResolved(string taskDirectory, string taskName);
}
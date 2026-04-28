namespace HWGA.ReadmeUpdater.Models;

public record ThemeProgress(
    string Name,
    IReadOnlyDictionary<string, DifficultyStats> Stats)
{
    public int AllDone => Stats.Values.Sum(s => s.Done);
    public int AllTotal => Stats.Values.Sum(s => s.Total);
    public bool IsCompleted => AllDone == AllTotal && AllTotal > 0;
}
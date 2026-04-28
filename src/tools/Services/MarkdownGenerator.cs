using System.Text;
using HWGA.ReadmeUpdater.Abstractions;
using HWGA.ReadmeUpdater.Models;

namespace HWGA.ReadmeUpdater.Services;

public class MarkdownGenerator() : IMarkdownGenerator
{
    public string GenerateTable(IEnumerable<ThemeProgress> themes, string[] levels)
    {
        var sb = new StringBuilder();
        sb.Append("| Тема | ").AppendJoin(" | ", levels).AppendLine(" | Status |");
        sb.Append("| :--- | ").AppendJoin(" | ", Enumerable.Repeat(":---:", levels.Length)).AppendLine(" | :--- |");

        foreach (var t in themes)
        {
            sb.Append($"| {t.Name} | ");
            foreach (var level in levels)
            {
                var s = t.Stats[level];
                sb.Append($"{s.Done}/{s.Total} | ");
            }
            sb.AppendLine($"{(t.IsCompleted ? "✅" : "🟡")} {t.AllDone}/{t.AllTotal} |");
        }
        return sb.ToString().TrimEnd();
    }
}

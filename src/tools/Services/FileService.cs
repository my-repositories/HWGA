using System.Text.RegularExpressions;
using HWGA.ReadmeUpdater.Abstractions;

namespace HWGA.ReadmeUpdater.Services;

public class FileService : IFileService
{
    private static readonly Regex TableRegex = new(
        @"(?<=<!-- PROGRESS_TABLE_START -->).*?(?=<!-- PROGRESS_TABLE_END -->)", 
        RegexOptions.Singleline | RegexOptions.Compiled);

    public void UpdateReadmeTable(string filePath, string newContent)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        var content = File.ReadAllText(filePath);
        File.WriteAllText(filePath, TableRegex.Replace(content, $"\n{newContent}\n"));
    }

    public static string FindProjectRoot(string solutionExtension)
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !dir.GetFiles(solutionExtension).Any()) dir = dir.Parent;
        return dir?.Parent?.FullName ?? AppContext.BaseDirectory;
    }
}

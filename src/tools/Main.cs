using System.Text;
using System.Text.RegularExpressions;

void UpdateTable(string filePath, string newTable)
{
    string content = File.ReadAllText(filePath);
    string pattern = @"(?<=<!-- PROGRESS_TABLE_START -->).*?(?=<!-- PROGRESS_TABLE_END -->)";
    string updatedContent = Regex.Replace(content, pattern, $"\n{newTable}\n", RegexOptions.Singleline);

    File.WriteAllText(filePath, updatedContent);
}

(int done, int total) GetTasksInfoByTheme(string level, string[] dirs)
{
    var done = 0;
    var filteredDirsByLevel = dirs.Where(x => Path.GetFileName(x).StartsWith(level, StringComparison.OrdinalIgnoreCase)).ToList();

    foreach(var dir in filteredDirsByLevel)
    {
        var files = Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories);
        if (files.Length > 0)
        {
            ++done;
        }
    }

    return (done, filteredDirsByLevel.Count);
}

string GetNewTable(string root)
{
    var sb = new StringBuilder();
    var themesPath = Path.Combine(root, "src", "app");
    var dirRegex = new Regex(@"Theme\d+\..+");
    var themeDirs = Directory.GetDirectories(themesPath)
        .Where(dir => dirRegex.IsMatch(Path.GetFileName(dir)));

    foreach(var theme in themeDirs)
    {
        var tasks = Directory.GetDirectories(theme);
        var (easyDone, easyTotal) = GetTasksInfoByTheme("Easy", tasks);
        var (mediumDone, mediumTotal) = GetTasksInfoByTheme("Medium", tasks);
        var (hardDone, hardTotal) = GetTasksInfoByTheme("Hard", tasks);
        var allDone = easyDone + mediumDone + hardDone;
        var allTotal = easyTotal + mediumTotal + hardTotal;
        var status = allDone == allTotal ? "✅" : "🟡";

        sb.Append($"|{Path.GetFileName(theme)}");
        sb.Append($"|{easyDone}/{easyTotal}");
        sb.Append($"|{mediumDone}/{mediumTotal}");
        sb.Append($"|{hardDone}/{hardTotal}");
        sb.AppendLine($"|{status}{allDone}/{allTotal}|");
    }

    return sb.ToString().TrimEnd();
}

string GetProjectRoot()
{
    var directory = new DirectoryInfo(AppContext.BaseDirectory);
    
    while (directory != null && !directory.GetFiles("*.slnx").Any())
    {
        directory = directory.Parent;
    }
    
    return directory?.Parent?.FullName ?? AppContext.BaseDirectory;
}

string root = GetProjectRoot();

Console.WriteLine(GetNewTable(root));

UpdateTable(Path.Combine(root, "README.md"), GetNewTable(root));

using HWGA.ReadmeUpdater.Abstractions;
using HWGA.ReadmeUpdater.Services;
using HWGA.ReadmeUpdater.Strategies;

namespace HWGA.ReadmeUpdater;

public class Program(
    IFileService fileService, 
    Func<ITaskResolutionStrategy, string[], ITaskScanner> scannerFactory,
    IMarkdownGenerator generator)
{
    public static void Main(string[]? args = null)
    {
        var root = FileService.FindProjectRoot("*.slnx");
        
        var program = new Program
        (
            new FileService(),
            (strategy, levels) => new TaskScanner(strategy, levels),
            new MarkdownGenerator()
        );

        program.Run(root, ["Easy", "Medium", "Hard"]);
    }

    public void Run(string rootPath, string[] levels)
    {
        var unitTestsPath = Path.Combine(rootPath, "src", "tests", "app.unit");

        var strategy = new AllRequirementsStrategy(
            new FileExistenceStrategy("cs"),
            new TestExistenceStrategy(unitTestsPath)
        );

        var scanner = scannerFactory(strategy, levels);
        
        var themes = scanner.ScanThemes(rootPath);
        var table = generator.GenerateTable(themes, levels);
        var readmePath = Path.Combine(rootPath, "README.md");
        
        fileService.UpdateReadmeTable(readmePath, table);
    }
}
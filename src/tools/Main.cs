using HWGA.ReadmeUpdater.Services;
using HWGA.ReadmeUpdater.Strategies;

var root = FileService.FindProjectRoot("*.slnx");
var taskLevels = new string[] { "Easy", "Medium", "Hard" };
var unitTestsPath = Path.Combine(root, "src", "tests", "app.unit");

var hasCode = new FileExistenceStrategy("cs");
var hasUnitTests = new TestExistenceStrategy(unitTestsPath);
var complexStrategy = new AllRequirementsStrategy(hasCode, hasUnitTests);
var scanner = new TaskScanner(complexStrategy, taskLevels);
var generator = new MarkdownGenerator(taskLevels);

var table = generator.GenerateTable(scanner.ScanThemes(root));
var readmePath = Path.Combine(root, "README.md");
new FileService().UpdateReadmeTable(readmePath, table);

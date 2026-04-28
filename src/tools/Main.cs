using HWGA.ReadmeUpdater.Services;
using HWGA.ReadmeUpdater.Strategies;

var taskLevels = new string[] { "Easy", "Medium", "Hard" };
var hasCode = new FileExistenceStrategy("cs");
var hasTests = new TestExistenceStrategy("src/tests");
var complexStrategy = new AllRequirementsStrategy(hasCode, hasTests);
var scanner = new TaskScanner(complexStrategy, taskLevels);
var generator = new MarkdownGenerator(taskLevels);

var root = FileService.FindProjectRoot("*.slnx");
var table = generator.GenerateTable(scanner.ScanThemes(root));
var readmePath = Path.Combine(root, "README.md");
new FileService().UpdateReadmeTable(readmePath, table);

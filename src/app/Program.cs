using HWGA.Core;

namespace HWGA;

public class Program
{
    public static async Task Main(string[] args)
    {
        var output = Console.Out;
        var commands = new[] { "0", "q", "e", "quit", "exit" };
        var app = new App(new AssemblyTypeProvider(), output, Console.In, commands);
        await new Program().Run(app, output, commands);
    }

    public async Task Run(IApp app, TextWriter? output, string[] commandsForTerminate)
    {
        string programName = await app.AskProgramName();

        while (!commandsForTerminate.Contains(programName))
        {
            await app.StartProgram(programName);
            programName = await app.AskProgramName();
        }

        await output!.WriteLineAsync("Goodbye!");
    }
}

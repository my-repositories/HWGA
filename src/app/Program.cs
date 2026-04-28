using HWGA.Core;

namespace HWGA;

public class Program
{
    public static async Task Main(string[] args)
    {
        var commands = new[] { "0", "q", "e", "quit", "exit" };
        var app = new App(new AssemblyTypeProvider(), Console.Out, Console.In, commands);
        await new Program().Run(app, commands);
    }

    public async Task Run(IApp app, string[] commandsForTerminate)
    {
        string programName = await app.AskProgramName();

        while (!commandsForTerminate.Contains(programName))
        {
            await app.StartProgram(programName);
            programName = await app.AskProgramName();
        }

        Console.WriteLine("Goodbye!");
    }
}

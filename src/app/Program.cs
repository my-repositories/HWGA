using HWGA;

string[] commandsForTerminate = { "0", "q", "e", "quit", "exit" };
App app = new App(commandsForTerminate);

string programName = app.AskProgramName();

while (!commandsForTerminate.Contains(programName))
{
    Console.WriteLine("I want to start " + programName + "...");
    app.StartProgram(programName);
    programName = app.AskProgramName();
}
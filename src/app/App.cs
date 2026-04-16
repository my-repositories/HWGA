namespace HWGA;

public class App
{
    private readonly string[] _commandsForTerminate;
    private readonly IList<Type> _programTypesList;

    public App(string[] commandsForTerminate = null)
    {
        _commandsForTerminate = commandsForTerminate ?? ["quit", "exit"];
        var interfaceType = typeof(IProgram);
        _programTypesList = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => interfaceType.IsAssignableFrom(p) && p is { IsClass: true, IsAbstract: false })
            .ToList();
    }
    
    public async Task StartProgram(string programName)
    {
        if (!TryParseProgramName(programName, out var programType))
        {
            Console.WriteLine("Incorrect id!");
        }
        
        var program = (BaseProgram)Activator.CreateInstance(programType);
        await program.Start();
    }
    
    public string AskProgramName()
    {
        Console.WriteLine(System.Environment.NewLine + "Enter a number of program:");
        PrintAllPrograms();
        Console.WriteLine("Or type one of command to exit: " + string.Join(", ", _commandsForTerminate));
        return Console.ReadLine()?.Trim().ToLower() ?? "";
    }
    
    private void PrintAllPrograms()
    {
        var programList = _programTypesList.ToList();
        for (int i = 0, length = programList.Count; i < length; ++i)
        {
            Console.WriteLine($"{1 + i} - {programList[i]}");
        }
    }
    
    private bool TryParseProgramName(string programName, out Type programType)
    {
        if (int.TryParse(programName, out var programNumber))
        {
            if (programNumber < 0 || programNumber > _programTypesList.Count)
            {
                programType = null;
                return false;
            }

            programType = _programTypesList[programNumber - 1];
            return true;
        }
        
        programType = _programTypesList.FirstOrDefault(x => x.FullName.ToLower() == programName.ToLower());
        return programType != null;
    }
}
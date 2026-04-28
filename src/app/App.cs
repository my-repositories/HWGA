using HWGA.Core;

namespace HWGA;

public class App
{
    private readonly string[] _commandsForTerminate;
    private readonly IList<Type> _programTypesList;
    private readonly Func<Type, IProgram> _factory;
    private readonly TextWriter _output;
    private readonly TextReader _input;

    public App(ITypeProvider typeProvider, TextWriter output, TextReader input, string[] commandsForTerminate = null, Func<Type, IProgram> factory = null)
    {
        _output = output;
        _input = input;
        _commandsForTerminate = commandsForTerminate ?? ["quit", "exit"];
        _programTypesList = typeProvider.GetProgramTypes();
        _factory = factory ?? (t => (IProgram)Activator.CreateInstance(t, _output));
    }
    
    public async Task StartProgram(string programName)
    {
        if (!TryParseProgramName(programName, out var programType))
        {
            await _output.WriteLineAsync("Incorrect id!");
        }
        
        var program = _factory(programType);
        await program.Start();
    }
    
    public async Task<string> AskProgramName()
    {
        await _output.WriteLineAsync(Environment.NewLine + "Enter a number of program:");
        await PrintAllPrograms();
        await _output.WriteLineAsync("Or type one of command to exit: " + string.Join(", ", _commandsForTerminate));
        return _input.ReadLine()?.Trim().ToLower() ?? "";
    }
    
    private async Task PrintAllPrograms()
    {
        var programList = _programTypesList.ToList();
        for (int i = 0, length = programList.Count; i < length; ++i)
        {
            await _output.WriteLineAsync($"{1 + i} - {programList[i]}");
        }
    }
    
    private bool TryParseProgramName(string name, out Type type)
    {
        if (int.TryParse(name, out var num))
        {
            if (num < 1 || num > _programTypesList.Count)
            {
                type = null;
                return false;
            }
            type = _programTypesList[num - 1];
            return true;
        }
        type = _programTypesList.FirstOrDefault(x => x.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
        return type != null;
    }
}

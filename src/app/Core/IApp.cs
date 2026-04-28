namespace HWGA.Core;

public interface IApp 
{
    Task<string> AskProgramName();
    Task StartProgram(string name);
}

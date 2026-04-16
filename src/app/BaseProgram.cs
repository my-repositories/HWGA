namespace HWGA;

public abstract class BaseProgram : IProgram
{
    protected abstract Task Run(string[] args);

    public async Task Start(string[] args = null)
    {
        string programName = this.GetType().FullName;

        Console.WriteLine($"{System.Environment.NewLine}Run {programName}...");
        await Run(args);
        Console.WriteLine($"Terminate {programName}...");
    }
}
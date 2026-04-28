namespace HWGA.Core;

public abstract class BaseProgram : IProgram
{
    protected readonly TextWriter Output;

    protected BaseProgram(TextWriter output)
    {
        Output = output;
    }

    protected abstract Task Run(string[]? args);

    public async Task Start(string[]? args = null)
    {
        string? programName = GetType().FullName;

        await Output.WriteLineAsync($"{Environment.NewLine}Run {programName}...");
        try
        {
            await Run(args);            
        }
        catch (Exception ex)
        {
            await Output.WriteLineAsync($"ERROR with {programName}: {ex.Message}");
        }
        finally
        {
            await Output.WriteLineAsync($"Terminate {programName}...");
        }
    }
}

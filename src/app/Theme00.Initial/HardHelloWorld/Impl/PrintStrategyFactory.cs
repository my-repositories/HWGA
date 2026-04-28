using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class PrintStrategyFactory
{
    public static PrintStrategyFactory Instance { get; } = new();
    public IPrintStrategy CreateIPrintStrategy(TextWriter? output = null)
    {
        var strategy = new PrintStrategyImplementation(output);
        return strategy.SetupPrinting() switch 
        {
            { StatusCode: 0 } => strategy,
            _ => throw new Exception("Failed to initialize strategy")
        };
    }
}

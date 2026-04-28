using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class PrintStrategyFactory
{
    public static PrintStrategyFactory Instance { get; } = new();
    public IPrintStrategy CreateIPrintStrategy()
    {
        var strategy = new PrintStrategyImplementation();
        return strategy.SetupPrinting() switch 
        {
            { StatusCode: 0 } => strategy,
            _ => throw new Exception("Failed to initialize strategy")
        };
    }
}

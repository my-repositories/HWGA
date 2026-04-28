using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class HelloWorldImplementation : IHelloWorld
{
    public IHelloWorldString GetHelloWorld() => new HelloWorldStringImplementation();
    public IPrintStrategy GetPrintStrategy(TextWriter? output = null) => PrintStrategyFactory.Instance.CreateIPrintStrategy(output);
    public IStatusCode Print(IPrintStrategy strategy, IHelloWorldString toPrint) => strategy.Print(toPrint);
}

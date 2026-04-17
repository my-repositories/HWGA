using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class HelloWorldImplementation : IHelloWorld
{
    public IHelloWorldString GetHelloWorld() => new HelloWorldStringImplementation();
    public IPrintStrategy GetPrintStrategy() => PrintStrategyFactory.Instance.CreateIPrintStrategy();
    public IStatusCode Print(IPrintStrategy strategy, IHelloWorldString toPrint) => strategy.Print(toPrint);
}
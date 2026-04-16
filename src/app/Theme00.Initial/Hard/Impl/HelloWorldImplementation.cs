using HWGA.Interfaces;

namespace HWGA.Impl;

public class HelloWorldImplementation : IHelloWorld
{
    public IHelloWorldString GetHelloWorld() => new HelloWorldStringImplementation();
    public IPrintStrategy GetPrintStrategy() => PrintStrategyFactory.Instance.CreateIPrintStrategy();
    public IStatusCode Print(IPrintStrategy strategy, IHelloWorldString toPrint) => strategy.Print(toPrint);
}
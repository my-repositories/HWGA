namespace HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

public interface IHelloWorld
{
    IHelloWorldString GetHelloWorld();
    IPrintStrategy GetPrintStrategy();
    IStatusCode Print(IPrintStrategy strategy, IHelloWorldString toPrint);
}
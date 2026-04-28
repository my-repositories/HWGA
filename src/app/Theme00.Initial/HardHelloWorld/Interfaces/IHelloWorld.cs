namespace HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

public interface IHelloWorld
{
    IHelloWorldString GetHelloWorld();
    IPrintStrategy GetPrintStrategy(TextWriter? output = null);
    IStatusCode Print(IPrintStrategy strategy, IHelloWorldString toPrint);
}
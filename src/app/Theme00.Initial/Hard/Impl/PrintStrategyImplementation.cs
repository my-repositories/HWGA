using System.Text;
using HWGA.Interfaces;

namespace HWGA.Impl;

public class PrintStrategyImplementation : IPrintStrategy
{
    private Stream? _printStream;

    public IStatusCode SetupPrinting()
    {
        try
        {
            _printStream = Console.OpenStandardOutput();
            return new StatusCodeImplementation(0);
        }
        catch
        {
            return new StatusCodeImplementation(-1);
        }
    }

    public IStatusCode Print(IHelloWorldString stringObj)
    {
        try
        {
            var output = $"{stringObj.GetHelloWorldString().GetHelloWorldString()}{Environment.NewLine}";
            _printStream?.Write(Encoding.UTF8.GetBytes(output));
            return new StatusCodeImplementation(0);
        }
        catch
        {
            return new StatusCodeImplementation(-1);
        }
    }
}
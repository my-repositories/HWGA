using System.Text;
using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class PrintStrategyImplementation(TextWriter? writer = null) : IPrintStrategy
{
    private TextWriter? _printStream = writer;

    public IStatusCode SetupPrinting()
    {
        try
        {
            _printStream ??= Console.Out;
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
            _printStream?.Write(output);
            _printStream?.Flush();
            return new StatusCodeImplementation(0);
        }
        catch
        {
            return new StatusCodeImplementation(-1);
        }
    }
}

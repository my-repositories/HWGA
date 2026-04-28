using System.Text;
using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class PrintStrategyImplementation(Stream? outputStream = null) : IPrintStrategy
{
    private Stream? _printStream = outputStream;

    public IStatusCode SetupPrinting()
    {
        try
        {
            _printStream ??= Console.OpenStandardOutput();
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
            _printStream?.Flush();
            return new StatusCodeImplementation(0);
        }
        catch
        {
            return new StatusCodeImplementation(-1);
        }
    }
}

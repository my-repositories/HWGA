using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class PrintStrategyImplementation(TextWriter? writer = null, Func<TextWriter>? consoleOutProvider = null) : IPrintStrategy
{
    private TextWriter? _printStream = writer;
    private readonly Func<TextWriter> _consoleOutProvider = consoleOutProvider ?? (() => Console.Out);


    public IStatusCode SetupPrinting()
    {
        try
        {
            _printStream ??= _consoleOutProvider();
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

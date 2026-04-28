namespace HWGA.Theme00.Initial.HardHelloWorld;

using HWGA.Core;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class HelloWorldProgram(TextWriter output) : BaseProgram(output)
{
    protected override async Task Run(string[] args = null)
    {
        var factory = HelloWorldFactory.Instance;
        var helloWorld = factory.CreateHelloWorld();
        
        if (helloWorld.Print(helloWorld.GetPrintStrategy(), helloWorld.GetHelloWorld()) is { StatusCode: not 0 } err)
        {
            throw new Exception($"Failed to print: {err.StatusCode}");
        }
    }
}
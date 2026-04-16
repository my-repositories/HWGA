namespace HWGA;

using HWGA.Impl;

public class HelloWorld : BaseProgram
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
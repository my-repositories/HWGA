using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class HelloWorldFactory
{
    public static HelloWorldFactory Instance { get; } = new();
    public IHelloWorld CreateHelloWorld() => new HelloWorldImplementation();
}

using HWGA.Interfaces;

namespace HWGA.Impl;

public class HelloWorldFactory
{
    public static HelloWorldFactory Instance { get; } = new();
    public IHelloWorld CreateHelloWorld() => new HelloWorldImplementation();
}

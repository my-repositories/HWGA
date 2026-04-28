using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class HelloWorldStringImplementation : IHelloWorldString
{
    public HelloWorldString GetHelloWorldString() => 
        StringFactory.Instance.CreateHelloWorldString("Hello, World!");
}

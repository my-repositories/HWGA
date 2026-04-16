using HWGA.Interfaces;

namespace HWGA.Impl;

public class HelloWorldStringImplementation : IHelloWorldString
{
    public HelloWorldString GetHelloWorldString() => 
        StringFactory.Instance.CreateHelloWorldString("Hello, World!");
}
namespace HWGA.Impl;

public class StringFactory
{
    public static StringFactory Instance { get; } = new();
    public HelloWorldString CreateHelloWorldString(string str) => new(str);
}
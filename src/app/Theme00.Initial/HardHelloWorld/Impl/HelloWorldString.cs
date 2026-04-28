namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public class HelloWorldString(string? s)
{
    public string? S { get; set; } = s;
    public string? GetHelloWorldString() => S;
}

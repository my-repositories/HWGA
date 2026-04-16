namespace HWGA.Impl;

public class HelloWorldString(string? s)
{
    public string? S { get; set; } = s;
    public string? GetHelloWorldString() => S;
}
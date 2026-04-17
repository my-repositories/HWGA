using HWGA.Theme00.Initial.HardHelloWorld.Interfaces;

namespace HWGA.Theme00.Initial.HardHelloWorld.Impl;

public record StatusCodeImplementation(int StatusCode) : IStatusCode;

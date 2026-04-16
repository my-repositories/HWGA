using HWGA.Interfaces;

namespace HWGA.Impl;

public record StatusCodeImplementation(int StatusCode) : IStatusCode;

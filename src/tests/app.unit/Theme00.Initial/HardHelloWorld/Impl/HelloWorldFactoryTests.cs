using FluentAssertions;
using HWGA.Theme00.Initial.HardHelloWorld.Impl;

namespace HWGA.Theme00.Initial.HardHelloWorld.Tests;

public class HelloWorldFactoryTests
{
    [Fact]
    public void Instance_ShouldBeSingleton()
    {
        // Act
        var instance1 = HelloWorldFactory.Instance;
        var instance2 = HelloWorldFactory.Instance;

        // Assert
        instance1.Should().BeSameAs(instance2);
    }

    [Fact]
    public void CreateHelloWorld_ShouldReturnImplementation()
    {
        // Act
        var result = HelloWorldFactory.Instance.CreateHelloWorld();

        // Assert
        result.Should().NotBeNull()
              .And.BeOfType<HelloWorldImplementation>();
    }
}

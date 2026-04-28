using HWGA.Core;
using FluentAssertions;

namespace HWGA.Tests.Unit;

public class AssemblyTypeProviderTests
{
    [Fact]
    public void GetProgramTypes_ShouldFindExistingPrograms()
    {
        // Arrange
        var sut = new AssemblyTypeProvider();

        // Act
        var types = sut.GetProgramTypes();

        // Assert
        types.Should().NotBeEmpty();
        types.Should().NotContain(t => t.IsAbstract);
        types.Should().AllSatisfy(t => typeof(IProgram).IsAssignableFrom(t).Should().BeTrue());
    }

    [Fact]
    public void GetProgramTypes_ShouldBeSortedAlphabetically_ByFullName()
    {
        // Arrange
        var sut = new AssemblyTypeProvider();

        // Act
        var types = sut.GetProgramTypes();

        // Assert
        types.Should().BeInAscendingOrder(x => x.FullName);
    }

    [Fact]
    public void GetProgramTypes_ShouldIgnoreInterfaces_AndInternalAbstractClasses()
    {
        // Arrange
        var sut = new AssemblyTypeProvider();

        // Act
        var types = sut.GetProgramTypes();

        // Assert
        types.Should().NotContain(typeof(IProgram));
        types.Should().OnlyContain(t => t.IsClass && !t.IsAbstract);
    }
}

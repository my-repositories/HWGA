using HWGA.Theme01.CLR.EasyValueTypeVsReferenceType;

namespace HWGA.Tests.Theme01.CLR.EasyValueTypeVsReferenceType;

public class ProgramTests
{
    [Fact]
    public async Task Program_Should_Not_Fail()
    {
        using var tw = TextWriter.Null;
        var program = new Program(tw);
        await program.Start();
    }
}

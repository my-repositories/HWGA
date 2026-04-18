using HWGA.ThemeXX_EFCore;
using HWGA.ThemeXX_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace HWGA.Tests;

public class UnitTest1 : IClassFixture<MyFixture>
{
    private readonly HWGADbContext _context;

    public UnitTest1(MyFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public async Task CJ_Should_Live_In_LosSantos()
    {
        // Arange
        var cj = "Carl Johnson";

        // Act
        await _context.Users.AddAsync(new User { Name = cj });
        await _context.SaveChangesAsync();
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Name == cj);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cj, result.Name);
        Assert.NotEqual(0, result.Id);
    }
}

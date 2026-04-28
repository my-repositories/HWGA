using HWGA.ThemeXX_EFCore;
using Microsoft.EntityFrameworkCore;

namespace HWGA.Tests;

public class MyFixture : IDisposable
{
    public HWGADbContext Context { get; }

    public MyFixture()
    {
        var options = new DbContextOptionsBuilder<HWGADbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        Context = new HWGADbContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}

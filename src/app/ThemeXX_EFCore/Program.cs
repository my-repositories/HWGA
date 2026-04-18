using HWGA.ThemeXX_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace HWGA.ThemeXX_EFCore;

public class EFCoreProgram : BaseProgram
{
    protected override async Task Run(string[] args)
    {
        using var db = new HWGADbContext();
        await db.Database.MigrateAsync(); 

        db.Users.Add(new User { Name = "Carl Johnson" });
        await db.SaveChangesAsync();

        var users = await db.Users.AsNoTracking().ToListAsync();
        users.ForEach(u => Console.WriteLine($"{u.Id}: {u.Name}"));
    }
}
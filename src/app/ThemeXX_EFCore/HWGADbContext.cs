using HWGA.ThemeXX_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace HWGA.ThemeXX_EFCore;

public class HWGADbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=localhost;Database=hwga;Username=cj;Password=HESOYAM");
}

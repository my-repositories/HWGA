using HWGA.ThemeXX_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace HWGA.ThemeXX_EFCore;

public class HWGADbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public HWGADbContext() : base() {}
    public HWGADbContext(DbContextOptions options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured) {
            options.UseNpgsql("Host=localhost;Database=hwga;Username=cj;Password=HESOYAM");
        }
    }
}

using MapSettingsTask.APIs.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MapSettingsTask.APIs.Data.Ccontext;

public class MapContext : IdentityDbContext
{
    public MapContext(DbContextOptions<MapContext> options):base(options)
    {
        
    }
    public DbSet<MapSettings> Settings { get; set; }
    public DbSet<MapType> MapTypes { get; set; }
    public DbSet<MapSubType> MapSubType { get; set; }

    public DbSet<MapCreator> MapCreator { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<MapCreator>().ToTable("Creators");
        builder.Entity<IdentityUserClaim<string>>().ToTable("MapCreatorClaims");
    }
}


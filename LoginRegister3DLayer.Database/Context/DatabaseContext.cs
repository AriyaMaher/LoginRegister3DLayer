using LoginRegister3DLayer.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegister3DLayer.Database.Context;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder option)
    {
        option.UseSqlServer(@"Data source=.;
                            Initial Catalog=LoginRegister3DLayerDb;
                            Integrated Security=SSPI");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new DbInitializer(modelBuilder).Seed();
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
}

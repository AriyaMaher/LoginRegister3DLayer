using LoginRegister3DLayer.Database.Classes;
using LoginRegister3DLayer.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegister3DLayer.Database.Context
{
    internal class DbInitializer
    {
        private ModelBuilder _modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        internal async void Seed()
        {
            Role adminRole = new Role()
            {
                Id = Guid.NewGuid(),
                RoleName = "admin",
            }; _modelBuilder.Entity<Role>().HasData(adminRole);

            Role userRole = new Role()
            {
                Id = Guid.NewGuid(),
                RoleName = "user",
            }; _modelBuilder.Entity<Role>().HasData(userRole);

            User adminUser = new User()
            {
                Id = Guid.NewGuid(),
                RoleId = adminRole.Id,
                Mobile = "09120000000",
                Password = await new Security().HashPassword("12345678"),
            }; _modelBuilder.Entity<User>().HasData(adminUser);
        }
    }
}
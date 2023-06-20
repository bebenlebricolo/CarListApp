using CarListApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarListApi.Services
{
    public class CarListDbContext : IdentityDbContext
    {
        public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Make = "Renault",
                    Model = "4L",
                    Vin = "123456"
                },
                new Car
                {
                    Id = 2,
                    Make = "Renault",
                    Model = "Espace",
                    Vin = "123456"
                },
                new Car
                {
                    Id = 3,
                    Make = "Renault",
                    Model = "Clio",
                    Vin = "123456"
                },
                new Car
                {
                    Id = 4,
                    Make = "Citroën",
                    Model = "2 Chevaux",
                    Vin = "123456"
                },
                new Car
                {
                    Id = 5,
                    Make = "Citroen",
                    Model = "DS",
                    Vin = "123456"
                },
                new Car
                {
                    Id = 6,
                    Make = "Peugeot",
                    Model = "205",
                    Vin = "123456"
                },
                new Car
                {
                    Id = 7,
                    Make = "Peugeot",
                    Model = "208",
                    Vin = "123456"
                }
            );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "2304b13a-86e4-4c5c-8325-f8acb266ab34",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                },
                new IdentityRole
                {
                    Id = "0d2f2532-68a4-4e01-89eb-00e6cf54138b",
                    Name = "User",
                    NormalizedName = "USER",
                }
            );

            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "c0bb7fcc-58c0-4b68-a761-6a257c3b8ba4",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                },
                new IdentityUser
                {
                    Id = "da62ea4f-d884-4b67-87f3-ed0dbfdbf1c0",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssword2"),
                    EmailConfirmed = true
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2304b13a-86e4-4c5c-8325-f8acb266ab34",
                    UserId = "c0bb7fcc-58c0-4b68-a761-6a257c3b8ba4",
                },
                new IdentityUserRole<string>
                {
                    RoleId = "0d2f2532-68a4-4e01-89eb-00e6cf54138b",
                    UserId = "da62ea4f-d884-4b67-87f3-ed0dbfdbf1c0",
                }
            );
        }
    }
}
using CarListApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarListApi.Services
{
    public class CarListDbContext : DbContext
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
        }
    }
}
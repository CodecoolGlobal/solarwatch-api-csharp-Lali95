using Microsoft.EntityFrameworkCore;
using SolarWatch.Data.SunriseSunset;

namespace SolarWatch.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<SunriseSunset.SunriseSunset> SunriseSunsets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure the City entity - making the 'Name' unique
            builder.Entity<City>()
                .HasIndex(u => u.Name)
                .IsUnique();

            // Seed initial data for City
            builder.Entity<City>()
                .HasData(
                    new City { Id = 1, Name = "London", Coordinates = new Coordinates { Latitude = 51.509865, Longitude = -0.118092 }, State = "SomeState", Country = "UK" },
                    new City { Id = 2, Name = "Budapest", Coordinates = new Coordinates { Latitude = 47.497913, Longitude = 19.040236 }, State = "SomeState", Country = "Hungary" },
                    new City { Id = 3, Name = "Paris", Coordinates = new Coordinates { Latitude = 48.864716, Longitude = 2.349014 }, State = "SomeState", Country = "France" }
                );

            // Other configurations...

            // Call base class method
            base.OnModelCreating(builder);
        }
    }
}
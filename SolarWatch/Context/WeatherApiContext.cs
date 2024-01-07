using Microsoft.EntityFrameworkCore;
using SolarWatch.Model;

namespace SolarWatch.Context;

public class WeatherApiContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SunriseSunset> SunriseSunsets { get; set; }


    public WeatherApiContext(DbContextOptions<WeatherApiContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=WeatherApi;User Id=sa;Password=Tt19372846519;Integrated Security=False;Trusted_Connection=False;Encrypt=False;");
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Configure the City entity - making the 'Name' unique
        builder.Entity<City>()
            .HasIndex(u => u.Name)
            .IsUnique();
    
        builder.Entity<City>()
            .HasData(
                new City { Id = 1, Name = "London", Latitude = 51.509865, Longitude = -0.118092 },
                new City { Id = 2, Name = "Budapest", Latitude = 47.497913, Longitude = 19.040236 },
                new City { Id = 3, Name = "Paris", Latitude = 48.864716, Longitude = 2.349014 }
            );
    }
}
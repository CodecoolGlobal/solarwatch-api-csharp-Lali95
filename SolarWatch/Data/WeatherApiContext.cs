using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;
using SolarWatch.Models;

public class WeatherApiContext : DbContext
{
    public DbSet<City> Cities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=WeatherApi;User Id=sa;Password=Tt19372846519;Encrypt=false;");
    }
}
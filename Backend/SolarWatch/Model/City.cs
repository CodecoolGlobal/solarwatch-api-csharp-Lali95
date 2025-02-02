using System.ComponentModel.DataAnnotations;

namespace SolarWatch.Model;

public class City
{
    [Key] 
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
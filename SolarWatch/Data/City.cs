using System.ComponentModel.DataAnnotations.Schema;

namespace SolarWatch.Models
{
    public class City
    {
        public int Id { get; set; } // Primary key

        public string Name { get; set; }
        
        [NotMapped] // This tells Entity Framework to ignore Coordinates as an entity
        public Coordinates Coordinates { get; set; }
        
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class Coordinates
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
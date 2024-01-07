using System.ComponentModel.DataAnnotations.Schema;

namespace SolarWatch.Data
{
    public class City
    {
        public int Id { get; set; } // Primary key

        public string Name { get; set; }

        [NotMapped]
        public Coordinates Coordinates { get; set; }

        public string State { get; set; }
        public string Country { get; set; }

        // Navigation property to represent the relationship with SunriseSunset
        public List<SunriseSunset.SunriseSunset> SunriseSunsets { get; set; }
    }

    // Ensure that Coordinates is in the same namespace
    public class Coordinates
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
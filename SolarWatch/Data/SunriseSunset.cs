using SolarWatch.Models;

namespace SolarWatch.Data.SunriseSunset
{
    public class SunriseSunset
    {
        public int Id { get; set; } // Primary key

        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        // Foreign key to reference the associated city
        public int CityId { get; set; }

        // Navigation property to represent the relationship with City
        public City City { get; set; }
    }
}
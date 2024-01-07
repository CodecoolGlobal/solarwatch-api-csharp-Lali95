namespace SolarWatch.Models
{
    public class City
    {
        public string Name { get; set; }
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
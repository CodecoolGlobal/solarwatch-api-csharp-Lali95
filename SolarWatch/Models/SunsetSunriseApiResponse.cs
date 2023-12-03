namespace SolarWatch.Models;

public class SunriseSunsetApiResponse
{
    public Results Results { get; set; }
    public string Status { get; set; }
    public string TzId { get; set; }
}
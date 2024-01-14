namespace SolarWatch.Service;

public interface ISunriseSunsetService
{
    Task<(TimeSpan sunrise, TimeSpan sunset)> GetSunriseSunsetAsync(double latitude, double longitude, DateTime date);
}
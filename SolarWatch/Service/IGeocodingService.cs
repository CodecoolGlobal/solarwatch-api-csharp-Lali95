namespace SolarWatch.Service;

public interface IGeocodingService
{
    Task<(double latitude, double longitude)> GetCoordinatesForCityAsync(string city);
}
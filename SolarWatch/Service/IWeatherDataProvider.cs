namespace SolarWatch.Service;

public interface IWeatherDataProvider
{
    string GetCurrent(double lat, double lon);
    Task<string> GetCurrentAsync(double lat, double lon);
}

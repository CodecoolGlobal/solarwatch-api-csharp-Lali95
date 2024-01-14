using SolarWatch;
using SolarWatch.Model;

namespace SolarWatch.Service;

public interface IJsonProcessor
{
    WeatherForecast Process(string data);
}
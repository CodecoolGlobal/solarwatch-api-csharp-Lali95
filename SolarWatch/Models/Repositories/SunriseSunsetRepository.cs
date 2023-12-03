using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SolarWatch.Models.Repositories
{
    public class SunriseSunsetRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SunriseSunsetRepository> _logger;
        private readonly string _openWeatherMapApiKey;

        public SunriseSunsetRepository(HttpClient httpClient, ILogger<SunriseSunsetRepository> logger, string openWeatherMapApiKey)
        {
            _httpClient = httpClient;
            _logger = logger;
            _openWeatherMapApiKey = openWeatherMapApiKey;
        }

        public async Task<LocationModel> GetGeographicalCoordinatesAsync(string cityName)
        {
            try
            {
                var openWeatherMapApiUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid={_openWeatherMapApiKey}";

                var response = await _httpClient.GetStringAsync(openWeatherMapApiUrl);

                var result = JsonConvert.DeserializeObject<LocationModel[]>(response);

                if (result.Length > 0)
                {
                    return result[0];
                }
                else
                {
                    _logger.LogError($"No coordinates found for {cityName} in the OpenWeatherMap response.");
                    throw new InvalidOperationException($"No coordinates found for {cityName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching geographical coordinates for {cityName} from OpenWeatherMap: {ex.Message}");
                throw;
            }
        }

        public async Task<SunriseSunsetApiResponse> GetSunriseSunsetTimesAsync(string cityName, DateTime date)
        {
            try
            {
                var location = await GetGeographicalCoordinatesAsync(cityName);

                var sunriseSunsetApiUrl = $"https://api.sunrise-sunset.org/json?lat={location.Latitude}&lng={location.Longitude}&date={date:yyyy-MM-dd}&formatted=0";

                var response = await _httpClient.GetStringAsync(sunriseSunsetApiUrl);
                var result = JsonConvert.DeserializeObject<SunriseSunsetApiResponse>(response);

                if (result != null && result.Status == "OK")
                {
                    return result;
                }
                else
                {
                    _logger.LogError($"Error in Sunrise-Sunset API response. Status: {result?.Status}");
                    throw new InvalidOperationException($"Error in Sunrise-Sunset API response.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching sunrise and sunset times from Sunrise-Sunset API: {ex.Message}");
                throw;
            }
        }
    }
}

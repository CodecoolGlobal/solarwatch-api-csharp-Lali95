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

        public SunriseSunsetRepository(HttpClient httpClient, ILogger<SunriseSunsetRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<LocationModel> GetGeographicalCoordinatesAsync(string cityName)
        {
            var _openWeatherMapApiKey = "8921956c7a9183a7c24b85d014c85aab";
            var openWeatherMapApiUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid={_openWeatherMapApiKey}";

            try
            {
                // Make the API request to OpenWeatherMap to get geographical coordinates
                var response = await _httpClient.GetStringAsync(openWeatherMapApiUrl);

                // Deserialize the response directly into LocationModel
                var result = JsonConvert.DeserializeObject<LocationModel[]>(response);

                if (result.Length > 0)
                {
                    // Return the first element (assuming the API returns an array)
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
        
        public async Task<SunriseSunsetApiResponse> GetSunriseSunsetTimesAsync(double latitude, double longitude, DateTime date)
        {
            var sunriseSunsetApiUrl = $"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}&date={date:yyyy-MM-dd}&formatted=0";

            try
            {
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
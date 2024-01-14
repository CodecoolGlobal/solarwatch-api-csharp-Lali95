namespace SolarWatch.Service;

public class GeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "http://api.openweathermap.org/geo/1.0";
    private readonly string _apiKey = "8921956c7a9183a7c24b85d014c85aab";

    public GeocodingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(double latitude, double longitude)> GetCoordinatesForCityAsync(string city)
    {
        try
        {
            string endpoint = $"{_apiBaseUrl}/direct?q={city}&appid={_apiKey}";

            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<GeocodingResponse[]>();
                if (data.Length > 0)
                {
                    var result = data[0];
                    return (result.Lat, result.Lon);
                }
            }
            
            throw new Exception($"City '{city}' not found.");
        }
        catch (Exception ex)
        {
            throw new Exception("Error while calling Geocoding API.", ex);
        }
    }
}
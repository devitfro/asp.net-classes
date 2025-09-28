using app_hw.Models;
using System.Text.Json;

namespace app_hw.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "632b0fea1c21ea78b545d1391af7601d";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Weather> GetWeatherAsync(string city)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                var weather = JsonSerializer.Deserialize<Weather>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return weather;
            }
            catch
            {
                return null;
            }
        }
    }
}

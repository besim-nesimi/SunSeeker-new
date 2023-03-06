using Newtonsoft.Json;
using SunSeeker.Models;

namespace SunSeeker.Api
{
    public class ApiHelper
    {
        private readonly IConfiguration config;

        public static HttpClient HttpClient { get; set; } = new();

        public ApiHelper(IConfiguration config)
        {
            this.config = config;
        }

        // Sätt bas address / grund- inställningar för HttpClienten
        public static void InitializeClient() 
        {
            HttpClient.BaseAddress = new Uri("https://api.weatherstack.com/");
        }

        // Metoden visar aktuella vädret för den stad som valts / skickats till metoden.
        public async Task<WeatherModel?> GetCurrentWeather(string city)
        {
            // Gör själva call:et till API:t - GET request
            var response = await HttpClient.GetAsync($"current?access_key{config["AccessKey"]}&query={city.ToLower()}");


            // Om response från call:et är OK (200)...
            if(response.IsSuccessStatusCode)
            {
                // Läs bodyn i responset som JSON
                var json = await response.Content.ReadAsStringAsync();

                // Deserialisera (konvertera) JSON till C#-objekt
                Root? data = JsonConvert.DeserializeObject<Root>(json);

                // Om vi har data...
                if(data != null)
                {
                    // Projicera data-objektet till WeatherModel-objekt
                    WeatherModel weather = new()
                    {
                        City = data.location.name,
                        Country = data.location.country,
                        Temperature = data.current.temperature,
                        FeelsLike = data.current.feelslike,
                        // Konvertera (pars:a) en sträng till en DateTime.
                        Date = DateTime.Parse(data.location.localtime),
                        Icon = data.current.weather_icons[0]
                    };

                    // Returnera WeatherModel objektet som instansierats ovan
                    return weather;
                }
            }

            // Annars, returnera null om call:et inte är OK (200)
            return null;
        }
    }
}

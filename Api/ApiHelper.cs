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
            HttpClient.BaseAddress = new Uri("http://api.weatherstack.com/");
        }

        // Metoden visar aktuella vädret för den stad som valts / skickats till metoden.
        public async Task<WeatherModel?> GetCurrentWeather(string city)
        {
            // Gör själva call:et till API:t - GET request
            var response = await HttpClient.GetAsync($"current?access_key={config["AccessKey"]}&query={city.ToLower()}");


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
                        City = data.Location.name,
                        Country = data.Location.country,
                        Temperature = data.Current.temperature,
                        FeelsLike = data.Current.feelslike,
                        // Konvertera (pars:a) en sträng till en DateTime.
                        Date = DateTime.Parse(data.Location.localtime),
                        Icons = data.Current.weather_icons
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

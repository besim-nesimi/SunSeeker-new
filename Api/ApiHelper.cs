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

        // Sätt bas address / grund- inställningar
        public static void InitializeClient() 
        {
            HttpClient.BaseAddress = new Uri("https://api.weatherstack.com/");
        }

        // Metoden visar aktuella vädret för den stad som valts / skickats till metoden.
        public void GetCurrentWeather(string city)
        {
            var response = HttpClient.GetAsync($"current?access_key{config["AccessKey"]}&query={city.ToLower()}");
        }
    }
}

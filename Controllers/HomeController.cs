using Microsoft.AspNetCore.Mvc;
using SunSeeker.Api;
using SunSeeker.Models;
using System.Diagnostics;

namespace SunSeeker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;

        public HomeController(IConfiguration config)
        {
            this.config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(WeatherModel weather)
        {
            // Gör call till API:t
            WeatherModel? result = await new ApiHelper(config).GetCurrentWeather(weather.City);

            // Om data finns
            if(result != null)
            {
                return View("Display", result);
            }

            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            // Skicka användaren till Display-sidan (med väderinfo)
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
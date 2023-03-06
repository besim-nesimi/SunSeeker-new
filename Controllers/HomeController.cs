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
            var result = await new ApiHelper(config).GetCurrentWeather(weather.City);

            // Skicka användaren till Display-sidan (med väderinfo)
            return View("Display");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
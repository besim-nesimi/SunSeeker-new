using Microsoft.AspNetCore.Mvc;
using SunSeeker.Api;
using SunSeeker.Data;
using SunSeeker.Models;
using System.Diagnostics;

namespace SunSeeker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;
        private readonly AppDbContext context;

        public HomeController(IConfiguration config, AppDbContext context)
        {
            this.config = config;
            this.context = context;
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

        [HttpPost]
        public IActionResult Save(WeatherModel weather)
        {
            // Spara till Databas
            context.Weather.Add(weather);
            context.SaveChanges();
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
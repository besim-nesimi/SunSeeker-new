using System.ComponentModel.DataAnnotations.Schema;

namespace SunSeeker.Models
{
    public class WeatherModel
    {
        public int Id { get; set; }
        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public int Temperature { get; set; }

        public int FeelsLike { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        public List<string>? Icons { get; set; }
    }
}

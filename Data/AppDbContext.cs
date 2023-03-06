using Microsoft.EntityFrameworkCore;
using SunSeeker.Models;

namespace SunSeeker.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			
		}

		public DbSet<WeatherModel> Weather { get; set; }
	}
}

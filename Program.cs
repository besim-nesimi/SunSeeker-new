using Microsoft.EntityFrameworkCore;
using SunSeeker.Api;
using SunSeeker.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var accessKey = builder.Configuration["AccessKey"];

// Initiera HttpClient
ApiHelper.InitializeClient();

// Initiera DB kontakt
var connectionString = builder.Configuration.GetConnectionString("WeatherDbConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

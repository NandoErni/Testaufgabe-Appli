using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using testaufgabe.Data;
using testaufgabe.Repositories;
using testaufgabe.Services;
using testaufgabe.Utils;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<WeatherDataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<WeatherDataRepository>();
builder.Services.AddScoped<WeatherDataService>();
builder.Services.AddHttpClient<WeatherDataFetcher>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather Data API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather Data API V1");
});

app.MapControllers();

app.Run();


using System;
using Microsoft.EntityFrameworkCore;
using testaufgabe.Models;

namespace testaufgabe.Data
{
	public class WeatherDataContext : DbContext
	{
        public WeatherDataContext(DbContextOptions<WeatherDataContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherData> WeatherData { get; set; }
    }
}


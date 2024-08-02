using System;
using Microsoft.EntityFrameworkCore;

namespace testaufgabe.Data
{
	public class WeatherDataContext : DbContext
	{
        public WeatherDataContext(DbContextOptions<WeatherDataContext> options)
            : base(options)
        {
        }
    }
}


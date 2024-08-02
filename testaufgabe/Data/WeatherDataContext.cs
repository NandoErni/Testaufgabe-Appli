using System;
using Microsoft.EntityFrameworkCore;
using testaufgabe.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace testaufgabe.Data
{
	public class WeatherDataContext : DbContext
	{
        public WeatherDataContext(DbContextOptions<WeatherDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherData>(entity =>
            {
                entity.OwnsOne(e => e.AirTemperature, navigationBuilder =>
                {
                    navigationBuilder.Property(p => p.Value).HasColumnName("AirTemperature_Value");
                    navigationBuilder.Property(p => p.Unit).HasColumnName("AirTemperature_Unit");
                });

                entity.OwnsOne(e => e.WaterTemperature, navigationBuilder =>
                {
                    navigationBuilder.Property(p => p.Value).HasColumnName("WaterTemperature_Value");
                    navigationBuilder.Property(p => p.Unit).HasColumnName("WaterTemperature_Unit");
                });

                entity.OwnsOne(e => e.BarometricPressure, navigationBuilder =>
                {
                    navigationBuilder.Property(p => p.Value).HasColumnName("BarometricPressure_Value");
                    navigationBuilder.Property(p => p.Unit).HasColumnName("BarometricPressure_Unit");
                });

                entity.OwnsOne(e => e.Humidity, navigationBuilder =>
                {
                    navigationBuilder.Property(p => p.Value).HasColumnName("Humidity_Value");
                    navigationBuilder.Property(p => p.Unit).HasColumnName("Humidity_Unit");
                });
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<WeatherData> WeatherData { get; set; }
    }
}


using System;
using Microsoft.EntityFrameworkCore;
using testaufgabe.Data;
using testaufgabe.Models;

namespace testaufgabe.Repositories
{
	public class WeatherDataRepository
	{
        private readonly WeatherDataContext _context;

        public WeatherDataRepository(WeatherDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeatherData>> GetWeatherDataAsync(DateTime start, DateTime end, WeatherDataStation? station = null)
        {
            if (end < start)
            {
                throw new ArgumentException($"The start time {start} has to be before the end time {end}");
            }

            var query = _context.WeatherData.AsQueryable();
            if (station.HasValue)
            {
                query = query.Where(d => d.Station == station);
            }
            query = query.Where(d => d.Timestamp > start && d.Timestamp < end);

            return await query.ToListAsync();
        }

        public async Task SaveWeatherDataAsync(IEnumerable<WeatherData> weatherData)
		{
            _context.WeatherData.AddRange(weatherData);
            await _context.SaveChangesAsync();
		}

        public async Task RemoveGivenWeatherDataAsync(IEnumerable<WeatherData> weatherData)
        {
            _context.WeatherData.RemoveRange(weatherData);
            await _context.SaveChangesAsync();
        }
    }
}


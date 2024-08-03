using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
            var query = GetQueryable(start, end, station);

            return await query.ToListAsync();
        }

        public async Task SaveUniqueWeatherDataAsync(IEnumerable<WeatherData> weatherData)
		{

            foreach (var data in weatherData)
            {
                if (await _context.WeatherData.AnyAsync(w => w.Timestamp == data.Timestamp && w.Station == data.Station))
                {
                    continue;
                }

                await _context.WeatherData.AddAsync(data);
            }

            await _context.SaveChangesAsync();

        }

        public async Task RemoveGivenWeatherDataAsync(IEnumerable<WeatherData> weatherData)
        {
            _context.WeatherData.RemoveRange(weatherData);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetWeatherDataCountAsync(DateTime start, DateTime end, WeatherDataStation? station = null)
        {
            var query = GetQueryable(start, end, station);

            return await query.CountAsync();
        }

        private IQueryable<WeatherData> GetQueryable(DateTime start, DateTime end, WeatherDataStation? station = null)
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

            return query;
        }

        public async Task<double> GetWeatherDataAverageAsync(DateTime start, DateTime end, WeatherDataType weatherDataType, WeatherDataStation? station)
        {
            var query = GetQueryable(start, end, station);

            switch (weatherDataType)
            {
                case WeatherDataType.AirTemperature:
                    return await query.AverageAsync(w => w.AirTemperature.Value);
                case WeatherDataType.WaterTemperature:
                    return await query.AverageAsync(w => w.WaterTemperature.Value);
                case WeatherDataType.BarometricPressure:
                    return await query.AverageAsync(w => w.BarometricPressure.Value);
                case WeatherDataType.Humidity:
                    return await query.AverageAsync(w => w.Humidity.Value);
                default: throw new ArgumentException($"Unkown Enum Value {weatherDataType}");
            }
            
        }
    }
}


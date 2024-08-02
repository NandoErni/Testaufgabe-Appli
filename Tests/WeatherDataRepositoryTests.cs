using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using testaufgabe.Data;
using testaufgabe.Models;
using testaufgabe.Repositories;

namespace Tests
{
	public class WeatherDataRepositoryTests
	{
        private readonly DbContextOptions<WeatherDataContext> _dbContextOptions;

        public WeatherDataRepositoryTests()
		{
            _dbContextOptions = new DbContextOptionsBuilder<WeatherDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        private async Task<WeatherDataRepository> CreateRepositoryWithData(WeatherDataContext context)
        {
            var repository = new WeatherDataRepository(context);

            var weatherData = new List<WeatherData>(){
            new WeatherData
            {
                Station = WeatherDataStation.Tiefenbrunnen,
                Timestamp = new DateTime(2021, 4, 13),
                Type = WeatherDataType.WaterTemperature,
                Value = 17.69,
                Unit = "°C"
            },new WeatherData
            {
                Station = WeatherDataStation.Mythenquai,
                Timestamp = new DateTime(2020, 4, 13),
                Type = WeatherDataType.AirTemperature,
                Value = 44,
                Unit = "°C"
            },new WeatherData
            {
                Station = WeatherDataStation.Tiefenbrunnen,
                Timestamp = new DateTime(2022, 4, 13),
                Type = WeatherDataType.Humidity,
                Value = 20,
                Unit = "%"
            },new WeatherData
            {
                Station = WeatherDataStation.Mythenquai,
                Timestamp = new DateTime(2023, 4, 13),
                Type = WeatherDataType.WaterTemperature,
                Value = 6.9,
                Unit = "°C"
            },new WeatherData
            {
                Station = WeatherDataStation.Tiefenbrunnen,
                Timestamp = new DateTime(2024, 4, 13),
                Type = WeatherDataType.BarometricPressure,
                Value = 420.4,
                Unit = "hPa"
            },new WeatherData
            {
                Station = WeatherDataStation.Mythenquai,
                Timestamp = new DateTime(2025, 4, 13),
                Type = WeatherDataType.BarometricPressure,
                Value = 666,
                Unit = "hPa"
            },
            };

            await context.WeatherData.AddRangeAsync(weatherData);
            await context.SaveChangesAsync();

            return repository;
        }

        [Fact]
        public async Task SaveWeatherDataAsync_ShouldSaveData()
        {
            using var context = new WeatherDataContext(_dbContextOptions);
            var repository = new WeatherDataRepository(context);

            var weatherData = new List<WeatherData>(){
            new WeatherData
            {
                Station = WeatherDataStation.Tiefenbrunnen,
                Timestamp = new DateTime(2021, 4, 10),
                Type = WeatherDataType.WaterTemperature,
                Value = 17.69,
                Unit = "°C"
            }};

            await repository.SaveWeatherDataAsync(weatherData);
            var savedData = await context.WeatherData.FirstOrDefaultAsync();

            Assert.NotNull(savedData);
            Assert.Equal(weatherData.First().Station, savedData.Station);
            Assert.Equal(weatherData.First().Timestamp, savedData.Timestamp);
            Assert.Equal(weatherData.First().Type, savedData.Type);
            Assert.Equal(weatherData.First().Value, savedData.Value);
            Assert.Equal(weatherData.First().Unit, savedData.Unit);
        }

        [Fact]
        public async Task GetWeatherDataAsync_ShouldCheckOrderOfTimestamps()
        {
            using var context = new WeatherDataContext(_dbContextOptions);

            var repository = await CreateRepositoryWithData(context);

            await Assert.ThrowsAsync<ArgumentException>(() => repository.GetWeatherDataAsync(DateTime.MaxValue, DateTime.MinValue));

        }

        [Fact]
        public async Task GetWeatherDataAsync_ShouldRespectTimeBoundary()
        {
            using var context = new WeatherDataContext(_dbContextOptions);

            var repository = await CreateRepositoryWithData(context);

            var start = new DateTime(2021, 1, 1);
            var end = new DateTime(2022, 1, 1);

            var data = await repository.GetWeatherDataAsync(start, end);

            data.ToList().ForEach(d => Assert.True(start < d.Timestamp && d.Timestamp < end));
        }

        [Fact]
        public async Task GetWeatherDataAsync_ShouldRespectDesiredStation()
        {

            using var context = new WeatherDataContext(_dbContextOptions);

            var repository = await CreateRepositoryWithData(context);

            var data = await repository.GetWeatherDataAsync(DateTime.MinValue, DateTime.MaxValue, null, WeatherDataStation.Mythenquai);

            data.ToList().ForEach(d => Assert.True(d.Station == WeatherDataStation.Mythenquai));
        }

        [Fact]
        public async Task GetWeatherDataAsync_ShouldRespectDesiredType()
        {
            using var context = new WeatherDataContext(_dbContextOptions);

            var repository = await CreateRepositoryWithData(context);

            var data = await repository.GetWeatherDataAsync(DateTime.MinValue, DateTime.MaxValue, WeatherDataType.AirTemperature, null);

            data.ToList().ForEach(d => Assert.True(d.Type == WeatherDataType.AirTemperature));

        }
    }
}


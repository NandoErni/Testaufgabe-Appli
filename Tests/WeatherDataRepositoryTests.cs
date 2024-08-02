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

            var weatherData = TestUtils.CreateTestsData();

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
                    AirTemperature = new WeatherDataValue()
                    {
                        Value = 11.2,
                        Unit = "°C"
                    },
                    WaterTemperature = new WeatherDataValue()
                    {
                        Value = 2.1,
                        Unit = "°C"
                    },
                    BarometricPressure = new WeatherDataValue()
                    {
                        Value = 910.6,
                        Unit = "hPa"
                    },
                    Humidity = new WeatherDataValue()
                    {
                        Value = 98.7,
                        Unit = "%"
                    },
                
            }};

            await repository.SaveWeatherDataAsync(weatherData);
            var savedData = await context.WeatherData.FirstOrDefaultAsync();

            Assert.NotNull(savedData);
            Assert.Equal(weatherData.First().Station, savedData.Station);
            Assert.Equal(weatherData.First().Timestamp, savedData.Timestamp);
            Assert.Equal(weatherData.First().AirTemperature, savedData.AirTemperature);
            Assert.Equal(weatherData.First().WaterTemperature, savedData.WaterTemperature);
            Assert.Equal(weatherData.First().BarometricPressure, savedData.BarometricPressure);
            Assert.Equal(weatherData.First().Humidity, savedData.Humidity);
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

            var data = await repository.GetWeatherDataAsync(DateTime.MinValue, DateTime.MaxValue, WeatherDataStation.Mythenquai);

            data.ToList().ForEach(d => Assert.True(d.Station == WeatherDataStation.Mythenquai));
        }

        [Fact]
        public async Task RemoveGivenWeatherDataAsync_RemoveData()
        {
            using var context = new WeatherDataContext(_dbContextOptions);

            var repository = await CreateRepositoryWithData(context);

            var data = await repository.GetWeatherDataAsync(DateTime.MinValue, DateTime.MaxValue);
            var dataToDelete = data.Where(d => d.Station == WeatherDataStation.Mythenquai);

            await repository.RemoveGivenWeatherDataAsync(dataToDelete);

            data = await repository.GetWeatherDataAsync(DateTime.MinValue, DateTime.MaxValue);
            data.ToList().ForEach(d => Assert.DoesNotContain(d, dataToDelete));
        }
    }
}


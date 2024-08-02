using System;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using testaufgabe.Data;
using testaufgabe.Repositories;
using testaufgabe.Services;
using testaufgabe.Utils;

namespace Tests
{
	public class WeatherDataServiceTests
	{
		private readonly WeatherDataService _service;


        private readonly WeatherDataRepository _repository;

        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly WeatherDataFetcher _weatherDataFetcher;

        public WeatherDataServiceTests()
		{
            var dbContextOptions = new DbContextOptionsBuilder<WeatherDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _repository = new WeatherDataRepository(new WeatherDataContext(dbContextOptions));

            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _weatherDataFetcher = new WeatherDataFetcher(new HttpClient(_httpMessageHandlerMock.Object));

			_service = new WeatherDataService(_repository, _weatherDataFetcher);
		}

		[Fact]
		public async Task GetWeatherData_ShouldThrow_WhenDateTimesNotValid()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherData(DateTime.MaxValue, DateTime.MinValue, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataMin(DateTime.MaxValue, DateTime.MinValue, null, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataMax(DateTime.MaxValue, DateTime.MinValue, null, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataAvg(DateTime.MaxValue, DateTime.MinValue, null, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataCount(DateTime.MaxValue, DateTime.MinValue, null));
        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnAllData()
        {

        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnMinimalEntry()
        {

        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnMaximalEntry()
        {

        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnAverageOfAllEntries()
        {

        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnNumberOfEntries()
        {

        }
    }
}


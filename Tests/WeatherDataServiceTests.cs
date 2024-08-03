using Microsoft.EntityFrameworkCore;
using Moq;
using testaufgabe.Data;
using testaufgabe.Models;
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
            var weatherData = TestUtils.CreateTestsData();
            var context = new WeatherDataContext(dbContextOptions);

            context.WeatherData.AddRange(weatherData);
            context.SaveChanges();

            _repository = new WeatherDataRepository(context);

            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _weatherDataFetcher = new WeatherDataFetcher(new HttpClient(_httpMessageHandlerMock.Object));

			_service = new WeatherDataService(_repository, _weatherDataFetcher);
		}

		[Fact]
		public async Task GetWeatherData_ShouldThrow_WhenDateTimesNotValid()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherData(DateTime.MaxValue, DateTime.MinValue, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataMin(DateTime.MaxValue, DateTime.MinValue, WeatherDataType.AirTemperature, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataMax(DateTime.MaxValue, DateTime.MinValue, WeatherDataType.AirTemperature, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataAvg(DateTime.MaxValue, DateTime.MinValue, WeatherDataType.AirTemperature, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataCount(DateTime.MaxValue, DateTime.MinValue, null));
        }

        [Fact]
        public async Task GetWeatherData_ShouldHaveValidWeatherDataType()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataMin(DateTime.MinValue, DateTime.MaxValue, null, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataMax(DateTime.MinValue, DateTime.MaxValue, null, null));
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetWeatherDataAvg(DateTime.MinValue, DateTime.MaxValue, null, null));

        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnAllData()
        {
            var weatherData = await _service.GetWeatherData(DateTime.MinValue, DateTime.MaxValue, null);
            Assert.Equal(6, weatherData.Count);
        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnMinimalEntry()
        {
            var weatherData = await _service.GetWeatherDataMin(DateTime.MinValue, DateTime.MaxValue, WeatherDataType.AirTemperature, null);
            Assert.Equal(11.2, weatherData.AirTemperature.Value);
            Assert.Equal("°C", weatherData.AirTemperature.Unit);

            weatherData = await _service.GetWeatherDataMin(DateTime.MinValue, DateTime.MaxValue, WeatherDataType.BarometricPressure, null);
            Assert.Equal(893, weatherData.BarometricPressure.Value);
            Assert.Equal("hPa", weatherData.BarometricPressure.Unit);
        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnMaximalEntry()
        {
            var weatherData = await _service.GetWeatherDataMax(DateTime.MinValue, DateTime.MaxValue, WeatherDataType.AirTemperature, null);
            Assert.Equal(60, weatherData.AirTemperature.Value);
            Assert.Equal("°C", weatherData.AirTemperature.Unit);

            weatherData = await _service.GetWeatherDataMax(DateTime.MinValue, DateTime.MaxValue, WeatherDataType.BarometricPressure, null);
            Assert.Equal(999, weatherData.BarometricPressure.Value);
            Assert.Equal("hPa", weatherData.BarometricPressure.Unit);
        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnAverageOfAllEntries()
        {
            var averageAirTemp = await _service.GetWeatherDataAvg(DateTime.MinValue, DateTime.MaxValue, WeatherDataType.AirTemperature, null);
            Assert.Equal(30, averageAirTemp);
        }

        [Fact]
        public async Task GetWeatherData_ShouldReturnNumberOfEntries()
        {
            var weatherDataCount = await _service.GetWeatherDataCount(DateTime.MinValue, DateTime.MaxValue, null);
            Assert.Equal(6, weatherDataCount);
        }
    }
}


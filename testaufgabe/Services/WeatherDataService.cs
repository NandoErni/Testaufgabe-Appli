using System;
using testaufgabe.Dtos;
using testaufgabe.Models;
using testaufgabe.Repositories;
using testaufgabe.Utils;

namespace testaufgabe.Services
{
	public class WeatherDataService
	{
        private readonly WeatherDataRepository _repository;
        private readonly WeatherDataFetcher _fetcher;

        public WeatherDataService(WeatherDataRepository repository, WeatherDataFetcher fetcher)
        {
            _repository = repository;
            _fetcher = fetcher;
        }

        public async Task FetchAndStoreWeatherDataAsync()
        {
            var data = await _fetcher.FetchSortedWeatherDataAsync(
                DateTime.Today,
                DateTime.Today.Subtract(TimeSpan.FromDays(1)),
                100,
                WeatherStationEnum.Tiefenbrunnen,
                WeatherStationEnum.Mythenquai
                );
            //await _repository.SaveWeatherDataAsync(data);
        }

        public Task<List<WeatherData>> GetWeatherData(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        public Task<List<WeatherData>> GetWeatherDataAvg(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        public Task<List<WeatherData>> GetWeatherDataCount(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        public Task<List<WeatherData>> GetWeatherDataMax(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        public Task<List<WeatherData>> GetWeatherDataMin(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }
    }
}


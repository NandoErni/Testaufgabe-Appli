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
            var data = await _fetcher.FetchWeatherDataAsync(WeatherStationEnum.Tiefenbrunnen, WeatherStationEnum.Mythenquai);
            //await _repository.SaveWeatherDataAsync(data);
        }

        internal Task<List<WeatherData>> GetWeatherData(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        internal Task<List<WeatherData>> GetWeatherDataAvg(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        internal Task<List<WeatherData>> GetWeatherDataCount(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        internal Task<List<WeatherData>> GetWeatherDataMax(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        internal Task<List<WeatherData>> GetWeatherDataMin(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            throw new NotImplementedException();
        }

        internal Task UpdateWeatherData()
        {
            throw new NotImplementedException();
        }
    }
}


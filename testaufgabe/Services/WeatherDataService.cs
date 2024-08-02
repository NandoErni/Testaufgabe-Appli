using System;
using testaufgabe.Dtos;
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
    }
}


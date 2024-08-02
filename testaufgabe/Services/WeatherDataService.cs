using System;
using testaufgabe.Dtos;
using testaufgabe.Models;
using testaufgabe.Repositories;
using testaufgabe.Utils;
using static System.Collections.Specialized.BitVector32;

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
                DateTime.Today.Subtract(TimeSpan.FromDays(1)),
                DateTime.Today.Subtract(TimeSpan.FromDays(2)),
                100,
                WeatherStationEnum.Tiefenbrunnen,
                WeatherStationEnum.Mythenquai
                );

            var dataUsingModels = data.Select(WeatherDataDtoToModel);
            await _repository.SaveWeatherDataAsync(dataUsingModels);
        }

        public Task<List<WeatherData>> GetWeatherData(DateTime start, DateTime end, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            throw new NotImplementedException();
        }

        public Task<List<WeatherData>> GetWeatherDataAvg(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            throw new NotImplementedException();
        }

        public Task<List<WeatherData>> GetWeatherDataCount(DateTime start, DateTime end, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            throw new NotImplementedException();
        }

        public Task<List<WeatherData>> GetWeatherDataMax(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            throw new NotImplementedException();
        }

        public Task<List<WeatherData>> GetWeatherDataMin(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            throw new NotImplementedException();
        }

        private void CheckDateTimes(DateTime start, DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentException($"The start time {start} has to be before the end time {end}");
            }
        }

        private WeatherData WeatherDataDtoToModel(WeatherDataDto dto)
        {
            WeatherDataStation station = dto.Station switch
            {
                WeatherStationEnum.Mythenquai => WeatherDataStation.Mythenquai,
                WeatherStationEnum.Tiefenbrunnen => WeatherDataStation.Tiefenbrunnen,
                _ => throw new ArgumentException($"Unkown Weather data type '{dto.Station}'")

            };




            return new WeatherData()
            {
                Station = station,
                Timestamp = dto.Timestamp,
                AirTemperature = new WeatherDataValue()
                {
                    Value = dto.Values.AirTemperature.Value.Value,
                    Unit = dto.Values.AirTemperature.Unit
                },

                WaterTemperature = new WeatherDataValue()
                {
                    Value = dto.Values.WaterTemperature.Value.Value,
                    Unit = dto.Values.WaterTemperature.Unit
                },

                BarometricPressure = new WeatherDataValue()
                {
                    Value = dto.Values.BarometricPressure.Value.Value,
                    Unit = dto.Values.BarometricPressure.Unit
                },

                Humidity = new WeatherDataValue()
                {
                    Value = dto.Values.Humidity.Value.Value,
                    Unit = dto.Values.Humidity.Unit
                }

            };

        }
    }
}



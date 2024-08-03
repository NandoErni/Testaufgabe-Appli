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
                DateTime.Today.Subtract(TimeSpan.FromDays(2)),
                DateTime.Today.Subtract(TimeSpan.FromDays(1)),
                50,
                WeatherStationEnum.Tiefenbrunnen,
                WeatherStationEnum.Mythenquai
                );

            var dataUsingModels = data.Where(d => d.Timestamp.HasValue).Select(WeatherDataDtoToModel);
            await _repository.SaveUniqueWeatherDataAsync(dataUsingModels);
        }

        public async Task<List<WeatherData>> GetWeatherData(DateTime start, DateTime end, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            return (List<WeatherData>)await _repository.GetWeatherDataAsync(start, end, station);
        }

        public async Task<double> GetWeatherDataAvg(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            CheckWeatherDataType(weatherDataType);


            return await _repository.GetWeatherDataAverageAsync(start, end, weatherDataType.Value, station);
        }

        public async Task<int> GetWeatherDataCount(DateTime start, DateTime end, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            return await _repository.GetWeatherDataCountAsync(start, end, station);
        }

        public async Task<WeatherData> GetWeatherDataMax(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            CheckWeatherDataType(weatherDataType);

            var weatherData = await _repository.GetWeatherDataAsync(start, end, station);

            var maxEntry = weatherDataType switch
            {
                WeatherDataType.AirTemperature => weatherData.MaxBy(w => w.AirTemperature.Value),
                WeatherDataType.WaterTemperature => weatherData.MaxBy(w => w.WaterTemperature.Value),
                WeatherDataType.BarometricPressure => weatherData.MaxBy(w => w.BarometricPressure.Value),
                WeatherDataType.Humidity => weatherData.MaxBy(w => w.Humidity.Value),
                _ => throw new ArgumentException($"Unkown Enum Value {weatherDataType}")
            };

            return maxEntry;
        }

        public async Task<WeatherData> GetWeatherDataMin(DateTime start, DateTime end, WeatherDataType? weatherDataType, WeatherDataStation? station)
        {
            CheckDateTimes(start, end);
            CheckWeatherDataType(weatherDataType);

            var weatherData = await _repository.GetWeatherDataAsync(start, end, station);

            var minEntry = weatherDataType switch
            {
                WeatherDataType.AirTemperature => weatherData.MinBy(w => w.AirTemperature.Value),
                WeatherDataType.WaterTemperature => weatherData.MinBy(w => w.WaterTemperature.Value),
                WeatherDataType.BarometricPressure => weatherData.MinBy(w => w.BarometricPressure.Value),
                WeatherDataType.Humidity => weatherData.MinBy(w => w.Humidity.Value),
                _ => throw new ArgumentException($"Unkown Enum Value {weatherDataType}")
            };

            return minEntry;
        }

        private void CheckDateTimes(DateTime start, DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentException($"The start time {start} has to be before the end time {end}");
            }
        }

        private void CheckWeatherDataType(WeatherDataType? weatherDataType)
        {
            if (!weatherDataType.HasValue)
            {
                throw new ArgumentException("No weather data type provided.");
            }
        }

        private WeatherData WeatherDataDtoToModel(WeatherDataDto dto)
        {
            if (!dto.Timestamp.HasValue)
            {
                throw new ArgumentException("There is no Timestamp");
            }

            WeatherDataStation station = dto.Station switch
            {
                WeatherStationEnum.Mythenquai => WeatherDataStation.Mythenquai,
                WeatherStationEnum.Tiefenbrunnen => WeatherDataStation.Tiefenbrunnen,
                _ => throw new ArgumentException($"Unkown Weather data type '{dto.Station}'")
            };




            return new WeatherData()
            {
                Station = station,
                Timestamp = dto.Timestamp.Value,
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



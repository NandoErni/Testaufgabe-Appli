using System;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using testaufgabe.Dtos;

namespace testaufgabe.Utils
{
	public class WeatherDataFetcher
	{
        private const string _baseUrl = "https://tecdottir.herokuapp.com/measurements/";

        private readonly HttpClient _httpClient;
		public WeatherDataFetcher(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

        public async Task<List<WeatherDataDto>> FetchSortedWeatherDataAsync(DateTime start, DateTime end, int limit, params WeatherStationEnum[] weatherStations)
        {
            var allWeatherData = await Task.WhenAll(
                from weatherStation
                in weatherStations
                select FetchSortedWeatherDataAsync(start, end, limit, weatherStation)
                );
            return allWeatherData.SelectMany(i => i).ToList();
        }

        public async Task<List<WeatherDataDto>> FetchSortedWeatherDataAsync(DateTime start, DateTime end, int limit, WeatherStationEnum weatherStation)
        {

            if (end < start)
            {
                throw new ArgumentException($"The start time {start} has to be before the end time {end}");
            }

            var url = _baseUrl + weatherStation.ToString().ToLower();


            var param = new Dictionary<string, string>
            {
                { "start", start.ToString() },
                { "end", start.ToString() },
                { "sort", "timestamp_cet desc" },
                { "limit",  limit.ToString() },
            };

            var endpoint = new Uri(QueryHelpers.AddQueryString(url, param));

            var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch data: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            var records = json["result"]?.ToObject<List<WeatherDataDto>>();

            if (records == null)
            {
                throw new Exception("Failed to parse weather data.");
            }

            return records;
        }
    }
}


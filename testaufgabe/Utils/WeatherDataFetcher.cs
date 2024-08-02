using System;
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

        public async Task<List<WeatherDataDto>> FetchWeatherDataAsync(WeatherStationEnum weatherStation)
        {
            var url = _baseUrl + weatherStation.ToString().ToLower();
            var response = await _httpClient.GetAsync(url);

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


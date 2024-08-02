using System;
namespace testaufgabe.Utils
{
	public class WeatherDataFetcher
	{
		private readonly HttpClient _httpClient;
		public WeatherDataFetcher(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<WeatherDataDto>> FetchWeatherDataAsync()
		{

		}
	}
}


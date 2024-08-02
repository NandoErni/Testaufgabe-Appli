using System;
namespace testaufgabe.Dtos
{
	public class WeatherDataValuesDto
	{
		public WeatherDataTypeDto AirTemperature { get; set; }

		public WeatherDataTypeDto WaterTemperature { get; set; }

		public WeatherDataTypeDto BarometricPressure { get; set; }

		public WeatherDataTypeDto Humidity { get; set; }
	}
}


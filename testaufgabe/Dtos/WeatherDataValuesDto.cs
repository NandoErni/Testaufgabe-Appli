using System;
using Newtonsoft.Json;

namespace testaufgabe.Dtos
{
	public class WeatherDataValuesDto
	{
        [JsonProperty("air_temperature")]
        public required WeatherDataTypeDto AirTemperature { get; set; }

        [JsonProperty("water_temperature")]
        public required WeatherDataTypeDto WaterTemperature { get; set; }

        [JsonProperty("barometric_pressure_qfe")]
        public required WeatherDataTypeDto BarometricPressure { get; set; }

        [JsonProperty("humidity")]
        public required WeatherDataTypeDto Humidity { get; set; }
	}
}


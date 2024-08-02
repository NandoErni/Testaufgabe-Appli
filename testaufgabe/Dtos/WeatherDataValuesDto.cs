using System;
using Newtonsoft.Json;

namespace testaufgabe.Dtos
{
	public class WeatherDataValuesDto
	{
        [JsonProperty("air_temperature")]
        public WeatherDataTypeDto AirTemperature { get; set; }

        [JsonProperty("water_temperature")]
        public WeatherDataTypeDto WaterTemperature { get; set; }

        [JsonProperty("barometric_pressure_qfe")]
        public WeatherDataTypeDto BarometricPressure { get; set; }

        [JsonProperty("humidity")]
        public WeatherDataTypeDto Humidity { get; set; }
	}
}


using System;
namespace testaufgabe.Dtos
{
	public class WeatherDataDto
	{
        public WeatherStationEnum Station { get; set; }
        public DateTime? Timestamp { get; set; }
        public required WeatherDataValuesDto Values { get; set; }
    }
}


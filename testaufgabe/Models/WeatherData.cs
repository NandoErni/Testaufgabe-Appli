using System;
using Microsoft.EntityFrameworkCore;

namespace testaufgabe.Models
{
    [PrimaryKey(nameof(Station), nameof(Timestamp))]
    public class WeatherData
	{
        public WeatherDataStation Station;
        public DateTime Timestamp;

        public required WeatherDataValue AirTemperature;

        public required WeatherDataValue WaterTemperature;

        public required WeatherDataValue BarometricPressure;

        public required WeatherDataValue Humidity;
    }
}
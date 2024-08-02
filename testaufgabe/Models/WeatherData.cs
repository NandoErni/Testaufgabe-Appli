using System;
using Microsoft.EntityFrameworkCore;

namespace testaufgabe.Models
{
    [PrimaryKey(nameof(Station), nameof(Timestamp))]
    public class WeatherData
	{
        public WeatherDataStation Station;
        public DateTime Timestamp;

        public WeatherDataValue AirTemperature;

        public WeatherDataValue WaterTemperature;

        public WeatherDataValue BarometricPressure;

        public WeatherDataValue Humidity;
    }
}
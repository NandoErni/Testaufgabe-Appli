using System;
using Microsoft.EntityFrameworkCore;

namespace testaufgabe.Models
{
    [PrimaryKey(nameof(Station), nameof(Timestamp), nameof(Type))]
    public class WeatherData
	{
        public WeatherDataStation Station { get; set; }
        public DateTime Timestamp { get; set; }
        public WeatherDataType Type { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
    }
}


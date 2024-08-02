using System;
using testaufgabe.Models;

namespace Tests
{
    public class TestUtils
    {
        public static List<WeatherData> CreateTestsData()
        {
            return new List<WeatherData>(){
            new WeatherData
            {
                Station = WeatherDataStation.Tiefenbrunnen,
                Timestamp = new DateTime(2021, 4, 13),
                    AirTemperature = new WeatherDataValue()
                    {
                        Value = 17.69,
                        Unit = "°C"
                    },
                    WaterTemperature = new WeatherDataValue()
                    {
                        Value = 17.69,
                        Unit = "°C"
                    },
                    BarometricPressure = new WeatherDataValue()
                    {
                        Value = 940.5,
                        Unit = "hPa"
                    },
                    Humidity = new WeatherDataValue()
                    {
                        Value = 22,
                        Unit = "%"
                    },

            },new WeatherData
            {
                Station = WeatherDataStation.Mythenquai,
                Timestamp = new DateTime(2020, 4, 13),
                    AirTemperature = new WeatherDataValue()
                    {
                        Value = 23.3,
                        Unit = "°C"
                    },
                    WaterTemperature = new WeatherDataValue()
                    {
                        Value = 10.1,
                        Unit = "°C"
                    },
                    BarometricPressure = new WeatherDataValue()
                    {
                        Value = 999,
                        Unit = "hPa"
                    },
                    Humidity = new WeatherDataValue()
                    {
                        Value = 14.3,
                        Unit = "%"
                    },

            },new WeatherData
            {
                Station = WeatherDataStation.Tiefenbrunnen,
                Timestamp = new DateTime(2022, 4, 13),
                    AirTemperature = new WeatherDataValue()
                    {
                        Value = 34.2,
                        Unit = "°C"
                    },
                    WaterTemperature = new WeatherDataValue()
                    {
                        Value = 5.5,
                        Unit = "°C"
                    },
                    BarometricPressure = new WeatherDataValue()
                    {
                        Value = 893,
                        Unit = "hPa"
                    },
                    Humidity = new WeatherDataValue()
                    {
                        Value = 10,
                        Unit = "%"
                    },

            },new WeatherData
            {
                Station = WeatherDataStation.Mythenquai,
                Timestamp = new DateTime(2023, 4, 13),
                    AirTemperature = new WeatherDataValue()
                    {
                        Value = 60,
                        Unit = "°C"
                    },
                    WaterTemperature = new WeatherDataValue()
                    {
                        Value = 5,
                        Unit = "°C"
                    },
                    BarometricPressure = new WeatherDataValue()
                    {
                        Value = 900,
                        Unit = "hPa"
                    },
                    Humidity = new WeatherDataValue()
                    {
                        Value = 5,
                        Unit = "%"
                    },

            },new WeatherData
            {
                Station = WeatherDataStation.Tiefenbrunnen,
                Timestamp = new DateTime(2024, 4, 13),
                    AirTemperature = new WeatherDataValue()
                    {
                        Value = 44.12,
                        Unit = "°C"
                    },
                    WaterTemperature = new WeatherDataValue()
                    {
                        Value = 23.3,
                        Unit = "°C"
                    },
                    BarometricPressure = new WeatherDataValue()
                    {
                        Value = 922.1,
                        Unit = "hPa"
                    },
                    Humidity = new WeatherDataValue()
                    {
                        Value = 20.1,
                        Unit = "%"
                    },

            },new WeatherData
            {
                Station = WeatherDataStation.Mythenquai,
                Timestamp = new DateTime(2025, 4, 13),
                    AirTemperature = new WeatherDataValue()
                    {
                        Value = 11.2,
                        Unit = "°C"
                    },
                    WaterTemperature = new WeatherDataValue()
                    {
                        Value = 2.1,
                        Unit = "°C"
                    },
                    BarometricPressure = new WeatherDataValue()
                    {
                        Value = 910.6,
                        Unit = "hPa"
                    },
                    Humidity = new WeatherDataValue()
                    {
                        Value = 98.7,
                        Unit = "%"
                    },

            },
            };
        }
    }
}


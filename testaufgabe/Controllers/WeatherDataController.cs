using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testaufgabe.Models;
using testaufgabe.Services;
using static System.Collections.Specialized.BitVector32;

namespace testaufgabe.Controllers
{

    [ApiController]
    [Route("api/weatherdata")]
    public class WeatherDataController : Controller
    {

        private readonly WeatherDataService _service;

        public WeatherDataController(WeatherDataService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherData(DateTime start, DateTime end, WeatherDataStation? station = null)
        {
            List<WeatherData> weatherData;
            try
            {
                weatherData = await _service.GetWeatherData(start, end, station);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(weatherData);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWeatherData()
        {
            await _service.FetchAndStoreWeatherDataAsync();

            return Ok();
        }

        [HttpGet("min")]
        public async Task<IActionResult> GetWeatherDataMin(DateTime start, DateTime end, WeatherDataType? weatherDataType = null, WeatherDataStation? station = null)
        {
            WeatherData weatherData;
            try
            {
                weatherData = await _service.GetWeatherDataMin(start, end, weatherDataType, station);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(weatherData);
        }

        [HttpGet("max")]
        public async Task<IActionResult> GetWeatherDataMax(DateTime start, DateTime end, WeatherDataType? weatherDataType = null, WeatherDataStation? station = null)
        {
            WeatherData weatherData;
            try
            {
                weatherData = await _service.GetWeatherDataMax(start, end, weatherDataType, station);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(weatherData);
        }

        [HttpGet("avg")]
        public async Task<IActionResult> GetWeatherDataAvg(DateTime start, DateTime end, WeatherDataType? weatherDataType = null, WeatherDataStation? station = null)
        {
            double averageWeatherData;
            try
            {
                averageWeatherData = await _service.GetWeatherDataAvg(start, end, weatherDataType, station);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(averageWeatherData);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetWeatherDataCount(DateTime start, DateTime end, WeatherDataStation? station = null)
        {
            int weatherDataCount;
            try
            {
                weatherDataCount = await _service.GetWeatherDataCount(start, end, station);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(weatherDataCount);
        }
    }
}


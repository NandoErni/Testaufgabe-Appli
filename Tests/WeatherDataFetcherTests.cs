

using System;
using System.Net;
using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using testaufgabe.Dtos;
using testaufgabe.Utils;
namespace Tests;

public class WeatherDataFetcherTests
{

    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly WeatherDataFetcher _weatherDataFetcher;

    public WeatherDataFetcherTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _weatherDataFetcher = new WeatherDataFetcher(_httpClient);
    }

    private void SetupHttpMessageHandlerMock(HttpResponseMessage desiredResponseMessage)
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                nameof(HttpClient.SendAsync),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(desiredResponseMessage);
    }

    [Fact]
    public async Task FetchWeatherDataAsync_ShouldThrowHttpRequestException_WhenRequestFails()
    {



        SetupHttpMessageHandlerMock(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            });

        await Assert.ThrowsAsync<HttpRequestException>(async () =>
            await _weatherDataFetcher.FetchSortedWeatherDataAsync(DateTime.MinValue, DateTime.MaxValue, int.MaxValue, WeatherStationEnum.Tiefenbrunnen));
    }

    [Fact]
    public async Task FetchWeatherDataAsync_ShouldReturnListOfWeatherDataDto()
    {
        var jsonResponseTiefenbrunnen = new JObject
        {
            ["result"] = new JArray
            {
                   new JObject
                        {
                            ["station"] = "tiefenbrunnen",
                            ["timestamp"] = "2024-08-01T00:00:00Z",
                            ["values"] = new JObject
                            {
                                ["air_temperature"] = new JObject
                                {
                                    ["value"] = 17.4,
                                    ["unit"] = "°C",
                                    ["status"] = "ok"
                                },
                                ["water_temperature"] = new JObject
                                {
                                    ["value"] = 11.9,
                                    ["unit"] = "°C",
                                    ["status"] = "ok"
                                },
                                ["barometric_pressure_qfe"] = new JObject
                                {
                                    ["value"] = 973.2,
                                    ["unit"] = "hPa",
                                    ["status"] = "ok"
                                },
                                ["humidity"] = new JObject
                                {
                                    ["value"] = 59,
                                    ["unit"] = "%",
                                    ["status"] = "ok"
                                },
                            }
                        },
                        new JObject
                        {
                            ["station"] = "tiefenbrunnen",
                            ["timestamp"] = "2024-08-01T00:00:00Z",
                            ["values"] = new JObject
                            {
                                ["air_temperature"] = new JObject
                                {
                                    ["value"] = 17.4,
                                    ["unit"] = "°C",
                                    ["status"] = "ok"
                                },
                                ["water_temperature"] = new JObject
                                {
                                    ["value"] = 11.9,
                                    ["unit"] = "°C",
                                    ["status"] = "ok"
                                },
                                ["barometric_pressure_qfe"] = new JObject
                                {
                                    ["value"] = 973.2,
                                    ["unit"] = "hPa",
                                    ["status"] = "ok"
                                },
                                ["humidity"] = new JObject
                                {
                                    ["value"] = 59,
                                    ["unit"] = "%",
                                    ["status"] = "ok"
                                },
                            }
                        },
                        new JObject
                        {
                            ["station"] = "tiefenbrunnen",
                            ["timestamp"] = "2024-08-01T00:00:00Z",
                            ["values"] = new JObject
                            {
                                ["air_temperature"] = new JObject
                                {
                                    ["value"] = null,
                                    ["unit"] = "°C",
                                    ["status"] = "ok"
                                },
                                ["water_temperature"] = new JObject
                                {
                                    ["value"] = 11.9,
                                    ["unit"] = null,
                                    ["status"] = "ok"
                                },
                                ["barometric_pressure_qfe"] = new JObject
                                {
                                    ["value"] = null,
                                    ["unit"] = null,
                                    ["status"] = "ok"
                                },
                                ["humidity"] = new JObject
                                {
                                    ["value"] = null,
                                    ["unit"] = "%",
                                    ["status"] = "ok"
                                },
                            }
                        }
                }
        };

        SetupHttpMessageHandlerMock(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponseTiefenbrunnen.ToString())
            });

        var result = await _weatherDataFetcher.FetchSortedWeatherDataAsync(DateTime.MinValue, DateTime.MaxValue, int.MaxValue, WeatherStationEnum.Tiefenbrunnen);

        Assert.NotNull(result);

        Assert.Equal(WeatherStationEnum.Tiefenbrunnen, result[0].Station);
        Assert.Equal(new DateTime(2024, 8, 1, 0, 0, 0, DateTimeKind.Utc), result[0].Timestamp);
        Assert.Equal(17.4, result[0].Values.AirTemperature.Value);
        Assert.Equal("°C", result[0].Values.WaterTemperature.Unit);
        Assert.Equal(59, result[1].Values.Humidity.Value);
        Assert.Equal("%", result[1].Values.Humidity.Unit);
    }

    [Fact]
    public async Task FetchWeatherDataAsync_ShouldThrowException_WhenParsingFails()
    {
        SetupHttpMessageHandlerMock(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("invalid json")
            });

        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await _weatherDataFetcher.FetchSortedWeatherDataAsync(DateTime.MinValue, DateTime.MaxValue, int.MaxValue, WeatherStationEnum.Tiefenbrunnen));
    }

    [Fact]
    public async Task FetchWeatherDataAsync_ShouldThrowException_WhenDatetimesAreIncorrect()
    {
        SetupHttpMessageHandlerMock(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("invalid json")
        });

        await Assert.ThrowsAnyAsync<ArgumentException>(async () =>
            await _weatherDataFetcher.FetchSortedWeatherDataAsync(DateTime.MaxValue, DateTime.MinValue, int.MaxValue, WeatherStationEnum.Tiefenbrunnen));
    }
}




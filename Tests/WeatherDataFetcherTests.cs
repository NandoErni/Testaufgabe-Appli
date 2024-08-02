

using System;
using System.Net;
using Moq;
using Moq.Protected;
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

        [Fact]
        public async Task FetchWeatherDataAsync_ShouldThrowHttpRequestException_WhenRequestFails()
        {



            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    nameof(HttpClient.SendAsync),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _weatherDataFetcher.FetchWeatherDataAsync(WeatherStationEnum.Tiefenbrunnen));
        }
    }




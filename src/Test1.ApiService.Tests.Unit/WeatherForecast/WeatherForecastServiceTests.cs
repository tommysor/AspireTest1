using Microsoft.Extensions.Time.Testing;
using Test1.ApiService.Features.WeatherForecastFeature;

namespace Test1.ApiService.Tests.Unit.WeatherForecast
{
    public class WeatherForecastServiceTests
    {
        private readonly WeatherForecastService _sut;
        private readonly FakeTimeProvider _timeProviderMock;

        public WeatherForecastServiceTests()
        {
            _timeProviderMock = new FakeTimeProvider();
            _timeProviderMock.SetUtcNow(DateTimeOffset.UtcNow);
            _sut = new WeatherForecastService(_timeProviderMock);
        }

        [Fact]
        public async Task ShouldReturnForecasts()
        {
            var actual = await _sut.GetForecast();
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
        }

        [Fact]
        public async Task ShouldReturnForecastForNextDay()
        {
            var actual = await _sut.GetForecast();

            var nextDay = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            Assert.Contains(actual, x => x.Date == nextDay);
        }

        [Fact]
        public async Task ShouldNotReturnForecastForThePast()
        {
            var actual = await _sut.GetForecast();

            var today = DateOnly.FromDateTime(DateTime.Today);
            Assert.DoesNotContain(actual, x => x.Date < today);
        }
    }
}

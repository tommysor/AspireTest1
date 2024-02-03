using DomainSpesificLanguage;

namespace Spesification.Stories;

/// <summary>
/// As a user
/// I want to get the weather forecast
/// So that I can plan for tomorrow
/// </summary>
public class WeatherForecastStory : IClassFixture<UserFixture>
{
    private readonly User _user;

    public WeatherForecastStory(UserFixture userFixture)
    {
        _user = userFixture.User;
    }

    [Fact]
    public async Task ShouldBeAbleToGetWeatherForecast()
    {
        // When
        await _user.GetWeatherForecast();
        // Then
        await _user.AssertHaveWeatherForecast();
    }
}

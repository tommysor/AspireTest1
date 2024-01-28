using spesification.Dsl;
using spesification.StorySetup;

namespace spesification.Stories;

public class WeatherIsDisplayed : IClassFixture<VisitorSetup>
{
    private readonly Visitor _visitor;

    public WeatherIsDisplayed(VisitorSetup visitorSetup)
    {
        // As a visitor
        _visitor = visitorSetup.Visitor;
        // I want to know the weather
        // So that I can plan my day
    }

    [Fact]
    public async Task AskingForWeather_ShouldGiveTheWeather()
    {
        // Given
        // When
        await _visitor.AskForWeather();
        // Then
        await _visitor.HasTheWeather();
    }

    [Fact]
    public void TEST_TEST_TEST_FailTheTest()
    {
        Assert.Fail("TEST_TEST_TEST");
    }
}

namespace Test1.ApiService.Tests.Integration;

public class WeatherForecast : IClassFixture<ApiServiceServer>
{
    private readonly HttpClient _client;

    public WeatherForecast(ApiServiceServer server)
    {
        _client = server.CreateClientAnonymous();
    }

    [Fact]
    public async Task ShouldGetWeatherForecast()
    {
        var response = await _client.GetAsync("/weatherforecast");

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(responseString);
    }
}

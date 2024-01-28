using spesification.Dsl;

namespace spesification.Adapters;

public sealed class HttpClientAdapter : IAdapter
{
    private readonly HttpClient _client;

    public HttpClientAdapter()
    {
        var baseAddress = Environment.GetEnvironmentVariable("SPESIFICATIONS_BASEADDRESS") ?? throw new InvalidOperationException("SPESIFICATIONS_BASEADDRESS not found");
        var handler = new SocketsHttpHandler();
        _client = new HttpClient(handler)
        {
            // "https://webfrontend.kindmushroom-c0497470.norwayeast.azurecontainerapps.io/"
            BaseAddress = new Uri(baseAddress),
        };
    }

    public async Task<object> AskForWeather()
    {
        var response = await _client.GetAsync("/weather");
        response.EnsureSuccessStatusCode();
        return response;
    }

    public async Task VerifyWeather(object? weather)
    {
        if (weather is not HttpResponseMessage response)
        {
            Assert.Fail($"Unexpected. Weather was not of type {nameof(HttpResponseMessage)}");
            return;
        }

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Weather", content);
        Assert.Contains("Temp. (C)", content);
    }
}

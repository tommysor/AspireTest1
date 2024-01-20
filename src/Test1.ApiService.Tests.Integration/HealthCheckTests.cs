using System.Net;

namespace Test1.ApiService.Tests.Integration;

public class HealthCheckTests : IClassFixture<ApiServiceServer>
{
    private readonly ApiServiceServer _server;
    private readonly HttpClient _client;

    public HealthCheckTests(ApiServiceServer server)
    {
        _server = server;
        _client = _server.CreateClientAnonymous();
    }

    [Fact]
    public async Task LiveEndpoint_ReturnsOk()
    {
        var response = await _client.GetAsync("/alive");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }

    [Fact]
    public async Task HealthEndpoint_ReturnsOk()
    {
        var response = await _client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }
}
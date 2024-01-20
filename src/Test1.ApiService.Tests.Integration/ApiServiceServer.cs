using Microsoft.AspNetCore.Mvc.Testing;

namespace Test1.ApiService.Tests.Integration;

public sealed class ApiServiceServer : IDisposable
{
    private WebApplicationFactory<IApiServiceAssemblyMarker> _webApplicationFactory = null!;

    public ApiServiceServer()
    {
        _webApplicationFactory = new WebApplicationFactory<IApiServiceAssemblyMarker>();
    }

    public HttpClient CreateClientAnonymous()
        => _webApplicationFactory.CreateClient();

    void IDisposable.Dispose()
    {
        if (_webApplicationFactory is not null)
        {
            _webApplicationFactory.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}

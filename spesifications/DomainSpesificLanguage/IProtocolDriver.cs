namespace DomainSpesificLanguage;

public interface IProtocolDriver : IAsyncDisposable
{
    Task Initialize();
    Task GetWeatherForecast();
    Task AssertHaveWeatherForecast();
}

namespace DomainSpesificLanguage;

public interface IProtocolDriver : IAsyncDisposable
{
    Task Initialize();
    Task GetWeatherForecast();
    Task AssertHaveWeatherForecast();
    Task GoToSearchTest();
    Task AssertSearchTestHaveTestData();
}

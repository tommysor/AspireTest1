namespace DomainSpesificLanguage;

public class User
{
    private readonly IProtocolDriver _protocolDriver;

    public User(IProtocolDriver protocolDriver)
    {
        _protocolDriver = protocolDriver;
    }

    public async Task GetWeatherForecast()
    {
        await _protocolDriver.GetWeatherForecast();
    }

    public async Task AssertHaveWeatherForecast()
    {
        await _protocolDriver.AssertHaveWeatherForecast();
    }

    public Task GoToSearchTest()
    {
        throw new NotImplementedException();
    }

    public Task AssertSearchTestHaveTestData()
    {
        throw new NotImplementedException();
    }
}

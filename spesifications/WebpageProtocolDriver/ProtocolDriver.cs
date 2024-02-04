using System.Threading.Tasks;
using Microsoft.Playwright;
using DomainSpesificLanguage;

namespace WebpageProtocolDriver;

public class ProtocolDriver : IProtocolDriver
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;
    private string _baseAddress = null!;
    private WeatherForecastDriver _weatherForecastDriver = null!;

    public async Task Initialize()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = true });
        _page = await _browser.NewPageAsync();
        _baseAddress = Environment.GetEnvironmentVariable("SPESIFICATIONS_BASEADDRESS") ?? throw new InvalidOperationException("SPESIFICATIONS_BASEADDRESS not found");
        var isContainsSchema = _baseAddress.Contains("http://") || _baseAddress.Contains("https://");
        if (!isContainsSchema)
        {
            _baseAddress = $"https://{_baseAddress}";
        }
        await _page.GotoAsync(_baseAddress);
        _weatherForecastDriver = new WeatherForecastDriver(_page);
    }

    public async ValueTask DisposeAsync()
    {
        if (_browser is not null)
        {
            await _browser.DisposeAsync();
        }
        _playwright?.Dispose();
    }

    public Task GetWeatherForecast() => _weatherForecastDriver.GetWeatherForecast();
    public Task AssertHaveWeatherForecast() => _weatherForecastDriver.AssertHaveWeatherForecast();
}

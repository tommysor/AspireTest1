using Microsoft.Playwright;
using DomainSpesificLanguage;

namespace WebpageProtocolDriver;

internal class WeatherForecastDriver
{
    private readonly IPage _page;

    public WeatherForecastDriver(IPage page)
    {
        _page = page;
    }

    public async Task GetWeatherForecast()
    {
        var weatherLink = await _page.WaitForSelectorAsync("a[href='weather']")
            ?? throw new AssertionException("Weather link not found");
        await weatherLink.ClickAsync();
    }

    public async Task AssertHaveWeatherForecast()
    {
        var table = await _page.WaitForSelectorAsync(".table")
            ?? throw new AssertionException("WeatherForecast table not found");
        
        string[] colNrTexts = [
            "Date",
            "Temp. (C)",
            "Temp. (F)",
            "Summary",
        ];
        for (var colNr = 0; colNr < 4; colNr++)
        {
            var columnHeaderSelector = $"thead tr th:nth-child({colNr + 1})";
            var headerCellTempC = await table.WaitForSelectorAsync(columnHeaderSelector)
                ?? throw new AssertionException($"Column header not found. '{columnHeaderSelector}'");
            
            var innerHtml = await headerCellTempC.InnerHTMLAsync();
            var expected = colNrTexts[colNr];
            if (innerHtml != expected)
            {
                throw new AssertionException($"Column nr {colNr} content. Actual: '{innerHtml}' Expected: '{expected}'");
            }
        }
    }
}

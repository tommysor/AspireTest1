using Microsoft.Playwright;
using DomainSpesificLanguage;

namespace WebpageProtocolDriver;

public class SearchTestDriver (IPage _page)
{
    public async Task GoToSearchTest()
    {
        //todo: update to use link when launched, current direct url dark launch
        await _page.GotoAsync("/SearchTest");
    }

    public Task AssertSearchTestHaveTestData()
    {
        throw new AssertionException("AssertSearchTestHaveTestData: Not implemented");
    }
}

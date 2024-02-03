using DomainSpesificLanguage;
using WebpageProtocolDriver;

namespace Spesification;

public class UserFixture : IAsyncLifetime
{
    public User User { get; private set; } = null!;
    private IProtocolDriver _protocolDriver = null!;

    async Task IAsyncLifetime.InitializeAsync()
    {
        _protocolDriver = new ProtocolDriver();
        await _protocolDriver.Initialize();
        User = new User(_protocolDriver);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _protocolDriver.DisposeAsync();
    }
}

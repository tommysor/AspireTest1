namespace spesification.Dsl;

public sealed class Visitor
{
    private readonly IAdapter _adapter;
    private object? _lastReceived = null;

    public Visitor(IAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task AskForWeather()
    {
        _lastReceived = await _adapter.AskForWeather();
    }

    public async Task HasTheWeather()
    {
        await _adapter.VerifyWeather(_lastReceived);
    }
}

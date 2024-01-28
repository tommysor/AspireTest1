namespace spesification.Dsl;

public interface IAdapter
{
    Task<object> AskForWeather();
    Task VerifyWeather(object? weather);
}

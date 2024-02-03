using Test1.ApiService.Features.WeatherForecastFeature.Contracts;

namespace Test1.ApiService.Features.WeatherForecastFeature
{
    public sealed class WeatherForecastService
    {
        private readonly TimeProvider _timeProvider;

        public WeatherForecastService(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public async Task<WeatherForecast[]> GetForecast()
        {
            await Task.CompletedTask;
            
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            return Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(_timeProvider.GetLocalNow().AddDays(index).Date),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
        }
    }
}

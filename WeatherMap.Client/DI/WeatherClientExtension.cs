using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherMap.Client.Configuration;


namespace WeatherMap.Client.DI
{
    public static class WeatherMapClientExtension
    {
        public static IServiceCollection AddWeatherMapClient(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var weatherSection = configuration.GetSection(nameof(WeatherMapConfiguration));
            serviceCollection.Configure<WeatherMapConfiguration>(options => weatherSection.Bind(options));
            var weatherConfiguration = weatherSection.Get<WeatherMapConfiguration>();
            serviceCollection.AddHttpClient<IWeatherMapClient, WeatherMapClient>(client =>
                client.BaseAddress = new Uri(weatherConfiguration.Url));
            return serviceCollection;
        }
    }
}

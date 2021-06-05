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
            var weatherConfiguration = configuration.GetSection(nameof(WeatherMapConfiguration)).Get<WeatherMapConfiguration>();
            serviceCollection.AddHttpClient<IWeatherMapClient, WeatherMapClient>(client =>
                client.BaseAddress = new Uri(weatherConfiguration.Url));
            return serviceCollection;
        }
    }
}

using AutoMapper;
using WeatherService.Profiles;

namespace WeatherService.Extensions
{
    internal static class AutoMapperExtension
    {
        internal static string GetCityName(this ResolutionContext context)
        {
            return context.Items.TryGetValue(WeatherProfileConst.CityNameKey, out var cityName) ? cityName.ToString() : string.Empty;
        }

        internal static string GetMetric(this ResolutionContext context)
        {
            return context.Items.TryGetValue(WeatherProfileConst.MetricKey, out var metric) ? metric.ToString() : string.Empty;
        }
    }
}

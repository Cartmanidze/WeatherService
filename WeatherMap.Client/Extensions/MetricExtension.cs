using System;
using WeatherMap.Client.Enums;

namespace WeatherMap.Client.Extensions
{
    internal static class MetricExtension
    {
        internal static string GetMetricForRequest(this Metric metric)
        {
            return metric switch
            {
                Metric.Celsius => "metric",
                Metric.Fahrenheit => "imperial",
                _ => throw new ArgumentOutOfRangeException(nameof(metric), metric, null)
            };
        }
    }
}

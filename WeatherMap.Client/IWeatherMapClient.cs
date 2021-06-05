using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherMap.Client.Entities;
using WeatherMap.Client.Enums;

namespace WeatherMap.Client
{
    public interface IWeatherMapClient
    {
        Task<IEnumerable<TemperatureWithDate>> GetWeatherForFiveDaysAsync(string cityName, Metric metric);

        Task<Wind> GetWindDataAsync(string cityName);

        Task<Temperature> GetWeatherAsync(string cityName, Metric metric);
    }
}

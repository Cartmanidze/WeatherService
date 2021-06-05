using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherMap.Client.Configuration;
using WeatherMap.Client.Entities;
using WeatherMap.Client.Enums;
using WeatherMap.Client.Extensions;

namespace WeatherMap.Client
{
    internal class WeatherMapClient : IWeatherMapClient
    {
        private readonly HttpClient _client;

        private readonly WeatherMapConfiguration _weatherMapConfiguration;

        public WeatherMapClient(HttpClient client, IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _weatherMapConfiguration = configuration.GetSection(nameof(WeatherMapConfiguration)).Get<WeatherMapConfiguration>();
        }

        public async Task<IEnumerable<TemperatureWithDate>> GetWeatherForFiveDaysAsync(string cityName, Metric metric)
        {
            if (string.IsNullOrEmpty(cityName)) throw new ArgumentNullException(nameof(cityName));
            var metricValue = metric.GetMetricForRequest();
            var query = $"/data/2.5/forecast?q={cityName}&units={metricValue}&appid={_weatherMapConfiguration.Token}";
            var content = await GetContentAsync(query).ConfigureAwait(false);
            var weatherForFiveDays = JsonConvert.DeserializeObject<List<TemperatureWithDate>>(content);
            return weatherForFiveDays;
        }

        public async Task<Wind> GetWindDataAsync(string cityName)
        {
            if (string.IsNullOrEmpty(cityName)) throw new ArgumentNullException(nameof(cityName));
            var query = $"/data/2.5/weather?q={cityName}&appid={_weatherMapConfiguration.Token}";
            var content = await GetContentAsync(query).ConfigureAwait(false);
            var wind = JsonConvert.DeserializeObject<Wind>(content);
            return wind;
        }

        public async Task<Temperature> GetWeatherAsync(string cityName, Metric metric)
        {
            if (string.IsNullOrEmpty(cityName)) throw new ArgumentNullException(nameof(cityName));
            var metricValue = metric.GetMetricForRequest();
            var query = $"/data/2.5/weather?q={cityName}&units={metricValue}&appid={_weatherMapConfiguration.Token}";
            var content = await GetContentAsync(query).ConfigureAwait(false);
            var temperature = JsonConvert.DeserializeObject<Temperature>(content);
            return temperature;
        }

        private async Task<string> GetContentAsync(string query)
        {
            var response = await _client
                .GetAsync(query).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
        }
    }
}

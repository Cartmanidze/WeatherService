using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WeatherMap.Client;
using WeatherMap.Client.Entities;
using WeatherMap.Client.Enums;
using WeatherService.Models;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherMapClient _weatherMapClient;

        private readonly ILogger<WeatherController> _logger;

        private readonly IMapper _mapper;

        public WeatherController(ILogger<WeatherController> logger, IWeatherMapClient weatherMapClient, IMapper mapper)
        {
            _logger = logger;
            _weatherMapClient = weatherMapClient;
            _mapper = mapper;
        }

        [HttpGet("temperature/{cityName}/{metric}")]
        public async Task<ActionResult> GetTemperature(string cityName, string metric)
        {
            if (string.IsNullOrEmpty(cityName)) return BadRequest("parameter cityName is null or empty");
            if (string.IsNullOrEmpty(metric)) return BadRequest("parameter metric is null or empty");
            var metricValue = GetMetricValue(metric);
            if (metricValue == null) return BadRequest("parameter metric is not correct");
            var temperature = await _weatherMapClient.GetWeatherAsync(cityName, metricValue.Value).ConfigureAwait(false);
            var weatherTemperatureResponse = _mapper.Map<Temperature, WeatherTemperatureResponse>(temperature,
                opts => opts.AfterMap((_, dest) =>
                {
                    dest.CityName = cityName;
                    dest.Metric = metric;
                }));
            return Ok(weatherTemperatureResponse);

        }

        [HttpGet("wind/{cityName}")]
        public async Task<ActionResult> GetWind(string cityName)
        {
            if (string.IsNullOrEmpty(cityName)) return BadRequest("parameter cityName is null or empty");
            var wind = await _weatherMapClient.GetWindDataAsync(cityName).ConfigureAwait(false);
            var weatherWindResponse = _mapper.Map<Wind, WeatherWindResponse>(wind,
                opts => opts.AfterMap((_, dest) =>
                {
                    dest.CityName = cityName;
                }));
            return Ok(weatherWindResponse);

        }


        [HttpGet("{cityName}/future/{metric}")]
        public async Task<ActionResult> GetWind(string cityName, string metric)
        {
            if (string.IsNullOrEmpty(cityName)) return BadRequest("parameter cityName is null or empty");
            if (string.IsNullOrEmpty(metric)) return BadRequest("parameter metric is null or empty");
            var metricValue = GetMetricValue(metric);
            if (metricValue == null) return BadRequest("parameter metric is not correct");
            var wind = await _weatherMapClient.GetWeatherForFiveDaysAsync(cityName, metricValue.Value).ConfigureAwait(false);
            var weatherWindResponse = _mapper.Map<IEnumerable<TemperatureWithDate>, IEnumerable<WeatherTemperatureWithDateResponse>>(wind,
                opts => opts.AfterMap((_, dest) =>
                {
                    foreach (var item in dest)
                    {
                        item.CityName = cityName;
                    }
                }));
            return Ok(weatherWindResponse);

        }

        private static Metric? GetMetricValue(string metric)
        {
            return metric switch
            {
                "celsius" => Metric.Celsius,
                "fahrenheit" => Metric.Fahrenheit,
                _ => null
            };
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WeatherMap.Client;
using WeatherMap.Client.Entities;
using WeatherMap.Client.Enums;
using WeatherService.Models;
using WeatherService.Profiles;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherMapClient _weatherMapClient;

        private readonly IMapper _mapper;

        public WeatherController(IWeatherMapClient weatherMapClient, IMapper mapper)
        {
            _weatherMapClient = weatherMapClient;
            _mapper = mapper;
        }

        [HttpGet("temperature/{cityName}/{metric}")]
        public async Task<ActionResult> GetTemperature(string cityName, string metric)
        {
            if (string.IsNullOrEmpty(cityName)) return BadRequest("parameter cityName is null or empty");
            if (string.IsNullOrEmpty(metric)) return BadRequest("parameter metric is null or empty");
            if (!Enum.TryParse(metric, true, out Metric metricValue)) return BadRequest("parameter metric is not correct");
            var temperature = await _weatherMapClient.GetWeatherAsync(cityName, metricValue).ConfigureAwait(false);
            var weatherTemperatureResponse = _mapper.Map<Temperature, WeatherTemperatureResponse>(temperature,
                opts =>
                {
                    opts.Items.Add(WeatherProfileConst.CityNameKey, cityName);
                    opts.Items.Add(WeatherProfileConst.MetricKey, metric);
                });
            return Ok(weatherTemperatureResponse);

        }

        [HttpGet("wind/{cityName}")]
        public async Task<ActionResult> GetWind(string cityName)
        {
            if (string.IsNullOrEmpty(cityName)) return BadRequest("parameter cityName is null or empty");
            var wind = await _weatherMapClient.GetWindDataAsync(cityName).ConfigureAwait(false);
            var weatherWindResponse = _mapper.Map<Wind, WeatherWindResponse>(wind,
                opts  => opts.Items.Add(WeatherProfileConst.CityNameKey, cityName));
            return Ok(weatherWindResponse);
        }


        [HttpGet("{cityName}/future/{metric}")]
        public async Task<ActionResult> GetWind(string cityName, string metric)
        {
            if (string.IsNullOrEmpty(cityName)) return BadRequest("parameter cityName is null or empty");
            if (string.IsNullOrEmpty(metric)) return BadRequest("parameter metric is null or empty");
            if (!Enum.TryParse(metric, true, out Metric metricValue)) return BadRequest("parameter metric is not correct");
            var wind = await _weatherMapClient.GetWeatherForFiveDaysAsync(cityName, metricValue).ConfigureAwait(false);
            var weatherWindResponse = _mapper.Map<IEnumerable<TemperatureWithDate>, List<WeatherTemperatureWithDateResponse>>(wind,
                opts =>
                {
                    opts.Items.Add(WeatherProfileConst.CityNameKey, cityName);
                    opts.Items.Add(WeatherProfileConst.MetricKey, metric);
                });
            return Ok(weatherWindResponse);
        }
    }
}

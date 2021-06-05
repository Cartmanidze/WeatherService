using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherService.Models
{
    public class WeatherWindResponse : BaseWeatherResponse
    {
        public double Speed { get; set; }

        public string Direction { get; set; }
    }
}

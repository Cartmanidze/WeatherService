namespace WeatherService.Models
{
    public class WeatherTemperatureResponse : BaseWeatherResponse
    {
        public double Temperature { get; set; }
        public string Metric { get; set; }
    }
}

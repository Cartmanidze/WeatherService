using Newtonsoft.Json;

namespace WeatherMap.Client.Entities
{
    public class RootObjectForFiveDays
    {
        [JsonProperty(PropertyName = "list")]
        public TemperatureWithDate[] TemperaturesWithDates { get; set; }
    }
}

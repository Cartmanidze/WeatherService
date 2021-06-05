using Newtonsoft.Json;

namespace WeatherMap.Client.Entities
{
    public class TemperatureWithDate
    {
        public Temperature Temperature { get; set; }

        [JsonProperty(PropertyName = "dt_txt")]
        public string Date { get; set; }
    }
}

using Newtonsoft.Json;

namespace WeatherMap.Client.Entities
{
    public class TemperatureWithDate
    {
        [JsonProperty(PropertyName = "main")]
        public Temperature Temperature { get; set; }

        [JsonProperty(PropertyName = "dt_txt")]
        public string Date { get; set; }
    }
}

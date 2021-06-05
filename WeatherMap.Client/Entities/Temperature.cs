using Newtonsoft.Json;

namespace WeatherMap.Client.Entities
{
    [JsonObject(Title = "main")]
    public class Temperature
    {
        [JsonProperty(PropertyName = "temp")]
        public double Value { get; set; }
    }
}

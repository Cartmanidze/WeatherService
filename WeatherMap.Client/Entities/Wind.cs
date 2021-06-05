using Newtonsoft.Json;

namespace WeatherMap.Client.Entities
{
    [JsonObject(Title = "wind")]
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }

        [JsonProperty(PropertyName = "deg")]
        public double Degrees { get; set; }
    }
}

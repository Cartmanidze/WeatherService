using Newtonsoft.Json;

namespace WeatherMap.Client.Entities
{
    public class RootObject
    {
        [JsonProperty(PropertyName = "main")]
        public Temperature Temperature { get; set; }

        [JsonProperty(PropertyName = "wind")]
        public Wind Wind { get; set; }
    }
}

using Newtonsoft.Json;

namespace FootballData.ExternalServices.FootballDataApi.Entities
{
    public class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}

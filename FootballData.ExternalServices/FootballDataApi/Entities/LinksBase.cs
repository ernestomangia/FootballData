using System.Collections.Generic;
using Newtonsoft.Json;

namespace FootballData.ExternalServices.FootballDataApi.Entities
{
    public class LinksBase
    {
        [JsonProperty("self")]
        public List<Link> Self { get; set; }
    }
}

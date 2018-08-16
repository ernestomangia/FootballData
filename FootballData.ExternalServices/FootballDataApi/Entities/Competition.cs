using System.Collections.Generic;
using Newtonsoft.Json;

namespace FootballData.ExternalServices.FootballDataApi.Entities
{
    public class Competition
    {
        [JsonProperty("_links")]
        public CompetitionLinks Links { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("league")]
        public string League { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }
    }

    public class CompetitionLinks : LinksBase
    {
        [JsonProperty("teams")]
        public List<Link> Teams { get; set; }
    }
}

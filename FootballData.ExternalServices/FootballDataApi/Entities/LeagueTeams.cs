using System.Collections.Generic;
using Newtonsoft.Json;

namespace FootballData.ExternalServices.FootballDataApi.Entities
{
    public class LeagueTeams
    {
        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }
    }

    public class Team
    {
        [JsonProperty("_links")]
        public TeamLinks Links { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }
    }

    public class TeamLinks : LinksBase
    {
        [JsonProperty("players")]
        public List<Link> Players { get; set; }
    }
}

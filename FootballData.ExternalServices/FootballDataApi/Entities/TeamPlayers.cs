using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FootballData.ExternalServices.FootballDataApi.Entities
{
    public class TeamPlayers
    {
        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }

    public class Player
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("jerseyNumber")]
        public int JerseyNumber { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("contractUntil")]
        public DateTime ContractUntil { get; set; }
    }
}

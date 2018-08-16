using Newtonsoft.Json;

namespace FootballData.WebApi.Models
{
    public class TotalPlayersResponseDto
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
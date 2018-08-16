using Newtonsoft.Json;

namespace FootballData.WebApi.Models
{
    public class ImportLeagueResponseDto
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
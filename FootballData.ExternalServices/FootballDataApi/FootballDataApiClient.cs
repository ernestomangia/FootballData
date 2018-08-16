using System.Collections.Generic;
using System.Configuration;

using FootballData.ExternalServices.FootballDataApi.Entities;

namespace FootballData.ExternalServices.FootballDataApi
{
    public class FootballDataApiClient : AbstractRestService
    {
        #region Properties

        protected override string ApiUrl
        {
            get { return ConfigurationManager.AppSettings["FootballData.ApiUrl"]; }
        }

        protected override string ApiToken
        {
            get { return ConfigurationManager.AppSettings["FootballData.ApiToken"]; }
        }

        private const string CompetitionsResourceUrl = "competitions";

        #endregion

        public List<Competition> GetLeagues()
        {
            return Get<List<Competition>>(CompetitionsResourceUrl);
        }

        public List<Team> GetTeamsByLeage(string resourceUrl)
        {
            // The resourceUrl is the full url of the resource, so remove the ApiUrl part 
            resourceUrl = resourceUrl.Replace(ApiUrl, string.Empty);

            return Get<LeagueTeams>(resourceUrl).Teams;
        }

        public List<Player> GetTeamPlayers(string resourceUrl)
        {
            // The resourceUrl is the full url of the resource, so remove the ApiUrl part 
            resourceUrl = resourceUrl.Replace(ApiUrl, string.Empty);

            return Get<TeamPlayers>(resourceUrl).Players;
        }
    }
}

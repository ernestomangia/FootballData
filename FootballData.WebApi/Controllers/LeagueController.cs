using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;

using FootballData.Database;
using FootballData.ExternalServices.FootballDataApi;
using FootballData.Repositories;
using FootballData.Services.General;
using FootballData.WebApi.Models;
using FootballData.Domain;
using FootballData.ExternalServices.Exceptions;

namespace FootballData.WebApi.Controllers
{
    [RoutePrefix("")]
    public class LeagueController : ApiController
    {
        private readonly FootballDataApiClient _footballDataApiClient;
        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly PlayerService _playerService;

        public LeagueController()
        {
            var modelContainer = new ModelContainer();

            _footballDataApiClient = new FootballDataApiClient();
            _leagueService = new LeagueService(new GenericRepository<League>(modelContainer));
            _teamService = new TeamService(new GenericRepository<Team>(modelContainer));
            _playerService = new PlayerService(new GenericRepository<Player>(modelContainer));
        }

        #region Get Methods

        [HttpGet]
        [Route("import-league/{leagueCode}")]
        public async Task<HttpResponseMessage> ImportLeague(string leagueCode)
        {
            try
            {
                // Get League from DB
                var dbLeague = await _leagueService.GetAsync(l => l.Code == leagueCode);

                // If the League was already imported before, then return a 409 response
                if (dbLeague != null)
                    return Request.CreateResponse(HttpStatusCode.Conflict, new ImportLeagueResponseDto { Message = "League already imported" });

                // Get league from external service
                var leagues = _footballDataApiClient.GetLeagues();
                var league = leagues.FirstOrDefault(l => l.League == leagueCode);

                // If the requested LeagueCode doesn't exist, then return a 404 response
                if (league == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new ImportLeagueResponseDto { Message = "Not found" });

                // Create new League
                dbLeague = Mapper.Map<League>(league);

                // Get teams from external service
                var teamsResourceUrl = league.Links.Teams.First().Href;
                var teams = _footballDataApiClient.GetTeamsByLeage(teamsResourceUrl);

                if (teams == null)
                    throw new ApplicationException("No teams were found");

                foreach (var team in teams)
                {
                    // Get Team from DB
                    var dbTeam = await _teamService.GetAsync(t => t.Code == team.Code);

                    // If the Team wasn't already imported before, then import it now
                    if (dbTeam == null)
                    {
                        var teamPlayersResourceUrl = team.Links.Players.First().Href;
                        var teamPlayers = _footballDataApiClient.GetTeamPlayers(teamPlayersResourceUrl);

                        // Create new Team
                        dbTeam = Mapper.Map<Team>(team);

                        // Create new Players
                        dbTeam.Players = Mapper.Map<List<Player>>(teamPlayers);
                    }

                    // Associate the Team (new or existing one) with the new League
                    dbLeague.Teams.Add(dbTeam);
                }

                // Import the League
                _leagueService.Insert(dbLeague);

                // Return a 201 response
                return Request.CreateResponse(HttpStatusCode.Created, new ImportLeagueResponseDto { Message = "Successfully imported" });
            }
            catch (RestServiceException ex)
            {
                return Request.CreateResponse(HttpStatusCode.GatewayTimeout, new ImportLeagueResponseDto { Message = "Server Error" });
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.GatewayTimeout, new ImportLeagueResponseDto { Message = "Server Error" });
            }
            catch (ApplicationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.GatewayTimeout, new ImportLeagueResponseDto { Message = "Server Error" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ImportLeagueResponseDto { Message = "Unexpected Error" });
            }
        }

        [HttpGet]
        [Route("total-players/{leagueCode}")]
        public async Task<HttpResponseMessage> TotalPlayers(string leagueCode)
        {
            try
            {
                var dbLeague = await _leagueService.GetAsync(l => l.Code == leagueCode);

                if (dbLeague == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                // Get the total players of the league
                var totalPlayers = await _playerService.List(p => p.Team.Leagues.Any(l => l.Id == dbLeague.Id)).CountAsync();

                // Return a 200 response
                return Request.CreateResponse(HttpStatusCode.OK, new TotalPlayersResponseDto { Total = totalPlayers });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}

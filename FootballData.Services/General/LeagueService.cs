using FootballData.Domain;
using FootballData.Repositories;

namespace FootballData.Services.General
{
    public class LeagueService : AbstractService<League>
    {
        public LeagueService(IRepository<League> repository) : base(repository)
        {
        }
    }
}

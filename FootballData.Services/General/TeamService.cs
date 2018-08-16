using FootballData.Domain;
using FootballData.Repositories;

namespace FootballData.Services.General
{
    public class TeamService : AbstractService<Team>
    {
        public TeamService(IRepository<Team> repository) : base(repository)
        {
        }
    }
}

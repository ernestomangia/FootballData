using FootballData.Domain;
using FootballData.Repositories;

namespace FootballData.Services.General
{
    public class PlayerService : AbstractService<Player>
    {
        public PlayerService(IRepository<Player> repository) : base(repository)
        {
        }
    }
}

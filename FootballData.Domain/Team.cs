using System.Collections.Generic;

namespace FootballData.Domain
{
    public class Team : DomainBase
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string ShortName { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<League> Leagues { get; set; }
    }
}

using System.Collections.Generic;

namespace FootballData.Domain
{
    public class League : DomainBase
    {
        public virtual string Caption { get; set; }
        public virtual string Code { get; set; }
        public virtual string Year { get; set; }
        public virtual long FootballDataLeagueId { get; set; }
        public virtual ICollection<Team> Teams { get; set; } 
    }
}

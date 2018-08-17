using System;

namespace FootballData.Domain
{
    public class Player : DomainBase
    {
        public virtual string Name { get; set; }
        public virtual string Position { get; set; }
        public virtual int JerseyNumber { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual string Nationality { get; set; }
        public virtual DateTime? ContractUntil { get; set; }
        public virtual Team Team { get; set; }
    }
}

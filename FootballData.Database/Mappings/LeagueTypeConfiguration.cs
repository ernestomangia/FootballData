using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FootballData.Domain;

namespace FootballData.Database.Mappings
{
    public class LeagueTypeConfiguration : EntityTypeConfiguration<League>
    {
        public LeagueTypeConfiguration()
        {
            // PK
            HasKey(l => l.Id);

            Property(l => l.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("LeagueId");

            // Properties
            Property(l => l.Caption)
                .HasMaxLength(100)
                .IsRequired();

            Property(l => l.Code)
                .HasMaxLength(4)
                .IsRequired();

            Property(l => l.Year)
                .HasMaxLength(4)
                .IsRequired();

            Property(l => l.FootballDataLeagueId);

            // FK
            HasMany(l => l.Teams)
                .WithMany(t => t.Leagues)
                .Map(lt =>
                        {
                            lt.ToTable("LeagueTeam");
                            lt.MapLeftKey("LeagueId");
                            lt.MapRightKey("TeamId");
                        });

            // Table
            ToTable("League");
        }
    }
}

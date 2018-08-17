using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FootballData.Domain;

namespace FootballData.Database.Mappings
{
    public class TeamTypeConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamTypeConfiguration()
        {
            // PK
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("TeamId");

            // Properties
            Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(t => t.Code)
                .HasMaxLength(10)
                .IsOptional();

            Property(t => t.ShortName)
                .HasMaxLength(50)
                .IsOptional();

            // FK
            HasMany(t => t.Leagues);

            HasMany(t => t.Players)
                .WithRequired(p => p.Team);

            // Table
            ToTable("Team");
        }
    }
}

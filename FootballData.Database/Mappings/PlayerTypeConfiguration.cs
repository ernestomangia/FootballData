using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FootballData.Domain;

namespace FootballData.Database.Mappings
{
    public class PlayerTypeConfiguration : EntityTypeConfiguration<Player>
    {
        public PlayerTypeConfiguration()
        {
            // PK
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("PlayerId");

            // Properties
            Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.Position)
                .HasMaxLength(50)
                .IsOptional();

            Property(p => p.JerseyNumber)
                .IsOptional();

            Property(p => p.DateOfBirth)
                .IsOptional();

            Property(p => p.Nationality)
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.ContractUntil)
                .IsRequired();

            // FK
            HasRequired(p => p.Team);

            // Table
            ToTable("Player");
        }
    }
}

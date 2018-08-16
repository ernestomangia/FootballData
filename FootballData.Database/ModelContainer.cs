using Entity = System.Data.Entity;

namespace FootballData.Database
{
    public class ModelContainer : Entity.DbContext
    {
        public ModelContainer() : base("name=FootballDataDbContext")
        {
            Entity.Database.SetInitializer(new Entity.CreateDatabaseIfNotExists<ModelContainer>());
        }

        protected override void OnModelCreating(Entity.DbModelBuilder modelBuilder)
        {
            // Register all mappings from current assembly
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

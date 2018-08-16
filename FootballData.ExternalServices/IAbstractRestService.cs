namespace FootballData.ExternalServices
{
    public interface IAbstractRestService
    {
        TEntity Get<TEntity>(string resourceUrl, object parameters = null) where TEntity : new();
    }
}

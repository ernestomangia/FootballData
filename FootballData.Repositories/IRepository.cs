using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FootballData.Repositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        TEntity Get(long id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;

namespace FootballData.Services
{
    public interface IAbstractService<TEntity>
    {
        IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        TEntity Get(long id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
    }
}

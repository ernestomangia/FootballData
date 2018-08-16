using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using FootballData.Domain;
using FootballData.Repositories;

namespace FootballData.Services
{
    public class AbstractService<TEntity> : IAbstractService<TEntity> where TEntity : DomainBase
    {
        #region Private Members

        private readonly IRepository<TEntity> _repository;

        #endregion

        #region Constructor(s)

        protected AbstractService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public virtual IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression)
        {
            return _repository.List(expression);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.GetAsync(expression);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _repository.Get(expression);
        }

        public virtual TEntity Get(long id)
        {
            return _repository.Get(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        #endregion
    }
}

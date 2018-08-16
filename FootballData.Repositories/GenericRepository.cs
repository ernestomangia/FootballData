using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using FootballData.Database;
using FootballData.Domain;

namespace FootballData.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : DomainBase
    {
        #region Private Members

        private readonly ModelContainer _context;
        private readonly DbSet<TEntity> _dbSet;

        #endregion

        #region Costructor(s)

        public GenericRepository(ModelContainer container)
        {
            _context = container;
            _dbSet = container.Set<TEntity>();
        }

        #endregion

        #region Public Methods

        public IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public TEntity Get(long id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion
    }
}

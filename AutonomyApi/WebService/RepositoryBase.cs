using Microsoft.EntityFrameworkCore;
using AutonomyApi.WebService.Exceptions;
using AutonomyApi.WebService.DynamicFilters;
using AutonomyApi.Models.Entities;
using System.Linq;

namespace AutonomyApi.WebService
{
    public abstract class RepositoryBase<TDbContext, TEntity> where TDbContext : DbContext where TEntity : class
    {
        protected TDbContext DbContext { get; }
        protected DbSet<TEntity> Entities { get; }
        

        public RepositoryBase(TDbContext dbContext, Func<TDbContext, DbSet<TEntity>> entitiesFromContext)
        {
            Entities = entitiesFromContext(dbContext);
            DbContext = dbContext;
        }

        public virtual void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        protected SearchResults<T> Search<T>(Search<TEntity> search, IQueryable<TEntity> query, Func<TEntity, T> selector) where T : class
        {
            var composed = Compose(query);

            return search.GetResults(composed, selector);
        }

        protected T FindFirst<T>(IQueryable<TEntity> query, Func<TEntity, T> selector, params object[] keyValues) where T : class
        {
            var composed = Compose(query);

            var result = composed.Select(selector).FirstOrDefault();

            if (result == null)
            {
                if (keyValues.Length > 0)
                {
                    throw new EntityNotFoundException(typeof(TEntity), keyValues);
                }

                throw new EntityNotFoundException(typeof(TEntity));
            }

            return result;
        }

        protected virtual IQueryable<TEntity> Compose(IQueryable<TEntity> query)
        {
            return query;
        }
    }
}
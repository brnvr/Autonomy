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

        public virtual List<TEntity> FindAll(DynamicFilterPipelineDelegate<TEntity>? filter = null)
        {
            if (filter == null)
            {
                return Compose(Entities).ToList();
            }

            return filter(Compose(Entities)).ToList();
        }

        public virtual TEntity FindFirst(DynamicFilterPipelineDelegate<TEntity>? filter = null)
        {
            TEntity? result;

            if (filter == null)
            {
                result = Compose(Entities).FirstOrDefault();
            }
            else
            {
                result = filter(Compose(Entities)).FirstOrDefault();
            }

            if (result == null)
            {
                throw new EntityNotFoundException(typeof(TEntity));
            }

            return result;
        }

        public virtual void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public virtual bool Exists(params object[] keyValues)
        {
            return Entities.Find(keyValues) != null;
        }

        public virtual bool Exists(DynamicFilterPipelineDelegate<TEntity>? filter)
        {
            if (filter == null)
            {
                return Entities.Any();
            }

            return filter(Entities).Any();
        }

        protected virtual IQueryable<TEntity> Compose(IQueryable<TEntity> query)
        {
            return query;
        }

        protected SearchResults<TEntity> FromSearch(Search<TEntity> search, IQueryable<TEntity> query)
        {
             return search.GetResults(Compose(query));
        }

        protected SearchResults<TResult> FromSearch<TResult>(Search<TEntity> search, IQueryable<TEntity> query, Func<TEntity, TResult> selector)
        {
            return search.GetResults(Compose(query), selector);
        }

        protected TEntity FromQuery(IQueryable<TEntity> query, params object[] keyValues)
        {
            return FromQuery<TEntity>(Compose(query), keyValues);
        }

        protected T FromQuery<T>(IQueryable<T> query, params object[] keyValues)
        {
            var result = query.FirstOrDefault();

            if (result == null)
            {
                if (keyValues.Count() > 0)
                {
                    throw new EntityNotFoundException(typeof(TEntity), keyValues);
                }

                throw new EntityNotFoundException(typeof(TEntity));
            }

            return result;
        }
    }
}
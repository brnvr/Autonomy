using Microsoft.EntityFrameworkCore;
using AutonomyApi.WebService.Exceptions;

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

        public virtual List<TEntity> FindAll(Filter<TEntity>? filter = null)
        {
            if (filter == null)
            {
                return Compose(Entities).ToList();
            }

            return filter(Compose(Entities)).ToList();
        }

        public virtual TEntity FindFirst(Filter<TEntity>? filter = null)
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

        public virtual bool Exists(Filter<TEntity>? filter = null)
        {
            if (filter == null)
            {
                return Entities.Any();
            }

            return filter(Entities).Any();
        }

        public virtual IQueryable<TEntity> Compose(IQueryable<TEntity> query)
        {
            return query;
        }
    }
}
using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.ServiceProvided;
using AutonomyApi.WebService;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class ServiceProvidedRepository : RepositoryBase<AutonomyDbContext, ServiceProvided>
    {
        public ServiceProvidedRepository(AutonomyDbContext dbContext, bool useComposition=true) : base(dbContext, ctx => ctx.ServicesProvided, useComposition) { }

        public SearchResults<T> Search<T>(int userId, Search<ServiceProvided> search, Func<ServiceProvided, T> selector) where T : class
        {
            var query = Entities.Where(sp => sp.UserId == userId);

            return Search(search, query, selector);
        }

        protected override IQueryable<ServiceProvided> Compose(IQueryable<ServiceProvided> query)
        {
            return query.Include(s => s.Clients);
        }
    }
}

using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using AutonomyApi.WebService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class ServiceProvidedRepository : RepositoryBase<AutonomyDbContext, ServiceProvided>
    {
        public ServiceProvidedRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.ServicesProvided) { }

        public SearchResults<ServiceProvided> FindAll(int userId, Search<ServiceProvided> search)
        {
            var query = Entities.Where(sp => sp.UserId == userId);

            return FromSearch(search, query);
        }

        protected override IQueryable<ServiceProvided> Compose(IQueryable<ServiceProvided> query)
        {
            return query.Include(s => s.Clients);
        }
    }
}

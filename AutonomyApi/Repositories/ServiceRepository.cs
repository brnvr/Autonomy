using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;

namespace AutonomyApi.Repositories
{
    public class ServiceRepository : RepositoryBase<AutonomyDbContext, Service>
    {
        public ServiceRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Services) { }

        public Service Find(int userId, int id)
        {
            var query = from service in Entities
                        where service.UserId == userId && service.Id == id
                        select service;

            return FromQuery(query, id);
        }

        public SearchResults<ServiceSummaryView> FindAll(int userId, ServiceSearchView search)
        {
            var query = Entities.Where(service => service.UserId == userId);

            return FromSearch(search, query, service => new ServiceSummaryView
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description
            });
        }
    }
}

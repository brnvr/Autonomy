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

        public List<ServiceSummaryView> FindAll(int userId, DynamicFilterPipeline<ServiceSummaryView>? filter = null)
        {
            var query = from service in Entities
                        where service.UserId == userId
                        select new ServiceSummaryView
                        {
                            Id = service.Id,
                            Name = service.Name,
                            Description = service.Description
                        };

            return GetFiltered(query, filter);
        }
    }
}

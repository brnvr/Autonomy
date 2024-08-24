using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.WebService;

namespace AutonomyApi.Repositories
{
    public class ServiceRepository : RepositoryBase<AutonomyDbContext, Service>
    {
        public ServiceRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Services) { }

        public T Find<T>(int userId, int id, Func<Service, T> selector) where T : class
        {
            var query = from service in Entities
                        where service.UserId == userId && service.Id == id
                        select service;

            return FindFirst(query, selector, id);
        }

        public SearchResults<T> Search<T>(int userId, ServiceSearchView search, Func<Service, T> selector) where T : class
        {
            var query = Entities.Where(service => service.UserId == userId);

            return Search(search, query, selector);
        }
    }
}

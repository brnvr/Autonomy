using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using AutonomyApi.WebService.Exceptions;
using Microsoft.EntityFrameworkCore;

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

            var result = Compose(query).FirstOrDefault();

            if (result ==  null)
            {
                throw new EntityNotFoundException(typeof(Service), id);
            }

            return result;
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

            if (filter == null)
            {
                return query.ToList();
            }

            return filter.GetDelegate()(query).ToList();
        }

        public override IQueryable<Service> Compose(IQueryable<Service> query)
        {
            return query
                .Include(s => s.BudgetTemplate)
#nullable disable
                .ThenInclude(template => template.Items)
                .ThenInclude(item => item.Currency);
        }
    }
}

using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.ServiceProvided;
using AutonomyApi.Repositories;
using AutonomyApi.WebService;

namespace AutonomyApi.Services
{
    public class ServiceProvidedService
    {
        AutonomyDbContext _dbContext;

        public ServiceProvidedService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public dynamic Get(int userId, ServiceProvidedSearchView search)
        {
            return new ServiceProvidedRepository(_dbContext).Search(userId, search, service => new
            {
                service.Id,
                service.ServiceId,
                service.ServiceName,
                service.Clients,
                service.BudgetId,
                service.Date,
                service.CreationDate,
                Total = service.Budget is null ? 0 : service.Budget.GetTotal()
            });
        }
    }
}

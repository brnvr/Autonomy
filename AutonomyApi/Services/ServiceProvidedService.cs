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
            return new ServiceProvidedRepository(_dbContext).Search(userId, search, service => service);
        }
    }
}

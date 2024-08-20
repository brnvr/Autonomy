using AutonomyApi.Database;
using AutonomyApi.Enums;
using AutonomyApi.Models.Entities;
using AutonomyApi.Repositories;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using AutonomyApi.WebService.Exceptions;

namespace AutonomyApi.Services
{
    public class ServiceProvidedService
    {
        AutonomyDbContext _dbContext;

        public ServiceProvidedService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*public List<ServiceProvided> Get(int userId, int page, int pageLength, Orderer<ServiceProvided>? orderer, string? searchService, string? searchClient, DateTime? startDate, DateTime? endDate, DynamicFilterPipeline<ServiceProvided>? filters)
        {
            return new ServiceProvidedRepository(_dbContext).FindAll(userId, page, pageLength, orderer, searchService, searchClient, startDate, endDate, filters);
        }*/
    }
}

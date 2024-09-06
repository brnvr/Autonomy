using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Repositories;
using AutonomyApi.WebService.DynamicFilters;

namespace AutonomyApi.Services
{
    public class CurrencyService
    {
        AutonomyDbContext _dbContext;

        public CurrencyService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Currency> Get(string? search)
        {
            var filters = new DynamicFilterPipeline<Currency>
            {
                new SubstringFilter<Currency>(search, currency => [ currency.Code, currency.Name ])
            };

            return new CurrencyRepository(_dbContext).Find(filters);
        }

        public Currency Get(int id)
        {
            return new CurrencyRepository(_dbContext).Find(id, c => c);
        }
    }
}
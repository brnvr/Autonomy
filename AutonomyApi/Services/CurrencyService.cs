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

        public List<Currency> Get(string? search)
        {
            var filters = new DynamicFilterPipeline<Currency>
            {
                new SubstringFilter<Currency>(search, currency => [ currency.Code, currency.Name ])
            };

            return new CurrencyRepository(_dbContext).FindAll(filters.GetDelegate());
        }

        public Currency Get(int id)
        {
            return new CurrencyRepository(_dbContext).FindFirst(q => q.Where(currency => currency.Id == id));
        }
    }
}

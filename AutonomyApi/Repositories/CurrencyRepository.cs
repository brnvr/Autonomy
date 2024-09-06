using AutonomyApi.Controllers;
using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AutonomyApi.Repositories
{
    public class CurrencyRepository : RepositoryBase<AutonomyDbContext, Currency>
    {
        public CurrencyRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Currencies)  { }

        public T Find<T>(int id, Func<Currency, T> selector) where T : class
        {
            var query = from currency in Entities
                        where currency.Id == id
                        select currency;

            return FindFirst(query, selector, id);
        }

        public new IEnumerable<Currency> Find(DynamicFilterPipeline<Currency>? filters)
        {
            return base.Find(filters);
        }
    }
}
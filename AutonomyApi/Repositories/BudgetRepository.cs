using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.WebService;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class BudgetRepository : RepositoryBase<AutonomyDbContext, Budget>
    {
        public BudgetRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Budgets) { }

        public T Find<T>(int userId, int id, bool? isTemplate, Func<Budget, T> selector) where T : class
        {
            var query = from budget in Entities
                        where budget.UserId == userId && budget.Id == id && (isTemplate == null || budget.IsTemplate == isTemplate)
                        select budget;

            return FindFirst(query, selector, id);
        }

        public T FindByServiceId<T>(int userId, int serviceId, Func<Budget, T> selector) where T : class
        {
            var query = from budget in Entities
                        join service in DbContext.Services
                        on budget.Id equals service.BudgetTemplateId
                        where budget.UserId == userId
                        select budget;

            return FindFirst(query, selector);
        }

        public SearchResults<T> Search<T>(int userId, bool? isTemplate, Search<Budget> search, Func<Budget, T> selector) where T : class
        {
            var query = Entities.Where(budget => budget.UserId == userId && (isTemplate == null || budget.IsTemplate == isTemplate));

            return Search(search, query, selector);
        }

        protected override IQueryable<Budget> Compose(IQueryable<Budget> query)
        {
            return query
                .Include(c => c.Items)
                .ThenInclude(item => item.Currency);
        }
    }
}

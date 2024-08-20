using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Budget;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using AutonomyApi.WebService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class BudgetRepository : RepositoryBase<AutonomyDbContext, Budget>
    {
        public BudgetRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Budgets) { }

        public Budget Find(int userId, int id, bool? isTemplate)
        {
            var query = from budget in Entities
                        where budget.UserId == userId && budget.Id == id && (isTemplate == null || budget.IsTemplate == isTemplate)
                        select budget;

            return FromQuery(query);
        }

        public Budget FindByServiceId(int userId, int serviceId)
        {
            var query = from budget in Entities
                        join service in DbContext.Services
                        on budget.Id equals service.BudgetTemplateId
                        where budget.UserId == userId
                        select budget;

            return FromQuery(query);
        }

        public SearchResults<BudgetSummaryView> FindAll(int userId, bool? isTemplate, BudgetSearchView search)
        {
            var query = Entities.Where(budget => budget.UserId == userId && (isTemplate == null || budget.IsTemplate == isTemplate));

            return FromSearch(search, query, budget => new BudgetSummaryView
            {
                Id = budget.Id,
                Name = budget.Name
            });
        }

        protected override IQueryable<Budget> Compose(IQueryable<Budget> query)
        {
            return query
                .Include(c => c.Items)
                .ThenInclude(item => item.Currency);
        }
    }
}

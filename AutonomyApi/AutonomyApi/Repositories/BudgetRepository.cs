using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.Views.Budget;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using AutonomyApi.WebService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class BudgetRepository : RepositoryBase<AutonomyDbContext, Budget>
    {
        public BudgetRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Budgets) { }

        public Budget Find(int userId, int id)
        {
            var query = from budget in Entities
                        where budget.UserId == userId && budget.Id == id
                        select budget;

            var result = Compose(query).FirstOrDefault();

            if (result ==  null)
            {
                throw new EntityNotFoundException(typeof(Budget), id);
            }

            return result;
        }

        public List<BudgetSummary> FindAll(int userId, DynamicFilterPipeline<BudgetSummary>? filter = null)
        {
            var query = from budget in Entities
                        where budget.UserId == userId
                        select new BudgetSummary
                        {
                            Id = budget.Id,
                            Name = budget.Name
                        };

            if (filter == null)
            {
                return query.ToList();
            }

            return filter.GetDelegate()(query).ToList();
        }

        public override IQueryable<Budget> Compose(IQueryable<Budget> query)
        {
            return query.Include(c => c.Items);
        }
    }
}

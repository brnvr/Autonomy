using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.Views.Budget;
using AutonomyApi.Repositories;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutonomyApi.Services
{
    public class BudgetService
    {
        AutonomyDbContext _dbContext;

        public BudgetService(AutonomyDbContext dbContext)
        { 
            _dbContext = dbContext;
        }

        public List<BudgetSummary> Get(int userId, string? search)
        {
            var filters = new DynamicFilterPipeline<BudgetSummary>
            {
                new TextMatchFilter<BudgetSummary>(budget => budget.Name, search)
            };

            return new BudgetRepository(_dbContext).FindAll(userId, filters);
        }

        public Budget Get(int userId, int id)
        {
            return new BudgetRepository(_dbContext).Find(userId, id);
        }

        public int Add(int userId, BudgetCreationData data)
        {
            var budget = new Budget
            {
                Name = data.Name,
                Header = data.Header,
                Footer = data.Footer,
                UserId = userId,
                Items = data.Items
            };

            new BudgetRepository(_dbContext).Add(budget);

            _dbContext.SaveChanges();

            return budget.Id;
        }

        public void Update(int userId, int id, BudgetUpdateData data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var budget = new BudgetRepository(_dbContext).Find(userId, id);

                budget.Name = data.Name;
                budget.Header = data.Header;
                budget.Footer = data.Footer;
                budget.Items = data.Items;

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void Remove(int userId, int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var repo = new BudgetRepository(_dbContext);
                var budget = repo.Find(userId, id);

                repo.Remove(budget);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}

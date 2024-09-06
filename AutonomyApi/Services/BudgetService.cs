using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Budget;
using AutonomyApi.Repositories;

namespace AutonomyApi.Services
{
    public class BudgetService
    {
        AutonomyDbContext _dbContext;

        public BudgetService(AutonomyDbContext dbContext)
        { 
            _dbContext = dbContext;
        }

        public dynamic Get(int userId, BudgetSearchView search)
        {
            return new BudgetRepository(_dbContext).Search(userId, false, search, budget => new
            {
                budget.Id,
                budget. Name,
                Total = budget.GetTotal()
            });
        }

        public dynamic Get(int userId, int id)
        {
            return new BudgetRepository(_dbContext).Find(userId, id, false, budget => new
            {
                budget.Id,
                budget.Name,
                budget.Header,
                budget.Footer,
                Items = budget.Items.Select(item => new
                {
                    item.Name,
                    item.Quantity,
                    item.UnitPrice,
                    item.Duration,
                    item.DurationTimeUnit,
                    Total = item.GetTotal()
                }).ToList(),
                Total = budget.GetTotal()
            });
        }

        public int Create(int userId, BudgetCreationView data)
        {
            var budget = new Budget
            {
                Name = data.Name,
                UserId = userId,
                Items = new List<BudgetItem>(),
                CurrencyId = data.CurrencyId,
                IsTemplate = false,
                CreationDate = DateTime.UtcNow
            };

            new BudgetRepository(_dbContext).Add(budget);

            _dbContext.SaveChanges();

            return budget.Id;
        }

        public int Copy(int userId, int id, string name)
        {
            var repo = new BudgetRepository(_dbContext);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var budget = repo.Find(userId, id, null, budget => budget);

                var newBudget = budget.Copy();

                repo.Add(newBudget);

                _dbContext.SaveChanges();
                transaction.Commit();

                return newBudget.Id;
            }
        }

        public void Update(int userId, int id, BudgetUpdateView data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var budget = new BudgetRepository(_dbContext).Find(userId, id, false, budget => budget);

                budget.Name = data.Name;
                budget.Header = data.Header;
                budget.Footer = data.Footer;

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void Remove(int userId, int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var repo = new BudgetRepository(_dbContext);
                var budget = repo.Find(userId, id, false, budget => budget);

                repo.Remove(budget);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void UpdateItems(int userId, int id, BudgetItemUpdateViewList data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var repo = new BudgetRepository(_dbContext);
                var budget = repo.Find(userId, id, false, budget => budget);

                budget.Items = data.ToBudgetItemList();
                
                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}

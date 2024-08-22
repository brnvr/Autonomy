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
                budget.Name
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
                    item.Currency,
                    item.Duration,
                    item.DurationTimeUnit
                }).ToList()
            });
        }

        public int Create(int userId, BudgetCreationView data)
        {
            var budget = new Budget
            {
                Name = data.Name,
                UserId = userId,
                Items = new List<BudgetItem>(),
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
                var budget = repo.Find(userId, id, null, budget => new
                {
                    budget.Header, budget.Footer, budget.Items
                });

                var newBudget = new Budget
                {
                    Name = name,
                    Header = budget.Header,
                    Footer = budget.Footer,
                    IsTemplate = false,
                    UserId = userId,
                    Items = budget.Items.Select(item => new BudgetItem
                    {
                        Name = item.Name,
                        Position = item.Position,
                        Quantity = item.Quantity,
                        CurrencyId = item.CurrencyId,
                        UnitPrice = item.UnitPrice,
                        Duration = item.Duration,
                        DurationTimeUnit = item.DurationTimeUnit
                    }).ToList(),
                    CreationDate = DateTime.UtcNow
                };

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

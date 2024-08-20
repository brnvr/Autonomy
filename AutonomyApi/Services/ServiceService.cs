using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.Repositories;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;

namespace AutonomyApi.Services
{
    public class ServiceService
    {
        AutonomyDbContext _dbContext;

        public ServiceService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public SearchResults<ServiceSummaryView> Get(int userId, ServiceSearchView search)
        {
            return new ServiceRepository(_dbContext).FindAll(userId, search);
        }

        public Service Get(int userId, int id)
        {
            return new ServiceRepository(_dbContext).Find(userId, id);
        }

        public int Create(int userId, ServiceCreationView data)
        {
            var service = new Service
            {
                UserId = userId,
                Name = data.Name,
                CreationDate = DateTime.UtcNow,
            };

            new ServiceRepository(_dbContext).Add(service);

            _dbContext.SaveChanges();

            return service.Id;
        }

        public void Update(int userId, int id, ServiceUpdateView data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var repo = new ServiceRepository(_dbContext);
                var service = repo.Find(userId, id);

                service.Name = data.Name;
                service.Description = data.Description;

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void Remove(int userId, int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var repo = new ServiceRepository(_dbContext);
                var service = repo.Find(userId, id);

                repo.Remove(service);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public Budget GetBudgetTemplate(int userId, int id)
        {
            return new BudgetRepository(_dbContext).FindByServiceId(userId, id);
        }

        public void UpdateBudgetTemplate(int userId, int id, BudgetTemplateUpdateView data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var serviceRepo = new ServiceRepository(_dbContext);
                var budgetRepo = new BudgetRepository(_dbContext);
                var service = serviceRepo.Find(userId, id);

                if (service.BudgetTemplateId == null)
                {
                    var budget = new Budget
                    {
                        Name = data.Name,
                        Header = data.Header,
                        Footer = data.Footer,
                        IsTemplate = true,
                        UserId = userId,
                        CreationDate = DateTime.UtcNow,
                        Items = data.Items.ToBudgetItemList()
                    };

                    budgetRepo.Add(budget);

                    _dbContext.SaveChanges();

                    service.BudgetTemplateId = budget.Id;
                }
                else
                {
                    var budget = budgetRepo.Find(userId, (int)service.BudgetTemplateId, null);

                    budget.Name = data.Name;
                    budget.Header = data.Header;
                    budget.Footer = data.Footer;
                    budget.Items = data.Items.ToBudgetItemList();
                }

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void RemoveBudgetTemplate(int userId, int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var serviceRepo = new ServiceRepository(_dbContext);
                var service = serviceRepo.Find(userId, id);

                if (service.BudgetTemplateId != null)
                {
                    var budgetRepo = new BudgetRepository(_dbContext);
                    var budget = budgetRepo.Find(userId, (int)service.BudgetTemplateId, null);

                    budgetRepo.Remove(budget);
                    service.BudgetTemplateId = null; 
                }

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}

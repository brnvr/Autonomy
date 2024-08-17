using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Budget;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.Repositories;
using AutonomyApi.WebService.DynamicFilters;
using Microsoft.AspNetCore.Components.Routing;
using static AutonomyApi.Constants;
using System.Reflection.PortableExecutable;

namespace AutonomyApi.Services
{
    public class ServiceService
    {
        AutonomyDbContext _dbContext;

        public ServiceService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public List<ServiceSummaryView> Get(int userId, string? search)
        {
            var filters = new DynamicFilterPipeline<ServiceSummaryView>
            {
                new TextMatchFilter<ServiceSummaryView>(service => service.Name+service.Description, search)
            };

            return new ServiceRepository(_dbContext).FindAll(userId, filters);
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

        public void UpdateBudgetTemplate(int userId, int id, BudgetTemplateUpdateView? data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var repo = new ServiceRepository(_dbContext);
                var service = repo.Find(userId, id);

                if (data == null)
                {
                    if (service.BudgetTemplate != null)
                    {
                        new BudgetRepository(_dbContext).Remove(service.BudgetTemplate);
                        service.BudgetTemplateId = null;
                    }
                }
                else if (service.BudgetTemplate == null)
                {
                    service.BudgetTemplate = new Budget
                    {
                        Name = data.Name,
                        Header = data.Header,
                        Footer = data.Footer,
                        IsTemplate = true,
                        UserId = userId,
                        CreationDate = DateTime.UtcNow,
                        Items = data.Items.ToBudgetItemList()
                    };
                }
                else
                {
                    service.BudgetTemplate.Name = data.Name;
                    service.BudgetTemplate.Header = data.Header;
                    service.BudgetTemplate.Footer = data.Footer;
                    service.BudgetTemplate.Items = data.Items.ToBudgetItemList();
                }

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}

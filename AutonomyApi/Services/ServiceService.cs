using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.Repositories;

namespace AutonomyApi.Services
{
    public class ServiceService
    {
        AutonomyDbContext _dbContext;

        public ServiceService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public dynamic Get(int userId, ServiceSearchView search)
        {
            return new ServiceRepository(_dbContext).Search(userId, search, service => new {
                service.Id,
                service.Name,
                service.Description
            });
        }

        public dynamic Get(int userId, int id)
        {
            return new ServiceRepository(_dbContext).Find(userId, id, service => service);
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
                var repo = new ServiceRepository(_dbContext, false);
                var service = repo.Find(userId, id, service => service);

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
                var repo = new ServiceRepository(_dbContext, false);
                var service = repo.Find(userId, id, service => service);

                repo.Remove(service);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public dynamic GetBudgetTemplate(int userId, int id)
        {
            return new BudgetRepository(_dbContext).FindByServiceId(userId, id, budget => budget);
        }

        public void UpdateBudgetTemplate(int userId, int id, BudgetTemplateUpdateView data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var serviceRepo = new ServiceRepository(_dbContext, false);
                var budgetRepo = new BudgetRepository(_dbContext);
                var service = serviceRepo.Find(userId, id, service => service);

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
                    var budget = budgetRepo.Find(userId, (int)service.BudgetTemplateId, true, budget => budget);

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
                var serviceRepo = new ServiceRepository(_dbContext, false);
                var service = serviceRepo.Find(userId, id, service => service);

                if (service.BudgetTemplateId != null)
                {
                    var budgetRepo = new BudgetRepository(_dbContext, false);
                    var budget = budgetRepo.Find(userId, (int)service.BudgetTemplateId, null, budget => budget);

                    budgetRepo.Remove(budget);
                    service.BudgetTemplateId = null; 
                }

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public int Provide(int userId, int id, ServiceProvideView data)
        {
            var clientRepo = new ClientRepository(_dbContext);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var service = new ServiceRepository(_dbContext).Find(userId, id, service => new
                {
                    service.Id, service.Name
                });

                var serviceProvided = new ServiceProvided
                {
                    ServiceId = service.Id,
                    ServiceName = service.Name,
                    UserId = userId,
                    Date = data.Date,
                    CreationDate = DateTime.UtcNow,
                    Clients = data.ClientIds.Select(clientId =>
                    {
                        var client = clientRepo.Find(userId, clientId, client => new
                        {
                            client.Id,
                            client.Name,
                            client.Documents
                        });

                        var doc = client.Documents.FirstOrDefault();

                        return new ServiceProvidedClient
                        {
                            ClientId = client.Id,
                            ClientName = client.Name,
                            ClientDocument = doc is null ? null : doc.Value,
                            ClientDocumentType = doc is null ? null : doc.Type
                        };
                    }).ToList()
                };
                
                new ServiceProvidedRepository(_dbContext).Add(serviceProvided);

                _dbContext.SaveChanges();
                transaction.Commit();

                return serviceProvided.Id;
            }
        }
    }
}

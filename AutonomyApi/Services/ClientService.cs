using AutonomyApi.Database;
using AutonomyApi.Enums;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Client;
using AutonomyApi.Repositories;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using AutonomyApi.WebService.Exceptions;

namespace AutonomyApi.Services
{
    public class ClientService
    {
        AutonomyDbContext _dbContext;

        public ClientService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SearchResults<ClientSummaryView> Get(int userId, ClientSearchView search)
        {
            return new ClientRepository(_dbContext).FindAll(userId, search);
        }

        public Client Get(int userId, int id)
        {
            return new ClientRepository(_dbContext).Find(userId, id);
        }

        public int Create(int userId, ClientCreationView data)
        {
            var client = new Client
            {
                UserId = userId,
                Name = data.Name,
                Documents = new List<ClientDocument>(),
                CreationDate = DateTime.UtcNow
            };

            new ClientRepository(_dbContext).Add(client);
            _dbContext.SaveChanges();

            return client.Id;
        }

        public void Update(int userId, int id, ClientUpdateView data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var client = new ClientRepository(_dbContext).Find(userId, id);
                client.Name = data.Name;

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void Remove(int userId, int id)
        {
            var repo = new ClientRepository(_dbContext);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var client = repo.Find(userId, id);

                repo.Remove(client);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void UpdateDocument(int userId, int clientId, ClientDocumentUpdateView data)
        {
            var repo = new ClientRepository(_dbContext);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var client = repo.Find(userId, clientId);

                var document = client.Documents.Find(document => document.Type == data.Type);

                if (document is null)
                {
                    if (!string.IsNullOrEmpty(data.Value))
                    {
                        client.Documents.Add(new ClientDocument
                        {
                            Type = data.Type,
                            Value = data.Value
                        });
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(data.Value))
                    {
                        repo.Remove(client);
                    }
                    else
                    {
                        document.Value = data.Value;
                    }
                }

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}

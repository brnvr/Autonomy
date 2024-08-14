using AutonomyApi.Database;
using AutonomyApi.Enums;
using AutonomyApi.Models.Dtos.Client;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.Views.Budget;
using AutonomyApi.Models.Views.Client;
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

        public List<ClientSummary> Get(int userId, string? search)
        {
            var filters = new DynamicFilterPipeline<ClientSummary>
            {
                new TextMatchFilter<ClientSummary>(client => client.Name, search)
            };

            return new ClientRepository(_dbContext).FindAll(userId, filters);
        }

        public Client Get(int userId, int id)
        {
            return new ClientRepository(_dbContext).Find(userId, id);
        }

        public int Create(int userId, ClientCreationData data)
        {
            var client = new Client
            {
                UserId = userId,
                Name = data.Name,
                Documents = new List<ClientDocument>(),
                RegistrationDate = DateTime.UtcNow
            };

            new ClientRepository(_dbContext).Add(client);
            _dbContext.SaveChanges();

            return client.Id;
        }

        public void Update(int userId, int id, ClientUpdateData data)
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

        public void UpdateDocument(int userId, int clientId, DocumentType type, ClientDocumentUpdateData data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var client = new ClientRepository(_dbContext).Find(userId, clientId);

                var document = client.Documents.Find(document => document.Type == type);

                if (document is null)
                {
                    client.Documents.Add(new ClientDocument
                    {
                        Type = type,
                        Value = data.Value
                    });
                }
                else
                {
                    document.Value = data.Value;
                }

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void RemoveDocument(int userId, int clientId, DocumentType type)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var client = new ClientRepository(_dbContext).Find(userId, clientId);
                var document = client.Documents.Find(document => document.Type == type);

                if (document is null)
                {
                    throw new EntityNotFoundException($"Document of type {type} not found for client {clientId}.", null);
                }

                client.Documents.Remove(document);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}

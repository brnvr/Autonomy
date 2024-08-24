using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Client;
using AutonomyApi.Repositories;

namespace AutonomyApi.Services
{
    public class ClientService
    {
        readonly AutonomyDbContext _dbContext;

        public ClientService(AutonomyDbContext dbContext) => _dbContext = dbContext;

        public dynamic Get(int userId, ClientSearchView search)
        {
            return new ClientRepository(_dbContext).Search(userId, search, client => new
            {
                client.Id,
                client.Name
            });
        }

        public dynamic Get(int userId, int id)
        {
            return new ClientRepository(_dbContext).Find(userId, id, client => client);
        }

        public int Create(int userId, ClientCreationView data)
        {
            var client = new Client
            {
                UserId = userId,
                Name = data.Name,
                Documents = [],
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
                var client = new ClientRepository(_dbContext).Find(userId, id, client => client);
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
                var client = repo.Find(userId, id, client => client);

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
                var client = repo.Find(userId, clientId, client => client);

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

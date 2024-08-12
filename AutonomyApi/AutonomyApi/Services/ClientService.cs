using AutonomyApi.Database;
using AutonomyApi.Dtos;
using AutonomyApi.Entities;
using AutonomyApi.Repositories;
using AutonomyApi.WebService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutonomyApi.Services
{
    public class ClientService
    {
        AppDbContext _dbContext;
        

        public ClientService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ClientOverview> Get(int userId, string? name)
        {
            Filter<ClientOverview> filter = e =>
            {
                return e.Where(client =>
                {
                    return FilterCondition.Contains(client.Name, name);
                });
            };

            return new ClientRepository(_dbContext).FindAll(userId, filter);
        }

        public Client Get(int userId, int id)
        {
            return new ClientRepository(_dbContext).Find(userId, id);
        }

        public int Register(int userId, InsertClientDto data)
        {
            var client = new Client
            {
                UserId = userId,
                Name = data.Name,
                Documents = new List<ClientDocument>(),
            };

            new ClientRepository(_dbContext).Add(client);
            _dbContext.SaveChanges();

            return client.Id;
        }

        public void Edit(int userId, int id, UpdateClientDto data)
        {
            _dbContext.Database.BeginTransaction();

            var client = new ClientRepository(_dbContext).Find(userId, id);
            client.Name = data.Name;

            _dbContext.SaveChanges();
            _dbContext.Database.CommitTransaction();
        }

        public void Remove(int userId, int id)
        {
            var repo = new ClientRepository(_dbContext);

            _dbContext.Database.BeginTransaction();

            var client = repo.Find(userId, id);

            repo.Remove(client);

            _dbContext.SaveChanges();
            _dbContext.Database.CommitTransaction();
        }
    }
}

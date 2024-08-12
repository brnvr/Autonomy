using AutonomyApi.Database;
using AutonomyApi.Dtos;
using AutonomyApi.Entities;
using AutonomyApi.WebService;
using AutonomyApi.WebService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class ClientRepository : RepositoryBase<AppDbContext, Client>
    {
        public ClientRepository(AppDbContext dbContext) : base(dbContext, ctx => ctx.Clients) { }

        public Client Find(int userId, int id)
        {
            var query = from client in Entities
                        where client.UserId == userId && client.Id == id
                        select client;

            var result = Compose(query).FirstOrDefault();

            if (result ==  null)
            {
                throw new EntityNotFoundException(typeof(Client), id);
            }

            return result;
        }

        public List<ClientOverview> FindAll(int userId, Filter<ClientOverview>? filter = null)
        {
            var query = from client in Entities
                        where client.UserId == userId
                        select new ClientOverview
                        {
                            Id = client.Id,
                            Name = client.Name
                        };

            if (filter == null)
            {
                return query.ToList();
            }

            return filter(query).ToList();
        }

        public override IQueryable<Client> Compose(IQueryable<Client> query)
        {
            return query.Include(c => c.Documents);
        }
    }
}

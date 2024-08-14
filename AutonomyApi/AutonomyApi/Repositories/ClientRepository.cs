using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.Views.Client;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using AutonomyApi.WebService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class ClientRepository : RepositoryBase<AutonomyDbContext, Client>
    {
        public ClientRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Clients) { }

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

        public List<ClientSummary> FindAll(int userId, DynamicFilterPipeline<ClientSummary>? filter = null)
        {
            var query = from client in Entities
                        where client.UserId == userId
                        select new ClientSummary
                        {
                            Id = client.Id,
                            Name = client.Name
                        };

            if (filter == null)
            {
                return query.ToList();
            }

            return filter.GetDelegate()(query).ToList();
        }

        public override IQueryable<Client> Compose(IQueryable<Client> query)
        {
            return query.Include(c => c.Documents);
        }
    }
}

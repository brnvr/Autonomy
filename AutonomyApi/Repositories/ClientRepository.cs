using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Client;
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

            return FromQuery(query, id);
        }

        public List<ClientSummaryView> FindAll(int userId, DynamicFilterPipeline<ClientSummaryView>? filter = null)
        {
            var query = from client in Entities
                        where client.UserId == userId
                        select new ClientSummaryView
                        {
                            Id = client.Id,
                            Name = client.Name
                        };

            return GetFiltered(query, filter);
        }

        protected override IQueryable<Client> Compose(IQueryable<Client> query)
        {
            return query.Include(c => c.Documents);
        }
    }
}

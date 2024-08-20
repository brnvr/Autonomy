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

        public SearchResults<ClientSummaryView> FindAll(int userId, ClientSearchView search)
        {
            var query = Entities.Where(client => client.UserId == userId);

            return FromSearch(search, query, client => new ClientSummaryView
            {
                Id = client.Id,
                Name = client.Name
            });
        }

        protected override IQueryable<Client> Compose(IQueryable<Client> query)
        {
            return query.Include(c => c.Documents);
        }
    }
}

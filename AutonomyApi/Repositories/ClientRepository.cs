using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.WebService;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class ClientRepository : RepositoryBase<AutonomyDbContext, Client>
    {
        public ClientRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Clients) { }

        public T Find<T>(int userId, int id, Func<Client, T> selector) where T : class
        {
            var query = from client in Entities
                        where client.UserId == userId && client.Id == id
                        select client;

            return FindFirst(query, selector, id);
        }

        public SearchResults<T> Search<T>(int userId, Search<Client> search, Func<Client, T> selector) where T : class
        {
            var query = Entities.Where(client => client.UserId == userId);

            return Search(search, query, selector);
        }

        protected override IQueryable<Client> Compose(IQueryable<Client> query)
        {
            return query.Include(c => c.Documents);
        }
    }
}

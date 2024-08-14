using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.WebService;

namespace AutonomyApi.Repositories
{
    public class UserRepository : RepositoryBase<AutonomyDbContext, User>
    {
        public UserRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Users) { }
    }
}

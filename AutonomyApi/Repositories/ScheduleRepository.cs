using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Client;
using AutonomyApi.Models.ViewModels.Schedule;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.WebService;
using AutonomyApi.WebService.DynamicFilters;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Repositories
{
    public class ScheduleRepository : RepositoryBase<AutonomyDbContext, Schedule>
    {
        public ScheduleRepository(AutonomyDbContext dbContext) : base(dbContext, ctx => ctx.Schedules) { }

        public T Find<T>(int userId, int id, Func<Schedule, T> selector) where T : class
        {
            var query = from schedule in Entities
                        where schedule.UserId == userId && schedule.Id == id
                        select schedule;

            return FindFirst(query, selector, id);
        }

        public SearchResults<T> Search<T>(int userId, ScheduleSearchView search, Func<Schedule, T> selector) where T : class
        {
            var query = Entities.Where(item => item.UserId == userId);

            return Search(search, query, selector);
        }

        protected override IQueryable<Schedule> Compose(IQueryable<Schedule> query)
        {
            return query
                .Include(schedule => schedule.Clients);
        }
    }
}

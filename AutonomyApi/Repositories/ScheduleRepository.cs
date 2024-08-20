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

        public Schedule Find(int userId, int id)
        {
            var query = from schedule in Entities
                        where schedule.UserId == userId && schedule.Id == id
                        select schedule;

            return FromQuery(query, id);
        }

        public SchedulePresentationView FindDetails(int userId, int id)
        {
            var query = from schedule in Entities
                        where schedule.UserId == userId && schedule.Id == id
                        select new SchedulePresentationView
                        {
                            Id = schedule.Id,
                            Name = schedule.Name,
                            Date = schedule.Date,
                            CreationDate = schedule.CreationDate,
                            Description = schedule.Description,
                            Service = schedule.Service == null ? null : new ServiceSummaryView
                            {
                                Id = schedule.Service.Id,
                                Name = schedule.Service.Name,
                                Description = schedule.Service.Description
                            },
                            Clients = schedule.Clients.Select(client => new ClientSummaryView
                            {
                                Id = client.Id,
                                Name = client.Name
                            }).ToList()
                        };

            return FromQuery(query, id);
        }

        public SearchResults<ScheduleSummaryView> FindAll(int userId, ScheduleSearchView search)
        {
            var query = Entities.Where(item => item.UserId == userId);

            return FromSearch(search, query, item => new ScheduleSummaryView
            {
                Id = item.Id,
                Name = item.Name,
                Date = item.Date,
                Description = item.Description,
                Service = item.Service == null ? null : new ServiceSummaryView
                {
                    Id = item.Service.Id,
                    Name = item.Service.Name,
                    Description = item.Service.Description
                },
                Clients = item.Clients.Select(client => new ClientSummaryView
                {
                    Id = client.Id,
                    Name = client.Name
                }).ToList()
            });
        }

        protected override IQueryable<Schedule> Compose(IQueryable<Schedule> query)
        {
            return query
                .Include(schedule => schedule.Clients);
        }
    }
}

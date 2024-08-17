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

        public List<SchedulePresentationView> FindAll(int userId, DynamicFilterPipeline<SchedulePresentationView>? filter = null)
        {
            var query = from schedule in Entities
                        where schedule.UserId == userId
                        select new SchedulePresentationView
                        {
                            Id = schedule.Id,
                            Name = schedule.Name,
                            Date = schedule.Date,
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

            return GetFiltered(query, filter);
        }

        protected override IQueryable<Schedule> Compose(IQueryable<Schedule> query)
        {
            return query
                .Include(schedule => schedule.Clients);
        }
    }
}

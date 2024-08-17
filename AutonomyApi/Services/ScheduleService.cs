using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Schedule;
using AutonomyApi.Repositories;
using AutonomyApi.WebService.DynamicFilters;

namespace AutonomyApi.Schedules
{
    public class ScheduleService
    {
        AutonomyDbContext _dbContext;

        public ScheduleService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public List<SchedulePresentationView> Get(int userId, string? search, int? clientId)
        {
            var filters = new DynamicFilterPipeline<SchedulePresentationView>
            {
                new TextMatchFilter<SchedulePresentationView>(schedule => schedule.Name+schedule.Description, search),
                new EqualsFilter<SchedulePresentationView, int?>(schedule => 
                {
                    if (schedule.Service == null)
                    {
                        return null;
                    }

                    return schedule.Service.Id;
                }, clientId)
            };

            return new ScheduleRepository(_dbContext).FindAll(userId, filters);
        }

        public SchedulePresentationView Get(int userId, int id)
        {
            return new ScheduleRepository(_dbContext).FindDetails(userId, id);
        }

        public int Create(int userId, ScheduleCreationView data)
        {
            var schedule = new Schedule
            {
                UserId = userId,
                Name = data.Name,
                Description = data.Description,
                ServiceId = data.ServiceId,
                Date = data.Date,
                Clients = new List<Client>(),
                CreationDate = DateTime.UtcNow,
            };

            new ScheduleRepository(_dbContext).Add(schedule);

            _dbContext.SaveChanges();

            return schedule.Id;
        }

        public void Update(int userId, int id, ScheduleUpdateView data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var repo = new ScheduleRepository(_dbContext);
                var schedule = repo.Find(userId, id);

                schedule.Name = data.Name;
                schedule.Description = data.Description;
                schedule.ServiceId = data.ServiceId;
                schedule.Date = data.Date;

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void Remove(int userId, int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var repo = new ScheduleRepository(_dbContext);
                var schedule = repo.Find(userId, id);

                repo.Remove(schedule);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void AddClient(int userId, int id, int clientId)
        {
            var scheduleRepo = new ScheduleRepository(_dbContext);
            var clientRepo = new ClientRepository(_dbContext);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var schedule = scheduleRepo.Find(userId, id);
                var client = clientRepo.Find(userId, clientId);

                schedule.Clients.Add(client);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void RemoveClient(int userId, int id, int clientId)
        {
            var scheduleRepo = new ScheduleRepository(_dbContext);
            var clientRepo = new ClientRepository(_dbContext);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var schedule = scheduleRepo.Find(userId, id);
                var client = clientRepo.Find(userId, clientId);

                schedule.Clients.Remove(client);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}

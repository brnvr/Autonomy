using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.Client;
using AutonomyApi.Models.ViewModels.Schedule;
using AutonomyApi.Models.ViewModels.Service;
using AutonomyApi.Repositories;

namespace AutonomyApi.Schedules
{
    public class ScheduleService
    {
        readonly AutonomyDbContext _dbContext;

        public ScheduleService(AutonomyDbContext dbContext) => _dbContext = dbContext;

        public dynamic Get(int userId, ScheduleSearchView search)
        {
            return new ScheduleRepository(_dbContext).Search(userId, search, item => new
            {
                item.Id,
                item.Name,
                item.Date,
                item.Description,
                Service = item.Service == null ? null : new
                {
                    item.Service.Id,
                    item.Service.Name,
                    item.Service.Description
                },
                Clients = item.Clients.Select(client => new
                {
                    client.Id,
                    client.Name
                }).ToList()
            });
        }

        public dynamic Get(int userId, int id)
        {
            return new ScheduleRepository(_dbContext).Find(userId, id, schedule => new
            {
                schedule.Id,
                schedule.Name,
                schedule.Date,
                schedule.CreationDate,
                schedule.Description,
                Service = schedule.Service == null ? null : new
                {
                    schedule.Service.Id,
                    schedule.Service.Name,
                    schedule.Service.Description
                },
                Clients = schedule.Clients.Select(client => new
                {
                    client.Id,
                    client.Name
                }).ToList()
            });
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
                Clients = [],
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
                var schedule = repo.Find(userId, id, schedule => schedule);

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
            using var transaction = _dbContext.Database.BeginTransaction();
            {
                var repo = new ScheduleRepository(_dbContext);
                var schedule = repo.Find(userId, id, item => item);

                repo.Remove(schedule);

                _dbContext.SaveChanges();
                transaction.Commit();
            }
        }

        public void AddClient(int userId, int id, int clientId)
        {
            var scheduleRepo = new ScheduleRepository(_dbContext);
            var clientRepo = new ClientRepository(_dbContext);

            using var transaction = _dbContext.Database.BeginTransaction();

            var schedule = scheduleRepo.Find(userId, id, item => item);
            var client = clientRepo.Find(userId, clientId, client => client);

            schedule.Clients.Add(client);

            _dbContext.SaveChanges();
            transaction.Commit();
        }

        public void RemoveClient(int userId, int id, int clientId)
        {
            var scheduleRepo = new ScheduleRepository(_dbContext);
            var clientRepo = new ClientRepository(_dbContext);

            using var transaction = _dbContext.Database.BeginTransaction();

            var schedule = scheduleRepo.Find(userId, id, schedule => schedule);
            var client = clientRepo.Find(userId, clientId, client => client);

            schedule.Clients.Remove(client);

            _dbContext.SaveChanges();
            transaction.Commit();
        }
    }
}

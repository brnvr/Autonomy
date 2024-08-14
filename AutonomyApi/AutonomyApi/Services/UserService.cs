using AutonomyApi.Database;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.Views.User;
using AutonomyApi.Repositories;

namespace AutonomyApi.Services
{
    public class UserService
    {
        AutonomyDbContext _dbContext;

        public UserService(AutonomyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(UserCreationData data)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(data.Password);

            var user = new User
            {
                Name = data.Name,
                Password = hashedPassword,
                RegistrationDate = DateTime.UtcNow
            };

            new UserRepository(_dbContext).Add(user);

            _dbContext.SaveChanges();

            return user.Id;
        }
    }
}

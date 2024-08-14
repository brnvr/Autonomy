using AutonomyApi.Enums;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.Views.User;
using AutonomyApi.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientDocument> ClientDocuments { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }

        public AutonomyDbContext(DbContextOptions<AutonomyDbContext> options) : base(options) { }

        public void Initialize()
        {
            if (!Users.Any())
            {
                var userService = new UserService(this);

                userService.Create(new UserCreationData
                {
                    Name = "admin",
                    Password = "admin"
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<DocumentType>();
            modelBuilder.HasPostgresEnum<TimeUnit>();

            OnUserModelCreating(modelBuilder.Entity<User>());
            OnClientModelCreating(modelBuilder.Entity<Client>());
            OnClientDocumentModelCreating(modelBuilder.Entity<ClientDocument>());
            OnBudgeModelCreating(modelBuilder.Entity<Budget>());
            OnBudgetItemModelCreating(modelBuilder.Entity<BudgetItem>());

            base.OnModelCreating(modelBuilder);
        }
    }
}

using AutonomyApi.Enums;
using AutonomyApi.Models.Entities;
using AutonomyApi.Models.ViewModels.User;
using AutonomyApi.Services;
using Microsoft.EntityFrameworkCore;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientDocument> ClientDocuments { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ServiceProvided> ServicesProvided { get; set; }
        public DbSet<ServiceProvidedClient> ServiceProvidedClients { get; set; }

        public AutonomyDbContext(DbContextOptions<AutonomyDbContext> options) : base(options) { }

        public void Initialize()
        {
            if (!Users.Any())
            {
                var userService = new UserService(this);

                userService.Create(new UserCreationView
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
            OnBudgetModelCreating(modelBuilder.Entity<Budget>());
            OnBudgetItemModelCreating(modelBuilder.Entity<BudgetItem>());
            OnServiceModelCreating(modelBuilder.Entity<Service>());
            OnCurrencyModelCreating(modelBuilder.Entity<Currency>());
            OnScheduleModelCreating(modelBuilder.Entity<Schedule>());
            OnServiceProvidedModelCreating(modelBuilder.Entity<ServiceProvided>());
            OnServiceProvidedClientModelCreating(modelBuilder.Entity<ServiceProvidedClient>());

            base.OnModelCreating(modelBuilder);
        }
    }
}

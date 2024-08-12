using AutonomyApi.Entities;
using AutonomyApi.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AutonomyApi.Database
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientDocument> ClientDocuments { get; set; }

        public AppDbContext() : base() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(AppSettings.ConnectionString);
            dataSourceBuilder.MapEnum<DocumentType>();
            var dataSource = dataSourceBuilder.Build();

            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseNpgsql(dataSource);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<DocumentType>();

            OnClientModelCreating(modelBuilder.Entity<Client>());
            OnClientDocumentModelCreating(modelBuilder.Entity<ClientDocument>());

            base.OnModelCreating(modelBuilder);
        }
    }
}

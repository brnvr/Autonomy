using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnServiceProvidedModelCreating(EntityTypeBuilder<ServiceProvided> builder)
        {
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.UserId);
            builder
                .HasOne<Service>()
                .WithMany()
                .HasForeignKey(s => s.ServiceId);
            builder
                .Property(s => s.ServiceName)
                .HasMaxLength(Constants.Name.MaxLength);
            builder
                .HasMany(s => s.Clients)
                .WithOne()
                .HasForeignKey(c => c.ServiceProvidedId);
            builder
                .HasOne(s => s.Budget)
                .WithMany()
                .HasForeignKey(s => s.BudgetId);
        }
    }
}

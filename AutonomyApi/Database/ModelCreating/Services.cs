using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnServiceModelCreating(EntityTypeBuilder<Service> builder)
        {
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(serv => serv.UserId);
            builder
                .Property(serv => serv.Name)
                .HasMaxLength(Constants.Name.MaxLength);
            builder
                .HasIndex(serv => serv.Name)
                .IsUnique();
            builder
                .Property(serv => serv.Description)
                .HasMaxLength(Constants.Description.MaxLength);
            builder
                .HasOne<Budget>()
                .WithMany()
                .HasForeignKey(serv => serv.BudgetTemplateId);
        }
    }
}

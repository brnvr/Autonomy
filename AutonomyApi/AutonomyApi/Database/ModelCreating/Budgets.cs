using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnBudgetModelCreating(EntityTypeBuilder<Budget> builder)
        {
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId);
            builder
                .Property(budget => budget.Name)
                .HasMaxLength(Constants.Name.MaxLength);
            builder
                .HasIndex(budget => new { budget.UserId, budget.IsTemplate, budget.Name })
                .IsUnique();
            builder
                .Property(budget => budget.Header)
                .HasMaxLength(Constants.Description.MaxLength);
            builder
                .Property(budget => budget.Footer)
                .HasMaxLength(Constants.Description.MaxLength);
            builder
                .HasMany(budget => budget.Items)
                .WithOne()
                .HasForeignKey(item => item.BudgetId);
        }
    }
}

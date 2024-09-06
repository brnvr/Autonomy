using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnBudgetItemModelCreating(EntityTypeBuilder<BudgetItem> builder)
        {
            builder
                .HasKey(item => new { item.BudgetId, item.Position } );
            builder
                .Property(item => item.Name)
                .HasMaxLength(Constants.Name.MaxLength);
            builder
                .HasIndex(item => new { item.BudgetId, item.Name })
                .IsUnique();
        }
    }
}

using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnCurrencyModelCreating(EntityTypeBuilder<Currency> builder)
        {
            builder
                .Property(item => item.Code)
                .HasMaxLength(3);
            builder
                .Property(item => item.Name)
                .HasMaxLength(20);
            builder
                .Property(item => item.Symbol)
                .HasMaxLength(5);
        }
    }
}

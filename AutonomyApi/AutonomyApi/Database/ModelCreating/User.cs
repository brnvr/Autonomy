using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnUserModelCreating(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(u => u.Name)
                .IsUnique();
            builder
                .Property(u => u.Name)
                .HasMaxLength(Constants.Name.MaxLength);
            builder
                .Property(u => u.Password)
                .HasMaxLength(Constants.Password.HashLength);
        }
    }
}
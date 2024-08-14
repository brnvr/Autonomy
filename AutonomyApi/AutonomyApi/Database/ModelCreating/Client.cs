using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnClientModelCreating(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId);
            builder
                .HasIndex(c => new { c.UserId, c.Name })
                .IsUnique();
            builder
                .Property(c => c.Name)
                .HasMaxLength(Constants.Name.MaxLength);
            builder
                .HasMany(c => c.Documents)
                .WithOne()
                .HasForeignKey(d => d.ClientId);
        }
    }
}

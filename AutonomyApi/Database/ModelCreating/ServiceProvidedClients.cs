using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnServiceProvidedClientModelCreating(EntityTypeBuilder<ServiceProvidedClient> builder)
        {
            builder
                .HasKey(c => c.ServiceProvidedId);
            builder
                .Property(c => c.ServiceProvidedId)
                .ValueGeneratedNever();
            builder
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(c => c.ClientId);
            builder
                .Property(c => c.ClientName)
                .HasMaxLength(Constants.Name.MaxLength);
            builder
                .Property(c => c.ClientDocument)
                .HasMaxLength(Constants.Document.MaxLength);
        }
    }
}

using AutonomyApi.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AppDbContext
    {
        protected void OnClientDocumentModelCreating(EntityTypeBuilder<ClientDocument> builder)
        {
            builder
                .HasKey(d => new { d.ClientId, d.Type });
            builder
                .Property(d => d.Value).HasMaxLength(Constants.Document.MaxLength);
        }
    }
}

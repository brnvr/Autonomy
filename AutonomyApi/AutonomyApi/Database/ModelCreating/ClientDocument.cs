using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnClientDocumentModelCreating(EntityTypeBuilder<ClientDocument> builder)
        {
            builder
                .HasKey(d => new { d.ClientId, d.Type });
            builder
                .Property(d => d.Value)
                .HasMaxLength(Constants.Document.MaxLength);
        }
    }
}

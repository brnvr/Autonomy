using AutonomyApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutonomyApi.Database
{
    public partial class AutonomyDbContext
    {
        protected void OnScheduleModelCreating(EntityTypeBuilder<Schedule> builder)
        {
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(schedule => schedule.UserId);
            builder
                .HasIndex(schedule => new { schedule.UserId, schedule.Date, schedule.Name })
                .IsUnique();
            builder
                .Property(schedule => schedule.Name)
                .HasMaxLength(Constants.Name.MaxLength);
            builder
                .Property(schedule => schedule.Description)
                .HasMaxLength(Constants.Description.MaxLength);
            builder
                .HasMany(schedule => schedule.Clients)
                .WithMany();
            builder
                .HasOne(schedule => schedule.Service)
                .WithMany()
                .HasForeignKey(schedule => schedule.ServiceId);
        }
    }
}

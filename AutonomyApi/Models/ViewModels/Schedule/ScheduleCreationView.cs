using AutonomyApi.WebService.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.Schedule
{
    public class ScheduleCreationView
    {
        [Length(Constants.Name.MinLength, Constants.Name.MaxLength)]
        public required string Name { get; set; }
        [Length(Constants.Description.MinLength, Constants.Description.MaxLength)]
        public string? Description { get; set; }
        public int? ServiceId { get; set; }
        [FutureDate]
        public required DateTime Date { get; set; }
    }
}

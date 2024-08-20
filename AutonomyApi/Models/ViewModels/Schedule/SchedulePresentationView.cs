using AutonomyApi.Models.ViewModels.Client;
using AutonomyApi.Models.ViewModels.Service;

namespace AutonomyApi.Models.ViewModels.Schedule
{
    public class SchedulePresentationView : ScheduleSummaryView
    {
        public required DateTime CreationDate { get; set; }
    }
}

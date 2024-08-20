using AutonomyApi.Models.ViewModels.Client;
using AutonomyApi.Models.ViewModels.Service;

namespace AutonomyApi.Models.ViewModels.Schedule
{
    public class ScheduleSummaryView
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ServiceSummaryView? Service { get; set; }
        public required List<ClientSummaryView> Clients { get; set; }
        public required DateTime Date { get; set; }
    }
}

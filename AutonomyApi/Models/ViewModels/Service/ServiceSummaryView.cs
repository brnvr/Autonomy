using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.Service
{
    public class ServiceSummaryView
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}

using AutonomyApi.WebService.ValidationAttributes;

namespace AutonomyApi.Models.ViewModels.Service
{
    public class ServiceProvideView
    {
        [PastDate]
        public required DateTime Date { get; set; }
        public required int[] ClientIds { get; set; }
    }
}

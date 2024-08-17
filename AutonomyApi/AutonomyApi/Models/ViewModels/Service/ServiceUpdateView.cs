using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.Service
{
    public class ServiceUpdateView : ServiceCreationView
    {
        [Length(Constants.Description.MinLength, Constants.Description.MaxLength)]
        public string? Description { get; set; }
    }
}

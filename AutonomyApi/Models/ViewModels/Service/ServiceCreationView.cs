using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.Service
{
    public class ServiceCreationView
    {
        [Length(Constants.Name.MinLength, Constants.Name.MaxLength)]
        public required string Name { get; set; }
    }
}

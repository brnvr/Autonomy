using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.ViewModels.Client
{
    public class ClientCreationView 
    {
        [Length(Constants.Name.MinLength, Constants.Name.MaxLength)]
        public required string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Models.Dtos.Client
{
    public class ClientUpdateData
    {
        [MinLength(Constants.Name.MinLength)]
        [MaxLength(Constants.Name.MaxLength)]
        public required string Name { get; set; }
    }
}

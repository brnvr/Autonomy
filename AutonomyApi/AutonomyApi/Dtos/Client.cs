using System.ComponentModel.DataAnnotations;

namespace AutonomyApi.Dtos
{
    public class UpdateClientDto
    {
        [MinLength(Constants.Name.MinLength)]
        [MaxLength(Constants.Name.MaxLength)]
        public required string Name { get; set; }
    }

    public class InsertClientDto : UpdateClientDto { }

    public class ClientOverview
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
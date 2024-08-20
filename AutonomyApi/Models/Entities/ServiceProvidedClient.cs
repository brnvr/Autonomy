namespace AutonomyApi.Models.Entities
{
    public class ServiceProvidedClient
    {
        public int ServiceProvidedId { internal get; set; }
        public int? ClientId { get; set; }
        public required string ClientName { get; set; }
        public required string ClientDocument { get; set; }
    }
}
